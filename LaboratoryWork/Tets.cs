using System;
using System.Windows.Forms;
using System.Xml;
using System.Collections;

namespace LaboratoryWork
{
    public partial class TestForm : Form
    {
        public int CountQuestion = 0;
        public int CountErrors   = 0;
        public ArrayList OutputShowQuastions = new ArrayList(4);
        public int ChekedRadioButton;

        XmlDocument doc = new XmlDocument();

        public TestForm()
        {
            InitializeComponent();
            
        }
        //начать
        private void button2_Click(object sender, EventArgs e)
        {
            LoadTestFromXml();

            ShowRandomQuestions(groupBox1, doc);

            ShowElement();

        }

        private void LoadTestFromXml()
        {/*
            if (NumberTest == 1)
            {
                doc.Load("Test1.xml");
            }
            if (NumberTest == 2)
            {
                doc.Load("Test2.xml");
            }*/
            doc.Load("Test.xml");
        }
        //новые вопросы
        private void button1_Click(object sender, EventArgs e)
        {
            //проверка выбран ли радиобаттон в групбоксе
            ChekedRadioButton = CountChekedAnswer(groupBox1);
            if ( ChekedRadioButton != -1)
            {
                CheckAnswer(groupBox1, doc, CountQuestion, ref CountErrors);
                if (CountQuestion != doc.DocumentElement.ChildNodes.Count)
                {
                    ShowRandomQuestions(groupBox1, doc);
                }
                else
                {
                    button1.Visible = false;
                    EndTest();
                }
                ((RadioButton)groupBox1.Controls[ChekedRadioButton]).Checked = false;
                ChekedRadioButton = -1;
            }

            
        }

        public void ShowRandomQuestions(GroupBox groupBox, XmlDocument doc)
        {
            OutputShowQuastions.Clear();
            if (CountQuestion < doc.DocumentElement.ChildNodes.Count)
            {
                richTextBox1.Text = doc.DocumentElement.ChildNodes[CountQuestion].ChildNodes[0].ChildNodes[0].Value.ToString();//запись в ричбокс вопроса


                Random rnd1 = new Random();

                for (int j = 0; j != groupBox.Controls.Count; j++)
                {


                    int rndVar = rnd1.Next(4);

                    while (true)
                    {
                        if (!OutputShowQuastions.Contains(rndVar))
                        {

                            OutputShowQuastions.Add(rndVar);
                            groupBox.Controls[rndVar].Text = doc.DocumentElement.ChildNodes[CountQuestion].ChildNodes[j + 1].ChildNodes[0].Value.ToString();
                            break;
                        }
                        else
                            rndVar = rnd1.Next(4);
                    }
                }
            }
            else
            {
                MessageBox.Show("Был последний вопрос" + doc.DocumentElement.ChildNodes.Count.ToString(), "ddd", MessageBoxButtons.OK);

            }
            CountQuestion++;

        }


        private void ShowElement()
        {
            button2.Visible = false;
            button1.Visible = true;

            richTextBox1.Visible = true;
            groupBox1.Visible = true;
            label1.Visible = true;
        }


        private void HideElement()
        {
            richTextBox1.Visible = false;
            groupBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = false;
            button4.Visible = false;//используется только если нажат кнопка4 (закончить)
            label1.Visible = false;
        }

        

        public void ShowQuestions(GroupBox groupBox, XmlDocument doc)
        {            
            if (CountQuestion < doc.DocumentElement.ChildNodes.Count)
            {
                richTextBox1.Text = doc.DocumentElement.ChildNodes[CountQuestion].ChildNodes[0].ChildNodes[0].Value.ToString();
                for (int j = 1; j != groupBox.Controls.Count+1; j++)
                {
                    groupBox.Controls[j - 1].Text = doc.DocumentElement.ChildNodes[CountQuestion].ChildNodes[j].ChildNodes[0].Value.ToString(); 
                }
                    
            }
            else
            {
                MessageBox.Show("Был последний вопрос" + doc.DocumentElement.ChildNodes.Count.ToString(), "ddd", MessageBoxButtons.OK);
                
            }
            CountQuestion++;
        }

        public void CheckAnswer(GroupBox groupBox, XmlDocument doc, int CountQuestion, ref int CountErrors)
        {
            int NumberCorrectAnswer = Convert.ToInt32(doc.DocumentElement.ChildNodes[CountQuestion - 1].ChildNodes[5].ChildNodes[0].Value) - 1;

            //ChekedRadioButton = CountChekedAnswer(groupBox);

            int NumberSelectAnswer = GetNumberSelectAnswer(groupBox, ChekedRadioButton);

            if (NumberSelectAnswer != NumberCorrectAnswer)
            {
                CountErrors++;
            }
        }

        public int CountChekedAnswer(GroupBox groupBox)
        {
            int CountCheked = -1;
            
            for (int i = 0; i < 4; i++)
            {
                if (((RadioButton)groupBox.Controls[i]).Checked == true)
                {
                    CountCheked = i;
                }
            }

            return CountCheked;
        }


        private int GetNumberSelectAnswer(GroupBox groupBox, int CountCheked)
        {
            for (int i = 0; i != OutputShowQuastions.Count; i++)
            {
                if (CountCheked == (int)OutputShowQuastions[i])
                {
                    return i;
                }
            }
            return 10;
        }
        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        
        private void EndTest()
        {
            if (CountErrors > 0)
            {
                button3.Visible = true;//кнопка заново
                MessageBox.Show("Количество ошибок - " + CountErrors.ToString(), "Тест не пройден", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Количество ошибок - " + CountErrors.ToString(), "Тест пройден", MessageBoxButtons.OK);
                button4.Visible = true;//закончить
            }

        }
        //передвинуть все кнопки в центр
        private void MoveButtons()
        {
            button1.Left = Width / 2 - button1.Width / 2;
            button2.Left = Width / 2 - button1.Width / 2;
            button3.Left = Width / 2 - button1.Width / 2;
            button4.Left = Width / 2 - button1.Width / 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MoveButtons();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshVariable();
            HideElement();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            refreshVariable();
            HideElement();

            ShowButtonGraficOnMainForm();
            this.Hide();
        }

        private void refreshVariable()
        {
            CountQuestion = 0;
            CountErrors = 0;
            ChekedRadioButton = 0;
        }

        private void ShowButtonGraficOnMainForm()
        {
            Delegates.EnableButtonsGraphic(1);
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            refreshVariable();
            HideElement();
            Hide();
        }

        private void краткиеТеоретическиеСведенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonHelper.MyHelp(this, "1.htm");
        }

        private void заданиеНаСамоподготовкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonHelper.MyHelp(this, "2.htm");
        }        

        private void лабораторноеЗаданиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonHelper.MyHelp(this, "3.htm");
        }

        private void руководствоПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonHelper.MyHelp(this, "4.htm");
        }
        
    }
}
