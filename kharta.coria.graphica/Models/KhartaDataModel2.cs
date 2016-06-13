namespace kharta.coria.graphica.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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
