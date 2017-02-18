using LaboratoryWork;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace LaboratoryWork
{
    class ImageBoxNew : PictureBox 
    {

        private float _Dx = 1F;//шаг
        
        public float Dx //шаг
        {
            get { return _Dx; }
            set { _Dx = value; }
        }
        float xFirst = -180F;//начальное
        float xLast = 180F;//конечное

        private Pen _PenDrawAxis = new Pen(Color.Black, 2);
        public Pen PenDrawAxis
        {
            get { return _PenDrawAxis; }
            set { _PenDrawAxis = value; }
        }
        private Pen _PenDrawGrid = new Pen(Color.Green, 1);
        public Pen PenDrawGrid
        {
            get { return _PenDrawGrid; }
            set { _PenDrawGrid = value; }
        }
        private Pen _PenDrawGraph = new Pen(Color.Red, 2);
        public Pen PenDrawGraph
        {
            get { return _PenDrawGraph; }
            set { _PenDrawGraph = value; }
        }
        Func<float, float> functionForCalculate = x => { return x; };
        public Func<float, float> FunctionForCalculate
        {
            get { return functionForCalculate; }
            set { functionForCalculate = value; }
        }


        float Coefficient_X_Cartesian;
        float Coefficient_Y_Cartesian;
        float Coefficient_X_Polar;
        float Coefficient_Y_Polar;
        
        private int CountLine_Y;
        private int CountLine_X;

        private int Count_Degrees;
        private float Step_Radius;

        private int X0_Cartesian;//декартовый
        private int Y0_Cartesian;//декартовый

        private int X0_Polar;
        private int Y0_Polar;

        private int Height1;

        LaboratoryWork.Enums.TypeCoordinateSystem TypeCoordinateSystem;

        public int HeightNew
        {
            get { return Height1; }

            set { Height1 = value; }
        }

        public int X0_Dec
        {
            get { return X0_Cartesian; }

            set { X0_Cartesian = value; }
        }

        public int Y0_Dec
        {
            get { return Y0_Cartesian; }

            set { Y0_Cartesian = value; }
        }

        public int X0_Pol
        {
            get { return X0_Polar; }

            set { X0_Polar = value; }
        }

        public int Y0_Pol
        {
            get { return Y0_Polar; }

            set { Y0_Polar = value; }
        }

       

        public Pen PenGrid
        {
            get { return PenDrawGrid; }

            set { PenDrawGrid = value; }
        }

        public Pen PenAxis
        {
            get { return PenDrawAxis; }

            set { PenDrawAxis = value; }
        }
        
        
        
        public LaboratoryWork.Enums.TypeCoordinateSystem CoordinateSystem
        {

            get { return TypeCoordinateSystem; }
            set { TypeCoordinateSystem = value; }
        }

        public int x0
        {
            get
            {
                if (CoordinateSystem == Enums.TypeCoordinateSystem.Cartesian)
                {
                    return  X0_Dec;
                }
                if (CoordinateSystem == Enums.TypeCoordinateSystem.Polar)
                {
                    return  X0_Pol;
                }
                return 0;
            }
        }

        public int y0
        {
            get
            {
                if (CoordinateSystem == Enums.TypeCoordinateSystem.Cartesian)
                {
                    return  Y0_Dec;
                }
                if (CoordinateSystem == Enums.TypeCoordinateSystem.Polar)
                {
                    return  Y0_Pol;
                }
                return 0;
            }
        }
        

        public int CountLineX
        {
            get { return CountLine_X; }

            set { CountLine_X = value; }
        }

        public int CountLineY
        {
            get { return CountLine_Y; }

            set { CountLine_Y = value; }
        }

        public int Degrees
        {
            get { return Count_Degrees; }

            set { Count_Degrees = value; }
        }

        public float StepRadius
        {
            get { return Step_Radius; }

            set { Step_Radius = value; }
        }


        public float Coef_X_Cartesian
        {
            get { return Coefficient_X_Cartesian; }

            set { Coefficient_X_Cartesian = value; }
        }

        public float Coef_Y_Cartesian
        {
            get { return Coefficient_Y_Cartesian; }

            set { Coefficient_Y_Cartesian = value; }
        }

        public float Coef_X_Polar
        {
            get { return Coefficient_X_Polar; }

            set { Coefficient_X_Polar = value; }
        }

        public float Coef_Y_Polar
        {
            get { return Coefficient_Y_Polar; }

            set { Coefficient_Y_Polar = value; }
        }

        public float CoefficientX
        {
            get
            {
                if (CoordinateSystem == Enums.TypeCoordinateSystem.Cartesian)
                {
                    return Coef_X_Cartesian;
                }
                if (CoordinateSystem == Enums.TypeCoordinateSystem.Polar)
                {
                    return Coef_X_Polar;
                }
                return 0;
            }
        }

        public float CoefficientY
        {
            get
            {
                if (CoordinateSystem == Enums.TypeCoordinateSystem.Cartesian)
                {
                    return Coef_Y_Cartesian;
                }
                if (CoordinateSystem == Enums.TypeCoordinateSystem.Polar)
                {
                    return Coef_Y_Polar;
                }
                return 0;
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            DrawGrid(e);//рисуем сетку
            DrawAxis(e);//рисуем оси
            e.Graphics.TranslateTransform(x0, y0);
            
            DrawGraph(e, FunctionForCalculate, PenDrawGraph);
            base.OnPaint(e);//возможность использовать функции предка у объектов этого класса
        }
        
        
        private void DrawGrid(PaintEventArgs e)
        {
            if (CoordinateSystem == Enums.TypeCoordinateSystem.Cartesian)
            {
                DrawGridDec(e, CountLineX, CountLineY, PenGrid);//рисуем координатную сетку
            }

            if (CoordinateSystem == Enums.TypeCoordinateSystem.Polar)
            {
                DrawGridPol(e, Degrees, StepRadius, x0, y0, PenGrid);
            }
        }

        private void DrawAxis(PaintEventArgs e)
        {
            e.Graphics.DrawLine(PenAxis, x0, 0, x0, Height);
            e.Graphics.DrawLine(PenAxis, 0, y0, Width, y0);
        }
        
        private void DrawGridDec(PaintEventArgs e, int CountLineX, int CountLineY, Pen MyP)
        {
            for (int i = 1; i < CountLineX; i++)
            {
                e.Graphics.DrawLine(MyP, 0, i * Height / CountLineY, Width, i * Height / CountLineX);
            }

            for (int i = 1; i < CountLineY; i++)
            {
                e.Graphics.DrawLine(MyP, i * Width / CountLineX, 0, i * Width / CountLineX, Height);
            }
        }

        private void DrawGridPol(PaintEventArgs e, int Degrees, float StepRadius, int x0, int y0, Pen MyP)
        {
            DrawPolGridLine(e, Degrees, x0, y0, PenGrid);//рисуем координатную сетку (линии)
            DrawPolGridCircles(e, StepRadius, x0, y0, PenGrid);//рисуем координатную сетку (окружности)
        }

        /// <summary>
        /// рисуем линии для грида в полярной
        /// </summary>
        /// <param name="e">где рисовать</param>
        /// <param name="Degrees">угол, влияет на количество линий</param>
        /// <param name="x0">коориданта х0 центра</param>
        /// <param name="y0">коориданта у0 центра</param>
        /// <param name="MyP">карандаш</param>

        private void DrawPolGridLine(PaintEventArgs e, int Degrees, int x0, int y0, Pen MyP)
        {
            int Radius = Height / 2;

            int Catet1;//для отрисовки конца линии по ОХ
            int Catet2;//для отрисовки конца линии по ОУ
            for (int i = 1; i < 360 / Degrees; i++)
            {
                if ((Degrees * i != 90) && (Degrees * i != 270) && (Degrees * i != 180) && (Degrees * i != 360))
                {
                    Calculations.Catets(i * Degrees, Radius, out Catet1, out Catet2);
                    e.Graphics.DrawLine(MyP, x0, y0, x0 + Catet1, Catet2 + y0);
                }
            }
        }

        /// <summary>
        /// рисуем окружности для грида в полярной
        /// </summary>
        /// <param name="e">где рисовать</param>
        /// <param name="StepRadius">радиус, влияет на количество окружностей</param>
        /// <param name="x0">коориданта х0 центра</param>
        /// <param name="y0">коориданта у0 центра</param>
        /// <param name="MyP">карандаш</param>
        private void DrawPolGridCircles(PaintEventArgs e, float StepRadius, int x0, int y0, Pen MyPen)
        {
            int Radius1 = Height / 2;
            int Radius;
            //1 это максимальное значение которое может принимать Y, 10 нужно для расчетов в int иначе во флоте не верно решает (StepRadius * i <= 1)
            for (float i = 1F; (int)(StepRadius * i * 10) <= 1 * 10; i++)
            {
                Radius = (int)(Radius1 * StepRadius * i);
                e.Graphics.DrawEllipse(MyPen, x0 - Radius, y0 - Radius, Radius * 2, Radius * 2);
            }
        }


        private void DrawGraph(PaintEventArgs e, Func<float, float> funcForCalculate, Pen MyP)
        {
            //инициализация значений
            var pointDecAct = new PointF(xFirst, funcForCalculate(xFirst));   //текущая точка отрисовки графика
            var pointDecNext = new PointF(pointDecAct.X + Dx, 0);  //следующая точка отрисовки графика            
            PointF pointPolAct;
            PointF pointPolNext;

            var pointActForPaint = new PointF();
            var pointNextForPaint = new PointF();

            for (; pointDecAct.X < xLast; pointDecNext.X = pointDecAct.X + Dx)//расчет х начального и последующего (x+Dx)
            {
                pointDecNext.Y = funcForCalculate(pointDecNext.X);
                if (TypeCoordinateSystem == Enums.TypeCoordinateSystem.Polar)
                {
                    pointPolAct = TranslateDecInPol(pointDecAct);
                    pointPolNext = TranslateDecInPol(pointDecNext);
                    //возможно вынести в функцию, для удобвства чтения
                    pointActForPaint.X = pointPolAct.X * CoefficientY;
                    pointActForPaint.Y = pointPolAct.Y * CoefficientY;

                    pointNextForPaint.X = pointPolNext.X * CoefficientY;
                    pointNextForPaint.Y = pointPolNext.Y * CoefficientY;
                }
                else
                {
                    pointActForPaint.X = pointDecAct.X * CoefficientX;
                    pointActForPaint.Y = pointDecAct.Y * CoefficientY;

                    pointNextForPaint.X = pointDecNext.X * CoefficientX;
                    pointNextForPaint.Y = pointDecNext.Y * CoefficientY;
                }
                e.Graphics.DrawLine(MyP, pointActForPaint, pointNextForPaint);

                pointDecAct.X = pointDecNext.X;
                pointDecAct.Y = pointDecNext.Y;// текущий равняем к следующему для следующей итерации
            }
        }

        /// <summary>
        /// перевод декартовых координат в полярные для отрисовки в деркатовой системе
        /// </summary>
        /// <param name="pointDec"></param>
        /// <returns></returns>
        private static PointF TranslateDecInPol(PointF pointDec)
        {
            var pointPol = new PointF();

            pointPol.X = -CosInRadians(pointDec.X) * pointDec.Y;
            pointPol.Y = -SinInRadians(pointDec.X) * pointDec.Y;
            return pointPol;
        }

        /// <summary>
        /// функция Cos возвращающая флот и считающая в градусах, а не радианах
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static float CosInRadians(float x)
        {
            return (float)Math.Cos(x / 180F * Math.PI);
        }

        /// <summary>
        /// функция Sin возвращающая флот и считающая в градусах, а не радианах
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static float SinInRadians(float x)
        {
            return (float)Math.Sin(x / 180F * Math.PI);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBoxNew
            // 
            
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

