using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIApi = Telligent.Evolution.Extensibility.UI.Version1;
using Telligent.Evolution.Extensibility.Administration.Version1;
using TEApiV1P = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using TEApi = Telligent.Evolution.Extensibility.Api;
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
    public class CoriaMapPanel : IPlugin, IApplicationPanel, UIApi.IScriptablePlugin //, IContainerPanel
    {
        Guid _CoriaMapManPanel_WidgetId = new Guid("e4f6917d-953b-40bc-ae2f-d114a2194345");
        public static readonly Guid _scriptedContentFragmentFactoryDefaultIdentifier = new Guid("23098a95-0892-41cd-bb3e-fa7f78cdd21b");

        public static Guid _panelId = new Guid("f272a349-0fae-4f17-a550-a85fec41e647");
        public static Guid _applicationPanel = new Guid("c4315566-7dcc-46b3-9ab7-7715d05498ad");
        UIApi.IScriptedContentFragmentController _iScriptedContentFragmentController;
        #region IPlugin
        public string Name { get { return "MapBook Map Management Panel"; } }
        public string Description { get { return "panel to better manage mapbook maps"; } }
        public void Initialize() { }
        #endregion
        #region IApplicationPanel
        public Guid[] ApplicationTypes { get { return new Guid[] { Plugins.Application.CoriaMapBookType._applicationTypeId }; } }
        public string CssClass { get { return _iScriptedContentFragmentController.GetMetadata(_CoriaMapManPanel_WidgetId).CssClass; } }
        public int? DisplayOrder { get { return 4; } }
        public bool IsCacheable { get { return _iScriptedContentFragmentController.GetMetadata(_CoriaMapManPanel_WidgetId).IsCacheable; ; } }
        public Guid PanelId { get { return _panelId; } }
        public bool VaryCacheByUser { get { return true; } }
        public string GetPanelDescription(Guid applicationType, Guid applicationId) { return "update get panel description"; }
        public string GetPanelName(Guid applicationType, Guid applicationId) { return "Map Book App Map Panel"; }
        public string GetViewHtml(Guid type, Guid id) { return _iScriptedContentFragmentController.RenderContent(_CoriaMapManPanel_WidgetId, new NameValueCollection() { { "ApplicationTypeId", type.ToString() }, { "ApplicationId", id.ToString() } }); }
        public bool HasAccess(int userId, Guid applicationType, Guid applicationId) { return true; }
        #endregion

        public Guid ScriptedContentFragmentFactoryDefaultIdentifier { get { return _scriptedContentFragmentFactoryDefaultIdentifier; } } //_CoriaMapBookManPanel_WidgetId; } }
        public Guid[] ContainerTypes { get { return new Guid[] { Apis.Get<IGroups>().ContainerTypeId }; } }
        public void Register(UIApi.IScriptedContentFragmentController controller)
        {
            var options = new UIApi.ScriptedContentFragmentOptions(_CoriaMapManPanel_WidgetId)
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
            public string ExtensionName { get { return "coria_v1_panel_context"; } }
            public object GetExtension(NameValueCollection context)
            {
                Guid applicationId   = Guid.TryParse(context["applicationId"], out applicationId)     ? applicationId : Guid.Empty;
                Guid applicationType = Guid.TryParse(context["applicationTypeId"], out applicationType) ? applicationType : Guid.Empty;
                if (applicationId.Equals(Guid.Empty)) { return null; }
                return new MapPanelApi(applicationType, applicationId);
            }
        }
        
        [Documentation(Category = "Coria")]
        public class MapPanelApi
        {
            Guid _applicationType, _applicationId;//, contentType, _contentId;
            PublicApi.MapBook _mapbook;
            //PublicApi.Map _map;
            IList<PublicApi.Map> _maps;
            public MapPanelApi() { }
            public MapPanelApi(PublicApi.MapBook mapbook)
            {
                _mapbook = mapbook;
                //_maps = PublicApi.Maps.List(_mapbook.Group.Id.Value,_mapbook.SafeName, new PublicApi.MapsListOptions() { });
            }
            public MapPanelApi(Guid applicationType, Guid applicationId)
            {
                _applicationId = applicationId;
                _applicationType = applicationType;
                
                _mapbook = PublicApi.MapBooks.Get(_applicationId) as PublicApi.MapBook;
            }
            [Documentation("Map ")]
            public Guid SectionId { get { return _mapbook.Id; } }
            [Documentation("MapBook Application Guid ")]
            public Guid ApplicationId { get { return _applicationId; } }
            [Documentation("Coria Mapbook Type Guid")]
            public Guid ApplicationTypeId { get { return _applicationType; } }
            [Documentation("Coria Mapbook")]
            public PublicApi.MapBook Mapbook { get { return _mapbook; } }
        } 
    }
}

////This lambda expression is cool, but maybe hard to read to figure out how to modify it
/// PublicApi.ApplicationTypes.List().Where(c => Telligent.Evolution.Extensibility.Version1.PluginManager.Get<IWebContextualApplicationType>().Any(a => a.ApplicationTypeId == c.Id.GetValueOrDefault(Guid.Empty))).Select(c => c.Id.Value).ToArray(); 
/// so, here is the above code broken down:
/// Get "All" Telligent ApplicationTypes
//IEnumerable< TEApi.Entities.Version1.ApplicationType> all_teApplicationTypesIEnum = Telligent.Evolution.Extensibility.Api.Version1.PublicApi.ApplicationTypes.List();
////Get Web Page "Context" ApplicationTypes
//IEnumerable<IWebContextualApplicationType> page_webContextApplicationTypesIEnum = Telligent.Evolution.Extensibility.Version1.PluginManager.Get<IWebContextualApplicationType>();
////Get "Matching" ApplicationTypes, between "All" and "Context"
//IEnumerable<TEApi.Entities.Version1.ApplicationType> matching_teApplicationTypeListIEnum = all_teApplicationTypesIEnum.Where(c => page_webContextApplicationTypesIEnum.Any(a => a.ApplicationTypeId == c.Id.GetValueOrDefault(Guid.Empty)));
////Select "Guids" of  "Matching" ApplicationTypes
//IEnumerable<Guid> guidIEnum = matching_teApplicationTypeListIEnum.Select(c => c.Id.Value);
////Convert IEnum to Array
//Guid[] guidArray = guidIEnum.ToArray();
////Alternative: Convert to an IList
////IList<Guid> guidIList = guidIEnum.ToList();
////Add custom Application Type Guid
////guidIList.Add(Application.CoriaMapType._applicationTypeId);
////Convert to array
////Guid[] guidArray = guidIList.ToArray(); 