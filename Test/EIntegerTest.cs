using System;
using System.Text;
using NUnit.Framework;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class EIntegerTest {
    private static long[] BitLengths = { -4294967297, 33, -4294967296, 32,
      -4294967295, 32, -2147483649, 32, -2147483648, 31, -2147483647, 31,
      -1073741825, 31, -1073741824, 30, -1073741823, 30, -536870913, 30,
      -536870912, 29, -536870911, 29, -268435457, 29, -268435456, 28,
      -268435455, 28, -134217729, 28, -134217728, 27, -134217727, 27,
      -67108865, 27, -67108864, 26, -67108863, 26, -33554433, 26, -33554432,
      25, -33554431, 25, -16777217, 25, -16777216, 24, -16777215, 24,
      -8388609, 24, -8388608, 23, -8388607, 23, -4194305, 23, -4194304, 22,
      -4194303, 22, -2097153, 22, -2097152, 21, -2097151, 21, -1048577, 21,
      -1048576, 20, -1048575, 20, -524289, 20, -524288, 19, -524287, 19,
      -262145, 19, -262144, 18, -262143, 18, -131073, 18, -131072, 17,
      -131071, 17, -65537, 17, -65536, 16, -65535, 16, -32769, 16, -32768,
      15, -32767, 15, -16385, 15, -16384, 14, -16383, 14, -8193, 14, -8192,
      13, -8191, 13, -4097, 13, -4096, 12, -4095, 12, -2049, 12, -2048, 11,
      -2047, 11, -1025, 11, -1024, 10, -1023, 10, -513, 10, -512, 9, -511,
      9, -257, 9, -256, 8, -255, 8, -129, 8, -128, 7, -127, 7, -65, 7, -64,
      6, -63, 6, -33, 6, -32, 5, -31, 5, -17, 5, -16, 4, -15, 4, -9, 4, -8,
      3, -7, 3, -5, 3, -4, 2, -3, 2, -2, 1, -1, 0, 0, 0, 1, 1, 2, 2, 3, 2,
      4, 3, 5, 3, 7, 3, 8, 4, 9, 4, 15, 4, 16, 5, 17, 5, 31, 5, 32, 6, 33,
      6, 63, 6, 64, 7, 65, 7, 127, 7, 128, 8, 129, 8, 255, 8, 256, 9, 257,
      9, 511, 9, 512, 10, 513, 10, 1023, 10, 1024, 11, 1025, 11, 2047, 11,
      2048, 12, 2049, 12, 4095, 12, 4096, 13, 4097, 13, 8191, 13, 8192, 14,
      8193, 14, 16383, 14, 16384, 15, 16385, 15, 32767, 15, 32768, 16,
      32769, 16, 65535, 16, 65536, 17, 65537, 17, 131071, 17, 131072, 18,
      131073, 18, 262143, 18, 262144, 19, 262145, 19, 524287, 19, 524288,
      20, 524289, 20, 1048575, 20, 1048576, 21, 1048577, 21, 2097151, 21,
      2097152, 22, 2097153, 22, 4194303, 22, 4194304, 23, 4194305, 23,
      8388607, 23, 8388608, 24, 8388609, 24, 16777215, 24, 16777216, 25,
      16777217, 25, 33554431, 25, 33554432, 26, 33554433, 26, 67108863, 26,
      67108864, 27, 67108865, 27, 134217727, 27, 134217728, 28, 134217729,
      28, 268435455, 28, 268435456, 29, 268435457, 29, 536870911, 29,
      536870912, 30, 536870913, 30, 1073741823, 30, 1073741824, 31,
      1073741825, 31, 2147483647, 31, 2147483648, 32, 2147483649, 32,
      4294967295, 32, 4294967296, 33, 4294967297, 33 };

    private static long[] LowBits = { 0, -1, 1, 0, 2, 1, 3, 0, 4, 2, 5, 0,
      7, 0, 8, 3, 9, 0, 15, 0, 16, 4, 17, 0, 31, 0, 32, 5, 33, 0, 63, 0, 64,
      6, 65, 0, 127, 0, 128, 7, 129, 0, 255, 0, 256, 8, 257, 0, 511, 0, 512,
      9, 513, 0, 1023, 0, 1024, 10, 1025, 0, 2047, 0, 2048, 11, 2049, 0,
      4095, 0, 4096, 12, 4097, 0, 8191, 0, 8192, 13, 8193, 0, 16383, 0,
      16384, 14, 16385, 0, 32767, 0, 32768, 15, 32769, 0, 65535, 0, 65536,
      16, 65537, 0, 131071, 0, 131072, 17, 131073, 0, 262143, 0, 262144, 18,
      262145, 0, 524287, 0, 524288, 19, 524289, 0, 1048575, 0, 1048576, 20,
      1048577, 0, 2097151, 0, 2097152, 21, 2097153, 0, 4194303, 0, 4194304,
      22, 4194305, 0, 8388607, 0, 8388608, 23, 8388609, 0, 16777215, 0,
      16777216, 24, 16777217, 0, 33554431, 0, 33554432, 25, 33554433, 0,
      67108863, 0, 67108864, 26, 67108865, 0, 134217727, 0, 134217728, 27,
      134217729, 0, 268435455, 0, 268435456, 28, 268435457, 0, 536870911, 0,
      536870912, 29, 536870913, 0, 1073741823, 0, 1073741824, 30,
      1073741825, 0, 2147483647, 0, 2147483648, 31, 2147483649, 0,
      4294967295, 0, 4294967296, 32, 4294967297, 0 };

    public static void AssertAdd(EInteger bi, EInteger bi2, string s) {
      EIntegerTest.AssertBigIntegersEqual(s, bi + (EInteger)bi2);
      EIntegerTest.AssertBigIntegersEqual(s, bi2 + (EInteger)bi);
      EInteger negbi = EInteger.Zero - (EInteger)bi;
      EInteger negbi2 = EInteger.Zero - (EInteger)bi2;
      EIntegerTest.AssertBigIntegersEqual(s, bi - (EInteger)negbi2);
      EIntegerTest.AssertBigIntegersEqual(s, bi2 - (EInteger)negbi);
    }

    public static void AssertBigIntegersEqual(string a, EInteger b) {
      Assert.AreEqual(a, b.ToString());
      EInteger a2 = BigFromString(a);
      TestCommon.CompareTestEqualAndConsistent(a2, b);
      TestCommon.AssertEqualsHashCode(a2, b);
    }

    public static void DoTestDivide(
string dividend,
string divisor,
string result) {
      EInteger bigintA = BigFromString(dividend);
      EInteger bigintB = BigFromString(divisor);
      EInteger bigintTemp;
      if (bigintB.IsZero) {
        try {
          bigintTemp = bigintA / bigintB;
          Assert.Fail("Expected divide by 0 error");
        } catch (ArithmeticException ex) {
          Console.WriteLine(ex.Message);
        }
        try {
          bigintA.DivRem(bigintB);
          Assert.Fail("Should have failed");
        } catch (ArithmeticException) {
          Console.Write(String.Empty);
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      } else {
        AssertBigIntegersEqual(result, bigintA / bigintB);
        AssertBigIntegersEqual(result, bigintA.DivRem(bigintB)[0]);
      }
    }

    public static void DoTestDivRem(
string dividend,
string divisor,
string result,
string rem) {
      EInteger bigintA = BigFromString(dividend);
      EInteger bigintB = BigFromString(divisor);
      EInteger rembi;
      if (bigintB.IsZero) {
        try {
          EInteger quo;
          {
            EInteger[] divrem = (bigintA).DivRem(bigintB);
            quo = divrem[0];
            rembi = divrem[1];
          }
          if (((object)quo) == null) {
            Assert.Fail();
          }
          Assert.Fail("Expected divide by 0 error");
        } catch (ArithmeticException ex) {
          Console.WriteLine(ex.Message);
        }
      } else {
        EInteger quo;
        {
          EInteger[] divrem = (bigintA).DivRem(bigintB);
          quo = divrem[0];
          rembi = divrem[1];
        }
        AssertBigIntegersEqual(result, quo);
        AssertBigIntegersEqual(rem, rembi);
      }
    }

    public static void DoTestMultiply(string m1, string m2, string result) {
      EInteger bigintA = BigFromString(m1);
      EInteger bigintB = BigFromString(m2);
      bigintA *= bigintB;
      AssertBigIntegersEqual(result, bigintA);
    }

    public static void DoTestPow(string m1, int m2, string result) {
      EInteger bigintA = BigFromString(m1);
      AssertBigIntegersEqual(result, bigintA.Pow(m2));
      AssertBigIntegersEqual(result, bigintA.PowBigIntVar((EInteger)m2));
    }

    public static void DoTestRemainder(
string dividend,
string divisor,
string result) {
      EInteger bigintA = BigFromString(dividend);
      EInteger bigintB = BigFromString(divisor);
      if (bigintB.IsZero) {
        try {
          bigintA.Remainder(bigintB); Assert.Fail("Expected divide by 0 error");
        } catch (ArithmeticException ex) {
          Console.WriteLine(ex.Message);
        }
        try {
          bigintA.DivRem(bigintB);
          Assert.Fail("Should have failed");
        } catch (ArithmeticException) {
          Console.Write(String.Empty);
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      } else {
        AssertBigIntegersEqual(result, bigintA.Remainder(bigintB));
        AssertBigIntegersEqual(result, bigintA.DivRem(bigintB)[1]);
      }
    }

    public static void DoTestShiftLeft(string m1, int m2, string result) {
      EInteger bigintA = BigFromString(m1);
      AssertBigIntegersEqual(result, bigintA << m2);
      m2 = -m2;
      AssertBigIntegersEqual(result, bigintA >> m2);
    }

    public static void DoTestShiftRight(string m1, int m2, string result) {
      EInteger bigintA = BigFromString(m1);
      AssertBigIntegersEqual(result, bigintA >> m2);
      m2 = -m2;
      AssertBigIntegersEqual(result, bigintA << m2);
    }

    public static void DoTestShiftRight2(string m1, int m2, EInteger result) {
      EInteger bigintA = BigFromString(m1);
      TestCommon.CompareTestEqualAndConsistent(result, bigintA >> m2);
      m2 = -m2;
      TestCommon.CompareTestEqualAndConsistent(result, bigintA << m2);
    }

    public static bool IsPrime(int n) {
      if (n < 2) {
        return false;
      }
      if (n == 2) {
        return true;
      }
      if (n % 2 == 0) {
        return false;
      }
      if (n <= 23) {
        return n == 3 || n == 5 || n == 7 || n == 11 ||
          n == 13 || n == 17 || n == 19 || n == 23;
      }
      // Use a deterministic Rabin-Miller test
      int d = n - 1;
      var shift = 0;
      while ((d & 1) == 0) {
        d >>= 1;
        ++shift;
      }
      int mp = 0, mp2 = 0;
      var found = false;
      // For all 32-bit integers it's enough
      // to check the strong pseudoprime
      // bases 2, 7, and 61
      if (n > 2) {
        mp = ModPow(2, d, n);
        if (mp != 1 && mp + 1 != n) {
          found = false;
          for (var i = 1; i < shift; ++i) {
            mp2 = ModPow(2, d << i, n);
            if (mp2 + 1 == n) {
              found = true;
              break;
            }
          }
          if (found) {
            return false;
          }
        }
      }
      if (n > 7) {
        mp = ModPow(7, d, n);
        if (mp != 1 && mp + 1 != n) {
          found = false;
          for (var i = 1; i < shift; ++i) {
            mp2 = ModPow(7, d << i, n);
            if (mp2 + 1 == n) {
              found = true;
              break;
            }
          }
          if (found) {
            return false;
          }
        }
      }
      if (n > 61) {
        mp = ModPow(61, d, n);
        if (mp != 1 && mp + 1 != n) {
          found = false;
          for (var i = 1; i < shift; ++i) {
            mp2 = ModPow(61, d << i, n);
            if (mp2 + 1 == n) {
              found = true;
              break;
            }
          }
          if (found) {
            return false;
          }
        }
      }
      return true;
    }

    public static int ModPow(int x, int pow, int intMod) {
      if (x < 0) {
        throw new ArgumentException(
          "x (" + x + ") is less than 0");
      }
      if (pow <= 0) {
        throw new ArgumentException(
          "pow (" + pow + ") is not greater than 0");
      }
      if (intMod <= 0) {
        throw new ArgumentException(
          "mod (" + intMod + ") is not greater than 0");
      }
      var r = 1;
      int v = x;
      while (pow != 0) {
        if ((pow & 1) != 0) {
          r = (int)(((long)r * (long)v) % intMod);
        }
        pow >>= 1;
        if (pow != 0) {
          v = (int)(((long)v * (long)v) % intMod);
        }
      }
      return r;
    }

    public static EInteger RandomBigInteger(FastRandom r) {
      int selection = r.NextValue(100);
      int count = r.NextValue(60) + 1;
      if (selection < 40) {
        count = r.NextValue(7) + 1;
      }
      if (selection < 50) {
        count = r.NextValue(15) + 1;
      }
      var bytes = new byte[count];
      for (var i = 0; i < count; ++i) {
        bytes[i] = (byte)((int)r.NextValue(256));
      }
      return BigFromBytes(bytes);
    }

    [Test]
    public void TestAdd() {
      var posSmall = (EInteger)5;
      EInteger negSmall = -(EInteger)5;
      var posLarge = (EInteger)5555555;
      EInteger negLarge = -(EInteger)5555555;
      AssertAdd(posSmall, posSmall, "10");
      AssertAdd(posSmall, negSmall, "0");
      AssertAdd(posSmall, posLarge, "5555560");
      AssertAdd(posSmall, negLarge, "-5555550");
      AssertAdd(negSmall, negSmall, "-10");
      AssertAdd(negSmall, posLarge, "5555550");
      AssertAdd(negSmall, negLarge, "-5555560");
      AssertAdd(posLarge, posLarge, "11111110");
      AssertAdd(posLarge, negLarge, "0");
      AssertAdd(negLarge, negLarge, "-11111110");
    }

    [Test]
    public void TestAddSubtract() {
      var r = new FastRandom();
      for (var i = 0; i < 10000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        EInteger bigintB = RandomBigInteger(r);
        EInteger bigintC = bigintA + (EInteger)bigintB;
        EInteger bigintD = bigintC - (EInteger)bigintB;
        if (!bigintD.Equals(bigintA)) {
          Assert.AreEqual(
bigintA,
bigintD,
"TestAddSubtract " + bigintA + "; " + bigintB);
        }
        bigintD = bigintC - (EInteger)bigintA;
        if (!bigintD.Equals(bigintB)) {
          Assert.AreEqual(
bigintB,
bigintD,
"TestAddSubtract " + bigintA + "; " + bigintB);
        }
        bigintC = bigintA - (EInteger)bigintB;
        bigintD = bigintC + (EInteger)bigintB;
        if (!bigintD.Equals(bigintA)) {
          Assert.AreEqual(
bigintA,
bigintD,
"TestAddSubtract " + bigintA + "; " + bigintB);
        }
      }
    }

    [Test]
    public void TestAsInt32Checked() {
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).AsInt32Checked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).AsInt32Checked());
      try {
        BigValueOf(Int32.MinValue - 1L).AsInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigValueOf(Int32.MaxValue + 1L).AsInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString("999999999999999999999999999999999").AsInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).AsInt32Checked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).AsInt32Checked());
      try {
        BigValueOf(Int32.MinValue - 1L).AsInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigValueOf(Int32.MaxValue + 1L).AsInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestAsInt64Checked() {
      Assert.AreEqual(
        Int64.MinValue,
        BigValueOf(Int64.MinValue).AsInt64Checked());
      Assert.AreEqual(
        Int64.MaxValue,
        BigValueOf(Int64.MaxValue).AsInt64Checked());
      try {
        EInteger bigintTemp = BigValueOf(Int64.MinValue);
        bigintTemp -= EInteger.One;
        bigintTemp.AsInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger bigintTemp = BigValueOf(Int64.MaxValue);
        bigintTemp += EInteger.One;
        bigintTemp.AsInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(
        unchecked((long)0xFFFFFFF200000000L),
  BigValueOf(unchecked((long)0xFFFFFFF200000000L)).AsInt64Checked());
      Assert.AreEqual(
        unchecked((long)0xFFFFFFF280000000L),
  BigValueOf(unchecked((long)0xFFFFFFF280000000L)).AsInt64Checked());
      Assert.AreEqual(
        unchecked((long)0xFFFFFFF280000001L),
  BigValueOf(unchecked((long)0xFFFFFFF280000001L)).AsInt64Checked());
      Assert.AreEqual(
        unchecked((long)0xFFFFFFF27FFFFFFFL),
  BigValueOf(unchecked((long)0xFFFFFFF27FFFFFFFL)).AsInt64Checked());
      Assert.AreEqual(
        0x0000000380000001L,
        BigValueOf(0x0000000380000001L).AsInt64Checked());
      Assert.AreEqual(
        0x0000000382222222L,
        BigValueOf(0x0000000382222222L).AsInt64Checked());
      Assert.AreEqual(-8L, BigValueOf(-8L).AsInt64Checked());
      Assert.AreEqual(-32768L, BigValueOf(-32768L).AsInt64Checked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).AsInt64Checked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).AsInt64Checked());
      Assert.AreEqual(
        0x80000000L,
        BigValueOf(0x80000000L).AsInt64Checked());
      Assert.AreEqual(
        0x90000000L,
        BigValueOf(0x90000000L).AsInt64Checked());
      try {
        BigFromString("999999999999999999999999999999999").AsInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(
        Int64.MinValue,
        BigValueOf(Int64.MinValue).AsInt64Checked());
      Assert.AreEqual(
        Int64.MaxValue,
        BigValueOf(Int64.MaxValue).AsInt64Checked());
      try {
        EInteger bigintTemp = BigValueOf(Int64.MinValue);
        bigintTemp -= EInteger.One;
        bigintTemp.AsInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger bigintTemp = BigValueOf(Int64.MaxValue);
        bigintTemp += EInteger.One;
        bigintTemp.AsInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      long longV = unchecked((long)0xFFFFFFF200000000L);
      Assert.AreEqual(
longV,
BigValueOf(longV).AsInt64Checked());
      longV = unchecked((long)0xFFFFFFF280000000L);
      Assert.AreEqual(
longV,
BigValueOf(longV).AsInt64Checked());
      longV = unchecked((long)0xFFFFFFF280000001L);
      Assert.AreEqual(
longV,
BigValueOf(longV).AsInt64Checked());
      longV = unchecked((long)0xFFFFFFF27FFFFFFFL);
      Assert.AreEqual(
longV,
BigValueOf(longV).AsInt64Checked());
      Assert.AreEqual(
        0x0000000380000001L,
        BigValueOf(0x0000000380000001L).AsInt64Checked());
      Assert.AreEqual(
        0x0000000382222222L,
        BigValueOf(0x0000000382222222L).AsInt64Checked());
      Assert.AreEqual(-8L, BigValueOf(-8L).AsInt64Checked());
      Assert.AreEqual(-32768L, BigValueOf(-32768L).AsInt64Checked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).AsInt64Checked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).AsInt64Checked());
      Assert.AreEqual(
        0x80000000L,
        BigValueOf(0x80000000L).AsInt64Checked());
      Assert.AreEqual(
        0x90000000L,
        BigValueOf(0x90000000L).AsInt64Checked());
    }
    [Test]
    public void TestBigIntegerModPow() {
      try {
        EInteger.One.ModPow(null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(null, EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(EInteger.Zero, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("-1"), BigFromString("1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("0"), BigFromString("0"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("0"), BigFromString("-1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("1"), BigFromString("0"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("1"), BigFromString("-1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestCanFitInInt() {
      // not implemented yet
    }
    [Test]
    public void TestCompareTo() {
      var r = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        EInteger bigintB = RandomBigInteger(r);
        EInteger bigintC = RandomBigInteger(r);
        TestCommon.CompareTestRelations(bigintA, bigintB, bigintC);
        TestCommon.CompareTestConsistency(bigintA, bigintB, bigintC);
      }
    }
    [Test]
    public void TestDivide() {
      int intA, intB;
      var fr = new FastRandom();
      for (var i = 0; i < 10000; ++i) {
        intA = fr.NextValue(0x1000000);
        intB = fr.NextValue(0x1000000);
        if (intB == 0) {
          continue;
        }
        int c = intA / intB;
        var bigintA = (EInteger)intA;
        var bigintB = (EInteger)intB;
        EInteger bigintC = bigintA / (EInteger)bigintB;
        Assert.AreEqual((int)bigintC, c);
      }
      DoTestDivide("2472320648", "2831812081", "0");
      DoTestDivide("-2472320648", "2831812081", "0");
      DoTestDivide(
    "9999999999999999999999",
    "281474976710655",
    "35527136");
    }
    [Test]
    public void TestDivRem() {
      // not implemented yet
    }

    [Test]
    public void TestDivRem2() {
      try {
        EInteger.One.DivRem(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.DivRem(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestEquals() {
      Assert.IsFalse(EInteger.One.Equals(null));
      Assert.IsFalse(EInteger.Zero.Equals(null));
      Assert.IsFalse(EInteger.One.Equals(EInteger.Zero));
      Assert.IsFalse(EInteger.Zero.Equals(EInteger.One));
      TestCommon.AssertEqualsHashCode(
        EInteger.Zero,
        EInteger.FromString("-0"));
      TestCommon.AssertEqualsHashCode(
        EInteger.FromString("0"),
        EInteger.FromString("-0"));
      TestCommon.AssertEqualsHashCode(
        EInteger.Zero,
        EInteger.One);
      var r = new FastRandom();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomObjects.RandomBigInteger(r);
        EInteger bigintB = RandomObjects.RandomBigInteger(r);
        TestCommon.AssertEqualsHashCode(bigintA, bigintB);
      }
    }

    [Test]
    public void TestExceptions() {
      try {
        BigFromString(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EInteger.Zero.GetSignedBit(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString("x11");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString(".");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString("..");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString("e200");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EInteger.One.Mod((EInteger)(-1));
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Add(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Subtract(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Divide(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Divide(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Remainder(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Mod(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Remainder(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(EInteger.One, ((EInteger)13).Mod((EInteger)4));
      Assert.AreEqual((EInteger)3, ((EInteger)(-13)).Mod((EInteger)4));
    }
    [Test]
    public void TestFromBytes() {
      Assert.AreEqual(
        EInteger.Zero, EInteger.FromBytes(new byte[] { }, false));

      try {
        EInteger.FromBytes(null, false);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestFromRadixString() {
      try {
        EInteger.FromRadixString(null, 10);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", -37);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", Int32.MinValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", Int32.MaxValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var fr = new FastRandom();
      for (int i = 2; i <= 36; ++i) {
        for (int j = 0; j < 100; ++j) {
          StringAndBigInt sabi = StringAndBigInt.Generate(fr, i);
          Assert.AreEqual(
            sabi.BigIntValue,
            EInteger.FromRadixString(sabi.StringValue, i));
        }
      }
    }
    [Test]
    public void TestFromRadixSubstring() {
      try {
        EInteger.FromRadixSubstring(null, 10, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", 1, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", 0, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", -37, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", Int32.MinValue, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", Int32.MaxValue, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, -1, 2);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 4, 5);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 0, -8);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 0, 6);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 2, 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 0, 0);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 1, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("-", 10, 0, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("g", 16, 0, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0123gggg", 16, 0, 8);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0123gggg", 10, 0, 8);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0123aaaa", 10, 0, 8);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var fr = new FastRandom();
      for (int i = 2; i <= 36; ++i) {
        var padding = new StringBuilder();
        for (int j = 0; j < 100; ++j) {
          StringAndBigInt sabi = StringAndBigInt.Generate(fr, i);
          padding.Append('!');
          string sabiString = sabi.StringValue;
          EInteger actualBigInt = EInteger.FromRadixSubstring(
            padding + sabiString,
            i,
            j + 1,
            j + 1 + sabiString.Length);
          Assert.AreEqual(
            sabi.BigIntValue,
            actualBigInt);
        }
      }
    }
    [Test]
    public void TestFromString() {
      try {
        BigFromString("xyz");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString(String.Empty);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestFromSubstring() {
      try {
        EInteger.FromSubstring(null, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EInteger.FromSubstring("123", -1, 2);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 4, 2);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 1, -1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 1, 4);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 1, 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 2, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      {
        string stringTemp = EInteger.FromSubstring(
          "0123456789",
          9,
          10).ToString();
        Assert.AreEqual(
          "9",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromSubstring(
          "0123456789",
          8,
          10).ToString();
        Assert.AreEqual(
          "89",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromSubstring(
          "0123456789",
          7,
          10).ToString();
        Assert.AreEqual(
          "789",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromSubstring(
          "0123456789",
          6,
          10).ToString();
        Assert.AreEqual(
          "6789",
          stringTemp);
      }
    }

    [Test]
    public void TestGcd() {
      try {
        EInteger.Zero.Gcd(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      {
        string stringTemp = EInteger.Zero.Gcd(BigFromString(
        "244")).ToString();
        Assert.AreEqual(
        "244",
        stringTemp);
      }
      {
        string stringTemp = EInteger.Zero.Gcd(BigFromString(
        "-244")).ToString();
        Assert.AreEqual(
        "244",
        stringTemp);
      }
      {
        string stringTemp = BigFromString(
        "244").Gcd(EInteger.Zero).ToString();
        Assert.AreEqual(
        "244",
        stringTemp);
      }
      {
        string stringTemp = BigFromString(
        "-244").Gcd(EInteger.Zero).ToString();
        Assert.AreEqual(
        "244",
        stringTemp);
      }
      {
        string stringTemp = EInteger.One.Gcd(BigFromString("244")).ToString();
        Assert.AreEqual(
        "1",
        stringTemp);
      }
      {
        string stringTemp = EInteger.One.Gcd(BigFromString(
        "-244")).ToString();
        Assert.AreEqual(
        "1",
        stringTemp);
      }
      {
        string stringTemp = BigFromString("244").Gcd(EInteger.One).ToString();
        Assert.AreEqual(
        "1",
        stringTemp);
      }
      {
        string stringTemp = BigFromString(
        "-244").Gcd(EInteger.One).ToString();
        Assert.AreEqual(
        "1",
        stringTemp);
      }
      {
        string stringTemp = BigFromString("15").Gcd(BigFromString(
        "15")).ToString();
        Assert.AreEqual(
        "15",
        stringTemp);
      }
      {
        string stringTemp = BigFromString("-15").Gcd(
                BigFromString("15")).ToString();
        Assert.AreEqual(
        "15",
        stringTemp);
      }
      {
        string stringTemp = BigFromString("15").Gcd(
                BigFromString("-15")).ToString();
        Assert.AreEqual(
        "15",
        stringTemp);
      }
      {
        string stringTemp = BigFromString(
        "-15").Gcd(BigFromString("-15")).ToString();
        Assert.AreEqual(
        "15",
        stringTemp);
      }
      var prime = 0;
      var rand = new FastRandom();
      for (var i = 0; i < 1000; ++i) {
        while (true) {
          prime = rand.NextValue(0x7fffffff);
          prime |= 1;
          if (IsPrime(prime)) {
            break;
          }
        }
        var bigprime = (EInteger)prime;
        EInteger ba = RandomBigInteger(rand);
        if (ba.IsZero) {
          continue;
        }
        ba *= (EInteger)bigprime;
        Assert.AreEqual(
          bigprime,
          EInteger.GreatestCommonDivisor(bigprime, ba));
      }
      TestGcdPair((EInteger)(-1867), (EInteger)(-4456), EInteger.One);
      TestGcdPair((EInteger)4604, (EInteger)(-4516), (EInteger)4);
      TestGcdPair((EInteger)(-1756), (EInteger)4525, EInteger.One);
      TestGcdPair((EInteger)1568, (EInteger)(-4955), EInteger.One);
      TestGcdPair((EInteger)2519, (EInteger)2845, EInteger.One);
      TestGcdPair((EInteger)(-1470), (EInteger)132, (EInteger)6);
      TestGcdPair((EInteger)(-2982), (EInteger)2573, EInteger.One);
      TestGcdPair((EInteger)(-244), (EInteger)(-3929), EInteger.One);
      TestGcdPair((EInteger)(-3794), (EInteger)(-2325), EInteger.One);
      TestGcdPair((EInteger)(-2667), (EInteger)2123, EInteger.One);
      TestGcdPair((EInteger)(-3712), (EInteger)(-1850), (EInteger)2);
      TestGcdPair((EInteger)2329, (EInteger)3874, EInteger.One);
      TestGcdPair((EInteger)1384, (EInteger)(-4278), (EInteger)2);
      TestGcdPair((EInteger)213, (EInteger)(-1217), EInteger.One);
      TestGcdPair((EInteger)1163, (EInteger)2819, EInteger.One);
      TestGcdPair((EInteger)1921, (EInteger)(-579), EInteger.One);
      TestGcdPair((EInteger)3886, (EInteger)(-13), EInteger.One);
      TestGcdPair((EInteger)(-3270), (EInteger)(-3760), (EInteger)10);
      TestGcdPair((EInteger)(-3528), (EInteger)1822, (EInteger)2);
      TestGcdPair((EInteger)1547, (EInteger)(-333), EInteger.One);
      TestGcdPair((EInteger)2402, (EInteger)2850, (EInteger)2);
      TestGcdPair((EInteger)4519, (EInteger)1296, EInteger.One);
      TestGcdPair((EInteger)1821, (EInteger)2949, (EInteger)3);
      TestGcdPair((EInteger)(-2634), (EInteger)(-4353), (EInteger)3);
      TestGcdPair((EInteger)(-1728), (EInteger)199, EInteger.One);
      TestGcdPair((EInteger)(-4646), (EInteger)(-1418), (EInteger)2);
      TestGcdPair((EInteger)(-35), (EInteger)(-3578), EInteger.One);
      TestGcdPair((EInteger)(-2244), (EInteger)(-3250), (EInteger)2);
      TestGcdPair((EInteger)(-3329), (EInteger)1039, EInteger.One);
      TestGcdPair((EInteger)(-3064), (EInteger)(-4730), (EInteger)2);
      TestGcdPair((EInteger)(-1214), (EInteger)4130, (EInteger)2);
      TestGcdPair((EInteger)(-3038), (EInteger)(-3184), (EInteger)2);
      TestGcdPair((EInteger)(-209), (EInteger)(-1617), (EInteger)11);
      TestGcdPair((EInteger)(-1101), (EInteger)(-2886), (EInteger)3);
      TestGcdPair((EInteger)(-3021), (EInteger)(-4499), EInteger.One);
      TestGcdPair((EInteger)3108, (EInteger)1815, (EInteger)3);
      TestGcdPair((EInteger)1195, (EInteger)4618, EInteger.One);
      TestGcdPair((EInteger)(-3643), (EInteger)2156, EInteger.One);
      TestGcdPair((EInteger)(-2067), (EInteger)(-3780), (EInteger)3);
      TestGcdPair((EInteger)4251, (EInteger)1607, EInteger.One);
      TestGcdPair((EInteger)438, (EInteger)741, (EInteger)3);
      TestGcdPair((EInteger)(-3692), (EInteger)(-2135), EInteger.One);
      TestGcdPair((EInteger)(-1076), (EInteger)2149, EInteger.One);
      TestGcdPair((EInteger)(-3224), (EInteger)(-1532), (EInteger)4);
      TestGcdPair((EInteger)(-3713), (EInteger)1721, EInteger.One);
      TestGcdPair((EInteger)3038, (EInteger)(-2657), EInteger.One);
      TestGcdPair((EInteger)4977, (EInteger)(-110), EInteger.One);
      TestGcdPair((EInteger)(-3305), (EInteger)(-922), EInteger.One);
      TestGcdPair((EInteger)1902, (EInteger)2481, (EInteger)3);
      TestGcdPair((EInteger)(-4804), (EInteger)(-1378), (EInteger)2);
      TestGcdPair((EInteger)(-1446), (EInteger)(-4226), (EInteger)2);
      TestGcdPair((EInteger)(-1409), (EInteger)3303, EInteger.One);
      TestGcdPair((EInteger)(-1626), (EInteger)(-3193), EInteger.One);
      TestGcdPair((EInteger)912, (EInteger)(-421), EInteger.One);
      TestGcdPair((EInteger)751, (EInteger)(-1755), EInteger.One);
      TestGcdPair((EInteger)3135, (EInteger)(-3581), EInteger.One);
      TestGcdPair((EInteger)(-4941), (EInteger)(-2885), EInteger.One);
      TestGcdPair((EInteger)4744, (EInteger)3240, (EInteger)8);
      TestGcdPair((EInteger)3488, (EInteger)4792, (EInteger)8);
      TestGcdPair((EInteger)3632, (EInteger)3670, (EInteger)2);
      TestGcdPair((EInteger)(-4821), (EInteger)(-1749), (EInteger)3);
      TestGcdPair((EInteger)4666, (EInteger)2013, EInteger.One);
      TestGcdPair((EInteger)810, (EInteger)(-3466), (EInteger)2);
      TestGcdPair((EInteger)2199, (EInteger)161, EInteger.One);
      TestGcdPair((EInteger)(-1137), (EInteger)(-1620), (EInteger)3);
      TestGcdPair((EInteger)(-472), (EInteger)66, (EInteger)2);
      TestGcdPair((EInteger)3825, (EInteger)2804, EInteger.One);
      TestGcdPair((EInteger)(-2895), (EInteger)1942, EInteger.One);
      TestGcdPair((EInteger)1576, (EInteger)(-4209), EInteger.One);
      TestGcdPair((EInteger)(-277), (EInteger)(-4415), EInteger.One);
      for (var i = 0; i < 1000; ++i) {
        prime = rand.NextValue(0x7fffffff);
        if (rand.NextValue(2) == 0) {
          prime = -prime;
        }
        int intB = rand.NextValue(0x7fffffff);
        if (rand.NextValue(2) == 0) {
          intB = -intB;
        }
        var biga = (EInteger)prime;
        var bigb = (EInteger)intB;
        EInteger ba = EInteger.GreatestCommonDivisor(biga, bigb);
        EInteger bb = EInteger.GreatestCommonDivisor(bigb, biga);
        Assert.AreEqual(ba, bb);
      }
    }

    [Test]
    public void TestGetBits() {
      // not implemented yet
    }
    [Test]
    public void TestGetDigitCount() {
      var r = new FastRandom();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        String str = bigintA.Abs().ToString();
        Assert.AreEqual(str.Length, bigintA.GetDigitCount());
      }
    }
    [Test]
    public void TestGetSignedBit() {
      Assert.IsFalse(EInteger.Zero.GetSignedBit(0));
      Assert.IsFalse(EInteger.Zero.GetSignedBit(1));
      Assert.IsTrue(EInteger.One.GetSignedBit(0));
      Assert.IsFalse(EInteger.One.GetSignedBit(1));
      for (int i = 0; i < 32; ++i) {
        Assert.IsTrue(BigValueOf(-1).GetSignedBit(i));
      }
    }

    [Test]
    public void TestGetSignedBitLength() {
      for (var i = 0; i < LowBits.Length; i += 2) {
        Assert.AreEqual(
          (int)BitLengths[i + 1],
          BigValueOf(BitLengths[i]).GetSignedBitLength(),
          TestCommon.LongToString(BitLengths[i]));
      }
    }

    [Test]
    public void TestGetUnsignedBit() {
      for (var i = 0; i < LowBits.Length; i += 2) {
        var lowbit = (int)LowBits[i + 1];
        EInteger posint = BigValueOf(LowBits[i]);
        EInteger negint = BigValueOf(-LowBits[i]);
        for (var j = 0; j < lowbit; ++j) {
          Assert.IsFalse(posint.GetUnsignedBit(j));
          Assert.IsFalse(negint.GetUnsignedBit(j));
        }
        if (lowbit >= 0) {
          Assert.IsTrue(posint.GetUnsignedBit(lowbit));
          Assert.IsTrue(negint.GetUnsignedBit(lowbit));
        }
      }
    }

    [Test]
    public void TestGetUnsignedBitLength() {
      for (var i = 0; i < LowBits.Length; i += 2) {
        if (BitLengths[i] < 0) {
          continue;
        }
        Assert.AreEqual(
          (int)BitLengths[i + 1],
          BigValueOf(BitLengths[i]).GetUnsignedBitLength(),
          TestCommon.LongToString(BitLengths[i]));
        Assert.AreEqual(
          (int)BitLengths[i + 1],
          BigValueOf(-BitLengths[i]).GetUnsignedBitLength(),
          TestCommon.LongToString(-BitLengths[i]));
      }
    }

    [Test]
    public void TestGetLowBit() {
      for (var i = 0; i < LowBits.Length; i += 2) {
        Assert.AreEqual(
          (int)LowBits[i + 1],
          BigValueOf(LowBits[i]).GetLowBit());
        Assert.AreEqual(
          (int)LowBits[i + 1],
          BigValueOf(-LowBits[i]).GetLowBit());
      }
    }

    [Test]
    public void TestIntValueUnchecked() {
      Assert.AreEqual(0L, EInteger.Zero.AsInt32Unchecked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).AsInt32Unchecked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).AsInt32Unchecked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MinValue - 1L).AsInt32Unchecked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MaxValue + 1L).AsInt32Unchecked());
    }

    [Test]
    public void TestIsEven() {
      var r = new FastRandom();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        EInteger mod = bigintA.Remainder(BigValueOf(2));
        Assert.AreEqual(mod.IsZero, bigintA.IsEven);
      }
    }
    [Test]
    public void TestIsPowerOfTwo() {
      Assert.IsTrue(BigValueOf(1).IsPowerOfTwo);
      Assert.IsTrue(BigValueOf(2).IsPowerOfTwo);
      Assert.IsTrue(BigValueOf(4).IsPowerOfTwo);
      Assert.IsTrue(BigValueOf(8).IsPowerOfTwo);
      Assert.IsTrue(BigValueOf(16).IsPowerOfTwo);
      Assert.IsTrue(BigValueOf(32).IsPowerOfTwo);
      Assert.IsTrue(BigValueOf(64).IsPowerOfTwo);
      Assert.IsFalse(BigValueOf(65535).IsPowerOfTwo);
      Assert.IsTrue(BigValueOf(65536).IsPowerOfTwo);
      Assert.IsFalse(BigValueOf(65537).IsPowerOfTwo);
      Assert.IsTrue(BigValueOf(0x100000).IsPowerOfTwo);
      Assert.IsTrue(BigValueOf(0x10000000).IsPowerOfTwo);
      Assert.IsFalse(BigValueOf(0).IsPowerOfTwo);
      Assert.IsFalse(BigValueOf(-1).IsPowerOfTwo);
      Assert.IsFalse(BigValueOf(-2).IsPowerOfTwo);
      Assert.IsFalse(BigValueOf(-3).IsPowerOfTwo);
      Assert.IsFalse(BigValueOf(-4).IsPowerOfTwo);
      Assert.IsFalse(BigValueOf(-5).IsPowerOfTwo);
      Assert.IsFalse(BigValueOf(-65536).IsPowerOfTwo);
    }
    [Test]
    public void TestIsZero() {
      // not implemented yet
    }
    [Test]
    public void TestLongValueUnchecked() {
      Assert.AreEqual(0L, EInteger.Zero.AsInt64Unchecked());
      Assert.AreEqual(
        Int64.MinValue,
        BigValueOf(Int64.MinValue).AsInt64Unchecked());
      Assert.AreEqual(
        Int64.MaxValue,
        BigValueOf(Int64.MaxValue).AsInt64Unchecked());
      {
        object objectTemp = Int64.MaxValue;
        object objectTemp2 = BigValueOf(Int64.MinValue)
                .Subtract(EInteger.One).AsInt64Unchecked();
        Assert.AreEqual(objectTemp, objectTemp2);
      }
      Assert.AreEqual(
        Int64.MinValue,
        BigValueOf(Int64.MaxValue).Add(EInteger.One).AsInt64Unchecked());
      long aa = unchecked((long)0xFFFFFFF200000000L);
      Assert.AreEqual(
              aa,
              BigValueOf(aa).AsInt64Unchecked());
      aa = unchecked((long)0xFFFFFFF280000000L);
      Assert.AreEqual(
              aa,
              BigValueOf(aa).AsInt64Unchecked());
      aa = unchecked((long)0xFFFFFFF200000001L);
      Assert.AreEqual(
              aa,
              BigValueOf(aa).AsInt64Unchecked());
      aa = unchecked((long)0xFFFFFFF27FFFFFFFL);
      Assert.AreEqual(
              aa,
              BigValueOf(aa).AsInt64Unchecked());
      Assert.AreEqual(
        0x0000000380000001L,
        BigValueOf(0x0000000380000001L).AsInt64Unchecked());
      Assert.AreEqual(
        0x0000000382222222L,
        BigValueOf(0x0000000382222222L).AsInt64Unchecked());
      Assert.AreEqual(-8L, BigValueOf(-8L).AsInt64Unchecked());
      Assert.AreEqual(
        -32768L,
        BigValueOf(-32768L).AsInt64Unchecked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).AsInt64Unchecked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).AsInt64Unchecked());
      Assert.AreEqual(
        0x80000000L,
        BigValueOf(0x80000000L).AsInt64Unchecked());
      Assert.AreEqual(
        0x90000000L,
        BigValueOf(0x90000000L).AsInt64Unchecked());
    }

    [Test]
    public void TestMiscellaneous() {
      Assert.AreEqual(1, EInteger.Zero.GetDigitCount());
      var minValue = (EInteger)Int32.MinValue;
      EInteger minValueTimes2 = minValue + (EInteger)minValue;
      Assert.AreEqual(Int32.MinValue, (int)minValue);
      try {
        Console.WriteLine((int)minValueTimes2);
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      EInteger verybig = EInteger.One << 80;
      try {
        Console.WriteLine((int)verybig);
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        Console.WriteLine((long)verybig);
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.PowBigIntVar(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Pow(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        (EInteger.Zero - EInteger.One).PowBigIntVar(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.IsFalse(EInteger.One.Equals(EInteger.Zero));
      Assert.IsFalse(verybig.Equals(EInteger.Zero));
      Assert.IsFalse(EInteger.One.Equals(EInteger.Zero - EInteger.One));
      Assert.AreEqual(1, EInteger.One.CompareTo(null));
      EInteger[] tmpsqrt = EInteger.Zero.SqrtRem();
      Assert.AreEqual(EInteger.Zero, tmpsqrt[0]);
    }

    [Test]
    public void TestMod() {
      try {
        EInteger.One.Mod(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ((EInteger)13).Mod(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ((EInteger)13).Mod((EInteger)(-4));
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ((EInteger)(-13)).Mod((EInteger)(-4));
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestMultiply() {
      try {
        EInteger.One.Multiply(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var r = new FastRandom();
      for (var i = 0; i < 10000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        EInteger bigintB = bigintA + EInteger.One;
        EInteger bigintC = bigintA * (EInteger)bigintB;
        // Test near-squaring
        if (bigintA.IsZero || bigintB.IsZero) {
          Assert.AreEqual(EInteger.Zero, bigintC);
        }
        if (bigintA.Equals(EInteger.One)) {
          Assert.AreEqual(bigintB, bigintC);
        }
        if (bigintB.Equals(EInteger.One)) {
          Assert.AreEqual(bigintA, bigintC);
        }
        bigintB = bigintA;
        // Test squaring
        bigintC = bigintA * (EInteger)bigintB;
        if (bigintA.IsZero || bigintB.IsZero) {
          Assert.AreEqual(EInteger.Zero, bigintC);
        }
        if (bigintA.Equals(EInteger.One)) {
          Assert.AreEqual(bigintB, bigintC);
        }
        if (bigintB.Equals(EInteger.One)) {
          Assert.AreEqual(bigintA, bigintC);
        }
      }
      DoTestMultiply(
"39258416159456516340113264558732499166970244380745050",
"39258416159456516340113264558732499166970244380745051",
"1541223239349076530208308657654362309553698742116222355477449713742236585667505604058123112521437480247550");
      DoTestMultiply(
  "5786426269322750882632312999752639738983363095641642905722171221986067189342123124290107105663618428969517616421742429671402859775667602123564",
  "331378991485809774307751183645559883724387697397707434271522313077548174328632968616330900320595966360728317363190772921",
  "1917500101435169880779183578665955372346028226046021044867189027856189131730889958057717187493786883422516390996639766012958050987359732634213213442579444095928862861132583117668061032227577386757036981448703231972963300147061503108512300577364845823910107210444");
    }

    [Test]
    public void TestMultiplyDivide() {
      var r = new FastRandom();
      for (var i = 0; i < 10000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        EInteger bigintB = RandomBigInteger(r);
        this.TestMultiplyDivideOne(bigintA, bigintB);
        this.TestMultiplyDivideOne(bigintB, bigintA);
      }
    }

    [Test]
    public void TestNegate() {
      // not implemented yet
    }
    [Test]
    public void TestNot() {
      // not implemented yet
    }
    [Test]
    public void TestOne() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorAddition() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorDivision() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorExplicit() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorGreaterThan() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorGreaterThanOrEqual() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorImplicit() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorLeftShift() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorLessThan() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorLessThanOrEqual() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorModulus() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorMultiply() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorRightShift() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorSubtraction() {
      // not implemented yet
    }
    [Test]
    public void TestOperatorUnaryNegation() {
      // not implemented yet
    }
    [Test]
    public void TestOr() {
      // not implemented yet
    }

    [Test]
    public void TestPow() {
      var r = new FastRandom();
      for (var i = 0; i < 200; ++i) {
        int power = 1 + r.NextValue(8);
        EInteger bigintA = RandomBigInteger(r);
        EInteger bigintB = bigintA;
        for (int j = 1; j < power; ++j) {
          bigintB *= bigintA;
        }
        DoTestPow(bigintA.ToString(), power, bigintB.ToString());
      }
    }
    [Test]
    public void TestPowBigIntVar() {
      // not implemented yet
    }
    [Test]
    public void TestRemainder() {
      DoTestRemainder("2472320648", "2831812081", "2472320648");
      DoTestRemainder("-2472320648", "2831812081", "-2472320648");
    }
    [Test]
    public void TestShiftLeft() {
      EInteger bigint = EInteger.One;
      bigint <<= 100;
      TestCommon.CompareTestEqualAndConsistent(bigint << 12, bigint >> -12);
      TestCommon.CompareTestEqualAndConsistent(bigint << -12, bigint >> 12);
      var r = new FastRandom();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        EInteger bigintB = bigintA;
        for (int j = 0; j < 100; ++j) {
          EInteger ba = bigintA;
          ba <<= j;
          TestCommon.CompareTestEqualAndConsistent(bigintB, ba);
          int negj = -j;
          ba = bigintA;
          ba >>= negj;
          TestCommon.CompareTestEqualAndConsistent(bigintB, ba);
          bigintB *= (EInteger)2;
        }
      }
    }
    [Test]
    public void TestShiftRight() {
      EInteger bigint = EInteger.One;
      bigint <<= 80;
      TestCommon.CompareTestEqualAndConsistent(bigint << 12, bigint >> -12);
      TestCommon.CompareTestEqualAndConsistent(bigint << -12, bigint >> 12);
      var r = new FastRandom();
      EInteger minusone = EInteger.Zero;
      minusone -= EInteger.One;
      for (var i = 0; i < 1000; ++i) {
        int smallint = r.NextValue(0x7fffffff);
        var bigintA = (EInteger)smallint;
        string str = bigintA.ToString();
        for (int j = 32; j < 80; ++j) {
          DoTestShiftRight2(str, j, EInteger.Zero);
          DoTestShiftRight2("-" + str, j, minusone);
        }
      }
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        bigintA = bigintA.Abs();
        EInteger bigintB = bigintA;
        for (int j = 0; j < 100; ++j) {
          EInteger ba = bigintA;
          ba >>= j;
          TestCommon.CompareTestEqualAndConsistent(bigintB, ba);
          int negj = -j;
          ba = bigintA;
          ba <<= negj;
          TestCommon.CompareTestEqualAndConsistent(bigintB, ba);
          bigintB /= (EInteger)2;
        }
      }
    }
    [Test]
    public void TestSign() {
      // not implemented yet
    }
    [Test]
    public void TestSignedBitLength() {
      Assert.AreEqual(31, BigValueOf(-2147483647L).GetSignedBitLength());
      Assert.AreEqual(31, BigValueOf(-2147483648L).GetSignedBitLength());
      Assert.AreEqual(32, BigValueOf(-2147483649L).GetSignedBitLength());
      Assert.AreEqual(32, BigValueOf(-2147483650L).GetSignedBitLength());
      Assert.AreEqual(31, BigValueOf(2147483647L).GetSignedBitLength());
      Assert.AreEqual(32, BigValueOf(2147483648L).GetSignedBitLength());
      Assert.AreEqual(32, BigValueOf(2147483649L).GetSignedBitLength());
      Assert.AreEqual(32, BigValueOf(2147483650L).GetSignedBitLength());
      Assert.AreEqual(0, BigValueOf(0).GetSignedBitLength());
      Assert.AreEqual(1, BigValueOf(1).GetSignedBitLength());
      Assert.AreEqual(2, BigValueOf(2).GetSignedBitLength());
      Assert.AreEqual(2, BigValueOf(2).GetSignedBitLength());
      Assert.AreEqual(31, BigValueOf(Int32.MaxValue).GetSignedBitLength());
      Assert.AreEqual(31, BigValueOf(Int32.MinValue).GetSignedBitLength());
      Assert.AreEqual(16, BigValueOf(65535).GetSignedBitLength());
      Assert.AreEqual(16, BigValueOf(-65535).GetSignedBitLength());
      Assert.AreEqual(17, BigValueOf(65536).GetSignedBitLength());
      Assert.AreEqual(16, BigValueOf(-65536).GetSignedBitLength());
      Assert.AreEqual(
        65,
        BigFromString("19084941898444092059").GetSignedBitLength());
      Assert.AreEqual(
        65,
        BigFromString("-19084941898444092059").GetSignedBitLength());
      Assert.AreEqual(0, BigValueOf(-1).GetSignedBitLength());
      Assert.AreEqual(1, BigValueOf(-2).GetSignedBitLength());
    }

    [Test]
    public void TestSqrt() {
      var r = new FastRandom();
      for (var i = 0; i < 10000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        if (bigintA.Sign < 0) {
          bigintA = -bigintA;
        }
        if (bigintA.Sign == 0) {
          bigintA = EInteger.One;
        }
        EInteger sr = bigintA.Sqrt();
        EInteger srsqr = sr * (EInteger)sr;
        sr += EInteger.One;
        EInteger sronesqr = sr * (EInteger)sr;
        if (srsqr.CompareTo(bigintA) > 0) {
          Assert.Fail(srsqr + " not " + bigintA +
            " or less (TestSqrt, sqrt=" + sr + ")");
        }
        if (sronesqr.CompareTo(bigintA) <= 0) {
          Assert.Fail(srsqr + " not greater than " + bigintA +
            " (TestSqrt, sqrt=" + sr + ")");
        }
      }
    }
    [Test]
    public void TestSqrtWithRemainder() {
      // not implemented yet
    }
    [Test]
    public void TestSubtract() {
      // not implemented yet
    }
    [Test]
    public void TestToByteArray() {
      // not implemented yet
    }

    [Test]
    public void TestToRadixString() {
      var fr = new FastRandom();
      try {
        EInteger.One.ToRadixString(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(37);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(Int32.MinValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(Int32.MaxValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      for (int i = 2; i <= 36; ++i) {
        for (int j = 0; j < 100; ++j) {
          StringAndBigInt sabi = StringAndBigInt.Generate(fr, i);
          // Upper case result expected
          string expected = ToUpperCaseAscii(sabi.StringValue);
          var k = 0;
          // Expects result with no unnecessary leading zeros
          bool negative = sabi.BigIntValue.Sign < 0;
          if (expected[0] == '-') {
            ++k;
          }
          while (k < expected.Length - 1) {
            if (expected[k] == '0') {
              ++k;
            } else {
              break;
            }
          }
          expected = expected.Substring(k);
          if (negative) {
            expected = "-" + expected;
          }
          Assert.AreEqual(
            expected,
            sabi.BigIntValue.ToRadixString(i));
        }
      }
      var r = new FastRandom();
      for (var radix = 2; radix < 36; ++radix) {
        for (var i = 0; i < 80; ++i) {
          EInteger bigintA = RandomBigInteger(r);
          String s = bigintA.ToRadixString(radix);
          EInteger big2 = EInteger.FromRadixString(s, radix);
          Assert.AreEqual(big2.ToRadixString(radix), s);
        }
      }
    }

    [Test]
    public void TestToString() {
      var bi = (EInteger)3;
      AssertBigIntegersEqual("3", bi);
      var negseven = (EInteger)(-7);
      AssertBigIntegersEqual("-7", negseven);
      var other = (EInteger)(-898989);
      AssertBigIntegersEqual("-898989", other);
      other = (EInteger)898989;
      AssertBigIntegersEqual("898989", other);
      var r = new FastRandom();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        String s = bigintA.ToString();
        EInteger big2 = BigFromString(s);
        Assert.AreEqual(big2.ToString(), s);
      }
    }
    [Test]
    public void TestValueOf() {
      // not implemented yet
    }
    [Test]
    public void TestXor() {
      // not implemented yet
    }
    [Test]
    public void TestZero() {
      // not implemented yet
      {
        string stringTemp = EInteger.Zero.ToString();
        Assert.AreEqual(
          "0",
          stringTemp);
      }
    }

    internal static EInteger BigFromBytes(byte[] bytes) {
      return EInteger.FromBytes(bytes, true);
    }

    internal static EInteger BigFromString(string str) {
      return EInteger.FromString(str);
    }
    internal static EInteger BigValueOf(long value) {
      return EInteger.FromInt64(value);
    }

    private static void TestGcdPair(
      EInteger biga,
      EInteger bigb,
      EInteger biggcd) {
      EInteger ba = EInteger.GreatestCommonDivisor(biga, bigb);
      EInteger bb = EInteger.GreatestCommonDivisor(bigb, biga);
      Assert.AreEqual(ba, biggcd);
      Assert.AreEqual(bb, biggcd);
    }

    private static string ToUpperCaseAscii(string str) {
      if (str == null) {
        return null;
      }
      int len = str.Length;
      var c = (char)0;
      var hasLowerCase = false;
      for (var i = 0; i < len; ++i) {
        c = str[i];
        if (c >= 'a' && c <= 'z') {
          hasLowerCase = true;
          break;
        }
      }
      if (!hasLowerCase) {
        return str;
      }
      var builder = new StringBuilder();
      for (var i = 0; i < len; ++i) {
        c = str[i];
        if (c >= 'a' && c <= 'z') {
          builder.Append((char)(c - 0x20));
        } else {
          builder.Append(c);
        }
      }
      return builder.ToString();
    }

    private void TestMultiplyDivideOne(EInteger bigintA, EInteger bigintB) {
      // Test that A*B/A = B and A*B/B = A
      EInteger bigintC = bigintA * (EInteger)bigintB;
      EInteger bigintRem;
      EInteger bigintE;
      EInteger bigintD;
      if (!bigintB.IsZero) {
        {
          EInteger[] divrem = (bigintC).DivRem(bigintB);
          bigintD = divrem[0];
          bigintRem = divrem[1];
        }
        if (!bigintD.Equals(bigintA)) {
          Assert.AreEqual(
bigintA,
bigintD,
"TestMultiplyDivide " + bigintA + "; " + bigintB + ";\n" + bigintC);
        }
        if (!bigintRem.IsZero) {
          Assert.AreEqual(
EInteger.Zero,
bigintRem,
"TestMultiplyDivide " + bigintA + "; " + bigintB);
        }
        bigintE = bigintC / (EInteger)bigintB;
        if (!bigintD.Equals(bigintE)) {
          // Testing that divideWithRemainder and division method return
          // the same value
          Assert.AreEqual(
bigintD,
bigintE,
"TestMultiplyDivide " + bigintA + "; " + bigintB + ";\n" + bigintC);
        }
        bigintE = bigintC % (EInteger)bigintB;
        if (!bigintRem.Equals(bigintE)) {
          Assert.AreEqual(
bigintRem,
bigintE,
"TestMultiplyDivide " + bigintA + "; " + bigintB + ";\n" + bigintC);
        }
        if (bigintE.Sign > 0 && !bigintC.Mod(bigintB).Equals(bigintE)) {
          Assert.Fail("TestMultiplyDivide " + bigintA + "; " + bigintB +
            ";\n" + bigintC);
        }
      }
      if (!bigintA.IsZero) {
        {
          EInteger[] divrem = (bigintC).DivRem(bigintA);
          bigintD = divrem[0];
          bigintRem = divrem[1];
        }
        if (!bigintD.Equals(bigintB)) {
          Assert.AreEqual(
bigintB,
bigintD,
"TestMultiplyDivide " + bigintA + "; " + bigintB);
        }
        if (!bigintRem.IsZero) {
          Assert.AreEqual(
EInteger.Zero,
bigintRem,
"TestMultiplyDivide " + bigintA + "; " + bigintB);
        }
      }
      if (!bigintB.IsZero) {
        {
          EInteger[] divrem = (bigintA).DivRem(bigintB);
          bigintC = divrem[0];
          bigintRem = divrem[1];
        }
        bigintD = bigintB * (EInteger)bigintC;
        bigintD += (EInteger)bigintRem;
        if (!bigintD.Equals(bigintA)) {
          Assert.AreEqual(
bigintA,
bigintD,
"TestMultiplyDivide " + bigintA + "; " + bigintB);
        }
      }
    }
  }
}
