<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Page.Master" CodeBehind="Contact.aspx.vb" Inherits="Travel.Web.Contact" %>

<%@ Register Src="~/Pages/SharedCode/FollowUs.ascx" TagPrefix="uc1" TagName="FollowUs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <div class="contact">

        <div class="blog-post">
            <h2 class="blog-post-title">Contact us</h2>
        </div>

        <h3>Send an Email</h3>

        <div class="contact-inner">
            Whats your story? We would love to here from you.  <a class="email" href="mailto:nomads@intrepidnomads.com">nomads@intrepidnomads.com </a>
        </div>

        <h3>Be Social</h3>

        <div class="contact-inner">
            <uc1:FollowUs runat="server" ID="FollowUs" />
        </div>


        <h3>Or send us a message...</h3>

        <div class="contact-inner">

            <div class="form-horizontal">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-3 control-label">Your Name</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name" ValidationGroup="Contact"> </asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqName" runat="server" ControlToValidate="txtName" ValidationGroup="Contact" Text="*" ForeColor="red" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputPassword3" class="col-sm-3 control-label">Your Email address?</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" ValidationGroup="Contact"> </asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="Contact" Text="*" ForeColor="red" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputPassword3" class="col-sm-3 control-label">Whats this about?</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="Subject" ValidationGroup="Contact"> </asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqSubject" runat="server" ControlToValidate="txtSubject" ValidationGroup="Contact" Text="*" ForeColor="red" Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputPassword3" class="col-sm-3 control-label">Whats on your mind?</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" Rows="10" TextMode="MultiLine" placeholder="message" ValidationGroup="Contact"> </asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqMessage" runat="server" ControlToValidate="txtMessage" ValidationGroup="Contact" Text="*" ForeColor="red" Display="Dynamic" />
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-10">
                        <asp:Button ID="btnContact" runat="server" CssClass="btn btn-primary" Text="Send It" ValidationGroup="Contact"></asp:Button>
                    </div>
                    <div class="col-sm-offset-3 col-sm-10">
                        <asp:Label ID="lblMessageSent" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>
        </div>      
    </div>

</asp:Content>
