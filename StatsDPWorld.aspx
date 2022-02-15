<%@ Page Title="Stats DP World" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="StatsDPWorld.aspx.vb" Inherits="WebApp_Manifmonitoring.StatsDPWorld" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:Label ID="page_title" style= "font-size:25px;font-weight:600;color:#3d8328;margin-left:2%;margin-top:20px;font-family:'Times New Roman'" runat="server" Text="Statistiques des manifestes de DP World "></asp:Label>

    <div id="manif" >      
            <div id="grid" >
                <div class="boxRechercheStats">
                   <table style="width: 100%;">

                        <tr>
                           <td style="text-align:center" colspan="2">Consignataire <asp:DropDownList ID = "consign" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>                            
                            <td style="text-align:center" colspan="2"> <br />
                                Du&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtDatedeb" TextMode="Date" runat="server"  Width="160px" ></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; au&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtDatefin" TextMode="Date" runat="server" Width="160px" ></asp:TextBox>
                            </td>                      
                        </tr>
                        <tr class="GroupBtnRechercheStat">
                            <td colspan="2">
                                <br />
                                <asp:Button ID="btnRechercher" class="BtnRechercheStat" runat="server" Text="Chercher" />
                                <asp:Button ID="ExportTab" class="BtnRechercheStat" runat="server" Text="Exporter vers Excel" />
                            </td>
                        </tr>
                    </table>
                 </div>
               
                <asp:GridView ID="GridView1" runat="server" RowStyle-CssClass="GvGrid" EmptyDataText="Aucune ligne à afficher" AutoGenerateColumns="False" BorderStyle="Solid" AllowPaging="True" OnPageIndexChanging="OnPageIndexChanging" ShowFooter="True" style="text-align:center;margin-left:10%" Width="90%" PageSize="15" HorizontalAlign ="Justify">
                            <Columns>                              
                                
                                <asp:BoundField ItemStyle-Width="170px" DataField="libelle" HeaderText="Consignataire">
                                <ItemStyle Width="170px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="170px" DataField="nbrManif" HeaderText="Nombre de Manifestes">
                                <ItemStyle Width="170px"></ItemStyle>
                                </asp:BoundField>                                
                                <asp:BoundField ItemStyle-Width="170px" DataField="NomMois" HeaderText="Mois de Transfert">
                                <ItemStyle Width="170px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="170px" DataField="Annee" HeaderText="Année de transfert">
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

                            </Columns>
                            <FooterStyle BackColor="#39A6A0" Font-Bold="True" Font-Names="Times New Roman" />
                                <HeaderStyle BackColor="#71CECA" Font-Bold="True" Font-Italic="False" Font-Names="Times New Roman" />
                                <RowStyle CssClass="GvGrid" Height="28px" Width="20px" />
                </asp:GridView> 
               </div>
            
            </div>
</asp:Content>
