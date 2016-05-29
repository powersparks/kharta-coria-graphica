using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using System.Runtime.CompilerServices;

namespace te.extension.kharta.PublicApi
{
    
    public class KhartaOntology : ApiEntity, IContainer
    {
        InternalApi.KhartaOntology _kartaOntology = null;
       

        public KhartaOntology()
			: base()
		{
        }

        public KhartaOntology(AdditionalInfo additionalInfo)
			: base(additionalInfo)
		{
        }

        public KhartaOntology(IList<Warning> warnings, IList<Error> errors)
			: base(warnings, errors)
		{
        }

        internal KhartaOntology(InternalApi.KhartaOntology khartaOntology)
			: base()
		{
            _kartaOntology = khartaOntology;
        }
        public string AvatarUrl
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Guid ContainerId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Guid ContainerTypeId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Url
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string HtmlName(string target)
        {
            throw new NotImplementedException();
        }
    }
}
