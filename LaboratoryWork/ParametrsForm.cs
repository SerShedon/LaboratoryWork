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
            //imageBoxNew1
            InitializeImageBoxNew(imageBoxNew1);
            imageBoxNew1.FunctionForCalculate = x => { return - Calculations.GeneralFuncForDrawDec(x, Consts.TypeFunctions, Enums.NameFunction.F_4); };

            //imageBoxNew2

            InitializeImageBoxNew(imageBoxNew2);
            imageBoxNew2.FunctionForCalculate = x => { return - Calculations.GeneralFuncForDrawDec(x, Consts.TypeFunctions, Enums.NameFunction.F_Q); };

            //imageBoxNew3

            InitializeImageBoxNew(imageBoxNew3);
            imageBoxNew3.FunctionForCalculate = x => { return - Calculations.GeneralFuncForDrawDec(x, Consts.TypeFunctions, Enums.NameFunction.r); };

        }
        private void InitializeImageBoxNew(ImageBoxNew MyImageBoxNew)
        {
            MyImageBoxNew.CoordinateSystem = Enums.TypeCoordinateSystem.Polar;
            MyImageBoxNew.X0_Pol = MyImageBoxNew.Width / 2;
            MyImageBoxNew.Y0_Pol = MyImageBoxNew.Height / 2;

            MyImageBoxNew.X0_Dec = MyImageBoxNew.Width / 2;
            MyImageBoxNew.Y0_Dec = MyImageBoxNew.Height;

            MyImageBoxNew.Degrees = 30;
            MyImageBoxNew.StepRadius = 0.2F;

            MyImageBoxNew.CountLineX = 10;
            MyImageBoxNew.CountLineY = 10;

            MyImageBoxNew.Coef_X_Cartesian = MyImageBoxNew.Width / 360F;
            MyImageBoxNew.Coef_Y_Cartesian = MyImageBoxNew.Height / 1F;

            MyImageBoxNew.Coef_X_Polar = MyImageBoxNew.Width / 360F;
            MyImageBoxNew.Coef_Y_Polar = MyImageBoxNew.Height / 2F;
        }
        
        //инициализация новых переменных, присвоением им значений 
        #region
        //шаг
        private static float Dx = 1F;//шаг

        public static float MyDx //шаг
        {
            get { return Dx;}
        }

        //в каких пределах будут значения тетта (Q)-аргумента функции
        float Xfirst = -180F;//начальное
        float Xlast  =  180F;//конечное

        public PointF P_first; //точка с которой будет начинаться рисоваться график       
        public PointF P_act;   //текущая точка отрисовки графика
        public PointF P_next;  //следующая точка отрисовки графика
        public PointF P_last;  //точка которой будет заканчиваться рисоваться график 


        //коэффициенты для различных ImageBoxNew (1,2,3)
        //для ImageBoxNew1
        float Coef_X1;
        float Coef_Y1;
        //для ImageBoxNew2
        float Coef_X2;
        float Coef_Y2;
        //для ImageBoxNew3
        float Coef_X3;
        float Coef_Y3;
        //

        private void GetCoefficientsForImageBoxNew1()//переделать программу и удалить эту функцию
        {
            Coef_X1 = imageBoxNew1.CoefficientX;
            Coef_Y1 = imageBoxNew1.CoefficientY;
            if (Consts.TypeFunctions == Enums.TypeFunction.E_Crit)
            {
                Coef_Y1 = Coef_Y1 / 3.5F;
            }
        }

        private void GetCoefficientsForImageBoxNew2()//переделать программу и удалить эту функцию
        {
            Coef_X2 = imageBoxNew2.CoefficientX;
            Coef_Y2 = imageBoxNew2.CoefficientY;
            if (Consts.TypeFunctions == Enums.TypeFunction.E_Crit)
            {
                Coef_Y2 = Coef_Y2 / 3.5F;
            }
        }

        private void GetCoefficientsForImageBoxNew3()//переделать программу и удалить эту функцию
        {
            Coef_X3 = imageBoxNew3.CoefficientX;         
            Coef_Y3 = GetCoefY();            
        }

        Pen PenDrawGrid = new Pen(Color.Green, 1);
        Pen PenDrawAxis = new Pen(Color.Black, 2);
        Pen PenDrawGraph = new Pen(Color.Red, 2);
                
        private void ParametrsFormLoad(object sender, EventArgs e)
        {
            GetValuesFromTrackBars();
            WriteValuesInTextBoxs();
            GetCoefY();
            SetValueInCalculations();
            RefreshConstants();
        }

        private void GetValuesFromTrackBars()
        {
            Consts.f = trackBar1?.Value ?? 0;
            Consts.N = trackBar2?.Value ?? 0;
            Consts.a = trackBar3?.Value ?? 0;
            Consts.M = trackBar4?.Value ?? 0;
            Consts.d_f = trackBar5?.Value ?? 0;
            Consts.Q = trackBar6?.Value ?? 0;
        }

        private void WriteValuesInTextBoxs()
        {
            textBox1.Text = Consts.f.ToString();
            textBox2.Text = Consts.N.ToString();
            textBox3.Text = Consts.a.ToString();
            textBox4.Text = Consts.M.ToString();
            textBox5.Text = (Consts.d_f / 100f).ToString("0.00");
            textBox6.Text = Consts.Q.ToString();
        }

        private void SetValueInCalculations()
        {
            Calculations.k_f = Consts.f;            
            Calculations.My_d_f(Consts.d_f, Consts.f);            
        }

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
        }
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
            Consts.f = trackBar1.Value;

            RefreshConstants();
            RefreshPictureBoxs();
            textBox1.Text = Consts.f.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Consts.N = trackBar2.Value;
            textBox2.Text = trackBar2.Value.ToString();
            RefreshPictureBoxs();            
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Consts.a = trackBar3.Value;
            textBox3.Text = trackBar3.Value.ToString();
            RefreshPictureBoxs();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            Consts.M = trackBar4.Value;
            textBox4.Text = trackBar4.Value.ToString();
            RefreshPictureBoxs();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            Consts.d_f = trackBar5.Value ;
            
            textBox5.Text = (Consts.d_f /100.0).ToString("0.00");
            RefreshPictureBoxs();
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            Consts.Q = trackBar6.Value;

            textBox6.Text = Consts.Q.ToString();
            RefreshPictureBoxs();
        }
        #endregion
        
        //выбор типа функции через radioButtons 
        #region
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_LessOpt;
            RefreshPictureBoxs();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_Opt;
            RefreshPictureBoxs();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_Crit;
            RefreshPictureBoxs();
        }
        #endregion

        //определение коориднат курсора мыши при движении по pictuareBox        
        #region
        //функции для определения значений
        #region

        private void ShowCoordinateInLabel(Label lb1, Label lb2, string TextForLabel1, string TextForLabel2, ImageBoxNew imageBoxNew, MouseEventArgs e, float CoefficientX, float CoefficientY)
        {

            if (imageBoxNew.CoordinateSystem == Enums.TypeCoordinateSystem.Cartesian)
            {
                lb1.Text = "По оси X " + CoordinateX(e.X, imageBoxNew.Width, imageBoxNew.x0, CoefficientX).ToString();
                lb2.Text = "По оси Y " + CoordinateY(e.Y, imageBoxNew.Height, imageBoxNew.y0, CoefficientY).ToString("0.00");
            }
            if (imageBoxNew.CoordinateSystem == Enums.TypeCoordinateSystem.Polar)
            {
                lb1.Text = TextForLabel1 + " " + CoordinateR(imageBoxNew.ClientRectangle, e, imageBoxNew.x0, imageBoxNew.y0, CoefficientY).ToString("0.00");
                lb2.Text = TextForLabel2 + " " + CoordinateFi(imageBoxNew.ClientRectangle, e, imageBoxNew.x0, imageBoxNew.y0).ToString("0.00") + ", град";
            }
        }

        public int CoordinateX(int LocationX, int PictureBoxWidth, int x0, float Coef_X)
        {
            return (int)((LocationX - x0) / Coef_X);
        }
        public float CoordinateY(int LocationY, int PictureBoxHeight, int y0, float Coef_Y)
        {
            return -(LocationY - y0) / Coef_Y;
        }

        public float CoordinateR(Rectangle PictureBoxRectangle, MouseEventArgs e, int x0, int y0, float Coef_Y)
        {
            float x = ((e.X - x0) / Coef_Y);
            float y = (-(e.Y - y0) / Coef_Y);

            //по формуле R = (X^2+Y^2)^(1/2)
            return (float)(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
        }

        public float CoordinateFi(Rectangle PictureBoxRectangle, MouseEventArgs e, int x0, int y0)
        {
            //
            float Y = -(e.Y - y0);
            float X = (e.X - x0);

            return (float)(Calculations.Atan2(Y, X));
        }

        
    

        #endregion
        //pictureBox_MouseMove 
        #region
        private void imageBoxNew1_MouseMove(object sender, MouseEventArgs e)
        {
            GetCoefficientsForImageBoxNew1();
            ShowCoordinateInLabel(label6, label7, "Значение ДН по радиальному направлению", "Полярный угол", imageBoxNew1, e, Coef_X1, Coef_Y1);
        }

        private void imageBoxNew2_MouseMove(object sender, MouseEventArgs e)
        {

            GetCoefficientsForImageBoxNew2();
            ShowCoordinateInLabel(label8, label9, "Значение ДН по радиальному направлению", "Полярный угол", imageBoxNew2, e, Coef_X2, Coef_Y2);
        }

        private void imageBoxNew3_MouseMove(object sender, MouseEventArgs e)
        {
            GetCoefficientsForImageBoxNew3();
            ShowCoordinateInLabel(label10, label11, "Величина поляризационной характеристики по радиальному направлению", "Полярный угол", imageBoxNew3, e, Coef_X3, Coef_Y3);
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

        private void RefreshPictureBoxs()
        {
            imageBoxNew1.Refresh();
            imageBoxNew2.Refresh();
            imageBoxNew3.Refresh();
        }
        #endregion

        
        
        // Для выбора полярной или декартовой системы координат
        #region
        private void button4_Click(object sender, EventArgs e)
        {
            imageBoxNew1.CoordinateSystem = Enums.TypeCoordinateSystem.Cartesian;
            imageBoxNew2.CoordinateSystem = Enums.TypeCoordinateSystem.Cartesian;
            RefreshPictureBoxs();            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            imageBoxNew1.CoordinateSystem = Enums.TypeCoordinateSystem.Polar;
            imageBoxNew2.CoordinateSystem = Enums.TypeCoordinateSystem.Polar;
            RefreshPictureBoxs();

        }
        #endregion

        private void ParametrsFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        

        private void ParametrsFormMove(object sender, EventArgs e)
        {
            RefreshPictureBoxs();
        }

        private void VisibilityComponents()
        {
            bool TrueOrFalse = false;
            if (Consts.TypeSpiralAntennas == Enums.TypeSpiralAntennas.System)
            {
                TrueOrFalse = true;                
            }

            label4.Visible = TrueOrFalse;
            label5.Visible = TrueOrFalse;
            label18.Visible = TrueOrFalse;
            label19.Visible = TrueOrFalse;

            trackBar4.Visible = TrueOrFalse;
            trackBar5.Visible = TrueOrFalse;

            textBox4.Visible = TrueOrFalse;
            textBox5.Visible = TrueOrFalse;


            
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
            RefreshPictureBoxs();
        }

        private void оптимальныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_Opt;
            RefreshPictureBoxs();
        }

        private void критическийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consts.TypeFunctions = Enums.TypeFunction.E_Crit;
            RefreshPictureBoxs();
        }

        private void декартоваяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageBoxNew1.CoordinateSystem = Enums.TypeCoordinateSystem.Cartesian;
            imageBoxNew2.CoordinateSystem = Enums.TypeCoordinateSystem.Cartesian;
            RefreshPictureBoxs();
        }

        private void полярнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageBoxNew1.CoordinateSystem = Enums.TypeCoordinateSystem.Polar;
            imageBoxNew2.CoordinateSystem = Enums.TypeCoordinateSystem.Polar;
            RefreshPictureBoxs();
        }
        
    }
}

