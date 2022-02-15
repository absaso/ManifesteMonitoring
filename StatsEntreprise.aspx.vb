Imports System.Data
Imports System
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Web.UI.DataVisualization.Charting
Imports System.Web.Security
Imports System.IO
Imports System.Drawing

Public Class StatsEntreprise
    Inherits System.Web.UI.Page
    Private ChaineDeConnexion As String = Configuration.ConfigurationManager.AppSettings("ChaineDeConnexion").ToString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Not Me.IsPostBack) Then
                If Session("S_NOM") Is Nothing Then
                    Response.Redirect("Connexion.aspx")
                End If

                Dim DateDebut As String
                Dim DateFin As String = ""
                DateDebut = "01/" & Month(Now) & "/" & Year(Now).ToString
                getList(DateDebut, DateFin)
            End If
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub
    Private Function OuvertureConnexion(ByRef ObjetConnexion As System.Data.SqlClient.SqlConnection) As Boolean
        Try
            ObjetConnexion.ConnectionString = ChaineDeConnexion
            ObjetConnexion.Open()
            OuvertureConnexion = True
        Catch ex As Exception
            OuvertureConnexion = False
            'MsgBox(ex.Message)
            AfficherMessage(ex.Message)

        End Try
    End Function
    Private Sub getList(ByVal UneDateDebut As String, ByVal UneDateFin As String)
        Try
            Dim UneConnexion As New SqlConnection
            Dim requete As New SqlCommand
            Dim dt As New DataTable
            Dim sda As New SqlDataAdapter
            Dim UnCritere As String = ""
            Dim UneRequete As String = ""
            Dim LocalDate As Date

            If UneDateDebut.ToString.Trim <> "" And IsDate(UneDateDebut) Then
                UnCritere = " AND DATETRANSMISSION>='" & UneDateDebut & "'"
            End If
            If UneDateFin.ToString.Trim <> "" And IsDate(UneDateFin) Then
                LocalDate = DateAdd(DateInterval.Day, 1, Convert.ToDateTime(txtDatefin.Text)).ToString("dd/MM/yyyy")
                UnCritere = UnCritere & " AND DATETRANSMISSION < '" & LocalDate & "'"
            End If

            UneRequete = "Select Format(sg.DATETRANSMISSION,'dd-MM-yyyy') as 'DateTransmission', ve.libelle,count(*) as NbManifeste,sum(sg.manifNbreArticle) [NbrBL]
            From V_SEGMENTGEN sg inner Join v_entreprises ve on sg.entreprise=ve.id Where 1 = 1" & UnCritere & " group by FORMAT( sg.DATETRANSMISSION,'dd-MM-yyyy'), ve.libelle order by 1, 2 "

            If OuvertureConnexion(UneConnexion) Then
                requete.CommandText = UneRequete
                requete.Connection = UneConnexion
                sda.SelectCommand = requete
                sda.Fill(dt)
                GridView1.DataSource = dt
                GridView1.DataBind()

                If Not (GridView1.Rows.Count = 0) Then
                    Dim SumMnf As Integer = dt.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("NbManifeste"))
                    Dim SumBL As Integer = dt.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("NbrBL"))
                    GridView1.FooterRow.Cells(2).Text = SumMnf.ToString("N0")
                    GridView1.FooterRow.Cells(3).Text = SumBL.ToString("N0")
                End If

            End If

            If UneConnexion.State = ConnectionState.Open Then UneConnexion.Close()

        Catch ex As Exception
            AfficherMessage(ex.Message)

        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        btnRechercher_Click(sender, e)
    End Sub
    Protected Sub btnRechercher_Click(sender As Object, e As EventArgs) Handles btnRechercher.Click
        Dim UneDateDebut As String = "", UneDatefin As String = ""
        Try
            If txtDatedeb.Text.Trim <> "" Then
                If IsDate(txtDatedeb.Text) Then
                    UneDateDebut = Convert.ToDateTime(txtDatedeb.Text).ToString("dd/MM/yyyy")
                End If
            Else
                UneDateDebut = "01/" & Month(Now) & "/" & Year(Now)
            End If
            If txtDatefin.Text.Trim <> "" Then
                If IsDate(txtDatefin.Text) Then
                    UneDatefin = Convert.ToDateTime(txtDatefin.Text).ToString("dd/MM/yyyy")
                End If
            End If
            getList(UneDateDebut, UneDatefin)
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub
    Private Sub AfficherMessage(ByVal Message As String)
        Message = Message.Replace("'", " ")
        Response.Write("<script type='text/javascript' language='javascript'> alert(""" & Message & """);</script>")
    End Sub

    Protected Sub ExportTab_Click(sender As Object, e As EventArgs) Handles ExportTab.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Reporting_Manifestes.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            GridView1.AllowPaging = False
            btnRechercher_Click(sender, e)

            GridView1.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In GridView1.HeaderRow.Cells
                cell.BackColor = GridView1.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In GridView1.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = GridView1.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            GridView1.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered 
    End Sub

End Class