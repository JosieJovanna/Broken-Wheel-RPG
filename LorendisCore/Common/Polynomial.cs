using System;
using System.Linq;

namespace LorendisCore.Common
{
    public class Polynomial
    {
        public int NumberOfExponents => _coefficients.Length;
        
        /// <summary>
        /// Array of coefficients, starting from the constant at index 0.
        /// Index corresponds to exponent of x to which the coefficient applies.
        /// </summary>
        private readonly double[] _coefficients;

        public Polynomial(double[] coefficients)
        {
            ValidateCoefficients(coefficients);
            var len = IndexOfLastNonZeroValue(coefficients);
            // clone array
            _coefficients = new double[len];
            for (var i = 0; i < len; i++)
                _coefficients[i] = coefficients[i];
        }

        public double Fx(int x)
        {
            return _coefficients
                .Select((coef, idx) => coef * Math.Pow(x, idx))
                .Sum();
        }

        public double GetCoefficientForExponent(int exponent)
        {
            if (exponent < 0 || exponent >= _coefficients.Length)
                return 0;
            return _coefficients[exponent];
        }
        
        
        #region Utility
        private static void ValidateCoefficients(double[] coefficients)
        {
            if (coefficients == null)
                throw new ArgumentNullException(nameof(coefficients));
            if (coefficients.Length == 0)
                throw new ArgumentException("There must be coefficients.");
            if (coefficients.All(c => c == 0))
                throw new ArgumentException("At least one coefficient must be non-zero.");
        }

        private static int IndexOfLastNonZeroValue(double[] coefficients)
        {
            for (var i = coefficients.Length - 1; i >= 0; i--)
            {
                var c = coefficients[i];
                if (c != 0)
                    return i;
            }
            return coefficients.Length - 1; // Should not happen: already validated some nonzero coefficient exists.
        }
        #endregion
    }
}