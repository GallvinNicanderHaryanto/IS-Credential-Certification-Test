using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Encoders;
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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        string connectionstring = "server=localhost;uid=root;pwd=;database=dbd_LPSlibrary;";
        string query = "";
        MySqlConnection sqlConnect;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        MySqlDataReader sqlDataReader;
        //create datatable
        DataTable dtBorrow = new DataTable();
        public void loadData()
        {
            try
            {
                //Initialize the query
                query = "SELECT * FROM PEMINJAMAN;";

                //Create and open the connection
                sqlConnect = new MySqlConnection(connectionstring);
                sqlConnect.Open();

                //Create the command
                sqlCommand = new MySqlCommand(query, sqlConnect);

                //Use DataAdapter to fill the DataTable
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtBorrow);

                //Optionally, bind the DataTable to a DataGridView (if present on the form)
                dgvBorrow.DataSource = dtBorrow;

                //Close the connection
                sqlConnect.Close();
            }
            catch (Exception ex)
            {
                //Handle exceptions
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void Form8_Load(object sender, EventArgs e)
        {
            txtBoxID.Enabled = false;
            loadData();
            //fill combobox buku
            query = "SELECT ID_BUKU, JUDUL_BUKU FROM BUKU";
            sqlConnect = new MySqlConnection(connectionstring);
            sqlCommand = new MySqlCommand(query, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            DataTable dtbuku = new DataTable();
            sqlAdapter.Fill(dtbuku);

            cmbBook.ValueMember = "ID_BUKU";
            cmbBook.DataSource = dtbuku;
            cmbBook.DisplayMember = "JUDUL_BUKU";
            cmbBook.SelectedIndex = -1;

            //fill combobox mahasiswa
            query = "SELECT ID_MAHASISWA, NAMA_MAHASISWA FROM MAHASISWA";
            sqlConnect = new MySqlConnection(connectionstring);
            sqlCommand = new MySqlCommand(query, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            DataTable dtsiswa = new DataTable();
            sqlAdapter.Fill(dtsiswa);

            cmbMahasiswa.ValueMember = "ID_MAHASISWA";
            cmbMahasiswa.DataSource = dtsiswa;
            cmbMahasiswa.DisplayMember = "NAMA_MAHASISWA";
            cmbMahasiswa.SelectedIndex = -1;
            dtpReturn.Enabled = false;

        }

        private void dgvBorrow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dtpBorrow.Value = Convert.ToDateTime(dgvBorrow.CurrentRow.Cells[5].Value);
            }
            catch
            {
                MessageBox.Show("Invalid date value in the selected cell.");
            }
            try
            {
                dtpReturn.Value = Convert.ToDateTime(dgvBorrow.CurrentRow.Cells[6].Value);
            }
            catch
            {
                MessageBox.Show("Invalid date value in the selected cell.");
            }
            cmbMahasiswa.SelectedItem = dgvBorrow.CurrentRow.Cells[3].Value.ToString();
            cmbBook.SelectedItem = dgvBorrow.CurrentRow.Cells[4].Value.ToString();
            txtBoxID.Text = dgvBorrow.CurrentRow.Cells[0].Value.ToString();
        }


        private void txtBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            int idpinjam = Convert.ToInt32(txtBoxID.Text);
            int idbuku = Convert.ToInt32(dgvBorrow.CurrentRow.Cells[1].Value.ToString());
            sqlConnect.Open();
            query = $"UPDATE BUKU SET " +
                    $"DELETE_BUKU = 0 WHERE ID_BUKU = {idbuku};";
            sqlCommand = new MySqlCommand(query, sqlConnect);
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            MessageBox.Show("Book Returned Successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dtBorrow.Clear();
            loadData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }
    }
}
