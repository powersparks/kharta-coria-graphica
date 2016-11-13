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
        public void NewCoriaMapBookTest()
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
            mapbook.ApplicationId = Guid.NewGuid();
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
        public void CreateCoriaMapTest()
        {

            //teCoria.PublicApi.Map map = new teCoria.PublicApi.Map();
            //teCoria.InternalApi.CoriaMap cMap = new teCoria.InternalApi.CoriaMap();
            //cMap = teCoria.InternalApi.CoriaDataService.CreateUpdateMap(cMap);
            //kharta.coria.graphica.Models.Map map = new Models.Map();
            teCoria.InternalApi.CoriaMap map = new teCoria.InternalApi.CoriaMap();
            Models.MapBook mapbook = new Models.MapBook();
            using (kharta.coria.graphica.Models.KhartaDataModel dbcontext = new kharta.coria.graphica.Models.KhartaDataModel())
            {
                mapbook = (from m in dbcontext.MapBooks
                           where m.GroupId.Equals(8)
                           select m).First();

            }
            map.Title = "new test map";
            map.Description = "description is not required, Title, MapId, MapTypeId are required";
            map.MapId = Guid.NewGuid();
            map.MapTypeId = mapbook.ApplicationId;
            map.CreateByUserId = 2100;

            map = te.extension.coria.InternalApi.CoriaDataService.CreateCoriaMap(map);
            Assert.IsTrue(map.Id > 0);
            te.extension.coria.InternalApi.CoriaDataService.DeleteCoriaMap(map.MapId);
        }

        [TestMethod]
        public void CreateMapTest() {

            //teCoria.PublicApi.Map map = new teCoria.PublicApi.Map();
            //teCoria.InternalApi.CoriaMap cMap = new teCoria.InternalApi.CoriaMap();
            //cMap = teCoria.InternalApi.CoriaDataService.CreateUpdateMap(cMap);
            kharta.coria.graphica.Models.Map map = new Models.Map();
            Models.MapBook mapbook = new Models.MapBook();
            using (kharta.coria.graphica.Models.KhartaDataModel dbcontext = new kharta.coria.graphica.Models.KhartaDataModel())
            {
                 mapbook = (from m in dbcontext.MapBooks
                                          where m.GroupId.Equals(8)
                                          select m).First();

            }
                map.Title = "new test map";
            map.Description = "description is not required, Title, MapId, MapTypeId are required";
            map.MapId = Guid.NewGuid();
            map.MapTypeId = mapbook.ApplicationId ;
            map.CreateByUserId = 2100;

            map = te.extension.coria.InternalApi.CoriaDataService.CreateMap(map);
            Assert.IsTrue( map.Id > 0);
        }
        [TestMethod]
        public void ReadCoriaMapTest() {
           var maps = te.extension.coria.InternalApi.CoriaDataService.ReadMaps();
            Assert.IsTrue(maps.Count() > 0);
        }

        [TestMethod]
        public void UpdateCoriaMapTest() {
            //var maps = te.extension.coria.InternalApi.CoriaDataService.ReadMaps();
            //var map = maps.FirstOrDefault();
            //map.Description = "Testing";
            // map = te.extension.coria.InternalApi.CoriaDataService.CreateUpdateMap()
            //Assert.IsTrue(> 0);
            Assert.Fail();
              }
        [TestMethod]
        public void DeleteCoriaMapTest()
        {
            Guid mapid = new Guid("B7020BEB-231C-444F-8BB1-0632D1D20AEB");
            var map = te.extension.coria.InternalApi.CoriaDataService.GetCoriaMap(mapid);
            if (map.MapId == mapid)
            {
                te.extension.coria.InternalApi.CoriaDataService.DeleteCoriaMap(map.MapId);
                var map2 = te.extension.coria.InternalApi.CoriaDataService.GetCoriaMap(mapid);
                Assert.IsNull(map2);
            }
            else
            {
                //var maps = te.extension.coria.InternalApi.CoriaDataService.CreateCoriaMap();
                Assert.Fail();
            }
        }
    }
}
