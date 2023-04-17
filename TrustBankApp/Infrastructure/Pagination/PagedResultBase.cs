namespace TrustBankApp.Infrastructure.Pagination
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get {return CurrentPage - 5;}}
        public int EndPage { get {return CurrentPage + 5;} }
        public int TotalRows { get; set; }
        public int FirstRowOnPage 
        {
            get { return (CurrentPage - 1) * PageSize + 1; } 
        }
        public int LastRowOnPage 
        {
            get { return Math.Min(CurrentPage * PageSize, TotalRows); }
        }
    }
}
