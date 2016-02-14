using System;
using NUnit.Framework;
using PeterO.Numbers;
using System.Text;

namespace Test {
  [TestFixture]
  public class EFloatTest {
    public static EFloat FromBinary(string str) {
      var smallExponent = 0;
      var index = 0;
      EInteger ret = EInteger.Zero;
      while (index < str.Length) {
        if (str[index] == '0') {
          ++index;
        } else {
          break;
        }
      }
      while (index < str.Length) {
        if (str[index] == '.') {
          ++index;
          break;
        }
        if (str[index] == '1') {
          ++index;
          if (ret.IsZero) {
            ret = EInteger.One;
          } else {
            ret <<= 1;
            ret = ret.Add(EInteger.One);
          }
        } else if (str[index] == '0') {
          ++index;
          ret <<= 1;
          continue;
        } else {
          break;
        }
      }
      while (index < str.Length) {
        if (str[index] == '1') {
          ++index;
          --smallExponent;
          if (ret.IsZero) {
            ret = EInteger.One;
          } else {
            ret <<= 1;
            ret = ret.Add(EInteger.One);
          }
        } else if (str[index] == '0') {
          ++index;
          --smallExponent;
          ret <<= 1;
          continue;
        } else {
          break;
        }
      }
      return EFloat.Create(ret, (EInteger)smallExponent);
    }

    [Test]
    public void TestAbs() {
      // not implemented yet
    }

    [Test]
    public void TestAdd() {
      try {
        EFloat.Zero.Add(null, EContext.Unlimited);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var fr = new FastRandom();
      TestAddCloseExponent(fr, 0);
      TestAddCloseExponent(fr, 100);
      TestAddCloseExponent(fr, -100);
      TestAddCloseExponent(fr, Int32.MinValue);
      TestAddCloseExponent(fr, Int32.MaxValue);
    }
    [Test]
    public void TestCompareTo() {
      var r = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        EFloat bigintA = RandomObjects.RandomEFloat(r);
        EFloat bigintB = RandomObjects.RandomEFloat(r);
        EFloat bigintC = RandomObjects.RandomEFloat(r);
        TestCommon.CompareTestRelations(bigintA, bigintB, bigintC);
      }
      TestCommon.CompareTestLess(EFloat.Zero, EFloat.NaN);
      EDecimal a = EDecimal.FromString(
        "7.00468923842476447758037175245551511770928808756622205663208" + "4784688080253355047487262563521426272927783429622650146484375");
      EDecimal b = EDecimal.FromString("5");
      TestCommon.CompareTestLess(b, a);
    }
    [Test]
    public void TestCompareToSignal() {
      // not implemented yet
    }
    [Test]
    public void TestCompareToWithContext() {
      // not implemented yet
    }
    [Test]
    public void TestCreate() {
      // not implemented yet
    }
    [Test]
    public void TestCreateNaN() {
      // not implemented yet
    }
    [Test]
    public void TestDivide() {
      try {
        EFloat.FromString("1").Divide(EFloat.FromString("3"), null);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      {
string stringTemp = EFloat.FromString(
"1").Divide(EFloat.FromInt32(8)).ToString();
Assert.AreEqual(
"0.125",
stringTemp);
}
      {
string stringTemp = EFloat.FromString(
"10").Divide(EFloat.FromInt32(80)).ToString();
Assert.AreEqual(
"0.125",
stringTemp);
}
      {
string stringTemp = EFloat.FromString(
"10000").Divide(EFloat.FromInt32(80000)).ToString();
Assert.AreEqual(
"0.125",
stringTemp);
}
      {
string stringTemp = EFloat.FromString(
"1000").Divide(EFloat.FromInt32(8)).ToString();
Assert.AreEqual(
"125",
stringTemp);
}
      {
string stringTemp = EFloat.FromString(
"1").Divide(EFloat.FromInt32(256)).ToString();
Assert.AreEqual(
"0.00390625",
stringTemp);
}
      var fr = new FastRandom();
      for (var i = 0; i < 5000; ++i) {
        EFloat ed1 = RandomObjects.RandomEFloat(fr);
        EFloat ed2 = RandomObjects.RandomEFloat(fr);
        if (!ed1.IsFinite || !ed2.IsFinite) {
          continue;
        }
        EFloat ed3 = ed1.Multiply(ed2);
        Assert.IsTrue(ed3.IsFinite);
        EFloat ed4;
        ed4 = ed3.Divide(ed1);
        if (!ed1.IsZero) {
          TestCommon.CompareTestEqual(ed4, ed2);
        } else {
          Assert.IsTrue(ed4.IsNaN());
        }
        ed4 = ed3.Divide(ed2);
        if (!ed2.IsZero) {
          TestCommon.CompareTestEqual(ed4, ed1);
        } else {
          Assert.IsTrue(ed4.IsNaN());
        }
      }
    }
    [Test]
    public void TestDivideToExponent() {
      // not implemented yet
    }
    [Test]
    public void TestDivideToIntegerNaturalScale() {
      // not implemented yet
    }
    [Test]
    public void TestDivideToIntegerZeroScale() {
      // not implemented yet
    }
    [Test]
    public void TestDivideToSameExponent() {
      // not implemented yet
    }
    [Test]
    public void TestEquals() {
      var r = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        EFloat bigintA = RandomObjects.RandomEFloat(r);
        EFloat bigintB = RandomObjects.RandomEFloat(r);
        TestCommon.AssertEqualsHashCode(bigintA, bigintB);
      }
    }
    [Test]
    public void TestEqualsInternal() {
      // not implemented yet
    }
    [Test]
    public void TestExp() {
      // not implemented yet
    }
    [Test]
    public void TestExponent() {
      // not implemented yet
    }

    [Test]
    public void TestEFloatDouble() {
      TestEFloatDoubleCore(3.5, "3.5");
      TestEFloatDoubleCore(7, "7");
      TestEFloatDoubleCore(1.75, "1.75");
      TestEFloatDoubleCore(3.5, "3.5");
      TestEFloatDoubleCore((double)Int32.MinValue, "-2147483648");
      TestEFloatDoubleCore(
        (double)Int64.MinValue,
        "-9223372036854775808");
      var rand = new FastRandom();
      for (var i = 0; i < 2047; ++i) {
        // Try a random double with a given
        // exponent
        TestEFloatDoubleCore(RandomObjects.RandomDouble(rand, i), null);
        TestEFloatDoubleCore(RandomObjects.RandomDouble(rand, i), null);
        TestEFloatDoubleCore(RandomObjects.RandomDouble(rand, i), null);
        TestEFloatDoubleCore(RandomObjects.RandomDouble(rand, i), null);
      }
    }
    [Test]
    public void TestEFloatSingle() {
      var rand = new FastRandom();
      for (var i = 0; i < 255; ++i) {
        // Try a random float with a given
        // exponent
        TestEFloatSingleCore(RandomObjects.RandomSingle(rand, i), null);
        TestEFloatSingleCore(RandomObjects.RandomSingle(rand, i), null);
        TestEFloatSingleCore(RandomObjects.RandomSingle(rand, i), null);
        TestEFloatSingleCore(RandomObjects.RandomSingle(rand, i), null);
      }
    }

    [Test]
    public void TestFloatDecimalSpecific() {
      string str =

  "874952453585761710286297571153092638434027760916318352601207433388312948219720355694692773665688395541653.74728887385887787786487024277448654174804687500"
;
      EDecimal ed = EDecimal.FromString(str);
      EFloat ef2 = ed.ToEFloat();
      Assert.AreEqual(0, ed.CompareToBinary(ef2), ef2.ToString());
    }

    [Test]
    public void TestFloatDecimalRoundTrip() {
      var r = new FastRandom();
      for (var i = 0; i < 5000; ++i) {
        EFloat ef = RandomObjects.RandomEFloat(r);
        EDecimal ed = ef.ToEDecimal();
          EFloat ef2 = ed.ToEFloat();
          // Tests that values converted from float to decimal and
          // back have the same numerical value
          TestCommon.CompareTestEqual(ef, ef2);
      }
    }
    [Test]
    public void TestFromBigInteger() {
      // not implemented yet
    }
    [Test]
    public void TestFromDouble() {
      // not implemented yet
    }

    [Test]
    public void TestFromInt32() {
      // not implemented yet
    }
    [Test]
    public void TestFromInt64() {
      // not implemented yet
    }
    [Test]
    public void TestFromSingle() {
      // not implemented yet
    }
    [Test]
    public void TestFromString() {
      try {
        EFloat.FromString("0..1");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("0.1x+222");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("0.1g-222");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("2", 0, 1, null);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(String.Empty);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(EFloat.Zero, EFloat.FromString("0"));
      Assert.AreEqual(EFloat.Zero, EFloat.FromString("0", null));
      try {
        EFloat.FromString(null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(String.Empty);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(null, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", -1, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", 2, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", 0, -1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", 0, 2);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", 1, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(null, 0, 1, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", -1, 1, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", 2, 1, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", 0, -1, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", 0, 2, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("x", 1, 1, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(
          "Infinity",
          EContext.Unlimited.WithSimplified(true));
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(
          "-Infinity",
          EContext.Unlimited.WithSimplified(true));
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(
          "NaN",
          EContext.Unlimited.WithSimplified(true));
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(
          "sNaN",
          EContext.Unlimited.WithSimplified(true));
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(
          "Infinity",
          EContext.Unlimited.WithSimplified(true));
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(
          "-Infinity",
          EContext.Unlimited.WithSimplified(true));
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(
          "NaN",
          EContext.Unlimited.WithSimplified(true));
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(
          "sNaN",
          EContext.Unlimited.WithSimplified(true));
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestIsFinite() {
      // not implemented yet
    }

    [Test]
    public void TestIsInfinity() {
      Assert.IsTrue(EFloat.PositiveInfinity.IsInfinity());
      Assert.IsTrue(EFloat.NegativeInfinity.IsInfinity());
      Assert.IsFalse(EFloat.Zero.IsInfinity());
      Assert.IsFalse(EFloat.NaN.IsInfinity());
    }
    [Test]
    public void TestIsNaN() {
      Assert.IsFalse(EFloat.PositiveInfinity.IsNaN());
      Assert.IsFalse(EFloat.NegativeInfinity.IsNaN());
      Assert.IsFalse(EFloat.Zero.IsNaN());
      Assert.IsTrue(EFloat.NaN.IsNaN());
    }
    [Test]
    public void TestIsNegative() {
      // not implemented yet
    }
    [Test]
    public void TestIsNegativeInfinity() {
      // not implemented yet
    }
    [Test]
    public void TestIsPositiveInfinity() {
      // not implemented yet
    }
    [Test]
    public void TestIsQuietNaN() {
      // not implemented yet
    }
    [Test]
    public void TestIsSignalingNaN() {
      // not implemented yet
    }
    [Test]
    public void TestIsZero() {
      Assert.IsFalse(EFloat.NaN.IsZero);
      Assert.IsFalse(EFloat.SignalingNaN.IsZero);
    }
    [Test]
    public void TestLog() {
      Assert.IsTrue(EFloat.One.Log(null).IsNaN());
      Assert.IsTrue(EFloat.One.Log(EContext.Unlimited).IsNaN());
    }
    [Test]
    public void TestLog10() {
      Assert.IsTrue(EFloat.One.Log10(null).IsNaN());
      Assert.IsTrue(EFloat.One.Log10(EContext.Unlimited)
              .IsNaN());
    }
    [Test]
    public void TestMantissa() {
      // not implemented yet
    }
    [Test]
    public void TestMax() {
      try {
        EFloat.Max(null, EFloat.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.Max(EFloat.One, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var r = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        EFloat bigintA = RandomObjects.RandomEFloat(r);
        EFloat bigintB = RandomObjects.RandomEFloat(r);
        if (!bigintA.IsFinite || !bigintB.IsFinite) {
          continue;
        }
        int cmp = TestCommon.CompareTestReciprocal(bigintA, bigintB);
        if (cmp < 0) {
          TestCommon.CompareTestEqual(
     bigintB,
     EFloat.Max(bigintA, bigintB));
        } else if (cmp > 0) {
          TestCommon.CompareTestEqual(
     bigintA,
     EFloat.Max(bigintA, bigintB));
        } else {
          TestCommon.CompareTestEqual(
     bigintA,
     EFloat.Max(bigintA, bigintB));
          TestCommon.CompareTestEqual(
     bigintB,
     EFloat.Max(bigintA, bigintB));
        }
      }
    }
    [Test]
    public void TestMaxMagnitude() {
      try {
        EFloat.MaxMagnitude(null, EFloat.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.MaxMagnitude(EFloat.One, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestMin() {
      try {
        EFloat.Min(null, EFloat.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.Min(EFloat.One, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      var r = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        EFloat bigintA = RandomObjects.RandomEFloat(r);
        EFloat bigintB = RandomObjects.RandomEFloat(r);
        if (!bigintA.IsFinite || !bigintB.IsFinite) {
          continue;
        }
        int cmp = TestCommon.CompareTestReciprocal(bigintA, bigintB);
        if (cmp < 0) {
          TestCommon.CompareTestEqual(
     bigintA,
     EFloat.Min(bigintA, bigintB));
        } else if (cmp > 0) {
          TestCommon.CompareTestEqual(
     bigintB,
     EFloat.Min(bigintA, bigintB));
        } else {
          TestCommon.CompareTestEqual(
     bigintA,
     EFloat.Min(bigintA, bigintB));
          TestCommon.CompareTestEqual(
     bigintB,
     EFloat.Min(bigintA, bigintB));
        }
      }
    }
    [Test]
    public void TestMinMagnitude() {
      try {
        EFloat.MinMagnitude(null, EFloat.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.MinMagnitude(EFloat.One, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestMovePointLeft() {
      EFloat ef;
      EFloat ef2;
      ef = EFloat.FromInt32(0x150).MovePointLeft(4);
      ef2 = EFloat.FromInt32(0x15);
      Assert.AreEqual(0, ef.CompareTo(ef2));
    }
    [Test]
    public void TestMovePointRight() {
      EFloat ef;
      EFloat ef2;
      ef = EFloat.FromInt32(0x100).MovePointRight(4);
      ef2 = EFloat.FromInt32(0x1000);
      Assert.AreEqual(0, ef.CompareTo(ef2));
    }
    [Test]
    public void TestMultiply() {
      // not implemented yet
    }
    [Test]
    public void TestMultiplyAndAdd() {
      // not implemented yet
    }
    [Test]
    public void TestMultiplyAndSubtract() {
      // not implemented yet
    }
    [Test]
    public void TestNegate() {
      // not implemented yet
    }
    [Test]
    public void TestNextMinus() {
      // not implemented yet
    }
    [Test]
    public void TestNextPlus() {
      // not implemented yet
    }
    [Test]
    public void TestNextToward() {
      // not implemented yet
    }

    private static string[] valueFPIntegers = { "1", "2", "4", "8",
      "281474976710656", "562949953421312", "1125899906842624",
      "2251799813685248", "4503599627370496", "9007199254740992",
      "18014398509481984", "36028797018963968", "72057594037927936",
      "144115188075855872", "288230376151711744",

  "11235582092889474423308157442431404585112356118389416079589380072358292237843810195794279832650471001320007117491962084853674360550901038905802964414967132773610493339054092829768888725077880882465817684505312860552384417646403930092119569408801702322709406917786643639996702871154982269052209770601514008576"
,

  "22471164185778948846616314884862809170224712236778832159178760144716584475687620391588559665300942002640014234983924169707348721101802077811605928829934265547220986678108185659537777450155761764931635369010625721104768835292807860184239138817603404645418813835573287279993405742309964538104419541203028017152"
,

  "44942328371557897693232629769725618340449424473557664318357520289433168951375240783177119330601884005280028469967848339414697442203604155623211857659868531094441973356216371319075554900311523529863270738021251442209537670585615720368478277635206809290837627671146574559986811484619929076208839082406056034304"
,

  "89884656743115795386465259539451236680898848947115328636715040578866337902750481566354238661203768010560056939935696678829394884407208311246423715319737062188883946712432742638151109800623047059726541476042502884419075341171231440736956555270413618581675255342293149119973622969239858152417678164812112068608"
      };

    private static int[] valueFPIntegersExp = { 0, 1, 2, 3, 48, 49, 50, 51, 52,
      53, 54, 55, 56, 57, 58, 1020, 1021, 1022, 1023 };

    [Test]
    public void TestFPDoubles() {
      for (var i = 0; i < valueFPIntegersExp.Length; ++i) {
        // Positive
        EFloat ef = EFloat.Create(1, valueFPIntegersExp[i]);
        Assert.AreEqual(valueFPIntegers[i], ef.ToString());
        ef = EFloat.FromDouble(ef.ToDouble());
        Assert.AreEqual(valueFPIntegers[i], ef.ToString());
        ef = EFloat.FromDouble(
          EDecimal.FromString(valueFPIntegers[i]).ToDouble());
        Assert.AreEqual(valueFPIntegers[i], ef.ToString());
        // Negative
        ef = EFloat.Create(-1, valueFPIntegersExp[i]);
        Assert.AreEqual("-" + valueFPIntegers[i], ef.ToString());
        ef = EFloat.FromDouble(ef.ToDouble());
        Assert.AreEqual("-" + valueFPIntegers[i], ef.ToString());
        ef = EFloat.FromDouble(
          EDecimal.FromString("-" + valueFPIntegers[i]).ToDouble());
        Assert.AreEqual("-" + valueFPIntegers[i], ef.ToString());
      }
      for (var i = -1074; i < 1024; ++i) {
        string intstr = TestCommon.IntToString(i);
        // Positive
        EFloat ef = EFloat.Create(1, i);
        string fpstr = ef.ToString();
        ef = EFloat.FromDouble(ef.ToDouble());
        Assert.AreEqual(fpstr, ef.ToString(), intstr);
        ef = EFloat.FromDouble(
          EDecimal.FromString(fpstr).ToDouble());
        Assert.AreEqual(fpstr, ef.ToString(), intstr);
        // Negative
        ef = EFloat.Create(-1, i);
        Assert.AreEqual("-" + fpstr, ef.ToString(), intstr);
        ef = EFloat.FromDouble(ef.ToDouble());
        Assert.AreEqual("-" + fpstr, ef.ToString(), intstr);
        ef = EFloat.FromDouble(
          EDecimal.FromString("-" + fpstr).ToDouble());
        Assert.AreEqual("-" + fpstr, ef.ToString(), intstr);
      }
      EFloat ef2 = EFloat.Create(1, 1024);
      Assert.IsTrue(Double.IsInfinity(ef2.ToDouble()));
      ef2 = EFloat.Create(-1, 1024);
      Assert.IsTrue(Double.IsInfinity(ef2.ToDouble()));
      ef2 = EFloat.Create(1, -1075);
      Assert.IsTrue(EFloat.FromDouble(ef2.ToDouble()).IsZero);
      ef2 = EFloat.Create(-1, -1075);
      Assert.IsTrue(EFloat.FromDouble(ef2.ToDouble()).IsZero);
    }

    [Test]
    public void TestPI() {
      // not implemented yet
    }

    [Test]
    public void TestPlus() {
      Assert.AreEqual(
EFloat.Zero,
EFloat.NegativeZero.Plus(EContext.Basic));
      Assert.AreEqual(
EFloat.Zero,
EFloat.NegativeZero.Plus(null));
    }
    [Test]
    public void TestPow() {
      // not implemented yet
    }
    [Test]
    public void TestQuantize() {
      // not implemented yet
    }
    [Test]
    public void TestReduce() {
      // not implemented yet
    }
    [Test]
    public void TestRemainder() {
      // not implemented yet
    }
    [Test]
    public void TestRemainderNaturalScale() {
      // not implemented yet
    }
    [Test]
    public void TestRemainderNear() {
      // not implemented yet
    }
    [Test]
    public void TestRoundToBinaryPrecision() {
      // not implemented yet
    }
    [Test]
    public void TestRoundToExponent() {
      // not implemented yet
    }
    [Test]
    public void TestRoundToExponentExact() {
      // not implemented yet
    }
    [Test]
    public void TestRoundToIntegralExact() {
      // not implemented yet
    }
    [Test]
    public void TestRoundToIntegralNoRoundedFlag() {
      // not implemented yet
    }
    [Test]
    public void TestRoundToPrecision() {
      // not implemented yet
    }
    [Test]
    public void TestSign() {
      // not implemented yet
    }
    [Test]
    public void TestSquareRoot() {
      // not implemented yet
    }
    [Test]
    public void TestSubtract() {
      try {
        EFloat.Zero.Subtract(null, EContext.Unlimited);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestToDouble() {
      // not implemented yet
    }
    [Test]
    public void TestToEInteger() {
      try {
        EFloat.PositiveInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.NegativeInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.NaN.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.SignalingNaN.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      EFloat flo = EFloat.Create(999, -1);
      try {
        flo.ToEInteger();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.PositiveInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.NegativeInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    public EFloat RandomDoubleEFloat(FastRandom rnd) {
      return this.RandomDoubleEFloat(rnd, false);
    }

    public EFloat RandomDoubleEFloat(FastRandom rnd, bool subnormal) {
      var sb = new StringBuilder();
      if (rnd.NextValue(2) == 0) {
        sb.Append('-');
      }
      sb.Append(subnormal ? '0' : '1');
      var subSize = 52;
      int[] oneChances = { 98, 2, 50, 50, 50 };
      int oneChance = oneChances[rnd.NextValue(oneChances.Length)];
      if (subnormal) {
        subSize = rnd.NextValue(51);
      }
      for (var i = 0; i < 52; ++i) {
        sb.Append(((i < 52 - subSize) || (rnd.NextValue(100) >= oneChance)) ?
          '0' : '1');
      }
      string valueSbString = sb.ToString();
      int expo = 0, exponent;
      if (subnormal) {
        exponent = -1074;
      } else {
        expo = rnd.NextValue(2045) + 1 - 1023;
        exponent = expo - 52;
      }
      var valueEiExponent = (EInteger)exponent;
      EFloat ef = EFloat.Create(
        EInteger.FromRadixString(valueSbString, 2),
        valueEiExponent);
      return ef;
    }

    public EFloat RandomSingleEFloat(FastRandom rnd) {
      return this.RandomSingleEFloat(rnd, false);
    }

    public EFloat RandomSingleEFloat(FastRandom rnd, bool subnormal) {
      var sb = new StringBuilder();
      if (rnd.NextValue(2) == 0) {
        sb.Append('-');
      }
      sb.Append(subnormal ? '0' : '1');
      var subSize = 23;
      int[] oneChances = { 98, 2, 50, 50, 50 };
      int oneChance = oneChances[rnd.NextValue(oneChances.Length)];
      if (subnormal) {
        subSize = rnd.NextValue(22);
      }
      for (var i = 0; i < 23; ++i) {
        sb.Append(((i < 23 - subSize) || (rnd.NextValue(100) >= oneChance)) ?
          '0' : '1');
      }
      string valueSbString = sb.ToString();
      int expo = 0, exponent;
      if (subnormal) {
        exponent = -149;
      } else {
        expo = rnd.NextValue(252) + 1 - 127;
        exponent = expo - 23;
      }
      var valueEiExponent = (EInteger)exponent;
      EFloat ef = EFloat.Create(
        EInteger.FromRadixString(valueSbString, 2),
        valueEiExponent);
      return ef;
    }

    public static string OutputDouble(double dbl) {
      EFloat ef = EFloat.FromDouble(dbl);
      return dbl + " [" + ef.Mantissa.Abs().ToRadixString(2) +
        "," + ef.Exponent + "]";
    }

    public static string OutputSingle(float flt) {
      EFloat ef = EFloat.FromSingle(flt);
      return flt + " [" + ef.Mantissa.Abs().ToRadixString(2) +
        "," + ef.Exponent + "]";
    }

    public static string OutputEF(EFloat ef) {
      return ef.ToDouble() + " [" + ef.Mantissa.Abs().ToRadixString(2) +
        "," + ef.Exponent + "]";
    }

    public static void TestDoubleRounding(
EFloat expected,
EFloat input,
EFloat src) {
      if (!input.IsFinite || !expected.IsFinite) {
        return;
      }
      double expectedDouble = expected.ToDouble();
      if (Double.IsInfinity(expectedDouble)) {
        return;
      }
      string str = input.ToString();
      if (input.ToDouble() != expectedDouble) {
  string msg =
  "\nexpectedDbl " + OutputDouble(expectedDouble) +
  ",\ngot----- " +
        OutputDouble(input.ToDouble()) +"\nsrc-----=" + OutputEF(src) +
        "\nexpected=" + OutputEF(expected) +"\ninput---=" + OutputEF(input);
        Assert.Fail(msg);
      }
      double inputDouble = EDecimal.FromString(str).ToDouble();
      if (inputDouble != expectedDouble) {
  string msg = "\nexpectedDbl " + OutputDouble(expectedDouble) +
          ",\ngot----- " +
        OutputDouble(inputDouble) +"\nsrc-----=" + OutputEF(src) +
        "\nexpected=" + OutputEF(expected) +"\ninput---=" + OutputEF(input);
        Assert.Fail(msg);
      }
    }

    public static void TestSingleRounding(
EFloat expected,
EFloat input,
EFloat src) {
      if (!input.IsFinite || !expected.IsFinite) {
        return;
      }
      float expectedSingle = expected.ToSingle();
      if (Single.IsInfinity(expectedSingle)) {
        return;
      }
      string str = input.ToString();
      if (input.ToSingle() != expectedSingle) {
        string msg = "\nexpectedDbl " + OutputSingle(expectedSingle) +
        ",\ngot----- " +
              OutputSingle(input.ToSingle()) + "\nsrc-----=" + OutputEF(src) +
          "\nexpected=" + OutputEF(expected) + "\ninput---=" +
                OutputEF(input);
        Assert.Fail(msg);
      }
      float inputSingle = EDecimal.FromString(str).ToSingle();
      if (inputSingle != expectedSingle) {
        string msg = "\nexpectedDbl " + OutputSingle(expectedSingle) +
                ",\ngot----- " +
              OutputSingle(inputSingle) + "\nsrc-----=" + OutputEF(src) +
          "\nexpected=" + OutputEF(expected) + "\ninput---=" +
                OutputEF(input);
        Assert.Fail(msg);
      }
    }

    private static EFloat quarter = EFloat.FromString("0.25");
    private static EFloat half = EFloat.FromString("0.5");
    private static EFloat threequarter = EFloat.FromString("0.75");

    private static void TestToFloatRoundingOne(EFloat efa, bool dbl) {
      bool isEven = efa.UnsignedMantissa.IsEven;
      EFloat efprev = efa.NextMinus(dbl ? EContext.Binary64 :
        EContext.Binary32);
      EFloat efnext = efa.NextPlus(dbl ? EContext.Binary64 :
        EContext.Binary32);
      EFloat efnextgap = efnext.Subtract(efa);
      EFloat efprevgap = efa.Subtract(efprev);
      // Console.WriteLine("efa___=" + OutputEF(efa));
      // Console.WriteLine("efprev=" + OutputEF(efprev));
      // Console.WriteLine("efnext=" + OutputEF(efnext));
      EFloat efprev1q = efprev.Add(
     efprevgap.Multiply(quarter));
   EFloat efprev2q = efprev.Add(
     efprevgap.Multiply(half));
   EFloat efprev3q = efprev.Add(
     efprevgap.Multiply(threequarter));
      EFloat efnext1q = efa.Add(efnextgap.Multiply(quarter));
      EFloat efnext2q = efa.Add(efnextgap.Multiply(half));
      EFloat efnext3q = efa.Add(efnextgap.Multiply(threequarter));
      if (dbl) {
        TestDoubleRounding(efprev, efprev, efa);
        TestDoubleRounding(efprev, efprev1q, efa);
        TestDoubleRounding(isEven ? efa : efprev, efprev2q, efa);
        TestDoubleRounding(efa, efprev3q, efa);
        TestDoubleRounding(efa, efa, efa);
        TestDoubleRounding(efa, efnext1q, efa);
        TestDoubleRounding(isEven ? efa : efnext, efnext2q, efa);
        TestDoubleRounding(efnext, efnext3q, efa);
        TestDoubleRounding(efnext, efnext, efa);
      } else {
        TestSingleRounding(efprev, efprev, efa);
        TestSingleRounding(efprev, efprev1q, efa);
        TestSingleRounding(isEven ? efa : efprev, efprev2q, efa);
        TestSingleRounding(efa, efprev3q, efa);
        TestSingleRounding(efa, efa, efa);
        TestSingleRounding(efa, efnext1q, efa);
        TestSingleRounding(isEven ? efa : efnext, efnext2q, efa);
        TestSingleRounding(efnext, efnext3q, efa);
        TestSingleRounding(efnext, efnext, efa);
      }
    }

    private static string EFToString(EFloat ef) {
      return "[" + ef.Mantissa.ToRadixString(2) +"," +
        ef.Mantissa.GetUnsignedBitLength() + "," + ef.Exponent + "]";
    }

    private static void TestBinaryToDecimal(
string input,
int digits,
string expected,
string msg) {
    EContext ec = EContext.ForPrecisionAndRounding(
digits,
ERounding.HalfEven);
      string str = EFloat.FromString(input, EContext.Binary64)
          .ToEDecimal().RoundToPrecision(ec).ToString();
      TestCommon.CompareTestEqual(
       EDecimal.FromString(expected),
       EDecimal.FromString(str),
       msg);
    }

    [Test]
    public void TestBinaryDecimalLine() {
TestBinaryToDecimal(
"9.5673250588722716156829968E22",
12,
"9.56732505887E22",
String.Empty);
    }

    [Test]
    public void TestToShortestString() {
      {
string stringTemp = EFloat.FromSingle(0.1f).ToShortestString(EContext.Binary32);
Assert.AreEqual(
"0.1",
stringTemp);
}
      {
string stringTemp = EFloat.NegativeZero.ToShortestString(EContext.Binary32);
Assert.AreEqual(
"-0",
stringTemp);
}
      {
string stringTemp = EFloat.FromDouble(0.1).ToShortestString(EContext.Binary64);
Assert.AreEqual(
"0.1",
stringTemp);
}
      {
string stringTemp = EFloat.FromString(
"100").ToShortestString(EContext.Binary64);
Assert.AreEqual(
"100",
stringTemp);
}
      {
string stringTemp = EFloat.FromString(
"1000").ToShortestString(EContext.Binary64);
Assert.AreEqual(
"1000",
stringTemp);
}
      {
string stringTemp = EFloat.FromString(
"1000000").ToShortestString(EContext.Binary64);
Assert.AreEqual(
"1000000",
stringTemp);
}
      {
string stringTemp = EFloat.FromString(
"10000000").ToShortestString(EContext.Binary64);
Assert.AreEqual(
"1E+7",
stringTemp);
}
      {
string stringTemp = EFloat.FromString(
"10000000000").ToShortestString(EContext.Binary64);
Assert.AreEqual(
"1E+10",
stringTemp);
}
      {
string stringTemp =
  EFloat.FromDouble(199999d).ToShortestString(EContext.Binary64);
Assert.AreEqual(
"199999",
stringTemp);
}
      var fr = new FastRandom();
      for (var i = 0; i < 10000; ++i) {
        EFloat efa = this.RandomDoubleEFloat(fr);
        string shortestStr = efa.ToShortestString(EContext.Binary64);
        EFloat shortest = EFloat.FromString(
          shortestStr,
          EContext.Binary64);
     string msg = "\n" + EFToString(efa) + "\n" + EFToString(shortest) +
          "\n" + shortestStr;
        TestCommon.CompareTestEqual(
          efa,
          shortest,
          msg);
      }
    }
    [Test]
    public void TestToSingleRounding() {
      var fr = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        EFloat efa = this.RandomSingleEFloat(fr, i >= 250);
        TestToFloatRoundingOne(efa, false);
      }
    }
    [Test]
    public void TestToDoubleRounding() {
      var fr = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        EFloat efa = this.RandomDoubleEFloat(fr, i >= 250);
        TestToFloatRoundingOne(efa, true);
      }
      TestToFloatRoundingOne(EFloat.Create(0, -1074), true);
      TestToFloatRoundingOne(EFloat.Create(
  EInteger.FromRadixString("10000000000000000000000000000000000000000000000000000", 2),
  EInteger.FromInt32(-1074)), true);
      {
EFloat objectTemp = EFloat.Create(
  EInteger.FromRadixString("-10000000000000000000000000000000000000000000000000000", 2),
  EInteger.FromInt32(-1074));
TestToFloatRoundingOne(objectTemp, true);
}
    }

    [Test]
    public void TestToEIntegerExact() {
      EFloat flo = EFloat.Create(999, -1);
      try {
        flo.ToEIntegerExact();
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestToEngineeringString() {
      // not implemented yet
    }
    [Test]
    public void TestToEDecimal() {
      // not implemented yet
    }
    [Test]
    public void TestToPlainString() {
      // not implemented yet
    }
    [Test]
    public void TestToSingle() {
      // not implemented yet
    }
    [Test]
    public void TestToString() {
      // not implemented yet
    }
    [Test]
    public void TestUnsignedMantissa() {
      // not implemented yet
    }

    private static void TestAddCloseExponent(FastRandom fr, int exp) {
      for (var i = 0; i < 1000; ++i) {
        EInteger exp1 = EInteger.FromInt32(exp)
          .Add(EInteger.FromInt32(fr.NextValue(32) - 16));
        EInteger exp2 = exp1.Add(
          EInteger.FromInt32(fr.NextValue(18) - 30));
        EInteger mant1 = EInteger.FromInt32(fr.NextValue(0x10000000));
        EInteger mant2 = EInteger.FromInt32(fr.NextValue(0x10000000));
        EFloat decA = EFloat.Create(mant1, exp1);
        EFloat decB = EFloat.Create(mant2, exp2);
        EFloat decC = decA.Add(decB);
        EFloat decD = decC.Subtract(decA);
        TestCommon.CompareTestEqual(decD, decB);
        decD = decC.Subtract(decB);
        TestCommon.CompareTestEqual(decD, decA);
      }
    }

    private static void TestEFloatDoubleCore(double d, string s) {
      double oldd = d;
      EFloat bf = EFloat.FromDouble(d);
      if (s != null) {
        Assert.AreEqual(s, bf.ToString());
      }
      d = bf.ToDouble();
      Assert.AreEqual((double)oldd, d);
    }

    private static void TestEFloatSingleCore(float d, string s) {
      float oldd = d;
      EFloat bf = EFloat.FromSingle(d);
      if (s != null) {
        Assert.AreEqual(s, bf.ToString());
      }
      d = bf.ToSingle();
      Assert.AreEqual((float)oldd, d);
    }
  }
}
