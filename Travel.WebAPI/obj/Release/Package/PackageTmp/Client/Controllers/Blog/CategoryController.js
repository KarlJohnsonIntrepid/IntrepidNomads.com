nomadsApp.controller("categoryController", ['$scope', '$sce', '$routeParams', '$filter', '$timeout', 'modelFactory', 'categoryHttpFacade', 'page', 'categoryModel', 'htmlParser', 'blogHttpFacade', 'blogModel', function ($scope, $sce, $routeParams, $filter, $timeout, modelFactory, categoryHttpFacade, page, categoryModel, htmlParser, blogHttpFacade, blogModel) {
    'use strict';
    $scope.initializing = 0;

    categoryHttpFacade.getCategories().success(function (data) {
        $scope.results = modelFactory.load(data, categoryModel);

        $scope.category = $filter('filter')($scope.results, {'categoryURL': $routeParams.category})[0];

        page.setTitle($scope.category.seoTitle);
        page.setDescription($scope.category.seoDescription);

        $timeout(function () {
            $('.blog-post img').addClass('img-responsive');
        }, 0);

        $scope.loadRelatedBlogs();

        $scope.initializing = $scope.initializing + 1;

    }).error(function (data, status) {
        console.log(data, status);
    });

    $scope.loadRelatedBlogs = function () {
        blogHttpFacade.getBlogsByCategory($scope.category.categoryID).success(function (data) {

            $scope.blogs = modelFactory.load(data, blogModel);
        
            if ($scope.category.reverseDateOrder) {
                $scope.blogs.reverse();
            }

            //  Stop lines showing before list
            $timeout(function () {
                $scope.initializing = $scope.initializing + 1;
            }, 150);

        }).error(function (data, status) {
            console.log(data, status);
        });
    };

    $scope.parseHTML = function (html) {
        return htmlParser.parseHTML(html);
    };

    $scope.bindHTML = function (html) {
        return $sce.trustAsHtml(html);
    };


}]);