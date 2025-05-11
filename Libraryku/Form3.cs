using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libraryku
{
    public partial class Form3 : Form
    {
        string connectionstring = "server=localhost;uid=root;pwd=;database=dbd_LPSlibrary;";
        string query = "";
        MySqlConnection sqlConnect;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        MySqlDataReader sqlDataReader;
        //create datatable
        DataTable dtStudent = new DataTable();
        public Form3()
        {
            InitializeComponent();
            sqlConnect = new MySqlConnection();
        }
        public void loadData()
        {
            try
            {
                //Initialize the query
                query = "SELECT * FROM MAHASISWA;";

                //Create and open the connection
                sqlConnect = new MySqlConnection(connectionstring);
                sqlConnect.Open();

                //Create the command
                sqlCommand = new MySqlCommand(query, sqlConnect);

                //Use DataAdapter to fill the DataTable
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtStudent);

                //Optionally, bind the DataTable to a DataGridView (if present on the form)
                dgvStudent.DataSource = dtStudent;

                //Close the connection
                sqlConnect.Close();
            }
            catch (Exception ex)
            {
                //Handle exceptions
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //store user input on the textbox to variable
            string name = txtBoxName.Text;
            string nim = txtBoxNim.Text;
            string telp = txtBoxTelp.Text;
            string address = txtBoxAddress.Text;
            string gender = cmbGender.SelectedItem.ToString();
            string sex = "";
            if (gender == "Male")
            {
                sex = "M";
            }
            else
            {
                sex = "F";
            }
            string email = txtBoxEmail.Text;
            //validation if there's black textbox
            if (name == "" || nim == "" || gender == "" || telp == "" || address == "" || email == "")
            {
                //prohibited for user to add blank information
                MessageBox.Show("Fill all of the blank", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //connect sql
                sqlConnect = new MySqlConnection(connectionstring);
                sqlConnect.Open();
                //insert query sql & execute, no need for id_buku already auto increment
                query = $"INSERT INTO MAHASISWA(NIM_MAHASISWA, NAMA_MAHASISWA, JENIS_KELAMIN_MAHASISWA, TELEPON_MAHASISWA, ALAMAT_MAHASISWA, EMAIL_MAHASISWA, DELETE_MAHASISWA) VALUES ('{nim}', '{name}', '{sex}', '{telp}', '{address}', '{email}', 0);";
                sqlCommand = new MySqlCommand(query, sqlConnect);
                sqlCommand.ExecuteNonQuery().ToString();
                sqlConnect.Close();
                //show messagebox success
                MessageBox.Show("Student Added Successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvStudent.Update();
                dgvStudent.Refresh();
                cmbGender.SelectedIndex = 0;
                txtBoxName.Clear();
                txtBoxNim.Clear();
                txtBoxAddress.Clear();
                txtBoxTelp.Clear();
                txtBoxEmail.Clear();
                loadData();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
