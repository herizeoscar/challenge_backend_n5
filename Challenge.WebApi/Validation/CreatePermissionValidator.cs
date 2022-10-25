using Challenge.Application.Commands.PermissionsCommands;
using FluentValidation;

namespace Challenge.WebApi.Validation {
    public class CreatePermissionValidator : AbstractValidator<CreatePermissionCommand> {
        public CreatePermissionValidator() {
            RuleFor(command => command.EmployeeForename).NotNull().NotEmpty();
            RuleFor(command => command.EmployeeSurname).NotNull().NotEmpty();
            RuleFor(command => command.PermissionTypeId).GreaterThan(0);
            RuleFor(command => command.PermissionDate).NotNull();
        }
    }
}
