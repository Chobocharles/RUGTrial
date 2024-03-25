using Microsoft.AspNetCore.Mvc;
using RUGTrial.Models.Requests;
using RUGTrial.Models.Responses;
using RUGTrial.Services;

namespace RUGTrial.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class RUGController(ILogger<RUGController> logger, IRUGService rugService) : ControllerBase
    {
        private readonly ILogger<RUGController> logger = logger;
        private readonly IRUGService rugService = rugService;

        [HttpPost(Name = "Get Default Stuff Single")]
        public RUGResponseModel DefaultMethod(RUGRequestModel request)
        {
            logger.LogInformation($"Executing {nameof(DefaultMethod)} with single request.");
            return rugService.CalculatePercentages(request);
        }
    }
}
