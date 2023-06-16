using Beck_end_lib.Get_Sort_Requests.Default;
using Beck_end_lib.Get_Sort_Requests.GET;
using Beck_end_lib.Get_Sort_Requests.Serialize_Classes;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Beck_end_lib.Get_Sort_Requests.UPDATE
{
    internal class Update_client_info
    {

        public void UPDATE_DATA(HttpListenerResponse resp, SqlConnection con, string url,string keyROW, string table, List<Class_ROW_Client> rowID, List<Class_ROW_Client> rowUpdate, string[] keySearch ,bool debug = true)
        {
            Dictionary<string, string> listRequests = Get_Value.GetDictionaryRequest(url);

            try
            {
                string sql_res_pOne = "UPDATE " + table + " SET ";
                string sql_res_pTwo = " WHERE ";
                string[] id_and_name = new string[2];
                int index = 0;

                string value;

                foreach (Class_ROW_Client rowIDt in rowID)
                {
                    if (rowIDt.type_data == "Update_data") continue;
                    if (listRequests.TryGetValue(rowIDt.type_data, out value))
                    {
                        try
                        {
                            listRequests[rowIDt.type_data] = Get_Value.FieldsFromURL(listRequests[rowIDt.type_data]);
                            sql_res_pTwo += " CONVERT( varchar, " + rowIDt.type_data + ") = '" + listRequests[rowIDt.type_data] + "'";
                            rowID[index].row = listRequests[rowIDt.type_data];
                        }
                        catch { continue; }
                    }
                    index++;

                }

                foreach (string check in keySearch)
                {
                    if (!new SQL_GET().isInTable(con, rowID, table, keyROW, "Update_data", check))
                    {
                        HTTP_msg http_Msg = new HTTP_msg();
                        http_Msg.handle_msg(resp, "Such a reader does not exist", "HTTP 1.1 OK 200");

                        return;
                    }
                }



                if (listRequests.TryGetValue("Update_data", out value))
                {
                    if (debug == true)
                    {
                        listRequests["Update_data"] = url.Split("[")[1];
                        string urlC = listRequests["Update_data"][..^1];
                        Dictionary<string, string> listRequestsUpdate = Get_Value.GetDictionaryRequest(urlC);
                        

                        foreach (Class_ROW_Client valueROW in rowUpdate)
                        {
                            if (listRequestsUpdate.TryGetValue(valueROW.type_data, out value))
                            {
                                listRequestsUpdate[valueROW.type_data] = Get_Value.FieldsFromURL(listRequestsUpdate[valueROW.type_data]);
                                sql_res_pOne += valueROW.type_data + " = CONVERT( " + valueROW.row + ", '" + listRequestsUpdate[valueROW.type_data] + "'), ";
                            }
                        }

                        sql_res_pOne = sql_res_pOne[..^2];
                        sql_res_pOne += sql_res_pTwo;
                        new Update_class_sql().Update_SQL(resp, sql_res_pOne, con);
                    }
                }
                else
                {
                    HTTP_msg http_MsgT = new HTTP_msg();
                    http_MsgT.handle_msg(resp, "Not found Update_date ", "HTTP 1.1 OK 200, Update_date, Data not updating");
                }
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
