using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services.Interfaces;

using MediatR;
using Serilog;

namespace Themis.Geometry.Server.Handlers
{
    public class SetProjectionWktHandler : IRequestHandler<SetProjectionWktCommand>
    {
        private readonly IProjectionService service;

        public SetProjectionWktHandler(IProjectionService service)
        {
            this.service = service;
        }

        public Task<Unit> Handle(SetProjectionWktCommand request, CancellationToken cancellationToken)
        {
            lock (request.projectionLock)
            {
                Log.Information($"ProjectionService -- Updating WKT to: {request.wellKnownText}");
                service.SetWellKnownText(request.wellKnownText);
            }

            return Unit.Task;
        }
    }
}
