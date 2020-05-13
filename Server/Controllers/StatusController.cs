using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Data;
using Api.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogInformation("Get Status");
            var query = new StatusResponse
            {
                Status = WorkStatus.Work,
                Work = ModStatus.GWC,
                Heater = false,
                ByPass = false,
                Errors = 0,
                BateryLow = false,
            };
            return Ok(query);
        }

        [HttpGet("ping")]
        public async Task GetSse()
        {
            var response = Response;
            response.Headers.Add("Content-Type", "text/event-stream");
            response.Headers.Add("Cache-Control", "no-cache");
            response.Headers.Add("Connection", "keep-alive");

            _logger.LogInformation("Sse started");

            try
            {
                while (HttpContext.RequestAborted.IsCancellationRequested == false)
                {
                    var message = new StatusPingLowResponse { NoChange = 1 };

                    await response.WriteAsync(string.Format("data:{0}\n\n", JsonSerializer.Serialize(message)),
                        HttpContext.RequestAborted);
                    await response.Body.FlushAsync();

                    await Task.Delay(1000);
                }
            }
            finally
            {
                _logger.LogInformation("Sse stoped");
            }
        }
    }
}
