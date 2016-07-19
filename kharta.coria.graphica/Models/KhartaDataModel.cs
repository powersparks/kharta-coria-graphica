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
        public virtual DbSet<Coria_Map> Coria_Map { get; set; }
        public virtual DbSet<Hosting> Hostings { get; set; }
        public virtual DbSet<Ontology> Ontologies { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Transform> Transforms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
