using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.WebAPI.Models
{
    public class ImageModel
    {
        public int ImageID;
        public string ImageDescription;
        public string ImageCaption;
        public Nullable<int> BlogID;
        public System.DateTime DateUploaded;
        public string BlogTitle;
    }
}