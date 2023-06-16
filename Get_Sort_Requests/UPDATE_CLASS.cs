using Beck_end_lib.Get_Sort_Requests.Default;
using Beck_end_lib.Get_Sort_Requests.UPDATE;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Beck_end_lib.Get_Sort_Requests
{
    interface IAnswerUPDATE
    {
        void PUT_Client(HttpListenerResponse resp, SqlConnection con, string urlRequest);
        void PUT_Book(HttpListenerResponse resp, SqlConnection con, string urlRequest);
        void PUT_Author(HttpListenerResponse resp, SqlConnection con, string urlRequest);
        void PUT_ORDER(HttpListenerResponse resp, SqlConnection con, string urlRequest);
        void PUT_BOOK_HM(HttpListenerResponse resp, SqlConnection con, string urlRequest);
    }

    interface IAnswerProcessorUpdate
    {
        void Put(IAnswerUPDATE update,
            HttpListenerResponse resp,
            SqlConnection connect,
            string url,
            string rawURL);
    }


    public class UpdateCLassAPI : IAnswerProcessorUpdate
    {
        void IAnswerProcessorUpdate.Put(IAnswerUPDATE update, HttpListenerResponse resp, SqlConnection connect, string url, string rawURL)
        {
            switch (rawURL)
            {
                case "/v1/PUT_UPDATE/CLIENT":
                    update.PUT_Client(resp, connect, url);
                    break;

                case "/v1/PUT_UPDATE/BOOK":
                    update.PUT_Book(resp, connect, url);
                    break;

                case "/v1/PUT_UPDATE/Author":
                    update.PUT_Author(resp, connect, url);
                    break;

                case "/v1/PUT_UPDATE/ORDER":
                    update.PUT_ORDER(resp, connect, url);
                    break;

                case "/v1/PUT_UPDATE/BOOK_how_meny":
                    update.PUT_BOOK_HM(resp, connect, url);
                    break;

                default:
                    Default_Request default_Request = new Default_Request();
                    default_Request.Undefined_URL(resp, "not found url (UPDATE - PUT request) - " + rawURL);
                    break;
            }
        }
    }

    internal class UPDATE_CLASS : IAnswerUPDATE
    {
        void IAnswerUPDATE.PUT_Author(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Update_client_info update_Client_Info = new Update_client_info();
            string teble = "dbo.About_Author";
            string keyROW = "ID_Author";
            string[] keySearch = { keyROW, "Full_name" };
            List<Class_ROW_Client> rows_search_by = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    type_data = keyROW,
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "Full_name",
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "Update_data",
                    row = ""
                }
            };

            List<Class_ROW_Client> rows_value_update = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    row = "VARCHAR",
                    type_data = "Full_name",
                },
            };

            update_Client_Info.UPDATE_DATA(resp, con, urlRequest, keyROW, teble, rows_search_by, rows_value_update, keySearch);
        }

        void IAnswerUPDATE.PUT_Book(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Update_client_info update_Client_Info = new Update_client_info();
            string teble = "dbo.Book";
            string keyROW = "ID_Book";
            string[] keySearch = { keyROW, "Name_Book", "Year_Public" };
            List<Class_ROW_Client> rows_search_by = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    type_data = keyROW,
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "Name_Book",
                    row = ""
                },                
                
                new Class_ROW_Client
                {
                    type_data = "Year_Public",
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "Update_data",
                    row = ""
                }
            };

            List<Class_ROW_Client> rows_value_update = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    row = "VARCHAR",
                    type_data = "Name_Book",
                },

                new Class_ROW_Client
                {
                    row = "INT",
                    type_data = "ID_Author",
                },

                new Class_ROW_Client
                {
                    row = "INT",
                    type_data = "Year_Public",
                },
            };

            update_Client_Info.UPDATE_DATA(resp, con, urlRequest, keyROW, teble, rows_search_by, rows_value_update, keySearch);
        }

        void IAnswerUPDATE.PUT_BOOK_HM(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Update_client_info update_Client_Info = new Update_client_info();
            List<Class_ROW_Client> rows_search_by = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    type_data = "ID",
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "ID_Book",
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "Update_data",
                    row = ""
                }
            };

            List<Class_ROW_Client> rows_value_update = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    row = "INT",
                    type_data = "How_many",
                },

            };

            string teble = "dbo.Number_Of_Books";
            string keyROW = "ID_Book";
            string[] keySearch = { "ID_Book", "How_many" };
            update_Client_Info.UPDATE_DATA(resp, con, urlRequest, keyROW, teble, rows_search_by, rows_value_update, keySearch);

        }

        void IAnswerUPDATE.PUT_Client(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Update_client_info update_Client_Info = new Update_client_info();
            List<Class_ROW_Client> rows_search_by = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    type_data = "Person_ID",
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "Full_Name",
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "Update_data",
                    row = ""
                }
            };

            List<Class_ROW_Client> rows_value_update = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    row = "VARCHAR",
                    type_data = "Full_Name",
                },

                new Class_ROW_Client
                {
                    row = "VARCHAR",
                    type_data = "Numer_Phone",
                },

                new Class_ROW_Client
                {
                    row = "VARCHAR",
                    type_data = "Email",
                },

                new Class_ROW_Client
                {
                    row = "VARCHAR",
                    type_data = "Address",
                },

                new Class_ROW_Client
                {
                    row = "VARCHAR",
                    type_data = "NIE_Code",
                }
            };

            string teble = "dbo.Reader_Person";
            string keyROW = "Person_ID";
            string[] keySearch = { "Person_ID", "Full_Name" };
            update_Client_Info.UPDATE_DATA(resp, con, urlRequest, keyROW, teble, rows_search_by, rows_value_update, keySearch);
        }

        void IAnswerUPDATE.PUT_ORDER(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Update_client_info update_Client_Info = new Update_client_info();
            List<Class_ROW_Client> rows_search_by = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    type_data = "ID_Order",
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "ID_Reader",
                    row = ""
                },

                new Class_ROW_Client
                {
                    type_data = "ID_book",
                    row = ""
                }
            };

            List<Class_ROW_Client> rows_value_update = new List<Class_ROW_Client>
            {
                new Class_ROW_Client
                {
                    row = "INT",
                    type_data = "ID_Reader",
                },

                new Class_ROW_Client
                {
                    row = "INT",
                    type_data = "ID_book",
                },

                new Class_ROW_Client
                {
                    row = "CHAR(15)",
                    type_data = "Date_Get",
                },

                new Class_ROW_Client
                {
                    row = "CHAR(15)",
                    type_data = "Date_Need_Return",
                },

                new Class_ROW_Client
                {
                    row = "CHAR(15)",
                    type_data = "Date_Return",
                }
            };

            string teble = "dbo.Issued_Books";
            string keyROW = "ID_Order";
            string[] keySearch = { "ID_Order", "ID_Reader", "ID_book" };
            update_Client_Info.UPDATE_DATA(resp, con, urlRequest, keyROW, teble, rows_search_by, rows_value_update, keySearch);
        }
    }

}
