using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using te.extension.kharta.Plugins.UI;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Components;
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Administration.Version1;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.Security.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Urls.Routing;
using Permission = Telligent.Evolution.Extensibility.Security.Version1.Permission;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using UIApi = Telligent.Evolution.Extensibility.UI.Version1;

namespace te.extension.kharta.Plugins
{
    public class KhartaCoriaGraphicaPluginGroup : IPlugin, IPluginGroup
    {
        public string Description { get { return "Kharta Coria Graphic Plugin, bringing geospatial driven applications to your community"; } }

        public string Name { get { return "Kharta Coria Graphica"; } }

        public IEnumerable<Type> Plugins { get { return new Type[] {
            typeof (te.extension.kharta.Plugins.KhartaApplications),
            typeof (te.extension.coria.Plugins.CoriaApplications),
            typeof (te.extension.graphica.Plugins.GraphicaApplications)
        }; } }

        public void Initialize() {  }

    }
}
