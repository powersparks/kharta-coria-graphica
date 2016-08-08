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
        //application id get's set with each new application
        // public static Guid _applicationId = new Guid("7b3cd226-ef49-4aca-94eb-72f1e49f3688");
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
            PublicApi.MapBooks.Get(applicationId);
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
            return true;
        }

        public bool CanSetEnabled(int userId, Guid applicationId)
        {
            return true;
        }

        public PropertyGroup[] GetCreateConfiguration(int userId, Guid containerTypeId, Guid containerId)
        {
            PropertyGroup mapBook = new PropertyGroup("MapBook", "Option", 1);
            mapBook.Properties.Add(new Property("mapBookName", "Name", PropertyType.String, mapBook.Properties.Count, "Maps") { DescriptionText = "Title of the Map Book" });
            mapBook.Properties.Add(new Property("mapBookDesc", "Description", PropertyType.String, mapBook.Properties.Count, "a list of maps") { DescriptionText = "Description about the Map Book" });
            mapBook.Properties.Add(new Property("mapBookAvatarUrl", "Avatar URL", PropertyType.String, mapBook.Properties.Count, "/cfs-filesystemfile/__key/system/images/grid.svg") { DescriptionText = "Map Book Avatar's Url to svg or image" });
            mapBook.Properties.Add(new Property("mapBookIsEnabled", "Is Enabled", PropertyType.Bool, mapBook.Properties.Count, "true") { DescriptionText = "Map Book is enabled after creating" });
            mapBook.Properties.Add(new Property("mapBookUrl", "URL name of mapbook", PropertyType.String, mapBook.Properties.Count, "mapbook") { DescriptionText = "URL name of the mapbook" });

            Property apiProperty = new Property("defaultApi", "Default Mapping Api", PropertyType.String, 1, "googleMaps") { DescriptionText = "New maps will use this api by default."};
            apiProperty.SelectableValues.Add(new PropertyValue("none", System.Web.HttpUtility.HtmlEncode("raw information for debugging"), apiProperty.SelectableValues.Count));
            apiProperty.SelectableValues.Add( new PropertyValue("mapbox", System.Web.HttpUtility.HtmlEncode("Map Box Api"), apiProperty.SelectableValues.Count));
            apiProperty.SelectableValues.Add(new PropertyValue("googleMaps", System.Web.HttpUtility.HtmlEncode("Google Maps Api"), apiProperty.SelectableValues.Count));
            apiProperty.SelectableValues.Add(new PropertyValue("arcGis", System.Web.HttpUtility.HtmlEncode("ArcGIS Api"), apiProperty.SelectableValues.Count));
            mapBook.Properties.Add(apiProperty);
             
            return new PropertyGroup[] { mapBook };
        }

        //1) can userId create application? anyone for now
        //2) what containerTypes are supported? just group containers for now
        //3) containerId is the group Guid, or any other application that has a nodeId
        //4) configurationDatabase is the properties set in the panel used to create the application

        public IApplication Create(int userId, Guid containerTypeId, Guid containerId, ConfigurationDataBase createConfigurationData)
        {
            
            foreach (Guid _containerTypeId in ContainerTypes)
            {
                //container types for groups is 
                if (Apis.Get<IGroups>().ContainerTypeId == _containerTypeId)
                {
                    int groupId = Apis.Get<IGroups>().Get(containerId).Id.Value;
                    InternalApi.CoriaMapBook coriaMapBook = new InternalApi.CoriaMapBook();
                    coriaMapBook.ApplicationId = Guid.NewGuid();
                    coriaMapBook.ApplicationTypeId = CoriaMapType._applicationTypeId;
                    coriaMapBook.AvatarUrl = createConfigurationData.GetStringValue("mapBookAvatarUrl", "/cfs-filesystemfile/__key/system/images/grid.svg");
                    coriaMapBook.Name = createConfigurationData.GetStringValue("mapBookName", "Map Book");
                    coriaMapBook.GroupId = groupId;
                    coriaMapBook.IsEnabled = createConfigurationData.GetBoolValue("mapBookIsEnabled", true);
                    coriaMapBook.Id = 0;
                    coriaMapBook.OntologyId = 0;
                    coriaMapBook.Description = createConfigurationData.GetStringValue("mapBookDesc", "a list of maps");
                    coriaMapBook.Url = createConfigurationData.GetStringValue("mapBookUrl", "mapbook");
                    //coriaMapBook.SafeName = createConfigurationData.GetStringValue("mapBookUrl", coriaMapBook.ApplicationId.ToString());

                    coriaMapBook = InternalApi.CoriaDataService.CreateUpdateMapBook(coriaMapBook);

                    return PublicApi.MapBooks.Get(coriaMapBook.Id);
                }
                if (Apis.Get<IUsers>().ContainerTypeId == _containerTypeId)
                {
                    //TODO: implement user's map applications
                    return null;
                }
            }
            return null;
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
            IList<IApplication> mapbooks = new List<IApplication>();
           // IList<PublicApi.Entity.MapBook> mapbooks = new List<PublicApi.Entity.MapBook>();
            foreach (Guid _containerTypeId in ContainerTypes)
            {
                //container types for groups is 
                if (Apis.Get<IGroups>().ContainerTypeId == _containerTypeId)
                {
                    int groupId = Apis.Get<IGroups>().Get(containerId).Id.Value;
                     
                     mapbooks = PublicApi.MapBooks.GetMapBookApplicationsByGroup(groupId);
                }
                if (Apis.Get<IUsers>().ContainerTypeId == _containerTypeId)
                {
                    //TODO: implement user's map applications
                    return mapbooks;
                }
            }


            return mapbooks;//.Cast<IApplication>().ToList(); ;
        }

        public IList<IApplication> Search(int userId, Guid containerTypeId, Guid containerId, string searchText)
        {
            IList<IApplication> maps = new List<IApplication>();
            return maps;
        }
        #endregion
    }
}
