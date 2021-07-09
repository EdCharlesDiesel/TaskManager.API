using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    public class RouterLoggerController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public RouterLoggerController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("api/routerlogger")]
        public IActionResult Index()
        {
            string logMessage = null;
            using (StreamReader streamReader = new StreamReader(Request.Body, Encoding.ASCII))
            {
                logMessage = streamReader.ReadToEnd() + "\n";
            }
            string filePath = this._hostingEnvironment.ContentRootPath + "\\RouterLogger.txt";
            System.IO.File.AppendAllText(filePath, logMessage);
            return Ok();
        }
    }
}

