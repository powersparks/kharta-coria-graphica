using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("te.extension.kharta.InternalApi")]
[assembly: InternalsVisibleTo("kharta.coria.graphica.test")]
namespace kharta.coria.graphica.Models
{
    
    public partial class KhartaDataModel : DbContext
    {
        public KhartaDataModel()
            : base("name=KhartaDataModel")
        {
        }

        public virtual DbSet<Ontology> Ontologies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
