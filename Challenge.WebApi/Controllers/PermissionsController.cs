using Challenge.Application.Commands.PermissionsCommands;
using Challenge.Application.DTOs;
using Challenge.Application.ExtensionsMethods;
using Challenge.Application.Query.PermissionsQueries;
using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;
using static Challenge.Application.Enums.ApplicationEnums;

namespace Challenge.WebApi.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : Controller {

        private readonly ILogger<PermissionsController> logger;
        private readonly IMediator mediator;
        private readonly IElasticClient elasticClient;
        private readonly ProducerConfig config;

        public PermissionsController(ILogger<PermissionsController> logger, IMediator mediator, IElasticClient elasticClient, ProducerConfig config) {
            this.logger = logger;
            this.mediator = mediator;
            this.elasticClient = elasticClient;
            this.config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                logger.LogInformation("Get All Permissions");
                var result = await mediator.Send(new GetAllPermissionsQuery());
                if(result.Count() > 0) {
                    await elasticClient.IndexManyAsync(result);
                }
                using(var producer = new ProducerBuilder<Null, string>(config).Build()) {
                    await producer.ProduceAsync("test-topic", new Message<Null, string> {
                        Value = JsonConvert.SerializeObject(new KafkaMessageDto() {
                            Id = Guid.NewGuid(),
                            NameOperation = Operation.Get.ToDescription()
                        })
                    });
                }
                return Ok(result);
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
                using(var producer = new ProducerBuilder<Null, string>(config).Build()) {
                    await producer.ProduceAsync("test-topic", new Message<Null, string> {
                        Value = JsonConvert.SerializeObject(new KafkaMessageDto() {
                            Id = Guid.NewGuid(),
                            NameOperation = Operation.Request.ToDescription()
                        })
                    });
                }
                return Ok(result);
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
                using(var producer = new ProducerBuilder<Null, string>(config).Build()) {
                    await producer.ProduceAsync("test-topic", new Message<Null, string> {
                        Value = JsonConvert.SerializeObject(new KafkaMessageDto() {
                            Id = Guid.NewGuid(),
                            NameOperation = Operation.Modify.ToDescription()
                        })
                    });
                }
                return Ok(result);
            } catch(Exception e) {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
