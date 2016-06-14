namespace te.extension.kharta.PublicApi
{
    internal class SourcesListOptions
    {
        public SourcesListOptions()
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