using System;
using System.Drawing;
using System.Windows.Forms;

namespace LaboratoryWork
{
    public partial class GraphFrom : Form
    {
        public string TextForFirstCartesianCoordinate { get; set; }
        public string TextForFirstPolarCoordinate { get; set; }

        public string TextForSecondCartesianCoordinate { get; set; }
        public string TextForSecondPolarCoordinate { get; set; }
        public string nameFunction;
        public string NameFunction {
            get { return nameFunction; }
            set
            {
                nameFunction = value;
                lblNameFunction.Text = nameFunction;
            }
        }
        public void SetFunctionForCalculate(Func<float, float> function) => imageBox.FunctionForCalculate = function;
        public void SetCoordinateSystem(Enums.TypeCoordinateSystem function) => imageBox.CoordinateSystem = function;

        public GraphFrom()
        {
            InitializeComponent();
            InitializeImageBoxNew(imageBox);
            
            Delegates.OnChangeConsts = new Delegates.ChangeConsts(RefreshPainBox);
            Delegates.OnChangeTypeFunctions = new Delegates.ChangeTypeFunctions(RefreshPainBox);
        }

        private void RefreshPainBox()
        {
            imageBox.Refresh();
        }

        private void InitializeImageBoxNew(GraphBox MyImageBoxNew)
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
            MyImageBoxNew.PenDrawGraph = new Pen(Color.Red, 2);
        }
        

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void imageBoxNew1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        public string GetTextForFirstCoordinate()
        {
            if (imageBox.CoordinateSystem == Enums.TypeCoordinateSystem.Cartesian)
                return TextForFirstCartesianCoordinate;
            if (imageBox.CoordinateSystem == Enums.TypeCoordinateSystem.Polar)
                return TextForFirstPolarCoordinate;
            throw new ArgumentException("Не известная система координат");
        }

        public string GetTextForSecondCoordinate()
        {
            if (imageBox.CoordinateSystem == Enums.TypeCoordinateSystem.Cartesian)
                return TextForSecondCartesianCoordinate;
            if (imageBox.CoordinateSystem == Enums.TypeCoordinateSystem.Polar)
                return TextForSecondPolarCoordinate;
            throw new ArgumentException("Не известная система координат");
        }

        private void imageBox_Move(object sender, MouseEventArgs e) =>
            ShowCoordinateInLabels(lblFirstCoordinate, lblSecondCoordinate, GetTextForFirstCoordinate(), GetTextForSecondCoordinate(), imageBox, e, imageBox.CoefficientX, imageBox.CoefficientY);

        private void ShowCoordinateInLabels(Label label1, Label label2, string textForFirstCoordinate, string textForSecondCoordinate, GraphBox imageBoxNew, MouseEventArgs e, float CoefficientX, float CoefficientY)
        {
            GetTextForFirstCoordinate();
            label1.Text = String.Format(textForFirstCoordinate, imageBoxNew.GetFirstCoordinate(e.X, e.Y).ToString("0.00"));
            label2.Text = String.Format(textForSecondCoordinate, imageBoxNew.GetSecondCoordinate(e.X, e.Y).ToString("0.00"));
        }
    }
}
