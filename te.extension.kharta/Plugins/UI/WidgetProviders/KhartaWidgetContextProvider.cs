using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;

namespace te.extension.kharta.Plugins.UI
{
    public class KhartaWidgetContextProvider : IPlugin, IScriptedContentFragmentContextProvider
    {
        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<ContextItem> GetSupportedContextItems()
        {
            throw new NotImplementedException();
        }

        public bool HasContextItem(System.Web.UI.Page page, Guid contextItemId)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
