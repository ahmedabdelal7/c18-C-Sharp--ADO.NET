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
        private clsCountry(int ID, string CountryName)
        {
            this.ID = ID;
            this.CountryName = CountryName;
            Mode = enMode.Update;
        }
        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";

            Mode = enMode.AddNew;
        }
        public static clsCountry FindCountryByID(int ID)
        {
            string CountryName = "";
            if (clsCountryDataAccess.GetCountryInfoByID(ID, ref CountryName))
            {
                return new clsCountry(ID, CountryName);

            }
            else return null;
        }
        public static clsCountry FindCountryByName(string CountryName)
        {
            int CountryID = -1;

            if (clsCountryDataAccess.GetCountryInfoByName(ref CountryID, CountryName))
            {
                return new clsCountry(CountryID, CountryName);

            }
            else return null;
        }
        private bool _AddNewCountry()
        {
            this.ID = clsCountryDataAccess.AddNewCountry(this.CountryName);
            return ID > 0;
        }
        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(this.ID, this.CountryName);
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
