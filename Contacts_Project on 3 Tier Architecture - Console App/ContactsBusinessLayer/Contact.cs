using System;
using System.Data;
using ContactsDataAccessLayer;


namespace ContactsBusinessLayer
{
    public class clsContact
    {

        private enum enMode { AddNew, Update, Delete }
        enMode Mode;
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName {  get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth {  get; set; }
        public int CountryID { get; set; }

        private clsContact(int ID, string FirstName,string LastName, string Email, string Phone, 
            string ImagePath, string Address, DateTime DateOfBirth, int CountryID) 
        { 
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.ImagePath = ImagePath;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;

            Mode = enMode.Update;
        }
        
        public clsContact()
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Address = "";
            this.ImagePath = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;

            Mode = enMode.AddNew;
        }
        
        public static clsContact Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", ImagePath = "", Address = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;

            if (clsContactDataAccess.GetContactInfoByID(ID, ref FirstName, ref LastName, ref Email, ref Phone, ref ImagePath, ref Address,
                ref DateOfBirth, ref CountryID))
            {
                return new clsContact(ID, FirstName,LastName, Email, Phone, ImagePath, Address, DateOfBirth, CountryID);
            }
            else
            {
                return null;
            }

        }

        private bool _AddNewContact()
        {
            this.ID = clsContactDataAccess.AddNewContact(this.FirstName, this.LastName, this.Email,
                this.Phone,this.Address, this.ImagePath, this.DateOfBirth, this.CountryID);

            return (this.ID != -1);
        }
        private bool _UpdateContact()
        {
            return (clsContactDataAccess.UpdateContact(this.ID, this.FirstName, this.LastName, this.Email, this.Phone,
                this.Address, this.ImagePath, this.DateOfBirth, this.CountryID));
        }

        public static bool DeleteContact(int ID)
        {
            return (clsContactDataAccess.DeleteContact(ID));
        }
        public static DataTable GetAllContacts()
        {
            return clsContactDataAccess.GetAllContacts();
        }


        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false; 
                    
                case enMode.Update:
                   return _UpdateContact();


            }
            return false;
        }


    }
}
