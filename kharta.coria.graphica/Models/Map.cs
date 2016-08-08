namespace kharta.coria.graphica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Map
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MapId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid MapTypeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(256)]
        public string Title { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(512)]
        public string ThumbnailUrl { get; set; }

        [StringLength(256)]
        public string SafeName { get; set; }

        public string MapOptions { get; set; }

        public int? OntologyId { get; set; }

        public int? GroupId { get; set; }

        public int? CreateByUserId { get; set; }

        public DateTime? CreateUtcDate { get; set; }
    }
}
