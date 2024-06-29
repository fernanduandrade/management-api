using Manager.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;


namespace Manager.API;

public class LogsController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : BaseController
{
    private readonly string _logPath = Path.GetFullPath(Path.Combine(webHostEnvironment.ContentRootPath, configuration["LogPath"]));

    [HttpGet("{date}")]
    public  IActionResult GetByDate(string date)
    {
        string filepath = Path.GetFullPath(Path.Combine(_logPath, $"log-{date}.txt"));
        if (System.IO.File.Exists(filepath))
        {
            var filename = Path.GetFileName(filepath);
            var provider = new PhysicalFileProvider(Path.GetFullPath(_logPath));
            var fileInfo = provider.GetFileInfo(filename);
            var readStream = fileInfo.CreateReadStream();

            return File(readStream, "application/octet-stream", filename);
        }

        return NotFound();
    }
}
