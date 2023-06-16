using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Xml.Schema;
using System.Xml;
using System.Text;

public partial class CreateTable
{
    [SqlProcedure]
    public static void CreateTableUsingXmlSchema(string tableName, string xmlSchema)
    {
        try
        {
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add("", XmlReader.Create(new System.IO.StringReader(xmlSchema)));
            schemaSet.Compile();

            XmlSchema mainSchema = null;
            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                mainSchema = schema;
                break;
            }

            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    StringBuilder query = new StringBuilder();
                    query.AppendFormat("CREATE TABLE {0} (", tableName);
                    foreach (XmlSchemaElement element in mainSchema.Elements.Values)
                    {
                        query.AppendFormat("[{0}] {1} NULL,", "XMLData", GetSqlType(element.SchemaType));
                    }
                    query.Append(")");
                    command.CommandText = query.ToString();
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating table: " + ex.Message);
        }
    }

    private static string GetSqlType(XmlSchemaType schemaType)
    {
        if (schemaType is XmlSchemaSimpleType)
        {
            XmlSchemaSimpleType simpleType = (XmlSchemaSimpleType)schemaType;
            if (simpleType.Datatype != null && simpleType.Datatype.ValueType != null)
            {
                switch (simpleType.Datatype.ValueType.Name)
                {
                    case "string":
                        return "NVARCHAR(MAX)";
                    case "int":
                        return "INT";
                    case "decimal":
                        return "DECIMAL(18, 2)";
                    case "boolean":
                        return "BIT";
                    case "dateTime":
                        return "DATETIME";
                    default:
                        return "NVARCHAR(MAX)";
                }
            }
        }
        return "NVARCHAR(MAX)";
    }
}