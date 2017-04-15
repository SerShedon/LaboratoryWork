using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaboratoryWork
{
    class Calculations
    {
        /// <summary>
        /// перевод частоты f из мегагерц в герцы
        /// </summary>
        private static float pi = (float) Math.PI;//для удобства
        /// <summary>
        /// d_f и присвоение первоначального значения, позже для обсчета будет вызываться функция My_d_f
        /// </summary>
        private static float d_f = Consts.D_f / Consts.F;        
        /// <summary>
        /// для хранения значения коэффициента К, присвоение и чтение происходит из функции k_f
        /// </summary>
        private static float my_k_f = 0;
        /// <summary>
        /// частота в герцах (GraficForm.f в мегагерцах) задается через трекбар или текстбокс в GraficForm
        /// </summary>
        private static float f
        {
            get  { return Consts.F * (float)Math.Pow(10, 6); }
        }
        /// <summary>
        /// коэффициент К
        /// </summary>
        public static float k_f
        {
            get { return my_k_f;}            
            set
            {
                my_k_f = 2F * pi * value / (3 * (float)Math.Pow(10, 2));//(float)Math.Pow(10, 6) т.к. в GraficForm частота в мегагерцах, тут в герцах
            }
        }
        public static void My_d_f(float d, float f) => d_f = d / f;

        /// <summary>
        /// вычисление конструктивных параметров
        /// </summary>
        /// <param name="Q"></param>
        public static void SetCoefficients()
        {
            Consts.L = 3F * (float)Math.Pow(10, 8) / f;
            Consts.S_a_f = Consts.L * SinInRadians(Consts.A) ;
            Consts.A_a_f = Consts.L * CosInRadians(Consts.A) / (2F * pi);
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
        /// <param name="q">передавать значение Х</param>
        /// <param name="typeFunction">передавать 1, 2 или 3 рисуется 3 разных графика в зависимости от кси E</param>
        /// <param name="nameFunc">передвать "F_4" функция от фи , "F_Q"функция от тетта или "r" поляризационная характеристика</param>
        /// <returns>возвращает значение функции в точке X</returns>
        public static float GeneralFuncForDrawDec(float q, Enums.TypeFunction typeFunction, Enums.NameFunction nameFunc)
            => generalFunctionsByNameFunc[nameFunc](q, typeFunction);

        /// <summary>
        /// в зависимости от типа спиральных антенн и вида кси (Е_а) выбираются фунции для просчета F от фи, тип спиральных антенн задается в GraficForm имеет значения 1 или 2
        /// </summary>
        /// <param name="x">аргумент функции - (тут)Q</param>
        /// <param name="typeFunction">тип функции в зависимости от вида кси (Е_а) принимает значение 1, 2 или 3</param>
        /// <returns></returns>
        public static float General_F_4_WithOutAbs(float x, Enums.TypeFunction typeFunction) 
            => functionByTypeSpiralAntennasAndTypeFunction[Consts.TypeSpiralAntennas][typeFunction](E_a(Consts.A, typeFunction), x);

        public static float General_F_4(float x, Enums.TypeFunction typeFunction)
            => Math.Abs(General_F_4_WithOutAbs(x, typeFunction));

        public static float General_F_Q(float x, Enums.TypeFunction typeFunction)
            => Math.Abs(General_F_4_WithOutAbs(x, typeFunction) * CosInRadians(x));
        
        public static float FuncSingleSpiralAntenna_E_LessOpt(float e_a1, float q)
            => 1F / Consts.N * FuncInside(e_a1, q);

        public static float FuncSingleSpiralAntenna_E_Opt(float e_a1, float Q)
            => 1F / K(e_a1, Q) * FuncInside(e_a1, Q);

        public static float FuncSingleSpiralAntenna_E_Crit(float E_a1, float Q)
            => FuncInside(E_a1, Q);
                
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
        //ToDo переписать: правильно обрабатывать ситуацию, когда знаменатель равен нулю
        /// <summary>
        /// основная часть математической функции
        /// </summary>
        /// <param name="e_a">передаем кси </param>
        /// <param name="q">аргумент математической функции</param>
        /// <returns></returns>

        private static float FuncInside(float e_a, float q)
        {
            var argumentSinForDenominator = pi * (e_a - SinInRadians(Consts.A) * CosInRadians(q));
            double funcInsideUp = Math.Sin (Consts.N * argumentSinForDenominator);
            double funcInsideDown = Math.Sin (argumentSinForDenominator);
            for (float Dx = ParametrsForm.Dx; funcInsideDown == 0; Dx++)
            {
                funcInsideUp = Math.Sin(Consts.N * pi * (e_a - SinInRadians(Consts.A) * CosInRadians(q + ParametrsForm.Dx)));
                funcInsideDown = Math.Sin(pi * (e_a - SinInRadians(Consts.A) * CosInRadians(q + ParametrsForm.Dx)));
                //выход из цикличности, если число проходов больше 100
                if (Dx == ParametrsForm.Dx * 100)
                {
                    funcInsideDown = 0.01F;
                    MessageBox.Show("Возможна ошибка в отрисовке графиков, зациклилась функция FuncInside, число циклов 100");
                }
            }
            
            return (float)(funcInsideUp/funcInsideDown);
            
        }
        /// <summary>
        /// расчет коэффициента к для математической функции
        /// </summary>
        /// <param name="e_a">передаем кси</param>
        /// <param name="q">аргумент</param>
        /// <returns></returns>
        private static float K(float e_a, float q)
        {
            float k_Up = (float)Math.Sin(Consts.N / 2F * (2F * pi * SinInRadians(Consts.A) - 2F * pi * e_a));
            float k_Down = (float)Math.Sin(1F / 2F * (2F * pi * SinInRadians(Consts.A) - 2F * pi * e_a));
            return k_Up / k_Down;
        }

        /// <summary>
        /// вторая основная часть функции, испльзуется в расчетах, если TypeSpiralAntennas == 2
        /// </summary>
        /// <param name="q">аргумент математической функции</param>
        /// <returns></returns>
        private static float Fc_QfM(float q)
        {
            float fc_QfM_Up = (float)Math.Sin(Consts.M * k_f * d_f * SinInRadians(q) / 2F);
            float fc_QfM_Down = Consts.M * (float)Math.Sin(k_f * d_f * SinInRadians(q) / 2F);
            
            for (float dx = ParametrsForm.Dx; fc_QfM_Down == 0; dx++)
            {
                fc_QfM_Up = (float)Math.Sin(Consts.M * k_f * d_f * SinInRadians(q - dx) / 2F);
                fc_QfM_Down = Consts.M * (float)Math.Sin(k_f * d_f * SinInRadians(q - dx) / 2F);
                //выход из цикличности, если число проходов больше 100
                if (dx == ParametrsForm.Dx * 100)
                {
                    fc_QfM_Down = 0.01F;
                    MessageBox.Show("Возможна ошибка в отрисовке графиков, зациклилась функция Fc_QfM_, число циклов 100");
                }
            }  
            return fc_QfM_Up / fc_QfM_Down;
        }
        /// <summary>
        /// математическая функция для просчета поляризационной характеристики
        /// </summary>
        /// <param name="fi">аргумент (для этой программы это тетта Q)</param>
        /// <param name="typeFunction">передаем тип функции зависит от кси и может быть 1, 2 или 3</param>
        /// <returns></returns>
        public static float PolarCharacteristic(float fi, Enums.TypeFunction typeFunction)
        {
            float f_4 = General_F_4_WithOutAbs(Consts.Q, typeFunction);
            float f_4_abs = Math.Abs(f_4);
            float f_Q_abs = Math.Abs(f_4 * CosInRadians(Consts.Q));
            float a = 1;
            if (f_Q_abs == 0)
                f_Q_abs = 0.0001F;
            float m = f_4_abs / f_Q_abs;

            if (m > 1000)
                m = 1000;

            float b = m * a;
            float e = b * b > 1 ? (float)Math.Sqrt(1 - a * a / (b * b)) : (float)Math.Sqrt(0.1);
            var radicand = 1 - e * e * CosInRadians(fi) * CosInRadians(fi);
            float r = radicand < 0 //для защиты от отрицательного подкоренного выражения
                ? b / (float)Math.Sqrt(radicand)
                : b;
            return r;
        }
        
        /// <summary>
        /// функция Cos возвращающая флот и считающая в градусах, а не радианах
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static float CosInRadians (float x) => (float)Math.Cos(x / 180F * Math.PI);

        /// <summary>
        /// функция Sin возвращающая флот и считающая в градусах, а не радианах
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static float SinInRadians(float x) => (float)Math.Sin(x / 180F * Math.PI);
    }
}

