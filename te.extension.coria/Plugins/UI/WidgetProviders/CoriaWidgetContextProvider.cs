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

namespace te.extension.coria.Plugins.UI
{
    public class CoriaWidgetContextProvider : IScriptedContentFragmentContextProvider
    {
        
            private readonly Guid CoriaContextId = new Guid("336abd13-7910-42d0-b338-4f8c74a5c432");
            public string Name { get { return "Coria Widget Context Provider"; } }
            public string Description { get { return "Coria Widgets using this provider ensure to only be used in the appropriate context"; } }
            public IEnumerable<ContextItem> GetSupportedContextItems()
            {
                return new ContextItem[] {
                new ContextItem("Coria", CoriaContextId)
            };
            }
            public bool HasContextItem(System.Web.UI.Page page, Guid contextItemId)
            {
                bool coriaExistance = false;
                string[] coriaTypeIds = { "CoriaId" };
                object coriaIdObject;
                Guid coriaGuid;
                foreach (string typeId in coriaTypeIds)
                {
                    try
                    {
                        if (page.RouteData.Values[typeId].GetType() == typeof(string))
                        {
                            coriaIdObject = page.RouteData.Values[typeId];
                            if (Guid.TryParse(coriaIdObject.ToString(), out coriaGuid))
                            {
                                switch (typeId)
                                {
                                    case "CoriaId":
                                        //call data service
                                        coriaExistance = true;
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        if (coriaExistance)
                        {
                            //object was found so done checking
                            var exception = ex;
                            return coriaExistance;
                        }
                    }//value was not found keep checking...}

                }
                return coriaExistance;
                //InternalApi.OntologyDataService.KhartaExist(khartId)!= null;
            }

            public void Initialize() { }
        }
}
