using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaboratoryWork
{
    class Consts
    {
        private static float _f;
        private static float _N;
        private static float _a;
        private static float _M;
        private static float _d_f;
        private static float _Q;
        private static Enums.TypeSpiralAntennas _TypeSpiralAntennas;
        private static Enums.TypeFunction _TypeFunctions;
        private static float _L;
        private static float _S_a_f;
        private static float _A_a_f;

        public static float f
        {
            get { return _f; }
            set
            {
                _f = value;
                Calculations.SetCoefficients();

                if (Delegates.OnChangeConsts != null)
                    Delegates.OnChangeConsts();
            }
        }        

        public static float N
        {
            get { return _N; }
            set
            {
                _N = value;
                if (Delegates.OnChangeConsts != null)
                    Delegates.OnChangeConsts();
            }
        }
        public static float a
        {
            get { return _a; }
            set
            {
                _a = value;
                if (Delegates.OnChangeConsts != null)
                    Delegates.OnChangeConsts();
            }
        }
        public static float M
        {
            get { return _M; }
            set
            {
                _M = value;
                if (Delegates.OnChangeConsts != null)
                    Delegates.OnChangeConsts();
            }
        }
        public static float d_f
        {
            get { return _d_f; }
            set
            {
                _d_f = value;
                if (Delegates.OnChangeConsts != null)
                    Delegates.OnChangeConsts();
            }
        }
        public static float Q
        {
            get { return _Q; }
            set
            {
                _Q = value;
                if (Delegates.OnChangeConsts != null)
                    Delegates.OnChangeConsts();
            }
        }
        public static Enums.TypeSpiralAntennas TypeSpiralAntennas
        {
            get { return _TypeSpiralAntennas; }
            set
            {
                _TypeSpiralAntennas = value;
                if (Delegates.OnChangeConsts != null)
                    Delegates.OnChangeConsts();
            }
        }

        public static Enums.TypeFunction TypeFunctions
        {
            get { return _TypeFunctions; }
            set
            {
                _TypeFunctions = value;
                if (Delegates.OnChangeConsts != null)
                    Delegates.OnChangeConsts();
            }
        }

        public static float L
        {
            get { return _L; }
            set
            {
                _L = value;
            }
        }

        public static float S_a_f
        {
            get { return _S_a_f; }
            set
            {
                _S_a_f = value;
            }
        }

        public static float A_a_f
        {
            get { return _A_a_f; }
            set
            {
                _A_a_f = value;
            }
        }


    }
}
