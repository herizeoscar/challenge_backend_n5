using Challenge.Application.Commands.PermissionsCommands;
using FluentValidation;

namespace Challenge.WebApi.Validation {
    public class UpdatePermissionValidator : AbstractValidator<UpdatePermissionCommand> { 

        public UpdatePermissionValidator() {
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.EmployeeForename).NotNull().NotEmpty();
            RuleFor(command => command.EmployeeSurname).NotNull().NotEmpty();
            RuleFor(command => command.PermissionTypeId).GreaterThan(0);
            RuleFor(command => command.PermissionDate).NotNull();
        }

    }
}
