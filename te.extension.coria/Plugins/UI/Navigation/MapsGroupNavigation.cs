using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.DynamicConfiguration.Components;

namespace te.extension.coria.Plugins.UI
{

    public class MapsGroupNavigation : IPlugin, ITranslatablePlugin,ISiteCustomNavigationPlugin, IGroupCustomNavigationPlugin, IGroupDefaultCustomNavigationPlugin
    {
        private readonly Guid _defaultId = new Guid("9ff37043-8c76-40ce-891e-ffa216415a0c");

        ITranslatablePluginController _translation;

        #region IPlugin Members

        public string Name
        {
            get { return "Coria Map Book Group Navigation"; }
        }

        public string Description
        {
            get { return "Adds Map Book custom navigation support within groups."; }
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
                translation.Set("configuration_defaultLabel", "Maps");
                translation.Set("navigationitem_name", "Group Maps");

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
            var groups = new PropertyGroup[] { 
             new PropertyGroup("options", _translation.GetLanguageResourceValue("configuration_options"), 1)
            };
            if (groupId > -1)
            {

                groups[0].Properties.Add(new Property("groupid", "Current Group", PropertyType.Int, 1, groupId.ToString()) { Visible = false, Editable = false });
                groups[0].Properties.Add(new Property("label", _translation.GetLanguageResourceValue("configuration_label"), PropertyType.String, 1, "") { DescriptionText = _translation.GetLanguageResourceValue("configuration_label_description") });


            }
            else {

                groups[0].Properties.Add(new Property("groupid", "Current Group", PropertyType.Custom, 0, string.Empty) {
                    ControlType = typeof(Telligent.Evolution.Controls.GroupSelectionList),
                    DescriptionText = "Select a Group"
                });
                groups[0].Properties.Add(new Property("label", _translation.GetLanguageResourceValue("configuration_label"), PropertyType.String, 1, "") { DescriptionText = _translation.GetLanguageResourceValue("configuration_label_description") });

            }
            return groups;
        }

        public ICustomNavigationItem GetNavigationItem(Guid id, ICustomNavigationItemConfiguration configuration)
        {
            int groupId = configuration.GetIntValue("groupid", -1);
            if (groupId == -1)
                return null;

            string label = configuration.GetStringValue("label", "");

            return new MapsGroupNavigationItem(this, configuration, id, groupId, () => string.IsNullOrEmpty(label) ? _translation.GetLanguageResourceValue("configuration_defaultLabel") : label);
        }

        public string NavigationTypeName
        {
            get { return _translation.GetLanguageResourceValue("navigationitem_name"); }
        }

        #endregion

        #region IGroupDefaultCustomNavigationPlugin Members

        public int DefaultOrderNumber
        {
            get { return 3; }
        }

        public ICustomNavigationItem GetDefaultNavigationItem(int groupId, ICustomNavigationItemConfiguration configuration)
        {
            return new MapsGroupNavigationItem(this, configuration, _defaultId, groupId, () => _translation.GetLanguageResourceValue("configuration_defaultLabel"));
        }

        public PropertyGroup[] GetConfigurationProperties()
        {
            return GetConfigurationProperties(-1);
        }

        #endregion

        public class MapsGroupNavigationItem : ICustomNavigationItem
        {
            int _groupId = -1;
            Func<string> _getLabel;
            IList<PublicApi.MapBook> _mapbooks;
            internal MapsGroupNavigationItem(ICustomNavigationPlugin plugin, ICustomNavigationItemConfiguration configuration, Guid id, int groupId, Func<string> getLabel)
            {
                Plugin = plugin;
                Configuration = configuration;
                UniqueID = id;
                _mapbooks = PublicApi.MapBooks.List(groupId);
                _groupId = groupId;
                _getLabel = getLabel;
            }

            #region ICustomNavigationItem Members

            public ICustomNavigationItem[] Children { get; set; }
            public ICustomNavigationItemConfiguration Configuration { get; private set; }
            public string CssClass { get { return "mapbook"; } }
            public string Label { get { return _getLabel(); } }
            public ICustomNavigationPlugin Plugin { get; private set; }
            public Guid UniqueID { get; private set; }

            public bool IsSelected(string currentFullUrl)
            {
                return currentFullUrl.Contains("/mapbooks");
            }

            public bool IsVisible(int userID)
            {
                return !string.IsNullOrEmpty(Url);
            }

            public string Url
            {
                get
                {
                    // if (_groupId == -1) { return null; }
                    if ( _mapbooks != null && _mapbooks.Count > 0)
                    {
                        if (_mapbooks.Count == 1)
                        {
                            if (_mapbooks.FirstOrDefault().Group != null && _mapbooks.FirstOrDefault().Group.Url != null)// && _mapbook.SafeName != null)
                            {
                                return _mapbooks.FirstOrDefault().Group.Url + "mapbooks/" + _mapbooks.FirstOrDefault().SafeName; // InternalApi.SourceingUrlService.SourceListUrl(_groupId);
                            }
                        }
                        else
                        {
                            if (_mapbooks.FirstOrDefault().Group != null && _mapbooks.FirstOrDefault().Group.Url != null)// && _mapbook.SafeName != null)
                            {
                                return _mapbooks.FirstOrDefault().Group.Url + "mapbooks/";
                            }
                        }
                    }
                         
                    return null;
                }
            }

            #endregion
        }
    }

}
