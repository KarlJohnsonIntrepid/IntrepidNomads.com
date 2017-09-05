function startGeoLogging() {

    $.getScript("http://maps.googleapis.com/maps/api/js?sensor=false", function () {
        $.getScript("https://www.google.com/jsapi", function () {
            google.load("visualization", "1", { packages: ["map"], 'callback': startGeoLogging2 });
        });
    });
}

function startGeoLogging2() {

    var geocoder;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(successFunction, errorFunction);
    }

    //load geo information and save to the database.
    getLocation();
    initialize();
}

function showPosition(position) {

    var x = document.getElementById("location");
    var lat = document.getElementById("latitude");
    var long = document.getElementById("longitude");

    x.innerHTML = "Latitude: " + position.coords.latitude +
    "<br>Longitude: " + position.coords.longitude;

    lat.innerHTML = position.coords.latitude
    long.innerHTML = position.coords.longitude

    showPositionOnMap(position);

    $('img').addClass('img-responsive');
}

function getLocation() {
    var x = document.getElementById("location");
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showError(error) {
    var x = document.getElementById("location");
    switch (error.code) {
        case error.PERMISSION_DENIED:
            x.innerHTML = "User denied the request for Geolocation."
            break;
        case error.POSITION_UNAVAILABLE:
            x.innerHTML = "Location information is unavailable."
            break;
        case error.TIMEOUT:
            x.innerHTML = "The request to get user location timed out."
            break;
        case error.UNKNOWN_ERROR:
            x.innerHTML = "An unknown error occurred."
            break;
    }
}

///Find Location
function showPositionOnMap(position) {
    var latlon = position.coords.latitude + "," + position.coords.longitude;

    var img_url = "http://maps.googleapis.com/maps/api/staticmap?center="
    + latlon + "&zoom=14&size=400x300&sensor=false";
    document.getElementById("mapholder").innerHTML = "<img src='" + img_url + "'>";
}

//Get the latitude and the longitude;
function successFunction(position) {
    var lat = position.coords.latitude;
    var lng = position.coords.longitude;
    codeLatLng(lat, lng);
}

function errorFunction() {
    console.log("Geocoder failed");
}

function initialize() {
    geocoder = new google.maps.Geocoder();

}

function codeLatLng(lat, lng) {

    var y = document.getElementById("address");

    var latlng = new google.maps.LatLng(lat, lng);
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
          //  console.log(results)
            if (results[1]) {
                //formatted address
                y.innerHTML =  results[2].formatted_address;

                insertData();

            } else {
                y.innerHTML = "No results found";
            }
        } else {
            y.innerHTML = "Geocoder failed due to: " + status;
        }
    });
}

function insertData() {
    var L = {};
    L.Latitude = document.getElementById("latitude").innerHTML;
    L.Longitude = document.getElementById("longitude").innerHTML;
    L.Location1 = document.getElementById("address").innerHTML;


    $.post("/api/location/", L)
    .done(function (data, status) {
         toastr.success('Location added successfully');
    }).fail(function (status) {
        toastr.error(status.responseText);
    });
}