using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
namespace te.extension.graphica.Plugins.UI
{
    public class GraphicaFactoryDefaultWidgetProvider : IScriptedContentFragmentFactoryDefaultProvider
    {
        public static readonly Guid WidgetFactoryDefault_id = new Guid("d9f18152-6bcf-4d54-b1d3-14a95407a46d");
        public string Name { get { return "Graphica Factory Default Widget Provider"; } }
        public string Description { get { return "Graphica widgets must use this provider identifier to load"; } }
        public Guid ScriptedContentFragmentFactoryDefaultIdentifier { get { return WidgetFactoryDefault_id; } }
        public void Initialize() { }
    }
}
