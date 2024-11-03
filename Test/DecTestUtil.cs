using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using PeterO;
using PeterO.Numbers;
namespace Test {
  public static class DecTestUtil {
    private const string TestLineRegex =

  "^([A-Za-z0-9_]+)\\s+([A-Za-z0-9_\\-]+)\\s+(\\'[^\\']*\\'|\\S+)\\s+(?:(\\S+)\\s+)?(?:(\\S+)\\s+)?->\\s+(\\S+)\\s*(.*)";

    private static readonly Regex ValuePropertyLine = new Regex(
      "^(\\w+)\\:\\s*(\\S+).*",
      RegexOptions.Compiled);

    private static readonly Regex ValueQuotes = new Regex(
      "^[\\'\\\"]|[\\'\\\"]$",
      RegexOptions.Compiled);

    private static readonly Regex ValueTestLine = new Regex(
      TestLineRegex,
      RegexOptions.Compiled);

    public static string[] SplitAtFast(
      string str,
      char c,
      int minChunks,
      int maxChunks) {
      var chunks = new int[maxChunks];
      string[] ret;
      var chunk = 0;
      if (str == null) {
        throw new ArgumentNullException(nameof(str));
      }
      for (var i = 0; i < str.Length && chunk < maxChunks; ++i) {
        if (str[i] == c) {
          chunks[chunk++] = i;
        }
      }
      if (chunk >= minChunks - 1 && chunk < maxChunks) {
        chunks[chunk++] = str.Length;
      } else if (chunk < minChunks) {
        return null;
      }
      ret = new string[chunk];
      for (var i = 0; i < chunk; ++i) {
        int st = (i == 0) ? 0 : chunks[i - 1] + 1;
        ret[i] = str.Substring(st, chunks[i] - st);
      }
      return ret;
    }

    public static string[] SplitAt(string str, string delimiter) {
      if (delimiter == null) {
        throw new ArgumentNullException(nameof(delimiter));
      }
      if (delimiter.Length == 0) {
        throw new ArgumentException("delimiter is empty.");
      }
      if (String.IsNullOrEmpty(str)) {
        return new[] { String.Empty };
      }
      var index = 0;
      var first = true;
      List<string> strings = null;
      int delimLength = delimiter.Length;
      while (true) {
        int index2 = str.IndexOf(delimiter, index, StringComparison.Ordinal);
        if (index2 < 0) {
          if (first) {
            var strret = new string[1];
            strret[0] = str;
            return strret;
          }
          strings = strings ?? new List<string>();
          strings.Add(str.Substring(index));
          break;
        } else {
          first = false;
          string newstr = str.Substring(index, index2 - index);
          strings = strings ?? new List<string>();
          strings.Add(newstr);
          index = index2 + delimLength;
        }
      }
      return (string[])strings.ToArray();
    }

    public static string[] SplitAtSpaceRuns(string str) {
      if (String.IsNullOrEmpty(str)) {
        return new[] { String.Empty };
      }
      var index = 0;
      var first = true;
      List<string> strings = null;
      while (true) {
        int index2 = str.IndexOf(' ', index);
        if (index2 < 0) {
          if (first) {
            var strret = new string[1];
            strret[0] = str;
            return strret;
          }
          strings = strings ?? new List<string>();
          strings.Add(str.Substring(index));
          break;
        } else {
          first = false;
          string newstr = str.Substring(index, index2 - index);
          strings = strings ?? new List<string>();
          strings.Add(newstr);
          index = index2 + 1;
          // skip further spaces
          while (index < str.Length && str[index] == ' ') {
            ++index;
          }
        }
      }
      return (string[])strings.ToArray();
    }

    /// <summary>Returns a string with the basic upper-case letters A to Z
    /// (U+0041 to U+005A) converted to lower-case. Other characters remain
    /// unchanged.</summary>
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
      return (str[0] == '+') ? TestCommon.StringToInt(str.Substring(1)) :
        TestCommon.StringToInt(str);
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

    public static void AssertFlagsRestricted(
      int expected,
      int actual,
      string str) {
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

    public static bool Contains(string str, string sub) {
      if (str == null) {
        throw new ArgumentNullException(nameof(str));
      }
      if (sub == null) {
        throw new ArgumentNullException(nameof(sub));
      }
      if (sub.Length == 1) {
        for (var i = 0; i < str.Length; ++i) {
          if (str[i] == sub[0]) {
            return true;
          }
        }
        return false;
      }
      return str.IndexOf(sub, StringComparison.Ordinal) >= 0;
    }

    private static bool StartsWith(string str, string sub) {
      return str.StartsWith(sub, StringComparison.Ordinal);
    }

    private static bool EndsWith(string str, string sub) {
      return str.EndsWith(sub, StringComparison.Ordinal);
    }

    private static int HexInt(string str) {
      return EInteger.FromRadixString(str, 16).ToInt32Unchecked();
    }

    private static EContext SetRounding(
      EContext ctx,
      string round) {
      if (round.Equals(">", StringComparison.Ordinal)) {
        ctx = ctx.WithRounding(ERounding.Ceiling);
      }
      if (round.Equals("<", StringComparison.Ordinal)) {
        ctx = ctx.WithRounding(ERounding.Floor);
      }
      if (round.Equals("0", StringComparison.Ordinal)) {
        ctx = ctx.WithRounding(ERounding.Down);
      }
      if (round.Equals("=0", StringComparison.Ordinal)) {
        ctx = ctx.WithRounding(ERounding.HalfEven);
      }
      if (round.Equals("h>", StringComparison.Ordinal) ||
        round.Equals("=^", StringComparison.Ordinal)) {
        ctx = ctx.WithRounding(ERounding.HalfUp);
      }
      if (round.Equals("h<", StringComparison.Ordinal)) {
        ctx = ctx.WithRounding(ERounding.HalfDown);
      }
      return ctx;
    }

    private static string ConvertOp(string s) {
      return s.Equals("S", StringComparison.Ordinal) ? "sNaN" :
        ((s.Equals("Q", StringComparison.Ordinal) || s.Equals("#",
              StringComparison.Ordinal)) ? "NaN" : s);
    }

    private interface IExtendedNumber : IComparable<IExtendedNumber> {
      object Value {
        get;
      }

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
        var dn = new DecimalNumber();
        dn.ed = dec;
        return dn;
      }

      public override bool Equals(object obj) {
        var other = obj as DecimalNumber;
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

      public IExtendedNumber Add(
        IExtendedNumber b,
        EContext ctx) {
        return Create(this.ed.Add(ToValue(b), ctx));
      }

      public IExtendedNumber Subtract(
        IExtendedNumber b,
        EContext ctx) {
        return Create(this.ed.Subtract(ToValue(b), ctx));
      }

      public IExtendedNumber Multiply(
        IExtendedNumber b,
        EContext ctx) {
        return Create(this.ed.Multiply(ToValue(b), ctx));
      }

      public IExtendedNumber Divide(
        IExtendedNumber b,
        EContext ctx) {
        return Create(this.ed.Divide(ToValue(b), ctx));
      }

      public IExtendedNumber SquareRoot(EContext ctx) {
        return Create(this.ed.Sqrt(ctx));
      }

      public IExtendedNumber MultiplyAndAdd(
        IExtendedNumber b,
        IExtendedNumber c,
        EContext ctx) {
        return Create(this.ed.MultiplyAndAdd(ToValue(b), ToValue(c), ctx));
      }

      public IExtendedNumber MultiplyAndSubtract(
        IExtendedNumber b,
        IExtendedNumber c,
        EContext ctx) {
        return Create(this.ed.MultiplyAndSubtract(
              ToValue(b),
              ToValue(c),
              ctx));
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
        var dn = new BinaryNumber();
        if (dec == null) {
          throw new ArgumentNullException(nameof(dec));
        }
        dn.ef = dec;
        return dn;
      }

      public static BinaryNumber FromString(String str) {
        if (str.Equals("NaN", StringComparison.Ordinal)) {
          return Create(EFloat.NaN);
        }
        if (str.Equals("sNaN", StringComparison.Ordinal)) {
          return Create(EFloat.SignalingNaN);
        }
        if (str.Equals("+Zero", StringComparison.Ordinal)) {
          return Create(EFloat.Zero);
        }
        if (str.Equals("0x0", StringComparison.Ordinal)) {
          return Create(EFloat.Zero);
        }
        if (str.Equals("0x1", StringComparison.Ordinal)) {
          return Create(EFloat.One);
        }
        if (str.Equals("-Zero", StringComparison.Ordinal)) {
          return Create(EFloat.NegativeZero);
        }
        if (str.Equals("-Inf", StringComparison.Ordinal)) {
          return Create(EFloat.NegativeInfinity);
        }
        if (str.Equals("+Inf", StringComparison.Ordinal)) {
          return Create(EFloat.PositiveInfinity);
        }
        var offset = 0;
        var negative = false;
        if (str[0] == '+' || str[0] == '-') {
          negative = str[0] == '-';
          ++offset;
        }
        int i = offset;
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
          throw new ArgumentNullException(nameof(words));
        }
        if (words.Length == 1) {
          bool neg = (words[0] >> 31) != 0;
          int exponent = (words[0] >> 23) & 0xff;
          int mantissa = words[0] & 0x7fffff;
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
          bool neg = (words[0] >> 31) != 0;
          int exponent = (words[0] >> 20) & 0x7ff;
          int mantissa = words[0] & 0xfffff;
          int mantissaNonzero = mantissa | words[1];
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
          EInteger bigmantissa = EInteger.Zero;
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
          bool neg = (words[0] >> 31) != 0;
          int exponent = (words[0] >> 16) & 0x7fff;
          int mantissa = words[0] & 0xffff;
          int mantissaNonzero = mantissa | words[3] | words[1] | words[2];
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
          EInteger bigmantissa = EInteger.Zero;
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

      public override bool Equals(object obj) {
        var other = obj as BinaryNumber;
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

      public IExtendedNumber Add(
        IExtendedNumber b,
        EContext ctx) {
        return Create(this.ef.Add(ToValue(b), ctx));
      }

      public IExtendedNumber Subtract(
        IExtendedNumber b,
        EContext ctx) {
        return Create(this.ef.Subtract(ToValue(b), ctx));
      }

      public IExtendedNumber Multiply(
        IExtendedNumber b,
        EContext ctx) {
        return Create(this.ef.Multiply(ToValue(b), ctx));
      }

      public IExtendedNumber Divide(
        IExtendedNumber b,
        EContext ctx) {
        return Create(this.ef.Divide(ToValue(b), ctx));
      }

      public BinaryNumber Pow(
        IExtendedNumber b,
        EContext ctx) {
        return Create(this.ef.Pow(ToValue(b), ctx));
      }

      public IExtendedNumber SquareRoot(EContext ctx) {
        return Create(this.ef.Sqrt(ctx));
      }

      public IExtendedNumber MultiplyAndAdd(
        IExtendedNumber b,
        IExtendedNumber c,
        EContext ctx) {
        return Create(this.ef.MultiplyAndAdd(ToValue(b), ToValue(c), ctx));
      }

      public IExtendedNumber MultiplyAndSubtract(
        IExtendedNumber b,
        IExtendedNumber c,
        EContext ctx) {
        return Create(this.ef.MultiplyAndSubtract(ToValue(b), ToValue(c), ctx));
      }

      public bool IsNear(IExtendedNumber bn) {
        EFloat ulpdiff = EFloat.Create(
            (EInteger)2,
            ToValue(this).Exponent);
        return ToValue(this).Subtract(ToValue(bn)).Abs().CompareTo(
            ulpdiff) <= 0;
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

    internal static int ParseLineInput(string ln) {
      if (ln.Length == 0) {
        return 0;
      }
      var ix = -1;
      for (var i = 0; i < ln.Length; ++i) {
        if (ln[i] == '\u0020') {
          // Space found
          ix = i;
          break;
        }
      }
      // NOTE: ix < 2 includes cases where space is not found
      if (ix < 2 || (ln[ix - 1] != 'd' && ln[ix - 1] != 's' &&
          ln[ix - 1] != 'q')) {
        return 0;
      }
      string[] chunks = SplitAtSpaceRuns(ln);
      if (chunks.Length < 4) {
        return 0;
      }
      string type = chunks[0];
      EContext ctx = null;
      string op = String.Empty;
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
      string round = chunks[1];
      if (round.Length != 1) {
        { return 0;
        }
      }
      string flags = chunks[3];
      string compareOp = chunks[2];
      // sw.Start();
      switch (round[0]) {
        case 'm':
          ctx = ctx.WithRounding(ERounding.Floor);
          break;
        case 'p':
          ctx = ctx.WithRounding(ERounding.Ceiling);
          break;
        case 'z':
          ctx = ctx.WithRounding(ERounding.Down);
          break;
        case 'n':
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
          if (chunks.Length == 6 || String.IsNullOrEmpty(chunks[6])) {
            result = op2;
            op2 = null;
          } else {
            result = BinaryNumber.FromFloatWords(new[] {
              HexInt(chunks[6]),
            });
          }

          break;
        case 1:
          // double
          if (chunks.Length < 8) {
            return 0;
          }
          op1 = BinaryNumber.FromFloatWords(new[] {
            HexInt(chunks[4]),
            HexInt(chunks[5]),
          });
          op2 = BinaryNumber.FromFloatWords(new[] {
            HexInt(chunks[6]),
            HexInt(chunks[7]),
          });
          if (chunks.Length == 8 || String.IsNullOrEmpty(chunks[8])) {
            result = op2;
            op2 = null;
            return 0;
          }
          result = BinaryNumber.FromFloatWords(new[] {
            HexInt(chunks[8]),
            HexInt(chunks[9]),
          });
          break;
        case 2:
          // quad
          if (chunks.Length < 12) {
            return 0;
          }
          op1 = BinaryNumber.FromFloatWords(new[] {
            HexInt(chunks[4]),
            HexInt(chunks[5]), HexInt(chunks[6]),
            HexInt(chunks[7]),
          });
          op2 = BinaryNumber.FromFloatWords(new[] {
            HexInt(chunks[8]),
            HexInt(chunks[9]), HexInt(chunks[10]),
            HexInt(chunks[11]),
          });
          if (chunks.Length == 12 || String.IsNullOrEmpty(chunks[12])) {
            result = op2;
            op2 = null;
          } else {
            result = BinaryNumber.FromFloatWords(new[] {
              HexInt(chunks[12]), HexInt(chunks[13]),
              HexInt(chunks[14]), HexInt(chunks[15]),
            });
          }

          break;
        default:
          return 0;
      }

      if (compareOp.Equals("uo", StringComparison.Ordinal)) {
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
      if (op.Equals("add", StringComparison.Ordinal)) {
        IExtendedNumber d3 = op1.Add(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("sub", StringComparison.Ordinal)) {
        IExtendedNumber d3 = op1.Subtract(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("mul", StringComparison.Ordinal)) {
        IExtendedNumber d3 = op1.Multiply(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("pow", StringComparison.Ordinal)) {
        IExtendedNumber d3 = op1.Pow(op2, ctx);
        // Check for cases that contradict the General Decimal
        // Arithmetic spec
        if (op1.IsZeroValue() && op2.IsZeroValue()) {
          return 0;
        }
        if (((EFloat)op1.Value).Sign < 0 && op2.IsInfinity()) {
          return 0;
        }
        bool powIntegral = op2.Equals(op2.RoundToIntegralExact(null));
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
          if (compareOp.Equals("vn", StringComparison.Ordinal)) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else if (compareOp.Equals("nb", StringComparison.Ordinal)) {
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
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("floor", StringComparison.Ordinal)) {
        ctx = ctx.WithRounding(ERounding.Floor);
        IExtendedNumber d3 = op1.RoundToIntegralExact(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
      } else if (op.Equals("fabs", StringComparison.Ordinal)) {
        // NOTE: Fabs never sets flags
        IExtendedNumber d3 = op1.Abs(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
      } else if (op.Equals("ceil", StringComparison.Ordinal)) {
        ctx = ctx.WithRounding(ERounding.Ceiling);
        IExtendedNumber d3 = op1.RoundToIntegralExact(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
      } else if (op.Equals("sqrt", StringComparison.Ordinal)) {
        IExtendedNumber d3 = op1.SquareRoot(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
      } else if (op.Equals("log", StringComparison.Ordinal)) {
        IExtendedNumber d3 = op1.Log(ctx);
        if (!result.Equals(d3)) {
          if (compareOp.Equals("vn", StringComparison.Ordinal)) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else if (compareOp.Equals("nb", StringComparison.Ordinal)) {
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
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("exp", StringComparison.Ordinal)) {
        IExtendedNumber d3 = op1.Exp(ctx);
        if (!result.Equals(d3)) {
          if (compareOp.Equals("vn", StringComparison.Ordinal)) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else if (compareOp.Equals("nb", StringComparison.Ordinal)) {
            if (!result.IsNear(d3)) {
              Assert.AreEqual(result, d3, ln);
            }
          } else {
            Assert.AreEqual(result, d3, ln);
          }
        }
        AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
      } else if (op.Equals("log10", StringComparison.Ordinal)) {
        IExtendedNumber d3 = op1.Log10(ctx);
        if (!result.Equals(d3)) {
          if (compareOp.Equals("vn", StringComparison.Ordinal)) {
            if (!result.IsNear(d3)) {
              Console.WriteLine("op1=..." + op1 + " result=" + result +
                " d3=...." + d3);
              Assert.AreEqual(result, d3, ln);
            }
          } else if (compareOp.Equals("nb", StringComparison.Ordinal)) {
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
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("div", StringComparison.Ordinal)) {
        IExtendedNumber d3 = op1.Divide(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN()) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (op.Equals("fmod", StringComparison.Ordinal)) {
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
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      }
      // sw.Stop();
      return 0;
    }

    internal static int ParseLine(string ln) {
      return ParseLine(ln, false);
    }

    internal static int ParseLine(string ln, bool exactResultCheck) {
      string[] chunks = SplitAtFast(ln, (char)0x20, 4, 8);
      if (chunks == null) {
        return 0;
      }
      string type = chunks[0];
      EContext ctx = null;
      var binaryFP = false;
      string op = String.Empty;
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
      if (Contains(op, "cff")) {
        // skip test cases for
        // conversion to another floating point format
        return 0;
      }
      bool squroot = op.Equals("V", StringComparison.Ordinal);
      // var mod = op.Equals("%");
      bool div = op.Equals("/", StringComparison.Ordinal);
      bool fma = op.Equals("*+", StringComparison.Ordinal);
      bool fms = op.Equals("*-", StringComparison.Ordinal);
      bool addop = op.Equals("+", StringComparison.Ordinal);
      bool subop = op.Equals("-", StringComparison.Ordinal);
      bool mul = op.Equals("*", StringComparison.Ordinal);
      string round = chunks[1];
      ctx = SetRounding(ctx, round);
      var offset = 0;
      string traps = String.Empty;
      if (Contains(chunks[2], "x") ||
        chunks[2].Equals("i", StringComparison.Ordinal) ||
        StartsWith(chunks[2], "o")) {
        // traps
        ++offset;
        traps = chunks[2];
      }
      if (Contains(traps, "u") || Contains(traps, "o")) {
        // skip tests that trap underflow or overflow,
        // the results there may be wrong
        /* try {
          throw new InvalidOperationException();
         } catch (InvalidOperationException ex) {
          Console.Write(ln+"\n"+ex);
        }
        */ return 0;
      }
      string op1str = ConvertOp(chunks[2 + offset]);
      string op2str = ConvertOp(chunks[3 + offset]);
      string op3str = String.Empty;
      if (chunks.Length <= 4 + offset) {
        return 0;
      }
      string sresult = String.Empty;
      string flags = String.Empty;
      op3str = chunks[4 + offset];
      if (op2str.Equals("->", StringComparison.Ordinal)) {
        if (chunks.Length <= 5 + offset) {
          return 0;
        }
        op2str = String.Empty;
        op3str = String.Empty;
        sresult = chunks[4 + offset];
        flags = chunks[5 + offset];
      } else if (op3str.Equals("->", StringComparison.Ordinal)) {
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
      // sw.Start();
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
        IExtendedNumber d3 = op1.Add(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          if (exactResultCheck && (expectedFlags & (EContext.FlagInexact |
                EContext.FlagInvalid)) == 0) {
            d3 = op1.Add(op2, null);
            TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (subop) {
        IExtendedNumber d3 = op1.Subtract(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          if (exactResultCheck && (expectedFlags & (EContext.FlagInexact |
                EContext.FlagInvalid)) == 0) {
            d3 = op1.Subtract(op2, null);
            TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (mul) {
        IExtendedNumber d3 = op1.Multiply(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          if (exactResultCheck && (expectedFlags & (EContext.FlagInexact |
                EContext.FlagInvalid)) == 0) {
            d3 = op1.Multiply(op2, null);
            TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (div) {
        IExtendedNumber d3 = op1.Divide(op2, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          if (exactResultCheck && (expectedFlags & (EContext.FlagInexact |
                EContext.FlagInvalid)) == 0) {
            d3 = op1.Divide(op2, null);
            TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (squroot) {
        IExtendedNumber d3 = op1.SquareRoot(ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
      } else if (fma) {
        IExtendedNumber d3 = op1.MultiplyAndAdd(op2, op3, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (binaryFP && (
            (op1.IsQuietNaN() && (op2.IsSignalingNaN() ||
                op3.IsSignalingNaN())) ||
            (op2.IsQuietNaN() && op3.IsSignalingNaN()))) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          if (exactResultCheck && (expectedFlags & (EContext.FlagInexact |
                EContext.FlagInvalid)) == 0) {
            d3 = op1.MultiplyAndAdd(op2, op3, null);
            TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      } else if (fms) {
        IExtendedNumber d3 = op1.MultiplyAndSubtract(op2, op3, ctx);
        if (!result.Equals(d3)) {
          Assert.AreEqual(result, d3, ln);
        }
        if (op1.IsQuietNaN() && op2.IsSignalingNaN() && binaryFP) {
          // Don't check flags for binary test cases involving quiet
          // NaN followed by signaling NaN, as the semantics for
          // the invalid operation flag in those cases are different
          // than in the General Decimal Arithmetic Specification
        } else {
          if (exactResultCheck && (expectedFlags & (EContext.FlagInexact |
                EContext.FlagInvalid)) == 0) {
            d3 = op1.MultiplyAndSubtract(op2, op3, null);
            TestCommon.CompareTestEqual(result, d3, ln);
          }
          AssertFlagsRestricted(expectedFlags, ctx.Flags, ln);
        }
      }
      return 0;
    }

    public static void ParseDecTests(
      string lines) {
      ParseDecTests(lines, true);
    }

    public static void ParseDecTests(
      string lines,
      bool checkFlags) {
      if (lines == null) {
        throw new ArgumentNullException(nameof(lines));
      }
      string[] linearray = SplitAt(lines, "\n");
      var context = new Dictionary<string, string>();
      foreach (string str in linearray) {
        ParseDecTest(str, context, checkFlags);
      }
    }

    public static void ParseDecTest(
      string ln,
      IDictionary<string, string> context) {
      ParseDecTest(ln, context, true);
    }

    private static string TrimQuotes(string str) {
      return (str == null || str.Length == 0 || (
            str[0] != '\'' && str[0] != '\"' && str[str.Length - 1] != '\'' &&
            str[str.Length - 1] != '\"')) ? str :
        ValueQuotes.Replace(str, String.Empty);
    }

    public static void ParseDecTest(
      string ln,
      IDictionary<string, string> context,
      bool checkFlags) {
      Match match;
      if (ln == null) {
        throw new ArgumentNullException(nameof(ln));
      }
      if (context == null) {
        throw new ArgumentNullException(nameof(context));
      }
      if (ParseLineInput(ln) != 0) {
        return;
      }
      if (ParseLine(ln) != 0) {
        return;
      }
      if (Contains(ln, "-- ")) {
        ln = ln.Substring(0, ln.IndexOf("-- ", StringComparison.Ordinal));
      }
      match = (!Contains(ln, ":")) ? null : ValuePropertyLine.Match(ln);
      if (match != null && match.Success) {
        string paramName = ToLowerCaseAscii(
            match.Groups[1].ToString());
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
        input1 = input1 ?? String.Empty;
        input2 = input2 ?? String.Empty;
        input3 = input3 ?? String.Empty;
        output = output ?? String.Empty;
        flags = flags ?? String.Empty;
        input1 = TrimQuotes(input1);
        input2 = TrimQuotes(input2);
        input3 = TrimQuotes(input3);
        output = TrimQuotes(output);
        if (context == null) {
          throw new ArgumentNullException(nameof(context));
        }
        bool extended = GetKeyOrDefault(
            context,
            "extended",
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
        if (Contains(input1, "#") ||
                 Contains(input2, "#") ||
                 Contains(input3, "#") ||
                 Contains(output, "#")) {
          return;
        }
        if (!extended && (Contains(input1, "sNaN") ||
            Contains(input2, "sNaN") || Contains(input3, "sNaN") ||
            Contains(output, "sNaN"))) {
          Console.WriteLine(ln);
        }
        if (name.Equals("S", StringComparison.Ordinal)) {
          return;
        }
        if (name.Equals("Q", StringComparison.Ordinal)) {
          return;
        }

        if (StartsWith(name, "d32")) {
          return;
        }
        if (StartsWith(name, "d64")) {
          return;
        }
        if (StartsWith(name, "b32")) {
          return;
        }
        if (StartsWith(name, "d128")) {
          return;
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
        if (Contains(input1, "?")) {
          return;
        }
        if (Contains(flags, "Invalid_context")) {
          return;
        }

        ctx = EContext.ForPrecision(precision)
          .WithExponentClamp(clamp).WithExponentRange(
            minexponent,
            maxexponent);
        rounding = ToLowerCaseAscii(GetKeyOrDefault(
              context,
              "rounding",
              "half_even"));
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
        if (checkFlags) {
          ctx = ctx.WithBlankFlags();
        }
        op = ToLowerCaseAscii(op);
        if (op.Length > 3 && op.Substring(op.Length - 3).Equals("_eq",
            StringComparison.Ordinal)) {
          // Binary operators with both operands the same
          input2 = input1;
          op = op.Substring(0, op.Length - 3);
        }
        EDecimal d1 = EDecimal.Zero, d2 = null, d2a = null;
        if (!op.Equals("tosci", StringComparison.Ordinal) &&
          !op.Equals("toeng", StringComparison.Ordinal) &&
          !op.Equals("class", StringComparison.Ordinal) &&
          !op.Equals("format", StringComparison.Ordinal)) {
          try {
            d1 = String.IsNullOrEmpty(input1) ? EDecimal.Zero :
              EDecimal.FromString(input1);
            d2 = String.IsNullOrEmpty(input2) ? null :
              EDecimal.FromString(input2);
            d2a = String.IsNullOrEmpty(input3) ? null :
              EDecimal.FromString(input3);
          } catch (FormatException ex) {
            throw new InvalidOperationException(ln, ex);
          }
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
            // Console.WriteLine("Three-op power not yet supported");
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
            Assert.AreEqual(EDecimals.IsSignalingNaN(d1),
              d1.IsSignalingNaN());
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
            // Console.WriteLine("unknown op " + op);
            return;
          }
        }
        flags = ToLowerCaseAscii(flags);
        bool invalid = Contains(flags, "division_impossible") ||
          Contains(flags, "division_undefined") ||
          Contains(flags, "invalid_operation");
        bool divzero = Contains(flags, "division_by_zero");
        var expectedFlags = 0;
        if (Contains(flags, "inexact")) {
          expectedFlags |= EContext.FlagInexact;
        }
        if (Contains(flags, "subnormal")) {
          expectedFlags |= EContext.FlagSubnormal;
        }
        if (Contains(flags, "rounded")) {
          expectedFlags |= EContext.FlagRounded;
        }
        if (Contains(flags, "underflow")) {
          expectedFlags |= EContext.FlagUnderflow;
        }
        if (Contains(flags, "overflow")) {
          expectedFlags |= EContext.FlagOverflow;
        }
        if (Contains(flags, "clamped")) {
          if (extended || clamp) {
            expectedFlags |= EContext.FlagClamped;
          }
        }
        if (Contains(flags, "lost_digits")) {
          expectedFlags |= EContext.FlagLostDigits;
        }
        bool conversionError = Contains(flags, "conversion_syntax");
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
                  d3.Exponent + "]\n" + ln + "\n" + ctx;
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
        if (checkFlags && !name.Equals("pow118", StringComparison.Ordinal) &&
          !name.Equals("pow119", StringComparison.Ordinal) &&
          !name.Equals("pow120", StringComparison.Ordinal) &&
          !name.Equals("pow121", StringComparison.Ordinal) &&
          !name.Equals("pow122", StringComparison.Ordinal)) {
          AssertFlags(expectedFlags, ctx.Flags, ln);
        }
      }
    }

    public static string ContextToDecTestForm(EContext ec) {
      string roundingstr = "half_even";
      if (ec == null) {
        throw new ArgumentNullException(nameof(ec));
      }
      if (ec.Rounding == ERounding.Ceiling) {
        roundingstr = "ceiling";
      }
      if (ec.Rounding == ERounding.Floor) {
        roundingstr = "floor";
      }
      if (ec.Rounding == ERounding.Up) {
        roundingstr = "up";
      }
      if (ec.Rounding == ERounding.Down) {
        roundingstr = "down";
      }
      if (ec.Rounding == ERounding.HalfEven) {
        roundingstr = "half_even";
      }
      if (ec.Rounding == ERounding.HalfUp) {
        roundingstr = "half_up";
      }
      if (ec.Rounding == ERounding.HalfDown) {
        roundingstr = "half_down";
      }
      if (ec.Rounding == ERounding.OddOrZeroFiveUp) {
        roundingstr = "05up";
      }
      return "\nprecision: " + (ec.Precision.Sign == 0 ? "9999999" :
          ec.Precision.ToString()) + "\nrounding: " + roundingstr +
        "\nmaxexponent: " + (ec.EMax.Sign == 0 ? "999999999999999" :
          ec.EMax.ToString()) +
        "\nminexponent: " + (ec.EMin.Sign == 0 ? "-999999999999999" :
          ec.EMin.ToString()) +
        "\n# adjustexp: " + (ec.AdjustExponent ? "1" : "0") +
        "\nextended: 1\nclamp: " + (ec.ClampNormalExponents ? "1" : "0") +
        "\n";
    }

    public static string FlagsToString(int flags) {
      if (flags == 0) {
        return String.Empty;
      }
      var sb = new System.Text.StringBuilder();
      if ((flags & EContext.FlagInexact) != 0) {
        if (sb.Length > 0) {
          sb.Append(' ');
        }
        sb.Append("Inexact");
      }
      if ((flags & EContext.FlagRounded) != 0) {
        if (sb.Length > 0) {
          sb.Append(' ');
        }
        sb.Append("Rounded");
      }
      if ((flags & EContext.FlagSubnormal) != 0) {
        if (sb.Length > 0) {
          sb.Append(' ');
        }
        sb.Append("Subnormal");
      }
      if ((flags & EContext.FlagOverflow) != 0) {
        if (sb.Length > 0) {
          sb.Append(' ');
        }
        sb.Append("Overflow");
      }
      if ((flags & EContext.FlagUnderflow) != 0) {
        if (sb.Length > 0) {
          sb.Append(' ');
        }
        sb.Append("Underflow");
      }
      if ((flags & EContext.FlagClamped) != 0) {
        if (sb.Length > 0) {
          sb.Append(' ');
        }
        sb.Append("Clamped");
      }
      if ((flags & EContext.FlagInvalid) != 0) {
        if (sb.Length > 0) {
          sb.Append(' ');
        }
        sb.Append("Invalid");
      }
      if ((flags & EContext.FlagDivideByZero) != 0) {
        if (sb.Length > 0) {
          sb.Append(' ');
        }
        sb.Append("Divide_by_zero");
      }
      if ((flags & EContext.FlagLostDigits) != 0) {
        if (sb.Length > 0) {
          sb.Append(' ');
        }
        sb.Append("Lost_digits");
      }
      return sb.ToString();
    }
    public static void AssertOneFlag(
      int expected,
      int actual,
      int flag,
      string name) {
      if (((expected & flag) != 0) != ((actual & flag) != 0)) {
        string msg = name + ": " + FlagsToString(flag) +
          "\nExpected flags: " + FlagsToString(expected) +
          "\nActual flags..: " + FlagsToString(actual);
        Assert.AreEqual(
          (expected & flag) != 0,
          (actual & flag) != 0,
          msg);
      }
    }
    public static void AssertFlags(int expected, int actual, string name) {
      if (expected == actual) {
        return;
      }
      AssertOneFlag(expected, actual, EContext.FlagInexact, name);
      AssertOneFlag(expected, actual, EContext.FlagRounded, name);
      AssertOneFlag(expected, actual, EContext.FlagSubnormal, name);
      AssertOneFlag(expected, actual, EContext.FlagOverflow, name);
      AssertOneFlag(expected, actual, EContext.FlagUnderflow, name);
      AssertOneFlag(expected, actual, EContext.FlagClamped, name);
      AssertOneFlag(expected, actual, EContext.FlagInvalid, name);
      AssertOneFlag(expected, actual, EContext.FlagDivideByZero, name);
      AssertOneFlag(expected, actual, EContext.FlagLostDigits, name);
    }
  }
}
