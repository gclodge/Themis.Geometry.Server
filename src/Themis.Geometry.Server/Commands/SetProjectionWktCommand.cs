using System.Diagnostics.CodeAnalysis;

using MediatR;

namespace Themis.Geometry.Server.Commands
{
    [ExcludeFromCodeCoverage]
    public class SetProjectionWktCommand : IRequest
    {
        /// <summary>
        /// Mutex to be used while updating the underlying projection information
        /// </summary>
        public object projectionLock;

        /// <summary>
        /// The well-known-text (WKT) representation to be assigned to the Server's IProjectionService
        /// </summary>
        public string wellKnownText;

        public SetProjectionWktCommand(string wellKnownText, ref object projectionLock)
        {
            this.wellKnownText = wellKnownText;
            this.projectionLock = projectionLock;
        }
    }
}
