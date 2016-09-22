using System;
using System.Web;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace te.extension.coria.Plugins.UI.Navigation
{
    public class MapBookMapsSiteNavigation : IPlugin, ITranslatablePlugin, ISiteCustomNavigationPlugin //, IGroupCustomNavigationPlugin, IGroupDefaultCustomNavigationPlugin
    {
        ITranslatablePluginController _translation;
        #region IPlugin
        public string Name { get { return "MapBookMaps"; } }
        public string Description { get { return "Map Book - Maps"; } }
        public void Initialize() { }
        #endregion New Region

        #region ITranslatablePlugin
        public Translation[] DefaultTranslations
        {
            get
            {
                var translation = new Translation("en-US");
                translation.Set("link_label", "Maps");
                return new Translation[] { translation };
            }
        }
        public void SetController(ITranslatablePluginController controller) { _translation = controller; }
        #endregion
        #region ISiteCustomNavigationPlugin
        public string NavigationTypeName { get { return "GroupMapBookMaps"; } }
        public PropertyGroup[] GetConfigurationProperties()
        {
            var pGroups = new PropertyGroup[] { new PropertyGroup("options","Options",0) };
            //properties for Options
            //Group Id
            //MapBook Id
            return pGroups; 
        }
        public ICustomNavigationItem GetNavigationItem(Guid id, ICustomNavigationItemConfiguration configuration) { throw new NotImplementedException(); }
        #endregion

    }
}