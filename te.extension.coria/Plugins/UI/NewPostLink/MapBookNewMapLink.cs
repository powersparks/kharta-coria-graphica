using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
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
        public  IGroupNewPostLink[] CoriaNewMapBookLabelUrl(int groupId)
        {  
            string linkPrefix = _translation.GetLanguageResourceValue("link_label");
            IList< GroupNewMapLink> groupNewPostLinks = new List<GroupNewMapLink>();// groupNewPostLinks = ;// new IGroupNewPostLink[] { };//{ groupNewMapLink};
            IList<PublicApi.MapBook> mapbooks = PublicApi.MapBooks.List(groupId);
            
            if (mapbooks != null && mapbooks.Count > 0)
            {    
                foreach (PublicApi.MapBook m in mapbooks)
                {
                    Uri uri = new Uri( m.Group.Url);
                    Uri requestedUri = HttpContext.Current.Request.Url;
                    string absPath = requestedUri.AbsolutePath;

                    GroupNewMapLink groupNewMapLinkItem = new GroupNewMapLink(_translation.GetLanguageResourceValue("link_label"), "");

                    groupNewMapLinkItem.Label = linkPrefix + " to " + m.SafeName;
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("mapBook", m.SafeName.ToString());
                    parameters.Add("_cptype", "panel");
                    parameters.Add("_cpcontexttype", "Application");
                    parameters.Add("_cppanelid", Plugins.UI.ManagementPanels.CoriaMapBookPanel._panelId.ToString("N"));

                    groupNewMapLinkItem.Url = TEApi.Url.ConvertQueryStringToHash( TEApi.Url.BuildUrl("GroupMapBookSingle", groupId, parameters)); 
                   
                    if(requestedUri.AbsolutePath.EndsWith("/mapbooks/"+ m.SafeName) )
                    {
                        IList<GroupNewMapLink> singleGroupNewPostLinks = new List<GroupNewMapLink>();
                        singleGroupNewPostLinks.Add(groupNewMapLinkItem);
                        return singleGroupNewPostLinks.ToArray();
                    }
                  
                  groupNewPostLinks.Add(groupNewMapLinkItem);
                }
                // groupNewPostLinks.Add();

            }
            return groupNewPostLinks.ToArray();
        }
        public IEnumerable<IGroupNewPostLink> GetNewPostLinks(int groupId, int userId)
        {
            var group = TEApi.Groups.Get(new GroupsGetOptions { Id = groupId });
            if (group == null && group.HasErrors()) { return null; }
            Uri groupUri = new Uri(group.Url);
            Uri requestUri = HttpContext.Current.Request.Url; 
            string url = null;
            if (TEApi.Url.CurrentContext == null || TEApi.Url.CurrentContext.ApplicationTypeId == null) { return null; }
            //Guid container = group.ApplicationId; 
            GroupNewMapLink groupNewMapLink = new GroupNewMapLink("","");
            IGroupNewPostLink[] groupNewPostLink = group.Id.HasValue ?  CoriaNewMapBookLabelUrl(groupId) : null; ;
             //url = group.Id.HasValue ? InternalApi.CoriaMappingUrlService.CoriaNewMapBookUrl(groupId, false) : null;
            /***
            if (string.IsNullOrEmpty(url)) { return null; }
            var labelUrl =  new GroupNewMapLink(_translation.GetLanguageResourceValue("link_label"), url);
            var lab = labelUrl.Label;
            labelUrl.Label = lab;
            **/
            return groupNewPostLink;

        }
        public bool HasNewPostLinks(int groupId, int userId) {
            var group = TEApi.Groups.Get(new GroupsGetOptions { Id = groupId });
            if (group == null && group.HasErrors()) { return false; }
             
            Uri requestUri = HttpContext.Current.Request.Url;
            if (requestUri.AbsolutePath.Contains("/mapbooks/") && InternalApi.CoriaDataService.CanCreateMapBookMap(groupId)) { return true; }
            return InternalApi.CoriaDataService.CanCreateMapBookMap(groupId);
        }
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
            public string Label { get; set; }
            public string NewPostTypeName { get { return Label; } }
            public string Url { get;  set; }

            #endregion
        }
        #endregion
    }

}
