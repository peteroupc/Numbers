/*
Written by Peter O.
Any copyright to this work is released to the Public Domain.
In case this is not possible, this work is also
licensed under the Unlicense: https://unlicense.org/

 */
using System;

namespace PeterO.Numbers {
  internal static class Extras {
    public static byte[] CharsConcat(
      byte[] c1,
      int offset1,
      int length1,
      byte[] c2,
      int offset2,
      int length2) {
      var chars = new byte[length1 + length2];
      Array.Copy(c1, offset1, chars, 0, length1);
      Array.Copy(c2, offset2, chars, length1, length2);
      return chars;
    }
    public static string CharsConcat(
      string s1,
      int offset1,
      int length1,
      string s2,
      int offset2,
      int length2) {
      // Console.WriteLine(s1.Substring(offset1, length1));
      // Console.WriteLine(s2.Substring(offset2, length2));
      return s1.Substring(offset1, length1) + s2.Substring(offset2, length2);
    }

    public static char[] CharsConcat(
      char[] c1,
      int offset1,
      int length1,
      char[] c2,
      int offset2,
      int length2) {
      var chars = new char[length1 + length2];
      Array.Copy(c1, offset1, chars, 0, length1);
      Array.Copy(c2, offset2, chars, length1, length2);
      return chars;
    }

    public static int[] DoubleToIntegers(double dbl) {
      long value = BitConverter.ToInt64(
          BitConverter.GetBytes((double)dbl),
          0);
      var ret = new int[2];
      ret[0] = unchecked((int)(value & 0xffffffffL));
      ret[1] = unchecked((int)((value >> 32) & 0xffffffffL));
      return ret;
    }

    public static double IntegersToDouble(int[] integers) {
      // NOTE: least significant word first
      return IntegersToDouble(integers[0], integers[1]);
    }

    public static double IntegersToDouble(int lsw, int msw) {
      // NOTE: least significant word first
      long value = ((long)lsw) & 0xffffffffL;
      value |= (((long)msw) & 0xffffffffL) << 32;
      return BitConverter.ToDouble(BitConverter.GetBytes((long)value), 0);
    }
  }
}
