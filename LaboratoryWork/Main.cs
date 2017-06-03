using System;
using System.Windows.Forms;

namespace LaboratoryWork
{
    public partial class MainForm : Form
    {
        private Consts consts;
        private Calculations calculations;
        public Form MyTestForm { get; set; }
        public Form MyGrafigForm { get; set; }
        public Form MyBootForm { get; set; }

        public MainForm(Consts consts, Calculations calculations)
        {
            this.consts = consts;
            this.calculations = calculations;
            MyBootForm = new BootForm();
            MyTestForm = new TestForm();
            MyGrafigForm = new ParametrsForm(consts, calculations);
            MyBootForm.ShowDialog();//окно с информацией о программе
            
            InitializeComponent();

            //для того чтобы вызывать func функцию из TestForm 
            Delegates.EnableButtonsGraphic = new Delegates.MyEvent(ShowControl);
        }
        

        void ShowControl(int Button)
        {
            ControlVisible(true);
        }

        private void ControlVisible(bool isVisible)
        {
            ButtonGraphic1.Visible = isVisible;
            ButtonGraphic2.Visible = isVisible;
            label1.Visible = isVisible;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            MyTestForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            consts.TypeSpiralAntennas = Enums.TypeSpiralAntennas.Single;
            consts.TypeFunctions = Enums.TypeFunction.E_LessOpt;

            MyGrafigForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            consts.TypeSpiralAntennas = Enums.TypeSpiralAntennas.System;
            consts.TypeFunctions = Enums.TypeFunction.E_LessOpt;

            MyGrafigForm.ShowDialog();
        }
        // читерские кнопочки для отображения кнопок с графиками в обход верного выполненого тестирования
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                ControlVisible(true);

            if (e.KeyCode == Keys.F11)
                ControlVisible(false);
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
    }
}
