using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Content.Version1;

namespace te.extension.coria.PublicApi
{
    public class MapBooks
    {
        public static IApplication Get(int id)
        {
            try
            {
              Entity.MapBook mapbook = new Entity.MapBook(InternalApi.CoriaDataService.GetCoriaMapBookApplication(id));
                         
              return mapbook;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;
            } 
        }
        public static IApplication Get(Guid id)
        {
            try
            {
                Entity.MapBook mapbook = new Entity.MapBook(InternalApi.CoriaDataService.GetCoriaMapBookApplication(id));

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
                IList<Entity.MapBook>  mapbooks = InternalApi.CoriaDataService.GetMapBookApplicationsByGroup(groupId);
                return mapbooks.Cast<IApplication>().ToList(); ;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;
            }
        }

         
    }
}
