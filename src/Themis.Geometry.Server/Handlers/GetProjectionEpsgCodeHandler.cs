using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services.Interfaces;

using MediatR;

namespace Themis.Geometry.Server.Handlers
{
    public class GetProjectionEpsgCodeHandler : IRequestHandler<GetProjectionEpsgCodeCommand, int?>
    {
        private readonly IProjectionService service;

        public GetProjectionEpsgCodeHandler(IProjectionService service)
        {
            this.service = service;
        }

        public Task<int?> Handle(GetProjectionEpsgCodeCommand request, CancellationToken cancellationToken)
        {
            lock (request.projectionLock)
            {
                return Task.FromResult(service.EpsgCode);
            }
        }
    }
}
