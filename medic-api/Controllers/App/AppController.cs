using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using medic_api.Controllers.DTO;
using Microsoft.AspNetCore.Authorization;

namespace medic_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Patient")]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<AppGetResponse> Get()
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            List<string> ipList = new List<string>();
            foreach (var ipAddress in localIPs)
            {
                ipList.Add(ipAddress.ToString());
            }

            AppGetResponse response = new AppGetResponse()
            {
                Host = Environment.MachineName,
                Ips = ipList,
                Path = Request.Path,
            };
            return Ok(response);
        }
    }
}