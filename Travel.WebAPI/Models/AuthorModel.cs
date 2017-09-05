using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Travel.WebAPI.Models
{
   
    public class AuthorModel
    {

        public int ID { get; set; }
        public String AuthorName { get; set; }
    }
}