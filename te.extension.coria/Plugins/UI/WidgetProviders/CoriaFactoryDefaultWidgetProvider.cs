using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
namespace te.extension.coria.Plugins.UI
{
    public class CoriaFactoryDefaultWidgetProvider : IScriptedContentFragmentFactoryDefaultProvider
    {
        public static readonly Guid WidgetFactoryDefault_id = new Guid("11d24858-4e70-4d4f-9bb7-de3e627bc200");
        public string Name { get { return "Coria Factory Default Widget Provider"; } }
        public string Description { get { return "Coria widgets must use this provider identifier to load"; } }
        public Guid ScriptedContentFragmentFactoryDefaultIdentifier { get { return WidgetFactoryDefault_id; } }
        public void Initialize() { }
    }
}
