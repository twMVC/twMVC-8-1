Imports System.Web.Http.OData.Query.Validators
Imports Microsoft.Data.OData

Public Class MyOrderByValidator
    Inherits OrderByQueryValidator

    Public Overrides Sub Validate(orderByOption As Http.OData.Query.OrderByQueryOption,
                                  validationSettings As Http.OData.Query.ODataValidationSettings)

        If orderByOption.OrderByNodes.Any(Function(node) node.Direction = Microsoft.Data.OData.Query.OrderByDirection.Descending) Then
            Throw New ODataException("不支援desc選項！")
        End If

        MyBase.Validate(orderByOption, validationSettings)
    End Sub
End Class

