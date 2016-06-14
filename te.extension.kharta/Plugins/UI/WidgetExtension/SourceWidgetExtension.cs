using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;
 
namespace te.extension.kharta.Plugins.UI.WidgetExtension
{
    public class SourceWidgetExtension : IScriptedContentFragmentExtension
    {
  

        public string Name { get { return "Kharta Sources"; } }
        public string Description { get { return "Widget API for working with sources"; } } 
        public object Extension { get { return new WidgetApi.Sources(); } } 
        public string ExtensionName { get { return "kharta_v1_sources"; } }
        public void Initialize() {   }
    }
}
