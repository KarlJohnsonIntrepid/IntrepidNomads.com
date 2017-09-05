<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Page.Master" CodeBehind="Blog.aspx.vb" Inherits="Travel.Web.Blog" ViewStateMode="Disabled" %>

<%@ Register Src="~/Pages/SharedCode/Pager.ascx" TagPrefix="uc1" TagName="Pager" %>
<%@ Register Src="~/Pages/SharedCode/CommentCounter.ascx" TagPrefix="uc1" TagName="CommentCounter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            loadFancyBox();
            $('.blog-post img').addClass('img-responsive');
        });


    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Go to www.addthis.com/dashboard to customize your tools -->
    <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-550ef22b778e8312" async="async"></script>


    <%--Blog Post--%>
    <article class="blog-post">
        <h1 class="blog-post-title">
            <asp:Label ID="lblTitle" runat="server" />
        </h1>
        <h3>
            <em>
                <asp:Label ID="lblSubTitle" runat="server" /></em>
        </h3>


        <p class="blog-post-meta">
            <span class="glyphicon glyphicon-time"></span>
            <em>
                <asp:Label ID="lblDate" runat="server" /></em>
            by
                <asp:Label ID="lblAuthor" runat="server" />
            <uc1:CommentCounter runat="server" ID="CommentCounter" />
            &nbsp&nbsp
      
        </p>

        <div runat="server" id="divFacebook" class="fb-like" data-href="https://developers.facebook.com/docs/plugins/" data-layout="standard" data-action="like" data-show-faces="false" data-share="true"></div>
        <br /><br />

        <asp:Literal ID="litBlogContent" runat="server" Mode="PassThrough" />
    </article>
    <!-- /.blog-post -->
    <%-- Images--%>

    <!-- Go to www.addthis.com/dashboard to customize your tools -->
    <div class="addthis_native_toolbox"></div>

    <br />

    <asp:Panel ID="pnlPhotos" runat="server" CssClass="photos">
        <div>
            <div class="form-group">
                <div class="row" style="margin-left: 0; margin-right: 0;">
                    <asp:Repeater ID="rptImages" runat="server">
                        <ItemTemplate>
                            <div class="col-xs-4 col-sm-2 column">
                                <asp:HyperLink runat="server" CssClass="fancybox thumbnail" rel="group" title='<%#  Eval("ImageCaption")%>'
                                    NavigateUrl='<%# ResolveUrl("~/images/uploads/original/") + Eval("ImageDescription")%>'>
                                    <asp:Image ID="imgThumbNail" runat="server" ImageUrl='<%# ResolveUrl("~/images/uploads/thumbnail/") + Eval("ImageDescription")%>' />

                                </asp:HyperLink>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--  End Images--%>


    <br />
    <uc1:Pager runat="server" ID="Pager" />

    <div class="row sidebar-module-inset hidden">
        <h3>Join The Mailing List </h3>
        <asp:TextBox runat="server" ID="txtSubscribe" type="email" value="" name="EMAIL" class="required email form-control form-group" placeholder="Email Address..." />

        <asp:Button runat="server" ID="bntSubscribe" CssClass="button btn btn-primary form-group" Text="Subscribe" />

    </div>

    <hr />

    <br />

    <%--  <div class="row" style="padding: 5px;">
        <asp:Repeater ID="rptRelated" runat="server">
            <ItemTemplate>
                <div class="col-md-3 col-sm-3 hidden-xs related" style="padding: 5px; padding-top: 0;">
                    <a href="<%# URLCreator.BlogURL(Eval("TitleURL"), False, Nothing)%>">
                        <div class="thumbnail">
                            <img src='<%# ResolveUrl("~/images/uploads/medium/") + Eval("CategoryImageDescription")%>' class="img-responsive" />
                            <div class="caption-medium">
                                <asp:Label ID="lblTitle" runat="server" Text='<%# bind("Title") %>' Font-Size="small" />
                            </div>
                        </div>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>--%>


    <%--Disquss Comments--%>
    <br />
    <h3>Comments</h3>
    <div id="disqus_thread"></div>
    <noscript>Please enable JavaScript to view the <a href="http://disqus.com/?ref_noscript">comments powered by Disqus.</a></noscript>
    <a href="http://disqus.com" class="dsq-brlink">comments powered by <span class="logo-disqus">Disqus</span></a>

    <%-- End Comments--%>


    <script type="text/javascript">

        //Comments
        var disqus_shortname = 'intrepidnomads';
        var disqus_identifier = "<%: BlogID%>";

        (function () {
            var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
            dsq.src = '//' + disqus_shortname + '.disqus.com/embed.js';
            (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
        })();


        //Comment Counter
        (function () {
            var s = document.createElement('script'); s.async = true;
            s.type = 'text/javascript';
            s.src = '//' + disqus_shortname + '.disqus.com/count.js';
            (document.getElementsByTagName('HEAD')[0] || document.getElementsByTagName('BODY')[0]).appendChild(s);
        }());

    </script>

</asp:Content>
