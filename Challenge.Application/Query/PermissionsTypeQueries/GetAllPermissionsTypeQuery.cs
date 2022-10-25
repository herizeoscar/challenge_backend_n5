using Challenge.Domain.Entities;
using Challenge.Infrastructure.Repositories.Abstractions;
using MediatR;

namespace Challenge.Application.QueryHandler.PermissionsTypeQueryHandler {
    public class GetAllPermissionsTypeQuery : IRequest<IEnumerable<PermissionType>> {

        public class GetAllPermissionsTypeQueryHandler : IRequestHandler<GetAllPermissionsTypeQuery, IEnumerable<PermissionType>> {
            
            private readonly IUnitOfWork unitOfWork;

            public GetAllPermissionsTypeQueryHandler(IUnitOfWork unitOfWork) {
                this.unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<PermissionType>> Handle(GetAllPermissionsTypeQuery request, CancellationToken cancellationToken) {
                return await unitOfWork.GetRepository<PermissionType>().Get(orderBy: result => result.OrderBy(x => x.Id));
            }
        }

    }
}
