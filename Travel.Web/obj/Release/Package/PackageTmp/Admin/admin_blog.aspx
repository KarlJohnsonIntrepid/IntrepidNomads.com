<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.master" CodeBehind="admin_blog.aspx.vb" Inherits="Travel.Web.admin_blog" ValidateRequest="false" %>

<%@ Register Src="~/Admin/Shared/admin_addnewbuttons.ascx" TagPrefix="uc1" TagName="admin_addnewbuttons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%: ResolveUrl("~/Content/tinymce/tinymce.min.js")%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            loadTinyMCE('<%: ResolveUrl("~/Content/bootstrap.min.css")%>', '<%: ResolveUrl("~/Content/blog.css")%>');
            // copy($("[id$='btnCopy'"), $("[id$='txtImageURL']"));
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:admin_addnewbuttons runat="server" ID="btnAddNewButtons" />

    <asp:Panel ID="grpSubmitMsg" Style="display: none;" runat="server">
        <div class="alert alert-success">
            <strong>
                <asp:Label ID="lblSubmitMessage" runat="server" /></strong>
        </div>

        <ul class="list-group">
            <li class="list-group-item">
                <asp:HyperLink ID="lnkBlog" runat="server" Text="View Blog" Target="_blank" /></li>
            <li class="list-group-item">
                <asp:HyperLink ID="lnkEditBlog" runat="server" Text="Edit Blog" /></li>
            <li class="list-group-item" runat="server" id="liImageEdit">
                <asp:HyperLink ID="lnkuploadImagesEdit" runat="server" Text="Click Here To Edit Images and Add Captions" /></li>
            <li class="list-group-item">
                <asp:HyperLink ID="lnkuploadImagesAdd" runat="server" Text="Click Here To Add New Images" /></li>
        </ul>


    </asp:Panel>


    <%-- Details Form--%>

    <div id="divDetails" runat="server" class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">
                <asp:Label ID="lblDetailsFormTitle" Text="Add New Blog" runat="server" />
            </h3>
        </div>
        <div class="panel-body">
            <div class="form-group">
                <asp:Label ID="lblTitle" AssociatedControlID="txtTitle" runat="server" Text="Article Title:" />
                Invalid Chars ("\ /")
                <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="Title" />
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="lblAuthor" AssociatedControlID="ddlAuthor" runat="server" Text="Author:" />
                        <asp:DropDownList ID="ddlAuthor" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="lblDate" AssociatedControlID="txtDate" runat="server" Text="Date:" />

                        delete
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="Country" AssociatedControlID="ddlCountrys" runat="server" Text="Country:" />
                        <ul class="list-inline">

                            <li>
                                <asp:DropDownList ID="ddlCountrys" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                                </asp:DropDownList></li>
                            <li>
                                <tc:DetailsButton ID="btnCountryDetails" runat="server" /></li>
                        </ul>

                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">

                        <asp:Label ID="ddlCategory" AssociatedControlID="ddlCategories" runat="server" Text="Category:" />
                        <ul class="list-inline">

                            <li>
                                <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <tc:DetailsButton ID="btnCategoryDetails" runat="server" /></li>

                        </ul>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblSeoTitle" AssociatedControlID="txtSeoTitle" runat="server" Text="SEO Title:" />
                <asp:TextBox ID="txtSeoTitle" runat="server" CssClass="form-control" AutoCompleteType="None" MaxLength="64" />
                <asp:RequiredFieldValidator ID="valSEOTitle" ControlToValidate="txtSeoTitle" ValidationGroup="Blog" runat="server" Text="SEO Title Required" ForeColor="Red" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblSeoDescription" AssociatedControlID="txtSeoDescription" runat="server" Text="SEO Description:" />
                <asp:TextBox ID="txtSeoDescription" runat="server" CssClass="form-control" AutoCompleteType="None" MaxLength="160" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblNiceDescription" AssociatedControlID="txtNiceDescription" runat="server" Text="Nice Description:" />
                If completed this is used instead of the first 200 char of the main text to display previews
                  <asp:TextBox ID="txtNiceDescription" runat="server" CssClass="form-control" AutoCompleteType="None" TextMode="MultiLine" />
            </div>

            <div runat="server" id="divImages">
                <h4>Images</h4>
                <p>
                    <small>Add images using the add button below. The dropdowns will then populate with images, copy the URL and use to add image to main text.
                    </small>
                </p>
                <asp:Button ID="btnSubmitAddImages" runat="server" Text="Add Images" class="btn btn-default btn-md pull-left" ValidationGroup="Blog" />
                <asp:Button ID="btnSubmitEditImages" runat="server" Text="Edit Images" class="btn btn-default btn-md pull-left " ValidationGroup="Blog" />
                <br />
                <br />

            </div>

            <hr />

            <div class="form-group">

                <div class="row">
                    <div class="col-sm-8">
                        <asp:Label ID="lblBlog" AssociatedControlID="txtContent" runat="server" Text="Blog Content:" />

                        <asp:TextBox ID="txtContent" runat="server" Rows="20" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="None" />

                    </div>
                    <div class="col-sm-4">
                        <asp:UpdatePanel ID="upImagesURL" runat="server">
                            <ContentTemplate>

                                <div class="form-group">


                                    <asp:Label ID="lblImages" AssociatedControlID="ddlImages" runat="server" Text="Associated Images:" />
                                    <asp:DropDownList ID="ddlImages" runat="server" CssClass="form-control" AutoPostBack="true" />
                                    <ul class="list-inline" style="padding-top: 5px;">

                                        <li>
                                            <asp:Label ID="lblTxtImageUrl" AssociatedControlID="txtImageURL" runat="server" Text="Selected Image URL:" Font-Bold="false" /></li>
                                        <li>
                                            <asp:TextBox ID="txtImageURL" runat="server" CssClass="form-control"></asp:TextBox></li>
                                        <li>
                                            <asp:Image ID="imgAssociated" runat="server" Height="80" />
                                        </li>

                                    </ul>

                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblCategoryImage" AssociatedControlID="ddlCategoryImages" runat="server" Text="Category Page Image:" />
                                    <asp:DropDownList ID="ddlCategoryImages" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                        <asp:ListItem Text="--Select ---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>

                                    <ul class="list-inline" style="padding-top: 5px;">

                                        <li>Selected Category Image:</li>
                                        <li>
                                            <asp:Image ID="imgCategory" runat="server" Height="80" />
                                    </ul>


                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>


            </div>
            <asp:RequiredFieldValidator ID="valtxtTitle" ControlToValidate="txtTitle" ValidationGroup="Blog" runat="server" Text="Title Required" ForeColor="Red" />



            <hr />
            <div class="form-group">
                <asp:Label ID="lblIsVisible" AssociatedControlID="chkIsVisible" runat="server" Text="Is Visible:" />
                <asp:CheckBox runat="server" ID="chkIsVisible" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblIsFullScreen" AssociatedControlID="chkIsFullScreen" runat="server" Text="Is Full Screen (hides the side bar), useful for photo pages." />
                <asp:CheckBox runat="server" ID="chkIsFullScreen" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblShowPhotos" AssociatedControlID="chkShowPhotos" runat="server" Text="Show Photo Section" />
                <asp:CheckBox runat="server" ID="chkShowPhotos" />
            </div>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-default btn-lg" ValidationGroup="Blog" />
        </div>
    </div>

    <%--End Details Form--%>



    <%--Select Edit Grid--%>

    <div id="divEditGrid" class="panel panel-primary" runat="server">
        <div class="panel-heading">
            <h3 class="panel-title">
                <asp:Label ID="lblSelectGridTitle" Text="Select Blog To Edit" runat="server" />
            </h3>
        </div>
        <div class="panel-body">

            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="lblCategoryFilter" AssociatedControlID="ddlCategoryFilter" runat="server" Text="Category:" />
                        <asp:DropDownList ID="ddlCategoryFilter" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                            <asp:ListItem Text="--Filter By Category--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="lblCountryFilter" AssociatedControlID="ddlCountryFilter" runat="server" Text="Country:" />
                        <asp:DropDownList ID="ddlCountryFilter" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                            <asp:ListItem Text="--Filter By Country--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView runat="server" ID="grdBlogs" AutoGenerateColumns="false" AutoGenerateSelectButton="true" AutoGenerateDeleteButton="true" class="table table-striped" AllowPaging="true" PageSize="20">
                    <Columns>
                        <asp:BoundField DataField="BlogID" Visible="false" />
                      <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="lnkTitle" Text='<%# Eval("Title")%>' href='<%# BlogURL(Eval("TitleURL"))%>' Target="_blank"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CategoryDescription" HeaderText="Category" />
                        <asp:BoundField DataField="CountryDescription" HeaderText="Country" />
                        <asp:BoundField DataField="Date" HeaderText="Date" />
                        <asp:BoundField DataField="DateCreated" HeaderText="Date Created" />
                        
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkVisible" Checked='<%# Eval("IsVisible")%>' AutoPostBack="true" OnCheckedChanged="ChangeVisibility"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red" />
            </div>
        </div>
    </div>



    <%-- End Edit Grid--%>
</asp:Content>
