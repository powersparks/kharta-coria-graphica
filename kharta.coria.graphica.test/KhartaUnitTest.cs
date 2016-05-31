using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using kcgCore = kharta.coria.graphica;
using teKharta = te.extension.kharta;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace kharta.coria.graphica.test
{
  
    [TestClass]
    public class KhartaUnitTest
    {
        internal teKharta.InternalApi.KhartaOntology addContainer(kcgCore.Models.Ontology ontology)
        {
            
            teKharta.InternalApi.KhartaOntology container = new teKharta.InternalApi.KhartaOntology();
            Type ct = container.GetType();
            Type ot = ontology.GetType();
            IList<PropertyInfo> cprop = new List<PropertyInfo>(ct.GetProperties());
            IList<PropertyInfo> oprop = new List<PropertyInfo>(ot.GetProperties());
             
            using (var dbcontext = new kharta.coria.graphica.Models.KhartaDataModel())
            {
                var _ontology = dbcontext.Ontologies.Add(ontology);
                
                dbcontext.SaveChanges();
                foreach (PropertyInfo op in oprop) {
                   var value = op.GetValue(_ontology, null);
                    op.SetValue(container,value ,null);
                }
               
            }
            return  container;
        }
        [TestMethod]
        public void getContainerTest(int id)
        {

            Assert.Fail();
        }
        [TestMethod]
        public void deleteContainerTest()
        {

            Assert.Fail();
        }


        [TestMethod]
        public void addContainerTest()
        {
            var id = 0;
            try
            {
                kcgCore.Models.Ontology ontology = new kcgCore.Models.Ontology();
                teKharta.InternalApi.KhartaOntology container = new teKharta.InternalApi.KhartaOntology();
             
                container = teKharta.InternalApi.OntologyDataService.addContainer( container);// new teKharta.InternalApi.KhartaOntology();

               // container = teKharta.InternalApi.OntologyDataService.addUpdateContainer(container);
                id = container.Id;
                Debug.WriteLine("addContainer:");
                Debug.WriteLine("list id: " + container.Id);

                Assert.IsTrue( id != 0);
            }
            catch (Exception ex)
            {
                Assert.Fail();
                Debug.WriteLine("error: " + ex.Message);
            }
        }
        [TestMethod]
        public void getContainerTest()
        {
          int id = 0;
          var list = teKharta.InternalApi.OntologyDataService.getContainer(id);
            Debug.WriteLine("getContainer");
            Debug.WriteLine(list);
          
            Assert.AreEqual(list.Id, id);

        }
    }
}
