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

namespace te.extension.kharta.PublicApi
{
    public class KhartaSource : ApiEntity, IApplication
    {
        InternalApi.KhartaSource _khartaSource = null;
        #region ApiEntity
        public KhartaSource() : base() { }
        public KhartaSource(AdditionalInfo additionalInfo) : base(additionalInfo) { }
        public KhartaSource(IList<Warning> warnings, IList<Error> errors) : base(warnings, errors) { }
        internal KhartaSource(InternalApi.KhartaSource khartaSource) : base() { _khartaSource = khartaSource; }
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
