<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CategoryList.ascx.vb" Inherits="Travel.Web.CategoryList" ViewStateMode="Disabled" %>
<%@ Register Src="~/Pages/SharedCode/CommentCounter.ascx" TagPrefix="uc1" TagName="CommentCounter" %>

   <script type="text/javascript">
       $(document).ready(function () {
           loadFancyBox();
           $('.blog-post img').addClass('img-responsive');
       });

       //Comments
       var disqus_shortname = 'intrepidnomads';
   
       //Comment Counter
       (function () {
           var s = document.createElement('script'); s.async = true;
           s.type = 'text/javascript';
           s.src = '//' + disqus_shortname + '.disqus.com/count.js';
           (document.getElementsByTagName('HEAD')[0] || document.getElementsByTagName('BODY')[0]).appendChild(s);
       }());

    </script>

<asp:Repeater ID="lstCategory" runat="server">
    <ItemTemplate>

        <article class="panel ">
            <div class="panel-heading">
                <h2>
                    <a href='<%# BlogURL(Eval("TitleURL"))%>'>
                        <asp:Label ID="lblBlogTitle" runat="server" Text='<%# bind("Title") %>' />
                    </a>
                </h2>
                <span class="hidden-xxs">
                    <span class="glyphicon glyphicon-time"></span>
                    <em>
                        <asp:Label ID="lblDate" runat="server" Text='<%# CDate(eval("Date")).ToString("MMMM dd, yyyy") %>' /></em>

                    by 
                    <asp:Label ID="lblAuthor" runat="server" Text='<%# Bind("AuthorName")%>' />
                    <uc1:CommentCounter runat="server" ID="CommentCounter" BlogURL='<%# BlogURL(Eval("TitleURL"))%>' BLogID='<%# Eval("BlogID") %>' />
                </span>
            </div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-xs-6 col-md-5">
                        <a href='<%# BlogURL(Eval("TitleURL"))%>'>
                            <img src='<%# ResolveUrl("~/images/uploads/medium/") + Eval("CategoryImageDescription")%>' height="180" class="img-responsive img-rounded" /></a>
                    </div>

                    <div class=" col-md-7">
                        <div class="wrap">
                            <p class="">
                                <asp:Label ID="lblContentPreview" runat="server" Text='<%# LoadPreview(Eval("ContentPreview"), BlankLibrary.NoBlank(Eval("NiceDescription"), ""))%>' />
                                <a href='<%# BlogURL(Eval("TitleURL"))%>' title="read more" class="readmore">Read more »</a>

                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </article>
    </ItemTemplate>
</asp:Repeater>

<script type="text/javascript">

    var disqus_shortname = 'travelwac';

    //Comment Counter
    (function () {
        var s = document.createElement('script'); s.async = true;
        s.type = 'text/javascript';
        s.src = '//' + disqus_shortname + '.disqus.com/count.js';
        (document.getElementsByTagName('HEAD')[0] || document.getElementsByTagName('BODY')[0]).appendChild(s);
    }());

</script>
