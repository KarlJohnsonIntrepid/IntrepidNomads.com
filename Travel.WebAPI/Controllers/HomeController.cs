using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travel.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Home()
        {

            int numArticles = 20;
            ViewBag.Blogs = Business.Blog.GetRecentBlogs(numArticles);

            var RandomCountries = Business.Country.GetCountries(numArticles).OrderBy(x => Guid.NewGuid()).Take(numArticles);
            var ExcludedCountries = new List<string> { "Italy", "Scotland","Jordan"};
            RandomCountries = RandomCountries.Where(x => !ExcludedCountries.Contains(x.CountryDescription));

            ViewBag.Destinations = RandomCountries;
            ViewBag.Diaries = Business.Category.SelectCategoriesFeatured().Take(numArticles);
            return View();
        }

        [AjaxCrawlable]
        public ActionResult Index()
        {

           DateTime DateOfDeparture = new DateTime(2015, 5, 2);
           ViewBag.DaysOnTheRoad = Math.Ceiling(DateTime.Now.Subtract(DateOfDeparture).TotalDays);

            ViewBag.RecentBlogs = Business.Blog.GetRecentBlogs(10);

            return View();
        }
    }
}
