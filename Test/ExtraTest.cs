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
        ERational.FromExtendedDecimal(EDecimal.PositiveInfinity));
      Assert.AreEqual(
        ERational.NegativeInfinity,
        ERational.FromExtendedDecimal(EDecimal.NegativeInfinity));
      Assert.AreEqual(
        ERational.PositiveInfinity,
        ERational.FromExtendedFloat(EFloat.PositiveInfinity));
      Assert.AreEqual(
        ERational.NegativeInfinity,
        ERational.FromExtendedFloat(EFloat.NegativeInfinity));

  Assert.IsTrue(Double.IsPositiveInfinity(ERational.PositiveInfinity.ToDouble()));

  Assert.IsTrue(Double.IsNegativeInfinity(ERational.NegativeInfinity.ToDouble()));

  Assert.IsTrue(Single.IsPositiveInfinity(ERational.PositiveInfinity.ToSingle()));

  Assert.IsTrue(Single.IsNegativeInfinity(ERational.NegativeInfinity.ToSingle()));
      try {
        EDecimal.PositiveInfinity.ToBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.NegativeInfinity.ToBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.PositiveInfinity.ToBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.NegativeInfinity.ToBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ERational.PositiveInfinity.ToBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        ERational.NegativeInfinity.ToBigInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        Console.Write(String.Empty);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestExtendedMiscellaneous() {
      Assert.AreEqual(EDecimal.One, EDecimal.FromInt32(1));

      Assert.AreEqual(
        EFloat.Zero,
        EDecimal.Zero.ToExtendedFloat());
      Assert.AreEqual(
        EFloat.NegativeZero,
        EDecimal.NegativeZero.ToExtendedFloat());
      if (0.0f != EFloat.Zero.ToSingle()) {
        Assert.Fail("Failed " + EFloat.Zero.ToDouble());
      }
      if (0.0f != EFloat.Zero.ToDouble()) {
        Assert.Fail("Failed " + EFloat.Zero.ToDouble());
      }
    }
  }
}
