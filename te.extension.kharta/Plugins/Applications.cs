using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Components;
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Administration.Version1;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.Security.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Urls.Routing;
using Permission = Telligent.Evolution.Extensibility.Security.Version1.Permission;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using UIApi = Telligent.Evolution.Extensibility.UI.Version1;

 

namespace te.extension.kharta.Plugins
{
    public class Applications :  IPlugin, IInstallablePlugin, IPluginGroup, IConfigurablePlugin, IAdministrationPanel, IAdministrationPanelCategory, IAdministrationExplicitPanel, IAdministrationExplicitPanelController
    {
        private static readonly Version _emptyVersion = new Version(0, 0, 0, 0);
        public static readonly Guid Panel_id = new Guid("4ebcc1b3-9daa-4550-aca5-77bb13d026d0");
        public static readonly Guid ApplicationsType_id = new Guid("e504f58d-c1d8-40a8-bf55-bc38c65625e9");
        private IAdministrationExplicitPanelController _iAdministrationExplicitPanelController;// = new AdministrationExplicitPanelController();

        #region IPlugin
        public string Name { get { return "Kharta Coria Graphica"; } }
        public string Description { get { return "A Telligent friendly tools for creating Lists, Charts & Maps from geospatial sources"; } }
        public Guid DataTypeId { get { return ApplicationsType_id; } }
        public void Initialize() { }
        #region IConfigurablePlugin
        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                PropertyGroup group1 = new PropertyGroup("option1", "Setup", 1);//tab 1 ui
                group1.Properties.Add(new Property("propertyKeyName1", "Property Key Name Title", PropertyType.String, 1, "property key default value") { DescriptionText = "Property Key and Value Description." });
                PropertyGroup group2 = new PropertyGroup("option2", "Advanced", 2);//tab 2 ui
                group2.Properties.Add(new Property("propertyKeyName2", "Property Key Name Title", PropertyType.String, 1, "property key default value") { DescriptionText = "Property Key and Value Description." });

                return new PropertyGroup[] { group1, group2 };
            }
        }
        public void Update(IPluginConfiguration configuration)
        {
            //TODO: configuration values need to be passed in and used somewhere
            //InternalApi.InternalClassName.PropertyKeyName1 = configuration.GetString("propertyKeyName1");
        }
        #endregion
        #region IInstallablePlugin
        public Version Version { get { return GetType().Assembly.GetName().Version; } }
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
        #region IPluginGroup
        public IEnumerable<Type> Plugins {  get { return new Type[] { typeof(KhartaContainerType) };} }
        #endregion
        #endregion
        #region Administration Panels
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
            return "KCG view html"; //throw new NotImplementedException();
        }

        public void SetController(IAdministrationExplicitPanelController controller)
        {
            _iAdministrationExplicitPanelController = controller;
        }

        #endregion
        #endregion

    }
}
