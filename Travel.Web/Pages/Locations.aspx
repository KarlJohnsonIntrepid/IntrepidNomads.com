<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pages/Page.Master" CodeBehind="Locations.aspx.vb" Inherits="Travel.Web.Locations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script src="../Scripts/googlegraphs.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            loadLocationChart();
            loadLastKnownChart();
            drawRegionsMap();
        });

 
    </script>

    <h1 class="blog-post-title">Where are the Intrepid Nomads?
    </h1>
    <br />

    <h3>Last seen in    <asp:Label ID="lblLastKnownLocation" runat="server" ForeColor="#8C4646" /></h3>

    <br />
     <div id="lastknown_div" style="width: 100%; height: 300px;"></div>

    <br />
    <br />


    <h3>Current trip history..</h3>

    <p>
        Several years later when the memory of the trip fades, you may wish that there was a way of remembering where you have been. 
        So everytime we log into this blog it will record where we were at the time, the date and the precise GPS coordinates, one day in the future we may look back at this and be able to show our children where we went.
    </p>

    <div id="map_div" style="width: 100%; height: 500px"></div>

    <br />
    <br />

    <h3>Where we have been... </h3>

    <ul class="list-inline">
        <li>
            <div style="height: 30px; width: 100px; background-color: #8C4646; color: white; padding: 5px;">Visted</div>
        </li>
        <li>
            <div style="height: 30px; width: 100px; background-color: #F6F4C2; color: #8C4646; padding: 5px;">Planned</div>
        </li>
    </ul>

    <div id="regions_div" style="width: 100%; height: 500px;"></div>

    <br />
    <br />
    <br />
    <br />   
    
</asp:Content>
