using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[Route("api/files")]
[ApiController]
public class FilesController : ControllerBase
{
    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId)
    {
        //Look up the actual file depending on the fileId
        var pathToFile = "skipper.pdf";

        //check if the file exists
        if(System.IO.File.Exists(pathToFile) is false)
        {
            return NotFound();
        }
        else
        {
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, "text/plain", Path.GetFileName(pathToFile));
        }
    }
}
