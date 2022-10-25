using Challenge.Domain.Entities;
using Challenge.Infrastructure.Repositories.Abstractions;
using MediatR;

namespace Challenge.Application.Query.PermissionsQueries {
    public class GetAllPermissionsQuery : IRequest<IEnumerable<Permission>> {
        public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<Permission>> {

            private readonly IUnitOfWork unitOfWork;

            public GetAllPermissionsQueryHandler(IUnitOfWork unitOfWork) {
                this.unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Permission>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken) {
                return await unitOfWork.GetRepository<Permission>().Get(orderBy: result => result.OrderBy(x => x.Id));
            }
        }
    }
}
