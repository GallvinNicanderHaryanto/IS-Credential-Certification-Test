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
    public partial class Form7 : Form
    {
        public Form7()
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //store user input on the textbox to variable
            string buku = ((DataRowView)cmbBook.SelectedItem)["JUDUL_BUKU"].ToString(); ;
            string mahasiswa = ((DataRowView)cmbMahasiswa.SelectedItem)["NAMA_MAHASISWA"].ToString(); ;
            string tglpinjam = dtpBorrow.Value.ToString("yyyy-MM-dd");
            string tglkembali = dtpReturn.Value.ToString("yyyy-MM-dd");
            int idbuku = Convert.ToInt32(cmbBook.SelectedValue);
            int idmahasiswa = Convert.ToInt32(cmbMahasiswa.SelectedValue);
            //validation if there's black textbox
            if (buku == "" || mahasiswa == "" || tglpinjam == "" || tglkembali == "")
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
                query = $"INSERT INTO PEMINJAMAN(ID_BUKU, ID_MAHASISWA, NAMA_MAHASISWA, JUDUL_BUKU, TANGGAL_PINJAM, TANGGAL_PENGEMBALIAN) VALUES ({idbuku}, {idmahasiswa}, '{mahasiswa}', '{buku}', '{tglpinjam}', '{tglkembali}');";
                sqlCommand = new MySqlCommand(query, sqlConnect);
                sqlCommand.ExecuteNonQuery().ToString();
                sqlConnect.Close();
                //show messagebox success
                MessageBox.Show("Borrow Added Successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBorrow.Update();
                dgvBorrow.Refresh();
                cmbBook.SelectedIndex = 0;
                cmbMahasiswa.SelectedIndex = 0;
                dtBorrow.Clear();
                loadData();
                sqlConnect.Open();
                //insert query sql & execute, no need for id_buku already auto increment
                query = $"UPDATE BUKU SET DELETE_BUKU = 1 WHERE ID_BUKU = {idbuku};";
                sqlCommand = new MySqlCommand(query, sqlConnect);
                sqlCommand.ExecuteNonQuery().ToString();
                sqlConnect.Close();
                dtbuku.Clear();
            }
        }
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
        DataTable dtbuku = new DataTable();

        private void Form7_Load(object sender, EventArgs e)
        {
            loadData();
            //fill combobox buku
            query = "SELECT ID_BUKU, JUDUL_BUKU FROM BUKU WHERE DELETE_BUKU = 0";
            sqlConnect = new MySqlConnection(connectionstring);
            sqlCommand = new MySqlCommand(query, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }

        private bool isUpdating = false;

        private void dtpBorrow_ValueChanged(object sender, EventArgs e)
        {
            //Prevent recursive updates
            if (isUpdating)
                return;

            //Ensure dtpReturn is accessible in this context
            if (dtpReturn != null)
            {
                try
                {
                    //Set the flag to true to prevent recursion
                    isUpdating = true;

                    //Calculate the new date
                    DateTime d1 = dtpBorrow.Value;
                    DateTime result = d1.AddDays(7);

                    //Update dtpReturn value
                    dtpReturn.Value = result;
                }
                finally
                {
                    //Reset the flag after the operation is complete
                    isUpdating = false;
                }
            }
        }


        private void dtpReturn_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
