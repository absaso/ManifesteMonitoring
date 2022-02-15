Imports System.Data
Imports System
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Web.Security
Public Class InfosPerso2
    Inherits System.Web.UI.Page
    Private ChaineDeConnexion As String = Configuration.ConfigurationManager.AppSettings("ChaineDeConnexion").ToString

    Private Function OuvertureConnexion(ByRef ObjetConnexion As System.Data.SqlClient.SqlConnection) As Boolean
        Try
            ObjetConnexion.ConnectionString = ChaineDeConnexion
            ObjetConnexion.Open()
            OuvertureConnexion = True
        Catch ex As Exception
            OuvertureConnexion = False
            AfficherMessage(ex.Message)

        End Try
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Session("S_NOM") Is Nothing Then
                    Response.Redirect("Connexion.aspx")
                End If

                TextNom.Enabled = False
                TextPrenom.Enabled = False
                TextEmail.Enabled = False
                TextTel.Enabled = False
                TextLogin.Enabled = False
                TextMDP.Enabled = False
                SaveModifications.Enabled = False

                GetInfo()
            End If
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try


    End Sub

    Private Sub GetInfo()
        Try
            Dim UneConnexion As New SqlConnection
            Dim requete As New SqlCommand
            Dim UneRequete As String = ""
            Dim UnCritere As String = ""
            Dim UneCommand As New SqlCommand
            Dim UnReader As SqlDataReader

            UneRequete = "select * from USERS where IDUSER = '" & Session("S_ID") & "'"

            If OuvertureConnexion(UneConnexion) Then
                UneCommand = New SqlCommand(UneRequete, UneConnexion)
                UnReader = UneCommand.ExecuteReader
                UnReader.Read()
                TextNom.Text = UnReader("nom")
                TextPrenom.Text = UnReader("prenom")
                TextEmail.Text = UnReader("email")
                TextTel.Text = UnReader("telephone")
                TextLogin.Text = UnReader("login")
                ' TextMDP.Text = Decrypt(UnReader("password"))
            Else
                AfficherMessage("Echec de connexion")
            End If



        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try
    End Sub

    Private Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    Private Sub AfficherMessage(ByVal Message As String)
        Message = Message.Replace("'", " ")
        Response.Write("<script type='text/javascript' language='javascript'> alert(""" & Message & """);</script>")
    End Sub

    Protected Sub Modif_Click(sender As Object, e As EventArgs) Handles Modif.Click
        Try
            TextNom.Enabled = True
            TextPrenom.Enabled = True
            TextEmail.Enabled = True
            TextTel.Enabled = True
            TextLogin.Enabled = True
            TextMDP.Enabled = True
            SaveModifications.Enabled = True
            Modif.Enabled = False
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub

    Protected Sub SaveModifications_Click(sender As Object, e As EventArgs) Handles SaveModifications.Click
        Try
            Dim NewNom As String = ""
            Dim NewPrenom As String = ""
            Dim NewEmail As String = ""
            Dim NewTelephone As String = ""
            Dim NewLogin As String = ""
            Dim NewMDP As String = ""

            NewNom = TextNom.Text.Replace("'", "''")
            'NewNom = Request.Form("TextNom")
            If NewNom.Trim = "" Then
                AfficherMessage("Saisir un nom")
                Exit Sub
            End If

            NewPrenom = TextPrenom.Text.Replace("'", "''")
            If NewPrenom.Trim = "" Then
                AfficherMessage("Saisir un prénom")
                Exit Sub
            End If

            NewEmail = TextEmail.Text.Replace("'", "''")
            If NewEmail.Trim = "" Then
                AfficherMessage("Saisir un email")
                Exit Sub
            End If

            NewTelephone = TextTel.Text.Replace("'", "''")
            If NewTelephone.Trim = "" Then
                AfficherMessage("Saisir un numéro de téléphone")
                Exit Sub
            End If

            NewLogin = TextLogin.Text.Replace("'", "''")
            If NewLogin.Trim = "" Then
                AfficherMessage("Saisir un login")
                Exit Sub
            End If

            NewMDP = TextMDP.Text.Replace("'", "''")
            If NewMDP.Trim = "" Then
                AfficherMessage("Saisir un mot de passe")
                Exit Sub
            End If

            UpdateUser(NewNom, NewPrenom, NewEmail, NewTelephone, NewLogin, NewMDP)
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub

    Private Sub UpdateUser(ByVal NewNom As String, ByVal NewPrenom As String, ByVal NewEmail As String, ByVal NewTelephone As String, ByVal NewLogin As String, ByVal NewMDP As String)
        Try
            Dim UneConnexion As New SqlConnection
            Dim UneCommand As New SqlCommand
            Dim UneRequete As String, Resultat As Int16
            Dim MotDepass As String = Encrypt(NewMDP)

            If OuvertureConnexion(UneConnexion) Then
                UneRequete = "UPDATE USERS set nom = '" & NewNom & "', prenom = '" & NewPrenom & "', email = '" & NewEmail & "', telephone = '" & NewTelephone & "', login = '" & NewLogin & "', password = '" & MotDepass & "' where IDUSER = " & Session("S_ID") & " "
                UneCommand = New SqlCommand(UneRequete, UneConnexion)
                Resultat = UneCommand.ExecuteNonQuery()
                If Resultat >= 1 Then
                    AfficherMessage("Modifications prises en compte")
                    SaveModifications.Enabled = False
                    Modif.Enabled = True
                    GetInfo()
                Else
                    AfficherMessage("Aucune mise à jour effectuee")
                End If

            Else
                AfficherMessage("echec de connexion")
            End If

            UneCommand.Dispose()
            UneCommand = Nothing

            If UneConnexion.State = ConnectionState.Open Then UneConnexion.Close()
            UneConnexion = Nothing
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try
    End Sub
End Class