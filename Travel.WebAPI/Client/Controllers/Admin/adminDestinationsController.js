nomadsApp.controller('adminDestinationsController', ["$scope", "filterFilter", "pager", "tinymceOptions", "continentHttpFacade", "destinationHttpFacade", "continentModel", "destinationModel", "modelFactory", function ($scope, filterFilter, pager, tinymceOptions, continentHttpFacade, destinationHttpFacade, continentModel, destinationModel, modelFactory) {
    'use strict';
    $scope.tinymceOptions = tinymceOptions;
    $scope.pager = pager;
    $scope.initializing = true;
    $scope.filter = '';
    $scope.selectedIndex = 0;
    $scope.isNew = false;
    $scope.currentPage = 1;

    $scope.loadCountries = function () {
        destinationHttpFacade.getDestinations().success(function (data) {
            $scope.countries = modelFactory.load(data, destinationModel);
            $scope.loadGrid();
            $scope.initializing = false;
        }).error(function (status, data) {
            console.log(status, data);
        });
    };

    $scope.loadCountries();

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
        $scope.filterList = filterFilter($scope.countries, $scope.filter);
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
        destinationHttpFacade.update($scope.selectedItem).success(function () {
            $scope.loadCountries();
            toastr.success('Destination updated successfully');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };

    $scope.save = function () {
        destinationHttpFacade.save($scope.selectedItem).success(function () {
            $scope.loadCountries();
            toastr.success('Destination added successfully');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };

    $scope.deleteRow = function (i) {

        if (confirm('Are you certain you want to delete?')) {
            var id = $scope.pagedItems[i].countryID;
            destinationHttpFacade.delete(id).success(function () {
                $scope.loadCountries();
                toastr.success('Destination deleted successfully');
            }).error(function (data) {
                toastr.error(data.Message);
            });
       }
    };

    continentHttpFacade.getContinents().success(function (data) {
        $scope.continents = modelFactory.load(data, continentModel);
    }).error(function (status, data) {
        console.log(status, data);
    });


}]);