nomadsApp.controller('adminImagesController', ['$scope', '$http', 'filterFilter', 'pager', 'imageHttpFacade', 'blogHttpFacade', 'blogModel', 'imageModel', 'modelFactory', function ($scope, $http, filterFilter, pager, imageHttpFacade, blogHttpFacade, blogModel, imageModel, modelFactory) {
    'use strict';
    $scope.pager = pager;
    $scope.initializing = true;
    $scope.filter = '';
    $scope.selectedIndex = 0;
    $scope.isNew = false;
    $scope.currentPage = 1;

    $scope.getImages = function () {
        imageHttpFacade.getImages().success(function (data) {
            $scope.images = modelFactory.load(data, imageModel);
            $scope.loadGrid();
            $scope.initializing = false;
        }).error(function (data, status) {
            console.log(data, status);
        });
    };

    $scope.getImages();


    //Page and Search  --------------------------
    $scope.$watch('search', function (val) {
        if (!$scope.initializing) {
            $scope.filter = val;
            $scope.loadGrid();
        }
    });

    $scope.addNew = function () {
        $scope.isNew = true;
        var newItem = {};
        $scope.selectedItem = newItem;
    };


    //Grid -------------------------------------------

    $scope.loadGrid = function () {
        $scope.filterList = filterFilter($scope.images, $scope.filter);
        $scope.pagedItems = $scope.pager.getPagedItems($scope.filterList);
        $scope.totalItems = $scope.filterList.length;
        $scope.pager.setCurrentPage($scope.currentPage);
    };

    $scope.editRow = function (i) {
        $scope.isNew = false;
        $scope.setSelectedItem(i);
    };

    $scope.pageChanged = function () {
        $scope.pager.setCurrentPage($scope.currentPage);
        $scope.pagedItems = $scope.pager.getPagedItems($scope.filterList);
    };


    $scope.setSelectedItem = function (i) {
        $scope.selectedItem = $scope.pagedItems[i];
        $scope.selectedIndex = i;
    };

    //Save-Add-Delete-Update   ------------------------------------------

    $scope.submitForm = function () {
        if ($scope.form.$valid) {
            if ($scope.isNew === true) {
                $scope.save();
            } else {
                $scope.update();
            }
            $('#detailsModal').modal('hide');
        }
    };

    $scope.update = function () {
        imageHttpFacade.update($scope.selectedItem).success(function () {
            $scope.getImages();
            toastr.success('Image updated successfully');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };

    $scope.save = function () {
        imageHttpFacade.save($scope.selectedItem).success(function (data, status) {
            $scope.getImages();
            toastr.success('Image saved successfully');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };


    $scope.deleteRow = function (i) {
        var id = $scope.pagedItems[i].imageID;
        var url = "UploadImages?Filename=" + $scope.pagedItems[i].imageDescription + "&ImageID=" + id;
        $http.delete(url).success(function () {
            $scope.getImages();
            toastr.success('Image deleted successfully');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };

    blogHttpFacade.getBlogsAdmin().success(function (data) {
        $scope.blogs = modelFactory.load(data, blogModel);
    }).error(function (data, status) {
        console.log(data, status);
    });

}]);