// create the module and name it nomadsApp
// also include ngRoute 
var nomadsApp = angular.module('nomadsApp', ['ngRoute', 'ngSanitize', 'ui.bootstrap', 'ui.tinymce']);


// configure the routes
nomadsApp.config(['$routeProvider', '$locationProvider', function AppConfig($routeProvider, $locationProvider) {
    $routeProvider

        // route for the about page
        .when('/about/', {
            templateUrl: '/client/views/blog/AboutView.html',
            controller: 'aboutController'
        })

        // route for the contact page
        .when('/contact/', {
            templateUrl: '/client/views/blog/ContactView.html',
            controller: 'contactController'
        })

         // route for the BlogList page
        .when('/blog/:page?', {
            templateUrl: '/client/views/blog/BlogListView.html',
            controller: 'blogListController'
        })
         // route for the Destinations page
        .when('/destinations/', {
            templateUrl: '/client/views/blog/DestinationsView.html',
            controller: 'destinationsController'
        })

         // route for the Diaries page
        .when('/diaries/', {
            templateUrl: '/client/views/blog//DiariesView.html',
            controller: 'diariesController'
        })

         // route for the contact page
        .when('/around-the-world-in-80-drinks/', {
            templateUrl: '/client/views/blog/DrinksView.html',
            controller: 'drinksController'
        })

          // route for the findus page
        .when('/findus/', {
            templateUrl: '/client/views/blog/FindUsView.html',
            controller: 'findUsController'
        })

        // route for the blogs page
        .when('/blog/:destination/:category/:title/', {
            templateUrl: '/client/views/blog/BlogView.html',
            controller: 'blogController'
        })

        // route for the country page
        .when('/country/:destination/', {
            templateUrl: '/client/views/blog/Countryview.html',
            controller: 'countryController'
        })

        // route for the diariy page
        .when('/category/:category/', {
            templateUrl: '/client/views/blog/Categoryview.html',
            controller: 'categoryController'
        })

          // route for the home page
        .when('/admin', {
            templateUrl: '/client/views/admin/AdminHomeView.html',
            controller: 'adminHomeController'
        })

         // route for the blog page
        .when('/admin/blog', {
            templateUrl: '/client/views/admin/AdminBlogView.html',
            controller: 'adminBlogController'
        })

        .when('/admin/category', {
            templateUrl: '/client/views/admin/AdminCategoryView.html',
            controller: 'adminCategoryController'
        })

        .when('/admin/destinations', {
            templateUrl: '/client/views/admin/AdminDestinationsView.html',
            controller: 'adminDestinationsController'
        })

        .when('/admin/images', {
            templateUrl: '/client/views/admin/AdminImagesView.html',
            controller: 'adminImagesController'
        })

        .when('/admin/locations', {
            templateUrl: '/client/views/admin/AdminLocationsView.html',
            controller: 'adminLocationsController'
        })

        .when('/admin/slideshow', {
            templateUrl: '/client/views/admin/AdminSlideShowView.html',
            controller: 'adminSlideShowController'
        });

    $locationProvider.html5Mode(true);

    toastr.options = {
        positionClass: 'toast-bottom-right'
    };


}]);
