using ContactsDataAccessLayer;
using System.Data;


namespace ContactsBusinessLayer
{
    public class clsCountry
    {
        private enum enMode { AddNew, Update, Delete }
        enMode Mode;
        public int ID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string PhoneCode {  get; set; }
        private clsCountry(int ID, string CountryName,string CountryCode,string PhoneCode )
        {
            this.ID = ID;
            this.CountryName = CountryName;
            this.CountryCode = CountryCode;
            this.PhoneCode = PhoneCode;
            Mode = enMode.Update;
        }
        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
            this.CountryCode = "";
            this.PhoneCode = "";

            Mode = enMode.AddNew;
        }
        public static clsCountry FindCountryByID(int ID)
        {
            string CountryName = "";
            string CountryCode = "";
            string PhoneCode = "";
            if (clsCountryDataAccess.GetCountryInfoByID(ID, ref CountryName, ref  CountryCode, ref  PhoneCode))
            {
                return new clsCountry(ID, CountryName, CountryCode, PhoneCode);

            }
            else return null;
        }
        public static clsCountry FindCountryByName(string CountryName)
        {
            int CountryID = -1;
            string CountryCode = "";
            string PhoneCode = "";
            if (clsCountryDataAccess.GetCountryInfoByName(ref CountryID, CountryName, ref CountryCode, ref PhoneCode))
            {
                return new clsCountry(CountryID, CountryName, CountryCode, PhoneCode);

            }
            else return null;
        }
        private bool _AddNewCountry()
        {
            this.ID = clsCountryDataAccess.AddNewCountry(this.CountryName, this.CountryCode, this.PhoneCode);
            return ID > 0;
        }
        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(this.ID, this.CountryName, this.CountryCode,this.PhoneCode);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;

                case enMode.Update:
                    return _UpdateCountry();
            }
            return false;
        }
        public static bool DeleteCountry(int ID)
        {
            return clsCountryDataAccess.DeleteCountry(ID);
        }
        public static bool IsCountryExist(int ID)
        {
            return clsCountryDataAccess.IsCountryExist(ID);
        }
        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }

    }
}
