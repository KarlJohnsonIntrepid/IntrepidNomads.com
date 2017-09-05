//Destinations-----------------------------
nomadsApp.factory("destinationHttpFacade", ['$http', function ($http) {
    'use strict';
    var _getDestinations = function () {
        return $http.get("/api/country");
    };

     var _getDestinationsPage = function () {
         return $http.get("/api/country/NotNA");
    };

    var _getDestination = function (id) {
        return $http.get("/api/country/" + id);
    };

    var _save = function (Destination) {
        return $http.post("/api/country/", Destination);
    };

    var _update = function (Destination) {
        return $http.put("/api/country/" + Destination.CountryID, Destination);
    };

    var _delete = function (id) {
        return $http.delete("/api/country/" + id);
    };

    return {
        getDestinations: _getDestinations,
        getDestination: _getDestination,
        getDestinationsPage:_getDestinationsPage,
        save: _save,
        update: _update,
        delete: _delete
    };
}]);



//Categories/ Diaries-----------------------------

nomadsApp.factory("categoryHttpFacade", ['$http', function ($http) {
    'use strict';
    var _getCategories = function () {
        return $http.get("/api/category");
    };

    var _getCategory = function (id) {
        return $http.get("/api/category/" + id);
    };

    var _save = function (category) {
        return $http.post("/api/category/", category);
    };

    var _update = function (category) {
        return $http.put("/api/category/" + category.categoryID, category);
    };

    var _delete = function (id) {
        return $http.delete("/api/category/" + id);
    };

    var _getFeatured = function () {
        return $http.get("api/category/featured");
    };

    return {
        getCategories: _getCategories,
        getCategory: _getCategory,
        save: _save,
        update: _update,
        delete: _delete,
        getFeatured: _getFeatured
    };
}]);

//Blogs-----------------------------

nomadsApp.factory("blogHttpFacade", ['$http', function ($http) {
    'use strict';
    var _getBlogs = function () {
        return $http.get("/api/blog");
    };

    var _getBlogsAdmin = function () {
        return $http.get("/api/blog/includehidden");
    };

    var _getBlog = function (id) {
        return $http.get("/api/blog/" + id);
    };

     var _getBlogsMinimal = function () {
        return $http.get("api/blog/minimal");
    };

    var _getBlogsRelated = function (id, number) {
        return $http.get("api/blog/related/" + id + "/" + number);
    };

    var _getBlogsByCategory = function (id) {
        return $http.get("api/blog/bycategory/" + id + "/1/0");
    };

    var _getBlogsByCountry = function(id) {
        return $http.get('api/blog/ByCountry/' + id + '/0')
    };

    var _getCount = function () {
        return $http.get('api/blog/count');
    };

    var _save = function (Blog) {
        return $http.post("/api/blog/", Blog);
    };

    var _update = function (Blog) {
        return $http.put("/api/blog/" + Blog.blogID, Blog);
    };

    var _delete = function (id) {
        return $http.delete("/api/blog/" + id);
    };

    var _getDrinks = function () {
        return $http.get("api/blog/ForDrinks/0");
    };

    var _paged = function (pagenumber, pagesize, includehidden) {
        //includehidden = 1 or 0
        return $http.get("api/blog/paged/" + pagenumber + "/" + pagesize + "/" + includehidden);
    };

    var _getBlogByTitle = function (destination, category, title) {
        return $http.get("api/blog/BlogByTitle/" + destination + "/" + category + "/" + title);
    };

    return {
        getBlogs: _getBlogs,
        getBlog: _getBlog,
        save: _save,
        update: _update,
        delete: _delete,
        getDrinks: _getDrinks,
        getBlogByTitle: _getBlogByTitle,
        getBlogsAdmin: _getBlogsAdmin,
        getBlogsMinimal: _getBlogsMinimal,
        getBlogsRelated: _getBlogsRelated,
        getBlogsByCategory: _getBlogsByCategory,
        getBlogsByCountry: _getBlogsByCountry,
        paged: _paged,
        getCount: _getCount
    };
}]);

///Categories/ Diaries-----------------------------

nomadsApp.factory("locationHttpFacade", ['$http', function ($http) {
    'use strict';

    var _getLocations = function () {
        return $http.get("api/location");
    };

    var _getLocationsGraph = function () {
        return $http.get("api/location");
    };

    var _getLastKnownLocation = function () {
        return $http.get("api/location/lastknown");
    };

    var _delete = function (id) {
        return $http.delete("api/location/" + id);
    };

    var _update = function ( location) {
        return $http.put("api/location/" + location.locationID,  location);
    };

    var _save = function (location) {
        return $http.post("/api/location/", location);
    };

    return {
        getLastKnownLocation: _getLastKnownLocation,
        getLocations: _getLocations,
        getLocationsGraph: _getLocationsGraph,
        delete: _delete,
        update: _update,
        save: _save
    };
}]);


//Creates and array of models and applies any business logic held within the model
nomadsApp.factory("modelFactory",  function () {
    'use strict';
    var _load = function (data, model) {
        var m = [];
        var destinations = JSON.parse(data);

        var i;
        for (i = 0; i < destinations.length; i += 1) {
            //Load up models
            m.push(model(destinations[i]));
        }
        return m;
    };

    return {
        load: _load
    };
});

//Images-----------------------------
nomadsApp.factory("imageHttpFacade", ['$http', function ($http) {
    'use strict';
    var _getImages = function () {
        return $http.get("/api/image");
    };

    var _getImage = function (id) {
        return $http.get("/api/image/" + id);
    };


    var _selectImagesByBlog = function (id) {
        return $http.get("/api/image/selectImagesByBlog/" + id);
    };

    var _save = function (image) {
        return $http.post("/api/image/", image);
    };

    var _update = function (image) {
        return $http.put("/api/image/" + image.imageID, image);
    };

    var _delete = function (id) {
        return $http.delete("/api/image/" + id);
    };


    return {
        getImages: _getImages,
        getImage: _getImage,
        selectImagesByBlog: _selectImagesByBlog,
        delete: _delete,
        update: _update,
        save: _save

    };
}]);

//Continent-----------------------------
nomadsApp.factory("continentHttpFacade", ['$http', function ($http) {
    'use strict';
    var _getContinents = function () {
        return $http.get("/api/continent");
    };

    var _getContinent = function (id) {
        return $http.get("/api/continent/" + id);
    };

    return {
        getContinents: _getContinents,
        getContinent: _getContinents
    };
}]);



nomadsApp.factory("htmlParser", function () {
    'use strict';
    var parser = {};

    //Move to linrary next two functions..
    parser.parseHTML = function (html) {
        return decodeHtml(html.replace(/<\/?[^>]+(>|$)/g, ""));
    };

    function decodeHtml(html) {
        var txt = document.createElement("textarea");
        txt.innerHTML = html;
        return txt.value;
    };


    return parser;
});







