using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.WebAPI.Models
{
    public class BlogModel
    {
        public int BlogID { get; set; }
        public string Title { get; set; }
        public string TitleURL { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string Content { get; set; }
        public string ContentPreview { get; set; }
        public string AuthorName { get; set; }
        public string CountryDescription { get; set; }
        public string CategoryDescription { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string ContinentDescription { get; set; }
        public Nullable<int> AuthorID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public bool IsFeature { get; set; }
        public Nullable<int> CategoryImageID { get; set; }
        public string CategoryImageDescription { get; set; }
        public bool IsVisible { get; set; }
        public bool IsFullScreen { get; set; }
        public bool ShowInSlideShow { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string NiceDescription { get; set; }
        public bool ShowPhotos { get; set; }

    }
}