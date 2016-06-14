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
using Telligent.Evolution.Extensibility;

namespace te.extension.kharta.Plugins.Application
{
    public class KhartaOntologyType : IApplicationType
    {
        IApplicationStateChanges _applicationState = null;
        public static Guid _applicationTypeId = new Guid("924b7cd5-a0f9-4a41-894f-3f033c7a786a");
        public static Guid _applicationId = new Guid("89f402df-4aa4-4eb8-96d9-7f6236d0c646");
        public Guid ApplicationTypeId { get { return _applicationTypeId; } }

        public string ApplicationTypeName { get { return "Kharta Ontology Application"; } }

        public Guid[] ContainerTypes { get { return new Guid[] { Apis.Get<IGroups>().ContentTypeId } ; } }

        public string Description { get { return "Disciplines to categorize sources"; } }

        public string Name { get { return "Kharta Ontology"; } }

        public void AttachChangeEvents(IApplicationStateChanges stateChanges) { _applicationState = stateChanges; }

        public IApplication Get(Guid applicationId) {

            return PublicApi.Ontologies.GetOntologyApplication(applicationId); }

        public void Initialize() {   }
    }
}
