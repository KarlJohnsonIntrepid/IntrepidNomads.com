
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.master" CodeBehind="admin.aspx.vb" Inherits="Travel.Web.admin1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/geolocations.js"></script>
    <script>

        $(document).ready(function () {
            //Load Geo information and save to the database.
            getLocation();
            initialize();
        });

    </script>


    <div class="row">

        <div class="col-sm-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Current Location</h3>
                </div>
                <div class="panel-body">

                    <p id="latitude" class="hidden"></p>
                    <p id="longitude" class="hidden"></p>
                    <p id="location"></p>

                    <p id="address"></p>
                    <div id="mapholder"></div>
                </div>

            </div>
        </div>

        <div class="col-sm-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">To Do List</h3>

                    http://www.free-picture.net/
                </div>
                <div class="panel-body">


                    <ul>

                        <li>Home Page Images</li>
                        
                    </ul>
                </div>

            </div>
        </div>

        <div class="col-sm-4">
        </div>

    </div>
</asp:Content>
