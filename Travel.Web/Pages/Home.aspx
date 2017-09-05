<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Page.Master" CodeBehind="Home.aspx.vb" Inherits="Travel.Web.Home" %>

<%@ Register Src="~/Pages/SharedCode/header.ascx" TagPrefix="uc1" TagName="header" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400italic,400|Amatic+SC' rel='stylesheet' type='text/css' />
    <script type="text/javascript" src="../Content/slick/slick.min.js"></script>
    <script type="text/javascript" src="../Scripts/home.js"></script>
    <link href="../Content/home.css" rel="stylesheet" />
    <link href="../Content/slick/slick.css" rel="stylesheet" />
    <link href="../Content/slick/slick-theme.css" rel="stylesheet" />

    <uc1:header runat="server" ID="head" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Page Content -->
    <div class="container home">

        <hr class="featurette-divider">


        <!-- First Featurette -->

        <div class="featurette" id="services">


            <asp:Image ID="img2" runat="server" CssClass="featurette-image img-circle img-responsive pull-right" ImageUrl="~/Images/Home/intrepid-nomads-blog-home.jpg" />
            <h2 class="featurette-heading"><a href="/Blog/">Travel Blog</a>
                <span class="text-muted">Get ready to be inspired </span>
            </h2>
            <p class="lead">Follow us on our journey and be inspired start your own budget backpacking trip. </p>

            <asp:LinkButton runat="server" ID="lnkBlog" CssClass="button btn btn-primary" Text="More.." PostBackUrl="/blog/" />
        </div>


        <div class="home-blogs-slider">
            <asp:Repeater ID="rptRelatedPictureList1" runat="server">
                <ItemTemplate>
                    <div>
                        <div class="slide-container">
                            <asp:HyperLink ID="lnkpop" runat="server" NavigateUrl='<%# ResolveUrl("~/blog/" + Eval("TitleURL")) + "/" %>'>
                                <img data-lazy='<%# ResolveUrl("~/images/uploads/medium/") + Eval("CategoryImageDescription")%>' />
                                <asp:Label ID="lblLinkText" runat="server" Text='<%# Eval("Title")%>' />
                            </asp:HyperLink>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>


        <hr class="featurette-divider">

        <!-- Second Featurette -->

        <div class="featurette" id="about">
            <asp:Image ID="img1" runat="server" CssClass="featurette-image img-circle img-responsive pull-left" ImageUrl="~/Images/Home/I-Want-To-Travel-The-World.jpg" />
            <h2 class="featurette-heading"><a href="/Destinations/">Top Destinations</a>
                <span class="text-muted">The best budget travel tips from the road</span>
            </h2>
            <p class="lead">Find out where to go, what to see, and how much it costs to backpack around the world as a couple on a budget. Start planning your own trip now.</p>

            <asp:LinkButton runat="server" ID="lbtnDestinations" CssClass="button btn btn-primary" Text="More.." PostBackUrl="/Destinations/" />
        </div>


        <div class="home-blogs-slider">
            <asp:Repeater ID="rptDestinations" runat="server">
                <ItemTemplate>
                    <div>
                        <div class="slide-container">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# ResolveUrl("~/country/" & Eval("CountryURL"))%>'>
                                <img data-lazy='<%# ResolveUrl("~/images/uploads/medium/") + Eval("ImageDescription")%>' height="120" />
                                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("CountryDescription")%>' />
                            </asp:HyperLink>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>


        <hr class="featurette-divider">

        <!-- Third Featurette -->
        <div class="featurette" id="contact">
            <asp:Image ID="img3" runat="server" CssClass="featurette-image img-circle img-responsive pull-right" ImageUrl="~/Images/Home/Old_World_Map.JPG" />
            <h2 class="featurette-heading"><a href="/diaries">Travel Diaries </a>
                <span class="text-muted">Real life experiences and personal accounts</span>
            </h2>
            <p class="lead">Ever thought of trekking to Everest Base Camp or Hiking the Atlas Mountains? See what life is really like by reading our day-by-day diaries of our more unique trips. </p>

            <asp:LinkButton runat="server" ID="lbtnDiarys" CssClass="button btn btn-primary" Text="More.." PostBackUrl="/Diaries/" />
        </div>


        <div class="home-blogs-slider">
            <asp:Repeater ID="rptDiaries" runat="server">
                <ItemTemplate>
                    <div>
                        <div class="slide-container">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# ResolveUrl("~/category/" + Eval("CategoryURL"))%>'>
                                <img data-lazy='<%# ResolveUrl("~/images/uploads/medium/") + Eval("ImageDescription")%>' />
                                <asp:Label ID="lblCountry" runat="server" Text='<%# CStr(Eval("CategoryDescription")).Replace("Diary", "").Replace("diary", "")%>' />
                            </asp:HyperLink>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <hr class="featurette-divider">


        <div class="container">
            <!-- Example row of columns -->
            <div class="row text-center">
                <div class="col-sm-6 small-feature">

                    <asp:Image ID="img4" runat="server" CssClass="featurette2 img-circle img-responsive text-center" AlternateText="Around the world in 80 drinks intrepid nomads" ImageUrl="~/Images/Home/Drinks-Around-The-World.jpg" />
                    <a href="/around-the-world-in-80-drinks/">
                        <h2>Around The World In 80 Drinks</h2>
                    </a>

                    <p>Every country has its own culinary delicacies, but we are in search of the specialty beverages. Can we make it around the world in 80 drinks?  </p>

                    <asp:LinkButton runat="server" ID="btnDrinks" CssClass="button btn btn-primary" Text="More.." PostBackUrl="/around-the-world-in-80-drinks/" />

                </div>
                <div class="col-sm-6 small-feature">

                    <asp:Image ID="img5" runat="server" CssClass="featurette2 img-circle img-responsive text-center" ImageUrl="~/Images/Home/couple-backpacker.jpg" AlternateText="Intrepid Nomads About Us sunset" />
                    <a href="/about/">
                        <h2>Who Are The Intrepid Nomads?</h2>
                    </a>

                    <p>One guy, one girl, two backpacks, and a passion for travel. Find out who we are and what has inspired us to start our adventure. </p>

                    <asp:LinkButton runat="server" ID="bntAbout" CssClass="button btn btn-primary" Text="More.." PostBackUrl="/about/" />

                </div>
            </div>

            <hr class="featurette-divider">
        </div>

        <!-- /.container -->

    </div>
    <footer class="text-right">
        <p>&copy; @DateTime.Now.Year - Intrepid Nomads | Developed By Karl Johnson</p>
    </footer>

</asp:Content>
