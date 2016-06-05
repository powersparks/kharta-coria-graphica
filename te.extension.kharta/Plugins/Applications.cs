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
    public class Applications : IPlugin , IApplications, IConfigurablePlugin
    {
        public readonly Guid Applications_id = new Guid("5127c319-fda6-40e4-bfd7-37bbfef3ba39");
        public readonly Guid ApplicationsType_id = new Guid("e504f58d-c1d8-40a8-bf55-bc38c65625e9");

        #region IPlugin
        public string Name { get { return "Data Tools"; } }
        public string Description { get { return "Kharta data sources inventory and mananagement tools"; } }
        public Guid DataTypeId { get { return Applications_id; } }
        public void Initialize()
        {
            //throw new NotImplementedException();
        }

        #region IConfigurablePlugin
        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                PropertyGroup group3 = new PropertyGroup("option1", "Setup", 1);//tab 1 ui
                group3.Properties.Add(new Property("propertyKeyName1", "Property Key Name Title", PropertyType.String, 1, "property key default value") { DescriptionText = "Property Key and Value Description." });
                PropertyGroup group4 = new PropertyGroup("option2", "Advanced", 2);//tab 2 ui
                group4.Properties.Add(new Property("propertyKeyName2", "Property Key Name Title", PropertyType.String, 1, "property key default value") { DescriptionText = "Property Key and Value Description." });

                return new PropertyGroup[] { group3, group4 };
            }
        }
        public void Update(IPluginConfiguration configuration)
        {
            //TODO: configuration values need to be passed in and used somewhere
            //InternalApi.InternalClassName.PropertyKeyName1 = configuration.GetString("propertyKeyName1");
        }
        #endregion


        #region IApplications

        public IApplicationEvents Events
        {
            get
            {
                return null;// throw new NotImplementedException();
            }
        }
        public Application Get([Documentation("Application Id")] Guid applicationId, [Documentation("Application type Id")] Guid applicationTypeId)
        {
            return null;// throw new NotImplementedException();
        }

        public ApiList<Application> List(Guid containerTypeId, Guid containerId)
        {
            return null; // throw new NotImplementedException();
        }

        public ApiList<Application> Search(Guid containerTypeId, Guid containerId, string searchText)
        {
            return null; // throw new NotImplementedException();
        }

        #endregion
       




        #endregion

    }
}
