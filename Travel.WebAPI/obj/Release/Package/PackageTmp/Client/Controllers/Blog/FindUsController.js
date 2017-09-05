nomadsApp.controller('findUsController', ['$scope', 'locationHttpFacade', 'page', 'graphService', '$timeout', function ($scope, locationHttpFacade, page, graphService, $timeout) {
    'use strict';
    page.setTitle("Where Are the Intrepid Nomads");


    $scope.initializing = false;
    $scope.initialized = false;
    //Stop lines showing before list
    $timeout(function () {
        if (!$scope.initialized) {
            $scope.initializing = true;
        }
    }, 1000);

    locationHttpFacade.getLastKnownLocation().success(function (data) {
        $scope.lastKnown = data;
        $scope.initializing = false;
        $scope.initialized = true;

    }).error(function (data, status) {
        console.log(data, status);
    });

    graphService.loadGraphs();
    console.log(graphService.lastKnownComplete)
}]);