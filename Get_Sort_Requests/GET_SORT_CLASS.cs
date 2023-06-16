using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;

using Beck_end_lib.Get_Sort_Requests.SEND;
using Beck_end_lib.Get_Sort_Requests.GET;
using Beck_end_lib.Get_Sort_Requests.Default;
using Beck_end_lib.Get_Sort_Requests.Serialize_Classes;

namespace Beck_end_lib.Get_Sort_Requests
{
    interface IAnswerGET
    {
        void GET_ABOUT_SERVER(HttpListenerResponse resp);
        void GET_ALL_BOOKS(HttpListenerResponse resp, SqlConnection con);
        void GET_BOOK(HttpListenerResponse resp, SqlConnection con, string urlRequest, string param);
        void SEARCH_BY_INFO(HttpListenerResponse resp, SqlConnection con, string urlRequest);
        void GET_CLIENTS_LIST_ORDERS(HttpListenerResponse resp, SqlConnection con);
        void GET_CLIENTS_LIST(HttpListenerResponse resp, SqlConnection con);
        void SEARCH_CLIENT(HttpListenerResponse resp, SqlConnection con, string urlRequest);
    }

    interface IAnswerProcessor
    {
        void IAprocessor
            (IAnswerGET callFunc,
            HttpListenerResponse resp,
            SqlConnection connect,
            string url,
            string rawURL);
    }

    public class GetSortDATA : IAnswerProcessor
    {
        void IAnswerProcessor.IAprocessor
            (IAnswerGET callFunc,
            HttpListenerResponse resp,
            SqlConnection connect,
            string url,
            string rawURL)
        {

            switch (rawURL)
            {
                case "/v1/GET/AllBooks":
                    callFunc.GET_ALL_BOOKS(resp, connect);
                    break;

                case "/v1/GET/AboutServer":
                    callFunc.GET_ABOUT_SERVER(resp);
                    break;

                case "/v1/GET/AllClients/WithOrder":
                    callFunc.GET_CLIENTS_LIST_ORDERS(resp, connect);
                    break;

                case "/v1/GET/AllClients":
                    callFunc.GET_CLIENTS_LIST(resp, connect);
                    break;



                case "/v1/GET/Search/Book/M_AND":
                    callFunc.GET_BOOK(resp, connect, url, "AND");
                    break;

                case "/v1/GET/Search/Book/M_OR":
                    callFunc.GET_BOOK(resp, connect, url, "OR");
                    break;

                case "/v1/Search/Client":
                    callFunc.SEARCH_CLIENT(resp, connect, url);
                    break;

                case "/v1/Search/Book":
                    callFunc.SEARCH_BY_INFO(resp, connect, url);
                    break;

                default:
                    Default_Request default_Request = new Default_Request();
                    default_Request.Undefined_URL(resp, "not found url (GET request) - " + rawURL);
                    break;
            }
        }
    }

    internal class GET_CLASS : IAnswerGET
    {

        void IAnswerGET.GET_ABOUT_SERVER(HttpListenerResponse resp)
        {
            var answer_json = new ANSWER_SERIALIZE_CLASS<string>
            {
                Date_answer = DateTime.Now.ToString(),
                Name_answer = "ABOUT_SERVER",
                Json_answer = new List<string>() { { ".Net 6.0, core - Windows NT 11" } },
            };

            string data = JsonSerializer.Serialize(answer_json);
            Send.SendData(resp, data);
        }

        void IAnswerGET.GET_ALL_BOOKS(HttpListenerResponse resp, SqlConnection con)
        {
            Get_All_About_Books get_All_About_Books = new Get_All_About_Books();
            get_All_About_Books.JSON_All_Books(resp, con);
        }

        void IAnswerGET.GET_BOOK(HttpListenerResponse resp, SqlConnection con, string urlRequest, string param)
        {
            Get_All_About_Books get_All_About_Books = new Get_All_About_Books();
            get_All_About_Books.JSON_GET_BOOK(resp, con, urlRequest, param);
        }

        void IAnswerGET.SEARCH_BY_INFO(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Get_All_About_Books get_All_About_Books = new Get_All_About_Books();
            get_All_About_Books.JSON_SEARCH_BOOK(resp, con, urlRequest);
        }

        void IAnswerGET.GET_CLIENTS_LIST_ORDERS(HttpListenerResponse resp, SqlConnection con)
        {
            Get_About_Info_Client get_About_Info_Client = new Get_About_Info_Client();
            get_About_Info_Client.SEND_AND_SERIALIZE_JSON(resp, con, "");
        }
        void IAnswerGET.GET_CLIENTS_LIST(HttpListenerResponse resp, SqlConnection con)
        {
            Get_About_Info_Client get_About_Info_Client = new Get_About_Info_Client();
            get_About_Info_Client.SQL_GET_CLIENTS(resp, con);
        }
        void IAnswerGET.SEARCH_CLIENT(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Get_About_Info_Client get_About_Info_Client = new Get_About_Info_Client();
            get_About_Info_Client.SQL_SEARCH_CLIENT(resp, con, urlRequest);
        }


    }
}
