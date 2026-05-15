using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using ContactsBusinessLayer;

//Presentation Layer
namespace ContactsConsoleApp
{
    internal class Program
    {
        //Test Contact
        static void TestFindContact(int ID)
        {
            clsContact Contact = clsContact.Find(ID);

            if (Contact != null) {

                Console.WriteLine("\nContact Information: \n");
                Console.WriteLine(Contact.ID);
                Console.WriteLine(Contact.FirstName);
                Console.WriteLine(Contact.LastName);
                Console.WriteLine(Contact.Email);
                Console.WriteLine(Contact.Phone);
                Console.WriteLine(Contact.Address);
                Console.WriteLine(Contact.CountryID);
            }
            else
            {
                Console.WriteLine("Contact With ID [" + ID + "] Not Found!");
            }

        }
        static void TestAddNewContact() {

            clsContact contact = new clsContact();

            contact.FirstName = "Mahanad";
            contact.LastName = "Ali";
            contact.Email = "ma@gmail.com";
            contact.Phone = "25444645";
            contact.Address = "155 Abelaziz st";
            contact.ImagePath = "";
            contact.DateOfBirth = new DateTime(2004, 7, 7, 10, 0, 0);
            contact.CountryID = 2;


            if (contact.Save())
            {
                Console.WriteLine("Contact Added Successfully With ID {0}", contact.ID);
            }

        }

        static void TestUpdateContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);

            if (Contact1 != null)
            {
                Contact1.FirstName = "Samy";
                Contact1.LastName = "Adil";
                Contact1.Email = "sa@gmail.com";
                Contact1.Phone = "0102214456";
                Contact1.Address = "56 Dolly st";
                Contact1.DateOfBirth = new DateTime(2000, 1, 2, 8, 0, 0);
                Contact1.ImagePath = "";
                Contact1.CountryID = 3;


                if (Contact1.Save()) {
                    Console.WriteLine("Contact Updated Successfuly.");
                } else
                    Console.WriteLine("Faild to update contact!");

            } else
                Console.WriteLine("Contact is not found!");


        }

        static void TestDeleteContact(int ID)
        {
            if (clsContact.IsContatcExist(ID))
            {
                if (clsContact.DeleteContact(ID))
                    Console.WriteLine("Contact Deleted Successfully.");
                else Console.WriteLine("Faild To Delete Contact!");

            }
            else
                Console.WriteLine("Faild, Because Contact Is Not Exists.");
        }
        static void TestListAllContacts()
        {
            DataTable dataTable = clsContact.GetAllContacts();


            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]} - {row["FirstName"]} {row["LastName"]}");
            }
        }

        static void TestIsContactExist(int ID)
        {
            if (clsContact.IsContatcExist(ID))
                Console.WriteLine("Yes, Contact Is Exists.");
            else
                Console.WriteLine("No, Contact Is Not Exists.");
        }
        //Test Country
        static void TestFindCountryByID(int ID)
        {
            clsCountry country = clsCountry.FindCountryByID(ID);

            if (country != null)
            {
                Console.WriteLine($"Country ID   : {ID}");
                Console.WriteLine($"Country Name : {country.CountryName}");
            }
            else Console.WriteLine("Country Not Founded!");
        }
        static void TestFindCountryByName(string Name)
        {
            clsCountry country1 = clsCountry.FindCountryByName(Name);

            if (country1 != null)
            {
                Console.WriteLine($"Country ID   : {country1.ID}");
                Console.WriteLine($"Country Name : {country1.CountryName}");
            }
            else Console.WriteLine("Country Not Founded!");
        }
        static void TestAddNewCountry(string CountryName)
        {
            clsCountry country = new clsCountry();

            country.CountryName = CountryName;

            if (country.Save())
            {
                Console.WriteLine($"Country Added Successfully. with ID: {country.ID}");
            } else
            {
                Console.WriteLine("Faild to add country!");
            }
        }
        static void TestUpdateCountry(int ID)
        {
            clsCountry country = clsCountry.FindCountryByID(ID);
            if(country != null)
            {
                country.CountryName = "Dobai";
                if (country.Save())
                {
                    Console.WriteLine("Country Updated Successfully.");
                }else
                    Console.WriteLine("Faild To Update Country!");

            }
            else
                Console.WriteLine("Country Is Not Found!");
        }
        static void TestDeleteCountry(int ID) {

            if (clsCountry.IsCountryExist(ID))
            {
                if(clsCountry.DeleteCountry(ID))
                    Console.WriteLine("Country Deleted Successfully.");
                else
                    Console.WriteLine("Faild to delete Country!");
            }else
                Console.WriteLine("Country Not Founded");
        }
        static void TestListAllCountries()
        {
            DataTable dt = new DataTable();

            dt = clsCountry.GetAllCountries();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"Country {row["CountryID"]} - {row["CountryName"]}");
                }
            }
            else
            {
                Console.WriteLine("There Is No Countries To Show!");
            }
        }
        static void Main(string[] args)
        {
            //TestFindCountryByID(1);
            //TestFindCountryByName("Canada");
            //TestAddNewCountry("Moroco");
            //TestUpdateCountry(7);
            //TestDeleteCountry(7);
            TestListAllCountries();
            Console.ReadLine();

        }
    }


}