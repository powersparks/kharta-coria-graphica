using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using te.extension.coria.Plugins.UI;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Extensibility.Version1;
using UIApi = Telligent.Evolution.Extensibility.UI.Version1;
using Embr = te.extension.coria.InternalApi.Utility.EmbeddedResources;

namespace te.extension.coria.Plugins
{ 
    public class CoriaApplications : IPlugin, IInstallablePlugin, IPluginGroup, IConfigurablePlugin //IPlugin, IConfigurablePlugin, IRequiredConfigurationPlugin, IInstallablePlugin, IPluginGroup, IApplicationNavigable, INavigable, ITranslatablePlugin, IPermissionRegistrar, ICategorizedPlugin
    {
        private static readonly Version _emptyVersion = new Version(0, 0, 0, 0);
        public static readonly Guid CoriaType_id = new Guid("6894aeae-4e9d-459c-8d56-c3ca70f39d92");
        // private KhartaFactoryDefaultWidgetProvider _widgetProvider;
        private CoriaFactoryDefaultWidgetProvider _widgetProvider;


        #region IPlugin
        public string Name { get { return "Coria Applications"; } }
        public string Description { get { return "A Telligent friendly tools for creating Lists, Charts & Maps using standard data sources"; } }
        public Guid DataTypeId { get { return CoriaType_id; } }
        public void Initialize() { _widgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<CoriaFactoryDefaultWidgetProvider>().FirstOrDefault(); }
        #region IConfigurablePlugin
        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                PropertyGroup MapApis = new PropertyGroup("MapApis", "Map APIs", 10);//tab 1 ui
                MapApis.Properties.Add(new Property("publicKey", "Public Key", PropertyType.String, 101, "") { DescriptionText = "Google Maps API key is added to the url" });
                MapApis.Properties.Add(new Property("publicClientId", "Public Client Id", PropertyType.String, 102, "") { DescriptionText = "Google Maps business or premium plans offer a client id which is added to the url" });
                MapApis.Properties.Add(new Property("defaultUseKeyOrClientId", "Client Id used by default", PropertyType.Bool, 103, "True") { DescriptionText = "The client Id will be used first if checked. If unchecked, the key will be used first." });
                MapApis.Properties.Add(new Property("gmVersion", "Google Maps Api Version", PropertyType.String, 104, "3") { DescriptionText = "Api version examples: 3, 3.23, 3.exp" });

                //PropertyGroup mapBoxApi = new PropertyGroup("MapBoxApi", "MapBox API", 11);//tab 2 ui
                MapApis.Properties.Add(new Property("publicaccess_token", "Public access_token", PropertyType.String, 111, "") { DescriptionText = "MapBox access_token = pk_{your token}" });
                MapApis.Properties.Add(new Property("secretKey", "Secret Key", PropertyType.String, 112, "") { DescriptionText = "MapBox secret key sk_{your token}.  currently not implemented" });
                MapApis.Properties.Add(new Property("mapBoxVersion", "MapBox API Version", PropertyType.String, 113, "v0.21.0") { DescriptionText = "MapBox API Version, example:v0.21.0" });

                //PropertyGroup arcGisApi = new PropertyGroup("ArcGisApi", "ArcGis API", 12);
                MapApis.Properties.Add(new Property("arcGisVersion", "ArcGis Version", PropertyType.String, 121, "4.0") { DescriptionText = "ArcGIS API Version, example: 4.0 " });

                //PropertyGroup googleMapsApi = new PropertyGroup("GoogleMapsApi", "Google Maps API", 10);//tab 1 ui
                //googleMapsApi.Properties.Add(new Property("publicKey", "Public Key", PropertyType.String, 101, "") { DescriptionText = "Google Maps API key is added to the url" });
                //googleMapsApi.Properties.Add(new Property("publicClientId", "Public Client Id", PropertyType.String, 102, "") { DescriptionText = "Google Maps business or premium plans offer a client id which is added to the url" });
                //googleMapsApi.Properties.Add(new Property("defaultUseKeyOrClientId", "Client Id used by default", PropertyType.Bool, googleMapsApi.Properties.Count(), "True") { DescriptionText = "The client Id will be used first if checked. If unchecked, the key will be used first." });
                //googleMapsApi.Properties.Add(new Property("gmVersion", "Google Maps Api Version", PropertyType.String, 103, "3") { DescriptionText = "Api version examples: 3, 3.23, 3.exp" });

                //PropertyGroup mapBoxApi = new PropertyGroup("MapBoxApi", "MapBox API", 11);//tab 2 ui
                //mapBoxApi.Properties.Add(new Property("publicaccess_token", "Public access_token", PropertyType.String, 111, "") { DescriptionText = "MapBox access_token = pk_{your token}" });
                //mapBoxApi.Properties.Add(new Property("secretKey", "Secret Key", PropertyType.String, 112, "") { DescriptionText = "MapBox secret key sk_{your token}.  currently not implemented" });
                //mapBoxApi.Properties.Add(new Property("mapBoxVersion", "MapBox API Version", PropertyType.String, 113, "v0.21.0") { DescriptionText = "MapBox API Version, example:v0.21.0" });

                //PropertyGroup arcGisApi = new PropertyGroup("ArcGisApi", "ArcGis API", 12);
                //arcGisApi.Properties.Add(new Property("arcGisVersion", "ArcGis Version", PropertyType.String, 121, "4.0") { DescriptionText = "ArcGIS API Version, example: 4.0 "});

                return new PropertyGroup[] { MapApis };//googleMapsApi, mapBoxApi, arcGisApi };
            }
        }
        public void Update(IPluginConfiguration configuration)
        {
            te.extension.coria.PublicApi.Maps.GoogleMapsApiDefaultUseKeyOrClientId = configuration.GetBool("defaultUseKeyOrClientId");
            te.extension.coria.PublicApi.Maps.GoogleMapsApiClientId = configuration.GetString("publicClientId") ;
            te.extension.coria.PublicApi.Maps.GoogleMapsApiKey = configuration.GetString("publicKey");
            te.extension.coria.PublicApi.Maps.MapBoxApiAccessToken = configuration.GetString("publicaccess_token");
            te.extension.coria.PublicApi.Maps.MapBoxApiSecretAccessToken = configuration.GetString("secretKey");
            te.extension.coria.PublicApi.Maps.MapBoxVersion = configuration.GetString("mapBoxVersion");
            te.extension.coria.PublicApi.Maps.ArcGisVerions = configuration.GetString("arcGisVersion");
            te.extension.coria.PublicApi.Maps.GoogleMapsVersion = configuration.GetString("gmVersion");
            //TODO: configuration values need to be passed in and used somewhere
            //InternalApi.InternalClassName.PropertyKeyName1 = configuration.GetString("propertyKeyName1");
        }
        #endregion
        #region IInstallablePlugin
        public Version Version { get { return GetType().Assembly.GetName().Version; } }
        public void Install(Version lastInstalledVersion)
        {
            #region Install SQL

            #endregion

            #region Install Widgets
          
            #region Coria Map Widgets
           
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_widgetProvider);

            var definitionFilesSources = new string[] {
                "CoriaMap/CoriaMap.xml",
                "CoriaMapBooks/CoriaMapBooks.xml",
                "CoriaMapTitle/CoriaMapTitle.xml",
                "CoriaMapList/CoriaMapList.xml",
                "CoriaMapBookManPanel/CoriaMapBookManPanel.xml"
            };

            foreach (var definitionFile in definitionFilesSources)
            {
                using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.coria.Resources.Widgets." + definitionFile.Replace("/", ".")))
                {
                    UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateDefinitionFile(_widgetProvider, definitionFile.Substring(definitionFile.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);
                }
            }

            var supplementaryFiles = new Dictionary<Guid, string[]>();
            supplementaryFiles[new Guid("9b8fc000-a165-40c1-a0f1-42708cca00b2")] = new string[] {
                "CoriaMap/Supplementary/CoriaMap.js",
                "CoriaMap/Supplementary/CoriaMap.css",
                "CoriaMap/Supplementary/CoriaMap.vm",
            };
            supplementaryFiles[new Guid("887d27c0-deb0-440d-b4c8-7452df690410")] = new string[] {
                "CoriaMapBooks/Supplementary/CoriaMapBooks.js",
                "CoriaMapBooks/Supplementary/CoriaMapBooks.css",
                "CoriaMapBooks/Supplementary/CoriaMapBooks.vm",
            };
            supplementaryFiles[new Guid("61e75699-3e2d-4080-b03d-d9f0b85d7654")] = new string[] {
                "CoriaMapTitle/Supplementary/CoriaMapTitle.js",
                "CoriaMapTitle/Supplementary/CoriaMapTitle.css",
                "CoriaMapTitle/Supplementary/CoriaMapTitle.vm",
            };
            supplementaryFiles[new Guid("2612925e-a9c5-4e8a-b210-2ef685fa1eef")] = new string[] {
                "CoriaMapList/Supplementary/CoriaMapList.js",
                "CoriaMapList/Supplementary/CoriaMapList.css",
                "CoriaMapList/Supplementary/CoriaMapList.vm",
            };
            supplementaryFiles[new Guid("99f22a55-f1f4-4584-8a76-dd0a64452d6b")] = new string[] {
                "CoriaMapBookManPanel/Supplementary/CoriaMapBookManPanel.js",
                "CoriaMapBookManPanel/Supplementary/CoriaMapBookManPanel.css",
                "CoriaMapBookManPanel/Supplementary/CoriaMapBookManPanel.vm",
            };
            //99f22a55-f1f4-4584-8a76-dd0a64452d6b
            foreach (var instanceId in supplementaryFiles.Keys)
            {
                foreach (var relativePath in supplementaryFiles[instanceId])
                {
                    using (var stream = Embr.GetStream("te.extension.coria.Resources.Widgets." + relativePath.Replace("/", ".")))
                    {
                        UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateSupplementaryFile(_widgetProvider, instanceId, relativePath.Substring(relativePath.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);
                    }
                }
            }


            #endregion
            #endregion
            #region Install Pages
            //string pageName = "";
            //int id = 1;
            //bool isCustom = false;
            //Guid themeTypeId = new Guid();
            //Guid themeContextId = new Guid("47bec3c6-b081-4f36-8812-e42953ef133b");
            // ContentFragmentPage page = new ContentFragmentPage(pageName,id,isCustom,themeTypeID, themeContextID);
            //var tabs = page.GetContentFragmentTabs();
            //ContentFragmentTab item = new ContentFragmentTab(page, 1);

            XmlDocument xml;
            foreach (var theme in UIApi.Themes.List(UIApi.ThemeTypes.Group))
            {
                xml = new XmlDocument();
                xml.LoadXml(Embr.GetString("te.extension.coria.Resources.Pages.coria-mapbook-list-Social-Group-Page.xml"));
                UIApi.ThemePages.AddUpdateFactoryDefault(theme, xml.SelectSingleNode("theme/contentFragmentPages/contentFragmentPage"));
                UIApi.ThemePages.DeleteDefault(theme, "coria-mapbooklist", true);

                xml = new XmlDocument();
                xml.LoadXml(Embr.GetString("te.extension.coria.Resources.Pages.coria-mapbook-map-Social-Group-Page.xml"));
                UIApi.ThemePages.AddUpdateFactoryDefault(theme, xml.SelectSingleNode("theme/contentFragmentPages/contentFragmentPage"));
                UIApi.ThemePages.DeleteDefault(theme, "coria-map-page", true);
            }
            #endregion



        }
        public void Uninstall()
        {
            #region Remove Widget Files
           
            //_widgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<CoriaFactoryDefaultWidgetProvider>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_widgetProvider);
            /******/
            #endregion

            #region Remove Page files
            foreach (var theme in UIApi.Themes.List(UIApi.ThemeTypes.Group))
            {
            
                UIApi.ThemePages.DeleteFactoryDefault(theme, "coria-mapbooklist", true);
                UIApi.ThemePages.DeleteDefault(theme, "coria-mapbooklist", true);
                UIApi.ThemePages.Delete(theme, "coria-mapbooklist", true);

                UIApi.ThemePages.DeleteFactoryDefault(theme, "coria-map-page", true);
                UIApi.ThemePages.DeleteDefault(theme, "coria-map-page", true);
                UIApi.ThemePages.Delete(theme, "coria-map-page", true);

            }
            #endregion
        }
        #endregion
        #region IPluginGroup
        public IEnumerable<Type> Plugins
        {
            get
            {
                return new Type[] {
          typeof(te.extension.coria.Plugins.Application.CoriaMapBookType),
          typeof (UI.CoriaFactoryDefaultWidgetProvider),
          typeof (UI.CoriaWidgetContextProvider),
          typeof(UI.WidgetExtension.MapWidgetExtension),
          typeof(UI.WidgetExtension.MapBookWidgetExtension),
          typeof(UI.CoriaManagementPanels.CoriaMapBookPanel),
          typeof(Content.MapContentType),
          typeof(UI.MapBooksGroupNavigation),
          typeof(UI.NewPostLink.MapBookNewBookLink),
          typeof(UI.NewPostLink.MapBookNewMapLink),
          typeof(InternalApi.CoriaPermissions)
          
        };
            }
        }
       
        #endregion
        #endregion


    }
}

