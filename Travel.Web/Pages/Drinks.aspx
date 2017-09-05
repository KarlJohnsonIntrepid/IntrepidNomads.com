<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Page.Master" CodeBehind="Drinks.aspx.vb" Inherits="Travel.Web.Drinks" %>

<%@ Register Src="~/Pages/SharedCode/DrinksItem.ascx" TagPrefix="uc1" TagName="DrinksItem" %>
<%@ Register Src="~/Pages/SharedCode/pager.ascx" TagPrefix="uc1" TagName="pager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        h5 {
            color: #8C4646 !important;
        }

        .minheight {
            min-height: 200px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Go to www.addthis.com/dashboard to customize your tools -->
    <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-550ef22b778e8312" async="async"></script>

    <article>
        <h1 class="blog-post-title">Around The World in 80 Drinks</h1>

        <p>
            During this trip we will be <s>drinking</s> reviewing the local drinks from around the world, every country has its own culinary delicacies,
                 but we are in search of the specialty beverages. Can we make it around the world in 80 drinks?
        </p>

        <br />
        <br />
        <div class="row">
            <div class="col-xs-6">
                <asp:Panel ID="pnlDrinks1" runat="server">
                </asp:Panel>

            </div>
            <div class="col-xs-6">
                <asp:Panel ID="pnlDrinks2" runat="server">
                </asp:Panel>
            </div>

        </div>
    </article>

    <div class="col-sm-12">

        <h3 style="margin-top: 10px;">Why not buy us a drink</h3>

        <p>This is a non profit website, help keep this website running and the beers flowing. Why not buy the drinks and we will write a review especially for you.</p>

        <iframe src="../pages/paypal.htm" style="height: 80px; border: 0"></iframe>

    </div>


</asp:Content>
