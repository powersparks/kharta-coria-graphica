using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using kcgModels = kharta.coria.graphica.Models;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;

namespace te.extension.coria.InternalApi 
{
    internal class CoriaMap : kcgModels.Map
    {
        internal CoriaMap() { }
        /***
        internal new int Id { get; set; }
        internal new Guid MapId { get; set; }
        internal new Guid MapTypeId { get; set; }
        internal new string Title { get; set; }
        internal new string Description { get; set; }
        internal new DateTime CreateUtcDate { get; set; }
        internal new int CreateByUserId { get; set; }
        internal new int ModifiedByUserId { get; set; }
        internal new DateTime ModifiedUtcDate { get; set; }
        internal CoriaMapBook CoriaMapBook { get; set; }
        internal new kcgModels.MapBook MapBook { get; set; }
      ***/
        internal CoriaMapBook CoriaMapBook { get; set; }
        internal Func<kcgModels.Map, CoriaMap> toCoriaMap = (kcgModels.Map fromMap) => CoriaDataService.ToCoriaMap(fromMap, new CoriaMap());
        internal Func<CoriaMap, kcgModels.Map> toMap = (CoriaMap fromCoriaMap) => CoriaDataService.ToMap(fromCoriaMap, new kcgModels.Map());
        internal CoriaMap(kcgModels.Map map) {
        //    Func<CoriaMap, kcgModels.Map> toMap = (CoriaMap fromCoriaMap) => CoriaDataService.ToMap(fromCoriaMap, new kcgModels.Map());
        //    Func<kcgModels.Map, CoriaMap> toCoriaMap = (kcgModels.Map fromMap) => CoriaDataService.ToCoriaMap(fromMap, new CoriaMap());
            //toCoriaMap(map);
            Id = map.Id;
            MapId = map.MapId;
            MapTypeId = map.MapTypeId;
            Title = map.Title;
            Description = map.Description;
            CreateByUserId = map.CreateByUserId.Value;
            CreateUtcDate = map.CreateUtcDate.HasValue ? map.CreateUtcDate.Value : DateTime.UtcNow;
            ModifiedByUserId = map.ModifiedByUserId.Value;
            ModifiedUtcDate = map.ModifiedUtcDate.HasValue ? map.ModifiedUtcDate.Value : DateTime.UtcNow;
            GroupId =  map.GroupId.Value ;
            IsIndexed = map.IsIndexed;
            CoriaMapBook = CoriaDataService.GetCoriaMapBookApplication(MapTypeId);
            //MapBookId = map.MapBook.Id;
            
        }
        internal string Name { get { return Title; } }
        internal string HtmlDescription(string target)
        {
            // var _kcgName = new KhartaOntology();
            string description = Description;
            if (target == "web" || target == "email" || target == "raw")
            {
                //html s/b applied, translated, etc.. unless raw
            }
            return description;
            //    throw new NotImplementedException();
        }
        internal string HtmlName(string target)
        {
            // var _kcgName = new KhartaOntology();
            string name = Name;
            if (target == "web" || target == "email" || target == "raw")
            {
                //html s/b applied, translated, etc.. unless raw
            }
            return name;
            //    throw new NotImplementedException();
        }
    }
}
