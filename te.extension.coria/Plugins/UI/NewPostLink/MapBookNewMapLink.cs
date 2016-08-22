using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.DynamicConfiguration.Components;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using Telligent.Evolution.Extensibility.Api.Version1;

namespace te.extension.coria.Plugins.UI.NewPostLink
{
    public class MapBookNewMapLink : IPlugin, ITranslatablePlugin, IGroupNewPostLinkPlugin
    {
        ITranslatablePluginController _translation;

        #region IPlugin Members

        public string Name
        {
            get { return "Group New Map Link"; }
        }

        public string Description
        {
            get { return "Adds support for creating mapbook via the \"New Map\" menu in groups."; }
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
                translation.Set("link_label", "Add a Map");

                return new Translation[] { translation };
            }
        }

        public void SetController(ITranslatablePluginController controller)
        {
            _translation = controller;
        }

        #endregion

        #region IGroupNewPostLinkPlugin Members

        public IEnumerable<IGroupNewPostLink> GetNewPostLinks(int groupId, int userId)
        {
            string url = null;
            if (TEApi.Url.CurrentContext == null || TEApi.Url.CurrentContext.ApplicationTypeId != null)
                return null;

            var group = TEApi.Groups.Get(new GroupsGetOptions { Id = groupId });
            Guid container = group.ApplicationId;
            if (group.Id.HasValue)
            {
                
                if (group == null && group.HasErrors())
                    return null;
                //            PublicApi.MapBook mapbook = PublicApi.MapBooks.Get(;
                Uri groupUri = new Uri(group.Url);
                Uri requestUri = HttpContext.Current.Request.Url;
                IList<PublicApi.MapBook> mapbooks = PublicApi.MapBooks.List(group.Id.Value);
                if (mapbooks.Count == 0) { return null; }
                foreach (PublicApi.MapBook m in mapbooks)
                {
                    if (requestUri.AbsolutePath.Contains(m.SafeName))
                    {
                        if (requestUri.AbsolutePath.EndsWith("/"))
                        {
                            if (!requestUri.AbsolutePath.EndsWith(groupUri.AbsolutePath) || requestUri.AbsolutePath.EndsWith("/mapbooks/") || requestUri.AbsolutePath.EndsWith("/" + m.SafeName + "/") || !requestUri.AbsolutePath.EndsWith("/map/") || requestUri.AbsolutePath.EndsWith("/map/ping/") || requestUri.AbsolutePath.EndsWith("/map/new/") || requestUri.AbsolutePath.EndsWith("/map/on/"))
                            {
                                 url = "mapbooks#_cptype=panel&_cpcontexttype=Application&_cppanelid=" + UI.CoriaManagementPanels.CoriaMapBookPanel._panelId.ToString("N");
                                //a06a4d37-82d6-42a4-b20c-140ffd882677
                                //url = groupUri + "mapbooks/" + m.SafeName + "/map/new/";
                                break;
                            }
                        }
                        else
                        {
                            if (!requestUri.AbsolutePath.EndsWith(groupUri.AbsolutePath) || requestUri.AbsolutePath.EndsWith("/mapbooks") || requestUri.AbsolutePath.EndsWith("/" + m.SafeName) || !requestUri.AbsolutePath.EndsWith("/map") || requestUri.AbsolutePath.EndsWith("/map/ping") || requestUri.AbsolutePath.EndsWith("/map/new") || requestUri.AbsolutePath.EndsWith("/map/on"))
                            {
                                url = "mapbooks#_cptype=panel&_cpcontexttype=Application&_cppanelid=" + UI.CoriaManagementPanels.CoriaMapBookPanel._panelId.ToString("N");
                                //url = groupUri + "/mapbooks/" + m.SafeName + "/map/new/";
                                break;
                            }
                         

                        }
                    }
                }
            }
            //groupUri.AbsolutePath +
            // InternalApi.CoriaDataService.CreateMapUrl(groupId, true);

            if (string.IsNullOrEmpty(url))
                return null;

            return new IGroupNewPostLink[] { new GroupNewMapLink(_translation.GetLanguageResourceValue("link_label"), url) };
        }

        public bool HasNewPostLinks(int groupId, int userId)
        {
            var links = GetNewPostLinks(groupId, userId);
            return links == null ? false : links.Any();
        }

        #endregion

        public class GroupNewMapLink : IGroupNewPostLink
        {
            internal GroupNewMapLink(string label, string url)
            {
                Label = label;
                Url = url;
            }

            #region IGroupNewPostLink Members

            public string CssClass
            {
                get { return "internal-link add-post map"; }
            }

            public string Label { get; private set; }

            public string NewPostTypeName
            {
                get { return Label; }
            }

            public string Url { get; private set; }

            #endregion
        }
    }
}
