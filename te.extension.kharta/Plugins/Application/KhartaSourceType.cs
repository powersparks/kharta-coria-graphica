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
    public class KhartaSourceType : IApplicationType, IPluginGroup, IManageableApplicationType, IQueryableApplicationType
    {
        IApplicationStateChanges _applicationState = null;
        public static Guid _applicationTypeId = new Guid("514581ca-4b1b-4be9-be6b-eed9c80c4938");
        public static Guid _applicationId = new Guid("17bb4a43-c87c-4238-b560-0fd157933c8e");
        public Guid ApplicationTypeId { get { return _applicationTypeId; } }

        public string ApplicationTypeName { get { return "Source Library"; } }

        public Guid[] ContainerTypes { get { return new Guid[] { Apis.Get<IGroups>().ContainerTypeId }; } }

        public string Description { get { return "Sources and metadata management tools"; } }

        public string Name { get { return "Kharta Sources Application"; } }

        public IEnumerable<Type> Plugins
        {
            get
            {
                return new Type[] { typeof(Content.SourceContentType) };
            }
        }

        public void AttachChangeEvents(IApplicationStateChanges stateChanges) { _applicationState = stateChanges; }

        public IApplication Get(Guid applicationId) { return PublicApi.Sources.GetSourceApplication(applicationId); }

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
            IList<IApplication> sources = new List<IApplication>();
            return sources;
        }

        public IList<IApplication> Search(int userId, Guid containerTypeId, Guid containerId, string searchText)
        {
            IList<IApplication> sources = new List<IApplication>();
            return sources;
        }
        #endregion
    }
}
