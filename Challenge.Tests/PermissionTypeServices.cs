using AutoMapper;
using Challenge.WebApi.Controllers;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Challenge.Tests {
    public class PermissionTypeServices {

        private readonly ILogger<PermissionsTypeController> logger;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly PermissionsTypeController controller;

        public PermissionTypeServices() {
            this.logger = A.Fake<ILogger<PermissionsTypeController>>();
            this.mapper = A.Fake<IMapper>();
            this.mediator = A.Fake<IMediator>();
            controller = new PermissionsTypeController(logger, mapper, mediator);
        }

        [Fact]
        public void GetAll_ReturnsCorrectResults() {
            // Arrange
            // Act
            var result = controller.GetAll();
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

    }
}