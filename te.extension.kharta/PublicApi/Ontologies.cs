using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;

namespace te.extension.kharta.PublicApi
{
    public class Ontologies
    {
        public static Ontology GetOntologyApplication(Guid applicationId)
        {
            //KhartaSource khartaSource = new KhartaSource();
            try
            {
                //TODO: should be getting the application, so either change container to an application or create a application data servcies for ontology
                InternalApi.KhartaOntology ontology = InternalApi.OntologyDataService.getContainerByGuid(applicationId);
                if (ontology == null)
                    return null;

                return new Ontology(ontology);

            }
            catch (Exception ex)
            {

                return new Ontology(new AdditionalInfo(new Error(ex.GetType().FullName, ex.Message)));
            }
        }

       
    }
}
