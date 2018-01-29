using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
//Add in the using statements System.Data; System.Data.SqlClient; System.Configuration; and System.IO
namespace SQLSuppliers
{
    class Program
    {
        /// <summary>
        /// Writes an exception to the programs master log file.
        /// </summary>
        /// <param name="methodName">The name of the method throwing the exception. </param>
        /// <param name="ex">The exception itself. </param>
        private static void LogFile(string methodName, Exception ex)
        {
            //A stream writer that represents the log file.
            StreamWriter writer = null;

            try
            {
                //Attempt to connect to the log file.
                writer = new StreamWriter(ConfigurationManager.AppSettings["LogFile"], false);

                //Write the provided exception to the file with the current date time and other info.
                writer.WriteLine(ex.Message + "/n " + DateTime.Now.ToString() + "/n " + methodName + "/n" + "Warning");
            }
            catch (IOException writerException)
            {
                //Throw to the next layer above.
                throw writerException;
            }
            finally
            {
                //If we were able to connect to the file.
                if (writer != null)
                {
                    //Close any active connections.
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
        static void Main(string[] args)
        {
            //Values for the menus and loops 
            char mainMenuSelection;
            string returnMenu;
            string exit = "y";
            returnMenu = "Y";

            //Loop to repeat the menu after the switch statement takes place
            while (string.Equals(returnMenu, "Y", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Please choose an option from the following menu: ");
                Console.WriteLine();
                Console.WriteLine("1. View all current suppliers");
                Console.WriteLine("2. Update existing supplier");
                Console.WriteLine("3. Add a new supplier");
                Console.WriteLine("4. Remove existing supplier");
                Console.WriteLine("5. Exit program");

                mainMenuSelection = Console.ReadKey(true).KeyChar;
                Console.Clear();

                //Switch statement that connects the user's selection to the method that handles the option they selected
                switch (mainMenuSelection)
                {
                    case '1':
                        ViewSuppliers();
                        break;
                    case '2':
                        UpdateSupplier();
                        break;
                    case '3':
                        CreateSupplier();
                        break;
                    case '4':
                        RemoveSupplier();
                        break;
                    case '5':
                        Console.WriteLine("Are you sure you want to exit program? Y/N");
                        exit = Console.ReadLine();
                        if (exit == "y")
                        {
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        Console.WriteLine("Please enter a vaild option!");
                        Console.ReadLine();
                        break;
                }

                Console.WriteLine("Return to Main Menu? Y/N");
                returnMenu = Console.ReadLine();
                Console.Clear();
            }
        }

        private static void ViewSuppliers()
        {
            //Method to view all the suppliers recorded inside the database and the connection values for connecting to SQL
            string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            DataTable suppliers = new DataTable();
            SqlDataAdapter adapter = null;

            //SQL connection housed inside a try/catch to close the connection after finishing the view method
            try
            {
                //Connects to SQL and obtains the Obtain Suppliers stored procedure from SQL
                connectionToSql = new SqlConnection(connectionString);
                storedProcedure = new SqlCommand("OBTAIN_SUPPLIERS", connectionToSql);
                //Sets the stored procedure to a variable that can accept input from the user
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();
                adapter = new SqlDataAdapter(storedProcedure);
                adapter.Fill(suppliers);

                //Loop to display all the rows within the suppliers table
                foreach (DataRow row in suppliers.Rows)
                {
                    foreach (DataColumn col in suppliers.Columns)
                    {
                        Console.WriteLine("{0}:     {1}", col.ColumnName, row[col]);
                    }
                    Console.WriteLine();
                }
            }

            //Calls the method LogFile that will throw the exception
            catch (Exception ex)
            {
                string methodName = "ViewSuppliers";
                LogFile(methodName, ex);
            }

            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }

            Console.ReadKey(true);
        }

        private static void UpdateSupplier()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            SqlConnection connectionToSql = null;
            SqlCommand storedCommand = null;
            //SqlCommand storedCommand2 = null;

            string companyName;
            string contactName;
            string contactTitle;
            string country;
            string phone;

            try
            {
                connectionToSql = new SqlConnection(connectionString);
                //storedCommand = new SqlCommand("OBTAIN_SUPPLIERS", connectionToSql);
                //storedCommand.CommandType = System.Data.CommandType.StoredProcedure;
                storedCommand = new SqlCommand("UPDATE_SUPPLIER", connectionToSql);
                storedCommand.CommandType = System.Data.CommandType.StoredProcedure;
                string updateConfirmation;
                
                Console.WriteLine("Input the supplier's ID number to update their information");
                int.TryParse(Console.ReadLine(), out int supplierID);


                storedCommand.Parameters.AddWithValue("@SupplierID", supplierID);
                Console.Clear();

                Console.WriteLine("Input the new updated information for the supplier selected");
                Console.WriteLine();
                Console.Write("1. Company Name:   ");
                companyName = Console.ReadLine();
                Console.Write("2. Contact Name:   ");
                contactName = Console.ReadLine();
                Console.Write("3. Contact Title   ");
                contactTitle = Console.ReadLine();
                Console.Write("4. Country   ");
                country = Console.ReadLine();
                Console.Write("5. Phone   ");
                phone = Console.ReadLine();


                connectionToSql.Open();

                Console.WriteLine("Are you satisfied with the changes you've made?");
                Console.WriteLine("Press Y for yes, any other key to return");

                updateConfirmation = Console.ReadLine();

                if (string.Equals(updateConfirmation, "Y", StringComparison.CurrentCultureIgnoreCase))
                {
                    storedCommand.Parameters.AddWithValue("@CompanyName", companyName);
                    storedCommand.Parameters.AddWithValue("@ContactName", contactName);
                    storedCommand.Parameters.AddWithValue("@ContactTitle", contactTitle);
                    storedCommand.Parameters.AddWithValue("@Country", country);
                    storedCommand.Parameters.AddWithValue("@Phone", phone);


                    storedCommand.ExecuteNonQuery();
                }
            }

            catch (Exception ex)
            {
                string methodName = "UpdateSupplier";
                LogFile(methodName, ex);
            }

            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }

            Console.ReadKey(true);
        }

        private static void CreateSupplier()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            SqlConnection connectionToSql = null;
            SqlCommand storedCommand = null;
            string companyName;
            string contactName;
            string contactTitle;
            string country;
            string phone;

            try
            {
                connectionToSql = new SqlConnection(connectionString);
                storedCommand = new SqlCommand("ADD_SUPPLIER", connectionToSql);
                storedCommand.CommandType = System.Data.CommandType.StoredProcedure;

                Console.WriteLine("Input Company Name");
                companyName = Console.ReadLine();
                storedCommand.Parameters.AddWithValue("@CompanyName", companyName);
                Console.Clear();

                Console.WriteLine("Input Contact Name");
                contactName = Console.ReadLine();
                storedCommand.Parameters.AddWithValue("@ContactName", contactName);
                Console.Clear();

                Console.WriteLine("Input Contact Title");
                contactTitle = Console.ReadLine();
                storedCommand.Parameters.AddWithValue("@ContactTitle", contactTitle);
                Console.Clear();

                Console.WriteLine("Input Country");
                country = Console.ReadLine();
                storedCommand.Parameters.AddWithValue("@Country", country);
                Console.Clear();

                Console.WriteLine("Input Phone Number");
                phone = Console.ReadLine();
                storedCommand.Parameters.AddWithValue("@Phone", phone);
                Console.Clear();

                Console.WriteLine("You input {0}, {1}, {2}, {3}, {4}", companyName, contactName, contactTitle, country, phone);
                Console.WriteLine("The Supplier has been successfully added.");
                Console.ReadKey();
                Console.Clear();

                connectionToSql.Open();

                storedCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                string methodName = "CreateSupplier";
                LogFile(methodName, ex);
            }

            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }

            Console.ReadKey(true);
        }

        private static void RemoveSupplier()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            SqlConnection connectionToSql = null;
            SqlCommand storedCommand = null;
            string confirmation;

            try
            {
                connectionToSql = new SqlConnection(connectionString);
                storedCommand = new SqlCommand("REMOVE_SUPPLIER", connectionToSql);
                storedCommand.CommandType = System.Data.CommandType.StoredProcedure;

                Console.WriteLine("Input the ID number of the supplier you wish to remove.");
                int.TryParse(Console.ReadLine(), out int identificationCode);
                storedCommand.Parameters.AddWithValue("@SupplierID", identificationCode);
                Console.Clear();

                connectionToSql.Open();

                Console.WriteLine("Are you sure you want to remove supplier?");
                Console.WriteLine("Press Y for yes, any other key to return");

                confirmation = Console.ReadLine();

                if (string.Equals(confirmation, "Y", StringComparison.CurrentCultureIgnoreCase))
                {
                    storedCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string methodName = "RemoveSupplier";
                LogFile(methodName, ex);
            }

            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }

            Console.ReadKey(true);
        }
    }
}