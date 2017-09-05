nomadsApp.controller('adminBlogController', ["$scope", "$location", "filterFilter", "blogModel", "blogHttpFacade", "tinymceOptions", "imageHttpFacade", "imageModel", "categoryHttpFacade", "destinationHttpFacade", "modelFactory", "categoryModel", "destinationModel", "pager", function ($scope,$location, filterFilter, blogModel, blogHttpFacade, tinymceOptions, imageHttpFacade, imageModel, categoryHttpFacade, destinationHttpFacade, modelFactory, categoryModel, destinationModel, pager) {
    'use strict';
    $scope.tinymceOptions = tinymceOptions;
    $scope.pager = pager;
    $scope.initializing = true;
    $scope.filter = '';
    $scope.selectedIndex = 0;
    $scope.isNew = false;
    $scope.currentPage = 1;

    $scope.getBlogs = function () {
        blogHttpFacade.getBlogsAdmin().success(function (data) {
            $scope.blogs = modelFactory.load(data, blogModel);
            $scope.loadGrid();
            $scope.initializing = false;
        }).error(function (data, status) {
            console.log(data, status);
        });
    };

    $scope.getBlogs();

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
        $scope.loadImages();

        //SetDefault Values
        $scope.selectedItem.categoryID = 10;
        $scope.selectedItem.authorID = 1;

    };

      //Grid -------------------------------------------

    $scope.loadGrid = function () {
        $scope.filterList = filterFilter($scope.blogs, $scope.filter);
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
        $scope.loadImages();
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
        blogHttpFacade.update($scope.selectedItem).success(function () {
            $scope.getBlogs();
            toastr.success('Blog updated');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };

    $scope.save = function () {
        blogHttpFacade.save($scope.selectedItem).success(function () {
            $scope.getBlogs();
            toastr.success('Blog added successfully');
        }).error(function (data) {
            toastr.error(data.Message);
        });
    };

    $scope.deleteRow = function (i) {

        if (confirm('Are you certain you want to delete?')) {
            var id = $scope.pagedItems[i].blogID;
            blogHttpFacade.delete(id).success(function () {
                $scope.getBlogs();
                toastr.success('Blog deleted successfully');
            }).error(function (data) {
                toastr.error(data.Message);
            });
        }
    };

    $scope.categoryImageChanged = function () {
        var ImageId = $scope.pagedItems[$scope.selectedItem].categoryImageID;
        //Update image, need image description to do so

    };

    $scope.authors = [
        {"key": 1, "value": "Karl Johnson"},
        {"key": 2, "value": "Leanne Reveley"},
        {"key": 3, "value": "Karl and Leanne"}
    ];


    $scope.open1 = function () {
        $scope.datepicker.opened = true;
    };

    $scope.datepicker = {
        opened: false
    };

  
    categoryHttpFacade.getCategories().success(function (data) {
        $scope.categories = modelFactory.load(data, categoryModel);
    }).error(function (status, data) {
        console.log(status, data);
    });

    destinationHttpFacade.getDestinations().success(function (data) {
        $scope.destinations = modelFactory.load(data, destinationModel);
    }).error(function (status, data) {
        console.log(status, data);
    });


    $scope.loadImages = function () {

        if ($scope.isNew === true) {
            $scope.images = {};
        } else {

            imageHttpFacade.selectImagesByBlog($scope.selectedItem.blogID).success(function (data) {
                $scope.images = modelFactory.load(data, imageModel);
            }).error(function (data, status) {
                console.log(data, status);
            });
        }

    };

    $scope.addImages = function () {
        if ($scope.form.$valid) {
            $scope.submitForm();
            var url = 'uploadimages?blogId=' + $scope.selectedItem.blogID;
            window.location.href = url;
        }
    };
}]);