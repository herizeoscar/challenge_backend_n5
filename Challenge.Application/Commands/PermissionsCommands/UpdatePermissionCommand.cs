using Challenge.Domain.Entities;
using Challenge.Infrastructure.Repositories.Abstractions;
using MediatR;

namespace Challenge.Application.Commands.PermissionsCommands {
    public class UpdatePermissionCommand : IRequest<Permission> {
        public int Id { get; set; }
        public string EmployeeForename { get; set; } = string.Empty;
        public string EmployeeSurname { get; set; } = string.Empty;
        public int PermissionTypeId { get; set; } = 0;
        public DateTime PermissionDate { get; set; }

        public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Permission> {
            
            private readonly IUnitOfWork unitOfWork;

            public UpdatePermissionCommandHandler(IUnitOfWork unitOfWork) {
                this.unitOfWork = unitOfWork;
            }

            public async Task<Permission> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken) {
                var result = await unitOfWork.GetRepository<Permission>().Update(new Permission {
                    Id = request.Id,
                    EmployeeForename = request.EmployeeForename,
                    EmployeeSurname = request.EmployeeSurname,
                    PermissionTypeId = request.PermissionTypeId,
                    PermissionDate = request.PermissionDate
                });
                await unitOfWork.SaveChangesAsync();
                return result.Entity;
            }
        }
    }
}