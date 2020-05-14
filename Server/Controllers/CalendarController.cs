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
    public class CalendarController : ControllerBase
    {
        private readonly ILogger<CalendarController> _logger;

        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogInformation("Get Calendar => Random response");

            var message = new CalendarResponse
            {
                Enabled = true,
                CalState = 0b1011,
                ActiveDays = 0b1001101,
                Date = DateTime.UtcNow,
            };

            return Ok(message);
        }

        [HttpPost("save")]
        public ActionResult Save([FromBody]CalendarSave query)
        {
            _logger.LogInformation("Saving Calendar {0} {1} {2}", query.ActiveDays, query.Enabled, query.Date.ToString());
            var result = new OkErrorResponce { Result = OkStatus.Ok };

            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post([FromBody]CalendarDayQuery query)
        {
            _logger.LogInformation("Get Calendar Day => Random response");

            var message = new CalendarDayResponse
            {
                Day = query.Day,
                Tasks = new List<CalendarTask>
                {
                    new CalendarTask
                    {
                        Enabled = true,
                        Hour = 1,
                        Minute = 52,
                        Duration = 320,
                        ExhaustPower = 20,
                        AirflowPower = 60,
                        Heater = false,
                        AirTemperature = 56,
                        Boost = false,
                    },
                    new CalendarTask
                    {
                        Enabled = false,
                        Hour = 4,
                        Minute = 00,
                        Duration = new Random().Next(0, 600),
                        ExhaustPower = 45,
                        AirflowPower = 50,
                        Heater = true,
                        AirTemperature = 56,
                        Boost = new Random().NextDouble() > 0.5,
                    }
                }
            };

            return Ok(message);
        }

        [HttpPost("day/save")]
        public ActionResult DaySave([FromBody]CalendarDaySave query)
        {
            _logger.LogInformation("Saving Calendar Day {0} {1}", query.Day,
                string.Join(";", query.Tasks.Select(x => x.AirflowPower.ToString() + " " + x.AirTemperature.ToString()
                + " " + x.Boost.ToString()
                + " " + x.Duration.ToString()
                + " " + x.Enabled.ToString()
                + " " + x.ExhaustPower.ToString() + " ...")));

            var result = new OkErrorResponce { Result = OkStatus.Ok };

            return Ok(result);
        }
    }
}
