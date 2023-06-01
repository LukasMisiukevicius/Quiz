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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql s = new sql();
            try
            {
                if(s.isTeacher(textBox1.Text) == "True")
                {
                    teacherMenu dForm = new teacherMenu();
                    dForm.Show();
                    this.Hide();
                }
                else
                {
                    studentMenu dForm = new studentMenu(textBox1.Text);
                    dForm.Show();
                    this.Hide();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Paskyra nerasta, prašome registruotis");
                registration dForm = new registration(textBox1.Text);
                dForm.Show();
                this.Hide();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
