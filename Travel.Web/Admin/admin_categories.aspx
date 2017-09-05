<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.master" CodeBehind="admin_categories.aspx.vb" Inherits="Travel.Web.admin_categories" ValidateRequest="false" %>

<%@ Register Src="~/Admin/Shared/admin_addnewbuttons.ascx" TagPrefix="uc1" TagName="admin_addnewbuttons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%: ResolveUrl("~/Content/tinymce/tinymce.min.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            loadTinyMCE('<%: ResolveUrl("~/Content/bootstrap.min.css")%>', '<%: ResolveUrl("~/Content/blog.css")%>');
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:admin_addnewbuttons runat="server" ID="btnAddNewButtons" />

    <%-- Details Form--%>

    <div id="divDetails" class="panel panel-primary" runat="server">

        <div class="panel-heading">
            <h3 class="panel-title">
                <asp:Label ID="lblDetailsFormTitle" Text="Add New Country" runat="server" />
            </h3>
        </div>
        <div class="panel-body">
            <div class="form-group">
                <asp:Label ID="lblCategory" AssociatedControlID="txtCategory" runat="server" Text="Category:" />
                <asp:TextBox ID="txtCategory" runat="server" class="form-control" placeholder="Category" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblSeoTitle" AssociatedControlID="txtSeoTitle" runat="server" Text="SEO Title:" />
                <asp:TextBox ID="txtSeoTitle" runat="server" CssClass="form-control" AutoCompleteType="None" MaxLength="64" />
                <asp:RequiredFieldValidator ID="valSEOTitle" ControlToValidate="txtSeoTitle" ValidationGroup="Category" runat="server" Text="SEO Title Required" ForeColor="Red" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblSeoDescription" AssociatedControlID="txtSeoDescription" runat="server" Text="SEO Description:" />
                <asp:TextBox ID="txtSeoDescription" runat="server" CssClass="form-control" AutoCompleteType="None" MaxLength="160" />
            </div>

            <div class="form-group">

                <asp:Label ID="lblBlog" AssociatedControlID="txtContent" runat="server" Text="Category Information:" />

                <asp:TextBox ID="txtContent" runat="server" Rows="15" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="None" />

            </div>

            <div class="form-group">
                <asp:Label ID="lblReverseOrder" AssociatedControlID="chkReverseOrder" runat="server" Text="Reverse Date Order:" />
                <asp:CheckBox runat="server" ID="chkReverseOrder" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblIsFeature" AssociatedControlID="chkFeature" runat="server" Text="Is Diary:" />
                <asp:CheckBox runat="server" ID="chkFeature" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblShowInMenu" AssociatedControlID="chkFeature" runat="server" Text="Show In Menu:" />
                <asp:CheckBox runat="server" ID="chkShowInMenu" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblShowFeature" AssociatedControlID="ddlShowFeature" runat="server" Text="Show Feature In This Category" />
                <asp:DropDownList ID="ddlShowFeature" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="--Select ---" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Label ID="lblShowFeatureCountry" AssociatedControlID="ddlShowFeatureCountry" runat="server" Text="Show Feature In This Country" />
                <asp:DropDownList ID="ddlShowFeatureCountry" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="--Select ---" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:UpdatePanel ID="upImage" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <asp:Label ID="lblCategoryImage" AssociatedControlID="txtCategoryImage" runat="server" Text="Category Image:" />
                        To add an image first upload it in Image Management and enter the ID here, lazy but its quick to program.....
                        <asp:TextBox ID="txtCategoryImage" runat="server" CssClass="form-control" AutoCompleteType="None" MaxLength="160" />
                        <asp:Image ID="imgCategoryImage" runat="server" Height="80" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCategoryImage" ValidationGroup="Category" runat="server" Text="Required" ForeColor="Red" />
                        <asp:Button ID="btnCheckImageID" runat="server" Text="Test Image" CssClass="btn btn-primary" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>



            <asp:RequiredFieldValidator ID="valCategory" ControlToValidate="txtCategory" ValidationGroup="category" runat="server" Text="Category - Required" ForeColor="Red" />

            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-default" ValidationGroup="category" />

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

            <div class="table-responsive">

                <asp:GridView runat="server" ID="grdCategory" AutoGenerateColumns="false" AutoGenerateSelectButton="true" AutoGenerateDeleteButton="true" class="table table-striped" AllowPaging="true" PageSize="20">
                    <Columns>
                        <asp:BoundField DataField="CategoryId" Visible="false" />
                        <asp:BoundField DataField="CategoryDescription" Visible="true" HeaderText="Category" />

                    </Columns>
                </asp:GridView>

                <br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red" /></asp:Label>

            </div>

        </div>
    </div>

    <%-- End Edit Grid--%>
</asp:Content>
