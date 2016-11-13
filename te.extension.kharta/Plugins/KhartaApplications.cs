using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using te.extension.kharta.Plugins.UI;
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
using kharta.coria.graphica;


namespace te.extension.kharta.Plugins
{
    public class KhartaApplications :  IPlugin, IInstallablePlugin, IPluginGroup, IConfigurablePlugin
    {
        private static readonly Version _emptyVersion = new Version(0, 0, 0, 0);
        public static readonly Guid ApplicationsType_id = new Guid("e504f58d-c1d8-40a8-bf55-bc38c65625e9");
       // private KhartaFactoryDefaultWidgetProvider _widgetProvider;
        private UI.AdminPanels.AdminPanel _adminWidgetProvider;
        private KhartaFactoryDefaultWidgetProvider _widgetProvider ;
        private UI.ManagementPanels.KhartaGroupOptionsManPanel _manWidgetProvider;
     
        #region IPlugin
        public string Name { get { return "Kharta Applications"; } }
        public string Description { get { return "A Telligent friendly tools to manage geospatial sources, hosting, and transforming data"; } }
        public Guid DataTypeId { get { return ApplicationsType_id; } }
        public void Initialize() {    }
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
            //if (lastInstalledVersion == null || lastInstalledVersion.Major == 0)


            // if (lastInstalledVersion == null || lastInstalledVersion <= new Version(1, 1))

            #endregion

            #region Install Widgets
       
            #region AdminPanelWidget Install

            _adminWidgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<UI.AdminPanels.AdminPanel>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_adminWidgetProvider);
            var definitionFiles = new string[] {
                "KhartaAdminPanel/KhartaAdminPanel.xml"
            };
            foreach (var definitionFile in definitionFiles) {
                using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.kharta.Resources.Widgets." + definitionFile.Replace("/", ".")))
                {
                    UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateDefinitionFile(_adminWidgetProvider, definitionFile.Substring(definitionFile.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);

                }
            }
            var supplementaryFiles = new Dictionary<Guid, string[]>();
            supplementaryFiles[new Guid("b089b96795074ad1ad130141f62bc937")] = new string[] {
                "KhartaAdminPanel/Supplementary/KhartaAdminPanel.css",
                "KhartaAdminPanel/Supplementary/KhartaAdminPanel.js",
                "KhartaAdminPanel/Supplementary/KhartaAdminPanel.vm",
            };
            foreach (var instanceId in supplementaryFiles.Keys)
            {
                foreach (var relativePath in supplementaryFiles[instanceId])
                {
                    using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.kharta.Resources.Widgets." + relativePath.Replace("/", ".")))
                    {
                        UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateSupplementaryFile(_adminWidgetProvider, instanceId, relativePath.Substring(relativePath.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);
                    }
                }
            }
            #endregion
            #region manPanelWidget Install

            _manWidgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<UI.ManagementPanels.KhartaGroupOptionsManPanel>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_manWidgetProvider);
            var manDefinitionFiles = new string[] {
                "KhataGroupOptionsManPanel/KhataGroupOptionsManPanel.xml"
            };
            foreach (var manDefinitionFile in manDefinitionFiles)
            {
                using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.kharta.Resources.Widgets." + manDefinitionFile.Replace("/", ".")))
                {
                    UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateDefinitionFile(_manWidgetProvider, manDefinitionFile.Substring(manDefinitionFile.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);

                }
            }
            var manSupplementaryFiles = new Dictionary<Guid, string[]>();
            manSupplementaryFiles[new Guid("c23d40789a6d48809ff69da6e868ec8a")] = new string[] {
                "KhataGroupOptionsManPanel/Supplementary/KhataGroupOptionsManPanel.css",
                "KhataGroupOptionsManPanel/Supplementary/KhataGroupOptionsManPanel.js",
                "KhataGroupOptionsManPanel/Supplementary/KhataGroupOptionsManPanel.vm",
            };
            foreach (var manSupFile in manSupplementaryFiles.Keys)
            {
                foreach (var manRelativePath in manSupplementaryFiles[manSupFile])
                {
                    using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.kharta.Resources.Widgets." + manRelativePath.Replace("/", ".")))
                    {
                        UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateSupplementaryFile(_manWidgetProvider, manSupFile, manRelativePath.Substring(manRelativePath.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);
                    }
                }
            }
            #endregion
            #region Kharta Sources Widgets



            _widgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<KhartaFactoryDefaultWidgetProvider>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_widgetProvider);

            var definitionFilesSources = new string[] {
                "KhartaSourceApps/KhartaSourceApps.xml", 
            };

            foreach (var definitionFile in definitionFilesSources)
            {
                using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.kharta.Resources.Widgets." + definitionFile.Replace("/", ".")))
                {
                    UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.AddUpdateDefinitionFile(_widgetProvider, definitionFile.Substring(definitionFile.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1), stream);
                }
            }

            var supplementaryFilesSources = new Dictionary<Guid, string[]>();
            supplementaryFilesSources[new Guid("2165b010beac4805ad6743b61ce41b7f")] = new string[] {
                "KhartaSourceApps/Supplementary/KhartaSourceApps.js",
                "KhartaSourceApps/Supplementary/KhartaSourceApps.css",
                "KhartaSourceApps/Supplementary/KhartaSourceApps.vm",
            };  
            foreach (var instanceId in supplementaryFilesSources.Keys)
            {
                foreach (var relativePath in supplementaryFilesSources[instanceId])
                {
                    using (var stream = InternalApi.Utility.EmbeddedResources.GetStream("te.extension.kharta.Resources.Widgets." + relativePath.Replace("/", ".")))
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
                xml.LoadXml(InternalApi.Utility.EmbeddedResources.GetString("te.extension.kharta.Resources.Pages.kharta-source-appslist-Social-Group-Page.xml"));
                UIApi.ThemePages.AddUpdateFactoryDefault(theme, xml.SelectSingleNode("theme/contentFragmentPages/contentFragmentPage"));
 
                UIApi.ThemePages.DeleteDefault(theme, "kharta-sourceapplist", true);
            }
            #endregion

        }
        public void Uninstall()
        {
            #region Remove Widget Files
            _adminWidgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<UI.AdminPanels.AdminPanel>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_adminWidgetProvider);
            _manWidgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<UI.ManagementPanels.KhartaGroupOptionsManPanel>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_manWidgetProvider);
            _widgetProvider = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<KhartaFactoryDefaultWidgetProvider>().FirstOrDefault();
            UIApi.FactoryDefaultScriptedContentFragmentProviderFiles.DeleteAllFiles(_widgetProvider);
            /******/
            #endregion

            #region Remove Page files
            foreach (var theme in UIApi.Themes.List(UIApi.ThemeTypes.Group))
            {
                UIApi.ThemePages.DeleteFactoryDefault(theme, "kharta-sourceapplist", true);
                UIApi.ThemePages.DeleteDefault(theme, "kharta-sourceapplist", true);
                UIApi.ThemePages.Delete(theme, "kharta-sourceapplist", true);

            }
                #endregion
            }
        #endregion
        #region IPluginGroup
        public IEnumerable<Type> Plugins {  get { return new Type[] {
            typeof(Container.KhartaOntologyType),
            typeof(Application.KhartaOntologyType),
            typeof(Content.KhartaOntologyType),
            typeof(Application.KhartaSourceType),
            typeof (UI.KhartaFactoryDefaultWidgetProvider),
            typeof(UI.KhartaWidgetContextProvider),
            typeof(UI.WidgetExtension.SourceWidgetExtension),
            typeof(UI.SourceNewPostLink),
            typeof(UI.SourceGroupNavigation),
            typeof(UI.AdminPanels.AdminPanel),
            typeof(UI.WidgetExtension.OntologyWidgetExtension),
            typeof(UI.ManagementPanels.KhartaGroupOptionsManPanel)
           
            
        };} }
        #endregion
        #endregion
       

    }
}
