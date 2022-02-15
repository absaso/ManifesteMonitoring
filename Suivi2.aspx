<%@ Page Title="Suivi Manifeste" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Suivi2.aspx.vb" Inherits="WebApp_Manifmonitoring.Suivi2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" style= "font-size:25px;font-weight:600;color:#3d8328;margin-left:2%;margin-top:20px;font-family:'Times New Roman'" runat="server" Text="Suivi des manifestes journaliers"></asp:Label>
     <div class="bodySuivi">
    <div id="manif" >
            
                   <table style="width:92%; margin-left:50px">
                            
                       <tr>
                           <td>Nom Entreprise</td>                           
                            <td>Numero Manifeste</td>                            
                            <td>Nom Narvire</td>
                           <td>Numero Voyage</td>
                        </tr>
            
                        <tr>
                            
                            <td><asp:DropDownList ID="txtNomEntreprise" class="inputSearchSuivi" runat="server"></asp:DropDownList></td>
                            <td><asp:TextBox ID="txtNumeroManifeste" class="inputSearchSuivi" MaxLength="25" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtNomNavire" class="inputSearchSuivi" MaxLength="80" runat="server"></asp:TextBox></td>                          
                            <td><asp:TextBox ID="txtNumeroVoyage" class="inputSearchSuivi" MaxLength="25" runat="server"></asp:TextBox></td>
                        </tr>
</table>
         <table style="width:58%; margin-left:50px; margin-left:200px">
                        <tr><br /><td style="text-align:center" >Du&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDatedeb" TextMode="Date" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; au&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtDatefin" TextMode="Date" runat="server"></asp:TextBox></td>
                        
                        </tr>
                        <tr><td colspan="4" style="text-align:center"> <br />
                            <asp:Button ID="btnRechercher" class="RechercheBtnSuivi" runat="server" Text="Chercher" />
                            <asp:Button ID="ExportTab" class="RechercheBtnSuivi" runat="server" Text="Exporter" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
                <div id="grid" style="width: 95%; height: 500px; text-align:center">
                    <asp:GridView ID="GridView1" RowStyle-CssClass="GvGrid" runat="server" EmptyDataText="Aucune ligne à afficher" AutoGenerateColumns="False" BorderStyle="Solid" AllowPaging="true" PageSize="10" OnPageIndexChanging="OnPageIndexChanging" ShowFooter="true">
                                <AlternatingRowStyle Font-Names="Times New Roman" />
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="170px" DataField="manifNumEnregManif" HeaderText="N° Manifeste">
<ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="170px" DataField="manifDateArrivee" HeaderText="E.T.A">
<ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="170px" DataField="DATEENR" HeaderText="Date Chargement">
<ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="170px" DataField="manifNbreArticle" HeaderText="Nombre BL">
<ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="170px" DataField="manifNumVolVoyage" HeaderText="N° Vol/Voyage">
<ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="170px" DataField="NOM_NAVIRE" HeaderText="Nom Navire">
<ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="170px" DataField="NOM_COMPAGNIE" HeaderText="Compagnie">
<ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="170px" DataField="entreprise" HeaderText="Entreprise">
<ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="170px" DataField="DATETRANSMISSION" HeaderText="Date Transfert">
<ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>

                                </Columns>
           
                                <FooterStyle BackColor="#39A6A0" Font-Bold="True" Font-Names="Times New Roman" />
                                <HeaderStyle BackColor="#71CECA" Font-Bold="True" Font-Italic="False" Font-Names="Times New Roman" />
                                <RowStyle CssClass="GvGrid" Height="40px" Width="30px" />
           
                    </asp:GridView>
               
                </div>
          </div>
</asp:Content>
