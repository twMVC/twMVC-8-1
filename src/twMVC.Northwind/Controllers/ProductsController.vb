Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.OData
Imports System.Web.Http.OData.Query

Public Class ProductsController
    Inherits System.Web.Http.ApiController

    Private db As New NorthwindEntities

    '' GET api/Products
    ''' <summary>
    ''' 回傳所有產品資料
    ''' </summary>
    Function GetProducts() As IEnumerable(Of Product)
        Return db.Products.AsEnumerable()
    End Function

    ' GET api/Products/5
    ''' <summary>
    ''' 回傳某一筆產品資料
    ''' </summary>
    ''' <param name="id">產品編號</param>
    Function GetProduct(ByVal id As Integer) As Product
        Dim product As Product = db.Products.Find(id)
        If IsNothing(product) Then
            Throw New HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound))
        End If
        Return product
    End Function

    ' PUT api/Products/5
    ''' <summary>
    ''' 更新某一筆產品資料
    ''' </summary>
    ''' <param name="id">產品編號</param>
    ''' <param name="product">產品物件</param>
    Function PutProduct(ByVal id As Integer, ByVal product As Product) As HttpResponseMessage
        If Not ModelState.IsValid Then
            Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
        End If

        If Not id = product.ProductID Then
            Return Request.CreateResponse(HttpStatusCode.BadRequest)
        End If

        db.Entry(product).State = EntityState.Modified

        Try
            db.SaveChanges()
        Catch ex As DbUpdateConcurrencyException
            Return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex)
        End Try

        Return Request.CreateResponse(HttpStatusCode.OK)
    End Function

    ' POST api/Products
    ''' <summary>
    ''' 新增一筆產品資料
    ''' </summary>
    ''' <param name="product">產品物件</param>
    Function PostProduct(ByVal product As Product) As HttpResponseMessage
        If ModelState.IsValid Then
            db.Products.Add(product)
            db.SaveChanges()

            Dim response As HttpResponseMessage = Request.CreateResponse(HttpStatusCode.Created, product)
            response.Headers.Location = New Uri(Url.Link("DefaultApi", New With {.id = product.ProductID}))
            Return response
        Else
            Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
        End If
    End Function

    ' DELETE api/Products/5
    ''' <summary>
    ''' 刪除某一筆產品資料
    ''' </summary>
    ''' <param name="id">產品編號</param>
    Function DeleteProduct(ByVal id As Integer) As HttpResponseMessage
        Dim product As Product = db.Products.Find(id)
        If IsNothing(product) Then
            Return Request.CreateResponse(HttpStatusCode.NotFound)
        End If

        db.Products.Remove(product)

        Try
            db.SaveChanges()
        Catch ex As DbUpdateConcurrencyException
            Return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex)
        End Try

        Return Request.CreateResponse(HttpStatusCode.OK, product)
    End Function

    ' PATCH api/Products/5
    ''' <summary>
    ''' 僅更新產品名稱
    ''' </summary>
    ''' <param name="id">產品編號</param>
    ''' <param name="data">例用白名單過濾(Bind屬性)</param>
    Function PatchProduct(ByVal id As Integer, <Bind(include:="ProductName")> data As Product) As HttpResponseMessage
        Dim product As Product = db.Products.Find(id)

        If IsNothing(product) Then
            Throw New HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound))
        End If

        product.ProductName = data.ProductName
        db.Entry(product).State = EntityState.Modified

        Try
            db.SaveChanges()
        Catch ex As DbUpdateConcurrencyException
            Return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex)
        End Try

        Return Request.CreateResponse(HttpStatusCode.OK)
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub
End Class