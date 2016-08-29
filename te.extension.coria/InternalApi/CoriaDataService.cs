using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kharta.coria.graphica.Models;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;

using System.Reflection;
using Telligent.Evolution.Extensibility.Content.Version1;
 
using Telligent.Evolution.Extensibility.Api.Entities.Version1;

[assembly: InternalsVisibleTo("kharta.coria.graphica.test")]
namespace te.extension.coria.InternalApi
{
    [Serializable]
    internal class CoriaDataService
    { 
        
        internal static  CoriaMapBook CreateUpdateMapBook(CoriaMapBook coriaMapBook )
        {
            if (coriaMapBook.Id == 0)
            {
                coriaMapBook = CreateNewMapBookApplication(coriaMapBook);
            }
            else
            { 
                Func<CoriaMapBook, MapBook> toMapBook = (CoriaMapBook fromCoriaMapBook) =>  ToMapBook(fromCoriaMapBook, new MapBook());
                Func<MapBook, CoriaMapBook> toCoriaMapBook = (MapBook fromMapBook) => ToCoriaMapBook(fromMapBook, new CoriaMapBook());
                 
                using (KhartaDataModel dbcontext = new KhartaDataModel())
                {
                    try
                    {
                        dbcontext.Entry(toMapBook(coriaMapBook)).State = System.Data.Entity.EntityState.Modified;
                        dbcontext.SaveChanges();

                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        //TODO: handle exception and log it

                        var exception = ex;
                    }

                }
                coriaMapBook = GetCoriaMapBookApplication(coriaMapBook.Id);
            }
                return coriaMapBook;
        }

        internal static IList<PublicApi.MapBook> GetMapBookApplicationsByGroup(int groupId)
        {
            IList<PublicApi.MapBook> mapbookApps = new List<PublicApi.MapBook>();
            IList<CoriaMapBook> coriaMapBooks = GetCoriaMapBooksByGroup(groupId);
            //mapbookApps = coriaMapBooks.Cast<PublicApi.Entity.MapBook>().ToList();
            foreach(CoriaMapBook cmb in coriaMapBooks)
            {
                mapbookApps.Add(new PublicApi.MapBook(cmb));
            }
            return mapbookApps;// mapbookApps.Cast<IApplication>().ToList();
        }

        internal static PublicApi.MapBook GetMapBookByGroupId_Name(int groupId, string mapBookName, string mapBookName2)
        {
            mapBookName = mapBookName2 != null ? mapBookName2 : mapBookName;
              PublicApi.MapBook mapbook = new PublicApi.MapBook(GetCoriaMapBookByGroupId_MapBookName(groupId, mapBookName));
            return mapbook;
             
        }

        internal static PagedList<CoriaMapBook> CoriaMapBookPagedList(int groupId, int pageIndex, int pageSize, string sortBy, string sortOrder)
        {
            Func<MapBook, CoriaMapBook> toCoriaMapBook = (MapBook fromMapBook) => ToCoriaMapBook(fromMapBook, new CoriaMapBook());
             
            PagedList<CoriaMapBook> listMapBooks = new PagedList<CoriaMapBook>();
            PagedList<MapBook> MapBookList = MapBookPagedList(groupId, pageIndex, pageSize, sortBy, sortOrder);
            int totalCount = MapBookList.Count();
            return new PagedList<CoriaMapBook>(
                     MapBookList.Select(n => toCoriaMapBook(n) ),
                     pageSize,
                     pageIndex,
                     totalCount);
        }
        internal static PagedList<MapBook> MapBookPagedList(int groupId, int pageIndex, int pageSize, string  sortBy, string sortOrder)
        {
            using (KhartaDataModel dbcontext = new KhartaDataModel())
            {
                int skip = pageIndex * pageSize;
                IList<MapBook> mapbooks =   new List<MapBook>();
                //switch (sortBy){
                //    case PublicApi.Entity.SortBy.Id:
                //        break;
                //    case PublicApi.Entity.SortBy.CreatedDateTimeUtc:
                //        break;
                //    case PublicApi.Entity.SortBy.ModifiedDateTimeUtc:
                //        break;
                //    case PublicApi.Entity.SortBy.Title:
                //        break;
                //    default:  break; }
                          mapbooks = (from m in dbcontext.MapBooks
                                    where m.GroupId.Equals(groupId)
                                    orderby m.Id ascending
                                    select m).Skip(skip).Take(pageSize).ToList();




                int totalCount = mapbooks.Count();
                return new PagedList<MapBook>(mapbooks , pageSize, pageIndex, totalCount);
            }
               

        }

        /****
             * IEnumerable<apan_CommunityMap> communityMaps = (from m in context.apan_CommunityMap
                                                                where m.GroupId == groupId
                                                                select m).ToList();

                int totalCount = communityMaps.Count();
                switch (sortBy)
                {
                    case SortBy.Title:
                        if (sortOrder == SortOrder.Descending)
                            communityMaps = communityMaps.OrderByDescending(m => m.Title).Skip(pageIndex * pageSize).Take(pageSize);
                        else
                            communityMaps = communityMaps.OrderBy(m => m.Title).Skip(pageIndex * pageSize).Take(pageSize);
                        break;
                    case SortBy.CreatedDateTimeUtc:
                        if (sortOrder == SortOrder.Descending)
                            communityMaps = communityMaps.OrderByDescending(m => m.CreatedDateTimeUtc).Skip(pageIndex * pageSize).Take(pageSize);
                        else
                            communityMaps = communityMaps.OrderBy(m => m.CreatedDateTimeUtc).Skip(pageIndex * pageSize).Take(pageSize);
                        break;
                    case SortBy.ModifiedDateTimeUtc:
                    default:
                        if (sortOrder == SortOrder.Descending)
                            communityMaps = communityMaps.OrderByDescending(m => m.ModifiedDateTimeUtc).Skip(pageIndex * pageSize).Take(pageSize);
                        else
                            communityMaps = communityMaps.OrderBy(m => m.ModifiedDateTimeUtc).Skip(pageIndex * pageSize).Take(pageSize);
                        break;
                }

                return new PagedList<Map>(
                    communityMaps.Select(n => new Map { Id = n.Id, Title = n.Title, Description = n.Description, GroupId = n.GroupId, CreatedByUserId = n.CreatedByUserId, CreatedDateTimeUtc = n.CreatedDateTimeUtc, ModifiedByUserId = n.ModifiedByUserId, ModifiedDateTimeUtc = n.ModifiedDateTimeUtc }),
                    pageSize,
                    pageIndex,
                    totalCount);
             */


        private static CoriaMapBook GetCoriaMapBookByGroupId_MapBookName(int groupId, string mapBookName)
        {
            Func<MapBook, CoriaMapBook> toCoriaMapBook = (MapBook fromMapBook) => ToCoriaMapBook(fromMapBook, new CoriaMapBook());
            MapBook mapbook = GetMapBookByGroupId_MapBookName(groupId, mapBookName);

            return toCoriaMapBook(mapbook);
        }

        private static MapBook GetMapBookByGroupId_MapBookName(int groupId, string mapBookName)
        {
            using (KhartaDataModel dbcontext = new KhartaDataModel())
            {
                MapBook mapbook = (from m in dbcontext.MapBooks
                              where m.GroupId.Equals(groupId)
                              && m.SafeName.Equals(mapBookName)
                              select m).FirstOrDefault();

                return mapbook;
            }
        }

        internal static IList<CoriaMapBook> GetCoriaMapBooksByGroup(int groupId)
        {
            Func<MapBook, CoriaMapBook> toCoriaMapBook = (MapBook fromMapBook) => ToCoriaMapBook(fromMapBook, new CoriaMapBook());
       
            IList<MapBook> mapbooks = GetMapBooksByGroup(groupId);
            if (mapbooks.Count == 0) { return null; }
            IList<CoriaMapBook> coriaMapBooks = new List<CoriaMapBook>();
            foreach (MapBook mapbook in mapbooks)
            {
                coriaMapBooks.Add(toCoriaMapBook(mapbook));
            }
            return coriaMapBooks;
        }
        internal static IList<MapBook> GetMapBooksByGroup(int groupId)
        {
            IList<MapBook> mapbooks = new List<MapBook>();
            try
            {
                using (KhartaDataModel dbcontext = new KhartaDataModel())
                {
                  mapbooks = (from m in dbcontext.MapBooks
                                            where m.GroupId.Equals(groupId)
                                            select m).ToList();
                    return mapbooks;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        internal static CoriaMap GetCoriaMap(Guid mapId)
        {
            Func<Map, CoriaMap> toCoriaMap = (Map fromMap) => ToCoriaMap(fromMap, new CoriaMap());
            return toCoriaMap(GetMap(mapId));
        }
        internal static CoriaMap GetCoriaMap(int id)
        {
            Func<Map, CoriaMap> toCoriaMap = (Map fromMap) => ToCoriaMap(fromMap, new CoriaMap());
            return toCoriaMap(GetMap(id));
        }
        internal static CoriaMap ToCoriaMap(Map fromMap, CoriaMap toCoriaMap) { return (CoriaMap)ConvertFromPropertiesTo(fromMap, toCoriaMap); }

        private static Map GetMap(int id)
        {
            try
            {
                using (KhartaDataModel dbcontext = new KhartaDataModel())
                {
                    return (from m in dbcontext.Maps
                            where m.Id.Equals(id)
                            select m).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                //TODO: log exceptions in community 
                throw;
            }


        }
        private static Map GetMap(Guid id)
        {
            try
            {
                using (KhartaDataModel dbcontext = new KhartaDataModel())
                {
                    return (from m in dbcontext.Maps
                            where m.MapId.Equals(id)
                            select m).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                //TODO: log exceptions in community 
                throw;
            }


        }
        internal static CoriaMapBook GetCoriaMapBookApplication(int id)
        {
            Func<MapBook, CoriaMapBook> toCoriaMapBook = (MapBook fromMapBook) => ToCoriaMapBook(fromMapBook, new CoriaMapBook());
            return  toCoriaMapBook(GetMapBook(id));
        }
        internal static CoriaMapBook GetCoriaMapBookApplication(Guid id)
        {
            Func<MapBook, CoriaMapBook> toCoriaMapBook = (MapBook fromMapBook) => ToCoriaMapBook(fromMapBook, new CoriaMapBook());
            return toCoriaMapBook(GetMapBook(id));
        }

        private static MapBook GetMapBook(int id)
        {
            try
            {
                 using (KhartaDataModel dbcontext = new KhartaDataModel())
                {
                    return (from m in dbcontext.MapBooks
                            where m.Id.Equals(id)
                            select m).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                //TODO: log exceptions in community 
                throw;
            }
           
             
        }
        private static MapBook GetMapBook(Guid id)
        {
            try
            {
                using (KhartaDataModel dbcontext = new KhartaDataModel())
                {
                    return (from m in dbcontext.MapBooks
                            where m.ApplicationId.Equals(id)
                            select m).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                //TODO: log exceptions in community 
                throw;
            }


        }

        private static object ConvertFromPropertiesTo(object fromObject, object toObject)
        {
            if (object.ReferenceEquals(null,fromObject) ){ return null; }
            //usage: (castToClass)ConvertFromPropertiesTo(fromObject, toObject)
            var inputObject = fromObject;
            var outputObject = toObject;
            //1) define the "from class" or input type to the "to class" or the output type
            Type input = inputObject.GetType();//fromMapBook.GetType();
            Type output = outputObject.GetType(); // toCoriaMapBook.GetType();

            //1) get the list of output properties
            IList<PropertyInfo> outProperties = new List<PropertyInfo>(output.GetProperties());
            //2) and the list of the input properties
            IList<PropertyInfo> inProperties = new List<PropertyInfo>(input.GetProperties());
            //3) loop thru each output property to update
            foreach (PropertyInfo outProp in outProperties)
            {
                //4) confirm that you can write to the output field
                if (outProp.CanWrite)
                {
                    //5)try to get the intput property with the same name and type
                    try
                    {
                        var inputValue = inputObject.GetType()
                                       .GetProperty(outProp.Name, outProp.PropertyType)
                                       .GetValue(inputObject);
                        //6) set the output property for the class
                        outProp.SetValue(outputObject, inputValue);
                    }
                    catch (ArgumentNullException e) { var nullExcept = e; } //inprop is null for some reason
                    catch (AmbiguousMatchException e) { var moreThan1e = e; }//multiple matches
                    catch (NullReferenceException e) { var nullResult = e; } //either name not found or the property didn't match

                } //end of if

            } //end of loop
            //7) return the result
            return outputObject;
        }
        internal static CoriaMapBook ToCoriaMapBook(MapBook fromMapBook, CoriaMapBook toCoriaMapBook){ return (CoriaMapBook)ConvertFromPropertiesTo(fromMapBook, toCoriaMapBook); }

        /// <summary>   Converts a fromCoriaMapBook to a map book. 
        ///             ToMapBook loops thru each of it's properties and finds 
        ///             the CoriaMapBook derived property. CoriaMapBook, as a child of MapBook,
        ///             inherits properties. 
        ///             </summary>
        ///
        /// <remarks>   Admin, 8/6/2016. Advantage: frequent updates to the data model are expected.
        ///              Limitation: assumes property names are the same, also doesn't check for relationships.
        ///              TODO: deprecate when the data model is stable
        ///              TODO: create an entity that gets and sets the required properties 
        ///              </remarks>
        ///
        /// <param name="fromCoriaMapBook"> from coria map book. </param>
        ///
        /// <returns>   fromCoriaMapBook as a MapBook. </returns>

        internal static MapBook ToMapBook(CoriaMapBook fromCoriaMapBook, MapBook toMapBook){ return (MapBook)ConvertFromPropertiesTo(fromCoriaMapBook, toMapBook); }

        /// <summary>   Creates new map book application. </summary>
        ///
        /// <remarks>   Admin, 8/6/2016. </remarks>
        ///
        /// <param name="coriaMapBook"> The coria map book. </param>
        ///
        /// <returns>   The new new map book application. </returns>

        internal static CoriaMapBook CreateNewMapBookApplication(CoriaMapBook coriaMapBook)
        {
            Func<CoriaMapBook, MapBook> toMapBook = (CoriaMapBook fromCoriaMapBook) => ToMapBook(fromCoriaMapBook, new MapBook());
            Func<MapBook, CoriaMapBook> toCoriaMapBook = (MapBook fromMapBook) => ToCoriaMapBook(fromMapBook, new CoriaMapBook());
            try
            {
                using (KhartaDataModel dbcontext = new KhartaDataModel())
                {

                    MapBook mapbook = dbcontext.MapBooks.Add(toMapBook(coriaMapBook));
                    dbcontext.SaveChanges();
                    return toCoriaMapBook(mapbook);
                }

            }
            catch (Exception ex)
            {
                //TODO: log exception
                var exception = ex;
                throw;
            }
         
        }

        internal static void DeleteCoriaMapBookApplication(CoriaMapBook coriamapbook)
        {
            using (KhartaDataModel dbcontext = new KhartaDataModel())
            {
                var result = (from m in dbcontext.MapBooks
                              where m.ApplicationId.Equals(coriamapbook.Id)
                              select m).FirstOrDefault();
                MapBook mapbook = result;
                dbcontext.MapBooks.Remove(mapbook);
                dbcontext.SaveChanges();

            }

        }
        internal static void DeleteCoriaMapBookApplication(Guid id)
        {
            using (KhartaDataModel dbcontext = new KhartaDataModel())
            {
                var result = (from m in dbcontext.MapBooks
                              where m.ApplicationId.Equals(id)
                              select m).FirstOrDefault();
                MapBook mapbook = result;
                dbcontext.MapBooks.Remove(mapbook);
                dbcontext.SaveChanges();

            }

        }
        /*****
internal static KhartaSource AddUpdateSourceApplication(KhartaSource khartaSource)
{
   if (khartaSource.Id == 0)
   {
       khartaSource = CreateNewSourceApplication(khartaSource);
   }
   else
   {
       Func<KhartaSource, Source> toSource = (KhartaSource fromKhartaSource) => ToSource(fromKhartaSource);
       Func<Source, KhartaSource> toKhartaSource = (Source fromSource) => ToKhartaSource(fromSource);
       Source _updateSource = new Source();
       _updateSource = toSource(khartaSource);
       using (KhartaDataModel dbcontext = new KhartaDataModel())
       {
           try
           {
               dbcontext.Entry(_updateSource).State = System.Data.Entity.EntityState.Modified;

               dbcontext.SaveChanges();
           }
           catch (DbUpdateConcurrencyException ex)
           {
               //ex contains the message related to the entity was delete or updated external
               // unit test deletes the record
               //TODO: handle the condition   
               var exception = ex;

           }

       }
       khartaSource = GetSourceApplication(_updateSource.Id);
   }

   return khartaSource;
}
***/
        //private static readonly List<CoriaApplication> AllApplications = new List<CoriaApplication>()
        //{
        //    new CoriaApplication() { ApplicationId = new Guid("cd3cdc13-8b4f-4dfd-9a97-9e5c06d1fa73"), Name = "Map", Description = "Displays a Map from a source" },
        //    new CoriaApplication() { ApplicationId = new Guid("15238a71-e923-463c-9df8-a98f022b0760"), Name = "List", Description = "Displays a List from a source" },
        //    new CoriaApplication() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), Name = "Chart", Description = "Displays a Chart from a source." }
        //};

        //private static readonly List<Map> AllLinks = new List<Map>()
        //{
        //    new Map() { ApplicationId = new Guid("cd3cdc13-8b4f-4dfd-9a97-9e5c06d1fa73"), ContentId = new Guid("f01c4ef7-25e0-4b61-8722-891fc8049202"), Name = "Google", Description = "Search the world's information, including webpages, images, videos and more.", Url = "http://www.google.com" },
        //    new Map() { ApplicationId = new Guid("cd3cdc13-8b4f-4dfd-9a97-9e5c06d1fa73"), ContentId = new Guid("df022553-3b96-409b-9f8a-0cdecfa4f35d"), Name = "Bing", Description = "Bing helps you turn information into action, making it faster and easier to go from searching to doing.", Url = "http://www.bing.com" },
        //    new Map() { ApplicationId = new Guid("cd3cdc13-8b4f-4dfd-9a97-9e5c06d1fa73"), ContentId = new Guid("fb70bffe-9707-4fbd-bfd1-9238385afb14"), Name = "Yahoo", Description = "The new Yahoo experience makes it easier to discover the news and information that you care about most.", Url = "http://www.yahoo.com" },

        //    new List() { ApplicationId = new Guid("15238a71-e923-463c-9df8-a98f022b0760"), ContentId = new Guid("713933ad-2651-41a0-9eba-cf27061ae52c"), Name = "Telligent", Description = "Telligent is an enterprise collaboration and community software company", Url = "http://www.telligent.com" },
        //    new List() { ApplicationId = new Guid("15238a71-e923-463c-9df8-a98f022b0760"), ContentId = new Guid("cd3f9006-0d1d-4558-b543-b0ddac1ac2f8"), Name = "Telligent Community", Description = "", Url = "http://community.telligent.com" },
        //    new List() { ApplicationId = new Guid("15238a71-e923-463c-9df8-a98f022b0760"), ContentId = new Guid("4b0ea76d-9e14-4ee7-a7ca-304901e1f90d"), Name = "Verint", Description = "Verint solutions capture, distill, and analyze complex and underused information sources (such as voice, video, and unstructured text) to help organizations make timely and effective decisions.", Url = "http://www.verint.com" },

        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("0b3a382e-b407-4606-8182-0462beeb0252"), Name = "Jquery", Description = "The Write Less, Do More, JavaScript Library.", Url = "http://www.jquery.com" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("8d9e4444-19ad-42ba-938a-723645e3979d"), Name = "Asp.Net", Description = "Home of the Microsoft ASP.NET development community", Url = "http://www.asp.net" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("a7936a19-aa21-438b-b1d7-45dd0eb9b525"), Name = "GitHub", Description = "Where people build software", Url = "https://github.com" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("f3a82dcf-4517-43d2-91b4-ede4c35db3e1"), Name = "Stack Overflow", Description = "A language-independent collaboratively edited question and answer site for programmers.", Url = "http://stackoverflow.com/" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("437f8f2c-45bb-4408-9486-8392e8ddcdd8"), Name = "Apache Tomcat", Description = "An open source software implementation of the Java Servlet, JavaServer Pages, Java Expression Language and Java WebSocket technologies. ", Url = "http://tomcat.apache.org/" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("2ecacd3d-6345-485a-88e7-e6e2827433eb"), Name = "Less Css", Description = "Less extends CSS with dynamic behavior such as variables, mixins, operations and functions.", Url = "http://lesscss.org" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("83873c74-afd5-4385-a877-bf6c613ef4c2"), Name = "Apache Solr", Description = "An open-source search server based on the Lucene Java search library.", Url = "http://lucene.apache.org/solr/" },
        //};

        //public static LinksApplication GetApplication(Guid applicationId)
        //{
        //    return AllApplications.FirstOrDefault(a => a.ApplicationId == applicationId);
        //}

        //public static List<LinksApplication> ListApplications()
        //{
        //    return AllApplications;
        //}

        //public static LinkItem GetLink(Guid contentId)
        //{
        //    return AllLinks.FirstOrDefault(a => a.ContentId == contentId);
        //}

        //public static List<LinkItem> ListLinks(Guid applicationId)
        //{
        //    return AllLinks.Where(a => a.ApplicationId == applicationId).ToList();
        //}
    }
    }
