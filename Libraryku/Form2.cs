using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libraryku
{
    public partial class Form2 : Form
    {
        string connectionstring = "server=localhost;uid=root;pwd=;database=dbd_LPSlibrary;";
        string query = "";
        MySqlConnection sqlConnect;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        MySqlDataReader sqlDataReader;
        //create datatable
        DataTable dtBook = new DataTable();
        public Form2()
        {
            InitializeComponent();
            sqlConnect = new MySqlConnection();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //store user input on the textbox to variable
            string title = txtBoxTitle.Text;
            string author = txtBoxAuth.Text;
            string publisher = txtBoxPublisher.Text;
            string category = txtBoxCat.Text;
            //validation if there's black textbox
            if (title == "" || author == "" || publisher =="" || category == "")
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
                query = $"INSERT INTO BUKU(JUDUL_BUKU, KATEGORI, PENERBIT, PENULIS, DELETE_BUKU) VALUES ('{title}', '{category}', '{publisher}', '{author}', 0);";
                sqlCommand = new MySqlCommand(query, sqlConnect);
                sqlCommand.ExecuteNonQuery().ToString();
                sqlConnect.Close();
                //show messagebox success
                MessageBox.Show("Book Added Successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBook.Update();
                dgvBook.Refresh();
                txtBoxAuth.Clear();
                txtBoxTitle.Clear();
                txtBoxPublisher.Clear();
                txtBoxCat.Clear();
                loadData();
            }

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

        private void Form2_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgvBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //trial for refresh button
            Form2 f2 = new Form2();
            f2.Refresh();
            dgvBook.Refresh();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }
    }
}
