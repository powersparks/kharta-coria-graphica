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
    [Documentation(Category = "KhartaAdminPanel")]
    public class AdminPanel
    {
        [Documentation("content type for Ontology")]
        public Guid ContentTypeId { get { return new Guid(); } }
    }
}
