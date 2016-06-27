using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kharta.coria.graphica.Models;
using System.Runtime.CompilerServices;
using System.Diagnostics;

using System.Reflection;
using PublicApi = te.extension.kharta.PublicApi;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;

[assembly: InternalsVisibleTo("kharta.coria.graphica.test")]
namespace te.extension.kharta.InternalApi
{

    internal class OntologyDataService
    {
        private static Ontology toOntology(KhartaOntology container)
        {
            Ontology ontology = FromContainer(container);
            return ontology;
        }

   
      

        private static Ontology FromContainer(KhartaOntology container)
        {
            Ontology ontology = new Ontology();
            if (container != null)
            {
                //Type ct = container.GetType();
                Type ot = ontology.GetType();
                // IList<PropertyInfo> cprop = new List<PropertyInfo>(ct.GetProperties());
                IList<PropertyInfo> oprop = new List<PropertyInfo>(ot.GetProperties());
                foreach (PropertyInfo op in oprop)
                {
                    var value = op.GetValue(container, null);
                    op.SetValue(ontology, value, null);
                }
            }
            return ontology;
        }
        private static KhartaOntology ToContainer(Ontology ontology)
        {
            KhartaOntology khartaOntolgy = FromOntology(ontology);
            return khartaOntolgy;
        }
        private static KhartaOntology FromOntology(Ontology ontology)
        {
            KhartaOntology container = new KhartaOntology();
            if (ontology != null)
            { 
                 
                Type ot = ontology.GetType();
                 
                IList<PropertyInfo> oprop = new List<PropertyInfo>(ot.GetProperties());
                foreach (PropertyInfo op in oprop)
                {
                    var value = op.GetValue(ontology, null);
                    op.SetValue(container, value, null);
                }
            }
            return container;
        }

        internal static void deleteContainer(KhartaOntology container)
        {
           // Ontology ontology = FromContainer(container);
            using (var dbcontext = new KhartaDataModel()) {
                var result = (from o in dbcontext.Ontologies
                             where o.Id.Equals(container.Id)
                             select o).FirstOrDefault();
                Ontology ontology = result;
               // dbcontext.Ontologies.i.Select(ontology);
                dbcontext.Ontologies.Remove(ontology);
                dbcontext.SaveChanges();
            }
        }
        internal static void deleteContainers(List<KhartaOntology> containers)
        {
            IList<Ontology> ontologies = FromContainers(containers);
            using (var dbcontext = new KhartaDataModel())
            {
               // IList<Ontology> ontologies = new List<Ontology>(); // (new { ontology });
               // ontologies.Add(ontology);
                var db = dbcontext.Ontologies.RemoveRange(ontologies);
                dbcontext.SaveChanges();
            }
        }

        private static IList<Ontology> FromContainers(List<KhartaOntology> containers)
        {
            //not sure this can work... since it was next to impossible to cast Containers to the base type Ontology 
            IList<Ontology> ontologies = containers.Cast<Ontology>().ToList(); 
            return ontologies;
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
                if (ontology != null)
                {
                    container = toContainer(ontology);
                }
            }
            
            return container;
        }
        internal static KhartaOntology getParentContainer(int id)
        {
            Func<Ontology, KhartaOntology> toContainer = (Ontology fromOntology) => FromOntology(fromOntology);
            KhartaOntology container = new KhartaOntology();
            using (var dbcontext = new KhartaDataModel())
            {
                var id2 = from o in dbcontext.Ontologies
                             where o.Id.Equals(id)
                             select o.ParentOntologyId.Value;
                int pId = id2.FirstOrDefault();
                var result = from o in dbcontext.Ontologies
                             where o.ParentOntologyId.Value.Equals(pId)
                             select o;
                Ontology ontology = result.FirstOrDefault();
                if (ontology != null)
                {
                    container = toContainer(ontology);
                }
            }

            return container;
        }

        internal static KhartaOntology getContainerByGuid(Guid id)
        {
            
            Func<Ontology, KhartaOntology> toContainer = (Ontology fromOntology) => FromOntology(fromOntology);
            KhartaOntology container = new KhartaOntology();
            using (var dbcontext = new KhartaDataModel())
            {
                var result = from o in dbcontext.Ontologies
                             where o.ContainerId == id
                             select o;
                Ontology ontology = result.FirstOrDefault();
                if (ontology != null)
                {
                    container = toContainer(ontology);
                }
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
                    var currentContainer = containers.FirstOrDefault();
                    
                    
                    foreach (PropertyInfo op in oprop)
                    {
                        if(op.CanWrite){
                            var value = op.GetValue(_ontology, null);
                            op.SetValue(currentContainer, value, null);
                        }
                    }
                    dbcontext.SaveChanges();
                   container = ToContainer(currentContainer);
                }
            }

            return container;
        }

        internal static KhartaOntology getContainerByGuidType(Guid containerTypeId, Guid containerId)
        {
            Func<Ontology, KhartaOntology> toContainer = (Ontology fromOntology) => FromOntology(fromOntology);
            KhartaOntology container = new KhartaOntology();
            using (var dbcontext = new KhartaDataModel())
            {
                var result = from o in dbcontext.Ontologies
                             where o.ContainerTypeId == containerTypeId && o.ContainerId == containerId
                             select o;
                Ontology ontology = result.FirstOrDefault();
                if (ontology != null)
                {
                    container = toContainer(ontology);
                }
            }

            return container;
        }

        internal static IList<KhartaOntology> getContainers() {
            Func<Ontology, KhartaOntology> toContainer = (Ontology fromOntology) => FromOntology(fromOntology);
            IList<KhartaOntology> containers = new List<KhartaOntology>();
            IList<Ontology> _ontologies = new List<Ontology>();
            using (var dbcontext = new KhartaDataModel()) {
                 
                var ontologies = from o in dbcontext.Ontologies
                                 select o;
                // _ontologies = new ApiList<Ontology>(ListKhartaOntology().Select(x => new Ontology(_khartaOntology)));

                _ontologies = ontologies.ToList();
            }
            containers = new List<KhartaOntology>(_ontologies.Select(_ontology => toContainer(_ontology)));


            return containers;
        }
    }
     
}
