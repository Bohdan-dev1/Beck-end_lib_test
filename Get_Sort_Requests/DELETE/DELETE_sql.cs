namespace Beck_end_lib.Get_Sort_Requests.DELETE
{
    internal class DELETE_sql
    {
        public string? delete_order
        {
            get
            {
                return "DELETE FROM dbo.Issued_Books WHERE";
            }
        }

        public string? delete_client
        {
            get
            {
                return "DELETE FROM dbo.Reader_Person WHERE ";
            }
        }

        public string? delete_book
        {
            get
            {
                return "DELETE FROM dbo.Book WHERE ";
            }
        }

        public string? delete_NFB
        {
            get
            {
                return "DELETE FROM dbo.Number_Of_Books WHERE ";
            }
        }

        public string? delete_Author
        {
            get
            {
                return "DELETE FROM dbo.About_Author WHERE ";
            }
        }
    }
}
