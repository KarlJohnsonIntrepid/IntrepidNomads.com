﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.master" CodeBehind="imageupload.aspx.vb" Inherits="Travel.Web.imageupload" %>

<%@ Register Src="~/Admin/Shared/admin_addnewbuttons.ascx" TagPrefix="uc1" TagName="admin_addnewbuttons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:admin_addnewbuttons runat="server" ID="admin_addnewbuttons" />

    <%--Upload Images--%>

    <div id="divUploadImages" class="panel panel-primary" runat="server">

        <div class="panel-heading">
            <h3 class="panel-title">
                <asp:Label ID="Label1" Text="Upload New Images" runat="server" />
            </h3>
        </div>
        <div class="panel-body">

            <div class="form-group">
                <asp:Label ID="lblAssoicatedBlogID" AssociatedControlID="ddlAssoicatedBlogID" runat="server" Text="Associated Blog:" />
                <asp:DropDownList ID="ddlAssoicatedBlogID" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true">
                    <asp:ListItem Text="-- Associate With Blog Entry --" Value="0"></asp:ListItem>

                </asp:DropDownList>

                <br />
                <asp:HyperLink ID="lnkGoToBlogNew" runat="server" Text="Modify this blog" Visible="false" class="btn btn-primary btn-sm pull-right" />
                <br />
            </div>

            <act:AjaxFileUpload ID="actFileUpload" runat="server" AllowedFileTypes="jpg,jpeg" />

        </div>
    </div>

    <%-- End Upload Images--%>

    <%-- Details Form--%>

    <div id="divDetails" class="panel panel-default" runat="server">

        <div class="panel-heading">
            <h3 class="panel-title">
                <asp:Label ID="lblDetailsFormTitle" Text="Edit Image" runat="server" />
            </h3>
        </div>
        <div class="panel-body">
            <div class="form-group">
                <asp:Label ID="lblImage" AssociatedControlID="imgPreview" runat="server" Text="Image Preview:" />
                <asp:Image ID="imgPreview" runat="server" class="img-responsive" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblImageDescription" AssociatedControlID="lblImageDescription" runat="server" Text="Description (the name of the image file):" />
                <asp:TextBox ID="txtImageDescription" runat="server" class="form-control" placeholder="Description" ValidationGroup="Image" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblCaption" AssociatedControlID="txtCaption" runat="server" Text="Caption" />
                <asp:TextBox ID="txtCaption" runat="server" class="form-control" placeholder="Caption" Rows="3" TextMode="MultiLine" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblBlog" AssociatedControlID="ddlBlog" runat="server" Text="Blog:" />
                <asp:DropDownList ID="ddlBlog" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                    <asp:ListItem Text="-- Associated With blog --" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:RequiredFieldValidator ID="valCategory" ControlToValidate="txtImageDescription" ValidationGroup="Image" runat="server" Text="Category - Required" ForeColor="Red" />

            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-default" ValidationGroup="Image" />



        </div>
    </div>

    <%--End Details Form--%>


    <%--Select Edit Grid--%>
    <div id="divEditGrid" class="panel panel-primary" runat="server">
        <div class="panel-heading">
            <h3 class="panel-title">
                <asp:Label ID="lblSelectGridTitle" Text="Select Category To Edit" runat="server" />
            </h3>
        </div>
        <div class="panel-body">

            <div class="form-group">
                <asp:Label ID="lblBlogFilter" AssociatedControlID="ddlBlogFilter" runat="server" Text="Blog:" />
                <asp:DropDownList ID="ddlBlogFilter" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                    <asp:ListItem Text="--Filter By Blog --" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:HyperLink ID="lnkGoToBlog" runat="server" Text="Modify this blog" Visible="false" class="btn btn-primary btn-sm pull-right" />
                <br />
            </div>

            <div class="table-responsive">

                <asp:GridView runat="server" ID="grdBlog" AutoGenerateColumns="false" AutoGenerateSelectButton="true" AutoGenerateDeleteButton="true" class="table table-striped" AllowPaging="true" PageSize="20">
                    <Columns>
                        <asp:BoundField DataField="ImageId" Visible="True" HeaderText="ID" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="imgThumbnail" runat="server" ImageUrl='<%# ResolveUrl("~/images/uploads/thumbnail/") + Eval("ImageDescription")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="200px" HeaderText="Image Path">
                            <ItemTemplate>
                                <asp:Label ID="lblUll" runat="server" Text='<%# ResolveUrl("~/images/uploads/original/") + Eval("ImageDescription")%>' Style="word-break: break-all;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ImageCaption" Visible="true" HeaderText="Caption" />
                        <asp:BoundField DataField="BlogTitle" Visible="true" HeaderText="Blog" />
                        <asp:BoundField DataField="DateUploaded" Visible="true" HeaderText="Date Uploaded" />

                    </Columns>
                </asp:GridView>

            </div>

        </div>
    </div>

    <%-- End Edit Grid--%>
</asp:Content>
