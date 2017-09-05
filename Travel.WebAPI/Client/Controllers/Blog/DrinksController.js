// jshint ignore: start

nomadsApp.controller('drinksController', ['$scope', 'blogHttpFacade', 'modelFactory', 'page', '$timeout', '$sce', 'blogModel', function ($scope, blogHttpFacade, modelFactory, page, $timeout, $sce, blogModel) {
    'use strict';

    $scope.title = 'Around the World in 80 Drinks';
    page.setTitle('Around the world in 80 drinks - Intrepid Nomads');
    page.setogURL("http://intrepidnomads.com/around-the-world-in-80-drinks/");

    
    $scope.initializing = false;
    $scope.initialized = false;
    //Stop lines showing before list
    $timeout(function () {
        if (!$scope.initialized) {
            $scope.initializing = true;
        }
    }, 1000);

    blogHttpFacade.getDrinks().success(function (data) {
        $scope.blogs = modelFactory.load(data, blogModel);
        $scope.blogs.reverse();
        $scope.initializing = false;
        $scope.initialized = true;
    }).error(function(data, status) {
        console.log(data, status);
    });

    $scope.setSelectedItem = function (i) {
        //$scope.blogs.re
        $scope.selectedItem = i;

        //Move to library
        $timeout(function () {
            $('.blog-post img').addClass('img-responsive').addClass('fade-in').attr("spinner-load", "");
            $('#detailsModal').animate({scrollTop: 0}, 'slow');
        }, 0);

    };

    $scope.bindHTML = function (html) {
        return $sce.trustAsHtml(html);
    };

    //Paging
    $scope.pageNext = function () {
        if ($scope.selectedItem < $scope.blogs.length - 1) {
            $scope.setSelectedItem($scope.selectedItem + 1);
        }
    };

    $scope.pagePrev = function () {
        if ($scope.selectedItem > 0) {
            $scope.setSelectedItem($scope.selectedItem - 1);
        }
    };

    $scope.isFirstPage = function () {
        return $scope.selectedItem === 0;
    };

    $scope.isLastPage = function () {
        if ($scope.blogs) {
            return $scope.selectedItem === $scope.blogs.length - 1;
        }
    };

}]);

/* jshint ignore:end */