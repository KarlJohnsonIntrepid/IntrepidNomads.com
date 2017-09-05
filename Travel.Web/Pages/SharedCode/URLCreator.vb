Public Class URLCreator

    Public Shared Function BlogURL(ByVal URL As String, IsCategoryPage As Boolean, CategoryID As String) As String

        Dim page = EnvironmentLibrary.CurrentPage

        URL = page.ResolveUrl("~/blog/" + URL) + "/"
        If IsCategoryPage Then

            Dim Category As vCategory = Business.Category.GetCategoryRow(CategoryID)
            Dim IsFeatured = BlankLibrary.NoBlank(Category.IsFeature, False)
            If IsFeatured Then
                Return URL + "?IsFeature=1"
            End If
        End If

        Return URL

    End Function
End Class
