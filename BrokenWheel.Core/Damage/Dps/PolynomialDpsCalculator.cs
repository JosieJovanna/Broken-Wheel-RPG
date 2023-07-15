using System.Globalization;
using System.Text;
using BrokenWheel.Math;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.Core.Damage.Dps
{
    internal class PolynomialDpsCalculator : DpsCalculator
    {
        private readonly Polynomial _func;
        private double _overflow;

        /// <summary>
        /// Deal damage over a set amount of time, determined by a polynomial with the coefficients given.
        /// The index of the coefficient is the exponent of the variable it is applied to.
        /// For example: `damage = factors[0] + factors[1]*x + factors[2]*x^2`.
        /// Total damage will be determined by the duration and the equation.
        /// Only positive amounts of damage will be dealt; negatives in the equation act as delays.
        /// </summary>
        public PolynomialDpsCalculator(DamageType type, double[] coefficients, int duration)
            : base(type, GetTotal(coefficients, duration), duration)
        {
            _func = new Polynomial(coefficients);
        }
        
        protected override int CalculateDps()
        {
            var y = Fx();
            var amount = MathUtil.LowerDoubleToInt(y);
            return WithOverflow();

            // LOCAL FX
            double Fx()
            {
                var result = _func.Fx(SecondsPassed);
                return MathUtil.NonNeg(result);
            }
            int WithOverflow()
            {
                _overflow += y - amount;
                if (_overflow < 1)
                    return amount;
                _overflow--;
                return amount + 1;
            }
        }

        protected override bool IsDoneCondition() => TimeRemaining <= 0;

        public override string ToDataString()
        {
            var sb = new StringBuilder(
                $"[({DamageDealt}/{TotalDamage}){Type.GetName()}/({SecondsPassed}/{Duration})s@({_func.GetCoefficientForExponent(0)}");
            for (var i = 1; i < _func.NumberOfExponents; i++)
                sb.Append($",{_func.GetCoefficientForExponent(i)}");
            return sb.Append(")]").ToString();
        }

        protected override string ChildInfoString()
        {
            var sb = new StringBuilder(
                $"{DamageDealt}/{TotalDamage} {Type.GetName()} damage dealt over {SecondsPassed}/{Duration} seconds with F(x)=");
            sb.Append(_func.GetCoefficientForExponent(0).ToString(CultureInfo.InvariantCulture));
            AppendCoefficients();
            return sb.ToString();
            
            // LOCAL FX
            void AppendCoefficients()
            {
                for (var i = 1; i < _func.NumberOfExponents; i++)
                {
                    var coef = _func.GetCoefficientForExponent(i);
                    if (coef != 0)
                        sb.Append($" + {coef}*x^{i}");
                }
            }
        }
        

        private static int GetTotal(double[] coefficients, int x)
        {
            var f = new Polynomial(coefficients);
            var ttl = 0.0;
            for (var i = 1; i <= x; i++)
                ttl += MathUtil.NonNeg(f.Fx(i));
            return MathUtil.LowerDoubleToInt(ttl);
        }
    }
}
