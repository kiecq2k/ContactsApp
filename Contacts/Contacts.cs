using Contacts.ContactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class Contacts : Form
    {
        private Contact contact;
        private static string connstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        public Contacts()
        {
            InitializeComponent();
            contact = new Contact();
        }

        /// <summary>
        /// Adding new contact 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            getTheTextFromTextBoxes();
            bool success = contact.Insert(contact);
            if(success)
            {
                MessageBox.Show("New contact successfully inserted!");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to add new contact. Try again.");
            }
            DataTable dataTable = contact.Select();
            dataGridView.DataSource = dataTable;
        }

        /// <summary>
        /// Load data to data table after program was ran
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Contacts_Load(object sender, EventArgs e)
        {
            DataTable dataTable = contact.Select();
            dataGridView.DataSource = dataTable;
        }

        /// <summary>
        /// Exiting the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Clear the text of controls
        /// </summary>
        private void Clear()
        {
            txtBoxFirstName.Text = string.Empty;
            txtBoxLastName.Text = string.Empty;
            txtboxAddress.Text = string.Empty;
            txtBoxContactNo.Text = string.Empty;
            comboBoxGender.Text = string.Empty;
            txtBoxID.Text = string.Empty;
        }

        /// <summary>
        /// Udate data in database and datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            contact.ContactID = Convert.ToInt32(txtBoxID.Text);
            getTheTextFromTextBoxes();

            bool success = contact.Update(contact);
            if(success)
            {
                MessageBox.Show("Contact has been updated successfully.");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update contact.Try again.");
            }
            DataTable dataTable = contact.Select();
            dataGridView.DataSource = dataTable;
        }

        /// <summary>
        /// Insert the text to textboxex from the datagridview by clicking row header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            txtBoxID.Text = dataGridView.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxFirstName.Text = dataGridView.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxLastName.Text = dataGridView.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNo.Text = dataGridView.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxAddress.Text = dataGridView.Rows[rowIndex].Cells[4].Value.ToString();
            comboBoxGender.Text = dataGridView.Rows[rowIndex].Cells[5].Value.ToString();
        }

        /// <summary>
        /// Gets the text from texboxes 
        /// </summary>
        private void getTheTextFromTextBoxes()
        {
            contact.FirstName = txtBoxFirstName.Text;
            contact.LastName = txtBoxLastName.Text;
            contact.Address = txtboxAddress.Text;
            contact.ContactNo = txtBoxContactNo.Text;
            contact.Gender = comboBoxGender.Text;
        }

        /// <summary>
        /// Clear the text of textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        /// <summary>
        /// Delete a contact from database and datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            contact.ContactID = Convert.ToInt32(txtBoxID.Text);
            bool success = contact.Delete(contact);
            if(success)
            {
                DataTable dataTable = contact.Select();
                dataGridView.DataSource = dataTable;
                MessageBox.Show("Contact successfully deleted.");
            }
            else
            {
                MessageBox.Show("Failed to delete contact.Try again.");
            }
            Clear();
        }

        /// <summary>
        /// Searching contacts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtBoxSearch.Text;

            SqlConnection sqlConnection = new SqlConnection(connstring);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM table_contacts WHERE" +
                "FirstName LIKE '%" + inputText + "%' OR LastName LIKE '%" + inputText + "%' OR Address LIKE '%" + inputText + "%'", connstring);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView.DataSource = dataTable;
        }
    }
}
