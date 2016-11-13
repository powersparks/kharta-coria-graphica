using System;
using System.Collections.Specialized;
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Administration.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Version1;
using UIApi = Telligent.Evolution.Extensibility.UI.Version1;

namespace te.extension.kharta.Plugins.UI.ManagementPanels
{
    public class KhartaGroupOptionsManPanel : IPlugin, IContainerPanel, UIApi.IScriptablePlugin
    {
        Guid _KhartaGroupOptionsManPanel_WidgetId = new Guid("c23d4078-9a6d-4880-9ff6-9da6e868ec8a");
        public static readonly Guid _scriptedContentFragmentFactoryDefaultIdentifier = new Guid("9ffc8dcd-4f53-415c-ab88-e534ca6de742");
        public static Guid _panelId = new Guid("06e56801-952a-4e81-b96f-470b0a91e88d");
        //Group Options Panel uses guid 9ee64c4c-2e5f-4c11-9ac9-ee0f805de2fa
        public static Guid _containerPanel = new Guid("9ee64c4c-2e5f-4c11-9ac9-ee0f805de2fa");
        UIApi.IScriptedContentFragmentController _iScriptedContentFragmentController;

        #region IPlugin
        public string Name { get { return "Kharta Options"; } }
        public string Description { get { return "- manage group options"; } }
        public void Initialize() { }
        #endregion
        #region IContainerPanel
        public Guid[] ContainerTypes { get { return new Guid[] { Apis.Get<IGroups>().ContainerTypeId }; } }
        public Guid PanelId { get { return _panelId; } }
        public string CssClass { get { return "kharta"; } }
        public int? DisplayOrder { get { return 0; } }
        public bool IsCacheable { get { return false; } }
        public bool VaryCacheByUser { get { return true; } }
        public string GetPanelName(Guid containerType, Guid containerId) { return Name; }
        public string GetPanelDescription(Guid containerType, Guid containerId) { return Description; }
        public bool HasAccess(int userId, Guid containerType, Guid containerId) { return true; }
        public string GetViewHtml(Guid type, Guid id) { return _iScriptedContentFragmentController.RenderContent(_KhartaGroupOptionsManPanel_WidgetId, new NameValueCollection() { { "ApplicationTypeId", type.ToString() }, { "ApplicationId", id.ToString() } }); }

        #endregion

        #region IScriptablePlugin
        public Guid ScriptedContentFragmentFactoryDefaultIdentifier { get { return _scriptedContentFragmentFactoryDefaultIdentifier; } }  
        
        public void Register(UIApi.IScriptedContentFragmentController controller)
        {
            var options = new UIApi.ScriptedContentFragmentOptions(_KhartaGroupOptionsManPanel_WidgetId)
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
            public string ExtensionName { get { return "kharta_v1_panel_context"; } }
            public object GetExtension(NameValueCollection context)
            {
                //Guid applicationId = Guid.TryParse(context["applicationId"], out applicationId) ? applicationId : Guid.Empty;
                //Guid applicationType = Guid.TryParse(context["applicationTypeId"], out applicationType) ? applicationType : Guid.Empty;
                //if (applicationId.Equals(Guid.Empty)) { return null; }
                return new KhartaPanelApi();
            }
        }

        [Documentation(Category = "Kharta")]
        public class KhartaPanelApi
        {
            Guid _containerType, _contrainerId,  _applicationType, _applicationId;  
            public KhartaPanelApi() { }
           
           
            public KhartaPanelApi(Guid applicationType, Guid applicationId)
            {
                _applicationId = applicationId;
                _applicationType = applicationType;
                 
            }
                 

        }
        #endregion

    }
}
