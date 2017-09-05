nomadsApp.controller("countryController", ['$scope', '$routeParams', 'modelFactory', '$timeout', '$filter', 'destinationHttpFacade', 'page', 'destinationModel', '$sce', 'blogHttpFacade', 'blogModel', 'htmlParser', function ($scope, $routeParams, modelFactory, $timeout, $filter, destinationHttpFacade, page, destinationModel, $sce, blogHttpFacade, blogModel, htmlParser) {
    'use strict';
    $scope.initializing = 0;
    destinationHttpFacade.getDestinations().success(function (data) {
        $scope.results = modelFactory.load(data, destinationModel);

        //Loads all destinations to match the url from old system
        $scope.destination = $filter('filter')($scope.results, {'countryURL': $routeParams.destination})[0];

        page.setTitle($scope.destination.seoTitle);
        page.setDescription($scope.destination.seoDescription);

        $timeout(function () {
            $('.blog-post img').addClass('img-responsive');
        }, 0);

        $scope.loadRelatedBlogs();
        $scope.initializing = $scope.initializing + 1;

    }).error(function (data, status) {
        console.log(data, status);
    });

    $scope.loadRelatedBlogs = function () {
        blogHttpFacade.getBlogsByCountry($scope.destination.countryID).success(function (data) {
            $scope.related = modelFactory.load(data, blogModel);
            //  Stop lines showing before list
            $timeout(function () {
                $scope.initializing = $scope.initializing + 1;
             }, 150);
        }).error(function (data, status) {
            console.log(data, status);
        });
    };


    $scope.bindHTML = function (html) {
        return $sce.trustAsHtml(html);
    };

    $scope.parseHTML = function (html) {
        return htmlParser.parseHTML(html);
    };

}]);