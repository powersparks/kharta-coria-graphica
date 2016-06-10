using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Components;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Security.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Urls.Routing;
using Permission = Telligent.Evolution.Extensibility.Security.Version1.Permission;
using InteralApi = te.extension.kharta.InternalApi;


namespace te.extension.kharta.Plugins.Application
{
    public class KhartaOntologyType : IApplicationType
    {
        public Guid ApplicationTypeId { get { throw new NotImplementedException(); } }

        public string ApplicationTypeName { get { throw new NotImplementedException(); } }

        public Guid[] ContainerTypes { get { throw new NotImplementedException(); } }

        public string Description { get { throw new NotImplementedException(); } }

        public string Name { get { throw new NotImplementedException(); } }

        public void AttachChangeEvents(IApplicationStateChanges stateChanges) { throw new NotImplementedException(); }

        public IApplication Get(Guid applicationId) { throw new NotImplementedException(); }

        public void Initialize() { throw new NotImplementedException(); }
    }
}
