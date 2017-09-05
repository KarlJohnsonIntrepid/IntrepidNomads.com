Public Class Category

#Region "Cache"

    Public Shared Property MyCache As List(Of vCategory)
        Get
            Return HttpContext.Current.Cache("Category")
        End Get
        Set(value As List(Of vCategory))
            HttpContext.Current.Cache("Category") = value
        End Set
    End Property

    Private Shared Sub UpdateCache()
        SelectCategories(True)
    End Sub

#End Region

#Region "Select"

    Public Shared Function SelectCategories(Optional ByVal ClearCache As Boolean = False) As List(Of vCategory)

        If MyCache Is Nothing Or ClearCache Then
            Using Travel As New TravelEntities
                Dim query = From Category In Travel.vCategories
                            Order By Category.CategoryDescription

                MyCache = query.ToList
            End Using
        End If
        Return MyCache
    End Function

    Public Shared Function SelectCategoriesNotFeatured() As List(Of vCategory)

        Dim query = From c In SelectCategories()
                      Where BlankLibrary.NoBlank(c.IsFeature, False) = False

        If query.Count > 0 Then
            Return query.ToList
        Else : Return Nothing
        End If
    End Function

    Public Shared Function SelectCategoriesForMenu() As List(Of vCategory)

        Dim query = From c In SelectCategoriesNotFeatured()
                      Where c.CategoryDescription <> "Drink Reviews" And c.CategoryDescription <> "Blog" And c.ShowInMenu = True

        If query.Count > 0 Then
            Return query.ToList
        Else : Return Nothing
        End If
    End Function

    Public Shared Function SelectCategoriesBlogFilter() As List(Of vCategory)

        Dim query = From c In SelectCategoriesNotFeatured()
        '' Where c.CategoryDescription <> "Drink Reviews" And c.CategoryDescription <> "Blog"

        If query.Count > 0 Then
            Return query.ToList
        Else : Return Nothing
        End If
    End Function


    Public Shared Function SelectCategoriesFeatured() As List(Of vCategory)

        Dim query = From c In SelectCategories()
                      Where BlankLibrary.NoBlank(c.IsFeature, False) = True

        If query.Count > 0 Then
            Return query.ToList
        Else : Return Nothing
        End If
    End Function


    Public Shared Function SelectChildCategories(CategoryID, CountryID) As List(Of vCategory)

        Dim query = From c In SelectCategories()
                      Where BlankLibrary.NoBlank(c.ParentCategoryID, -1) = CategoryID Or BlankLibrary.NoBlank(c.ParentCountryID, -1) = CountryID _
                      And CountryID <> 0 And CategoryID <> 0

        If query.Count > 0 Then
            Return query.ToList
        Else : Return Nothing
        End If
    End Function

    Public Shared Function GetCategoryRow(CategoryID) As vCategory

        Dim query = From c In SelectCategories()
                    Where c.CategoryID = CategoryID

        If query.Count > 0 Then
            Return query.First
        Else : Return Nothing
        End If
    End Function

#End Region

#Region "Update/Insert/Delete"

    Public Shared Sub Delete(CategoryID As Integer)

        Using Travel As New TravelEntities
            Dim c = (From Category In Travel.Categories
                     Where Category.CategoryID = CategoryID).First

            Travel.Categories.Remove(c)
            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub

    Public Shared Sub Insert(Category As DAL.Category)
        Using context As New TravelEntities
            context.Categories.Add(Category)
            context.SaveChanges()
        End Using

        UpdateCache()

    End Sub

    Public Shared Sub Insert(CategoryDescription As String, CategoryInformation As String, ReverseOrder As Nullable(Of Boolean), IsFeature As Nullable(Of Boolean), ParentCategoryID As Nullable(Of Integer),
                             ParentCountryID As Nullable(Of Integer), SEOTitle As String, SEODescription As String, ShowInMenu As Nullable(Of Boolean), CategoryImageID As Nullable(Of Integer))

        Dim Category As New DAL.Category
        With Category
            .CategoryDescription = RTrim(CategoryDescription)
            .CategoryInformation = CategoryInformation
            .ReverseDateOrder = ReverseOrder
            .IsFeature = IsFeature
            .ParentCategoryID = ParentCategoryID
            .ParentCountryID = ParentCountryID
            .SEOTitle = SEOTitle
            .SEODescription = SEODescription
            .ShowInMenu = ShowInMenu
            .CategoryImageID = CategoryImageID
        End With

        Using context As New TravelEntities
            context.Categories.Add(Category)
            context.SaveChanges()
        End Using

        UpdateCache()

    End Sub


    Public Shared Sub Update(CategoryID As String, CategoryDescription As String, CategoryInformation As String, ReverseOrder As Boolean, IsFeature As Boolean,
                             ParentCategoryID As String, ParentCountryID As String, SEOTitle As String, SEODescription As String, ShowInMenu As Boolean, CategoryImageID As Integer)

        Using Travel As New TravelEntities
            Dim c = (From Category In Travel.Categories
                     Where Category.CategoryID = CategoryID).First

            c.CategoryID = CategoryID
            c.CategoryDescription = RTrim(CategoryDescription)
            c.CategoryInformation = CategoryInformation
            c.ReverseDateOrder = ReverseOrder
            c.IsFeature = IsFeature
            c.ParentCategoryID = ParentCategoryID
            c.ParentCountryID = ParentCountryID
            c.SEOTitle = SEOTitle
            c.SEODescription = SEODescription
            c.ShowInMenu = ShowInMenu
            c.CategoryImageID = CategoryImageID

            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub

    Public Shared Sub Update(Category As DAL.Category, Id As Integer)

        Using Travel As New TravelEntities
            Dim updated = (From c In Travel.Categories
                           Where c.CategoryID = Id).First


            Travel.Entry(updated).CurrentValues.SetValues(Category)

            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub

#End Region

End Class
