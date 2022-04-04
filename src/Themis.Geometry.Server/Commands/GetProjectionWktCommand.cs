using System.Diagnostics.CodeAnalysis;

using MediatR;

namespace Themis.Geometry.Server.Commands
{
    [ExcludeFromCodeCoverage]
    public class GetProjectionWktCommand : IRequest<string?>
    {
        /// <summary>
        /// Mutex to be used while updating the underlying projection information
        /// </summary>
        public object projectionLock;

        public GetProjectionWktCommand(ref object projectionLock)
        {
            this.projectionLock = projectionLock;
        }
    }
}
