using Microsoft.AspNetCore.Mvc;
using RUGTrial.Models.Requests;
using RUGTrial.Services;

namespace RUGTrial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RUGController(ILogger<RUGController> logger, IRUGService rugService) : ControllerBase
    {
        private readonly ILogger<RUGController> logger = logger;
        private readonly IRUGService rugService = rugService;

        [HttpGet(Name = "Get Default Stuff Single")]
        public async Task DefaultMethod(RUGRequestModel request)
        {
            logger.LogInformation($"Executing {nameof(DefaultMethod)} with single request.");
        }
    }
}
