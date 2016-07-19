using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using te.extension.graphica.Plugins.UI;
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

namespace te.extension.graphica.Plugins
{
    public class GraphicaApplications : IPlugin, IInstallablePlugin, IPluginGroup, IConfigurablePlugin //IPlugin, IConfigurablePlugin, IRequiredConfigurationPlugin, IInstallablePlugin, IPluginGroup, IApplicationNavigable, INavigable, ITranslatablePlugin, IPermissionRegistrar, ICategorizedPlugin
    {
        private static readonly Version _emptyVersion = new Version(0, 0, 0, 0);
        public static readonly Guid GraphicaType_id = new Guid("73595265-ef2c-4c33-bb3d-86d54bb7ff05");
        // private KhartaFactoryDefaultWidgetProvider _widgetProvider;
        private GraphicaFactoryDefaultWidgetProvider _widgetProvider;


        #region IPlugin
        public string Name { get { return "Graphica Applications"; } }
        public string Description { get { return "A Telligent friendly tools for creating Lists, Charts & Maps from geospatial sources"; } }
        public Guid DataTypeId { get { return GraphicaType_id; } }
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

            #endregion

            #region Install Widgets

            #region Graphica Map Widgets
            _widgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<GraphicaFactoryDefaultWidgetProvider>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_widgetProvider);

            var definitionFilesSources = new string[] {
                "GraphicaModel/GraphicaModel.xml"
            };

            foreach (var definitionFile in definitionFilesSources)
            {
                using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.graphica.Resources.Widgets." + definitionFile.Replace("/", ".")))
                {
                    UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateDefinitionFile(_widgetProvider, definitionFile.Substring(definitionFile.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);
                }
            }

            var supplementaryFiles = new Dictionary<Guid, string[]>();
            supplementaryFiles[new Guid("efcb5a61-4a03-4e2b-bd47-db2e6ea906e4")] = new string[] {
                "GraphicaModel/Supplementary/GraphicaModel.js",
                "GraphicaModel/Supplementary/GraphicaModel.css",
                "GraphicaModel/Supplementary/GraphicaModel.vm",
            };
            foreach (var instanceId in supplementaryFiles.Keys)
            {
                foreach (var relativePath in supplementaryFiles[instanceId])
                {
                    using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.graphica.Resources.Widgets." + relativePath.Replace("/", ".")))
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

            _widgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<GraphicaFactoryDefaultWidgetProvider>().FirstOrDefault();
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
          typeof(UI.GraphicaFactoryDefaultWidgetProvider),
          typeof(UI.GraphicaWidgetContextProvider)
            //typeof(Application.KhartaOntologyType),
           // typeof(Content.KhartaOntologyType),
           
        };
            }
        }
        #endregion
        #endregion
    }
}
