using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viktorina_3
{
    public partial class teacherMenu : Form
    {
        public teacherMenu()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login dForm = new login();
            dForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createTest dForm = new createTest();
            dForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modifyTest dForm = new modifyTest();
            dForm.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkResultsTeacher dForm = new checkResultsTeacher();
            dForm.Show();
            this.Close();
        }
    }
}
