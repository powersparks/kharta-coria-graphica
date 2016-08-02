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
using System.ComponentModel.DataAnnotations.Schema;

namespace te.extension.kharta.PublicApi
{
    public class Source : ApiEntity, IApplication
    { 
        InternalApi.KhartaSource _khartaSource = null;
        #region ApiEntity
        public Source() : base() { }
        public Source(AdditionalInfo additionalInfo) : base(additionalInfo) { }
        public Source(IList<Warning> warnings, IList<Error> errors) : base(warnings, errors) { }
        internal Source(InternalApi.KhartaSource khartaSource) : base() { _khartaSource = khartaSource; }
        #endregion
        #region IApplication
        public Guid ApplicationId { get { return _khartaSource.ApplicationId; } }

        public Guid ApplicationTypeId { get { return _khartaSource.ApplicationTypeId; } }

        public string AvatarUrl { get { return _khartaSource.AvatarUrl; } }
        
        public IContainer Container
        {
            get
            {
                GroupsGetOptions groupOpt = new GroupsGetOptions();
                groupOpt.Id = _khartaSource.GroupId.HasValue ? _khartaSource.GroupId.Value:0;
                if (groupOpt.Id > 0)
                {
                    return Apis.Get<IGroups>().Get(groupOpt);
                }
                return Apis.Get<IGroups>().Root;
            }
        }
        public bool IsEnabled { get { return _khartaSource.IsEnabled != null ? _khartaSource.IsEnabled.Value : false; } }

        public string Url { get { return _khartaSource.Url; } }

        public string HtmlDescription(string target) { return _khartaSource.HtmlDescription(target); }

        public string HtmlName(string target) { return _khartaSource.HtmlName(target); }
        #endregion
    }
}
