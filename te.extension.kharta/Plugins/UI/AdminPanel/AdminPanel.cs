using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Components;
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Administration.Version1;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.Security.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Urls.Version1;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Urls.Routing;
using Permission = Telligent.Evolution.Extensibility.Security.Version1.Permission;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using UIApi = Telligent.Evolution.Extensibility.UI.Version1;

namespace te.extension.kharta.Plugins.UI
{
    public class AdminPanel : IPlugin, IScriptablePlugin, IAdministrationPanel, IAdministrationPanelCategory //, IAdministrationExplicitPanel, IAdministrationExplicitPanelController
    {
        //, IAdministrationPanel, IAdministrationPanelCategory, IAdministrationExplicitPanel, IAdministrationExplicitPanelController
        public static readonly Guid Panel_id = new Guid("4ebcc1b3-9daa-4550-aca5-77bb13d026d0");
        public static readonly Guid _scriptedContentFragmentFactoryDefaultIdentifier = new Guid("56887da1-0ecb-404c-8b03-cc45ed4ac374");
        //UI.KhartaFactoryDefaultWidgetProvider.WidgetFactoryDefault_id;// //this should be and actual widget i think
        public static readonly Guid AdminPanelCategoryId = new Guid("5730e295-422e-411b-90d6-a21026750e4e");
        //private IAdministrationExplicitPanelController _iAdministrationExplicitPanelController;// = new AdministrationExplicitPanelController();
        private IScriptedContentFragmentController _iScriptedContentFragmentController;
        #region IPlugin
        public string Description { get { return "Kharta Admin Panel"; } }

        public string Name { get { return "Kharta Admin Panel"; } }

        public void Initialize()
        {
           // throw new NotImplementedException();
        }
        #endregion
        #region IScriptablePlugin
        public Guid ScriptedContentFragmentFactoryDefaultIdentifier { get { return _scriptedContentFragmentFactoryDefaultIdentifier; } }
        public void Register(IScriptedContentFragmentController controller)
        {
            Guid adminPanelWidgetId = new Guid("b089b96795074ad1ad130141f62bc937");
            var options = new ScriptedContentFragmentOptions(adminPanelWidgetId)
            {
                CanBeThemeVersioned = true,
                CanHaveHeader = true,
                CanHaveWrapperCss = true,
                CanReadPluginConfiguration = true,
                IsEditable = true,

            };
            //options.Extensions.Add(new PanelContext());

            controller.Register(options);


            // throw new NotImplementedException();
            _iScriptedContentFragmentController = controller;

         
        }
        public class PanelContext : IContextualScriptedContentFragmentExtension
        {
            public string ExtensionName
            {
                get { return "Kharta"; }
            }

            public object GetExtension(NameValueCollection context)
            {
                return null;
            }
        }
        #endregion
        #region IAdministrationPanel
        public Guid AdministrationPanelCategoryId { get { return AdminPanelCategoryId; } }
        public string CssClass { get { return "kharta-class"; } }
        public int? DisplayOrder { get { return 3; } }
        public bool IsCacheable { get { return false; } }
        public string PanelDescription { get {return "Admin Kharta Panel"; } }
        public Guid PanelId { get { return Panel_id; } }
        public string PanelName { get { return "Admin Kharta Panel"; } }
        public bool VaryCacheByUser { get { return false; } }
        public string GetViewHtml()
        {
           
            Guid adminPanelWidgetId = new Guid("b089b96795074ad1ad130141f62bc937");
            NameValueCollection context = new NameValueCollection();
            string adminPanelHtml = _iScriptedContentFragmentController.RenderContent(adminPanelWidgetId, context);

            return adminPanelHtml;// "Kharta view html";//
        }
        public bool HasAccess(int userId)
        {
            return true; //throw new NotImplementedException();
        }
        #endregion
        #region IAdministrationPanelCategory
        public string AvatarUrl { get { return "/cfs-filesystemfile/__key/system/images/kharta.svg"; } }
        public string CategoryName { get { return "Ontologies"; } }
        #endregion

         
    }
}
