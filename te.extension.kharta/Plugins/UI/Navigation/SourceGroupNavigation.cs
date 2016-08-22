using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.DynamicConfiguration.Components;

namespace te.extension.kharta.Plugins.UI
{
    
        public class SourceGroupNavigation : IPlugin, ITranslatablePlugin, IGroupCustomNavigationPlugin, IGroupDefaultCustomNavigationPlugin
        {
            private readonly Guid _defaultId = new Guid("f57b5bc1546f45c8a0f260cfef540582");

            ITranslatablePluginController _translation;

            #region IPlugin Members

            public string Name
            {
                get { return "Kharta Source Group Navigation"; }
            }

            public string Description
            {
                get { return "Adds Sources custom navigation support within groups."; }
            }

            public void Initialize()
            {
            }

            #endregion

            #region ITranslatablePlugin Members

            public Translation[] DefaultTranslations
            {
                get
                {
                    var translation = new Translation("en-US");
                    translation.Set("configuration_options", "Options");
                    translation.Set("configuration_label", "Label");
                    translation.Set("configuration_label_description", "Enter an optional label for this link.");
                    translation.Set("configuration_defaultLabel", "Sources");
                    translation.Set("navigationitem_name", "Group Sources");

                    return new Translation[] { translation };
                }
            }

            public void SetController(ITranslatablePluginController controller)
            {
                _translation = controller;
            }

            #endregion

            #region IGroupCustomNavigationPlugin Members

            public PropertyGroup[] GetConfigurationProperties(int groupId)
            {
                PropertyGroup group = new PropertyGroup("options", _translation.GetLanguageResourceValue("configuration_options"), 1);

                group.Properties.Add(new Property("groupid", "", PropertyType.Int, 1, groupId.ToString()) { Visible = false, Editable = false });
                group.Properties.Add(new Property("label", _translation.GetLanguageResourceValue("configuration_label"), PropertyType.String, 2, "") { DescriptionText = _translation.GetLanguageResourceValue("configuration_label_description") });

                return new PropertyGroup[] { group };
            }

            public ICustomNavigationItem GetNavigationItem(Guid id, ICustomNavigationItemConfiguration configuration)
            {
                int groupId = configuration.GetIntValue("groupid", -1);
                if (groupId == -1)
                    return null;

                string label = configuration.GetStringValue("label", "");

                return new SourceGroupNavigationItem(this, configuration, id, groupId, () => string.IsNullOrEmpty(label) ? _translation.GetLanguageResourceValue("configuration_defaultLabel") : label);
            }

            public string NavigationTypeName
            {
                get { return _translation.GetLanguageResourceValue("navigationitem_name"); }
            }

            #endregion

            #region IGroupDefaultCustomNavigationPlugin Members

            public int DefaultOrderNumber
            {
                get { return 200; }
            }

            public ICustomNavigationItem GetDefaultNavigationItem(int groupId, ICustomNavigationItemConfiguration configuration)
            {
                return new SourceGroupNavigationItem(this, configuration, _defaultId, groupId, () => _translation.GetLanguageResourceValue("configuration_defaultLabel"));
            }

            #endregion

            public class SourceGroupNavigationItem : ICustomNavigationItem
            {
                int _groupId;
                Func<string> _getLabel;

                internal SourceGroupNavigationItem(ICustomNavigationPlugin plugin, ICustomNavigationItemConfiguration configuration, Guid id, int groupId, Func<string> getLabel)
                {
                    Plugin = plugin;
                    Configuration = configuration;
                    UniqueID = id;
                    _groupId = groupId;
                    _getLabel = getLabel;
                }

                #region ICustomNavigationItem Members

                public ICustomNavigationItem[] Children { get; set; }
                public ICustomNavigationItemConfiguration Configuration { get; private set; }
                public string CssClass { get { return "kharta"; } }
                public string Label { get { return _getLabel(); } }
                public ICustomNavigationPlugin Plugin { get; private set; }
                public Guid UniqueID { get; private set; }

                public bool IsSelected(string currentFullUrl)
                {
                    return currentFullUrl.Contains("/sources");
                }

                public bool IsVisible(int userID)
                {
                    return !string.IsNullOrEmpty(Url);
                }

                public string Url
                {
                    get
                    {
                    //if (_source != null)
                    //    return _source.Group.Url + "/" + _source.SafeName; // InternalApi.SourceingUrlService.SourceListUrl(_groupId);
                    //if (_groupId > -1)
                    //    return PublicApi.Sources.GetSourcesApplicationsByGroup(_groupId).FirstOrDefault().Container.Url + "/mapbooks";

                    return null;
                }
                }

                #endregion
            }
        }
     
}
