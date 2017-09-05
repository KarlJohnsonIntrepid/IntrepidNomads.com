nomadsApp.controller('adminSlideShowController', ['$scope', 'blogHttpFacade', 'blogModel', 'modelFactory',  function ($scope, blogHttpFacade, blogModel, modelFactory) {
    'use strict';

    blogHttpFacade.getBlogs().success(function (data) {
        $scope.blogs = modelFactory.load(data, blogModel);
    }).error(function (data, status) {
        console.log(data, status);
    });

    $scope.submitForm = function () {

    };
}]);