using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Version1;
using System.Collections;
using Telligent.Evolution.Urls.Routing;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;

namespace te.extension.coria.WidgetApi
{
    [Documentation(Category = "Coria")]
    public class Maps
    {
        [Documentation("The MapBook identifier for maps.")]
        public Guid MapBookId { get { return PublicApi.Maps.ContentTypeId; } }

        [Documentation("The content type identifier for maps.")]
        public Guid ContentTypeId { get { return PublicApi.Maps.ContentTypeId; } }
        [Documentation("Google Maps Api Default Use: True, use 'client'. False, use 'key'.")]
        public bool GoogleMapsApiDefaultUseClient { get { return PublicApi.Maps.GoogleMapsApiDefaultUseKeyOrClientId; } }
        [Documentation("Google Maps Api Client Id")]
        public string GoogleMapsApiClientId { get { return PublicApi.Maps.GoogleMapsApiClientId; } }
        [Documentation("Google Maps Api Key")]
        public string GoogleMapsApiKey { get { return PublicApi.Maps.GoogleMapsApiKey; } }
        [Documentation("Google Maps Api Version,ex. 3.23")]
        public string GoogleMapsVersion { get { return PublicApi.Maps.GoogleMapsVersion; } }
        [Documentation("MapBox Api access_token")]
        public string MapBoxApiAccessToken { get { return PublicApi.Maps.MapBoxApiAccessToken; } }
        [Documentation("MapBox API Version, ex. v0.21.0")]
        public string MapBoxVersion { get { return PublicApi.Maps.MapBoxVersion; } }
        [Documentation("ArcGIS API Version, ex. 4.0")]
        public string ArcGisVersion { get { return PublicApi.Maps.ArcGisVerions; } }
       
        [Documentation("The current contextual map.")]
        public PublicApi.Map Current
        {
            get
            {
                PublicApi.Map map = null;

                var mapId = TEApi.Url.CurrentContext.GetTokenValue("MapId");

                if (mapId != null)
                {
                    Guid id;
                    if (Guid.TryParse(mapId.ToString(), out id))
                        map = PublicApi.Maps.Get(id);
                }

                return map;
            }
        }

        [Documentation("List maps within a group.")]
        public PagedList<PublicApi.Map> List(
            [Documentation("The group identifier.")]
            int groupId
            )
        {
            return List(groupId, null, null);
        }
        [Documentation("List maps within a group.")]
        public PagedList<PublicApi.Map> List(
          [Documentation("The group identifier.")]
            int groupId,
          [Documentation("mapbook safe name")]
            string safename
          )
        {
            return List(groupId,safename, null);
        }

        [Documentation("List maps within a group and mapbook.")]
        public PagedList<PublicApi.Map> List(
            [Documentation("The group identifier.")]
            int groupId, 
            [Documentation("map book safe name")]
            string safename,
            [
            Documentation(Name="IncludeSubGroups", Type = typeof(bool), Default = false),
            Documentation(Name="AuthorUserId", Type = typeof(int), Description = "UserId of Author"),
            Documentation(Name="PageIndex", Type = typeof(int), Default = 0, Description = "The page index."),
            Documentation(Name="PageSize", Type=typeof(int), Default=20, Description="The number of maps to return in a single page."),
            Documentation(Name="SortBy", Type=typeof(string), Default="Date", Description="The sorting mechanism. ToppMapsScore does not support IncludeSubGroups or AuthorUserId options.", Options=new string[] { "Date", "TopMapsScore" }),
            Documentation(Name="SortOrder", Type = typeof(string), Options = new string[] { "ascending", "descending" }, Default="descending")
            ]
            IDictionary options
            )
        {
            PublicApi.MapsListOptions query = new PublicApi.MapsListOptions();

            if (options != null)
            {
                if (options["IncludeSubGroups"] != null)
                    query.IncludeSubGroups = Convert.ToBoolean(options["IncludeSubGroups"]);

                if (options["AuthorUserId"] != null)
                    query.AuthorUserId = Convert.ToInt32(options["AuthorUserId"]);

                if (options["PageIndex"] != null)
                    query.PageIndex = Convert.ToInt32(options["PageIndex"]);

                if (options["PageSize"] != null)
                    query.PageSize = Convert.ToInt32(options["PageSize"]);

                if (options["SortBy"] != null)
                    query.SortBy = options["SortBy"].ToString();

                if (options["SortOrder"] != null)
                    query.SortOrder = options["SortOrder"].ToString();
            }

            return PublicApi.Maps.List(groupId, safename, query);
        }

        [Documentation("Get a map.")]
        public PublicApi.Map Get(
            [Documentation("The map's identifier.")]
            Guid id
            )
        {
            return PublicApi.Maps.Get(id);
        }

        [Documentation("Delete a map.")]
        public AdditionalInfo Delete(
            [Documentation("The map's identifier.")]
            Guid id
            )
        {
            return PublicApi.Maps.Delete(id);
        }

        [Documentation("Create a new map.")]
        public PublicApi.Map Create(
            [Documentation("The identifier of the group in which to create the map.")]
            int groupId,
            [Documentation("The name of the new map.")]
            string name
            )
        {
            return Create(groupId, name, null);
        }

        [Documentation("Create a new map.")]
        public PublicApi.Map Create(
            [Documentation("The identifier of the group in which to create the map.")]
            int groupId,
            [Documentation("The name of the new map.")]
            string name,
            [
                Documentation(Name="Description", Type=typeof(string), Description="The description of the map.")
                  ]
            IDictionary options)
        {
            string description = null;

            if (options != null)
            {
                if (options["Description"] != null)
                    description = options["Description"].ToString();

            }

            return PublicApi.Maps.Create(groupId, name, description);
        }

        [Documentation("Update an existing map.")]
        public PublicApi.Map Update(
            [Documentation("The identifier of the map to update.")]
            Guid id,
            [
                Documentation(Name="Description", Type=typeof(string), Description="The description of the map."),
                Documentation(Name = "Name", Type = typeof(string), Description = "The name of the map.")]
            IDictionary options
            )
        {
            string name = null;
            string description = null;

            if (options != null)
            {
                if (options["Description"] != null)
                    description = options["Description"].ToString();

                if (options["Name"] != null)
                    name = options["Name"].ToString();
            }

            return PublicApi.Maps.Update(id, name, description);
        }

        [Documentation("Identifies if the accessing user can create a map in the given group.")]
        public bool CanCreate(int groupId)
        {
            return PublicApi.Maps.CanCreate(groupId);
        }



        [Documentation("Identifies if the accessing user can edit the given map.")]
        public bool CanEdit(Guid mapId)
        {
            return PublicApi.Maps.CanEdit(mapId);
        }

        [Documentation("Identifies if the accessing user can delete the given map.")]
        public bool CanDelete(Guid mapId)
        {
            return PublicApi.Maps.CanDelete(mapId);
        }

        [Documentation("Renders the map user interface.")]
        public string UI(
            Guid mapId
            )
        {
            return UI(mapId, null);
        }

        [Documentation("Renders the map user interface.")]
        public string UI(
            Guid mapId,
            [
                Documentation(Name="ReadOnly", Type=typeof(bool), Description="When true, the UI does not support interactions/voting.", Default=false),
                Documentation(Name="ShowNameAndDescription", Type=typeof(bool), Description="When true, the UI includes the name and description of the map.", Default=true)
            ]
            IDictionary options
            )
        {
            bool readOnly = false;
            bool showNameAndDescription = true;

            if (options != null)
            {
                if (options["ReadOnly"] != null)
                    readOnly = Convert.ToBoolean(options["ReadOnly"]);

                if (options["ShowNameAndDescription"] != null)
                    showNameAndDescription = Convert.ToBoolean(options["ShowNameAndDescription"]);
            }

            return PublicApi.Maps.UI(mapId, readOnly, showNameAndDescription);
        }
    }
}


