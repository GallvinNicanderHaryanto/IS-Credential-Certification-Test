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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        private void Form9_Load(object sender, EventArgs e)
        {

        }
        public void FillDGV()
        {

        }
        private void BTN_CHOOSE_IMAGE_Click(object sender, EventArgs e)
        {

            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Choose Image (*.JPG;*.PNG;*.GIF;)|*.jpg;*.png;*.gif;";

            if(opf.ShowDialog() == DialogResult.OK)
            { 
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }
    }
}
