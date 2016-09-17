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
    internal class CoriaMapBook : kcgModels.MapBook
    {
        internal CoriaMapBook() { }
        /***
       
        internal new int Id { get; set; }
        internal new Guid ApplicationId { get; set; }
        internal new Guid ApplicationTypeId { get; set; }
        internal new string Name { get; set; }
        internal new int OntologyId { get; set; }
        internal new string SafeName { get; set; }
        internal new string AvatarUrl { get; set; }
        internal new string Url { get; set; }
        internal new int GroupId { get; set; }
        internal ICollection<CoriaMap> CoriaMaps { get; set; }
        internal new ICollection<kcgModels.Map> Maps { get; set; }
        */
        internal Func<kcgModels.MapBook, CoriaMapBook> toCoriaMapBook = (kcgModels.MapBook fromMapBook) => CoriaDataService.ToCoriaMapBook(fromMapBook, new CoriaMapBook());
        internal Func<CoriaMapBook, kcgModels.MapBook> toMapBook = (CoriaMapBook fromCoriaMapBook) => CoriaDataService.ToMapBook(fromCoriaMapBook, new kcgModels.MapBook());

        internal CoriaMapBook(kcgModels.MapBook mapbook)
        {
            Id = mapbook.Id;
            ApplicationId = mapbook.ApplicationId;
            ApplicationTypeId = mapbook.ApplicationTypeId.Value;
            AvatarUrl = mapbook.AvatarUrl;
            Description = mapbook.Description;
            Name = mapbook.Name;
            OntologyId = mapbook.OntologyId.Value;
            SafeName = mapbook.SafeName;
            IsEnabled = mapbook.IsEnabled;
            Url = mapbook.Url;
            GroupId = mapbook.GroupId;
            IsIndexed = mapbook.IsIndexed;
            //CoriaMaps = (ICollection<CoriaMap>) mapbook.Maps;
            Maps = mapbook.Maps;

        }
        internal string HtmlDescription(string target)
        {
            // var _kcgName = new KhartaOntology();
            string description = Description;
            if (target.ToLower() == "web" || target.ToLower() == "email" || target.ToLower() == "raw")
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
            if (target.ToLower() == "web" || target.ToLower() == "email" || target.ToLower() == "raw")
            {
                //html s/b applied, translated, etc.. unless raw
            }
            return name;
            //    throw new NotImplementedException();
        }
    }
}
