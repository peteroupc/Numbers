/*
Written by Peter O.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using PeterO;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class DecimalTest {
    internal static void PrintTime(System.Diagnostics.Stopwatch sw) {
      Console.WriteLine("Elapsed time: " + (sw.ElapsedMilliseconds / 1000.0) +
        " s");
    }

    [Test]
    public void TestPi() {
      var sw = new System.Diagnostics.Stopwatch();
      sw.Start();
      EDecimal.PI(EContext.ForPrecision(1000)).ToString();
      sw.Stop();
      PrintTime(sw);
    }

    private static decimal RandomDecimal(RandomGenerator rand) {
      int a, b, c;
      a = rand.UniformInt(0x10000);
      a = unchecked((a << 16) + rand.UniformInt(0x10000));
      b = rand.UniformInt(0x10000);
      b = unchecked((b << 16) + rand.UniformInt(0x10000));
      c = rand.UniformInt(0x10000);
      c = unchecked((c << 16) + rand.UniformInt(0x10000));
      int scale = rand.UniformInt(29);
      return new Decimal(a, b, c, rand.UniformInt(2) == 0, (byte)scale);
    }

    [Test]
    [Timeout(100000)]
    public void TestDecimalString() {
      var fr = new RandomGenerator();
      // var sw = new System.Diagnostics.Stopwatch();
      // var sw2 = new System.Diagnostics.Stopwatch();
      for (var i = 0; i < 10000; ++i) {
        if (i % 100 == 0) {
          Console.WriteLine(i);
        }
        EDecimal ed = RandomObjects.RandomEDecimal(fr);
        // Reduce to Decimal128. Without this reduction,
        // Decimal.Parse would run significantly more slowly
        // on average for random
        // EDecimals than
        // EDecimal.FromString(CliDecimal) does.
        // Decimal128 covers all numbers representable
        // in a CliDecimal.
        ed = ed.RoundToPrecision(EContext.Decimal128);
        if (!ed.IsFinite) {
          continue;
        }
        string edString = ed.ToString();
        // Console.WriteLine("eds=" + (//edString.Length) + " sw=" +
        // sw.ElapsedMilliseconds + ", " + (sw2.ElapsedMilliseconds));
        decimal d;
        try {
          System.Globalization.NumberStyles numstyles =
            System.Globalization.NumberStyles.AllowExponent |
            System.Globalization.NumberStyles.Number;
          // sw.Start();
          d = Decimal.Parse(
              edString,
              numstyles,
              System.Globalization.CultureInfo.InvariantCulture);
          // sw.Stop();
          // sw2.Start();
          EDecimal ed3 = EDecimal.FromString(
              edString,
              EContext.CliDecimal);
          // sw2.Stop();
          var edd = (EDecimal)d;
          if (!edd.Equals(ed3)) {
            string msg = ed.ToString() + " (expanded: " +
              EDecimal.FromString(ed.ToString()) + ")";
            TestCommon.CompareTestEqual(
              (EDecimal)d,
              ed3,
              msg);
          }
        } catch (OverflowException ex) {
          EDecimal ed2 = EDecimal.FromString(
              edString,
              EContext.CliDecimal);
          if (!ed2.IsInfinity()) {
            Assert.Fail(edString + "\n" + ex.ToString());
          }
        }
      }
    }

    [Test]
    public static void TestUint64() {
      EInteger ei = EInteger.FromString("9223372036854775808");
      Assert.AreEqual((ulong)9223372036854775808, ei.ToUInt64Checked());
      Assert.AreEqual((ulong)9223372036854775808, ei.ToUInt64Unchecked());
    }

    [Test]
    public static void TestToDecimal() {
      try {
        EDecimal.FromString("8.8888888e-7").ToDecimal();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("8.8888888e-8").ToDecimal();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("8.8888888e-18").ToDecimal();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestDecimal() {
      var fr = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        decimal d = RandomDecimal(fr);
        EDecimal ed = d;
        TestCommon.CompareTestEqual(d, (decimal)ed, ed.ToString());
        EDecimal ed2 =

          EDecimal.FromString(
            d.ToString(System.Globalization.CultureInfo.InvariantCulture));
        TestCommon.CompareTestEqual(ed, ed2);
      }
    }

    [Test]
    public void TestToUintChecked() {
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.1").ToUInt16Checked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.4").ToUInt16Checked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.5").ToUInt16Checked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.6").ToUInt16Checked());
      try {
        EDecimal.FromString("-1.0").ToUInt16Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.4").ToUInt16Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.5").ToUInt16Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.6").ToUInt16Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.1").ToUInt32Checked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.4").ToUInt32Checked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.5").ToUInt32Checked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.6").ToUInt32Checked());
      try {
        EDecimal.FromString("-1.0").ToUInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.4").ToUInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.5").ToUInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.6").ToUInt32Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.1").ToUInt64Checked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.4").ToUInt64Checked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.5").ToUInt64Checked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.6").ToUInt64Checked());
      try {
        EDecimal.FromString("-1.0").ToUInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.4").ToUInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.5").ToUInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.6").ToUInt64Checked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
  }
}
