using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Web;
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;

namespace te.extension.coria.Plugins.UI.Navigation
{
    public class MapBookForumNavigation : IApplicationNavigable
    {
        Guid IApplicationNavigable.ApplicationTypeId
        {
            get { return Apis.Get<IForums>().ApplicationTypeId; }
        }

        void IApplicationNavigable.RegisterUrls(IUrlController controller)
        {
            controller.AddRaw("forum-raw", "{ForumApp}/raw", null, null,
               (a, p) =>
               {
                   var handler = new CustomForumRawHttpHandler();
                   handler.ProcessRequest(a.ApplicationInstance.Context);
               }, new RawDefinitionOptions() { ParseContext = ParseForumContext });
        }

        private void ParseForumContext(PageContext context)
        {
            var appKey = context.GetTokenValue("ForumApp");
            var forumsApi = Apis.Get<IForums>();
            var groupsApi = Apis.Get<IGroups>();

            if (appKey != null)
            {
                var groupItem = context.ContextItems.GetAllContextItems().FirstOrDefault(a => a.ContentTypeId == groupsApi.ContentTypeId);
                if (groupItem != null)
                {
                    /**workaround until **/
                    var forums = forumsApi.List(new ForumsListOptions { GroupId = int.Parse(groupItem.Id), PageSize = 200});
                    var forum = forums.FirstOrDefault(f => f.Url.Contains(appKey.ToString())); // forumsApi.Get(new ForumsGetOptions() { GroupId = int.Parse(groupItem.Id), Key = appKey.ToString() });
                    if (forum != null)
                    {
                        var contextItem = new ContextItem()
                        {
                            TypeName = "Forum",
                            ApplicationId = forum.ApplicationId,
                            ApplicationTypeId = forumsApi.ApplicationTypeId,
                            ContainerId = forum.Group.ApplicationId,
                            ContainerTypeId = groupsApi.ContentTypeId,
                            ContentId = forum.ApplicationId,
                            ContentTypeId = forumsApi.ApplicationTypeId,
                            Id = forum.Id.ToString()
                        };

                        context.ContextItems.Put(contextItem);
                    }
                }
            }
        }

        public string Description
        {
            get { return "Define custom forum raw url"; }
        }

        public void Initialize()
        {
        }

        public string Name
        {
            get { return "Raw Forum Page"; }
        }
    }

    public class CustomForumRawHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var urls = Apis.Get<IUrl>();
            
            var forum =
               urls.CurrentContext.ContextItems.GetAllContextItems()
                    .FirstOrDefault(f => f.ContentTypeId == Apis.Get<IForums>().ContentTypeId);

            if (forum != null)
            {
                var threads = Apis.Get<IForumThreads>().List(new ForumThreadsListOptions() { ForumId = int.Parse(forum.Id) });
               
                foreach (var post in threads)
                    context.Response.Write(post.Excerpt + "<br />");
            }

            context.Response.End();
        }
    }
}
