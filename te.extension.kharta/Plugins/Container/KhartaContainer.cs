using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Components;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Security.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Urls.Routing;
using Permission = Telligent.Evolution.Extensibility.Security.Version1.Permission;

using UIApi = Telligent.Evolution.Extensibility.UI.Version1;

namespace te.extension.kharta.Plugins
{
    public class KhartaContainer :  IPlugin, IContainer
    {
        public readonly Guid Container_id = new Guid("1927439a-6c4e-47da-8563-27749845f242"); 
        #region IPlugin Member
        public string Name { get { return "KhartaOlogyContainer"; } }
        public string Description { get { return "Kharta Ology Container"; } }
        public void Initialize()
        {
           // throw new NotImplementedException();
        }
        #endregion
        public string AvatarUrl
        {
            get
            {
               return "https://s.gravatar.com/avatar/533c75456f1e9fa7d8e539bccdb3eff7?s=80";
            }
        }

        public Guid ContainerId
        {
            get { return Container_id; }
        }

        public Guid ContainerTypeId
        {
            get
            {
                return new KhartaContainerType().ContainerTypeId;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return true;
            }
        }

        public string Url
        {
            get
            {
                return null;
            }
        }

        public string HtmlName(string target)
        {
            return Name;
        }
    }
}
