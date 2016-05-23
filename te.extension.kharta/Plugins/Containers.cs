using System;
using System.Collections.Generic;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Version1;
 

namespace te.extension.kharta.Plugins
{
    public class Containers : IPlugin, IPluginGroup, IContainers, IConfigurablePlugin
    {
        public readonly Guid ContainerType_id = new Guid("4ebcc1b3-9daa-4550-aca5-77bb13d026d0");
        public readonly Guid Containers_id = new Guid("5127c319-fda6-40e4-bfd7-37bbfef3ba39");
        public readonly Guid ContainersType_id = new Guid("e504f58d-c1d8-40a8-bf55-bc38c65625e9");

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

        #region IContainers
        public IContainerEvents Events
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


        public Container Get([Documentation("Container Id")] Guid containerId, [Documentation("Container type Id")] Guid containerTypeId)
        {
            throw new NotImplementedException();
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
        public void Update(IPluginConfiguration configuration)
        {
            throw new NotImplementedException();
        }
        #endregion

        


        #endregion
    }
}
