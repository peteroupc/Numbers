using System;
using System.Text;
using NUnit.Framework;
using PeterO;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class EIntegerTest {
    private static long[] valueBitLengths = {
      -4294967297L, 33L, -4294967296L,
      32L,
      -4294967295L, 32L, -2147483649L, 32L, -2147483648L, 31L, -2147483647L,
      31L,
      -1073741825L, 31L, -1073741824L, 30L, -1073741823L, 30L,
      -536870913L, 30L,
      -536870912L, 29L, -536870911L, 29L, -268435457L, 29L, -268435456L, 28L,
      -268435455L, 28L, -134217729L, 28L, -134217728L, 27L, -134217727L, 27L,
      -67108865L, 27L, -67108864L, 26L, -67108863L, 26L, -33554433L, 26L,
      -33554432L,
      25L, -33554431L, 25L, -16777217L, 25L, -16777216L, 24L, -16777215L, 24L,
      -8388609L, 24L, -8388608L, 23L, -8388607L, 23L, -4194305L, 23L,
      -4194304L,
      22L,
      -4194303L, 22L, -2097153L, 22L, -2097152L, 21L, -2097151L, 21L,
      -1048577L,
      21L,
      -1048576L, 20L, -1048575L, 20L, -524289L, 20L, -524288L, 19L, -524287L,
      19L,
      -262145L, 19L, -262144L, 18L, -262143L, 18L, -131073L, 18L,
      -131072L, 17L,
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
      32769L, 16L, 65535L, 16L, 65536L, 17L, 65537L, 17L, 131071L, 17L,
      131072L,
      18L,
      131073L, 18L, 262143L, 18L, 262144L, 19L, 262145L, 19L, 524287L, 19L,
      524288L,
      20L, 524289L, 20L, 1048575L, 20L, 1048576L, 21L, 1048577L, 21L,
      2097151L,
      21L,
      2097152L, 22L, 2097153L, 22L, 4194303L, 22L, 4194304L, 23L,
      4194305L, 23L,
      8388607L, 23L, 8388608L, 24L, 8388609L, 24L, 16777215L, 24L, 16777216L,
      25L,
      16777217L, 25L, 33554431L, 25L, 33554432L, 26L, 33554433L, 26L,
      67108863L,
      26L,
      67108864L, 27L, 67108865L, 27L, 134217727L, 27L, 134217728L, 28L,
      134217729L,
      28L, 268435455L, 28L, 268435456L, 29L, 268435457L, 29L, 536870911L, 29L,
      536870912L, 30L, 536870913L, 30L, 1073741823L, 30L, 1073741824L, 31L,
      1073741825L, 31L, 2147483647L, 31L, 2147483648L, 32L, 2147483649L, 32L,
      4294967295L, 32L, 4294967296L, 33L, 4294967297L, 33,
    };

    private static long[] valueLowBits = {
      0L, -1L, 1L, 0L, 2L, 1L, 3L, 0L, 4L,
      2L, 5L, 0L,
      7L, 0L, 8L, 3L, 9L, 0L, 15L, 0L, 16L, 4L, 17L, 0L, 31L, 0L, 32L, 5L,
      33L, 0L, 63L, 0L, 64L,
      6L, 65L, 0L, 127L, 0L, 128L, 7L, 129L, 0L, 255L, 0L, 256L, 8L, 257L,
      0L, 511L, 0L, 512L,
      9L, 513L, 0L, 1023L, 0L, 1024L, 10L, 1025L, 0L, 2047L, 0L, 2048L, 11L,
      2049L, 0L,
      4095L, 0L, 4096L, 12L, 4097L, 0L, 8191L, 0L, 8192L, 13L, 8193L, 0L,
      16383L,
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
      4294967295L, 0L, 4294967296L, 32L, 4294967297L, 0,
    };

    public static void AssertAdd(EInteger bi, EInteger bi2, string s) {
      EIntegerTest.AssertBigIntegersEqual(s, bi + (EInteger)bi2);
      EIntegerTest.AssertBigIntegersEqual(s, bi2 + (EInteger)bi);
      EInteger negbi = EInteger.Zero - (EInteger)bi;
      EInteger negbi2 = EInteger.Zero - (EInteger)bi2;
      EIntegerTest.AssertBigIntegersEqual(s, bi - (EInteger)negbi2);
      EIntegerTest.AssertBigIntegersEqual(s, bi2 - (EInteger)negbi);
    }

    public static void AssertBigIntegersEqual(string a, EInteger b) {
      if (b == null) {
        throw new ArgumentNullException(nameof(b));
      }
      Assert.AreEqual(a, b.ToString());
      EInteger a2 = EInteger.FromString(a);
      TestCommon.CompareTestEqualAndConsistent(a2, b);
      TestCommon.AssertEqualsHashCode(a2, b);
    }

    public static void DoTestDivide(
      string dividend,
      string divisor,
      string result) {
      EInteger bigintA = EInteger.FromString(dividend);
      EInteger bigintB = EInteger.FromString(divisor);
      EInteger bigintTemp;
      if (bigintB.IsZero) {
        try {
          bigintTemp = bigintA / bigintB;
          string msg = "Expected divide by 0 error, but got " +
            bigintTemp;
          Assert.Fail(msg);
        } catch (ArithmeticException ex) {
          Console.WriteLine(ex.Message);
        }
        try {
          bigintA.DivRem(bigintB);
          Assert.Fail("Should have failed");
        } catch (ArithmeticException) {
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      } else {
        AssertBigIntegersEqual(result, bigintA.Divide(bigintB));
        AssertBigIntegersEqual(result, bigintA.DivRem(bigintB)[0]);
      }
    }

    public static void DoTestDivRem(
      string dividend,
      string divisor,
      string result,
      string rem) {
      EInteger bigintA = EInteger.FromString(dividend);
      EInteger bigintB = EInteger.FromString(divisor);
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
      EInteger bigintA = EInteger.FromString(m1);
      EInteger bigintB = EInteger.FromString(m2);
      EInteger bigintC = bigintA.Multiply(bigintB);
      if (result != null) {
        AssertBigIntegersEqual(result, bigintC);
      }
      TestMultiplyDivideOne(bigintA, bigintB);
    }

    public static void DoTestPow(string m1, int m2, string result) {
      EInteger bigintA = EInteger.FromString(m1);
      AssertBigIntegersEqual(result, bigintA.Pow(m2));
      AssertBigIntegersEqual(result, bigintA.PowBigIntVar((EInteger)m2));
    }

    public static void DoTestRemainder(
      string dividend,
      string divisor,
      string result) {
      EInteger bigintA = EInteger.FromString(dividend);
      EInteger bigintB = EInteger.FromString(divisor);
      if (bigintB.IsZero) {
        try {
          bigintA.Remainder(bigintB);
          Assert.Fail("Expected divide by 0 error");
        } catch (ArithmeticException ex) {
          Console.WriteLine(ex.Message);
        }
        try {
          bigintA.DivRem(bigintB);
          Assert.Fail("Should have failed");
        } catch (ArithmeticException) {
          // NOTE: Intentionally empty
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
      EInteger bigintA = EInteger.FromString(m1);
      AssertBigIntegersEqual(result, bigintA << m2);
      m2 = -m2;
      AssertBigIntegersEqual(result, bigintA >> m2);
    }

    public static void DoTestShiftRight(string m1, int m2, string result) {
      EInteger bigintA = EInteger.FromString(m1);
      AssertBigIntegersEqual(result, bigintA >> m2);
      m2 = -m2;
      AssertBigIntegersEqual(result, bigintA << m2);
    }

    public static void DoTestShiftRight2(string m1, int m2, EInteger result) {
      EInteger bigintA = EInteger.FromString(m1);
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
        throw new ArgumentException (
          "x (" + x + ") is less than 0");
      }
      if (pow <= 0) {
        throw new ArgumentException (
          "pow (" + pow + ") is not greater than 0");
      }
      if (intMod <= 0) {
        throw new ArgumentException (
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
      if (r == null) {
        throw new ArgumentNullException(nameof(r));
      }
      int selection = r.UniformInt(100);
      int count = r.UniformInt(60) + 1;
      if (selection < 40) {
        count = r.UniformInt(7) + 1;
      }
      if (selection < 50) {
        count = r.UniformInt(15) + 1;
      }
      if (selection < 3) {
        count = r.UniformInt(250) + 1;
      }
      var bytes = new byte[count];
      for (var i = 0; i < count; ++i) {
        bytes[i] = (byte)((int)r.UniformInt(256));
      }
      return BigFromBytes(bytes);
    }
    [Test]
    public void TestFromBoolean() {
      Assert.AreEqual(EInteger.One, EInteger.FromBoolean(true));
      Assert.AreEqual(EInteger.Zero, EInteger.FromBoolean(false));
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
      for (var i = 0; i < 10000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        int smallIntB = r.UniformInt(0x7fffffff);
        EInteger bigintC = bigintA.Add(smallIntB);
        EInteger bigintD = bigintC.Subtract(smallIntB);
        if (!bigintD.Equals(bigintA)) {
          Assert.AreEqual(
            bigintA,
            bigintD,
            "TestAddSubtract " + bigintA + "; " + smallIntB);
        }
        bigintD = bigintC - (EInteger)bigintA;
        if (!bigintD.Equals(EInteger.FromInt32(smallIntB))) {
          Assert.AreEqual (
            EInteger.FromInt32(smallIntB),
            bigintD,
            "TestAddSubtract " + bigintA + "; " + smallIntB);
        }
        bigintC = bigintA.Subtract(smallIntB);
        bigintD = bigintC.Add(smallIntB);
        if (!bigintD.Equals(bigintA)) {
          Assert.AreEqual(
            bigintA,
            bigintD,
            "TestAddSubtract " + bigintA + "; " + smallIntB);
        }
      }
    }
    [Test]
    public void TestAddSubSmall() {
      // Test int overloads
      Assert.AreEqual (
        EInteger.FromInt32(2370),
        EInteger.FromInt32(1970).Add(400));
      Assert.AreEqual (
        EInteger.FromInt32(1570),
        EInteger.FromInt32(1970).Add(-400));
      Assert.AreEqual (
        EInteger.FromInt32(1970),
        EInteger.FromInt32(1570).Add(400));
      Assert.AreEqual (
        EInteger.FromInt32(770),
        EInteger.FromInt32(370).Add(400));
      Assert.AreEqual (
        EInteger.FromInt32(-30),
        EInteger.FromInt32(370).Add(-400));
      Assert.AreEqual (
        EInteger.FromInt32(370),
        EInteger.FromInt32(-30).Add(400));
      Assert.AreEqual (
        EInteger.FromInt32(-430),
        EInteger.FromInt32(-30).Add(-400));
      Assert.AreEqual (
        EInteger.FromInt32(1570),
        EInteger.FromInt32(1970).Subtract(400));
      Assert.AreEqual (
        EInteger.FromInt32(2370),
        EInteger.FromInt32(1970).Subtract(-400));
      Assert.AreEqual (
        EInteger.FromInt32(1170),
        EInteger.FromInt32(1570).Subtract(400));
      Assert.AreEqual (
        EInteger.FromInt32(-30),
        EInteger.FromInt32(370).Subtract(400));
      Assert.AreEqual (
        EInteger.FromInt32(770),
        EInteger.FromInt32(370).Subtract(-400));
      Assert.AreEqual (
        EInteger.FromInt32(-430),
        EInteger.FromInt32(-30).Subtract(400));
      Assert.AreEqual (
        EInteger.FromInt32(370),
        EInteger.FromInt32(-30).Subtract(-400));
      // Check with EInteger overloads
      Assert.AreEqual (
        EInteger.FromInt32(2370),
        EInteger.FromInt32(1970).Add(EInteger.FromInt32(400)));
      Assert.AreEqual (
        EInteger.FromInt32(1570),
        EInteger.FromInt32(1970).Add(EInteger.FromInt32(-400)));
      Assert.AreEqual (
        EInteger.FromInt32(1970),
        EInteger.FromInt32(1570).Add(EInteger.FromInt32(400)));
      Assert.AreEqual (
        EInteger.FromInt32(770),
        EInteger.FromInt32(370).Add(EInteger.FromInt32(400)));
      Assert.AreEqual (
        EInteger.FromInt32(-30),
        EInteger.FromInt32(370).Add(EInteger.FromInt32(-400)));
      Assert.AreEqual (
        EInteger.FromInt32(370),
        EInteger.FromInt32(-30).Add(EInteger.FromInt32(400)));
      Assert.AreEqual (
        EInteger.FromInt32(-430),
        EInteger.FromInt32(-30).Add(EInteger.FromInt32(-400)));
      Assert.AreEqual (
        EInteger.FromInt32(1570),
        EInteger.FromInt32(1970).Subtract(EInteger.FromInt32(400)));
      Assert.AreEqual (
        EInteger.FromInt32(2370),
        EInteger.FromInt32(1970).Subtract(EInteger.FromInt32(-400)));
      Assert.AreEqual (
        EInteger.FromInt32(1170),
        EInteger.FromInt32(1570).Subtract(EInteger.FromInt32(400)));
      Assert.AreEqual (
        EInteger.FromInt32(-30),
        EInteger.FromInt32(370).Subtract(EInteger.FromInt32(400)));
      Assert.AreEqual (
        EInteger.FromInt32(770),
        EInteger.FromInt32(370).Subtract(EInteger.FromInt32(-400)));
      Assert.AreEqual (
        EInteger.FromInt32(-430),
        EInteger.FromInt32(-30).Subtract(EInteger.FromInt32(400)));
      Assert.AreEqual (
        EInteger.FromInt32(370),
        EInteger.FromInt32(-30).Subtract(EInteger.FromInt32(-400)));
      // Other tests
      EInteger bigintC = EInteger.FromInt32(0).Add(60916);
      EInteger bigintD = bigintC.Subtract(60916);
      Assert.AreEqual(EInteger.FromInt32(60916), bigintC);
      Assert.AreEqual(EInteger.FromInt32(0), bigintD);

      bigintC = EInteger.FromInt32(0).Add(EInteger.FromInt32(60916));
      bigintD = bigintC.Subtract(EInteger.FromInt32(60916));
      Assert.AreEqual(EInteger.FromInt32(60916), bigintC);
      Assert.AreEqual(EInteger.FromInt32(0), bigintD);
    }

    [Test]
    public void TestAsInt32Checked() {
      Assert.AreEqual (
        Int32.MinValue,
        EInteger.FromInt64(Int32.MinValue).ToInt32Checked());
      Assert.AreEqual (
        Int32.MaxValue,
        EInteger.FromInt64(Int32.MaxValue).ToInt32Checked());
      try {
        EInteger.FromInt64(Int32.MinValue - 1L).ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromInt64(Int32.MaxValue + 1L).ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromString(
  "999999999999999999999999999999999").ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual (
        Int32.MinValue,
        EInteger.FromInt64(Int32.MinValue).ToInt32Checked());
      Assert.AreEqual (
        Int32.MaxValue,
        EInteger.FromInt64(Int32.MaxValue).ToInt32Checked());
      try {
        EInteger.FromInt64(Int32.MinValue - 1L).ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromInt64(Int32.MaxValue + 1L).ToInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestAsInt64Checked() {
      Assert.AreEqual (
        Int64.MinValue,
        EInteger.FromInt64(Int64.MinValue).ToInt64Checked());
      Assert.AreEqual (
        Int64.MaxValue,
        EInteger.FromInt64(Int64.MaxValue).ToInt64Checked());
      try {
        EInteger bigintTemp = EInteger.FromInt64(Int64.MinValue);
        bigintTemp -= EInteger.One;
        bigintTemp.ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger bigintTemp = EInteger.FromInt64(Int64.MaxValue);
        bigintTemp += EInteger.One;
        bigintTemp.ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var checklongs = new long[] {
        unchecked((long)0xfffffff200000000L),
        unchecked((long)0xfffffff280000000L),
        unchecked((long)0xfffffff200000001L),
        unchecked((long)0xfffffff280000001L),
        unchecked((long)0xfffffff380000001L),
        unchecked((long)0xfffffff382222222L),
        unchecked((long)0x0000000380000001L),
        unchecked((long)0x0000000382222222L),
        unchecked((long)0xfffffff27fffffffL),
        -8, -32768,
      };
      foreach (long lng in checklongs) {
        Assert.AreEqual(
          lng,
          EInteger.FromInt64(lng).ToInt64Checked());
      }
      Assert.AreEqual (
        Int32.MinValue,
        EInteger.FromInt64(Int32.MinValue).ToInt64Checked());
      Assert.AreEqual (
        Int32.MaxValue,
        EInteger.FromInt64(Int32.MaxValue).ToInt64Checked());
      Assert.AreEqual (
        0x80000000L,
        EInteger.FromInt64(0x80000000L).ToInt64Checked());
      Assert.AreEqual (
        0x90000000L,
        EInteger.FromInt64(0x90000000L).ToInt64Checked());
      try {
        EInteger.FromString(
  "999999999999999999999999999999999").ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual (
        Int64.MinValue,
        EInteger.FromInt64(Int64.MinValue).ToInt64Checked());
      Assert.AreEqual (
        Int64.MaxValue,
        EInteger.FromInt64(Int64.MaxValue).ToInt64Checked());
      try {
        EInteger bigintTemp = EInteger.FromInt64(Int64.MinValue);
        bigintTemp -= EInteger.One;
        bigintTemp.ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger bigintTemp = EInteger.FromInt64(Int64.MaxValue);
        bigintTemp += EInteger.One;
        bigintTemp.ToInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      long longV = unchecked((long)0xfffffff200000000L);
      Assert.AreEqual (
        longV,
        EInteger.FromInt64(longV).ToInt64Checked());
      longV = unchecked((long)0xfffffff280000000L);
      Assert.AreEqual (
        longV,
        EInteger.FromInt64(longV).ToInt64Checked());
      longV = unchecked((long)0xfffffff280000001L);
      Assert.AreEqual (
        longV,
        EInteger.FromInt64(longV).ToInt64Checked());
      longV = unchecked((long)0xfffffff27fffffffL);
      Assert.AreEqual (
        longV,
        EInteger.FromInt64(longV).ToInt64Checked());
      Assert.AreEqual (
        0x0000000380000001L,
        EInteger.FromInt64(0x0000000380000001L).ToInt64Checked());
      Assert.AreEqual (
        0x0000000382222222L,
        EInteger.FromInt64(0x0000000382222222L).ToInt64Checked());
      Assert.AreEqual(-8L, EInteger.FromInt64(-8L).ToInt64Checked());
      Assert.AreEqual(-32768L, EInteger.FromInt64(-32768L).ToInt64Checked());
      Assert.AreEqual (
        Int32.MinValue,
        EInteger.FromInt64(Int32.MinValue).ToInt64Checked());
      Assert.AreEqual (
        Int32.MaxValue,
        EInteger.FromInt64(Int32.MaxValue).ToInt64Checked());
      Assert.AreEqual (
        0x80000000L,
        EInteger.FromInt64(0x80000000L).ToInt64Checked());
      Assert.AreEqual (
        0x90000000L,
        EInteger.FromInt64(0x90000000L).ToInt64Checked());
    }
    [Test]
    public void TestBigIntegerModPow() {
      try {
        EInteger.One.ModPow(null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(null, EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(EInteger.Zero, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(EInteger.FromString("-1"),
          EInteger.FromString("1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(EInteger.FromString("0"), EInteger.FromString(
  "0"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(EInteger.FromString("0"),
          EInteger.FromString("-1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(EInteger.FromString("1"), EInteger.FromString(
  "0"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ModPow(EInteger.FromString("1"),
          EInteger.FromString("-1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
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
        Assert.AreEqual (
          bigintA.CanFitInInt32(),
          bigintA.GetSignedBitLengthAsEInteger().CompareTo(31) <= 0);
        Assert.AreEqual (
          bigintA.CanFitInInt64(),
          bigintA.GetSignedBitLengthAsEInteger().CompareTo(63) <= 0);
      }
    }

    [Test]
    public void TestCanFitInInt64() {
      EInteger ei;
      ei = EInteger.FromString("9223372036854775807");
      Assert.IsTrue(ei.CanFitInInt64(), ei.ToString());
      {
        long numberTemp = ei.GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(63, numberTemp);
      }
      ei = EInteger.FromString("9223372036854775808");
      Assert.IsFalse(ei.CanFitInInt64(), ei.ToString());
      {
        long numberTemp = ei.GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(64, numberTemp);
      }
      ei = EInteger.FromString("-9223372036854775807");
      Assert.IsTrue(ei.CanFitInInt64(), ei.ToString());
      {
        long numberTemp = ei.GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(63, numberTemp);
      }
      ei = EInteger.FromString("-9223372036854775808");
      Assert.IsTrue(ei.CanFitInInt64(), ei.ToString());
      {
        long numberTemp = ei.GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(63, numberTemp);
      }
      ei = EInteger.FromString("-9223372036854775809");
      Assert.IsFalse(ei.CanFitInInt64(), ei.ToString());
      {
        long numberTemp = ei.GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(64, numberTemp);
      }
      ei = EInteger.FromString("-9223373136366403584");
      Assert.IsFalse(ei.CanFitInInt64(), ei.ToString());
      {
        long numberTemp = ei.GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(64, numberTemp);
      }
      ei = EInteger.FromString("9223373136366403584");
      Assert.IsFalse(ei.CanFitInInt64(), ei.ToString());
      {
        long numberTemp = ei.GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(64, numberTemp);
      }
      var strings = new string[] {
        "8000FFFFFFFF0000",
        "8000AAAAAAAA0000",
        "8000800080000000",
        "8000000100010000",
        "8000FFFF00000000",
        "80000000FFFF0000",
        "8000800000000000",
        "8000000080000000",
        "8000AAAA00000000",
        "80000000AAAA0000",
        "8000000100000000",
        "8000000000010000",
      };
      foreach (string str in strings) {
        ei = EInteger.FromRadixString(str, 16);
        Assert.IsFalse(ei.CanFitInInt64());
        {
          long numberTemp = ei.GetSignedBitLengthAsEInteger().ToInt32Checked();
          Assert.AreEqual(64, numberTemp);
        }
        ei = ei.Negate();
        Assert.IsFalse(ei.CanFitInInt64());
        {
          long numberTemp = ei.GetSignedBitLengthAsEInteger().ToInt32Checked();
          Assert.AreEqual(64, numberTemp);
        }
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
    public void TestDivideA() {
      DoTestDivide(
        "39401404978667143593022554770633078187236345017741021112301638514137074723630749875836463116600753265992771999563776",
        "6277005955876855982708123588802319701019026907066160578560",
        "6277101735386680763835789423207589043669308487479442014208");
    }

    [Test]
    public void TestDivide() {
      int intA, intB;
      DoTestDivide(
        "39401404978667143593022554770633078187236345017741021112301638514137074723630749875836463116600753265992771999563776",
        "6277005955876855982708123588802319701019026907066160578560",
        "6277101735386680763835789423207589043669308487479442014208");
      DoTestDivide(
        "340277174703306882242637262502835978240",
        "79226953606891185567396986880",
        "4294967296");
      DoTestDivide(
        "44461738044811866704570272160729755524383493147516085922742403681586307620758054502667856562873477505768158700319760453047044081412393321568753479912147358343844563186048273758088945022589574729044743021988362306225753942249201773678443992606696524197361479929661991788310321409367753462284203449631729626517511224343015354155975783754763572354740724506742793459644155837703671449155713000260325445046273385372701820583016334341594713806706345456633635125343104401883366671083569152",
        "6667912688606651657935168942074070387623462798286393292334546164025938697493268465740399785103348978411106010660409247384863031649363973174034406552719188394559243700794785023362300512913065060420313203793021880700852215978918600154969735168",
        "6668014432879854274079851790721257797144739185760979705624542990230371779898108261760364709743735387156366994446448705720136517621612785459920009307944044809722559761949909348022458684413967432579072465854783948147327367860791365121685323776");
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
      DoTestDivide("4294901760", "281470681808895", "0");
      DoTestDivide("281470681808895", "281470681808895", "1");
      DoTestDivide("281195803901951", "281470681808895", "0");
      DoTestDivide(
        "281470681808895",
        "79226953606891185567396986880",
        "0");
      DoTestDivide(
        "1208907373151751269056511",
        "281470681808895",
        "4294967295");
      DoTestDivide(
        "1208907373151751269056511",
        "79226953606891185567396986880",
        "0");
      DoTestDivide(
        "79226953606891185567396986880",
        "79226953606891185567396986880",
        "1");
      DoTestDivide(
        "79226953606891185567396986880",
        "79226953606891185567396986880",
        "1");
      DoTestDivide(
        "79149582354435849300215791616",
        "281470681808895",
        "281200094609408");
      DoTestDivide(
        "79149582354435849304510693376",
        "79226953606891185567396986880",
        "0");
      DoTestDivide(
        "340277174703229510990181926235654782976",
        "79226953606891185567396986880",
        "4294967295");
      DoTestDivide(
        "340277174703229510990181926235654782976",
        "79226953606891185567396986880",
        "4294967295");
      DoTestDivide(
        "79226953606891185567396986880",
        "6277005955876855982708123588802319701019026907066160578560",
        "0");
      DoTestDivide(
        "22278626849872979772991819660510225504468991",
        "79226953606891185567396986880",
        "281200094609408");
      DoTestDivide(
        "6270875973713392427274690200693718464284551950581721071616",
        "79226953606891185567396986880",
        "79150790081217380608951451648");
      DoTestDivide(
        "6277005955876855982708123588802242329766571570798979383296",
        "6277005955876855982708123588802319701019026907066160578560",
        "0");
      DoTestDivide(
        "6277005955876855982708123588802242329766571570798979383296",
        "6277005955876855982708123588802319701019026907066160578560",
        "0");
      DoTestDivide(
        "26959535297282185466869868771998681536704617202858716036715199266816",
        "6277005955876855982708123588802319701019026907066160578560",
        "4294967295");
      DoTestDivide(
        "496829980752160275550680055858571148163286974448396184421327120687227627818219200249856",
        "6277005955876855982708123588802319701019026907066160578560",
        "79150790081217380608951451648");
      DoTestDivide(
        "2135954443842118711369801686589217620410698847025641089415087336821733096438436218376946913837056",
        "6277005955876855982708123588802319701019026907066160578560",
        "340282366920861091030327650447175712768");
    }

[Test]
public void TestEIntegerSpeed() {
  // var sw = new System.Diagnostics.Stopwatch();
  // sw.Start();
  string str = TestCommon.Repeat("7", 100000);
  EInteger ei = EInteger.FromString(str);
  // sw.Stop();
  // Console.WriteLine(String.Empty + sw.ElapsedMilliseconds + " ms");
}

[Test]
public void TestLongIntegerStrings() {
  string str = TestCommon.Repeat("7", 10000);
  for (var i = 8; i <= 36; ++i) {
    EInteger ei = EInteger.FromRadixString(str, i);
    Assert.AreEqual(str, ei.ToRadixString(i), "radix=" + i);
  }
  str = TestCommon.Repeat("7", 5000) +
    TestCommon.Repeat("5", 5000);
  for (var i = 8; i <= 36; ++i) {
    EInteger ei = EInteger.FromRadixString(str, i);
    Assert.AreEqual(str, ei.ToRadixString(i), "radix=" + i);
  }
  var sb = new StringBuilder();
  var rg = new RandomGenerator();
  for (var i = 0; i < 10000; ++i) {
    sb.Append((char)(0x31 + rg.UniformInt(7)));
  }
  str = sb.ToString();
  for (var i = 8; i <= 36; ++i) {
    EInteger ei = EInteger.FromRadixString(str, i);
    Assert.AreEqual(str, ei.ToRadixString(i), "radix=" + i);
  }
}

    [Test]
    public void TestDivRem() {
      try {
        EInteger.One.DivRem(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.DivRem(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
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
      TestCommon.AssertEqualsHashCode (
        EInteger.Zero,
        EInteger.FromString("-0"));
      TestCommon.AssertEqualsHashCode (
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
        EInteger.FromString(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EInteger.Zero.GetSignedBit(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromString("x11");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromString(".");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromString("..");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromString("e200");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EInteger.One.Mod((EInteger)(-1));
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Add(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Subtract(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Divide(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Divide(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Remainder(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Mod(EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (DivideByZeroException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Remainder(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(EInteger.One, ((EInteger)13).Mod((EInteger)4));
      Assert.AreEqual((EInteger)3, ((EInteger)(-13)).Mod((EInteger)4));
    }
    [Test]
    public void TestFromBytes() {
      Assert.AreEqual (
        EInteger.Zero, EInteger.FromBytes(new byte[] { }, false));

      try {
        EInteger.FromBytes(null, false);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
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
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", -37);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", Int32.MinValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixString("0", Int32.MaxValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var fr = new RandomGenerator();
      for (int i = 2; i <= 36; ++i) {
        for (int j = 0; j < 100; ++j) {
          StringAndBigInt sabi = StringAndBigInt.Generate(fr, i);
          Assert.AreEqual (
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
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", 1, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", 0, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", -37, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", Int32.MinValue, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0", Int32.MaxValue, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, -1, 2);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 4, 5);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 0, -8);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 0, 6);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 2, 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 0, 0);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("123", 10, 1, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("-", 10, 0, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("g", 16, 0, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0123gggg", 16, 0, 8);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0123gggg", 10, 0, 8);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromRadixSubstring("0123aaaa", 10, 0, 8);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
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
        EInteger.FromString("xyz");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromString(String.Empty);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromString(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

[Test]
public void TestFromStringInnerMinus() {
  string str = TestCommon.Repeat("1", 1000) + "-" + TestCommon.Repeat("2", 999);
  try {
 EInteger.FromString(str);
 Assert.Fail("Should have failed");
} catch (FormatException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
 throw new InvalidOperationException(String.Empty, ex);
}
  str = TestCommon.Repeat("1", 999) + "-" + TestCommon.Repeat("2", 1000);
  try {
 EInteger.FromString(str);
 Assert.Fail("Should have failed");
} catch (FormatException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
 throw new InvalidOperationException(String.Empty, ex);
}
  str = TestCommon.Repeat("1", 1001) + "-" + TestCommon.Repeat("2", 998);
  try {
 EInteger.FromString(str);
 Assert.Fail("Should have failed");
} catch (FormatException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
 throw new InvalidOperationException(String.Empty, ex);
}
  str = "-"+TestCommon.Repeat("1", 1000) + "-" + TestCommon.Repeat("2", 999);
  try {
 EInteger.FromString(str);
 Assert.Fail("Should have failed");
} catch (FormatException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
 throw new InvalidOperationException(String.Empty, ex);
}
  str = "-"+TestCommon.Repeat("1", 999) + "-" + TestCommon.Repeat("2", 1000);
  try {
 EInteger.FromString(str);
 Assert.Fail("Should have failed");
} catch (FormatException) {
// NOTE: Intentionally empty
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
 throw new InvalidOperationException(String.Empty, ex);
}
  str = "-"+TestCommon.Repeat("1", 1001) + "-" + TestCommon.Repeat("2", 998);
  try {
 EInteger.FromString(str);
 Assert.Fail("Should have failed");
} catch (FormatException) {
// NOTE: Intentionally empty
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
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EInteger.FromSubstring("123", -1, 2);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 4, 2);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 1, -1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 1, 4);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 1, 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.FromSubstring("123", 2, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
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
        string stringTemp = EInteger.FromString("781631509928000000").Gcd(
            EInteger.FromString("1000000")).ToString();
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
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      {
        string stringTemp = EInteger.Zero.Gcd(EInteger.FromString(
              "244")).ToString();
        Assert.AreEqual(
          "244",
          stringTemp);
      }
      {
        string stringTemp = EInteger.Zero.Gcd(EInteger.FromString(
              "-244")).ToString();
        Assert.AreEqual(
          "244",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromString (
            "244").Gcd(EInteger.Zero).ToString();
        Assert.AreEqual(
          "244",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromString (
            "-244").Gcd(EInteger.Zero).ToString();
        Assert.AreEqual(
          "244",
          stringTemp);
      }
      {
        string stringTemp =
          EInteger.One.Gcd(EInteger.FromString("244")).ToString();
        Assert.AreEqual(
          "1",
          stringTemp);
      }
      {
        string stringTemp = EInteger.One.Gcd(EInteger.FromString(
              "-244")).ToString();
        Assert.AreEqual(
          "1",
          stringTemp);
      }
      {
        string stringTemp =
          EInteger.FromString("244").Gcd(EInteger.One).ToString();
        Assert.AreEqual(
          "1",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromString (
            "-244").Gcd(EInteger.One).ToString();
        Assert.AreEqual(
          "1",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromString("15").Gcd(
  EInteger.FromString (
              "15")).ToString();
        Assert.AreEqual(
          "15",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromString("-15").Gcd(
            EInteger.FromString("15")).ToString();
        Assert.AreEqual(
          "15",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromString("15").Gcd(
            EInteger.FromString("-15")).ToString();
        Assert.AreEqual(
          "15",
          stringTemp);
      }
      {
        string stringTemp = EInteger.FromString (
            "-15").Gcd(EInteger.FromString("-15")).ToString();
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
        Assert.AreEqual (
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

#pragma warning disable CS0618 // We're testing an obsolete method here
    [Test]
    public void TestGetDigitCount() {
      var r = new RandomGenerator();
      {
        int integerTemp2 = EInteger.FromString (
            "101754295360222878437145684059582837272").GetDigitCount();
        Assert.AreEqual(39, integerTemp2);
      }
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        String str = bigintA.Abs().ToString();
        Assert.AreEqual (
          EInteger.FromInt32(str.Length),
          bigintA.GetDigitCountAsEInteger(),
          str);
      }
    }
#pragma warning restore CS0618

    [Test]
    [Timeout(1000)]
    public void TestGetSignedBit() {
      Assert.IsFalse(EInteger.Zero.GetSignedBit(0));
      Assert.IsFalse(EInteger.Zero.GetSignedBit(1));
      Assert.IsTrue(EInteger.One.GetSignedBit(0));
      Assert.IsFalse(EInteger.One.GetSignedBit(1));
      for (int i = 0; i < 32; ++i) {
        Assert.IsTrue(EInteger.FromInt64(-1).GetSignedBit(i));
      }
      Assert.IsFalse(EInteger.Zero.GetSignedBit(EInteger.Zero));
      Assert.IsFalse(EInteger.Zero.GetSignedBit(EInteger.One));
      Assert.IsTrue(EInteger.One.GetSignedBit(EInteger.Zero));
      Assert.IsFalse(EInteger.One.GetSignedBit(EInteger.One));
      for (int i = 0; i < 32; ++i) {
        Assert.IsTrue (
          EInteger.FromInt64(-1).GetSignedBit(EInteger.FromInt32(i)));
      }
      try {
        EInteger.Zero.GetSignedBit(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.GetSignedBit(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.Zero.GetSignedBit(EInteger.FromInt32(-1));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.GetSignedBit(EInteger.FromInt32(-1));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

#pragma warning disable CS0618 // We're testing an obsolete method here
    [Test]
    public void TestGetSignedBitLength() {
      for (var i = 0; i < valueBitLengths.Length; i += 2) {
        Assert.AreEqual (
          (int)valueBitLengths[i + 1],
          EInteger.FromInt64(valueBitLengths[i]).GetSignedBitLength(),
          TestCommon.LongToString(valueBitLengths[i]));
      }
      {
        long numberTemp = EInteger.FromInt64(
  -2147483647L).GetSignedBitLength();
        Assert.AreEqual(31, numberTemp);
      }
      {
        long numberTemp = EInteger.FromInt64(
  -2147483648L).GetSignedBitLength();
        Assert.AreEqual(31, numberTemp);
      }
      {
        long numberTemp = EInteger.FromInt64(
  -2147483649L).GetSignedBitLength();
        Assert.AreEqual(32, numberTemp);
      }
      {
        long numberTemp = EInteger.FromInt64(
  -2147483650L).GetSignedBitLength();
        Assert.AreEqual(32, numberTemp);
      }
      {
        int integerTemp2 = EInteger.FromInt64(
  2147483647L).GetSignedBitLength();
  Assert.AreEqual(31, integerTemp2);
}
      {
        int integerTemp2 = EInteger.FromInt64(
  2147483648L).GetSignedBitLength();
  Assert.AreEqual(32, integerTemp2);
}
      {
        int integerTemp2 = EInteger.FromInt64(
  2147483649L).GetSignedBitLength();
  Assert.AreEqual(32, integerTemp2);
}
      {
        int integerTemp2 = EInteger.FromInt64(
  2147483650L).GetSignedBitLength();
  Assert.AreEqual(32, integerTemp2);
}
      Assert.AreEqual(0, EInteger.FromInt64(0).GetSignedBitLength());
      Assert.AreEqual(1, EInteger.FromInt64(1).GetSignedBitLength());
      Assert.AreEqual(2, EInteger.FromInt64(2).GetSignedBitLength());
      Assert.AreEqual(2, EInteger.FromInt64(2).GetSignedBitLength());
      {
        long numberTemp =
          EInteger.FromInt64(Int32.MaxValue).GetSignedBitLength();
        Assert.AreEqual(31, numberTemp);
      }
      {
        long numberTemp =
          EInteger.FromInt64(Int32.MinValue).GetSignedBitLength();
        Assert.AreEqual(31, numberTemp);
      }
      Assert.AreEqual(16, EInteger.FromInt64(65535).GetSignedBitLength());
      Assert.AreEqual(16, EInteger.FromInt64(-65535).GetSignedBitLength());
      Assert.AreEqual(17, EInteger.FromInt64(65536).GetSignedBitLength());
      Assert.AreEqual(16, EInteger.FromInt64(-65536).GetSignedBitLength());
      {
        long numberTemp =
          EInteger.FromString("19084941898444092059").GetSignedBitLength();
        Assert.AreEqual(65, numberTemp);
      }
      {
        long numberTemp =
          EInteger.FromString("-19084941898444092059").GetSignedBitLength();
        Assert.AreEqual(65, numberTemp);
      }
      Assert.AreEqual(0, EInteger.FromInt64(-1).GetSignedBitLength());
      Assert.AreEqual(1, EInteger.FromInt64(-2).GetSignedBitLength());
    }
#pragma warning restore CS0618

    [Test]
    public void TestGetSignedBitLengthAsEInteger() {
      for (var i = 0; i < valueBitLengths.Length; i += 2) {
        {
          object objectTemp = (int)valueBitLengths[i + 1];
          object objectTemp2 =

            EInteger.FromInt64 (
              valueBitLengths[i]).GetSignedBitLengthAsEInteger()
            .ToInt32Checked();
          string messageTemp = TestCommon.LongToString(valueBitLengths[i]);
          Assert.AreEqual(objectTemp, objectTemp2, messageTemp);
        }
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            -2147483647L).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(31, integerTemp2);
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            -2147483648L).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(31, integerTemp2);
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            -2147483649L).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(32, integerTemp2);
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            -2147483650L).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(32, integerTemp2);
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            2147483647L).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(31, integerTemp2);
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            2147483648L).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(32, integerTemp2);
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            2147483649L).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(32, integerTemp2);
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            2147483650L).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(32, integerTemp2);
      }
      {
        long numberTemp =
          EInteger.FromInt64(
  0).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(0, numberTemp);
      }
      {
        long numberTemp =
          EInteger.FromInt64(
  1).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(1, numberTemp);
      }
      {
        long numberTemp =
          EInteger.FromInt64(
  2).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(2, numberTemp);
      }
      {
        long numberTemp =
          EInteger.FromInt64(
  2).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(2, numberTemp);
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            Int32.MaxValue).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(31, integerTemp2);
      }
      {
        int integerTemp2 = EInteger.FromInt64 (
            Int32.MinValue).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(31, integerTemp2);
      }
      {
        long numberTemp =
          EInteger.FromInt64(
  65535).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(16, numberTemp);
      }
      {
        long numberTemp =
          EInteger.FromInt64(
  -65535).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(16, numberTemp);
      }
      {
        long numberTemp =
          EInteger.FromInt64(
  65536).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(17, numberTemp);
      }
      {
        long numberTemp =
          EInteger.FromInt64(
  -65536).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(16, numberTemp);
      }
      {
        int integerTemp2 = EInteger.FromString("19084941898444092059")
          .GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(65, integerTemp2);
      }
      {
        int integerTemp2 = EInteger.FromString("-19084941898444092059")
          .GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(65, integerTemp2);
      }
      {
        long numberTemp =

          EInteger.FromInt64 (
            -1).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(0, numberTemp);
      }
      {
        long numberTemp =

          EInteger.FromInt64 (
            -2).GetSignedBitLengthAsEInteger().ToInt32Checked();
        Assert.AreEqual(1, numberTemp);
      }
    }

    [Test]
    public void TestGetUnsignedBit() {
      for (var i = 0; i < valueLowBits.Length; i += 2) {
        var lowbit = (int)valueLowBits[i + 1];
        EInteger posint = EInteger.FromInt64(valueLowBits[i]);
        EInteger negint = EInteger.FromInt64(-valueLowBits[i]);
        for (var j = 0; j < lowbit; ++j) {
          Assert.IsFalse(posint.GetUnsignedBit(j));
          Assert.IsFalse(negint.GetUnsignedBit(j));
          Assert.IsFalse(posint.GetUnsignedBit(EInteger.FromInt32(j)));
          Assert.IsFalse(negint.GetUnsignedBit(EInteger.FromInt32(j)));
        }
        if (lowbit >= 0) {
          Assert.IsTrue(posint.GetUnsignedBit(lowbit));
          Assert.IsTrue(negint.GetUnsignedBit(lowbit));
          Assert.IsTrue(posint.GetUnsignedBit(EInteger.FromInt32(lowbit)));
          Assert.IsTrue(negint.GetUnsignedBit(EInteger.FromInt32(lowbit)));
        }
        try {
          posint.GetUnsignedBit(EInteger.FromInt32(-1));
          Assert.Fail("Should have failed");
        } catch (ArgumentException) {
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
        try {
          negint.GetUnsignedBit((int)-1);
          Assert.Fail("Should have failed");
        } catch (ArgumentException) {
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
        try {
          posint.GetUnsignedBit(null);
          Assert.Fail("Should have failed");
        } catch (ArgumentNullException) {
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      }
    }

#pragma warning disable CS0618 // We're testing an obsolete method here
    [Test]
    public void TestGetUnsignedBitLength() {
      for (var i = 0; i < valueBitLengths.Length; i += 2) {
        if (valueBitLengths[i] < 0) {
          continue;
        }
        Assert.AreEqual (
          (int)valueBitLengths[i + 1],
          EInteger.FromInt64(valueBitLengths[i]).GetUnsignedBitLength(),
          TestCommon.LongToString(valueBitLengths[i]));
        Assert.AreEqual (
          (int)valueBitLengths[i + 1],
          EInteger.FromInt64(-valueBitLengths[i]).GetUnsignedBitLength(),
          TestCommon.LongToString(-valueBitLengths[i]));
      }
    }
#pragma warning restore CS0618

#pragma warning disable CS0618 // We're testing an obsolete method here
    [Test]
    public void TestGetLowBit() {
      for (var i = 0; i < valueLowBits.Length; i += 2) {
        Assert.AreEqual (
          (int)valueLowBits[i + 1],
          EInteger.FromInt64(valueLowBits[i]).GetLowBit());
        Assert.AreEqual (
          (int)valueLowBits[i + 1],
          EInteger.FromInt64(-valueLowBits[i]).GetLowBit());
      }
    }
#pragma warning restore CS0618

    [Test]
    public void TestGetLowBitAsEInteger() {
      for (var i = 0; i < valueLowBits.Length; i += 2) {
        {
          long longTemp = valueLowBits[i + 1];
          long longTemp2 = EInteger.FromInt64 (
              valueLowBits[i]).GetLowBitAsEInteger().ToInt64Checked();
          Assert.AreEqual(longTemp, longTemp2);
        }
        {
          long longTemp = valueLowBits[i + 1];
          long longTemp2 = EInteger.FromInt64 (
              -valueLowBits[i]).GetLowBitAsEInteger().ToInt64Checked();
          Assert.AreEqual(longTemp, longTemp2);
        }
      }
    }

    [Test]
    public void TestIntValueUnchecked() {
      Assert.AreEqual(0L, EInteger.Zero.ToInt32Unchecked());
      Assert.AreEqual (
        Int32.MinValue,
        EInteger.FromInt64(Int32.MinValue).ToInt32Unchecked());
      Assert.AreEqual (
        Int32.MaxValue,
        EInteger.FromInt64(Int32.MaxValue).ToInt32Unchecked());
      Assert.AreEqual (
        Int32.MaxValue,
        EInteger.FromInt64(Int32.MinValue - 1L).ToInt32Unchecked());
      Assert.AreEqual (
        Int32.MinValue,
        EInteger.FromInt64(Int32.MaxValue + 1L).ToInt32Unchecked());
    }

    [Test]
    public void TestIsEven() {
      var r = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        EInteger mod = bigintA.Remainder(EInteger.FromInt64(2));
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
      Assert.IsTrue(EInteger.FromInt64(1).IsPowerOfTwo);
      Assert.IsTrue(EInteger.FromInt64(2).IsPowerOfTwo);
      Assert.IsTrue(EInteger.FromInt64(4).IsPowerOfTwo);
      Assert.IsTrue(EInteger.FromInt64(8).IsPowerOfTwo);
      Assert.IsTrue(EInteger.FromInt64(16).IsPowerOfTwo);
      Assert.IsTrue(EInteger.FromInt64(32).IsPowerOfTwo);
      Assert.IsTrue(EInteger.FromInt64(64).IsPowerOfTwo);
      Assert.IsFalse(EInteger.FromInt64(65535).IsPowerOfTwo);
      Assert.IsTrue(EInteger.FromInt64(65536).IsPowerOfTwo);
      Assert.IsFalse(EInteger.FromInt64(65537).IsPowerOfTwo);
      Assert.IsTrue(EInteger.FromInt64(0x100000).IsPowerOfTwo);
      Assert.IsTrue(EInteger.FromInt64(0x10000000).IsPowerOfTwo);
      Assert.IsFalse(EInteger.FromInt64(0).IsPowerOfTwo);
      Assert.IsFalse(EInteger.FromInt64(-1).IsPowerOfTwo);
      Assert.IsFalse(EInteger.FromInt64(-2).IsPowerOfTwo);
      Assert.IsFalse(EInteger.FromInt64(-3).IsPowerOfTwo);
      Assert.IsFalse(EInteger.FromInt64(-4).IsPowerOfTwo);
      Assert.IsFalse(EInteger.FromInt64(-5).IsPowerOfTwo);
      Assert.IsFalse(EInteger.FromInt64(-65536).IsPowerOfTwo);
    }
    [Test]
    public void TestIsZero() {
      // not implemented yet
    }
    [Test]
    public void TestLongValueUnchecked() {
      Assert.AreEqual(0L, EInteger.Zero.ToInt64Unchecked());
      Assert.AreEqual (
        Int64.MinValue,
        EInteger.FromInt64(Int64.MinValue).ToInt64Unchecked());
      Assert.AreEqual (
        Int64.MaxValue,
        EInteger.FromInt64(Int64.MaxValue).ToInt64Unchecked());
      {
        object objectTemp = Int64.MaxValue;
        object objectTemp2 = EInteger.FromInt64(Int64.MinValue)
          .Subtract(EInteger.One).ToInt64Unchecked();
        Assert.AreEqual(objectTemp, objectTemp2);
      }
      {
        object objectTemp = Int64.MinValue;
        object objectTemp2 = EInteger.FromInt64 (
            Int64.MaxValue).Add(EInteger.One).ToInt64Unchecked();
        Assert.AreEqual(objectTemp, objectTemp2);
      }
      long aa = unchecked((long)0xfffffff200000000L);
      Assert.AreEqual (
        aa,
        EInteger.FromInt64(aa).ToInt64Unchecked());
      aa = unchecked((long)0xfffffff280000000L);
      Assert.AreEqual (
        aa,
        EInteger.FromInt64(aa).ToInt64Unchecked());
      aa = unchecked((long)0xfffffff200000001L);
      Assert.AreEqual (
        aa,
        EInteger.FromInt64(aa).ToInt64Unchecked());
      aa = unchecked((long)0xfffffff27fffffffL);
      Assert.AreEqual (
        aa,
        EInteger.FromInt64(aa).ToInt64Unchecked());
      Assert.AreEqual (
        0x0000000380000001L,
        EInteger.FromInt64(0x0000000380000001L).ToInt64Unchecked());
      Assert.AreEqual (
        0x0000000382222222L,
        EInteger.FromInt64(0x0000000382222222L).ToInt64Unchecked());
      Assert.AreEqual(-8L, EInteger.FromInt64(-8L).ToInt64Unchecked());
      Assert.AreEqual (
        -32768L,
        EInteger.FromInt64(-32768L).ToInt64Unchecked());
      Assert.AreEqual (
        Int32.MinValue,
        EInteger.FromInt64(Int32.MinValue).ToInt64Unchecked());
      Assert.AreEqual (
        Int32.MaxValue,
        EInteger.FromInt64(Int32.MaxValue).ToInt64Unchecked());
      Assert.AreEqual (
        0x80000000L,
        EInteger.FromInt64(0x80000000L).ToInt64Unchecked());
      Assert.AreEqual (
        0x90000000L,
        EInteger.FromInt64(0x90000000L).ToInt64Unchecked());
    }

    [Test]
    public void TestMiscellaneous() {
      Assert.AreEqual(EInteger.One, EInteger.Zero.GetDigitCountAsEInteger());
      var minValue = (EInteger)Int32.MinValue;
      EInteger minValueTimes2 = minValue + (EInteger)minValue;
      Assert.AreEqual(Int32.MinValue, (int)minValue);
      try {
        Console.WriteLine((int)minValueTimes2);
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      EInteger verybig = EInteger.One << 80;
      try {
        Console.WriteLine((int)verybig);
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        Console.WriteLine((long)verybig);
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.PowBigIntVar(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.Pow(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try { (EInteger.Zero - EInteger.One).PowBigIntVar(null);

        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
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
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try { ((EInteger)13).Mod(null);

        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try { ((EInteger)13).Mod((EInteger)(-4));

        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try { ((EInteger)(-13)).Mod((EInteger)(-4));

        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        // NOTE: Intentionally empty
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
        // NOTE: Intentionally empty
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
      EInteger ebits = ei.GetUnsignedBitLengthAsEInteger();
      int bits = ebits.CanFitInInt32() ? ebits.ToInt32Checked() :
        Int32.MaxValue;
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
        ei = (i % 2 == 0) ? ei.ShiftLeft(16).Add(EInteger.FromInt32(
  0xffff)) : ei.ShiftLeft(16);
      }
      return ei;
    }

    [Test]
    [Timeout(5000)]
    public void TestMultiplyDivideSpecific() {
      string

      strParam =

  "D28E831580A0A69BD2259283B7E894A5B766C1FC9C93E776AB78E226A66983788A36C8458A1EAB8DA505CBFBCD41F7A4953CF426CCB884CCFF85B189D2759102C0CCF7A3DE909AE486B38A6DEC0B86FBE95DA041D8FEC163D24D95CEECCDBC7DE2FD88A99CF9A25AB3078E4BBFE3A2BBAD61C53CEA68E40BA3D7D66296C6CE66A6E4DC32E1A0F020DAD8820C9A698282EB5ADDC9CFF8F42ED565";
      {
        EInteger valueEObjectTemp = EInteger.FromRadixString(
          strParam,
          16);
        strParam =

  "E29BE968D480A9FEE535E95FD35DD081868CDF4ED961B2148530A98AD961D4249920AE57AF49E6E1BB50940FD710E5C598249829FA8886C6A63D853BC52CE8D1D2E8B6EF927DC5AF9D14F3AFA2669EC4DAB7FD88F15BACB79149";
        EInteger valueEObjectTemp2 = EInteger.FromRadixString(
          strParam,
          16);
        TestMultiplyDivideOne(valueEObjectTemp, valueEObjectTemp2);
      }
      TestMultiplyDivideOne (
        EInteger.FromRadixString("E6E8FFFFFFFF", 16),
        EInteger.FromRadixString("E6E8FFFFFFFF", 16));
      TestMultiplyDivideOne (
        EInteger.FromRadixString("AE0CFFFFFFFFFFFFFFFF", 16),
        EInteger.FromRadixString("AE0CFFFFFFFFFFFF", 16));
      {
        EInteger ei1 = EInteger.FromRadixString(
          "E6E8FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF",
          16);
        EInteger ei2 = EInteger.FromRadixString("FFFFFFFFFFFFFFFFFFFFFFFF", 16);
        TestMultiplyDivideOne(ei1, ei2);
      }
      TestMultiplyDivideOne (
        EInteger.FromRadixString("83E7FFFFFFFFFFFFFFFF", 16),
        EInteger.FromRadixString("83E7FFFFFFFFFFFF", 16));
      {
        EInteger ei1 = EInteger.FromRadixString(
          "C57DFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF",
          16);
        EInteger ei2 = EInteger.FromRadixString("C57DFFFFFFFFFFFFFFFFFFFF", 16);
        TestMultiplyDivideOne(ei1, ei2);
}
      EInteger eia = EInteger.FromRadixString(
        "C66BE66EC212C77883DAFEB9F73C914BF88E9DEB897CB817EBA7DBC7D0ABEB55A164EAFB9C9A856A8532D901FADC85E7EEC28A329670968AE45AEECDC050F12AA34CBF75B0DC81C588CEE8CDE6138704D73E958DF5FEED5E80C4D86BD0C2D60C8DFCFF72B43BBBF2A3B68760DF35E3F1B1588584971CE9EF983D8678D7C8BB84D196C37585FC8B4FC8F88CDCA65B843F8DBAA4F0F324D003B0AAD4EACA04961EBF63936FFF29F459B0A197D79B38B5B8E31C9E88FA67BD97C2F9DBE8B926D06FF80E8D7AB0D5E7D1C0B2E4DED8FA8EA4E96C9597ABB9F801B9CA8F98F4088990AFB58427A57BBDC983B1",
        16);
      EInteger eib = EInteger.FromRadixString(
        "EB7E892CD29F9B4182F58769C12BD885B7D7DE038074F48ACCAA9F6CFB63D6CCF1D4C603C5A08721F2F3F81FD380F847AE37EEC8FCF39C87A351F816E9D4EDF3B6C9AB0A958FC3FEF04BA3B38D4BF005A29A9D83F8B9F850BB36C9568C99CF3FFFDE9977BFD7D62AF597E4E8D483DE5FF323B0C49732EE23CC4EAA0EEF4AF47FE4BCB0D1C081F315CBE2D892DCA8F3E9A3AFA4CAE67082EBBDC9A59AB82D96009BC5CC8492699F89E21CD8A3F6DE8E86",
        16);
      TestMultiplyDivideOne(eia, eib);
      {
        string str1 =

  "10101000100010101010101000100000101000001010000000000000001000001000001010001010100010100010100000000000101010001000101010000000001000100000101000000000100010100000000010100010000000101010001000101000001000101010100000000010001010001010100010000000101010100000000010100000000010100000100010101010001010100000100000100010100000001010001010101000000000001010000010000010100010100010001010001010001010101000101000001000000000000000000000000010000000101000001000001000000010000010001000101010101000100010100010100000100000101000100010101010100000000000100000101000000000001010101000100000100000001000000000001010101000000010100000100000000010000000001000101010001010001";

        string str2 =

  "101010101000101010100000000010100000101000001010001000000000101000101010000000100010001000100010000000001010101010100010101010000010000000100000100000100010001010001010100000000000100010001000000000001010000000100000000000100010001000101000001010101000000010000010100010100000100010000000001000101010001000001000101000101000000000001010001010001000000000101010100000001000001010000010100010101000001000101000001000000000000010100010100010001010001000001010000010100000000";

        EInteger objectTemp = EInteger.FromRadixString(
          str1,
          16);
        EInteger objectTemp2 = EInteger.FromRadixString(
          str2,
          16);
        TestMultiplyDivideOne(objectTemp, objectTemp2);
      }
      {
        EInteger ei1, ei2;
        ei1 = EInteger.FromString (
  "44461738044811866704570272160729755524383493147516085922742403681586307620758054502667856562873477505768158700319760453047044081412393321568753479912147358343844563186048273758088945022589574729044743021988362306225753942249201773678443992606696524197361479929661991788310321409367753462284203449631729626517511224343015354155975783754763572354740724506742793459644155837703671449155713000260325445046273385372701820583016334341594713806706345456633635125343104401883366671083569152");
        ei2 = EInteger.FromString (
  "6667912688606651657935168942074070387623462798286393292334546164025938697493268465740399785103348978411106010660409247384863031649363973174034406552719188394559243700794785023362300512913065060420313203793021880700852215978918600154969735168");
        TestMultiplyDivideOne(ei1, ei2);
      }
    }

    [Test]
    public void TestMultiplyDivideA() {
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
        EInteger.FromString (
  "5903310052234442839693218602919688229567185544510721229016780853271484375");
      EInteger ei2 = EInteger.FromString("710542735760100185871124267578125");
      {
        string stringTemp = ei1.Subtract(ei2).ToString();
        {
          object objectTemp =
"5903310052234442839693218602919688229566475001774961128830909729003906250";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
}
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
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(37);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(Int32.MinValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.One.ToRadixString(Int32.MaxValue);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
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
          Assert.AreEqual (
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
    [Timeout(100000)]
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
      if (bigintA == null) {
        throw new ArgumentNullException(nameof(bigintA));
      }
      if (bigintB == null) {
        throw new ArgumentNullException(nameof(bigintB));
      }
      // Test that A*B/A = B and A*B/B = A
      try {
        EInteger bigintRem;
        EInteger bigintE;
        EInteger bigintD;
        EInteger bigintC = bigintA.Multiply(bigintB);
        TestCommon.CompareTestEqualAndConsistent (
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
            TestCommon.CompareTestEqualAndConsistent (
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
        TestCommon.CompareTestEqualAndConsistent (
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
        TestCommon.CompareTestEqualAndConsistent (
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
      } catch (Exception ex) {
        string testLine = "TestMultiplyDivideOne (\nEInteger.FromRadixString" +
          "\u0020(\"" + bigintA.ToRadixString(16) +
          "\",16),\nEInteger.FromRadixString(\"" +
          bigintB.ToRadixString(16) + "\",16));";
        Console.WriteLine(testLine);
        throw new InvalidOperationException(ex.Message + "\n" +
          testLine,
          ex);
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
      bi2 = EInteger.FromString("1").ShiftLeft(333330).Subtract(
  EInteger.One);
      r = bi.Divide(bi2);
      EInteger r2 = bi.Divide(bi2);
      Assert.AreEqual(r, r2);
    }

    [Test]
    public void TT() {
      EInteger bi = EInteger.FromString (
          "1").ShiftLeft(742072).Subtract(EInteger.One);
      bi.ToString();
    }
  }
}
