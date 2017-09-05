<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="404.aspx.vb" Inherits="Travel.Web.Error404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>404 Page could not be found - Intrepid Nomads</title>

    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400italic,400|Amatic+SC' rel='stylesheet' type='text/css' />
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {background: url("  <%: ResolveUrl("~/Images/ErrorPageBackGround.jpg")%> ") no-repeat; background-size: 100%; background-color:#B3CBC4;}
    </style>
    <style>

        .msg-container {
            background: #fff;
            background: rgba(255,255,255,0.9);
            text-align: center;
            margin-top: 100px;
            padding: 10px;
        }

         h1 {
            font-family: 'Amatic SC', cursive;
            font-size: 100px;        
            color: #588C7C;         
        }

        h2 {
            font-family: 'Amatic SC', cursive;
            font-size: 60px;
            color: #6a3e35;
        }

        @media (max-width:1300px) {
            body {
                background-size: 150%;
            }       
        }

        @media (max-width:767px) {
            body {
                background-size: 250%;
            }

            .msg-container {
                margin-left: 20px;
                margin-right: 20px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="msg-container container" style="">
            <h1>The Nomads could not be found</h1>

            <h2>Illusive and Intrepid</h2>

            <br />
            <a class="btn btn-sm btn-primary" href="http://intrepidnomads.com">Where are they?</a>
        </div>
    </form>
</body>
</html>
