<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.master" CodeBehind="admin_home.aspx.vb" Inherits="Travel.Web.admin_home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Home Page</h1>

    <h3>Slide Show</h3>

    <div class="row">

        <div class="col-md-6">
            <div class="form-group">
                <ul class="list-inline">
                    <li>
                        <asp:Label ID="lblShow1" runat="server" Text="Slide Show 1" AssociatedControlID="ddlSlideShow1" /></li>
                    <li>
                        <asp:DropDownList ID="ddlSlideShow1" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                            <asp:ListItem Text="--Select--" Value="0" />
                        </asp:DropDownList></li>
                </ul>
            </div>
            <div class="form-group">
                <ul class="list-inline">
                    <li>
                        <asp:Label ID="lblShow2" runat="server" Text="Slide Show 2" AssociatedControlID="ddlSlideShow2" /></li>
                    <li>
                        <asp:DropDownList ID="ddlSlideShow2" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                            <asp:ListItem Text="--Select--" Value="0" />
                        </asp:DropDownList></li>
                </ul>
            </div>
            <div class="form-group">
                <ul class="list-inline">
                    <li>
                        <asp:Label ID="lblShow3" runat="server" Text="Slide Show 3" AssociatedControlID="ddlSlideShow3" /></li>
                    <li>
                        <asp:DropDownList ID="ddlSlideShow3" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                            <asp:ListItem Text="--Select--" Value="0" />
                        </asp:DropDownList></li>
                </ul>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <ul class="list-inline">
                    <li>
                        <asp:Label ID="lblShow4" runat="server" Text="Slide Show 4" AssociatedControlID="ddlSlideShow4" /></li>
                    <li>
                        <asp:DropDownList ID="ddlSlideShow4" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                            <asp:ListItem Text="--Select--" Value="0" />
                        </asp:DropDownList></li>
                </ul>
            </div>
            <div class="form-group">
                <ul class="list-inline">
                    <li>
                        <asp:Label ID="lblShow5" runat="server" Text="Slide Show 5" AssociatedControlID="ddlSlideShow5" /></li>
                    <li>
                        <asp:DropDownList ID="ddlSlideShow5" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                            <asp:ListItem Text="--Select--" Value="0" />
                        </asp:DropDownList></li>
                </ul>
            </div>
            <div class="form-group">
                <ul class="list-inline">
                    <li>
                        <asp:Label ID="lblShow6" runat="server" Text="Slide Show 6" AssociatedControlID="ddlSlideShow6" /></li>
                    <li>
                        <asp:DropDownList ID="ddlSlideShow6" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                            <asp:ListItem Text="--Select--" Value="0" />
                        </asp:DropDownList></li>
                </ul>
            </div>
        </div>
    </div>




    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-default btn-lg" />

</asp:Content>
