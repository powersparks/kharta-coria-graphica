using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;

namespace te.extension.kharta.PublicApi
{
    public class Sources
    {
        private static readonly Guid _contentTypeId = new Guid("9c335c7d-ce69-44fe-ac7a-f86a55689c7b");
        public static Guid ContentTypeId { get { return _contentTypeId; } /**internal set { } **/}

       

        //create or add Source

        //get Source by id

        //get Source by Guid
        public static Source GetSourceApplication(Guid applicationId)
        {
            //KhartaSource khartaSource = new KhartaSource();
            try {
                InternalApi.KhartaSource source = InternalApi.SourceDataService.GetSourceApplication(applicationId);
                if (source == null)
                    return null;

                return new Source(source);

            } catch (Exception ex) {

                return new Source(new AdditionalInfo(new Error(ex.GetType().FullName, ex.Message)));
            }
   
        }

        internal static Source Get(Guid id)
        {
            throw new NotImplementedException();
        }

        internal static PagedList<Source> List(int groupId, SourcesListOptions query)
        {
            PagedList<Source> _sources = new PagedList<Source>();

            return _sources;
        }

        internal static AdditionalInfo Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        internal static Source Create(int groupId, string name, string description)
        {
            Source source = new Source();
            return source;
        }

        internal static Source Update(Guid id, string name, string description)
        {
            throw new NotImplementedException();
        }

        internal static bool CanCreate(int groupId)
        {
            throw new NotImplementedException();
        }

        internal static bool CanEdit(Guid sourceId)
        {
            throw new NotImplementedException();
        }

        internal static string GetCreateSourceUrl(int groupId, string v1, string v2)
        {
            string url = "";
            return url;
        }

        internal static bool CanDelete(Guid sourceId)
        {
            throw new NotImplementedException();
        }

        internal static string UI(Guid sourceId, bool readOnly, bool showNameAndDescription)
        {
            throw new NotImplementedException();
        }
        //Update Source
        //Delete Source
    }
}
