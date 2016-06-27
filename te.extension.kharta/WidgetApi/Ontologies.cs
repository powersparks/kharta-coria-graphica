using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Version1;
using System.Collections;
using Telligent.Evolution.Urls.Routing;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;

namespace te.extension.kharta.WidgetApi
{
    [Documentation(Category = "Kharta")]
    public class Ontologies
    {
        [Documentation("content type for Ontology")]
        public static Guid ContentTypeId { get; internal set; }

        [Documentation("List Ontologies")]
        public ApiList<PublicApi.Ontology> List()
        {
            return  PublicApi.Ontologies.List;
        }

        [Documentation("Create an Ontology")]
        public PublicApi.Ontology Create() { return new PublicApi.Ontology(); }

        [Documentation("Add or Update an Ontology")]
        public PublicApi.Ontology AddUpdate(PublicApi.Ontology ontology) {
            InternalApi.KhartaOntology _ontology = new InternalApi.KhartaOntology();
            
            return ontology;
        }
        //public ApiList<LinksApplication> List()
        //{
        //    return new ApiList<LinksApplication>(LinksData.ListApplications());
        //}
    }
}
