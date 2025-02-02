/*
Written by Peter O.
Any copyright to this work is released to the Public Domain.
In case this is not possible, this work is also
licensed under the Unlicense: https://unlicense.org/
 */
using System;
using PeterO.Numbers;

namespace PeterO {
  /// <summary>Description of Runner.</summary>
  public static class Runner {
    public static void Main() {
      new Test.EDecimalTest().TestToString();
    }
  }
}
