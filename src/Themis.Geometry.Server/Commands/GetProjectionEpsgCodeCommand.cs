using System.Diagnostics.CodeAnalysis;

using MediatR;

namespace Themis.Geometry.Server.Commands
{
    [ExcludeFromCodeCoverage]
    public class GetProjectionEpsgCodeCommand : IRequest<int?>
    {
        /// <summary>
        /// Mutex to be used while updating the underlying projection information
        /// </summary>
        public object projectionLock;

        public GetProjectionEpsgCodeCommand(ref object projectionLock)
        {
            this.projectionLock = projectionLock;
        }
    }
}
