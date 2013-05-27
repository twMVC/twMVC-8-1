Imports System.Collections.Generic
Imports System.IO
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Microsoft.WindowsAzure.StorageClient

Public Class AzureBlobStorageMultipartProvider
    Inherits MultipartFileStreamProvider

    Private _container As CloudBlobContainer
    Public Property Files As List(Of FileDetails)
    Sub New(container As CloudBlobContainer)
        MyBase.New(Path.GetTempPath())
        _container = container
        Files = New List(Of FileDetails)()
    End Sub

    Public Overrides Function ExecutePostProcessingAsync() As Task
        For Each Data As MultipartFileData In FileData
            Dim fileName As String = Path.GetFileName(Data.Headers.ContentDisposition.FileName.Trim(""""))
            Dim blob As CloudBlob = _container.GetBlobReference(fileName)
            blob.Properties.ContentType = Data.Headers.ContentType.MediaType
            blob.UploadFile(Data.LocalFileName)
            File.Delete(Data.LocalFileName)
            Files.Add(New FileDetails With {
                      .ContentType = blob.Properties.ContentType,
                      .Location = blob.Uri.AbsoluteUri,
                      .Name = blob.Name,
                      .Size = blob.Properties.Length})
        Next

        Return MyBase.ExecutePostProcessingAsync()
    End Function
End Class
