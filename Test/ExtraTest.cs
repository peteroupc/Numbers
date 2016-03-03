/*
Written in 2013 by Peter O.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://upokecenter.dreamhosters.com/articles/donate-now-2/
 */
using System;
using NUnit.Framework;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class ExtraTest {
    [Test]
    public void TestExtendedInfinity() {
      Assert.IsTrue(EDecimal.PositiveInfinity.IsPositiveInfinity());
      Assert.IsFalse(EDecimal.PositiveInfinity.IsNegativeInfinity());
      Assert.IsFalse(EDecimal.PositiveInfinity.IsNegative);
      Assert.IsFalse(EDecimal.NegativeInfinity.IsPositiveInfinity());
      Assert.IsTrue(EDecimal.NegativeInfinity.IsNegativeInfinity());
      Assert.IsTrue(EDecimal.NegativeInfinity.IsNegative);
      Assert.IsTrue(EFloat.PositiveInfinity.IsInfinity());
      Assert.IsTrue(EFloat.PositiveInfinity.IsPositiveInfinity());
      Assert.IsFalse(EFloat.PositiveInfinity.IsNegativeInfinity());
      Assert.IsFalse(EFloat.PositiveInfinity.IsNegative);
      Assert.IsTrue(EFloat.NegativeInfinity.IsInfinity());
      Assert.IsFalse(EFloat.NegativeInfinity.IsPositiveInfinity());
      Assert.IsTrue(EFloat.NegativeInfinity.IsNegativeInfinity());
      Assert.IsTrue(EFloat.NegativeInfinity.IsNegative);
      Assert.IsTrue(ERational.PositiveInfinity.IsInfinity());
      Assert.IsTrue(ERational.PositiveInfinity.IsPositiveInfinity());
      Assert.IsFalse(ERational.PositiveInfinity.IsNegativeInfinity());
      Assert.IsFalse(ERational.PositiveInfinity.IsNegative);
      Assert.IsTrue(ERational.NegativeInfinity.IsInfinity());
      Assert.IsFalse(ERational.NegativeInfinity.IsPositiveInfinity());
      Assert.IsTrue(ERational.NegativeInfinity.IsNegativeInfinity());
      Assert.IsTrue(ERational.NegativeInfinity.IsNegative);

      Assert.AreEqual(
        EDecimal.PositiveInfinity,
        EDecimal.FromDouble(Double.PositiveInfinity));
      Assert.AreEqual(
        EDecimal.NegativeInfinity,
        EDecimal.FromDouble(Double.NegativeInfinity));
      Assert.AreEqual(
        EDecimal.PositiveInfinity,
        EDecimal.FromSingle(Single.PositiveInfinity));
      Assert.AreEqual(
        EDecimal.NegativeInfinity,
        EDecimal.FromSingle(Single.NegativeInfinity));

      Assert.AreEqual(
        EFloat.PositiveInfinity,
        EFloat.FromDouble(Double.PositiveInfinity));
      Assert.AreEqual(
        EFloat.NegativeInfinity,
        EFloat.FromDouble(Double.NegativeInfinity));
      Assert.AreEqual(
        EFloat.PositiveInfinity,
        EFloat.FromSingle(Single.PositiveInfinity));
      Assert.AreEqual(
        EFloat.NegativeInfinity,
        EFloat.FromSingle(Single.NegativeInfinity));

      Assert.AreEqual(
        ERational.PositiveInfinity,
        ERational.FromDouble(Double.PositiveInfinity));
      Assert.AreEqual(
        ERational.NegativeInfinity,
        ERational.FromDouble(Double.NegativeInfinity));
      Assert.AreEqual(
        ERational.PositiveInfinity,
        ERational.FromSingle(Single.PositiveInfinity));
      Assert.AreEqual(
        ERational.NegativeInfinity,
        ERational.FromSingle(Single.NegativeInfinity));

      Assert.AreEqual(
        ERational.PositiveInfinity,
        ERational.FromEDecimal(EDecimal.PositiveInfinity));
      Assert.AreEqual(
        ERational.NegativeInfinity,
        ERational.FromEDecimal(EDecimal.NegativeInfinity));
      Assert.AreEqual(
        ERational.PositiveInfinity,
        ERational.FromEFloat(EFloat.PositiveInfinity));
      Assert.AreEqual(
        ERational.NegativeInfinity,
        ERational.FromEFloat(EFloat.NegativeInfinity));

  Assert.IsTrue(Double.IsPositiveInfinity(ERational.PositiveInfinity.ToDouble()));

  Assert.IsTrue(Double.IsNegativeInfinity(ERational.NegativeInfinity.ToDouble()));

  Assert.IsTrue(Single.IsPositiveInfinity(ERational.PositiveInfinity.ToSingle()));

  Assert.IsTrue(Single.IsNegativeInfinity(ERational.NegativeInfinity.ToSingle()));
    }
    /*
    [Test]
    public void TestEIntegerAnd() {
      try {
        EInteger.And(EInteger.Zero, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EInteger.And(null, EInteger.Zero);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        new Object();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    */
  }
}
