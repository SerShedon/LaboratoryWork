using System;
using System.Drawing;
using System.Windows.Forms;

namespace LaboratoryWork
{
    public partial class ParametrsForm : Form
    {
       
        public ParametrsForm()
        {
            InitializeComponent();
            GetValuesFromTrackBars();
            
        }
        private void InitializeImageBoxNew(GraphBox graphBox)
        {
            graphBox.CoordinateSystem = Enums.TypeCoordinateSystem.Polar;
            graphBox.X0_Pol = graphBox.Width / 2;
            graphBox.Y0_Pol = graphBox.Height / 2;

            graphBox.X0_Dec = graphBox.Width / 2;
            graphBox.Y0_Dec = graphBox.Height;

            graphBox.Degrees = 30;
            graphBox.StepRadius = 0.2F;

            graphBox.CountLineX = 10;
            graphBox.CountLineY = 10;

            graphBox.Coef_X_Cartesian = graphBox.Width / 360F;
            graphBox.Coef_Y_Cartesian = graphBox.Height / 1F;

            graphBox.Coef_X_Polar = graphBox.Width / 360F;
            graphBox.Coef_Y_Polar = graphBox.Height / 2F;
        }
        
        //инициализация новых переменных, присвоением им значений 
        #region
        private static float dx = 1F;
        /// <summary>
        /// шаг
        /// </summary>
        public static float Dx
        {
            get { return dx;}
        }

        //ToDo: подумать об автоматическом масштабировани, например при Consts.TypeFunctions == Enums.TypeFunction.E_Crit, нужно уменьшить коэффициент Coef_Y = Coef_Y / 3.5F;
        Pen PenDrawGrid = new Pen(Color.Green, 1);
        Pen PenDrawAxis = new Pen(Color.Black, 2);
        Pen PenDrawGraph = new Pen(Color.Red, 2);
                
        private void ParametrsFormLoad(object sender, EventArgs e)
        {
            GetValuesFromTrackBars();
            WriteValuesInTextBoxs();
            SetValueInCalculations();
            RefreshConstants();
        }

        private void GetValuesFromTrackBars()
        {
            Consts.F = trackBar1?.Value ?? 0;
            Consts.N = trackBar2?.Value ?? 0;
            Consts.A = trackBar3?.Value ?? 0;
            Consts.M = trackBar4?.Value ?? 0;
            Consts.D_f = trackBar5?.Value ?? 0;
            Consts.Q = trackBar6?.Value ?? 0;
        }

        private void WriteValuesInTextBoxs()
        {
            textBox1.Text = Consts.F.ToString();
            textBox2.Text = Consts.N.ToString();
            textBox3.Text = Consts.A.ToString();
            textBox4.Text = Consts.M.ToString();
            textBox5.Text = (Consts.D_f / 100f).ToString("0.00");
            textBox6.Text = Consts.Q.ToString();
        }

        private void SetValueInCalculations()
        {
            Calculations.k_f = Consts.F;            
            Calculations.My_d_f(Consts.D_f, Consts.F);            
        }
        /*
        private float GetCoefY()
        {
            float CoefY;
            float X = Xfirst;
            float Ymax = 0;
            
            for (; X <= Xlast; X = X + MyDx)
            {
                CoefY = Calculations.GeneralFuncForDrawDec(X, Consts.TypeFunctions, Enums.NameFunction.F_4);
                if (Ymax > CoefY)
                {
                    Ymax = CoefY;
                }
                
            }
            return -(imageBoxNew3.Height - (imageBoxNew3.Height - imageBoxNew3.y0)) / (Ymax);
        }*/
        #endregion

        //рисование в pictureBoxs
        #region
        private void imageBoxNew1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void imageBoxNew2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void imageBoxNew3_Paint(object sender, PaintEventArgs e)
        {
            
        }
        #endregion
       

        //дейсвтия при  изменении положения ползунка в  trackBars
        //переприсвоение значений для переменных в функции графиков
        //отрисовка новой функции в pictureBoxs
        //вывод значений трекбара в textboxs
        #region
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Consts.F = trackBar1.Value;

            RefreshConstants();
            textBox1.Text = Consts.F.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Consts.N = trackBar2.Value;
            textBox2.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Consts.A = trackBar3.Value;
            textBox3.Text = trackBar3.Value.ToString();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            Consts.M = trackBar4.Value;
            textBox4.Text = trackBar4.Value.ToString();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            Consts.D_f = trackBar5.Value ;
            
            textBox5.Text = (Consts.D_f /100.0).ToString("0.00");
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            Consts.Q = trackBar6.Value;

            textBox6.Text = Consts.Q.ToString();
        }
        #endregion
        
        //выбор типа функции через radioButtons 
        #region
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_LessOpt;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_Opt;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_Crit;
        }
        #endregion

        //определение коориднат курсора мыши при движении по pictuareBox        
        #region
        //функции для определения значений
        #region

        private void ShowCoordinateInLabel(Label lb1, Label lb2, string TextForLabel1, string TextForLabel2, GraphBox imageBoxNew, MouseEventArgs e, float CoefficientX, float CoefficientY)
        {

            if (imageBoxNew.CoordinateSystem == Enums.TypeCoordinateSystem.Cartesian)
            {
                lb1.Text = "По оси X " + imageBoxNew.CoordinateX(e.X, imageBoxNew.x0, CoefficientX).ToString();
                lb2.Text = "По оси Y " + imageBoxNew.CoordinateY(e.Y, imageBoxNew.y0, CoefficientY).ToString("0.00");
            }
            if (imageBoxNew.CoordinateSystem == Enums.TypeCoordinateSystem.Polar)
            {
                lb1.Text = TextForLabel1 + " " + imageBoxNew.CoordinateR(e.X, e.Y, imageBoxNew.x0, imageBoxNew.y0, CoefficientY).ToString("0.00");
                lb2.Text = TextForLabel2 + " " + imageBoxNew.CoordinateFi(e.X, e.Y, imageBoxNew.x0, imageBoxNew.y0).ToString("0.00") + ", град";
            }
        }


        #endregion
        #endregion

        //задание значений трекбаров через текстбоксы, а так же проверка
        #region
        private void ChackValue(KeyPressEventArgs e, TrackBar trackBar, TextBox textBox)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    trackBar.Value = Convert.ToInt32(textBox.Text);
                }
                catch
                {
                    MessageBox.Show("Вы ввели не верное значение, введите заного", "Ошибка ввода", MessageBoxButtons.OK);
                }
            }
        }
        //использование функции в событиях pressKey у textBox
        #region
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ChackValue(e, trackBar1, textBox1);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ChackValue(e, trackBar2, textBox2);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ChackValue(e, trackBar3, textBox3);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            ChackValue(e, trackBar4, textBox4);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            ChackValue(e, trackBar5, textBox5);
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            ChackValue(e, trackBar6, textBox6);
        }
        #endregion
        #endregion

        //рефреши
        #region
        

        private void RefreshConstants()
        {
            textBox9.Text = Consts.L.ToString("0.000");
            textBox7.Text = Consts.S_a_f.ToString("0.000");
            textBox8.Text = Consts.A_a_f.ToString("0.000");
        }
        #endregion

        private void ParametrsFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void VisibilityComponents()
        {
            bool isVisible = false;
            if (Consts.TypeSpiralAntennas == Enums.TypeSpiralAntennas.System)
                isVisible = true;

            label4.Visible = isVisible;
            label5.Visible = isVisible;
            label18.Visible = isVisible;
            label19.Visible = isVisible;

            trackBar4.Visible = isVisible;
            trackBar5.Visible = isVisible;

            textBox4.Visible = isVisible;
            textBox5.Visible = isVisible;
        }

        private void ParametrsFormShown(object sender, EventArgs e)
        {
            VisibilityComponents();
        }

        private void краткиеТеоретическиеСведенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonHelper.MyHelp(this, "1.htm");
        }

        private void заданиеНаСамоподготовкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonHelper.MyHelp(this, "2.htm");
        }        

        private void лабораторноеЗаданиеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CommonHelper.MyHelp(this, "3.htm");
        }

        private void руководствоПользователяToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CommonHelper.MyHelp(this, "4.htm");
        }

        private void меньшеОптимальногоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_LessOpt;
        }

        private void оптимальныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_Opt;
        }

        private void критическийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_Crit;
        }

        private void декартоваяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*imageBoxNew1.CoordinateSystem = Enums.TypeCoordinateSystem.Cartesian;
            imageBoxNew2.CoordinateSystem = Enums.TypeCoordinateSystem.Cartesian;
            RefreshPictureBoxs();*/
        }

        private void полярнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*imageBoxNew1.CoordinateSystem = Enums.TypeCoordinateSystem.Polar;
            imageBoxNew2.CoordinateSystem = Enums.TypeCoordinateSystem.Polar;
            RefreshPictureBoxs();*/
        }
        private GraphFrom GraphFrom1;
        private GraphFrom GraphFrom2;
        private GraphFrom GraphFrom3;
        private void btnGraphForm1_Click(object sender, EventArgs e)
        {
            GraphFrom1 = new GraphFrom();
            GraphFrom1.Show();
            GraphFrom1.SetFunctionForCalculate((x) => { return -Calculations.GeneralFuncForDrawDec(x, Consts.TypeFunctions, Enums.NameFunction.F_4); });
            GraphFrom1.TextForFirstCartesianCoordinate = "По оси X {0}";
            GraphFrom1.TextForSecondCartesianCoordinate = "По оси Y {0}";
            GraphFrom1.TextForFirstPolarCoordinate = "Значение ДН по радиальному направлению {0}";
            GraphFrom1.TextForSecondPolarCoordinate = "Полярный угол {0} град";
            GraphFrom1.NameFunction = "Нормированная ДН в плоскости, перпендикулярной витку";
            //GraphFrom1.SetCoordinateSystem(Enums.TypeCoordinateSystem.Polar);
        }

        private void btnGraphForm2_Click(object sender, EventArgs e)
        {
            GraphFrom2 = new GraphFrom();
            GraphFrom2.Show();
            GraphFrom2.SetFunctionForCalculate((x) => { return -Calculations.GeneralFuncForDrawDec(x, Consts.TypeFunctions, Enums.NameFunction.F_Q); });
            GraphFrom2.TextForFirstCartesianCoordinate = "По оси X {0}";
            GraphFrom2.TextForSecondCartesianCoordinate = "По оси Y {0}";
            GraphFrom2.TextForFirstPolarCoordinate = "Значение ДН по радиальному направлению {0}";
            GraphFrom2.TextForSecondPolarCoordinate = "Полярный угол {0} град";
            GraphFrom2.NameFunction = "Нормированная ДН в плоскости, содержащей витки";
            //GraphFrom1.SetCoordinateSystem(Enums.TypeCoordinateSystem.Polar);
        }

        private void btnGraphForm3_Click(object sender, EventArgs e)
        {
            GraphFrom3 = new GraphFrom();
            GraphFrom3.Show();
            GraphFrom3.SetFunctionForCalculate((x) => { return -Calculations.GeneralFuncForDrawDec(x, Consts.TypeFunctions, Enums.NameFunction.r); });
            GraphFrom3.TextForFirstCartesianCoordinate = "По оси X {0}";
            GraphFrom3.TextForSecondCartesianCoordinate = "По оси Y {0}";
            GraphFrom3.TextForFirstPolarCoordinate = "Величина поляризационной характеристики по радиальному направлению {0}";
            GraphFrom3.TextForSecondPolarCoordinate = "Полярный угол {0} град";
            GraphFrom3.NameFunction = "Поляризационный эллипс";
            //GraphFrom1.SetCoordinateSystem(Enums.TypeCoordinateSystem.Polar);
        }
    }
}

