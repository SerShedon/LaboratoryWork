using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using LaboratoryWork;

namespace LaboratoryWork
{
    class Calculations
    {
        //перевод частоты f из мегагерц в герцы
        /// <summary>
        /// /ngynt
        /// </summary>
        private static float Pi = (float) Math.PI;//для удобства
        private static float d_f = Consts.d_f / Consts.f;//d_f и присвоение первоначального значения, позже для обсчета будет вызываться функция My_d_f
//для вывода на форме графиков - коэффициентов
        private static float My_k_f = 0;//для хранения значения коэффициента К, присвоение и чтение происходит из функции k_f

        private static float f //частота в герцах (GraficForm.f в мегагерцах) задается через трекбар или текстбокс в GraficForm
        {
            get
            {
                return Consts.f * (float)Math.Pow(10, 6);
            }
        }

        public static float k_f//функция  чтения и записи коэффициента К
        {
            get
            {
                return My_k_f;
            }
            
            set
            {
                My_k_f = 2F * Pi * value * (float)Math.Pow(10, 6) / (3 * (float)Math.Pow(10, 8));//(float)Math.Pow(10, 6) т.к. в GraficForm частота в мегагерцах, тут в герцах
            }
        }
        //функция для расчета d(f) (вызывается из GraficForm )
        //можно через get и set для этого разкоментить и подбравить
        public static void My_d_f(float d, float f)
        {
            d_f = d / f;
        }
        /* 
        public static float My_d_f
        {
            get
            {
                return d_f;
            }
            set
            {
                d_f = GraficForm.d_f / GraficForm.f; // написать value вместо GraficForm.d_f или GraficForm.f, задаваться будет из GraficForm
            }
        }*/
        /// <summary>
        /// вычисление конструктивных параметров
        /// </summary>
        /// <param name="Q"></param>
        public static void SetCoefficients()
        {
            Consts.L = 3F * (float)Math.Pow(10, 8) / f;
            Consts.S_a_f = Consts.L * Sin(Consts.a) ;
            Consts.A_a_f = Consts.L * Cos(Consts.a) / (2F * Pi);
        }
        /// <summary>
        /// процедура для нахождения точки с координатами х и у в декартовой системе, которая необходима для рисования радиуса заданной длинны на определенном угле в полярной системе координат
        /// название Catets, т.к. процедура работает по принципу катетов в прямоугольном треугольнике
        /// </summary>
        /// <param name="Coal">задать угол радиуса</param>
        /// <param name="Hypotenuse">задать размер радиуса</param>
        /// <param name="Catet1">получить координату х</param>
        /// <param name="Catet2">получить координату у</param>
        public static void Catets(int Coal, int Hypotenuse, out int Catet1, out int Catet2)
        {
            if ((Coal < 90) || (Coal > 270))
            {
                Catet1 = (int)(Hypotenuse * Math.Cos(Coal / 180.0 * Math.PI)); //прилежащий
            }
            else
            {
                Catet1 = (int)(-(Hypotenuse * Math.Cos(Coal / 180.0 * Math.PI)));
            }

            if ((Coal > 90) && (Coal < 270))
            {
                Catet1 = -Catet1;
            }

            if ((Coal > 0) && (Coal < 180))
            {
                Catet2 = (int)(Hypotenuse * Math.Sin(Coal / 180.0 * Math.PI)); //противолежащий
            }
            else
            {
                Catet2 = (int)((Hypotenuse * Math.Sin(Coal / 180.0 * Math.PI)));
            }
        }

        /// <summary>
        /// перевод декартовых координат в полярные для отрисовки в деркатовой системе
        /// </summary>
        /// <param name="P_actual"></param>
        /// <param name="P_next"></param>
        /// <param name="P_actual1"></param>
        /// <param name="P_next1"></param>
        public static void TranslateDecInPol(PointF P_actual, PointF P_next, ref PointF P_actual1, ref PointF P_next1)
        {
            float X;
            float Y;

            X = Cos(P_actual.X) * P_actual.Y;
            Y = Sin(P_actual.X) * P_actual.Y;

            P_actual1.X = -X;
            P_actual1.Y = -Y;

            X = Cos(P_next.X) * P_next.Y;
            Y = Sin(P_next.X) * P_next.Y;

            P_next1.X = -X;
            P_next1.Y = -Y;
        }
        /// <summary>
        /// перевод декартовых координат в полярные для отрисовки в деркатовой системе
        /// </summary>
        /// <param name="pointDec"></param>
        /// <returns></returns>
        public static PointF TranslateDecInPol(PointF pointDec)
        {
            var pointPol = new PointF();

            pointPol.X = -Cos(pointDec.X) * pointDec.Y;
            pointPol.Y = -Sin(pointDec.X) * pointDec.Y;
            return pointPol;
        }

        /// <summary>
        /// Основная функция, которая выбирает, другие функции для просчета значения функции F в точке с аргументом X
        /// </summary>
        /// <param name="Q">передавать значение Х</param>
        /// <param name="TypeFunction">передавать 1, 2 или 3 рисуется 3 разных графика в зависимости от кси E</param>
        /// <param name="NameFunc">передвать "F_4" функция от фи , "F_Q"функция от тетта или "r" поляризационная характеристика</param>
        /// <returns>возвращает значение функции в точке X</returns>
        public static float GeneralFuncForDrawDec(float Q, Enums.TypeFunction TypeFunction, Enums.NameFunc NameFunc)
        {
            float F_4 = General_F_4_WithOutAbs(Q, TypeFunction);
            float F_4_abs = Math.Abs(F_4);
            if (NameFunc == Enums.NameFunc.F_4)
            {
                return -F_4_abs;
            }


            float F_Q_abs = Math.Abs(F_4 * Cos(Q));
            if (NameFunc == Enums.NameFunc.F_Q)
            {
                return -F_Q_abs;
            }

            if (NameFunc == Enums.NameFunc.r)
            {
                return -PolarCharacteristic(Q, TypeFunction);
            }
            return 0;
        }
        /// <summary>
        /// в зависимости от типа спиральных антенн и вида кси (Е_а) выбираются фунции для просчета F от фи, тип спиральных антенн задается в GraficForm имеет значения 1 или 2
        /// </summary>
        /// <param name="x">аргумент функции - (тут)Q</param>
        /// <param name="TypeFunction">тип функции в зависимости от вида кси (Е_а) принимает значение 1, 2 или 3</param>
        /// <returns></returns>
        public static float General_F_4_WithOutAbs(float x, Enums.TypeFunction TypeFunction)
        {
            float F_4 = 0;
            if (Consts.TypeSpiralAntennas == Enums.TypeSpiralAntennas.First)
            {
                switch (TypeFunction)
                {
                    case Enums.TypeFunction.First:
                        F_4 = FuncSingleSpiralAntenna_E_LessOpt(x);
                        break;
                    case Enums.TypeFunction.Second:
                        F_4 = FuncSingleSpiralAntenna_E_Opt(x);
                        break;
                    case Enums.TypeFunction.Third:
                        F_4 = FuncSingleSpiralAntenna_E_Crit(x);
                        break;
                    default:
                        MessageBox.Show("не верное переданное значение TypeFunction в функцию General_F_4_WithOutAbs");
                        break;
                }
            }
            else
                if (Consts.TypeSpiralAntennas == Enums.TypeSpiralAntennas.Second)
                {
                    switch (TypeFunction)
                    {
                        case Enums.TypeFunction.First:
                            F_4 = FuncSystemSpiralAntennas_E_LessOpt(x);
                            break;
                        case Enums.TypeFunction.Second:
                            F_4 = FuncSystemSpiralAntennas_E_Opt(x);
                            break;
                        case Enums.TypeFunction.Third:
                            F_4 = FuncSystemSpiralAntennas_E_Crit(x);
                            break;
                        default:
                            MessageBox.Show("не верное переданное значение TypeFunction в функцию General_F_4_WithOutAbs");
                            break;
                    }
                }
                else
                {
                    //MessageBox.Show("переменная TypeSpiralAntennas имеет не верное значение");
                }
            return F_4;
        }
        // для TypeSpiralAntennas == 1 одиночная спиральная антенна
        public static float FuncSingleSpiralAntenna_E_LessOpt(float Q)
        {
            //коэффициент замеделния
            float E_a1 = E_a(Consts.a, 1);

            float Func_F = 1F / Consts.N * FuncInside(E_a1, Q);

            return Func_F;
        }

        public static float FuncSingleSpiralAntenna_E_Opt(float Q)
        {
            //коэффициент замеделния
            float E_a1 = E_a(Consts.a, 2);

            float Func_F = 1F / K(E_a1, Q) * FuncInside(E_a1, Q);
            return Func_F;
        }

        public static float FuncSingleSpiralAntenna_E_Crit(float Q)
        {
            //коэффициент замеделния
            float E_a1 = E_a(Consts.a, 3);

            float Func_F = FuncInside(E_a1, Q);
            return Func_F;
        }

        // для TypeSpiralAntennas == 1 системы спиральных антенн
        public static float FuncSystemSpiralAntennas_E_LessOpt(float Q)
        {
            //коэффициент замеделния
            float E_a1 = E_a(Consts.a, 1);

            float Func_F = 1F / Consts.N * FuncInside(E_a1, Q) * Fc_QfM_(Q);

            return Func_F;
        }

        public static float FuncSystemSpiralAntennas_E_Opt(float Q)
        {
            //коэффициент замеделния
            float E_a1 = E_a(Consts.a, 2);

            float Func_F = 1F / K(E_a1, Q) * FuncInside(E_a1, Q) * Fc_QfM_(Q);

            return Func_F;
        }

        public static float FuncSystemSpiralAntennas_E_Crit(float Q)
        {
            //коэффициент замеделния
            float E_a1 = E_a(Consts.a, 3);

            float Func_F = FuncInside(E_a1, Q) * Fc_QfM_(Q);
            return Func_F;
        }

        /// <summary>
        /// расчет кси в зависимости от вида фукции 
        /// </summary>
        /// <param name="x">аргумент функции</param>
        /// <param name="TypeE_a">передается 1(если кси меньше оптимального), 2(кси оптимальный) или 3(если кси критический) </param>
        /// <returns></returns>
        private static float E_a(float x, int TypeE_a)
        {
            float E = 0F;
            switch (TypeE_a)
            {                    
                case 1:                    
                    E = Sin(x);
                    break;                    
                case 2:                    
                    E = 1F+1F/(2F* Consts.N)+Sin(x);
                    break;                    
                case 3:                   
                    E = 1 / Consts.N + Sin(x);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;                    
            }
            
            return E;

        }
        /// <summary>
        /// основная часть математической функции
        /// </summary>
        /// <param name="E_a">передаем кси </param>
        /// <param name="Q">аргумент математической функции</param>
        /// <returns></returns>
 
        private static float FuncInside(float E_a, float Q)
        {
            double FuncInsideUp = Math.Sin (Consts.N * Pi * (E_a - Sin(Consts.a) * Cos(Q)));
            double FuncInsideDown = Math.Sin (Pi * (E_a - Sin(Consts.a) * Cos(Q)));            
            for (float Dx = GraficForm.MyDx; FuncInsideDown == 0; Dx++)
            {
                FuncInsideUp = Math.Sin(Consts.N * Pi * (E_a - Sin(Consts.a) * Cos(Q + GraficForm.MyDx)));
                FuncInsideDown = Math.Sin(Pi * (E_a - Sin(Consts.a) * Cos(Q + GraficForm.MyDx)));
                //выход из цикличности, если число проходов больше 100
                if (Dx == GraficForm.MyDx * 100)
                {
                    FuncInsideDown = 0.01F;
                    MessageBox.Show("Возможна ошибка в отрисовке графиков, зациклилась функция FuncInside, число циклов 100");
                }
            }
            
            return (float)(FuncInsideUp/FuncInsideDown);
            
        }
        /// <summary>
        /// расчет коэффициента к для математической функции
        /// </summary>
        /// <param name="E_a">передаем кси</param>
        /// <param name="Q">аргумент</param>
        /// <returns></returns>
        private static float K(float E_a, float Q)
        {
            float K_Up = (float)Math.Sin(Consts.N / 2F * (2F * Pi * Sin(Consts.a) - 2F * Pi * E_a));
            float K_Down = (float)Math.Sin(1F / 2F * (2F * Pi * Sin(Consts.a) - 2F * Pi * E_a));
            return K_Up / K_Down;
        }

        /// <summary>
        /// вторая основная часть функции, испльзуется в расчетах, если TypeSpiralAntennas == 2
        /// </summary>
        /// <param name="Q">аргумент математической функции</param>
        /// <returns></returns>
        private static float Fc_QfM_(float Q)
        {
            float Fc_QfM_Up = (float)Math.Sin(Consts.M * k_f * d_f * Sin(Q) / 2F);
            float Fc_QfM_Down = Consts.M * (float)Math.Sin(k_f * d_f * Sin(Q) / 2F);
            
            for (float Dx = GraficForm.MyDx; Fc_QfM_Down == 0; Dx++)
            {
                Fc_QfM_Up = (float)Math.Sin(Consts.M * k_f * d_f * Sin(Q - Dx) / 2F);
                Fc_QfM_Down = Consts.M * (float)Math.Sin(k_f * d_f * Sin(Q - Dx) / 2F);
                //выход из цикличности, если число проходов больше 100
                if (Dx == GraficForm.MyDx * 100)
                {
                    Fc_QfM_Down = 0.01F;
                    MessageBox.Show("Возможна ошибка в отрисовке графиков, зациклилась функция Fc_QfM_, число циклов 100");
                }
            }  
            

            return Fc_QfM_Up / Fc_QfM_Down;

        }
        /// <summary>
        /// математическая функция для просчета поляризационной характеристики
        /// </summary>
        /// <param name="Fi">аргумент (для этой программы это тетта Q)</param>
        /// <param name="TypeFunction">передаем тип функции зависит от кси и может быть 1, 2 или 3</param>
        /// <returns></returns>
        public static float PolarCharacteristic(float Fi, Enums.TypeFunction TypeFunction)
        {
            float F_4 = General_F_4_WithOutAbs(Consts.Q, TypeFunction);
            float F_4_abs = Math.Abs(F_4);
            float F_Q_abs = Math.Abs(F_4 * Cos(Consts.Q));

            float a = 1;
            if (F_Q_abs == 0)
            {
                F_Q_abs = 0.0001F;
            }
            float m = F_4_abs / F_Q_abs;

            if (m > 1000)
            {
                m = 1000;
            }

            float b = m * a;
            float e;
            if (b * b > 1)
            {
                e = (float)Math.Sqrt(1 - a * a / (b * b));
            }
            else
            {
                e = (float)Math.Sqrt(0.1);
            }

            float r;

            r = b;//для защиты от отрицательного подкоренного выражения

            if ((e * e * Cos(Fi) * Cos(Fi)) <= 1)
            {
                r = b / (float)Math.Sqrt(1 - e * e * Cos(Fi) * Cos(Fi));
            }

            return r;
        }


        /// <summary>
        /// функция Atan2 для обсчета координаты Fi (Угла в полярной системе координат)
        /// </summary>
        /// <param name="y">декартовая координата у </param>
        /// <param name="x">декартовая координата х </param>
        /// <returns></returns>
        public static double Atan2(double y, double x)
        {
            double Atan2 = 0;
            
            if (x > 0 && y>=0) 
            {
                Atan2 = Math.Atan(y / x);
            }

            if (x > 0 && y < 0)
            {
                Atan2 = Math.Atan(y / x) + 2 * Pi;
            }

            if (x < 0)
            {
                Atan2 = Math.Atan(y / x) + Pi;   
            }
            
            if (x == 0) 
            {
                if (y < 0)
                {
                    Atan2 = Pi + Pi / 2;
                }
                else
                {
                    Atan2 = Pi - Pi / 2;
                }
            }
            if (x == 0 && y == 0)
            {
                Atan2 = 0;
            }
            return Atan2 = Atan2 / Math.PI*180;
        }
        /// <summary>
        /// функция Cos возвращающая флот и считающая в градусах, а не радианах
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static float Cos (float x)
        {
            return (float)Math.Cos(x / 180F * Pi);
        }

        /// <summary>
        /// функция Sin возвращающая флот и считающая в градусах, а не радианах
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static float Sin(float x)
        {
            return (float)Math.Sin(x / 180F * Math.PI);
        }
    }
}

