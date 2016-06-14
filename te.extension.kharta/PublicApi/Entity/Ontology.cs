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
     
    
    public class Ontology : ApiEntity, IContainer, IApplication
    {
        InternalApi.KhartaOntology _khartaOntology = null;
        #region ApiEntity
        public Ontology() : base() { }
        public Ontology(AdditionalInfo additionalInfo) : base(additionalInfo) { }
        public Ontology(IList<Warning> warnings, IList<Error> errors) : base(warnings, errors) { }
        internal Ontology(InternalApi.KhartaOntology khartaOntology) : base() { _khartaOntology = khartaOntology; }
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

        public string HtmlDescription(string target)
        {
            throw new NotImplementedException();
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
                Ontology iContainer = new Ontology(parentIContainer);
                
                Container container = new Container(iContainer);
                
                return container;
            }
        }

        public Guid ApplicationId
        {
            get
            {
                return _khartaOntology.ContainerId;
                //throw new NotImplementedException();
            }
        }

        public Guid ApplicationTypeId
        {
            get
            {
                return _khartaOntology.ContainerTypeId;//throw new NotImplementedException();
            }
        }

        public IContainer Container
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
