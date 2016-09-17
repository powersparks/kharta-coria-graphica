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
        internal static string CoriaMapBookUrl(int groupId, bool checkPermissions = false)
        {
            //        internal static string CreatePollUrl(int groupId, bool checkPermissions)
            string url = null;
            var group = TEApi.Groups.Get(new GroupsGetOptions { Id = groupId });
            if (group == null)
                return null;

            if (checkPermissions && !InternalApi.CoriaPermissionService.CanCreateMapBook(TEApi.Groups.ContentTypeId, group.ApplicationId))
                return null;

            Guid container = group.ApplicationId;
            if (group.Id.HasValue)
            {

             
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
                                url = groupUri + "/mapbooks/" + m.SafeName + "#_cptype=panel&_cpcontexttype=Application&_cppanelid=" + Plugins.UI.CoriaManagementPanels.CoriaMapBookPanel._panelId.ToString("N");
                                //a06a4d37-82d6-42a4-b20c-140ffd882677
                                //url = groupUri + "mapbooks/" + m.SafeName + "/map/new/";
                                break;
                            }
                        }
                        else
                        {
                            if (!requestUri.AbsolutePath.EndsWith(groupUri.AbsolutePath) || requestUri.AbsolutePath.EndsWith("/mapbooks") || requestUri.AbsolutePath.EndsWith("/" + m.SafeName) || !requestUri.AbsolutePath.EndsWith("/map") || requestUri.AbsolutePath.EndsWith("/map/ping") || requestUri.AbsolutePath.EndsWith("/map/new") || requestUri.AbsolutePath.EndsWith("/map/on"))
                            {
                                url = groupUri + "/mapbooks/" + m.SafeName + "#_cptype=panel&_cpcontexttype=Application&_cppanelid=" + Plugins.UI.CoriaManagementPanels.CoriaMapBookPanel._panelId.ToString("N");
                                //url = groupUri + "/mapbooks/" + m.SafeName + "/map/new/";
                                // //http://localhost/ideas/fuzzy/mapbooks/mapbook2#_cptype=panel&_cpcontexttype=Application&_cppanelid=a06a4d37-82d6-42a4-b20c-140ffd882677
                                break;
                            }


                        }
                    }
                }
            }
            var testUrl = TEApi.Url.BuildUrl("GroupMapBookList", groupId, null);
            return url; // TEApi.Url.BuildUrl("mapbook_create", groupId, null);
         
        }
        //coria map url
    }
}
