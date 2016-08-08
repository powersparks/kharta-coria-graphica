using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace te.extension.coria.Plugins.UI.WidgetExtension 
{
    public class MapWidgetExtension : IPlugin, IScriptedContentFragmentExtension
    {
        public string Name { get { return "Coria Map Widget Extension(coria_v1_map)"; } }
        public string Description { get { return "Widget API Extension for working with coria maps"; } }
        public object Extension { get { return new WidgetApi.Maps(); } }
        public string ExtensionName { get { return "coria_v1_map"; } }
        public void Initialize() { }
    }
    public class MapBookWidgetExtension : IPlugin, IScriptedContentFragmentExtension
    {
        public string Name { get { return "Coria Map Widget Extension(coria_v1_mapBook)"; } }
        public string Description { get { return "Widget API Extension for working with coria map books"; } }
        public object Extension { get { return new WidgetApi.Maps(); } }
        public string ExtensionName { get { return "coria_v1_mapBook"; } }
        public void Initialize() { }
    }

}
