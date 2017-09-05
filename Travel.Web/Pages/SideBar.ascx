<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SideBar.ascx.vb" Inherits="Travel.Web.SideBar" %>
<%@ Register Src="~/Pages/SharedCode/FollowUs.ascx" TagPrefix="uc1" TagName="FollowUs" %>


<asp:Label ID="lblTagLine" runat="server" Text="Budget BackPacking As A Couple" CssClass="tagline hidden-xs" />

<div class="sidebar-module sidebar-module-inset hidden-xs">

    <div class="thumbnail no-margin-bot ">
        <asp:Image ID="imgAbout" runat="server" ImageUrl="~/Images/karl_ and_leanne.jpg" CssClass="img-rounded" Style="padding: 9px;" />
        <div class="caption">
            <p>
                One last chance of adventure? For us the answer is simple. One life, one chance, follow your dreams, the world is a big place didn't you know.
                        <a href="/About/">Read more...</a>
            </p>
        </div>
    </div>
    <br />
    <asp:TextBox runat="server" ID="txtSubscribe" type="email" value="" name="EMAIL" class="required email form-control form-group" placeholder="Email Address..." />
    <asp:Button runat="server" ID="bntSubscribe" CssClass="button btn btn-primary form-group" Style="width: 100%" Text="Subscribe" />

    <uc1:FollowUs runat="server" ID="FollowUs" />

</div>

<section class="sidebar-module sidebar-module-bordered">
    <div role="tabpanel">

        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#recent" aria-controls="recent" role="tab" data-toggle="tab">Recent</a></li>
            <%--<li role="presentation"><a href="#related" aria-controls="related" role="tab" data-toggle="tab">Related</a></li>--%>
            <li role="presentation"><a href="#popular" aria-controls="popular" role="tab" data-toggle="tab">Popular</a></li>
        </ul>

        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="recent">
                <br />
                <nav>
                    <asp:Repeater ID="rptRecent" runat="server">
                        <HeaderTemplate>
                            <ol class="blog-list">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="lnkpop" runat="server" NavigateUrl='<%# ResolveUrl("~/blog/" + Eval("TitleURL")) + "/" %>' Text='<%# Eval("Title")%>'></asp:HyperLink></li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ol>
                        </FooterTemplate>
                    </asp:Repeater>
                </nav>
            </div>
            <%-- <div role="tabpanel" class="tab-pane" id="related">
                <br />
                <nav>
                    <asp:Repeater ID="rptRelated" runat="server">
                        <HeaderTemplate>
                            <ol class="blog-list">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="lnkpop" runat="server" NavigateUrl='<%# ResolveUrl("~/blog/" + Eval("TitleURL")) + "/" %>' Text='<%# Eval("Title")%>'></asp:HyperLink></li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ol>
                        </FooterTemplate>
                    </asp:Repeater>
                </nav>
            </div>--%>
            <div role="tabpanel" class="tab-pane" id="popular">
                <br />
                <nav>
                    <ol class="blog-list">
                        <li>
                            <asp:HyperLink ID="lnkBestOf" runat="server" NavigateUrl="http://intrepidnomads.com/blog/na/travel/the-best-of-2015/">The Best of 2015</asp:HyperLink>
                        </li>
                        <li>

                            <asp:HyperLink ID="lnkGrandCanyon" runat="server" NavigateUrl="http://intrepidnomads.com/blog/usa/travel/honey-i%27v-left-the-car-keys-in-the-grand-canyon/">Honey I left The Keys in the Grand Canyon</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://intrepidnomads.com/blog/guatemala/walking-and-trekking/trekking-volcano-acetenango/">Trekking Volcano Acetenango</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="http://intrepidnomads.com/blog/mexico/travel/around-palenque-into-the-jungle/">Palenque - Into The Jungle</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="http://intrepidnomads.com/blog/mexico/travel/puerto-escondido/">Puerto Escondido</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://intrepidnomads.com/blog/guatemala/travel/semuc-champey-beautiful-pools-and-dark-caves/">Semuc Champey - Beautiful Pools and Dark Caves</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="http://intrepidnomads.com/blog/guatemala/world-history/tikal-and-flores-visiting-the-greatest-ancient-mayan-city/">Tikal the Greatest Ancient Mayan City</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="http://intrepidnomads.com/blog/mexico/travel/swimming-with-whales-sharks-and-turtles-on-isla-holbox/">Swimming with Whale Sharks and Turtles on Isla Holbox</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="http://intrepidnomads.com/blog/nicaragua/travel/peering-into-the-gates-of-hell-masaya-volcano-national-park/">Peering into the Gates of Hell - Masaya Volcano National Park</asp:HyperLink></li>


                        <li>
                            <asp:HyperLink ID="lnkPlan" runat="server" NavigateUrl="~/category/plan-your-own-trip/">Plan your own trip</asp:HyperLink>
                        </li>
                    </ol>
                </nav>
            </div>

            <div role="tabpanel" class="tab-pane" id="messages">...</div>
        </div>
    </div>
</section>

<aside class="sidebar-module sidebar-module-bordered" style="overflow: auto;" >
     <div class="side-bar-info text-center">
        <p style="font-size: 32px;">
            Top Destinations
        </p>
       
    </div>
     <div class="col-xs-6">
            <ul class="list-unstyled text-center">
                <li>  <asp:HyperLink ID="lnkTopDestGuatemala" runat="server" NavigateUrl="~/country/guatemala/">Guatemala</asp:HyperLink></li>
           <li><asp:HyperLink ID="lnkMexico" runat="server" NavigateUrl="~/country/mexico/">Mexico</asp:HyperLink></li>
                 <li>  <asp:HyperLink ID="lnkNicaragua" runat="server" NavigateUrl="~/country/nicaragua/">Nicaragua</asp:HyperLink></li>
                 <li>  <asp:HyperLink ID="lnkBelize" runat="server" NavigateUrl="~/country/belize/">Belize</asp:HyperLink></li>
                
                  </ul>
           
        </div>
         <div class="col-xs-6">
               <ul class="list-unstyled text-center">
                <li>  <asp:HyperLink ID="lnkTopDestColombia" runat="server" NavigateUrl="~/country/colombia/">Colombia</asp:HyperLink></li>
                     <li>  <asp:HyperLink ID="lnkArgentina" runat="server" NavigateUrl="~/country/Argentina/">Argentina</asp:HyperLink></li>
                     <li>  <asp:HyperLink ID="lnkChile" runat="server" NavigateUrl="~/country/Chile/">Chile</asp:HyperLink></li>
                   <li>  <asp:HyperLink ID="lnkBolivia" runat="server" NavigateUrl="~/country/Bolivia/">Bolivia</asp:HyperLink></li>
            </ul>
        </div>
</aside>

<section class="sidebar-module sidebar-module-bordered" style="overflow: hidden; padding: 0;">
    <div class="fb-like-box" data-href="https://www.facebook.com/IntrepidNomads" data-colorscheme="light" data-show-faces="true" data-header="false" data-stream="false" data-show-border="false"></div>
</section>

<section class="sidebar-module sidebar-module-bordered side-bar-info hidden-sm hidden-md hidden-lg">
    <hr />
    <h3>Follow Us</h3>
    <uc1:FollowUs runat="server" ID="FollowUs2" />
</section>

<section class="sidebar-module-slideshow text-center sidebar-module-bordered hidden-xs">

    <div runat="server" id="divCarousel">
        <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
            <ol class="carousel-indicators" runat="server" id="carouselcircles">
            </ol>
            <div class="carousel-inner" runat="server" id="CarouselItems" />
            <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left"></span>
            </a>
            <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right"></span>
            </a>
        </div>
    </div>
</section>

<div class="sidebar-module text-center sidebar-module-bordered hidden-xs">

    <div class="side-bar-info">
        <p style="font-size: 32px;">
            Last Seen In...
        </p>
        <asp:HyperLink ID="lnkViewAll" runat="server" NavigateUrl="/locations/">
            <asp:Label ID="lblLastKnownLocation" runat="server" />
        </asp:HyperLink>
    </div>

</div>


<div class="sidebar-module text-center sidebar-module-bordered side-bar-info">
    <p>
        <asp:Label ID="lblDays" runat="server" />
        Days
    </p>
    <p>On the road</p>
</div>


<section class="sidebar-module">

    <div class="col-xs-6 col-sm-12 col-md-12">

        <asp:Repeater ID="rptRelatedPictureList1" runat="server">
            <HeaderTemplate>
                <ul class="blog-list list-unstyled">
            </HeaderTemplate>
            <ItemTemplate>
                <li style="padding: 20px;">
                    <asp:HyperLink ID="lnkpop" runat="server" NavigateUrl='<%# ResolveUrl("~/blog/" + Eval("TitleURL")) + "/" %>'>
                        <img src='<%# ResolveUrl("~/images/uploads/medium/") + Eval("CategoryImageDescription")%>' height="100" class="img-responsive " />
                        <asp:Label ID="lblLinkText" runat="server" Text='<%# Eval("Title")%>' Font-Size="16px" />
                    </asp:HyperLink></li>
            </ItemTemplate>
            <FooterTemplate>
                </ol>
            </FooterTemplate>
        </asp:Repeater>

    </div>

    <div class="col-xs-6 col-sm-12 col-md-12">

        <asp:Repeater ID="rptRelatedPictureList2" runat="server">
            <HeaderTemplate>
                <ul class="blog-list list-unstyled">
            </HeaderTemplate>
            <ItemTemplate>
                <li style="padding: 20px;">
                    <asp:HyperLink ID="lnkpop" runat="server" NavigateUrl='<%# ResolveUrl("~/blog/" + Eval("TitleURL")) + "/" %>'>
                        <img src='<%# ResolveUrl("~/images/uploads/medium/") + Eval("CategoryImageDescription")%>' height="100" class="img-responsive  " />
                        <asp:Label ID="lblLinkText" runat="server" Text='<%# Eval("Title")%>' Font-Size="16px" />
                    </asp:HyperLink></li>
            </ItemTemplate>
            <FooterTemplate>
                </ol>
            </FooterTemplate>
        </asp:Repeater>
    </div>

</section>

