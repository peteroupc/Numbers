using System;
using PeterO.Numbers;

namespace Test {
  public static class EDecimalExtras {
    public static EDecimal Radix(EContext ec) {
      return EDecimal.FromInt32(10).RoundToPrecision(ec);
    }

    public static EDecimal Int32ToDecimal(int i32, EContext ec) {
      // NOTE: Not a miscellaneous operation in the General Decimal
      // Arithmetic Specification 1.70, but required since some of the
      // miscellaneous operations here return integers
      return EDecimal.FromInt32(i32).RoundToPrecision(ec);
    }

    public static EDecimal BoolToDecimal(bool b, EContext ec) {
      // NOTE: Not a miscellaneous operation in the General Decimal
      // Arithmetic Specification 1.70, but required since some of the
      // miscellaneous operations here return booleans
      return Int32ToDecimal(b ? 1 : 0, ec);
    }

    public static bool IsCanonical(EDecimal ed) {
      return true;
    }

    public static bool IsFinite(EDecimal ed) {
      return ed.IsFinite;
    }

    public static bool IsInfinite(EDecimal ed) {
      return ed.IsInfinity();
    }

    public static bool IsNaN(EDecimal ed) {
      return ed != null && ed.IsNaN();
    }

    public static bool IsNormal(EDecimal ed, EContext ec) {
      return ed != null && ed.IsFinite && !ed.IsZero && !IsSubnormal(ed, ec);
    }

    public static bool IsQNaN(EDecimal ed) {
      return ed != null && ed.IsQuietNaN();
    }

    public static bool IsSigned(EDecimal ed) {
      return ed.IsNegative;
    }

    public static bool IsSNaN(EDecimal ed) {
      return ed != null && ed.IsSignalingNaN();
    }

    private static readonly string[] NumberClasses = {
 "+Normal","-Normal",
 "+Subnormal","-Subnormal",
 "+Zero","-Zero",
 "+Infinity","-Infinity",
 "NaN","sNaN"
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

    public static bool IsZero(EDecimal ed) {
      return ed != null && ed.IsZero;
    }

    public static EDecimal LogB(EDecimal ed, EContext ec) {
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
      if (ed.IsInfinity()) {
 return ed;
}
      if (!ed2.IsFinite || ed2.Exponent.Sign != 0) {
 return InvalidOperation(EDecimal.NaN, ec);
}
      throw new NotImplementedException();
    }

    public static int CompareTotal(EDecimal ed, EDecimal other, EContext ec) {
      return ed.CompareToTotal(other, ec);
    }

    public static int CompareTotalMagnitude(EDecimal ed, EDecimal other,
      EContext ec) {
      return ed.CompareToTotalMagnitude(other, ec);
    }

    public static EDecimal Copy(EDecimal ed) {
       return ed.Copy();
    }

    public static EDecimal Canonical(EDecimal ed) {
      return Copy(ed);
    }

    public static EDecimal CopyAbs(EDecimal ed) {
       return Copy(ed.Abs());
    }

    public static EDecimal CopyNegate(EDecimal ed) {
       return Copy(ed.Negate());
    }

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

    public static EDecimal And(EDecimal ed1, EDecimal ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1);
      if (logi1 == null) {
 return InvalidOperation(ed1, ec);
}
      byte[] logi2 = FromLogical(ed2);
      if (logi2 == null) {
 return InvalidOperation(ed2, ec);
}
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        smaller[i] &= bigger[i];
      }
      return ToLogical(smaller);
    }

    public static EDecimal Invert(EDecimal ed1, EDecimal ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1);
      if (logi1 == null) {
 return InvalidOperation(ed1, ec);
}
      throw new NotImplementedException();
    }

    public static bool SameQuantum(EDecimal ed1, EDecimal ed2) {
if (ed1 == null || ed2 == null) { {
 return false;
} }
if (ed1.IsFinite && ed2.IsFinite) {
  return ed1.Exponent.Equals(ed2.Exponent);
} else {
  return ed1.IsNaN() == ed2.IsNaN() || ed1.IsInfinity() == ed2.IsInfinity();
}
    }

    public static EDecimal Xor(EDecimal ed1, EDecimal ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1);
      if (logi1 == null) {
 return InvalidOperation(ed1, ec);
}
      byte[] logi2 = FromLogical(ed2);
      if (logi2 == null) {
 return InvalidOperation(ed2, ec);
}
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] ^= smaller[i];
      }
      return ToLogical(bigger);
    }

    public static EDecimal Or(EDecimal ed1, EDecimal ed2, EContext ec) {
      byte[] logi1 = FromLogical(ed1);
      if (logi1 == null) {
 return InvalidOperation(ed1, ec);
}
      byte[] logi2 = FromLogical(ed2);
      if (logi2 == null) {
 return InvalidOperation(ed2, ec);
}
      byte[] smaller = logi1.Length < logi2.Length ? logi1 : logi2;
      byte[] bigger = logi1.Length < logi2.Length ? logi2 : logi1;
      for (var i = 0; i < smaller.Length; ++i) {
        bigger[i] |= smaller[i];
      }
      return ToLogical(bigger);
    }

    private static EDecimal ToLogical(byte[] bytes) {
if (bytes == null) {
  throw new ArgumentNullException(nameof(bytes));
}
      EInteger ret = EInteger.Zero;
      for (var i = 0; i < bytes.Length; ++i) {
        for (var j = 0; j < 8; ++j) {
          if ((bytes[i] & (1 << j)) != 0) {
            ret = ret.Multiply(10).Add(1);
          }
        }
      }
      return EDecimal.FromEInteger(ret);
    }

    private static byte[] FromLogical(EDecimal ed) {
      if (!ed.IsFinite || ed.IsNegative || ed.Exponent.Sign != 0 ||
         ed.Mantissa.Sign <= 0) {
 return null;
}
      EInteger um = ed.UnsignedMantissa;
      EInteger ret = EInteger.Zero;
      while (um.Sign > 0) {
        EInteger[] divrem = um.DivRem(EInteger.FromInt32(10));
        um = divrem[0];
        if (um.CompareTo(1) == 0) {
          ret = ret.ShiftLeft(1).Add(1);
        } else if (um.CompareTo(0) == 0) {
          ret = ret.ShiftLeft(1);
        } else {
          return null;
        }
      }
      return ret.ToBytes(true);
    }
  }
}
