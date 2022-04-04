using System.Diagnostics.CodeAnalysis;

using MediatR;

namespace Themis.Geometry.Server.Commands
{
    [ExcludeFromCodeCoverage]
    public class SetProjectionEpsgCodeCommand : IRequest
    {
        /// <summary>
        /// Mutex to be used while updating the underlying projection information
        /// </summary>
        public object projectionLock;

        /// <summary>
        /// The integer EPSG code to be assigned to the IProjectionService
        /// </summary>
        public int epsgCode;

        public SetProjectionEpsgCodeCommand(int epsgCode, ref object projectionLock)
        {
            this.epsgCode = epsgCode;
            this.projectionLock = projectionLock;
        }
    }
}
