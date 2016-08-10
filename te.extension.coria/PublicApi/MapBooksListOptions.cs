using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te.extension.coria.PublicApi
{
    public class MapBooksListOptions
    {
        public MapBooksListOptions()
        {
        }
        //int pageIndex = 0, 
        // int pageSize = 10, 
        // SortBy sortBy = SortBy.Title, 
        // SortOrder sortOrder = SortOrder.Ascending
        public int AuthorUserId { get; internal set; }
        public bool IncludeSubGroups { get; internal set; }
        public int PageIndex { get; internal set; }
        public int PageSize { get; internal set; }
        public string SortBy { get; set; }
        public string SortOrder { get;  set; }
    }
}
