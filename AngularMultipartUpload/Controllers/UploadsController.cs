using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Playground.AngularMultipartUpload.Controllers
{
    [Route("api/[controller]")]
    public class UploadsController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public UploadsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                string pathToSave = $"{_hostingEnvironment.WebRootPath}\\uploads\\{file.FileName}";

                using (var fileStream = System.IO.File.Open(pathToSave, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return Ok();
        }
    }
}
