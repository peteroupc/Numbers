using System;
using System.Text;

namespace PeterO.Numbers {
  internal static class EIntegerCharArrayString {
    public static EInteger FromRadixSubstringImpl(
      char[] cs,
      int radix,
      int index,
      int endIndex) {
      if (radix < 2) {
        throw new ArgumentException("radix(" + radix +
          ") is less than 2");
      }
      if (radix > 36) {
        throw new ArgumentException("radix(" + radix +
          ") is more than 36");
      }
      if (index < 0) {
        throw new ArgumentException("index(" + index + ") is less than " +
          "0");
      }
      if (index > cs.Length) {
        throw new ArgumentException("index(" + index + ") is more than " +
          cs.Length);
      }
      if (endIndex < 0) {
        throw new ArgumentException("endIndex(" + endIndex +
          ") is less than 0");
      }
      if (endIndex > cs.Length) {
        throw new ArgumentException("endIndex(" + endIndex +
          ") is more than " + cs.Length);
      }
      if (endIndex < index) {
        throw new ArgumentException("endIndex(" + endIndex +
          ") is less than " + index);
      }
      if (index == endIndex) {
        throw new FormatException("No digits");
      }
      var negative = false;
      if (cs[index] == '-') {
        ++index;
        if (index == endIndex) {
          throw new FormatException("No digits");
        }
        negative = true;
      }
      // Skip leading zeros
      for (; index < endIndex; ++index) {
        char c = cs[index];
        if (c != 0x30) {
          break;
        }
      }
      int effectiveLength = endIndex - index;
      if (effectiveLength == 0) {
        return EInteger.Zero;
      }
      int[] c2d = EInteger.CharToDigit;
      short[] bigint;
      if (radix == 16) {
        // Special case for hexadecimal radix
        int leftover = effectiveLength & 3;
        int wordCount = effectiveLength >> 2;
        if (leftover != 0) {
          ++wordCount;
        }
        bigint = new short[wordCount];
        int currentDigit = wordCount - 1;
        // Get most significant digits if effective
        // length is not divisible by 4
        if (leftover != 0) {
          var extraWord = 0;
          for (int i = 0; i < leftover; ++i) {
            extraWord <<= 4;
            char c = cs[index + i];
            int digit = (c >= 0x80) ? 36 : c2d[(int)c];
            if (digit >= 16) {
              throw new FormatException("Illegal character found");
            }
            extraWord |= digit;
          }
          bigint[currentDigit] = unchecked((short)extraWord);
          --currentDigit;
          index += leftover;
        }
        #if DEBUG
        if ((endIndex - index) % 4 != 0) {
          throw new InvalidOperationException(
            "doesn't satisfy (endIndex - index) % 4 == 0");
        }
        #endif
        while (index < endIndex) {
          char c = cs[index + 3];
          int digit = (c >= 0x80) ? 36 : c2d[(int)c];
          if (digit >= 16) {
            throw new FormatException("Illegal character found");
          }
          int word = digit;
          c = cs[index + 2];
          digit = (c >= 0x80) ? 36 : c2d[(int)c];
          if (digit >= 16) {
            throw new FormatException("Illegal character found");
          }

          word |= digit << 4;
          c = cs[index + 1];
          digit = (c >= 0x80) ? 36 : c2d[(int)c];
          if (digit >= 16) {
            throw new FormatException("Illegal character found");
          }

          word |= digit << 8;
          c = cs[index];
          digit = (c >= 0x80) ? 36 : c2d[(int)c];
          if (digit >= 16) {
            throw new FormatException("Illegal character found");
          }
          word |= digit << 12;
          index += 4;
          bigint[currentDigit] = unchecked((short)word);
          --currentDigit;
        }
        int count = EInteger.CountWords(bigint);
        return (count == 0) ? EInteger.Zero : new EInteger(
            count,
            bigint,
            negative);
      } else if (radix == 2) {
        // Special case for binary radix
        int leftover = effectiveLength & 15;
        int wordCount = effectiveLength >> 4;
        if (leftover != 0) {
          ++wordCount;
        }
        bigint = new short[wordCount];
        int currentDigit = wordCount - 1;
        // Get most significant digits if effective
        // length is not divisible by 4
        if (leftover != 0) {
          var extraWord = 0;
          for (int i = 0; i < leftover; ++i) {
            extraWord <<= 1;
            char c = cs[index + i];
            int digit = (c == '0') ? 0 : ((c == '1') ? 1 : 2);
            if (digit >= 2) {
              throw new FormatException("Illegal character found");
            }
            extraWord |= digit;
          }
          bigint[currentDigit] = unchecked((short)extraWord);
          --currentDigit;
          index += leftover;
        }
        while (index < endIndex) {
          var word = 0;
          int idx = index + 15;
          for (var i = 0; i < 16; ++i) {
            char c = cs[idx];
            int digit = (c == '0') ? 0 : ((c == '1') ? 1 : 2);
            if (digit >= 2) {
              throw new FormatException("Illegal character found");
            }
            --idx;
            word |= digit << i;
          }
          index += 16;
          bigint[currentDigit] = unchecked((short)word);
          --currentDigit;
        }
        int count = EInteger.CountWords(bigint);
        return (count == 0) ? EInteger.Zero : new EInteger(
            count,
            bigint,
            negative);
      } else {
        return FromRadixSubstringGeneral(
            cs,
            radix,
            index,
            endIndex,
            negative);
      }
    }

    private static EInteger FromRadixSubstringGeneral(
      char[] cs,
      int radix,
      int index,
      int endIndex,
      bool negative) {
      if (endIndex - index > 72) {
        int midIndex = index + ((endIndex - index) / 2);
        EInteger eia = FromRadixSubstringGeneral(
            cs,
            radix,
            index,
            midIndex,
            false);
        EInteger eib = FromRadixSubstringGeneral(
            cs,
            radix,
            midIndex,
            endIndex,
            false);
        EInteger mult = null;
        int intpow = endIndex - midIndex;
        if (radix == 10) {
          eia = NumberUtility.MultiplyByPowerOfFive(eia,
              intpow).ShiftLeft(intpow);
        } else if (radix == 5) {
          eia = NumberUtility.MultiplyByPowerOfFive(eia, intpow);
        } else {
          mult = EInteger.FromInt32(radix).Pow(endIndex - midIndex);
          eia = eia.Multiply(mult);
        }
        eia = eia.Add(eib);
        // DebugUtility.Log("index={0} {1} {2} [pow={3}] [pow={4} ms, muladd={5} ms]",
        // index, midIndex, endIndex, endIndex-midIndex, swPow.ElapsedMilliseconds,
        // swMulAdd.ElapsedMilliseconds);
        if (negative) {
          eia = eia.Negate();
        }
        return eia;
      } else {
        return FromRadixSubstringInner(cs, radix, index, endIndex, negative);
      }
    }

    private static EInteger FromRadixSubstringInner(
      char[] cs,
      int radix,
      int index,
      int endIndex,
      bool negative) {
      if (endIndex - index <= 18 && radix <= 10) {
        long rv = 0;
        if (radix == 10) {
          for (int i = index; i < endIndex; ++i) {
            char c = cs[i];
            var digit = (int)c - 0x30;
            if (digit >= radix || digit < 0) {
              throw new FormatException("Illegal character found");
            }
            rv = (rv * 10) + digit;
          }
          return EInteger.FromInt64(negative ? -rv : rv);
        } else {
          for (int i = index; i < endIndex; ++i) {
            char c = cs[i];
            int digit = (c >= 0x80) ? 36 : ((int)c - 0x30);
            if (digit >= radix || digit < 0) {
              throw new FormatException("Illegal character found");
            }
            rv = (rv * radix) + digit;
          }
          return EInteger.FromInt64(negative ? -rv : rv);
        }
      }
      int[] c2d = EInteger.CharToDigit;
      int[] d2w = EInteger.DigitsInWord;
      long lsize = ((long)(endIndex - index) * 100 /
d2w[radix]) + 1;
      lsize = Math.Min(lsize, Int32.MaxValue);
      lsize = Math.Max(lsize, 5);
      var bigint = new short[(int)lsize];
      if (radix == 10) {
        long rv = 0;
        int ei = endIndex - index <= 18 ? endIndex : index + 18;
        for (int i = index; i < ei; ++i) {
          char c = cs[i];
          var digit = (int)c - 0x30;
          if (digit >= radix || digit < 0) {
            throw new FormatException("Illegal character found");
          }
          rv = (rv * 10) + digit;
        }
        bigint[0] = unchecked((short)(rv & unchecked((short)0xffff)));
        bigint[1] = unchecked((short)((rv >> 16) & unchecked((short)0xffff)));
        bigint[2] = unchecked((short)((rv >> 32) & unchecked((short)0xffff)));
        bigint[3] = unchecked((short)((rv >> 48) & unchecked((short)0xffff)));
        int bn = Math.Min(bigint.Length, 5);
        for (int i = ei; i < endIndex; ++i) {
          short carry = 0;
          var digit = 0;
          var overf = 0;
          if (i < endIndex - 3) {
            overf = 55536; // 2**16 minus 10**4
            var d1 = (int)cs[i] - 0x30;
            var d2 = (int)cs[i + 1] - 0x30;
            var d3 = (int)cs[i + 2] - 0x30;
            var d4 = (int)cs[i + 3] - 0x30;
            i += 3;
            if (d1 >= 10 || d1 < 0 || d2 >= 10 || d2 < 0 || d3 >= 10 ||
              d3 < 0 || d4 >= 10 || d4 < 0) {
              throw new FormatException("Illegal character found");
            }
            digit = (d1 * 1000) + (d2 * 100) + (d3 * 10) + d4;
            // Multiply by 10**4
            for (int j = 0; j < bn; ++j) {
              int p;
              p = unchecked((((int)bigint[j]) & unchecked((short)0xffff)) *
10000);
              int p2 = ((int)carry) & unchecked((short)0xffff);
              p = unchecked(p + p2);
              bigint[j] = unchecked((short)p);
              carry = unchecked((short)(p >> 16));
            }
          } else {
            overf = 65526; // 2**16 minus radix 10
            char c = cs[i];
            digit = (int)c - 0x30;
            if (digit >= 10 || digit < 0) {
              throw new FormatException("Illegal character found");
            }
            // Multiply by 10
            for (int j = 0; j < bn; ++j) {
              int p;
              p = unchecked((((int)bigint[j]) & unchecked((short)0xffff)) * 10);
              int p2 = ((int)carry) & unchecked((short)0xffff);
              p = unchecked(p + p2);
              bigint[j] = unchecked((short)p);
              carry = unchecked((short)(p >> 16));
            }
          }
          if (carry != 0) {
            bigint = EInteger.GrowForCarry(bigint, carry);
          }
          // Add the parsed digit
          if (digit != 0) {
            int d = bigint[0] & unchecked((short)0xffff);
            if (d <= overf) {
              bigint[0] = unchecked((short)(d + digit));
            } else if (EInteger.IncrementWords(
                bigint,
                0,
                bigint.Length,
                (short)digit) != 0) {
              bigint = EInteger.GrowForCarry(bigint, (short)1);
            }
          }
          bn = Math.Min(bigint.Length, bn + 1);
        }
      } else {
        var haveSmallInt = true;
        int[] msi = EInteger.MaxSafeInts;
        int maxSafeInt = msi[radix - 2];
        int maxShortPlusOneMinusRadix = 65536 - radix;
        var smallInt = 0;
        for (int i = index; i < endIndex; ++i) {
          char c = cs[i];
          int digit = (c >= 0x80) ? 36 : c2d[(int)c];
          if (digit >= radix) {
            throw new FormatException("Illegal character found");
          }
          if (haveSmallInt && smallInt < maxSafeInt) {
            smallInt = (smallInt * radix) + digit;
          } else {
            if (haveSmallInt) {
              bigint[0] = unchecked((short)(smallInt &
unchecked((short)0xffff)));
              bigint[1] = unchecked((short)((smallInt >> 16) &
unchecked((short)0xffff)));
              haveSmallInt = false;
            }
            // Multiply by the radix
            short carry = 0;
            int n = bigint.Length;
            for (int j = 0; j < n; ++j) {
              int p;
              p = unchecked((((int)bigint[j]) & unchecked((short)0xffff)) *
radix);
              int p2 = ((int)carry) & unchecked((short)0xffff);
              p = unchecked(p + p2);
              bigint[j] = unchecked((short)p);
              carry = unchecked((short)(p >> 16));
            }
            if (carry != 0) {
              bigint = EInteger.GrowForCarry(bigint, carry);
            }
            // Add the parsed digit
            if (digit != 0) {
              int d = bigint[0] & unchecked((short)0xffff);
              if (d <= maxShortPlusOneMinusRadix) {
                bigint[0] = unchecked((short)(d + digit));
              } else if (EInteger.IncrementWords(
                  bigint,
                  0,
                  bigint.Length,
                  (short)digit) != 0) {
                bigint = EInteger.GrowForCarry(bigint, (short)1);
              }
            }
          }
        }
        if (haveSmallInt) {
          bigint[0] = unchecked((short)(smallInt & unchecked((short)0xffff)));
          bigint[1] = unchecked((short)((smallInt >> 16) &
unchecked((short)0xffff)));
        }
      }
      int count = EInteger.CountWords(bigint);
      return (count == 0) ? EInteger.Zero : new EInteger(
          count,
          bigint,
          negative);
    }
}
}
