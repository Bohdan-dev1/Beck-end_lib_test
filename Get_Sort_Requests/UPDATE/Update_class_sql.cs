using Beck_end_lib.Get_Sort_Requests.Serialize_Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Beck_end_lib.Get_Sort_Requests.UPDATE
{
    internal class Update_class_sql
    {
        public string? update_var
        {
            get
            {
                return "DECLARE @ID INT;\r\nSET @ID = ";
            }
        }

        public string? update_count_books_dicrement
        {
            get
            {
                return "SELECT dbo.Number_Of_Books.ID, dbo.Number_Of_Books.How_many FROM dbo.Number_Of_Books\r\nWHERE dbo.Number_Of_Books.ID_Book = @ID\r\nUPDATE dbo.Number_Of_Books SET How_many = (How_many - 1)\r\nWHERE dbo.Number_Of_Books.ID_Book = @ID;";
            }
        }

        public void Update_SQL(HttpListenerResponse resp, string sql_command, SqlConnection con)
        {
            try
            {
                SqlCommand sqlCommandIDBook = new(sql_command, con);
                SqlDataReader readerIDBook = sqlCommandIDBook.ExecuteReader();

                HTTP_msg http_Msg = new HTTP_msg();
                http_Msg.handle_msg(resp, "Updated client date", "HTTP 1.1 OK 200");

                return;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                string[] nameOntFound = msg.Split("'");

                HTTP_msg http_Msg = new HTTP_msg();
                http_Msg.handle_msg(resp, ex.ToString(), msg);

                return;
            }

        }
    }
}
