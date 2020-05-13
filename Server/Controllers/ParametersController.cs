using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Queries;
using Api.Responses;
using Api.SaveQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        private readonly ILogger<ParametersController> _logger;

        public ParametersController(ILogger<ParametersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Post(new ParameterQuery { Parameter = Enumerable.Range(0, 20).ToList() });
        }

        [HttpPost]
        public ActionResult Post([FromBody]ParameterQuery query)
        {
            _logger.LogInformation("Get Parameters => Random response");

            var result = new ParameterResponse();
            var engine = new Random();
            foreach (var v in query.Parameter)
            {
                result.Parameters[v] = engine.Next(0, 100);
            }

            return Ok(result);
        }

        [HttpPost("save")]
        public ActionResult Save([FromBody]ParameterSave query)
        {
            _logger.LogInformation("Saving Parameters {0}", string.Join(";", query.Parameters.Select(x => x.Key + "=" + x.Value)));
            var result = new OkErrorResponce { Result = OkStatus.Ok };

            return Ok(result);
        }
    }
}
