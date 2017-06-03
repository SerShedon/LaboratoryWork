using System;
using System.Collections.Generic;

namespace LaboratoryWork
{
    public class Calculations
    {
        private Consts consts;
        private Dictionary<Enums.NameFunction, Func<float, Enums.TypeFunction, float, float>> generalFunctionsByNameFunc;
        private Dictionary<Enums.TypeSpiralAntennas, Dictionary<Enums.TypeFunction, Func<float, float, float, float>>> functionByTypeSpiralAntennasAndTypeFunction;

        public Calculations(Consts consts)
        {
            this.consts = consts;
            generalFunctionsByNameFunc = new Dictionary<Enums.NameFunction, Func<float, Enums.TypeFunction, float, float>>
            {
                { Enums.NameFunction.F_4, General_F_4},
                { Enums.NameFunction.F_Q, General_F_Q},
                { Enums.NameFunction.r, PolarCharacteristic},
            };

            functionByTypeSpiralAntennasAndTypeFunction = new Dictionary<Enums.TypeSpiralAntennas, Dictionary<Enums.TypeFunction, Func<float, float, float, float>>>
            {
                {
                    Enums.TypeSpiralAntennas.Single,
                    new Dictionary<Enums.TypeFunction, Func<float, float, float, float>>
                    {
                        { Enums.TypeFunction.E_LessOpt, FuncSingleSpiralAntenna_E_LessOpt },
                        { Enums.TypeFunction.E_Opt, FuncSingleSpiralAntenna_E_Opt },
                        { Enums.TypeFunction.E_Crit, FuncSingleSpiralAntenna_E_Crit },
                    }
                },
                {
                    Enums.TypeSpiralAntennas.System,
                    new Dictionary<Enums.TypeFunction, Func<float, float, float, float>>
                    {
                        { Enums.TypeFunction.E_LessOpt, FuncSystemSpiralAntennas_E_LessOpt },
                        { Enums.TypeFunction.E_Opt, FuncSystemSpiralAntennas_E_Opt },
                        { Enums.TypeFunction.E_Crit, FuncSystemSpiralAntennas_E_Crit },
                    }
                }
            };
        }

        /// <summary>
        /// перевод частоты f из мегагерц в герцы
        /// </summary>
        private float pi = (float) Math.PI;//для удобства
        /// <summary>
        /// d_f и присвоение первоначального значения, позже для обсчета будет вызываться функция My_d_f
        /// </summary>
        private float d_f;
        /// <summary>
        /// для хранения значения коэффициента К, присвоение и чтение происходит из функции k_f
        /// </summary>
        private float my_k_f = 0;
        /// <summary>
        /// частота в герцах (GraficForm.f в мегагерцах) задается через трекбар или текстбокс в GraficForm
        /// </summary>
        private float f
        {
            get  { return consts.F * (float)Math.Pow(10, 6); }
        }
        /// <summary>
        /// коэффициент К
        /// </summary>
        public float k_f
        {
            get { return my_k_f;}            
            set
            {
                my_k_f = 2F * pi * value / (3 * (float)Math.Pow(10, 2));//(float)Math.Pow(10, 6) т.к. в GraficForm частота в мегагерцах, тут в герцах
            }
        }

        public int CountCycliesBeforeThrowError = 100;

        public void My_d_f(float d, float f)
        {
            d_f = d / f;
        }

        //ToDo переписать через события, а так же начать использовать
        /// <summary>
        /// вычисление конструктивных параметров
        /// </summary>
        /// <param name="Q"></param>
        public void SetCoefficients()
        {
            consts.L = 3F * (float)Math.Pow(10, 8) / f;
            consts.S_a_f = consts.L * SinInRadians(consts.A) ;
            consts.A_a_f = consts.L * CosInRadians(consts.A) / (2F * pi);
        }

        /// <summary>
        /// Основная функция, которая выбирает, другие функции для просчета значения функции F в точке с аргументом X
        /// </summary>
        /// <param name="q">передавать значение Х</param>
        /// <param name="typeFunction">передавать 1, 2 или 3 рисуется 3 разных графика в зависимости от кси E</param>
        /// <param name="nameFunc">передвать "F_4" функция от фи , "F_Q"функция от тетта или "r" поляризационная характеристика</param>
        /// <returns>возвращает значение функции в точке X</returns>
        public float GeneralFuncForDrawDec(float q, Enums.TypeFunction typeFunction, Enums.NameFunction nameFunc, float dx)
            => generalFunctionsByNameFunc[nameFunc](q, typeFunction, dx);

        /// <summary>
        /// в зависимости от типа спиральных антенн и вида кси (Е_а) выбираются фунции для просчета F от фи, тип спиральных антенн задается в GraficForm имеет значения 1 или 2
        /// </summary>
        /// <param name="x">аргумент функции - (тут)Q</param>
        /// <param name="typeFunction">тип функции в зависимости от вида кси (Е_а) принимает значение 1, 2 или 3</param>
        /// <returns></returns>
        public float General_F_4_WithOutAbs(float x, Enums.TypeFunction typeFunction, float dx)
            => functionByTypeSpiralAntennasAndTypeFunction[consts.TypeSpiralAntennas][typeFunction](E_a(consts.A, typeFunction), x, dx);

        public float General_F_4(float x, Enums.TypeFunction typeFunction, float dx)
            => Math.Abs(General_F_4_WithOutAbs(x, typeFunction, dx));

        public float General_F_Q(float x, Enums.TypeFunction typeFunction, float dx)
            => Math.Abs(General_F_4_WithOutAbs(x, typeFunction, dx) * CosInRadians(x));
        
        public float FuncSingleSpiralAntenna_E_LessOpt(float e_a1, float q, float dx)
            => 1F / consts.N * FuncInside(e_a1, q, dx);

        public float FuncSingleSpiralAntenna_E_Opt(float e_a1, float Q, float dx)
            => 1F / K(e_a1, Q) * FuncInside(e_a1, Q, dx);

        public float FuncSingleSpiralAntenna_E_Crit(float E_a1, float Q, float dx)
            => FuncInside(E_a1, Q, dx);
                
        public float FuncSystemSpiralAntennas_E_LessOpt(float E_a1, float Q, float dx)
            => 1F / consts.N * FuncInside(E_a1, Q, dx) * Fc_QfM(Q, dx);

        public float FuncSystemSpiralAntennas_E_Opt(float E_a1, float Q, float dx)
            => 1F / K(E_a1, Q) * FuncInside(E_a1, Q, dx) * Fc_QfM(Q, dx);

        public float FuncSystemSpiralAntennas_E_Crit(float E_a1, float Q, float dx)
            => FuncInside(E_a1, Q, dx) * Fc_QfM(Q, dx);

        /// <summary>
        /// расчет кси в зависимости от вида фукции 
        /// </summary>
        /// <param name="x">аргумент функции</param>
        /// <param name="TypeE_a">передается 1(если кси меньше оптимального), 2(кси оптимальный) или 3(если кси критический) </param>
        /// <returns></returns>
        private float E_a(float x, Enums.TypeFunction TypeE_a)
        {
            switch (TypeE_a)
            {
                case Enums.TypeFunction.E_LessOpt:
                    return SinInRadians(x);
                case Enums.TypeFunction.E_Opt:
                    return 1F + 1F / (2F * consts.N) + SinInRadians(x);
                case Enums.TypeFunction.E_Crit:
                    return 1f / consts.N + SinInRadians(x);
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

        private float FuncInside(float e_a, float q, float dx)
        {
            Func<float, float, double> getNumerator = (x, _dx) => Math.Sin(consts.N * (pi * (e_a - SinInRadians(consts.A) * CosInRadians(x + _dx))));
            Func<float, float, double> getDenominator = (x, _dx) => Math.Sin(pi * (e_a - SinInRadians(consts.A) * CosInRadians(x + _dx)));
            return CalculateFraction(getNumerator, getDenominator, q, dx);
        }
        /// <summary>
        /// расчет коэффициента к для математической функции
        /// </summary>
        /// <param name="e_a">передаем кси</param>
        /// <param name="q">аргумент</param>
        /// <returns></returns>
        private float K(float e_a, float q)
        {
            float k_Up = (float)Math.Sin(consts.N / 2F * (2F * pi * SinInRadians(consts.A) - 2F * pi * e_a));
            float k_Down = (float)Math.Sin(1F / 2F * (2F * pi * SinInRadians(consts.A) - 2F * pi * e_a));
            return k_Up / k_Down;
        }

        /// <summary>
        /// вторая основная часть функции, испльзуется в расчетах, если TypeSpiralAntennas == 2
        /// </summary>
        /// <param name="q">аргумент математической функции</param>
        /// <returns></returns>
        private float Fc_QfM(float q, float Dx)
        {
            Func<float, float, double> getNumerator = (x, dx) => Math.Sin(consts.M * k_f * d_f * SinInRadians(x + dx) / 2F);
            Func<float, float, double> getDenominator = (x, dx) => consts.M * Math.Sin(k_f * d_f * SinInRadians(x + dx) / 2F);
            return CalculateFraction(getNumerator, getDenominator, q, Dx);
        }

        private float CalculateFraction(Func<float, float, double> getNumerator, Func<float, float, double> getDenominator, float argument, float dx)
        {
            var newDx = 0f;
            var denominator = 0.0;
            //can`t be divided by zero
            for (int i = 0; denominator == 0; i++)
            {
                newDx = i * dx;
                denominator = getDenominator(argument, newDx);

                if (newDx == dx * CountCycliesBeforeThrowError)
                    throw (new Exception(String.Format("Зациклилась функция {0}, количество циклов = {1}", nameof(Fc_QfM), CountCycliesBeforeThrowError)));
            }
            var numerator = getNumerator(argument, newDx);
            return (float)(numerator / denominator);
        }

        /// <summary>
        /// математическая функция для просчета поляризационной характеристики
        /// </summary>
        /// <param name="fi">аргумент (для этой программы это тетта Q)</param>
        /// <param name="typeFunction">передаем тип функции зависит от кси и может быть 1, 2 или 3</param>
        /// <returns></returns>
        public float PolarCharacteristic(float fi, Enums.TypeFunction typeFunction, float dx)
        {
            float f_4 = General_F_4_WithOutAbs(consts.Q, typeFunction, dx);
            float f_4_abs = Math.Abs(f_4);
            float f_Q_abs = Math.Abs(f_4 * CosInRadians(consts.Q));
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
        private float CosInRadians (float x) => (float)Math.Cos(x / 180F * Math.PI);

        /// <summary>
        /// функция Sin возвращающая флот и считающая в градусах, а не радианах
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private float SinInRadians(float x) => (float)Math.Sin(x / 180F * Math.PI);
    }
}

