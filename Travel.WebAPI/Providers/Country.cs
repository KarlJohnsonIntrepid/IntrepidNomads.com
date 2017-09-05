//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Travel.WebAPI.Providers
{
    using System;
    using System.Collections.Generic;
    
    public partial class Country
    {
        public Country()
        {
            this.Blogs = new HashSet<Blog>();
        }
    
        public int CountryID { get; set; }
        public string CountryDescription { get; set; }
        public Nullable<int> ContinentID { get; set; }
        public string CountryInformation { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public Nullable<int> CountryImageID { get; set; }
    
        public virtual Continent Continent { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}