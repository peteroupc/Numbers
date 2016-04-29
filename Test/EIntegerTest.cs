using System;
using System.Text;
using NUnit.Framework;
using PeterO;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class EIntegerTest {
    private static long[] valueBitLengths = { -4294967297L, 33L, -4294967296L,
      32L,
    -4294967295L, 32L, -2147483649L, 32L, -2147483648L, 31L, -2147483647L,
        31L,
      -1073741825L, 31L, -1073741824L, 30L, -1073741823L, 30L, -536870913L, 30L,
      -536870912L, 29L, -536870911L, 29L, -268435457L, 29L, -268435456L, 28L,
      -268435455L, 28L, -134217729L, 28L, -134217728L, 27L, -134217727L, 27L,
  -67108865L, 27L, -67108864L, 26L, -67108863L, 26L, -33554433L, 26L,
        -33554432L,
      25L, -33554431L, 25L, -16777217L, 25L, -16777216L, 24L, -16777215L, 24L,
  -8388609L, 24L, -8388608L, 23L, -8388607L, 23L, -4194305L, 23L, -4194304L,
        22L,
  -4194303L, 22L, -2097153L, 22L, -2097152L, 21L, -2097151L, 21L, -1048577L,
        21L,
   -1048576L, 20L, -1048575L, 20L, -524289L, 20L, -524288L, 19L, -524287L,
        19L,
      -262145L, 19L, -262144L, 18L, -262143L, 18L, -131073L, 18L, -131072L, 17L,
  -131071L, 17L, -65537L, 17L, -65536L, 16L, -65535L, 16L, -32769L, 16L,
        -32768L,
      15L, -32767L, 15L, -16385L, 15L, -16384L, 14L, -16383L, 14L, -8193L,
        14L, -8192L,
      13L, -8191L, 13L, -4097L, 13L, -4096L, 12L, -4095L, 12L, -2049L, 12L,
        -2048L, 11L,
      -2047L, 11L, -1025L, 11L, -1024L, 10L, -1023L, 10L, -513L, 10L, -512L,
        9L, -511L,
      9L, -257L, 9L, -256L, 8L, -255L, 8L, -129L, 8L, -128L, 7L, -127L, 7L,
        -65L, 7L, -64L,
      6L, -63L, 6L, -33L, 6L, -32L, 5L, -31L, 5L, -17L, 5L, -16L, 4L, -15L,
        4L, -9L, 4L, -8L,
      3L, -7L, 3L, -5L, 3L, -4L, 2L, -3L, 2L, -2L, 1L, -1L, 0L, 0L, 0L, 1L,
        1L, 2L, 2L, 3L, 2L,
      4L, 3L, 5L, 3L, 7L, 3L, 8L, 4L, 9L, 4L, 15L, 4L, 16L, 5L, 17L, 5L,
        31L, 5L, 32L, 6L, 33L,
      6L, 63L, 6L, 64L, 7L, 65L, 7L, 127L, 7L, 128L, 8L, 129L, 8L, 255L, 8L,
        256L, 9L, 257L,
      9L, 511L, 9L, 512L, 10L, 513L, 10L, 1023L, 10L, 1024L, 11L, 1025L,
        11L, 2047L, 11L,
      2048L, 12L, 2049L, 12L, 4095L, 12L, 4096L, 13L, 4097L, 13L, 8191L,
        13L, 8192L, 14L,
   8193L, 14L, 16383L, 14L, 16384L, 15L, 16385L, 15L, 32767L, 15L, 32768L,
        16L,
  32769L, 16L, 65535L, 16L, 65536L, 17L, 65537L, 17L, 131071L, 17L, 131072L,
        18L,
 131073L, 18L, 262143L, 18L, 262144L, 19L, 262145L, 19L, 524287L, 19L,
        524288L,
 20L, 524289L, 20L, 1048575L, 20L, 1048576L, 21L, 1048577L, 21L, 2097151L,
        21L,
      2097152L, 22L, 2097153L, 22L, 4194303L, 22L, 4194304L, 23L, 4194305L, 23L,
   8388607L, 23L, 8388608L, 24L, 8388609L, 24L, 16777215L, 24L, 16777216L,
        25L,
  16777217L, 25L, 33554431L, 25L, 33554432L, 26L, 33554433L, 26L, 67108863L,
        26L,
  67108864L, 27L, 67108865L, 27L, 134217727L, 27L, 134217728L, 28L,
        134217729L,
      28L, 268435455L, 28L, 268435456L, 29L, 268435457L, 29L, 536870911L, 29L,
      536870912L, 30L, 536870913L, 30L, 1073741823L, 30L, 1073741824L, 31L,
      1073741825L, 31L, 2147483647L, 31L, 2147483648L, 32L, 2147483649L, 32L,
      4294967295L, 32L, 4294967296L, 33L, 4294967297L, 33 };

    private static long[] valueLowBits = { 0L, -1L, 1L, 0L, 2L, 1L, 3L, 0L, 4L,
      2L, 5L, 0L,
      7L, 0L, 8L, 3L, 9L, 0L, 15L, 0L, 16L, 4L, 17L, 0L, 31L, 0L, 32L, 5L,
        33L, 0L, 63L, 0L, 64L,
      6L, 65L, 0L, 127L, 0L, 128L, 7L, 129L, 0L, 255L, 0L, 256L, 8L, 257L,
        0L, 511L, 0L, 512L,
      9L, 513L, 0L, 1023L, 0L, 1024L, 10L, 1025L, 0L, 2047L, 0L, 2048L, 11L,
        2049L, 0L,
  4095L, 0L, 4096L, 12L, 4097L, 0L, 8191L, 0L, 8192L, 13L, 8193L, 0L, 16383L,
        0L,
      16384L, 14L, 16385L, 0L, 32767L, 0L, 32768L, 15L, 32769L, 0L, 65535L,
        0L, 65536L,
      16L, 65537L, 0L, 131071L, 0L, 131072L, 17L, 131073L, 0L, 262143L, 0L,
        262144L, 18L,
      262145L, 0L, 524287L, 0L, 524288L, 19L, 524289L, 0L, 1048575L, 0L,
        1048576L, 20L,
      1048577L, 0L, 2097151L, 0L, 2097152L, 21L, 2097153L, 0L, 4194303L, 0L,
        4194304L,
   22L, 4194305L, 0L, 8388607L, 0L, 8388608L, 23L, 8388609L, 0L, 16777215L,
        0L,
   16777216L, 24L, 16777217L, 0L, 33554431L, 0L, 33554432L, 25L, 33554433L,
        0L,
 67108863L, 0L, 67108864L, 26L, 67108865L, 0L, 134217727L, 0L, 134217728L,
        27L,
      134217729L, 0L, 268435455L, 0L, 268435456L, 28L, 268435457L, 0L,
        536870911L, 0L,
      536870912L, 29L, 536870913L, 0L, 1073741823L, 0L, 1073741824L, 30L,
      1073741825L, 0L, 2147483647L, 0L, 2147483648L, 31L, 2147483649L, 0L,
      4294967295L, 0L, 4294967296L, 32L, 4294967297L, 0 };

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
          new Object();
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
            EInteger[] divrem = bigintA.DivRem(bigintB);
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
          EInteger[] divrem = bigintA.DivRem(bigintB);
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
      EInteger bigintC = bigintA.Multiply(bigintB);
      if (result != null) {
        AssertBigIntegersEqual(result, bigintC);
      }
      TestMultiplyDivideOne(bigintA, bigintB);
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
          new Object();
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

    public static EInteger RandomBigInteger(RandomGenerator r) {
      int selection = r.UniformInt(100);
      int count = r.UniformInt(60) + 1;
      if (selection < 40) {
        count = r.UniformInt(7) + 1;
      }
      if (selection < 50) {
        count = r.UniformInt(15) + 1;
      }
      var bytes = new byte[count];
      for (var i = 0; i < count; ++i) {
        bytes[i] = (byte)((int)r.UniformInt(256));
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
      var r = new RandomGenerator();
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
        BigValueOf(Int32.MinValue).ToInt32Checked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).ToInt32Checked());
      try {
        BigValueOf(Int32.MinValue - 1L).ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigValueOf(Int32.MaxValue + 1L).ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString("999999999999999999999999999999999").ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).ToInt32Checked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).ToInt32Checked());
      try {
        BigValueOf(Int32.MinValue - 1L).ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigValueOf(Int32.MaxValue + 1L).ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestAsInt64Checked() {
      Assert.AreEqual(
        Int64.MinValue,
        BigValueOf(Int64.MinValue).ToInt64Checked());
      Assert.AreEqual(
        Int64.MaxValue,
        BigValueOf(Int64.MaxValue).ToInt64Checked());
      try {
        EInteger bigintTemp = BigValueOf(Int64.MinValue);
        bigintTemp -= EInteger.One;
        bigintTemp.ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger bigintTemp = BigValueOf(Int64.MaxValue);
        bigintTemp += EInteger.One;
        bigintTemp.ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(
        unchecked((long)0xfffffff200000000L),
  BigValueOf(unchecked((long)0xfffffff200000000L)).ToInt64Checked());
      Assert.AreEqual(
        unchecked((long)0xfffffff280000000L),
  BigValueOf(unchecked((long)0xfffffff280000000L)).ToInt64Checked());
      Assert.AreEqual(
        unchecked((long)0xfffffff280000001L),
  BigValueOf(unchecked((long)0xfffffff280000001L)).ToInt64Checked());
      Assert.AreEqual(
        unchecked((long)0xfffffff27fffffffL),
  BigValueOf(unchecked((long)0xfffffff27fffffffL)).ToInt64Checked());
      Assert.AreEqual(
        0x0000000380000001L,
        BigValueOf(0x0000000380000001L).ToInt64Checked());
      Assert.AreEqual(
        0x0000000382222222L,
        BigValueOf(0x0000000382222222L).ToInt64Checked());
      Assert.AreEqual(-8L, BigValueOf(-8L).ToInt64Checked());
      Assert.AreEqual(-32768L, BigValueOf(-32768L).ToInt64Checked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).ToInt64Checked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).ToInt64Checked());
      Assert.AreEqual(
        0x80000000L,
        BigValueOf(0x80000000L).ToInt64Checked());
      Assert.AreEqual(
        0x90000000L,
        BigValueOf(0x90000000L).ToInt64Checked());
      try {
        BigFromString("999999999999999999999999999999999").ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(
        Int64.MinValue,
        BigValueOf(Int64.MinValue).ToInt64Checked());
      Assert.AreEqual(
        Int64.MaxValue,
        BigValueOf(Int64.MaxValue).ToInt64Checked());
      try {
        EInteger bigintTemp = BigValueOf(Int64.MinValue);
        bigintTemp -= EInteger.One;
        bigintTemp.ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger bigintTemp = BigValueOf(Int64.MaxValue);
        bigintTemp += EInteger.One;
        bigintTemp.ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      long longV = unchecked((long)0xfffffff200000000L);
      Assert.AreEqual(
  longV,
  BigValueOf(longV).ToInt64Checked());
      longV = unchecked((long)0xfffffff280000000L);
      Assert.AreEqual(
  longV,
  BigValueOf(longV).ToInt64Checked());
      longV = unchecked((long)0xfffffff280000001L);
      Assert.AreEqual(
  longV,
  BigValueOf(longV).ToInt64Checked());
      longV = unchecked((long)0xfffffff27fffffffL);
      Assert.AreEqual(
  longV,
  BigValueOf(longV).ToInt64Checked());
      Assert.AreEqual(
        0x0000000380000001L,
        BigValueOf(0x0000000380000001L).ToInt64Checked());
      Assert.AreEqual(
        0x0000000382222222L,
        BigValueOf(0x0000000382222222L).ToInt64Checked());
      Assert.AreEqual(-8L, BigValueOf(-8L).ToInt64Checked());
      Assert.AreEqual(-32768L, BigValueOf(-32768L).ToInt64Checked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).ToInt64Checked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).ToInt64Checked());
      Assert.AreEqual(
        0x80000000L,
        BigValueOf(0x80000000L).ToInt64Checked());
      Assert.AreEqual(
        0x90000000L,
        BigValueOf(0x90000000L).ToInt64Checked());
    }
    [Test]
    public void TestBigIntegerModPow() {
      try {
        EInteger.One.ModPow(null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(null, EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(EInteger.Zero, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("-1"), BigFromString("1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("0"), BigFromString("0"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("0"), BigFromString("-1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("1"), BigFromString("0"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(BigFromString("1"), BigFromString("-1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestCanFitInInt() {
      var r = new RandomGenerator();
      for (var i = 0; i < 2000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        Assert.AreEqual(
              bigintA.CanFitInInt32(),
              bigintA.GetSignedBitLength() <= 31);
        Assert.AreEqual(
              bigintA.CanFitInInt64(),
              bigintA.GetSignedBitLength() <= 63);
      }
    }
    [Test]
    public void TestCompareTo() {
      var r = new RandomGenerator();
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
      DoTestDivide(
    "9999999999999999999999",
    "281474976710655",
    "35527136");
      DoTestDivide("2472320648", "2831812081", "0");
      DoTestDivide("-2472320648", "2831812081", "0");
      var fr = new RandomGenerator();
      for (var i = 0; i < 10000; ++i) {
        intA = fr.UniformInt(0x1000000);
        intB = fr.UniformInt(0x1000000);
        if (intB == 0) {
          continue;
        }
        int c = intA / intB;
        var bigintA = (EInteger)intA;
        var bigintB = (EInteger)intB;
        EInteger bigintC = bigintA / (EInteger)bigintB;
        Assert.AreEqual((int)bigintC, c);
      }
    }

    [Test]
    public void TestDivRem() {
      try {
        EInteger.One.DivRem(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.DivRem(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
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
      var r = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomObjects.RandomEInteger(r);
        EInteger bigintB = RandomObjects.RandomEInteger(r);
        TestCommon.AssertEqualsHashCode(bigintA, bigintB);
      }
    }

    [Test]
    public void TestExceptions() {
      try {
        BigFromString(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EInteger.Zero.GetSignedBit(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString("x11");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString(".");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString("..");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString("e200");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EInteger.One.Mod((EInteger)(-1));
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Add(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Subtract(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Divide(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Divide(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Remainder(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Mod(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Remainder(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
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
        new Object();
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
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", -37);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", Int32.MinValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", Int32.MaxValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var fr = new RandomGenerator();
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
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", 1, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", 0, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", -37, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", Int32.MinValue, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", Int32.MaxValue, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, -1, 2);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 4, 5);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 0, -8);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 0, 6);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 2, 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 0, 0);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 1, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("-", 10, 0, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("g", 16, 0, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0123gggg", 16, 0, 8);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0123gggg", 10, 0, 8);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0123aaaa", 10, 0, 8);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var fr = new RandomGenerator();
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
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString(String.Empty);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        BigFromString(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
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
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EInteger.FromSubstring("123", -1, 2);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 4, 2);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 1, -1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 1, 4);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 1, 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 2, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
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
    [Timeout(10000)]
    public void TestGcdHang() {
      {
        string stringTemp = BigFromString("781631509928000000").Gcd(
                  BigFromString("1000000")).ToString();
        Assert.AreEqual(
          "1000000",
          stringTemp);
      }
    }

    [Test]
    public void TestGcd() {
      try {
        EInteger.Zero.Gcd(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
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
      var rand = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        while (true) {
          prime = rand.UniformInt(0x7fffffff);
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
          bigprime.Gcd(ba));
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
        prime = rand.UniformInt(0x7fffffff);
        if (rand.UniformInt(2) == 0) {
          prime = -prime;
        }
        int intB = rand.UniformInt(0x7fffffff);
        if (rand.UniformInt(2) == 0) {
          intB = -intB;
        }
        var biga = (EInteger)prime;
        var bigb = (EInteger)intB;
        EInteger ba = biga.Gcd(bigb);
        EInteger bb = bigb.Gcd(biga);
        Assert.AreEqual(ba, bb);
      }
    }

    [Test]
    public void TestGetBits() {
      // not implemented yet
    }
    [Test]
    public void TestGetDigitCount() {
      var r = new RandomGenerator();
      {
        object objectTemp = 39;
        object objectTemp2 = EInteger.FromString(
          "101754295360222878437145684059582837272").GetDigitCount();
        Assert.AreEqual(objectTemp, objectTemp2);
      }
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        String str = bigintA.Abs().ToString();
        Assert.AreEqual(str.Length, bigintA.GetDigitCount(), str);
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
      for (var i = 0; i < valueBitLengths.Length; i += 2) {
        Assert.AreEqual(
          (int)valueBitLengths[i + 1],
          BigValueOf(valueBitLengths[i]).GetSignedBitLength(),
          TestCommon.LongToString(valueBitLengths[i]));
      }
    }

    [Test]
    public void TestGetUnsignedBit() {
      for (var i = 0; i < valueLowBits.Length; i += 2) {
        var lowbit = (int)valueLowBits[i + 1];
        EInteger posint = BigValueOf(valueLowBits[i]);
        EInteger negint = BigValueOf(-valueLowBits[i]);
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
      for (var i = 0; i < valueBitLengths.Length; i += 2) {
        if (valueBitLengths[i] < 0) {
          continue;
        }
        Assert.AreEqual(
          (int)valueBitLengths[i + 1],
          BigValueOf(valueBitLengths[i]).GetUnsignedBitLength(),
          TestCommon.LongToString(valueBitLengths[i]));
        Assert.AreEqual(
          (int)valueBitLengths[i + 1],
          BigValueOf(-valueBitLengths[i]).GetUnsignedBitLength(),
          TestCommon.LongToString(-valueBitLengths[i]));
      }
    }

    [Test]
    public void TestGetLowBit() {
      for (var i = 0; i < valueLowBits.Length; i += 2) {
        Assert.AreEqual(
          (int)valueLowBits[i + 1],
          BigValueOf(valueLowBits[i]).GetLowBit());
        Assert.AreEqual(
          (int)valueLowBits[i + 1],
          BigValueOf(-valueLowBits[i]).GetLowBit());
      }
    }

    [Test]
    public void TestIntValueUnchecked() {
      Assert.AreEqual(0L, EInteger.Zero.ToInt32Unchecked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).ToInt32Unchecked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).ToInt32Unchecked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MinValue - 1L).ToInt32Unchecked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MaxValue + 1L).ToInt32Unchecked());
    }

    [Test]
    public void TestIsEven() {
      var r = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        EInteger mod = bigintA.Remainder(BigValueOf(2));
        Assert.AreEqual(mod.IsZero, bigintA.IsEven);
        if (bigintA.IsEven) {
          bigintA = bigintA.Add(EInteger.One);
          Assert.IsTrue(!bigintA.IsEven);
        } else {
          bigintA = bigintA.Add(EInteger.One);
          Assert.IsTrue(bigintA.IsEven);
        }
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
      Assert.AreEqual(0L, EInteger.Zero.ToInt64Unchecked());
      Assert.AreEqual(
        Int64.MinValue,
        BigValueOf(Int64.MinValue).ToInt64Unchecked());
      Assert.AreEqual(
        Int64.MaxValue,
        BigValueOf(Int64.MaxValue).ToInt64Unchecked());
      {
        object objectTemp = Int64.MaxValue;
        object objectTemp2 = BigValueOf(Int64.MinValue)
                .Subtract(EInteger.One).ToInt64Unchecked();
        Assert.AreEqual(objectTemp, objectTemp2);
      }
      Assert.AreEqual(
        Int64.MinValue,
        BigValueOf(Int64.MaxValue).Add(EInteger.One).ToInt64Unchecked());
      long aa = unchecked((long)0xfffffff200000000L);
      Assert.AreEqual(
              aa,
              BigValueOf(aa).ToInt64Unchecked());
      aa = unchecked((long)0xfffffff280000000L);
      Assert.AreEqual(
              aa,
              BigValueOf(aa).ToInt64Unchecked());
      aa = unchecked((long)0xfffffff200000001L);
      Assert.AreEqual(
              aa,
              BigValueOf(aa).ToInt64Unchecked());
      aa = unchecked((long)0xfffffff27fffffffL);
      Assert.AreEqual(
              aa,
              BigValueOf(aa).ToInt64Unchecked());
      Assert.AreEqual(
        0x0000000380000001L,
        BigValueOf(0x0000000380000001L).ToInt64Unchecked());
      Assert.AreEqual(
        0x0000000382222222L,
        BigValueOf(0x0000000382222222L).ToInt64Unchecked());
      Assert.AreEqual(-8L, BigValueOf(-8L).ToInt64Unchecked());
      Assert.AreEqual(
        -32768L,
        BigValueOf(-32768L).ToInt64Unchecked());
      Assert.AreEqual(
        Int32.MinValue,
        BigValueOf(Int32.MinValue).ToInt64Unchecked());
      Assert.AreEqual(
        Int32.MaxValue,
        BigValueOf(Int32.MaxValue).ToInt64Unchecked());
      Assert.AreEqual(
        0x80000000L,
        BigValueOf(0x80000000L).ToInt64Unchecked());
      Assert.AreEqual(
        0x90000000L,
        BigValueOf(0x90000000L).ToInt64Unchecked());
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
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      EInteger verybig = EInteger.One << 80;
      try {
        Console.WriteLine((int)verybig);
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        Console.WriteLine((long)verybig);
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.PowBigIntVar(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Pow(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        (EInteger.Zero - EInteger.One).PowBigIntVar(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
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
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ((EInteger)13).Mod(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ((EInteger)13).Mod((EInteger)(-4));
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ((EInteger)(-13)).Mod((EInteger)(-4));
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        new Object();
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
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var r = new RandomGenerator();
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

    private static EInteger WordAlignedInteger(RandomGenerator r) {
      int size = r.UniformInt(150) + 1;
      EInteger ei = EInteger.Zero;
      for (var i = 0; i < size; ++i) {
        ei = ei.ShiftLeft(16).Add(EInteger.FromInt32(r.UniformInt(0x10000) |
                 0x8000));
      }
      return ei;
    }

    private static EInteger FuzzInteger(
  EInteger ei,
  int fuzzes,
  RandomGenerator r) {
      byte[] bytes = ei.ToBytes(true);
      int bits = ei.GetUnsignedBitLength();
      for (var i = 0; i < fuzzes; ++i) {
        int bit = r.UniformInt(bits);
        bytes[bit / 8] ^= (byte)(1 << (bit & 0x07));
      }
      return EInteger.FromBytes(bytes, true);
    }

    private static EInteger AllOnesInteger(int size) {
      EInteger ei = EInteger.Zero;
      for (var i = 0; i < size; ++i) {
        ei = ei.ShiftLeft(16).Add(EInteger.FromInt32(0xffff));
      }
      return ei;
    }

    private static EInteger ZerosAndOnesInteger(int size) {
      EInteger ei = EInteger.FromInt32(0xffff);
      for (var i = 1; i < size; ++i) {
        ei = (i % 2 == 0) ? ei.ShiftLeft(16).Add(EInteger.FromInt32(0xffff)) :
          ei.ShiftLeft(16);
      }
      return ei;
    }

    [Test]
    public void TestMultiplyDivide() {
      var r = new RandomGenerator();
      for (var i = 1; i < 25; ++i) {
        for (var j = 1; j < i; ++j) {
          EInteger bigA, bigB;
          int highWord;
          bigA = AllOnesInteger(i);
          bigB = AllOnesInteger(j);
          TestMultiplyDivideOne(bigA, bigB);
          highWord = r.UniformInt(0x8000, 0x10000);
          bigA = EInteger.FromInt32(highWord).ShiftLeft(i * 16).Add(bigA);
          bigB = EInteger.FromInt32(highWord).ShiftLeft(j * 16).Add(bigB);
          TestMultiplyDivideOne(bigA, bigB);
          bigA = ZerosAndOnesInteger(i);
          bigB = ZerosAndOnesInteger(j);
          TestMultiplyDivideOne(bigA, bigB);
          highWord = r.UniformInt(0x8000, 0x10000);
          bigA = EInteger.FromInt32(highWord).ShiftLeft(i * 16).Add(bigA);
          bigB = EInteger.FromInt32(highWord).ShiftLeft(j * 16).Add(bigB);
          TestMultiplyDivideOne(bigA, bigB);
        }
      }
      EInteger startIntA = ZerosAndOnesInteger(100);
      EInteger startIntB = ZerosAndOnesInteger(50);
      for (var i = 1; i < 500; ++i) {
        EInteger bigA = FuzzInteger(startIntA, r.UniformInt(1, 100), r);
        EInteger bigB = FuzzInteger(startIntB, r.UniformInt(1, 50), r);
        TestMultiplyDivideOne(bigA, bigB);
      }
      {
        EInteger ei1 = EInteger.FromRadixString(
  "101000101010100000001000100000101000000000100000101000100010101000001010000000001010101000101010100010100",
  16);
        EInteger ei2 = EInteger.FromRadixString(
  "100000000000100010000000100000000010001000100010000010101010100000000000000000000",
  16);
        TestMultiplyDivideOne(ei1, ei2);
      }
      for (var i = 0; i < 1000; ++i) {
        EInteger bigA, bigB;
        do {
          bigA = WordAlignedInteger(r);
          bigB = WordAlignedInteger(r);
        } while (bigA.CompareTo(bigB) <= 0);
        TestMultiplyDivideOne(bigA, bigB);
      }
      for (var i = 0; i < 10000; ++i) {
        EInteger bigA = RandomObjects.RandomEInteger(r);
        EInteger bigB = RandomObjects.RandomEInteger(r);
        TestMultiplyDivideOne(bigA, bigB);
      }
      TestMultiplyDivideOne(EInteger.FromInt32(-985), EInteger.FromInt32(0));
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
      var r = new RandomGenerator();
      for (var i = 0; i < 200; ++i) {
        int power = 1 + r.UniformInt(8);
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
      var r = new RandomGenerator();
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
      var r = new RandomGenerator();
      EInteger minusone = EInteger.Zero;
      minusone -= EInteger.One;
      for (var i = 0; i < 1000; ++i) {
        int smallint = r.UniformInt(0x7fffffff);
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
      var r = new RandomGenerator();
      for (var i = 0; i < 20; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        if (bigintA.Sign < 0) {
          bigintA = -bigintA;
        }
        if (bigintA.Sign == 0) {
          bigintA = EInteger.One;
        }
        EInteger sqr = bigintA.Multiply(bigintA);
        EInteger sr = sqr.Sqrt();
        TestCommon.CompareTestEqual(bigintA, sr);
      }
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
      EInteger[] eintarr;
      eintarr = EInteger.Zero.SqrtRem();
      Assert.AreEqual(EInteger.Zero, eintarr[0]);
      Assert.AreEqual(EInteger.Zero, eintarr[1]);
      eintarr = EInteger.FromInt32(-1).SqrtRem();
      Assert.AreEqual(EInteger.Zero, eintarr[0]);
      Assert.AreEqual(EInteger.Zero, eintarr[1]);
      eintarr = EInteger.One.SqrtRem();
      Assert.AreEqual(EInteger.One, eintarr[0]);
      Assert.AreEqual(EInteger.Zero, eintarr[1]);
    }
    [Test]
    public void TestSubtract() {
      EInteger ei1 =
        EInteger.FromString(
  "5903310052234442839693218602919688229567185544510721229016780853271484375");
      EInteger ei2 = EInteger.FromString("710542735760100185871124267578125");
      {
        string stringTemp = ei1.Subtract(ei2).ToString();
        Assert.AreEqual(
  "5903310052234442839693218602919688229566475001774961128830909729003906250",
  stringTemp);
      }
    }
    [Test]
    public void TestToByteArray() {
      // not implemented yet
    }

    [Test]
    public void TestToRadixString() {
      var fr = new RandomGenerator();
      try {
        EInteger.One.ToRadixString(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(37);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(Int32.MinValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(Int32.MaxValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        new Object();
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
      var r = new RandomGenerator();
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
      var r = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        ExtraTest.TestStringEqualRoundTrip(bigintA);
      }
      // Test serialization of relatively big numbers
      for (var i = 0; i < 20; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        bigintA = bigintA.ShiftLeft(r.UniformInt(2000) + (16 * 500));
        bigintA = bigintA.Subtract(RandomBigInteger(r));
        ExtraTest.TestStringEqualRoundTrip(bigintA);
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
      EInteger ba = biga.Gcd(bigb);
      EInteger bb = bigb.Gcd(biga);
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

    public static void TestMultiplyDivideOne(
     EInteger bigintA,
     EInteger bigintB) {
      // Test that A*B/A = B and A*B/B = A
      EInteger bigintC = bigintA.Multiply(bigintB);
      EInteger bigintRem;
      EInteger bigintE;
      EInteger bigintD;
      TestCommon.CompareTestEqualAndConsistent(
        bigintC,
        bigintB.Multiply(bigintA));
      if (!bigintB.IsZero) {
        {
          EInteger[] divrem = bigintC.DivRem(bigintB);
          bigintD = divrem[0];
          bigintRem = divrem[1];
        }
        TestCommon.CompareTestEqualAndConsistent(bigintD, bigintA);
        TestCommon.CompareTestEqual(EInteger.Zero, bigintRem);
        bigintE = bigintC.Divide(bigintB);
        // Testing that DivRem and division method return
        // the same value
        TestCommon.CompareTestEqualAndConsistent(bigintD, bigintE);
        bigintE = bigintC.Remainder(bigintB);
        TestCommon.CompareTestEqualAndConsistent(bigintRem, bigintE);
        if (bigintE.Sign > 0 && !bigintC.Mod(bigintB).Equals(bigintE)) {
          TestCommon.CompareTestEqualAndConsistent(
            bigintE,
            bigintC.Mod(bigintB));
        }
      }
      if (!bigintA.IsZero) {
        EInteger[] divrem = bigintC.DivRem(bigintA);
        bigintD = divrem[0];
        bigintRem = divrem[1];
        TestCommon.CompareTestEqualAndConsistent(bigintD, bigintB);
        TestCommon.CompareTestEqual(EInteger.Zero, bigintRem);
      }
      if (!bigintB.IsZero) {
        EInteger[] divrem = bigintA.DivRem(bigintB);
        bigintC = divrem[0];
        bigintRem = divrem[1];
        bigintD = bigintB.Multiply(bigintC);
        bigintD += (EInteger)bigintRem;
        TestCommon.CompareTestEqualAndConsistent(bigintA, bigintD);
      }
      // -----------------------------------
      // EDecimal
      // -----------------------------------
      EDecimal edecA = EDecimal.FromEInteger(bigintA);
      EDecimal edecB = EDecimal.FromEInteger(bigintB);
      EDecimal edecC = edecA.Multiply(edecB);
      EDecimal edecRem;
      EDecimal edecE;
      EDecimal edecD;
      TestCommon.CompareTestEqualAndConsistent(
        edecC,
        edecB.Multiply(edecA));
      if (!edecB.IsZero) {
        EDecimal[] divrem = edecC.DivRemNaturalScale(edecB);
        edecD = divrem[0].Plus(null);
        edecRem = divrem[1];
        TestCommon.CompareTestEqualAndConsistent(edecD, edecA);
        TestCommon.CompareTestEqual(EDecimal.Zero, edecRem);
        edecE = edecC.DivideToExponent(edecB, 0, ERounding.Down);
        // Testing that DivRemNaturalScale and division method return
        // the same value
        TestCommon.CompareTestEqualAndConsistent(edecD, edecE);
        edecE = edecC.RemainderNaturalScale(edecB, null);
        TestCommon.CompareTestEqualAndConsistent(edecRem, edecE);
      }
      if (!edecA.IsZero) {
        EDecimal[] divrem = edecC.DivRemNaturalScale(edecA);
        edecD = divrem[0].Plus(null);
        edecRem = divrem[1];
        TestCommon.CompareTestEqualAndConsistent(edecD, edecB);
        TestCommon.CompareTestEqual(EDecimal.Zero, edecRem);
      }
      if (!edecB.IsZero) {
        EDecimal[] divrem = edecA.DivRemNaturalScale(edecB);
        edecC = divrem[0].Plus(null);
        edecRem = divrem[1];
        edecD = edecB.Multiply(edecC);
        edecD = edecD.Add(edecRem);
        TestCommon.CompareTestEqualAndConsistent(edecA, edecD);
      }
      // -----------------------------------
      // EFloat
      // -----------------------------------
      EFloat efloatA = EFloat.FromEInteger(bigintA);
      EFloat efloatB = EFloat.FromEInteger(bigintB);
      EFloat efloatC = efloatA.Multiply(efloatB);
      EFloat efloatRem;
      EFloat efloatE;
      EFloat efloatD;
      TestCommon.CompareTestEqualAndConsistent(
        efloatC,
        efloatB.Multiply(efloatA));
      if (!efloatB.IsZero) {
        EFloat[] divrem = efloatC.DivRemNaturalScale(efloatB);
        efloatD = divrem[0].Plus(null);
        efloatRem = divrem[1];
        TestCommon.CompareTestEqualAndConsistent(efloatD, efloatA);
        TestCommon.CompareTestEqual(EFloat.Zero, efloatRem);
        efloatE = efloatC.DivideToExponent(efloatB, 0, ERounding.Down);
        // Testing that DivRemNaturalScale and division method return
        // the same value
        TestCommon.CompareTestEqualAndConsistent(efloatD, efloatE);
        efloatE = efloatC.RemainderNaturalScale(efloatB, null);
        TestCommon.CompareTestEqualAndConsistent(efloatRem, efloatE);
      }
      if (!efloatA.IsZero) {
        EFloat[] divrem = efloatC.DivRemNaturalScale(efloatA);
        efloatD = divrem[0].Plus(null);
        efloatRem = divrem[1];
        TestCommon.CompareTestEqualAndConsistent(efloatD, efloatB);
        TestCommon.CompareTestEqual(EFloat.Zero, efloatRem);
      }
      if (!efloatB.IsZero) {
        EFloat[] divrem = efloatA.DivRemNaturalScale(efloatB);
        efloatC = divrem[0].Plus(null);
        efloatRem = divrem[1];
        efloatD = efloatB.Multiply(efloatC);
        efloatD = efloatD.Add(efloatRem);
        TestCommon.CompareTestEqualAndConsistent(efloatA, efloatD);
      }
    }

    /*
    private EInteger VBString(int len) {
      int cc = len;
      var sb = new System.Text.StringBuilder();
      for (var i = 0; i < len; ++i) {
        sb.Append('0' + Math.Abs(cc % 9));
        cc = unchecked(cc * 31);
      }
      return EInteger.FromString(sb.ToString());
    }
    */

    [Test]
    public void TT2() {
      EInteger bi, bi2, r;
      // bi = VBString(740000);
      // bi2 = VBString(333333);
      bi = EInteger.FromString("1").ShiftLeft(740000).Subtract(EInteger.One);
      bi2 = EInteger.FromString("1").ShiftLeft(333330).Subtract(EInteger.One);
      r = bi.Divide(bi2);
      EInteger r2 = bi.Divide(bi2);
      Assert.AreEqual(r, r2);
    }

    [Test]
    public void TT() {
      EInteger bi = EInteger.FromString(
       "1").ShiftLeft(742072).Subtract(EInteger.One);
      string valueBiStr = bi.ToString();
    }
  }
}
