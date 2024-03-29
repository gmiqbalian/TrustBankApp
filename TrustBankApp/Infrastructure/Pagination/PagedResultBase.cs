﻿namespace TrustBankApp.Infrastructure.Pagination
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
                if(TotalPages > 3)
                {
                    if (CurrentPage > 3 && CurrentPage <= TotalPages)
                        return CurrentPage - 3;
                    else
                        return CurrentPage;
                }
                else
                    return CurrentPage;
            }
        }
        public int EndPage {
            get
            {
                if (TotalPages > 3)
                {
                    if (CurrentPage == TotalPages)
                        return TotalPages;
                    else
                        return CurrentPage + 3;
                }
                else
                    return CurrentPage;
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
