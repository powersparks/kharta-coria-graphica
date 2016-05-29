using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Telligent.Evolution.Components;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Security.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Urls.Routing;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using UIApi = Telligent.Evolution.Extensibility.UI.Version1;
using Permission = Telligent.Evolution.Extensibility.Security.Version1.Permission;
 
 
using Telligent.Evolution.Extensibility;

using Telligent.Evolution.Extensibility.Content.Version1;
namespace te.extension.kharta.Plugins
{
    public class Applications : IPlugin, IPluginGroup, IApplications, IConfigurablePlugin
    {
        public readonly Guid Applications_id = new Guid("5127c319-fda6-40e4-bfd7-37bbfef3ba39");
        public readonly Guid ApplicationsType_id = new Guid("e504f58d-c1d8-40a8-bf55-bc38c65625e9");

        #region IPlugin

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public void Initialize()
        {
            throw new NotImplementedException();
        }


        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #region IPluginGroup
        public IEnumerable<Type> Plugins
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion

 

        #region IConfigurablePlugin
        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Guid DataTypeId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        

        public void Update(IPluginConfiguration configuration)
        {
            throw new NotImplementedException();
        }
        #region IApplications

        public IApplicationEvents Events
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public Application Get([Documentation("Application Id")] Guid applicationId, [Documentation("Application type Id")] Guid applicationTypeId)
        {
            throw new NotImplementedException();
        }

        public ApiList<Application> List(Guid containerTypeId, Guid containerId)
        {
            throw new NotImplementedException();
        }

        public ApiList<Application> Search(Guid containerTypeId, Guid containerId, string searchText)
        {
            throw new NotImplementedException();
        }

        #endregion
        #endregion




        #endregion

    }
}
