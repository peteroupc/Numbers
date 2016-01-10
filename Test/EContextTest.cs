using System;
using NUnit.Framework;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class EContextTest {
    [Test]
    public void TestConstructor() {
      try {
        Assert.AreEqual(
          null,
          new EContext(-1, ERounding.HalfEven, 0, 0, false));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        Assert.AreEqual(
          null,
          new EContext(0, ERounding.HalfEven, 0, -1, false));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestAdjustExponent() {
      // not implemented yet
    }
    [Test]
    public void TestClampNormalExponents() {
      // not implemented yet
    }
    [Test]
    public void TestCopy() {
      // not implemented yet
    }
    [Test]
    public void TestEMax() {
      EContext ctx = EContext.Unlimited;
      Assert.AreEqual(EInteger.Zero, ctx.EMax);
      ctx = EContext.Unlimited.WithExponentRange(-5, 5);
      Assert.AreEqual((EInteger)5, ctx.EMax);
    }
    [Test]
    public void TestEMin() {
      EContext ctx = EContext.Unlimited;
      Assert.AreEqual(EInteger.Zero, ctx.EMin);
      ctx = EContext.Unlimited.WithExponentRange(-5, 5);
      Assert.AreEqual((EInteger)(-5), ctx.EMin);
    }
    [Test]
    public void TestExponentWithinRange() {
  Assert.IsTrue(EContext.Unlimited.ExponentWithinRange(EInteger.FromString(
"-9999999")));

  Assert.IsTrue(EContext.Unlimited.ExponentWithinRange(EInteger.FromString(
"9999999")));
      try {
 EContext.Unlimited.ExponentWithinRange(null);
Assert.Fail("Should have failed");
} catch (ArgumentNullException) {
Console.Write(String.Empty);
} catch (Exception ex) {
 Assert.Fail(ex.ToString());
throw new InvalidOperationException(String.Empty, ex);
}
    }
    [Test]
    public void TestFlags() {
      EContext ctx = EContext.Unlimited;
      try {
        ctx.Flags = 5;
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      ctx = ctx.WithBlankFlags();
      try {
        ctx.Flags = 5;
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      ctx = ctx.WithNoFlags();
      try {
        ctx.Flags = 5;
        Assert.Fail("Should have failed");
      } catch (InvalidOperationException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestCliDecimal() {
      EDecimal valueEdTmp;
      valueEdTmp = EDecimal.FromString(
"-79228162514264337593543950336")
.RoundToPrecision(EContext.CliDecimal);
      Assert.AreEqual(
        EDecimal.NegativeInfinity,
        valueEdTmp);
      valueEdTmp = EDecimal.FromString(
     "8.782580686213340724E+28")
     .RoundToPrecision(EContext.CliDecimal);
      Assert.AreEqual(
        EDecimal.PositiveInfinity,
        valueEdTmp);
      {
object objectTemp = EDecimal.NegativeInfinity;
object objectTemp2 = EDecimal.FromString(
        "-9.3168444507547E+28").RoundToPrecision(EContext.CliDecimal);
Assert.AreEqual(objectTemp, objectTemp2);
}
      {
        string stringTemp =

          EDecimal.FromString(
"-9344285899206687626894794544.04982268810272216796875")
.RoundToPrecision(EContext.CliDecimal)
          .ToPlainString();
        Assert.AreEqual(
          "-9344285899206687626894794544",
          stringTemp);
      }
      {
object objectTemp = EDecimal.PositiveInfinity;
object objectTemp2 = EDecimal.FromString(
"96148154858060747311034406200").RoundToPrecision(EContext.CliDecimal);
Assert.AreEqual(objectTemp, objectTemp2);
}
      {
object objectTemp = EDecimal.PositiveInfinity;
object objectTemp2 = EDecimal.FromString(
"90246605365627217170000000000").RoundToPrecision(EContext.CliDecimal);
Assert.AreEqual(objectTemp, objectTemp2);
}
    }

    [Test]
    public void TestForPrecision() {
      // not implemented yet
    }
    [Test]
    public void TestForPrecisionAndRounding() {
      // not implemented yet
    }
    [Test]
    public void TestForRounding() {
      EContext ctx;
      ctx = EContext.ForRounding(ERounding.HalfEven);
      Assert.AreEqual(ERounding.HalfEven, ctx.Rounding);
      ctx = EContext.ForRounding(ERounding.HalfUp);
      Assert.AreEqual(ERounding.HalfUp, ctx.Rounding);
    }
    [Test]
    public void TestHasExponentRange() {
      // not implemented yet
    }
    [Test]
    public void TestHasFlags() {
      // not implemented yet
    }
    [Test]
    public void TestHasMaxPrecision() {
      // not implemented yet
    }
    [Test]
    public void TestIsPrecisionInBits() {
      // not implemented yet
    }
    [Test]
    public void TestIsSimplified() {
      // not implemented yet
    }
    [Test]
    public void TestPrecision() {
      // not implemented yet
    }
    [Test]
    public void TestRounding() {
      // not implemented yet
    }
    [Test]
    public void TestToString() {
      if (EContext.Unlimited.ToString() == null) {
 Assert.Fail();
 }
    }
    [Test]
    public void TestTraps() {
      // not implemented yet
    }
    [Test]
    public void TestWithAdjustExponent() {
      // not implemented yet
    }
    [Test]
    public void TestWithBigExponentRange() {
      // not implemented yet
    }
    [Test]
    public void TestWithBigPrecision() {
      try {
        EContext.Unlimited.WithBigPrecision(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EContext.Unlimited.WithBigPrecision(EInteger.One.Negate());
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestWithBlankFlags() {
      // not implemented yet
    }
    [Test]
    public void TestWithExponentClamp() {
      // not implemented yet
    }
    [Test]
    public void TestWithExponentRange() {
      try {
        EContext.Unlimited.WithExponentRange(1, 0);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EContext.Unlimited.WithBigExponentRange(null, EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EContext.Unlimited.WithBigExponentRange(EInteger.Zero, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger bigintBig = EInteger.One << 64;
        EContext.Unlimited.WithBigExponentRange(
          bigintBig,
          EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestWithNoFlags() {
      // not implemented yet
    }
    [Test]
    public void TestWithPrecision() {
      try {
        EContext.Unlimited.WithPrecision(-1);
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
Console.Write(String.Empty);
} catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      EContext ctx;
      ctx = EContext.Unlimited.WithPrecision(6);
      Assert.AreEqual((EInteger)6, ctx.Precision);
    }
    [Test]
    public void TestWithPrecisionInBits() {
      // not implemented yet
    }
    [Test]
    public void TestWithRounding() {
      // not implemented yet
    }
    [Test]
    public void TestWithSimplified() {
      var pc = new EContext(0, ERounding.HalfUp, 0, 5, true);
      Assert.IsFalse(pc.IsSimplified);
      pc = pc.WithSimplified(true);
      Assert.IsTrue(pc.IsSimplified);
      pc = pc.WithSimplified(false);
      Assert.IsFalse(pc.IsSimplified);
    }
    [Test]
    public void TestWithTraps() {
      // not implemented yet
    }
    [Test]
    public void TestWithUnlimitedExponents() {
      var pc = new EContext(0, ERounding.HalfUp, 0, 5, true);
      Assert.IsTrue(pc.HasExponentRange);
      pc = pc.WithUnlimitedExponents();
      Assert.IsFalse(pc.HasExponentRange);
    }
  }
}
