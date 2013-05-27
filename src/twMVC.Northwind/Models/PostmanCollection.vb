Imports System.Collections.ObjectModel

Public Class PostmanCollection
    Property id As Guid
    Property name As String
    Property timestamp As Long
    Property requests As Collection(Of PostmanRequest)
End Class
