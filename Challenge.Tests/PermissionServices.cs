using AutoMapper;
using Challenge.Application.Commands.PermissionsCommands;
using Challenge.Application.Services.Abstractions;
using Challenge.WebApi.Controllers;
using Confluent.Kafka;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;

namespace Challenge.Tests {
    public class PermissionServices {

        private readonly ILogger<PermissionsController> logger;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IElasticClient elasticClient;
        private readonly IKafkaService kafkaService;
        private readonly PermissionsController controller;

        public PermissionServices() {
            this.logger = A.Fake<ILogger<PermissionsController>>();
            this.mapper = A.Fake<IMapper>();
            this.mediator = A.Fake<IMediator>();
            this.elasticClient = A.Fake<IElasticClient>();
            this.kafkaService = A.Fake<IKafkaService>();
            controller = new PermissionsController(logger, mapper, mediator, elasticClient, kafkaService);
        }

        [Fact]
        public void GetAll_ReturnsOk() {
            // Arrange
            // Act
            var result = controller.GetAll();
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void RequestPermission_ValidCommand_ReturnsOk() {
            // Arrange
            var command = new CreatePermissionCommand {
                EmployeeForename = "EmployeeForename",
                EmployeeSurname = "EmployeeSurname",
                PermissionTypeId = 1,
                PermissionDate = DateTime.Now
            };
            // Act
            var result = controller.RequestPermission(command);
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void ModifyPermission_ValidCommand_ReturnsOk() {
            // Arrange
            var command = new UpdatePermissionCommand {
                Id = 1,
                EmployeeForename = "EmployeeForename",
                EmployeeSurname = "EmployeeSurname",
                PermissionTypeId = 1,
                PermissionDate = DateTime.Now
            };
            // Act
            var result = controller.ModifyPermission(command);
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

    }
}
