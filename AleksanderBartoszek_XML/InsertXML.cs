using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Xml;

public partial class InsertXML
{
    [SqlProcedure]
    public static void InsertXmlData(string tableName, SqlString xmlData)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xmlData.Value);
                    SqlParameter xmlParam = new SqlParameter("@XmlData", SqlDbType.Xml)
                    {
                        Value = new SqlXml(new XmlTextReader(xmlDocument.OuterXml, XmlNodeType.Document, null))
                    };
                    command.CommandText = $"INSERT INTO {tableName} (XMLData) VALUES (CONVERT(NVARCHAR(4000), @XmlData))";
                    command.Parameters.Add(xmlParam);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error inserting XML record: " + ex.Message);
        }
    }
}