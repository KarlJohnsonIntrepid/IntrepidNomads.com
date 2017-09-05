nomadsApp.controller('adminLocationsController', '$scope', 'pager', 'filterFilter', 'locationHttpFacade', 'modelFactory', 'locationModel', function ($scope, pager, filterFilter, locationHttpFacade, modelFactory, locationModel) {
    'use strict';
    $scope.pager = pager;
    $scope.initializing = true;
    $scope.filter = '';
    $scope.selectedIndex = 0;
    $scope.isNew = false;
    $scope.currentPage = 1;

    $scope.getLocations = function () {
        locationHttpFacade.getLocations().success(function (data) {
            $scope.locations = modelFactory.load(data, locationModel);
            $scope.loadGrid();
            $scope.initializing = false;
        }).error(function (data, status) {
            console.log(data, status);
        });
    };

    $scope.getLocations();

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
        $scope.filterList = filterFilter($scope.locations, $scope.filter);
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
        locationHttpFacade.update($scope.selectedItem).success(function () {
            $scope.getLocations();
            toastr.success('Location updated successfully');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };

    $scope.save = function () {
        locationHttpFacade.save($scope.selectedItem).success(function () {
            $scope.getLocations();
            toastr.success('Location added successfully');
        }).error(function (data) {
             toastr.error(data.Message);
        });
    };

    $scope.deleteRow = function (i) {

        if (confirm('Are you certain you want to delete?')) {
            var id = $scope.pagedItems[i].locationID;
            locationHttpFacade.delete(id).success(function () {
                $scope.getLocations();
                toastr.success('Location deleted successfully');
            }).error(function (data) {
                toastr.error(data.Message);
            });
        }
    };

    //Date Picker -------------

    $scope.open1 = function () {
        $scope.datepicker.opened = true;
    };

    $scope.datepicker = {
        opened: false
    };

 


});