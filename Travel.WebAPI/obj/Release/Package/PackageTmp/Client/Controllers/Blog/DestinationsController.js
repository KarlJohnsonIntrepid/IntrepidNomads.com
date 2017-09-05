nomadsApp.controller('destinationsController', ['$scope', 'destinationHttpFacade', 'modelFactory', 'page', 'destinationModel', '$timeout', function ($scope, destinationHttpFacade, modelFactory, page, destinationModel, $timeout) {
    'use strict';

    $scope.initializing = false;
    $scope.initialized = false;
    //Stop lines showing before list
    $timeout(function () {
        if (!$scope.initialized) {
            $scope.initializing = true;
        }
    }, 1000);

    page.setTitle('Destinations - Intrepid Nomads - Budget BackPacking As A Couple');
    page.setDescription('The best budget travel tips from the road. Find out where to go, what to see, and how much it costs. Start planning your own trip now.');
    page.setShowSideBar(true);
    page.setogURL("http://intrepidnomads.com/Destinations/");
  
    //Titles for each section
    $scope.NorthAmerica = 'North America';
    $scope.CentralAmerica = 'Central America';
    $scope.SouthAmerica = 'South America';
    $scope.Europe = 'Europe';
    $scope.Asia = 'Asia';
    $scope.MiddleEast = 'Middle East';
    $scope.Africa = 'Africa';
    $scope.Australasia = 'Australasia';

    $scope.getCountries = function () {
        destinationHttpFacade.getDestinationsPage().success(function (data) {
            $scope.destinations = modelFactory.load(data, destinationModel);

            $scope.initializing = false;
            $scope.initialized = true;
        }).error(function (data, status) {
            console.log(data, status);
        });
    };

    $scope.getCountries();

}]);




