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
    
    public partial class Author
    {
        public Author()
        {
            this.Blogs = new HashSet<Blog>();
        }
    
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
    
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
