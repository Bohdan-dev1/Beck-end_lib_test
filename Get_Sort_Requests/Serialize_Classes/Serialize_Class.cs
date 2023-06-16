using System.Net;
using System.Text.Json;
using Beck_end_lib.Get_Sort_Requests.SEND;
using Microsoft.Data.SqlClient;


namespace Beck_end_lib.Get_Sort_Requests.Serialize_Classes
{
    public class About_Order_Serialize
    {
        public int? ID_Order { get; set; }
        public string? Name_book { get; set; } 
        public string? Date_Get_order { get; set; }
        public string? Date_Return_Book { get; set; }
        public string? Date_Need_Return { get; set; }

    }

    public class About_Client_Serialize
    {
        public int? ID_client { get; set; }
        public string? Full_name_client { get; set; }
        public string? Number_Phone_client { get; set; }
        public string? Email_Client { get; set; }
        public string? Address_client { get; set; }
        public string? NIE_code_client { get; set; }
        public List<About_Order_Serialize>? List_books { get; set; }

    }


    public class About_Books_Serialize
    {
        public int? ID_Book { get; set; }
        public string? Name_Book { get; set; }
        public int? How_many_books { get; set; }
        public string? Full_Name_Author { get; set; }
        public string? Yeer_Public { get; set; }
        public List<string>? Ahter_books { get; set; }

    }

    public class ANSWER_SERIALIZE_CLASS<T>
    {
        public string? Date_answer { get; set; }
        public string? Name_answer { get; set; }
        public List<T>? Json_answer { get; set; }
    }

    public class MSGr
    {
        public string? MSG { get; set; }
        public string? column_name { get; set; }
    }

    internal class Get_Value
    {
        public static string FieldsFromURL(string s)
        {
            string tempOne = (s.Split("%22"))[1];
            string[] tempTwo = (tempOne.Split("%20"));

            tempOne = "";
            foreach (string temp in tempTwo) tempOne += temp + " ";
            tempOne = tempOne[..^1];

            return tempOne;
        } 

        public static string Get_string_search (string s)
        {
            string[] temp = s.Split(" ");
            string res = "%";

            foreach (string t in temp) res += t + "%";
            
            return res;
        }


        public static Dictionary<string, string> GetDictionaryRequest(string url)
        {
            string allRequests = url.Split("?")[1];
            string[] requests = allRequests.Split("&");
            Dictionary<string, string> listRequests = new Dictionary<string, string>();

            foreach (string request in requests)
            {
                string[] temp = request.Split("=");
                listRequests.Add(temp[0], temp[1]);
            }

            return listRequests;
        }
    } 

    internal class HTTP_msg
    {
        public void handle_msg(HttpListenerResponse resp, params string[] arg)
        {
            MSGr not_Found_In_URL = new MSGr
            {
                MSG = arg[0],
                column_name = arg[1],
            };

            string data = JsonSerializer.Serialize(not_Found_In_URL);
            Send.SendData(resp, data);
        }
    }

}
