using System;
using NUnit.Framework;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class ERationalTest {
    [Test]
    public void TestConstructor() {
      // not implemented yet
    }
    [Test]
    public void TestAbs() {
      // not implemented yet
    }

    [Test]
    public void TestFromString() {
      ERational er;
      er = ERational.FromString("-2/4");
      Assert.AreEqual(EInteger.FromInt32(-2), er.Numerator);
      Assert.AreEqual(EInteger.FromInt32(4), er.Denominator);
      er = ERational.FromString("2/4");
      Assert.AreEqual(EInteger.FromInt32(2), er.Numerator);
      Assert.AreEqual(EInteger.FromInt32(4), er.Denominator);
      er = ERational.FromString("293939393939/4");
      Assert.AreEqual(EInteger.FromString("293939393939"), er.Numerator);
      Assert.AreEqual(EInteger.FromInt32(4), er.Denominator);
      er = ERational.FromString("-293939393939/4");
      Assert.AreEqual(EInteger.FromString("-293939393939"), er.Numerator);
      Assert.AreEqual(EInteger.FromInt32(4), er.Denominator);
      er = ERational.FromString("-2/293939393939");
      Assert.AreEqual(EInteger.FromInt32(-2), er.Numerator);
      Assert.AreEqual(EInteger.FromString("293939393939"), er.Denominator);
      er = ERational.FromString("-2");
      Assert.AreEqual(EInteger.FromString("-2"), er.Numerator);
      Assert.AreEqual(EInteger.FromInt32(1), er.Denominator);
      er = ERational.FromString("2");
      Assert.AreEqual(EInteger.FromString("2"), er.Numerator);
      Assert.AreEqual(EInteger.FromInt32(1), er.Denominator);
      try {
 ERational.FromString("-2x");
Assert.Fail("Should have failed");
} catch (FormatException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
      try {
 ERational.FromString("-2/");
Assert.Fail("Should have failed");
} catch (FormatException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
      try {
 ERational.FromString("-2/x");
Assert.Fail("Should have failed");
} catch (FormatException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
      try {
 ERational.FromString("-2/2x");
Assert.Fail("Should have failed");
} catch (FormatException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
      var fr = new FastRandom();
      for (var i = 0; i < 1000; ++i) {
        EInteger ei1 = RandomObjects.RandomEInteger(fr);
        EInteger ei2 = RandomObjects.RandomEInteger(fr).Abs();
        if (ei2.IsZero) {
          ei2 = EInteger.One;
        }
        er = ERational.FromString(ei1 + "/" + ei2);
        Assert.AreEqual(ei1, er.Numerator);
        Assert.AreEqual(ei2, er.Denominator);
      }
    }
    [Test]
    public void TestAdd() {
      // not implemented yet
    }
    [Test]
    public void TestCompareTo() {
      var r = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        ERational bigintA = RandomObjects.RandomRational(r);
        ERational bigintB = RandomObjects.RandomRational(r);
        ERational bigintC = RandomObjects.RandomRational(r);
        TestCommon.CompareTestRelations(bigintA, bigintB, bigintC);
      }
      TestCommon.CompareTestLess(ERational.Zero, ERational.NaN);
      for (var i = 0; i < 100; ++i) {
        EInteger num = RandomObjects.RandomEInteger(r);
        if (num.IsZero) {
          // Skip if number is 0; 0/1 and 0/2 are
          // equal in that case
          continue;
        }
        num = num.Abs();
        var rat = new ERational(num, EInteger.One);
        var rat2 = new ERational(num, (EInteger)2);
        TestCommon.CompareTestLess(rat2, rat);
        TestCommon.CompareTestGreater(rat, rat2);
      }
      TestCommon.CompareTestLess(
        new ERational(EInteger.One, (EInteger)2),
        new ERational((EInteger)4, EInteger.One));
      for (var i = 0; i < 100; ++i) {
        EInteger num = RandomObjects.RandomEInteger(r);
        EInteger den = RandomObjects.RandomEInteger(r);
        if (den.IsZero) {
          den = EInteger.One;
        }
        var rat = new ERational(num, den);
        for (int j = 0; j < 10; ++j) {
          EInteger num2 = num;
          EInteger den2 = den;
          EInteger mult = RandomObjects.RandomEInteger(r);
          if (mult.IsZero || mult.Equals(EInteger.One)) {
            mult = (EInteger)2;
          }
          num2 *= (EInteger)mult;
          den2 *= (EInteger)mult;
          var rat2 = new ERational(num2, den2);
          TestCommon.CompareTestEqual(rat, rat2);
        }
      }
    }
    [Test]
    public void TestCompareToBinary() {
      // not implemented yet
    }
    [Test]
    public void TestCompareToDecimal() {
      var fr = new FastRandom();
      for (var i = 0; i < 100; ++i) {
        ERational er = RandomObjects.RandomRational(fr);
        int exp = -100000 + fr.NextValue(200000);
        EDecimal ed = EDecimal.Create(
          RandomObjects.RandomEInteger(fr),
          (EInteger)exp);
        ERational er2 = ERational.FromEDecimal(ed);
        int c2r = er.CompareTo(er2);
        int c2d = er.CompareToDecimal(ed);
        Assert.AreEqual(c2r, c2d);
      }
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
    public void TestDenominator() {
      // not implemented yet
    }
    [Test]
    public void TestDivide() {
      var fr = new FastRandom();
      for (var i = 0; i < 500; ++i) {
        ERational er = RandomObjects.RandomRational(fr);
        ERational er2 = RandomObjects.RandomRational(fr);
        if (er2.IsZero || !er2.IsFinite) {
          continue;
        }
        if (er.IsZero || !er.IsFinite) {
          continue;
        }
        ERational ermult = er.Multiply(er2);
        ERational erdiv = ermult.Divide(er);
        TestCommon.CompareTestEqual(erdiv, er2);
        erdiv = ermult.Divide(er2);
        TestCommon.CompareTestEqual(erdiv, er);
      }
    }
    [Test]
    public void TestEquals() {
      // not implemented yet
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
    public void TestConversions() {
      var fr = new FastRandom();
      for (var i = 0; i < 20000; ++i) {
        bool isNum, isTruncated, isInteger;
        EInteger eint;
        ERational enumber = RandomObjects.RandomRational(fr);
        if (!enumber.IsFinite) {
          try {
 enumber.ToByteChecked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
  Assert.AreEqual(
EInteger.Zero,
EInteger.FromByte(enumber.ToByteUnchecked()));
          try {
 enumber.ToByteIfExact();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          try {
 enumber.ToInt16Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
Assert.AreEqual(
EInteger.Zero,
EInteger.FromInt16(enumber.ToInt16Unchecked()));
          try {
 enumber.ToInt16IfExact();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          try {
 enumber.ToInt32Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
Assert.AreEqual(
EInteger.Zero,
EInteger.FromInt32(enumber.ToInt32Unchecked()));
          try {
 enumber.ToInt32IfExact();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          try {
 enumber.ToInt64Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
Assert.AreEqual(
EInteger.Zero,
EInteger.FromInt64(enumber.ToInt64Unchecked()));
          try {
 enumber.ToInt64IfExact();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          continue;
        }
        ERational enumberInteger = ERational.FromEInteger(enumber.ToEInteger());
        isInteger = enumberInteger.CompareTo(enumber) == 0;
        eint = enumber.ToEInteger();
        isNum = enumber.CompareTo(
        ERational.FromString("0")) >= 0 && enumber.CompareTo(
        ERational.FromString("255")) <= 0;
        isTruncated = enumber.ToEInteger().CompareTo(
        EInteger.FromString("0")) >= 0 && enumber.ToEInteger().CompareTo(
        EInteger.FromString("255")) <= 0;
        if (isNum) {
     TestCommon.AssertEquals(
eint,
EInteger.FromByte(enumber.ToByteChecked()));
   TestCommon.AssertEquals(
eint,
EInteger.FromByte(enumber.ToByteUnchecked()));
          if (isInteger) {
     TestCommon.AssertEquals(
eint,
EInteger.FromByte(enumber.ToByteIfExact()));
          } else {
            try {
 enumber.ToByteIfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          }
        } else if (isTruncated) {
     TestCommon.AssertEquals(
eint,
EInteger.FromByte(enumber.ToByteChecked()));
   TestCommon.AssertEquals(
eint,
EInteger.FromByte(enumber.ToByteUnchecked()));
          try {
 enumber.ToByteIfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
        } else {
          try {
 enumber.ToByteChecked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          try {
 enumber.ToByteUnchecked();
} catch (Exception ex) {
Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          if (isInteger) {
            try {
 enumber.ToByteIfExact();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          } else {
            try {
 enumber.ToByteIfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          }
        }
        isNum = enumber.CompareTo(
        ERational.FromString("-32768")) >= 0 && enumber.CompareTo(
        ERational.FromString("32767")) <= 0;
        isTruncated = enumber.ToEInteger().CompareTo(
        EInteger.FromString("-32768")) >= 0 && enumber.ToEInteger().CompareTo(
        EInteger.FromString("32767")) <= 0;
        if (isNum) {
   TestCommon.AssertEquals(
eint,
EInteger.FromInt16(enumber.ToInt16Checked()));
 TestCommon.AssertEquals(
eint,
EInteger.FromInt16(enumber.ToInt16Unchecked()));
          if (isInteger) {
   TestCommon.AssertEquals(
eint,
EInteger.FromInt16(enumber.ToInt16IfExact()));
          } else {
            try {
 enumber.ToInt16IfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          }
        } else if (isTruncated) {
   TestCommon.AssertEquals(
eint,
EInteger.FromInt16(enumber.ToInt16Checked()));
 TestCommon.AssertEquals(
eint,
EInteger.FromInt16(enumber.ToInt16Unchecked()));
          try {
 enumber.ToInt16IfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
        } else {
          try {
 enumber.ToInt16Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          try {
 enumber.ToInt16Unchecked();
} catch (Exception ex) {
Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          if (isInteger) {
            try {
 enumber.ToInt16IfExact();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          } else {
            try {
 enumber.ToInt16IfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          }
        }
        isNum = enumber.CompareTo(
        ERational.FromString("-2147483648")) >= 0 && enumber.CompareTo(
        ERational.FromString("2147483647")) <= 0;
        isTruncated = enumber.ToEInteger().CompareTo(
    EInteger.FromString("-2147483648")) >= 0 &&
          enumber.ToEInteger().CompareTo(
        EInteger.FromString("2147483647")) <= 0;
        if (isNum) {
   TestCommon.AssertEquals(
eint,
EInteger.FromInt32(enumber.ToInt32Checked()));
 TestCommon.AssertEquals(
eint,
EInteger.FromInt32(enumber.ToInt32Unchecked()));
          if (isInteger) {
   TestCommon.AssertEquals(
eint,
EInteger.FromInt32(enumber.ToInt32IfExact()));
          } else {
            try {
 enumber.ToInt32IfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          }
        } else if (isTruncated) {
   TestCommon.AssertEquals(
eint,
EInteger.FromInt32(enumber.ToInt32Checked()));
 TestCommon.AssertEquals(
eint,
EInteger.FromInt32(enumber.ToInt32Unchecked()));
          try {
 enumber.ToInt32IfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
        } else {
          try {
 enumber.ToInt32Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          try {
 enumber.ToInt32Unchecked();
} catch (Exception ex) {
Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          if (isInteger) {
            try {
 enumber.ToInt32IfExact();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          } else {
            try {
 enumber.ToInt32IfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          }
        }
        isNum = enumber.CompareTo(
        ERational.FromString("-9223372036854775808")) >= 0 && enumber.CompareTo(
        ERational.FromString("9223372036854775807")) <= 0;
        isTruncated = enumber.ToEInteger().CompareTo(
        EInteger.FromString("-9223372036854775808")) >= 0 &&
          enumber.ToEInteger().CompareTo(
        EInteger.FromString("9223372036854775807")) <= 0;
        if (isNum) {
   TestCommon.AssertEquals(
eint,
EInteger.FromInt64(enumber.ToInt64Checked()));
 TestCommon.AssertEquals(
eint,
EInteger.FromInt64(enumber.ToInt64Unchecked()));
          if (isInteger) {
   TestCommon.AssertEquals(
eint,
EInteger.FromInt64(enumber.ToInt64IfExact()));
          } else {
            try {
 enumber.ToInt64IfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          }
        } else if (isTruncated) {
   TestCommon.AssertEquals(
eint,
EInteger.FromInt64(enumber.ToInt64Checked()));
 TestCommon.AssertEquals(
eint,
EInteger.FromInt64(enumber.ToInt64Unchecked()));
          try {
 enumber.ToInt64IfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
        } else {
          try {
 enumber.ToInt64Checked();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          try {
 enumber.ToInt64Unchecked();
} catch (Exception ex) {
Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          if (isInteger) {
            try {
 enumber.ToInt64IfExact();
Assert.Fail("Should have failed");
} catch (OverflowException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          } else {
            try {
 enumber.ToInt64IfExact();
Assert.Fail("Should have failed");
} catch (ArithmeticException) {
new Object();
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
          }
        }
      }
    }
    [Test]
    public void TestFromEDecimal() {
      // not implemented yet
    }
    [Test]
    public void TestFromEFloat() {
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
    public void TestGetHashCode() {
      // not implemented yet
    }
    [Test]
    public void TestIsFinite() {
      Assert.IsFalse(ERational.PositiveInfinity.IsFinite);
      Assert.IsFalse(ERational.NegativeInfinity.IsFinite);
      Assert.IsTrue(ERational.Zero.IsFinite);
      Assert.IsFalse(ERational.NaN.IsFinite);
    }
    [Test]
    public void TestIsInfinity() {
      Assert.IsTrue(ERational.PositiveInfinity.IsInfinity());
      Assert.IsTrue(ERational.NegativeInfinity.IsInfinity());
      Assert.IsFalse(ERational.Zero.IsInfinity());
      Assert.IsFalse(ERational.NaN.IsInfinity());
    }
    [Test]
    public void TestIsNaN() {
      Assert.IsFalse(ERational.PositiveInfinity.IsNaN());
      Assert.IsFalse(ERational.NegativeInfinity.IsNaN());
      Assert.IsFalse(ERational.Zero.IsNaN());
      Assert.IsFalse(ERational.One.IsNaN());
      Assert.IsTrue(ERational.NaN.IsNaN());
    }
    [Test]
    public void TestIsNegative() {
      // not implemented yet
    }
    [Test]
    public void TestIsNegativeInfinity() {
      Assert.IsFalse(ERational.PositiveInfinity.IsNegativeInfinity());
      Assert.IsTrue(ERational.NegativeInfinity.IsNegativeInfinity());
      Assert.IsFalse(ERational.Zero.IsNegativeInfinity());
      Assert.IsFalse(ERational.One.IsNegativeInfinity());
      Assert.IsFalse(ERational.NaN.IsNegativeInfinity());
    }
    [Test]
    public void TestIsPositiveInfinity() {
      Assert.IsTrue(ERational.PositiveInfinity.IsPositiveInfinity());
      Assert.IsFalse(ERational.NegativeInfinity.IsPositiveInfinity());
      Assert.IsFalse(ERational.Zero.IsPositiveInfinity());
      Assert.IsFalse(ERational.One.IsPositiveInfinity());
      Assert.IsFalse(ERational.NaN.IsPositiveInfinity());
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
      Assert.IsTrue(ERational.NegativeZero.IsZero);
      Assert.IsTrue(ERational.Zero.IsZero);
      Assert.IsFalse(ERational.One.IsZero);
      Assert.IsFalse(ERational.NegativeInfinity.IsZero);
      Assert.IsFalse(ERational.PositiveInfinity.IsZero);
      Assert.IsFalse(ERational.NaN.IsZero);
      Assert.IsFalse(ERational.SignalingNaN.IsZero);
    }
    [Test]
    public void TestMultiply() {
      // not implemented yet
    }
    [Test]
    public void TestNegate() {
      // not implemented yet
    }
    [Test]
    public void TestNumerator() {
      // not implemented yet
    }
    [Test]
    public void TestRemainder() {
      var fr = new FastRandom();
      for (var i = 0; i < 100; ++i) {
        ERational er;
        ERational er2;
        er = new ERational(
          RandomObjects.RandomEInteger(fr),
          EInteger.One);
        er2 = new ERational(
          RandomObjects.RandomEInteger(fr),
          EInteger.One);
        if (er2.IsZero || !er2.IsFinite) {
          continue;
        }
        if (er.IsZero || !er.IsFinite) {
          // Code below will divide by "er",
          // so skip if "er" is zero
          continue;
        }
        ERational ermult = er.Multiply(er2);
        ERational erdiv = ermult.Divide(er);
        erdiv = ermult.Remainder(er);
        if (!erdiv.IsZero) {
          Assert.Fail(ermult + "; " + er);
        }
        erdiv = ermult.Remainder(er2);
        if (!erdiv.IsZero) {
          Assert.Fail(er + "; " + er2);
        }
      }
    }
    [Test]
    public void TestSign() {
      Assert.AreEqual(0, ERational.NegativeZero.Sign);
      Assert.AreEqual(0, ERational.Zero.Sign);
      Assert.AreEqual(1, ERational.One.Sign);
      Assert.AreEqual(-1, ERational.NegativeInfinity.Sign);
      Assert.AreEqual(1, ERational.PositiveInfinity.Sign);
    }
    [Test]
    public void TestSubtract() {
      // not implemented yet
    }
    [Test]
    public void TestToEInteger() {
      try {
        ERational.PositiveInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ERational.NegativeInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestToEIntegerIfExact() {
      try {
        ERational.PositiveInfinity.ToEIntegerIfExact();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ERational.NegativeInfinity.ToEIntegerIfExact();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ERational.NaN.ToEIntegerIfExact();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ERational.SignalingNaN.ToEIntegerIfExact();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestToDouble() {
      // test for correct rounding
      double dbl;
      dbl = ERational.FromEDecimal(
        EDecimal.FromString(
       "1.972579273363468721491642554610734805464744567871093749999999999999"))
        .ToDouble();
      {
string stringTemp = EFloat.FromDouble(dbl).ToPlainString();
Assert.AreEqual(
"1.9725792733634686104693400920950807631015777587890625",
stringTemp);
}
    }
    [Test]
    public void TestToEDecimal() {
      // not implemented yet
    }
    [Test]
    public void TestToEDecimalExactIfPossible() {
      // not implemented yet
    }
    [Test]
    public void TestToEFloat() {
      // not implemented yet
    }
    [Test]
    public void TestToEFloatExactIfPossible() {
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
    public void TestUnsignedNumerator() {
      // not implemented yet
    }
  }
}
