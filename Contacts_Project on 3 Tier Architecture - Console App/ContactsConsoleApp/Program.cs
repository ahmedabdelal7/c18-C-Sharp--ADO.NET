using System;
using System.Data;
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

       
        static void Main(string[] args)
        {
            TestFindContact(4); 

            Console.ReadLine();

        }
    }


}