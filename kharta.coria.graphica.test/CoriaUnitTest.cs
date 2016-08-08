using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
 
using kcgCore = kharta.coria.graphica;
using teKharta = te.extension.kharta;
using teCoria = te.extension.coria;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
namespace kharta.coria.graphica.test
{
    [TestClass]
    public class CoriaUnitTest
    {
        [TestMethod]
        public void NewCoriaMapTest()
        {
            teCoria.InternalApi.CoriaMapBook mapbook = new teCoria.InternalApi.CoriaMapBook();
            mapbook.Id = 1;
            mapbook.Name = "test";

            Assert.IsInstanceOfType(mapbook, typeof( kcgCore.Models.MapBook)); ;
        }
 
        [TestMethod]
        public void ConvertFromCoriaMapBookToMapBook()
        {
            teCoria.InternalApi.CoriaMapBook coriamapbook = new teCoria.InternalApi.CoriaMapBook();
            kcgCore.Models.MapBook mapbook = new kcgCore.Models.MapBook() ;
            coriamapbook.Id = 10;
            mapbook =   teCoria.InternalApi.CoriaDataService.ToMapBook(coriamapbook, new kcgCore.Models.MapBook());
            Assert.AreEqual(coriamapbook.Id, mapbook.Id);
        }
        [TestMethod]
        public void ConvertFromMapBookToCoriaMapBook()
        {
            teCoria.InternalApi.CoriaMapBook coriamapbook = new teCoria.InternalApi.CoriaMapBook();
            kcgCore.Models.MapBook mapbook = new kcgCore.Models.MapBook();
            mapbook.Id = 10;
            coriamapbook =  teCoria.InternalApi.CoriaDataService.ToCoriaMapBook(mapbook, new teCoria.InternalApi.CoriaMapBook());
            Assert.AreEqual(coriamapbook.Id, mapbook.Id);
        }

        [TestMethod]
        public void CreateCoriaMapBookTest()
        {
            teCoria.InternalApi.CoriaMapBook mapbook = new teCoria.InternalApi.CoriaMapBook();
            mapbook.ApplicationId = new Guid();
            mapbook.ApplicationTypeId = new Guid();
            mapbook.AvatarUrl = "";
            mapbook.Description = "testing description";
            mapbook.GroupId = 0;
            mapbook.Name = "test mapbook";
            mapbook = teCoria.InternalApi.CoriaDataService.CreateUpdateMapBook(mapbook);
            teCoria.InternalApi.CoriaDataService.DeleteCoriaMapBookApplication(mapbook);
            Assert.IsNotNull(mapbook);
        }
        [TestMethod]
        public void ReadCoriaMapTest() { Assert.Fail(); }

        [TestMethod]
        public void UpdateCoriaMapTest() { Assert.Fail(); }
        [TestMethod]
        public void DeleteCoriaMapTest() { Assert.Fail(); }
    }
}
