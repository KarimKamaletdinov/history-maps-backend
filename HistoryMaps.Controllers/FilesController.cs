using Microsoft.AspNetCore.Mvc;

namespace HistoryMaps;

[Route("[controller]")]
[Controller]
public class FilesController
{
    [HttpPost]
    public void Post([FromForm] List<IFormFile> files)
    {
        
    }
}