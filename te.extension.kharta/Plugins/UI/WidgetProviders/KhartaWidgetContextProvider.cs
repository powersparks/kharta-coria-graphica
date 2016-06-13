using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;

namespace te.extension.kharta.Plugins.UI
{
    public class KhartaWidgetContextProvider : IScriptedContentFragmentContextProvider
    {
        private readonly Guid KhartaContextId = new Guid("47bec3c6-b081-4f36-8812-e42953ef133b");
        private readonly Guid OntologyContextId = new Guid("0a68eca2-137e-49df-bccd-55687f45cdac");
        private readonly Guid SourceContextId = new Guid("6ef2c9ee-9858-4fff-8464-5709acfff9c4");
        private readonly Guid HostingContextId = new Guid("e5706a88-3a69-4697-bad7-45a408311e3b");
        public string Name { get { return "Kharta Widget Context Provider"; } }
        public string Description { get { return "Kharta Widgets using this provider ensure to only be used in the appropriate context"; } }
        public IEnumerable<ContextItem> GetSupportedContextItems()
        {
            return new ContextItem[] {
                new ContextItem("Kharta", KhartaContextId),
                new ContextItem("Ontology", OntologyContextId),
                new ContextItem("Source", SourceContextId),
                new ContextItem("Hosting", HostingContextId)
            };
        }
        public bool HasContextItem(System.Web.UI.Page page, Guid contextItemId)
        {
            /****
             * Kharta Widgets "Has Context Item" in these cases
             * 1) ContextId equals any of the Kharta Widget Id's
             * 2) Content Page Route Data Values[] contains a guid
             * 2a) and is equal to one of the Kharta parameter id
             * 3) the Kharta Guid has an entity
             */
        
            bool khartaExistance = false;
            string[] khartaTypeIds = {"OntologyId","SourceId","HostingId","KhartaId" };
            object khartaIdObject;
            Guid khartaGuid;
            foreach (string typeId in khartaTypeIds)
            {
                try
                {
                    if (page.RouteData.Values[typeId].GetType() == typeof(string))
                    {
                        khartaIdObject = page.RouteData.Values[typeId];
                        if (Guid.TryParse(khartaIdObject.ToString(), out khartaGuid))
                        {
                            switch (typeId)
                            {
                                case "OntologyId":
                                    //call data service
                                    khartaExistance = true;
                                    break;
                                case "SourceId":
                                    khartaExistance = true;
                                    break;
                                case "HostingId":
                                    khartaExistance = true;
                                    break;
                                case "KhartaId":
                                    khartaExistance = true;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                }
                catch (Exception ex) {
                    if (khartaExistance) {
                        //object was found so done checking
                        return khartaExistance;
                    }
                 }//value was not found keep checking...}
     
            } 
            return khartaExistance;
            //InternalApi.OntologyDataService.KhartaExist(khartId)!= null;
        }
        private static bool somemethod2(Guid id, Page page) { return true; }
        private bool somemethod(Guid id) {

            return true;
        }
        public void Initialize() {  }
      
    }
}
