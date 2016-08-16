using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIApi = Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Administration.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using System.Collections.Specialized;
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Api.Plugins.Administration;

using Telligent.Evolution.Components;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;

namespace te.extension.coria.Plugins.UI.CoriaManagementPanels
{
     public class CoriaMapBookPanel : IPlugin, IApplicationPanel,  UIApi.IScriptablePlugin //, IContainerPanel
    {
        Guid _CoriaMapBookManPanel_WidgetId = new Guid("99f22a55-f1f4-4584-8a76-dd0a64452d6b");
        Guid _panelId = new Guid("a06a4d37-82d6-42a4-b20c-140ffd882677");
        Guid _applicationPanel = new Guid("c4315566-7dcc-46b3-9ab7-7715d05498ad");
        
        UIApi.IScriptedContentFragmentController _iScriptedContentFragmentController;
        #region IPlugin
        public string Name { get { return "MapBook Management Panel"; } }
        public string Description { get { return "panel to better manage mapbooks"; } }
        public void Initialize() {}
        #endregion
        #region IApplicationPanel
        public Guid[] ApplicationTypes {    get {  return new Guid[] { Application.CoriaMapType._applicationTypeId }; } }
        public string CssClass { get { return _iScriptedContentFragmentController.GetMetadata(_CoriaMapBookManPanel_WidgetId).CssClass; } }
        public int? DisplayOrder { get { return 100; } }
        public bool IsCacheable { get { return _iScriptedContentFragmentController.GetMetadata(_CoriaMapBookManPanel_WidgetId).IsCacheable; ; } }
        public Guid PanelId { get { return _panelId; } }
        public bool VaryCacheByUser { get { return true; } }
        public string GetPanelDescription(Guid applicationType, Guid applicationId) { return "update get panel description"; }
        public string GetPanelName(Guid applicationType, Guid applicationId) { return "Map Book App Panel"; }
        public string GetViewHtml(Guid type, Guid id) { return _iScriptedContentFragmentController.RenderContent(_CoriaMapBookManPanel_WidgetId, new NameValueCollection() { { "ApplicationTypeId", type.ToString() }, { "ApplicationId", id.ToString() } });}
        public bool HasAccess(int userId, Guid applicationType, Guid applicationId) { return true; }
        #endregion
        public Guid ScriptedContentFragmentFactoryDefaultIdentifier { get { return _CoriaMapBookManPanel_WidgetId; } }

        public Guid[] ContainerTypes {  get { return new Guid[] { Apis.Get<IGroups>().ContainerTypeId }; } }

        public void Register(UIApi.IScriptedContentFragmentController controller) {
            var options = new UIApi.ScriptedContentFragmentOptions(_CoriaMapBookManPanel_WidgetId)
            {
                CanBeThemeVersioned = false,
                CanHaveHeader = false,
                CanHaveWrapperCss = false,
                CanReadPluginConfiguration = false,
                IsEditable = true,

            };
            options.Extensions.Add(new PanelContext());
            controller.Register(options);
            _iScriptedContentFragmentController = controller;
        }
        public class PanelContext : IContextualScriptedContentFragmentExtension
        {
            public string ExtensionName {  get { return "Coria Context"; } }
            public object GetExtension(NameValueCollection context) { return null;  }
        }
    }
}

