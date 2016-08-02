using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.DynamicConfiguration.Components;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using Telligent.Evolution.Extensibility.Api.Version1;

namespace te.extension.kharta.Plugins.UI
{
    public class SourceNewPostLink : IPlugin, ITranslatablePlugin, IGroupNewPostLinkPlugin
    {
        ITranslatablePluginController _translation;
        #region IPlugin Members

        public string Name
        {
            get { return "Add Source"; }
        }

        public string Description
        {
            get { return "Adds support for creating  via the \"New Post\" menu in groups."; }
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
                translation.Set("link_label", "Add Source");

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
            if (TEApi.Url.CurrentContext == null || TEApi.Url.CurrentContext.ApplicationTypeId != null)
                return null;

            var group = TEApi.Groups.Get(new GroupsGetOptions { Id = groupId });
            if (group == null && group.HasErrors())
                return null;

            var url = PublicApi.Sources.GetCreateSourceUrl(groupId, "New Source", "source"); //.SourceDataService.CreateNewSourceApplicationObject(groupId, true);
            if (string.IsNullOrEmpty(url))
                return null;

            return new IGroupNewPostLink[] { new GroupNewSourceLink(_translation.GetLanguageResourceValue("link_label"), url) };
        }

        public bool HasNewPostLinks(int groupId, int userId)
        {
            var links = GetNewPostLinks(groupId, userId);
            return links == null ? false : links.Any();
        }

        #endregion

        #region Group New Map Link
        public class GroupNewSourceLink : IGroupNewPostLink
        {
            internal GroupNewSourceLink(string label, string url)
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
        #endregion
    }
}
