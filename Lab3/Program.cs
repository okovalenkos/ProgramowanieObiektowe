using System;
using System.Globalization;

namespace Lab3
{
    public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>
    {
        private double re;
        private double im;

        public double Re
        {
            get => re;
            set => re = value;
        }

        public double Im
        {
            get => im;
            set => im = value;
        }

        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        public override string ToString()
        {
            // Użycie invariant culture, aby reprezentacja liczb była przewidywalna
            string reStr = re.ToString("G", CultureInfo.InvariantCulture);
            string imAbsStr = Math.Abs(im).ToString("G", CultureInfo.InvariantCulture);
            string sign = im >= 0 ? " + " : " - ";
            return $"{reStr}{sign}{imAbsStr}i";
        }

        // ICloneable
        public object Clone()
        {
            return new ComplexNumber(this.re, this.im);
        }

        // IEquatable<ComplexNumber>
        public bool Equals(ComplexNumber? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Double.Equals(this.re, other.re) && Double.Equals(this.im, other.im);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ComplexNumber);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(re, im);
        }

        // Operatory binarne
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            if (a is null) throw new ArgumentNullException(nameof(a));
            if (b is null) throw new ArgumentNullException(nameof(b));
            return new ComplexNumber(a.re + b.re, a.im + b.im);
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            if (a is null) throw new ArgumentNullException(nameof(a));
            if (b is null) throw new ArgumentNullException(nameof(b));
            return new ComplexNumber(a.re - b.re, a.im - b.im);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            if (a is null) throw new ArgumentNullException(nameof(a));
            if (b is null) throw new ArgumentNullException(nameof(b));
            double real = a.re * b.re - a.im * b.im;
            double imag = a.re * b.im + a.im * b.re;
            return new ComplexNumber(real, imag);
        }

        // Unarny operator - realizuje sprzężenie: -(a+bi) = a - bi (zgodnie z poleceniem)
        public static ComplexNumber operator -(ComplexNumber a)
        {
            if (a is null) throw new ArgumentNullException(nameof(a));
            return new ComplexNumber(a.re, -a.im);
        }

        // Operatory == oraz !=
        public static bool operator ==(ComplexNumber? left, ComplexNumber? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.Equals(right);
        }

        public static bool operator !=(ComplexNumber? left, ComplexNumber? right)
        {
            return !(left == right);
        }
    }
}