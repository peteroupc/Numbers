using System;
using PeterO.Numbers;

namespace Test {
  public static class EFloatExtras {
    private const int BinaryRadix = 2;

    public static EFloat Radix(EContext ec) {
      return EFloat.FromInt32(BinaryRadix).RoundToPrecision(ec);
    }

    public static EFloat Int32ToEFloat(int i32, EContext ec) {
      // NOTE: Not a miscellaneous operation in the General Decimal
      // Arithmetic Specification 1.70, but required since some of the
      // miscellaneous operations here return integers
      return EFloat.FromInt32(i32).RoundToPrecision(ec);
    }

    public static EFloat BoolToEFloat(bool b, EContext ec) {
      // NOTE: Not a miscellaneous operation in the General Decimal
      // Arithmetic Specification 1.70, but required since some of the
      // miscellaneous operations here return booleans
      return EFloat.FromInt32(b ? 1 : 0).RoundToPrecision(ec);
    }

    public static bool IsCanonical(EFloat ed) {
      return true;
    }

    public static bool IsFinite(EFloat ed) {
      return ed.IsFinite;
    }

    public static bool IsInfinite(EFloat ed) {
      return ed.IsInfinity();
    }

    public static bool IsNaN(EFloat ed) {
      return ed != null && ed.IsNaN();
    }

    public static bool IsNormal(EFloat ed, EContext ec) {
      return ed != null && ed.IsFinite && !ed.IsZero && !IsSubnormal(ed, ec);
    }

    public static bool IsQuietNaN(EFloat ed) {
      return ed != null && ed.IsQuietNaN();
    }

    public static bool IsSigned(EFloat ed) {
      return ed.IsNegative;
    }

    public static bool IsSignalingNaN(EFloat ed) {
      return ed != null && ed.IsSignalingNaN();
    }

    private static readonly string[] NumberClasses = {
 "+Normal", "-Normal",
 "+Subnormal", "-Subnormal",
 "+Zero", "-Zero",
 "+Infinity", "-Infinity",
 "NaN", "sNaN"
};

    public static string NumberClassString(int nc) {
      if (nc < 0) {
throw new ArgumentException("nc (" + nc + ") is not greater or equal to 0");
}
if (nc > 9) {
  throw new ArgumentException("nc (" + nc + ") is not less or equal to 9");
}
      return NumberClasses[nc];
    }

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

    public static bool IsZero(EFloat ed) {
      return ed != null && ed.IsZero;
    }

    public static EFloat LogB(EFloat ed, EContext ec) {
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
EInteger radix = EInteger.FromInt32(BinaryRadix);
if (shift.Sign < 0) {
  if (shift.Abs().CompareTo(mantprec) < 0) {
   // TODO: Add Pow(EInteger)
   EInteger divisor = radix.Pow(shift.Abs().ToInt32Checked());
   mant = mant.Divide(divisor);
  } else {
   mant = EInteger.Zero;
  }
  EFloat ret = EFloat.Create(mant, ed.Exponent);
  return ed.IsNegative ? ret.Negate() : ret;
} else {
  // TODO: Add Pow(EInteger)
  EInteger mult = radix.Pow(shift.ToInt32Checked());
  mant = mant.Multiply(mult);
  if (ec != null && ec.HasMaxPrecision) {
    EInteger mod = radix.Pow(ec.Precision.ToInt32Checked());
    mant = mant.Remainder(mod);
  }
  EFloat ret = EFloat.Create(mant, ed.Exponent);
  return ed.IsNegative ? ret.Negate() : ret;
}
    }

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
if (ec != null && ec.HasMaxPrecision && mantprec.CompareTo(ec.Precision) > 0) {
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
EInteger radix = EInteger.FromInt32(BinaryRadix);
// Right shift
// TODO: Add Pow(EInteger)
if (rightShift.CompareTo(mantprec) < 0) {
   EInteger divisor = radix.Pow(rightShift.ToInt32Checked());
   mantRight = mant.Divide(divisor);
} else {
   mantRight = EInteger.Zero;
}
// Left shift
// TODO: Add Pow(EInteger)
if (leftShift.IsZero) {
   mantLeft = mant;
} else if (leftShift.CompareTo(ec.Precision) == 0) {
   mantLeft = EInteger.Zero;
} else {
   EInteger mult = radix.Pow(leftShift.ToInt32Checked());
   mantLeft = mant.Multiply(mult);
   EInteger mod = radix.Pow(ec.Precision.ToInt32Checked());
   mantLeft = mantLeft.Remainder(mod);
}
EFloat ret = EFloat.Create(mantRight.Add(mantLeft), ed.Exponent);
return ed.IsNegative ? ret.Negate() : ret;
    }

    public static int CompareTotal(EFloat ed, EFloat other, EContext ec) {
      return ed.CompareToTotal(other, ec);
    }

    public static int CompareTotalMagnitude(
  EFloat ed,
  EFloat other,
  EContext ec) {
      return ed.CompareToTotalMagnitude(other, ec);
    }

    public static EFloat Copy(EFloat ed) {
       return ed.Copy();
    }

    public static EFloat Canonical(EFloat ed) {
      return Copy(ed);
    }

    public static EFloat CopyAbs(EFloat ed) {
       return Copy(ed.Abs());
    }

    public static EFloat CopyNegate(EFloat ed) {
       return Copy(ed.Negate());
    }

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

    public static bool SameQuantum(EFloat ed1, EFloat ed2) {
if (ed1 == null || ed2 == null) {
 return false;
}
if (ed1.IsFinite && ed2.IsFinite) {
  return ed1.Exponent.Equals(ed2.Exponent);
} else {
  return (ed1.IsNaN() && ed2.IsNaN()) || (ed1.IsInfinity() && ed2.IsInfinity());
}
    }

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
    public static EFloat And(EFloat ed1, EFloat ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1, ec);
      if (logi1 == null) {
 return InvalidOperation(EFloat.NaN, ec);
}
      byte[] logi2 = FromLogical(ed2, ec);
      if (logi2 == null) {
 return InvalidOperation(EFloat.NaN, ec);
}
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        smaller[i] &= bigger[i];
      }
      return ToLogical(smaller).RoundToPrecision(ec);
    }

    public static EFloat Invert(EFloat ed1, EContext ec) {
       if (ec == null || !ec.HasMaxPrecision) {
 return InvalidOperation(EFloat.NaN, ec);
}
      EInteger ei = EInteger.One.ShiftLeft(ec.Precision).Subtract(1);
      byte[] smaller = FromLogical(ed1, ec);
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
      return ToLogical(bigger).RoundToPrecision(ec);
    }

    public static EFloat Xor(EFloat ed1, EFloat ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1, ec);
      if (logi1 == null) {
 return InvalidOperation(EFloat.NaN, ec);
}
      byte[] logi2 = FromLogical(ed2, ec);
      if (logi2 == null) {
 return InvalidOperation(EFloat.NaN, ec);
}
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] ^= smaller[i];
      }
      return ToLogical(bigger).RoundToPrecision(ec);
    }

    public static EFloat Or(EFloat ed1, EFloat ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1, ec);
      if (logi1 == null) {
 return InvalidOperation(EFloat.NaN, ec);
}
      byte[] logi2 = FromLogical(ed2, ec);
      if (logi2 == null) {
 return InvalidOperation(EFloat.NaN, ec);
}
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] |= smaller[i];
      }
      return ToLogical(bigger).RoundToPrecision(ec);
    }

    private static EFloat ToLogical(byte[] bytes) {
if (bytes == null) {
  throw new ArgumentNullException(nameof(bytes));
}
      EInteger ret = EInteger.Zero;
      for (var i = bytes.Length - 1; i >= 0; --i) {
        int b = bytes[i];
        for (var j = 7; j >= 0; --j) {
       ret = ((bytes[i] & (1 << j)) != 0) ? ret.Multiply(BinaryRadix).Add(1) :
            ret.Multiply(BinaryRadix);
        }
      }
      return EFloat.FromEInteger(ret);
    }

    private static byte[] FromLogical(EFloat ed, EContext ec) {
      if (!ed.IsFinite || ed.IsNegative || ed.Exponent.Sign != 0 ||
         ed.Mantissa.Sign < 0) {
 return null;
}
      EInteger um = ed.UnsignedMantissa;
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
      EInteger radixint = EInteger.FromInt32(BinaryRadix);
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
  }
}
