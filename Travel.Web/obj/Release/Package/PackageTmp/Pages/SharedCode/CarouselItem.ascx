<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CarouselItem.ascx.vb" Inherits="Travel.Web.CarouselItem" ViewStateMode="Disabled" %>

<div class="item" runat="server" id="item">
    <asp:HyperLink ID="lnk" runat="server" >
        <asp:Image ID="imgSlideShow" runat="server" alt="First slide" />
        <div class="carousel-caption">
            <h3>
                <asp:Label ID="txtHeader" runat="server" class="slideshowHeader" /></h3>
            <p>
                <asp:Label ID="txtPreview" runat="server" class="slideshowHeader" />
            </p>
        </div>

    </asp:HyperLink>
</div>
