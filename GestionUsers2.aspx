<%@ Page Title="Gestion Utilisateurs" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="GestionUsers2.aspx.vb" Inherits="WebApp_Manifmonitoring.GestionUsers2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="page_title" style= "font-size:25px;font-weight:600;color:#3d8328;margin-left:2%;margin-top:20px;font-family:'Times New Roman'" runat="server" Text="Gestion des utilisateurs"></asp:Label>
    <div class="Content2_GestUsr">
        
    <div id="ExistingUsers">
             <table class="BoxRecherche">
                    <tr><td class="TitleSearchBox" ><asp:Label ID="Label1" class="LabelTitleSearchBox" style="font-weight: 200" runat="server" Text="Nom"></asp:Label></td> 
                        <td class="TitleSearchBox"><asp:Label ID="Label2" class="LabelTitleSearchBox" runat="server" Text="Prénom(s)"></asp:Label></td>
                    </tr>   
                    <tr><td class="SearchTextBox"><asp:TextBox ID="txtSearchNom" class="SearchBox" MaxLength="30" runat="server" ></asp:TextBox></td> 
                        <td class="SearchTextBox"><asp:TextBox ID="txtSearchPrenom" class="SearchBox" MaxLength="50" runat="server" ></asp:TextBox></td>
                    </tr> 
                    <tr>
                        <td colspan="2" class="BoxRechercheBtn">            
                            
                        <asp:Button ID="btnRechercher" runat="server" Text="Chercher" CssClass="RechercheBtn"  /></td>
                    </tr>
                </table>  
        

             <asp:GridView ID="GridView1" runat="server" EmptyDataText="Aucune ligne à afficher" style="text-align:center" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSourceUsers" ShowFooter="True" CssClass="GridExistingUsers">

                 <AlternatingRowStyle BackColor="#D2F0EF" />

                 <Columns>
                     <asp:BoundField DataField="nom" HeaderText="Nom" SortExpression="nom" />
                     <asp:BoundField DataField="prenom" HeaderText="Prénom(s)" SortExpression="prenom" />
                     <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                     <asp:BoundField DataField="telephone" HeaderText="Téléphone" SortExpression="telephone" />
                     <asp:BoundField DataField="profile" HeaderText="Profile" SortExpression="profile" />
                     <asp:BoundField DataField="DATECREATION" HeaderText="Date de Création" SortExpression="DATECREATION" />                    
                     <asp:CommandField HeaderText="Action"  SelectText="Sel" ShowSelectButton="true"  ShowCancelButton="False"  />
                 </Columns>
                 <FooterStyle BackColor="#39A6A0" Font-Bold="True" Font-Names="Times New Roman" />
                                <HeaderStyle BackColor="#71CECA" Font-Bold="True" Font-Italic="False" Font-Names="Times New Roman" />
                                <RowStyle  Height="50px" Width="90px" />
             </asp:GridView>
            
             <asp:SqlDataSource ID="SqlDataSourceUsers" runat="server" ConnectionString="<%$ ConnectionStrings:MANIFESTE_DBConnectionString %>" SelectCommand="SELECT nom,prenom,email,telephone,profile,DATECREATION,IDUSER FROM [USERS]"></asp:SqlDataSource>
            

        </div>
        

        <table class="BoxManageUser">
                 <tr>
                     <td>Nom</td>
                     <td>
                         <asp:TextBox ID="txtNomNewUser" class="inputBoxManage" MaxLength="30" runat="server"></asp:TextBox>
                     </td>
                     <td >Prénom(s)</td>
                     <td>
                         <asp:TextBox ID="txtPrenomNewUser" class="inputBoxManage" MaxLength="50" runat="server"></asp:TextBox>
                     </td>
                     <td >Téléphone</td>
                     <td>
                         <asp:TextBox ID="txtTelephoneNewUser" class="inputBoxManage" TextMode="Phone" MaxLength="15" runat="server"></asp:TextBox>
                     </td>
                     <td rowspan="2" class="ChoixProfile" >Profile <br />
                         <asp:DropDownList ID="DropDownProfile" runat="server" CssClass="SelectProfile">
                             <asp:ListItem Value="admin">Administrateur</asp:ListItem>
                             <asp:ListItem Value="normal">Normal</asp:ListItem>
                         </asp:DropDownList>
                         <br />
                    </td>
                 </tr>
                 <tr>
                     <td>Email</td>
                     <td>
                         <asp:TextBox ID="txtEmailNewUser" class="inputBoxManage" TextMode="Email" MaxLength="50" runat="server"></asp:TextBox>
                     </td>
                     <td >Identifiant</td>
                     <td >
                         <asp:TextBox ID="txtLoginNewUser" class="inputBoxManage" MaxLength="30" runat="server" ></asp:TextBox>
                     </td>
                     
                     <td>Mot de passe</td>
                     <td >
                         <asp:TextBox ID="txtPasswordNewUser" class="inputBoxManage" TextMode="Password" MaxLength="30" runat="server" ></asp:TextBox>
                     </td>
                 </tr>
                 <tr class="btnBoxManage">
                     <td colspan="7" >
                         <asp:Button ID="AddUser" CssClass="BBM" runat="server" Text="Ajouter" />
                         <asp:Button ID="SaveModifUser" CssClass="BBM" runat="server" Text="Enregistrer les modifications" />
                     </td>
                    
                 </tr>
             </table>
        </div>
      
</asp:Content>
