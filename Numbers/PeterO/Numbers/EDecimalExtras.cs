using System;

namespace PeterO.Numbers {
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="T:PeterO.Numbers.EDecimalExtras"]/*'/>
  public static class EDecimalExtras {
    private const int DecimalRadix = 10;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.Radix(PeterO.Numbers.EContext)"]/*'/>
    public static EDecimal Radix(EContext ec) {
      return EDecimal.FromInt32(DecimalRadix).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.Int32ToEDecimal(System.Int32,PeterO.Numbers.EContext)"]/*'/>
    public static EDecimal Int32ToEDecimal(int i32, EContext ec) {
      // NOTE: Not a miscellaneous operation in the General Decimal
      // Arithmetic Specification 1.70, but required since some of the
      // miscellaneous operations here return integers
      return EDecimal.FromInt32(i32).RoundToPrecision(ec);
    }

/// <include file='../../docs.xml'
    /// <param name='b'>The parameter <paramref name='b'/> is not
    /// documented yet.</param>
    /// <param name='ec'>The parameter <paramref name='ec'/> is not
    /// documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    [Obsolete]
    public static EDecimal BoolToEDecimal(bool b, EContext ec) {
      return EDecimal.FromInt32(b ? 1 : 0).RoundToPrecision(ec);
    }

    /// <summary>Converts a boolean value (either true or false) to an
    /// arbitrary-precision decimal number.</summary>
    /// <param name='b'>Either true or false.</param>
    /// <param name='ec'>A context used for rounding the result. Can be
    /// null.</param>
    /// <returns>Either 1 if <paramref name='b'/> is true, or 0 if
    /// <paramref name='b'/> is false.. The result will be rounded as
    /// specified by the given context, if any.</returns>
    public static EDecimal BooleanToEDecimal(bool b, EContext ec) {
      // NOTE: Not a miscellaneous operation in the General Decimal
      // Arithmetic Specification 1.70, but required since some of the
      // miscellaneous operations here return booleans
      return EDecimal.FromInt32(b ? 1 : 0).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsCanonical(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsCanonical(EDecimal ed) {
      return true;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsFinite(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsFinite(EDecimal ed) {
      return ed != null && ed.IsFinite;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsInfinite(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsInfinite(EDecimal ed) {
      return ed != null && ed.IsInfinity();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsNaN(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsNaN(EDecimal ed) {
      return ed != null && ed.IsNaN();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsNormal(PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static bool IsNormal(EDecimal ed, EContext ec) {
      return ed != null && ed.IsFinite && !ed.IsZero && !IsSubnormal(ed, ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsQuietNaN(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsQuietNaN(EDecimal ed) {
      return ed != null && ed.IsQuietNaN();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsSigned(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsSigned(EDecimal ed) {
      return ed != null && ed.IsNegative;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsSignalingNaN(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsSignalingNaN(EDecimal ed) {
      return ed != null && ed.IsSignalingNaN();
    }

    private static readonly string[] NumberClasses = {
 "+Normal", "-Normal",
 "+Subnormal", "-Subnormal",
 "+Zero", "-Zero",
 "+Infinity", "-Infinity",
 "NaN", "sNaN"
};

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.NumberClassString(System.Int32)"]/*'/>
    public static string NumberClassString(int nc) {
      if (nc < 0) {
   throw new ArgumentException("nc (" + nc +
          ") is not greater or equal to 0");
      }
      if (nc > 9) {
      throw new ArgumentException("nc (" + nc +
          ") is not less or equal to 9");
      }
      return NumberClasses[nc];
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.NumberClass(PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static int NumberClass(EDecimal ed, EContext ec) {
      if (ed == null) {
        throw new ArgumentNullException(nameof(ed));
      }
      if (ed.IsQuietNaN()) {
        return 8;
      }
      if (ed.IsNaN()) {
        return 9;
      }
      if (ed.IsInfinity()) {
        return ed.IsNegative ? 7 : 6;
      }
      if (ed.IsZero) {
        return ed.IsNegative ? 5 : 4;
      }
      return IsSubnormal(ed, ec) ? (ed.IsNegative ? 3 : 2) :
        (ed.IsNegative ? 1 : 0);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsSubnormal(PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static bool IsSubnormal(EDecimal ed, EContext ec) {
      if (ed.IsFinite && ec != null && !ed.IsZero && ec.HasExponentRange) {
        if (ec.AdjustExponent) {
          return ed.Exponent.Add(ed.Precision().Subtract(1)).CompareTo(
             ec.EMin) < 0;
        } else {
          return ed.Exponent.CompareTo(ec.EMin) < 0;
        }
      }
      return false;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsZero(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsZero(EDecimal ed) {
      return ed != null && ed.IsZero;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.LogB(PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static EDecimal LogB(EDecimal ed, EContext ec) {
      if ((ed) == null) {
  throw new ArgumentNullException(nameof(ed));
}
      if (ed.IsNaN()) {
        return ed.RoundToPrecision(ec);
      }
      if (ed.IsInfinity()) {
        return EDecimal.PositiveInfinity;
      }
      if (ed.IsZero) {
        return EDecimal.FromInt32(-1).Divide(EDecimal.Zero, ec);
      }
      EInteger ei = ed.Exponent.Add(ed.Precision().Subtract(1));
      return EDecimal.FromEInteger(ei).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.ScaleB(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static EDecimal ScaleB(EDecimal ed, EDecimal ed2, EContext ec) {
      if (ed == null) {
        throw new ArgumentNullException(nameof(ed));
      }
      if (ed2 == null) {
        throw new ArgumentNullException(nameof(ed2));
      }
      if (ed.IsNaN() || ed2.IsNaN()) {
        return ed.Add(ed2, ec);
      }
      if (!ed2.IsFinite || ed2.Exponent.Sign != 0) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      EInteger scale = ed2.Mantissa;
      if (ec != null && ec.HasMaxPrecision) {
        EInteger exp = ec.EMax.Add(ec.Precision).Multiply(2);
        if (scale.Abs().CompareTo(exp.Abs()) > 0) {
          return InvalidOperation(EDecimal.NaN, ec);
        }
      }
      if (ed.IsInfinity()) {
        return ed;
      }
      if (scale.IsZero) {
        return ed.RoundToPrecision(ec);
      }
      EDecimal ret = EDecimal.Create(
         ed.UnsignedMantissa,
         ed.Exponent.Add(scale));
      if (ed.IsNegative) {
        ret = ret.Negate();
      }
      return ret.RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.Shift(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static EDecimal Shift(EDecimal ed, EDecimal ed2, EContext ec) {
      if (ed == null) {
        throw new ArgumentNullException(nameof(ed));
      }
      if (ed2 == null) {
        throw new ArgumentNullException(nameof(ed2));
      }
      if (ed.IsNaN() || ed2.IsNaN()) {
        return ed.Add(ed2, ec);
      }
      if (!ed2.IsFinite || ed2.Exponent.Sign != 0) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      EInteger shift = ed2.Mantissa;
      if (ec != null) {
        if (shift.Abs().CompareTo(ec.Precision) > 0) {
          return InvalidOperation(EDecimal.NaN, ec);
        }
      }
      if (ed.IsInfinity()) {
        // NOTE: Must check for validity of second
        // parameter first, before checking if first
        // parameter is infinity here
        return ed;
      }
      EInteger mant = ed.UnsignedMantissa;
      if (mant.IsZero) {
        return ed.RoundToPrecision(ec);
      }
      EInteger mantprec = ed.Precision();
      EInteger radix = EInteger.FromInt32(DecimalRadix);
      if (shift.Sign < 0) {
        if (shift.Abs().CompareTo(mantprec) < 0) {
          EInteger divisor = radix.Pow(shift.Abs());
          mant = mant.Divide(divisor);
        } else {
          mant = EInteger.Zero;
        }
        EDecimal ret = EDecimal.Create(mant, ed.Exponent);
        return ed.IsNegative ? ret.Negate() : ret;
      } else {
        EInteger mult = radix.Pow(shift);
        mant = mant.Multiply(mult);
        if (ec != null && ec.HasMaxPrecision) {
          EInteger mod = radix.Pow(ec.Precision);
          mant = mant.Remainder(mod);
        }
        EDecimal ret = EDecimal.Create(mant, ed.Exponent);
        return ed.IsNegative ? ret.Negate() : ret;
      }
    }

    /// <summary>Rotates the digits of an arbitrary-precision decimal
    /// number's mantissa.</summary>
    /// <param name='ed'>An arbitrary-precision number containing the
    /// mantissa to rotate. If this mantissa contains more digits than the
    /// precision, the most-significant digits are chopped off the
    /// mantissa.</param>
    /// <param name='ed2'>An arbitrary-precision number indicating the
    /// number of digits to rotate the first operand's mantissa. Must be an
    /// integer with an exponent of 0. If this parameter is positive, the
    /// mantissa is shifted by the given number of digits and the
    /// most-significant digits shifted out of the mantissa become the
    /// least-significant digits instead. If this parameter is negative,
    /// the number is shifted by the given number of digits and the
    /// least-significant digits shifted out of the mantissa become the
    /// most-significant digits instead.</param>
    /// <param name='ec'>A context that specifies the precision of
    /// arbitrary-precision numbers. If this parameter is null or specifies
    /// an unlimited precision, this method has the same behavior as
    /// <c>Shift</c>.</param>
    /// <returns>An arbitrary-precision decimal number whose mantissa is
    /// rotated the given number of bits. Signals an invalid operation and
    /// returns NaN (not-a-number) if <paramref name='ed2'/> is a signaling
    /// NaN or if <paramref name='ed2'/> is not an integer, is negative,
    /// has an exponent other than 0, or has an absolute value that exceeds
    /// the maximum precision specified in the context.</returns>
    public static EDecimal Rotate(EDecimal ed, EDecimal ed2, EContext ec) {
      if (ec == null || !ec.HasMaxPrecision) {
        return Shift(ed, ed2, ec);
      }
      if (ed.IsNaN() || ed2.IsNaN()) {
        return ed.Add(ed2, ec);
      }
      if (!ed2.IsFinite || ed2.Exponent.Sign != 0) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      EInteger shift = ed2.Mantissa;
      if (shift.Abs().CompareTo(ec.Precision) > 0) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      if (ed.IsInfinity()) {
        // NOTE: Must check for validity of second
        // parameter first, before checking if first
        // parameter is infinity here
        return ed;
      }
      EInteger mant = ed.UnsignedMantissa;
      EInteger mantprec = ed.Precision();
if (ec != null && ec.HasMaxPrecision && mantprec.CompareTo(ec.Precision) >
        0) {
     mant =
  mant.Remainder(EInteger.FromInt32(DecimalRadix).Pow(ec.Precision));
        mantprec = ec.Precision;
      }
      if (mant.IsZero) {
        return ed.RoundToPrecision(ec);
      }
      EInteger rightShift = shift.Sign < 0 ? shift.Abs() :
        ec.Precision.Subtract(shift);
      EInteger leftShift = ec.Precision.Subtract(rightShift);
      EInteger mantRight = EInteger.Zero;
      EInteger mantLeft = EInteger.Zero;
      EInteger radix = EInteger.FromInt32(DecimalRadix);
      // Right shift
      if (rightShift.CompareTo(mantprec) < 0) {
        EInteger divisor = radix.Pow(rightShift);
        mantRight = mant.Divide(divisor);
      } else {
        mantRight = EInteger.Zero;
      }
      // Left shift
      if (leftShift.IsZero) {
        mantLeft = mant;
      } else if (leftShift.CompareTo(ec.Precision) == 0) {
        mantLeft = EInteger.Zero;
      } else {
        EInteger mult = radix.Pow(leftShift);
        mantLeft = mant.Multiply(mult);
        EInteger mod = radix.Pow(ec.Precision);
        mantLeft = mantLeft.Remainder(mod);
      }
      EDecimal ret = EDecimal.Create(mantRight.Add(mantLeft), ed.Exponent);
      return ed.IsNegative ? ret.Negate() : ret;
    }

    /// <summary>Compares the values of one arbitrary-precision number
    /// object and another object, imposing a total ordering on all
    /// possible values. In this method:
    /// <list>
    /// <item>For objects with the same value, the one with the higher
    /// exponent has a greater "absolute value".</item>
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
    /// <param name='ed'>The parameter <paramref name='ed'/> is not
    /// documented yet.</param>
    /// <param name='other'>The parameter <paramref name='other'/> is not
    /// documented yet.</param>
    /// <param name='ec'>The parameter <paramref name='ec'/> is not
    /// documented yet.</param>
    /// <returns>The number 0 if both objects have the same value, or -1 if
    /// the first object is less than the other value, or 1 if the first
    /// object is greater. Does not signal flags if either value is
    /// signaling NaN.</returns>
    public static int CompareTotal(EDecimal ed, EDecimal other, EContext ec) {
      return ed.CompareToTotal(other, ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.CompareTotalMagnitude(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static int CompareTotalMagnitude(
  EDecimal ed,
  EDecimal other,
  EContext ec) {
      return ed.CompareToTotalMagnitude(other, ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.Copy(PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal Copy(EDecimal ed) {
      return ed.Copy();
    }

    /// <summary>Returns a canonical version of the given
    /// arbitrary-precision number object. In this method, this is the same
    /// as that object.</summary>
    /// <param name='ed'>An arbitrary-precision number object.</param>
    /// <returns>The parameter <paramref name='ed'/>.</returns>
    public static EDecimal Canonical(EDecimal ed) {
      return Copy(ed);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.CopyAbs(PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal CopyAbs(EDecimal ed) {
      return Copy(ed.Abs());
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.CopyNegate(PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal CopyNegate(EDecimal ed) {
      return Copy(ed.Negate());
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.CopySign(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal CopySign(EDecimal ed, EDecimal other) {
      return ed.IsNegative == other.IsNegative ? Copy(ed) : CopyNegate(ed);
    }

    private static EDecimal InvalidOperation(EDecimal ed, EContext ec) {
      if (ec != null) {
        if (ec.HasFlags) {
          ec.Flags |= EContext.FlagInvalid;
        }
        if ((ec.Traps & EContext.FlagInvalid) != 0) {
          throw new ETrapException(EContext.FlagInvalid, ec, ed);
        }
      }
      return ed;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.SameQuantum(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal)"]/*'/>
    public static bool SameQuantum(EDecimal ed1, EDecimal ed2) {
      if (ed1 == null || ed2 == null) {
        return false;
      }
      if (ed1.IsFinite && ed2.IsFinite) {
        return ed1.Exponent.Equals(ed2.Exponent);
      } else {
 return (ed1.IsNaN() && ed2.IsNaN()) || (ed1.IsInfinity() &&
          ed2.IsInfinity());
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.Trim(PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static EDecimal Trim(EDecimal ed1, EContext ec) {
      EDecimal ed = ed1;
      if (ed1 == null) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      if (ed.IsSignalingNaN()) {
        return EDecimal.CreateNaN(
         ed.UnsignedMantissa,
         true,
         ed.IsNegative,
         ec);
      }
      if (ed.IsFinite) {
        if (ed.IsZero) {
          return (ed.IsNegative ? EDecimal.NegativeZero :
             EDecimal.Zero).RoundToPrecision(ec);
        } else if (ed.Exponent.Sign > 0) {
          return ed.Reduce(ec);
        } else if (ed.Exponent.Sign == 0) {
          return ed.RoundToPrecision(ec);
        } else {
          EInteger exp = ed.Exponent;
          EInteger mant = ed.UnsignedMantissa;
          bool neg = ed.IsNegative;
          var trimmed = false;
          EInteger radixint = EInteger.FromInt32(DecimalRadix);
          while (exp.Sign < 0 && mant.Sign > 0) {
            EInteger[] divrem = mant.DivRem(radixint);
            int rem = divrem[1].ToInt32Checked();
            if (rem != 0) {
              break;
            }
            mant = divrem[0];
            exp = exp.Add(1);
            trimmed = true;
          }
          if (!trimmed) {
            return ed.RoundToPrecision(ec);
          }
          EDecimal ret = EDecimal.Create(mant, exp);
          if (neg) {
            ret = ret.Negate();
          }
          return ret.RoundToPrecision(ec);
        }
      } else {
        return ed1.Plus(ec);
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.Rescale(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static EDecimal Rescale(EDecimal ed, EDecimal scale, EContext ec) {
      if (ed == null || scale == null) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      if (!scale.IsFinite) {
        return ed.Quantize(scale, ec);
      }
      if (scale.Exponent.IsZero) {
        return ed.Quantize(EDecimal.Create(EInteger.One, scale.Mantissa), ec);
      } else {
        EContext tec = ec == null ? null : ec.WithTraps(0).WithBlankFlags();
        EDecimal rv = scale.RoundToExponentExact(0, tec);
        if (!rv.IsFinite || (tec.Flags & EContext.FlagInexact) != 0) {
          if (ec != null && ec.IsSimplified) {
            // In simplified arithmetic, round scale to trigger
            // appropriate error conditions
            scale = scale.RoundToPrecision(ec);
          }
          return InvalidOperation(EDecimal.NaN, ec);
        }
        EDecimal rounded = scale.Quantize(0, tec);
        return ed.Quantize(
          EDecimal.Create(EInteger.One, rounded.Mantissa),
          ec);
      }
    }

    // Logical Operations
    /// <summary>Performs a logical AND operation on two decimal numbers in
    /// the form of
    /// <i>logical operands</i>. A <c>logical operand</c> is a
    /// non-negative base-10 number with an Exponent property of 0 and no
    /// other base-10 digits than 0 or 1 (examples include <c>01001</c> and
    /// <c>111001</c>, but not <c>02001</c> or <c>99999</c> ). The logical
    /// AND operation sets each digit of the result to 1 if the
    /// corresponding digits of each logical operand are both 1, and to 0
    /// otherwise. For example, <c>01001 AND 111010 = 01000</c></summary>
    /// <param name='ed1'>The first logical operand to the logical AND
    /// operation.</param>
    /// <param name='ed2'>The second logical operand to the logical AND
    /// operation.</param>
    /// <param name='ec'>A context that specifies the maximum precision of
    /// arbitrary-precision numbers. If a logical operand passed to this
    /// method has more digits than the maximum precision specified in this
    /// context, the operand's most significant digits that exceed that
    /// precision are discarded. This parameter can be null.</param>
    /// <returns>The result of the logical AND operation as a logical
    /// operand. Signals an invalid operation and returns not-a-number
    /// (NaN) if <paramref name='ed1'/>, <paramref name='ed2'/>, or both
    /// are not logical operands.</returns>
    public static EDecimal And(EDecimal ed1, EDecimal ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1, ec, 10);
      if (logi1 == null) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      byte[] logi2 = FromLogical(ed2, ec, 10);
      if (logi2 == null) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        smaller[i] &= bigger[i];
      }
      return EDecimal.FromEInteger(ToLogical(smaller, 10)).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.Invert(PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static EDecimal Invert(EDecimal ed1, EContext ec) {
      if (ec == null || !ec.HasMaxPrecision) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      byte[] smaller = FromLogical(ed1, ec, 10);
      if (smaller == null) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      EInteger ei = EInteger.One.ShiftLeft(ec.Precision).Subtract(1);
      byte[] bigger = ei.ToBytes(true);
#if DEBUG
      if (smaller.Length > bigger.Length) {
        throw new ArgumentException("smaller.Length (" + smaller.Length +
          ") is not less or equal to " + bigger.Length);
      }
#endif

      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] ^= smaller[i];
      }
      return EDecimal.FromEInteger(ToLogical(bigger, 10)).RoundToPrecision(ec);
    }

    /// <summary>Performs a logical exclusive-OR (XOR) operation on two
    /// decimal numbers in the form of
    /// <i>logical operands</i>. A <c>logical operand</c> is a
    /// non-negative base-10 number with an exponent of 0 and no other
    /// base-10 digits than 0 or 1 (examples include <c>01001</c> and
    /// <c>111001</c>, but not <c>02001</c> or <c>99999</c> ). The logical
    /// exclusive-OR operation sets each digit of the result to 1 if either
    /// corresponding digit of the logical operands, but not both, are 1,
    /// and to 0 otherwise. For example, <c>01001 OR 111010 =
    /// 101010</c></summary>
    /// <param name='ed1'>The first logical operand to the logical
    /// exclusive-OR operation.</param>
    /// <param name='ed2'>The second logical operand to the logical
    /// exclusive-OR operation.</param>
    /// <param name='ec'>A context that specifies the maximum precision of
    /// arbitrary-precision numbers. If a logical operand passed to this
    /// method has more digits than the maximum precision specified in this
    /// context, the operand's most significant digits that exceed that
    /// precision are discarded. This parameter can be null.</param>
    /// <returns>An EDecimal object.</returns>
    public static EDecimal Xor(EDecimal ed1, EDecimal ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1, ec, 10);
      if (logi1 == null) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      byte[] logi2 = FromLogical(ed2, ec, 10);
      if (logi2 == null) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] ^= smaller[i];
      }
      return EDecimal.FromEInteger(ToLogical(bigger, 10)).RoundToPrecision(ec);
    }

    /// <summary>Performs a logical OR operation on two decimal numbers in
    /// the form of
    /// <i>logical operands</i>. A <c>logical operand</c> is a
    /// non-negative base-10 number with an Exponent property of 0 and no
    /// other base-10 digits than 0 or 1 (examples include <c>01001</c> and
    /// <c>111001</c>, but not <c>02001</c> or <c>99999</c> ). The logical
    /// OR operation sets each digit of the result to 1 if either or both
    /// of the corresponding digits of the logical operands are 1, and to 0
    /// otherwise. For example, <c>01001 OR 111010 = 111011</c></summary>
    /// <param name='ed1'>The first logical operand to the logical OR
    /// operation.</param>
    /// <param name='ed2'>The second logical operand to the logical OR
    /// operation.</param>
    /// <param name='ec'>A context that specifies the maximum precision of
    /// arbitrary-precision numbers. If a logical operand passed to this
    /// method has more digits than the maximum precision specified in this
    /// context, the operand's most significant digits that exceed that
    /// precision are discarded. This parameter can be null.</param>
    /// <returns>The result of the logical OR operation as a logical
    /// operand. Signals an invalid operation and returns not-a-number
    /// (NaN) if <paramref name='ed1'/>, <paramref name='ed2'/>, or both
    /// are not logical operands.</returns>
    public static EDecimal Or(EDecimal ed1, EDecimal ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1, ec, 10);
      if (logi1 == null) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      byte[] logi2 = FromLogical(ed2, ec, 10);
      if (logi2 == null) {
        return InvalidOperation(EDecimal.NaN, ec);
      }
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] |= smaller[i];
      }
      return EDecimal.FromEInteger(ToLogical(bigger, 10)).RoundToPrecision(ec);
    }

    internal static EInteger ToLogical(byte[] bytes, int radix) {
      if (bytes == null) {
        throw new ArgumentNullException(nameof(bytes));
      }
      EInteger ret = EInteger.Zero;
      int i;
      for (i = bytes.Length - 1; i >= 0; --i) {
        int b = bytes[i];
        for (var j = 7; j >= 0; --j) {
          ret = ((bytes[i] & (1 << j)) != 0) ? ret.Multiply(radix).Add(1) :
               ret.Multiply(radix);
        }
      }
      return ret;
    }

    internal static byte[] FromLogical(EInteger um, EContext ec, int radix) {
      if (um == null || um.Sign < 0) {
 return null;
}
      if (um.Sign == 0) {
 return new byte[] { 0 };
}
      EInteger ret = EInteger.Zero;
      EInteger prec = um.GetDigitCountAsEInteger();
      EInteger maxprec = (ec != null && ec.HasMaxPrecision) ? ec.Precision :
           null;
      EInteger bytecount = prec.ShiftRight(3).Add(1);
      if (bytecount.CompareTo(0x7fffffff) > 0) {
        return null;  // Out of memory
      }
      var bitindex = 0;
      var bytes = new byte[bytecount.ToInt32Checked()];
      EInteger radixint = EInteger.FromInt32(radix);
      while (um.Sign > 0) {
        EInteger[] divrem = um.DivRem(radixint);
        int rem = divrem[1].ToInt32Checked();
        um = divrem[0];
        if (rem == 1) {
          // Don't collect bits beyond max precision
          if (maxprec == null || maxprec.CompareTo(bitindex) > 0) {
            int byteindex = bitindex >> 3;
            int mask = 1 << (bitindex & 7);
            bytes[byteindex] |= (byte)mask;
          }
        } else if (rem != 0) {
          return null;
        }
        ++bitindex;
      }
      return bytes;
    }

    internal static byte[] FromLogical(EDecimal ed, EContext ec, int radix) {
      if (ed == null) {
 return null;
}
      if (ec != null && ec.IsPrecisionInBits) {
 ed = ed.RoundToPrecision(ec);
}
      return (!ed.IsFinite || ed.IsNegative || ed.Exponent.Sign != 0 ||
    ed.Mantissa.Sign < 0) ? null : FromLogical(
  ed.UnsignedMantissa,
  ec,
  radix);
    }

    internal static byte[] FromLogical(EFloat ed, EContext ec, int radix) {
      if (ed == null) {
 return null;
}
      // NOTE: Precision of EFloat is already in bits, so no need to check for
      // IsPrecisionInBits here
      return (!ed.IsFinite || ed.IsNegative || ed.Exponent.Sign != 0 ||
    ed.Mantissa.Sign < 0) ? null : FromLogical(
  ed.UnsignedMantissa,
  ec,
  radix);
    }
  }
}
