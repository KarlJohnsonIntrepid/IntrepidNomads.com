Imports System.Web.Routing
Imports Microsoft.AspNet.FriendlyUrls
Imports System.Web.Http
Imports System.Net

Public Module RouteConfig
    Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.Clear()

        routes.EnableFriendlyUrls()

        routes.MapPageRoute("About", "about", "~/pages/about.aspx")
        routes.MapPageRoute("Contact", "contact", "~/pages/contact.aspx")
        routes.MapPageRoute("default", "", "~/pages/home.aspx")
        routes.MapPageRoute("Drinks", "around-the-world-in-80-drinks", "~/pages/drinks.aspx")
        routes.MapPageRoute("Destinations", "destinations", "~/pages/countries.aspx")
        routes.MapPageRoute("Blog", "blog", "~/pages/bloglist.aspx")
        routes.MapPageRoute("Diaries", "diaries", "~/pages/diaries.aspx")
        routes.MapPageRoute("Locations", "locations", "~/pages/locations.aspx")

        routes.MapPageRoute("RSS", "rss", "~/rss.ashx")

        ''Generate route from blog caption and blog ID
        Dim Description As String
        Dim BlogID As String

        For Each row As vBlog In Business.Blog.SelectBlogs(False, True)
            Description = Replace(WebUtility.UrlDecode(row.TitleURL), "?", "")
            BlogID = row.BlogID

            routes.MapPageRoute(Description + BlogID, "blog/" + Description + "/{id}", "~/pages/blog.aspx", True, New RouteValueDictionary(New With {.id = BlogID}))
        Next

        ''Generate route for categories page
        Dim CategoryDescription As String
        Dim CategoryID As String

        For Each row As vCategory In Business.Category.SelectCategories
            CategoryDescription = row.CategoryURL
            CategoryID = row.CategoryID

            routes.MapPageRoute(CategoryDescription + CategoryID, "category/" + CategoryDescription + "/{id}", "~/Pages/category.aspx", True, New RouteValueDictionary(New With {.id = CategoryID}))
        Next

        ''Generate route for countries page
        Dim CountryDescription As String
        Dim CountryID As String

        For Each row As vCountry In Business.Country.SelectCountries
            CountryDescription = row.CountryURL
            CountryID = row.CountryID

            routes.MapPageRoute(CountryDescription + CountryID, "country/" + CountryDescription + "/{id}/{IsCountry}", "~/Pages/category.aspx", True, New RouteValueDictionary(New With {.id = CountryID, .IsCountry = True}))
        Next


        ''Service WEB API
        RouteTable.Routes.MapHttpRoute(name:="DefaultApi", routeTemplate:="api/{controller}/{id}", defaults:=New With { _
                 Key .id = System.Web.Http.RouteParameter.[Optional] _
            })

        ''Ensure routes dont effect static files and toolkit ipmage uploader

        routes.Ignore("{*alljs}", New With { _
     Key .alljs = ".*\.js(/.*)?" _
         })
        routes.Ignore("{*alljpg}", New With { _
            Key .alljpg = ".*\.jpg(/.*)?" _
        })
        routes.Ignore("{*alljpeg}", New With { _
           Key .alljpeg = ".*\.jpeg(/.*)?" _
       })
        routes.Ignore("{*allcss}", New With { _
            Key .allcss = ".*\.css(/.*)?" _
        })

        routes.Ignore("{*allaxd}", New With { _
        Key .allaxd = ".*\.axd(/.*)?" _
    })


        ''Catch all 404
        ' routes.MapPageRoute("404", "{*url}", "~/pages/error/404.aspx")

    End Sub
End Module
