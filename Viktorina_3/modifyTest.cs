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
    public partial class modifyTest : Form
    {
        string testName;
        int questionID;
        sql s = new sql();
        DataTable dt;

        public modifyTest()
        {
            InitializeComponent();
        }
        public void hide()
        {
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox6.Hide();
            button2.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            radioButton4.Hide();
        }
        public void show()
        {
            textBox2.Show();
            textBox3.Show();
            textBox4.Show();
            textBox5.Show();
            textBox6.Show();
            button2.Show();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            radioButton1.Show();
            radioButton2.Show();
            radioButton3.Show();
            radioButton4.Show();
        }

        public void fill()
        {
            dt = s.fillQuestions(testName, questionID);
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            textBox6.Text = dt.Rows[0][1].ToString();
            textBox2.Text = dt.Rows[0][2].ToString();
            textBox3.Text = dt.Rows[0][4].ToString();
            textBox4.Text = dt.Rows[0][6].ToString();
            textBox5.Text = dt.Rows[0][8].ToString();
            if(dt.Rows[0][3].ToString() == "True")
            {
                radioButton1.Checked = true;
            }
            if (dt.Rows[0][5].ToString() == "True")
            {
                radioButton2.Checked = true;
            }
            if (dt.Rows[0][7].ToString() == "True")
            {
                radioButton3.Checked = true;
            }
            if (dt.Rows[0][9].ToString() == "True")
            {
                radioButton4.Checked = true;
            }
            label6.Text = "Klausimas Nr. " + dt.Rows[0][0].ToString();
        }

        private void modifyTest_Load(object sender, EventArgs e)
        {            
            questionID = 1;
            DataTable table = s.getTests();
            comboBox1.DisplayMember = "testName";
            comboBox1.ValueMember = "testName";
            comboBox1.DataSource = table;
            hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testName = comboBox1.Text;
            comboBox1.Hide();
            button1.Hide();
            button4.Hide();
            label1.Hide();
            show();
            if(s.maxID(testName) != 0)
            {
                fill();
            }
            else
            {
                MessageBox.Show("Testas neturi klausimų");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string question = textBox6.Text;
            string[] answer = new string[4];
            bool[] isCorrect = new bool[4];
            answer[0] = textBox2.Text;
            answer[1] = textBox3.Text;
            answer[2] = textBox4.Text;
            answer[3] = textBox5.Text;
            isCorrect[0] = radioButton1.Checked;
            isCorrect[1] = radioButton2.Checked;
            isCorrect[2] = radioButton3.Checked;
            isCorrect[3] = radioButton4.Checked;
            try { s.updateModifier(question, answer, isCorrect, testName, questionID); questionID++; MessageBox.Show("Išsaugota"); fill(); }
            catch (Exception ex){ MessageBox.Show("Testas daugiau neturi klausimų"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            teacherMenu dForm = new teacherMenu();
            dForm.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            testName = comboBox1.Text;
            try 
            { 
                s.deleteTable(testName); 
                MessageBox.Show("Testas pašalintas");
                DataTable table = s.getTests();
                comboBox1.DisplayMember = "testName";
                comboBox1.ValueMember = "testName";
                comboBox1.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
