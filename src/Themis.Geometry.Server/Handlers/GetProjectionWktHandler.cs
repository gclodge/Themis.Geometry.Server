using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services.Interfaces;

using MediatR;

namespace Themis.Geometry.Server.Handlers
{
    public class GetProjectionWktHandler : IRequestHandler<GetProjectionWktCommand, string?>
    {
        private readonly IProjectionService service;

        public GetProjectionWktHandler(IProjectionService service)
        {
            this.service = service;
        }

        public Task<string?> Handle(GetProjectionWktCommand request, CancellationToken cancellationToken)
        {
            lock (request.projectionLock)
            {
                return Task.FromResult(service.WellKnownText);
            }
        }
    }
}
