<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DrinksItem.ascx.vb" Inherits="Travel.Web.DrinksItem" %>

<asp:HyperLink ID="lnkDrink" runat="server" CssClass="thumbnail minheight thumbnail-pad-bot" style="margin-bottom: 20px;">
    <asp:Image ID="imgDrink" runat="server" />
    <div class="caption">
        <h4>
            <asp:Label ID="lblTitle" runat="server" />
        </h4>
        <h5>
            <asp:Label ID="lblCountry" runat="server" />
            <asp:label id="lblNumber" runat="server" class="badge pull-right">1</asp:label>
        </h5>

    </div>
</asp:HyperLink>

