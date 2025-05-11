using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libraryku
{
    public partial class Form6 : Form
    {
        string connectionstring = "server=localhost;uid=root;pwd=;database=dbd_LPSlibrary;";
        string query = "";
        MySqlConnection sqlConnect;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        MySqlDataReader sqlDataReader;
        //create datatable
        DataTable dtStudent = new DataTable();
        public Form6()
        {
            InitializeComponent();
            sqlConnect = new MySqlConnection();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            loadData();
            txtBoxID.Enabled = false;
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
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int idstud = Convert.ToInt32(txtBoxID.Text.ToString());
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
            sqlConnect.Open();
            query = $"UPDATE MAHASISWA SET " +
                    $"NIM_MAHASISWA = '{txtBoxNim.Text.ToString()}', " +
                    $"NAMA_MAHASISWA = '{txtBoxName.Text.ToString()}', " +
                    $"JENIS_KELAMIN_MAHASISWA = '{sex}', " +
                    $"TELEPON_MAHASISWA = '{txtBoxTelp.Text.ToString()}', " +
                    $"ALAMAT_MAHASISWA = '{txtBoxAddress.Text.ToString()}', " +
                    $"EMAIL_MAHASISWA = '{txtBoxEmail.Text.ToString()}', " +
                    $"DELETE_MAHASISWA = 0 WHERE ID_MAHASISWA = {idstud};";
            sqlCommand = new MySqlCommand(query, sqlConnect);
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            MessageBox.Show("Data Student Updated Successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvStudent.Update();
            dgvStudent.Refresh();
            dtStudent.Clear();
            txtBoxID.Clear();
            txtBoxName.Clear();
            txtBoxEmail.Clear();
            cmbGender.SelectedIndex = 0;
            cmbGender.Items.Clear();
            txtBoxAddress.Clear();
            txtBoxTelp.Clear();
            txtBoxNim.Clear();
            loadData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string gender = cmbGender.SelectedItem.ToString();
            //string sex = "";
            //if (gender == "Male")
            //{
            //    sex = "M";
            //}
            //else
            //{
            //    sex = "F";
            //}
            txtBoxID.Text = dgvStudent.CurrentRow.Cells[0].Value.ToString();
            txtBoxNim.Text = dgvStudent.CurrentRow.Cells[1].Value.ToString();
            txtBoxName.Text = dgvStudent.CurrentRow.Cells[2].Value.ToString();
            cmbGender.SelectedItem = dgvStudent.CurrentRow.Cells[3].Value.ToString();
            txtBoxTelp.Text = dgvStudent.CurrentRow.Cells[4].Value.ToString();
            txtBoxAddress.Text = dgvStudent.CurrentRow.Cells[5].Value.ToString();
            txtBoxEmail.Text = dgvStudent.CurrentRow.Cells[6].Value.ToString();

        }
    }
}
