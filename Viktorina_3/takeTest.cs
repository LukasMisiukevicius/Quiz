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
    public partial class takeTest : Form
    {        
        int seconds;
        int correct = 0;
        string testName;
        string studentName;
        int questionID;
        public takeTest(string name)
        {
            InitializeComponent();
            studentName = name;
        }

        private void takeTest_Load(object sender, EventArgs e)
        {
            groupBox1.Hide();
            label2.Hide();
            label11.Hide();
            button2.Hide();
            label12.Hide();
            sql s = new sql();
            DataTable table = s.getTests();
            comboBox1.DisplayMember = "testName";
            comboBox1.ValueMember = "testName";
            comboBox1.DataSource = table;
        }

        public void hide()
        {
            label1.Hide();
            comboBox1.Hide();
            button1.Hide();
        }

        public void show()
        {
            groupBox1.Show();
            label2.Show();
            label11.Show();
            label12.Show();
            button2.Show();
        }
        public void fill()
        {
            seconds = 30;
            timer1.Start();
            radioButton1.Checked = false; radioButton2.Checked = false; radioButton3.Checked = false; radioButton4.Checked = false;
            sql s = new sql();
            DataTable dt = s.fillQuestions(testName, questionID);
            label2.Text = "Klausimas Nr. " + dt.Rows[0][0].ToString(); 
            label11.Text = dt.Rows[0][1].ToString();
            label3.Text = dt.Rows[0][2].ToString();
            label4.Text = dt.Rows[0][4].ToString();
            label5.Text = dt.Rows[0][6].ToString();
            label6.Text = dt.Rows[0][8].ToString();
        }

        public void checkAnswer()
        {
            sql s = new sql();
            DataTable dt = s.fillQuestions(testName, questionID);
            if (radioButton1.Checked == true && dt.Rows[0][3].ToString() == "True" || radioButton2.Checked == true && dt.Rows[0][5].ToString() == "True" || radioButton3.Checked == true && dt.Rows[0][7].ToString() == "True" || radioButton4.Checked == true && dt.Rows[0][9].ToString() == "True")
            {
                MessageBox.Show("Atsakymas teisingas");
                correct++;
            }
            else
            {
                MessageBox.Show("Atsakymas neteisingas");
            }
            questionID++;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            testName = comboBox1.Text;
            sql s = new sql();
            hide();
            show();
            questionID = 1;
            if (s.maxID(testName) != 0)
            {
                fill();
            }
            else
            {
                MessageBox.Show("Testas yra tuščias");
                studentMenu dForm = new studentMenu(studentName);
                dForm.Show();
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            sql s = new sql();
            checkAnswer();
            if (questionID <= s.maxID(testName))
            {
                fill();
            }
            else
            {
                MessageBox.Show("Testo pabaiga. \n Surinkote " + correct + "/" + s.maxID(testName) + " taškų.");
                int outOf = s.maxID(testName);
                s.insertInResults(studentName, testName, correct, outOf);
                studentMenu dForm = new studentMenu(studentName);
                dForm.Show();
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sql s = new sql();
            if(seconds >= 10)
            {
                label12.Text = "Liko laiko: 00:00:" + seconds--.ToString();
            }
            else
            {
                label12.Text = "Liko laiko: 00:00:0" + seconds--.ToString();
            }
            if(seconds == -1)
            {
                timer1.Stop();
                MessageBox.Show("Baigėsi laikas. Klausimas vertinamas 0.");
                questionID++;
                if (questionID <= s.maxID(testName))
                {
                    fill();
                }
                else
                {
                    MessageBox.Show("Testo pabaiga. \n Surinkote " + correct + "/" + s.maxID(testName) + " taškų.");
                    int outOf = s.maxID(testName);
                    s.insertInResults(studentName, testName, correct, outOf);
                    studentMenu dForm = new studentMenu(studentName);
                    dForm.Show();
                    this.Close();
                }
            }
        }
    }
}
