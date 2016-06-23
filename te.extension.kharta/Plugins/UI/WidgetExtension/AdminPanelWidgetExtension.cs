using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;
namespace te.extension.kharta.Plugins.UI
{
    public class AdminPanelWidgetExtension : IPlugin, IScriptedContentFragmentExtension 
    {

        public string Name { get { return "Kharta Admin Panel (kharta_v1_admin_panel)"; } }
        public string Description { get { return "Widget API for working with admin panel"; } }
        public object Extension { get { return new WidgetApi.AdminPanel(); } }
        public string ExtensionName { get { return "kharta_v1_admin_panel"; } }
        public void Initialize() { }
    }
}
