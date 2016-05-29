using System;
using System.Collections.Generic;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.Administration.Version1;

using System.Collections.Specialized;

namespace te.extension.kharta.Plugins
{
    public class Containers : IPlugin, IPluginGroup, IContainers, IConfigurablePlugin, IAdministrationPanel, IAdministrationPanelCategory, IAdministrationExplicitPanel, IAdministrationExplicitPanelController
    {
        public readonly Guid Panel_id = new Guid("4ebcc1b3-9daa-4550-aca5-77bb13d026d0");
        public readonly Guid Containers_id = new Guid("5127c319-fda6-40e4-bfd7-37bbfef3ba39");
        public readonly Guid ContainersType_id = new Guid("e504f58d-c1d8-40a8-bf55-bc38c65625e9");

        #region IPlugin
        public string Name
        {
            get
            {
                return "Kharta Coria Graphica";
            }
        }
        public string Description
        {
            get
            {
                return "A Telligent friendly tools for creating Lists, Charts & Maps from geospatial sources";
            }
        }
        public void Initialize()
        {
            //throw new NotImplementedException();
        }
        
        #region IPluginGroup
        public IEnumerable<Type> Plugins
        {
            get
            {
                return  new Type[] {
                    //typeof(KhartaContents),
               }; 
            }
        }
        #endregion

        #region IContainers

        public IContainerEvents Events
        {
        //   // public delegate void KhartaBeforeCreateEventHandler(KhartaBeforeCreateEventArgs e);
        ////public delegate void KhartaAfterCreateEventHandler(KhartaAfterCreateEventArgs e);

        get
            {
        //        // event ContainerAfterCreateEventHandler AfterCreate;
        //        //event ContainerAfterDeleteEventHandler AfterDelete;
        //        //event ContainerAfterUpdateEventHandler AfterUpdate;
        //        //event ContainerBeforeCreateEventHandler BeforeCreate;
        //        //event ContainerBeforeDeleteEventHandler BeforeDelete;
        //        //event ContainerBeforeUpdateEventHandler BeforeUpdate;

        //        //void OnAfterCreate(IContainer contentContainer);
        //        //void OnAfterDelete(IContainer contentContainer);
        //        //void OnAfterUpdate(IContainer contentContainer);
        //        //void OnBeforeCreate(IContainer contentContainer);
        //        //void OnBeforeDelete(IContainer contentContainer);
        //        //void OnBeforeUpdate(IContainer contentContainer);
        //        //event _event = new event();
        //        //_event..Add(new Property("connectionString", "Database Connection String", PropertyType.String, 1, "") { DescriptionText = "The connection string used to access a SQL 2008 or newer database. The user identified should have db_owner permissions to the database." });
            return null;
            }
        }
        public Guid DataTypeId
        {
            get
            {
                return ContainersType_id;
            }
        }


        public Container Get([Documentation("Container Id")] Guid containerId, [Documentation("Container type Id")] Guid containerTypeId)
        {
            return null;       // throw new NotImplementedException();
        }


        #endregion

        #region IConfigurablePlugin
        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                PropertyGroup group = new PropertyGroup("setup", "Setup", 1);
                group.Properties.Add(new Property("connectionString", "Database Connection String", PropertyType.String, 1, "") { DescriptionText = "The connection string used to access a SQL 2008 or newer database. The user identified should have db_owner permissions to the database." });
                return new PropertyGroup[] { group }; // throw new NotImplementedException();
            }
        }
        public void Update(IPluginConfiguration configuration)
        {
            throw new NotImplementedException();
        }
        #endregion




        #endregion

        #region IAdministrationExplicit
        public string GetUrl()
        {
            throw new NotImplementedException();
        }

        public string GetUrl(NameValueCollection parameters)
        {
            throw new NotImplementedException();
        }
        #endregion



        #region IAdministrationPanel


        public string CssClass
        {
            get
            {
                return "kharta-class";
            }
        }
 

        public int? DisplayOrder
        {
            get
            {
                return 3;
            }
        }

        public bool IsCacheable
        {
            get
            {
                return false;
            }
        }

         

        public string PanelDescription
        {
            get
            {
                return "add Kharta admin panel";
            }
        }

        public Guid PanelId
        {
            get
            {
                return Panel_id;
            }
        }

        public string PanelName
        {
            get
            {
                return "Kharta Panel Name";
            }
        }

        public bool VaryCacheByUser
        {
            get
            {
                return false;// throw new NotImplementedException();
            }
        }

        public string GetViewHtml()
        {
            return "Kharta view html";//throw new NotImplementedException();
        }

        public bool HasAccess(int userId)
        {
            return true;//throw new NotImplementedException();
        }

         
        #endregion
        #region IAdministrationPanelCategory
        public Guid AdministrationPanelCategoryId
        {
            get
            {
                return Panel_id;
            }
        }


        public string AvatarUrl
        {
            get
            {
                return "/cfs-filesystemfile/__key/system/images/kharta.svg";
            }
        }

        public string CategoryName
        {
            get
            {
                return "Kharta Cat";
            }
        }


        #endregion

        #region IAdministrationPanelExplicitControler


        public string GetPanelName(NameValueCollection parameters)
        {
            throw new NotImplementedException();
        }

        public string GetPanelDescription(NameValueCollection parameters)
        {
            throw new NotImplementedException();
        }

        public bool HasAccess(int userId, NameValueCollection parameters)
        {
            throw new NotImplementedException();
        }

        public string GetViewHtml(NameValueCollection parameters)
        {
            throw new NotImplementedException();
        }

        public void SetController(IAdministrationExplicitPanelController controller)
        {
            //
        }


        #endregion
    }
}
