using System;

namespace PeterO.Numbers {
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="T:Test.EFloatExtras"]/*'/>
  public static class EFloatExtras {
    private const int BinaryRadix = 2;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.Radix(PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Radix(EContext ec) {
      return EFloat.FromInt32(BinaryRadix).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.Int32ToEFloat(System.Int32,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Int32ToEFloat(int i32, EContext ec) {
      // NOTE: Not a miscellaneous operation in the General Decimal
      // Arithmetic Specification 1.70, but required since some of the
      // miscellaneous operations here return integers
      return EFloat.FromInt32(i32).RoundToPrecision(ec);
    }

    /// <summary>Converts a boolean value (either true or false) to an
    /// arbitrary-precision binary floating-point number.</summary>
    /// <param name='b'>Either true or false.</param>
    /// <param name='ec'>A context used for rounding the result. Can be
    /// null.</param>
    /// <returns>Either 1 if <paramref name='b'/> is true, or 0 if
    /// <paramref name='b'/> is false.. The result will be rounded as
    /// specified by the given context, if any.</returns>
    [Obsolete]
    public static EFloat BoolToEFloat(bool b, EContext ec) {
      return EFloat.FromInt32(b ? 1 : 0).RoundToPrecision(ec);
    }

    /// <summary>Converts a boolean value (either true or false) to an
    /// arbitrary-precision binary floating-point number.</summary>
    /// <param name='b'>Either true or false.</param>
    /// <param name='ec'>A context used for rounding the result. Can be
    /// null.</param>
    /// <returns>Either 1 if <paramref name='b'/> is true, or 0 if
    /// <paramref name='b'/> is false.. The result will be rounded as
    /// specified by the given context, if any.</returns>
    public static EFloat BooleanToEFloat(bool b, EContext ec) {
      // NOTE: Not a miscellaneous operation in the General Decimal
      // Arithmetic Specification 1.70, but required since some of the
      // miscellaneous operations here return booleans
      return EFloat.FromInt32(b ? 1 : 0).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsCanonical(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsCanonical(EFloat ed) {
      return true;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsFinite(PeterO.Numbers.EDecimal)"]/*'/>
   public static bool IsFinite(EFloat ed) {
      return ed != null && ed.IsFinite;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsInfinite(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsInfinite(EFloat ed) {
      return ed != null && ed.IsInfinity();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsNaN(PeterO.Numbers.EDecimal)"]/*'/>
   public static bool IsNaN(EFloat ed) {
      return ed != null && ed.IsNaN();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsNormal(PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static bool IsNormal(EFloat ed, EContext ec) {
      return ed != null && ed.IsFinite && !ed.IsZero && !IsSubnormal(ed, ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsQuietNaN(PeterO.Numbers.EDecimal)"]/*'/>
  public static bool IsQuietNaN(EFloat ed) {
      return ed != null && ed.IsQuietNaN();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsSigned(PeterO.Numbers.EDecimal)"]/*'/>
     public static bool IsSigned(EFloat ed) {
      return ed != null && ed.IsNegative;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.IsSignalingNaN(PeterO.Numbers.EDecimal)"]/*'/>
    public static bool IsSignalingNaN(EFloat ed) {
      return ed != null && ed.IsSignalingNaN();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloatExtras.NumberClassString(System.Int32)"]/*'/>
    public static string NumberClassString(int nc) {
      return EDecimalExtras.NumberClassString(nc);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.NumberClass(PeterO.Numbers.EDecimal,PeterO.Numbers.EContext)"]/*'/>
    public static int NumberClass(EFloat ed, EContext ec) {
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
     public static bool IsSubnormal(EFloat ed, EContext ec) {
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
    public static bool IsZero(EFloat ed) {
      return ed != null && ed.IsZero;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloatExtras.LogB(PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat LogB(EFloat ed, EContext ec) {
      if ((ed) == null) {
  throw new ArgumentNullException(nameof(ed));
}
      if (ed.IsNaN()) {
        return ed.RoundToPrecision(ec);
      }
      if (ed.IsInfinity()) {
        return EFloat.PositiveInfinity;
      }
      if (ed.IsZero) {
        return EFloat.FromInt32(-1).Divide(EFloat.Zero, ec);
      }
      EInteger ei = ed.Exponent.Add(ed.Precision().Subtract(1));
      return EFloat.FromEInteger(ei).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.ScaleB(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat ScaleB(EFloat ed, EFloat ed2, EContext ec) {
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
        return InvalidOperation(EFloat.NaN, ec);
      }
      EInteger scale = ed2.Mantissa;
      if (ec != null && ec.HasMaxPrecision) {
        EInteger exp = ec.EMax.Add(ec.Precision).Multiply(2);
        if (scale.Abs().CompareTo(exp.Abs()) > 0) {
          return InvalidOperation(EFloat.NaN, ec);
        }
      }
      if (ed.IsInfinity()) {
        return ed;
      }
      if (scale.IsZero) {
        return ed.RoundToPrecision(ec);
      }
      EFloat ret = EFloat.Create(
         ed.UnsignedMantissa,
         ed.Exponent.Add(scale));
      if (ed.IsNegative) {
        ret = ret.Negate();
      }
      return ret.RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.Shift(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Shift(EFloat ed, EFloat ed2, EContext ec) {
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
        return InvalidOperation(EFloat.NaN, ec);
      }
      EInteger shift = ed2.Mantissa;
      if (ec != null) {
        if (shift.Abs().CompareTo(ec.Precision) > 0) {
          return InvalidOperation(EFloat.NaN, ec);
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
      if (shift.Sign < 0) {
        if (shift.Abs().CompareTo(mantprec) < 0) {
          EInteger divisor = EInteger.One.ShiftLeft(shift.Abs());
          mant = mant.Divide(divisor);
        } else {
          mant = EInteger.Zero;
        }
        EFloat ret = EFloat.Create(mant, ed.Exponent);
        return ed.IsNegative ? ret.Negate() : ret;
      } else {
        EInteger mult = EInteger.One.ShiftLeft(shift);
        mant = mant.Multiply(mult);
        if (ec != null && ec.HasMaxPrecision) {
          EInteger mod = EInteger.One.ShiftLeft(ec.Precision);
          mant = mant.Remainder(mod);
        }
        EFloat ret = EFloat.Create(mant, ed.Exponent);
        return ed.IsNegative ? ret.Negate() : ret;
      }
    }

    /// <summary>Rotates the digits of an arbitrary-precision binary
    /// number's mantissa.</summary>
    /// <param name='ed'>An arbitrary-precision number containing the
    /// mantissa to rotate. If this mantissa contains more bits than the
    /// precision, the most-significant bits are chopped off the
    /// mantissa.</param>
    /// <param name='ed2'>An arbitrary-precision number indicating the
    /// number of bits to rotate the first operand's mantissa. Must be an
    /// integer with an exponent of 0. If this parameter is positive, the
    /// mantissa is shifted by the given number of bits and the
    /// most-significant bits shifted out of the mantissa become the
    /// least-significant bits instead. If this parameter is negative, the
    /// number is shifted by the given number of bits and the
    /// least-significant bits shifted out of the mantissa become the
    /// most-significant bits instead.</param>
    /// <param name='ec'>A context that specifies the precision of
    /// arbitrary-precision numbers. If this parameter is null or specifies
    /// an unlimited precision, this method has the same behavior as
    /// <c>Shift</c>.</param>
    /// <returns>An arbitrary-precision binary number whose mantissa is
    /// rotated the given number of bits. Signals an invalid operation and
    /// returns NaN (not-a-number) if <paramref name='ed2'/> is a signaling
    /// NaN or if <paramref name='ed2'/> is not an integer, is negative,
    /// has an exponent other than 0, or has an absolute value that exceeds
    /// the maximum precision specified in the context.</returns>
    public static EFloat Rotate(EFloat ed, EFloat ed2, EContext ec) {
      if (ec == null || !ec.HasMaxPrecision) {
        return Shift(ed, ed2, ec);
      }
      if (ed.IsNaN() || ed2.IsNaN()) {
        return ed.Add(ed2, ec);
      }
      if (!ed2.IsFinite || ed2.Exponent.Sign != 0) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      EInteger shift = ed2.Mantissa;
      if (shift.Abs().CompareTo(ec.Precision) > 0) {
        return InvalidOperation(EFloat.NaN, ec);
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
        mant = mant.Remainder(EInteger.One.ShiftLeft(ec.Precision));
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
      // Right shift
      if (rightShift.CompareTo(mantprec) < 0) {
        EInteger divisor = EInteger.One.ShiftLeft(rightShift);
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
        EInteger mult = EInteger.One.ShiftLeft(leftShift);
        mantLeft = mant.Multiply(mult);
        EInteger mod = EInteger.One.ShiftLeft(ec.Precision);
        mantLeft = mantLeft.Remainder(mod);
      }
      EFloat ret = EFloat.Create(mantRight.Add(mantLeft), ed.Exponent);
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
    /// <param name='ed'>Not documented yet.</param>
    /// <param name='other'>Not documented yet.</param>
    /// <param name='ec'>Not documented yet. (3).</param>
    /// <returns>The number 0 if both objects have the same value, or -1 if
    /// the first object is less than the other value, or 1 if the first
    /// object is greater. Does not signal flags if either value is
    /// signaling NaN.</returns>
    public static int CompareTotal(EFloat ed, EFloat other, EContext ec) {
      return ed.CompareToTotal(other, ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.CompareTotalMagnitude(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static int CompareTotalMagnitude(
  EFloat ed,
  EFloat other,
  EContext ec) {
      return ed.CompareToTotalMagnitude(other, ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.Copy(PeterO.Numbers.EFloat)"]/*'/>
    public static EFloat Copy(EFloat ed) {
      return ed.Copy();
    }

    /// <summary>Returns a canonical version of the given
    /// arbitrary-precision number object. In this method, this is the same
    /// as that object.</summary>
    /// <param name='ed'>An arbitrary-precision number object.</param>
    /// <returns>The parameter <paramref name='ed'/>.</returns>
    public static EFloat Canonical(EFloat ed) {
      return Copy(ed);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.CopyAbs(PeterO.Numbers.EFloat)"]/*'/>
    public static EFloat CopyAbs(EFloat ed) {
      return Copy(ed.Abs());
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.CopyNegate(PeterO.Numbers.EFloat)"]/*'/>
    public static EFloat CopyNegate(EFloat ed) {
      return Copy(ed.Negate());
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.CopySign(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat)"]/*'/>
    public static EFloat CopySign(EFloat ed, EFloat other) {
      return ed.IsNegative == other.IsNegative ? Copy(ed) : CopyNegate(ed);
    }

    private static EFloat InvalidOperation(EFloat ed, EContext ec) {
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
    /// path='docs/doc[@name="M:Test.EFloatExtras.SameQuantum(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat)"]/*'/>
    public static bool SameQuantum(EFloat ed1, EFloat ed2) {
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
    /// path='docs/doc[@name="M:Test.EFloatExtras.Trim(PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Trim(EFloat ed1, EContext ec) {
      EFloat ed = ed1;
      if (ed1 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      if (ed.IsSignalingNaN()) {
        return EFloat.CreateNaN(
         ed.UnsignedMantissa,
         true,
         ed.IsNegative,
         ec);
      }
      if (ed.IsFinite) {
        if (ed.IsZero) {
          return (ed.IsNegative ? EFloat.NegativeZero :
             EFloat.Zero).RoundToPrecision(ec);
        } else if (ed.Exponent.Sign > 0) {
          return ed.Reduce(ec);
        } else if (ed.Exponent.Sign == 0) {
          return ed.RoundToPrecision(ec);
        } else {
          EInteger exp = ed.Exponent;
          EInteger mant = ed.UnsignedMantissa;
          bool neg = ed.IsNegative;
          var trimmed = false;
          EInteger radixint = EInteger.FromInt32(BinaryRadix);
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
          EFloat ret = EFloat.Create(mant, exp);
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
    /// path='docs/doc[@name="M:Test.EFloatExtras.Rescale(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Rescale(EFloat ed, EFloat scale, EContext ec) {
      if (ed == null || scale == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      if (!scale.IsFinite) {
        return ed.Quantize(scale, ec);
      }
      if (scale.Exponent.IsZero) {
        return ed.Quantize(EFloat.Create(EInteger.One, scale.Mantissa), ec);
      } else {
        EContext tec = ec == null ? null : ec.WithTraps(0).WithBlankFlags();
        EFloat rv = scale.RoundToExponentExact(0, tec);
        if (!rv.IsFinite || (tec.Flags & EContext.FlagInexact) != 0) {
          if (ec != null && ec.IsSimplified) {
            // In simplified arithmetic, round scale to trigger
            // appropriate error conditions
            scale = scale.RoundToPrecision(ec);
          }
          return InvalidOperation(EFloat.NaN, ec);
        }
        EFloat rounded = scale.Quantize(0, tec);
        return ed.Quantize(
          EFloat.Create(EInteger.One, rounded.Mantissa),
          ec);
      }
    }

    // Logical Operations
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.And(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat And(EFloat ed1, EFloat ed2, EContext ec) {
      byte[] logi1 = EDecimalExtras.FromLogical(ed1, ec, 2);
      if (logi1 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] logi2 = EDecimalExtras.FromLogical(ed2, ec, 2);
      if (logi2 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        smaller[i] &= bigger[i];
      }
      return EFloat.FromEInteger(
  EDecimalExtras.ToLogical(
  smaller,
  2)).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.Invert(PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Invert(EFloat ed1, EContext ec) {
      if (ec == null || !ec.HasMaxPrecision) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      EInteger ei = EInteger.One.ShiftLeft(ec.Precision).Subtract(1);
      byte[] smaller = EDecimalExtras.FromLogical(ed1, ec, 2);
      if (smaller == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
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
      return EFloat.FromEInteger(
  EDecimalExtras.ToLogical(
  bigger,
  2)).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.Xor(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Xor(EFloat ed1, EFloat ed2, EContext ec) {
      byte[] logi1 = EDecimalExtras.FromLogical(ed1, ec, 2);
      if (logi1 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] logi2 = EDecimalExtras.FromLogical(ed2, ec, 2);
      if (logi2 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] ^= smaller[i];
      }
      return EFloat.FromEInteger(
  EDecimalExtras.ToLogical(
  bigger,
  2)).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.Or(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Or(EFloat ed1, EFloat ed2, EContext ec) {
      byte[] logi1 = EDecimalExtras.FromLogical(ed1, ec, 2);
      if (logi1 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] logi2 = EDecimalExtras.FromLogical(ed2, ec, 2);
      if (logi2 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] |= smaller[i];
      }
      return EFloat.FromEInteger(
  EDecimalExtras.ToLogical(
  bigger,
  2)).RoundToPrecision(ec);
    }
  }
}
