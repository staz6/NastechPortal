using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BiometricManagement.Dto;
using BiometricManagement.Interface;
using FileHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetMQ;
using NetMQ.Sockets;

namespace BiometricManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BiometricController
    {
        private readonly ILogger<BiometricController> _logger;
        private readonly IGenericRepository _repo;
        public BiometricController(ILogger<BiometricController> logger, IGenericRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> getAttendance()
        {

            _repo.GenerateAttendanceRecordAsync();

            return "result";

        }
    }
}