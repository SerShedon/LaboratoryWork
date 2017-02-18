using System;
using System.Drawing;
using System.Windows.Forms;

namespace LaboratoryWork
{
    public partial class Grafic1 : Form
    {
        public Grafic1()
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
            MyImageBoxNew.PenDrawGraph = new Pen(Color.Red, 2);
            MyImageBoxNew.FunctionForCalculate = x => { return - Calculations.GeneralFuncForDrawDec(x, Consts.TypeFunctions, Enums.NameFunction.F_4); };
        }
        

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void imageBoxNew1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
