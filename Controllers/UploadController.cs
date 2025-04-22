using Microsoft.AspNetCore.Mvc;
using SerhendKumbara.Core.HTTP;

namespace SerhendKumbara.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    public UploadController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost]
    public async Task<DataResult<string>> UploadFile(IFormFile file)
    {
        if (file.Length > 0)
        {
            var uploadFolder = Path.Combine(_environment.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var filePath = Path.Combine(_environment.ContentRootPath, "Uploads", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return new SuccessDataResult<string>(filePath);
        }

        throw new Exception();
    }
}
