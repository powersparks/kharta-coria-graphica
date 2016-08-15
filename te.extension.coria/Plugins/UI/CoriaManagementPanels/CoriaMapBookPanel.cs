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
     public class CoriaMapBookPanel : IPlugin, IApplicationPanel,  UIApi.IScriptablePlugin, IContainerPanel
    {
        Guid _manPanelWidgetId = new Guid("99f22a55-f1f4-4584-8a76-dd0a64452d6b");
        Guid _panelId = new Guid("a06a4d37-82d6-42a4-b20c-140ffd882677");
        Guid _applicationPanel = new Guid("c4315566-7dcc-46b3-9ab7-7715d05498ad");
        Guid _applicationTypeId = new Guid("bfdb6103-e8e5-4cbf-8fbf-42dbac4046ef");
        UIApi.IScriptedContentFragmentController _iScriptedContentFragmentController;
        #region IPlugin
        public string Name { get { return "MapBook Management Panel"; } }
        public string Description { get { return "panel to better manage mapbooks"; } }
        public void Initialize() {}
        #endregion
        #region IApplicationPanel
        public Guid[] ApplicationTypes
        { 
            get { return TEApi.ApplicationTypes.List().Where(c => Telligent.Evolution.Extensibility.Version1.PluginManager.Get<IWebContextualApplicationType>().Any(a => a.ApplicationTypeId == c.Id.GetValueOrDefault(Guid.Empty))).Select(c => c.Id.Value).ToArray(); }
        }
     
        public string CssClass { get { return _iScriptedContentFragmentController.GetMetadata(_manPanelWidgetId).CssClass; } }
        public int? DisplayOrder { get { return 0; } }
        public bool IsCacheable { get { return _iScriptedContentFragmentController.GetMetadata(_manPanelWidgetId).IsCacheable; ; } }
        public Guid PanelId { get { return _panelId; } }
        public bool VaryCacheByUser { get { return true; } }
        public string GetPanelDescription(Guid applicationType, Guid applicationId) { return "update get panel description"; }
        public string GetPanelName(Guid applicationType, Guid applicationId) { return "Map Book App Panel"; }
        public string GetViewHtml(Guid applicationType, Guid applicationId)
        {
            _manPanelWidgetId = new Guid("99f22a55-f1f4-4584-8a76-dd0a64452d6b");
             NameValueCollection context = new NameValueCollection();
            context.Add("TypeId", applicationType.ToString("N"));
            context.Add("Id", applicationId.ToString("N"));
            //return _controller.RenderContent(_instanceIdentifier, new NameValueCollection() { { "TypeId", type.ToString() }, { "Id", id.ToString() } });
            string manPanelHtml = _iScriptedContentFragmentController.RenderContent(_manPanelWidgetId,context);
            return manPanelHtml;
        }
        public bool HasAccess(int userId, Guid applicationType, Guid applicationId)
        { return true; }
        #endregion
        public Guid ScriptedContentFragmentFactoryDefaultIdentifier
        { get { return _manPanelWidgetId; } }

        public Guid[] ContainerTypes
        {
            get
            {
                return new Guid[] { Apis.Get<IGroups>().ContainerTypeId };
            }
        }

        public void Register(UIApi.IScriptedContentFragmentController controller) {
            var options = new UIApi.ScriptedContentFragmentOptions(_manPanelWidgetId)
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
    }
}

