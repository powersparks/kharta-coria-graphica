using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Api.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using te.extension.coria.Plugins.UI.NewPostLink;
using Telligent.Evolution.Extensibility.UI.Version1;

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
            string url = null;
            if (group != null && group.Id.HasValue && canCreateMapBook)
            {
                Uri groupUri = new Uri(group.Url);
                Uri requestUri = HttpContext.Current.Request.Url;
                url = requestUri.AbsolutePath;
                IList<PublicApi.MapBook> mapbooks = PublicApi.MapBooks.List(group.Id.Value);
                if (mapbooks == null) { return null; }
                if (mapbooks.Count == 0) { return null; }
                //why am I looping thru the list...umm.. why can't I select from the url, and query for it?
                if (requestUri.AbsolutePath.EndsWith("/mapbooks/") )
                {
                    url = requestUri.AbsolutePath + "#_cptype=panel&_cpcontexttype=Container&_cppanelid=c4315566-7dcc-46b3-9ab7-7715d05498ad&__cpo=2";
                }
                else
                {
                    url = null;
                }
            }
            else {
                return null;
            }
            //GroupMapBookSingle GroupMapBookList
            return url;//TEApi.Url.ConvertQueryStringToHash(TEApi.Url.BuildUrl("GroupMapBookSingle", groupId, parameters));
        }

       
    }
}
