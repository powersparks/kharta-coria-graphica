using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;



 
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
