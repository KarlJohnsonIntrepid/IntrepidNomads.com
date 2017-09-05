//Destination
nomadsApp.factory("destinationModel", function () {
    'use strict';

    return function (destination) {

        var d = destination;
        var _buildImageUrl = function () {
            return "http://intrepidnomads.com/images/uploads/thumbnail/" + destination.ImageDescription;
        };

        var _buildThumbnailUrl = function () {
            return "/images/uploads/thumbnail/" + destination.ImageDescription;
        };

        var _buildCountryUrl = function () {
            return "/country/" + destination.CountryURL + "/";
        };

        d.countryID = destination.CountryID;
        d.countryDescription = destination.CountryDescription;
        d.countryURL = _buildCountryUrl();
        d.continentDescription = destination.ContinentDescription;
        d.continentID = destination.ContinentID;
        d.countryInformation = destination.CountryInformation;
        d.seoTitle = destination.SEOTitle;
        d.seoDescription = destination.SEODescription;
        d.countryImageID = destination.CountryImageID;
        d.imageDescription = destination.ImageDescription;
        d.imageURL = _buildImageUrl();
        d.thumbnailURL = _buildThumbnailUrl();

        return d;
    }
});


//Category Model
nomadsApp.factory("categoryModel", function () {
    'use strict';

    return function (category) {

        var c = {};

        var _buildImageUrl = function () {
            return "/images/uploads/medium/" + category.ImageDescription;
        };

        var _buildThumbnailUrl = function () {
            return "/images/uploads/thumbnail/" + category.ImageDescription;
        };

        var _buildLinkUrl = function () {
            return "/category/" + category.CategoryURL + "/";
        };

        var _buildDescription = function () {
            return category.CategoryDescription.replace("Diary", "").replace("diary", "");
        };

        c.categoryID = category.CategoryID;
        c.categoryDescription = _buildDescription();
        c.categoryInformation = category.CategoryInformation;
        c.reverseDateOrder = category.ReverseDateOrder;
        c.isFeature = category.IsFeature;
        c.parentCategoryID = category.ParentCategoryID;
        c.parentCountryID = category.ParentCountryID;
        c.seoTitle = category.SEOTitle;
        c.seoDescription = category.SEODescription;
        c.showInMenu = category.ShowInMenu;
        c.categoryImageID = category.CategoryImageID;
        c.imageURL = _buildImageUrl();
        c.linkURL = _buildLinkUrl();
        c.categoryURL = category.CategoryURL;
        c.thumbnailURL = _buildThumbnailUrl();

        return c;

    }
});

//Blog Model
nomadsApp.factory("blogModel", function () {
    'use strict';
    return function (blog) {

        var b = {}

        var _buildImageUrl = function () {
            return "/images/uploads/medium/" + blog.CategoryImageDescription;
        };

        var _buildThumbnailUrl = function () {
            return "/images/uploads/thumbnail/" + blog.CategoryImageDescription;
        };

        var _buildLinkUrl = function () {
            return "/blog/" + blog.TitleURL + "/";
        };

        var _buildContentPreview = function () {
            if (blog.NiceDescription) {
                return blog.NiceDescription;
            } else {
                return blog.ContentPreview;
            }
        };

       b.blogID = blog.BlogID;
       b.title = blog.Title;
       b.titleURL = blog.TitleURL;
       b.dateCreated = new Date(blog.DateCreated);
       b.content = blog.Content;
       b.contentPreview = _buildContentPreview();
       b.authorName = blog.AuthorName;
       b.countryDescription = blog.CountryDescription;
       b.categoryDescription = blog.CategoryDescription;
       b.countryID = blog.CountryID;
       b.categoryID = blog.CategoryID;
       b.continentDescription = blog.ContinentDescription;
       b.authorID = blog.AuthorID;
       b.date = new Date(blog.Date);
       b.isFeature = blog.IsFeature;
       b.categoryImageID = blog.CategoryImageID;
       b.categoryImageDescription = blog.CategoryImageDescription;
       b.isVisible = blog.IsVisible;
       b.isFullScreen = blog.IsFullScreen;
       b.showInSlideShow = blog.ShowInSlideShow;
       b.seoTitle = blog.SEOTitle;
       b.seoDescription = blog.SEODescription;
       b.niceDescription = blog.NiceDescription;
       b.showPhotos = blog.ShowPhotos;
       b.imageURL = _buildImageUrl();
       b.linkURL = _buildLinkUrl();
       b.thumbnailURL = _buildThumbnailUrl();

       return b;
   }
})

nomadsApp.factory("imageModel", function() {

    'use strict';

    return function (image) {

        var i = {};

        var _buildOriginalImageUrl = function () {
            return "http://intrepidnomads.com/images/uploads/original/" + image.ImageDescription;
        };

        var _buildThumbnailURL = function () {
            return "/images/uploads/thumbnail/" + image.ImageDescription;
        };

          var _buildMediumURL = function () {
            return "/images/uploads/medium/" + image.ImageDescription;
        };

        i.imageID = image.ImageID;
        i.imageDescription = image.ImageDescription;
        i.imageCaption = image.ImageCaption;
        i.blogID = image.BlogID;
        i.dateUploaded = new Date(image.DateUploaded);
        i.blogTitle = image.BlogTitle;
        i.originalImageURL = _buildOriginalImageUrl();
        i.thumbnailImageURL = _buildThumbnailURL();
        i.mediumImageURL = _buildMediumURL();

        return i;
    }
});


nomadsApp.factory("continentModel", function() {

    'use strict';

    return function (continent) {

        var c = {};

        c.continentID = continent.ContinentID;
        c.continentDescription = continent.ContinentDescription;

        return c;
    }
});

nomadsApp.factory("locationModel", function() {
    'use strict';

    return function (location) {

       var l = {};

       l.locationID = location.LocationID;
       l.latitude = location.Latitude;
       l.longitude = location.Longitude;
       l.location1 = location.Location1;
       l.date = new Date(location.Date);

        return l;
    }
});