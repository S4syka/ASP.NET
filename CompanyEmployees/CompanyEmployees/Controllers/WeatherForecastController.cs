using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILoggerManager _logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Here is the info from our values controller");
            _logger.LogDebug("Here is the debug from our values controller");
            _logger.LogWarning("Here is the warning from our values controller");
            _logger.LogError("Here is the error from our values controller");

            return new string[] { "Sagol", "Zma" };
        }
    }
}