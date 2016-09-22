using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Urls.Version1;

namespace te.extension.coria.Plugins.UI.Navigation
{
    public class MapBookUserNavigation : INavigable
    {
        public string Description
        {
            get
            {
                return "Navigation for User Map Books";
            }
        }

        public string Name
        {
            get
            {
                return "UserMapBookNavigation";
            }
        }

        public void Initialize()
        {
           
        }

        public void RegisterUrls(IUrlController controller)
        {
            controller.AddPage("UserMapBookList", "members/{UserName}/mapbooks", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-mapbooklist", MapBooks_PdOptions);
            controller.AddPage("UserMapBookSingle", "members/{UserName}/{mapBooks}/{mapBook}", new Telligent.Evolution.Urls.Routing.NotSiteRootRouteConstraint(), null, "coria-mapbook-map-list", MapBook_PdOptions);
        }
        private PageDefinitionOptions MapBooks_PdOptions
        {
            get
            {
                return new PageDefinitionOptions()
                {
                    ForceLowercaseUrl = true,
                    HasApplicationContext = true,
                   // ParseContext = new Action<PageContext>(this.ParseMapBooks_Context)
                    //Validate = new Action<PageContext, IUrlAccessController>(Validate)
                };
            }
        }
        private PageDefinitionOptions MapBook_PdOptions
        {
            get
            {
                return new PageDefinitionOptions()
                {
                    ForceLowercaseUrl = true,
                    HasApplicationContext = true,
                   // ParseContext = new Action<PageContext>(this.ParseMapBook_Context)
                    //Validate = new Action<PageContext, IUrlAccessController>(Validate)
                };
            }
        }
    }
}
