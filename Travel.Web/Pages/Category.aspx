<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Page.Master" CodeBehind="Category.aspx.vb" Inherits="Travel.Web.Category" ViewStateMode="Disabled" %>

<%@ Register Src="~/Pages/SharedCode/pager.ascx" TagPrefix="uc1" TagName="pager" %>
<%@ Register Src="~/Pages/SharedCode/CategoryList.ascx" TagPrefix="uc1" TagName="CategoryList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.blog-post img').addClass('img-responsive');
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Go to www.addthis.com/dashboard to customize your tools -->
    <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-550ef22b778e8312" async="async"></script>

    <div class="blog-post">
        <h1 class="blog-post-title">
            <asp:Label ID="lblTitle" runat="server" />
        </h1>

        <p>
            <asp:Literal ID="litInformation" runat="server" Mode="PassThrough" />
        </p>

        <ul>
            <asp:Repeater ID="rptLinkedCategories" runat="server">
                <ItemTemplate>
                    <li><a href='<%# "/category/" + Eval("CategoryURL") + "/"%>'><%# Eval("CategoryDescription")%> </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>

        <hr />

        <uc1:CategoryList runat="server" ID="CategoryList1" />

    </div>
    <!-- /.blog-post -->

    <uc1:pager runat="server" ID="pager" />

</asp:Content>
