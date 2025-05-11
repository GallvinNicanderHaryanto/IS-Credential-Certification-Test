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
    public partial class Form5 : Form
    {
        string connectionstring = "server=localhost;uid=root;pwd=;database=dbd_LPSlibrary;";
        string query = "";
        MySqlConnection sqlConnect;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        MySqlDataReader sqlDataReader;
        //create datatable
        DataTable dtBook = new DataTable();
        public Form5()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }

        //method to load data and refresh datagridview book after insert
        public void loadData()
        {
            try
            {
                //Initialize the query
                query = "SELECT * FROM BUKU;";

                //Create and open the connection
                sqlConnect = new MySqlConnection(connectionstring);
                sqlConnect.Open();

                //Create the command
                sqlCommand = new MySqlCommand(query, sqlConnect);

                //Use DataAdapter to fill the DataTable
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtBook);

                //Optionally, bind the DataTable to a DataGridView (if present on the form)
                dgvBook.DataSource = dtBook;

                //Close the connection
                sqlConnect.Close();
            }
            catch (Exception ex)
            {
                //Handle exceptions
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBoxID.Text = dgvBook.CurrentRow.Cells[0].Value.ToString();
            txtBoxTitle.Text = dgvBook.CurrentRow.Cells[1].Value.ToString();
            txtBoxCat.Text = dgvBook.CurrentRow.Cells[2].Value.ToString();
            txtBoxPublisher.Text = dgvBook.CurrentRow.Cells[3].Value.ToString();
            txtBoxAuth.Text = dgvBook.CurrentRow.Cells[4].Value.ToString();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            loadData();
            txtBoxID.Enabled = false;
        }

        private void dgvBook_BindingContextChanged(object sender, EventArgs e)
        {
            //txtBoxID.Text = dgvBook.CurrentRow.Cells[0].Value.ToString();
            //txtBoxTitle.Text = dgvBook.CurrentRow.Cells[1].Value.ToString();
            //txtBoxCat.Text = dgvBook.CurrentRow.Cells[2].Value.ToString();
            //txtBoxPublisher.Text = dgvBook.CurrentRow.Cells[3].Value.ToString();
            //txtBoxAuth.Text = dgvBook.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int idbook = Convert.ToInt32(txtBoxID.Text.ToString());
            sqlConnect.Open();
            query = $"UPDATE BUKU SET " +
                    $"JUDUL_BUKU = '{txtBoxTitle.Text.ToString()}', " +
                    $"KATEGORI = '{txtBoxCat.Text.ToString()}', " +
                    $"PENERBIT = '{txtBoxPublisher.Text.ToString()}', " +
                    $"PENULIS = '{txtBoxAuth.Text.ToString()}', " +
                    $"DELETE_BUKU = 0 WHERE ID_BUKU = {idbook};";
            sqlCommand = new MySqlCommand(query, sqlConnect);
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            MessageBox.Show("Data Book Updated Successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvBook.Update();
            dgvBook.Refresh();
            dtBook.Clear();
            txtBoxID.Clear();
            txtBoxAuth.Clear();
            txtBoxTitle.Clear();
            txtBoxPublisher.Clear();
            txtBoxCat.Clear();
            loadData();
        }
    }
}
