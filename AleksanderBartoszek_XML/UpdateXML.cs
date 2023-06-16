using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Xml;
public partial class UpdateXML
{
    [SqlProcedure]
    public static void UpdateXMLData(string tableName, string elementName, string newElementValue, string condition)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();
                string sqlSelect = $"SELECT XMLData FROM {tableName}";
                string xmlString;

                using (SqlCommand selectCommand = new SqlCommand(sqlSelect, connection))
                {
                    xmlString = selectCommand.ExecuteScalar()?.ToString();
                }
                
                if (!string.IsNullOrEmpty(xmlString))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlString);
                    XmlNode targetNode = xmlDoc.SelectSingleNode($"//{elementName}");

                    if (targetNode != null)
                    {
                        targetNode.InnerText = newElementValue;
                        string updatedXmlString = xmlDoc.OuterXml;
                        string sqlUpdate = $"UPDATE {tableName} SET XMLData = @UpdatedXml WHERE {condition}";
                        using (SqlCommand updateCommand = new SqlCommand(sqlUpdate, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@UpdatedXml", updatedXmlString);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating record: " + ex.Message);
        }
    }
}
