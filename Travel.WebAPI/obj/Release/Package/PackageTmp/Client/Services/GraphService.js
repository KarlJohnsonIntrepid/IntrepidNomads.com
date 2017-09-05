nomadsApp.factory("graphService", function () {
    'use strict';

    var service = {};

     service.loadGraphs = function () {
        if (location.pathname == "/findus/") {
            $.getScript("https://www.google.com/jsapi", function () {
                google.load("visualization", "1", { packages: ["map"], 'callback': graphs });
            });
        }
    };

    function graphs() {
        service.loadLocationChart();
        service.loadLastKnownChart();
        google.setOnLoadCallback(service.drawRegionsMap());
    }


    service.loadLocationChart = function () {
        jQuery.support.cors = true;
        $.ajax({
            url: '/api/Location/graph',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                drawLocationChart(data);
            },
            error: function (x, y, z) {
                console.log(x + '\n' + y + '\n' + z);
            }
        });
    };

    service.loadLastKnownChart = function () {
        jQuery.support.cors = true;
        $.ajax({
            url: '/api/Location/0',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                drawLastKnownChart(data);
            },
            error: function (x, y, z) {
                console.log(x + '\n' + y + '\n' + z);
            }
        });
    };

    var drawLastKnownChart = function (json) {
        var options = {
            zoomLevel: 3,
            showTip: true,
            useMapTypeControl: true
        };
        var data = new google.visualization.DataTable(json);
        var map = new google.visualization.Map(document.getElementById('lastknown_div'));
        map.draw(data, options);
    };

    var drawLocationChart = function (json) {
        var options = {
            zoomLevel: 2,
            showTip: true,
            useMapTypeControl: true
        };
        var data = new google.visualization.DataTable(json);
        var map = new google.visualization.Map(document.getElementById('map_div'));
        map.draw(data, options);
    };

    service.drawRegionsMap = function () {

        var data = google.visualization.arrayToDataTable([
          ['Country', ''],
          ['Austalia', 1000],
          ['United Kingdom', 1000],
          ['Ireland', 1000],
          ['France', 1000],
          ['Germany', 1000],
          ['Belgium', 1000],
          ['Netherlands', 1000],
          ['Denmark', 1000],
          ['Spain', 1000],
          ['Egypt', 1000],
          ['United Arab Emirates', 1000],
          ['Austria', 1000],
          ['Bulgaria', 1000],
          ['Greece', 1000],
          ['Hungary', 1000],
          ['India', 1000],
          ['Italy', 1000],
          ['Jordan', 1000],
          ['Cambodia', 1000],
          ['LA', 1000],
          ['Sri Lanka', 1000],
          ['Morocco', 1000],
          ['Nepal', 1000],
          ['Poland', 1000],
          ['Thailand', 1000],
          ['Turkey', 1000],
          ['United States of America', 1000],
          ['New Zealand', 1000],
           ['Canada', 1000],
           ['Mexico', 1000],
           ['Belize', 1000],
           ['Guatemala', 1000],
           ['El Salvador', 1000],
           ['Honduras', 1000],
           ['Nicaragua', 1000],
           ['Costa Rica', 1000],
           ['Panama', 1000],
           ['Colombia', 1000],
           ['Peru', 1000],
           ['Argentina', 1000],
           ['Brazil', 1000],
           ['Uruguay', 1000],
           ['Paraguay', 1000],
           ['Bolivia', 1000],
           ['Chile', 1000],
           ['Romania', 1000],
           ['Singapore', 1000],
           ['Malaysia', 1000],
           ['Equador', 100]
        ]);

        var options = {backgroundColor: '#588C7C', colorAxis: {colors: ['#F6F4C2', '#8C4646']}};

        var chart = new google.visualization.GeoChart(document.getElementById('regions_div'));

        chart.draw(data, options);
    };



    return service;

});