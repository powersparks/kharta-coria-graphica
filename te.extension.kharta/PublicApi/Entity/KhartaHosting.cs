using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using System.Runtime.CompilerServices;
using kcgModels = kharta.coria.graphica.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace te.extension.kharta.PublicApi.Entity
{
    public class KhartaHosting : ApiEntity, IApplication
    {
        InternalApi.KhartaHosting _khartaHosting = null;
    #region ApiEntity
    public KhartaHosting() : base() { }
    public KhartaHosting(AdditionalInfo additionalInfo) : base(additionalInfo) { }
    public KhartaHosting(IList<Warning> warnings, IList<Error> errors) : base(warnings, errors) { }
    internal KhartaHosting(InternalApi.KhartaHosting khartaHosting) : base() { _khartaHosting = khartaHosting; }
    #endregion
    #region IApplication
    public Guid ApplicationId { get { throw new NotImplementedException(); } }

    public Guid ApplicationTypeId { get { throw new NotImplementedException(); } }

    public string AvatarUrl { get { throw new NotImplementedException(); } }

    public IContainer Container { get { throw new NotImplementedException(); } }

    public bool IsEnabled { get { throw new NotImplementedException(); } }

    public string Url { get { throw new NotImplementedException(); } }

    public string HtmlDescription(string target) { throw new NotImplementedException(); }

    public string HtmlName(string target) { throw new NotImplementedException(); }
    #endregion
}
}
