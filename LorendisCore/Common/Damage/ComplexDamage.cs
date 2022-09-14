using System;

namespace LorendisCore.Common.Damage
{
    public class ComplexDamage : Damage
    {
        private const int TIME_LIMIT = 120; // The number of seconds that can be passed before damage is dealt.

        /// <summary> Array of coefficients, starting from the constant at index 0. Index corresponds to exponent. </summary>
        public float[] Coefficients { get; }


        /// <summary>
        ///   Deal the total damage over time, with damage determined by a polynomial with the factors given. 
        ///   The index of the factor is the exponent of x it is applied to.
        ///   For example: `damage = factors[0] + factors[1]*x + factors[2]*x^2`.
        ///   Only positive amounts of damage will be dealt; negatives in the equation act as delays.
        /// </summary>
        public ComplexDamage(DamageType type, int total, float[] coefficients)
            : base(type, total, 1)
        {
            Coefficients = coefficients;
            _duration = TimeToFinish();
            SnubUnresolvableDamage();
        }
        /// <summary>
        ///   Deal damage over a set amount of time, determined by a polynomial with the coefficients given.
        ///   The index of the coefficient is the exponent of the variable it is applied to.
        ///   For example: `damage = factors[0] + factors[1]*x + factors[2]*x^2`.
        ///   Total damage will be determined by the duration and the equation.
        ///   Only positive amounts of damage will be dealt; negatives in the equation act as delays.
        /// </summary>
        public ComplexDamage(DamageType type, float[] factors, int duration)
            : base(type, 1, duration)
        {
            Coefficients = factors;
            _total = Fx(_duration);
            _remaining = _total;
            SnubUnresolvableDamage();
        }
        private void SnubUnresolvableDamage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   The current change per second as of t=time.
        ///   Will not deal negative damage. 
        ///   Thus, valleys in the curve will be treated as delays.
        /// </summary>
        protected override int CalculateTick()
        {
            if (_time == 0)
                return NonNegative(GetConstant());
            return Fx(_time) - Fx(_time - 1);
        }

        #region Strings
        public override string ToDataString()
        {
            throw new NotImplementedException();
        }

        protected override string ChildInfoString()
        {
            throw new NotImplementedException();
        }

        private string PolynomialString()
        {
            if (Coefficients.Length == 0) return "0";

            string s = GetConstant().ToString();
            for (int i = 1; i < Coefficients.Length; i++)
            {
                float factor = GetCoefficient(i);
                if (factor == 0)
                    continue;

                var op = factor >= 0 ? "+" : string.Empty;
                s = $"{s}{op}{factor}x^{i}";
            }
            return s;
        }
        #endregion

        #region Utility
        /// <summary>
        ///   The total damage dealt as of t=time. Negative damage becomes 0.
        /// </summary>
        private int Fx(int x)
        {
            int y = 0;
            for (int exponent = 0; exponent < Coefficients.Length; exponent++)
                y += CalculateTerm(x, exponent);
            return NonNegative(y);
        }

        private int CalculateTerm(int x, int exponent)
        {
            int y = (int)Math.Pow(x, exponent);
            return Ceiling(GetCoefficient(exponent) * y);
        }

        private bool IsUnresolvable()
        {
            return Coefficients == null
              || Coefficients.Length == 0
              || Fx(TIME_LIMIT) > 0;
        }
        private int TimeToFinish(int start = 0)
        {
            int damageAtT = 0;
            int t;
            for (t = start; damageAtT < _remaining; t++)
                damageAtT = Fx(t);
            return t;
        }

        private float GetCoefficient(int exponent)
        {
            if (exponent >= Coefficients.Length)
                return 0;
            return Coefficients[exponent];
        }
        private int GetConstant()
        {
            return Coefficients.Length > 0
              ? Ceiling(GetCoefficient(0))
              : 0;
        }

        private int Ceiling(double value)
        {
            return (int)Math.Ceiling(value);
        }

        private int NonNegative(int value)
        {
            return Math.Max(0, value);
        }
        #endregion
    }
}
