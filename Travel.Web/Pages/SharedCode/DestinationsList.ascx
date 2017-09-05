<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DestinationsList.ascx.vb" Inherits="Travel.Web.DestinationsList" %>

<h2>
    <asp:Literal ID="lblContinentTitle" runat="server" /></h2>

<br />

<div class="row">
    <asp:Repeater ID="rptCountries" runat="server">
        <ItemTemplate>
            <div class="col-xs-6 col-sm-4 col-md-4 col-lg-3">
                <asp:HyperLink ID="lnk" runat="server" NavigateUrl='<%# ResolveUrl("~/country/" & Eval("CountryURL"))%>'>
                    <img src='<%# ResolveUrl("~/images/uploads/thumbnail/") + Eval("ImageDescription")%>' height="110" width="110" class="img-rounded"/></a>            
                </asp:HyperLink>
                
                    <asp:HyperLink ID="lnk2" runat="server" NavigateUrl='<%# ResolveUrl("~/country/" & Eval("CountryURL"))%>'>
                        <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("CountryDescription")%>' />
                    </asp:HyperLink>            
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<br />

