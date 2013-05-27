Imports Microsoft.WindowsAzure
Imports Microsoft.WindowsAzure.StorageClient

Public Class BlobHelper
    Public Shared Function GetWebApiContainer() As CloudBlobContainer
        Dim storageAccount As CloudStorageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("CloudStorageConnectionString"))

        Dim blobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()

        Dim container As CloudBlobContainer = blobClient.GetContainerReference("webapicontainer")

        container.CreateIfNotExist()

        Dim permissions As BlobContainerPermissions = container.GetPermissions()
        If permissions.PublicAccess = BlobContainerPublicAccessType.Off Then
            permissions.PublicAccess = BlobContainerPublicAccessType.Blob
            container.SetPermissions(permissions)
        End If

        Return container
    End Function

End Class
