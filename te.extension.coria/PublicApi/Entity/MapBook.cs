using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;

using Telligent.Evolution.Extensibility;

using System.Runtime.CompilerServices;
using kcgModels = kharta.coria.graphica.Models;
using System.ComponentModel.DataAnnotations;
using te.extension.coria.InternalApi;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
namespace te.extension.coria.PublicApi
{
    public class MapBook: ApiEntity, IApplication
    {
        private InternalApi.CoriaMapBook _mapbook = null;


        #region ApiEntity
        public MapBook() : base() { }
        public MapBook(AdditionalInfo additionalInfo) : base(additionalInfo) { }
        public MapBook(IList<Warning> warnings, IList<Error> errors) : base(warnings, errors) { }
        internal MapBook(InternalApi.CoriaMapBook mapbook) : base() { _mapbook = mapbook; }
        
        #endregion
        
        #region IApplication
        public string Name { get { return _mapbook.Name; } }
        public Guid Id { get { return _mapbook.ApplicationId; } }
        public int ObjectId { get { return _mapbook.Id; } }
        public Guid ApplicationId { get { return _mapbook.ApplicationId; } }

        public Guid ApplicationTypeId { get { return _mapbook.ApplicationTypeId.Value; } }

        public string AvatarUrl { get { return _mapbook.AvatarUrl; } }

        public IContainer Container
        {
            get
            {
                GroupsGetOptions groupOpt = new GroupsGetOptions();
                groupOpt.Id = _mapbook.GroupId;
                if (groupOpt.Id > 0)
                {
                    return Apis.Get<IGroups>().Get(groupOpt);
                }
                return Apis.Get<IGroups>().Root;
            }
        }
        public Group Group
        {
            get {
                GroupsGetOptions groupOpt = new GroupsGetOptions();
                groupOpt.Id = _mapbook.GroupId;
                if (groupOpt.Id > 0)
                {
                    Group group = Apis.Get<IGroups>().Get(groupOpt);
                    return group;
                }
                Group groupRoot = Apis.Get<IGroups>().Root;
                return groupRoot;
            }
        }

        internal static MapBook Get(string mapBookName)
        {
            throw new NotImplementedException();
        }

        internal static MapBook Get(int groupId, string mapBookName)
        {
            MapBook mapbook = new MapBook();
            mapbook = CoriaDataService.GetMapBookByGroupId_Name(groupId, mapBookName);
            return mapbook;
        }

        public bool IsEnabled { get { return _mapbook.IsEnabled != null ? _mapbook.IsEnabled.Value : false; } }

        public string Url { get {
                //string groupUrl = TEApi.Groups.Get(new GroupsGetOptions { Id = _mapbook.GroupId }).Url;
                //groupUrl + "maps/" +
                return _mapbook.Url; } }

        public string HtmlDescription(string target) { return _mapbook.HtmlDescription(target); }

        public string HtmlName(string target) { return _mapbook.HtmlName(target); }
        #endregion
    }
}
