using AutoMapper;
using Challenge.Application.Commands.PermissionsCommands;
using Challenge.Application.DTOs;
using Challenge.Application.Query.PermissionsQueries;
using Challenge.Application.Services.Abstractions;
using Challenge.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nest;
using static Challenge.Application.Enums.ApplicationEnums;

namespace Challenge.WebApi.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : Controller {

        private readonly ILogger<PermissionsController> logger;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IElasticClient elasticClient;
        private readonly IKafkaService kafkaService;

        public PermissionsController(ILogger<PermissionsController> logger, IMapper mapper, IMediator mediator, IElasticClient elasticClient, IKafkaService kafkaService) {
            this.logger = logger;
            this.mapper = mapper;
            this.mediator = mediator;
            this.elasticClient = elasticClient;
            this.kafkaService = kafkaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                logger.LogInformation("Get All Permissions");
                var result = await mediator.Send(new GetAllPermissionsQuery());
                if(result.Count() > 0) {
                    await elasticClient.IndexManyAsync(result);
                }
                await kafkaService.RegisterOperation("test-topic", Operation.Get);
                return Ok(mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionDto>>(result));
            } catch(Exception e) {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RequestPermission(CreatePermissionCommand command) {
            try {
                logger.LogInformation("Request Permissions");
                var result = await mediator.Send(command);
                await elasticClient.IndexDocumentAsync(result);
                await kafkaService.RegisterOperation("test-topic", Operation.Request);
                return Ok(mapper.Map<Permission, PermissionDto>(result));
            } catch(Exception e) {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ModifyPermission(UpdatePermissionCommand command) {
            try {
                logger.LogInformation("Modify Permissions");
                var result = await mediator.Send(command);
                await elasticClient.IndexDocumentAsync(result);
                await kafkaService.RegisterOperation("test-topic", Operation.Modify);
                return Ok(mapper.Map<Permission, PermissionDto>(result));
            } catch(Exception e) {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

    }
}
