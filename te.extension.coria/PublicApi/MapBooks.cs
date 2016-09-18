using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;

namespace te.extension.coria.PublicApi
{
    public class MapBooks
    {
        public static IApplication Get(int id)
        {
            try
            {
              MapBook mapbook = new MapBook(InternalApi.CoriaDataService.GetCoriaMapBookApplication(id));
                         
              return mapbook;
            }
            catch (Exception ex)
            {
                var exception = ex;
                throw null;
            } 
        }
        public static MapBook GetMapBook(int id)
        {
            try
            {
                MapBook mapbook = new MapBook(InternalApi.CoriaDataService.GetCoriaMapBookApplication(id));

                return mapbook;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;//throw ex;//new InternalApi.Utility.CoriaException("Coria", ex.Message);
            }
        }
        public static IApplication Get(Guid mapBookGuId)
        {
            try
            {
                MapBook mapbook = new MapBook(InternalApi.CoriaDataService.GetCoriaMapBookApplication(mapBookGuId));

                return  mapbook;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;
            }
        }

        internal static IList<IApplication> GetMapBookApplicationsByGroup(int groupId)
        {

            try
            {
                //IList<InternalApi.CoriaMapBook> coriaMapBooks = InternalApi.CoriaDataService.GetCoriaMapBooksByGroup(groupId);
                //IList<IApplication> mapbooks = coriaMapBooks.Cast<IApplication>().ToList();
                //or
                IList<MapBook>  mapbooks = InternalApi.CoriaDataService.GetMapBookApplicationsByGroup(groupId);
                return mapbooks.Cast<IApplication>().ToList(); ;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;
            }
        }

        internal static AdditionalInfo Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        public static IList<MapBook> List(int groupId) {
            IList<InternalApi.CoriaMapBook> cMapBooks = InternalApi.CoriaDataService.GetCoriaMapBooksByGroup(groupId);
            if (cMapBooks == null ){ return null; }
            if (cMapBooks.Count == 0) { return null; }
            return new List<MapBook>(cMapBooks.Select(m => new MapBook(m)));
        }
    internal static PagedList<MapBook> List(int groupId, MapBooksListOptions query)
        {
            int pageIndex = query.PageIndex < 1 ? 0 : query.PageIndex;
            int pageSize = query.PageSize >= 10 ? query.PageSize : 10;
            string sortBy = query.SortBy;
            string sortOrder = query.SortOrder;// ? query.SortOrder : Entity.SortOrder.Ascending;
            PagedList<InternalApi.CoriaMapBook> mapbooks = InternalApi.CoriaDataService.CoriaMapBookPagedList(groupId, pageIndex, pageSize,  sortBy,  sortOrder);

             
            return new PagedList<PublicApi.MapBook>(
                mapbooks.Select(s => new MapBook(s)),
                mapbooks.PageIndex,
                mapbooks.PageSize,
                mapbooks.TotalCount);
                 
        }

        internal static MapBook Create(int groupId, string name, string description)
        {
            throw new NotImplementedException();
        }

        internal static MapBook Update(Guid id, string name, string description)
        {
            throw new NotImplementedException();
        }

        internal static bool CanCreate(int groupId)
        {
            throw new NotImplementedException();
        }

        internal static bool CanEdit(Guid mapBookId)
        {
            throw new NotImplementedException();
        }

        internal static bool CanDelete(Guid mapBookId)
        {
            throw new NotImplementedException();
        }

        internal static string UI(Guid mapBookId, bool readOnly, bool showNameAndDescription)
        {
            throw new NotImplementedException();
        }
    }
}
