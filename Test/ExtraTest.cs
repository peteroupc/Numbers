/*
Written by Peter O. in 2013.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;
using NUnit.Framework;
using PeterO.Numbers;

namespace Test {
  public static class ExtraTest {
    public static void TestStringEqualRoundTrip(EDecimal obj) {
      string str = obj.ToString();
      EDecimal newobj = EDecimal.FromString(str);
      string str2 = newobj.ToString();
      TestCommon.AssertEqualsHashCode(obj, newobj);
      TestCommon.AssertEqualsHashCode(str, str2);
    }

    public static void TestStringEqualRoundTrip(ERational obj) {
      string str = obj.ToString();
      ERational newobj = ERational.FromString(str);
      string str2 = newobj.ToString();
      TestCommon.AssertEqualsHashCode(obj, newobj);
      TestCommon.AssertEqualsHashCode(str, str2);
    }
    public static void TestStringEqualRoundTrip(EInteger obj) {
      string str = obj.ToString();
      EInteger newobj = EInteger.FromString(str);
      string str2 = newobj.ToString();
      TestCommon.AssertEqualsHashCode(obj, newobj);
      TestCommon.AssertEqualsHashCode(str, str2);
    }

    [Obsolete]
    public static void TestStringEqualRoundTrip(EFloat obj) {
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
      Assert.IsTrue(
        Double.IsPositiveInfinity(
          ERational.PositiveInfinity.ToDouble()));
      Assert.IsTrue(
        Double.IsNegativeInfinity(
          ERational.NegativeInfinity.ToDouble()));
      Assert.IsTrue(
    Single.IsPositiveInfinity(
      ERational.PositiveInfinity.ToSingle()));
      Assert.IsTrue(
    Single.IsNegativeInfinity(
      ERational.NegativeInfinity.ToSingle()));
    }
  }
}
