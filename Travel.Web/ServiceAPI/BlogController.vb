Imports System.Net
Imports System.Web.Http
Imports Newtonsoft.Json

Public Class BlogController
    Inherits ApiController

    ' GET api/<controller>
    Public Function GetValues() As String
        Dim Blog As List(Of BlogModel) = AutoMapper.Mapper.Map(Of List(Of DAL.vBlog), List(Of BlogModel))(Business.Blog.SelectBlogs)
        Return JsonConvert.SerializeObject(Blog)
    End Function

    ' GET api/<controller>/5
    Public Function GetValue(ByVal id As Integer) As String
        Dim blog = Business.Blog.GetBlogItem(id)

        If blog IsNot Nothing Then
            Dim blogDTO As BlogModel = AutoMapper.Mapper.Map(Of DAL.vBlog, BlogModel)(blog)
            Return JsonConvert.SerializeObject(blogDTO)

        Else : Return "Not Found for property " & id
        End If
    End Function

End Class
