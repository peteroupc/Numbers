using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using PeterO;
using PeterO.Numbers;
namespace Test {
  public static class DecTestUtil {
    private static readonly Regex ValuePropertyLine = new Regex(
      "^(\\w+)\\:\\s*(\\S+)",
      RegexOptions.Compiled);

    private static readonly Regex ValueQuotes = new Regex(
      "^[\\'\\\"]|[\\'\\\"]$",
      RegexOptions.Compiled);

    private static readonly Regex ValueTestLine = new Regex(
  "^([A-Za-z0-9_]+)\\s+([A-Za-z0-9_\\-]+)\\s+(\\'[^\\']*\\'|\\S+)\\s+(?:(\\S+)\\s+)?(?:(\\S+)\\s+)?->\\s+(\\S+)\\s*(.*)",
  RegexOptions.Compiled);

    /// <summary>Returns a string with the basic upper-case letters A to Z
    /// (U + 0041 to U + 005A) converted to lower-case. Other characters
    /// remain unchanged.</summary>
    /// <param name='str'>The parameter <paramref name='str'/> is a text
    /// string.</param>
    /// <returns>The converted string, or null if <paramref name='str'/> is
    /// null.</returns>
    public static string ToLowerCaseAscii(string str) {
      if (str == null) {
        return null;
      }
      var len = str.Length;
      var c = (char)0;
      var hasUpperCase = false;
      for (var i = 0; i < len; ++i) {
        c = str[i];
        if (c >= 'A' && c <= 'Z') {
          hasUpperCase = true;
          break;
        }
      }
      if (!hasUpperCase) {
        return str;
      }
      var builder = new StringBuilder();
      for (var i = 0; i < len; ++i) {
        c = str[i];
        if (c >= 'A' && c <= 'Z') {
          builder.Append((char)(c + 0x20));
        } else {
          builder.Append(c);
        }
      }
      return builder.ToString();
    }

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

    public static string ParseJSONString(string str) {
      int c;
      if (str == null) {
        throw new ArgumentNullException(nameof(str));
      }
      if (str.Length == 0 || str[0] != '"') {
        return null;
      }
      var index = 1;
      var sb = new StringBuilder();
      while (index < str.Length) {
        c = index >= str.Length ? -1 : str[index++];
        if (c == -1 || c < 0x20) {
          return null;
        }
        if ((c & 0xfc00) == 0xd800 && index < str.Length &&
          (str[index] & 0xfc00) == 0xdc00) {
          // Get the Unicode code point for the surrogate pair
          c = 0x10000 + ((c & 0x3ff) << 10) + (str[index] & 0x3ff);
          ++index;
        } else if ((c & 0xf800) == 0xd800) {
          return null;
        }
        switch (c) {
          case '\\':
            c = index >= str.Length ? -1 : str[index++];
            switch (c) {
              case '\\':
                sb.Append('\\');
                break;
              case '/':
                // Now allowed to be escaped under RFC 8259
                sb.Append('/');
                break;
              case '\"':
                sb.Append('\"');
                break;
              case 'b':
                sb.Append('\b');
                break;
              case 'f':
                sb.Append('\f');
                break;
              case 'n':
                sb.Append('\n');
                break;
              case 'r':
                sb.Append('\r');
                break;
              case 't':
                sb.Append('\t');
                break;
              case 'u': { // Unicode escape
                  c = 0;
                  // Consists of 4 hex digits
                  for (var i = 0; i < 4; ++i) {
                    int ch = index >= str.Length ? -1 : str[index++];
                    if (ch >= '0' && ch <= '9') {
                      c <<= 4;
                      c |= ch - '0';
                    } else if (ch >= 'A' && ch <= 'F') {
                      c <<= 4;
                      c |= ch + 10 - 'A';
                    } else if (ch >= 'a' && ch <= 'f') {
                      c <<= 4;
                      c |= ch + 10 - 'a';
                    } else {
                      return null;
                    }
                  }
                  if ((c & 0xf800) != 0xd800) {
                    // Non-surrogate
                    sb.Append((char)c);
                  } else if ((c & 0xfc00) == 0xd800) {
                    int ch = index >= str.Length ? -1 : str[index++];
                    if (ch != '\\' ||
                       (index >= str.Length ? -1 : str[index++]) != 'u') {
                      return null;
                    }
                    var c2 = 0;
                    for (var i = 0; i < 4; ++i) {
                      ch = index >= str.Length ? -1 : str[index++];
                      if (ch >= '0' && ch <= '9') {
                        c2 <<= 4;
                        c2 |= ch - '0';
                      } else if (ch >= 'A' && ch <= 'F') {
                        c2 <<= 4;
                        c2 |= ch + 10 - 'A';
                      } else if (ch >= 'a' && ch <= 'f') {
                        c2 <<= 4;
                        c2 |= ch + 10 - 'a';
                      } else {
                        return null;
                      }
                    }
                    if ((c2 & 0xfc00) != 0xdc00) {
                      return null;
                    } else {
                      sb.Append((char)c);
                      sb.Append((char)c2);
                    }
                  } else {
                    return null;
                  }
                  break;
                }
              default: {
                  // NOTE: Includes surrogate code
                  // units
                  return null;
                }
            }
            break;
          case 0x22: // double quote
            return sb.ToString();
          default: {
              // NOTE: Assumes the character reader
              // throws an error on finding illegal surrogate
              // pairs in the string or invalid encoding
              // in the stream
              if ((c >> 16) == 0) {
                sb.Append((char)c);
              } else {
                sb.Append((char)((((c - 0x10000) >> 10) & 0x3ff) | 0xd800));
                sb.Append((char)(((c - 0x10000) & 0x3ff) | 0xdc00));
              }
              break;
            }
        }
      }
      return null;
    }

    public static void ParseDecTest(
  string ln,
  IDictionary<string, string> context) {
      Match match;
      if (ln == null) {
        throw new ArgumentNullException(nameof(ln));
      }
      if (ln.Contains("-- ")) {
        ln = ln.Substring(0, ln.IndexOf("-- ", StringComparison.Ordinal));
      }
      match = (!ln.Contains(":")) ? null : ValuePropertyLine.Match(ln);
      if (match != null && match.Success) {
        string paramName = ToLowerCaseAscii(
           match.Groups[1].ToString());
        if (context == null) {
          throw new ArgumentNullException(nameof(context));
        }
        context[paramName] = match.Groups[2].ToString();
        return;
      }
      // var sw = new System.Diagnostics.Stopwatch();
      // sw.Start();
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
        if (context == null) {
          throw new ArgumentNullException(nameof(context));
        }
        bool extended = GetKeyOrDefault(context, "extended",
  "1").Equals("1", StringComparison.Ordinal);
        bool clamp = GetKeyOrDefault(context, "clamp", "0").Equals("1",
  StringComparison.Ordinal);
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
        if (name.Equals("pow250", StringComparison.Ordinal) ||
name.Equals("pow251", StringComparison.Ordinal) ||
name.Equals("pow252", StringComparison.Ordinal)) {
          return;
        }
        // Assumes a maximum supported
        // value for pow exponent
        if (name.Equals("powx4008", StringComparison.Ordinal)) {
          return;
        }
        // Skip these unofficial test cases which involve
        // pretruncation of the first operand of a shift operation;
        // actually, the Gen. Decimal Arithmetic Spec. doesn't
        // say to truncate that operand's coefficient before
        // the shift operation, unlike for the rotate operation.
        if (name.Equals("extr1651", StringComparison.Ordinal) ||
name.Equals("extr1652", StringComparison.Ordinal) ||
name.Equals("extr1653", StringComparison.Ordinal) ||
name.Equals("extr1654", StringComparison.Ordinal)) {
          return;
        }
        // Skip these unofficial test cases, which misapply the
        // 'clamp' directive to NaNs in the test case format:
        // 'clamp' governs only exponent clamping;
        // although exponent clamping can
        // indirectly affect the coefficient in certain cases, NaNs
        // have neither a coefficient nor an exponent, so the
        // 'clamp' directive doesn't properly apply to NaNs
        if (name.Equals("covx5076", StringComparison.Ordinal) ||
name.Equals("covx5082", StringComparison.Ordinal) ||
name.Equals("covx5085", StringComparison.Ordinal) ||
name.Equals("covx5086", StringComparison.Ordinal)) {
          return;
        }
        // Skip these unofficial test cases, which are incorrect
        // for expecting underflow on raising a huge
        // positive integer to its own power
        if (name.Equals("power_eq4", StringComparison.Ordinal) ||
name.Equals("power_eq46", StringComparison.Ordinal) ||
name.Equals("power_eq48", StringComparison.Ordinal) ||
name.Equals("power_eq11", StringComparison.Ordinal) ||
name.Equals("power_eq65", StringComparison.Ordinal) ||
name.Equals("power_eq84", StringComparison.Ordinal) ||
name.Equals("power_eq94", StringComparison.Ordinal)) {
          return;
        }
        // Skip some test cases that are incorrect
        // (all simplified arithmetic test cases)
        if (!extended) {
          if (
  // result expected by test case is wrong by > 0.5 ULP
  name.Equals("ln116", StringComparison.Ordinal) ||
// assumes that the input will underflow to 0
name.Equals("qua530", StringComparison.Ordinal) ||
              // assumes that the input will underflow to 0
              name.Equals("qua531", StringComparison.Ordinal) ||
name.Equals("rpow068", StringComparison.Ordinal) ||
name.Equals("rpow159", StringComparison.Ordinal) ||
name.Equals("rpow217", StringComparison.Ordinal) ||
name.Equals("rpow272", StringComparison.Ordinal) ||
name.Equals("rpow324", StringComparison.Ordinal) ||
name.Equals("rpow327", StringComparison.Ordinal) ||
              // following cases incorrectly remove trailing zeros
              name.Equals("sqtx2207", StringComparison.Ordinal) ||
name.Equals("sqtx2231", StringComparison.Ordinal) ||
name.Equals("sqtx2271", StringComparison.Ordinal) ||
name.Equals("sqtx2327", StringComparison.Ordinal) ||
name.Equals("sqtx2399", StringComparison.Ordinal) ||
name.Equals("sqtx2487", StringComparison.Ordinal) ||
name.Equals("sqtx2591", StringComparison.Ordinal) ||
name.Equals("sqtx2711", StringComparison.Ordinal) ||
name.Equals("sqtx2847", StringComparison.Ordinal)) {
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
        if (rounding.Equals("half_up", StringComparison.Ordinal)) {
          ctx = ctx.WithRounding(ERounding.HalfUp);
        }
        if (rounding.Equals("half_down", StringComparison.Ordinal)) {
          ctx = ctx.WithRounding(ERounding.HalfDown);
        }
        if (rounding.Equals("half_even", StringComparison.Ordinal)) {
          ctx = ctx.WithRounding(ERounding.HalfEven);
        }
        if (rounding.Equals("up", StringComparison.Ordinal)) {
          ctx = ctx.WithRounding(ERounding.Up);
        }
        if (rounding.Equals("down", StringComparison.Ordinal)) {
          ctx = ctx.WithRounding(ERounding.Down);
        }
        if (rounding.Equals("ceiling", StringComparison.Ordinal)) {
          ctx = ctx.WithRounding(ERounding.Ceiling);
        }
        if (rounding.Equals("floor", StringComparison.Ordinal)) {
          ctx = ctx.WithRounding(ERounding.Floor);
        }
        if (rounding.Equals("05up", StringComparison.Ordinal)) {
          // NOTE: This rounding mode is like Odd in the case
          // of binary numbers, and ZeroFiveUp in the case of
          // decimal numbers
          ctx = ctx.WithRounding(ERounding.OddOrZeroFiveUp);
        }
        if (!extended) {
          ctx = ctx.WithSimplified(true);
        }
        ctx = ctx.WithBlankFlags();
        if (op.Length > 3 && op.Substring(op.Length - 3).Equals("_eq",
  StringComparison.Ordinal)) {
          // Binary operators with both operands the same
          input2 = input1;
          op = op.Substring(0, op.Length - 3);
        }
        EDecimal d1 = EDecimal.Zero, d2 = null, d2a = null;
        if (!op.Equals("toSci", StringComparison.Ordinal) &&
!op.Equals("toEng", StringComparison.Ordinal) &&
!op.Equals("tosci", StringComparison.Ordinal) &&
!op.Equals("toeng", StringComparison.Ordinal) &&
!op.Equals("class", StringComparison.Ordinal) &&
!op.Equals("format", StringComparison.Ordinal)) {
          d1 = String.IsNullOrEmpty(input1) ? EDecimal.Zero :
            EDecimal.FromString(input1);
          d2 = String.IsNullOrEmpty(input2) ? null :
            EDecimal.FromString(input2);
          d2a = String.IsNullOrEmpty(input3) ? null :
            EDecimal.FromString(input3);
        }
        EDecimal d3 = null;
        if (op.Equals("fma", StringComparison.Ordinal) && !extended) {
          // This implementation does implement multiply-and-add
          // in the simplified arithmetic, even though the test cases expect
          // an invalid operation to be raised. This seems to be allowed
          // under appendix A, which merely says that multiply-and-add
          // "is not defined" in the simplified arithmetic.
          return;
        }
        if (op.Equals("multiply", StringComparison.Ordinal)) {
          d3 = d1.Multiply(d2, ctx);
        } else if (op.Equals("toSci", StringComparison.Ordinal)) {
// handled below
        } else if (op.Equals("toEng", StringComparison.Ordinal)) {
// handled below
        } else if (op.Equals("tosci", StringComparison.Ordinal)) {
// handled below
        } else if (op.Equals("toeng", StringComparison.Ordinal)) {
// handled below
        } else if (op.Equals("class", StringComparison.Ordinal)) {
// handled below
        } else if (op.Equals("fma", StringComparison.Ordinal)) {
          d3 = d1.MultiplyAndAdd(d2, d2a, ctx);
        } else if (op.Equals("min", StringComparison.Ordinal)) {
          d3 = EDecimal.Min(d1, d2, ctx);
        } else if (op.Equals("max", StringComparison.Ordinal)) {
          d3 = EDecimal.Max(d1, d2, ctx);
        } else if (op.Equals("minmag", StringComparison.Ordinal)) {
          d3 = EDecimal.MinMagnitude(d1, d2, ctx);
        } else if (op.Equals("maxmag", StringComparison.Ordinal)) {
          d3 = EDecimal.MaxMagnitude(d1, d2, ctx);
        } else if (op.Equals("compare", StringComparison.Ordinal)) {
          d3 = d1.CompareToWithContext(d2, ctx);
        } else if (op.Equals("comparetotal", StringComparison.Ordinal)) {
          int id3 = d1.CompareToTotal(d2, ctx);
          d3 = EDecimal.FromInt32(id3);
          Assert.AreEqual(id3, EDecimals.CompareTotal(d1, d2, ctx), ln);
        } else if (op.Equals("comparetotmag", StringComparison.Ordinal)) {
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
        } else if (op.Equals("copyabs", StringComparison.Ordinal)) {
          d3 = d1.Abs();
          Assert.AreEqual(d3, EDecimals.CopyAbs(d1));
        } else if (op.Equals("copynegate", StringComparison.Ordinal)) {
          d3 = d1.Negate();
          Assert.AreEqual(d3, EDecimals.CopyNegate(d1));
        } else if (op.Equals("copysign", StringComparison.Ordinal)) {
          d3 = d1.CopySign(d2);
          Assert.AreEqual(d3, EDecimals.CopySign(d1, d2));
        } else if (op.Equals("comparesig", StringComparison.Ordinal)) {
          d3 = d1.CompareToSignal(d2, ctx);
        } else if (op.Equals("subtract", StringComparison.Ordinal)) {
          d3 = d1.Subtract(d2, ctx);
        } else if (op.Equals("tointegral", StringComparison.Ordinal)) {
          d3 = d1.RoundToIntegerNoRoundedFlag(ctx);
        } else if (op.Equals("tointegralx", StringComparison.Ordinal)) {
          d3 = d1.RoundToIntegerExact(ctx);
        } else if (op.Equals("divideint", StringComparison.Ordinal)) {
          d3 = d1.DivideToIntegerZeroScale(d2, ctx);
        } else if (op.Equals("divide", StringComparison.Ordinal)) {
          d3 = d1.Divide(d2, ctx);
        } else if (op.Equals("remainder", StringComparison.Ordinal)) {
          d3 = d1.Remainder(d2, ctx);
        } else if (op.Equals("exp", StringComparison.Ordinal)) {
          d3 = d1.Exp(ctx);
        } else if (op.Equals("ln", StringComparison.Ordinal)) {
          // NOTE: Gen. Decimal Arithmetic Spec.'s ln supports
          // only round-half-even mode, but EDecimal Log is not limited
          // to that rounding mode
          ctx = ctx.WithRounding(ERounding.HalfEven);
          d3 = d1.Log(ctx);
        } else if (op.Equals("log10", StringComparison.Ordinal)) {
          // NOTE: Gen. Decimal Arithmetic Spec.'s log10 supports
          // only round-half-even mode, but EDecimal Log10 is not limited
          // to that rounding mode
          ctx = ctx.WithRounding(ERounding.HalfEven);
          d3 = d1.Log10(ctx);
        } else if (op.Equals("power", StringComparison.Ordinal)) {
          if (d2a != null) {
            Console.WriteLine("Three-op power not yet supported");
            return;
          }
          d3 = d1.Pow(d2, ctx);
        } else if (op.Equals("squareroot", StringComparison.Ordinal)) {
          d3 = d1.Sqrt(ctx);
        } else if (op.Equals("remaindernear", StringComparison.Ordinal) ||
op.Equals("remainderNear", StringComparison.Ordinal)) {
          d3 = d1.RemainderNear(d2, ctx);
        } else if (op.Equals("nexttoward", StringComparison.Ordinal)) {
          d3 = d1.NextToward(d2, ctx);
        } else if (op.Equals("nextplus", StringComparison.Ordinal)) {
          d3 = d1.NextPlus(ctx);
        } else if (op.Equals("nextminus", StringComparison.Ordinal)) {
          d3 = d1.NextMinus(ctx);
        } else if (op.Equals("copy", StringComparison.Ordinal)) {
          d3 = d1;
          Assert.AreEqual(d3, EDecimals.Copy(d1), "copy equiv");
        } else if (op.Equals("abs", StringComparison.Ordinal)) {
          d3 = d1.Abs(ctx);
        } else if (op.Equals("reduce", StringComparison.Ordinal)) {
          d3 = d1.Reduce(ctx);
        } else if (op.Equals("quantize", StringComparison.Ordinal)) {
          d3 = d1.Quantize(d2, ctx);
        } else if (op.Equals("add", StringComparison.Ordinal)) {
          d3 = d1.Add(d2, ctx);
        } else if (op.Equals("minus", StringComparison.Ordinal)) {
          d3 = d1.Negate(ctx);
        } else if (op.Equals("apply", StringComparison.Ordinal)) {
          d3 = d1.RoundToPrecision(ctx);
        } else if (op.Equals("plus", StringComparison.Ordinal)) {
          d3 = d1.Plus(ctx);
        } else {
          if (op.Equals("and", StringComparison.Ordinal)) {
            d3 = EDecimals.And(d1, d2, ctx);
          } else if (op.Equals("or", StringComparison.Ordinal)) {
            d3 = EDecimals.Or(d1, d2, ctx);
          } else if (op.Equals("xor", StringComparison.Ordinal)) {
            d3 = EDecimals.Xor(d1, d2, ctx);
          } else if (op.Equals("invert", StringComparison.Ordinal)) {
            d3 = EDecimals.Invert(d1, ctx);
          } else if (op.Equals("rescale", StringComparison.Ordinal)) {
            d3 = EDecimals.Rescale(d1, d2, ctx);
          } else if (op.Equals("shift", StringComparison.Ordinal)) {
            d3 = EDecimals.Shift(d1, d2, ctx);
          } else if (op.Equals("rotate", StringComparison.Ordinal)) {
            d3 = EDecimals.Rotate(d1, d2, ctx);
          } else if (op.Equals("iscanonical", StringComparison.Ordinal)) {
            d3 = EDecimal.FromBoolean(EDecimals.IsCanonical(d1));
          } else if (op.Equals("isnan", StringComparison.Ordinal)) {
            Assert.AreEqual(EDecimals.IsNaN(d1), d1.IsNaN());
            d3 = EDecimal.FromBoolean(EDecimals.IsNaN(d1));
          } else if (op.Equals("issigned", StringComparison.Ordinal)) {
            Assert.AreEqual(EDecimals.IsSigned(d1), d1.IsNegative);
            d3 = EDecimal.FromBoolean(EDecimals.IsSigned(d1));
          } else if (op.Equals("isqnan", StringComparison.Ordinal)) {
            Assert.AreEqual(EDecimals.IsQuietNaN(d1), d1.IsQuietNaN());
            d3 = EDecimal.FromBoolean(EDecimals.IsQuietNaN(d1));
          } else if (op.Equals("issnan", StringComparison.Ordinal)) {
            Assert.AreEqual(EDecimals.IsSignalingNaN(d1), d1.IsSignalingNaN());
            d3 = EDecimal.FromBoolean(EDecimals.IsSignalingNaN(d1));
          } else if (op.Equals("isfinite", StringComparison.Ordinal)) {
            Assert.AreEqual(EDecimals.IsFinite(d1), d1.IsFinite);
            d3 = EDecimal.FromBoolean(EDecimals.IsFinite(d1));
          } else if (op.Equals("isinfinite", StringComparison.Ordinal)) {
            Assert.AreEqual(EDecimals.IsInfinite(d1), d1.IsInfinity());
            d3 = EDecimal.FromBoolean(EDecimals.IsInfinite(d1));
          } else if (op.Equals("issubnormal", StringComparison.Ordinal)) {
            d3 = EDecimal.FromBoolean(EDecimals.IsSubnormal(d1, ctx));
          } else if (op.Equals("isnormal", StringComparison.Ordinal)) {
            d3 = EDecimal.FromBoolean(EDecimals.IsNormal(d1, ctx));
          } else if (op.Equals("iszero", StringComparison.Ordinal)) {
            Assert.AreEqual(EDecimals.IsZero(d1), d1.IsZero);
            d3 = EDecimal.FromBoolean(EDecimals.IsZero(d1));
          } else if (op.Equals("logb", StringComparison.Ordinal)) {
            d3 = EDecimals.LogB(d1, ctx);
          } else if (op.Equals("scaleb", StringComparison.Ordinal)) {
            d3 = EDecimals.ScaleB(d1, d2, ctx);
          } else if (op.Equals("trim", StringComparison.Ordinal)) {
            d3 = EDecimals.Trim(d1, ctx);
          } else if (op.Equals("samequantum", StringComparison.Ordinal)) {
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
        if (op.Equals("class", StringComparison.Ordinal)) {
          d1 = EDecimal.FromString(input1);
          string numclass = EDecimals.NumberClassString(
                  EDecimals.NumberClass(d1, ctx));
          Assert.AreEqual(output, numclass, input1);
        } else if (op.Equals("toSci", StringComparison.Ordinal) ||
op.Equals("tosci", StringComparison.Ordinal)) {
          try {
            d1 = EDecimal.FromString(input1, ctx);
            Assert.IsTrue(!conversionError, "Expected no conversion error");
            String converted = d1.ToString();
            if (!output.Equals("?", StringComparison.Ordinal)) {
              Assert.AreEqual(output, converted, input1);
            }
          } catch (FormatException) {
            Assert.IsTrue(conversionError, "Expected conversion error");
          }
        } else if (op.Equals("toEng", StringComparison.Ordinal) ||
op.Equals("toeng", StringComparison.Ordinal)) {
          try {
            d1 = EDecimal.FromString(input1, ctx);
            Assert.IsTrue(!conversionError, "Expected no conversion error");
            String converted = d1.ToEngineeringString();
            if (!output.Equals("?", StringComparison.Ordinal)) {
              Assert.AreEqual(output, converted, input1);
            }
          } catch (FormatException) {
            Assert.IsTrue(conversionError, "Expected conversion error");
          }
        } else {
          if (!output.Equals("?", StringComparison.Ordinal)) {
            if (output == null && d3 != null) {
              Assert.Fail(name + ": d3 must be null");
            }
            if (output != null && !d3.ToString().Equals(output,
  StringComparison.Ordinal)) {
              EDecimal d4 = EDecimal.FromString(output);
              {
                object objectTemp = output;
                object objectTemp2 = d3.ToString();
                string messageTemp = name + ": expected: [" +
                d4.UnsignedMantissa + " " + d4.Exponent +
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
        if (!name.Equals("pow118", StringComparison.Ordinal) &&
!name.Equals("pow119", StringComparison.Ordinal) &&
!name.Equals("pow120", StringComparison.Ordinal) &&
!name.Equals("pow121", StringComparison.Ordinal) &&
!name.Equals("pow122", StringComparison.Ordinal)) {
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
  }
}
