using Challenge.WebApi.Controllers;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Challenge.Tests {
    public class PermissionTypeServices {

        private readonly ILogger<PermissionsTypeController> logger;
        private readonly IMediator mediator;
        private readonly PermissionsTypeController controller;

        public PermissionTypeServices() {
            this.logger = A.Fake<ILogger<PermissionsTypeController>>();
            this.mediator = A.Fake<IMediator>();
            controller = new PermissionsTypeController(logger, mediator);
        }

        [Fact]
        public void GetAll_ReturnsCorrectResults() {
            // Arrange
            var result = controller.GetAll();

            // Act  

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

    }
}