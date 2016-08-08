namespace kharta.coria.graphica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MapBook")]
    public partial class MapBook
    {
        public int Id { get; set; }

        public Guid? ApplicationTypeId { get; set; }

        public Guid ApplicationId { get; set; }

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

        public int? OntologyId { get; set; }

        public int GroupId { get; set; }

        [StringLength(256)]
        public string SafeName { get; set; }
    }
}
