using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.DynamicConfiguration.Components;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;

namespace te.extension.coria.Plugins.UI.Navigation
{
    public class MapsGroupNavigationItem : ICustomNavigationItem
    {
        int _groupId;
        Func<PublicApi.MapBook, string> _getAppLabel = (PublicApi.MapBook mapbook) => mapbook.SafeName;
        string _getLabel;
        string _mapItemUrlName;
        PublicApi.Map _map = new PublicApi.Map();
        PublicApi.MapBook _mapbook = new PublicApi.MapBook();
        IList<PublicApi.MapBook> _mapbooks = new List<PublicApi.MapBook>();
        PublicApi.MapBooksListOptions maplistOp = new PublicApi.MapBooksListOptions();
    internal  MapsGroupNavigationItem(ICustomNavigationPlugin plugin, ICustomNavigationItemConfiguration configuration, Guid id, int groupId, string label = "")
        {
            Plugin = plugin;
            Configuration = configuration;
            UniqueID = id;
            _groupId = groupId;
            maplistOp.IncludeSubGroups = false;
            _mapbooks = PublicApi.MapBooks.List(groupId,maplistOp );
            _mapbook = _mapbooks.FirstOrDefault();
            Label = string.IsNullOrEmpty(label)? _getAppLabel(_mapbook): label;
            Plugin = plugin;
            
        }

        #region ICustomNavigationItem Members

        public ICustomNavigationItem[] Children { get; set; }
        public ICustomNavigationItemConfiguration Configuration { get; set; }
        public string CssClass { get; set; }
        public string Label { get; set; }
        public ICustomNavigationPlugin Plugin { get;  set; }
        public Guid UniqueID { get; set; }

        public bool IsSelected(string currentFullUrl)
        {
            return currentFullUrl.Contains(_mapItemUrlName);
        }

        public bool IsVisible(int userID)
        {
            //TODO: userID is used to check permissions
            return !string.IsNullOrEmpty(Url);
        }

        public string Url
        {
            get
            {
                return _mapbook.Group.Url + "/" + _mapbook.SafeName;//InternalApi.PollingUrlService.PollListUrl(_groupId);
            }
            set {

            }
        }
        #endregion

    }
}
