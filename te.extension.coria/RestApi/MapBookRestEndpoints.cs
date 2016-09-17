using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Rest.Version2;
using Telligent.Evolution.Extensibility.Version1;
 

namespace te.extension.coria.RestApi
{
    public class MapBookRestEndpoints : IPlugin, IRestEndpoints
    {
        public string Name { get { return "Map Book REST Endpoints"; } }
        public string Description { get { return "Map Book REST API for map data services"; } }
        public void Initialize() {  }
        /*****
             * Method: GET
             * Action: coria applications
             * Rest Endpoints:
             *************** if id's are guids,
             * api.ashx/v2/coria.{ext}                                                                           // coria applications
             * api.ashx/v2/coria/{containerId}.{ext}                                                             // container's application types
             * api.ashx/v2/coria/{containerId}/{applicationTypeId}.ext                                           // applications 
             * api.ashx/v2/coria/{containerId}/{applicationTypeId}/{applicationId}.ext                           // applicationId details
             * api.ashx/v2/coria/{containerId}/{applicationTypeId}/{applicationId}/{contentType}.ext             // content types
             * api.ashx/v2/coria/{containerId}/{applicationTypeId}/{applicationId}/{contentType}/{contentId}.ext // contents
             ************ mapbooks for container
             * api.ashx/v2/coria/mapbooks.{ext}                               // mapbooks, site-wide
             * api.ashx/v2/coria/mapbooks/users/{userId}.{ext}                // mapbooks, user's mapbooks
             * api.ashx/v2/coria/mapbooks/users/{userId}/{mapbookId}.{ext}    // mapbook, user's mapbook 
             * api.ashx/v2/coria/mapbooks/groups/{groupId}.{ext}              // mapbooks, group's mapbooks
             * api.ashx/v2/coria/mapbooks/groups/{groupId}/{mapbookId}.{ext}  // mapbook group's mapbook 
             * ********** maps for mapbook
             * api.ashx/v2/coria/mapbooks/{mapbookId}.{ext}                   // mapbook
             * api.ashx/v2/coria/mapbooks/{mapbookId}/maps.{ext}              // mapbook maps
             * api.ashx/v2/coria/mapbooks/{mapbookId}/{mapId}.{ext}           // mapbook map 
             ************ layers for map
             * api.ashx/v2/coria/mapbooks/map/{mapId}.{ext}                   // mapbook 
             * api.ashx/v2/coria/mapbooks/map/{mapId}/layers.{ext}            // mapbook map layers 
             * api.ashx/v2/coria/mapbooks/map/{mapId}/layer/{layerId}.{ext}   // mapbook map layer
             *******/
        public void Register(IRestEndpointController restRoutes)
        {
            object parameterList = new { resource = "CoriaMapBook", action = "List" };
            object parameterCreate = new { resource = "CoriaMapBook", action = "Create" };
            object parameterConstraints = new { };
            int version = 2;
            HttpMethod postMethod = HttpMethod.Post;
            HttpMethod getMethod = HttpMethod.Get;
            HttpMethod putMethod = HttpMethod.Put;
            HttpMethod deleteMethod = HttpMethod.Delete;

            #region coria/mapbooks/groups/{groupId}
            Func<IRestRequest, IRestResponse> ListGroupMapBooksResponse = (IRestRequest request) => ListGroupMapBooksRequestResponseFunc(request);
            Func<IRestRequest, IRestResponse> CreateGroupMapBookResponse = (IRestRequest request) => CreateGroupMapBookRequestResponseFunc(request);

            /*****
             * Rest Endpoints: 
             *   api.ashx/v2/coria/mapbooks/groups/{groupId}.{ext} 
             ********/
            restRoutes.Add(version, "coria/mapbooks/groups/{groupId}", parameterList,   parameterConstraints, getMethod , ListGroupMapBooksResponse);
            restRoutes.Add(version, "coria/mapbooks/groups/{groupId}", parameterCreate, parameterConstraints, postMethod, CreateGroupMapBookResponse);
            #endregion
            #region
            /*****
             * Rest Endpoints: 
             *   api.ashx/v2/coria/mapbooks/users/{userId}.{ext} 
             *   Func<CoriaMapBook, MapBook> toMapBook = (CoriaMapBook fromCoriaMapBook) =>  ToMapBook(fromCoriaMapBook, new MapBook()); 
             ********/
            // restRoutes.Add(version, "coria/mapbooks/users/{userId}.{ext}", parameterList,   parameterConstraints, getMethod, (IRestRequest request) => ListUserMapBooksRequestResponseFunc(request);
            // restRoutes.Add(version, "coria/mapbooks/users/{userId}.{ext}", parameterCreate, parameterConstraints, getMethod, (IRestRequest request) => ListUserMapBooksRequestResponseFunc(request);
            #endregion

        }

        private IRestResponse CreateGroupMapBookRequestResponseFunc(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        private IRestResponse ListGroupMapBooksRequestResponseFunc(IRestRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
