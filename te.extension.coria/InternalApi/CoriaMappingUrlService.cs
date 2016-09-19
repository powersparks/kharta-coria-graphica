using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Api.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
namespace te.extension.coria.InternalApi
{
    internal class CoriaMappingUrlService
    {
        //coria mapbook url
        internal static string CoriaNewMapBookUrl(int groupId, bool checkPermissions = false)
        {

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var group = TEApi.Groups.Get(new GroupsGetOptions { Id = groupId });
            bool canCreateMapBook = checkPermissions ? InternalApi.CoriaPermissionService.CanCreateMapBook(TEApi.Groups.ContentTypeId, group.ApplicationId) : true;

            if (group != null && group.Id.HasValue && canCreateMapBook)
            {
                Uri groupUri = new Uri(group.Url);
                Uri requestUri = HttpContext.Current.Request.Url;
                IList<PublicApi.MapBook> mapbooks = PublicApi.MapBooks.List(group.Id.Value);
                if (mapbooks == null) { return null; }
                if (mapbooks.Count == 0) { return null; }
                //why am I looping thru the list...umm.. why can't I select from the url, and query for it?
                foreach (PublicApi.MapBook m in mapbooks)
                {
                    if (!requestUri.AbsolutePath.Contains(m.SafeName))
                    {
                        if (requestUri.AbsolutePath.EndsWith("/"))
                        {
                            if (!requestUri.AbsolutePath.EndsWith(groupUri.AbsolutePath) || requestUri.AbsolutePath.EndsWith("/mapbooks/") || !requestUri.AbsolutePath.EndsWith("/" + m.SafeName + "/") || !requestUri.AbsolutePath.EndsWith("/map/") || !requestUri.AbsolutePath.EndsWith("/map/ping/") || !requestUri.AbsolutePath.EndsWith("/map/new/") || !requestUri.AbsolutePath.EndsWith("/map/on/"))
                            {
                                parameters.Add("_cptype", "panel");
                                parameters.Add("_cpcontexttype", "Application");
                                parameters.Add("_cppanelid", Plugins.UI.CoriaManagementPanels.CoriaMapBookPanel._panelId.ToString("N"));

                                break;
                            }
                        }
                        else
                        {
                            if (!requestUri.AbsolutePath.EndsWith(groupUri.AbsolutePath) || requestUri.AbsolutePath.EndsWith("/mapbooks") || !requestUri.AbsolutePath.EndsWith("/" + m.SafeName) || !requestUri.AbsolutePath.EndsWith("/map") || !requestUri.AbsolutePath.EndsWith("/map/ping") || !requestUri.AbsolutePath.EndsWith("/map/new") || !requestUri.AbsolutePath.EndsWith("/map/on"))
                            {

                                parameters.Add("_cptype", "panel");
                                parameters.Add("_cpcontexttype", "Application");
                                parameters.Add("_cppanelid", Plugins.UI.CoriaManagementPanels.CoriaMapBookPanel._panelId.ToString("N"));

                                // url = "#_cptype=panel&_cpcontexttype=Application&_cppanelid=" + Plugins.UI.CoriaManagementPanels.CoriaMapBookPanel._panelId.ToString("N");
                                break;
                            }
                        }
                    }
                }
            }
            else {
                return null;
            }
            return TEApi.Url.ConvertQueryStringToHash(TEApi.Url.BuildUrl("GroupMapBookList", groupId, parameters));
        }
    }
}
