/*
Written by Peter O.
Any copyright to this work is released to the Public Domain.
In case this is not possible, this work is also
licensed under Creative Commons Zero (CC0):
https://creativecommons.org/publicdomain/zero/1.0/

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
