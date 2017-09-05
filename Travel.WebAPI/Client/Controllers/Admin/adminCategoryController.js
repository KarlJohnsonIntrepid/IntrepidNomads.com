nomadsApp.controller('adminCategoryController', ["$scope", "filterFilter", "pager", "tinymceOptions", "destinationHttpFacade", "categoryHttpFacade", "categoryModel", "destinationModel", "modelFactory", function ($scope, filterFilter, pager, tinymceOptions, destinationHttpFacade, categoryHttpFacade, categoryModel, destinationModel, modelFactory) {
    'use strict';
    $scope.tinymceOptions = tinymceOptions;
    $scope.pager = pager;
    $scope.initializing = true;
    $scope.filter = '';
    $scope.selectedIndex = 0;
    $scope.isNew = false;
    $scope.currentPage = 1;


    $scope.getCategories = function () {
        categoryHttpFacade.getCategories().success(function (data) {
            $scope.categories = modelFactory.load(data, categoryModel);
            $scope.loadGrid();
            $scope.initializing = false;
        }).error(function (status, data) {
            console.log(status, data);
        });
    };

    $scope.getCategories();

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
        $scope.filterList = filterFilter($scope.categories, $scope.filter);
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
        categoryHttpFacade.update($scope.selectedItem).success(function () {
            $scope.getCategories();
            toastr.success('Category updated successfully');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };

    $scope.save = function () {
        categoryHttpFacade.save($scope.selectedItem).success(function () {
            $scope.getCategories();
            toastr.success('Category added successfully');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };


    $scope.deleteRow = function (i) {
        if (confirm('Are you certain you want to delete?')) {
            var id = $scope.categories[i].categoryID;
            categoryHttpFacade.delete(id).success(function () {
                $scope.getCategories();
                toastr.success('Category deleted successfully');
            }).error(function (data) {
                toastr.error(data.Message);
            });
        }
    };

  
    //Load dropdown
    destinationHttpFacade.getDestinations().success(function (data) {
        $scope.destinations = modelFactory.load(data, destinationModel);
    }).error(function (status, data) {
        console.log(status, data);
    });

    
}]);