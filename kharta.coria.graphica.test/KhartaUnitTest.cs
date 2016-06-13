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
    public class KhartaApplicationsUnitTest {
        [TestMethod]
        public void getApplicationTest() {
            Assert.Fail();
        }
    }
    [TestClass]
    public class KhartaContainersUnitTest
    {
        [TestMethod]
        public void getContainerByGuidTest()
        {
            teKharta.InternalApi.KhartaOntology newContainer = newTestContainer();

            teKharta.InternalApi.KhartaOntology existingContainer = teKharta.InternalApi.OntologyDataService.addContainer(newContainer);
            teKharta.InternalApi.KhartaOntology container = teKharta.InternalApi.OntologyDataService.getContainerByGuid(existingContainer.ContainerId);
            Assert.IsTrue(existingContainer.Id == container.Id);
        }
        [TestMethod]
        public void getContainerTest()
        {
            teKharta.InternalApi.KhartaOntology newContainer = newTestContainer();

            teKharta.InternalApi.KhartaOntology existingContainer = teKharta.InternalApi.OntologyDataService.addContainer(newContainer);
            teKharta.InternalApi.KhartaOntology container = teKharta.InternalApi.OntologyDataService.getContainer(existingContainer.Id );
            Assert.IsTrue(existingContainer.Id == container.Id);
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

                    container = teKharta.InternalApi.OntologyDataService.addUpdateContainer(container);
                    teKharta.InternalApi.OntologyDataService.deleteContainer(container);
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
        /// <summary>
        /// test Creates new Source Application. 
        /// </summary>
        /// 
        [TestMethod]
        public void CreateInternalKhartaSourceTest() {

            teKharta.InternalApi.KhartaSource result = teKharta.InternalApi.SourceDataService.CreateNewSourceApplication(newSourceApplication());
            int id = result.Id;
            teKharta.InternalApi.SourceDataService.DeleteSourceApplication(result);
            Equals(id > 0);
        }
        [TestMethod]
        public void ReadInternalKhartaSourceTest() {
            teKharta.InternalApi.KhartaSource result = teKharta.InternalApi.SourceDataService.CreateNewSourceApplication(newSourceApplication());
            int id = result.Id;
            result = teKharta.InternalApi.SourceDataService.GetSourceApplication(id);
            int id2 = result.Id;
            teKharta.InternalApi.SourceDataService.DeleteSourceApplication(result);
            Equals(id == id2);

        }
        [TestMethod]
        public void UpdateInternalKhartaSourceTest() {
            teKharta.InternalApi.KhartaSource result = teKharta.InternalApi.SourceDataService.AddUpdateSourceApplication(newSourceApplication());
            int id = result.Id;
            Guid appId = result.ApplicationId;
            Guid appTypeId = result.ApplicationId;
            string avatarUrl = result.AvatarUrl;
            string description = result.Description;
            string name = result.Name;
            int ontologyId = result.OntologyId.Value;
            bool isEnabled = result.IsEnabled.Value;
            string SafeName = result.SafeName;
            string url = result.Url;
            int groupId = result.GroupId.Value;
             
            result.ApplicationId = Guid.NewGuid();
            result.ApplicationTypeId = Guid.NewGuid();
            result.AvatarUrl = "new url";
            result.Name = "New Name";
            result.Description = "new description";
            result.GroupId = 10;
            result.IsEnabled = false;
            result.SafeName = "new safe name";
            result.Url = "new url";
            teKharta.InternalApi.SourceDataService.DeleteSourceApplication(result);
           // result = teKharta.InternalApi.SourceDataService.AddUpdateSourceApplication(result);
           // need a test for what happens when entities are deleted and id doesn't exist
            result = teKharta.InternalApi.SourceDataService.GetSourceApplication(result.Id);
             
            int id2 = result.Id;
            Guid appId2 = result.ApplicationId;
            Guid appTypeId2 = result.ApplicationId;
            string avatarUrl2 = result.AvatarUrl;
            string description2 = result.Description;
            string name2 = result.Name;
            int ontologyId2 = result.OntologyId.Value;
            bool isEnabled2 = result.IsEnabled.Value;
            string SafeName2 = result.SafeName;
            string url2 = result.Url;
            int groupId2 = result.GroupId.Value;
  
             teKharta.InternalApi.SourceDataService.DeleteSourceApplication(result);

            Equals(  SafeName == SafeName2);
        }
        
        [TestMethod]
        public void DeleteInternalKhartaSourceTest() {
            teKharta.InternalApi.KhartaSource result = teKharta.InternalApi.SourceDataService.CreateNewSourceApplication(newSourceApplication());
            int id = result.Id;
            teKharta.InternalApi.SourceDataService.DeleteSourceApplication(result);
            result = teKharta.InternalApi.SourceDataService.GetSourceApplication(id);
            id = result.Id; 
            Equals(id == 0);
        }

        /// <summary>
        /// creates a new test container obect
        /// </summary>
        /// <returns>KhartaOntology</returns>
        internal teKharta.InternalApi.KhartaOntology newTestContainer()
        {
            teKharta.InternalApi.KhartaOntology container = new teKharta.InternalApi.KhartaOntology();



            container.AvatarUrl = "https://s.gravatar.com/avatar/533c75456f1e9fa7d8e539bccdb3eff7?s=80";
            container.ContainerId = Guid.NewGuid();
            container.ContainerTypeId = Guid.NewGuid();
            container.Description = "test description";
            container.Name = "TestName";
            container.IsEnabled = true;
            container.Url = "/testurl";
            container.ParentOntologyId = 1;
            container.SafeName = "testurl";
            

            return container;
        }
        /// <summary>
        /// Creates a new test application
        /// </summary>
        /// <returns></returns>
        internal teKharta.InternalApi.KhartaSource newSourceApplication() {
            teKharta.InternalApi.KhartaSource khartaSource = new te.extension.kharta.InternalApi.KhartaSource();
            khartaSource.ApplicationId = Guid.NewGuid();
            khartaSource.ApplicationTypeId = Guid.NewGuid();
            khartaSource.AvatarUrl = "https://s.gravatar.com/avatar/533c75456f1e9fa7d8e539bccdb3eff7?s=80";
            khartaSource.Description = "test application description";
            khartaSource.Name = "test application name";
            khartaSource.OntologyId = 1;
            khartaSource.GroupId = -1;
            khartaSource.IsEnabled = true;
            khartaSource.SafeName = "testapplicationname";




            return khartaSource;
        }
    }
}
