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

namespace te.extension.kharta.Plugins
{
    public class KhartaContainerType : IPlugin, IContainerType
    {
        IContainerStateChanges _containerState = null;
       
           KhartaContainer _khContainer = new KhartaContainer();

        public readonly Guid ContainerType_id = new Guid("8c25d5e3-18b6-4af7-9f37-e1a297c01605");
        InternalApi.OntologyDataService _internalKHC = new InternalApi.OntologyDataService();

        public Guid ContainerTypeId
        {
            get
            { 
                return ContainerType_id;
            }
        }

        public string ContainerTypeName
        {
            get
            {
                return "KhartaContainerType";
            }
        }

        public string Description
        {
            get
            {
                return "Kharta Container Type";
            }
        }

        public string Name
        {
            get
            {
                return "KhartaContainerType";
            }
        }

        public void AttachChangeEvents(IContainerStateChanges stateChanges)
        {
            _containerState = stateChanges;
        }

        public IContainer Get(Guid containerId)
        {
              IContainer iContainer = (IContainer)InteralApi.OntologyDataService.getContainerByGuid(containerId) ;
            return iContainer;
        }

        public void Initialize()
        {
            //throw new NotImplementedException();
        }

       
    }
}
