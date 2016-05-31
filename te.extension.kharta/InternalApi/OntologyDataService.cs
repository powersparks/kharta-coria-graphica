using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kharta.coria.graphica.Models;
using System.Runtime.CompilerServices;
using System.Diagnostics;

using System.Reflection;
 
[assembly: InternalsVisibleTo("kharta.coria.graphica.test")]
namespace te.extension.kharta.InternalApi
{

    internal class OntologyDataService
    {
        private static Ontology FromContainer(KhartaOntology container)
        {
            Ontology ontology = new Ontology();
            //Type ct = container.GetType();
            Type ot = ontology.GetType();
            // IList<PropertyInfo> cprop = new List<PropertyInfo>(ct.GetProperties());
            IList<PropertyInfo> oprop = new List<PropertyInfo>(ot.GetProperties());
            foreach (PropertyInfo op in oprop)
            {
                var value = op.GetValue(container, null);
                op.SetValue(ontology, value, null);
            }
            return ontology;
        }
        private static KhartaOntology FromOntology(Ontology ontology)
        {
            KhartaOntology container = new KhartaOntology();
            //Type ct = container.GetType();
            Type ot = ontology.GetType();
            // IList<PropertyInfo> cprop = new List<PropertyInfo>(ct.GetProperties());
            IList<PropertyInfo> oprop = new List<PropertyInfo>(ot.GetProperties());
            foreach (PropertyInfo op in oprop)
            {
                var value = op.GetValue(ontology, null);
                op.SetValue( container, value, null);
            }
            return container;
        }

        /// <summary>
        /// A "find", unlike "get", makes a call once to the database,
        /// cache results are kept in the context until saved. If the current 
        /// record is needed, use a "get". Composite keys are allowed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        internal static IList<KhartaOntology> findContainer(int id)
        {
            IList<KhartaOntology> kcgContainers = new List<KhartaOntology>();
            using (var dbcontext = new KhartaDataModel() )
            {
                var containers = dbcontext.Ontologies.Find(id);


            }
            return null;
        }

        /// <summary>
        /// A "Get" makes fresh call to the database for each record
        /// one  Ontology container is provided at atime
        /// </summary>
        /// <returns></returns>
        internal static KhartaOntology getContainer(int id)
        { 
            Func<Ontology, KhartaOntology> toContainer = (Ontology fromOntology) => FromOntology(fromOntology);
            KhartaOntology container = new KhartaOntology();
            using (var dbcontext = new KhartaDataModel())
            {
                var result = from o in dbcontext.Ontologies
                             where o.Id.Equals(id)
                             select o;
                Ontology ontology = result.FirstOrDefault();

                 container = toContainer(ontology);
            }
            
            return container;
        }

       
        internal static KhartaOntology addContainer(KhartaOntology container)
        {
            Func<KhartaOntology, Ontology> toOntology = (KhartaOntology fromContainer) => FromContainer(fromContainer);
            Ontology _ontology = toOntology(container);

            //KhartaOntology container = new KhartaOntology();
            Type ct = container.GetType();
            Type ot = _ontology.GetType();
            IList<PropertyInfo> cprop = new List<PropertyInfo>(ct.GetProperties());
            IList<PropertyInfo> oprop = new List<PropertyInfo>(ot.GetProperties());

            using (var dbcontext = new KhartaDataModel())
            {
                 _ontology = dbcontext.Ontologies.Add(_ontology);

                dbcontext.SaveChanges();
                foreach (PropertyInfo op in oprop)
                {
                    var value = op.GetValue(_ontology, null);
                    op.SetValue(container, value, null);
                }

            }
            return container;
        }
        internal static KhartaOntology addUpdateContainer(KhartaOntology container)
        {
            Func<KhartaOntology, Ontology> toOntology = (KhartaOntology fromContainer) => FromContainer(fromContainer);
            Ontology _ontology = toOntology(container);

            //Ontology _ontology = (Ontology) container;// new Ontology() ;//(Ontology) container;
            int id = container.Id;
            // KhartaOntology container = new KhartaOntology();
            Type ct = container.GetType();
            Type ot = _ontology.GetType();
            IList<PropertyInfo> cprop = new List<PropertyInfo>(ct.GetProperties());
            IList<PropertyInfo> oprop = new List<PropertyInfo>(ot.GetProperties());

            //KhartaOntology container = new KhartaOntology();
            if (id == 0)
            {
                try
                {
                   
                    using (var dbcontext = new KhartaDataModel())
                    {
                         _ontology = dbcontext.Ontologies.Add(_ontology);

                        dbcontext.SaveChanges();
                        foreach (PropertyInfo op in oprop)
                        {
                            var value = op.GetValue(_ontology, null);
                            op.SetValue(container, value, null);
                        }
 
                    }
                    return container;
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            }
            else {
                using (var dbcontext = new KhartaDataModel())
                {
                    var containers = from o in dbcontext.Ontologies
                                     where o.Id.Equals(_ontology.Id)
                                     select o;
                    _ontology = containers.FirstOrDefault();
                    
                    dbcontext.SaveChanges();
                    foreach (PropertyInfo op in oprop)
                    {
                        var value = op.GetValue(_ontology, null);
                        op.SetValue(container, value, null);
                    }
                }
            }

            return container;
        }

        internal static IList<KhartaOntology> getContainers() {
            IList<KhartaOntology> containers = new List<KhartaOntology>();

            using (var dbcontext = new KhartaDataModel()) {
                var ontologies = from o in dbcontext.Ontologies
                                 select o;
                containers = (IList<KhartaOntology>)ontologies.ToList().Cast<Ontology>();

            }
                return containers;
        }
    }
     
}
