Imports System.Web.Http

Public Class MyQueryableAttribute
    Inherits QueryableAttribute

    Public Overrides Sub ValidateQuery(request As Net.Http.HttpRequestMessage,
                                       queryOptions As OData.Query.ODataQueryOptions)

        If queryOptions.OrderBy IsNot Nothing Then
            queryOptions.OrderBy.Validator = New MyOrderByValidator()
        End If

        MyBase.ValidateQuery(request, queryOptions)
    End Sub

End Class
