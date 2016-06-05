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
                teKharta.InternalApi.KhartaOntology container = newTestContainer();
                //new teKharta.InternalApi.KhartaOntology();
                string containerName = container.Name;
                string containerDescription = container.Description;
                container = teKharta.InternalApi.OntologyDataService.addUpdateContainer(container);
                id = container.Id;
                if (container.Name == containerName)
                {
                    container.Name = "Test Name Changed";

                    container =  teKharta.InternalApi.OntologyDataService.addUpdateContainer(container);
                }           
                Assert.IsTrue(id != 0 && id == container.Id && container.Name == "Test Name Changed");
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
            teKharta.InternalApi.KhartaOntology newContainer = newTestContainer();

            teKharta.InternalApi.KhartaOntology existingContainer = teKharta.InternalApi.OntologyDataService.addContainer(newContainer);
            teKharta.InternalApi.KhartaOntology container = teKharta.InternalApi.OntologyDataService.getContainer(existingContainer.Id );
            Assert.IsTrue(existingContainer.Id == container.Id);
        }
        internal teKharta.InternalApi.KhartaOntology newTestContainer() {
            teKharta.InternalApi.KhartaOntology container = new teKharta.InternalApi.KhartaOntology()
            {
                Id = 0,
                AvatarUrl = "https://s.gravatar.com/avatar/533c75456f1e9fa7d8e539bccdb3eff7?s=80",
                ContainerId = Guid.NewGuid(),
                ContainerTypeId = Guid.NewGuid(),
                Description = "test description",
                Name = "TestName",
                IsEnabled = true,
                Url = "/testurl"

            };
            return container;
        }
        [TestMethod]
        public void deleteContainerTest()
        {
             
            teKharta.InternalApi.KhartaOntology container = newTestContainer();
             
                container = teKharta.InternalApi.OntologyDataService.addContainer(container);
            if (container is teKharta.InternalApi.KhartaOntology && container.Id > 0)
            {
                
                 
                    teKharta.InternalApi.OntologyDataService.deleteContainer(container);
                 

            }
            else
            {
                Assert.Fail();
            }
            if (container.Id > 0)
            {
                teKharta.InternalApi.KhartaOntology container2 = teKharta.InternalApi.OntologyDataService.getContainer(container.Id);
                //Assert equals 0 meaning an object is always returned 
                Equals(container2.Id == 0);
            }
            else {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void addContainerTest()
        {
            var id = 0;
            try
            {
                teKharta.InternalApi.KhartaOntology container = newTestContainer();
                container = teKharta.InternalApi.OntologyDataService.addContainer( container); 
                id = container.Id;
                teKharta.InternalApi.OntologyDataService.deleteContainer(container);
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
