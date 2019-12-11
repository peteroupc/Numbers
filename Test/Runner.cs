/*
Written by Peter O. in 2013.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
 */
using System;
using PeterO.Numbers;

namespace PeterO {
  /// <summary>Description of Runner.</summary>
  public static class Runner {
    public static void Main() {
      new Test.EDecimalTest().TestFromStringSubstring();
      new Test.ERationalTest().TestFromString();
    }
  }
}
