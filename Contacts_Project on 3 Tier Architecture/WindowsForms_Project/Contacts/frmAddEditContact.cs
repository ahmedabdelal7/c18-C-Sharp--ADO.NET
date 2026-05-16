using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class frmAddEditContact : Form
    {

        public  enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        private int _ContactID;
        private clsContact _Contact;

        public frmAddEditContact(int ContactID)
        {
            InitializeComponent();

            _Mode = (ContactID == -1 ? enMode.AddNew : enMode.Update);

            _ContactID = ContactID;

        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows) {

                cbCountry.Items.Add(row["CountryName"]);
            }
            
        }
        private void _LoadData()
        {
            //Get Contact Object
            _Contact = clsContact.Find(_ContactID);

            _FillCountriesInComboBox();
            cbCountry.SelectedIndex = 0;        

            if (_Mode == enMode.AddNew)
            {
                lblAddEditContact.Text = "Add New Contact";
                _Contact = new clsContact();
                return;
            }

            cbCountry.SelectedItem = clsCountry.FindCountryByID(_Contact.CountryID).CountryName;

            lblContactID.Text = _Contact.ID.ToString();
            txtFirstName.Text = _Contact.FirstName.ToString();  
            txtLastName.Text = _Contact.LastName.ToString();
            txtEmail.Text = _Contact.Email.ToString();
            txtPhone.Text = _Contact.Phone.ToString();
            txtAddress.Text = _Contact.Address.ToString();
            dateTimePicker1.Value = _Contact.DateOfBirth;    
            
            if(_Contact.ImagePath != "")
                pbImage.Load (_Contact.ImagePath);

            llRemoveImage.Visible = (_Contact.ImagePath != "");

            lblAddEditContact.Text = $"Edit Contact ID = {_ContactID}";
        }
       

        private void _AddNewContact()
        {
            _LoadData();
        }
        private void frmAddEditContact_Load(object sender, EventArgs e)
        {

            _LoadData();

        }

        private bool Save()
        {
           if(_Contact.Save())
            {
                _Mode = enMode.Update;
                return true;
            } 
           return false;
           
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            int CountryID = clsCountry.FindCountryByName(cbCountry.Text).ID;

            _Contact.FirstName = txtFirstName.Text;
            _Contact.LastName = txtLastName.Text;
            _Contact.Email = txtEmail.Text;
            _Contact.Phone = txtPhone.Text;
            _Contact.Address = txtAddress.Text;
            _Contact.DateOfBirth = dateTimePicker1.Value;
            _Contact.CountryID = CountryID;

            

            DialogResult result = MessageBox.Show("Are you sure you want to save contact?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if(result == DialogResult.Yes)
            {
                if (Save())
                {
                    MessageBox.Show("Contact Saved Successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblContactID.Text = _Contact.ID.ToString();
                }
                else
                {
                    MessageBox.Show("Faild to Save Contact!","Faild",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void brnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            openFileDialog1.InitialDirectory = @"E\";
            openFileDialog1.Title = "Choose Image";
            openFileDialog1.DefaultExt = "jpg";
            openFileDialog1.Filter = "images (*.jpg) |*.jpg| All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _Contact.ImagePath = openFileDialog1.FileName;
            }
            if (_Contact.ImagePath != "")
            {
                pbImage.Load(_Contact.ImagePath);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _Contact.ImagePath = "";
            pbImage.Image = null;
            llRemoveImage.Visible = false;
        }
    }
}
