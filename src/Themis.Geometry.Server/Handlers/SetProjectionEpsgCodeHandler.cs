using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services.Interfaces;

using MediatR;
using Serilog;

namespace Themis.Geometry.Server.Handlers
{
    public class SetProjectionEpsgCodeHandler : IRequestHandler<SetProjectionEpsgCodeCommand>
    {
        private readonly IProjectionService service;

        public SetProjectionEpsgCodeHandler(IProjectionService service)
        {
            this.service = service;
        }

        public Task<Unit> Handle(SetProjectionEpsgCodeCommand request, CancellationToken cancellationToken)
        {
            lock (request.projectionLock)
            {
                Log.Information($"ProjectionService -- Updating EPSG code to: {request.epsgCode}");
                service.SetEpsgCode(request.epsgCode);
            }

            return Unit.Task;
        }
    }
}
