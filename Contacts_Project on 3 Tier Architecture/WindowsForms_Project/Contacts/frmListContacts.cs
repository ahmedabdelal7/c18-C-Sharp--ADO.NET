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
    public partial class frmListContacts : Form
    {
        public frmListContacts()
        {
            InitializeComponent();
        }

        private void frmListContacts_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
        }


        private void _RefreshContactsList()
        {
            dgvListContacts.DataSource = clsContact.GetAllContacts();
        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditContact(-1);
            frm.ShowDialog();
            _RefreshContactsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form frm = new frmAddEditContact((int) dgvListContacts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshContactsList();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ContactID = (int) dgvListContacts.CurrentRow.Cells [0].Value;

            DialogResult dialogResult =  MessageBox.Show($"Are you sure you want to delete this contact", "Confirm", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.No)
                return;

           if(clsContact.DeleteContact(ContactID))
            {
                MessageBox.Show("Contact Deleted Successfully");
            }else
                MessageBox.Show("Faild to Deleted Contact!");
            _RefreshContactsList();

        }
    }
}
