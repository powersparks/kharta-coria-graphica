using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
namespace te.extension.kharta.Plugins.UI
{
    public class KhartaFactoryDefaultWidgetProvider : IScriptedContentFragmentFactoryDefaultProvider
    {
        public static readonly Guid WidgetFactoryDefault_id = new Guid("c1b6977e-c23d-4bb7-9227-8a1c57c83499");
        public string Name { get { return "Kharta Factory Default Widget Provider"; } }
        public string Description { get { return "Kharta widgets must use this provider identifier to load"; } }
        public Guid ScriptedContentFragmentFactoryDefaultIdentifier { get { return WidgetFactoryDefault_id; } }
        public void Initialize() { }
    }
}
