using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers;

[Route("api/files")]
[ApiController]
public class FilesController : ControllerBase
{

    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

    public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
            ?? throw new ArgumentNullException(nameof(fileExtensionContentTypeProvider));
    }

    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId)
    {        

        //Look up the actual file depending on the fileId
        var pathToFile = "Skipper.pdf";

        //check if the file exists
        if(System.IO.File.Exists(pathToFile) is false)
        {
            return NotFound();
        }

        //Get the file type
        if(_fileExtensionContentTypeProvider.TryGetContentType(pathToFile,out var contentType) is false)
        {
            contentType = "application/octet-stream";//the default media type for arbitrary binary data
        }
        
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        
    }
}
