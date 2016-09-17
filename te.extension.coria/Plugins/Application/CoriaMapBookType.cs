using System;
using System.Collections.Generic;
using System.Web;

using Telligent.Evolution.Components;
using Telligent.DynamicConfiguration.Components;

using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;
using Telligent.Evolution.Extensibility.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using TeUi = Telligent.Evolution.Extensibility.UI.Version1;

namespace te.extension.coria.Plugins.Application
{
    public class CoriaMapBookType : IPlugin, IApplicationType,  IManageableApplicationType, IQueryableApplicationType, IApplicationNavigable, ICategorizedPlugin, IWebContextualApplicationType
    {
        IApplicationStateChanges _applicationState = null;
        public static Guid _applicationTypeId = new Guid("bfdb6103-e8e5-4cbf-8fbf-42dbac4046ef");
        public Guid ApplicationTypeId { get { return _applicationTypeId; } }
        public string ApplicationTypeName { get { return "MapBook"; } }
        public Guid[] ContainerTypes { get { return new Guid[] { Apis.Get<IGroups>().ContainerTypeId }; } }
        public string Description { get { return "Maps and metadata management tools"; } }
        public string Name { get { return "Coria Maps Application"; } }
        public void AttachChangeEvents(IApplicationStateChanges stateChanges) { _applicationState = stateChanges; }
        public IApplication Get(Guid applicationId) { return PublicApi.MapBooks.Get(applicationId); }
        public void Initialize()
        {

        }
        #region  IManageableApplicationType 
        public bool CanCreate(int userId, Guid containerTypeId, Guid containerId) { return true; }

        public bool CanDelete(int userId, Guid applicationId) { return true; }

        public bool CanSetEnabled(int userId, Guid applicationId) { return true; }

        public PropertyGroup[] GetCreateConfiguration(int userId, Guid containerTypeId, Guid containerId)
        {
            PropertyGroup mapBook = new PropertyGroup("MapBook", "Option", 1);
            mapBook.Properties.Add(new Property("mapBookName", "Name", PropertyType.String, mapBook.Properties.Count, "Map Book") { DescriptionText = "Title of the Map Book" });
            mapBook.Properties.Add(new Property("mapBookDesc", "Description", PropertyType.String, mapBook.Properties.Count, "a list of maps") { DescriptionText = "Description about the Map Book" });
            mapBook.Properties.Add(new Property("mapBookAvatarUrl", "Avatar URL", PropertyType.String, mapBook.Properties.Count, "/cfs-filesystemfile/__key/system/images/grid.svg") { DescriptionText = "Map Book Avatar's Url to svg or image" });
            mapBook.Properties.Add(new Property("mapBookIsEnabled", "Is Enabled", PropertyType.Bool, mapBook.Properties.Count, "true") { DescriptionText = "Map Book is enabled after creating" });
            mapBook.Properties.Add(new Property("mapBookUrl", "URL name of mapbooks list", PropertyType.String, mapBook.Properties.Count, "mapbooks") { DescriptionText = "URL name of mapbook list" });
            mapBook.Properties.Add(new Property("safeNameUrl", "URL name of mapbook", PropertyType.String, mapBook.Properties.Count, "mapbook") { DescriptionText = "key name" });

            Property apiProperty = new Property("defaultApi", "Default Mapping Api", PropertyType.String, mapBook.Properties.Count, "googleMaps") { DescriptionText = "New maps will use this api by default." };
            apiProperty.SelectableValues.Add(new PropertyValue("none", System.Web.HttpUtility.HtmlEncode("raw information for debugging"), apiProperty.SelectableValues.Count));
            apiProperty.SelectableValues.Add(new PropertyValue("mapbox", System.Web.HttpUtility.HtmlEncode("Map Box Api"), apiProperty.SelectableValues.Count));
            apiProperty.SelectableValues.Add(new PropertyValue("googleMaps", System.Web.HttpUtility.HtmlEncode("Google Maps Api"), apiProperty.SelectableValues.Count));
            apiProperty.SelectableValues.Add(new PropertyValue("arcGis", System.Web.HttpUtility.HtmlEncode("ArcGIS Api"), apiProperty.SelectableValues.Count));
            mapBook.Properties.Add(apiProperty);

            //PropertyGroup mapBook2 = new PropertyGroup("MapBook2", "Setup", 2);
            //mapBook2.Properties.Add(new Property("name", "name", PropertyType.String, mapBook2.Properties.Count, "more options") { DescriptionText = "more descriptions" });

            return new PropertyGroup[] { mapBook };
        }

        //1) can userId create application? anyone for now
        //2) what containerTypes are supported? just group containers for now
        //3) containerId is the group Guid, or any other application that has a nodeId
        //4) configurationDatabase is the properties set in the panel used to create the application

        public IApplication Create(int userId, Guid containerTypeId, Guid containerId, ConfigurationDataBase createConfigurationData)
        {
            try
            {  
              foreach (Guid _containerTypeId in ContainerTypes)
                {
                    //container types for groups is 
                    if (Apis.Get<IGroups>().ContainerTypeId == _containerTypeId)
                    {
                        int groupId = Apis.Get<IGroups>().Get(containerId).Id.Value;
                        InternalApi.CoriaMapBook coriaMapBook = new InternalApi.CoriaMapBook();
                        coriaMapBook.ApplicationId = Guid.NewGuid();
                        coriaMapBook.ApplicationTypeId = CoriaMapBookType._applicationTypeId;
                        coriaMapBook.AvatarUrl = createConfigurationData.GetStringValue("mapBookAvatarUrl", "/cfs-filesystemfile/__key/system/images/grid.svg");
                        coriaMapBook.Name = createConfigurationData.GetStringValue("mapBookName", "Map Book");
                        coriaMapBook.GroupId = groupId;
                        coriaMapBook.IsEnabled = createConfigurationData.GetBoolValue("mapBookIsEnabled", true);
                        coriaMapBook.Id = 0;
                        coriaMapBook.OntologyId = 0;
                        coriaMapBook.Description = createConfigurationData.GetStringValue("mapBookDesc", "a list of maps");
                        coriaMapBook.Url = createConfigurationData.GetStringValue("mapBookUrl", "mapbooks");
                        coriaMapBook.SafeName = createConfigurationData.GetStringValue("safeNameUrl", coriaMapBook.ApplicationId.ToString());
                        //coriaMapBook.SafeName = createConfigurationData.GetStringValue("mapBookUrl", "mapbook");

                        coriaMapBook = InternalApi.CoriaDataService.CreateUpdateMapBook(coriaMapBook);

                        return PublicApi.MapBooks.Get(coriaMapBook.Id);
                    }
                    if (Apis.Get<IUsers>().ContainerTypeId == _containerTypeId)
                    {
                        //TODO: implement user's map applications
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //IUserRenderableException.
                string exceptions = ex.Message;
                throw;
            }
            return null;
        }

        public void SetEnabled(int userId, Guid applicationId, bool enabled)
        {
            // throw new NotImplementedException();
        }

        public void Delete(int userId, Guid applicationId)
        {
            InternalApi.CoriaDataService.DeleteCoriaMapBookApplication(applicationId);
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
        #region IApplicationNavigable
        Guid IApplicationNavigable.ApplicationTypeId { get { return TEApi.Groups.ApplicationTypeId; } }

        public string[] Categories
        {
            get
            {
                return new string[] { "Coria" };
            }
        }

        public void RegisterUrls(IUrlController controller)
        {
            //var mapBookAction = new Action<HttpContextBase, PageContext>(MapBookAction); 
            controller.AddPage("GroupMapBookList", "mapbooks", null, null, "coria-mapbooklist", PdOptions);
            controller.AddPage("GroupMapBookSingle", "mapbooks/{mapBook}", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-mapbooklist", PdOptions);

            controller.AddPage("GroupMap", "mapbooks/{mapBook}/map", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-map-page", PdOptions);
            controller.AddPage("GroupMapNew", "mapbooks/{mapBook}/map/new", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-map-page", PdOptions);

            controller.AddPage("UserMapBookList", "members/{UserName}/{mapBooks}", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-mapbooklist", PdOptions);
            controller.AddPage("UserMapBookSingle", "members/{UserName}/{mapBooks}/{mapBook}", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-mapbooklist", PdOptions);

        }
        private PageDefinitionOptions PdOptions
        {
            get
            {
                return new PageDefinitionOptions()
                {
                    ForceLowercaseUrl = true,
                    HasApplicationContext = true,
                    ParseContext = new Action<PageContext>(this.ParseMapBookContext)
                    //Validate = new Action<PageContext, IUrlAccessController>(Validate)
                };
            }
        }

        private void MapBookAction(HttpContextBase arg1, PageContext arg2)
        {
            //throw new NotImplementedException();
        }
        private ContextItem BuildUserContextItem(Telligent.Evolution.Extensibility.Api.Entities.Version1.User user)
        {
            var item = new ContextItem()
            {
                TypeName = "User",
                ApplicationId = user.ContentId,
                ApplicationTypeId = TEApi.Users.ContentTypeId,
                ContainerId = user.ContentId,
                ContainerTypeId = TEApi.Users.ContentTypeId,
                ContentId = user.ContentId,
                ContentTypeId = TEApi.Users.ContentTypeId,
                Id = user.Id.ToString()
            };
            return item;
        }

        private void ParseMapBookContext(PageContext pageContext)
        {
            //try
            //{
            var ApplicationId = pageContext.GetTokenValue("ApplicationId");
            var ApplicationTypeId = pageContext.GetTokenValue("ApplicationTypeId");
            var allContext = pageContext.ContextItems.GetAllContextItems();
            var cnt = allContext.Count;
            int groupId = -1;
            var typename = allContext[0].TypeName;
            Guid appId = allContext[0].ApplicationId.Value;
            if (!int.TryParse(allContext[0].Id, out groupId))
            {
                //if groupid is not 
                // find by  mapbook name only... 
                // TODO: optimize database with safeName and GroupId indexing
            }
            string singleMapBook = pageContext.GetTokenValue("mapBook") as string;

            string appMapBookNameContext = "mapbooks";//pageContext.GetTokenValue("appName") as string;

            IList<PublicApi.MapBook> mapbooks = new List<PublicApi.MapBook>();
            var userName = pageContext.GetTokenValue("UserName");
            if (groupId > -1)
            {
                var group = TEApi.Groups.Get(new GroupsGetOptions { Id = groupId });
                ContextItem contextItem = new ContextItem();
                if (!string.IsNullOrEmpty(singleMapBook))
                {
                    PublicApi.MapBook mapbook = InternalApi.CoriaDataService.GetMapBookByGroupId_Name(groupId, "mapbooks", singleMapBook);

                    contextItem.ApplicationId = mapbook.ApplicationId;
                    contextItem.ApplicationTypeId = mapbook.ApplicationTypeId;
                    contextItem.ContainerId = mapbook.Container.ContainerId;
                    contextItem.ContainerTypeId = mapbook.Container.ContainerTypeId;    //Apis.Get<IGroups>().ContainerTypeId,  
                    contextItem.ContentId = mapbook.ApplicationId;
                    contextItem.ContentTypeId = mapbook.ApplicationTypeId;
                    contextItem.TypeName = ApplicationTypeName;
                    contextItem.Id = mapbook.Group.Id.Value.ToString();

                }
                else
                {
                    mapbooks = PublicApi.MapBooks.List(groupId);

                    contextItem.ApplicationId = mapbooks[0].ApplicationId;
                    contextItem.ApplicationTypeId = mapbooks[0].ApplicationTypeId;
                    contextItem.ContainerId = mapbooks[0].Container.ContainerId;
                    contextItem.ContainerTypeId = mapbooks[0].Container.ContainerTypeId;    //Apis.Get<IGroups>().ContainerTypeId,  
                    contextItem.ContentId = mapbooks[0].ApplicationId;
                    contextItem.ContentTypeId = mapbooks[0].ApplicationTypeId;
                    contextItem.Id = mapbooks[0].Group.Id.Value.ToString();

                }


                if (userName != null)
                {
                    var user = TEApi.Users.Get(new UsersGetOptions() { Username = userName.ToString() });
                    if (user != null)
                    {
                        contextItem.ContainerId = BuildUserContextItem(user).ContainerId;
                        contextItem.ContainerTypeId = BuildUserContextItem(user).ContainerTypeId.Value;
                        contextItem.Id = BuildUserContextItem(user).Id.ToString();
                    }
                    else
                    {

                        Globals.RedirectToLoginOrThrowException(new InternalApi.Utility.CoriaException(CSExceptionType.UserNotFound, "Coria Parse MapBook Context.", null));
                    }
                }

                pageContext.ContextItems.Put(contextItem);
            }
            //}
            //catch(Exception ex)
            //{
            //    throw new InternalApi.Utility.CoriaException(Categories[0], "ParseMapBookContex method error. ", ex);
            //}

        }
        #endregion
        #region IWebContext

                bool IWebContextualApplicationType.IsCurrentApplicationType(TeUi.IWebContext context)
        {
           bool iscurrentApplicationType = context.Url.Contains("mapbooks") ? true : false  ;
            return iscurrentApplicationType;
        }

        public IApplication GetCurrentApplication(TeUi.IWebContext context)
        {
            PublicApi.MapBook mapbook = new PublicApi.MapBook();
            IApplication app = mapbook as IApplication;
            var apptype = app.GetType();
            if (TEApi.Url.CurrentContext == null) return null;
            var item = TEApi.Url.CurrentContext.ContextItems.GetAllContextItems();
            foreach (var it in item)
            {
                var typeName = it.TypeName;
                if (ApplicationTypeName == typeName.ToString())
                {

                    if (it.ApplicationId.HasValue)
                        return PublicApi.MapBooks.Get(it.ApplicationId.Value);
                }
            }
            //    if (PublicApi.Url.CurrentContext == null) return null;
            //    var item = PublicApi.Url.CurrentContext.ContextItems.GetItemByContentType(ContentTypeId);

            //    if (item != null && item.ContentId.HasValue)
            //        return IdeasApi.Ideas.Get(item.ContentId.Value);

            //    return null;

            return null;
        }
        
        #endregion
    }
    /***

 using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace Telligent.Evolution.Extensibility.Content.Version1
{
public interface IWebContextualApplicationType : IPlugin, IApplicationType
{
IApplication GetCurrentApplication(IWebContext context);
bool IsCurrentApplicationType(IWebContext context);
}
}

    #endregion
    //IContent IWebContextualContentType.GetCurrentContent(Extensibility.UI.Version1.IWebContext context)
    //{
    //    if (PublicApi.Url.CurrentContext == null) return null;
    //    var item = PublicApi.Url.CurrentContext.ContextItems.GetItemByContentType(ContentTypeId);

    //    if (item != null && item.ContentId.HasValue)
    //        return IdeasApi.Ideas.Get(item.ContentId.Value);

    //    return null;
    //}
}
/**********
 public class CoriaMapManagePanel : IPlugin, IManageableApplicationType, IQueryableApplicationType
 {
     #region IPlugin
     public string Name { get { throw new NotImplementedException(); } }
     public string Description { get { throw new NotImplementedException(); } }
     public void Initialize() { }
     #endregion
     #region IQueryableApplicationType
     public string ApplicationTypeName { get { throw new NotImplementedException(); } }

     public Guid ApplicationTypeId { get { throw new NotImplementedException(); } }

     public Guid[] ContainerTypes { get { throw new NotImplementedException(); } }

     public IList<IApplication> List(int userId, Guid containerTypeId, Guid containerId) { throw new NotImplementedException(); }

     public IList<IApplication> Search(int userId, Guid containerTypeId, Guid containerId, string searchText) { throw new NotImplementedException(); }

     public IApplication Get(Guid applicationId) { throw new NotImplementedException(); }

     public void AttachChangeEvents(IApplicationStateChanges stateChanges) { throw new NotImplementedException(); }

     #endregion
     #region IManageableApplicationType

     public bool CanCreate(int userId, Guid containerTypeId, Guid containerId) { throw new NotImplementedException(); }
     public bool CanDelete(int userId, Guid applicationId) { throw new NotImplementedException(); }
     public bool CanEdit(int userID, Guid applicationId) { throw new NotImplementedException(); }
     public bool CanSetEnabled(int userId, Guid applicationId) { throw new NotImplementedException(); }
     public IApplication Create(int userId, Guid containerTypeId, Guid containerId, ConfigurationDataBase createConfigurationData) { throw new NotImplementedException(); }
     public void Delete(int userId, Guid applicationId) { throw new NotImplementedException(); }
     public PropertyGroup[] GetCreateConfiguration(int userId, Guid containerTypeId, Guid containerId) { throw new NotImplementedException(); }
     public void SetEnabled(int userId, Guid applicationId, bool enabled) { throw new NotImplementedException(); }
     #endregion
 }
 ***/
}
