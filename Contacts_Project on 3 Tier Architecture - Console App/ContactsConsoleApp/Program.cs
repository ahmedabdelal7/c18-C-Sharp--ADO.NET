using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using ContactsBusinessLayer;

//Presentation Layer
namespace ContactsConsoleApp
{
    internal class Program
    {
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
                Console.WriteLine("Contact With ID ["+ID+"] Not Found!");
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
                Console.WriteLine("Contact Added Successfully With ID {0}",contact.ID);
            }

        }

        static void TestUpdateContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);

            if(Contact1 != null)
            {
                Contact1.FirstName = "Samy";
                Contact1.LastName = "Adil";
                Contact1.Email = "sa@gmail.com";
                Contact1.Phone = "0102214456";
                Contact1.Address = "56 Dolly st";
                Contact1.DateOfBirth = new DateTime(2000,1,2,8,0,0);
                Contact1.ImagePath = "";
                Contact1.CountryID = 3;


                if (Contact1.Save()) {
                    Console.WriteLine("Contact Updated Successfuly.");
                }else
                    Console.WriteLine("Faild to update contact!");

            }else
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
                                            

            foreach(DataRow row in dataTable.Rows)
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

        static void Main(string[] args)
        {
            //TestFindContact(9);
            //TestAddNewContact();
            //TestUpdateContact(8);

            //TestDeleteContact(9);

            //TestListAllContacts();

            TestIsContactExist(5);

            Console.ReadLine();

        }
    }


}