using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Version1;
using System.Collections;
using Telligent.Evolution.Urls.Routing;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
using Apis = Telligent.Evolution.Extensibility.Apis;
using Api1 = Telligent.Evolution.Extensibility.Api.Version1;

namespace te.extension.coria.WidgetApi
{
    [Documentation(Category = "Coria")]
    public class MapBooks
    {
        
        [Documentation("The current contextual mapBook.")]
        public PublicApi.MapBook Current
        {
            get
            {
                PublicApi.MapBook mapBook = null;
                Guid groupGuid = Apis.Get<Api1.IGroups>().ContainerTypeId;
                var mapBookNameToken = TEApi.Url.CurrentContext.GetTokenValue("mapBook");
                var mapBooksNameToken = TEApi.Url.CurrentContext.GetTokenValue("mapBooks");
                string mapBookName = mapBooksNameToken != null ? mapBooksNameToken.ToString() : "";

                //CurrentContext.ContextItems.Find(c=> c.ContainerTypeId == {GroupContainerType})  
                IContextItem contextItem = TEApi.Url.CurrentContext.ContextItems.GetItemByContainerType(groupGuid);
                int groupId = 0;
                string mapBookId = null;
                if (int.TryParse(contextItem.Id, out groupId))
                {
                    if  (mapBookName != null && mapBookName.Length > 0)
                    {
                        mapBook = PublicApi.MapBook.Get(groupId, mapBookName) as PublicApi.MapBook;
                        if (mapBook.HasWarningsOrErrors()) {
                            mapBook = PublicApi.MapBook.Get(mapBookName) as PublicApi.MapBook;
                        }
                    }
                }
                else { }
                if (mapBookId != null)
                {
                    Guid id;
                    if (Guid.TryParse(mapBookId.ToString(), out id))
                        mapBook = PublicApi.MapBooks.Get(id) as PublicApi.MapBook;
                }
                
                return mapBook;
            }
        }

        [Documentation("List mapBooks within a group.")]
        public PagedList<PublicApi.MapBook> List(
            [Documentation("The group identifier.")]
            int groupId
            )
        {
            return List(groupId, null);
        }

        [Documentation("List mapBooks within a group.")]
        public PagedList<PublicApi.MapBook> List(
            [Documentation("The group identifier.")]
            int groupId,
            [
            Documentation(Name="IncludeSubGroups", Type = typeof(bool), Default = false),
            Documentation(Name="AuthorUserId", Type = typeof(int), Description = "UserId of Author"),
            Documentation(Name="PageIndex", Type = typeof(int), Default = 0, Description = "The page index."),
            Documentation(Name="PageSize", Type=typeof(int), Default=20, Description="The number of mapBooks to return in a single page."),
            Documentation(Name="SortBy", Type=typeof(string), Default="Date", Description="The sorting mechanism. ToppMapBooksScore does not support IncludeSubGroups or AuthorUserId options.", Options=new string[] { "Date", "TopMapBooksScore" }),
            Documentation(Name="SortOrder", Type = typeof(string), Options = new string[] { "ascending", "descending" }, Default="descending")
            ]
            IDictionary options
            )
        {
            PublicApi.MapBooksListOptions query = new PublicApi.MapBooksListOptions();

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
                    query.SortBy =   options["SortBy"].ToString();

                if (options["SortOrder"] != null)
                    query.SortOrder =    options["SortOrder"].ToString()  ;
            }

            return PublicApi.MapBooks.List(groupId, query);
        }

        [Documentation("Get a mapBook.")]
        public PublicApi.MapBook Get(
            [Documentation("The mapBook's identifier.")]
            Guid id
            )
        {
            return (PublicApi.MapBook)PublicApi.MapBooks.Get(id);
        }

        [Documentation("Delete a mapBook.")]
        public AdditionalInfo Delete(
            [Documentation("The mapBook's identifier.")]
            Guid id
            )
        {
            return PublicApi.MapBooks.Delete(id);
        }

        [Documentation("Create a new mapBook.")]
        public PublicApi.MapBook Create(
            [Documentation("The identifier of the group in which to create the mapBook.")]
            int groupId,
            [Documentation("The name of the new mapBook.")]
            string name
            )
        {
            return Create(groupId, name, null);
        }

        [Documentation("Create a new mapBook.")]
        public PublicApi.MapBook Create(
            [Documentation("The identifier of the group in which to create the mapBook.")]
            int groupId,
            [Documentation("The name of the new mapBook.")]
            string name,
            [
                Documentation(Name="Description", Type=typeof(string), Description="The description of the mapBook.")
                  ]
            IDictionary options)
        {
            string description = null;

            if (options != null)
            {
                if (options["Description"] != null)
                    description = options["Description"].ToString();

            }

            return PublicApi.MapBooks.Create(groupId, name, description);
        }

        [Documentation("Update an existing mapBook.")]
        public PublicApi.MapBook Update(
            [Documentation("The identifier of the mapBook to update.")]
            Guid id,
            [
                Documentation(Name="Description", Type=typeof(string), Description="The description of the mapBook."),
                Documentation(Name = "Name", Type = typeof(string), Description = "The name of the mapBook.")]
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

            return PublicApi.MapBooks.Update(id, name, description);
        }

        [Documentation("Identifies if the accessing user can create a mapBook in the given group.")]
        public bool CanCreate(int groupId)
        {
            return PublicApi.MapBooks.CanCreate(groupId);
        }



        [Documentation("Identifies if the accessing user can edit the given mapBook.")]
        public bool CanEdit(Guid mapBookId)
        {
            return PublicApi.MapBooks.CanEdit(mapBookId);
        }

        [Documentation("Identifies if the accessing user can delete the given mapBook.")]
        public bool CanDelete(Guid mapBookId)
        {
            return PublicApi.MapBooks.CanDelete(mapBookId);
        }

        [Documentation("Renders the mapBook user interface.")]
        public string UI(
            Guid mapBookId
            )
        {
            return UI(mapBookId, null);
        }

        [Documentation("Renders the mapBook user interface.")]
        public string UI(
            Guid mapBookId,
            [
                Documentation(Name="ReadOnly", Type=typeof(bool), Description="When true, the UI does not support interactions/voting.", Default=false),
                Documentation(Name="ShowNameAndDescription", Type=typeof(bool), Description="When true, the UI includes the name and description of the mapBook.", Default=true)
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

            return PublicApi.MapBooks.UI(mapBookId, readOnly, showNameAndDescription);
        }
    }
}
