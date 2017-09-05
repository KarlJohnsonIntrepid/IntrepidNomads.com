<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Pager.ascx.vb" Inherits="Travel.Web.pager" %>

<ul class="pager">
    <li class="previous">
        <asp:LinkButton ID="lnkPrevious" runat="server" Text="&larr; Previous" />

    </li>
    <li class="next">
        <asp:LinkButton ID="lnkNext" runat="server" Text="Next &rarr;" />
    </li>
</ul>
