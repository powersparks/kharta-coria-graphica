using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Version1;
using System.Collections;
using Telligent.Evolution.Urls.Routing;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;

namespace te.extension.kharta.WidgetApi
{
    [Documentation(Category = "Kharta")]
    public class Sources
    {

        [Documentation("The content type identifier for sources.")]
        public Guid ContentTypeId { get { return PublicApi.Sources.ContentTypeId; } }

        [Documentation("The current contextual source.")]
        public PublicApi.Source Current
        {
            get
            {
                PublicApi.Source source = null;

                var sourceId = TEApi.Url.CurrentContext.GetTokenValue("SourceId");

                if (sourceId != null)
                {
                    Guid id;
                    if (Guid.TryParse(sourceId.ToString(), out id))
                        source = PublicApi.Sources.GetSourceApplication(id);
                }

                return source;
            }
        }

        [Documentation("List sources within a group.")]
        public PagedList<PublicApi.Source> List(
            [Documentation("The group identifier.")]
            int groupId
            )
        {
            return List(groupId, null);
        }

        [Documentation("List sources within a group.")]
        public PagedList<PublicApi.Source> List(
            [Documentation("The group identifier.")]
            int groupId,
            [
            Documentation(Name="IncludeSubGroups", Type = typeof(bool), Default = false),
            Documentation(Name="AuthorUserId", Type = typeof(int), Description = "UserId of Author"),
            Documentation(Name="PageIndex", Type = typeof(int), Default = 0, Description = "The page index."),
            Documentation(Name="PageSize", Type=typeof(int), Default=20, Description="The number of sources to return in a single page."),
            Documentation(Name="SortBy", Type=typeof(string), Default="Date", Description="The sorting mechanism. ToppSourcesScore does not support IncludeSubGroups or AuthorUserId options.", Options=new string[] { "Date", "TopSourcesScore" }),
            Documentation(Name="SortOrder", Type = typeof(string), Options = new string[] { "ascending", "descending" }, Default="descending")
            ]
            IDictionary options
            )
        {
            PublicApi.SourcesListOptions query = new PublicApi.SourcesListOptions();

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

            return PublicApi.Sources.List(groupId, query);
        }

        [Documentation("Get a source.")]
        public PublicApi.Source Get(
            [Documentation("The source's identifier.")]
            Guid id
            )
        {
            return PublicApi.Sources.GetSourceApplication(id);
        }

        [Documentation("Delete a source.")]
        public AdditionalInfo Delete(
            [Documentation("The source's identifier.")]
            Guid id
            )
        {
            return PublicApi.Sources.Delete(id);
        }

        [Documentation("Create a new source.")]
        public PublicApi.Source Create(
            [Documentation("The identifier of the group in which to create the source.")]
            int groupId,
            [Documentation("The name of the new source.")]
            string name
            )
        {
            return Create(groupId, name, null);
        }

        [Documentation("Create a new source.")]
        public PublicApi.Source Create(
            [Documentation("The identifier of the group in which to create the source.")]
            int groupId,
            [Documentation("The name of the new source.")]
            string name,
            [
                Documentation(Name="Description", Type=typeof(string), Description="The description of the source.")
                  ]
            IDictionary options)
        {
            string description = null;

            if (options != null)
            {
                if (options["Description"] != null)
                    description = options["Description"].ToString();

            }

            return PublicApi.Sources.Create(groupId, name, description);
        }

        [Documentation("Update an existing source.")]
        public PublicApi.Source Update(
            [Documentation("The identifier of the source to update.")]
            Guid id,
            [
                Documentation(Name="Description", Type=typeof(string), Description="The description of the source."),
                Documentation(Name = "Name", Type = typeof(string), Description = "The name of the source.")]
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

            return PublicApi.Sources.Update(id, name, description);
        }

        [Documentation("Identifies if the accessing user can create a source in the given group.")]
        public bool CanCreate(int groupId)
        {
            return PublicApi.Sources.CanCreate(groupId);
        }



        [Documentation("Identifies if the accessing user can edit the given source.")]
        public bool CanEdit(Guid sourceId)
        {
            return PublicApi.Sources.CanEdit(sourceId);
        }

        [Documentation("Identifies if the accessing user can delete the given source.")]
        public bool CanDelete(Guid sourceId)
        {
            return PublicApi.Sources.CanDelete(sourceId);
        }

        [Documentation("Renders the source user interface.")]
        public string UI(
            Guid sourceId
            )
        {
            return UI(sourceId, null);
        }

        [Documentation("Renders the source user interface.")]
        public string UI(
            Guid sourceId,
            [
                Documentation(Name="ReadOnly", Type=typeof(bool), Description="When true, the UI does not support interactions/voting.", Default=false),
                Documentation(Name="ShowNameAndDescription", Type=typeof(bool), Description="When true, the UI includes the name and description of the source.", Default=true)
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

            return PublicApi.Sources.UI(sourceId, readOnly, showNameAndDescription);
        }
    }
}


