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

namespace te.extension.graphica.Plugins.UI
{
    public class GraphicaWidgetContextProvider : IScriptedContentFragmentContextProvider
    {
        
            private readonly Guid GraphicaContextId = new Guid("9051469e-e773-4d29-afe2-73c74b197da6");
            public string Name { get { return "Graphica Widget Context Provider"; } }
            public string Description { get { return "Graphica Widgets using this provider ensure to only be used in the appropriate context"; } }
            public IEnumerable<ContextItem> GetSupportedContextItems()
            {
                return new ContextItem[] {
                new ContextItem("Graphica", GraphicaContextId)
            };
            }
            public bool HasContextItem(System.Web.UI.Page page, Guid contextItemId)
            {
                bool graphicaExistance = false;
                string[] graphicaTypeIds = { "GraphicaId" };
                object graphicaIdObject;
                Guid graphicaGuid;
                foreach (string typeId in graphicaTypeIds)
                {
                    try
                    {
                        if (page.RouteData.Values[typeId].GetType() == typeof(string))
                        {
                            graphicaIdObject = page.RouteData.Values[typeId];
                            if (Guid.TryParse(graphicaIdObject.ToString(), out graphicaGuid))
                            {
                                switch (typeId)
                                {
                                    case "GraphicaId":
                                        //call data service
                                        graphicaExistance = true;
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        if (graphicaExistance)
                        {
                            //object was found so done checking
                            var exception = ex;
                            return graphicaExistance;
                        }
                    }//value was not found keep checking...}

                }
                return graphicaExistance;
                //InternalApi.OntologyDataService.KhartaExist(khartId)!= null;
            }

            public void Initialize() { }
        }
}
