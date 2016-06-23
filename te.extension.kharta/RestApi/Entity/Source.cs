using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Api.Version1;

namespace te.extension.kharta.RestApi.Entity
{
    public class Source
    {
         
        public Source() { }

        internal Source(InternalApi.KhartaSource source) {
            var a = source.Id;
            ApplicationId = source.ApplicationId;
            ApplicationTypeId = source.ApplicationTypeId;
            AvatarUrl = source.AvatarUrl;
            Description = source.Description;
            Name = source.Name;
            OntologyId = source.OntologyId.HasValue ? source.OntologyId.Value : 0 ;
            SafeName = source.SafeName;
            Url = source.Url;
            IsEnabled = source.IsEnabled.HasValue ? source.IsEnabled.Value : true;
            GroupId = source.GroupId.HasValue ? source.GroupId.Value : Apis.Get<IGroups>().Root.Id.Value;


        }
        public int Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid ApplicationTypeId { get; set; }
        public string AvatarUrl { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string SafeName { get; set; }
        public int OntologyId { get; set; }
        public bool IsEnabled { get; set; }
        public int GroupId { get; set; }




    }
}
