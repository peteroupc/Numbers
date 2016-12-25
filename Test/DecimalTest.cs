/*
Written in 2013 by Peter O.
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
        int precision = Convert.ToInt32(
  context["precision"],
  System.Globalization.CultureInfo.InvariantCulture);
        int minexponent = Convert.ToInt32(
  context["minexponent"],
  System.Globalization.CultureInfo.InvariantCulture);
        int maxexponent = Convert.ToInt32(
  context["maxexponent"],
  System.Globalization.CultureInfo.InvariantCulture);
        // Skip tests that take null as input or output;
        // also skip tests that take a hex number format
        if (input1.Contains("#") || input2.Contains("#") ||
            input3.Contains("#") || output.Contains("#")) {
          return;
        }
        // Skip some tests that assume a maximum
        // supported precision of 999999999
        if (name.Equals("pow250") || name.Equals("pow251") ||
            name.Equals("pow252")) {
          return;
        }
        // Skip some test cases that are incorrect
        // (all simplified arithmetic test cases)
        if (!extended) {
          if (name.Equals("ln116") ||
              name.Equals("qua530") || // assumes that the input will underflow
                    // to 0
              name.Equals("qua531") || // assumes that the input will underflow
                    // to 0
              name.Equals("rpow068") || name.Equals("rpow159") ||
              name.Equals("rpow217") || name.Equals("rpow272") ||
              name.Equals("rpow324") || name.Equals("rpow327") ||
              // following cases incorrectly remove trailing zeros
              name.Equals("sqtx2207") || name.Equals("sqtx2231") ||
              name.Equals("sqtx2271") || name.Equals("sqtx2327") ||
              name.Equals("sqtx2399") || name.Equals("sqtx2487") ||
              name.Equals("sqtx2591") || name.Equals("sqtx2711") ||
              name.Equals("sqtx2847")) {
            return;
          }
        }
        if (input1.Contains("?")) {
          return;
        }
        if (flags.Contains("Invalid_context")) {
          return;
        }
        EContext ctx = EContext.ForPrecision(precision)
          .WithExponentClamp(clamp).WithExponentRange(
            minexponent,
            maxexponent);
        string rounding = context["rounding"];
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
          ctx = ctx.WithRounding(ERounding.ZeroFiveUp);
        }
        if (!extended) {
          ctx = ctx.WithSimplified(true);
        }
        ctx = ctx.WithBlankFlags();
        EDecimal d1 = EDecimal.Zero, d2 = null, d2a = null;
        if (!op.Equals("toSci") && !op.Equals("toEng")) {
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
        } else if (op.Equals("toSci")) { // handled below
        } else if (op.Equals("toEng")) { // handled below
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
          d3 = d1.CompareToTotal(d2, ctx);
        } else if (op.Equals("comparetotmag")) {
          d3 = EDecimal.FromInt32(d1.CompareToTotalMagnitude(d2));
        } else if (op.Equals("copyabs")) {
          d3 = d1.Abs();
        } else if (op.Equals("copynegate")) {
          d3 = d1.Negate();
        } else if (op.Equals("copysign")) {
          d3 = d1.CopySign(d2);
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
          d3 = d1.Log(ctx);
        } else if (op.Equals("log10")) {
          d3 = d1.Log10(ctx);
        } else if (op.Equals("power")) {
          d3 = d1.Pow(d2, ctx);
        } else if (op.Equals("squareroot")) {
          d3 = d1.Sqrt(ctx);
        } else if (op.Equals("remaindernear")) {
          d3 = d1.RemainderNear(d2, ctx);
        } else if (op.Equals("nexttoward")) {
          d3 = d1.NextToward(d2, ctx);
        } else if (op.Equals("nextplus")) {
          d3 = d1.NextPlus(ctx);
        } else if (op.Equals("nextminus")) {
          d3 = d1.NextMinus(ctx);
        } else if (op.Equals("copy")) {
          d3 = d1;
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
          // Console.WriteLine("unknown op "+op);
          return;
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
        if (op.Equals("toSci")) {
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
        } else if (op.Equals("toEng")) {
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
                    "]\\n" + "but was: [" + d3.UnsignedMantissa + " " +
                    d3.Exponent + "]";
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
        if (!name.Equals("pow118") && !name.Equals("pow119") &&
            !name.Equals("pow120") && !name.Equals("pow121") &&
            !name.Equals("pow122")) {
          AssertFlags(expectedFlags, ctx.Flags, name);
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
      for (var i = 0; i < 1000; ++i) {
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
    public void TestParser() {
      long failures = 0;
      for (var i = 0; i < 1; ++i) {
        // Reads decimal test files described in:
        // <http://speleotrove.com/decimal/dectest.html>
        foreach (var f in CBOR.ExtensiveTest.GetTestFiles()) {
          if (!Path.GetFileName(f).Contains(".decTest")) {
            continue;
          }
          Console.WriteLine(f);
          IDictionary<string, string> context =
            new Dictionary<string, string>();
           using (var fileStream = File.Open(f, FileMode.Open))
           using (var w = new StreamReader(fileStream)) {
            while (!w.EndOfStream) {
              string ln = w.ReadLine();
              {
                try {
                  TextWriter oldOut = Console.Out;
                  try {
                    Console.SetOut(TextWriter.Null);
                    ParseDecTest(ln, context);
                  } catch (Exception) {
                    Console.SetOut(oldOut);
                    ParseDecTest(ln, context);
                  } finally {
                    Console.SetOut(oldOut);
                  }
                } catch (Exception ex) {
                  Console.WriteLine(ln);
                  Console.WriteLine(ex.Message);
                  Console.WriteLine(ex.StackTrace);
                  ++failures;
                  // throw;
                }
              }
            }
          }
        }
      }
      if (failures > 0) {
        Assert.Fail(failures + " failure(s)");
      }
    }
  }
}
