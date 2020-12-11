using IITUforum.Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;

namespace IITUforum.Service
{
    public class UploadService : IUpload
    {
        public UploadService()
        {
        }

        public CloudBlobContainer GetBlobContainer(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference("profile-images");
        }
    }
}
