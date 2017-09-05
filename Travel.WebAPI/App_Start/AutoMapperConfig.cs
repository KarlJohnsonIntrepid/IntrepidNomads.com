using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.WebAPI
{
    public class AutoMapperConfig
    {

        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<DAL.Author, Models.AuthorModel>();
            AutoMapper.Mapper.CreateMap<DAL.Continent, Models.ContinentModel>();
            AutoMapper.Mapper.CreateMap<DAL.vBlog, Models.BlogModel>();
            AutoMapper.Mapper.CreateMap<DAL.vCategory, Models.CategoryModel>();
            AutoMapper.Mapper.CreateMap<DAL.vCountry, Models.CountryModel>();
            AutoMapper.Mapper.CreateMap<DAL.vUploadedImage, Models.ImageModel>();
            AutoMapper.Mapper.CreateMap<DAL.Location, Models.LocationModel>();
        }
    }
}