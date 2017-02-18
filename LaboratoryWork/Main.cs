using System;
using System.Windows.Forms;


namespace LaboratoryWork
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            Form MyBootForm = new BootForm();
            MyBootForm.ShowDialog();//окно с информацией о программе            
            
            InitializeComponent();

            //для того чтобы вызывать func функцию из TestForm 
            Delegates.EnableButtonsGraphic = new Delegates.MyEvent(ShowControl);
        }
        public Form MyTestForm = new TestForm();
        public Form MyGrafigForm = new ParametrsForm();

        void ShowControl(int Button)
        {
            ControlVisible(true);
        }

        private void ControlVisible(bool TrueOrFalse)
        {
            if (TrueOrFalse == true)
            {
                ButtonGraphic1.Visible = true;
                ButtonGraphic2.Visible = true;
                label1.Visible = true;
            }
            else
            {
                ButtonGraphic1.Visible = false;
                ButtonGraphic2.Visible = false;
                label1.Visible = false;
            }
        }
        

        //раскоменить если будет несколько тестов
        //показывет форму для тестирования
        private void button2_Click(object sender, EventArgs e)
        {
            MyTestForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //задаем тип спиральной антенны и тип функции
            Consts.TypeSpiralAntennas = Enums.TypeSpiralAntennas.Single;
            Consts.TypeFunctions = Enums.TypeFunction.E_LessOpt;
            //показываем форму с графиками
            MyGrafigForm.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //задаем тип спиральной антенны и тип функции
            Consts.TypeSpiralAntennas = Enums.TypeSpiralAntennas.System;
            Consts.TypeFunctions = Enums.TypeFunction.E_LessOpt;
            //показываем форму с графиками
            MyGrafigForm.ShowDialog();
        }
        // читерские кнопочки для отображения кнопок с графиками в обход верного выполненого тестирования
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {   
            //показать
            if (e.KeyCode == Keys.F12)
            {
                ControlVisible(true);
            }
            //скрыть
            if (e.KeyCode == Keys.F11)
            {
                ControlVisible(false);
            }
        }

        private void краткиеТеоритическиеСведенияToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {

            Form MyGrafigForm1 = new Grafic1();
            MyGrafigForm1.Show();
        }
    }
}
