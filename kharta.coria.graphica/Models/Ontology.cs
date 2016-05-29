using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("te.extension.kharta.InternalApi")]
[assembly: InternalsVisibleTo("kharta.coria.graphica.test")]
namespace kharta.coria.graphica.Models
{

    [Table("Ontology")]
   public partial class Ontology
    {
        public int Id { get; set; }

        public Guid? ContainerTypeId { get; set; }

        public Guid? ContainerId { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(512)]
        public string AvatarUrl { get; set; }

        public bool? IsEnabled { get; set; }

        [StringLength(512)]
        public string Url { get; set; }
    }
}
