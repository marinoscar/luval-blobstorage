using Luval.BlobStorage.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.BlobStorage.Web.Areas.BlobStorage.Controllers
{
    [Area("Media"), Authorize]
    public class HomeController : Controller
    {
        public HomeController(ICloudBlobStorage cloudBlobStorage)
        {
            BlobStorage = cloudBlobStorage;
        }

        protected ICloudBlobStorage BlobStorage { get; private set; }

        public async Task<IActionResult> Index(string dir)
        {
            var files = await BlobStorage.GetBlobsAsync(dir, CancellationToken.None);
            return View(files.Where(i => !i.IsDirectory)
                            .OrderByDescending(i => i.UpdatedOn)
                            .Select(i => new FileModel(i)));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFile(FileModel file)
        {
            if(file != null && !string.IsNullOrWhiteSpace(file.FullName))
                await BlobStorage.DeleteAsync(file.FullName, CancellationToken.None);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UploadMedia(List<IFormFile> fileItems)
        {
            if (fileItems == null || !fileItems.Any()) return RedirectToAction("Index");
            foreach (var file in fileItems)
            {
                using (var stream = file.OpenReadStream())
                {
                    BlobStorage.UploadAsync(file.FileName, stream, CancellationToken.None).Wait();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
