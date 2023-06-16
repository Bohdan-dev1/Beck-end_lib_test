using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Text.Json;
using System.Net;

using Beck_end_lib.Get_Sort_Requests.SEND;
using Beck_end_lib.Get_Sort_Requests.Serialize_Classes;


namespace Beck_end_lib.Get_Sort_Requests.GET
{
    internal class Get_About_Info_Client
    {
        string convet_string_for_like(string s)
        {
            string[] temp = s.Split("%22");
            temp = temp[1].Split("%20");
            string res2 = "%";
            if (temp.Length != 1)
            {
                foreach (string e in temp)
                {
                    res2 += e + "%";
                }
                return res2;
            }
            else return "%" + temp[0] + "%";

        }

        List<About_Order_Serialize> get_list_orders_clien(int client_ID, HttpListenerResponse resp)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconfingLink"].ConnectionString);
            sqlConnection.Open();
            List<About_Order_Serialize> list_order_person = new List<About_Order_Serialize>();
            string sql_select = new SQL_GET().sql_get_info_client + client_ID.ToString() + ";";

            try
            {
                SqlCommand sqlCommandRun = new SqlCommand(sql_select, sqlConnection);
                SqlDataReader reader = sqlCommandRun.ExecuteReader();
                while (reader.Read())
                {
                    list_order_person.Add(new About_Order_Serialize
                    {
                        ID_Order = (int)reader["ID_Order"],
                        Name_book = (string)reader["Name_Book"],
                        Date_Get_order = reader["Date_Get"].ToString(),
                        Date_Return_Book = reader["Date_Return"].ToString(),
                        Date_Need_Return = reader["Date_Need_Return"].ToString(),

                    });
                }
                sqlConnection.Close();

            }
            catch (Exception)
            {
                sqlConnection.Close();
                return list_order_person;
            }


            return list_order_person;
        }
        public void SEND_AND_SERIALIZE_JSON(HttpListenerResponse resp, SqlConnection con, string customSQL)
        {
            SqlCommand sqlCommandRun = new SqlCommand(customSQL == "" ? new SQL_GET().sql_get_all_clients : customSQL, con);
            List<About_Client_Serialize> list_cliens_data = new List<About_Client_Serialize>();

            try
            {
                SqlDataReader reader = sqlCommandRun.ExecuteReader();
                int coutCreating = 1, lastID = 1;
                while (reader.Read())
                {
                    if (coutCreating == 1)
                    {
                        list_cliens_data.Add(new About_Client_Serialize
                        {
                            ID_client = (int)reader["Person_ID"],
                            Full_name_client = (string)reader["Full_Name"],
                            Number_Phone_client = (string)reader["Numer_Phone"],
                            Email_Client = (string)reader["Email"],
                            Address_client = (string)reader["Address"],
                            NIE_code_client = (string)reader["NIE_Code"],
                            List_books = get_list_orders_clien((int)reader["Person_ID"], resp)
                        });
                        lastID = (int)reader["Person_ID"];
                        coutCreating++;
                    }
                    if (lastID != (int)reader["Person_ID"])
                    {
                        coutCreating = 1;
                        list_cliens_data.Add(new About_Client_Serialize
                        {
                            ID_client = (int)reader["Person_ID"],
                            Full_name_client = (string)reader["Full_Name"],
                            Number_Phone_client = (string)reader["Numer_Phone"],
                            Email_Client = (string)reader["Email"],
                            Address_client = (string)reader["Address"],
                            NIE_code_client = (string)reader["NIE_Code"],
                            List_books = get_list_orders_clien((int)reader["Person_ID"], resp)
                        });
                    }
                }
                IEnumerable< About_Client_Serialize > dataList = list_cliens_data.DistinctBy(x => x.ID_client);

                string data = JsonSerializer.Serialize(dataList);
                Send.SendData(resp, data);
            }
            catch (Exception)
            {
                Send.SendData(resp, "{\"Error\" : \"Incorrect URL\"}");
                return;
            }
        }


        public void SQL_GET_CLIENTS(HttpListenerResponse resp, SqlConnection con)
        {
            SqlCommand sqlCommandRun = new SqlCommand(new SQL_GET().sqlSriptWithoutOrder, con);
            List<About_Client_Serialize> list_cliens_data = new List<About_Client_Serialize>();

            try
            {
                SqlDataReader reader = sqlCommandRun.ExecuteReader();
                while (reader.Read())
                {
                    list_cliens_data.Add(new About_Client_Serialize
                    {
                        ID_client = (int)reader["Person_ID"],
                        Full_name_client = (string)reader["Full_Name"],
                        Number_Phone_client = (string)reader["Numer_Phone"],
                        Email_Client = (string)reader["Email"],
                        Address_client = (string)reader["Address"],
                        NIE_code_client = (string)reader["NIE_Code"],
                    });

                }

                string data = JsonSerializer.Serialize(list_cliens_data);
                Send.SendData(resp, data);
            }
            catch (Exception)
            {
                Send.SendData(resp, "{\"Error\" : \"Incorrect URL\"}");
                return;
            }
        }

        public void SQL_SEARCH_CLIENT(HttpListenerResponse resp, SqlConnection con, string url)
        {
            string[] request = url.Split('?');
            string[] stringReq = request[1].Split("&");
            string resAddRequest = new SQL_GET().sql_get_all_clients[..^1];


            string[] tempR = stringReq[0].Split("=");
            switch (tempR[0])
            {
                case "Name_client":
                    resAddRequest += " WHERE dbo.Reader_Person.Full_Name LIKE '" + convet_string_for_like(tempR[1]) + "';";
                    break;
                case "Email_client":
                    resAddRequest += " WHERE dbo.Reader_Person.Email LIKE '" + convet_string_for_like(tempR[1]) + "';";
                    break;
                case "NIE_client":
                    resAddRequest += " WHERE dbo.Reader_Person.NIE_Code LIKE '" + convet_string_for_like(tempR[1]) + "';";
                    break;
                case "Phone_client":
                    resAddRequest += " WHERE dbo.Reader_Person.Numer_Phone LIKE '" + convet_string_for_like(tempR[1]) + "';";
                    break;
                case "Client_Address":
                    resAddRequest += " WHERE dbo.Reader_Person.Address LIKE '" + convet_string_for_like(tempR[1]) + "';";
                    break;
                case "By_Client_ID":
                    resAddRequest += " WHERE dbo.Reader_Person.Person_ID = '" + tempR[1] + "';";
                    break;
            }

            SEND_AND_SERIALIZE_JSON(resp, con, resAddRequest);
        }
    }
}
