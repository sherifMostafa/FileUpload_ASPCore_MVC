using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace FileUpload.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadfileController : ControllerBase
    {
        [HttpPost , DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                if(file.Length > 0)
                {
                    var fileName = file.FileName;
                    var fullPath = Path.Combine(pathToSave, fileName);

                    var dbPath =  Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath , FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error"); 
            }
        }
    }
}
