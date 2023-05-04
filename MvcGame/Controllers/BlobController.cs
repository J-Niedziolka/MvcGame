using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.IO;

namespace Projekt.Controllers
{
    public class BlobController : Controller
    {
        private CloudBlobContainer container;

        public BlobController()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=storageprojektjanek;AccountKey=SU5kgJh7UCY1oDNrC8+fybbmlbpB8eoqmPkDgHFrEo8V911QPkpCrPYMqS3/kmk/wlNVEFHVj8pe+AStFmTrkQ==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference("janekprojektblob");
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                int length = file.FileName.Split(".").Length;
                string fileExt = file.FileName.Split(".")[length - 1];
                Guid myuuid = Guid.NewGuid();
                string uuid = Guid.NewGuid().ToString();
                string newFile = uuid + "." + fileExt;
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(newFile);
                using (var stream = file.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(stream);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Index()
        {
            var imageNames = new List<string>();

            BlobContinuationToken continuationToken = null;

            do
            {
                BlobResultSegment segment = await container.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = segment.ContinuationToken;
                foreach (IListBlobItem item in segment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        imageNames.Add(blob.Name);
                        Console.WriteLine(blob.Name);
                    }
                }
            }
            while (continuationToken != null);

            return View(imageNames);
        }

        public async Task<ActionResult> Display(string name)
        {
            // Retrieve the image blob
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            if (await blockBlob.ExistsAsync())
            {
                var memoryStream = new MemoryStream();
                await blockBlob.DownloadToStreamAsync(memoryStream);
                memoryStream.Position = 0;
                return File(memoryStream.ToArray(), "image/jpeg");

            }
            else
            {
                // Handle the case when the image is not found
                return RedirectToAction("NotFound");
            }
        }

        public async Task<ActionResult> Delete(string name)
        {
            // Retrieve the image blob
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);

            if (await blockBlob.ExistsAsync())
            {
                // Delete the image
                await blockBlob.DeleteAsync();
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the case when the image is not found
                return RedirectToAction("NotFound");
            }
        }

        public ActionResult NotFound()
        {
            return View();
        }



    }
}