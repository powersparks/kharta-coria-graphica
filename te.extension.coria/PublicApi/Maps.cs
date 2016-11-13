using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;

 
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
 
 
namespace te.extension.coria.PublicApi
{
    public class Maps
    {
        private static readonly Guid _contentTypeId = new Guid("8acb93a7-f580-4c23-95c8-98ad8cf94d45");
        public static Guid ContentTypeId { get { return _contentTypeId; } /**internal set { } **/}

        public static bool GoogleMapsApiDefaultUseKeyOrClientId{ get; set; }
        public static string GoogleMapsApiClientId { get; set; }
        public static string GoogleMapsApiKey { get; set; }
        public static string GoogleMapsVersion { get; set; }
        public static string MapBoxApiAccessToken { get; set; }
        public static string MapBoxApiSecretAccessToken { get; set; }
        public static string MapBoxVersion { get; set; }
        public static string ArcGisVerions { get; set; }
        //private static readonly MapEvents _events = new MapEvents();


        //create or add Map

        //get Map by id

        //get Map by Guid
        public static Map GetMap(Guid mapId)
        {
            //CoriaMap coriaMap = new CoriaMap();
            try {
                InternalApi.CoriaMap map = InternalApi.CoriaDataService.GetCoriaMap(mapId);
                if (map == null)
                    return null;

                return new Map(map);

            } catch (Exception ex) {

                return new Map(new AdditionalInfo(new Error(ex.GetType().FullName, ex.Message)));
            }
   
        }

        internal static Map Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public static PagedList<Map> List(int groupId,string mapbooksafename, MapsListOptions query)
        {
            if (mapbooksafename == null) { return null; }
            int pageIndex = query.PageIndex < 1 ? 0 : query.PageIndex;
            int pageSize = query.PageSize >= 10 ? query.PageSize : 10;
            string sortBy = query.SortBy;
            string sortOrder = query.SortOrder;// ? query.SortOrder : Entity.SortOrder.Ascending;
                                               //new InternalApi.CoriaMapBook(InternalApi.CoriaDataService.(groupId, mapbooksafename, ""));
            InternalApi.CoriaMapBook mapbook = InternalApi.CoriaDataService.GetCoriaMapBookByGroupId_MapBookName(groupId, mapbooksafename);

            PagedList<InternalApi.CoriaMap> map = InternalApi.CoriaDataService.CoriaMapPagedList(mapbook, pageIndex, pageSize, sortBy, sortOrder);


            return new PagedList<PublicApi.Map>(
                map.Select(s => new Map(s)),
                map.PageIndex,
                map.PageSize,
                map.TotalCount);
        }

        internal static AdditionalInfo Delete(Guid id)
        {
            Guid mapId = id;
            try
            {
                var map = InternalApi.CoriaDataService.GetCoriaMap(mapId);
                if (map != null)
                {
                    InternalApi.CoriaDataService.DeleteCoriaMap(map.MapId);
                }

                return new AdditionalInfo();
            }
            catch (Exception ex)
            {
                return new AdditionalInfo(new Error(ex.GetType().FullName, ex.Message));
            }
        }

        internal static Map Create(int groupId, string name, string description)
        {
            Map map = new Map();
            return map;
        }

        internal static Map Update(Guid id, string name, string description)
        {
            throw new NotImplementedException();
        }

        internal static bool CanCreate(int groupId)
        {
            throw new NotImplementedException();
        }

        internal static bool CanEdit(Guid mapId)
        {
            throw new NotImplementedException();
        }

        internal static string GetCreateMapUrl(int groupId, string v1, string v2)
        {
            string url = "";
            return url;
        }

        internal static bool CanDelete(Guid mapId)
        {
            throw new NotImplementedException();
        }

        internal static string UI(Guid mapId, bool readOnly, bool showNameAndDescription)
        {
            throw new NotImplementedException();
        }
        //Update Map
        //Delete Map
    }
}
