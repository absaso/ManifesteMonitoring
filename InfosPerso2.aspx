<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="InfosPerso2.aspx.vb" Inherits="WebApp_Manifmonitoring.InfosPerso2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="page_title" style= "font-size:25px;font-weight:600;color:#3d8328;margin-left:2%;margin-top:20px;font-family:'Times New Roman'" runat="server" Text="Mes informations personnelles"></asp:Label>

            <table class="BoxInfos" style="width: 75%;">
                <tr>
                    <td>Nom</td>
                    <td><asp:TextBox ID="TextNom" CssClass="inputInfo" MaxLength="30" runat="server"></asp:TextBox></td>
                    <td>Prénom</td>
                    <td><asp:TextBox ID="TextPrenom" CssClass="inputInfo" MaxLength="50" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td><asp:TextBox ID="TextEmail" CssClass="inputInfo" TextMode="Email" MaxLength="50" runat="server"></asp:TextBox></td>
                    <td>Téléphone</td>
                    <td><asp:TextBox ID="TextTel" CssClass="inputInfo" TextMode="Phone" MaxLength="15" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Login</td>
                    <td><asp:TextBox ID="TextLogin" CssClass="inputInfo" MaxLength="30" runat="server"></asp:TextBox></td>
                    <td>Mot de Passe</td>
                    <td><asp:TextBox ID="TextMDP" CssClass="inputInfo" TextMode="Password" MaxLength="30" runat="server"></asp:TextBox></td>
                </tr>
                <tr><td colspan="4">             
                        <asp:Button ID="Modif" CssClass="btnInfo" runat="server" Text="Activer les modifications" />
                        <asp:Button ID="SaveModifications" CssClass="btnInfo" runat="server" Text="Enregistrer" />
                    </td>
                </tr>
            </table>
</asp:Content>
