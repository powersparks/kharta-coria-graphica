using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Components;
using Telligent.Evolution.Extensibility.Administration.Version1;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Security.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Urls.Routing;
using Permission = Telligent.Evolution.Extensibility.Security.Version1.Permission;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using UIApi = Telligent.Evolution.Extensibility.UI.Version1;
using System.Reflection;
using System.Text;
using Telligent.Evolution.Extensibility.UI.Version1;


namespace te.extension.kharta.Plugins
{
    public class Containers : IPlugin, IInstallablePlugin, IPluginGroup, IContainers,  IConfigurablePlugin, IAdministrationPanel, IAdministrationPanelCategory, IAdministrationExplicitPanel, IAdministrationExplicitPanelController
    {
        public readonly Guid Panel_id = new Guid("4ebcc1b3-9daa-4550-aca5-77bb13d026d0");
        public readonly Guid Containers_id = new Guid("5127c319-fda6-40e4-bfd7-37bbfef3ba39");
        public readonly Guid ContainersType_id = new Guid("e504f58d-c1d8-40a8-bf55-bc38c65625e9");
        //private static readonly List<Containers> AllContainers = new List<Container>()
        //{
        //    new kharta.PublicApi.KhartaOntology(){ ContainerId = new Guid("cd3cdc13-8b4f-4dfd-9a97-9e5c06d1fa73"), Name = "Best Search Engines", Description = "List of the popular engines" },
        //    new kharta.PublicApi.KhartaOntology() { ContainerId = new Guid("15238a71-e923-463c-9df8-a98f022b0760"), Name = "Telligent Sites", Description = "List of Telligent sites" },
        //    new kharta.PublicApi.KhartaOntology() { ContainerId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), Name = "Telligent Community Developer Sites", Description = "Some helpful sites for Telligent Community Platform development." }
        //};
        private static readonly Version _emptyVersion = new Version(0, 0, 0, 0);
        #region IInstallablePlugin
        public Version Version { get { return GetType().Assembly.GetName().Version; }}

        public void Install(Version lastInstalledVersion)
        {
            #region Install SQL
            /***** if (lastInstalledVersion == null || lastInstalledVersion.Major == 0)
                 InternalApi.PollingDataService.Install();

             if (lastInstalledVersion == null || lastInstalledVersion <= new Version(1, 1))
                 InternalApi.PollingDataService.Install("update-1.1.sql");

             if (lastInstalledVersion == null || lastInstalledVersion <= new Version(1, 3))
                 InternalApi.PollingDataService.Install("update-1.3.sql");

             InternalApi.PollingDataService.Install("storedprocedures.sql");***/
            #endregion

            #region Install Widgets
            /*****
            _widgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<KhartaApplicationsFactoryDefaultWidgetProvider>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_widgetProvider);

            var definitionFiles = new string[] {
                "KhartaSourcesWidget/KhartaSourcesWidget.xml",
                "KhartaFrameScrubber/KhartaFrameScrubber.xml",
                "KhartaGlobeViewer/KhartaGlobeViewer.xml",
            };

            foreach (var definitionFile in definitionFiles)
            {
                using (var stream = InternalApi.EmbeddedResources.GetStream("Telligent.Evolution.Extensions.Kharta.Resources." + definitionFile.Replace("/", ".")))
                {
                    UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateDefinitionFile(_widgetProvider, definitionFile.Substring(definitionFile.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);
                }
            }

            var supplementaryFiles = new Dictionary<Guid, string[]>();
            supplementaryFiles[new Guid("3f7f66e972964a0688d7fe014fb49500")] = new string[] {
                "KhartaSourcesWidget/supplementary/ui.js",
                "KhartaSourcesWidget/supplementary/map.css",
                "KhartaSourcesWidget/supplementary/jsondata.vm",
                "KhartaSourcesWidget/supplementary/d3.5.16.min.js"
            };
            // var supplementaryFiles = new Dictionary<Guid, string[]>();
            supplementaryFiles[new Guid("5f2949bbf2c741b180c53669e2359e08")] = new string[] {
                "KhartaFrameScrubber/supplementary/ui.js",
                "KhartaFrameScrubber/supplementary/map.css",
                "KhartaFrameScrubber/supplementary/jsondata.vm",
                "KhartaFrameScrubber/supplementary/d3.5.16.min.js"
            };
            supplementaryFiles[new Guid("3d3f16561e714f9e967e586221ac70a0")] = new string[] {
                "KhartaGlobeViewer/supplementary/globeui.js",
                "KhartaGlobeViewer/supplementary/map.css",
                "KhartaGlobeViewer/supplementary/world110m.vm",
                "KhartaGlobeViewer/supplementary/worldcountrynames.vm",
                "KhartaGlobeViewer/supplementary/jsondata.vm",
                "KhartaGlobeViewer/supplementary/d3.5.16.min.js"
            };
            foreach (var instanceId in supplementaryFiles.Keys)
            {
                foreach (var relativePath in supplementaryFiles[instanceId])
                {
                    using (var stream = InternalApi.EmbeddedResources.GetStream("Telligent.Evolution.Extensions.Kharta.Resources." + relativePath.Replace("/", ".")))
                    {
                        UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateSupplementaryFile(_widgetProvider, instanceId, relativePath.Substring(relativePath.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);
                    }
                }
            }


            *****/
            #endregion


        }

        public void Uninstall()
        {
            #region Remove Widget Files
            /***
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_widgetProvider);
            ***/
            #endregion
        }

        #endregion
        #region IPlugin
        public string Name { get  { return "Kharta Coria Graphica"; } }
        public string Description {  get { return "A Telligent friendly tools for creating Lists, Charts & Maps from geospatial sources"; } }
        public Guid DataTypeId { get { return Containers_id; } }

        public void Initialize()
        {
            //throw new NotImplementedException();
        }
/*****/
        #region IPluginGroup
        public IEnumerable<Type> Plugins
        {
            get
            {
                return new Type[] {
                    typeof(Applications)
               };
            }
        }
        #endregion

        #region IContainers

        public IContainerEvents Events
        {
            //   // public delegate void KhartaBeforeCreateEventHandler(KhartaBeforeCreateEventArgs e);
            ////public delegate void KhartaAfterCreateEventHandler(KhartaAfterCreateEventArgs e);

            get
            {
                //        // event ContainerAfterCreateEventHandler AfterCreate;
                //        //event ContainerAfterDeleteEventHandler AfterDelete;
                //        //event ContainerAfterUpdateEventHandler AfterUpdate;
                //        //event ContainerBeforeCreateEventHandler BeforeCreate;
                //        //event ContainerBeforeDeleteEventHandler BeforeDelete;
                //        //event ContainerBeforeUpdateEventHandler BeforeUpdate;

                //        //void OnAfterCreate(IContainer contentContainer);
                //        //void OnAfterDelete(IContainer contentContainer);
                //        //void OnAfterUpdate(IContainer contentContainer);
                //        //void OnBeforeCreate(IContainer contentContainer);
                //        //void OnBeforeDelete(IContainer contentContainer);
                //        //void OnBeforeUpdate(IContainer contentContainer);
                //        //event _event = new event();
                //        //_event..Add(new Property("connectionString", "Database Connection String", PropertyType.String, 1, "") { DescriptionText = "The connection string used to access a SQL 2008 or newer database. The user identified should have db_owner permissions to the database." });
                return null;
            }
        }
     
        public Container Get([Documentation("Container Id")] Guid containerId, [Documentation("Container type Id")] Guid containerTypeId)
        {
            return null;       // throw new NotImplementedException();
        }


        #endregion
       
        #region IConfigurablePlugin
        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                PropertyGroup group = new PropertyGroup("setup", "Setup", 1);
                group.Properties.Add(new Property("connectionString", "Database Connection String", PropertyType.String, 1, "") { DescriptionText = "The connection string used to access a SQL 2008 or newer database. The user identified should have db_owner permissions to the database." });
                return new PropertyGroup[] { group }; // throw new NotImplementedException();
            }
        }
        public void Update(IPluginConfiguration configuration)
        {
           // throw new NotImplementedException();
        }
        #endregion




        #endregion

        #region IAdministrationExplicit
        public string GetUrl()
        {
            return null;// throw new NotImplementedException();
        }

        public string GetUrl(NameValueCollection parameters)
        {
            return null;// throw new NotImplementedException();
        }
        #endregion



        #region IAdministrationPanel


        public string CssClass
        {
            get
            {
                return "kharta-class";
            }
        }


        public int? DisplayOrder
        {
            get
            {
                return 3;
            }
        }

        public bool IsCacheable
        {
            get
            {
                return false;
            }
        }



        public string PanelDescription
        {
            get
            {
                return "add Kharta admin panel";
            }
        }

        public Guid PanelId
        {
            get
            {
                return Panel_id;
            }
        }

        public string PanelName
        {
            get
            {
                return "Kharta Panel Name";
            }
        }

        public bool VaryCacheByUser
        {
            get
            {
                return false;// throw new NotImplementedException();
            }
        }

        public string GetViewHtml()
        {
            return "Kharta view html";//throw new NotImplementedException();
        }

        public bool HasAccess(int userId)
        {
            return true;//throw new NotImplementedException();
        }


        #endregion
        #region IAdministrationPanelCategory
        public Guid AdministrationPanelCategoryId
        {
            get
            {
                return Panel_id;
            }
        }


        public string AvatarUrl
        {
            get
            {
                return "/cfs-filesystemfile/__key/system/images/kharta.svg";
            }
        }

        public string CategoryName
        {
            get
            {
                return "Ontologies";
            }
        }


        #endregion

        #region IAdministrationPanelExplicitControler


        public string GetPanelName(NameValueCollection parameters)
        {
            return "KCG Admin Panel";
        }

        public string GetPanelDescription(NameValueCollection parameters)
        {
            return "KCG Admin Panel Description";
        }

        public bool HasAccess(int userId, NameValueCollection parameters)
        {
            return true;// throw new NotImplementedException();
        }

        public string GetViewHtml(NameValueCollection parameters)
        {
            return "KCG view html"; throw new NotImplementedException();
        }

        public void SetController(IAdministrationExplicitPanelController controller)
        {
            //
        }

      
        /*****/

        #endregion
    }
}
