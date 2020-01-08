/*
Written by Peter O. in 2014.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;

namespace PeterO.Numbers {
  /// <summary>Represents an arbitrary-precision rational number. This
  /// class can't be inherited. (The "E" stands for "extended", meaning
  /// that instances of this class can be values other than numbers
  /// proper, such as infinity and not-a-number.)
  /// <para><b>Thread safety:</b> Instances of this class are immutable,
  /// so they are inherently safe for use by multiple threads. Multiple
  /// instances of this object with the same properties are
  /// interchangeable, so they should not be compared using the "=="
  /// operator (which might only check if each side of the operator is
  /// the same instance).</para></summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Design",
        "CA1036",
        Justification = "Awaiting advice at dotnet/dotnet-api-docs#2937.")]
  public sealed partial class ERational : IComparable<ERational>,
    IEquatable<ERational> {
    private const int MaxSafeInt = 214748363;

    /// <summary>A not-a-number value.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Security",
        "CA2104",
        Justification = "ERational is immutable")]
    public static readonly ERational NaN = new ERational(
      EInteger.Zero,
      EInteger.One,
      BigNumberFlags.FlagQuietNaN);

    /// <summary>Negative infinity, less than any other number.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Security",
        "CA2104",
        Justification = "ERational is immutable")]
    public static readonly ERational NegativeInfinity =
      new ERational(
        EInteger.Zero,
        EInteger.One,
        BigNumberFlags.FlagInfinity | BigNumberFlags.FlagNegative);

    /// <summary>A rational number for negative zero.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Security",
        "CA2104",
        Justification = "ERational is immutable")]
    public static readonly ERational NegativeZero =
      new ERational(EInteger.Zero, EInteger.One, BigNumberFlags.FlagNegative);

    /// <summary>The rational number one.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Security",
        "CA2104",
        Justification = "ERational is immutable")]
    public static readonly ERational One = FromEInteger(EInteger.One);

    /// <summary>Positive infinity, greater than any other
    /// number.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Security",
        "CA2104",
        Justification = "ERational is immutable")]
    public static readonly ERational PositiveInfinity =
      new ERational(
        EInteger.Zero,
        EInteger.One,
        BigNumberFlags.FlagInfinity);

    /// <summary>A signaling not-a-number value.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Security",
        "CA2104",
        Justification = "ERational is immutable")]
    public static readonly ERational SignalingNaN =
      new ERational(
        EInteger.Zero,
        EInteger.One,
        BigNumberFlags.FlagSignalingNaN);

    /// <summary>The rational number ten.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Security",
        "CA2104",
        Justification = "ERational is immutable")]
    public static readonly ERational Ten = FromEInteger((EInteger)10);

    /// <summary>A rational number for zero.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Security",
        "CA2104",
        Justification = "ERational is immutable")]
    public static readonly ERational Zero = FromEInteger(EInteger.Zero);

    private readonly EInteger denominator;

    private readonly int flags;
    private readonly EInteger unsignedNumerator;

    private ERational(EInteger numerator, EInteger denominator, int flags) {
      #if DEBUG
      if (numerator == null) {
        throw new ArgumentNullException(nameof(numerator));
      }
      if (denominator == null) {
        throw new ArgumentNullException(nameof(denominator));
      }
      if (denominator.IsZero) {
        throw new ArgumentException("Denominator is zero.");
      }
      #endif
      this.unsignedNumerator = numerator;
      this.denominator = denominator;
      this.flags = flags;
    }

    /// <summary>Initializes a new instance of the
    /// <see cref='PeterO.Numbers.ERational'/> class.</summary>
    /// <param name='numerator'>An arbitrary-precision integer serving as
    /// the numerator.</param>
    /// <param name='denominator'>An arbitrary-precision integer serving as
    /// the denominator.</param>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='numerator'/> or <paramref name='denominator'/> is
    /// null.</exception>
    /// <exception cref='ArgumentException'>Denominator is
    /// zero.</exception>
    public ERational(EInteger numerator, EInteger denominator) {
      if (numerator == null) {
        throw new ArgumentNullException(nameof(numerator));
      }
      if (denominator == null) {
        throw new ArgumentNullException(nameof(denominator));
      }
      if (denominator.IsZero) {
        throw new ArgumentException("denominator is zero");
      }
      bool numNegative = numerator.Sign < 0;
      bool denNegative = denominator.Sign < 0;
      this.flags = (numNegative != denNegative) ?
        BigNumberFlags.FlagNegative : 0;
      if (numNegative) {
        numerator = -numerator;
      }
      if (denNegative) {
        denominator = -denominator;
      }
      this.unsignedNumerator = numerator;
      this.denominator = denominator;
    }

    /// <summary>Creates a copy of this arbitrary-precision rational
    /// number.</summary>
    /// <returns>An arbitrary-precision rational number.</returns>
    public ERational Copy() {
      return new ERational(
          this.unsignedNumerator,
          this.denominator,
          this.flags);
    }

    /// <summary>Gets this object's denominator.</summary>
    /// <value>This object's denominator.</value>
    public EInteger Denominator {
      get {
        return this.denominator;
      }
    }

    /// <summary>Gets a value indicating whether this object is finite (not
    /// infinity or NaN).</summary>
    /// <value><c>true</c> if this object is finite (not infinity or NaN);
    /// otherwise, <c>false</c>.</value>
    public bool IsFinite {
      get {
        return !this.IsNaN() && !this.IsInfinity();
      }
    }

    /// <summary>Gets a value indicating whether this object's value is
    /// negative (including negative zero).</summary>
    /// <value><c>true</c> if this object's value is negative (including
    /// negative zero); otherwise, <c>false</c>. <c>true</c> if this
    /// object's value is negative; otherwise, <c>false</c>.</value>
    public bool IsNegative {
      get {
        return (this.flags & BigNumberFlags.FlagNegative) != 0;
      }
    }

    /// <summary>Gets a value indicating whether this object's value equals
    /// 0.</summary>
    /// <value><c>true</c> if this object's value equals 0; otherwise,
    /// <c>false</c>. <c>true</c> if this object's value equals 0;
    /// otherwise, <c>false</c>.</value>
    public bool IsZero {
      get {
        return ((this.flags & (BigNumberFlags.FlagInfinity |
                BigNumberFlags.FlagNaN)) == 0) && this.unsignedNumerator.IsZero;
      }
    }

    /// <summary>Gets this object's numerator.</summary>
    /// <value>This object's numerator. If this object is a not-a-number
    /// value, returns the diagnostic information (which will be negative
    /// if this object is negative).</value>
    public EInteger Numerator {
      get {
        return this.IsNegative ? (-(EInteger)this.unsignedNumerator) :
          this.unsignedNumerator;
      }
    }

    /// <summary>Gets the sign of this rational number.</summary>
    /// <value>The sign of this rational number.</value>
    public int Sign {
      get {
        return ((this.flags & (BigNumberFlags.FlagInfinity |
                BigNumberFlags.FlagNaN)) != 0) ? (this.IsNegative ? -1 : 1) :
          (this.unsignedNumerator.IsZero ? 0 : (this.IsNegative ? -1 : 1));
      }
    }

    /// <summary>Gets this object's numerator with the sign
    /// removed.</summary>
    /// <value>This object's numerator. If this object is a not-a-number
    /// value, returns the diagnostic information.</value>
    public EInteger UnsignedNumerator {
      get {
        return this.unsignedNumerator;
      }
    }

    /// <summary>Creates a rational number with the given numerator and
    /// denominator.</summary>
    /// <param name='numeratorSmall'>The numerator.</param>
    /// <param name='denominatorSmall'>The denominator.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    /// <exception cref='ArgumentException'>The denominator is
    /// zero.</exception>
    public static ERational Create(
      int numeratorSmall,
      int denominatorSmall) {
      return Create((EInteger)numeratorSmall, (EInteger)denominatorSmall);
    }

    /// <summary>Creates a rational number with the given numerator and
    /// denominator.</summary>
    /// <param name='numeratorLong'>The numerator.</param>
    /// <param name='denominatorLong'>The denominator.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    /// <exception cref='ArgumentException'>The denominator is
    /// zero.</exception>
    public static ERational Create(
      long numeratorLong,
      long denominatorLong) {
      return Create((EInteger)numeratorLong, (EInteger)denominatorLong);
    }

    /// <summary>Creates a rational number with the given numerator and
    /// denominator.</summary>
    /// <param name='numerator'>The numerator.</param>
    /// <param name='denominator'>The denominator.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    /// <exception cref='ArgumentException'>The denominator is
    /// zero.</exception>
    public static ERational Create(
      EInteger numerator,
      EInteger denominator) {
      return new ERational(numerator, denominator);
    }

    /// <summary>Creates a not-a-number arbitrary-precision rational
    /// number.</summary>
    /// <param name='diag'>An integer, 0 or greater, to use as diagnostic
    /// information associated with this object. If none is needed, should
    /// be zero. To get the diagnostic information from another
    /// arbitrary-precision rational number, use that object's
    /// <c>UnsignedNumerator</c> property.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    /// <exception cref='ArgumentException'>The parameter <paramref
    /// name='diag'/> is less than 0.</exception>
    public static ERational CreateNaN(EInteger diag) {
      return CreateNaN(diag, false, false);
    }

    /// <summary>Creates a not-a-number arbitrary-precision rational
    /// number.</summary>
    /// <param name='diag'>An integer, 0 or greater, to use as diagnostic
    /// information associated with this object. If none is needed, should
    /// be zero. To get the diagnostic information from another
    /// arbitrary-precision rational number, use that object's
    /// <c>UnsignedNumerator</c> property.</param>
    /// <param name='signaling'>Whether the return value will be signaling
    /// (true) or quiet (false).</param>
    /// <param name='negative'>Whether the return value is
    /// negative.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    /// <exception cref='ArgumentException'>The parameter <paramref
    /// name='diag'/> is less than 0.</exception>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='diag'/> is null.</exception>
    public static ERational CreateNaN(
      EInteger diag,
      bool signaling,
      bool negative) {
      if (diag == null) {
        throw new ArgumentNullException(nameof(diag));
      }
      if (diag.Sign < 0) {
        throw new
        ArgumentException("Diagnostic information must be 0 or greater," +
          "\u0020 was: " + diag);
      }
      if (diag.IsZero && !negative) {
        return signaling ? SignalingNaN : NaN;
      }
      var flags = 0;
      if (negative) {
        flags |= BigNumberFlags.FlagNegative;
      }
      flags |= signaling ? BigNumberFlags.FlagSignalingNaN :
        BigNumberFlags.FlagQuietNaN;
      return new ERational(diag, EInteger.One, flags);
    }

    /// <summary>Converts a 64-bit floating-point number to a rational
    /// number. This method computes the exact value of the floating point
    /// number, not an approximation, as is often the case by converting
    /// the number to a string.</summary>
    /// <param name='flt'>The parameter <paramref name='flt'/> is a 64-bit
    /// floating-point number.</param>
    /// <returns>A rational number with the same value as <paramref
    /// name='flt'/>.</returns>
    public static ERational FromDouble(double flt) {
      return FromEFloat(EFloat.FromDouble(flt));
    }

    /// <summary>Converts an arbitrary-precision decimal number to a
    /// rational number.</summary>
    /// <param name='ef'>The number to convert as an arbitrary-precision
    /// decimal number.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    [Obsolete("Renamed to FromEDecimal.")]
    public static ERational FromExtendedDecimal(EDecimal ef) {
      return FromEDecimal(ef);
    }

    /// <summary>Converts an arbitrary-precision binary floating-point
    /// number to a rational number.</summary>
    /// <param name='ef'>The number to convert as an arbitrary-precision
    /// binary floating-point number.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    [Obsolete("Renamed to FromEFloat.")]
    public static ERational FromExtendedFloat(EFloat ef) {
      return FromEFloat(ef);
    }

    /// <summary>Converts an arbitrary-precision decimal number to a
    /// rational number.</summary>
    /// <param name='ef'>The number to convert as an arbitrary-precision
    /// decimal number.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='ef'/> is null.</exception>
    public static ERational FromEDecimal(EDecimal ef) {
      if (ef == null) {
        throw new ArgumentNullException(nameof(ef));
      }
      if (!ef.IsFinite) {
        var flags = 0;
        if (ef.IsNegative) {
          flags |= BigNumberFlags.FlagNegative;
        }
        if (ef.IsInfinity()) {
          flags |= BigNumberFlags.FlagInfinity;
        }
        if (ef.IsSignalingNaN()) {
          flags |= BigNumberFlags.FlagSignalingNaN;
        }
        if (ef.IsQuietNaN()) {
          flags |= BigNumberFlags.FlagQuietNaN;
        }
        return new ERational(ef.UnsignedMantissa, EInteger.One, flags);
      }
      EInteger num = ef.Mantissa;
      EInteger exp = ef.Exponent;
      if (exp.IsZero) {
        return FromEInteger(num);
      }
      bool neg = num.Sign < 0;
      num = num.Abs();
      EInteger den = EInteger.One;
      if (exp.Sign < 0) {
        exp = -(EInteger)exp;
        den = NumberUtility.FindPowerOfTenFromBig(exp);
      } else {
        EInteger powerOfTen = NumberUtility.FindPowerOfTenFromBig(exp);
        num *= (EInteger)powerOfTen;
      }
      if (neg) {
        num = -(EInteger)num;
      }
      return ERational.Create(num, den);
    }

    /// <summary>Converts an arbitrary-precision binary floating-point
    /// number to a rational number.</summary>
    /// <param name='ef'>The number to convert as an arbitrary-precision
    /// binary floating-point number.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='ef'/> is null.</exception>
    public static ERational FromEFloat(EFloat ef) {
      if (ef == null) {
        throw new ArgumentNullException(nameof(ef));
      }
      if (!ef.IsFinite) {
        var flags = 0;
        if (ef.IsNegative) {
          flags |= BigNumberFlags.FlagNegative;
        }
        if (ef.IsInfinity()) {
          flags |= BigNumberFlags.FlagInfinity;
        }
        if (ef.IsSignalingNaN()) {
          flags |= BigNumberFlags.FlagSignalingNaN;
        }
        if (ef.IsQuietNaN()) {
          flags |= BigNumberFlags.FlagQuietNaN;
        }
        return new ERational(ef.UnsignedMantissa, EInteger.One, flags);
      }
      EInteger num = ef.Mantissa;
      EInteger exp = ef.Exponent;
      if (exp.IsZero) {
        return FromEInteger(num);
      }
      bool neg = num.Sign < 0;
      num = num.Abs();
      EInteger den = EInteger.One;
      if (exp.Sign < 0) {
        exp = -(EInteger)exp;
        den = den.ShiftLeft(exp);
      } else {
        num = num.ShiftLeft(exp);
      }
      if (neg) {
        num = -(EInteger)num;
      }
      return ERational.Create(num, den);
    }

    /// <summary>Converts an arbitrary-precision integer to a rational
    /// number.</summary>
    /// <param name='bigint'>The number to convert as an
    /// arbitrary-precision integer.</param>
    /// <returns>The exact value of the integer as a rational
    /// number.</returns>
    public static ERational FromEInteger(EInteger bigint) {
      return ERational.Create(bigint, EInteger.One);
    }

    /// <summary>Converts a 32-bit binary floating-point number to a
    /// rational number. This method computes the exact value of the
    /// floating point number, not an approximation, as is often the case
    /// by converting the number to a string.</summary>
    /// <param name='flt'>The parameter <paramref name='flt'/> is a 32-bit
    /// binary floating-point number.</param>
    /// <returns>A rational number with the same value as <paramref
    /// name='flt'/>.</returns>
    public static ERational FromSingle(float flt) {
      return FromEFloat(EFloat.FromSingle(flt));
    }

    /// <summary>Creates a rational number from a text string that
    /// represents a number. See <c>FromString(String, int, int)</c> for
    /// more information.</summary>
    /// <param name='str'>A string that represents a number.</param>
    /// <returns>An arbitrary-precision rational number with the same value
    /// as the given string.</returns>
    /// <exception cref='FormatException'>The parameter <paramref
    /// name='str'/> is not a correctly formatted number
    /// string.</exception>
    public static ERational FromString(string str) {
      return FromString(str, 0, str == null ? 0 : str.Length);
    }

    /// <summary>
    /// <para>Creates a rational number from a text string that represents
    /// a number.</para>
    /// <para>The format of the string generally consists of:</para>
    /// <list type=''>
    /// <item>An optional plus sign ("+" , U+002B) or minus sign ("-",
    /// U+002D) (if '-' , the value is negative.)</item>
    /// <item>The numerator in the form of one or more digits (these digits
    /// may begin with any number of zeros).</item>
    /// <item>Optionally, "/" followed by the denominator in the form of
    /// one or more digits (these digits may begin with any number of
    /// zeros). If a denominator is not given, it's equal to
    /// 1.</item></list>
    /// <para>The string can also be "-INF", "-Infinity", "Infinity",
    /// "INF", quiet NaN ("NaN" /"-NaN") followed by any number of digits,
    /// or signaling NaN ("sNaN" /"-sNaN") followed by any number of
    /// digits, all in any combination of upper and lower case.</para>
    /// <para>All characters mentioned above are the corresponding
    /// characters in the Basic Latin range. In particular, the digits must
    /// be the basic digits 0 to 9 (U+0030 to U+0039). The string is not
    /// allowed to contain white space characters, including
    /// spaces.</para></summary>
    /// <param name='str'>A text string, a portion of which represents a
    /// number.</param>
    /// <param name='offset'>An index starting at 0 showing where the
    /// desired portion of <paramref name='str'/> begins.</param>
    /// <param name='length'>The length, in code units, of the desired
    /// portion of <paramref name='str'/> (but not more than <paramref
    /// name='str'/> 's length).</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    /// <exception cref='FormatException'>The parameter <paramref
    /// name='str'/> is not a correctly formatted number
    /// string.</exception>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='str'/> is null.</exception>
    /// <exception cref='ArgumentException'>Either <paramref
    /// name='offset'/> or <paramref name='length'/> is less than 0 or
    /// greater than <paramref name='str'/> 's length, or <paramref
    /// name='str'/> 's length minus <paramref name='offset'/> is less than
    /// <paramref name='length'/>.</exception>
    public static ERational FromString(
      string str,
      int offset,
      int length) {
      int tmpoffset = offset;
      if (str == null) {
        throw new ArgumentNullException(nameof(str));
      }
      if (tmpoffset < 0) {
        throw new FormatException("offset(" + tmpoffset + ") is less than " +
          "0");
      }
      if (tmpoffset > str.Length) {
        throw new FormatException("offset(" + tmpoffset + ") is more than " +
          str.Length);
      }
      if (length < 0) {
        throw new FormatException("length(" + length + ") is less than " +
          "0");
      }
      if (length > str.Length) {
        throw new FormatException("length(" + length + ") is more than " +
          str.Length);
      }
      if (str.Length - tmpoffset < length) {
        throw new FormatException("str's length minus " + tmpoffset + "(" +
          (str.Length - tmpoffset) + ") is less than " + length);
      }
      if (length == 0) {
        throw new FormatException();
      }
      var negative = false;
      int endStr = tmpoffset + length;
      if (str[tmpoffset] == '+' || str[tmpoffset] == '-') {
        negative = str[tmpoffset] == '-';
        ++tmpoffset;
      }
      var numerInt = 0;
      EInteger numer = null;
      var haveDigits = false;
      var haveDenominator = false;
      var ndenomInt = 0;
      EInteger ndenom = null;
      int i = tmpoffset;
      if (i + 8 == endStr) {
        if ((str[i] == 'I' || str[i] == 'i') &&
          (str[i + 1] == 'N' || str[i + 1] == 'n') &&
          (str[i + 2] == 'F' || str[i + 2] == 'f') &&
          (str[i + 3] == 'I' || str[i + 3] == 'i') && (str[i + 4] == 'N' ||
            str[i + 4] == 'n') && (str[i + 5] == 'I' || str[i + 5] == 'i') &&
          (str[i + 6] == 'T' || str[i + 6] == 't') && (str[i + 7] == 'Y' ||
            str[i + 7] == 'y')) {
          return negative ? NegativeInfinity : PositiveInfinity;
        }
      }
      if (i + 3 == endStr) {
        if ((str[i] == 'I' || str[i] == 'i') &&
          (str[i + 1] == 'N' || str[i + 1] == 'n') && (str[i + 2] == 'F' ||
            str[i + 2] == 'f')) {
          return negative ? NegativeInfinity : PositiveInfinity;
        }
      }
      var numerStart = 0;
      if (i + 3 <= endStr) {
        // Quiet NaN
        if ((str[i] == 'N' || str[i] == 'n') && (str[i + 1] == 'A' || str[i +
              1] == 'a') && (str[i + 2] == 'N' || str[i + 2] == 'n')) {
          if (i + 3 == endStr) {
            return (!negative) ? NaN : NaN.Negate();
          }
          i += 3;
          numerStart = i;
          for (; i < endStr; ++i) {
            if (str[i] >= '0' && str[i] <= '9') {
              var thisdigit = (int)(str[i] - '0');
              if (numerInt <= MaxSafeInt) {
                numerInt *= 10;
                numerInt += thisdigit;
              }
            } else {
              throw new FormatException();
            }
          }
          if (numerInt > MaxSafeInt) {
            numer = EInteger.FromSubstring(str, numerStart, endStr);
            return CreateNaN(numer, false, negative);
          } else {
            return CreateNaN(EInteger.FromInt32(numerInt), false, negative);
          }
        }
      }
      if (i + 4 <= endStr) {
        // Signaling NaN
        if ((str[i] == 'S' || str[i] == 's') && (str[i + 1] == 'N' || str[i +
              1] == 'n') && (str[i + 2] == 'A' || str[i + 2] == 'a') &&
          (str[i + 3] == 'N' || str[i + 3] == 'n')) {
          if (i + 4 == endStr) {
            return (!negative) ? SignalingNaN : SignalingNaN.Negate();
          }
          i += 4;
          numerStart = i;
          for (; i < endStr; ++i) {
            if (str[i] >= '0' && str[i] <= '9') {
              var thisdigit = (int)(str[i] - '0');
              haveDigits = haveDigits || thisdigit != 0;
              if (numerInt <= MaxSafeInt) {
                numerInt *= 10;
                numerInt += thisdigit;
              }
            } else {
              throw new FormatException();
            }
          }
          int flags3 = (negative ? BigNumberFlags.FlagNegative : 0) |
            BigNumberFlags.FlagSignalingNaN;
          if (numerInt > MaxSafeInt) {
            numer = EInteger.FromSubstring(str, numerStart, endStr);
            return new ERational(numer,
                EInteger.One,
                flags3);
          } else {
            return new ERational(EInteger.FromInt32(numerInt),
                EInteger.One,
                flags3);
          }
        }
      }
      // Ordinary number
      numerStart = i;
      int numerEnd = i;
      for (; i < endStr; ++i) {
        if (str[i] >= '0' && str[i] <= '9') {
          var thisdigit = (int)(str[i] - '0');
          numerEnd = i + 1;
          if (numerInt <= MaxSafeInt) {
            numerInt *= 10;
            numerInt += thisdigit;
          }
          haveDigits = true;
        } else if (str[i] == '/') {
          haveDenominator = true;
          ++i;
          break;
        } else {
          throw new FormatException();
        }
      }
      if (!haveDigits) {
        throw new FormatException();
      }
      if (numerInt > MaxSafeInt) {
        numer = EInteger.FromSubstring(str, numerStart, numerEnd);
      }
      if (haveDenominator) {
        EInteger denom = null;
        var denomInt = 0;
        tmpoffset = 1;
        haveDigits = false;
        if (i == endStr) {
          throw new FormatException();
        }
        numerStart = i;
        for (; i < endStr; ++i) {
          if (str[i] >= '0' && str[i] <= '9') {
            haveDigits = true;
            var thisdigit = (int)(str[i] - '0');
            numerEnd = i + 1;
            if (denomInt <= MaxSafeInt) {
              denomInt *= 10;
              denomInt += thisdigit;
            }
          } else {
            throw new FormatException();
          }
        }
        if (!haveDigits) {
          throw new FormatException();
        }
        if (denomInt > MaxSafeInt) {
          denom = EInteger.FromSubstring(str, numerStart, numerEnd);
        }
        if (denom == null) {
          ndenomInt = denomInt;
        } else {
          ndenom = denom;
        }
      } else {
        ndenomInt = 1;
      }
      if (i != endStr) {
        throw new FormatException();
      }
      if (ndenom == null ? (ndenomInt == 0) : ndenom.IsZero) {
        throw new FormatException();
      }
      ERational erat = Create(
          numer == null ? (EInteger)numerInt : numer,
          ndenom == null ? (EInteger)ndenomInt : ndenom);
      return negative ? erat.Negate() : erat;
    }

    /// <summary>Compares the absolute values of this object and another
    /// object, imposing a total ordering on all possible values (ignoring
    /// their signs). In this method:
    /// <list>
    /// <item>For objects with the same value, the one with the higher
    /// denominator has a greater "absolute value".</item>
    /// <item>Negative zero and positive zero are considered equal.</item>
    /// <item>Quiet NaN has a higher "absolute value" than signaling NaN.
    /// If both objects are quiet NaN or both are signaling NaN, the one
    /// with the higher diagnostic information has a greater "absolute
    /// value".</item>
    /// <item>NaN has a higher "absolute value" than infinity.</item>
    /// <item>Infinity has a higher "absolute value" than any finite
    /// number.</item></list></summary>
    /// <param name='other'>An arbitrary-precision rational number to
    /// compare with this one.</param>
    /// <returns>The number 0 if both objects have the same value, or -1 if
    /// this object is less than the other value, or 1 if this object is
    /// greater.
    /// <para>This implementation returns a positive number if <paramref
    /// name='other'/> is null, to conform to the.NET definition of
    /// CompareTo. This is the case even in the Java version of this
    /// library, for consistency's sake, even though implementations of
    /// <c>Comparable.compareTo()</c> in Java ought to throw an exception
    /// if they receive a null argument rather than treating null as less
    /// or greater than any object.</para>.</returns>
    public int CompareToTotalMagnitude(ERational other) {
      if (other == null) {
        return 1;
      }
      var valueIThis = 0;
      var valueIOther = 0;
      int cmp;
      if (this.IsSignalingNaN()) {
        valueIThis = 2;
      } else if (this.IsNaN()) {
        valueIThis = 3;
      } else if (this.IsInfinity()) {
        valueIThis = 1;
      }
      if (other.IsSignalingNaN()) {
        valueIOther = 2;
      } else if (other.IsNaN()) {
        valueIOther = 3;
      } else if (other.IsInfinity()) {
        valueIOther = 1;
      }
      if (valueIThis > valueIOther) {
        return 1;
      } else if (valueIThis < valueIOther) {
        return -1;
      }
      if (valueIThis >= 2) {
        cmp = this.unsignedNumerator.CompareTo(
            other.unsignedNumerator);
        return cmp;
      } else if (valueIThis == 1) {
        return 0;
      } else {
        cmp = this.Abs().CompareTo(other.Abs());
        if (cmp == 0) {
          cmp = this.denominator.CompareTo(
              other.denominator);
          return cmp;
        }
        return cmp;
      }
    }

    /// <summary>Compares the values of this object and another object,
    /// imposing a total ordering on all possible values. In this method:
    /// <list>
    /// <item>For objects with the same value, the one with the higher
    /// denominator has a greater "absolute value".</item>
    /// <item>Negative zero is less than positive zero.</item>
    /// <item>Quiet NaN has a higher "absolute value" than signaling NaN.
    /// If both objects are quiet NaN or both are signaling NaN, the one
    /// with the higher diagnostic information has a greater "absolute
    /// value".</item>
    /// <item>NaN has a higher "absolute value" than infinity.</item>
    /// <item>Infinity has a higher "absolute value" than any finite
    /// number.</item>
    /// <item>Negative numbers are less than positive
    /// numbers.</item></list></summary>
    /// <param name='other'>An arbitrary-precision rational number to
    /// compare with this one.</param>
    /// <returns>The number 0 if both objects have the same value, or -1 if
    /// this object is less than the other value, or 1 if this object is
    /// greater.
    /// <para>This implementation returns a positive number if <paramref
    /// name='other'/> is null, to conform to the.NET definition of
    /// CompareTo. This is the case even in the Java version of this
    /// library, for consistency's sake, even though implementations of
    /// <c>Comparable.compareTo()</c> in Java ought to throw an exception
    /// if they receive a null argument rather than treating null as less
    /// or greater than any object.</para>.</returns>
    public int CompareToTotal(ERational other) {
      if (other == null) {
        return 1;
      }
      bool neg1 = this.IsNegative;
      bool neg2 = other.IsNegative;
      if (neg1 != neg2) {
        return neg1 ? -1 : 1;
      }
      var valueIThis = 0;
      var valueIOther = 0;
      int cmp;
      if (this.IsSignalingNaN()) {
        valueIThis = 2;
      } else if (this.IsNaN()) {
        valueIThis = 3;
      } else if (this.IsInfinity()) {
        valueIThis = 1;
      }
      if (other.IsSignalingNaN()) {
        valueIOther = 2;
      } else if (other.IsNaN()) {
        valueIOther = 3;
      } else if (other.IsInfinity()) {
        valueIOther = 1;
      }
      if (valueIThis > valueIOther) {
        return neg1 ? -1 : 1;
      } else if (valueIThis < valueIOther) {
        return neg1 ? 1 : -1;
      }
      if (valueIThis >= 2) {
        cmp = this.unsignedNumerator.CompareTo(
            other.unsignedNumerator);
        return neg1 ? -cmp : cmp;
      } else if (valueIThis == 1) {
        return 0;
      } else {
        cmp = this.CompareTo(other);
        if (cmp == 0) {
          cmp = this.denominator.CompareTo(
              other.denominator);
          return neg1 ? -cmp : cmp;
        }
        return cmp;
      }
    }

    /// <summary>Returns the absolute value of this rational number, that
    /// is, a number with the same value as this one but as a nonnegative
    /// number.</summary>
    /// <returns>An arbitrary-precision rational number.</returns>
    public ERational Abs() {
      if (this.IsNegative) {
        return new ERational(
            this.unsignedNumerator,
            this.denominator,
            this.flags & ~BigNumberFlags.FlagNegative);
      }
      return this;
    }

    /// <summary>Adds two rational numbers.</summary>
    /// <param name='otherValue'>Another arbitrary-precision rational
    /// number.</param>
    /// <returns>The sum of the two numbers. Returns not-a-number (NaN) if
    /// either operand is NaN.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='otherValue'/> is null.</exception>
    public ERational Add(ERational otherValue) {
      if (otherValue == null) {
        throw new ArgumentNullException(nameof(otherValue));
      }
      if (this.IsSignalingNaN()) {
        return CreateNaN(this.unsignedNumerator, false, this.IsNegative);
      }
      if (otherValue.IsSignalingNaN()) {
        return CreateNaN(
            otherValue.unsignedNumerator,
            false,
            otherValue.IsNegative);
      }
      if (this.IsQuietNaN()) {
        return this;
      }
      if (otherValue.IsQuietNaN()) {
        return otherValue;
      }
      if (this.IsInfinity()) {
        return otherValue.IsInfinity() ? ((this.IsNegative ==
              otherValue.IsNegative) ? this : NaN) : this;
      }
      if (otherValue.IsInfinity()) {
        return otherValue;
      }
      EInteger ad = this.Numerator * (EInteger)otherValue.Denominator;
      EInteger bc = this.Denominator * (EInteger)otherValue.Numerator;
      EInteger bd = this.Denominator * (EInteger)otherValue.Denominator;
      ad += (EInteger)bc;
      return ERational.Create(ad, bd);
    }

    /// <summary>Compares the mathematical value of an arbitrary-precision
    /// rational number with that of this instance. This method currently
    /// uses the rules given in the CompareToValue method, so that it it is
    /// not consistent with the Equals method, but it may change in a
    /// future version to use the rules for the CompareToTotal method
    /// instead.</summary>
    /// <param name='other'>An arbitrary-precision rational number.</param>
    /// <returns>Zero if the values are equal; a negative number if this
    /// instance is less, or a positive number if this instance is greater.
    /// <para>This implementation returns a positive number if <paramref
    /// name='other'/> is null, to conform to the.NET definition of
    /// CompareTo. This is the case even in the Java version of this
    /// library, for consistency's sake, even though implementations of
    /// <c>Comparable.compareTo()</c> in Java ought to throw an exception
    /// if they receive a null argument rather than treating null as less
    /// or greater than any object.</para>.</returns>
    public int CompareTo(ERational other) {
      return this.CompareToValue(other);
    }

    /// <summary>Compares the mathematical value of an arbitrary-precision
    /// rational number with that of this instance. In this method, NaN
    /// values are greater than any other ERational value, and two NaN
    /// values (even if their payloads differ) are treated as equal by this
    /// method. This method is not consistent with the Equals
    /// method.</summary>
    /// <param name='other'>An arbitrary-precision rational number.</param>
    /// <returns>Zero if the values are equal; a negative number if this
    /// instance is less, or a positive number if this instance is greater.
    /// <para>This implementation returns a positive number if <paramref
    /// name='other'/> is null, to conform to the.NET definition of
    /// CompareTo. This is the case even in the Java version of this
    /// library, for consistency's sake, even though implementations of
    /// <c>Comparable.compareTo()</c> in Java ought to throw an exception
    /// if they receive a null argument rather than treating null as less
    /// or greater than any object.</para>.</returns>
    public int CompareToValue(ERational other) {
      if (other == null) {
        return 1;
      }
      if (this == other) {
        return 0;
      }
      if (this.IsNaN()) {
        return other.IsNaN() ? 0 : 1;
      }
      if (other.IsNaN()) {
        return -1;
      }
      int signA = this.Sign;
      int signB = other.Sign;
      if (signA != signB) {
        return (signA < signB) ? -1 : 1;
      }
      if (signB == 0 || signA == 0) {
        // Special case: Either operand is zero
        return 0;
      }
      if (this.IsInfinity()) {
        if (other.IsInfinity()) {
          // if we get here, this only means that
          // both are positive infinity or both
          // are negative infinity
          return 0;
        }
        return this.IsNegative ? -1 : 1;
      }
      if (other.IsInfinity()) {
        return other.IsNegative ? 1 : -1;
      }
      // At this point, both numbers are finite and
      // have the same sign
      #if DEBUG
      if (!this.IsFinite) {
        throw new InvalidOperationException("doesn't satisfy this.IsFinite");
      }
      if (!other.IsFinite) {
        throw new InvalidOperationException("doesn't satisfy other.IsFinite");
      }
      #endif

      int dencmp = this.denominator.CompareTo(other.denominator);
      // At this point, the signs are equal so we can compare
      // their absolute values instead
      int numcmp = this.unsignedNumerator.CompareTo(other.unsignedNumerator);
      if (signA < 0) {
        numcmp = -numcmp;
      }
      if (numcmp == 0) {
        // Special case: numerators are equal, so the
        // number with the lower denominator is greater
        return signA < 0 ? dencmp : -dencmp;
      }
      if (dencmp == 0) {
        // denominators are equal
        return numcmp;
      }
      EInteger ad = this.Numerator * (EInteger)other.Denominator;
      EInteger bc = this.Denominator * (EInteger)other.Numerator;
      return ad.CompareTo(bc);
    }

    /// <summary>Compares the mathematical value of an arbitrary-precision
    /// rational number with that of this instance. This method currently
    /// uses the rules given in the CompareToValue method, so that it it is
    /// not consistent with the Equals method, but it may change in a
    /// future version to use the rules for the CompareToTotal method
    /// instead.</summary>
    /// <param name='intOther'>The parameter <paramref name='intOther'/> is
    /// a 32-bit signed integer.</param>
    /// <returns>Zero if the values are equal; a negative number if this
    /// instance is less, or a positive number if this instance is
    /// greater.</returns>
    public int CompareTo(int intOther) {
      return this.CompareToValue(ERational.FromInt32(intOther));
    }

    /// <summary>Compares the mathematical value of an arbitrary-precision
    /// rational number with that of this instance. In this method, NaN
    /// values are greater than any other ERational value, and two NaN
    /// values (even if their payloads differ) are treated as equal by this
    /// method. This method is not consistent with the Equals
    /// method.</summary>
    /// <param name='intOther'>The parameter <paramref name='intOther'/> is
    /// a 32-bit signed integer.</param>
    /// <returns>Zero if the values are equal; a negative number if this
    /// instance is less, or a positive number if this instance is
    /// greater.</returns>
    public int CompareToValue(int intOther) {
      return this.CompareToValue(ERational.FromInt32(intOther));
    }

    /// <summary>Compares an arbitrary-precision binary floating-point
    /// number with this instance. In this method, NaN values are greater
    /// than any other ERational or EFloat value, and two NaN values (even
    /// if their payloads differ) are treated as equal by this
    /// method.</summary>
    /// <param name='other'>An arbitrary-precision binary floating-point
    /// number.</param>
    /// <returns>Zero if the values are equal; a negative number if this
    /// instance is less, or a positive number if this instance is greater.
    /// <para>This implementation returns a positive number if <paramref
    /// name='other'/> is null, to conform to the.NET definition of
    /// CompareTo. This is the case even in the Java version of this
    /// library, for consistency's sake, even though implementations of
    /// <c>Comparable.compareTo()</c> in Java ought to throw an exception
    /// if they receive a null argument rather than treating null as less
    /// or greater than any object.</para>.</returns>
    public int CompareToBinary(EFloat other) {
      if (other == null) {
        return 1;
      }
      if (this.IsNaN()) {
        return other.IsNaN() ? 0 : 1;
      }
      int signA = this.Sign;
      int signB = other.Sign;
      if (signA != signB) {
        return (signA < signB) ? -1 : 1;
      }
      if (signB == 0 || signA == 0) {
        // Special case: Either operand is zero
        return 0;
      }
      if (this.IsInfinity()) {
        if (other.IsInfinity()) {
          // if we get here, this only means that
          // both are positive infinity or both
          // are negative infinity
          return 0;
        }
        return this.IsNegative ? -1 : 1;
      }
      if (other.IsInfinity()) {
        return other.IsNegative ? 1 : -1;
      }
      // At this point, both numbers are finite and
      // have the same sign
      #if DEBUG
      if (!this.IsFinite) {
        throw new InvalidOperationException("doesn't satisfy this.IsFinite");
      }
      if (!other.IsFinite) {
        throw new InvalidOperationException("doesn't satisfy other.IsFinite");
      }
      #endif
      EInteger bigExponent = other.Exponent;
      if (bigExponent.IsZero) {
        // Special case: other has exponent 0
        EInteger otherMant = other.Mantissa;
        EInteger bcx = this.Denominator * (EInteger)otherMant;
        return this.Numerator.CompareTo(bcx);
      }
      if (bigExponent.Abs().CompareTo((EInteger)1000) > 0) {
        // Other has a high absolute value of exponent, so try different
        // approaches to
        // comparison
        EInteger thisRem;
        EInteger thisInt;
        {
          EInteger[] divrem = this.UnsignedNumerator.DivRem(this.Denominator);
          thisInt = divrem[0];
          thisRem = divrem[1];
        }
        EFloat otherAbs = other.Abs();
        EFloat thisIntDec = EFloat.FromEInteger(thisInt);
        if (thisRem.IsZero) {
          // This object's value is an integer
          // Console.WriteLine("Shortcircuit IV");
          int ret = thisIntDec.CompareTo(otherAbs);
          return this.IsNegative ? -ret : ret;
        }
        if (thisIntDec.CompareTo(otherAbs) > 0) {
          // Truncated absolute value is greater than other's untruncated
          // absolute value
          // Console.WriteLine("Shortcircuit I");
          return this.IsNegative ? -1 : 1;
        }
        // Round up
        thisInt = thisInt.Add(EInteger.One);
        thisIntDec = EFloat.FromEInteger(thisInt);
        if (thisIntDec.CompareTo(otherAbs) < 0) {
          // Absolute value rounded up is less than other's unrounded
          // absolute value
          // Console.WriteLine("Shortcircuit II");
          return this.IsNegative ? 1 : -1;
        }
        thisIntDec = EFloat.FromEInteger(this.UnsignedNumerator).Divide(
            EFloat.FromEInteger(this.Denominator),
            EContext.ForPrecisionAndRounding(256, ERounding.Down));
        if (thisIntDec.CompareTo(otherAbs) > 0) {
          // Truncated absolute value is greater than other's untruncated
          // absolute value
          // Console.WriteLine("Shortcircuit III");
          return this.IsNegative ? -1 : 1;
        }
        if (other.Exponent.Sign > 0) {
          // NOTE: if unsigned numerator is 0, bitLength will return
          // 0 instead of 1, but the possibility of 0 was already excluded
          EInteger bigDigitCount =
            this.UnsignedNumerator.GetSignedBitLengthAsEInteger()
            .Subtract(1);
          if (bigDigitCount.CompareTo(other.Exponent) < 0) {
            // Numerator's digit count minus 1 is less than the other's
            // exponent,
            // and other's exponent is positive, so this value's absolute
            // value is less
            return this.IsNegative ? 1 : -1;
          }
        }
      }
      // Convert to rational number and use usual rational number
      // comparison
      // Console.WriteLine("no shortcircuit");
      // Console.WriteLine(this);
      // Console.WriteLine(other);
      ERational otherRational = ERational.FromEFloat(other);
      EInteger ad = this.Numerator * (EInteger)otherRational.Denominator;
      EInteger bc = this.Denominator * (EInteger)otherRational.Numerator;
      return ad.CompareTo(bc);
    }

    /// <summary>Compares an arbitrary-precision decimal number with this
    /// instance.</summary>
    /// <param name='other'>An arbitrary-precision decimal number.</param>
    /// <returns>Zero if the values are equal; a negative number if this
    /// instance is less, or a positive number if this instance is greater.
    /// <para>This implementation returns a positive number if <paramref
    /// name='other'/> is null, to conform to the.NET definition of
    /// CompareTo. This is the case even in the Java version of this
    /// library, for consistency's sake, even though implementations of
    /// <c>Comparable.compareTo()</c> in Java ought to throw an exception
    /// if they receive a null argument rather than treating null as less
    /// or greater than any object.</para>.</returns>
    public int CompareToDecimal(EDecimal other) {
      if (other == null) {
        return 1;
      }
      if (this.IsNaN()) {
        return other.IsNaN() ? 0 : 1;
      }
      int signA = this.Sign;
      int signB = other.Sign;
      if (signA != signB) {
        return (signA < signB) ? -1 : 1;
      }
      if (signB == 0 || signA == 0) {
        // Special case: Either operand is zero
        return 0;
      }
      if (this.IsInfinity()) {
        if (other.IsInfinity()) {
          // if we get here, this only means that
          // both are positive infinity or both
          // are negative infinity
          return 0;
        }
        return this.IsNegative ? -1 : 1;
      }
      if (other.IsInfinity()) {
        return other.IsNegative ? 1 : -1;
      }
      // At this point, both numbers are finite and
      // have the same sign
      #if DEBUG
      if (!this.IsFinite) {
        throw new InvalidOperationException("doesn't satisfy this.IsFinite");
      }
      if (!other.IsFinite) {
        throw new InvalidOperationException("doesn't satisfy other.IsFinite");
      }
      #endif

      if (other.Exponent.IsZero) {
        // Special case: other has exponent 0
        EInteger otherMant = other.Mantissa;
        EInteger bcx = this.Denominator * (EInteger)otherMant;
        return this.Numerator.CompareTo(bcx);
      }
      if (other.Exponent.Abs().CompareTo((EInteger)50) > 0) {
        // Other has a high absolute value of exponent, so try different
        // approaches to
        // comparison
        EInteger thisRem;
        EInteger thisInt;
        {
          EInteger[] divrem = this.UnsignedNumerator.DivRem(this.Denominator);
          thisInt = divrem[0];
          thisRem = divrem[1];
        }
        EDecimal otherAbs = other.Abs();
        EDecimal thisIntDec = EDecimal.FromEInteger(thisInt);
        if (thisRem.IsZero) {
          // This object's value is an integer
          // Console.WriteLine("Shortcircuit IV");
          int ret = thisIntDec.CompareTo(otherAbs);
          return this.IsNegative ? -ret : ret;
        }
        if (thisIntDec.CompareTo(otherAbs) > 0) {
          // Truncated absolute value is greater than other's untruncated
          // absolute value
          // Console.WriteLine("Shortcircuit I");
          return this.IsNegative ? -1 : 1;
        }
        // Round up
        thisInt = thisInt.Add(EInteger.One);
        thisIntDec = EDecimal.FromEInteger(thisInt);
        if (thisIntDec.CompareTo(otherAbs) < 0) {
          // Absolute value rounded up is less than other's unrounded
          // absolute value
          // Console.WriteLine("Shortcircuit II");
          return this.IsNegative ? 1 : -1;
        }
        // Conservative approximation of this rational number's absolute value,
        // as a decimal number. The true value will be greater or equal.
        thisIntDec = EDecimal.FromEInteger(this.UnsignedNumerator).Divide(
            EDecimal.FromEInteger(this.Denominator),
            EContext.ForPrecisionAndRounding(20, ERounding.Down));
        if (thisIntDec.CompareTo(otherAbs) > 0) {
          // Truncated absolute value is greater than other's untruncated
          // absolute value
          // Console.WriteLine("Shortcircuit III");
          return this.IsNegative ? -1 : 1;
        }
        // Console.WriteLine("---" + this + " " + other);
        if (other.Exponent.Sign > 0) {
          EInteger bigDigitCount =
            this.UnsignedNumerator.GetDigitCountAsEInteger()
            .Subtract(1);
          if (bigDigitCount.CompareTo(other.Exponent) < 0) {
            // Numerator's digit count minus 1 is less than the other's
            // exponent,
            // and other's exponent is positive, so this value's absolute
            // value is less
            return this.IsNegative ? 1 : -1;
          }
        }
      }
      // Convert to rational number and use usual rational number
      // comparison
      // Console.WriteLine("no shortcircuit");
      // Console.WriteLine(this);
      // Console.WriteLine(other);
      ERational otherRational = ERational.FromEDecimal(other);
      EInteger ad = this.Numerator * (EInteger)otherRational.Denominator;
      EInteger bc = this.Denominator * (EInteger)otherRational.Numerator;
      return ad.CompareTo(bc);
    }

    /// <summary>Returns a number with the same value as this one, but
    /// copying the sign (positive or negative) of another
    /// number.</summary>
    /// <param name='other'>A number whose sign will be copied.</param>
    /// <returns>An arbitrary-precision rational number.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='other'/> is null.</exception>
    public ERational CopySign(ERational other) {
      if (other == null) {
        throw new ArgumentNullException(nameof(other));
      }
      if (this.IsNegative) {
        return other.IsNegative ? this : this.Negate();
      } else {
        return other.IsNegative ? this.Negate() : this;
      }
    }

    /// <summary>Divides this instance by the value of an
    /// arbitrary-precision rational number object.</summary>
    /// <param name='otherValue'>An arbitrary-precision rational
    /// number.</param>
    /// <returns>The quotient of the two objects.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='otherValue'/> is null.</exception>
    public ERational Divide(ERational otherValue) {
      if (otherValue == null) {
        throw new ArgumentNullException(nameof(otherValue));
      }
      if (this.IsSignalingNaN()) {
        return CreateNaN(this.unsignedNumerator, false, this.IsNegative);
      }
      if (otherValue.IsSignalingNaN()) {
        return CreateNaN(
            otherValue.unsignedNumerator,
            false,
            otherValue.IsNegative);
      }
      if (this.IsQuietNaN()) {
        return this;
      }
      if (otherValue.IsQuietNaN()) {
        return otherValue;
      }
      bool resultNeg = this.IsNegative ^ otherValue.IsNegative;
      if (this.IsInfinity()) {
        return otherValue.IsInfinity() ? NaN : (resultNeg ? NegativeInfinity :
            PositiveInfinity);
      }
      if (otherValue.IsInfinity()) {
        return resultNeg ? NegativeZero : Zero;
      }
      if (otherValue.IsZero) {
        return this.IsZero ? NaN : (resultNeg ? NegativeInfinity :
            PositiveInfinity);
      }
      if (this.IsZero) {
        return resultNeg ? NegativeZero : Zero;
      }
      EInteger ad = this.Numerator * (EInteger)otherValue.Denominator;
      EInteger bc = this.Denominator * (EInteger)otherValue.Numerator;
      return new ERational(
          ad.Abs(),
          bc.Abs(),
          resultNeg ? BigNumberFlags.FlagNegative : 0);
    }

    /// <summary>Determines whether this object's numerator, denominator,
    /// and properties are equal to those of another object and that other
    /// object is an arbitrary-precision rational number. Not-a-number
    /// values are considered equal if the rest of their properties are
    /// equal.</summary>
    /// <param name='obj'>The parameter <paramref name='obj'/> is an
    /// arbitrary object.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise,
    /// <c>false</c>. In this method, two objects are not equal if they
    /// don't have the same type or if one is null and the other
    /// isn't.</returns>
    public override bool Equals(object obj) {
      var other = obj as ERational;
      return (
          other != null) && (
          Object.Equals(
            this.unsignedNumerator,
            other.unsignedNumerator) && Object.Equals(
            this.denominator,
            other.denominator) && this.flags == other.flags);
    }

    /// <summary>Determines whether this object's numerator, denominator,
    /// and properties are equal to those of another object. Not-a-number
    /// values are considered equal if the rest of their properties are
    /// equal.</summary>
    /// <param name='other'>An arbitrary-precision rational number to
    /// compare to.</param>
    /// <returns>Either <c>true</c> or <c>false</c>.</returns>
    public bool Equals(ERational other) {
      return this.Equals((object)other);
    }

    /// <summary>Returns the hash code for this instance. No application or
    /// process IDs are used in the hash code calculation.</summary>
    /// <returns>A 32-bit signed integer.</returns>
    public override int GetHashCode() {
      var hashCode = 1857066527;
      unchecked {
        if (this.unsignedNumerator != null) {
          hashCode += 1857066539 * this.unsignedNumerator.GetHashCode();
        }
        if (this.denominator != null) {
          hashCode += 1857066551 * this.denominator.GetHashCode();
        }
        hashCode += 1857066623 * this.flags;
      }
      return hashCode;
    }

    /// <summary>Gets a value indicating whether this object's value is
    /// infinity.</summary>
    /// <returns><c>true</c> if this object's value is infinity; otherwise,
    /// <c>false</c>.</returns>
    public bool IsInfinity() {
      return (this.flags & BigNumberFlags.FlagInfinity) != 0;
    }

    /// <summary>Returns whether this object is a not-a-number
    /// value.</summary>
    /// <returns><c>true</c> if this object is a not-a-number value;
    /// otherwise, <c>false</c>.</returns>
    public bool IsNaN() {
      return (this.flags & BigNumberFlags.FlagNaN) != 0;
    }

    /// <summary>Returns whether this object is negative
    /// infinity.</summary>
    /// <returns><c>true</c> if this object is negative infinity;
    /// otherwise, <c>false</c>.</returns>
    public bool IsNegativeInfinity() {
      return (this.flags & (BigNumberFlags.FlagInfinity |
            BigNumberFlags.FlagNegative)) ==
        (BigNumberFlags.FlagInfinity | BigNumberFlags.FlagNegative);
    }

    /// <summary>Returns whether this object is positive
    /// infinity.</summary>
    /// <returns><c>true</c> if this object is positive infinity;
    /// otherwise, <c>false</c>.</returns>
    public bool IsPositiveInfinity() {
      return (this.flags & (BigNumberFlags.FlagInfinity |
            BigNumberFlags.FlagNegative)) == BigNumberFlags.FlagInfinity;
    }

    /// <summary>Returns whether this object is a quiet not-a-number
    /// value.</summary>
    /// <returns><c>true</c> if this object is a quiet not-a-number value;
    /// otherwise, <c>false</c>.</returns>
    public bool IsQuietNaN() {
      return (this.flags & BigNumberFlags.FlagQuietNaN) != 0;
    }

    /// <summary>Returns whether this object is a signaling not-a-number
    /// value (which causes an error if the value is passed to any
    /// arithmetic operation in this class).</summary>
    /// <returns><c>true</c> if this object is a signaling not-a-number
    /// value (which causes an error if the value is passed to any
    /// arithmetic operation in this class); otherwise, <c>false</c>.</returns>
    public bool IsSignalingNaN() {
      return (this.flags & BigNumberFlags.FlagSignalingNaN) != 0;
    }

    /// <summary>Multiplies this instance by the value of an
    /// arbitrary-precision rational number.</summary>
    /// <param name='otherValue'>An arbitrary-precision rational
    /// number.</param>
    /// <returns>The product of the two numbers.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='otherValue'/> is null.</exception>
    public ERational Multiply(ERational otherValue) {
      if (otherValue == null) {
        throw new ArgumentNullException(nameof(otherValue));
      }
      if (this.IsSignalingNaN()) {
        return CreateNaN(this.unsignedNumerator, false, this.IsNegative);
      }
      if (otherValue.IsSignalingNaN()) {
        return CreateNaN(
            otherValue.unsignedNumerator,
            false,
            otherValue.IsNegative);
      }
      if (this.IsQuietNaN()) {
        return this;
      }
      if (otherValue.IsQuietNaN()) {
        return otherValue;
      }
      bool resultNeg = this.IsNegative ^ otherValue.IsNegative;
      if (this.IsInfinity()) {
        return otherValue.IsZero ? NaN : (resultNeg ? NegativeInfinity :
            PositiveInfinity);
      }
      if (otherValue.IsInfinity()) {
        return this.IsZero ? NaN : (resultNeg ? NegativeInfinity :
            PositiveInfinity);
      }
      EInteger ac = this.Numerator * (EInteger)otherValue.Numerator;
      EInteger bd = this.Denominator * (EInteger)otherValue.Denominator;
      return ac.IsZero ? (resultNeg ? NegativeZero : Zero) :
        new ERational(
          ac.Abs(),
          bd.Abs(),
          resultNeg ? BigNumberFlags.FlagNegative : 0);
    }

    /// <summary>Returns a rational number with the same value as this one
    /// but with the sign reversed.</summary>
    /// <returns>An arbitrary-precision rational number.</returns>
    public ERational Negate() {
      return new ERational(
          this.unsignedNumerator,
          this.denominator,
          this.flags ^ BigNumberFlags.FlagNegative);
    }

    /// <summary>Finds the remainder that results when this instance is
    /// divided by the value of an arbitrary-precision rational
    /// number.</summary>
    /// <param name='otherValue'>An arbitrary-precision rational
    /// number.</param>
    /// <returns>The remainder of the two numbers.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='otherValue'/> is null.</exception>
    public ERational Remainder(ERational otherValue) {
      if (otherValue == null) {
        throw new ArgumentNullException(nameof(otherValue));
      }
      if (this.IsSignalingNaN()) {
        return CreateNaN(this.unsignedNumerator, false, this.IsNegative);
      }
      if (otherValue.IsSignalingNaN()) {
        return CreateNaN(
            otherValue.unsignedNumerator,
            false,
            otherValue.IsNegative);
      }
      if (this.IsQuietNaN()) {
        return this;
      }
      if (otherValue.IsQuietNaN()) {
        return otherValue;
      }
      bool resultNeg = this.IsNegative ^ otherValue.IsNegative;
      if (this.IsInfinity()) {
        return NaN;
      }
      if (otherValue.IsInfinity()) {
        return this;
      }
      if (otherValue.IsZero) {
        return NaN;
      }
      if (this.IsZero) {
        return this;
      }
      EInteger ad = this.Numerator * (EInteger)otherValue.Denominator;
      EInteger bc = this.Denominator * (EInteger)otherValue.Numerator;
      EInteger quo = ad / (EInteger)bc; // Find the integer quotient
      EInteger tnum = quo * (EInteger)otherValue.Numerator;
      EInteger tden = otherValue.Denominator;
      EInteger thisDen = this.Denominator;
      ad = this.Numerator * (EInteger)tden;
      bc = thisDen * (EInteger)tnum;
      tden *= (EInteger)thisDen;
      ad -= (EInteger)bc;
      return new ERational(
          ad.Abs(),
          tden.Abs(),
          resultNeg ? BigNumberFlags.FlagNegative : 0);
    }

    /// <summary>Subtracts an arbitrary-precision rational number from this
    /// instance.</summary>
    /// <param name='otherValue'>An arbitrary-precision rational
    /// number.</param>
    /// <returns>The difference of the two objects.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='otherValue'/> is null.</exception>
    public ERational Subtract(ERational otherValue) {
      if (otherValue == null) {
        throw new ArgumentNullException(nameof(otherValue));
      }
      if (this.IsSignalingNaN()) {
        return CreateNaN(this.unsignedNumerator, false, this.IsNegative);
      }
      if (otherValue.IsSignalingNaN()) {
        return CreateNaN(
            otherValue.unsignedNumerator,
            false,
            otherValue.IsNegative);
      }
      if (this.IsQuietNaN()) {
        return this;
      }
      if (otherValue.IsQuietNaN()) {
        return otherValue;
      }
      if (this.IsInfinity()) {
        if (otherValue.IsInfinity()) {
          return (this.IsNegative != otherValue.IsNegative) ?
            (this.IsNegative ? PositiveInfinity : NegativeInfinity) : NaN;
        }
        return this.IsNegative ? PositiveInfinity : NegativeInfinity;
      }
      if (otherValue.IsInfinity()) {
        return otherValue.IsNegative ? PositiveInfinity : NegativeInfinity;
      }
      EInteger ad = this.Numerator * (EInteger)otherValue.Denominator;
      EInteger bc = this.Denominator * (EInteger)otherValue.Numerator;
      EInteger bd = this.Denominator * (EInteger)otherValue.Denominator;
      ad -= (EInteger)bc;
      return ERational.Create(ad, bd);
    }

    /// <summary>Converts this value to a 64-bit floating-point number. The
    /// half-even rounding mode is used.</summary>
    /// <returns>The closest 64-bit floating-point number to this value.
    /// The return value can be positive infinity or negative infinity if
    /// this value exceeds the range of a 64-bit floating point
    /// number.</returns>
    public double ToDouble() {
      if (!this.IsFinite) {
        return this.ToEFloat(EContext.Binary64).ToDouble();
      }
      if (this.IsNegative && this.IsZero) {
        return EFloat.NegativeZero.ToDouble();
      }
      return EFloat.FromEInteger(this.Numerator)
        .Divide(EFloat.FromEInteger(this.denominator), EContext.Binary64)
        .ToDouble();
    }

    /// <summary>Converts this value to an arbitrary-precision integer by
    /// dividing the numerator by the denominator and discarding the
    /// fractional part of the result.</summary>
    /// <returns>An arbitrary-precision integer.</returns>
    /// <exception cref='OverflowException'>This object's value is infinity
    /// or not-a-number (NaN).</exception>
    public EInteger ToEInteger() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      return this.Numerator / (EInteger)this.denominator;
    }

    /// <summary>Converts this value to an arbitrary-precision integer,
    /// checking whether the value is an exact integer.</summary>
    /// <returns>An arbitrary-precision integer.</returns>
    /// <exception cref='OverflowException'>This object's value is infinity
    /// or not-a-number (NaN).</exception>
    [Obsolete("Renamed to ToEIntegerIfExact.")]
    public EInteger ToEIntegerExact() {
      return this.ToEIntegerIfExact();
    }

    /// <summary>Converts this value to an arbitrary-precision integer,
    /// checking whether the value is an exact integer.</summary>
    /// <returns>An arbitrary-precision integer.</returns>
    /// <exception cref='OverflowException'>This object's value is infinity
    /// or not-a-number (NaN).</exception>
    public EInteger ToEIntegerIfExact() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      EInteger rem;
      EInteger quo;
      {
        EInteger[] divrem = this.Numerator.DivRem(this.denominator);
        quo = divrem[0];
        rem = divrem[1];
      }
      if (!rem.IsZero) {
        throw new ArithmeticException("Value is not an integral value");
      }
      return quo;
    }

    /// <summary>Converts this rational number to an arbitrary-precision
    /// decimal number.</summary>
    /// <returns>The exact value of the rational number, or not-a-number
    /// (NaN) if the result can't be exact because it has a nonterminating
    /// decimal expansion.</returns>
    public EDecimal ToEDecimal() {
      return this.ToEDecimal(null);
    }

    /// <summary>Converts this rational number to an arbitrary-precision
    /// decimal number and rounds the result to the given
    /// precision.</summary>
    /// <param name='ctx'>An arithmetic context object to control the
    /// precision, rounding, and exponent range of the result. If HasFlags
    /// of the context is true, will also store the flags resulting from
    /// the operation (the flags are in addition to the pre-existing
    /// flags). Can be null, in which case the precision is unlimited and
    /// no rounding is needed.</param>
    /// <returns>The value of the rational number, rounded to the given
    /// precision. Returns not-a-number (NaN) if the context is null and
    /// the result can't be exact because it has a nonterminating decimal
    /// expansion.</returns>
    public EDecimal ToEDecimal(EContext ctx) {
      if (this.IsNaN()) {
        return EDecimal.CreateNaN(
            this.unsignedNumerator,
            this.IsSignalingNaN(),
            this.IsNegative,
            ctx);
      }
      if (this.IsPositiveInfinity()) {
        return EDecimal.PositiveInfinity.RoundToPrecision(ctx);
      }
      if (this.IsNegativeInfinity()) {
        return EDecimal.NegativeInfinity.RoundToPrecision(ctx);
      }
      EDecimal ef = (this.IsNegative && this.IsZero) ?
        EDecimal.NegativeZero : EDecimal.FromEInteger(this.Numerator);
      return ef.Divide(EDecimal.FromEInteger(this.Denominator), ctx);
    }

    /// <summary>Converts this rational number to an arbitrary-precision
    /// decimal number, but if the result would have a nonterminating
    /// decimal expansion, rounds that result to the given
    /// precision.</summary>
    /// <param name='ctx'>An arithmetic context object to control the
    /// precision, rounding, and exponent range of the result. This context
    /// will be used only if the exact result would have a nonterminating
    /// decimal expansion. If HasFlags of the context is true, will also
    /// store the flags resulting from the operation (the flags are in
    /// addition to the pre-existing flags). Can be null, in which case the
    /// precision is unlimited and no rounding is needed.</param>
    /// <returns>The exact value of the rational number if possible;
    /// otherwise, the rounded version of the result if a context is given.
    /// Returns not-a-number (NaN) if the context is null and the result
    /// can't be exact because it has a nonterminating decimal
    /// expansion.</returns>
    public EDecimal ToEDecimalExactIfPossible(EContext
      ctx) {
      if (ctx == null) {
        return this.ToEDecimal(null);
      }
      if (this.IsNaN()) {
        return EDecimal.CreateNaN(
            this.unsignedNumerator,
            this.IsSignalingNaN(),
            this.IsNegative,
            ctx);
      }
      if (this.IsPositiveInfinity()) {
        return EDecimal.PositiveInfinity.RoundToPrecision(ctx);
      }
      if (this.IsNegativeInfinity()) {
        return EDecimal.NegativeInfinity.RoundToPrecision(ctx);
      }
      if (this.IsNegative && this.IsZero) {
        return EDecimal.NegativeZero;
      }
      EDecimal valueEdNum = (this.IsNegative && this.IsZero) ?
        EDecimal.NegativeZero : EDecimal.FromEInteger(this.Numerator);
      EDecimal valueEdDen = EDecimal.FromEInteger(this.Denominator);
      EDecimal ed = valueEdNum.Divide(valueEdDen, null);
      if (ed.IsNaN()) {
        // Result would be inexact, try again using the precision context
        ed = valueEdNum.Divide(valueEdDen, ctx);
      }
      return ed;
    }

    /// <summary>Converts this rational number to an arbitrary-precision
    /// decimal number.</summary>
    /// <returns>The exact value of the rational number, or not-a-number
    /// (NaN) if the result can't be exact because it has a nonterminating
    /// decimal expansion.</returns>
    [Obsolete("Renamed to ToEDecimal.")]
    public EDecimal ToExtendedDecimal() {
      return this.ToEDecimal();
    }

    /// <summary>Converts this rational number to an arbitrary-precision
    /// decimal number and rounds the result to the given
    /// precision.</summary>
    /// <param name='ctx'>An arithmetic context object to control the
    /// precision, rounding, and exponent range of the result. If HasFlags
    /// of the context is true, will also store the flags resulting from
    /// the operation (the flags are in addition to the pre-existing
    /// flags). Can be null, in which case the precision is unlimited and
    /// no rounding is needed.</param>
    /// <returns>The value of the rational number, rounded to the given
    /// precision. Returns not-a-number (NaN) if the context is null and
    /// the result can't be exact because it has a nonterminating decimal
    /// expansion.</returns>
    [Obsolete("Renamed to ToEDecimal.")]
    public EDecimal ToExtendedDecimal(EContext ctx) {
      return this.ToEDecimal(ctx);
    }

    /// <summary>Converts this rational number to an arbitrary-precision
    /// decimal number, but if the result would have a nonterminating
    /// decimal expansion, rounds that result to the given
    /// precision.</summary>
    /// <param name='ctx'>An arithmetic context object to control the
    /// precision, rounding, and exponent range of the result. This context
    /// will be used only if the exact result would have a nonterminating
    /// decimal expansion. If HasFlags of the context is true, will also
    /// store the flags resulting from the operation (the flags are in
    /// addition to the pre-existing flags). Can be null, in which case the
    /// precision is unlimited and no rounding is needed.</param>
    /// <returns>The exact value of the rational number if possible;
    /// otherwise, the rounded version of the result if a context is given.
    /// Returns not-a-number (NaN) if the context is null and the result
    /// can't be exact because it has a nonterminating decimal
    /// expansion.</returns>
    [Obsolete("Renamed to ToEDecimalExactIfPossible.")]
    public EDecimal ToExtendedDecimalExactIfPossible(EContext ctx) {
      return this.ToEDecimalExactIfPossible(ctx);
    }

    /// <summary>Converts this rational number to a binary floating-point
    /// number.</summary>
    /// <returns>The exact value of the rational number, or not-a-number
    /// (NaN) if the result can't be exact because it has a nonterminating
    /// binary expansion.</returns>
    public EFloat ToEFloat() {
      return this.ToEFloat(null);
    }

    /// <summary>Converts this rational number to a binary floating-point
    /// number and rounds that result to the given precision.</summary>
    /// <param name='ctx'>An arithmetic context object to control the
    /// precision, rounding, and exponent range of the result. If HasFlags
    /// of the context is true, will also store the flags resulting from
    /// the operation (the flags are in addition to the pre-existing
    /// flags). Can be null, in which case the precision is unlimited and
    /// no rounding is needed.</param>
    /// <returns>The value of the rational number, rounded to the given
    /// precision. Returns not-a-number (NaN) if the context is null and
    /// the result can't be exact because it has a nonterminating binary
    /// expansion.</returns>
    public EFloat ToEFloat(EContext ctx) {
      if (this.IsNaN()) {
        return EFloat.CreateNaN(
            this.unsignedNumerator,
            this.IsSignalingNaN(),
            this.IsNegative,
            ctx);
      }
      if (this.IsPositiveInfinity()) {
        return EFloat.PositiveInfinity.RoundToPrecision(ctx);
      }
      if (this.IsNegativeInfinity()) {
        return EFloat.NegativeInfinity.RoundToPrecision(ctx);
      }
      EFloat ef = (this.IsNegative && this.IsZero) ?
        EFloat.NegativeZero : EFloat.FromEInteger(this.Numerator);
      return ef.Divide(EFloat.FromEInteger(this.Denominator), ctx);
    }

    /// <summary>Converts this rational number to a binary floating-point
    /// number, but if the result would have a nonterminating binary
    /// expansion, rounds that result to the given precision.</summary>
    /// <param name='ctx'>An arithmetic context object to control the
    /// precision, rounding, and exponent range of the result. This context
    /// will be used only if the exact result would have a nonterminating
    /// binary expansion. If HasFlags of the context is true, will also
    /// store the flags resulting from the operation (the flags are in
    /// addition to the pre-existing flags). Can be null, in which case the
    /// precision is unlimited and no rounding is needed.</param>
    /// <returns>The exact value of the rational number if possible;
    /// otherwise, the rounded version of the result if a context is given.
    /// Returns not-a-number (NaN) if the context is null and the result
    /// can't be exact because it has a nonterminating binary
    /// expansion.</returns>
    public EFloat ToEFloatExactIfPossible(EContext ctx) {
      if (ctx == null) {
        return this.ToEFloat(null);
      }
      if (this.IsNaN()) {
        return EFloat.CreateNaN(
            this.unsignedNumerator,
            this.IsSignalingNaN(),
            this.IsNegative,
            ctx);
      }
      if (this.IsPositiveInfinity()) {
        return EFloat.PositiveInfinity.RoundToPrecision(ctx);
      }
      if (this.IsNegativeInfinity()) {
        return EFloat.NegativeInfinity.RoundToPrecision(ctx);
      }
      if (this.IsZero) {
        return this.IsNegative ? EFloat.NegativeZero :
          EFloat.Zero;
      }
      EFloat valueEdNum = (this.IsNegative && this.IsZero) ?
        EFloat.NegativeZero : EFloat.FromEInteger(this.Numerator);
      EFloat valueEdDen = EFloat.FromEInteger(this.Denominator);
      EFloat ed = valueEdNum.Divide(valueEdDen, null);
      if (ed.IsNaN()) {
        // Result would be inexact, try again using the precision context
        ed = valueEdNum.Divide(valueEdDen, ctx);
      }
      return ed;
    }

    /// <summary>Converts this rational number to a binary floating-point
    /// number.</summary>
    /// <returns>The exact value of the rational number, or not-a-number
    /// (NaN) if the result can't be exact because it has a nonterminating
    /// binary expansion.</returns>
    [Obsolete("Renamed to ToEFloat.")]
    public EFloat ToExtendedFloat() {
      return this.ToEFloat();
    }

    /// <summary>Converts this rational number to a binary floating-point
    /// number and rounds that result to the given precision.</summary>
    /// <param name='ctx'>An arithmetic context object to control the
    /// precision, rounding, and exponent range of the result. If HasFlags
    /// of the context is true, will also store the flags resulting from
    /// the operation (the flags are in addition to the pre-existing
    /// flags). Can be null, in which case the precision is unlimited and
    /// no rounding is needed.</param>
    /// <returns>The value of the rational number, rounded to the given
    /// precision. Returns not-a-number (NaN) if the context is null and
    /// the result can't be exact because it has a nonterminating binary
    /// expansion.</returns>
    [Obsolete("Renamed to ToEFloat.")]
    public EFloat ToExtendedFloat(EContext ctx) {
      return this.ToEFloat(ctx);
    }

    /// <summary>Converts this rational number to a binary floating-point
    /// number, but if the result would have a nonterminating binary
    /// expansion, rounds that result to the given precision.</summary>
    /// <param name='ctx'>An arithmetic context object to control the
    /// precision, rounding, and exponent range of the result. This context
    /// will be used only if the exact result would have a nonterminating
    /// binary expansion. If HasFlags of the context is true, will also
    /// store the flags resulting from the operation (the flags are in
    /// addition to the pre-existing flags). Can be null, in which case the
    /// precision is unlimited and no rounding is needed.</param>
    /// <returns>The exact value of the rational number if possible;
    /// otherwise, the rounded version of the result if a context is given.
    /// Returns not-a-number (NaN) if the context is null and the result
    /// can't be exact because it has a nonterminating binary
    /// expansion.</returns>
    [Obsolete("Renamed to ToEFloatExactIfPossible.")]
    public EFloat ToExtendedFloatExactIfPossible(EContext ctx) {
      return this.ToEFloatExactIfPossible(ctx);
    }

    /// <summary>Converts this value to a 32-bit binary floating-point
    /// number. The half-even rounding mode is used.</summary>
    /// <returns>The closest 32-bit binary floating-point number to this
    /// value. The return value can be positive infinity or negative
    /// infinity if this value exceeds the range of a 32-bit floating point
    /// number.</returns>
    public float ToSingle() {
      if (!this.IsFinite) {
        return this.ToEFloat(EContext.Binary32).ToSingle();
      }
      if (this.IsNegative && this.IsZero) {
        return EFloat.NegativeZero.ToSingle();
      }
      return EFloat.FromEInteger(this.Numerator)
        .Divide(EFloat.FromEInteger(this.denominator), EContext.Binary32)
        .ToSingle();
    }

    /// <summary>Converts this object to a text string.</summary>
    /// <returns>A string representation of this object. If this object's
    /// value is infinity or not-a-number, the result is the analogous
    /// return value of the <c>EDecimal.ToString</c> method. Otherwise, the
    /// return value has the following form:
    /// <c>[-]numerator/denominator</c>.</returns>
    public override string ToString() {
      if (!this.IsFinite) {
        if (this.IsSignalingNaN()) {
          if (this.unsignedNumerator.IsZero) {
            return this.IsNegative ? "-sNaN" : "sNaN";
          }
          return this.IsNegative ? "-sNaN" + this.unsignedNumerator :
            "sNaN" + this.unsignedNumerator;
        }
        if (this.IsQuietNaN()) {
          if (this.unsignedNumerator.IsZero) {
            return this.IsNegative ? "-NaN" : "NaN";
          }
          return this.IsNegative ? "-NaN" + this.unsignedNumerator :
            "NaN" + this.unsignedNumerator;
        }
        if (this.IsInfinity()) {
          return this.IsNegative ? "-Infinity" : "Infinity";
        }
      }
      return (this.Numerator.IsZero && this.IsNegative) ? ("-0/" +
          this.Denominator) : (this.Numerator + "/" + this.Denominator);
    }

    /// <summary>Adds one to an arbitrary-precision rational
    /// number.</summary>
    /// <returns>The given arbitrary-precision rational number plus
    /// one.</returns>
    public ERational Increment() {
      return this.Add(FromInt32(1));
    }

    /// <summary>Subtracts one from an arbitrary-precision rational
    /// number.</summary>
    /// <returns>The given arbitrary-precision rational number minus
    /// one.</returns>
    public ERational Decrement() {
      return this.Subtract(FromInt32(1));
    }

    /// <summary>Returns the sum of a rational number and a 32-bit signed
    /// integer.</summary>
    /// <param name='v'>A 32-bit signed integer.</param>
    /// <returns>The sum of the two numbers. Returns not-a-number (NaN) if
    /// this object is NaN.</returns>
    public ERational Add(int v) {
      return this.Add(FromInt32(v));
    }

    /// <summary>Returns the result of subtracting a 32-bit signed integer
    /// from this instance.</summary>
    /// <param name='v'>The parameter <paramref name='v'/> is a 32-bit
    /// signed integer.</param>
    /// <returns>The difference of the two objects.</returns>
    public ERational Subtract(int v) {
      return this.Subtract(FromInt32(v));
    }

    /// <summary>Returns the value of this instance multiplied by a 32-bit
    /// signed integer.</summary>
    /// <param name='v'>The parameter <paramref name='v'/> is a 32-bit
    /// signed integer.</param>
    /// <returns>The product of the two numbers.</returns>
    public ERational Multiply(int v) {
      return this.Multiply(FromInt32(v));
    }

    /// <summary>Divides this instance by the value of an
    /// arbitrary-precision rational number object.</summary>
    /// <param name='v'>The parameter <paramref name='v'/> is a 32-bit
    /// signed integer.</param>
    /// <returns>The quotient of the two objects.</returns>
    /// <exception cref='ArithmeticException'>The parameter <paramref
    /// name='v'/> is zero.</exception>
    public ERational Divide(int v) {
      return this.Divide(FromInt32(v));
    }

    /// <summary>Finds the remainder that results when this instance is
    /// divided by the value of an arbitrary-precision rational
    /// number.</summary>
    /// <param name='v'>The divisor.</param>
    /// <returns>The remainder of the two numbers.</returns>
    /// <exception cref='ArgumentException'>The parameter <paramref
    /// name='v'/> is zero.</exception>
    public ERational Remainder(int v) {
      return this.Remainder(FromInt32(v));
    }

    // Begin integer conversions

    /// <summary>Converts this number's value to a byte (from 0 to 255) if
    /// it can fit in a byte (from 0 to 255) after converting it to an
    /// integer by discarding its fractional part.</summary>
    /// <returns>This number's value, truncated to a byte (from 0 to
    /// 255).</returns>
    /// <exception cref='OverflowException'>This value is infinity or
    /// not-a-number, or the number, once converted to an integer by
    /// discarding its fractional part, is less than 0 or greater than
    /// 255.</exception>
    public byte ToByteChecked() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      return this.IsZero ? ((byte)0) : this.ToEInteger().ToByteChecked();
    }

    /// <summary>Converts this number's value to an integer by discarding
    /// its fractional part, and returns the least-significant bits of its
    /// two's-complement form as a byte (from 0 to 255).</summary>
    /// <returns>This number, converted to a byte (from 0 to 255). Returns
    /// 0 if this value is infinity or not-a-number.</returns>
    public byte ToByteUnchecked() {
      return this.IsFinite ? this.ToEInteger().ToByteUnchecked() : (byte)0;
    }

    /// <summary>Converts this number's value to a byte (from 0 to 255) if
    /// it can fit in a byte (from 0 to 255) without rounding to a
    /// different numerical value.</summary>
    /// <returns>This number's value as a byte (from 0 to 255).</returns>
    /// <exception cref='ArithmeticException'>This value is infinity or
    /// not-a-number, is not an exact integer, or is less than 0 or greater
    /// than 255.</exception>
    public byte ToByteIfExact() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      return this.IsZero ? ((byte)0) : this.ToEIntegerIfExact().ToByteChecked();
    }

    /// <summary>Converts a byte (from 0 to 255) to an arbitrary-precision
    /// rational number.</summary>
    /// <param name='inputByte'>The number to convert as a byte (from 0 to
    /// 255).</param>
    /// <returns>This number's value as an arbitrary-precision rational
    /// number.</returns>
    public static ERational FromByte(byte inputByte) {
      int val = ((int)inputByte) & 0xff;
      return FromInt32(val);
    }

    /// <summary>Converts this number's value to a 16-bit signed integer if
    /// it can fit in a 16-bit signed integer after converting it to an
    /// integer by discarding its fractional part.</summary>
    /// <returns>This number's value, truncated to a 16-bit signed
    /// integer.</returns>
    /// <exception cref='OverflowException'>This value is infinity or
    /// not-a-number, or the number, once converted to an integer by
    /// discarding its fractional part, is less than -32768 or greater than
    /// 32767.</exception>
    public short ToInt16Checked() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      return this.IsZero ? ((short)0) : this.ToEInteger().ToInt16Checked();
    }

    /// <summary>Converts this number's value to an integer by discarding
    /// its fractional part, and returns the least-significant bits of its
    /// two's-complement form as a 16-bit signed integer.</summary>
    /// <returns>This number, converted to a 16-bit signed integer. Returns
    /// 0 if this value is infinity or not-a-number.</returns>
    public short ToInt16Unchecked() {
      return this.IsFinite ? this.ToEInteger().ToInt16Unchecked() : (short)0;
    }

    /// <summary>Converts this number's value to a 16-bit signed integer if
    /// it can fit in a 16-bit signed integer without rounding to a
    /// different numerical value.</summary>
    /// <returns>This number's value as a 16-bit signed integer.</returns>
    /// <exception cref='ArithmeticException'>This value is infinity or
    /// not-a-number, is not an exact integer, or is less than -32768 or
    /// greater than 32767.</exception>
    public short ToInt16IfExact() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      return this.IsZero ? ((short)0) :
        this.ToEIntegerIfExact().ToInt16Checked();
    }

    /// <summary>Converts a 16-bit signed integer to an arbitrary-precision
    /// rational number.</summary>
    /// <param name='inputInt16'>The number to convert as a 16-bit signed
    /// integer.</param>
    /// <returns>This number's value as an arbitrary-precision rational
    /// number.</returns>
    public static ERational FromInt16(short inputInt16) {
      var val = (int)inputInt16;
      return FromInt32(val);
    }

    /// <summary>Converts this number's value to a 32-bit signed integer if
    /// it can fit in a 32-bit signed integer after converting it to an
    /// integer by discarding its fractional part.</summary>
    /// <returns>This number's value, truncated to a 32-bit signed
    /// integer.</returns>
    /// <exception cref='OverflowException'>This value is infinity or
    /// not-a-number, or the number, once converted to an integer by
    /// discarding its fractional part, is less than -2147483648 or greater
    /// than 2147483647.</exception>
    public int ToInt32Checked() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      return this.IsZero ? ((int)0) : this.ToEInteger().ToInt32Checked();
    }

    /// <summary>Converts this number's value to an integer by discarding
    /// its fractional part, and returns the least-significant bits of its
    /// two's-complement form as a 32-bit signed integer.</summary>
    /// <returns>This number, converted to a 32-bit signed integer. Returns
    /// 0 if this value is infinity or not-a-number.</returns>
    public int ToInt32Unchecked() {
      return this.IsFinite ? this.ToEInteger().ToInt32Unchecked() : (int)0;
    }

    /// <summary>Converts this number's value to a 32-bit signed integer if
    /// it can fit in a 32-bit signed integer without rounding to a
    /// different numerical value.</summary>
    /// <returns>This number's value as a 32-bit signed integer.</returns>
    /// <exception cref='ArithmeticException'>This value is infinity or
    /// not-a-number, is not an exact integer, or is less than -2147483648
    /// or greater than 2147483647.</exception>
    public int ToInt32IfExact() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      return this.IsZero ? ((int)0) : this.ToEIntegerIfExact().ToInt32Checked();
    }

    /// <summary>Converts a boolean value (true or false) to an
    /// arbitrary-precision rational number.</summary>
    /// <param name='boolValue'>Either true or false.</param>
    /// <returns>The number 1 if <paramref name='boolValue'/> is true;
    /// otherwise, 0.</returns>
    public static ERational FromBoolean(bool boolValue) {
      return FromInt32(boolValue ? 1 : 0);
    }

    /// <summary>Converts a 32-bit signed integer to an arbitrary-precision
    /// rational number.</summary>
    /// <param name='inputInt32'>The number to convert as a 32-bit signed
    /// integer.</param>
    /// <returns>This number's value as an arbitrary-precision rational
    /// number.</returns>
    public static ERational FromInt32(int inputInt32) {
      return FromEInteger(EInteger.FromInt32(inputInt32));
    }

    /// <summary>Converts this number's value to a 64-bit signed integer if
    /// it can fit in a 64-bit signed integer after converting it to an
    /// integer by discarding its fractional part.</summary>
    /// <returns>This number's value, truncated to a 64-bit signed
    /// integer.</returns>
    /// <exception cref='OverflowException'>This value is infinity or
    /// not-a-number, or the number, once converted to an integer by
    /// discarding its fractional part, is less than -9223372036854775808
    /// or greater than 9223372036854775807.</exception>
    public long ToInt64Checked() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      return this.IsZero ? 0L : this.ToEInteger().ToInt64Checked();
    }

    /// <summary>Converts this number's value to an integer by discarding
    /// its fractional part, and returns the least-significant bits of its
    /// two's-complement form as a 64-bit signed integer.</summary>
    /// <returns>This number, converted to a 64-bit signed integer. Returns
    /// 0 if this value is infinity or not-a-number.</returns>
    public long ToInt64Unchecked() {
      return this.IsFinite ? this.ToEInteger().ToInt64Unchecked() : 0L;
    }

    /// <summary>Converts this number's value to a 64-bit signed integer if
    /// it can fit in a 64-bit signed integer without rounding to a
    /// different numerical value.</summary>
    /// <returns>This number's value as a 64-bit signed integer.</returns>
    /// <exception cref='ArithmeticException'>This value is infinity or
    /// not-a-number, is not an exact integer, or is less than
    /// -9223372036854775808 or greater than
    /// 9223372036854775807.</exception>
    public long ToInt64IfExact() {
      if (!this.IsFinite) {
        throw new OverflowException("Value is infinity or NaN");
      }
      return this.IsZero ? 0L : this.ToEIntegerIfExact().ToInt64Checked();
    }

    /// <summary>Converts a 64-bit signed integer to an arbitrary-precision
    /// rational number.</summary>
    /// <param name='inputInt64'>The number to convert as a 64-bit signed
    /// integer.</param>
    /// <returns>This number's value as an arbitrary-precision rational
    /// number.</returns>
    public static ERational FromInt64(long inputInt64) {
      return FromEInteger(EInteger.FromInt64(inputInt64));
    }

    // End integer conversions
  }
}
