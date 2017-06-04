namespace LaboratoryWork
{
    public class Consts
    {
        private float _f;
        private float _N;
        private float _a;
        private float _M;
        private float _d_f;
        private float _Q;
        private Enums.TypeSpiralAntennas _TypeSpiralAntennas;
        private Enums.TypeFunction _TypeFunctions;
        private float _L;
        private float _S_a_f;
        private float _A_a_f;

        public float F
        {
            get { return _f; }
            set
            {
                _f = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }        

        public float N
        {
            get { return _N; }
            set
            {
                _N = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public float A
        {
            get { return _a; }
            set
            {
                _a = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public float M
        {
            get { return _M; }
            set
            {
                _M = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public float D_f
        {
            get { return _d_f; }
            set
            {
                _d_f = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public float Q
        {
            get { return _Q; }
            set
            {
                _Q = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
        public Enums.TypeSpiralAntennas TypeSpiralAntennas
        {
            get { return _TypeSpiralAntennas; }
            set
            {
                _TypeSpiralAntennas = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }

        public Enums.TypeFunction TypeFunctions
        {
            get { return _TypeFunctions; }
            set
            {
                _TypeFunctions = value;
                Delegates.OnChangeConsts?.Invoke();
            }
        }
    }
}
