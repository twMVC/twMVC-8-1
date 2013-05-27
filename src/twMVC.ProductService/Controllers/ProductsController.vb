Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.OData

Public Class ProductsController
    Inherits EntitySetController(Of Product, Integer)

    Private db As New NorthwindEntities()

    <Queryable()>
    Public Overrides Function [Get]() As IQueryable(Of Product)
        Return db.Products.AsQueryable()
    End Function

    Protected Overrides Function GetEntityByKey(key As Integer) As Product
        Return db.Products.FirstOrDefault(Function(p) p.ProductID = key)
    End Function

    Protected Overrides Function CreateEntity(entity As Product) As Product
        db.Products.Add(entity)
        db.SaveChanges()
        Return entity
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub
End Class
