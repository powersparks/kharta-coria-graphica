namespace te.extension.coria.PublicApi
{
    public class MapsListOptions
    {
        public MapsListOptions()
        {
        }

        public int AuthorUserId { get; internal set; }
        public bool IncludeSubGroups { get; internal set; }
        public int PageIndex { get; internal set; }
        public int PageSize { get; internal set; }
        public string SortBy { get; internal set; }
        public string SortOrder { get; internal set; }
    }
}