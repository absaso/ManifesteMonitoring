<%@ Page Title="Statistiques" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="StatsEntreprise.aspx.vb" Inherits="WebApp_Manifmonitoring.StatsEntreprise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <asp:Label ID="page_title" style= "font-size:25px;font-weight:600;color:#3d8328;margin-left:2%;margin-top:20px;font-family:'Times New Roman'" runat="server" Text="Statistiques des manifestes par entreprise "></asp:Label>

     <div id="manif" >      
            <div id="grid" >
                <div class="boxRechercheStats">
                   <table style="width: 100%;">
                        <tr><td style="text-align:center" >
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
               
                <asp:GridView ID="GridView1" runat="server" Class="GridStats" RowStyle-CssClass="GvGrid" EmptyDataText="Aucune ligne à afficher" AutoGenerateColumns="False" BorderStyle="Solid" AllowPaging="True" OnPageIndexChanging="OnPageIndexChanging" ShowFooter="True" style="text-align:center" Width="70%" PageSize="15" HorizontalAlign ="Justify">
                            <Columns>                              
                                
                                <asp:BoundField ItemStyle-Width="170px" DataField="DateTransmission" HeaderText="Date de Transmission">
                                <ItemStyle Width="170px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="170px" DataField="libelle" HeaderText="Consignataire">
                                <ItemStyle Width="170px"></ItemStyle>
                                </asp:BoundField>                                
                                <asp:BoundField ItemStyle-Width="170px" DataField="NbManifeste" HeaderText="Nombre de manifestes">
                                <ItemStyle Width="170px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="170px" DataField="NbrBL" HeaderText="Nombre de BL">
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
