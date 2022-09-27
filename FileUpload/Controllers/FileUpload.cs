using Microsoft.AspNetCore.Mvc;

namespace FileUpload.Controllers
{
    public class FileUpload : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpPost("uploadfile")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            var size = files.Sum(f => f.Length);
            var filePaths = new List<string>();


            foreach (var formFile in files)
            {
                if(formFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), formFile.FileName);
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath , FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                }
            }
            return Ok(new {files.Count , filePaths});
        }
    }
}
