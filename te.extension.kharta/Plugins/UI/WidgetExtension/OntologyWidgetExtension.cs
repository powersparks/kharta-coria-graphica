using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace te.extension.kharta.Plugins.UI.WidgetExtension
{
    public class OntologyWidgetExtension : IPlugin, IScriptedContentFragmentExtension
    {

        public string Name { get { return "Kharta Admin Panel (kharta_v1_ontologies)"; } }
        public string Description { get { return "Widget API for working with ontologies"; } }
        public object Extension { get { return new WidgetApi.Ontologies(); } }
        public string ExtensionName { get { return "kharta_v1_ontologies"; } }
        public void Initialize() { }
    }
}
 
