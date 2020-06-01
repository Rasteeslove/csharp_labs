using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace L7
{

    class RNumber : IEquatable<RNumber>, IComparable<RNumber>, IFormattable
    {

        #region Instance fields

        private BigInteger numerator, denominator;

        #endregion

        #region Instance properties

        public BigInteger Numerator { get { return numerator; } }
        public BigInteger Denominator { get { return denominator; } }
        public RNumber IntPart { get { return (BigInteger)this; } }
        public RNumber FractPart { get { return this - this.IntPart; } }
        public RNumber Sign { get { return numerator.Sign; } }

        #endregion

        #region Constructors

        RNumber() : this(0, 1, false) { }

        public RNumber(BigInteger numerator, BigInteger denominator) : this(numerator, denominator, true) { }

        public RNumber(BigInteger integer) : this(integer, 1, false) { }

        private RNumber(BigInteger numerator, BigInteger denominator, bool needsReduction)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("The fraction denominator can't be zero");
            }

            if (denominator < 0)
            {
                this.denominator = -denominator;
                this.numerator = -numerator;
            }
            else
            {
                this.numerator = numerator;
                this.denominator = denominator;
            }

            if (needsReduction)
            {
                this.Reduce();
            }
        }

        #endregion

        #region Operators

        #region Conversion 

        public static implicit operator RNumber(BigInteger num) { return new RNumber(num); }
        public static implicit operator RNumber(long num) { return new RNumber(num); }
        public static implicit operator RNumber((BigInteger, BigInteger) a) { return new RNumber(a.Item1, a.Item2); }
        public static implicit operator RNumber(decimal a) { return Parse(a.ToString(), "d"); }
        public static implicit operator RNumber(double a) { return Parse(a.ToString(), "d"); }
        public static implicit operator RNumber(float a) { return Parse(a.ToString(), "d"); }

        public static explicit operator BigInteger(RNumber num) { return num.numerator / num.denominator; }
        public static explicit operator double(RNumber num) { return RNumber.ToDouble(num); }

        #endregion

        #region Arithmetic

        #region Unary

        public static RNumber operator +(RNumber num) { return num; }
        public static RNumber operator -(RNumber num) { return new RNumber(-num.numerator, num.denominator, false); }
        public static RNumber operator ++(RNumber num) { return num + 1; }
        public static RNumber operator --(RNumber num) { return num - 1; }

        #endregion

        #region Binary

        public static RNumber operator +(RNumber a, RNumber b)
        {
            return new RNumber(a.numerator * b.denominator + b.numerator * a.denominator , a.denominator * b.denominator);
        }

        public static RNumber operator -(RNumber a, RNumber b)
        {
            return a + (-b);
        }

        public static RNumber operator *(RNumber a, RNumber b)
        {
            return new RNumber(a.numerator * b.numerator, a.denominator * b.denominator);
        }

        public static RNumber operator /(RNumber a, RNumber b)
        {
            return a * b.Flipped();
        }

        #endregion

        #endregion

        #region Comparison

        public static bool operator ==(RNumber a, RNumber b) { return a.Equals(b); }
        public static bool operator !=(RNumber a, RNumber b) { return !a.Equals(b); }
        public static bool operator >(RNumber a, RNumber b) { return a.CompareTo(b) > 0; }
        public static bool operator <(RNumber a, RNumber b) { return a.CompareTo(b) < 0; }
        public static bool operator >=(RNumber a, RNumber b) { return a.CompareTo(b) >= 0; }
        public static bool operator <=(RNumber a, RNumber b) { return a.CompareTo(b) <= 0; }

        #endregion

        #endregion

        #region Instance methods

        #region Additional math

        private void Reduce()
        {
            if (this.numerator == 0)
            {
                this.denominator = 1;
                return;
            }

            BigInteger gcd = ExtraMath.PosGCDofTwo(this.numerator, this.denominator);
            this.numerator /= gcd;
            this.denominator /= gcd;
        }      

        private RNumber Flipped()
        {
            if (this.numerator == 0)
            {
                throw new DivideByZeroException();
            }

            return new RNumber(this.denominator, this.numerator, false);
        }

        #endregion

        #region IEquattable, IComparable

        public int CompareTo(RNumber other)
        {
            return BigInteger.Compare(this.numerator * other.denominator, other.numerator * this.denominator);
        }

        public int CompareTo(object obj)
        {
            if (obj is RNumber)
                return this.CompareTo((RNumber)obj);

            if (obj == null)
                return 1;

            throw new ArgumentException("obj is not of RNumber type.", "obj");
        }

        public bool Equals(RNumber other) 
        { 
            return this.CompareTo(other) == 0; 
        }
        
        public override bool Equals(object obj) 
        { 
            return this.CompareTo(obj) == 0; 
        }

        #endregion

        #region Strings

        public override string ToString()
        {
            return ToString("s");
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (format == null) format = "s";
            switch (format)
            {
                case "s":
                    return string.Concat(this.numerator, "/", this.denominator);
                case "m":
                    string result = string.Concat(BigInteger.Abs(this.numerator / this.denominator), " ",
                        BigInteger.Abs(this.numerator % this.denominator), "/", this.denominator);
                    if (this.Sign < 0) result = "-" + result;
                    return result;
                case "d":
                    return RNumberToString(this.numerator, this.denominator);
                default:
                    throw new FormatException("Unknown format error.");
            }
        }

        #endregion

        public override int GetHashCode()
        {
            return HashCode.Combine(this.numerator.GetHashCode(), this.Denominator.GetHashCode());
        }

        #endregion

        #region Static methods

        #region Additional Math

        public static double ToDouble(RNumber num)
        {
            return (double)num.numerator / (double)num.denominator;
        }

        public static RNumber Abs(RNumber num)
        {
            return new RNumber(BigInteger.Abs(num.numerator), num.denominator, false);
        }

        public static RNumber Pow(RNumber num, int power)
        {
            if (power < 0) return RNumber.Pow(num.Flipped(), -power);
            if (power == 0) return 1;

            return new RNumber(BigInteger.Pow(num.numerator, power), BigInteger.Pow(num.denominator, power), false);
        }

        #endregion

        #region Strings

        static public RNumber Parse(string s, string format = "s")
        {
            if (!TryParse(format, s, out RNumber result)) throw new Exception("Parse error.");
            return result;
        }

        public static bool TryParse(string s, out RNumber result)
        {
            return TryParse("s", s, out result) || TryParse("m", s, out result) || TryParse("d", s, out result);
        }

        public static bool TryParse(string format, string s, out RNumber result)
        {
            s = s.Trim();
            result = default;
            switch (format)
            {
                case "s":
                    {
                        string pattern = @"^(-?\d+)\s*[/]\s*(\d+)$";
                        if (!Regex.IsMatch(s, pattern)) return false;

                        Match match = Regex.Match(s, pattern);
                        if (!BigInteger.TryParse(match.Groups[1].Value, out BigInteger value1)) return false;
                        if (!BigInteger.TryParse(match.Groups[2].Value, out BigInteger value2) || value2 == 0) return false;

                        result = new RNumber(value1, value2);
                        return true;
                    }
                case "m":
                    {
                        string pattern = @"^(-?)(\d+)\s+(\d+)\s*[/]\s*(\d+)$";
                        if (!Regex.IsMatch(s, pattern)) return false;

                        Match match = Regex.Match(s, pattern);
                        bool isNegative = match.Groups[1].Value == "-";
                        if (!BigInteger.TryParse(match.Groups[2].Value, out BigInteger value1)) return false;
                        if (!BigInteger.TryParse(match.Groups[3].Value, out BigInteger value2)) return false;
                        if (!BigInteger.TryParse(match.Groups[4].Value, out BigInteger value3)) return false;

                        result = value1 + new RNumber(value2, value3);
                        if (isNegative) result = -result;
                        return true;
                    }
                case "d":
                    {
                        return TryStringToRNumber(s, out result);
                    }
                default:
                    throw new FormatException("Unknown format error.");
            }
        }

        private static bool TryStringToRNumber(string s, out RNumber result)
        {
            string pattern = @"^(-?)(\d+)[.](\d*)[(](\d+)[)]$";
            if (Regex.IsMatch(s, pattern))
            {
                if (TryStringToDecimalFractionRNumber(s, out result)) return true;
                s = s.Replace("(", "");
                s = s.Replace(")", "");
            }
            return TryStringToCommonFractionRNumber(s, out result);
        }

        public static string RNumberToString(BigInteger numerator, BigInteger denominator)
        {
            Dictionary<BigInteger, int> dict = new Dictionary<BigInteger, int>();
            string result = "";

            if (numerator < 0)
            {
                result = "-";
                numerator = -numerator;
            }

            for (int i = 0; i < 30; i++)
            {
                result += numerator / denominator;
                BigInteger fract = numerator % denominator;

                if (fract == 0) return result;
                if (dict.TryGetValue(fract, out int ind)) return result.Insert(ind, "(") + ')';
                if (i == 0) result += ".";

                dict.Add(fract, result.Length);
                numerator = fract * 10;
            }

            return result + "...";
        }

        static bool TryStringToDecimalFractionRNumber(string s, out RNumber result)
        {
            result = default;
            string pattern = @"^(-?)(\d+)[.](\d*)[(](\d+)[)]$";
            if (!Regex.IsMatch(s, pattern)) return false;

            Match match = Regex.Match(s, pattern);
            bool isNegative = match.Groups[1].Value == "-";
            if (!BigInteger.TryParse(match.Groups[2].Value, out BigInteger intPart)) return false;

            string sa = match.Groups[3].Value + match.Groups[4].Value;
            string sb = match.Groups[3].Value;
            BigInteger a, b;
            if (!BigInteger.TryParse(sa, out a)) return false;

            if (sb == "") b = 0;
            else if (!BigInteger.TryParse(sb, out b)) return false;

            int n = sa.Length;
            int k = sa.Length - sb.Length;
            if (n > 18) return false;

            result = intPart + new RNumber(a - b, BigInteger.Parse(new string('9', k) + new string('0', n - k)));
            if (isNegative) result = -result;
            return true;
        }

        static bool TryStringToCommonFractionRNumber(string s, out RNumber result)
        {
            result = default;
            string pattern = @"^(-?)(\d+)([.](\d+))?$";
            if (!Regex.IsMatch(s, pattern)) return false;

            Match match = Regex.Match(s, pattern);
            bool isNegative = match.Groups[1].Value == "-";
            if (!BigInteger.TryParse(match.Groups[2].Value, out BigInteger intPart)) return false;

            string fracPart = match.Groups[4].Value;
            int i = 0;
            long den = 1;
            while (i < fracPart.Length && den <= 1e17)
            {
                intPart *= 10;
                intPart += fracPart[i] - '0';
                den *= 10;
                i++;
            }

            result = new RNumber(intPart, den);
            if (isNegative) result = -result;
            return true;
        }     

        #endregion

        #endregion

    }

}
