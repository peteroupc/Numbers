/*
Written by Peter O. in 2013.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using PeterO;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class DecimalTest {
    public static void Timeout(int duration, Action action, string msg) {
  action(); return;
    }

    private static readonly Regex ValuePropertyLine = new Regex(
      "^(\\w+)\\:\\s*(\\S+)",
      RegexOptions.Compiled);

    private static readonly Regex ValueQuotes = new Regex(
      "^[\\'\\\"]|[\\'\\\"]$",
      RegexOptions.Compiled);

    private static readonly Regex ValueTestLine = new Regex(
  "^([A-Za-z0-9_]+)\\s+([A-Za-z0-9_\\-]+)\\s+(\\'[^\\']*\\'|\\S+)\\s+(?:(\\S+)\\s+)?(?:(\\S+)\\s+)?->\\s+(\\S+)\\s*(.*)",
  RegexOptions.Compiled);

    private static TValue GetKeyOrDefault<TKey, TValue>(
  IDictionary<TKey, TValue> dict,
 TKey key,
 TValue defaultValue) {
      return (!dict.ContainsKey(key)) ? defaultValue : dict[key];
    }

    private static int StringToIntAllowPlus(string str) {
      if (String.IsNullOrEmpty(str)) {
 return 0;
}
   try {
      return (str[0] == '+') ? TestCommon.StringToInt(str.Substring(1)) :
        TestCommon.StringToInt(str);
   } catch (Exception ex) {
Console.WriteLine(ex.StackTrace);
throw;
   }
    }

    public static void ParseDecTest(
  string ln,
 IDictionary<string, string> context) {
      Match match;
      if (ln.Contains("-- ")) {
        ln = ln.Substring(0, ln.IndexOf("-- ", StringComparison.Ordinal));
      }
      match = (!ln.Contains(":")) ? null : ValuePropertyLine.Match(ln);
      if (match != null && match.Success) {
        context[match.Groups[1].ToString().ToLowerInvariant()] =
          match.Groups[2].ToString();
        return;
      }
var sw = new System.Diagnostics.Stopwatch();
sw.Start();
      match = ValueTestLine.Match(ln);
      if (match.Success) {
        string name = match.Groups[1].ToString();
        string op = match.Groups[2].ToString();
        string input1 = match.Groups[3].ToString();
        string input2 = match.Groups[4].ToString();
        string input3 = match.Groups[5].ToString();
        string output = match.Groups[6].ToString();
        string flags = match.Groups[7].ToString();
        input1 = ValueQuotes.Replace(input1, String.Empty);
        input2 = ValueQuotes.Replace(input2, String.Empty);
        input3 = ValueQuotes.Replace(input3, String.Empty);
        output = ValueQuotes.Replace(output, String.Empty);
        bool extended = GetKeyOrDefault(context, "extended", "1").Equals("1");
        bool clamp = GetKeyOrDefault(context, "clamp", "0").Equals("1");
int precision = 0, minexponent = 0, maxexponent = 0;
EContext ctx = null;
string rounding = null;
    precision = StringToIntAllowPlus(
  GetKeyOrDefault(context, "precision", "9"));
        minexponent = StringToIntAllowPlus(
  GetKeyOrDefault(context, "minexponent", "-9999"));
        maxexponent = StringToIntAllowPlus(
  GetKeyOrDefault(context, "maxexponent", "9999"));
        // Skip tests that take null as input or output;
        // also skip tests that take a hex number format
        if (input1.Contains("#") ||
input2.Contains("#") ||
input3.Contains("#") ||
output.Contains("#")) {
          return;
        }
if (!extended && (input1.Contains("sNaN") ||
input2.Contains("sNaN") ||
input3.Contains("sNaN") ||
output.Contains("sNaN"))) {
          Console.WriteLine(ln);
        }
        // Skip some tests that assume a maximum
        // supported precision of 999999999
        if (name.Equals("pow250") ||
name.Equals("pow251") ||
name.Equals("pow252")) {
          return;
        }
   // Assumes a maximum supported
   // value for pow exponent
        if (name.Equals("powx4008")) {
return;
        }
// Skip these unofficial test cases which involve
// pretruncation of the first operand of a shift operation;
// actually, the Gen. Decimal Arithmetic Spec. doesn't
// say to truncate that operand's coefficient before
// the shift operation, unlike for the rotate operation.
if (name.Equals("extr1651") ||
name.Equals("extr1652") ||
name.Equals("extr1653") ||
name.Equals("extr1654")) {
  return;
}
// Skip these unofficial test cases, which misapply the
// 'clamp' directive to NaNs in the test case format:
// 'clamp' governs only exponent clamping;
// although exponent clamping can
// indirectly affect the coefficient in certain cases, NaNs
// have neither a coefficient nor an exponent, so the
// 'clamp' directive doesn't properly apply to NaNs
if (name.Equals("covx5076") ||
name.Equals("covx5082") ||
name.Equals("covx5085") ||
name.Equals("covx5086")) {
  return;
}
// Skip these unofficial test cases, which are incorrect
// for expecting underflow on raising a huge
// positive integer to its own power
        if (name.Equals("power_eq4") ||
name.Equals("power_eq46") ||
name.Equals("power_eq48") ||
name.Equals("power_eq11") ||
name.Equals("power_eq65") ||
name.Equals("power_eq84") ||
name.Equals("power_eq94")) {
          return;
        }
        // Skip some test cases that are incorrect
        // (all simplified arithmetic test cases)
        if (!extended) {
          if (
   // result expected by test case is wrong by > 0.5 ULP
  name.Equals("ln116") ||
// assumes that the input will underflow to 0
name.Equals("qua530") ||
// assumes that the input will underflow to 0
              name.Equals("qua531") ||
name.Equals("rpow068") ||
name.Equals("rpow159") ||
name.Equals("rpow217") ||
name.Equals("rpow272") ||
name.Equals("rpow324") ||
name.Equals("rpow327") ||
// following cases incorrectly remove trailing zeros
              name.Equals("sqtx2207") ||
name.Equals("sqtx2231") ||
name.Equals("sqtx2271") ||
name.Equals("sqtx2327") ||
name.Equals("sqtx2399") ||
name.Equals("sqtx2487") ||
name.Equals("sqtx2591") ||
name.Equals("sqtx2711") ||
name.Equals("sqtx2847")) {
            return;
          }
        }
        if (input1.Contains("?")) {
          return;
        }
        if (flags.Contains("Invalid_context")) {
          Console.WriteLine(ln);
          return;
        }

        ctx = EContext.ForPrecision(precision)
          .WithExponentClamp(clamp).WithExponentRange(
            minexponent,
            maxexponent);
        rounding = GetKeyOrDefault(
  context,
  "rounding",
  "half_even");
        if (rounding.Equals("half_up")) {
          ctx = ctx.WithRounding(ERounding.HalfUp);
        }
        if (rounding.Equals("half_down")) {
          ctx = ctx.WithRounding(ERounding.HalfDown);
        }
        if (rounding.Equals("half_even")) {
          ctx = ctx.WithRounding(ERounding.HalfEven);
        }
        if (rounding.Equals("up")) {
          ctx = ctx.WithRounding(ERounding.Up);
        }
        if (rounding.Equals("down")) {
          ctx = ctx.WithRounding(ERounding.Down);
        }
        if (rounding.Equals("ceiling")) {
          ctx = ctx.WithRounding(ERounding.Ceiling);
        }
        if (rounding.Equals("floor")) {
          ctx = ctx.WithRounding(ERounding.Floor);
        }
        if (rounding.Equals("05up")) {
          // NOTE: This rounding mode is like Odd in the case
          // of binary numbers, and ZeroFiveUp in the case of
          // decimal numbers
          ctx = ctx.WithRounding(ERounding.OddOrZeroFiveUp);
        }
        if (!extended) {
          ctx = ctx.WithSimplified(true);
        }
        ctx = ctx.WithBlankFlags();
        if (op.Length > 3 && op.Substring(op.Length - 3).Equals("_eq")) {
            // Binary operators with both operands the same
            input2 = input1;
            op = op.Substring(0, op.Length - 3);
        }
        EDecimal d1 = EDecimal.Zero, d2 = null, d2a = null;
        if (!op.Equals("toSci") &&
!op.Equals("toEng") &&
!op.Equals("tosci") &&
!op.Equals("toeng") &&
!op.Equals("class") &&
!op.Equals("format")) {
          d1 = String.IsNullOrEmpty(input1) ? EDecimal.Zero :
            EDecimal.FromString(input1);
          d2 = String.IsNullOrEmpty(input2) ? null :
            EDecimal.FromString(input2);
          d2a = String.IsNullOrEmpty(input3) ? null :
            EDecimal.FromString(input3);
        }
        EDecimal d3 = null;
        if (op.Equals("fma") && !extended) {
          // This implementation does implement multiply-and-add
          // in the simplified arithmetic, even though the test cases expect
          // an invalid operation to be raised. This seems to be allowed
          // under appendix A, which merely says that multiply-and-add
          // "is not defined" in the simplified arithmetic.
          return;
        }
        if (op.Equals("multiply")) {
          d3 = d1.Multiply(d2, ctx);
        } else if (op.Equals("toSci")) {  // handled below
        } else if (op.Equals("toEng")) {  // handled below
        } else if (op.Equals("tosci")) {  // handled below
        } else if (op.Equals("toeng")) {  // handled below
        } else if (op.Equals("class")) {  // handled below
        } else if (op.Equals("fma")) {
          d3 = d1.MultiplyAndAdd(d2, d2a, ctx);
        } else if (op.Equals("min")) {
          d3 = EDecimal.Min(d1, d2, ctx);
        } else if (op.Equals("max")) {
          d3 = EDecimal.Max(d1, d2, ctx);
        } else if (op.Equals("minmag")) {
          d3 = EDecimal.MinMagnitude(d1, d2, ctx);
        } else if (op.Equals("maxmag")) {
          d3 = EDecimal.MaxMagnitude(d1, d2, ctx);
        } else if (op.Equals("compare")) {
          d3 = d1.CompareToWithContext(d2, ctx);
        } else if (op.Equals("comparetotal")) {
          int id3 = d1.CompareToTotal(d2, ctx);
          d3 = EDecimal.FromInt32(id3);
          Assert.AreEqual(id3, EDecimals.CompareTotal(d1, d2, ctx), ln);
        } else if (op.Equals("comparetotmag")) {
          int id3 = d1.CompareToTotalMagnitude(d2, ctx);
          d3 = EDecimal.FromInt32(id3);
    {
object objectTemp = id3;
object objectTemp2 = EDecimals.CompareTotalMagnitude(
  d1,
  d2,
  ctx);
string messageTemp = ln;
Assert.AreEqual(objectTemp, objectTemp2, messageTemp);
}
        } else if (op.Equals("copyabs")) {
          d3 = d1.Abs();
          Assert.AreEqual(d3, EDecimals.CopyAbs(d1));
        } else if (op.Equals("copynegate")) {
          d3 = d1.Negate();
          Assert.AreEqual(d3, EDecimals.CopyNegate(d1));
        } else if (op.Equals("copysign")) {
          d3 = d1.CopySign(d2);
Assert.AreEqual(d3, EDecimals.CopySign(d1, d2));
        } else if (op.Equals("comparesig")) {
          d3 = d1.CompareToSignal(d2, ctx);
        } else if (op.Equals("subtract")) {
          d3 = d1.Subtract(d2, ctx);
        } else if (op.Equals("tointegral")) {
          d3 = d1.RoundToIntegerNoRoundedFlag(ctx);
        } else if (op.Equals("tointegralx")) {
          d3 = d1.RoundToIntegerExact(ctx);
        } else if (op.Equals("divideint")) {
          d3 = d1.DivideToIntegerZeroScale(d2, ctx);
        } else if (op.Equals("divide")) {
          d3 = d1.Divide(d2, ctx);
        } else if (op.Equals("remainder")) {
          d3 = d1.Remainder(d2, ctx);
        } else if (op.Equals("exp")) {
          d3 = d1.Exp(ctx);
        } else if (op.Equals("ln")) {
// NOTE: Gen. Decimal Arithmetic Spec.'s ln supports
// only round-half-down mode, but EDecimal Log is not limited
// to that rounding mode
    ctx = ctx.WithRounding(ERounding.HalfEven);
          d3 = d1.Log(ctx);
        } else if (op.Equals("log10")) {
// NOTE: Gen. Decimal Arithmetic Spec.'s log10 supports
// only round-half-down mode, but EDecimal Log10 is not limited
// to that rounding mode
    ctx = ctx.WithRounding(ERounding.HalfEven);
          d3 = d1.Log10(ctx);
        } else if (op.Equals("power")) {
if (d2a != null) {
Console.WriteLine("Three-op power not yet supported");
return;
}
          d3 = d1.Pow(d2, ctx);
        } else if (op.Equals("squareroot")) {
          d3 = d1.Sqrt(ctx);
        } else if (op.Equals("remaindernear") || op.Equals("remainderNear")) {
          d3 = d1.RemainderNear(d2, ctx);
        } else if (op.Equals("nexttoward")) {
          d3 = d1.NextToward(d2, ctx);
        } else if (op.Equals("nextplus")) {
          d3 = d1.NextPlus(ctx);
        } else if (op.Equals("nextminus")) {
          d3 = d1.NextMinus(ctx);
        } else if (op.Equals("copy")) {
          d3 = d1;
Assert.AreEqual(d3, EDecimals.Copy(d1), "copy equiv");
        } else if (op.Equals("abs")) {
          d3 = d1.Abs(ctx);
        } else if (op.Equals("reduce")) {
          d3 = d1.Reduce(ctx);
        } else if (op.Equals("quantize")) {
          d3 = d1.Quantize(d2, ctx);
        } else if (op.Equals("add")) {
          d3 = d1.Add(d2, ctx);
        } else if (op.Equals("minus")) {
          d3 = d1.Negate(ctx);
        } else if (op.Equals("apply")) {
          d3 = d1.RoundToPrecision(ctx);
        } else if (op.Equals("plus")) {
          d3 = d1.Plus(ctx);
        } else {
if (op.Equals("and")) {
 d3 = EDecimals.And(d1, d2, ctx);
  } else if (op.Equals("or")) {
 d3 = EDecimals.Or(d1, d2, ctx);
  } else if (op.Equals("xor")) {
 d3 = EDecimals.Xor(d1, d2, ctx);
  } else if (op.Equals("invert")) {
 d3 = EDecimals.Invert(d1, ctx);
  } else if (op.Equals("rescale")) {
 d3 = EDecimals.Rescale(d1, d2, ctx);
  } else if (op.Equals("shift")) {
 d3 = EDecimals.Shift(d1, d2, ctx);
  } else if (op.Equals("rotate")) {
 d3 = EDecimals.Rotate(d1, d2, ctx);
  } else if (op.Equals("iscanonical")) {
 d3 = EDecimal.FromBoolean(EDecimals.IsCanonical(d1));
  } else if (op.Equals("isnan")) {
 Assert.AreEqual(EDecimals.IsNaN(d1), d1.IsNaN());
 d3 = EDecimal.FromBoolean(EDecimals.IsNaN(d1));
  } else if (op.Equals("issigned")) {
 Assert.AreEqual(EDecimals.IsSigned(d1), d1.IsNegative);
 d3 = EDecimal.FromBoolean(EDecimals.IsSigned(d1));
  } else if (op.Equals("isqnan")) {
 Assert.AreEqual(EDecimals.IsQuietNaN(d1), d1.IsQuietNaN());
 d3 = EDecimal.FromBoolean(EDecimals.IsQuietNaN(d1));
  } else if (op.Equals("issnan")) {
 Assert.AreEqual(EDecimals.IsSignalingNaN(d1), d1.IsSignalingNaN());
 d3 = EDecimal.FromBoolean(EDecimals.IsSignalingNaN(d1));
  } else if (op.Equals("isfinite")) {
 Assert.AreEqual(EDecimals.IsFinite(d1), d1.IsFinite);
 d3 = EDecimal.FromBoolean(EDecimals.IsFinite(d1));
  } else if (op.Equals("isinfinite")) {
 Assert.AreEqual(EDecimals.IsInfinite(d1), d1.IsInfinity());
 d3 = EDecimal.FromBoolean(EDecimals.IsInfinite(d1));
  } else if (op.Equals("issubnormal")) {
 d3 = EDecimal.FromBoolean(EDecimals.IsSubnormal(d1, ctx));
  } else if (op.Equals("isnormal")) {
 d3 = EDecimal.FromBoolean(EDecimals.IsNormal(d1, ctx));
  } else if (op.Equals("iszero")) {
 Assert.AreEqual(EDecimals.IsZero(d1), d1.IsZero);
 d3 = EDecimal.FromBoolean(EDecimals.IsZero(d1));
  } else if (op.Equals("logb")) {
 d3 = EDecimals.LogB(d1, ctx);
  } else if (op.Equals("scaleb")) {
 d3 = EDecimals.ScaleB(d1, d2, ctx);
  } else if (op.Equals("trim")) {
 d3 = EDecimals.Trim(d1, ctx);
  } else if (op.Equals("samequantum")) {
 d3 = EDecimal.FromBoolean(EDecimals.SameQuantum(d1, d2));
} else {
 Console.WriteLine("unknown op " + op);
          return;
}
        }
        bool invalid = flags.Contains("Division_impossible") ||
          flags.Contains("Division_undefined") ||
          flags.Contains("Invalid_operation");
        bool divzero = flags.Contains("Division_by_zero");
        var expectedFlags = 0;
        if (flags.Contains("Inexact") || flags.Contains("inexact")) {
          expectedFlags |= EContext.FlagInexact;
        }
        if (flags.Contains("Subnormal")) {
          expectedFlags |= EContext.FlagSubnormal;
        }
        if (flags.Contains("Rounded") || flags.Contains("rounded")) {
          expectedFlags |= EContext.FlagRounded;
        }
        if (flags.Contains("Underflow")) {
          expectedFlags |= EContext.FlagUnderflow;
        }
        if (flags.Contains("Overflow")) {
          expectedFlags |= EContext.FlagOverflow;
        }
        if (flags.Contains("Clamped")) {
          if (extended || clamp) {
            expectedFlags |= EContext.FlagClamped;
          }
        }
        if (flags.Contains("Lost_digits")) {
          expectedFlags |= EContext.FlagLostDigits;
        }
        bool conversionError = flags.Contains("Conversion_syntax");
        if (invalid) {
          expectedFlags |= EContext.FlagInvalid;
        }
        if (divzero) {
          expectedFlags |= EContext.FlagDivideByZero;
        }
        if (op.Equals("class")) {
            d1 = EDecimal.FromString(input1);
            string numclass = EDecimals.NumberClassString(
                    EDecimals.NumberClass(d1, ctx));
            Assert.AreEqual(output, numclass, input1);
        } else if (op.Equals("toSci") || op.Equals("tosci")) {
          try {
            d1 = EDecimal.FromString(input1, ctx);
            Assert.IsTrue(!conversionError, "Expected no conversion error");
            String converted = d1.ToString();
            if (!output.Equals("?")) {
              Assert.AreEqual(output, converted, input1);
            }
          } catch (FormatException) {
            Assert.IsTrue(conversionError, "Expected conversion error");
          }
        } else if (op.Equals("toEng") || op.Equals("toeng")) {
          try {
            d1 = EDecimal.FromString(input1, ctx);
            Assert.IsTrue(!conversionError, "Expected no conversion error");
            String converted = d1.ToEngineeringString();
            if (!output.Equals("?")) {
              Assert.AreEqual(output, converted, input1);
            }
          } catch (FormatException) {
            Assert.IsTrue(conversionError, "Expected conversion error");
          }
        } else {
          if (!output.Equals("?")) {
            if (output == null && d3 != null) {
              Assert.Fail(name + ": d3 must be null");
            }
            if (output != null && !d3.ToString().Equals(output)) {
              EDecimal d4 = EDecimal.FromString(output);
              {
                object objectTemp = output;
                object objectTemp2 = d3.ToString();
      string messageTemp = name + ": expected: [" + d4.UnsignedMantissa +
                " " + d4.Exponent +
                    "]\n" + "but was: [" + d3.UnsignedMantissa + " " +
                    d3.Exponent + "]\n" + ln;
                Assert.AreEqual(objectTemp, objectTemp2, messageTemp);
              }
            }
          }
        }
        // Don't check flags for five simplified arithmetic
        // test cases that say to set the rounded flag; the
        // extended arithmetic counterparts for at least
        // some of them have no flags in their
        // result.
        if (!name.Equals("pow118") &&
!name.Equals("pow119") &&
!name.Equals("pow120") &&
!name.Equals("pow121") &&
!name.Equals("pow122")) {
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      }
    }

    public static void AssertFlags(int expected, int actual, string name) {
      if (expected == actual) {
        return;
      }
      Assert.AreEqual(
        (expected & EContext.FlagInexact) != 0,
        (actual & EContext.FlagInexact) != 0,
        name + ": Inexact");
      Assert.AreEqual(
        (expected & EContext.FlagRounded) != 0,
        (actual & EContext.FlagRounded) != 0,
        name + ": Rounded");
      Assert.AreEqual(
        (expected & EContext.FlagSubnormal) != 0,
        (actual & EContext.FlagSubnormal) != 0,
        name + ": Subnormal");
      Assert.AreEqual(
        (expected & EContext.FlagOverflow) != 0,
        (actual & EContext.FlagOverflow) != 0,
        name + ": Overflow");
      Assert.AreEqual(
        (expected & EContext.FlagUnderflow) != 0,
        (actual & EContext.FlagUnderflow) != 0,
        name + ": Underflow");
      Assert.AreEqual(
        (expected & EContext.FlagClamped) != 0,
        (actual & EContext.FlagClamped) != 0,
        name + ": Clamped");
      Assert.AreEqual(
        (expected & EContext.FlagInvalid) != 0,
        (actual & EContext.FlagInvalid) != 0,
        name + ": Invalid");
      Assert.AreEqual(
        (expected & EContext.FlagDivideByZero) != 0,
        (actual & EContext.FlagDivideByZero) != 0,
        name + ": DivideByZero");
      Assert.AreEqual(
        (expected & EContext.FlagLostDigits) != 0,
        (actual & EContext.FlagLostDigits) != 0,
        name + ": LostDigits");
    }

    internal static void PrintTime(System.Diagnostics.Stopwatch sw) {
      Console.WriteLine("Elapsed time: " + (sw.ElapsedMilliseconds / 1000.0) +
                    " s");
    }

    [Test]
    public void TestPi() {
      var sw = new System.Diagnostics.Stopwatch();
      sw.Start();
      EDecimal.PI(EContext.ForPrecision(1000)).ToString();
      sw.Stop();
      PrintTime(sw);
    }

    private static decimal RandomDecimal(RandomGenerator rand) {
      int a, b, c;
      a = rand.UniformInt(0x10000);
      a = unchecked((a << 16) + rand.UniformInt(0x10000));
      b = rand.UniformInt(0x10000);
      b = unchecked((b << 16) + rand.UniformInt(0x10000));
      c = rand.UniformInt(0x10000);
      c = unchecked((c << 16) + rand.UniformInt(0x10000));
      int scale = rand.UniformInt(29);
      return new Decimal(a, b, c, rand.UniformInt(2) == 0, (byte)scale);
    }

    [Test]
    public void TestDecimalString() {
      var fr = new RandomGenerator();
      for (var i = 0; i < 10000; ++i) {
        EDecimal ed = RandomObjects.RandomEDecimal(fr);
        if (!ed.IsFinite) {
          continue;
        }
        decimal d;
        try {
          System.Globalization.NumberStyles numstyles =
            System.Globalization.NumberStyles.AllowExponent |
System.Globalization.NumberStyles.Number;
          d = Decimal.Parse(
  ed.ToString(),
  numstyles,
  System.Globalization.CultureInfo.InvariantCulture);
          EDecimal ed3 = EDecimal.FromString(
  ed.ToString(),
  EContext.CliDecimal);
          string msg = ed.ToString() + " (expanded: " +
            EDecimal.FromString(ed.ToString()) + ")";
          TestCommon.CompareTestEqual(
            (EDecimal)d,
            ed3,
            msg);
        } catch (OverflowException ex) {
          EDecimal ed2 = EDecimal.FromString(
  ed.ToString(),
  EContext.CliDecimal);
          Assert.IsTrue(
  ed2.IsInfinity(),
  ed.ToString(),
  ex.ToString());
        }
      }
    }

    [Test]
    public static void TestUint64() {
      EInteger ei = EInteger.FromString("9223372036854775808");
      Assert.AreEqual((ulong)9223372036854775808, ei.ToUInt64Checked());
      Assert.AreEqual((ulong)9223372036854775808, ei.ToUInt64Unchecked());
    }

    [Test]
    public static void TestToDecimal() {
      try {
        EDecimal.FromString("8.8888888e-7").ToDecimal();
} catch (Exception ex) {
Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
      try {
 EDecimal.FromString("8.8888888e-8").ToDecimal();
} catch (Exception ex) {
Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
      try {
 EDecimal.FromString("8.8888888e-18").ToDecimal();
} catch (Exception ex) {
Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
    }

    [Test]
    public void TestDecimal() {
      var fr = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        decimal d = RandomDecimal(fr);
        EDecimal ed = d;
        TestCommon.CompareTestEqual(d, (decimal)ed, ed.ToString());
        EDecimal ed2 =

  EDecimal.FromString(d.ToString(System.Globalization.CultureInfo.InvariantCulture));
        TestCommon.CompareTestEqual(ed, ed2);
      }
    }

    [Test]
public void TestToUintChecked() {
Assert.AreEqual((byte)0, EDecimal.FromString("-0.1").ToUInt16Checked());
Assert.AreEqual((byte)0, EDecimal.FromString("-0.4").ToUInt16Checked());
Assert.AreEqual((byte)0, EDecimal.FromString("-0.5").ToUInt16Checked());
Assert.AreEqual((byte)0, EDecimal.FromString("-0.6").ToUInt16Checked());
try {
 EDecimal.FromString("-1.0").ToUInt16Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
try {
 EDecimal.FromString("-1.4").ToUInt16Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
try {
 EDecimal.FromString("-1.5").ToUInt16Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
try {
 EDecimal.FromString("-1.6").ToUInt16Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
Assert.AreEqual((byte)0, EDecimal.FromString("-0.1").ToUInt32Checked());
Assert.AreEqual((byte)0, EDecimal.FromString("-0.4").ToUInt32Checked());
Assert.AreEqual((byte)0, EDecimal.FromString("-0.5").ToUInt32Checked());
Assert.AreEqual((byte)0, EDecimal.FromString("-0.6").ToUInt32Checked());
try {
 EDecimal.FromString("-1.0").ToUInt32Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
try {
 EDecimal.FromString("-1.4").ToUInt32Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
try {
 EDecimal.FromString("-1.5").ToUInt32Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
try {
 EDecimal.FromString("-1.6").ToUInt32Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
Assert.AreEqual((byte)0, EDecimal.FromString("-0.1").ToUInt64Checked());
Assert.AreEqual((byte)0, EDecimal.FromString("-0.4").ToUInt64Checked());
Assert.AreEqual((byte)0, EDecimal.FromString("-0.5").ToUInt64Checked());
Assert.AreEqual((byte)0, EDecimal.FromString("-0.6").ToUInt64Checked());
try {
 EDecimal.FromString("-1.0").ToUInt64Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
try {
 EDecimal.FromString("-1.4").ToUInt64Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
try {
 EDecimal.FromString("-1.5").ToUInt64Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
try {
 EDecimal.FromString("-1.6").ToUInt64Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
}

    [Test]
    public void TestParser() {
         this.TestParserEx(false);
         this.TestParserEx(true);
    }

    public void TestParserEx(bool recordfailing) {
      long failures = 0;
      var testfiles = CBOR.ExtensiveTest.GetTestFiles();
      if (testfiles.Length == 0) {
 return;
}
      string failingpath = Path.Combine(
  Path.GetDirectoryName(testfiles[0]),
  "failing.decTest");
      if (recordfailing && File.Exists(failingpath)) {
 return;
}
          var failedLines = new Dictionary<string, bool>();
          var sb = new System.Text.StringBuilder();
        // Reads decimal test files described in:
        // <http://speleotrove.com/decimal/dectest.html>
        foreach (var f in testfiles) {
          if (!Path.GetFileName(f).ToLowerInvariant().Contains(
               recordfailing ? ".dectest" : "failing.dectest")) {
            continue;
          }
          Console.WriteLine(f);
          var context = new Dictionary<string, string>();
          using (var w = new StreamReader(f)) {
            while (!w.EndOfStream) {
              string ln = w.ReadLine();
// if (!ln.Contains(" 0E") && !ln.Contains(" -0E")) {
// continue;
// }
// if (!ln.Contains("plus") &&
// !ln.Contains("minus") &&
// !ln.Contains("subtr") &&
// !ln.Contains("fma") &&
// !ln.Contains("add")) {
// continue;
// }
// if (ln.Contains("#")) {
// continue;
// }
// Console.WriteLine(ln);
try {
if (recordfailing) {
 Timeout(5000, () => ParseDecTest(ln, context), String.Empty);
} else {
 Timeout(15000, () => ParseDecTest(ln, context), ln);
}
} catch (Exception) {
   if (!failedLines.ContainsKey(ln)) {
   if (!context.ContainsKey("rounding")) {
 context["rounding"] = "half_even";
}
   if (!context.ContainsKey("extended")) {
 context["extended"] = "1";
}
   if (!context.ContainsKey("clamp")) {
 context["clamp"] = "0";
}
   foreach (var k in context.Keys) {
       sb.Append(k).Append(": ").Append(context[k])
            .Append("\r\n");
   }
   sb.Append(ln).Append("\r\n");
   failedLines[ln] = true;
   }
   ++failures;
}
            }
          }
        }
      if (failures > 0) {
 if (recordfailing) {
 File.WriteAllText(failingpath, sb.ToString());
 }
        Assert.Fail(failures + " failure(s)");
      } else {
File.Delete(failingpath);
      }
    }
  }
}
