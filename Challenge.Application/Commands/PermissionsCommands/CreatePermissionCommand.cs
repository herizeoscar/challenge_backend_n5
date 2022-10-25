using Challenge.Domain.Entities;
using Challenge.Infrastructure.Repositories.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Commands.PermissionsCommands {
    public class CreatePermissionCommand : IRequest<Permission> {
        public string EmployeeForename { get; set; } = string.Empty;
        public string EmployeeSurname { get; set; } = string.Empty;
        public int PermissionTypeId { get; set; } = 0;
        public DateTime PermissionDate { get; set; }

        public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Permission> {
            
            private readonly IUnitOfWork unitOfWork;

            public CreatePermissionCommandHandler(IUnitOfWork unitOfWork) {
                this.unitOfWork = unitOfWork;
            }

            public async Task<Permission> Handle(CreatePermissionCommand request, CancellationToken cancellationToken) {
                var result = await unitOfWork.GetRepository<Permission>().Insert(new Permission {
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
