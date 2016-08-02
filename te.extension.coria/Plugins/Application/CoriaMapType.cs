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
    public class CoriaMapType : IPlugin, IApplicationType, IPluginGroup, IManageableApplicationType, IQueryableApplicationType
    {
        IApplicationStateChanges _applicationState = null;
        public static Guid _applicationTypeId = new Guid("bfdb6103-e8e5-4cbf-8fbf-42dbac4046ef");
        public static Guid _applicationId = new Guid("7b3cd226-ef49-4aca-94eb-72f1e49f3688");
        public Guid ApplicationTypeId { get { return _applicationTypeId; } }

        public string ApplicationTypeName { get { return "Map Book"; } }

        public Guid[] ContainerTypes { get { return new Guid[] { Apis.Get<IGroups>().ContainerTypeId }; } }

        public string Description { get { return "Maps and metadata management tools"; } }

        public string Name { get { return "Coria Maps Application"; } }

        public IEnumerable<Type> Plugins
        {
            get
            {
                return new Type[]{
                    typeof(UI.WidgetExtension.MapWidgetExtension),
                    typeof(Content.MapContentType)



                };
            }
        }

        public void AttachChangeEvents(IApplicationStateChanges stateChanges) { _applicationState = stateChanges; }

        public IApplication Get(Guid applicationId)
        {
            return null;
        }//return PublicApi.Maps.GetMapApplication(applicationId); }

        public void Initialize() {   }

        #region  IManageableApplicationType

        public bool CanCreate(int userId, Guid containerTypeId, Guid containerId)
        {
            return true;
        }

        public bool CanDelete(int userId, Guid applicationId)
        {
            throw new NotImplementedException();
        }

        public bool CanSetEnabled(int userId, Guid applicationId)
        {
            return true;
        }

        public PropertyGroup[] GetCreateConfiguration(int userId, Guid containerTypeId, Guid containerId)
        {
            PropertyGroup sourceType = new PropertyGroup("ArcGisServer", "ArcGis Server", 3);
            sourceType.Properties.Add(new Property("mapServer", "Map Server", PropertyType.String, 1, "Dynamic") { DescriptionText = "Dynamic cached map" });

            return new PropertyGroup[] { sourceType };
        }

        public IApplication Create(int userId, Guid containerTypeId, Guid containerId, ConfigurationDataBase createConfigurationData)
        {
            throw new NotImplementedException();
        }

        public void SetEnabled(int userId, Guid applicationId, bool enabled)
        {
            throw new NotImplementedException();
        }

        public void Delete(int userId, Guid applicationId)
        {
            throw new NotImplementedException();
        }

        public bool CanEdit(int userID, Guid applicationId)
        {
            return true;
        }

        public IList<IApplication> List(int userId, Guid containerTypeId, Guid containerId)
        {
            IList<IApplication> maps = new List<IApplication>();
            return maps;
        }

        public IList<IApplication> Search(int userId, Guid containerTypeId, Guid containerId, string searchText)
        {
            IList<IApplication> maps = new List<IApplication>();
            return maps;
        }
        #endregion
    }
}
