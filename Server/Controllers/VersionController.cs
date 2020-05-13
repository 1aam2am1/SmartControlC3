using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Data;
using Api.Queries;
using Api.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;

        public VersionController(ILogger<VersionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var query = new VersionQuery { ApiVersion = new MyVersion { Major = 1 }, AppVersion = new MyVersion() };
            return Post(query);
        }

        [HttpPost]
        public ActionResult Post([FromBody]VersionQuery query)
        {
            _logger.LogInformation("Version Api {0}, Version App {1}", query.ApiVersion.ToString(), query.AppVersion.ToString());

            var result = new VersionResponse
            {
                ApiVersion = new Version(1, 0, 0, 0),
                ServerVersion = Assembly.GetExecutingAssembly().GetName().Version,
                DeviceVersion = new Version(0, 0, 0, 2) ///TODO: Get Version from device
            };

            return Ok(result);
        }
    }
}
