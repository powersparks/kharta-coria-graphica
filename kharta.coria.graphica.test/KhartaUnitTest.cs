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
        [TestMethod]
        public void addUpdateContainerTest()
        {
            var id = 0;
            try
            {
                kcgCore.Models.Ontology ontology = new kcgCore.Models.Ontology();
                teKharta.InternalApi.KhartaOntology container = new teKharta.InternalApi.KhartaOntology();

                container = teKharta.InternalApi.OntologyDataService.addUpdateContainer(container);
                  
                id = container.Id;
                Debug.WriteLine("addContainer:");
                Debug.WriteLine("list id: " + container.Id);

                Assert.IsTrue(id != 0);
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
            int id = 1;
            teKharta.InternalApi.KhartaOntology container = teKharta.InternalApi.OntologyDataService.getContainer(id);
            Assert.IsTrue(id == container.Id);
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
      
    }
}
