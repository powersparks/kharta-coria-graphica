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
        InternalApi.KhartaOntology _khartaOntology = null;
        #region ApiEntity
        public KhartaOntology() : base() { }
        public KhartaOntology(AdditionalInfo additionalInfo) : base(additionalInfo) { }
        public KhartaOntology(IList<Warning> warnings, IList<Error> errors) : base(warnings, errors) { }
        internal KhartaOntology(InternalApi.KhartaOntology khartaOntology) : base() { _khartaOntology = khartaOntology; }
        #endregion
        #region IContainer
        public string AvatarUrl { get { return _khartaOntology.AvatarUrl; } }
        public Guid ContainerId { get { return _khartaOntology.ContainerId; } }
        public Guid ContainerTypeId { get { return _khartaOntology.ContainerTypeId; } }
        public bool IsEnabled { get { return _khartaOntology.IsEnabled.Value; } }
        public string Url { get { return _khartaOntology.Url; } }
        public string HtmlName(string target)
        {
            return _khartaOntology.HtmlName(target);
        }
        #endregion
        #region kharta.coria.graphica.Ontology
        public int Id { get { return _khartaOntology.Id; } }

        [StringLength(256)]
        public string Name { get { return _khartaOntology.Name; } }

        [StringLength(512)]
        public string Description { get { return _khartaOntology.Description; } }
        public int? ParentOntologyId { get { return _khartaOntology.ParentOntologyId.Value; } }

        [StringLength(256)]
        public string SafeName { get; set; }
         
        public Container ParentContainer
        {
            get
            {
                InternalApi.KhartaOntology  parentIContainer = InternalApi.OntologyDataService.getParentContainer(_khartaOntology.Id);
                KhartaOntology iContainer = new KhartaOntology(parentIContainer);
                
                Container container = new Container(iContainer);
                
                return container;
            }
        }
        #endregion
    }
}
