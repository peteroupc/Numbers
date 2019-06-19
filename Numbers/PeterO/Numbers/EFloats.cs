using System;

namespace PeterO.Numbers {
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="T:Test.EFloatExtras"]/*'/>
  public static class EFloats {
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

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloatExtras.BooleanToEFloat(System.Boolean,PeterO.Numbers.EContext)"]/*'/>
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
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.NumberClassString(System.Int32)"]/*'/>
    public static string NumberClassString(int nc) {
      return EDecimals.NumberClassString(nc);
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
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloats.LogB(PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat LogB(EFloat ed, EContext ec) {
      if (ed == null) {
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
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloats.ScaleB(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
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
      if (ec != null && ec.HasMaxPrecision && ec.HasExponentRange) {
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

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloatExtras.Rotate(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
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

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloatExtras.CompareTotal(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
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
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.Copy(PeterO.Numbers.EDecimal)"]/*'/>
    public static EFloat Copy(EFloat ed) {
      return ed.Copy();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloats.Canonical(PeterO.Numbers.EFloat)"]/*'/>
    public static EFloat Canonical(EFloat ed) {
      return Copy(ed);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.CopyAbs(PeterO.Numbers.EDecimal)"]/*'/>
    public static EFloat CopyAbs(EFloat ed) {
      return Copy(ed.Abs());
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.CopyNegate(PeterO.Numbers.EDecimal)"]/*'/>
    public static EFloat CopyNegate(EFloat ed) {
      return Copy(ed.Negate());
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimalExtras.CopySign(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal)"]/*'/>
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
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloatExtras.Trim(PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
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
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloatExtras.Rescale(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
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
      byte[] logi1 = EDecimals.FromLogical(ed1, ec, 2);
      if (logi1 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] logi2 = EDecimals.FromLogical(ed2, ec, 2);
      if (logi2 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        smaller[i] &= bigger[i];
      }
      return EFloat.FromEInteger(
  EDecimals.ToLogical(
  smaller,
  2)).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloats.Invert(PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Invert(EFloat ed1, EContext ec) {
      if (ec == null || !ec.HasMaxPrecision) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      EInteger ei = EInteger.One.ShiftLeft(ec.Precision).Subtract(1);
      byte[] smaller = EDecimals.FromLogical(ed1, ec, 2);
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
  EDecimals.ToLogical(
  bigger,
  2)).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EFloatExtras.Xor(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Xor(EFloat ed1, EFloat ed2, EContext ec) {
      byte[] logi1 = EDecimals.FromLogical(ed1, ec, 2);
      if (logi1 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] logi2 = EDecimals.FromLogical(ed2, ec, 2);
      if (logi2 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] ^= smaller[i];
      }
      return EFloat.FromEInteger(
  EDecimals.ToLogical(
  bigger,
  2)).RoundToPrecision(ec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:Test.EFloatExtras.Or(PeterO.Numbers.EFloat,PeterO.Numbers.EFloat,PeterO.Numbers.EContext)"]/*'/>
    public static EFloat Or(EFloat ed1, EFloat ed2, EContext ec) {
      byte[] logi1 = EDecimals.FromLogical(ed1, ec, 2);
      if (logi1 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] logi2 = EDecimals.FromLogical(ed2, ec, 2);
      if (logi2 == null) {
        return InvalidOperation(EFloat.NaN, ec);
      }
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] |= smaller[i];
      }
      return EFloat.FromEInteger(
  EDecimals.ToLogical(
  bigger,
  2)).RoundToPrecision(ec);
    }
  }
}
