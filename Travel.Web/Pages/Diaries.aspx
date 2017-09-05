<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Page.Master" CodeBehind="Diaries.aspx.vb" Inherits="Travel.Web.Diaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%@ Register Src="~/Pages/SharedCode/header.ascx" TagPrefix="uc1" TagName="header" %>
    <uc1:header runat="server" ID="head" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="blog-post blog-post-with-header">

        <h1 class="area-title">Travel Diaries
        </h1>

        <div class="form-group">
            <p>
                Ever thought of trekking to Everest Base Camp or Hiking the Atlas Mountains? See what life is really like by reading our day-by-day diaries of our more unique trips. 
            </p>
            <p>Select a diary to find out more...</p>
            <br />
        </div>

        <asp:Repeater ID="rptDiaries" runat="server">
            <ItemTemplate>
                <div class="col-xs-6 col-sm-6 col-md-4 col-lg-3">

                    <a href="<%# "/category/" + Eval("CategoryURL")  %>">
                        <div class="thumbnail with-caption">
                            <img src='<%# ResolveUrl("~/images/uploads/medium/") + Eval("ImageDescription")%>' class="img-responsive" />
                            <p>
                                <asp:Literal ID="title" runat="server" Text='<%# CStr(Eval("CategoryDescription")).Replace("Diary", "").Replace("diary", "")%>' />
                            </p>
                        </div>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>


    </div>

</asp:Content>
