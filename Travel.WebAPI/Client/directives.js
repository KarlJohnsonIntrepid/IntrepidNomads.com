nomadsApp.directive("spinner", function () {
    'use strict';

    return {
        restrict: 'E',
        templateUrl: 'Client/Views/Shared/_Spinner.html',
        replace: true
    }

});

nomadsApp.directive('fancyboxgroup', function () {
    return {
        restrict: 'C',

        link: function (scope, element, attrs) {
            if (scope.$last) setTimeout(function () {
                $(".fancybox").fancybox({
                    type : "image",
                    helpers: {
                        title: {
                            type: 'over'
                        }
                    }
                });
            }, 100);
        }
    }
});

nomadsApp.directive('spinnerLoad', [function spinnerLoad() {
    return {
        restrict: 'A',
        link: function spinnerLoadLink(scope, elem, attrs) {
            scope.$watch('ngSrc', function watchNgSrc() {
                elem.hide();
                elem.after('<div class="spinner spinner-sm"> <div class="bounce1"></div> <div class="bounce2"></div><div class="bounce3"></div></div>');  // add spinner
            });
            elem.on('load', function onLoad() {
                elem.show();
                elem.next('div').remove(); // remove spinner
            });
        }
    };
}])




nomadsApp.directive("sharebuttons", function () {
    'use strict';
    return {
        restrict: 'E',
        templateUrl: 'client/views/shared/_ShareButtons.html',
        replace: true,
        controller: ['$scope', function ($scope) {
            $scope.facebook = "https://www.facebook.com/sharer/sharer.php?u=" + window.location.href;

            $scope.twitter = "https://twitter.com/intent/tweet?text=" + document.getElementsByTagName("title")[0].innerText + "&url=" + window.location.href;

            $scope.google = "https://plus.google.com/share?url=" + window.location.href;
            //$scope.pinit = "https://plus.google.com/share?url=" + window.location.href;
        }]
    }
});

nomadsApp.directive('fblike', ['$window', function ($window) {
    return {
        restrict: 'E',
        link: function (scope, element, attrs) {
            if (!$window.FB) {
                // Load Facebook SDK
                $.getScript('//connect.facebook.net/en_US/sdk.js', function () {
                    $window.FB.init({
                        appId: "1384975458471632",
                        xfbml: true,
                        version: 'v2.0'
                    });
                    renderLikeButton();
                });
            } else {
                renderLikeButton();
            }

            function renderLikeButton() {
                element.html('<div class="fb-like" data-layout="standard" data-action="like" data-show-faces="false" data-share="true"></div><br /><br />');
                $window.FB.XFBML.parse(element.parent()[0]);
            }
        }
    };
}]);

nomadsApp.directive('pinit', ['$window', '$location', function ($window, $location) {
    return {
        restrict: 'E',
        scope: {
            pinIt: '=',
            pinItImage: '=',
            pinItUrl: '='
        },
        link: function (scope, element, attrs) {
            if (!$window.parsePins) {
                // Load Pinterest SDK if not already loaded
                (function (d) {
                    var f = d.getElementsByTagName('SCRIPT')[0], p = d.createElement('SCRIPT');
                    p.type = 'text/javascript';
                    p.async = true;
                    p.src = '//assets.pinterest.com/js/pinit.js';
                    p['data-pin-build'] = 'parsePins';
                    p.onload = function () {
                        if (!!$window.parsePins) {
                            renderPinItButton();
                        } else {
                            setTimeout(p.onload, 100);
                        }
                    };
                    f.parentNode.insertBefore(p, f);
                }($window.document));
            } else {
                renderPinItButton();
            }

            var watchAdded = false;
            function renderPinItButton() {
                if (!scope.pinIt && !watchAdded) {
                    // wait for data if it hasn't loaded yet
                    watchAdded = true;
                    var unbindWatch = scope.$watch('pinIt', function (newValue, oldValue) {
                        if (newValue) {
                            renderPinItButton();

                            // only need to run once
                            unbindWatch();
                        }
                    });
                    return;
                } else {
                    element.html('<a href="//www.pinterest.com/pin/create/button/?url=' + (scope.pinItUrl || $location.absUrl()) + '&media=' + scope.pinItImage + '&description=' + scope.pinIt + '" data-pin-do="buttonPin" data-pin-config="beside"></a>');
                    $window.parsePins(element.parent()[0]);
                }
            }
        }
    };
}]);


nomadsApp.directive("modalButtonBar", function () {
    'use strict';

    return {
        restrict: 'E', //E = element, A = attribute, C = class, M = comment  
        scope: {
            setselected: '&',
            submit: '&',
            datasource: '=',
            selected: '=',
            isnew: '='

        },
        templateUrl: 'Client/Views/Shared/_ModalButtonBar.html',
        replace: true,
        controller: ['$scope', '$timeout', function ($scope, $timeout) {

            $scope.isFirst = function () {
                return $scope.isnew || $scope.selected === 0;
            };

            $scope.isLast = function () {
                if ($scope.isnew) {
                    return true;
                }
                if ($scope.datasource) {
                    return $scope.selected === $scope.datasource.length - 1;
                }
            };

            $scope.next = function () {
                if ($scope.selected < $scope.datasource.length - 1) {
                    var index = $scope.selected + 1;
                    $scope.setselected({ i: index });
                    $scope.setTop();

                }
            };

            $scope.prev = function () {
                if ($scope.selected > 0) {
                    var index = $scope.selected - 1;
                    $scope.setselected({ i: index });
                    $scope.setTop();
                }
            };

            $scope.setTop = function () {
                //Move to library
                $timeout(function () {
                    $('#detailsModal').animate({ scrollTop: 0 }, 'slow');
                }, 0);
            };
        }]
    };
});



nomadsApp.directive("destination", function () {
    'use strict';
    return {
        restrict: 'E',
        scope: { name: '@name', url: '@url', image: '@image' },
        templateUrl: "client/views/shared/_DestinationItem.html",
        replace: true
    };
});

nomadsApp.directive('drinks', function () {
    'use strict';
    return {
        restrict: 'E',
        templateUrl: 'client/views/shared/_DrinksItem.html',
        replace: true
    };
});

nomadsApp.directive('bloglist', function () {
    'use strict';
    return {
        restrict: 'E',
        templateUrl: 'client/views/shared/_BlogList.html',
        replace: true
    };
});


nomadsApp.directive('script', function () {
    return {
        restrict: 'E',
        scope: false,
        link: function (scope, elem, attr) {
            if (attr.type === 'text/javascript-lazy') {
                var code = elem.text();
                var f = new Function(code);
                f();
            }
        }
    };
});

