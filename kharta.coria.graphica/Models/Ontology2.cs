namespace kharta.coria.graphica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ontology")]
    public partial class Ontology
    {
        public int Id { get; set; }

        public Guid ContainerTypeId { get; set; }

        public Guid ContainerId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(512)]
        public string AvatarUrl { get; set; }

        public bool? IsEnabled { get; set; }

        [StringLength(512)]
        public string Url { get; set; }

        public int? ParentOntologyId { get; set; }

        [StringLength(256)]
        public string SafeName { get; set; }
    }
}
