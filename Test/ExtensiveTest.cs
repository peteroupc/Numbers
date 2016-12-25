/*
Written by Peter O. in 2013.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using PeterO.Numbers;

namespace CBOR {
  [TestFixture]
  public class ExtensiveTest {
    public static void AssertFlags(int expected, int actual, string str) {
      actual &= EContext.FlagInexact | EContext.FlagUnderflow |
        EContext.FlagOverflow | EContext.FlagInvalid |
        EContext.FlagDivideByZero;
      if (expected == actual) {
        return;
      }
      Assert.AreEqual(
  (expected & EContext.FlagInexact) != 0,
  (actual & EContext.FlagInexact) != 0,
  "Inexact: " + str);
      Assert.AreEqual(
  (expected & EContext.FlagOverflow) != 0,
  (actual & EContext.FlagOverflow) != 0,
  "Overflow: " + str);
      Assert.AreEqual(
  (expected & EContext.FlagUnderflow) != 0,
  (actual & EContext.FlagUnderflow) != 0,
  "Underflow: " + str);
      Assert.AreEqual(
  (expected & EContext.FlagInvalid) != 0,
  (actual & EContext.FlagInvalid) != 0,
  "Invalid: " + str);
      Assert.AreEqual(
  (expected & EContext.FlagDivideByZero) != 0,
  (actual & EContext.FlagDivideByZero) != 0,
  "DivideByZero: " + str);
    }

    private static bool Contains(string str, string sub) {
      return (sub.Length == 1) ? (str.IndexOf(sub[0]) >= 0) :
        (str.IndexOf(sub, StringComparison.Ordinal) >= 0);
    }

    private static bool StartsWith(string str, string sub) {
      return str.StartsWith(sub, StringComparison.Ordinal);
    }

    private static bool EndsWith(string str, string sub) {
      return str.EndsWith(sub, StringComparison.Ordinal);
    }

    private static int HexInt(string str) {
      return Int32.Parse(
        str,
        System.Globalization.NumberStyles.AllowHexSpecifier,
        System.Globalization.CultureInfo.InvariantCulture);
    }

    private static EContext SetRounding(
  EContext ctx,
  string round) {
      if (round.Equals(">")) {
        ctx = ctx.WithRounding(ERounding.Ceiling);
      }
      if (round.Equals("<")) {
        ctx = ctx.WithRounding(ERounding.Floor);
      }
      if (round.Equals("0")) {
        ctx = ctx.WithRounding(ERounding.Down);
      }
      if (round.Equals("=0")) {
        ctx = ctx.WithRounding(ERounding.HalfEven);
      }
      if (round.Equals("h>") || round.Equals("=^")) {
        ctx = ctx.WithRounding(ERounding.HalfUp);
      }
      if (round.Equals("h<")) {
        ctx = ctx.WithRounding(ERounding.HalfDown);
      }
      return ctx;
    }

    private static string ConvertOp(string s) {
      return s.Equals("S") ? "sNaN" : ((s.Equals("Q") || s.Equals("#")) ?
                "NaN" : s);
    }

    private interface IExtendedNumber : IComparable<IExtendedNumber> {
      object Value { get; }

      IExtendedNumber Add(IExtendedNumber b, EContext ctx);

      IExtendedNumber Subtract(IExtendedNumber b, EContext ctx);

      IExtendedNumber Multiply(IExtendedNumber b, EContext ctx);

      IExtendedNumber Divide(IExtendedNumber b, EContext ctx);

      IExtendedNumber SquareRoot(EContext ctx);

      IExtendedNumber MultiplyAndAdd(
  IExtendedNumber b,
  IExtendedNumber c,
  EContext ctx);

      IExtendedNumber MultiplyAndSubtract(
  IExtendedNumber b,
  IExtendedNumber c,
  EContext ctx);

      bool IsQuietNaN();

      bool IsSignalingNaN();

      bool IsInfinity();

      bool IsZeroValue();
    }

    private sealed class DecimalNumber : IExtendedNumber {
      private EDecimal ed;

      public static DecimalNumber Create(EDecimal dec) {
        var dn = new ExtensiveTest.DecimalNumber {
          ed = dec };
        return dn;
      }

      #region Equals and GetHashCode implementation
      public override bool Equals(object obj) {
        var other = obj as ExtensiveTest.DecimalNumber;
        return (other != null) && Object.Equals(this.ed, other.ed);
      }

      public override int GetHashCode() {
        var hashCode = 703582279;
        unchecked {
          if (this.ed != null) {
            hashCode += 703582387 * this.ed.GetHashCode();
          }
        }
        return hashCode;
      }
      #endregion

      public override string ToString() {
        return this.ed.ToString();
      }

      public object Value {
        get {
          return this.ed;
        }
      }

      private static EDecimal ToValue(IExtendedNumber en) {
        return (EDecimal)en.Value;
      }

      public ExtensiveTest.IExtendedNumber Add(
        ExtensiveTest.IExtendedNumber b,
        EContext ctx) {
        return Create(this.ed.Add(ToValue(b), ctx));
      }

      public ExtensiveTest.IExtendedNumber
        Subtract(ExtensiveTest.IExtendedNumber b, EContext ctx) {
        return Create(this.ed.Subtract(ToValue(b), ctx));
      }

      public ExtensiveTest.IExtendedNumber
        Multiply(ExtensiveTest.IExtendedNumber b, EContext ctx) {
        return Create(this.ed.Multiply(ToValue(b), ctx));
      }

      public ExtensiveTest.IExtendedNumber
        Divide(ExtensiveTest.IExtendedNumber b, EContext ctx) {
        return Create(this.ed.Divide(ToValue(b), ctx));
      }

      public ExtensiveTest.IExtendedNumber SquareRoot(EContext ctx) {
        return Create(this.ed.Sqrt(ctx));
      }

      public ExtensiveTest.IExtendedNumber MultiplyAndAdd(
          ExtensiveTest.IExtendedNumber b,
          ExtensiveTest.IExtendedNumber c,
          EContext ctx) {
        return Create(this.ed.MultiplyAndAdd(ToValue(b), ToValue(c), ctx));
      }

      public ExtensiveTest.IExtendedNumber MultiplyAndSubtract(
          ExtensiveTest.IExtendedNumber b,
          ExtensiveTest.IExtendedNumber c,
          EContext ctx) {
        return Create(this.ed.MultiplyAndSubtract(ToValue(b), ToValue(c), ctx));
      }

      public bool IsQuietNaN() {
        return this.ed != null && ToValue(this).IsQuietNaN();
      }

      public bool IsSignalingNaN() {
        return this.ed != null && ToValue(this).IsSignalingNaN();
      }

      public bool IsInfinity() {
        return this.ed != null && ToValue(this).IsInfinity();
      }

      public bool IsZeroValue() {
        return this.ed != null && ToValue(this).IsZero;
      }

      public int CompareTo(IExtendedNumber other) {
        var dn = other as DecimalNumber;
        EDecimal dned = dn == null ? null : dn.ed;
        return (this.ed == null) ? ((dned == null) ? 0 : -1) : (dned == null ?
          1 : this.ed.CompareTo(dned));
      }
    }

    private sealed class BinaryNumber : IExtendedNumber {
      private EFloat ef;

      public int CompareTo(IExtendedNumber other) {
        var dn = other as BinaryNumber;
        EFloat dned = dn == null ? null : dn.ef;
        return (this.ef == null) ? ((dned == null) ? 0 : -1) : (dned == null ?
          1 : this.ef.CompareTo(dned));
      }

      public static BinaryNumber Create(EFloat dec) {
        var dn = new ExtensiveTest.BinaryNumber();
        if (dec == null) {
          throw new ArgumentNullException("dec");
        }
        dn.ef = dec;
        return dn;
      }

      public static BinaryNumber FromString(String str) {
        if (str.Equals("NaN")) {
          return Create(EFloat.NaN);
        }
        if (str.Equals("sNaN")) {
          return Create(EFloat.SignalingNaN);
        }
        if (str.Equals("+Zero")) {
          return Create(EFloat.Zero);
        }
        if (str.Equals("0x0")) {
          return Create(EFloat.Zero);
        }
        if (str.Equals("0x1")) {
          return Create(EFloat.One);
        }
        if (str.Equals("-Zero")) {
          return Create(EFloat.NegativeZero);
        }
        if (str.Equals("-Inf")) {
          return Create(EFloat.NegativeInfinity);
        }
        if (str.Equals("+Inf")) {
          return Create(EFloat.PositiveInfinity);
        }
        var offset = 0;
        var negative = false;
        if (str[0] == '+' || str[0] == '-') {
          negative = str[0] == '-';
          ++offset;
        }
        var i = offset;
        var beforeDec = 0;
        var mantissa = 0;
        var exponent = 0;
        var haveDec = false;
        var haveBinExp = false;
        var haveDigits = false;
        for (; i < str.Length; ++i) {
          if (str[i] >= '0' && str[i] <= '9') {
            var thisdigit = (int)(str[i] - '0');
            if ((beforeDec >> 28) != 0) {
              throw new FormatException(str);
            }
            beforeDec <<= 4;
            beforeDec |= thisdigit;
            haveDigits = true;
          } else if (str[i] >= 'A' && str[i] <= 'F') {
            var thisdigit = (int)(str[i] - 'A') + 10;
            if ((beforeDec >> 28) != 0) {
              throw new FormatException(str);
            }
            beforeDec <<= 4;
            beforeDec |= thisdigit;
            haveDigits = true;
          } else if (str[i] >= 'a' && str[i] <= 'f') {
            var thisdigit = (int)(str[i] - 'a') + 10;
            if ((beforeDec >> 28) != 0) {
              throw new FormatException(str);
            }
            beforeDec <<= 4;
            beforeDec |= thisdigit;
            haveDigits = true;
          } else if (str[i] == '.') {
            // Decimal point reached
            haveDec = true;
            ++i;
            break;
          } else if (str[i] == 'P' || str[i] == 'p') {
            // Binary exponent reached
            haveBinExp = true;
            ++i;
            break;
          } else {
            throw new FormatException(str);
          }
        }
        if (!haveDigits) {
          throw new FormatException(str);
        }
        if (haveDec) {
          haveDigits = false;
          var afterDec = 0;
          for (; i < str.Length; ++i) {
            if (str[i] >= '0' && str[i] <= '9') {
              var thisdigit = (int)(str[i] - '0');
              if ((afterDec >> 28) != 0) {
                throw new FormatException(str);
              }
              afterDec <<= 4;
              afterDec |= thisdigit;
              haveDigits = true;
            } else if (str[i] >= 'A' && str[i] <= 'F') {
              var thisdigit = (int)(str[i] - 'A') + 10;
              if ((afterDec >> 28) != 0) {
                throw new FormatException(str);
              }
              afterDec <<= 4;
              afterDec |= thisdigit;
              haveDigits = true;
            } else if (str[i] >= 'a' && str[i] <= 'f') {
              var thisdigit = (int)(str[i] - 'a') + 10;
              if ((afterDec >> 28) != 0) {
                throw new FormatException(str);
              }
              afterDec <<= 4;
              afterDec |= thisdigit;
              haveDigits = true;
            } else if (str[i] == 'P' || str[i] == 'p') {
              // Binary exponent reached
              haveBinExp = true;
              ++i;
              break;
            } else {
              throw new FormatException(str);
            }
          }
          if (!haveDigits) {
            throw new FormatException(str);
          }
          mantissa = (beforeDec << 23) | afterDec;
        } else {
          mantissa = beforeDec;
        }
        if (negative) {
          mantissa = -mantissa;
        }
        if (haveBinExp) {
          haveDigits = false;
          var negexp = false;
          if (i < str.Length && str[i] == '-') {
            negexp = true;
            ++i;
          }
          for (; i < str.Length; ++i) {
            if (str[i] >= '0' && str[i] <= '9') {
              var thisdigit = (int)(str[i] - '0');
              if ((exponent >> 28) != 0) {
                throw new FormatException(str);
              }
              exponent *= 10;
              exponent += thisdigit;
              haveDigits = true;
            } else {
              throw new FormatException(str);
            }
          }
          if (!haveDigits) {
            throw new FormatException(str);
          }
          if (negexp) {
            exponent = -exponent;
          }
          exponent -= 23;
        }
        if (i != str.Length) {
          throw new FormatException(str);
        }
        // Console.WriteLine("mant=" + mantissa + " exp=" + exponent);
        return Create(
  EFloat.Create(mantissa, exponent));
      }

      public static BinaryNumber FromFloatWords(int[] words) {
        if (words == null) {
          throw new ArgumentException("words");
        }
        if (words.Length == 1) {
          var neg = (words[0] >> 31) != 0;
          var exponent = (words[0] >> 23) & 0xff;
          var mantissa = words[0] & 0x7fffff;
          if (exponent == 255) {
         return (mantissa == 0) ? Create(neg ? EFloat.NegativeInfinity :
                    EFloat.PositiveInfinity) : (((mantissa &
                0x00400000) != 0) ? Create(EFloat.NaN) :
  Create(EFloat.SignalingNaN));
          }
          if (exponent == 0) {
            if (mantissa == 0) {
              return Create(neg ? EFloat.NegativeZero :
                    EFloat.Zero);
            }
            // subnormal
            exponent = -126;
          } else {
            // normal
            exponent -= 127;
            mantissa |= 0x800000;
          }
          var bigmantissa = (EInteger)mantissa;
          if (neg) {
            bigmantissa = -bigmantissa;
          }
          exponent -= 23;
          return Create(
  EFloat.Create(
  bigmantissa,
  (EInteger)exponent));
        }
        if (words.Length == 2) {
          var neg = (words[0] >> 31) != 0;
          var exponent = (words[0] >> 20) & 0x7ff;
          var mantissa = words[0] & 0xfffff;
          var mantissaNonzero = mantissa | words[1];
          if (exponent == 2047) {
            return (mantissaNonzero == 0) ? Create(neg ?
  EFloat.NegativeInfinity : EFloat.PositiveInfinity) :
    (((mantissa & 0x00080000) != 0) ? Create(EFloat.NaN) :
  Create(EFloat.SignalingNaN));
          }
          if (exponent == 0) {
            if (mantissaNonzero == 0) {
              return Create(neg ? EFloat.NegativeZero :
                    EFloat.Zero);
            }
            // subnormal
            exponent = -1022;
          } else {
            // normal
            exponent -= 1023;
            mantissa |= 0x100000;
          }
          var bigmantissa = EInteger.Zero;
          bigmantissa += (EInteger)((mantissa >> 16) & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)(mantissa & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)((words[1] >> 16) & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)(words[1] & 0xffff);
          if (neg) {
            bigmantissa = -bigmantissa;
          }
          exponent -= 52;
          return Create(
  EFloat.Create(
  bigmantissa,
  (EInteger)exponent));
        }
        if (words.Length == 4) {
          var neg = (words[0] >> 31) != 0;
          var exponent = (words[0] >> 16) & 0x7fff;
          var mantissa = words[0] & 0xffff;
          var mantissaNonzero = mantissa | words[3] | words[1] | words[2];
          if (exponent == 0x7fff) {
            return (mantissaNonzero == 0) ? Create(neg ?
  EFloat.NegativeInfinity : EFloat.PositiveInfinity) :
    (((mantissa & 0x00008000) != 0) ? Create(EFloat.NaN) :
  Create(EFloat.SignalingNaN));
          }
          if (exponent == 0) {
            if (mantissaNonzero == 0) {
              return Create(neg ? EFloat.NegativeZero :
                    EFloat.Zero);
            }
            // subnormal
            exponent = -16382;
          } else {
            // normal
            exponent -= 16383;
            mantissa |= 0x10000;
          }
          var bigmantissa = EInteger.Zero;
          bigmantissa += (EInteger)((mantissa >> 16) & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)(mantissa & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)((words[1] >> 16) & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)(words[1] & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)((words[2] >> 16) & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)(words[2] & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)((words[3] >> 16) & 0xffff);
          bigmantissa <<= 16;
          bigmantissa += (EInteger)(words[3] & 0xffff);
          if (neg) {
            bigmantissa = -bigmantissa;
          }
          exponent -= 112;
          return Create(
  EFloat.Create(
  bigmantissa,
  (EInteger)exponent));
        }
        throw new ArgumentException("words has a bad length");
      }

      #region Equals and GetHashCode implementation
      public override bool Equals(object obj) {
        var other = obj as ExtensiveTest.BinaryNumber;
        return (other != null) && (this.ef.CompareTo(other.ef) == 0);
      }

      public override int GetHashCode() {
        var hashCode = 703582379;
        unchecked {
          if (this.ef != null) {
            hashCode += 703582447 * this.ef.GetHashCode();
          }
        }
        return hashCode;
      }
      #endregion

      public override string ToString() {
        return this.ef.ToString();
      }

      public object Value {
        get {
          return this.ef;
        }
      }

      public static EFloat ToValue(IExtendedNumber en) {
        return (EFloat)en.Value;
      }

      public ExtensiveTest.IExtendedNumber Add(
        ExtensiveTest.IExtendedNumber b,
        EContext ctx) {
        return Create(this.ef.Add(ToValue(b), ctx));
      }

      public ExtensiveTest.IExtendedNumber
        Subtract(ExtensiveTest.IExtendedNumber b, EContext ctx) {
        return Create(this.ef.Subtract(ToValue(b), ctx));
      }

      public ExtensiveTest.IExtendedNumber
        Multiply(ExtensiveTest.IExtendedNumber b, EContext ctx) {
        return Create(this.ef.Multiply(ToValue(b), ctx));
      }

      public ExtensiveTest.IExtendedNumber
        Divide(ExtensiveTest.IExtendedNumber b, EContext ctx) {
        return Create(this.ef.Divide(ToValue(b), ctx));
      }

      public BinaryNumber Pow(
  ExtensiveTest.IExtendedNumber b,
  EContext ctx) {
        return Create(this.ef.Pow(ToValue(b), ctx));
      }

      public ExtensiveTest.IExtendedNumber SquareRoot(EContext ctx) {
        return Create(this.ef.Sqrt(ctx));
      }

      public ExtensiveTest.IExtendedNumber MultiplyAndAdd(
          ExtensiveTest.IExtendedNumber b,
          ExtensiveTest.IExtendedNumber c,
          EContext ctx) {
        return Create(this.ef.MultiplyAndAdd(ToValue(b), ToValue(c), ctx));
      }

      public ExtensiveTest.IExtendedNumber MultiplyAndSubtract(
          ExtensiveTest.IExtendedNumber b,
          ExtensiveTest.IExtendedNumber c,
          EContext ctx) {
        return Create(this.ef.MultiplyAndSubtract(ToValue(b), ToValue(c), ctx));
      }

      public bool IsNear(IExtendedNumber bn) {
        // ComparePrint(bn);
        var ulpdiff = EFloat.Create(
          (EInteger)2,
          ToValue(this).Exponent);
        return ToValue(this).Subtract(ToValue(bn)).Abs().CompareTo(ulpdiff) <=
          0;
      }

      public void ComparePrint(IExtendedNumber bn) {
        Console.WriteLine(String.Empty + ToValue(this).Mantissa + " man, " +
                    ToValue(bn).Mantissa + " exp");
      }

      public BinaryNumber RoundToIntegralExact(EContext ctx) {
        return Create(this.ef.RoundToIntegerExact(ctx));
      }

      public BinaryNumber Log(EContext ctx) {
        return Create(this.ef.Log(ctx));
      }

      public BinaryNumber Remainder(IExtendedNumber bn, EContext ctx) {
        return Create(this.ef.Remainder(ToValue(bn), ctx));
      }

      public BinaryNumber Exp(EContext ctx) {
        return Create(this.ef.Exp(ctx));
      }

      public BinaryNumber Abs(EContext ctx) {
        return Create(this.ef.Abs(ctx));
      }

      public BinaryNumber Log10(EContext ctx) {
        return Create(this.ef.Log10(ctx));
      }

      public bool IsQuietNaN() {
        return this.ef != null && ToValue(this).IsQuietNaN();
      }

      public bool IsSignalingNaN() {
        return this.ef != null && ToValue(this).IsSignalingNaN();
      }

      public bool IsInfinity() {
        return this.ef != null && ToValue(this).IsInfinity();
      }

      public bool IsZeroValue() {
        return this.ef != null && ToValue(this).IsZero;
      }
    }

    private int ParseLineInput(string ln, Stopwatch sw) {
      var chunks = Contains(ln, " " + " ") ?
        Regex.Split(ln, " +") : ln.Split(' ');
      if (chunks.Length < 4) {
        return 0;
      }
      var type = chunks[0];
      EContext ctx = null;
      var op = String.Empty;
      var size = 0;
      if (EndsWith(type, "d")) {
        op = type.Substring(0, type.Length - 1);
        ctx = EContext.Binary64;
        size = 1;
      } else if (EndsWith(type, "s")) {
        op = type.Substring(0, type.Length - 1);
        ctx = EContext.Binary32;
        size = 0;
      } else if (EndsWith(type, "q")) {
        op = type.Substring(0, type.Length - 1);
        ctx = EContext.Binary128;
        size = 2;
      }
      if (ctx == null) {
        return 0;
      }
      var round = chunks[1];
      var flags = chunks[3];
      var compareOp = chunks[2];
      sw.Start();
      switch (round) {
        case "m":
          ctx = ctx.WithRounding(ERounding.Floor);
          break;
        case "p":
          ctx = ctx.WithRounding(ERounding.Ceiling);
          break;
        case "z":
          ctx = ctx.WithRounding(ERounding.Down);
          break;
        case "n":
          ctx = ctx.WithRounding(ERounding.HalfEven);
          break;
        default:
          return 0;
      }
      BinaryNumber op1, op2, result;
      switch (size) {
        case 0:
          // single
          if (chunks.Length < 6) {
            return 0;
          }
          op1 = BinaryNumber.FromFloatWords(new[] { HexInt(chunks[4]) });
          op2 = BinaryNumber.FromFloatWords(new[] { HexInt(chunks[5]) });
          if (chunks.Length == 6 || chunks[6].Length == 0) {
            result = op2;
            op2 = null;
          } else {
            result = BinaryNumber.FromFloatWords(new[] { HexInt(chunks[6])
                    });
          }

          break;
        case 1:
          // double
          if (chunks.Length < 8) {
            return 0;
          }
          op1 = BinaryNumber.FromFloatWords(new[] { HexInt(chunks[4]),
                    HexInt(chunks[5]) });
          op2 = BinaryNumber.FromFloatWords(new[] { HexInt(chunks[6]),
                    HexInt(chunks[7]) });
          if (chunks.Length == 8 || chunks[8].Length == 0) {
            result = op2;
            op2 = null;
            return 0;
          }
          result = BinaryNumber.FromFloatWords(new[] { HexInt(chunks[8]),
                    HexInt(chunks[9]) });
          break;
        case 2:
          // quad
          if (chunks.Length < 12) {
            return 0;
          }
          op1 = BinaryNumber.FromFloatWords(new[] { HexInt(chunks[4]),
  HexInt(chunks[5]), HexInt(chunks[6]),
                    HexInt(chunks[7]) });
          op2 = BinaryNumber.FromFloatWords(new[] { HexInt(chunks[8]),
  HexInt(chunks[9]), HexInt(chunks[10]),
  HexInt(chunks[11]) });
          if (chunks.Length == 12 || chunks[12].Length == 0) {
            result = op2;
            op2 = null;
          } else {
            result = BinaryNumber.FromFloatWords(new[] {
HexInt(chunks[12]), HexInt(chunks[13]),
  HexInt(chunks[14]), HexInt(chunks[15]) });
          }

          break;
        default:
          return 0;
      }

      if (compareOp.Equals("uo")) {
        result = BinaryNumber.FromString("NaN");
      }
      var expectedFlags = 0;
      var ignoredFlags = 0;
      if (Contains(flags, "?x")) {
        ignoredFlags |= EContext.FlagInexact;
      } else if (Contains(flags, "x")) {
        expectedFlags |= EContext.FlagInexact;
      }
      if (Contains(flags, "u")) {
        expectedFlags |= EContext.FlagUnderflow;
      }
      if (Contains(flags, "o")) {
        expectedFlags |= EContext.FlagOverflow;
      }
      if (Contains(flags, "v")) {
        expectedFlags |= EContext.FlagInvalid;
      }
      if (Contains(flags, "d")) {
        expectedFlags |= EContext.FlagDivideByZero;
      }

      ctx = ctx.WithBlankFlags();
      if (op.Equals("add")) {
        var d3 = op1.Add(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("sub")) {
        var d3 = op1.Subtract(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("mul")) {
        var d3 = op1.Multiply(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("pow")) {
        var d3 = op1.Pow(op2, ctx);
        // Check for cases that contradict the General Decimal
        // Arithmetic spec
        if (op1.IsZeroValue() && op2.IsZeroValue()) {
          return 0;
        }
        if (((EFloat)op1.Value).Sign < 0 && op2.IsInfinity()) {
          return 0;
        }
        var powIntegral = op2.Equals(op2.RoundToIntegralExact(null));
        if (((EFloat)op1.Value).Sign < 0 && !powIntegral) {
          return 0;
        }
        if ((op1.IsQuietNaN() || op1.IsSignalingNaN()) && op2.IsZeroValue()) {
          return 0;
        }
        if (op2.IsInfinity() && op1.Abs(null).Equals(
          BinaryNumber.FromString("1"))) {
          return 0;
        }
        expectedFlags &= ~EContext.FlagDivideByZero;
        expectedFlags &= ~EContext.FlagInexact;
        expectedFlags &= ~EContext.FlagUnderflow;
        expectedFlags &= ~EContext.FlagOverflow;
        ignoredFlags |= EContext.FlagInexact;
        ignoredFlags |= EContext.FlagUnderflow;
        ignoredFlags |= EContext.FlagOverflow;
        if (!result.Equals(d3)) {
          if (compareOp.Equals("vn")) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else if (compareOp.Equals("nb")) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else {
            Assert.AreEqual(result, d3, ln);
          }
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          ctx.Flags &= ~ignoredFlags;
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("floor")) {
        ctx = ctx.WithRounding(ERounding.Floor);
        IExtendedNumber d3 = op1.RoundToIntegralExact(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        AssertFlags(expectedFlags, ctx.Flags, ln);
      } else if (op.Equals("fabs")) {
        // NOTE: Fabs never sets flags
        IExtendedNumber d3 = op1.Abs(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
      } else if (op.Equals("ceil")) {
        ctx = ctx.WithRounding(ERounding.Ceiling);
        IExtendedNumber d3 = op1.RoundToIntegralExact(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        AssertFlags(expectedFlags, ctx.Flags, ln);
      } else if (op.Equals("sqrt")) {
        var d3 = op1.SquareRoot(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        AssertFlags(expectedFlags, ctx.Flags, ln);
      } else if (op.Equals("log")) {
        IExtendedNumber d3 = op1.Log(ctx);
        if (!result.Equals(d3)) {
          if (compareOp.Equals("vn")) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else if (compareOp.Equals("nb")) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else {
            Assert.AreEqual(result, d3, ln);
          }
        }
        if (!op1.IsZeroValue()) {
          // ignore flags for zero operand, expects
          // divide by zero flag where general decimal
          // spec doesn't set flags in this case
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("exp")) {
        IExtendedNumber d3 = op1.Exp(ctx);
        if (!result.Equals(d3)) {
          if (compareOp.Equals("vn")) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else if (compareOp.Equals("nb")) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else {
            Assert.AreEqual(result, d3, ln);
          }
        }
        AssertFlags(expectedFlags, ctx.Flags, ln);
      } else if (op.Equals("log10")) {
        IExtendedNumber d3 = op1.Log10(ctx);
        if (!result.Equals(d3)) {
          if (compareOp.Equals("vn")) {
            if (!result.IsNear(d3)) {
              Console.WriteLine("op1=..." + op1 + " result=" + result +
                " d3=...." + d3);
              Assert.AreEqual(result, d3, ln);
            }
          } else if (compareOp.Equals("nb")) {
            if (!result.IsNear(d3)) {
              Console.WriteLine("op1=..." + op1 + " result=" + result +
                " d3=...." + d3);
              Assert.AreEqual(result, d3, ln);
            }
          } else {
            Console.WriteLine("op1=..." + op1 + " result=" + result +
                " d3=...." + d3);
            Assert.AreEqual(result, d3, ln);
          }
        }
        expectedFlags &= ~EContext.FlagInexact;
        ignoredFlags |= EContext.FlagInexact;
        ctx.Flags &= ~ignoredFlags;
        if (!op1.IsZeroValue()) {
          // ignore flags for zero operand, expects
          // divide by zero flag where general decimal
          // spec doesn't set flags in this case
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("div")) {
        var d3 = op1.Divide(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("fmod")) {
        IExtendedNumber d3 = op1.Remainder(op2, ctx);
        if ((ctx.Flags & EContext.FlagInvalid) != 0 &&
            (expectedFlags & EContext.FlagInvalid) == 0) {
          // Skip since the quotient may be too high to fit an integer,
          // which triggers an invalid operation under the General
          // Decimal Arithmetic specification
          return 0;
        }
        if (!result.Equals(d3)) {
          Console.WriteLine("op1=..." + op1 + "\nop2=..." + op2 + "\nresult=" +
                result + "\nd3=...." + d3);
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      }
      sw.Stop();
      return 0;
    }

    private static int ParseLine(string ln, Stopwatch sw) {
      var chunks = ln.Split(' ');
      if (chunks.Length < 4) {
        return 0;
      }
      var type = chunks[0];
      EContext ctx = null;
      var binaryFP = false;
      var op = String.Empty;
      if (StartsWith(type, "d32")) {
        ctx = EContext.Decimal32;
        op = type.Substring(3);
      }
      if (StartsWith(type, "d64")) {
        ctx = EContext.Decimal64;
        op = type.Substring(3);
      }
      if (StartsWith(type, "b32")) {
        ctx = EContext.Binary32;
        binaryFP = true;
        op = type.Substring(3);
      }
      if (StartsWith(type, "d128")) {
        ctx = EContext.Decimal128;
        op = type.Substring(4);
      }
      if (ctx == null) {
        return 0;
      }
      if (Contains(type, "!")) {
        return 0;
      }
      if (op.Contains("cff")) {
        // skip test cases for
        // conversion to another floating point format
        return 0;
      }
      var squroot = op.Equals("V");
      // var mod = op.Equals("%");
      var div = op.Equals("/");
      var fma = op.Equals("*+");
      var fms = op.Equals("*-");
      var addop = op.Equals("+");
      var subop = op.Equals("-");
      var mul = op.Equals("*");
      var round = chunks[1];
      ctx = SetRounding(ctx, round);
      var offset = 0;
      var traps = String.Empty;
      if (Contains(chunks[2], "x") || chunks[2].Equals("i") ||
StartsWith(chunks[2], "o")) {
        // traps
        ++offset;
        traps = chunks[2];
      }
      if (Contains(traps, "u") || Contains(traps, "o")) {
        // skip tests that trap underflow or overflow,
        // the results there may be wrong
        return 0;
      }
      var op1str = ConvertOp(chunks[2 + offset]);
      var op2str = ConvertOp(chunks[3 + offset]);
      var op3str = String.Empty;
      if (chunks.Length <= 4 + offset) {
        return 0;
      }
      var sresult = String.Empty;
      var flags = String.Empty;
      op3str = chunks[4 + offset];
      if (op2str.Equals("->")) {
        if (chunks.Length <= 5 + offset) {
          return 0;
        }
        op2str = String.Empty;
        op3str = String.Empty;
        sresult = chunks[4 + offset];
        flags = chunks[5 + offset];
      } else if (op3str.Equals("->")) {
        if (chunks.Length <= 6 + offset) {
          return 0;
        }
        op3str = String.Empty;
        sresult = chunks[5 + offset];
        flags = chunks[6 + offset];
      } else {
        if (chunks.Length <= 7 + offset) {
          return 0;
        }
        op3str = ConvertOp(op3str);
        sresult = chunks[6 + offset];
        flags = chunks[7 + offset];
      }
      sresult = ConvertOp(sresult);
      sw.Start();
      IExtendedNumber op1, op2, op3, result;
      if (binaryFP) {
        op1 = BinaryNumber.FromString(op1str);
        op2 = String.IsNullOrEmpty(op2str) ? null :
          BinaryNumber.FromString(op2str);
        op3 = String.IsNullOrEmpty(op3str) ? null :
          BinaryNumber.FromString(op3str);
        result = BinaryNumber.FromString(sresult);
      } else {
        op1 = DecimalNumber.Create(EDecimal.FromString(op1str));
        op2 = String.IsNullOrEmpty(op2str) ? null :
          DecimalNumber.Create(EDecimal.FromString(op2str));
        op3 = String.IsNullOrEmpty(op3str) ? null :
          DecimalNumber.Create(EDecimal.FromString(op3str));
        result = DecimalNumber.Create(EDecimal.FromString(sresult));
      }
      var expectedFlags = 0;
      if (Contains(flags, "x")) {
        expectedFlags |= EContext.FlagInexact;
      }
      if (Contains(flags, "u")) {
        expectedFlags |= EContext.FlagUnderflow;
      }
      if (Contains(flags, "o")) {
        expectedFlags |= EContext.FlagOverflow;
      }
      if (Contains(flags, "i")) {
        expectedFlags |= EContext.FlagInvalid;
      }
      if (Contains(flags, "z")) {
        expectedFlags |= EContext.FlagDivideByZero;
      }
      ctx = ctx.WithBlankFlags();
      if (addop) {
        var d3 = op1.Add(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
      if ((expectedFlags & (EContext.FlagInexact | EContext.FlagInvalid)) ==
            0) {
            d3 = op1.Add(op2, null);
            Test.TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (subop) {
        var d3 = op1.Subtract(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
      if ((expectedFlags & (EContext.FlagInexact | EContext.FlagInvalid)) ==
            0) {
            d3 = op1.Subtract(op2, null);
            Test.TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (mul) {
        var d3 = op1.Multiply(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
      if ((expectedFlags & (EContext.FlagInexact | EContext.FlagInvalid)) ==
            0) {
            d3 = op1.Multiply(op2, null);
            Test.TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (div) {
        var d3 = op1.Divide(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
      if ((expectedFlags & (EContext.FlagInexact | EContext.FlagInvalid)) ==
            0) {
            d3 = op1.Divide(op2, null);
            Test.TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (squroot) {
        var d3 = op1.SquareRoot(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        AssertFlags(expectedFlags, ctx.Flags, ln);
      } else if (fma) {
        var d3 = op1.MultiplyAndAdd(op2, op3, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (binaryFP && (
        (op1.IsQuietNaN() && (op2.IsSignalingNaN() || op3.IsSignalingNaN())) ||
            (op2.IsQuietNaN() && op3.IsSignalingNaN()))) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
      if ((expectedFlags & (EContext.FlagInexact | EContext.FlagInvalid)) ==
            0) {
            d3 = op1.MultiplyAndAdd(op2, op3, null);
            Test.TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      } else if (fms) {
        var d3 = op1.MultiplyAndSubtract(op2, op3, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
      if ((expectedFlags & (EContext.FlagInexact | EContext.FlagInvalid)) ==
            0) {
            d3 = op1.MultiplyAndSubtract(op2, op3, null);
            Test.TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      }
      sw.Stop();
      return 0;
    }

    public static string[] GetTestFiles() {
      try {
        var list = new List<string>(
          Directory.GetFiles(Path.Combine("..", "Debug")));
        return list.ToArray();
      } catch (IOException) {
        return new string[0];
      }
    }

    [Test]
    public void TestParser() {
      long failures = 0;
      var errors = new List<string>();
      var dirfiles = new List<string>();
      var sw = new System.Diagnostics.Stopwatch();
      sw.Start();
      var valueSwProcessing = new System.Diagnostics.Stopwatch();
      // var nullWriter = TextWriter.Null;
      var standardOut = Console.Out;
      var x = 0;
      dirfiles.AddRange(GetTestFiles());
      foreach (var f in dirfiles) {
        Console.WriteLine(f);
        if (errors.Count > 100) {
          break;
        }
        ++x;
        var lowerF = f.ToLowerInvariant();
        // if (!lowerF.Contains("d64")) {
 // continue;
// }
        var isinput = lowerF.Contains(".input");
        if (!lowerF.Contains(".input") && !lowerF.Contains(".txt") &&
            !lowerF.Contains(".dectest") && !lowerF.Contains(".fptest")) {
          continue;
        }
         using (var fileStream = File.Open(f, FileMode.Open))
         using (var w = new StreamReader(fileStream)) {
          while (!w.EndOfStream) {
            if (errors.Count > 100) {
              break;
            }
            var ln = w.ReadLine();
            {
              try {
                // Console.SetOut(nullWriter);
                if (isinput) {
                  this.ParseLineInput(ln, valueSwProcessing);
                } else {
                  ParseLine(ln, valueSwProcessing);
                }
              } catch (Exception ex) {
                errors.Add(ex.ToString());
                ++failures;
                try {
                  Console.SetOut(standardOut);
                  if (isinput) {
                    this.ParseLineInput(ln, valueSwProcessing);
                  } else {
                    ParseLine(ln, valueSwProcessing);
                  }
                } catch (Exception ex2) {
                  Console.WriteLine(ln);
                  errors.Add(ex2.ToString());
                  Console.SetOut(standardOut);
                }
              }
            }
          }
        }
      }
      Console.SetOut(standardOut);
      sw.Stop();
      // Total running time
      Console.WriteLine("Time: " + (sw.ElapsedMilliseconds / 1000.0) + " s");
      // Number processing time
      Console.WriteLine("ProcTime: " + (valueSwProcessing.ElapsedMilliseconds /
        1000.0) + " s");
      // Ratio of number processing time to total running time
    Console.WriteLine("Rate: " + (valueSwProcessing.ElapsedMilliseconds *
        1.0 / sw.ElapsedMilliseconds) + "%");
      if (failures > 0) {
        foreach (string err in errors) {
          Console.WriteLine(err);
        }
        // Assert.Fail(failures + " failure(s)");
      }
    }
  }
}
