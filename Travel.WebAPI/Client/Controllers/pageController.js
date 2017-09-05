/// <reference path="pageController.js" />
nomadsApp.controller('pageController', ["$scope", "page", function ($scope, page) {
    'use strict'
    $scope.page = page;
}]);

nomadsApp.factory('page', ["locationHttpFacade", function (locationHttpFacade) {

    var _title = 'Intrepid Nomads';
    var _description = 'Intrepid Nomads';
    var _lastKnownLocation = getLastKnownLocation();
    var _ogImageURL = "";
    var _ogURL = "";
    var _showSideBar = true;
    
    function getLastKnownLocation() {
        locationHttpFacade.getLastKnownLocation().success(function (data) {
            _lastKnownLocation = data;
        }).error(function (data, status) {
            console.log(data, status);
        });
    }

    return {
        title: function () {return _title; },
        setTitle: function (newTitle) {
            _title = newTitle;
            _showSideBar = true;
        },
        description: function () {return _description; },
        setDescription: function (newDescription) {_description = newDescription },
        lastKnownLocation: function () { return _lastKnownLocation },
        ogImageURL: function () {return _ogImageURL; },
        setogImage: function (newogImageURL) { _ogImageURL = newogImageURL; },
        ogURL: function () { return _ogURL; },
        setogURL: function (newogURL) { _ogURL = newogURL; },
        showSideBar: function () {
            var blog = document.getElementById('divMain');
            if (_showSideBar) {
                $(blog).addClass('col-sm-8').removeClass('col-sm-12');
            } else {
                $(blog).removeClass('col-sm-8').addClass('col-sm-12');
            }
            return _showSideBar;
        },
        setShowSideBar: function (isFullScreen) { _showSideBar = !isFullScreen; }
    };
}]);