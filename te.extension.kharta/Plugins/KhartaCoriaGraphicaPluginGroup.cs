using System;
using System.Collections.Generic;
using kharta.coria.graphica.core; 
using Telligent.Evolution.Extensibility.Version1;
 
namespace te.extension.kharta.Plugins
{
    public class KhartaCoriaGraphicaPluginGroup : IPlugin, IPluginGroup
    {
        public string Description { get { return "Kharta Coria Graphic Plugin, bringing geospatial driven applications to your community"; } }

        public string Name { get { return "Kharta Coria Graphica"; } }

        public IEnumerable<Type> Plugins { get { return new Type[] {
            typeof (te.extension.kharta.Plugins.KhartaApplications),
            typeof (te.extension.coria.Plugins.CoriaApplications),
            typeof (te.extension.graphica.Plugins.GraphicaApplications),
            typeof(KhartaCore),
            typeof(GraphicaCore),
            typeof(CoriaCore)
            
        }; } }

        public void Initialize() {  }

    }
}
