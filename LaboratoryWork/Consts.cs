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
                Delegates.OnChangeConsts?.Invoke();
            }
        }        

        public static float N
        {
            get { return _N; }
            set
            {
                _N = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public static float a
        {
            get { return _a; }
            set
            {
                _a = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public static float M
        {
            get { return _M; }
            set
            {
                _M = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public static float d_f
        {
            get { return _d_f; }
            set
            {
                _d_f = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public static float Q
        {
            get { return _Q; }
            set
            {
                _Q = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public static Enums.TypeSpiralAntennas TypeSpiralAntennas
        {
            get { return _TypeSpiralAntennas; }
            set
            {
                _TypeSpiralAntennas = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }

        public static Enums.TypeFunction TypeFunctions
        {
            get { return _TypeFunctions; }
            set
            {
                _TypeFunctions = value;
                Delegates.OnChangeConsts?.Invoke();
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
