using Beck_end_lib.Get_Sort_Requests.Serialize_Classes;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Beck_end_lib.Get_Sort_Requests.POST
{
    class SQL_book
    {
        public string? sql_get_ID_author { 
            get 
            {
                return "SELECT ID_Author FROM dbo.About_Author WHERE CONVERT(VARCHAR, dbo.About_Author.Full_name) LIKE ";
            } 
        }

        public string? sql_new_book
        {
            get
            {
                return "INSERT dbo.Book (Name_Book, ID_Author, Year_Public) VALUES ( ";
            }
        }

        public string? sql_new_book_Count
        {
            get
            {
                return "INSERT dbo.Number_Of_Books (ID_Book, How_many) VALUES ( ";
            }
        }

        public string? sql_new_author
        {
            get
            {
                return "INSERT dbo.About_Author (Full_name) VALUES ( ";
            }
        }
    }
    public class Post_Add_Book
    {
        public void PostNewBook(HttpListenerResponse resp, SqlConnection con, string url)
        {
            Dictionary<string, string> listRequests = Get_Value.GetDictionaryRequest(url);

            try
            {
                string sql_res = new SQL_book().sql_new_book;
                string sql_res_two = new SQL_book().sql_new_book_Count;
                string sql_get_id_auther = new SQL_book().sql_get_ID_author;

                string id_author = "";
                string id_book = "";

                listRequests["Name_book"] = Get_Value.FieldsFromURL(listRequests["Name_book"]);
                listRequests["Name_author"] = Get_Value.FieldsFromURL(listRequests["Name_author"]);
                listRequests["Year_Public"] = Get_Value.FieldsFromURL(listRequests["Year_Public"]);
                listRequests["How_many"] = Get_Value.FieldsFromURL(listRequests["How_many"]);

                sql_get_id_auther += "'" + listRequests["Name_author"] + "'";

                SqlCommand sqlCommand = new SqlCommand(sql_get_id_auther, con);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                Isql_post_fun isql_Post_Fun = new SQL();
                string sql_Get_id_book = new SQL().sql_get_id_book;

                while (reader.Read()) id_author = reader["ID_Author"].ToString();

                reader.Close();

                string[] temp_string_id = id_author.Split(",");
                string res_id_author = "";

                if (temp_string_id.Length != 1) res_id_author += temp_string_id[0];
                else res_id_author += id_author;

                
                if (id_author == "")
                {
                    string new_author = new SQL_book().sql_new_author + ("'" + listRequests["Name_author"] + "')");
                    isql_Post_Fun.sql_post(con, new_author);
                }
                con.Close();

                sql_res += ("'" + listRequests["Name_book"] + "',") +
                            (res_id_author + ",") +
                            ("'" + listRequests["Year_Public"] + "')");

                con.Open();
                isql_Post_Fun.sql_post(con, sql_res);

                sql_Get_id_book += "'" + listRequests["Name_book"] + "'";

                con.Close();
                con.Open();
                SqlCommand sqlCommandIDBook = new(sql_Get_id_book, con);
                SqlDataReader readerIDBook = sqlCommandIDBook.ExecuteReader();

                while (readerIDBook.Read()) id_book = readerIDBook["ID_Book"].ToString();
                readerIDBook.Close();

                sql_res_two += id_book + "," + listRequests["How_many"] + ")";
                isql_Post_Fun.sql_post(con, sql_res_two);

                HTTP_msg http_Msg = new HTTP_msg();
                http_Msg.handle_msg(resp, "OK HTTP 1.1 200", "Created new book");

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
