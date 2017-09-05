nomadsApp.controller('contactController', ['$scope', '$http', function ($scope, $http) {
    'use strict';

    var formData = [];

    $scope.message = '';
    $scope.reset = function () {
        $scope.form.name = '';
        $scope.form.email = '';
        $scope.form.subject = '';
        $scope.form.messagetext = '';
    };

     // function to submit the form after all validation has occurred
    $scope.submitForm = function (isValid) {
        $scope.submitted = true;

        // check to make sure the form is completely valid
        if (isValid) {

            $scope.message = 'Sending message.....';

            formData = $scope.form;
            $http.post('api/contact', formData).success(function () {
                $scope.message = 'Thanks your message has been sent';
            }).error(function (data, status) {
                $scope.message = 'The message could not be sent';
                console.log(data, status);
            });

            //Clear the form
            $scope.reset();
            $scope.submitted = false;
        }
    };
}]);