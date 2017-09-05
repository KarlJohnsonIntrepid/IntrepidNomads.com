Public Class Menu

    Public Shared Function RecentMenuItems() As String

        Dim InnerHTML As New StringBuilder
        For Each dr In Business.Blog.GetRecentBlogs(10)
            InnerHTML.Append("<li><a href=""/blog/" + dr.TitleURL + "/"">" + dr.Title + "</a></li>")
        Next

        Return InnerHTML.ToString


    End Function

    Public Shared Function FeaturedMenuItems() As String

        Dim InnerHTML As New StringBuilder

        Dim dt As List(Of vCategory) = Business.Category.SelectCategoriesFeatured
        If dt IsNot Nothing Then
            For Each dr In dt
                InnerHTML.Append("<li><a href=""/category/" + dr.CategoryURL + "/"">" + dr.CategoryDescription + "</a></li>")
            Next
        End If
   
        Return InnerHTML.ToString

    End Function

    Public Shared Function CategoryMenuItems() As String

        If Business.Category.SelectCategoriesForMenu Is Nothing Then Return ""
        Dim InnerHTML As New StringBuilder
        For Each dr In Business.Category.SelectCategoriesForMenu
            InnerHTML.Append("<li><a href=""/category/" + dr.CategoryURL + "/"">" + dr.CategoryDescription + "</a></li>")
        Next

        Return InnerHTML.ToString

    End Function


    Public Shared Function CountriesMenuItems() As String

        Dim InnerHTML As New StringBuilder

        InnerHTML.Append("<li>")
        InnerHTML.Append("<div class=""row-fluid2"" style=""width: 500px;"">")

        Dim CurrentGroup As String = ""
        For Each dr In Business.Country.SelectCountriesOrderByContinent

            If dr.ContinentDescription <> CurrentGroup Then

                ''New section
                If CurrentGroup <> "" Then
                    ''close pf previous list

                    InnerHTML.Append("</ul>")
                End If

                InnerHTML.Append("<ul class=""menu-column dropdown-menu"">")
                CurrentGroup = dr.ContinentDescription
                InnerHTML.Append("<li class=""dropdown-header"">" + CurrentGroup + "</li>")
            End If

            InnerHTML.Append("<li><a href=""/country/" + dr.CountryURL + "/"">" + dr.CountryDescription + "</a></li>")
        Next

        InnerHTML.Append("</ul>")


        InnerHTML.Append("</div>")
        InnerHTML.Append("</li>")



        Return InnerHTML.ToString

    End Function



End Class
