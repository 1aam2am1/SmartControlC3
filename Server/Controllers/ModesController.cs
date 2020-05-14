using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Responses;
using Api.SaveQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ModesController : ControllerBase
    {
        private readonly ILogger<ModesController> _logger;

        public ModesController(ILogger<ModesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogInformation("Get Modes => Random response");

            var message = new ModesResponse
            {
                Modes = new Dictionary<int, ModesStatus>
                {
                    {0, new ModesStatus{ Active = true, Value = 50} },
                    {1, new ModesStatus{ Active = false, Value = 60} },
                    {2, new ModesStatus{ Active = false, Value = 0} },
                    {3, new ModesStatus{ Active = true, Value = 0} },
                    {4, new ModesStatus{ Active = false, Value = 20} }
                }
            };

            return Ok(message);
        }

        [HttpPost("save")]
        public ActionResult Save([FromBody]ModesSave query)
        {
            _logger.LogInformation("Saving Modes {0} {1} {2}", string.Join(";", query.Modes.Select(x => x.Key + "="
            + (x.Value.Active ? "1:" : "0:") + x.Value.Value)));
            var result = new OkErrorResponce { Result = OkStatus.Ok };

            return Ok(result);
        }
    }
}
