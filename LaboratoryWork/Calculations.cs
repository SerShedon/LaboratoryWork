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
            Consts.S_a_f = Consts.L * SinInRadians(Consts.a) ;
            Consts.A_a_f = Consts.L * CosInRadians(Consts.a) / (2F * Pi);
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

            X = CosInRadians(P_actual.X) * P_actual.Y;
            Y = SinInRadians(P_actual.X) * P_actual.Y;

            P_actual1.X = -X;
            P_actual1.Y = -Y;

            X = CosInRadians(P_next.X) * P_next.Y;
            Y = SinInRadians(P_next.X) * P_next.Y;

            P_next1.X = -X;
            P_next1.Y = -Y;
        }

        private static Dictionary<Enums.NameFunction, Func<float, Enums.TypeFunction, float>> generalFunctionsByNameFunc =
            new Dictionary<Enums.NameFunction, Func<float, Enums.TypeFunction, float>>
            {
                { Enums.NameFunction.F_4, General_F_4},
                { Enums.NameFunction.F_Q, General_F_Q},
                { Enums.NameFunction.r, PolarCharacteristic},
            };

        private static Dictionary<Enums.TypeSpiralAntennas, Dictionary<Enums.TypeFunction, Func<float, float, float>>> functionByTypeSpiralAntennasAndTypeFunction =
            new Dictionary<Enums.TypeSpiralAntennas, Dictionary<Enums.TypeFunction, Func<float, float, float>>>
            {
                {
                    Enums.TypeSpiralAntennas.Single,
                    new Dictionary<Enums.TypeFunction, Func<float, float, float>>
                    {
                        { Enums.TypeFunction.E_LessOpt, FuncSingleSpiralAntenna_E_LessOpt },
                        { Enums.TypeFunction.E_Opt, FuncSingleSpiralAntenna_E_Opt },
                        { Enums.TypeFunction.E_Crit, FuncSingleSpiralAntenna_E_Crit },
                    }
                },
                {
                    Enums.TypeSpiralAntennas.System,
                    new Dictionary<Enums.TypeFunction, Func<float, float, float>>
                    {
                        { Enums.TypeFunction.E_LessOpt, FuncSystemSpiralAntennas_E_LessOpt },
                        { Enums.TypeFunction.E_Opt, FuncSystemSpiralAntennas_E_Opt },
                        { Enums.TypeFunction.E_Crit, FuncSystemSpiralAntennas_E_Crit },
                    }
                }
            };

        /// <summary>
        /// Основная функция, которая выбирает, другие функции для просчета значения функции F в точке с аргументом X
        /// </summary>
        /// <param name="Q">передавать значение Х</param>
        /// <param name="typeFunction">передавать 1, 2 или 3 рисуется 3 разных графика в зависимости от кси E</param>
        /// <param name="NameFunc">передвать "F_4" функция от фи , "F_Q"функция от тетта или "r" поляризационная характеристика</param>
        /// <returns>возвращает значение функции в точке X</returns>
        public static float GeneralFuncForDrawDec(float Q, Enums.TypeFunction typeFunction, Enums.NameFunction NameFunc)
            => generalFunctionsByNameFunc[NameFunc](Q, typeFunction);

        /// <summary>
        /// в зависимости от типа спиральных антенн и вида кси (Е_а) выбираются фунции для просчета F от фи, тип спиральных антенн задается в GraficForm имеет значения 1 или 2
        /// </summary>
        /// <param name="x">аргумент функции - (тут)Q</param>
        /// <param name="typeFunction">тип функции в зависимости от вида кси (Е_а) принимает значение 1, 2 или 3</param>
        /// <returns></returns>
        public static float General_F_4_WithOutAbs(float x, Enums.TypeFunction typeFunction) 
            => functionByTypeSpiralAntennasAndTypeFunction[Consts.TypeSpiralAntennas][typeFunction](E_a(Consts.a, typeFunction), x);

        public static float General_F_4(float x, Enums.TypeFunction typeFunction)
            => Math.Abs(General_F_4_WithOutAbs(x, typeFunction));

        public static float General_F_Q(float x, Enums.TypeFunction typeFunction)
            => Math.Abs(General_F_4_WithOutAbs(x, typeFunction) * CosInRadians(x));

        // для TypeSpiralAntennas == 1 одиночная спиральная антенна
        public static float FuncSingleSpiralAntenna_E_LessOpt(float E_a1, float Q)
            => 1F / Consts.N * FuncInside(E_a1, Q);

        public static float FuncSingleSpiralAntenna_E_Opt(float E_a1, float Q)
            => 1F / K(E_a1, Q) * FuncInside(E_a1, Q);

        public static float FuncSingleSpiralAntenna_E_Crit(float E_a1, float Q)
            => FuncInside(E_a1, Q);

        // для TypeSpiralAntennas == 1 системы спиральных антенн
        public static float FuncSystemSpiralAntennas_E_LessOpt(float E_a1, float Q)
            => 1F / Consts.N * FuncInside(E_a1, Q) * Fc_QfM(Q);

        public static float FuncSystemSpiralAntennas_E_Opt(float E_a1, float Q)
            => 1F / K(E_a1, Q) * FuncInside(E_a1, Q) * Fc_QfM(Q);

        public static float FuncSystemSpiralAntennas_E_Crit(float E_a1, float Q)
            => FuncInside(E_a1, Q) * Fc_QfM(Q);

        /// <summary>
        /// расчет кси в зависимости от вида фукции 
        /// </summary>
        /// <param name="x">аргумент функции</param>
        /// <param name="TypeE_a">передается 1(если кси меньше оптимального), 2(кси оптимальный) или 3(если кси критический) </param>
        /// <returns></returns>
        private static float E_a(float x, Enums.TypeFunction TypeE_a)
        {
            switch (TypeE_a)
            {
                case Enums.TypeFunction.E_LessOpt:
                    return SinInRadians(x);
                case Enums.TypeFunction.E_Opt:
                    return 1F + 1F / (2F * Consts.N) + SinInRadians(x);
                case Enums.TypeFunction.E_Crit:
                    return 1f / Consts.N + SinInRadians(x);
                default:
                    throw new ArgumentException();
            }
        }
        /// <summary>
        /// основная часть математической функции
        /// </summary>
        /// <param name="E_a">передаем кси </param>
        /// <param name="Q">аргумент математической функции</param>
        /// <returns></returns>

        private static float FuncInside(float E_a, float Q)
        {
            double FuncInsideUp = Math.Sin (Consts.N * Pi * (E_a - SinInRadians(Consts.a) * CosInRadians(Q)));
            double FuncInsideDown = Math.Sin (Pi * (E_a - SinInRadians(Consts.a) * CosInRadians(Q)));            
            for (float Dx = ParametrsForm.MyDx; FuncInsideDown == 0; Dx++)
            {
                FuncInsideUp = Math.Sin(Consts.N * Pi * (E_a - SinInRadians(Consts.a) * CosInRadians(Q + ParametrsForm.MyDx)));
                FuncInsideDown = Math.Sin(Pi * (E_a - SinInRadians(Consts.a) * CosInRadians(Q + ParametrsForm.MyDx)));
                //выход из цикличности, если число проходов больше 100
                if (Dx == ParametrsForm.MyDx * 100)
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
            float K_Up = (float)Math.Sin(Consts.N / 2F * (2F * Pi * SinInRadians(Consts.a) - 2F * Pi * E_a));
            float K_Down = (float)Math.Sin(1F / 2F * (2F * Pi * SinInRadians(Consts.a) - 2F * Pi * E_a));
            return K_Up / K_Down;
        }

        /// <summary>
        /// вторая основная часть функции, испльзуется в расчетах, если TypeSpiralAntennas == 2
        /// </summary>
        /// <param name="Q">аргумент математической функции</param>
        /// <returns></returns>
        private static float Fc_QfM(float Q)
        {
            float Fc_QfM_Up = (float)Math.Sin(Consts.M * k_f * d_f * SinInRadians(Q) / 2F);
            float Fc_QfM_Down = Consts.M * (float)Math.Sin(k_f * d_f * SinInRadians(Q) / 2F);
            
            for (float Dx = ParametrsForm.MyDx; Fc_QfM_Down == 0; Dx++)
            {
                Fc_QfM_Up = (float)Math.Sin(Consts.M * k_f * d_f * SinInRadians(Q - Dx) / 2F);
                Fc_QfM_Down = Consts.M * (float)Math.Sin(k_f * d_f * SinInRadians(Q - Dx) / 2F);
                //выход из цикличности, если число проходов больше 100
                if (Dx == ParametrsForm.MyDx * 100)
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
            float F_Q_abs = Math.Abs(F_4 * CosInRadians(Consts.Q));

            float a = 1;
            if (F_Q_abs == 0)
                F_Q_abs = 0.0001F;
            float m = F_4_abs / F_Q_abs;

            if (m > 1000)
                m = 1000;

            float b = m * a;
            float e = b * b > 1 ? (float)Math.Sqrt(1 - a * a / (b * b)) : (float)Math.Sqrt(0.1);            

            float r = e * e * CosInRadians(Fi) * CosInRadians(Fi) <= 1 //для защиты от отрицательного подкоренного выражения
                ? b / (float)Math.Sqrt(1 - e * e * CosInRadians(Fi) * CosInRadians(Fi))
                : b;

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
                Atan2 = Math.Atan(y / x);

            if (x > 0 && y < 0)
                Atan2 = Math.Atan(y / x) + 2 * Pi;

            if (x < 0)
                Atan2 = Math.Atan(y / x) + Pi;
            
            if (x == 0) 
            {
                if (y < 0)
                    Atan2 = Pi + Pi / 2;
                else
                    Atan2 = Pi - Pi / 2;
            }

            if (x == 0 && y == 0)
                Atan2 = 0;
            return Atan2 / Math.PI*180;
        }
        /// <summary>
        /// функция Cos возвращающая флот и считающая в градусах, а не радианах
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static float CosInRadians (float x)
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
    }
}

