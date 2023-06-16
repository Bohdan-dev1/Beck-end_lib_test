using Beck_end_lib.Get_Sort_Requests.Default;
using Microsoft.Data.SqlClient;

namespace Beck_end_lib.Get_Sort_Requests.GET
{
    internal class SQL_GET
    {
        public string? sql_get_all_clients
        {
            get
            {
                return @"SELECT 
dbo.Reader_Person.Person_ID, dbo.Issued_Books.ID_Order, dbo.Reader_Person.Full_Name, dbo.Reader_Person.Numer_Phone, dbo.Reader_Person.Email, 
dbo.Reader_Person.Address, dbo.Reader_Person.NIE_Code, dbo.Reader_Person.Add_Details_Person, dbo.Book.Name_Book, dbo.Issued_Books.Date_Get, dbo.Issued_Books.Date_Return,
dbo.Issued_Books.Date_Need_Return
FROM dbo.Reader_Person
JOIN dbo.Issued_Books 
ON dbo.Issued_Books.ID_Reader = dbo.Reader_Person.Person_ID
JOIN dbo.Book
ON dbo.Book.ID_Book = dbo.Issued_Books.ID_book;";
            }
        }

        public string? sql_get_info_client
        {
            get
            {
                return @"SELECT 
dbo.Issued_Books.ID_Order, dbo.Book.Name_Book, dbo.Issued_Books.Date_Get, dbo.Issued_Books.Date_Return,  dbo.Issued_Books.Date_Need_Return
FROM dbo.Reader_Person
JOIN dbo.Issued_Books 
ON dbo.Issued_Books.ID_Reader = dbo.Reader_Person.Person_ID
JOIN dbo.Book
ON dbo.Book.ID_Book = dbo.Issued_Books.ID_book
WHERE dbo.Reader_Person.Person_ID = ";
            }
        }

        public string? sqlSriptWithoutOrder
        {
            get
            {
                return @"SELECT 
dbo.Reader_Person.Person_ID, dbo.Reader_Person.Full_Name, dbo.Reader_Person.Numer_Phone, dbo.Reader_Person.Email, 
dbo.Reader_Person.Address, dbo.Reader_Person.NIE_Code, dbo.Reader_Person.Add_Details_Person
FROM dbo.Reader_Person;";
            }
        }

        public string? sqlGetClientID
        {
            get
            {
                return "SELECT Person_ID FROM dbo.Reader_Person WHERE CONVERT(VARCHAR, dbo.Reader_Person.Full_Name) = '";
            }
        }

        public string? sqlCheckClientByID
        {
            get
            {
                return "SELECT Person_ID FROM dbo.Reader_Person WHERE dbo.Reader_Person.Person_ID = ";
            }
        }

        public string? getIdAuthorByName
        {
            get
            {
                return "SELECT dbo.About_Author.ID_Author FROM dbo.About_Author WHERE CONVERT( varchar ,Full_name ) = '";
            }
        }

        public string? getIdBookByIdAuthor
        {
            get
            {
                return "SELECT dbo.Book.ID_Book FROM dbo.Book WHERE CONVERT( varchar ,ID_Author ) = '";
            }
        }

        public bool isInTable(SqlConnection con, List<Class_ROW_Client> arg, string table, string keyROW, string ROWend, string check_row)
        {
            string res_sql = "";
            string res_sql_req = "SELECT " + keyROW + " FROM " + table + " WHERE ";

            foreach (Class_ROW_Client tempData in arg)
            {
                if (tempData.type_data == ROWend) break;
                res_sql_req += "CONVERT( varchar , " + tempData.type_data + ") = '" + tempData.row + "' AND ";
            }
            res_sql_req = res_sql_req[..^5];

            SqlCommand sqlCommandRun = new SqlCommand(res_sql_req, con);
            SqlDataReader reader = sqlCommandRun.ExecuteReader();
            while (reader.Read()) res_sql += reader[check_row].ToString() + ", ";

            reader.Close();

            if (res_sql_req == "") return false;
            else return true;

        }
    }
}
