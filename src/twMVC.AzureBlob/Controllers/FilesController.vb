Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports Microsoft.WindowsAzure.StorageClient

Public Class FilesController
    Inherits ApiController

    Function PostFile() As Task(Of List(Of FileDetails))
        If Not Request.Content.IsMimeMultipartContent("form-data") Then
            Throw New HttpResponseException(HttpStatusCode.UnsupportedMediaType)
        End If

        Dim streamProvider = New AzureBlobStorageMultipartProvider(BlobHelper.GetWebApiContainer())
        Return Request.Content.ReadAsMultipartAsync(Of AzureBlobStorageMultipartProvider)(streamProvider) _
            .ContinueWith(Of List(Of FileDetails))(
                Function(f)
                    If f.IsFaulted Then
                        Throw f.Exception
                    End If

                    Dim provider As AzureBlobStorageMultipartProvider = f.Result
                    Return provider.Files
                End Function)
    End Function

    Iterator Function GetFiles() As IEnumerable(Of FileDetails)
        Dim container As CloudBlobContainer = BlobHelper.GetWebApiContainer()
        For Each blob As CloudBlockBlob In container.ListBlobs()
            Yield New FileDetails With {
                .ContentType = blob.Properties.ContentType,
                .Location = blob.Uri.AbsoluteUri,
                .Name = blob.Name,
                .Size = blob.Properties.Length}
        Next
    End Function

End Class
