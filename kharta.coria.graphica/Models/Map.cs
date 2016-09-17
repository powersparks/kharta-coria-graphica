namespace kharta.coria.graphica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Map")]
    public partial class Map
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid MapId { get; set; }

        public Guid MapTypeId { get; set; }

        [Required]
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

        public int? ModifiedByUserId { get; set; }

        public DateTime? CreateUtcDate { get; set; }

        public DateTime? ModifiedUtcDate { get; set; }

        public bool IsIndexed { get; set; }

        public virtual MapBook MapBook { get; set; }
    }
}
