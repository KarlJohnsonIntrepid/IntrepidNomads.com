nomadsApp.factory("pager", ['$location', function ($location) {
    'use strict';

    var pager = {};
    var _numPerPage = 15;
    var _currentPage = 1;


    pager.setCurrentPage = function (x) {
        _currentPage = x;
    };

    pager.getCurrentPage = function () {
        return _currentPage;
    };

    pager.setNumPerPage = function (x) {
        _numPerPage = x;
    };

    pager.numPerPage = function () {
        return _numPerPage;
    };

    pager.getStart = function () {
        if (_currentPage == 1) {
            return 0;
        } else {
            return (_currentPage * _numPerPage) - _numPerPage;
        }
    };

    pager.getEnd = function () {
        return pager.getStart() + _numPerPage;
    };

    pager.getPagedItems = function (items) {
        return items.slice(pager.getStart(), pager.getEnd());
    };

    pager.pageChanged = function(url, currentPage) {
        _currentPage = currentPage;

        if (_currentPage == 1){
                $location.url(url);
        } else{ $location.url(url + _currentPage);}

        window.scrollTo(0, 0);
    };

    return pager;

}]);