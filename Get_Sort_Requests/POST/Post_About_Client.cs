using Microsoft.Data.SqlClient;
using System.Net;

using Beck_end_lib.Get_Sort_Requests.Serialize_Classes;
using Beck_end_lib.Get_Sort_Requests.UPDATE;


namespace Beck_end_lib.Get_Sort_Requests.POST
{ 
    public interface Isql_post_fun
    {
        public void sql_post(SqlConnection con, string sql_command);
    }
    public class SQL : Isql_post_fun
    {
        public string? sql_new_user {
            get 
            { 
                return @"INSERT INTO dbo.Reader_Person (dbo.Reader_Person.Full_Name, dbo.Reader_Person.Numer_Phone, dbo.Reader_Person.Email, dbo.Reader_Person.Address, dbo.Reader_Person.NIE_Code) VALUES ( "; 
            } 
        }

        public string? sql_new_order_client
        {
            get
            {
                return @"INSERT INTO dbo.Issued_Books (ID_Reader, ID_book, Date_Get, Date_Need_Return) VALUES ( ";
            }
        }

        public string? sql_get_id_book
        {
            get
            {
                return @"SELECT dbo.Book.ID_Book FROM dbo.Book WHERE CONVERT(VARCHAR, dbo.Book.Name_Book) = ";
            }
        }

        void Isql_post_fun.sql_post(SqlConnection con, string sql_command)
        {
            SqlCommand sqlCommand = new SqlCommand(sql_command, con);
            int result = sqlCommand.ExecuteNonQuery();
            con.Close();
        }
    }

    internal class Post_About_Client
    {

        public void POST_NEW_CLIENT(HttpListenerResponse resp, SqlConnection con, string url)
        {
            Dictionary <string, string> listRequests = Get_Value.GetDictionaryRequest(url);

            try
            {
                string sql_res = new SQL().sql_new_user;

                listRequests["Full_name"] = Get_Value.FieldsFromURL(listRequests["Full_Name"]);
                listRequests["Phone"] = Get_Value.FieldsFromURL(listRequests["Phone"]);
                listRequests["Email"] = Get_Value.FieldsFromURL(listRequests["Email"]);
                listRequests["Address"] = Get_Value.FieldsFromURL(listRequests["Address"]);
                listRequests["NIE"] = Get_Value.FieldsFromURL(listRequests["NIE"]);

                sql_res += ("'" + listRequests["Full_name"] + "',") +
                           ("'" + listRequests["Phone"] + "',") +
                           ("'" + listRequests["Email"] + "',") +
                           ("'" + listRequests["Address"] + "',") +
                           ("'" + listRequests["NIE"] + "');");

                Isql_post_fun isql_Post_Fun = new SQL();
                isql_Post_Fun.sql_post(con, sql_res);

                HTTP_msg http_Msg = new HTTP_msg();
                http_Msg.handle_msg(resp, "OK HTTP 1.1 200", "Created new client");

            }
            catch (Exception ex) 
            {
                string msg = ex.Message;
                string[] nameOntFound = msg.Split("'");

                HTTP_msg http_Msg = new HTTP_msg();
                http_Msg.handle_msg(resp, ex.ToString(), nameOntFound[1]);

                return;
            }
            
        }

        public void POST_NEW_ORDER(HttpListenerResponse resp, SqlConnection con, string url)
        {
            Dictionary<string, string> listRequests = Get_Value.GetDictionaryRequest(url);

            try
            {
                string sql_res = new SQL().sql_new_order_client;
                string sql_Get_id_book = new SQL().sql_get_id_book;

                listRequests["ID_Person"] = Get_Value.FieldsFromURL(listRequests["ID_Person"]);
                listRequests["Name_book"] = Get_Value.FieldsFromURL(listRequests["Name_book"]);
                listRequests["Date_Get"] = Get_Value.FieldsFromURL(listRequests["Date_Get"]);
                listRequests["Date_Need_Return"] = Get_Value.FieldsFromURL(listRequests["Date_Need_Return"]);

                sql_Get_id_book += "'" + listRequests["Name_book"] + "';";

                SqlCommand sqlCommand = new SqlCommand(sql_Get_id_book, con);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                string id_book = "";

                while (reader.Read()) id_book = reader["ID_Book"].ToString();

                sql_res += (listRequests["ID_Person"] + ",") +
                    (id_book + ",") +
                    ("'" + listRequests["Date_Get"] + "',") +
                    ("'" + listRequests["Date_Need_Return"] + "');");
                reader.Close();

                Isql_post_fun isql_Post_Fun = new SQL();
                isql_Post_Fun.sql_post(con, sql_res);

                string varSQL = new Update_class_sql().update_var;
                varSQL += id_book + ";";

                string ucbd = new Update_class_sql().update_count_books_dicrement;
                varSQL += ucbd;

                con.Open();
                isql_Post_Fun.sql_post(con, varSQL);

                HTTP_msg http_Msg = new HTTP_msg();
                http_Msg.handle_msg(resp, "OK HTTP 1.1 200", "Created new order for client");
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
