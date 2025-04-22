using Microsoft.AspNetCore.Mvc;
using SerhendKumbara.Core.HTTP;

namespace SerhendKumbara.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _token = "a8MGfmkw0ZdbTKIVjIdFloRbq5XZM6";

    public UploadController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    
    [HttpPost]
    public async Task<DataResult<string>> UploadFile([FromForm] UploadFileModel model)
    {
        var token = Request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(token) || token != _token)
        {
            throw new UnauthorizedAccessException("Token is not valid");
        }

        if (model.File.Length > 0)
        {
            var uploadFolder = Path.Combine(_environment.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var filePath = Path.Combine(_environment.ContentRootPath, "Uploads", model.File.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }
            return new SuccessDataResult<string>(filePath);
        }

        throw new Exception();
    }

    public class UploadFileModel
    {
        public IFormFile File { get; set; }
    }
}
