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
    public partial class registration : Form
    {
        string name;
        public registration(string name_prev)
        {
            name = name_prev;
            InitializeComponent();
        }

        private void registration_Load(object sender, EventArgs e)
        {
            textBox1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isTeacher;
            name = textBox1.Text;
            if(radioButton1.Checked == true)
            {
                isTeacher = true;
            }
            else
            {
                isTeacher = false;
            }
            sql s = new sql();
            try { s.registration(name, isTeacher); MessageBox.Show("Paskyra sekmingai užregistruota."); 
                if (isTeacher == true)
                {
                    teacherMenu dForm = new teacherMenu();
                    dForm.Show();
                    this.Close();
                }
                else
                {
                    studentMenu dForm = new studentMenu(name);
                    dForm.Show();
                    this.Close();
                }    
            
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }
    }
}
