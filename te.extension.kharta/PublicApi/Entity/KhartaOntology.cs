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
     
    
    public class KhartaOntology : ApiEntity, IContainer
    {
        InternalApi.KhartaOntology _kartaOntology = new InternalApi.KhartaOntology();
         //public Container(AdditionalInfo additionalInfo);
        //public Container(Error error);
        //public Container(Warning warning);
        //public Container(IContainer container);
        //public Container(IList<Warning> warnings, IList<Error> errors);

       
      
       
         
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
        [StringLength(512)]
        public string AvatarUrl
        {
            get
            {
                return _kartaOntology.AvatarUrl;
            }
        }

        public Guid ContainerId
        {
            get
            {
                return _kartaOntology.ContainerId.Value;
            }
        }

        public Guid ContainerTypeId
        {
            get
            {
                return _kartaOntology.ContainerTypeId.Value;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _kartaOntology.IsEnabled.Value;
            }
        }

        [StringLength(512)]
        public string Url
        {
            get
            {
                return _kartaOntology.Url;
            }
        }
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get { return _kartaOntology.Name; } }

        [StringLength(512)]
        public string Description { get { return _kartaOntology.Description; } }


        public Container ParentContainer { get {
                Container container = null;
                //container.ParentContainer = _kartaOntology.ParentContainer;

                return container; } }

        public string HtmlName(string target)
        {
            return _kartaOntology.HtmlName(target);
        }
    }
}
