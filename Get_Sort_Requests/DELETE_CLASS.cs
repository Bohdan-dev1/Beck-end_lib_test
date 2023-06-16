using Beck_end_lib.Get_Sort_Requests.Default;
using Beck_end_lib.Get_Sort_Requests.DELETE;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Beck_end_lib.Get_Sort_Requests
{
    interface IAnswerDELETE
    {
        void delete_order(HttpListenerResponse resp, SqlConnection connect, string url);
        void delete_client(HttpListenerResponse resp, SqlConnection connect, string url);
        void delete_book(HttpListenerResponse resp, SqlConnection connect, string url);

        void delete_author(HttpListenerResponse resp, SqlConnection connect, string url);
    }

    interface IAnswerProcessorDELETE
    {
        void DELETE(IAnswerDELETE update,
        HttpListenerResponse resp,
        SqlConnection connect,
        string url,
        string rawURL);
    }

    public class DeleteClassAPI : IAnswerProcessorDELETE
    {
        void IAnswerProcessorDELETE.DELETE(IAnswerDELETE del, HttpListenerResponse resp, SqlConnection connect, string url, string rawURL)
        {
            switch (rawURL)
            {
                case "/v1/DELETE/CLIENT":
                    del.delete_client(resp, connect, url);
                    break;

                case "/v1/DELETE/BOOK":
                    del.delete_book(resp, connect, url);
                    break;

                case "/v1/DELETE/Author":
                    del.delete_author(resp, connect, url);
                    break;

                case "/v1/DELETE/ORDER":
                    del.delete_order(resp, connect, url);
                    break;

                default:
                    Default_Request default_Request = new Default_Request();
                    default_Request.Undefined_URL(resp, "not found url (DELETE request) - " + rawURL);
                    break;
            }
        }
    }

    public class DELETE_CLASS : IAnswerDELETE
    {
        void IAnswerDELETE.delete_author(HttpListenerResponse resp, SqlConnection connect, string url)
        {
            new Delete_class().DELETE_AUTHOR(resp, connect, url);
        }

        void IAnswerDELETE.delete_book(HttpListenerResponse resp, SqlConnection connect, string url)
        {
            new Delete_class().DELETE_BOOK(resp, connect, url);
        }

        void IAnswerDELETE.delete_client(HttpListenerResponse resp, SqlConnection connect, string url)
        {
            new Delete_class().DELETE_CLIENT(resp, connect, url);
        }

        void IAnswerDELETE.delete_order(HttpListenerResponse resp, SqlConnection connect, string url)
        {
            new Delete_class().DELETE_ORDER(resp, connect, url);
        }
    }
}
