using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.WebAPI.Models
{
    public class CategoryModel
    {
        public string CategoryURL;
        public int CategoryID;
        public string CategoryDescription;
        public string CategoryInformation;
        public bool ReverseDateOrder;
        public bool IsFeature;
        public int ParentCategoryID;
        public int ParentCountryID;
        public string SEOTitle;
        public string SEODescription;
        public bool ShowInMenu;
        public int CategoryImageID;
        public string ImageDescription;
    }
}