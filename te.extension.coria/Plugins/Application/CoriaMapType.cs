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
using InteralApi = te.extension.coria.InternalApi;
using Telligent.Evolution.Extensibility;

namespace te.extension.coria.Plugins.Application
{
    public class CoriaMapType : IPlugin, IApplicationType, IPluginGroup
    {
        IApplicationStateChanges _applicationState = null;
        public static Guid _applicationTypeId = new Guid("bfdb6103-e8e5-4cbf-8fbf-42dbac4046ef");
        public static Guid _applicationId = new Guid("7b3cd226-ef49-4aca-94eb-72f1e49f3688");
        public Guid ApplicationTypeId { get { return _applicationTypeId; } }

        public string ApplicationTypeName { get { return "Coria Maps"; } }

        public Guid[] ContainerTypes { get { return new Guid[] { Apis.Get<IGroups>().ContentTypeId }; } }

        public string Description { get { return "Maps and metadata management tools"; } }

        public string Name { get { return "Coria Maps Application"; } }

        public IEnumerable<Type> Plugins
        {
            get
            {
                return new Type[]{
                  
                    
                };
            }
        }

        public void AttachChangeEvents(IApplicationStateChanges stateChanges) { _applicationState = stateChanges; }

        public IApplication Get(Guid applicationId)
        {
            return null;
        }//return PublicApi.Maps.GetMapApplication(applicationId); }

        public void Initialize() {   }
    }
}
