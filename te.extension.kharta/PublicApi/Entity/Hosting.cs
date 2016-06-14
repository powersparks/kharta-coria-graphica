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
    public class Hosting : ApiEntity, IApplication
    {
        InternalApi.KhartaHosting _khartaHosting = null;
    #region ApiEntity
    public Hosting() : base() { }
    public Hosting(AdditionalInfo additionalInfo) : base(additionalInfo) { }
    public Hosting(IList<Warning> warnings, IList<Error> errors) : base(warnings, errors) { }
    internal Hosting(InternalApi.KhartaHosting khartaHosting) : base() { _khartaHosting = khartaHosting; }
    #endregion
    #region IApplication
    public Guid ApplicationId { get { return _khartaHosting.ApplicationId; } }

    public Guid ApplicationTypeId { get { return _khartaHosting.ApplicationTypeId; } }

    public string AvatarUrl { get { return _khartaHosting.AvatarUrl; } }

    public IContainer Container { get
            {
                GroupsGetOptions groupOpt = new GroupsGetOptions();
                groupOpt.Id = _khartaHosting.GroupId.Value;
                if (groupOpt.Id > 0)
                {
                    return Apis.Get<IGroups>().Get(groupOpt);
                }
                return Apis.Get<IGroups>().Root;
            } }

    public bool IsEnabled { get { return _khartaHosting.IsEnabled.Value; } }

    public string Url { get { return _khartaHosting.Url; } }

    public string HtmlDescription(string target) { return _khartaHosting.HtmlDescription(target); }

    public string HtmlName(string target) { return _khartaHosting.HtmlName(target); }
    #endregion
}
}
