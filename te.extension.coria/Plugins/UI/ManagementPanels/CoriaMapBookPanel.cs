﻿using System;
using System.Collections.Generic;
using System.Web;
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

namespace te.extension.coria.Plugins.UI.ManagementPanels
{
    public class CoriaMapBookPanel : IPlugin, IApplicationPanel, UIApi.IScriptablePlugin //, IContainerPanel
    {
        Guid _CoriaMapBookManPanel_WidgetId = new Guid("99f22a55-f1f4-4584-8a76-dd0a64452d6b");
         
        public static readonly Guid _scriptedContentFragmentFactoryDefaultIdentifier = new Guid("23098a95-0892-41cd-bb3e-fa7f78cdd21b");

        public static Guid _panelId = new Guid("a06a4d37-82d6-42a4-b20c-140ffd882677");
        public static Guid _applicationPanel = new Guid("c4315566-7dcc-46b3-9ab7-7715d05498ad");
        UIApi.IScriptedContentFragmentController _iScriptedContentFragmentController;
        #region IPlugin
        public string Name { get { return "MapBook Management Panel"; } }
        public string Description { get { return "panel to better manage mapbooks"; } }
        public void Initialize() { }
        #endregion
        #region IApplicationPanel
        public Guid[] ApplicationTypes { get { return new Guid[] { Application.CoriaMapBookType._applicationTypeId }; } }
        public string CssClass { get { return _iScriptedContentFragmentController.GetMetadata(_CoriaMapBookManPanel_WidgetId).CssClass; } }
        public int? DisplayOrder { get { return 3; } }
        public bool IsCacheable { get { return _iScriptedContentFragmentController.GetMetadata(_CoriaMapBookManPanel_WidgetId).IsCacheable; ; } }
        public Guid PanelId { get { return _panelId; } }
        public bool VaryCacheByUser { get { return true; } }
        public string GetPanelDescription(Guid applicationType, Guid applicationId) { return "update get panel description"; }
        public string GetPanelName(Guid applicationType, Guid applicationId) { return "Map Book App Panel"; }
        public string GetViewHtml(Guid type, Guid id) { return _iScriptedContentFragmentController.RenderContent(_CoriaMapBookManPanel_WidgetId, new NameValueCollection() { { "ApplicationTypeId", type.ToString() }, { "ApplicationId", id.ToString() } }); }
        public bool HasAccess(int userId, Guid applicationType, Guid applicationId) { return true; }
        #endregion

        public Guid ScriptedContentFragmentFactoryDefaultIdentifier { get { return _scriptedContentFragmentFactoryDefaultIdentifier; } } //_CoriaMapBookManPanel_WidgetId; } }
        public Guid[] ContainerTypes { get { return new Guid[] { Apis.Get<IGroups>().ContainerTypeId }; } }
        public void Register(UIApi.IScriptedContentFragmentController controller)
        {
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
            public string ExtensionName { get { return "coria_v1_panel_context"; } }
            public object GetExtension(NameValueCollection context)
            {
                //https://harleyparks.com/departments/fuzzy/mapbooks/mapbook#_cptype=panel&_cpcontexttype=Container&_cppanelid=c4315566-7dcc-46b3-9ab7-7715d05498ad

                Guid applicationId   = Guid.TryParse(context["applicationId"], out applicationId)     ? applicationId : Guid.Empty;
                if (applicationId.Equals(Guid.Empty)) {  return null; }
                Guid applicationType = Guid.TryParse(context["applicationTypeId"], out applicationType) ? applicationType : Guid.Empty;
                string mapbookSafeName = context["mapBook"];
                //this url service needs to be in the internalapi url service
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                //parameters.Add("mapBook", m.SafeName.ToString());
                //parameters.Add("_cptype", "panel");
                //http://localhost/departments/fuzzy/mapbooks/
                //admin panel, Manage MapBook
                //#_cptype=category&_cpcontexttype=Application&_cpcontextid=bfdb6103-e8e5-4cbf-8fbf-42dbac4046ef
                //
                //#_cptype=panel&_cpcontexttype=Application&_cppanelid=a06a4d37-82d6-42a4-b20c-140ffd882677

                NameValueCollection param = HttpContext.Current.Request.Params;
                var _cptype = param["_cptype"];
                var _cpcontexttype = param["_cpcontexttype"];// Application 
                var _cppanelid = param["_cppanelid"];
                Uri requestUri = HttpContext.Current.Request.Url;
                var hash = requestUri.GetHashCode();
                //_cptype = root, if so, then we need to provide managment of specific mapbooks.
                return new MapBookPanelApi(applicationType, applicationId);
            }
        }
        
        [Documentation(Category = "Coria")]
        public class MapBookPanelApi
        {
            Guid _applicationType, _applicationId;//, contentType, _contentId;
            PublicApi.MapBook _mapbook;
            //PublicApi.Map _map;
            IList<PublicApi.Map> _maps;
            public MapBookPanelApi() { }
            public MapBookPanelApi(PublicApi.MapBook mapbook)
            {
                _mapbook = mapbook;
                _maps = PublicApi.Maps.List(_mapbook.Group.Id.Value,_mapbook.SafeName, new PublicApi.MapsListOptions() { });
            }
            public MapBookPanelApi(Guid applicationType, Guid applicationId)
            {
                _applicationId = applicationId;
                _applicationType = applicationType;
                
                _mapbook = PublicApi.MapBooks.Get(_applicationId) as PublicApi.MapBook;
            }
            [Documentation("Map Book ")]
            public Guid SectionId { get { return _mapbook.Id; } }
            [Documentation("Map Book Application Guid ")]
            public Guid ApplicationId { get { return _applicationId; } }
            [Documentation("Coria Map Book Type Guid")]
            public Guid ApplicationTypeId { get { return _applicationType; } }
            [Documentation("Coria Map Book")]
            public PublicApi.MapBook MapBook { get { return _mapbook; } }
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