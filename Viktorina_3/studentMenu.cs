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
    public partial class studentMenu : Form
    {
        string studentName;
        public studentMenu(string name)
        {
            InitializeComponent();
            studentName = name;
        }

        private void studentMenu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            takeTest dForm = new takeTest(studentName);
            dForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkResultsStudent dForm = new checkResultsStudent(studentName);
            dForm.Show();
            this.Close();
        }
    }
}
