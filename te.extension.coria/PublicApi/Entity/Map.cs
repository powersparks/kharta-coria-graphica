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

namespace te.extension.coria.PublicApi
{
    public class Map : ApiEntity, IContent
    {
        private InternalApi.CoriaMap _map = null;
        #region ApiEntity
        public Map() : base() { }
        public Map(AdditionalInfo additionalInfo) : base(additionalInfo) { }
        public Map(IList<Warning> warnings, IList<Error> errors) : base(warnings, errors) { }
        internal Map(InternalApi.CoriaMap map) : base() { _map = map; }
        #endregion
        #region IContent
        public IApplication Application { get { return PublicApi.MapBooks.Get(_map.MapTypeId); } }
        public string Name { get { return _map.Title; } }
        public string Title { get { return _map.Title; } }
        public string Description { get { return _map.Description; } }
        public string AvatarUrl { get { return _map.ThumbnailUrl; } }
        public Guid MapId { get { return _map.MapId; } }
        public Guid MapTypeId { get { return _map.MapTypeId; } }

        public Guid ContentId { get { return _map.MapId; } }

        public Guid ContentTypeId { get { return _map.MapTypeId; } }

        public int? CreatedByUserId { get { return _map.CreateByUserId.Value; } }

        public DateTime CreatedDate { get { return _map.CreateUtcDate.HasValue ? _map.CreateUtcDate.Value : DateTime.UtcNow; } }
        public int? ModifiedByUserId { get { return _map.ModifiedByUserId.Value; } }
        public DateTime ModifiedDate { get { return _map.ModifiedUtcDate.HasValue ? _map.ModifiedUtcDate.Value : DateTime.UtcNow; } }
        public bool IsEnabled { get { return true; } }
        public bool IsIndexed { get { return _map.IsIndexed; } }
        public int MapBookId { get{ return _map.CoriaMapBook.Id; } }
        public string Url { get { return  _map.CoriaMapBook.SafeName + "/map/"+ _map.MapId.ToString("N") ; } }

        public string HtmlDescription(string target) { return _map.Description; }

        public string HtmlName(string target)  { return _map.Title; }
        #endregion
    }
}
