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
    public class Map : ApiEntity
    {
        private InternalApi.Map _map = null;

      
        #region ApiEntity
        public Map() : base() { }
        public Map(AdditionalInfo additionalInfo) : base(additionalInfo) { }
        public Map(IList<Warning> warnings, IList<Error> errors) : base(warnings, errors) { }
        internal Map(InternalApi.Map map) : base() { _map = map; }
        #endregion

    }
}
