Imports System.Data
Imports System
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Web.Security
Imports System.IO
Imports System.Drawing

Public Class Suivi2
    Inherits System.Web.UI.Page
    Private ChaineDeConnexion As String = Configuration.ConfigurationManager.AppSettings("ChaineDeConnexion").ToString

    'connection àla db
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Not Me.IsPostBack) Then
                If Session("S_NOM") Is Nothing Then
                    Response.Redirect("Connexion.aspx")
                End If

                Dim DateDebut As String
                Dim EndDate As String = "", NomNavire As String = "", NumeroVoyage As String = "", Entreprise As String = "", NumeroManifeste As String = ""
                DateDebut = "01/" & Month(Now) & "/" & Year(Now).ToString
                GetData_2(DateDebut, EndDate, NomNavire, NumeroVoyage, Entreprise, NumeroManifeste)
                GetListConsignataire()

            End If
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try


    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        btnRechercher_Click(sender, e)

    End Sub


    Protected Sub btnRechercher_Click(sender As Object, e As EventArgs) Handles btnRechercher.Click
        Dim UneDateDebut As String = "", UneDatefin As String = "", NomNavire As String = "", NumeroVoyage As String = "", Entreprise As String = "", NumeroManifeste As String = ""

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
            If txtNomNavire.Text.Trim <> "" Then
                NomNavire = txtNomNavire.Text.Replace("'", "''")
            End If
            If txtNumeroVoyage.Text.Trim <> "" Then
                NumeroVoyage = txtNumeroVoyage.Text.Replace("'", "''")
            End If
            If Not txtNomEntreprise.SelectedValue = -1 Then
                Entreprise = txtNomEntreprise.SelectedValue
            Else
                Entreprise = ""
            End If
            If txtNumeroManifeste.Text.Trim <> "" Then
                NumeroManifeste = txtNumeroManifeste.Text.Replace("'", "''")
            End If
            GetData_2(UneDateDebut, UneDatefin, NomNavire, NumeroVoyage, Entreprise, NumeroManifeste)
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub

    Private Sub GetListConsignataire()
        Try
            Dim UneConnexion As New SqlConnection
            Dim UneCommand As New SqlCommand
            Dim UnReader As SqlDataReader
            Dim UneRequete As String

            UneRequete = "select Libelle, ID from V_ENTREPRISES where SecteurActivite = 'CONS' order by Libelle"

            If OuvertureConnexion(UneConnexion) Then
                UneCommand = New SqlCommand(UneRequete, UneConnexion)
                UnReader = UneCommand.ExecuteReader
                txtNomEntreprise.DataSource = UnReader
                txtNomEntreprise.DataTextField = "Libelle"
                txtNomEntreprise.DataValueField = "ID"
                txtNomEntreprise.DataBind()
                txtNomEntreprise.Items.Insert(0, New ListItem("--Selection--", "-1"))
            Else
                AfficherMessage("Echec de connexion")
            End If

            If UneConnexion.State = ConnectionState.Open Then UneConnexion.Close()
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub

    Private Sub GetData_2(ByVal UneDateDebut As String, ByVal UneDateFin As String, ByVal NomNavire As String, ByVal NumeroVoyage As String, ByVal Entreprise As String, ByVal NumeroManifeste As String)
        Try
            Dim UneConnexion As New SqlConnection
            Dim requete As New SqlCommand
            Dim dt As New DataTable
            Dim sda As New SqlDataAdapter
            Dim UneRequete As String = ""
            Dim LocalDate As Date
            Dim UnCritere As String = ""


            If UneDateDebut.ToString.Trim <> "" And IsDate(UneDateDebut) Then
                UnCritere = " AND DATETRANSMISSION>='" & UneDateDebut & "'"
            End If
            If UneDateFin.ToString.Trim <> "" And IsDate(UneDateFin) Then
                LocalDate = DateAdd(DateInterval.Day, 1, Convert.ToDateTime(txtDatefin.Text)).ToString("dd/MM/yyyy")
                UnCritere = UnCritere & " AND DATETRANSMISSION < '" & LocalDate & "'"
            End If
            If txtNomNavire.Text.Trim <> "" Then
                UnCritere = UnCritere & " AND NOM_NAVIRE LIKE '" & NomNavire & "%'"
            End If
            If txtNumeroVoyage.Text.Trim <> "" Then
                UnCritere = UnCritere & "AND manifNumVolVoyage LIKE '" & NumeroVoyage & "%'"
            End If
            If Entreprise.Trim <> "" Then
                UnCritere = UnCritere & " AND EN.ID = " & Entreprise
            End If
            If txtNumeroManifeste.Text.Trim <> "" Then
                UnCritere = UnCritere & "AND manifNumEnregManif LIKE '" & NumeroManifeste & "%'"
            End If
            UneRequete = "select  manifNumEnregManif,convert(varchar,manifDateArrivee,103) manifDateArrivee,DATEENR, manifNbreArticle,manifNumVolVoyage, NOM_NAVIRE,NOM_COMPAGNIE,EN.Libelle ENTREPRISE,DATETRANSMISSION " &
                " from V_SEGMENTGEN SEG INNER JOIN V_ENTREPRISES EN ON SEG.ENTREPRISE=EN.ID where 1=1 " & UnCritere & " order by DATETRANSMISSION desc"

            If OuvertureConnexion(UneConnexion) Then
                'Affichage du résultat de la recherche de l'utilisateur
                requete.CommandText = UneRequete
                requete.Connection = UneConnexion
                sda.SelectCommand = requete
                sda.Fill(dt)
                GridView1.DataSource = dt
                GridView1.DataBind()

                If Not (GridView1.Rows.Count = 0) Then
                    GridView1.FooterRow.Cells(0).Text = GridView1.Rows.Count.ToString()
                    Dim SumBL As Integer = dt.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("manifNbreArticle"))
                    GridView1.FooterRow.Cells(3).Text = SumBL.ToString("N0")
                End If
            Else
                AfficherMessage("Echec de connexion")
            End If

            If UneConnexion.State = ConnectionState.Open Then UneConnexion.Close()

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
        Response.AddHeader("content-disposition", "attachment;filename=Suivi_Manifestes.xls")
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