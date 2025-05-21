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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void btnAddBorrow_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.Show();
            this.Hide();
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.Show();
            this.Hide();
        }

        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.Show();
            this.Hide();
        }

        private void btnUpdateBook_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
        }

        private void btnAddpicture_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9();
            f9.Show();
            this.Hide();
        }
    }
}
