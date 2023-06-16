using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Xml;

public partial class DeleteXML
{
    [SqlProcedure]
    public static void DeleteXMLData(string tableName, string condition)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"DELETE FROM {tableName} WHERE {condition}";
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting record: " + ex.Message);
        }
    }
}