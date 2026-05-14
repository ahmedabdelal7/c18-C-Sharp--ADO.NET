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

        static void Main(string[] args)
        {
            TestFindContact(9);
            //TestAddNewContact();
            Console.ReadLine();

        }
    }


}