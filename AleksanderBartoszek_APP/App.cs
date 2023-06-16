using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AleksanderBartoszek_APP
{
    internal class App
    {
        private static String sqlConnection = "Data Source=localhost;Initial Catalog=master;Integrated Security=True";

        public void Run()
        {
            int option;
            while (true)
            {
                DisplayMenu();
                option = ReadIntBetween(0, 7, " Please choose option: ");

                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        CreateTablePrompt();
                        break;
                    case 2:
                        InsertPrompt();
                        break;
                    case 3:
                        DeletePrompt();
                        break;
                    case 4:
                        FindPrompt();
                        break;
                    case 5:
                        UpdatePrompt();
                        break;
                    case 6:
                        DisplayPrompt();
                        break;
                    case 7:
                        RunTest();
                        break;
                    default:
                        break;
                }

            }
        }
        private void DisplayMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("-              Functionalities             -");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("- 1. Create new table                      -");
            Console.WriteLine("- 2. Insert XML Document                   -");
            Console.WriteLine("- 3. Delete XML Document                   -");
            Console.WriteLine("- 4. Find XML Document                     -");
            Console.WriteLine("- 5. Update XML Docuemnt                   -");
            Console.WriteLine("- 6. Display table content                 -");
            Console.WriteLine("- 7. Run tests                             -");
            Console.WriteLine("-                                          -");
            Console.WriteLine("- 0. Exit                                  -");
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();
        }
        private void CreateTablePrompt()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("-            Create new table              -");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-         Please enter table name:         -");
            Console.WriteLine("--------------------------------------------");
            string tableName = Console.ReadLine();
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-         Please enter XML schema:         -");
            Console.WriteLine("--------------------------------------------");
            string schema = ReadMultipleLines();
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();

            CreateTableQuery(tableName, schema);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(" Press enter to continue ... ");
            Console.ResetColor();
            Console.ReadLine();
        }
        private void CreateTableQuery(string tableName, string schema)
        {
            String q = "EXEC dbo.CreateTableUsingXmlSchema @name, @schema";
            SqlConnection c = new SqlConnection(sqlConnection);

            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand(q, c);
                cmd.Parameters.Add("@name", SqlDbType.VarChar);
                cmd.Parameters["@name"].Value = tableName;
                cmd.Parameters.Add("@schema", SqlDbType.VarChar);
                cmd.Parameters["@schema"].Value = schema;
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" table '{tableName}' created ");
                Console.ResetColor();
            }
            catch (SqlException e)
            {
                Console.WriteLine(" Error occured: " + e.Message);
            }
        }
        private void InsertPrompt()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("-          Insert XML Document             -");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-         Please enter table name:         -");
            Console.WriteLine("--------------------------------------------");
            string tableName = Console.ReadLine();
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-        Please enter XML document:        -");
            Console.WriteLine("--------------------------------------------");
            string doc = ReadMultipleLines();
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();

            InsertQuery(tableName, doc);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(" Press enter to continue ... ");
            Console.ResetColor();
            Console.ReadLine();
        }
        private void InsertQuery(string tableName, string doc)
        {
            String q = "EXEC dbo.InsertXML @tableName, @xmlData";
            SqlConnection c = new SqlConnection(sqlConnection);

            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand(q, c);
                cmd.Parameters.Add("@tableName", SqlDbType.VarChar);
                cmd.Parameters["@tableName"].Value = tableName;
                cmd.Parameters.Add("@xmlData", SqlDbType.VarChar);
                cmd.Parameters["@xmlData"].Value = doc;
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" record inserted to '{tableName}'");
                Console.ResetColor();
            }
            catch (SqlException e)
            {
                Console.WriteLine(" Error occured: " + e.Message);
            }
        }
        private void DeletePrompt()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("-            Delete XML Document           -");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-         Please enter table name:         -");
            Console.WriteLine("--------------------------------------------");
            string tableName = Console.ReadLine();
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-   Please enter condition for deleting:   -");
            Console.WriteLine("--------------------------------------------");
            string condition = ReadMultipleLines();
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();

            DeleteQuery(tableName, condition);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(" Press enter to continue ... ");
            Console.ResetColor();
            Console.ReadLine();
        }
        private void DeleteQuery(string tableName, string condition)
        {
            String q = "EXEC DeleteXML @tableName, @condition";
            SqlConnection c = new SqlConnection(sqlConnection);

            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand(q, c);
                cmd.Parameters.Add("@tableName", SqlDbType.VarChar);
                cmd.Parameters["@tableName"].Value = tableName;
                cmd.Parameters.Add("@condition", SqlDbType.VarChar);
                cmd.Parameters["@condition"].Value = condition;
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" record(s) deleted from '{tableName}'");
                Console.ResetColor();
            }
            catch (SqlException e)
            {
                Console.WriteLine(" Error occured: " + e.Message);
            }
        }
        private void FindPrompt()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("-             Find XML Document            -");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-         Please enter table name:         -");
            Console.WriteLine("--------------------------------------------");
            string tableName = Console.ReadLine();
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-        Please enter element name:        -");
            Console.WriteLine("--------------------------------------------");
            string elementName = Console.ReadLine();
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-        Please enter element value:       -");
            Console.WriteLine("--------------------------------------------");
            string elementValue = ReadMultipleLines();
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();

            FindQuery(tableName, elementName, elementValue);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(" Press enter to continue ... ");
            Console.ResetColor();
            Console.ReadLine();
        }
        private void FindQuery(string tableName, string elementName, string elementValue)
        {
            String q = $"SELECT dbo.FindXML(@tableName, @elementName, @elementValue) AS FoundRecord";
            SqlConnection c = new SqlConnection(sqlConnection);

            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand(q, c);
                cmd.Parameters.Add("@tableName", SqlDbType.VarChar);
                cmd.Parameters["@tableName"].Value = tableName;
                cmd.Parameters.Add("@elementName", SqlDbType.VarChar);
                cmd.Parameters["@elementName"].Value = elementName;
                cmd.Parameters.Add("@elementValue", SqlDbType.VarChar);
                cmd.Parameters["@elementValue"].Value = elementValue;
                SqlDataReader d = cmd.ExecuteReader();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("-              Found record                -");
                Console.WriteLine("--------------------------------------------");
                Console.ResetColor();
                while (d.Read())
                {
                    Console.WriteLine(d["FoundRecord"].ToString());
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(" Error occured: " + e.Message);
            }
        }
        private void UpdatePrompt()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("-            Update XML Document           -");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-         Please enter table name:         -");
            Console.WriteLine("--------------------------------------------");
            string tableName = Console.ReadLine();
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-        Please enter element name:        -");
            Console.WriteLine("--------------------------------------------");
            string elementName = Console.ReadLine();
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-      Please enter new element value:     -");
            Console.WriteLine("--------------------------------------------");
            string newValue = Console.ReadLine();
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-   Please enter condition for updating:   -");
            Console.WriteLine("--------------------------------------------");
            string condition = ReadMultipleLines();
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();

            UpdateQuery(tableName, elementName, newValue, condition);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(" Press enter to continue ... ");
            Console.ResetColor();
            Console.ReadLine();
        }
        private void UpdateQuery(string tableName, string elementName, string newValue, string condition)
        {
            String q = "EXEC dbo.UpdateXML @tableName, @elementName, @newValue, @condition";
            SqlConnection c = new SqlConnection(sqlConnection);

            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand(q, c);
                cmd.Parameters.Add("@tableName", SqlDbType.VarChar);
                cmd.Parameters["@tableName"].Value = tableName;
                cmd.Parameters.Add("@elementName", SqlDbType.VarChar);
                cmd.Parameters["@elementName"].Value = elementName;
                cmd.Parameters.Add("@newValue", SqlDbType.VarChar);
                cmd.Parameters["@newValue"].Value = newValue;
                cmd.Parameters.Add("@condition", SqlDbType.VarChar);
                cmd.Parameters["@condition"].Value = condition;
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" record(s) updated in '{tableName}'");
                Console.ResetColor();
            }
            catch (SqlException e)
            {
                Console.WriteLine(" Error occured: " + e.Message);
            }
        }
        private void DisplayPrompt()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("-           Display table content          -");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("-         Please enter table name:         -");
            Console.WriteLine("--------------------------------------------");
            string tableName = Console.ReadLine();
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();

            DisplayQuery(tableName);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(" Press enter to continue ... ");
            Console.ResetColor();
            Console.ReadLine();
        }
        private void DisplayQuery(string tableName)
        {
            String q = $"SELECT * FROM {tableName}";
            SqlConnection c = new SqlConnection(sqlConnection);

            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand(q, c);
                SqlDataReader d = cmd.ExecuteReader();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("-              Table content               -");
                Console.WriteLine("--------------------------------------------");
                Console.ResetColor();
                while (d.Read())
                {
                    Console.WriteLine(d["XMLData"].ToString());
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(" Error occured: " + e.Message);
            }
        }
        private void DropQuery(string tableName)
        {
            String q = $"DROP TABLE {tableName}";
            SqlConnection c = new SqlConnection(sqlConnection);

            try
            {
                c.Open();
                SqlCommand cmd = new SqlCommand(q, c);
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" table '{tableName}' deleted");
                Console.ResetColor();
            }
            catch (SqlException e)
            {
                Console.WriteLine(" Error occured: " + e.Message);
            }
        }
        private void RunTest()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-                    Running tests                       -");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-            Creating new table 'ShippingInfo'           -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            CreateTableQuery("ShippingInfo", @"
                <xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'>
                    <xs:element name ='shipto'>
                        <xs:complexType>
                            <xs:sequence>
                                <xs:element name ='name' type='xs:string'/>
                                <xs:element name ='address' type='xs:string'/>
                                <xs:element name ='city' type='xs:string'/>
                                <xs:element name ='country' type='xs:string'/>
                            </xs:sequence>
                        </xs:complexType>
                    </xs:element>
                </xs:schema>
            ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-           Inserting records to 'ShippingInfo'          -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            InsertQuery("ShippingInfo", @"
                <shipto>
                  <name>John Doe</name>
                  <address>123 Main Street</address>
                  <city>Szczecin</city>
                  <country>Poland</country>
                </shipto>
            ");
            InsertQuery("ShippingInfo", @"
                <shipto>
                  <name>John Doe</name>
                  <address>456 Long Alley</address>
                  <city>Radom</city>
                  <country>Norway</country>
                </shipto>
            ");
            InsertQuery("ShippingInfo", @"
                <shipto>
                  <name>Jack Walker</name>
                  <address>Nowowiejska 53</address>
                  <city>Warszawa</city>
                  <country>Poland</country>
                </shipto>
            ");
            InsertQuery("ShippingInfo", @"
                <shipto>
                  <name>Jack Sparrow</name>
                  <address>12 This Street</address>
                  <city>Bay</city>
                  <country>Tortuga</country>
                </shipto>
            ");
            InsertQuery("ShippingInfo", @"
                <shipto>
                  <name>Jack Sparrow</name>
                  <address>21 That Street</address>
                  <city>Bay</city>
                  <country>Tortuga</country>
                </shipto>
            ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-            Displaying current table content            -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            DisplayQuery("ShippingInfo");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-          Deleting records with name 'John Doe'         -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            DeleteQuery("ShippingInfo", "XMLData LIKE '%<name>John Doe</name>%'");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-            Displaying current table content            -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            DisplayQuery("ShippingInfo");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-     Finding first record with name 'Jack Sparrow'      -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            FindQuery("ShippingInfo", "name", "Jack Sparrow");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("- Updating records 'Jack Sparrow' to 'cpt. Jack Sparrow' -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            UpdateQuery("ShippingInfo", "name", "cpt. Jack Sparrow", "XMLData LIKE '%<name>Jack Sparrow</name>%'");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-            Displaying current table content            -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            DisplayQuery("ShippingInfo");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-                        Cleanup                         -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
            DropQuery("ShippingInfo");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine(" Press enter to continue ... ");
            Console.ResetColor();
            Console.ReadLine();
        }

        private string ReadMultipleLines()
        {
            StringBuilder sb = new StringBuilder();
            string line = Console.ReadLine();
            while (line != "")
            {
                sb.Append(line);
                line = Console.ReadLine();
            }
            return sb.ToString();
        }

        private int ReadIntBetween(int min, int max, string info)
        {
            int result = -1;
            bool valid = false;
            string input;
            Console.Write(info);

            while (!valid)
            {
                input = Console.ReadLine();
                try
                {
                    result = Convert.ToInt32(input);
                    if (result >= min && result <= max)
                    {
                        valid = true;
                    }
                    else
                    {
                        Console.Write("Incorrect number. Please try again: ");
                    }
                }
                catch (Exception)
                {
                    Console.Write("Incorrect entry. Please try again: ");
                }
            }
            return result;
        }
    }
}
