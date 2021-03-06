﻿using System;
using System.Collections.Generic;
using System.Web;

 
using Telligent.DynamicConfiguration.Components;
 
 
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;
using TeUrls = Telligent.Evolution.Extensibility.Urls;
using Telligent.Evolution.Extensibility.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using TeUi = Telligent.Evolution.Extensibility.UI.Version1;

namespace te.extension.coria.Plugins.Application
{
    public class CoriaMapBookType : IPlugin, IApplicationType, IManageableApplicationType, IQueryableApplicationType, IApplicationNavigable, INavigableApplicationType, ICategorizedPlugin, IWebContextualApplicationType//, TeUi.IGroupCustomNavigationPlugin , TeUi.IGroupDefaultCustomNavigationPlugin //, TeUrls.Version1.INavigableApplicationType
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
            InternalApi.CoriaDataService.MapBook_SetEnabled(userId, applicationId, enabled);
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
        Guid IApplicationNavigable.ApplicationTypeId { get { return this.ApplicationTypeId /***TEApi.Groups.ApplicationTypeId**/; } }

        public string[] Categories
        {
            get
            {
                return new string[] { "Coria" };
            }
        }
        #region INavigableApplicationType
        public string PathDelimiter
        {
            get { return "mapbooks"; }
        }
        #endregion
        public void RegisterUrls(IUrlController controller)
        {
            //var mapBookAction = new Action<HttpContextBase, PageContext>(MapBookAction); 
            controller.AddPage("GroupMapBookList", "", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-mapbook-list", MapBooks_PdOptions);
            controller.AddPage("GroupMapBookSingle", "{mapBook}", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-mapbook-map-list", MapBook_PdOptions);
            controller.AddPage("GroupMap", "{mapBook}/map/{mapId}", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-map-page", Map_PdOptions);

        }
        private PageDefinitionOptions MapBooks_PdOptions
        {
            get
            {
                return new PageDefinitionOptions()
                {
                    ForceLowercaseUrl = true,
                    HasApplicationContext = true,
                    ParseContext = new Action<PageContext>(this.ParseMapBooks_Context)
                    //Validate = new Action<PageContext, IUrlAccessController>(Validate)
                };
            }
        }
        private PageDefinitionOptions MapBook_PdOptions
        {
            get
            {
                return new PageDefinitionOptions()
                {
                    ForceLowercaseUrl = true,
                    HasApplicationContext = true,
                    ParseContext = new Action<PageContext>(this.ParseMapBook_Context)
                    //Validate = new Action<PageContext, IUrlAccessController>(Validate)
                };
            }
        }
        private PageDefinitionOptions Map_PdOptions
        {
            get
            {
                return new PageDefinitionOptions()
                {
                    ForceLowercaseUrl = true,
                    HasApplicationContext = true,
                    ParseContext = new Action<PageContext>(this.ParseMapBook_Context)
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

        private void ParseMapBooks_Context(PageContext pageContext)
        {

            var ApplicationId = pageContext.GetTokenValue("ApplicationId");
            var ApplicationTypeId = pageContext.GetTokenValue("ApplicationTypeId");
            var allContext = pageContext.ContextItems.GetAllContextItems();
            var cnt = allContext.Count;
            int groupId = -1;
            var applicationId = allContext[0].ApplicationId.Value;
            var applicationTypeId = allContext[0].ApplicationTypeId.Value;
            var containerId = allContext[0].ContainerId.Value;
            var contrainerTypeId = allContext[0].ContainerTypeId;
            var contenId = allContext[0].ContentId.Value;
            var contentTypeId = allContext[0].ContentTypeId.Value;
            var typeNameId = allContext[0].Id;
            var relationship = allContext[0].Relationship;
            var typename = allContext[0].TypeName;
            var hashCode = allContext[0].GetHashCode();


            if (!int.TryParse(allContext[0].Id, out groupId))
            {
                //if groupid is not 
                // find by  mapbook name only... 
                // TODO: optimize database with safeName and GroupId indexing
            }

            IList<PublicApi.MapBook> mapbooks = new List<PublicApi.MapBook>();

            if (groupId > -1)
            {
                var group = TEApi.Groups.Get(new GroupsGetOptions { Id = groupId });
                ContextItem contextItem = new ContextItem();

                try
                {
                    if (groupId >= 0)
                    {
                        mapbooks = PublicApi.MapBooks.List(groupId);
                        if (mapbooks != null)
                        {
                            if (mapbooks.Count == 1)
                            {
                                contextItem.ApplicationId = mapbooks[0].ApplicationId;
                                contextItem.ContentId = mapbooks[0].ApplicationId;
                            }

                            contextItem.ContainerId = mapbooks[0].Container.ContainerId;
                            contextItem.ContainerTypeId = mapbooks[0].Container.ContainerTypeId;    //Apis.Get<IGroups>().ContainerTypeId,  

                            contextItem.ContentTypeId = mapbooks[0].ApplicationTypeId;
                            contextItem.TypeName = ApplicationTypeName;
                            contextItem.Id = mapbooks[0].Group.Id.Value.ToString();

                        }
                        contextItem.TypeName = ApplicationTypeName;
                        contextItem.ApplicationTypeId = this.ApplicationTypeId;
                    }
                }
                catch (Exception)
                {

                    //throw;
                }

                pageContext.ContextItems.Put(contextItem);
            }
        }
        private void ParseMapBook_Context(PageContext pageContext)
        {
            var allContext = pageContext.ContextItems.GetAllContextItems();
            var cnt = allContext.Count;
            int groupId = -1;
            var typename = allContext[0].TypeName;
            Guid appId = allContext[0].ApplicationId.Value;
            if (!int.TryParse(allContext[0].Id, out groupId))
            {
 
            }
            string singleMapBook = pageContext.GetTokenValue("mapBook") as string;

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
                pageContext.ContextItems.Put(contextItem);
            }
            

        }
        #endregion
        #region IWebContext

        bool IWebContextualApplicationType.IsCurrentApplicationType(TeUi.IWebContext context)
        {
            bool isCurrentMapBookType = false;
            bool maybeMapBookTypes = (context.Url.Contains("/mapbooks/")) ? true : false;
            //to be current Application requires "/mapbooks/" maybe it is
            bool maybeCurrentMapBookType = (!context.Url.EndsWith("/mapbooks/") && maybeMapBookTypes) ? true : false;
            //if the url doesn't end with 
            if (TEApi.Url.CurrentContext != null && maybeCurrentMapBookType)
            {
                var item = TEApi.Url.CurrentContext.ContextItems.Find(c => c.ApplicationTypeId.Value == ApplicationTypeId);//.GetAllContextItems();
                isCurrentMapBookType = (item != null && item.ApplicationId.HasValue) ? true : false;
            }
            return isCurrentMapBookType;
        }
        public IApplication GetCurrentApplication(TeUi.IWebContext context)
        {
            PublicApi.MapBook mapbook = new PublicApi.MapBook();
            IApplication app = mapbook as IApplication;

            if (TEApi.Url.CurrentContext == null) return null;
            var item = TEApi.Url.CurrentContext.ContextItems.Find(c => c.ApplicationTypeId.Value == ApplicationTypeId);//.GetAllContextItems();
            if (item != null && item.ApplicationId.HasValue) { return PublicApi.MapBooks.Get(item.ApplicationId.Value); }

            return null;
        }



        #endregion
    }
         
}
