using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using te.extension.coria.Plugins.UI;
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
        public void Initialize() { }
        #region IConfigurablePlugin
        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                PropertyGroup googleMapsApi = new PropertyGroup("GoogleMapsApi", "Google Maps API", 1);//tab 1 ui
                googleMapsApi.Properties.Add(new Property("publicKey", "Public Key", PropertyType.String, 1, "") { DescriptionText = "Google Maps API key is added to the url" });
                googleMapsApi.Properties.Add(new Property("publicClientId", "Public Client Id", PropertyType.String, 1, "") { DescriptionText = "Google Maps business or premium plans offer a client id which is added to the url" });
                googleMapsApi.Properties.Add(new Property("defaultUseKeyOrClientId", "Client Id used by default", PropertyType.Bool, 1, "True") { DescriptionText = "The client Id will be used first if checked. If unchecked, the key will be used first." });
                googleMapsApi.Properties.Add(new Property("gmVersion", "Google Maps Api Version", PropertyType.String, 1, "3") { DescriptionText = "Api version examples: 3, 3.23, 3.exp" });
                PropertyGroup mapBoxApi = new PropertyGroup("MapBoxApi", "MapBox API", 2);//tab 2 ui
                mapBoxApi.Properties.Add(new Property("publicaccess_token", "Public access_token", PropertyType.String, 1, "") { DescriptionText = "MapBox access_token = pk_{your token}" });
                mapBoxApi.Properties.Add(new Property("secretKey", "Secret Key", PropertyType.String, 1, "") { DescriptionText = "MapBox secret key sk_{your token}.  currently not implemented" });
                mapBoxApi.Properties.Add(new Property("mapBoxVersion", "MapBox API Version", PropertyType.String, 1, "v0.21.0") { DescriptionText = "MapBox API Version, example:v0.21.0" });
                PropertyGroup arcGisApi = new PropertyGroup("ArcGisApi", "ArcGis API", 3);
                arcGisApi.Properties.Add(new Property("arcGisVersion", "ArcGis Version", PropertyType.String, 1, "4.0") { DescriptionText = "ArcGIS API Version, example: 4.0 "});
                     
                return new PropertyGroup[] { googleMapsApi, mapBoxApi, arcGisApi };
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
            _widgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<CoriaFactoryDefaultWidgetProvider>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_widgetProvider);

            var definitionFilesSources = new string[] {
                "CoriaMap/CoriaMap.xml"
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
            foreach (var instanceId in supplementaryFiles.Keys)
            {
                foreach (var relativePath in supplementaryFiles[instanceId])
                {
                    using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.coria.Resources.Widgets." + relativePath.Replace("/", ".")))
                    {
                        UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateSupplementaryFile(_widgetProvider, instanceId, relativePath.Substring(relativePath.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);
                    }
                }
            }


            #endregion
            #endregion

          

        }
        public void Uninstall()
        {
            #region Remove Widget Files
           
            _widgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<CoriaFactoryDefaultWidgetProvider>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_widgetProvider);
            /******/
            #endregion

            #region Remove Page files
             
            #endregion
        }
        #endregion
        #region IPluginGroup
        public IEnumerable<Type> Plugins
        {
            get
            {
                return new Type[] {
          typeof(te.extension.coria.Plugins.Application.CoriaMapType),
          typeof (UI.CoriaFactoryDefaultWidgetProvider),
          typeof (UI.CoriaWidgetContextProvider)
        };
            }
        }
        #endregion
        #endregion


    }
}

