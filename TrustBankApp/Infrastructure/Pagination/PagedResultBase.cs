namespace TrustBankApp.Infrastructure.Pagination
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage 
        { 
            get
            {
                if (CurrentPage < TotalPages)                
                    return EndPage - 5;                
                else
                    return CurrentPage - 5;
            } 
        }
        public int EndPage { 
            get 
            { 
                if(CurrentPage == TotalPages)
                    return TotalPages;
                else
                    return CurrentPage + 5;
            } 
        }
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
