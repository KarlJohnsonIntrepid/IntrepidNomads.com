<%@ Page Title="Log in" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.vb" EnableEventValidation="false" Inherits="Travel.Web.Login" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">
    <link href="../Content/signin.css" rel="stylesheet" />

    <div class="container">
        <asp:Login runat="server" ViewStateMode="Disabled" RenderOuterTable="false" DestinationPageUrl="~/admin/admin.aspx">
            <LayoutTemplate>

                <form class="form-signin" role="form">
                    <h2>Enter details to login.</h2>
                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="FailureText" />
                    </p>
                    <asp:TextBox runat="server" ID="UserName" class="form-control" placeholder="Username" required autofocus />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />

                    <asp:TextBox runat="server" ID="Password" TextMode="Password" class="form-control" placeholder="Password" required />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." />


                    <%--<label class="checkbox">
                        <asp:CheckBox runat="server" ID="RememberMe" />
                        Remember me
                    </label>--%>

                    <asp:Button runat="server" ID="btnLogin" CommandName="Login" Text="Log in" class="btn btn-lg btn-primary btn-block" />
                </form>
            </LayoutTemplate>
        </asp:Login>

        <%--     <div class="form-signin" role="form">
            <p>
                <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
                if you don't have an account.
            </p>--%>
    </div>
    <%--<section id="socialLoginForm">
        <h2>Use another service to log in.</h2>
        <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
    </section>
    </div>--%>
</asp:Content>
