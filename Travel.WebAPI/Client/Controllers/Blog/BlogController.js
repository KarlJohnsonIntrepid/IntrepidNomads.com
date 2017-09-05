nomadsApp.controller("blogController", ['$scope', '$routeParams', 'blogHttpFacade', 'imageHttpFacade', '$timeout', 'page', '$sce', 'blogModel', 'modelFactory', 'imageModel', function ($scope, $routeParams, blogHttpFacade, imageHttpFacade, $timeout, page, $sce, blogModel, modelFactory, imageModel) {
    'use strict';
    $scope.initializing = 0;

    blogHttpFacade.getBlogByTitle($routeParams.destination, $routeParams.category, $routeParams.title).success(function (data) {

        $scope.blog = blogModel(JSON.parse(data));

        page.setTitle($scope.blog.seoTitle);
        page.setDescription($scope.blog.seoDescription);
        page.setShowSideBar($scope.blog.isFullScreen);
        page.setogImage("http://intrepidnomads.com" + $scope.blog.imageURL);
        page.setogURL("http://intrepidnomads.com" + $scope.blog.linkURL);

        if ($scope.blog.isFeature) {
            $scope.subTitle = "Featured In - " + $scope.blog.categoryDescription;
        }

        $timeout(function () {
            $('.blog-post img').addClass('img-responsive').addClass('fade-in').attr("spinner-load", "");
        }, 0);

        $scope.initializing = $scope.initializing + 1;


        //Load Images
        imageHttpFacade.selectImagesByBlog($scope.blog.blogID).success(function (data) {

            if ($scope.blog.showPhotos) {
                $scope.images = modelFactory.load(data, imageModel);
               
            }    
            
            $scope.initializing = $scope.initializing + 1;

        }).error(function (data, status) {
            console.log(data, status);
        });
   
        $scope.loadRelated($scope.blog.blogID);

    }).error(function (data, status) {
        console.log(data, status);
    });

    $scope.bindHTML = function (html) {
        return $sce.trustAsHtml(html);
    };


    $scope.loadRelated = function (blogId) {

        if ($scope.blog.isFeature === true) {
            //Load only categorys
            blogHttpFacade.getBlogsByCategory($scope.blog.categoryID).success(function (data) {
                $scope.related = modelFactory.load(data, blogModel);

                 $scope.initializing = $scope.initializing + 1;
            }).error(function (data, status) {
                console.log(data, status);
            });

        } else {

            blogHttpFacade.getBlogsRelated(blogId, 12).success(function (data) {
                $scope.related = modelFactory.load(data, blogModel);
                 $scope.initializing = $scope.initializing + 1;
            }).error(function (data, status) {
                console.log(data, status);
            });
        }
    };

}]);