using AutoMapper;
using Challenge.Application.DTOs;
using Challenge.Application.QueryHandler.PermissionsTypeQueryHandler;
using Challenge.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.WebApi.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsTypeController : Controller {
        
        private readonly ILogger<PermissionsTypeController> logger;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public PermissionsTypeController(ILogger<PermissionsTypeController> logger, IMapper mapper, IMediator mediator) {
            this.logger = logger;
            this.mapper = mapper;
            this.mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                logger.LogInformation("Get All Permission Types");
                var result = await mediator.Send(new GetAllPermissionsTypeQuery());
                return Ok(mapper.Map<IEnumerable<PermissionType>, IEnumerable<PermissionTypeDto>>(result));
            } catch(Exception e) {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
