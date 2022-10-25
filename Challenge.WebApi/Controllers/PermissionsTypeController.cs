using Challenge.Application.QueryHandler.PermissionsTypeQueryHandler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.WebApi.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsTypeController : Controller {
        
        private readonly ILogger<PermissionsTypeController> logger;
        private readonly IMediator mediator;

        public PermissionsTypeController(ILogger<PermissionsTypeController> logger, IMediator mediator) {
            this.logger = logger;
            this.mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                logger.LogInformation("Get All Permission Types");
                var result = await mediator.Send(new GetAllPermissionsTypeQuery());
                return Ok(result);
            } catch(Exception e) {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
