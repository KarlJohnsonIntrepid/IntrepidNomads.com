nomadsApp.controller('diariesController', ['$scope', 'categoryHttpFacade', 'modelFactory', 'page', 'categoryModel', '$timeout', function ($scope, categoryHttpFacade, modelFactory, page, categoryModel, $timeout) {
    // create a message to display in our view
     $scope.initializing = false;
    $scope.initialized = false;
    //Stop lines showing before list
    $timeout(function () {
        if (!$scope.initialized) {
            $scope.initializing = true;
        }
    }, 1000);
    page.setTitle('Diaries - Intrepid Nomads - Budget BackPacking As A Couple');
    page.setDescription('Ever thought of trekking to Everest Base Camp or Hiking the Atlas Mountains? See what life is really like by reading our day-by-day diaries of our more unique trips.');

    var getCountries = function () {
        categoryHttpFacade.getFeatured().success(function (data) {
            $scope.diaries = modelFactory.load(data, categoryModel);
            $scope.initializing = false;
            $scope.initialized = true;
        }).error(function (data, status) {
            console.log(data, status);
        });
    };

    getCountries();

}]);