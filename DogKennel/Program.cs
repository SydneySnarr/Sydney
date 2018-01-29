using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DogKennel
{
    class Program
    {
        static void Main(string[] args)
        {
            char MainMenuSelection;
            string ReturnMenu;

            ReturnMenu = "Y";

            while (string.Equals(ReturnMenu, "Y", StringComparison.CurrentCultureIgnoreCase))
            {   // Menu options
                Console.WriteLine("Please choose operation");
                Console.WriteLine();
                Console.WriteLine("1. Add Dog");
                Console.WriteLine("2. View all Dogs");
                Console.WriteLine("3. Empty File");
                Console.WriteLine("4. Enroll in Obedience Training");
                Console.WriteLine("5. View current Obedience Training Roster.");
                Console.WriteLine("6. Empty Obedience File.");
                Console.WriteLine("7. Exit");

                MainMenuSelection = Console.ReadKey(true).KeyChar;
                Console.Clear();
                // Menu switch to choose operation
                switch (MainMenuSelection)
                {
                    case '1':
                        AddDog();
                        
                        break;
                    case '2':
                        ViewDogs();
                        break;
                    case '3':
                        EmptyFile();
                        break;
                    case '4':
                        Obedience();
                        Console.WriteLine("Records have been updated.");
                        break;
                    case '5':
                        ViewObedience();
                        break;
                    case '6':
                        EmptyObedienceFile();
                        break;
                    case '7':
                        break;
                    default:
                        Console.WriteLine("Please enter a vaild option!");
                        Console.ReadLine();
                        break;
                }
                //Gives option to return to main menu after operation is performed
                Console.WriteLine("Return to Main Menu? Y/N");
                ReturnMenu = Console.ReadLine();
                Console.Clear();
            } 
        }
        /// <summary>
        /// Operation to add dog
        /// </summary>
        private static void AddDog()
        {
            StreamWriter writer = null;
            string name;
            decimal age;
            string gender;
            string breed;

            try
            {
                writer = new StreamWriter(@"C: \Users\Sydney\source\repos\DogKennel\DogKennel.txt", true);
                Console.WriteLine("Please enter your dog's name.");
                Console.WriteLine();
                name = Console.ReadLine();

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter your dog's age.");
                    Console.WriteLine();
                    string age2 = Console.ReadLine();
                    bool age3 = decimal.TryParse(age2, out age);
                    if (age3 == true)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid age!");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Please enter your dog's gender.");
                Console.WriteLine();
                gender = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Please enter your dog's breed.");
                Console.WriteLine();
                breed = Console.ReadLine();

                writer.WriteLine("{0}, {1}, {2}, {3}", name, age, gender, breed);
                Console.WriteLine("Records have been updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {

                writer.Close();
                writer.Dispose();
            }
        }
        //operation to view dogs
        private static void ViewDogs()
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(@"C: \Users\Sydney\source\repos\DogKennel\DogKennel.txt", true);
                Console.WriteLine(reader.ReadToEnd());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }
        //operation to clear file where dogs are added
        private static void EmptyFile()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(@"C: \Users\Sydney\source\repos\DogKennel\DogKennel.txt", false);
                writer.Write("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }
        }
        //Operation to enroll in obediance training
        private static void Obedience()
        {
            StreamWriter writer = null;
            string name;
            decimal age;
            string gender;
            string breed;
            string ownername;
            decimal Phone;

            try
            {
                writer = new StreamWriter(@"C: \Users\Sydney\source\repos\DogKennel\ObedianceTraining.txt", true);
                Console.WriteLine("Please enter your dog's name.");
                Console.WriteLine();
                name = Console.ReadLine();

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter your dog's age.");
                    Console.WriteLine();
                    string age2 = Console.ReadLine();
                    bool age3 = decimal.TryParse(age2, out age);
                    if (age3 == true)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid age!");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Please enter your dog's gender.");
                Console.WriteLine();
                gender = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Please enter your dog's breed.");
                Console.WriteLine();
                breed = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Please enter your name.");
                Console.WriteLine();
                ownername = Console.ReadLine();

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter your phone number.");
                    Console.WriteLine();
                    string PhoneNumber = Console.ReadLine();
                    bool Phonenumber = decimal.TryParse(PhoneNumber, out Phone);

                    if (Phonenumber)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid phone number.");
                    }
                }
                writer.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}", name, age, gender, breed, ownername, Phone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }
        }
        //operation to view dogs already enrolled in obediance training
        private static void ViewObedience()
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(@"C: \Users\Sydney\source\repos\DogKennel\ObedianceTraining.txt", true);
                Console.WriteLine(reader.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }
        //Operation to empty dogs enrolled in obediance training
        private static void EmptyObedienceFile()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(@"C: \Users\Sydney\source\repos\DogKennel\ObedianceTraining.txt", false);
                writer.Write("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }
        }
    }
}
