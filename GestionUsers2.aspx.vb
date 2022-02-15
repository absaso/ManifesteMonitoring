Imports System.Data
Imports System
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Web.Security

Public Class GestionUsers2
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

                SaveModifUser.Enabled = False
            End If
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub

    Protected Sub AddUser_Click(sender As Object, e As EventArgs) Handles AddUser.Click
        Try
            Dim NewNom As String = ""
            Dim NewPrenom As String = ""
            Dim NewLogin As String = ""
            Dim NewPassword As String = ""
            Dim NewEmail As String = ""
            Dim NewTelephone As String = ""
            Dim NewProfile As String = ""

            NewNom = txtNomNewUser.Text.Replace("'", "''")
            If NewNom.Trim = "" Then
                AfficherMessage("Saisir un nom")
                Exit Sub
            End If

            NewPrenom = txtPrenomNewUser.Text.Replace("'", "''")
            If NewPrenom.Trim = "" Then
                AfficherMessage("Saisir un prénom")
                Exit Sub
            End If

            NewLogin = txtLoginNewUser.Text.Replace("'", "''")
            If NewLogin.Trim = "" Then
                AfficherMessage("Saisir un identifiant")
                Exit Sub
            End If

            NewPassword = txtPasswordNewUser.Text.Replace("'", "''")
            If NewPassword.Trim = "" Then
                AfficherMessage("saisir un mot de passe")
                Exit Sub
            End If
            NewPassword = Encrypt(NewPassword)

            NewEmail = txtEmailNewUser.Text.Replace("'", "''")
            If NewEmail.Trim = "" Then
                AfficherMessage("Saisir un email")
                Exit Sub
            End If

            NewTelephone = txtTelephoneNewUser.Text.Replace("'", "''")
            If NewTelephone.Trim = "" Then
                AfficherMessage("Saisir un numéro de téléphone")
                Exit Sub
            End If

            If DropDownProfile.SelectedValue = "admin" Then
                NewProfile = DropDownProfile.SelectedValue.Replace("'", "''")
            ElseIf DropDownProfile.SelectedValue = "normal" Then
                NewProfile = DropDownProfile.SelectedValue.Replace("'", "''")
            Else
                AfficherMessage("Choisissez un profile")
            End If

            If (CheckProfile(NewNom, NewPrenom) Or CheckLogin(NewLogin)) = True Then
                Exit Sub
            ElseIf (CheckProfile(NewNom, NewPrenom) And CheckLogin(NewLogin)) = False Then
                InsertUser(NewNom, NewPrenom, NewLogin, NewPassword, NewEmail, NewTelephone, NewProfile)
            End If
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub

    Private Function CheckProfile(newNom As String, newPrenom As String) As Boolean
        'on vérifie si l'utilisateur a déjà créé un profile
        Try
            Dim UneConnexion As New SqlConnection
            Dim UneCommand As New SqlCommand
            Dim UnReader As SqlDataReader
            Dim UneRequete As String

            UneRequete = "select * from USERS where nom = '" & newNom & "' and prenom = '" & newPrenom & "'"

            If OuvertureConnexion(UneConnexion) Then
                UneCommand = New SqlCommand(UneRequete, UneConnexion)
                UnReader = UneCommand.ExecuteReader
                If UnReader.HasRows Then
                    AfficherMessage("Profile existant: Vous vous êtes déjà identifié sous ce nom")
                    Return True
                    Exit Function
                End If
            Else
                AfficherMessage("Echec de connexion")
            End If

            UneCommand.Dispose()
            UneCommand = Nothing
            If UneConnexion.State = ConnectionState.Open Then UneConnexion.Close()
            UneConnexion = Nothing

        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

        Return False
    End Function

    Private Sub InsertUser(ByVal NewNom As String, ByVal NewPrenom As String, ByVal NewLogin As String, ByVal NewPassword As String, ByVal NewEmail As String, ByVal NewTelephone As String, ByVal NewProfile As String)
        Try
            Dim UneConnexion As New SqlConnection
            Dim UneCommand As New SqlCommand
            Dim UneRequete As String

            If OuvertureConnexion(UneConnexion) Then
                UneRequete = "insert into USERS (nom,prenom,login,password,email,telephone,profile) values ('" & NewNom & "','" & NewPrenom &
                    "','" & NewLogin & "','" & NewPassword & "','" & NewEmail & "','" & NewTelephone & "','" & NewProfile & "')"

                UneCommand = New SqlCommand(UneRequete, UneConnexion)
                UneCommand.ExecuteNonQuery()
                ChargerGrid("")

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

    Private Function CheckLogin(ByVal NewLogin As String) As Boolean
        'on vérifie si l'identifiant existe déjà
        Try
            Dim UneConnexion As New SqlConnection
            Dim UneCommand As New SqlCommand
            Dim UnReader As SqlDataReader
            Dim UneRequete As String

            UneRequete = "select * from USERS where login = '" & NewLogin & "'"

            If OuvertureConnexion(UneConnexion) Then
                UneCommand = New SqlCommand(UneRequete, UneConnexion)
                UnReader = UneCommand.ExecuteReader
                If UnReader.HasRows Then
                    AfficherMessage("Cet identifiant est déjà pris")
                    Return True
                    Exit Function
                End If
            Else
                AfficherMessage("Echec de connexion")
            End If

            UneCommand.Dispose()
            UneCommand = Nothing
            If UneConnexion.State = ConnectionState.Open Then UneConnexion.Close()
            UneConnexion = Nothing

        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

        Return False

    End Function
    Private Sub AfficherMessage(ByVal Message As String)
        Message = Message.Replace("'", " ")
        Response.Write("<script type='text/javascript' language='javascript'> alert(""" & Message & """);</script>")
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

    Protected Sub btnRechercher_Click(sender As Object, e As EventArgs) Handles btnRechercher.Click
        Try
            Dim NewNom As String = "", NewPrenom As String = ""

            If txtSearchNom.Text.Trim <> "" Then
                NewNom = txtSearchNom.Text.Replace("'", "''")
            End If

            If txtSearchPrenom.Text.Trim <> "" Then
                NewPrenom = txtSearchPrenom.Text.Replace("'", "''")
            End If

            RechercheUser(NewNom, NewPrenom)
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub

    Private Sub RechercheUser(ByVal NewNom As String, ByVal NewPrenom As String)
        Try
            Dim requete As New SqlCommand
            Dim UneRequete As String = ""
            Dim UnCritere As String = ""

            If txtSearchNom.Text.Trim <> "" Then
                UnCritere = UnCritere & "AND nom LIKE '" & NewNom & "%'"
            End If
            If txtSearchPrenom.Text.Trim <> "" Then
                UnCritere = UnCritere & "AND prenom LIKE '" & NewPrenom & "%'"
            End If

            UneRequete = "select * from USERS where 1=1 " & UnCritere & " order by nom asc"
            'SqlDataSourceUsers.SelectCommand = UneRequete
            ChargerGrid(UneRequete)

        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try
    End Sub

    Private Sub ChargerGrid(ByVal UneRequete As String)
        Try
            If UneRequete.Trim = "" Then
                UneRequete = "SELECT * FROM USERS ORDER BY DATECREATION asc"
            End If
            SqlDataSourceUsers.SelectCommand = UneRequete
            'GridView1.DataSource = SqlDataSourceUsers
            With GridView1
                .PageSize = 10
                .PageIndex = 0
                .DataBind()
            End With
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try


    End Sub

    'Protected Sub ClearBox_Click(sender As Object, e As EventArgs) Handles ClearBox.Click
    '    txtNomNewUser.Text = ""
    '    txtPrenomNewUser.Text = ""
    '    txtLoginNewUser.Text = ""
    '    txtEmailNewUser.Text = ""
    '    txtTelephoneNewUser.Text = ""
    '    txtPasswordNewUser.Text = ""

    '    txtLoginNewUser.Enabled = True
    '    txtPasswordNewUser.Enabled = True
    '    SaveModifUser.Enabled = False
    'End Sub

    Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Try
            SaveModifUser.Enabled = True

            Dim Nom As String = CType(GridView1.Rows(e.NewSelectedIndex).Cells(0).Text, String)
            Dim Prenom As String = CType(GridView1.Rows(e.NewSelectedIndex).Cells(1).Text, String)
            Dim Email As String = CType(GridView1.Rows(e.NewSelectedIndex).Cells(2).Text, String)
            Dim Telephone As String = CType(GridView1.Rows(e.NewSelectedIndex).Cells(3).Text, String)
            Dim Profile As String = CType(GridView1.Rows(e.NewSelectedIndex).Cells(4).Text, String)

            txtNomNewUser.Text = Nom : txtPrenomNewUser.Text = Prenom : txtEmailNewUser.Text = Email
            txtTelephoneNewUser.Text = Telephone
            txtLoginNewUser.Enabled = False
            txtPasswordNewUser.Enabled = False

            If Profile = "admin" Then
                DropDownProfile.SelectedValue = "admin"
            ElseIf Profile = "normal" Then
                DropDownProfile.SelectedValue = "normal"
            End If
        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try


    End Sub

    Protected Sub SaveModifUser_Click(sender As Object, e As EventArgs) Handles SaveModifUser.Click
        Try

            Dim NewNom As String = ""
        Dim NewPrenom As String = ""
        Dim NewEmail As String = ""
        Dim NewTelephone As String = ""
        Dim NewProfile As String = ""

        NewNom = txtNomNewUser.Text.Replace("'", "''")
        If NewNom.Trim = "" Then
            AfficherMessage("Saisir un nom")
            Exit Sub
        End If

        NewPrenom = txtPrenomNewUser.Text.Replace("'", "''")
        If NewPrenom.Trim = "" Then
            AfficherMessage("Saisir un prénom")
            Exit Sub
        End If

        NewEmail = txtEmailNewUser.Text.Replace("'", "''")
        If NewEmail.Trim = "" Then
            AfficherMessage("Saisir un email")
            Exit Sub
        End If

        NewTelephone = txtTelephoneNewUser.Text.Replace("'", "''")
        If NewTelephone.Trim = "" Then
            AfficherMessage("Saisir un numéro de téléphone")
            Exit Sub
        End If

        If DropDownProfile.SelectedValue = "admin" Then
            NewProfile = DropDownProfile.SelectedValue.Replace("'", "''")
        ElseIf DropDownProfile.SelectedValue = "normal" Then
            NewProfile = DropDownProfile.SelectedValue.Replace("'", "''")
        Else
            AfficherMessage("Choisissez un profile")
        End If

            UpdateUser(NewNom, NewPrenom, NewEmail, NewTelephone, NewProfile)

        Catch ex As Exception
            AfficherMessage(ex.Message)
        End Try

    End Sub

    Private Sub UpdateUser(ByVal NewNom As String, ByVal NewPrenom As String, ByVal NewEmail As String, ByVal NewTelephone As String, ByVal NewProfile As String)
        Try
            Dim UneConnexion As New SqlConnection
            Dim UneCommand As New SqlCommand
            Dim UneRequete As String


            If OuvertureConnexion(UneConnexion) Then
                UneRequete = "update USERS set nom = '" & NewNom & "', prenom = '" & NewPrenom & "', email = '" & NewEmail & "', telephone = '" & NewTelephone & "', profile = '" & NewProfile & "' where nom = '" & NewNom & "' and prenom = '" & NewPrenom & "'"

                UneCommand = New SqlCommand(UneRequete, UneConnexion)
                UneCommand.ExecuteNonQuery()
                ChargerGrid("")

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