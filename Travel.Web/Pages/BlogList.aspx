<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Page.Master" CodeBehind="BlogList.aspx.vb" Inherits="Travel.Web.default_1" %>

<%@ Register Src="~/Pages/SharedCode/CategoryList.ascx" TagPrefix="uc1" TagName="CategoryList" %>
<%@ Register Src="~/Pages/SharedCode/CarouselItem.ascx" TagPrefix="uc1" TagName="CarouselItem" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <section>

        <div class="row">
            <div class="col-xs-8">
                <h1 class="blog-post-title">Latest Blogs</h1>
            </div>
            <div class="col-xs-4">
                <div class="dropdown pull-right">
                    <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-expanded="true">
                        <asp:Literal ID="litSearchText" runat="server">Search By</asp:Literal>
                        <span class="caret"></span>
                    </button>

                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                        <li role="presentation">
                            <asp:LinkButton ID="lnkViewAll" runat="server">
                                  View All
                            </asp:LinkButton>
                        </li>
                        <asp:PlaceHolder ID="plcCategorys" runat="server" />

                    </ul>
                </div>
            </div>

        </div>


        <div class="form-group">
            <uc1:CategoryList runat="server" ID="CategoryList1" />

                <div class="text-center">
                    <nav>
                        <ul class="pagination">
                            <li runat="server" id="liPrev">
                                <asp:LinkButton ID="lnkPagePrev" runat="server" aria-label="Previous">
                        <span aria-hidden="true">&laquo;</span>
                                </asp:LinkButton>
                            </li>
                            <asp:PlaceHolder ID="plcPager" runat="server"></asp:PlaceHolder>

                            <li runat="server" id="liNext">
                                <asp:LinkButton ID="lnkPageNext" runat="server" aria-label="Next">
                        <span aria-hidden="true">&raquo;</span>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </nav>
                </div>
                <div class="hidden">
                    <asp:LinkButton ID="btnLoadMore" runat="server" Text="Show More..." CssClass="btn btn-primary" />
                </div>
            
        </div>
    </section>
</asp:Content>

