using System;
using System.Text;
using NUnit.Framework;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class EIntegerTest {
    internal static EInteger BigValueOf(long value) {
      return EInteger.FromInt64(value);
    }

    internal static EInteger BigFromString(string str) {
      return EInteger.fromString(str);
    }

    internal static EInteger BigFromBytes(byte[] bytes) {
      return EInteger.FromBytes(bytes, true);
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

    public static void DoTestMultiply(string m1, string m2, string result) {
      EInteger bigintA = BigFromString(m1);
      EInteger bigintB = BigFromString(m2);
      bigintA *= bigintB;
      AssertBigIntegersEqual(result, bigintA);
    }

    public static void DoTestPow(string m1, int m2, string result) {
      EInteger bigintA = BigFromString(m1);
      AssertBigIntegersEqual(result, bigintA.pow(m2));
// #if UNUSED
      AssertBigIntegersEqual(result, bigintA.PowBigIntVar((EInteger)m2));
////#endif
    }

    public static void AssertBigIntegersEqual(string a, EInteger b) {
      Assert.AreEqual(a, b.ToString());
      EInteger a2 = BigFromString(a);
      TestCommon.CompareTestEqualAndConsistent(a2, b);
      TestCommon.AssertEqualsHashCode(a2, b);
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
rembi = divrem[1]; }
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
rembi = divrem[1]; }
        AssertBigIntegersEqual(result, quo);
        AssertBigIntegersEqual(rem, rembi);
      }
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
bigintRem = divrem[1]; }
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
          if (bigintE.Sign > 0 && !bigintC.mod(bigintB).Equals(bigintE)) {
            Assert.Fail("TestMultiplyDivide " + bigintA + "; " + bigintB +
              ";\n" + bigintC);
          }
        }
        if (!bigintA.IsZero) {
          {
EInteger[] divrem = (bigintC).DivRem(bigintA);
bigintD = divrem[0];
bigintRem = divrem[1]; }
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
bigintRem = divrem[1]; }
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

    public static void AssertAdd(EInteger bi, EInteger bi2, string s) {
      EIntegerTest.AssertBigIntegersEqual(s, bi + (EInteger)bi2);
      EIntegerTest.AssertBigIntegersEqual(s, bi2 + (EInteger)bi);
      EInteger negbi = EInteger.Zero - (EInteger)bi;
      EInteger negbi2 = EInteger.Zero - (EInteger)bi2;
      EIntegerTest.AssertBigIntegersEqual(s, bi - (EInteger)negbi2);
      EIntegerTest.AssertBigIntegersEqual(s, bi2 - (EInteger)negbi);
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
    public void TestEquals() {
      Assert.IsFalse(EInteger.One.Equals(null));
      Assert.IsFalse(EInteger.Zero.Equals(null));
      Assert.IsFalse(EInteger.One.Equals(EInteger.Zero));
      Assert.IsFalse(EInteger.Zero.Equals(EInteger.One));
      var r = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        EInteger bigintA = RandomObjects.RandomBigInteger(r);
        EInteger bigintB = RandomObjects.RandomBigInteger(r);
        TestCommon.AssertEqualsHashCode(bigintA, bigintB);
      }
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

    private static void TestGcdPair(
      EInteger biga,
      EInteger bigb,
      EInteger biggcd) {
      EInteger ba = EInteger.GreatestCommonDivisor(biga, bigb);
      EInteger bb = EInteger.GreatestCommonDivisor(bigb, biga);
      Assert.AreEqual(ba, biggcd);
      Assert.AreEqual(bb, biggcd);
    }

    [Test]
    public void TestGcd() {
      try {
 EInteger.Zero.gcd(null);
Assert.Fail("Should have failed");
} catch (ArgumentNullException) {
Console.Write(String.Empty);
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
      {
string stringTemp = EInteger.Zero.gcd(BigFromString(
"244")).ToString();
Assert.AreEqual(
"244",
stringTemp);
}
      {
string stringTemp = EInteger.Zero.gcd(BigFromString(
"-244")).ToString();
Assert.AreEqual(
"244",
stringTemp);
}
      {
string stringTemp = BigFromString(
"244").gcd(EInteger.Zero).ToString();
Assert.AreEqual(
"244",
stringTemp);
}
      {
string stringTemp = BigFromString(
"-244").gcd(EInteger.Zero).ToString();
Assert.AreEqual(
"244",
stringTemp);
}
      {
string stringTemp = EInteger.One.gcd(BigFromString("244")).ToString();
Assert.AreEqual(
"1",
stringTemp);
}
      {
string stringTemp = EInteger.One.gcd(BigFromString(
"-244")).ToString();
Assert.AreEqual(
"1",
stringTemp);
}
      {
string stringTemp = BigFromString("244").gcd(EInteger.One).ToString();
Assert.AreEqual(
"1",
stringTemp);
}
      {
string stringTemp = BigFromString(
"-244").gcd(EInteger.One).ToString();
Assert.AreEqual(
"1",
stringTemp);
}
      {
string stringTemp = BigFromString("15").gcd(BigFromString(
"15")).ToString();
Assert.AreEqual(
"15",
stringTemp);
}
      {
string stringTemp = BigFromString("-15").gcd(
        BigFromString("15")).ToString();
Assert.AreEqual(
"15",
stringTemp);
}
      {
string stringTemp = BigFromString("15").gcd(
        BigFromString("-15")).ToString();
Assert.AreEqual(
"15",
stringTemp);
}
      {
string stringTemp = BigFromString(
"-15").gcd(BigFromString("-15")).ToString();
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
    public void TestGetLowBit() {
      // not implemented yet
    }
    [Test]
    public void TestGetUnsignedBitLength() {
      // not implemented yet
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
    public void TestIsEven() {
      var r = new FastRandom();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        EInteger mod = bigintA.Remainder(BigValueOf(2));
        Assert.AreEqual(mod.IsZero, bigintA.IsEven);
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
    public void TestMod() {
      try {
        EInteger.One.mod(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ((EInteger)13).mod(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ((EInteger)13).mod((EInteger)(-4));
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ((EInteger)(-13)).mod((EInteger)(-4));
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
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
    public void TestIsPowerOfTwo() {
      // not implemented yet
    }
    [Test]
    public void TestIsZero() {
      // not implemented yet
    }

////#if true

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
    public void TestBitLength() {
      Assert.AreEqual(31, BigValueOf(-2147483647L).bitLength());
      Assert.AreEqual(31, BigValueOf(-2147483648L).bitLength());
      Assert.AreEqual(32, BigValueOf(-2147483649L).bitLength());
      Assert.AreEqual(32, BigValueOf(-2147483650L).bitLength());
      Assert.AreEqual(31, BigValueOf(2147483647L).bitLength());
      Assert.AreEqual(32, BigValueOf(2147483648L).bitLength());
      Assert.AreEqual(32, BigValueOf(2147483649L).bitLength());
      Assert.AreEqual(32, BigValueOf(2147483650L).bitLength());
      Assert.AreEqual(0, BigValueOf(0).bitLength());
      Assert.AreEqual(1, BigValueOf(1).bitLength());
      Assert.AreEqual(2, BigValueOf(2).bitLength());
      Assert.AreEqual(2, BigValueOf(2).bitLength());
      Assert.AreEqual(31, BigValueOf(Int32.MaxValue).bitLength());
      Assert.AreEqual(31, BigValueOf(Int32.MinValue).bitLength());
      Assert.AreEqual(16, BigValueOf(65535).bitLength());
      Assert.AreEqual(16, BigValueOf(-65535).bitLength());
      Assert.AreEqual(17, BigValueOf(65536).bitLength());
      Assert.AreEqual(16, BigValueOf(-65536).bitLength());
      Assert.AreEqual(
        65,
        BigFromString("19084941898444092059").bitLength());
      Assert.AreEqual(
        65,
        BigFromString("-19084941898444092059").bitLength());
      Assert.AreEqual(0, BigValueOf(-1).bitLength());
      Assert.AreEqual(1, BigValueOf(-2).bitLength());
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
    public void TestGetDigitCount() {
      var r = new FastRandom();
      for (var i = 0; i < 1000; ++i) {
        EInteger bigintA = RandomBigInteger(r);
        String str = bigintA.Abs().ToString();
        Assert.AreEqual(str.Length, bigintA.getDigitCount());
      }
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
    public void TestTestBit() {
      Assert.IsFalse(EInteger.Zero.testBit(0));
      Assert.IsFalse(EInteger.Zero.testBit(1));
      Assert.IsTrue(EInteger.One.testBit(0));
      Assert.IsFalse(EInteger.One.testBit(1));
      for (int i = 0; i < 32; ++i) {
        Assert.IsTrue(BigValueOf(-1).testBit(i));
      }
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

////#endif
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
    public void TestPowBigIntVar() {
      // not implemented yet
    }
    [Test]
    public void TestRemainder() {
      DoTestRemainder("2472320648", "2831812081", "2472320648");
      DoTestRemainder("-2472320648", "2831812081", "-2472320648");
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

////#if true
    [Test]
    public void TestMiscellaneous() {
      Assert.AreEqual(1, EInteger.Zero.getDigitCount());
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
        EInteger.One.pow(-1);
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
        EInteger.Zero.testBit(-1);
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
        EInteger.One.mod((EInteger)(-1));
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
        EInteger.One.mod(EInteger.Zero);
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
      Assert.AreEqual(EInteger.One, ((EInteger)13).mod((EInteger)4));
      Assert.AreEqual((EInteger)3, ((EInteger)(-13)).mod((EInteger)4));
    }

////#endif
  }
}
