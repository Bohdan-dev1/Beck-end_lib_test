using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;

using Beck_end_lib.Get_Sort_Requests.POST;
using Beck_end_lib.Get_Sort_Requests.Default;

namespace Beck_end_lib.Get_Sort_Requests
{
    interface IAnswerPOST
    {
        void POST_NEW_CLIENT_api(HttpListenerResponse resp, SqlConnection con, string urlRequest);
        void POST_NEW_ORDER_api(HttpListenerResponse resp, SqlConnection con, string urlRequest);

        void POST_NEW_BOOK_api(HttpListenerResponse resp, SqlConnection con, string urlRequest);
    };

    interface IAnswerProcessorPost
    {
        void Post
            (IAnswerPOST post,
            HttpListenerResponse resp,
            SqlConnection connect,
            string url,
            string rawURL);
    };

    public class PostClassAPI : IAnswerProcessorPost
    {
        void IAnswerProcessorPost.Post(IAnswerPOST post, HttpListenerResponse resp, SqlConnection connect, string url, string rawURL)
        {
            switch (rawURL) {
                case "/v1/POST/NEW_CLIENT":
                    post.POST_NEW_CLIENT_api(resp, connect, url);
                    break;

                case "/v1/POST/NEW_ORDER":
                    post.POST_NEW_ORDER_api(resp, connect, url);
                    break;

                case "/v1/POST/ADD_Book":
                    post.POST_NEW_BOOK_api (resp, connect, url);
                    break;

                default:
                    Default_Request default_Request = new Default_Request();
                    default_Request.Undefined_URL(resp, "not found url (POST request) - " + rawURL);
                    break;
            }
        }
    }

    internal class POST_CLASS : IAnswerPOST
    {
        public void POST_NEW_BOOK_api(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Post_Add_Book postBook = new Post_Add_Book();
            postBook.PostNewBook(resp, con, urlRequest);
        }

        void IAnswerPOST.POST_NEW_CLIENT_api(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Post_About_Client postCLient = new Post_About_Client();
            postCLient.POST_NEW_CLIENT(resp, con, urlRequest);
        }

        void IAnswerPOST.POST_NEW_ORDER_api(HttpListenerResponse resp, SqlConnection con, string urlRequest)
        {
            Post_About_Client postCLient = new Post_About_Client();
            postCLient.POST_NEW_ORDER(resp, con, urlRequest);
        }
    }
}
