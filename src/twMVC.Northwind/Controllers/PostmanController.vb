Imports System.Collections.ObjectModel
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Description

<ApiExplorerSettings(IgnoreApi:=True)>
Public Class PostmanController
    Inherits ApiController

    Public Function GetPostman() As HttpResponseMessage
        Dim collection As PostmanCollection = TryCast(Configuration.Properties.GetOrAdd("postmanCollection",
                                        Function(x)
                                            Dim requestUri = Request.RequestUri
                                            Dim baseUri As String = requestUri.Scheme & "://" & requestUri.Host & ":" & requestUri.Port & HttpContext.Current.Request.ApplicationPath
                                            Dim postManCollection As New PostmanCollection()
                                            postManCollection.id = Guid.NewGuid()
                                            postManCollection.name = "Web API Products Service"
                                            postManCollection.timestamp = DateTime.Now.Ticks
                                            postManCollection.requests = New Collection(Of PostmanRequest)()
                                            For Each apiDescription In Configuration.Services.GetApiExplorer().ApiDescriptions
                                                Dim request As PostmanRequest = New PostmanRequest() With {
                                                    .collectionId = postManCollection.id,
                                                    .id = Guid.NewGuid(),
                                                    .method = apiDescription.HttpMethod.Method,
                                                    .url = baseUri.TrimEnd("/") & "/" & apiDescription.RelativePath,
                                                    .description = apiDescription.Documentation,
                                                    .name = apiDescription.RelativePath,
                                                    .data = "",
                                                    .headers = "",
                                                    .dataMode = "params",
                                                    .timestamp = 0
                                                }
                                                postManCollection.requests.Add(request)
                                            Next
                                            Return postManCollection
                                        End Function), PostmanCollection)

        Return Request.CreateResponse(Of PostmanCollection)(HttpStatusCode.OK, collection, "application/json")
    End Function

End Class
