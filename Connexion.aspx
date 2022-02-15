<%@ Page Title="Connexion" Language="vb" AutoEventWireup="false" CodeBehind="Connexion.aspx.vb" Inherits="WebApp_Manifmonitoring.Connexion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="frontWebApp.css"/>
    <style type="text/css">
        
    </style>
</head>

    <script type="text/javascript">
        
        history.pushState(null, null, window.location.href);
        window.addEventListener('popstate', function (event)
        {
            history.pushState(null, null, window.location.href);
            event.preventDefault();
        });
    </script>

<body style="background-image: linear-gradient(to bottom right,#006699, #33cc33); height: 100%;">
    <form id="form1" runat="server">

            <div id="bodyConnexion">
                <h2 class="titleConnexion">Connexion</h2>
                <div class="fields">
                <asp:Label ID="labelID" class="label_input" runat="server" Text="Identifiant"></asp:Label>
                <asp:TextBox ID="txtUsername" class="input" MaxLength="30" runat="server" ></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="labelMDP" class="label_input" MaxLength="30" runat="server" Text="Mot de passe"></asp:Label>
                <asp:TextBox ID="txtPassword" class="input" TextMode="Password" runat="server"></asp:TextBox>
                <br />
                <br />
                    <asp:Button ID="getIn" runat="server" Text="Se connecter" />
                </div>
             </div>
            
   
       
    </form>
</body>
</html>
