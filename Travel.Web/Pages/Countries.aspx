<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Page.Master" CodeBehind="Countries.aspx.vb" Inherits="Travel.Web.Countries" %>

<%@ Register Src="~/Pages/SharedCode/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/Pages/SharedCode/DestinationsList.ascx" TagPrefix="uc1" TagName="DestinationsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <uc1:header runat="server" ID="head" />

    <style>
        .countries img {
            margin-right: 20px;
            margin-bottom: 40px;
        }

        .countries span {
            font-family: 'Amatic SC', cursive;
            font-size: 39px;
        }

        h2 {
            font-size: 34px;
        }

       @media (max-width:767px) {

            .countries span {
                font-size: 40px;
                margin-left:5px;
   
            }

            .countries img {
                margin-top: 16px;
                margin-bottom:0;
                display:block;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="blog-post blog-post-with-header">
        <h1 class="area-title">Destinations
        </h1>

        <div class="form-group">

            <p>
                Looking for information on a particular destination, want to know how much it costs to backpack on a budget around a particular country?
                </p>

            <p>
                For each destination we will include our average costs per day and a breakdown of what we spent the money on.
                We will also add any budget backpacking tips we pick up along the way as well as any useful information about that destination.
            </p>

            <p>Choose your destination...</p>

        </div>

        <br />

        <div class="countries">

            <uc1:DestinationsList runat="server" ID="lstNorthAmerica" Continent="North America" />
            <uc1:DestinationsList runat="server" ID="lstCentralAmerica" Continent="Central America" />
            <uc1:DestinationsList runat="server" ID="lstSouthAmerica" Continent="South America" />
            <uc1:DestinationsList runat="server" ID="lstEurope" Continent="Europe" />
            <uc1:DestinationsList runat="server" ID="lstAsia" Continent="Asia" />
            <uc1:DestinationsList runat="server" ID="lstAfrica" Continent="Africa" />
            <uc1:DestinationsList runat="server" ID="lstMiddleEast" Continent="Middle East" />
            <uc1:DestinationsList runat="server" ID="lstaustralasia" Continent="Australasia" />
        </div>


    </div>

    <br />
    <br />
</asp:Content>
