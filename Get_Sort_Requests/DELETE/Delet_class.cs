using Beck_end_lib.Get_Sort_Requests.GET;
using Beck_end_lib.Get_Sort_Requests.Serialize_Classes;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Beck_end_lib.Get_Sort_Requests.DELETE
{
    internal class Delete_class
    {

        public void DELETE_ORDER(HttpListenerResponse resp, SqlConnection con, string url)
        {
            Dictionary<string, string> valuePairs = Get_Value.GetDictionaryRequest(url);


            try
            {
                string sql_delete_order = new DELETE_sql().delete_order;
                string value;

                if (valuePairs.TryGetValue("ID_Order", out value))
                {
                    valuePairs["ID_Order"] = Get_Value.FieldsFromURL(valuePairs["ID_Order"]);
                    sql_delete_order += " CONVERT( varchar,  ID_Order ) = '" + valuePairs["ID_Order"] + "'";
                }
                else
                {
                    HTTP_msg http_Msg = new HTTP_msg();
                    http_Msg.handle_msg(resp, "Undefined ID order", "Undefined");

                    return;
                }

                SqlCommand sqlCommand = new SqlCommand(sql_delete_order, con);
                sqlCommand.ExecuteNonQuery();

                HTTP_msg http_MsgT = new HTTP_msg();
                http_MsgT.handle_msg(resp, "Deleted Order", "HTTP 1.1 OK 200");
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

        public void DELETE_CLIENT(HttpListenerResponse resp, SqlConnection con, string url)
        {
            Dictionary<string, string> valuePairs = Get_Value.GetDictionaryRequest(url);

            try
            {
                string sql_delete_order = new DELETE_sql().delete_order;
                string sql_delete_client = new DELETE_sql().delete_client;
                string value;

                if (valuePairs.TryGetValue("Person_ID", out value))
                {
                    valuePairs["Person_ID"] = Get_Value.FieldsFromURL(valuePairs["Person_ID"]);
                    sql_delete_order  += " CONVERT( varchar,  ID_Reader ) = '" + valuePairs["Person_ID"] + "'";
                    sql_delete_client+= " CONVERT( varchar,  Person_ID ) = '" + valuePairs["Person_ID"] + "'";
                }

                SqlCommand sqlCommand = new SqlCommand(sql_delete_order, con);
                sqlCommand.ExecuteNonQuery();

                con.Close();
                con.Open();

                SqlCommand sqlCommandC = new SqlCommand(sql_delete_client, con);
                sqlCommandC.ExecuteNonQuery();

                HTTP_msg http_MsgT = new HTTP_msg();
                http_MsgT.handle_msg(resp, "Deleted Client", "HTTP 1.1 OK 200");
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

        public void DELETE_BOOK(HttpListenerResponse resp, SqlConnection con, string url)
        {
            Dictionary<string, string> valuePairs = Get_Value.GetDictionaryRequest(url);

            try
            {
                string sql_delete_book = new DELETE_sql().delete_book;
                string sql_delete_NFB = new DELETE_sql().delete_NFB;
                string value;

                if (valuePairs.TryGetValue("ID_Book", out value))
                {
                    valuePairs["ID_Book"] = Get_Value.FieldsFromURL(valuePairs["ID_Book"]);
                    sql_delete_book += " CONVERT( varchar,  ID_Book ) = '" + valuePairs["ID_Book"] + "'";
                    sql_delete_NFB += " CONVERT( varchar,  ID_Book ) = '" + valuePairs["ID_Book"] + "'";
                }

                SqlCommand sqlCommand = new SqlCommand(sql_delete_NFB, con);
                sqlCommand.ExecuteNonQuery();

                con.Close();
                con.Open();

                SqlCommand sqlCommandC = new SqlCommand(sql_delete_book, con);
                sqlCommandC.ExecuteNonQuery();

                HTTP_msg http_MsgT = new HTTP_msg();
                http_MsgT.handle_msg(resp, "Deleted Book", "HTTP 1.1 OK 200");
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

        public void DELETE_AUTHOR(HttpListenerResponse resp, SqlConnection con, string url)
        {
            Dictionary<string, string> valuePairs = Get_Value.GetDictionaryRequest(url);



            try
            {
                string sql_delete_book = new DELETE_sql().delete_book;
                string sql_delete_NFB = new DELETE_sql().delete_NFB;
                string sql_delete_Author = new DELETE_sql().delete_Author;
                string sql_delete_order = new DELETE_sql().delete_order;

                string id_author = new SQL_GET().getIdAuthorByName;
                string id_book = new SQL_GET().getIdBookByIdAuthor;

                string value;

                if (valuePairs.TryGetValue("Full_name", out value))
                {
                    valuePairs["Full_name"] = Get_Value.FieldsFromURL(valuePairs["Full_name"]);

                    id_author += valuePairs["Full_name"] + "'";

                    SqlCommand sqlCommandIDa = new SqlCommand(id_author, con);
                    SqlDataReader sdr = sqlCommandIDa.ExecuteReader();
                    while (sdr.Read()) id_author = sdr["ID_Author"].ToString();

                    con.Close();
                    con.Open();

                    id_book += id_author + "'";
                    SqlCommand sqlCommandIDb = new SqlCommand(id_book, con);
                    SqlDataReader sdrb = sqlCommandIDb.ExecuteReader();
                    while (sdrb.Read()) id_book = sdrb["ID_Book"].ToString(); 
                }


                con.Close();
                con.Open();


                sql_delete_NFB += " CONVERT( varchar,  ID_Book ) = '" + id_book + "'";

                SqlCommand sqlCommand = new SqlCommand(sql_delete_NFB, con);
                sqlCommand.ExecuteNonQuery();


                con.Close();
                con.Open();


                sql_delete_order += " CONVERT( varchar,  ID_Book ) = '" + id_book + "'";

                SqlCommand sqlCommandC = new SqlCommand(sql_delete_order, con);
                sqlCommandC.ExecuteNonQuery();


                con.Close();
                con.Open();


                sql_delete_book += " CONVERT( varchar,  ID_Book ) = '" + id_book + "'";

                SqlCommand sqlCommandE = new SqlCommand(sql_delete_book, con);
                sqlCommandE.ExecuteNonQuery();


                con.Close();
                con.Open();


                sql_delete_Author += " CONVERT( varchar,  ID_Author ) = '" + id_author + "'";

                SqlCommand sqlCommandD = new SqlCommand(sql_delete_Author, con);
                sqlCommandD.ExecuteNonQuery();

                HTTP_msg http_MsgT = new HTTP_msg();
                http_MsgT.handle_msg(resp, "Deleted Book", "HTTP 1.1 OK 200");
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
