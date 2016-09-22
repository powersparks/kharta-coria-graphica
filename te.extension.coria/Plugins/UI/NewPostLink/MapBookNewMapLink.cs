using System;
using System.Collections.Generic;
using System.Web;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;

namespace te.extension.coria.Plugins.UI.NewPostLink
{
    public class MapBookNewMapLink : IPlugin, ITranslatablePlugin, IGroupNewPostLinkPlugin
    {
        ITranslatablePluginController _translation;

        #region IPlugin Members 
        public string Name { get { return "Coria - New Map Link"; } }
        public string Description { get { return "Adds support for creating maps via the \"New\" menu in groups."; } }
        public void Initialize(){}
        #endregion
        #region ITranslatablePlugin Members
        public Translation[] DefaultTranslations
        {
            get
            {
                var translation = new Translation("en-US");
                translation.Set("link_label", "New Map");
                return new Translation[] { translation };
            }
        }
        public void SetController(ITranslatablePluginController controller) { _translation = controller; }
        #endregion
        #region IGroupNewPostLinkPlugin Members
        public IEnumerable<IGroupNewPostLink> GetNewPostLinks(int groupId, int userId)
        {
            var group = TEApi.Groups.Get(new GroupsGetOptions { Id = groupId });
            if (group == null && group.HasErrors()) { return null; }
            Uri groupUri = new Uri(group.Url);
            Uri requestUri = HttpContext.Current.Request.Url;
            string url = null;
            if (TEApi.Url.CurrentContext == null || TEApi.Url.CurrentContext.ApplicationTypeId == null) { return null; }
            //Guid container = group.ApplicationId;
             url = group.Id.HasValue ? InternalApi.CoriaMappingUrlService.CoriaNewMapBookUrl(groupId, false) : null;
            if (string.IsNullOrEmpty(url)) { return null; }
            return new IGroupNewPostLink[] { new GroupNewMapLink(_translation.GetLanguageResourceValue("link_label"), url) };

        }
        public bool HasNewPostLinks(int groupId, int userId) { return InternalApi.CoriaDataService.CanCreateMapBookMap(groupId); }
        #endregion 
        #region Group New Map Link
        public class GroupNewMapLink : IGroupNewPostLink
        {
            internal GroupNewMapLink(string label, string url) { Label = label;   Url = url; }
            #region IGroupNewPostLink Members
            private string _cssClass = "internal-link add-application map";
            public string CssClass
            {
                get { return this._cssClass; }
                set { this._cssClass = value; }
            }
            public string Label { get; private set; }
            public string NewPostTypeName { get { return Label; } }
            public string Url { get; private set; }

            #endregion
        }
        #endregion
    }
}
