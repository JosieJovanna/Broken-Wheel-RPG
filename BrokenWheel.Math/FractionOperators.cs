namespace BrokenWheel.Math
{
    /// <summary>
    /// Operators for the Fraction object
    /// includes -(unary), and binary operators such as +, -, *, /
    /// also includes relational and logical operators such as ==, !=, <, >, <=, >=
    /// </summary>
    public sealed partial class Fraction
    {
        public static Fraction operator -(Fraction frac1) => Negate(frac1);
        public static Fraction operator +(Fraction frac1, Fraction frac2) => TryAdd(frac1, frac2);
        public static Fraction operator +(int iNo, Fraction frac1) => TryAdd(frac1, new Fraction(iNo));
        public static Fraction operator +(Fraction frac1, int iNo) => TryAdd(frac1, new Fraction(iNo));
        public static Fraction operator +(double dbl, Fraction frac1) => TryAdd(frac1, FromDouble(dbl));
        public static Fraction operator +(Fraction frac1, double dbl) => TryAdd(frac1, FromDouble(dbl));
        public static Fraction operator -(Fraction frac1, Fraction frac2) => TryAdd(frac1, -frac2);
        public static Fraction operator -(int iNo, Fraction frac1) => TryAdd(-frac1, new Fraction(iNo));
        public static Fraction operator -(Fraction frac1, int iNo) => TryAdd(frac1, -new Fraction(iNo));
        public static Fraction operator -(double dbl, Fraction frac1) => TryAdd(-frac1, FromDouble(dbl));
        public static Fraction operator -(Fraction frac1, double dbl) => TryAdd(frac1, -FromDouble(dbl));
        public static Fraction operator *(Fraction frac1, Fraction frac2) => TryMultiply(frac1, frac2);
        public static Fraction operator *(int iNo, Fraction frac1) => TryMultiply(frac1, new Fraction(iNo));
        public static Fraction operator *(Fraction frac1, int iNo) => TryMultiply(frac1, new Fraction(iNo));
        public static Fraction operator *(double dbl, Fraction frac1) => TryMultiply(frac1, FromDouble(dbl));
        public static Fraction operator *(Fraction frac1, double dbl) => TryMultiply(frac1, FromDouble(dbl));
        public static Fraction operator /(Fraction frac1, Fraction frac2) => TryMultiply(frac1, Inverse(frac2));
        public static Fraction operator /(int iNo, Fraction frac1) => TryMultiply(Inverse(frac1), new Fraction(iNo));
        public static Fraction operator /(Fraction frac1, int iNo) => TryMultiply(frac1, Inverse(new Fraction(iNo)));
        public static Fraction operator /(double dbl, Fraction frac1) => TryMultiply(Inverse(frac1), FromDouble(dbl));
        public static Fraction operator /(Fraction frac1, double dbl) => TryMultiply(frac1, Inverse(FromDouble(dbl)));
        
        public static bool operator ==(Fraction frac1, Fraction frac2) => frac1 != null && frac1.Equals(frac2);
        public static bool operator ==(Fraction frac1, int iNo) => frac1 != null && frac1.Equals(new Fraction(iNo));
        public static bool operator ==(Fraction frac1, double dbl) => frac1 != null && frac1.Equals(new Fraction(dbl));
        public static bool operator !=(Fraction frac1, Fraction frac2) => frac1 == null || !frac1.Equals(frac2);
        public static bool operator !=(Fraction frac1, int iNo) => frac1 == null || !frac1.Equals(new Fraction(iNo));
        public static bool operator !=(Fraction frac1, double dbl) => frac1 == null || !frac1.Equals(new Fraction(dbl));

        public static bool operator <(Fraction frac1, Fraction frac2) => frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator;
        public static bool operator >(Fraction frac1, Fraction frac2) => frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator;
        public static bool operator <=(Fraction frac1, Fraction frac2) => frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator;
        public static bool operator >=(Fraction frac1, Fraction frac2) => frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator;

        public static implicit operator Fraction(long lNo) => new Fraction(lNo);
        public static implicit operator Fraction(double dNo) => new Fraction(dNo);
        public static implicit operator Fraction(string strNo) => new Fraction(strNo);
        public static explicit operator double(Fraction frac) => frac.ToDouble();
        public static implicit operator string(Fraction frac) => frac.ToString();
    }
}
