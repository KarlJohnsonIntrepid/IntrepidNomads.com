<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MailChimp.aspx.vb" Inherits="Travel.Web.MailChimp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div id="mc_embed_signup" style="display:none;" >
        <form action="//intrepidnomads.us9.list-manage.com/subscribe/post?u=ffa47f716c99ea166b73e0687&amp;id=fa16f47ab0" method="post" id="mc-embedded-subscribe-form" name="mc-embedded-subscribe-form" class="validate" novalidate>
            <div id="mc_embed_signup_scroll">

                <div class="mc-field-group">
                    <input type="email" value='<%= request("email") %>'  name="EMAIL" class="required email form-control form-group" id="mce-EMAIL" placeholder="Email Address..." />
                </div>
                <div id="mce-responses" class="clear">
                    <div class="response" id="mce-error-response" style="display: none"></div>
                    <div class="response" id="mce-success-response" style="display: none"></div>
                </div>
                <!-- real people should not fill this in and expect good things - do not remove this or risk form bot signups-->
                <div style="position: absolute; left: -5000px;">
                    <input type="text" name="b_ffa47f716c99ea166b73e0687_fa16f47ab0" tabindex="-1" value="">
                </div>
                <div class="clear">
                    <input type="submit" value="Subscribe" name="subscribe" id="mc-embedded-subscribe" class="button btn btn-primary form-group" style="width: 100%" />
                </div>
            </div>
        </form>
    </div>

    <script type="text/javascript">
        document.forms["mc-embedded-subscribe-form"].submit();
    </script>
</body>
</html>
