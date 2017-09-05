Public Class ImageUpload

#Region "Cache"

    Public Shared Property MyCache As List(Of vUploadedImage)
        Get
            Return HttpContext.Current.Cache("vUploadedImage")
        End Get
        Set(value As List(Of vUploadedImage))
            HttpContext.Current.Cache("vUploadedImage") = value
        End Set
    End Property

    Public Shared Sub UpdateCache()
        SelectImages(True)
    End Sub

#End Region

#Region "Select"

    Public Shared Function SelectImages(Optional ByVal ClearCache As Boolean = False) As List(Of vUploadedImage)

        ''Note: This method is cached

        If MyCache Is Nothing Or ClearCache Then
            Using Travel As New TravelEntities
                Dim q = From a In Travel.vUploadedImages
                        Order By a.DateUploaded Descending
                MyCache = q.ToList
            End Using

        End If
        Return MyCache

    End Function

    Public Shared Function SelectImagesByBlog(BlogID As String) As List(Of vUploadedImage)

        Dim query = From b In SelectImages()
                    Where BlankLibrary.NoBlank(b.BlogID, 0) = BlogID
                    Order By b.ImageDescription

        If query.Count > 0 Then
            Return query.tolist
        Else : Return New List(Of vUploadedImage)
        End If

    End Function

    Public Shared Function GetImageRow(ImageID) As vUploadedImage

        Dim query = From b In SelectImages()
                    Where b.ImageID = ImageID

        If query.Count > 0 Then
            Return query.First
        Else : Return Nothing
        End If

    End Function

#End Region

#Region "Insert/Update/Delete"

    Public Shared Sub Insert(Image As DAL.UploadedImage)

        Image.DateUploaded = System.DateTime.Now

        Try
            Using context As New TravelEntities
                context.UploadedImages.Add(Image)
                context.SaveChanges()
            End Using
        Catch ex As Exception
            Throw ex
        End Try


        ''Cache is update when all images have been updated ( fileUplloadTest_UploadCompleteAll)

    End Sub

    Public Shared Sub Insert(ImageDescription As String, ImageCaption As String, BlogID As Nullable(Of Integer))

        Dim Image As New DAL.UploadedImage
        With Image
            .ImageDescription = ImageDescription
            .ImageCaption = ImageCaption
            .BlogID = BlankLibrary.NoBlank(BlogID, Nothing)
            .DateUploaded = Now
        End With


        Try
            Using context As New TravelEntities
                context.UploadedImages.Add(Image)
                context.SaveChanges()
            End Using
        Catch ex As Exception
            Throw ex
        End Try


        ''Cache is update when all images have been updated ( fileUplloadTest_UploadCompleteAll)

    End Sub


    Public Shared Sub Delete(ImageID As Integer)

        Using Travel As New TravelEntities
            Dim c = (From Image In Travel.UploadedImages
                       Where Image.ImageID = ImageID).First

            Travel.UploadedImages.Remove(c)
            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub


    Public Shared Sub Update(ImageID As String, ImageDescription As String, ImageCaption As String, BlogID As String)

        Using Travel As New TravelEntities
            Dim c = (From Image In Travel.UploadedImages
                     Where Image.ImageID = ImageID).First

            c.ImageDescription = ImageDescription
            c.ImageCaption = ImageCaption
            c.BlogID = BlogID

            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub

    Public Shared Sub Update(Image As DAL.UploadedImage, Id As Integer)

        Using Travel As New TravelEntities
            Dim updated = (From i In Travel.UploadedImages
                           Where i.ImageID = Id).First


            Travel.Entry(updated).CurrentValues.SetValues(Image)

            Travel.SaveChanges()
        End Using

        UpdateCache()

    End Sub
#End Region







End Class
