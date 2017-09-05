﻿'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core.Objects
Imports System.Data.Enitity.Objects.DataClasses
Imports System.Linq
Imports System.Data.Entity.Core

Partial Public Class TravelEntities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=TravelEntities")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Property Authors() As DbSet(Of Author)
    Public Property Blogs() As DbSet(Of Blog)
    Public Property Categories() As DbSet(Of Category)
    Public Property Continents() As DbSet(Of Continent)
    Public Property Countries() As DbSet(Of Country)
    Public Property Locations() As DbSet(Of Location)
    Public Property vBlogs() As DbSet(Of vBlog)
    Public Property vCategories() As DbSet(Of vCategory)
    Public Property vCountries() As DbSet(Of vCountry)
    Public Property vUploadedImages() As DbSet(Of vUploadedImage)
    Public Property UploadedImages() As DbSet(Of UploadedImage)

    Public Overridable Function spRelatedBlogs(blogID As Nullable(Of Integer)) As Integer
        Dim blogIDParameter As ObjectParameter = If(blogID.HasValue, New ObjectParameter("BlogID", blogID), New ObjectParameter("BlogID", GetType(Integer)))

        Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction("spRelatedBlogs", blogIDParameter)
    End Function

    Public Overridable Function spTypedRelatedBlogs(blogID As Nullable(Of Integer)) As ObjectResult(Of vBlog)
        Dim blogIDParameter As ObjectParameter = If(blogID.HasValue, New ObjectParameter("BlogID", blogID), New ObjectParameter("BlogID", GetType(Integer)))

        Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction(Of vBlog)("spTypedRelatedBlogs", blogIDParameter)
    End Function

    Public Overridable Function spTypedRelatedBlogs(blogID As Nullable(Of Integer), mergeOption As MergeOption) As ObjectResult(Of vBlog)
        Dim blogIDParameter As ObjectParameter = If(blogID.HasValue, New ObjectParameter("BlogID", blogID), New ObjectParameter("BlogID", GetType(Integer)))

        Return DirectCast(Me, IObjectContextAdapter).ObjectContext.ExecuteFunction(Of vBlog)("spTypedRelatedBlogs", mergeOption, blogIDParameter)
    End Function

End Class
