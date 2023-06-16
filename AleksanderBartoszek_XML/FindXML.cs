using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Xml;

public partial class FindXML
{
    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlXml FindXMLData(string tableName, string elementName, string elementValue)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"SELECT * FROM {tableName}";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string xmlData = reader.GetSqlString(reader.GetOrdinal("XMLData")).Value;
                            using (XmlReader xmlReader = XmlReader.Create(new System.IO.StringReader(xmlData)))
                            {
                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.Load(xmlReader);
                                XmlNodeList nodes = xmlDoc.GetElementsByTagName(elementName);

                                foreach (XmlNode node in nodes)
                                {
                                    if (node.InnerText == elementValue)
                                    {
                                        return new SqlXml(xmlDoc.CreateNavigator().ReadSubtree());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error finding record: " + ex.Message);
        }
        return SqlXml.Null;
    }
}
