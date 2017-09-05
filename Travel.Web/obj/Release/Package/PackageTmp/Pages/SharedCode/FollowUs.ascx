<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="FollowUs.ascx.vb" Inherits="Travel.Web.FollowUs" %>

<ul class="list-inline" style="margin-top: 20px">
    <li>
        <a href="http://www.facebook.com/IntrepidNomads" target="_blank">
            <asp:Image ID="imgFacebook" runat="server" ImageUrl="~/Images/Social/64x64/facebook.png" Height="40px" ToolTip="Follow us on facebook" />
        </a>
    </li>
    <li>
        <asp:HyperLink ID="lnkRSS" runat="server" NavigateUrl="~/rss.ashx" Target="_blank">
            <asp:Image ID="imgRSS" runat="server" ImageUrl="~/Images/Social/64x64/rss.png" Height="40px" ToolTip="Subscribe to our RSS feed" />
        </asp:HyperLink>
    </li>
     <li>
        <a href="https://twitter.com/IntrepidNomads" target="_blank">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Social/64x64/twitter.png" Height="40px" ToolTip="Follow us on twitter" />
        </a>
    </li>

    
</ul>
