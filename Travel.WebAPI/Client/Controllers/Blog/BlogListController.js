nomadsApp.controller('blogListController', ['$scope', '$timeout', '$routeParams', 'blogHttpFacade', 'modelFactory', 'page', 'blogModel', 'htmlParser', 'pager', function ($scope, $timeout, $routeParams, blogHttpFacade, modelFactory, page, blogModel, htmlParser, pager) {
    'use strict';
    page.setTitle('Blogs - Intrepid Nomads - Budget BackPacking As A Couple');
    page.setDescription('List of blogs and stories of our latest travels');

    $scope.pager = pager;
    $scope.pager.setNumPerPage(10);

    $scope.initializing = false;
    $scope.initialized = false;
    //Stop lines showing before list
    $timeout(function () {
        if (!$scope.initialized) {
            $scope.initializing = true;
        }
    }, 1000);

    $scope.initPaging = function () {
        if ($routeParams.page) {
            $scope.initPageNum = $routeParams.page;
        } else {
            $scope.initPageNum = 1;
        }
    };

    $scope.initPaging();

    blogHttpFacade.paged($scope.initPageNum, 10, 1).success(function (data) {
        $scope.filteredBlogs = modelFactory.load(data, blogModel);

        blogHttpFacade.getCount().success(function (data) {
            $scope.totalItems = data;
        });

        $scope.setCurrentPage();

        //Stop lines showing before list
        $timeout(function () {
            $scope.initializing = false;
             $scope.initialized = true;
        }, 150);
    }).error(function (data, status) {
        console.log(data, status);
    });

    $scope.setCurrentPage = function () {
        if ($routeParams.page) {
            $scope.currentPage = $routeParams.page;
            $scope.pager.setCurrentPage($routeParams.page);
        } else {
            $scope.currentPage = 1;
            $scope.pager.setCurrentPage(1);
        }
    };

    $scope.parseHTML = function (html) {
        return htmlParser.parseHTML(html);
    };

    $scope.pageChanged = function () {
        $scope.pager.pageChanged('blog/', $scope.currentPage);
    };

}]);