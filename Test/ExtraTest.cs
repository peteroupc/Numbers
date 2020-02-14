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
      if (obj == null) {
        throw new ArgumentNullException(nameof(obj));
      }
      string str = obj.ToString();
      EDecimal newobj = EDecimal.FromString(str);
      if (str.Length < 100 || !obj.Equals(newobj)) {
        TestCommon.AssertEqualsHashCode(obj, newobj);
        string str2 = newobj.ToString();
        TestCommon.AssertEqualsHashCode(str, str2);
      }
    }

    public static void TestStringEqualRoundTrip(ERational obj) {
      if (obj == null) {
        throw new ArgumentNullException(nameof(obj));
      }
      string str = obj.ToString();
      ERational newobj = ERational.FromString(str);
      if (str.Length < 100 || !obj.Equals(newobj)) {
        TestCommon.AssertEqualsHashCode(obj, newobj);
        string str2 = newobj.ToString();
        TestCommon.AssertEqualsHashCode(str, str2);
      }
    }
    public static void TestStringEqualRoundTrip(EInteger obj) {
      if (obj == null) {
        throw new ArgumentNullException(nameof(obj));
      }
      string str = obj.ToString();
      EInteger newobj = EInteger.FromString(str);
      if (str.Length < 100 || !obj.Equals(newobj)) {
        TestCommon.AssertEqualsHashCode(obj, newobj);
        string str2 = newobj.ToString();
        TestCommon.AssertEqualsHashCode(str, str2);
      }
    }
  }
}
