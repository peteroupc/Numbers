using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PeterO;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class EDecimalTest {
    private static readonly string[] ValueTestStrings = {
      "1.265e-4",
      "0.0001265", "0.0001265", "0.0001265", "0.000E-1", "0.0000",
      "0.0000", "0.0000", "0.0000000000000e-3", "0E-16", "0.0E-15",
      "0.0000000000000000", "0.000000000e+1", "0E-8", "0.00E-6",
      "0.00000000", "0.000000000000000e+12", "0.000", "0.000", "0.000",
      "0.00000000000000e-11", "0E-25", "0.0E-24",
      "0.0000000000000000000000000", "0.000000000000e+5", "0E-7",
      "0.0E-6", "0.0000000", "0.0000e-4", "0E-8", "0.00E-6",
      "0.00000000", "0.000000e+2", "0.0000", "0.0000", "0.0000",
      "0.0e+3", "0E+2", "0.0E+3", "0", "0.000000000000000e+8", "0E-7",
      "0.0E-6", "0.0000000", "0.000e+10", "0E+7", "0.00E+9", "0",
      "0.0000000000000000000e-12", "0E-31", "0.0E-30",
      "0." + TestCommon.Repeat("0", 31) + String.Empty, "0.0000e-1", "0.00000",
      "0.00000", "0.00000", "0.00000000000e-11", "0E-22", "0.0E-21",
      "0.0000000000000000000000", "0.00000000000e-17", "0E-28", "0.0E-27",
      "0.0000000000000000000000000000", "0.00000000000000e+9", "0.00000",
      "0.00000", "0.00000", "0.0000000000e-18", "0E-28", "0.0E-27",
      "0.0000000000000000000000000000", "0.0e-13", "0E-14", "0.00E-12",
      "0.00000000000000", "0.000000000000000000e+10", "0E-8", "0.00E-6",
      "0.00000000", "0.0000e+19", "0E+15", "0E+15", "0", "0.00000e-8",
      "0E-13", "0.0E-12", "0.0000000000000", "0.00000000000e+14", "0E+3",
      "0E+3", "0", "0.000e-14", "0E-17", "0.00E-15",
      "0.00000000000000000", "0.000000e-19", "0E-25", "0.0E-24",
      "0.0000000000000000000000000", "0.000000000000e+19", "0E+7",
      "0.00E+9", "0", "0.0000000000000e+18", "0E+5", "0.0E+6", "0",
      "0.00000000000000e-2", "0E-16", "0.0E-15", "0.0000000000000000",
      "0.0000000000000e-18", "0E-31", "0.0E-30",
      "0." + TestCommon.Repeat("0", 31) + String.Empty, "0e-17", "0E-17",
      "0.00E-15",
      "0.00000000000000000", "0e+17", "0E+17", "0.0E+18", "0",
      "0.00000000000000000e+0", "0E-17", "0.00E-15",
      "0.00000000000000000", "0.0000000000000e+0", "0E-13", "0.0E-12",
      "0.0000000000000", "0.0000000000000000000e-12", "0E-31", "0.0E-30",
      "0." + TestCommon.Repeat("0", 31) + String.Empty,
      "0.0000000000000000000e+10",
      "0E-9", "0E-9", "0.000000000", "0.00000e-2", "0E-7", "0.0E-6",
      "0.0000000", "0.000000e+15", "0E+9", "0E+9", "0",
      "0.000000000e-10", "0E-19", "0.0E-18", "0.0000000000000000000",
      "0.00000000000000e+6", "0E-8", "0.00E-6", "0.00000000",
      "0.00000e+17", "0E+12", "0E+12", "0", "0.000000000000000000e-0",
      "0E-18", "0E-18", "0.000000000000000000", "0.0000000000000000e+11",
      "0.00000", "0.00000", "0.00000", "0.000000000000e+15", "0E+3",
      "0E+3", "0", "0.00000000e-19", "0E-27", "0E-27",
      "0.000000000000000000000000000", "0.00000e-6", "0E-11", "0.00E-9",
      "0.00000000000", "0e-14", "0E-14", "0.00E-12", "0.00000000000000",
      "0.000000000e+9", "0", "0", "0", "0.00000e+13", "0E+8",
      "0.0E+9", "0", "0.000e-0", "0.000", "0.000", "0.000",
      "0.000000000000000e+6", "0E-9", "0E-9", "0.000000000",
      "0.000000000e+17", "0E+8", "0.0E+9", "0", "0.00000000000e+6",
      "0.00000", "0.00000", "0.00000", "0.00000000000000e+3", "0E-11",
      "0.00E-9", "0.00000000000", "0e+0", "0", "0", "0", "0.000e+12",
      "0E+9", "0E+9", "0", "0.00000000000e+9", "0.00", "0.00", "0.00",
      "0.00000000000000e-9", "0E-23", "0.00E-21",
      "0.00000000000000000000000", "0e-1", "0.0", "0.0", "0.0",
      "0.0000e-13", "0E-17", "0.00E-15", "0.00000000000000000",
      "0.00000000000e-7", "0E-18", "0E-18", "0.000000000000000000",
      "0.00000000000000e+4", "0E-10", "0.0E-9", "0.0000000000",
      "0.00000000e-8", "0E-16", "0.0E-15", "0.0000000000000000",
      "0.00e-6", "0E-8", "0.00E-6", "0.00000000", "0.0e-1", "0.00",
      "0.00", "0.00", "0.0000000000000000e-10", "0E-26", "0.00E-24",
      "0.00000000000000000000000000", "0.00e+14", "0E+12", "0E+12", "0",
      "0.000000000000000000e+5", "0E-13", "0.0E-12", "0.0000000000000",
      "0.0e+7", "0E+6", "0E+6", "0", "0.00000000e+8", "0", "0", "0",
      "0.000000000e+0", "0E-9", "0E-9", "0.000000000", "0.000e+13",
      "0E+10", "0.00E+12", "0", "0.0000000000000000e+16", "0", "0",
      "0", "0.00000000e-1", "0E-9", "0E-9", "0.000000000",
      "0.00000000000e-15", "0E-26", "0.00E-24",
      "0.00000000000000000000000000", "0.0e+11", "0E+10", "0.00E+12",
      "0", "0.00000e+7", "0E+2", "0.0E+3", "0",
      "0.0000000000000000000e-19", "0E-38", "0.00E-36",
      "0." + TestCommon.Repeat("0", 38) + String.Empty, "0.0000000000e-6",
      "0E-16", "0.0E-15", "0.0000000000000000", "0.00000000000000000e-15",
      "0E-32", "0.00E-30", "0." + TestCommon.Repeat("0", 32) + String.Empty,
      "0.000000000000000e+2", "0E-13", "0.0E-12", "0.0000000000000",
      "0.0e-18", "0E-19", "0.0E-18", "0.0000000000000000000",
      "0.00000000000000e-6", "0E-20", "0.00E-18",
      "0.00000000000000000000", "0.000e-17", "0E-20", "0.00E-18",
      "0.00000000000000000000", "0.00000000000000e-7", "0E-21", "0E-21",
      "0.000000000000000000000", "0.000000e-9", "0E-15", "0E-15",
      "0.000000000000000", "0e-11", "0E-11", "0.00E-9", "0.00000000000",
      "0.000000000e+11", "0E+2", "0.0E+3", "0",
      "0.0000000000000000e+15", "0.0", "0.0", "0.0",
      "0.0000000000000000e+10", "0.000000", "0.000000", "0.000000",
      "0.000000000e+4", "0.00000", "0.00000", "0.00000",
      "0.000000000000000e-13", "0E-28", "0.0E-27",
      "0.0000000000000000000000000000", "0.0000000000000000000e-8",
      "0E-27", "0E-27", "0.000000000000000000000000000",
      "0.00000000000e-15", "0E-26", "0.00E-24",
      "0.00000000000000000000000000", "0.00e+12", "0E+10", "0.00E+12",
      "0", "0.0e+5", "0E+4", "0.00E+6", "0", "0.0000000000000000e+7",
      "0E-9", "0E-9", "0.000000000", "0.0000000000000000e-0", "0E-16",
      "0.0E-15", "0.0000000000000000", "0.000000000000000e+13", "0.00",
      "0.00", "0.00", "0.00000000000e-13", "0E-24", "0E-24",
      "0.000000000000000000000000", "0.000e-10", "0E-13", "0.0E-12",
      "0.0000000000000",
    };
    [Test]
    public void TestAbs() {
      // not implemented yet
    }

    private static void TestAddCloseExponent(RandomGenerator fr, int exp) {
      for (var i = 0; i < 1000; ++i) {
        EInteger exp1 = EInteger.FromInt32(exp)
          .Add(EInteger.FromInt32(fr.UniformInt(32) - 16));
        EInteger exp2 = exp1.Add(EInteger.FromInt32(fr.UniformInt(18) - 9));
        EInteger mant1 = RandomObjects.RandomEInteger(fr);
        EInteger mant2 = RandomObjects.RandomEInteger(fr);
        EDecimal decA = EDecimal.Create(mant1, exp1);
        EDecimal decB = EDecimal.Create(mant2, exp2);
        EDecimal decC = decA.Add(decB);
        EDecimal decD = decC.Subtract(decA);
        TestCommon.CompareTestEqual(decD, decB);
        decD = decC.Subtract(decB);
        TestCommon.CompareTestEqual(decD, decA);
      }
    }
    [Test]
    public void TestFromBoolean() {
      Assert.AreEqual(EDecimal.Zero, EDecimal.FromBoolean(false));
      Assert.AreEqual(EDecimal.One, EDecimal.FromBoolean(true));
    }

    [Test]
    public void TestPrecisionOneHalfEven() {
      EDecimal edec = EDecimal.FromString("9.5e-1");
      EContext ectx = EContext.ForPrecisionAndRounding(1, ERounding.HalfEven);
      edec = edec.RoundToPrecision(ectx);
      TestCommon.CompareTestEqual(
        EDecimal.FromString("10.0e-1"),
        edec);
    }

    [Test]
    public void TestAdd() {
      try {
        EDecimal.Zero.Add(null, EContext.Unlimited);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var fr = new RandomGenerator();
      TestAddCloseExponent(fr, 0);
      TestAddCloseExponent(fr, 100);
      TestAddCloseExponent(fr, -100);
      TestAddCloseExponent(fr, Int32.MinValue);
      TestAddCloseExponent(fr, Int32.MaxValue);
      AssertAddSubtract("617862143", "1528127703");
    }

    [Test]
    public void TestAddThenCompare() {
      EDecimal a = EDecimal.FromString(
  "3432401181884624580219161996277760227145481682978308767347063168426989874100957186809774969532587926005597200790737572030389681269702414428117526594285731840");
      a = a.Add(
          EDecimal.FromString("18895577316172922617856"));
      EDecimal b = EDecimal.FromString(
  "3432401181884624580219161996277760227145481682978308767347063168426989874100957186809774969532587926005597200790737572030389681269702433323694842767208349696");
      Assert.AreEqual(a.ToString(), b.ToString());
      TestCommon.CompareTestEqual(a, b, String.Empty);
      Assert.AreEqual(a.Sign, b.Sign);
    }

    [Test]
    public void TestCompareTo() {
      var r = new RandomGenerator();
      for (var i = 0; i < 500; ++i) {
        EDecimal bigintA = RandomObjects.RandomEDecimal(r);
        EDecimal bigintB = RandomObjects.RandomEDecimal(r);
        EDecimal bigintC = RandomObjects.RandomEDecimal(r);
        TestCommon.CompareTestRelations(bigintA, bigintB, bigintC);
      }
      // Test equivalence of EInteger and EDecimal for integers
      for (var i = 0; i < 3000; ++i) {
        EInteger bigintA = RandomObjects.RandomEInteger(r);
        EInteger bigintB = RandomObjects.RandomEInteger(r);
        EInteger bigintC = bigintA.Add(bigintB);
        EDecimal ba1 = EDecimal.FromEInteger(bigintA)
          .Add(EDecimal.FromEInteger(bigintB));
        EDecimal ba2 = EDecimal.FromEInteger(bigintC);
        Assert.AreEqual(ba1.Sign, ba2.Sign);
        Assert.AreEqual(ba1.ToString(), ba2.ToString());
        TestCommon.CompareTestEqual(
          ba1,
          ba2,
          bigintA.ToString() + "/" + bigintB.ToString());
      }
      TestCommon.CompareTestEqual(
        EDecimal.FromString("-1.603074425947290000E+2147483671"),
        EDecimal.FromString("-1.60307442594729E+2147483671"));
      TestCommon.CompareTestLess(EDecimal.Zero, EDecimal.NaN);
      TestCommon.CompareTestLess(
        EDecimal.FromString("-4328117878201602191937590091183.9810549"),
        EDecimal.FromString("-14"));
      TestCommon.CompareTestGreater(
        EDecimal.FromString("937125319376706291597172.99"),
        EDecimal.FromString("9755.2823"));
      TestCommon.CompareTestLess(
        EDecimal.FromString("95"),
        EDecimal.FromString("1.41189247453434859259019E+26"));
      TestCommon.CompareTestGreater(
        EDecimal.FromString("379351600076111561037"),
        EDecimal.FromString("8451910"));
    }
    [Test]
    [Timeout(100000)]
    public void TestCompareToBinary() {
      {
        long numberTemp = EDecimal.NegativeInfinity.CompareToBinary(null);
        Assert.AreEqual(1, numberTemp);
      }
      {
        long numberTemp = EDecimal.PositiveInfinity.CompareToBinary(null);
        Assert.AreEqual(1, numberTemp);
      }
      Assert.AreEqual(1, EDecimal.NaN.CompareToBinary(null));
      Assert.AreEqual(1, EDecimal.SignalingNaN.CompareToBinary(null));
      Assert.AreEqual(1, EDecimal.Zero.CompareToBinary(null));
      Assert.AreEqual(1, EDecimal.One.CompareToBinary(null));
      {
        long numberTemp = EDecimal.NaN.CompareToBinary(EFloat.NaN);
        Assert.AreEqual(0, numberTemp);
      }

      {
        long numberTemp =
          EDecimal.SignalingNaN.CompareToBinary(EFloat.NaN);
        Assert.AreEqual(0, numberTemp);
      }

      {
        long numberTemp =
          EDecimal.NaN.CompareToBinary(EFloat.SignalingNaN);
        Assert.AreEqual(0, numberTemp);
      }

      {
        long numberTemp =
          EDecimal.SignalingNaN.CompareToBinary(EFloat.SignalingNaN);
        Assert.AreEqual(0, numberTemp);
      }
      {
        long numberTemp = EDecimal.NaN.CompareToBinary(EFloat.Zero);
        Assert.AreEqual(1, numberTemp);
      }

      {
        long numberTemp =
          EDecimal.SignalingNaN.CompareToBinary(EFloat.Zero);
        Assert.AreEqual(1, numberTemp);
      }
      var r = new RandomGenerator();
      for (var i = 0; i < 30000; ++i) {
        EInteger bigintA = RandomObjects.RandomEInteger(r);
        int cmp = EDecimal.FromEInteger(bigintA).CompareToBinary(
            EFloat.FromEInteger(bigintA));
        Assert.AreEqual(0, cmp);
      }
    }

    public void TestCompareToBinarySpecific1() {
      EFloat ef;
      EDecimal ed;
      ef = EFloat.Create(
  EInteger.FromString("-9664843630203122783591902764846273689479381470150824364957816856082399492757121214693125730981133439908"),
  -17247);
      ed = EDecimal.FromEFloat(ef);
      Assert.AreEqual(0, ed.CompareToBinary(ef));
      ef = EFloat.Create(
  EInteger.FromString("-2467365858090833230674801431449857270755907368718802724012643203913243742810052368602482131073985474496270026402026901711301805506483138316932981526091018161243282613098255694573218797136340778209470766121110562843973682548635610931370244160852853706133513187695008443087958097846576792398400512366911345362406722788893972946659265028631132327775167697067574535402565194880977992197706600276103283316072610566206275168152573770312869585911831390513851119697252481532540704706697299199745684224875503861044195414865414501211853220204780962526179105198234107560421422472675347263283990608089697319048891315126404866394172515587870915984406737373646406191043645162091665444977123227178202520024830788089974236992351854383678539013034083419874455158726412688896635633819017324077511735883476845008734150221821164999757875534151181805265806734244829066298005793659243034786743021850398154955226684325077441797994355484078284651814522091252889035546936605403702617900755410567544044017356945337917854344068537582739700174480391485976108360704358539233556356055416272178277957515564601573063597136628104876885194939300236889051122715898223511987845895344284500599930150533252352792434580138801671556601310092800472408049970821810559885509976187264868852561693833270555422604022272867933389426568067071959374066789450739921141187691825889495918119483261575309"),
  -11254);
      ed = EDecimal.FromEFloat(ef);
      Assert.AreEqual(0, ed.CompareToBinary(ef));
      ef = EFloat.Create(
  EInteger.FromString("-325087637545375466523593319915642000166507448768479353748943585507647929726919984092640176973845417409613528524759214900494790101204316104560025164009709666878119359566275239162086759961271534412991410161699457719070992680179292505"),
  -22909);
      ed = EDecimal.FromEFloat(ef);
      Assert.AreEqual(0, ed.CompareToBinary(ef));
    }

    [Test]
    [Timeout(1000)]
    public void TestSlowCompareTo() {
      EInteger ei = EInteger.FromString(
  "-108854259699738613336073386912819333959164543792902007057925129910904321192623590227704182838777516070192327852552376209933022606");
      EFloat ef = EFloat.Create(
          ei,
          EInteger.FromString("-94432713210"));
      EDecimal ed = EDecimal.FromString("-0.00007");
      Assert.AreEqual(-1, ed.CompareToBinary(ef));
    }
    [Test]
    [Timeout(1000)]
    public void TestSlowCompareTo2() {
      EFloat ef = EFloat.Create(
          EInteger.FromString("310698658007725142033104896"),
          EInteger.FromString("-910015527228"));
      EDecimal ed = EDecimal.FromString(
  "5.46812681195752988681792163205092489269012868995370381431608431437654836803981061017608940175753472E-373278497416");
      Assert.AreEqual(-1, ed.CompareToBinary(ef));
    }
    [Test]
    [Timeout(1000)]
    public void TestSlowCompareTo3() {
      EDecimal ed;
      EFloat ef;
      string str;
      str =

  "1766847170502052161990715830264538670879951287225036514637396697134727424";
      ef = EFloat.Create(
          EInteger.FromString(str),
          EInteger.FromString("-312166824097095580"));
      str =

  "9.173994463968662338877236893297097756859177826848079536001717300706132083132181223420891571892014689615873E-411";
      ed = EDecimal.FromString(str);
      Assert.AreEqual(1, ed.CompareToBinary(ef), ed.ToString());
      ed = EDecimal.FromString("-0.8686542656448184");
      EInteger num = EInteger.FromString(
  "-140066031252330072924596216562033152419723178072587092376847513280411121126147871380984127579961289495006067586678128473926216639728812381688517268223431349786843141449122136993998169636988109708853983609451615499412285220750795244924615776386873830928453488263516664209329914433973932921432682935336466252311348743988191166143");

      ef = EFloat.Create(
          num,
          EInteger.FromString("-6881037062769847"));
      Assert.AreEqual(-1, ed.CompareToBinary(ef), ed.ToString());
    }
    [Test]
    [Timeout(1000)]
    public void TestSlowCompareTo4() {
      EInteger eim =
        EInteger.FromString("22387857484482745027162156293292508271673600");
      EInteger eie = EInteger.FromString("17968626318971");
      EDecimal ed = EDecimal.FromString(
          "7.19575518693181831266567929996929334493885016E+432");
      EFloat ef = EFloat.Create(eim, eie);
      Assert.AreEqual(-1, ed.CompareToBinary(ef));
      eim = EInteger.FromString("309485028268090241945960705");
      eie = EInteger.FromString("525342875590");
      ed =

        EDecimal.FromString(
  "9.511414777277089412154948033116658722787183213120804541938141882272749696679385407387275461761800238977533242480831603777061215911374370925809077057683501541910383022943115134850573547079633633752563620027531228739865573373209036911484539031800435471797748936642897560822226476374652683917217409048036924712889788014206259609E+676");
      ef = EFloat.Create(eim, eie);
      Assert.AreEqual(-1, ed.CompareToBinary(ef));
    }
    [Test]
    public void TestCompareToSignal() {
      // not implemented yet
    }

    [Test]
    public void TestIsInteger() {
      EDecimal ed = EDecimal.NaN;
      Assert.IsFalse(ed.IsInteger());
      ed = EDecimal.SignalingNaN;
      Assert.IsFalse(ed.IsInteger());
      ed = EDecimal.PositiveInfinity;
      Assert.IsFalse(ed.IsInteger());
      ed = EDecimal.NegativeInfinity;
      Assert.IsFalse(ed.IsInteger());
      ed = EDecimal.NegativeZero;
      Assert.IsTrue(ed.IsInteger());
      ed = EDecimal.FromInt32(0);
      Assert.IsTrue(ed.IsInteger());
      ed = EDecimal.FromInt32(999);
      Assert.IsTrue(ed.IsInteger());
      ed = EDecimal.Create(999, 999);
      Assert.IsTrue(ed.IsInteger());
      ed = EDecimal.Create(999, -999);
      Assert.IsFalse(ed.IsInteger());
    }

    [Test]
    [Timeout(100000)]
    public void TestConversions() {
      var fr = new RandomGenerator();
      for (var i = 0; i < 20000; ++i) {
        EDecimal enumber = RandomObjects.RandomEDecimal(fr);
        TestConversionsOne(enumber);
      }
      TestConversionsOne(EDecimal.FromString("-0.8995"));
      TestConversionsOne(EDecimal.FromString("-4.061532283038E+14"));
    }
    public static void TestConversionsOne(EDecimal enumber) {
      bool isNum, isTruncated, isInteger;
      EInteger eint;
      if (enumber == null) {
        throw new ArgumentNullException(nameof(enumber));
      }
      if (!enumber.IsFinite) {
        try {
          enumber.ToByteChecked();
          Assert.Fail("Should have failed");
        } catch (OverflowException) {
          // NOTE: Intentionally empty
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
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
        try {
          enumber.ToInt16Checked();
          Assert.Fail("Should have failed");
        } catch (OverflowException) {
          // NOTE: Intentionally empty
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
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
        try {
          enumber.ToInt32Checked();
          Assert.Fail("Should have failed");
        } catch (OverflowException) {
          // NOTE: Intentionally empty
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
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
        try {
          enumber.ToInt64Checked();
          Assert.Fail("Should have failed");
        } catch (OverflowException) {
          // NOTE: Intentionally empty
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
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
        return;
      }
      try {
        eint = (enumber.Exponent.CompareTo(100) >= 0 && !enumber.IsZero) ?
null :
          enumber.ToEInteger();
      } catch (NotSupportedException) {
        eint = null;
      }
      isInteger = enumber.IsInteger();
      isNum = enumber.CompareTo(0) >= 0 && enumber.CompareTo(255) <= 0;
      isTruncated = eint != null && eint.CompareTo(0) >= 0 &&
        eint.CompareTo(255) <= 0;
      if (isNum) {
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromByte(enumber.ToByteChecked()));
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromByte(enumber.ToByteUnchecked()));
        if (isInteger) {
          TestCommon.AssertEqualsHashCode(
            eint,
            EInteger.FromByte(enumber.ToByteIfExact()));
        } else {
          try {
            enumber.ToByteIfExact();
            Assert.Fail("Should have failed");
          } catch (ArithmeticException) {
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      } else if (isTruncated) {
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromByte(enumber.ToByteChecked()));
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromByte(enumber.ToByteUnchecked()));
        try {
          enumber.ToByteIfExact();
          Assert.Fail("Should have failed");
        } catch (ArithmeticException) {
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      } else {
        try {
          enumber.ToByteChecked();
          Assert.Fail("Should have failed");
        } catch (OverflowException) {
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString() + " " + isTruncated + " eint=" + eint);
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
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        } else {
          try {
            enumber.ToByteIfExact();
            Assert.Fail("Should have failed");
          } catch (ArithmeticException) {
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      }
      isNum = enumber.CompareTo(
          EDecimal.FromString("-32768")) >= 0 && enumber.CompareTo(
          EDecimal.FromString("32767")) <= 0;
      isTruncated = eint != null && eint.CompareTo(
          EInteger.FromString("-32768")) >= 0 && eint.CompareTo(
          EInteger.FromString("32767")) <= 0;
      if (isNum) {
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt16(enumber.ToInt16Checked()));
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt16(enumber.ToInt16Unchecked()));
        if (isInteger) {
          TestCommon.AssertEqualsHashCode(
            eint,
            EInteger.FromInt16(enumber.ToInt16IfExact()));
        } else {
          try {
            enumber.ToInt16IfExact();
            Assert.Fail("Should have failed");
          } catch (ArithmeticException) {
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      } else if (isTruncated) {
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt16(enumber.ToInt16Checked()));
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt16(enumber.ToInt16Unchecked()));
        try {
          enumber.ToInt16IfExact();
          Assert.Fail("Should have failed");
        } catch (ArithmeticException) {
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      } else {
        try {
          enumber.ToInt16Checked();
          Assert.Fail("Should have failed");
        } catch (OverflowException) {
          // NOTE: Intentionally empty
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
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        } else {
          try {
            enumber.ToInt16IfExact();
            Assert.Fail("Should have failed");
          } catch (ArithmeticException) {
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      }
      isNum = enumber.CompareTo(
          EDecimal.FromString("-2147483648")) >= 0 && enumber.CompareTo(
          EDecimal.FromString("2147483647")) <= 0;
      isTruncated = eint != null && eint.CompareTo(
          EInteger.FromString("-2147483648")) >= 0 &&
        eint.CompareTo(
          EInteger.FromString("2147483647")) <= 0;
      if (isNum) {
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt32(enumber.ToInt32Checked()));
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt32(enumber.ToInt32Unchecked()));
        if (isInteger) {
          TestCommon.AssertEqualsHashCode(
            eint,
            EInteger.FromInt32(enumber.ToInt32IfExact()));
        } else {
          try {
            enumber.ToInt32IfExact();
            Assert.Fail("Should have failed");
          } catch (ArithmeticException) {
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      } else if (isTruncated) {
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt32(enumber.ToInt32Checked()));
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt32(enumber.ToInt32Unchecked()));
        try {
          enumber.ToInt32IfExact();
          Assert.Fail("Should have failed");
        } catch (ArithmeticException) {
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      } else {
        try {
          enumber.ToInt32Checked();
          Assert.Fail("Should have failed");
        } catch (OverflowException) {
          // NOTE: Intentionally empty
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
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        } else {
          try {
            enumber.ToInt32IfExact();
            Assert.Fail("Should have failed");
          } catch (ArithmeticException) {
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      }
      isTruncated = eint != null && eint.CompareTo(
          EInteger.FromString("-9223372036854775808")) >= 0 &&
        eint.CompareTo(
          EInteger.FromString("9223372036854775807")) <= 0;
      isNum = isTruncated && enumber.CompareTo(
          EDecimal.FromString("-9223372036854775808")) >= 0 &&
        enumber.CompareTo(
          EDecimal.FromString("9223372036854775807")) <= 0;
      if (isNum) {
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt64(enumber.ToInt64Checked()));
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt64(enumber.ToInt64Unchecked()));
        if (isInteger) {
          TestCommon.AssertEqualsHashCode(
            eint,
            EInteger.FromInt64(enumber.ToInt64IfExact()));
        } else {
          try {
            enumber.ToInt64IfExact();
            Assert.Fail("Should have failed");
          } catch (ArithmeticException) {
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      } else if (isTruncated) {
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt64(enumber.ToInt64Checked()));
        TestCommon.AssertEqualsHashCode(
          eint,
          EInteger.FromInt64(enumber.ToInt64Unchecked()));
        try {
          enumber.ToInt64IfExact();
          Assert.Fail("Should have failed");
        } catch (ArithmeticException) {
          // NOTE: Intentionally empty
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      } else {
        try {
          enumber.ToInt64Checked();
          Assert.Fail("Should have failed");
        } catch (OverflowException) {
          // NOTE: Intentionally empty
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
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        } else {
          try {
            enumber.ToInt64IfExact();
            Assert.Fail("Should have failed");
          } catch (ArithmeticException) {
            // NOTE: Intentionally empty
          } catch (Exception ex) {
            Assert.Fail(ex.ToString());
            throw new InvalidOperationException(String.Empty, ex);
          }
        }
      }
    }

    [Test]
    public void TestCompareToWithContext() {
      // not implemented yet
    }
    [Test]
    public void TestCreate() {
      try {
        EDecimal.Create(null, EInteger.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.Create(null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.Create(EInteger.One, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestCreateNaN() {
      try {
        EDecimal.CreateNaN(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.CreateNaN(EInteger.FromString("-1"));
        Assert.Fail("Should have failed");
      } catch (ArgumentException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.CreateNaN(null, false, false, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      EDecimal ef = EDecimal.CreateNaN(EInteger.Zero, false, true, null);
      Assert.IsTrue(ef.IsNegative);
      ef = EDecimal.CreateNaN(EInteger.Zero, false, false, null);
      Assert.IsTrue(!ef.IsNegative);
    }

    [Test]
    public void TestDecimalsEquivalent() {
      AssertDecimalsEquivalent("1.310E-7", "131.0E-9");
      AssertDecimalsEquivalent("0.001231", "123.1E-5");
      AssertDecimalsEquivalent("3.0324E+6", "303.24E4");
      AssertDecimalsEquivalent("3.726E+8", "372.6E6");
      AssertDecimalsEquivalent("2663.6", "266.36E1");
      AssertDecimalsEquivalent("34.24", "342.4E-1");
      AssertDecimalsEquivalent("3492.5", "349.25E1");
      AssertDecimalsEquivalent("0.31919", "319.19E-3");
      AssertDecimalsEquivalent("2.936E-7", "293.6E-9");
      AssertDecimalsEquivalent("6.735E+10", "67.35E9");
      AssertDecimalsEquivalent("7.39E+10", "7.39E10");
      AssertDecimalsEquivalent("0.0020239", "202.39E-5");
      AssertDecimalsEquivalent("1.6717E+6", "167.17E4");
      AssertDecimalsEquivalent("1.7632E+9", "176.32E7");
      AssertDecimalsEquivalent("39.526", "395.26E-1");
      AssertDecimalsEquivalent("0.002939", "29.39E-4");
      AssertDecimalsEquivalent("0.3165", "316.5E-3");
      AssertDecimalsEquivalent("3.7910E-7", "379.10E-9");
      AssertDecimalsEquivalent("0.000016035", "160.35E-7");
      AssertDecimalsEquivalent("0.001417", "141.7E-5");
      AssertDecimalsEquivalent("7.337E+5", "73.37E4");
      AssertDecimalsEquivalent("3.4232E+12", "342.32E10");
      AssertDecimalsEquivalent("2.828E+8", "282.8E6");
      AssertDecimalsEquivalent("4.822E-7", "48.22E-8");
      AssertDecimalsEquivalent("2.6328E+9", "263.28E7");
      AssertDecimalsEquivalent("2.9911E+8", "299.11E6");
      AssertDecimalsEquivalent("3.636E+9", "36.36E8");
      AssertDecimalsEquivalent("0.20031", "200.31E-3");
      AssertDecimalsEquivalent("1.922E+7", "19.22E6");
      AssertDecimalsEquivalent("3.0924E+8", "309.24E6");
      AssertDecimalsEquivalent("2.7236E+7", "272.36E5");
      AssertDecimalsEquivalent("0.01645", "164.5E-4");
      AssertDecimalsEquivalent("0.000292", "29.2E-5");
      AssertDecimalsEquivalent("1.9939", "199.39E-2");
      AssertDecimalsEquivalent("2.7929E+9", "279.29E7");
      AssertDecimalsEquivalent("1.213E+7", "121.3E5");
      AssertDecimalsEquivalent("2.765E+6", "276.5E4");
      AssertDecimalsEquivalent("270.11", "270.11E0");
      AssertDecimalsEquivalent("0.017718", "177.18E-4");
      AssertDecimalsEquivalent("0.003607", "360.7E-5");
      AssertDecimalsEquivalent("0.00038618", "386.18E-6");
      AssertDecimalsEquivalent("0.0004230", "42.30E-5");
      AssertDecimalsEquivalent("1.8410E+5", "184.10E3");
      AssertDecimalsEquivalent("0.00030427", "304.27E-6");
      AssertDecimalsEquivalent("6.513E+6", "65.13E5");
      AssertDecimalsEquivalent("0.06717", "67.17E-3");
      AssertDecimalsEquivalent("0.00031123", "311.23E-6");
      AssertDecimalsEquivalent("0.0031639", "316.39E-5");
      AssertDecimalsEquivalent("1.146E+5", "114.6E3");
      AssertDecimalsEquivalent("0.00039937", "399.37E-6");
      AssertDecimalsEquivalent("3.3817", "338.17E-2");
      AssertDecimalsEquivalent("0.00011128", "111.28E-6");
      AssertDecimalsEquivalent("7.818E+7", "78.18E6");
      AssertDecimalsEquivalent("2.6417E-7", "264.17E-9");
      AssertDecimalsEquivalent("1.852E+9", "185.2E7");
      AssertDecimalsEquivalent("0.0016216", "162.16E-5");
      AssertDecimalsEquivalent("2.2813E+6", "228.13E4");
      AssertDecimalsEquivalent("3.078E+12", "307.8E10");
      AssertDecimalsEquivalent("0.00002235", "22.35E-6");
      AssertDecimalsEquivalent("0.0032827", "328.27E-5");
      AssertDecimalsEquivalent("1.334E+9", "133.4E7");
      AssertDecimalsEquivalent("34.022", "340.22E-1");
      AssertDecimalsEquivalent("7.19E+6", "7.19E6");
      AssertDecimalsEquivalent("35.311", "353.11E-1");
      AssertDecimalsEquivalent("3.4330E+6", "343.30E4");
      AssertDecimalsEquivalent("0.000022923", "229.23E-7");
      AssertDecimalsEquivalent("2.899E+4", "289.9E2");
      AssertDecimalsEquivalent("0.00031", "3.1E-4");
      AssertDecimalsEquivalent("2.0418E+5", "204.18E3");
      AssertDecimalsEquivalent("3.3412E+11", "334.12E9");
      AssertDecimalsEquivalent("1.717E+10", "171.7E8");
      AssertDecimalsEquivalent("2.7024E+10", "270.24E8");
      AssertDecimalsEquivalent("1.0219E+9", "102.19E7");
      AssertDecimalsEquivalent("15.13", "151.3E-1");
      AssertDecimalsEquivalent("91.23", "91.23E0");
      AssertDecimalsEquivalent("3.4114E+6", "341.14E4");
      AssertDecimalsEquivalent("33.832", "338.32E-1");
      AssertDecimalsEquivalent("0.19234", "192.34E-3");
      AssertDecimalsEquivalent("16835", "168.35E2");
      AssertDecimalsEquivalent("0.00038610", "386.10E-6");
      AssertDecimalsEquivalent("1.6624E+9", "166.24E7");
      AssertDecimalsEquivalent("2.351E+9", "235.1E7");
      AssertDecimalsEquivalent("0.03084", "308.4E-4");
      AssertDecimalsEquivalent("0.00429", "42.9E-4");
      AssertDecimalsEquivalent("9.718E-8", "97.18E-9");
      AssertDecimalsEquivalent("0.00003121", "312.1E-7");
      AssertDecimalsEquivalent("3.175E+4", "317.5E2");
      AssertDecimalsEquivalent("376.6", "376.6E0");
      AssertDecimalsEquivalent("0.0000026110", "261.10E-8");
      AssertDecimalsEquivalent("7.020E+11", "70.20E10");
      AssertDecimalsEquivalent("2.1533E+9", "215.33E7");
      AssertDecimalsEquivalent("3.8113E+7", "381.13E5");
      AssertDecimalsEquivalent("7.531", "75.31E-1");
      AssertDecimalsEquivalent("991.0", "99.10E1");
      AssertDecimalsEquivalent("2.897E+8", "289.7E6");
      AssertDecimalsEquivalent("0.0000033211", "332.11E-8");
      AssertDecimalsEquivalent("0.03169", "316.9E-4");
      AssertDecimalsEquivalent("2.7321E+12", "273.21E10");
      AssertDecimalsEquivalent("394.38", "394.38E0");
      AssertDecimalsEquivalent("5.912E+7", "59.12E6");
    }

    [Test]
    public void TestCompareToNull() {
      TestCommon.CompareTestLess(0, EInteger.Zero.CompareTo(null));

      TestCommon.CompareTestLess(0, EDecimal.Zero.CompareTo(null));
      TestCommon.CompareTestLess(0, EDecimal.Zero.CompareToBinary(null));
      {
        int integerTemp2 = EDecimal.Zero.CompareToTotal(null,
            EContext.Unlimited);
        TestCommon.CompareTestLess(0, integerTemp2);
      }
      {
        int integerTemp2 = EDecimal.Zero.CompareToTotalMagnitude(null,
            EContext.Unlimited);
        TestCommon.CompareTestLess(0, integerTemp2);
      }
      TestCommon.CompareTestLess(0, EDecimal.Zero.CompareToTotal(null, null));
      {
        int integerTemp2 = EDecimal.Zero.CompareToTotalMagnitude(null,
            null);
        TestCommon.CompareTestLess(0, integerTemp2);
      }
      TestCommon.CompareTestGreater(
        EDecimal.Zero.CompareToSignal(null, EContext.Unlimited),
        EDecimal.Zero);
      TestCommon.CompareTestGreater(
        EDecimal.Zero.CompareToWithContext(null, null),
        EDecimal.Zero);

      TestCommon.CompareTestLess(0, EFloat.Zero.CompareTo(null));
      {
        int integerTemp2 = EFloat.Zero.CompareToTotal(null,
            EContext.Unlimited);
        TestCommon.CompareTestLess(0, integerTemp2);
      }
      {
        int integerTemp2 = EFloat.Zero.CompareToTotalMagnitude(null,
            EContext.Unlimited);
        TestCommon.CompareTestLess(0, integerTemp2);
      }
      TestCommon.CompareTestLess(0, EFloat.Zero.CompareToTotal(null, null));
      TestCommon.CompareTestLess(0,
        EFloat.Zero.CompareToTotalMagnitude(null, null));
      TestCommon.CompareTestGreater(
        EFloat.Zero.CompareToSignal(null, EContext.Unlimited),
        EFloat.Zero);
      TestCommon.CompareTestGreater(
        EFloat.Zero.CompareToWithContext(null, null),
        EFloat.Zero);

      TestCommon.CompareTestLess(0, ERational.Zero.CompareTo(null));
      TestCommon.CompareTestLess(0, ERational.Zero.CompareToTotal(null));
      TestCommon.CompareTestLess(0,
        ERational.Zero.CompareToTotalMagnitude(null));
      TestCommon.CompareTestLess(0, ERational.Zero.CompareToBinary(null));
      TestCommon.CompareTestLess(0, ERational.Zero.CompareToDecimal(null));
      {
        int objectTemp2 = EDecimals.CompareTotal(
            EDecimal.Zero,
            null,
            EContext.Unlimited);
        TestCommon.CompareTestLess(0, objectTemp2);
      }
      {
        int objectTemp2 = EDecimals.CompareTotalMagnitude(
            EDecimal.Zero,
            null,
            EContext.Unlimited);
        TestCommon.CompareTestLess(0, objectTemp2);
      }
      {
        int objectTemp2 = EDecimals.CompareTotal(
            null,
            EDecimal.Zero,
            EContext.Unlimited);
        TestCommon.CompareTestGreater(0, objectTemp2);
      }
      {
        int objectTemp2 = EDecimals.CompareTotalMagnitude(
            null,
            EDecimal.Zero,
            EContext.Unlimited);
        TestCommon.CompareTestGreater(0, objectTemp2);
      }
      {
        int integerTemp2 = EDecimals.CompareTotal(
            null,
            null,
            EContext.Unlimited);
        Assert.AreEqual(0, integerTemp2);
      }
      {
        int objectTemp2 = EDecimals.CompareTotalMagnitude(
            null,
            null,
            EContext.Unlimited);
        Assert.AreEqual(0, objectTemp2);
      }
      {
        int integerTemp2 = EFloats.CompareTotal(
            EFloat.Zero,
            null,
            EContext.Unlimited);
        TestCommon.CompareTestLess(0, integerTemp2);
      }
      {
        int integerTemp2 = EFloats.CompareTotalMagnitude(
            EFloat.Zero,
            null,
            EContext.Unlimited);
        TestCommon.CompareTestLess(0, integerTemp2);
      }
      {
        int integerTemp2 = EFloats.CompareTotal(
            null,
            EFloat.Zero,
            EContext.Unlimited);
        TestCommon.CompareTestGreater(0, integerTemp2);
      }
      {
        int integerTemp2 = EFloats.CompareTotalMagnitude(null,
            EFloat.Zero,
            EContext.Unlimited);
        TestCommon.CompareTestGreater(0, integerTemp2);
      }
      {
        int integerTemp2 = EFloats.CompareTotal(
            null,
            null,
            EContext.Unlimited);
        Assert.AreEqual(0, integerTemp2);
      }
      {
        int objectTemp2 = EFloats.CompareTotalMagnitude(
            null,
            null,
            EContext.Unlimited);
        Assert.AreEqual(0, objectTemp2);
      }
    }

    [Test]
    public void TestDivide() {
      {
        string stringTemp = EDecimal.FromString(
            "1").Divide(EDecimal.FromInt32(8)).ToString();
        Assert.AreEqual(
          "0.125",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "10").Divide(EDecimal.FromInt32(80)).ToString();
        Assert.AreEqual(
          "0.125",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "10000").Divide(EDecimal.FromInt32(80000)).ToString();
        Assert.AreEqual(
          "0.125",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "1000").Divide(EDecimal.FromInt32(8)).ToString();
        Assert.AreEqual(
          "125",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "1").Divide(EDecimal.FromInt32(256)).ToString();
        Assert.AreEqual(
          "0.00390625",
          stringTemp);
      }
      var fr = new RandomGenerator();
      for (var i = 0; i < 5000; ++i) {
        EDecimal ed1 = RandomObjects.RandomEDecimal(fr);
        EDecimal ed2 = RandomObjects.RandomEDecimal(fr);
        if (!ed1.IsFinite || !ed2.IsFinite) {
          continue;
        }
        EDecimal ed3 = ed1.Multiply(ed2);
        Assert.IsTrue(ed3.IsFinite);
        EDecimal ed4;
        ed4 = ed3.Divide(ed1);
        if (!ed1.IsZero) {
          TestCommon.CompareTestEqual(ed4, ed2);
        } else {
          Assert.IsTrue(ed4.IsNaN());
        }
        ed4 = ed3.Divide(ed2);
        if (!ed2.IsZero) {
          TestCommon.CompareTestEqual(ed4, ed1);
        } else {
          Assert.IsTrue(ed4.IsNaN());
        }
      }
      try {
        EDecimal.FromString("1").Divide(EDecimal.FromString("3"), null);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestDivideToExponent() {
      // not implemented yet
    }
    [Test]
    public void TestDivideToIntegerNaturalScale() {
      // not implemented yet
    }
    [Test]
    public void TestDivideToIntegerZeroScale() {
      // not implemented yet
    }
    [Test]
    public void TestDivideToSameExponent() {
      // not implemented yet
    }
    [Test]
    public void TestEquals() {
      var r = new RandomGenerator();
      for (var i = 0; i < 500; ++i) {
        EDecimal bigintA = RandomObjects.RandomEDecimal(r);
        EDecimal bigintB = RandomObjects.RandomEDecimal(r);
        TestCommon.AssertEqualsHashCode(bigintA, bigintB);
      }
    }
    [Test]
    public void TestExp() {
      {
        string stringTemp =
          EDecimal.One.Exp(EContext.ForPrecision(25)).ToString();
        Assert.AreEqual(
          "2.718281828459045235360287",
          stringTemp);
      }
      {
        string stringTemp =
          EDecimal.One.Exp(EContext.ForPrecision(10)).ToString();
        Assert.AreEqual(
          "2.718281828",
          stringTemp);
      }
      {
        string stringTemp =
          EDecimal.One.Exp(EContext.ForPrecision(9)).ToString();
        Assert.AreEqual(
          "2.71828183",
          stringTemp);
      }
      {
        string stringTemp =
          EDecimal.One.Exp(EContext.ForPrecision(8)).ToString();
        Assert.AreEqual(
          "2.7182818",
          stringTemp);
      }
      {
        string stringTemp =
          EDecimal.One.Exp(EContext.ForPrecision(5)).ToString();
        Assert.AreEqual(
          "2.7183",
          stringTemp);
      }
      {
        string stringTemp =
          EDecimal.One.Exp(EContext.ForPrecision(3)).ToString();
        Assert.AreEqual(
          "2.72",
          stringTemp);
      }
      {
        string stringTemp =
          EDecimal.One.Exp(EContext.ForPrecision(1)).ToString();
        Assert.AreEqual(
          "3",
          stringTemp);
      }
    }
    [Test]
    public void TestExponent() {
      Assert.AreEqual(
        (EInteger)(-7),
        EDecimal.FromString("1.265e-4").Exponent);
      Assert.AreEqual(
        (EInteger)(-4),
        EDecimal.FromString("0.000E-1").Exponent);
      Assert.AreEqual(
        (EInteger)(-16),
        EDecimal.FromString("0.57484848535648e-2").Exponent);
      Assert.AreEqual(
        (EInteger)(-22),
        EDecimal.FromString("0.485448e-16").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-20);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.5657575351495151495649565150e+8").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)(-10),
        EDecimal.FromString("0e-10").Exponent);
      Assert.AreEqual(
        (EInteger)(-17),
        EDecimal.FromString("0.504952e-11").Exponent);
      Assert.AreEqual(
        (EInteger)(-13),
        EDecimal.FromString("0e-13").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-43);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.49495052535648555757515648e-17").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)7,
        EDecimal.FromString("0.485654575150e+19").Exponent);
      Assert.AreEqual(
        EInteger.Zero,
        EDecimal.FromString("0.48515648e+8").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-45);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.49485251485649535552535451544956e-13").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)(-6),
        EDecimal.FromString("0.565754515152575448505257e+18").Exponent);
      Assert.AreEqual(
        (EInteger)16,
        EDecimal.FromString("0e+16").Exponent);
      Assert.AreEqual(
        (EInteger)6,
        EDecimal.FromString("0.5650e+10").Exponent);
      Assert.AreEqual(
        (EInteger)(-5),
        EDecimal.FromString("0.49555554575756575556e+15").Exponent);
      Assert.AreEqual(
        (EInteger)(-37),
        EDecimal.FromString("0.57494855545057534955e-17").Exponent);
      Assert.AreEqual(
        (EInteger)(-25),
        EDecimal.FromString("0.4956504855525748575456e-3").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-26);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.55575355495654484948525354545053494854e+12").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      {
        EInteger bigintTemp = EInteger.FromInt64(-22);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.484853575350494950575749545057e+8").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)11,
        EDecimal.FromString("0.52545451e+19").Exponent);
      Assert.AreEqual(
        (EInteger)(-29),
        EDecimal.FromString("0.48485654495751485754e-9").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-38);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.56525456555549545257535556495655574848e+0").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      {
        EInteger bigintTemp = EInteger.FromInt64(-15);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.485456485657545752495450554857e+15").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)(-37),
        EDecimal.FromString("0.485448525554495048e-19").Exponent);
      Assert.AreEqual(
        (EInteger)(-29),
        EDecimal.FromString("0.494952485550514953565655e-5").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-8);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.50495454554854505051534950e+18").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      {
        EInteger bigintTemp = EInteger.FromInt64(-37);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.5156524853575655535351554949525449e-3").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)3,
        EDecimal.FromString("0e+3").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-8);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.51505056554957575255555250e+18").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)(-14),
        EDecimal.FromString("0.5456e-10").Exponent);
      Assert.AreEqual(
        (EInteger)(-36),
        EDecimal.FromString("0.494850515656505252555154e-12").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-42);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.535155525253485757525253555749575749e-6").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      {
        EInteger bigintTemp = EInteger.FromInt64(-29);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.56554952554850525552515549564948e+3").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      {
        EInteger bigintTemp = EInteger.FromInt64(-40);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.494855545257545656515554495057e-10").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)(-18),
        EDecimal.FromString("0.5656504948515252555456e+4").Exponent);
      Assert.AreEqual(
        (EInteger)(-17),
        EDecimal.FromString("0e-17").Exponent);
      Assert.AreEqual(
        (EInteger)(-32),
        EDecimal.FromString("0.55535551515249535049495256e-6").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-31);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.4948534853564853565654514855e-3").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)(-38),
        EDecimal.FromString("0.5048485057535249555455e-16").Exponent);
      Assert.AreEqual(
        (EInteger)(-16),
        EDecimal.FromString("0e-16").Exponent);
      Assert.AreEqual(
        (EInteger)5,
        EDecimal.FromString("0.5354e+9").Exponent);
      Assert.AreEqual(
        EInteger.One,
        EDecimal.FromString("0.54e+3").Exponent);
      {
        EInteger bigintTemp = EInteger.FromInt64(-38);
        EInteger bigintTemp2 = EDecimal.FromString(
            "0.4849525755545751574853494948e-10").Exponent;
        Assert.AreEqual(bigintTemp, bigintTemp2);
      }
      Assert.AreEqual(
        (EInteger)(-33),
        EDecimal.FromString("0.52514853565252565251565548e-7").Exponent);
      Assert.AreEqual(
        (EInteger)(-13),
        EDecimal.FromString("0.575151545652e-1").Exponent);
      Assert.AreEqual(
        (EInteger)(-22),
        EDecimal.FromString("0.49515354514852e-8").Exponent);
      Assert.AreEqual(
        (EInteger)(-24),
        EDecimal.FromString("0.54535357515356545554e-4").Exponent);
      Assert.AreEqual(
        (EInteger)(-11),
        EDecimal.FromString("0.574848e-5").Exponent);
      Assert.AreEqual(
        (EInteger)(-3),
        EDecimal.FromString("0.565055e+3").Exponent);
    }

    [Test]
    public void TestEDecimalDouble() {
      TestEDecimalDoubleCore(3.5, "3.5");
      TestEDecimalDoubleCore(7, "7");
      TestEDecimalDoubleCore(1.75, "1.75");
      TestEDecimalDoubleCore(3.5, "3.5");
      TestEDecimalDoubleCore((double)Int32.MinValue, "-2147483648");
      TestEDecimalDoubleCore(
        (double)Int64.MinValue,
        "-9223372036854775808");
      var rand = new RandomGenerator();
      for (var i = 0; i < 2047; ++i) {
        // Try a random double with a given
        // exponent
        TestEDecimalDoubleCore(
          RandomObjects.RandomDouble(rand, i),
          null);
        TestEDecimalDoubleCore(
          RandomObjects.RandomDouble(rand, i),
          null);
        TestEDecimalDoubleCore(
          RandomObjects.RandomDouble(rand, i),
          null);
        TestEDecimalDoubleCore(
          RandomObjects.RandomDouble(rand, i),
          null);
      }
    }
    [Test]
    public void TestEDecimalSingle() {
      var rand = new RandomGenerator();
      for (var i = 0; i < 255; ++i) {
        // Try a random float with a given
        // exponent
        TestEDecimalSingleCore(
          RandomObjects.RandomSingle(rand, i),
          null);
        TestEDecimalSingleCore(
          RandomObjects.RandomSingle(rand, i),
          null);
        TestEDecimalSingleCore(
          RandomObjects.RandomSingle(rand, i),
          null);
        TestEDecimalSingleCore(
          RandomObjects.RandomSingle(rand, i),
          null);
      }
    }

    [Test]
    public void TestFromEInteger() {
      var fr = new RandomGenerator();
      for (var i = 0; i < 5000; ++i) {
        EInteger ei = RandomObjects.RandomEInteger(fr);
        EDecimal edec = EDecimal.FromEInteger(ei);
        Assert.AreEqual(EInteger.Zero, edec.Exponent);
        Assert.AreEqual(ei, edec.Mantissa);
        Assert.AreEqual(ei, edec.ToEInteger());
      }
    }
    [Test]
    public void TestFromDouble() {
      string stringTemp;
      {
        stringTemp = EDecimal.FromDouble(0.75).ToString();
        Assert.AreEqual(
          "0.75",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.5).ToString();
        Assert.AreEqual(
          "0.5",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.25).ToString();
        Assert.AreEqual(
          "0.25",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.875).ToString();
        Assert.AreEqual(
          "0.875",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.125).ToString();
        Assert.AreEqual(
          "0.125",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.2133).ToString();
        Assert.AreEqual(
          "0.213299999999999989608312489508534781634807586669921875",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(2.2936E-7).ToString();
        {
          object objectTemp =

  "2.29360000000000010330982488752915582352898127282969653606414794921875E-7";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }
      {
        stringTemp = EDecimal.FromDouble(3.8932E9).ToString();
        Assert.AreEqual(
          "3893200000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(128230.0).ToString();
        Assert.AreEqual(
          "128230",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(127210.0).ToString();
        Assert.AreEqual(
          "127210",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.26723).ToString();
        Assert.AreEqual(
          "0.267230000000000023074875343809253536164760589599609375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.30233).ToString();
        Assert.AreEqual(
          "0.302329999999999987636556397774256765842437744140625",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(1.9512E-6).ToString();
        {
          object objectTemp =

  "0.0000019512000000000000548530838806460252499164198525249958038330078125";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }
      {
        stringTemp = EDecimal.FromDouble(199500.0).ToString();
        Assert.AreEqual(
          "199500",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(3.6214E7).ToString();
        Assert.AreEqual(
          "36214000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1.9133E12).ToString();
        Assert.AreEqual(
          "1913300000000",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(2.1735E-4).ToString();
        Assert.AreEqual(
          "0.0002173499999999999976289799530349000633577816188335418701171875",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(3.1035E-5).ToString();
        {
          object objectTemp =
  "0.0000310349999999999967797807698399736864303122274577617645263671875";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }
      {
        stringTemp = EDecimal.FromDouble(1.275).ToString();
        Assert.AreEqual(
          "1.274999999999999911182158029987476766109466552734375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(214190.0).ToString();
        Assert.AreEqual(
          "214190",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(3.9813E9).ToString();
        Assert.AreEqual(
          "3981300000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1092700.0).ToString();
        Assert.AreEqual(
          "1092700",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(0.02361).ToString();
        Assert.AreEqual(
          "0.023609999999999999042987752773115062154829502105712890625",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(12.322).ToString();
        Assert.AreEqual(
          "12.321999999999999175770426518283784389495849609375",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(0.002587).ToString();
        Assert.AreEqual(
          "0.002586999999999999889921387108415729016996920108795166015625",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1.322E9).ToString();
        Assert.AreEqual(
          "1322000000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(9.531E10).ToString();
        Assert.AreEqual(
          "95310000000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(142.38).ToString();
        Assert.AreEqual(
          "142.3799999999999954525264911353588104248046875",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2252.5).ToString();
        Assert.AreEqual(
          "2252.5",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(3.636E11).ToString();
        Assert.AreEqual(
          "363600000000",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(3.237E-6).ToString();
        {
          object objectTemp =

  "0.00000323700000000000009386523676380154057596882921643555164337158203125";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }
      {
        stringTemp = EDecimal.FromDouble(728000.0).ToString();
        Assert.AreEqual(
          "728000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2.5818E7).ToString();
        Assert.AreEqual(
          "25818000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1090000.0).ToString();
        Assert.AreEqual(
          "1090000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1.551).ToString();
        Assert.AreEqual(
          "1.5509999999999999342747969421907328069210052490234375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(26.035).ToString();
        Assert.AreEqual(
          "26.035000000000000142108547152020037174224853515625",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(8.33E8).ToString();
        Assert.AreEqual(
          "833000000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(8.123E11).ToString();
        Assert.AreEqual(
          "812300000000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2622.9).ToString();
        Assert.AreEqual(
          "2622.90000000000009094947017729282379150390625",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1.291).ToString();
        Assert.AreEqual(
          "1.290999999999999925393012745189480483531951904296875",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(286140.0).ToString();
        Assert.AreEqual(
          "286140",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.06733).ToString();
        Assert.AreEqual(
          "0.06733000000000000095923269327613525092601776123046875",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(3.2516E-4).ToString();
        Assert.AreEqual(
          "0.000325160000000000010654532811571471029310487210750579833984375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(3.8323E8).ToString();
        Assert.AreEqual(
          "383230000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.028433).ToString();
        Assert.AreEqual(
          "0.02843299999999999994049204588009160943329334259033203125",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(8.37E8).ToString();
        Assert.AreEqual(
          "837000000",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(0.01608).ToString();
        Assert.AreEqual(
          "0.0160800000000000005428990590417015482671558856964111328125",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(3.621E12).ToString();
        Assert.AreEqual(
          "3621000000000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(78.12).ToString();
        Assert.AreEqual(
          "78.1200000000000045474735088646411895751953125",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1.308E9).ToString();
        Assert.AreEqual(
          "1308000000",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(0.031937).ToString();
        Assert.AreEqual(
          "0.031937000000000000110578213252665591426193714141845703125",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1581500.0).ToString();
        Assert.AreEqual(
          "1581500",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(244200.0).ToString();
        Assert.AreEqual(
          "244200",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(2.2818E-7).ToString();
        {
          object objectTemp =

  "2.28179999999999995794237200343046456652018605382181704044342041015625E-7";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }
      {
        stringTemp = EDecimal.FromDouble(39.734).ToString();
        Assert.AreEqual(
          "39.73400000000000176214598468504846096038818359375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1614.0).ToString();
        Assert.AreEqual(
          "1614",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(3.8319E-4).ToString();
        Assert.AreEqual(
          "0.0003831899999999999954607143859419693399104289710521697998046875",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(543.4).ToString();
        Assert.AreEqual(
          "543.3999999999999772626324556767940521240234375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(3.1931E8).ToString();
        Assert.AreEqual(
          "319310000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1429000.0).ToString();
        Assert.AreEqual(
          "1429000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2.6537E12).ToString();
        Assert.AreEqual(
          "2653700000000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(7.22E8).ToString();
        Assert.AreEqual(
          "722000000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(27.2).ToString();
        Assert.AreEqual(
          "27.199999999999999289457264239899814128875732421875",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(3.8025E-6).ToString();
        {
          object objectTemp =

  "0.00000380250000000000001586513038998038638283105683512985706329345703125";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }

      {
        stringTemp = EDecimal.FromDouble(3.6416E-5).ToString();
        Assert.AreEqual(
          "0.0000364159999999999982843446044711299691698513925075531005859375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2006000.0).ToString();
        Assert.AreEqual(
          "2006000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2.6812E9).ToString();
        Assert.AreEqual(
          "2681200000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2.7534E10).ToString();
        Assert.AreEqual(
          "27534000000",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(3.9116E-7).ToString();
        {
          object objectTemp =
  "3.911600000000000165617541382501176627783934236504137516021728515625E-7";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }

      {
        stringTemp = EDecimal.FromDouble(0.0028135).ToString();
        Assert.AreEqual(
          "0.0028135000000000000286437540353290387429296970367431640625",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.9119).ToString();
        Assert.AreEqual(
          "0.91190000000000004387601393318618647754192352294921875",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2241200.0).ToString();
        Assert.AreEqual(
          "2241200",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(32.45).ToString();
        Assert.AreEqual(
          "32.4500000000000028421709430404007434844970703125",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1.38E10).ToString();
        Assert.AreEqual(
          "13800000000",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(0.0473).ToString();
        Assert.AreEqual(
          "0.047300000000000001765254609153998899273574352264404296875",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(205.34).ToString();
        Assert.AreEqual(
          "205.340000000000003410605131648480892181396484375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(3.9819).ToString();
        Assert.AreEqual(
          "3.981899999999999995026200849679298698902130126953125",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1152.8).ToString();
        Assert.AreEqual(
          "1152.799999999999954525264911353588104248046875",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1322000.0).ToString();
        Assert.AreEqual(
          "1322000",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(1.3414E-4).ToString();
        {
          object objectTemp =
  "0.00013414000000000001334814203612921801322954706847667694091796875";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }

      {
        stringTemp = EDecimal.FromDouble(3.445E-7).ToString();
        {
          object objectTemp =

  "3.4449999999999999446924077266263264363033158588223159313201904296875E-7";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }

      {
        stringTemp = EDecimal.FromDouble(1.361E-7).ToString();
        {
          object objectTemp =

  "1.3610000000000000771138253079228785935583800892345607280731201171875E-7";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }
      {
        stringTemp = EDecimal.FromDouble(2.609E7).ToString();
        Assert.AreEqual(
          "26090000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(9.936).ToString();
        Assert.AreEqual(
          "9.93599999999999994315658113919198513031005859375",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(6.0E-6).ToString();
        {
          object objectTemp =
  "0.00000600000000000000015200514458246772164784488268196582794189453125";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }
      {
        stringTemp = EDecimal.FromDouble(260.31).ToString();
        Assert.AreEqual(
          "260.31000000000000227373675443232059478759765625",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(344.6).ToString();
        Assert.AreEqual(
          "344.6000000000000227373675443232059478759765625",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(3.4237).ToString();
        Assert.AreEqual(
          "3.423700000000000187583282240666449069976806640625",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2.3421E9).ToString();
        Assert.AreEqual(
          "2342100000",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(2.331E-4).ToString();
        {
          object objectTemp =
  "0.00023310000000000000099260877295392901942250318825244903564453125";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }
      {
        stringTemp = EDecimal.FromDouble(0.734).ToString();
        Assert.AreEqual(
          "0.7339999999999999857891452847979962825775146484375",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(0.015415).ToString();
        Assert.AreEqual(
          "0.01541499999999999988287147090204598498530685901641845703125",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(0.0035311).ToString();
        Assert.AreEqual(
          "0.0035311000000000001240729741169843691750429570674896240234375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(1.2217E12).ToString();
        Assert.AreEqual(
          "1221700000000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(0.483).ToString();
        Assert.AreEqual(
          "0.48299999999999998490096686509787105023860931396484375",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(2.872E-4).ToString();
        Assert.AreEqual(
          "0.0002871999999999999878506906636488338335766457021236419677734375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(96.11).ToString();
        Assert.AreEqual(
          "96.1099999999999994315658113919198513031005859375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(36570.0).ToString();
        Assert.AreEqual(
          "36570",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(1.83E-5).ToString();
        {
          object objectTemp =
  "0.00001830000000000000097183545932910675446692039258778095245361328125";
          object objectTemp2 = stringTemp;
          Assert.AreEqual(objectTemp, objectTemp2);
        }
      }
      {
        stringTemp = EDecimal.FromDouble(3.0131E8).ToString();
        Assert.AreEqual(
          "301310000",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(382200.0).ToString();
        Assert.AreEqual(
          "382200",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(2.4835E8).ToString();
        Assert.AreEqual(
          "248350000",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(0.001584).ToString();
        Assert.AreEqual(
          "0.0015839999999999999046040866090834242640994489192962646484375",
          stringTemp);
      }

      {
        stringTemp = EDecimal.FromDouble(7.62E-4).ToString();
        Assert.AreEqual(
          "0.000761999999999999982035203682784185730270110070705413818359375",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromDouble(3.133E11).ToString();
        Assert.AreEqual(
          "313300000000",
          stringTemp);
      }
    }
    [Test]
    public void TestFromEFloat() {
      Assert.AreEqual(
        EDecimal.Zero,
        EDecimal.FromEFloat(EFloat.Zero));
      Assert.AreEqual(
        EDecimal.NegativeZero,
        EDecimal.FromEFloat(EFloat.NegativeZero));
      try {
        EDecimal.FromEFloat(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      EFloat bf;
      bf = EFloat.FromInt64(20);
      {
        string stringTemp = EDecimal.FromEFloat(bf).ToString();
        Assert.AreEqual(
          "20",
          stringTemp);
      }
      bf = EFloat.Create((EInteger)3, (EInteger)(-1));
      {
        string stringTemp = EDecimal.FromEFloat(bf).ToString();
        Assert.AreEqual(
          "1.5",
          stringTemp);
      }
      bf = EFloat.Create((EInteger)(-3), (EInteger)(-1));
      {
        string stringTemp = EDecimal.FromEFloat(bf).ToString();
        Assert.AreEqual(
          "-1.5",
          stringTemp);
      }
    }
    [Test]
    public void TestFromInt32() {
      Assert.AreEqual(EDecimal.One, EDecimal.FromInt32(1));
    }
    [Test]
    public void TestFromSingle() {
      string stringTemp;
      {
        stringTemp = EDecimal.FromSingle(0.75f).ToString();
        Assert.AreEqual(
          "0.75",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromSingle(0.5f).ToString();
        Assert.AreEqual(
          "0.5",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromSingle(0.25f).ToString();
        Assert.AreEqual(
          "0.25",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromSingle(0.875f).ToString();
        Assert.AreEqual(
          "0.875",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromSingle(0.125f).ToString();
        Assert.AreEqual(
          "0.125",
          stringTemp);
      }
    }

    [Test]
    public void TestFromString() {
      try {
        EDecimal.FromString(String.Empty);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString(null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      Assert.AreEqual(EDecimal.Zero, EDecimal.FromString("0"));
      Assert.AreEqual(
        EDecimal.Zero,
        EDecimal.FromString("0", null));
      try {
        EDecimal.FromString(null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString(String.Empty);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString(null, 0, 1);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EDecimal.FromString("0..1");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("0.1x+222");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("0.1g-222");
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      try {
        EDecimal.FromString("x", -1, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("x", 2, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("x", 0, -1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("x", 0, 2);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("x", 1, 1);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString(null, 0, 1, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("x", -1, 1, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("x", 2, 1, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("x", 0, -1, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("x", 0, 2, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("x", 1, 1, null);
        Assert.Fail("Should have failed");
      } catch (FormatException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }

      var rand = new RandomGenerator();
      for (var i = 0; i < 3000; ++i) {
        string r = RandomObjects.RandomDecimalString(rand);
        try {
          EDecimal.FromString(r);
        } catch (Exception ex) {
          Assert.Fail(ex.ToString());
          throw new InvalidOperationException(String.Empty, ex);
        }
      }
    }

    [Test]
    public void TestFromStringDecimal() {
      {
        string stringTemp = EDecimal.FromString(
            "-89675213981993819.5183499484258059",
            EContext.CliDecimal).ToString();
        Assert.AreEqual(
          "-89675213981993819.51834994843",
          stringTemp);
      }
    }

    [Test]
    public void TestIsFinite() {
      // not implemented yet
    }
    [Test]
    public void TestIsInfinity() {
      Assert.IsTrue(EDecimal.PositiveInfinity.IsInfinity());
      Assert.IsTrue(EDecimal.NegativeInfinity.IsInfinity());
      Assert.IsFalse(EDecimal.Zero.IsInfinity());
      Assert.IsFalse(EDecimal.NaN.IsInfinity());
    }
    [Test]
    public void TestIsNaN() {
      Assert.IsFalse(EDecimal.PositiveInfinity.IsNaN());
      Assert.IsFalse(EDecimal.NegativeInfinity.IsNaN());
      Assert.IsFalse(EDecimal.Zero.IsNaN());
      Assert.IsTrue(EDecimal.NaN.IsNaN());
    }
    [Test]
    public void TestIsNegative() {
      // not implemented yet
    }
    [Test]
    public void TestIsNegativeInfinity() {
      // not implemented yet
    }
    [Test]
    public void TestIsPositiveInfinity() {
      Assert.IsTrue(EDecimal.PositiveInfinity.IsPositiveInfinity());
      Assert.IsFalse(EDecimal.NegativeInfinity.IsPositiveInfinity());
      Assert.IsFalse(EDecimal.Zero.IsPositiveInfinity());
      Assert.IsFalse(EDecimal.NaN.IsPositiveInfinity());
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
      Assert.IsFalse(EDecimal.NaN.IsZero);
      Assert.IsFalse(EDecimal.SignalingNaN.IsZero);
    }
    [Test]
    public void TestLog() {
      Assert.IsTrue(EDecimal.One.Log(null).IsNaN());
      Assert.IsTrue(EDecimal.One.Log(EContext.Unlimited).IsNaN());
      EContext ep = EContext.ForPrecision(15);
      var resources = new AppResources("Resources");
      string json = resources.GetString("logprec15");
      json = DecTestUtil.ParseJSONString(json);
      string[] items = DecTestUtil.SplitAt(json, "\u002c");
      for (var i = 0; i < items.Length; i += 2) {
        TestCommon.CompareTestEqual(
          EDecimal.FromString(items[i]),
          EDecimal.FromString(items[i + 1]).Log(ep));
      }
    }
    [Test]
    public void TestLog10() {
      Assert.IsTrue(EDecimal.One.Log10(null).IsNaN());
      Assert.IsTrue(EDecimal.One.Log10(EContext.Unlimited)
        .IsNaN());
    }
    [Test]
    public void TestMantissa() {
      // not implemented yet
    }
    [Test]
    public void TestMax() {
      try {
        EDecimal.Max(null, EDecimal.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.Max(EDecimal.One, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var r = new RandomGenerator();
      for (var i = 0; i < 500; ++i) {
        EDecimal bigintA = RandomObjects.RandomEDecimal(r);
        EDecimal bigintB = RandomObjects.RandomEDecimal(r);
        if (!bigintA.IsFinite || !bigintB.IsFinite) {
          continue;
        }
        int cmp = TestCommon.CompareTestReciprocal(bigintA, bigintB);
        if (cmp < 0) {
          TestCommon.CompareTestEqual(
            bigintB,
            EDecimal.Max(bigintA, bigintB));
        } else if (cmp > 0) {
          TestCommon.CompareTestEqual(
            bigintA,
            EDecimal.Max(bigintA, bigintB));
        } else {
          TestCommon.CompareTestEqual(
            bigintA,
            EDecimal.Max(bigintA, bigintB));
          TestCommon.CompareTestEqual(
            bigintB,
            EDecimal.Max(bigintA, bigintB));
        }
      }
    }
    [Test]
    public void TestMaxMagnitude() {
      try {
        EDecimal.MaxMagnitude(null, EDecimal.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.MaxMagnitude(EDecimal.One, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestMin() {
      try {
        EDecimal.Min(null, EDecimal.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.Min(EDecimal.One, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      var r = new RandomGenerator();
      for (var i = 0; i < 500; ++i) {
        EDecimal bigintA = RandomObjects.RandomEDecimal(r);
        EDecimal bigintB = RandomObjects.RandomEDecimal(r);
        if (!bigintA.IsFinite || !bigintB.IsFinite) {
          continue;
        }
        int cmp = TestCommon.CompareTestReciprocal(bigintA, bigintB);
        if (cmp < 0) {
          TestCommon.CompareTestEqual(
            bigintA,
            EDecimal.Min(bigintA, bigintB));
        } else if (cmp > 0) {
          TestCommon.CompareTestEqual(
            bigintB,
            EDecimal.Min(bigintA, bigintB));
        } else {
          TestCommon.CompareTestEqual(
            bigintA,
            EDecimal.Min(bigintA, bigintB));
          TestCommon.CompareTestEqual(
            bigintB,
            EDecimal.Min(bigintA, bigintB));
        }
      }
    }
    [Test]
    public void TestMinMagnitude() {
      try {
        EDecimal.MinMagnitude(null, EDecimal.One);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.MinMagnitude(EDecimal.One, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestMovePointLeft() {
      {
        string stringTemp = EDecimal.FromString(
            "1").MovePointLeft(EInteger.Zero, null).ToString();
        Assert.AreEqual(
          "1",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "0.1").MovePointLeft(EInteger.Zero, null).ToString();
        Assert.AreEqual(
          "0.1",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "0.01").MovePointLeft(EInteger.Zero, null).ToString();
        Assert.AreEqual(
          "0.01",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "1").MovePointLeft(0, null).ToString();
        Assert.AreEqual(
          "1",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "0.1").MovePointLeft(0, null).ToString();
        Assert.AreEqual(
          "0.1",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "0.01").MovePointLeft(0, null).ToString();
        Assert.AreEqual(
          "0.01",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "1").MovePointLeft(0).ToString();
        Assert.AreEqual(
          "1",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "0.1").MovePointLeft(0).ToString();
        Assert.AreEqual(
          "0.1",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "0.01").MovePointLeft(0).ToString();
        Assert.AreEqual(
          "0.01",
          stringTemp);
      }
    }
    [Test]
    public void TestMultiply() {
      // not implemented yet
    }
    [Test]
    public void TestMultiplyAndAdd() {
      try {
        EDecimal.One.MultiplyAndAdd(null, EDecimal.Zero, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.One.MultiplyAndAdd(EDecimal.Zero, null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.One.MultiplyAndAdd(null, null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.One.MultiplyAndAdd(null, EDecimal.Zero);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.One.MultiplyAndAdd(EDecimal.Zero, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.One.MultiplyAndAdd(null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestMultiplyAndSubtract() {
      try {
        EDecimal.One.MultiplyAndSubtract(
          null,
          EDecimal.Zero,
          null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.One.MultiplyAndSubtract(
          EDecimal.Zero,
          null,
          null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.One.MultiplyAndSubtract(null, null, null);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestCopySign() {
      var r = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        EDecimal ed = RandomObjects.RandomEDecimal(r);
        ed = ed.CopySign(EDecimal.Zero);
        Assert.IsFalse(ed.IsNegative);
        ed = ed.CopySign(EDecimal.NegativeZero);
        Assert.IsTrue(ed.IsNegative);
      }
      Assert.IsFalse(EDecimal.SignalingNaN.CopySign(
          EDecimal.Zero).IsNegative);
      Assert.IsTrue(
        EDecimal.SignalingNaN.CopySign(EDecimal.NegativeZero).IsNegative);
    }

    [Test]
    public void TestNegate() {
      var r = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        EDecimal ed = RandomObjects.RandomEDecimal(r);
        ed = ed.CopySign(EDecimal.Zero);
        Assert.IsTrue(ed.Negate().IsNegative);
        ed = ed.CopySign(EDecimal.NegativeZero);
        Assert.IsTrue(ed.IsNegative);
      }
      Assert.IsTrue(EDecimal.SignalingNaN.Negate().IsNegative);
    }
    [Test]
    public void TestNextMinus() {
      // not implemented yet
    }
    [Test]
    public void TestNextPlus() {
      // not implemented yet
    }
    [Test]
    public void TestNextToward() {
      // not implemented yet
    }

    [Test]
    public void TestPI() {
      EDecimal pi = EDecimal.PI(EContext.ForPrecision(3));
      {
        string stringTemp = pi.ToPlainString();
        Assert.AreEqual(
          "3.14",
          stringTemp);
      }
      pi = EDecimal.PI(EContext.ForPrecision(4));
      {
        string stringTemp = pi.ToPlainString();
        Assert.AreEqual(
          "3.142",
          stringTemp);
      }
      pi = EDecimal.PI(EContext.ForPrecision(5));
      {
        string stringTemp = pi.ToPlainString();
        Assert.AreEqual(
          "3.1416",
          stringTemp);
      }
      pi = EDecimal.PI(EContext.ForPrecision(6));
      {
        string stringTemp = pi.ToPlainString();
        Assert.AreEqual(
          "3.14159",
          stringTemp);
      }
      pi = EDecimal.PI(EContext.ForPrecision(7));
      {
        string stringTemp = pi.ToPlainString();
        Assert.AreEqual(
          "3.141593",
          stringTemp);
      }
      pi = EDecimal.PI(EContext.ForPrecision(8));
      {
        string stringTemp = pi.ToPlainString();
        Assert.AreEqual(
          "3.1415927",
          stringTemp);
      }
      pi = EDecimal.PI(EContext.ForPrecision(9));
      {
        string stringTemp = pi.ToPlainString();
        Assert.AreEqual(
          "3.14159265",
          stringTemp);
      }
      pi = EDecimal.PI(EContext.ForPrecision(10));
      {
        string stringTemp = pi.ToPlainString();
        Assert.AreEqual(
          "3.141592654",
          stringTemp);
      }
      pi = EDecimal.PI(EContext.ForPrecision(25));
      {
        string stringTemp = pi.ToPlainString();
        Assert.AreEqual(
          "3.141592653589793238462643",
          stringTemp);
      }
    }
    [Test]
    public void TestPlus() {
      Assert.AreEqual(
        EDecimal.Zero,
        EDecimal.NegativeZero.Plus(EContext.Basic));
      Assert.AreEqual(
        EDecimal.Zero,
        EDecimal.NegativeZero.Plus(null));
    }
    [Test]
    public void TestPow() {
      EDecimal a = EDecimal.FromInt32(33000);
      EDecimal b = EDecimal.FromInt32(6);
      EDecimal result = EDecimal.FromString("1291467969000000000000000000");
      EDecimal powa = a.Pow(b);
      TestCommon.CompareTestEqual((EDecimal)result, (EDecimal)powa);
      var r = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        a = EDecimal.FromInt32(r.UniformInt(1000000) + 1);
        b = EDecimal.FromInt32(r.UniformInt(12) + 2);
        EInteger ei = a.ToEInteger().Pow(b.ToEInteger());
        result = EDecimal.FromEInteger(ei);
        powa = a.Pow(b);
        TestCommon.CompareTestEqual((EDecimal)result, (EDecimal)powa);
      }
    }
    [Test]
    public void TestQuantize() {
      // not implemented yet
    }
    [Test]
    public void TestReduce() {
      // not implemented yet
    }
    [Test]
    [Timeout(60000)]
    public void TestRemainder() {
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("59840771232212955222033039906"),
            EInteger.FromInt32(0));
        EDecimal divisor = EDecimal.Create(56, -3);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.24e-1")),
          remainder.ToString());
      }
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("71335024459231687631753628354"),
            EInteger.FromInt32(-1));
        EDecimal divisor = EDecimal.Create(99, -4);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.42e-2")),
          remainder.ToString());
      }
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("68541311159644774501062173149"),
            EInteger.FromInt32(0));
        EDecimal divisor = EDecimal.Create(68, -4);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.4e-3")),
          remainder.ToString());
      }
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("72230705069845418380625072039"),
            EInteger.FromInt32(0));
        EDecimal divisor = EDecimal.Create(67, -4);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.36e-2")),
          remainder.ToString());
      }
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("66072931440684617011747269944"),
            EInteger.FromInt32(-1));
        EDecimal divisor = EDecimal.Create(29, -4);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.2e-2")),
          remainder.ToString());
      }
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("25599757933935193456538556810"),
            EInteger.FromInt32(0));
        EDecimal divisor = EDecimal.Create(74, -3);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.3e-1")),
          remainder.ToString());
      }
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("56970681730399566214724883073"),
            EInteger.FromInt32(0));
        EDecimal divisor = EDecimal.Create(38, -4);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.12e-2")),
          remainder.ToString());
      }
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("76187141448183306266415732331"),
            EInteger.FromInt32(-2));
        EDecimal divisor = EDecimal.Create(71, -4);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.6e-2")),
          remainder.ToString());
      }
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("56333974884046765961647166477"),
            EInteger.FromInt32(-2));
        EDecimal divisor = EDecimal.Create(63, -4);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.35e-2")),
          remainder.ToString());
      }
      {
        EDecimal dividend = EDecimal.Create(
            EInteger.FromString("53394382853562286627297867112"),
            EInteger.FromInt32(0));
        EDecimal divisor = EDecimal.Create(99, -3);
        EDecimal remainder = dividend.RemainderNoRoundAfterDivide(
            divisor,
            EContext.CliDecimal);
        Assert.AreEqual(
          0,
          remainder.CompareTo(EDecimal.FromString("0.32e-1")),
          remainder.ToString());
      }
    }
    [Test]
    public void TestRemainderNaturalScale() {
      // not implemented yet
    }
    [Test]
    public void TestRemainderNear() {
      // not implemented yet
    }
    [Test]
    [Timeout(60000)]
    public void TestRoundToExponent() {
      this.TestRoundToExponentOne("-0", "-0", 0, ERounding.Down);
      this.TestRoundToExponentOne("-0", "-0", 0, ERounding.HalfEven);
      this.TestRoundToExponentOne("-0", "-0", 0, ERounding.Floor);
      this.TestRoundToExponentOne("-0.0", "-0", 0, ERounding.Down);
      this.TestRoundToExponentOne("-0.0", "-0", 0, ERounding.HalfEven);
      this.TestRoundToExponentOne("-0.0", "-0", 0, ERounding.Floor);
      this.TestRoundToExponentOne("-0.0000", "-0", 0, ERounding.Down);
      this.TestRoundToExponentOne("-0.0000", "-0", 0, ERounding.HalfEven);
      this.TestRoundToExponentOne("-0.0000", "-0", 0, ERounding.Floor);
    }
    [Test]
    public void TestRoundToExponentExact() {
      // not implemented yet
    }
    [Test]
    public void TestRoundToIntegralExact() {
      // not implemented yet
    }
    [Test]
    public void TestRoundToIntegralNoRoundedFlag() {
      // not implemented yet
    }
    [Test]
    public void TestRoundToPrecision() {
      // not implemented yet
    }
    [Test]
    public void TestScaling() {
      {
        string stringTemp = EDecimal.FromString(
            "5.000").ScaleByPowerOfTen(5).ToString();
        Assert.AreEqual(
          "5.000E+5",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "5.000").ScaleByPowerOfTen(-5).ToString();
        Assert.AreEqual(
          "0.00005000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "500000").MovePointRight(5).ToString();
        Assert.AreEqual(
          "50000000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "500000").MovePointRight(-5).ToString();
        Assert.AreEqual(
          "5.00000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "500000").MovePointLeft(-5).ToString();
        Assert.AreEqual(
          "50000000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "500000").MovePointLeft(5).ToString();
        Assert.AreEqual(
          "5.00000",
          stringTemp);
      }
    }
    [Test]
    public void TestSign() {
      // not implemented yet
    }
    [Test]
    public void TestSignalingNaN() {
      {
        string stringTemp = EDecimal.SignalingNaN.ToString();
        Assert.AreEqual(
          "sNaN",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.SignalingNaN.ToEngineeringString();
        Assert.AreEqual(
          "sNaN",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.SignalingNaN.ToPlainString();
        Assert.AreEqual(
          "sNaN",
          stringTemp);
      }
    }

    /*
    private static void CA(byte[] bytes) {
      var sb = new System.Text.StringBuilder();
      for (var i = 0; i < bytes.Length; ++i) {
        sb.AppendFormat("{0:X2}", bytes[i]);
      }
      Console.WriteLine(sb.ToString());
    }
    */

    private static void AssertQuietNaN(string str) {
      EDecimal ed = EDecimal.FromString(str);
      Assert.IsTrue(ed.IsQuietNaN());
      Assert.IsTrue(EDecimal.FromDouble(ed.ToDouble()).IsQuietNaN());
      Assert.IsTrue(EDecimal.FromSingle(ed.ToSingle()).IsQuietNaN());
      EFloat ef = EFloat.FromString(str);
      Assert.IsTrue(ef.IsQuietNaN());
      Assert.IsTrue(EFloat.FromDouble(ef.ToDouble()).IsQuietNaN());
      Assert.IsTrue(EFloat.FromSingle(ef.ToSingle()).IsQuietNaN());
      ERational er = ERational.FromString(str);
      Assert.IsTrue(er.IsQuietNaN());
      Assert.IsTrue(ERational.FromDouble(er.ToDouble()).IsQuietNaN());
      Assert.IsTrue(ERational.FromSingle(er.ToSingle()).IsQuietNaN());
    }

    private static void AssertSignalingNaN(string str) {
      EDecimal ed = EDecimal.FromString(str);
      Assert.IsTrue(ed.IsSignalingNaN());
      EFloat ef = EFloat.FromString(str);
      Assert.IsTrue(ef.IsSignalingNaN());
      ERational er = ERational.FromString(str);
      Assert.IsTrue(er.IsSignalingNaN());
      // NOTE: Unfortunately, .NET might quiet signaling
      // NaNs it may otherwise generate
      Assert.IsTrue(
        EDecimal.FromDouble(ed.ToDouble()).IsNaN(),
        str);
      Assert.IsTrue(EDecimal.FromSingle(ed.ToSingle()).IsNaN());
      Assert.IsTrue(EFloat.FromDouble(ef.ToDouble()).IsNaN());
      Assert.IsTrue(EFloat.FromSingle(ef.ToSingle()).IsNaN());
      Assert.IsTrue(ERational.FromDouble(er.ToDouble()).IsNaN());
      Assert.IsTrue(ERational.FromSingle(er.ToSingle()).IsNaN());
    }

    [Test]
    public void TestQuietSignalingNaN() {
      for (var i = 0; i <= 50; ++i) {
        AssertQuietNaN("NaN" + TestCommon.IntToString(i));
        AssertSignalingNaN("sNaN" + TestCommon.IntToString(i));
      }
    }
    [Test]
    public void TestSquareRoot() {
      // not implemented yet
    }
    [Test]
    public void TestSubtract() {
      try {
        EDecimal.Zero.Subtract(null, EContext.Unlimited);
        Assert.Fail("Should have failed");
      } catch (ArgumentNullException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestToEInteger() {
      try {
        EDecimal.PositiveInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.NegativeInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.PositiveInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.NegativeInfinity.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.NaN.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.SignalingNaN.ToEInteger();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      EDecimal dec = EDecimal.Create(999, -1);
      try {
        dec.ToEInteger();
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }
    [Test]
    public void TestToEIntegerIfExact() {
      EDecimal dec = EDecimal.Create(999, -1);
      try {
        dec.ToEIntegerIfExact();
        Assert.Fail("Should have failed");
      } catch (ArithmeticException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    private static readonly EDecimal DoubleUnderflowToZero =
      EFloat.Create(1, -1075).ToEDecimal();

    private static readonly EDecimal DoubleOverflowToInfinity =
      EFloat.Create(
        EInteger.FromInt64((1L << 53) - 1),
        EInteger.FromInt32(971)).Add(
        EFloat.Create(1, 970)).ToEDecimal();

    private static readonly EDecimal SingleUnderflowToZero =
      EFloat.Create(1, -150).ToEDecimal();

    private static readonly EDecimal SingleOverflowToInfinity =
      EFloat.Create(
        EInteger.FromInt64((1L << 24) - 1),
        EInteger.FromInt32(104)).Add(
        EFloat.Create(1, 103)).ToEDecimal();

    private static EDecimal[] MakeUlpTable() {
      var edecarr = new EDecimal[2048];
      for (int i = 0; i < 2048; ++i) {
        edecarr[i] = EFloat.Create(1, i - 1075).ToEDecimal();
      }
      return edecarr;
    }

    private static readonly EDecimal[] UlpTable = MakeUlpTable();

    private static EDecimal GetHalfUlp(double dbl) {
      long value = BitConverter.ToInt64(
          BitConverter.GetBytes((double)dbl),
          0);
      var exponent = (int)((value >> 52) & 0x7ffL);
      if (exponent == 0) {
        return UlpTable[exponent];
      } else if (exponent == 2047) {
        throw new ArgumentException("dbl is non-finite");
      } else {
        return UlpTable[exponent - 1];
      }
    }

    private static EDecimal GetHalfUlp(float sng) {
      int value = BitConverter.ToInt32(
          BitConverter.GetBytes((float)sng),
          0);
      var exponent = (int)((value >> 23) & 0xff);
      if (exponent == 0) {
        return UlpTable[exponent + 925];
      } else if (exponent == 255) {
        throw new ArgumentException("sng is non-finite");
      } else {
        return UlpTable[exponent + 924];
      }
    }

    [Test]
    public void TestToByteChecked() {
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.1").ToByteChecked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.4").ToByteChecked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.5").ToByteChecked());
      Assert.AreEqual((byte)0, EDecimal.FromString("-0.6").ToByteChecked());
      try {
        EDecimal.FromString("-1.0").ToByteChecked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.4").ToByteChecked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.5").ToByteChecked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString("-1.6").ToByteChecked();
        Assert.Fail("Should have failed");
      } catch (OverflowException) {
        // NOTE: Intentionally empty
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestToDouble() {
      // test for correct rounding
      double dbl;
      dbl = EDecimal.FromString(
  "1.972579273363468721491642554610734805464744567871093749999999999999")
        .ToDouble();
      {
        string stringTemp = EFloat.FromDouble(dbl).ToPlainString();
        Assert.AreEqual(
          "1.9725792733634686104693400920950807631015777587890625",
          stringTemp);
      }
      var fr = new RandomGenerator();
      dbl = DoubleOverflowToInfinity.ToDouble();
      Assert.IsTrue(Double.IsPositiveInfinity(dbl));
      dbl = DoubleOverflowToInfinity.Negate().ToDouble();
      Assert.IsTrue(Double.IsNegativeInfinity(dbl));
      dbl = DoubleUnderflowToZero.ToDouble();
      Assert.IsTrue(dbl == 0.0);
      dbl = DoubleUnderflowToZero.Negate().ToDouble();
      Assert.IsTrue(dbl == 0.0);
      for (var i = 0; i < 100000; ++i) {
        EDecimal edec;
        string edecstr;
        if (fr.UniformInt(100) < 10) {
          string decimals = RandomObjects.RandomBigIntString(fr);
          if (decimals[0] == '-') {
            decimals = decimals.Substring(1);
          }
          edecstr = RandomObjects.RandomBigIntString(fr) +
            "." + decimals + "e" + RandomObjects.RandomBigIntString(fr);
          edec = EDecimal.FromString(edecstr);
        } else {
          edec = RandomObjects.RandomEDecimal(fr);
          edecstr = edec.ToString();
        }
        if (edec.IsFinite) {
          dbl = edec.ToDouble();
          if (Double.IsNegativeInfinity(dbl)) {
            Assert.IsTrue(edec.IsNegative);
            TestCommon.CompareTestGreaterEqual(
              edec.Abs(),
              DoubleOverflowToInfinity,
              edecstr);
          } else if (Double.IsPositiveInfinity(dbl)) {
            Assert.IsTrue(!edec.IsNegative);
            TestCommon.CompareTestGreaterEqual(
              edec.Abs(),
              DoubleOverflowToInfinity,
              edecstr);
          } else if (dbl == 0.0) {
            TestCommon.CompareTestLessEqual(
              edec.Abs(),
              DoubleUnderflowToZero,
              edecstr);
            Assert.AreEqual(
              edec.IsNegative,
              EDecimal.FromDouble(dbl).IsNegative,
              edecstr);
          } else {
            Assert.IsTrue(!Double.IsNaN(dbl));
            edec = edec.Abs();
            TestCommon.CompareTestGreater(
              edec,
              DoubleUnderflowToZero,
              edecstr);
            TestCommon.CompareTestLess(
              edec,
              DoubleOverflowToInfinity,
              edecstr);
            EDecimal halfUlp = GetHalfUlp(dbl);
            EDecimal difference = EDecimal.FromDouble(dbl).Abs()
              .Subtract(edec).Abs();
            TestCommon.CompareTestLessEqual(
              difference,
              halfUlp,
              edecstr);
          }
        }
      }
    }
    [Test]
    public void TestToEngineeringString() {
      string stringTemp;
      {
        stringTemp = EDecimal.FromString(
            "89.12E-1").ToEngineeringString();
        Assert.AreEqual(
          "8.912",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "242.31E-4").ToEngineeringString();
        Assert.AreEqual(
          "0.024231",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "229.18E5").ToEngineeringString();
        Assert.AreEqual(
          "22.918E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "326.18E-7").ToEngineeringString();
        Assert.AreEqual(
          "0.000032618",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "55.0E6").ToEngineeringString();
        Assert.AreEqual(
          "55.0E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "224.36E3").ToEngineeringString();
        Assert.AreEqual(
          "224.36E+3",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "230.12E9").ToEngineeringString();
        Assert.AreEqual(
          "230.12E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "113.20E-7").ToEngineeringString();
        Assert.AreEqual(
          "0.000011320",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "317.7E-9").ToEngineeringString();
        Assert.AreEqual(
          "317.7E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "339.3E-2").ToEngineeringString();
        Assert.AreEqual(
          "3.393",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "271.35E8").ToEngineeringString();
        Assert.AreEqual(
          "27.135E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "377.19E-9").ToEngineeringString();
        Assert.AreEqual(
          "377.19E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "321.27E7").ToEngineeringString();
        Assert.AreEqual(
          "3.2127E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "294.22E-2").ToEngineeringString();
        Assert.AreEqual(
          "2.9422",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "110.31E-8").ToEngineeringString();
        Assert.AreEqual(
          "0.0000011031",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "243.24E-2").ToEngineeringString();
        Assert.AreEqual(
          "2.4324",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "64.12E-5").ToEngineeringString();
        Assert.AreEqual(
          "0.0006412",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "142.23E1").ToEngineeringString();
        Assert.AreEqual(
          "1422.3",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "293.0E0").ToEngineeringString();
        Assert.AreEqual(
          "293.0",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "253.20E-8").ToEngineeringString();
        Assert.AreEqual(
          "0.0000025320",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "366.6E8").ToEngineeringString();
        Assert.AreEqual(
          "36.66E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "345.26E10").ToEngineeringString();
        Assert.AreEqual(
          "3.4526E+12",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "270.4E-2").ToEngineeringString();
        Assert.AreEqual(
          "2.704",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "4.32E8").ToEngineeringString();
        Assert.AreEqual(
          "432E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "224.22E0").ToEngineeringString();
        Assert.AreEqual(
          "224.22",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "315.30E-7").ToEngineeringString();
        Assert.AreEqual(
          "0.000031530",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "115.32E5").ToEngineeringString();
        Assert.AreEqual(
          "11.532E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "394.20E2").ToEngineeringString();
        Assert.AreEqual(
          "39420",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "67.24E-9").ToEngineeringString();
        Assert.AreEqual(
          "67.24E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "349.33E2").ToEngineeringString();
        Assert.AreEqual(
          "34933",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "67.8E-9").ToEngineeringString();
        Assert.AreEqual(
          "67.8E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "192.31E5").ToEngineeringString();
        Assert.AreEqual(
          "19.231E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "173.17E7").ToEngineeringString();
        Assert.AreEqual(
          "1.7317E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "43.9E0").ToEngineeringString();
        Assert.AreEqual(
          "43.9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "168.12E-8").ToEngineeringString();
        Assert.AreEqual(
          "0.0000016812",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "371.5E10").ToEngineeringString();
        Assert.AreEqual(
          "3.715E+12",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "42.4E-8").ToEngineeringString();
        Assert.AreEqual(
          "424E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "161.23E10").ToEngineeringString();
        Assert.AreEqual(
          "1.6123E+12",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "302.8E6").ToEngineeringString();
        Assert.AreEqual(
          "302.8E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "175.13E0").ToEngineeringString();
        Assert.AreEqual(
          "175.13",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "298.20E-9").ToEngineeringString();
        Assert.AreEqual(
          "298.20E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "362.23E8").ToEngineeringString();
        Assert.AreEqual(
          "36.223E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "277.39E2").ToEngineeringString();
        Assert.AreEqual(
          "27739",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "117.34E-4").ToEngineeringString();
        Assert.AreEqual(
          "0.011734",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "190.13E-9").ToEngineeringString();
        Assert.AreEqual(
          "190.13E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "350.19E-2").ToEngineeringString();
        Assert.AreEqual(
          "3.5019",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "383.27E-9").ToEngineeringString();
        Assert.AreEqual(
          "383.27E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "242.17E5").ToEngineeringString();
        Assert.AreEqual(
          "24.217E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "299.23E7").ToEngineeringString();
        Assert.AreEqual(
          "2.9923E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "302.22E-2").ToEngineeringString();
        Assert.AreEqual(
          "3.0222",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "45.21E-3").ToEngineeringString();
        Assert.AreEqual(
          "0.04521",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "150.0E-1").ToEngineeringString();
        Assert.AreEqual(
          "15.00",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "29.0E4").ToEngineeringString();
        Assert.AreEqual(
          "290E+3",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "263.37E3").ToEngineeringString();
        Assert.AreEqual(
          "263.37E+3",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "283.21E-1").ToEngineeringString();
        Assert.AreEqual(
          "28.321",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "21.32E0").ToEngineeringString();
        Assert.AreEqual(
          "21.32",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "69.20E-6").ToEngineeringString();
        Assert.AreEqual(
          "0.00006920",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "72.8E-3").ToEngineeringString();
        Assert.AreEqual(
          "0.0728",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "164.6E7").ToEngineeringString();
        Assert.AreEqual(
          "1.646E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "118.17E-2").ToEngineeringString();
        Assert.AreEqual(
          "1.1817",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "262.35E-7").ToEngineeringString();
        Assert.AreEqual(
          "0.000026235",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "233.7E5").ToEngineeringString();
        Assert.AreEqual(
          "23.37E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "391.24E0").ToEngineeringString();
        Assert.AreEqual(
          "391.24",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "221.36E1").ToEngineeringString();
        Assert.AreEqual(
          "2213.6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "353.32E0").ToEngineeringString();
        Assert.AreEqual(
          "353.32",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "129.31E-4").ToEngineeringString();
        Assert.AreEqual(
          "0.012931",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "176.26E-5").ToEngineeringString();
        Assert.AreEqual(
          "0.0017626",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "207.5E3").ToEngineeringString();
        Assert.AreEqual(
          "207.5E+3",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "314.10E0").ToEngineeringString();
        Assert.AreEqual(
          "314.10",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "379.20E9").ToEngineeringString();
        Assert.AreEqual(
          "379.20E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "379.12E-6").ToEngineeringString();
        Assert.AreEqual(
          "0.00037912",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "74.38E-8").ToEngineeringString();
        Assert.AreEqual(
          "743.8E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "234.17E-9").ToEngineeringString();
        Assert.AreEqual(
          "234.17E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "13.26E7").ToEngineeringString();
        Assert.AreEqual(
          "132.6E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "251.5E5").ToEngineeringString();
        Assert.AreEqual(
          "25.15E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "87.32E0").ToEngineeringString();
        Assert.AreEqual(
          "87.32",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "331.16E7").ToEngineeringString();
        Assert.AreEqual(
          "3.3116E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "61.4E8").ToEngineeringString();
        Assert.AreEqual(
          "6.14E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "209.7E-6").ToEngineeringString();
        Assert.AreEqual(
          "0.0002097",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "5.4E6").ToEngineeringString();
        Assert.AreEqual(
          "5.4E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "219.9E0").ToEngineeringString();
        Assert.AreEqual(
          "219.9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "26.31E-6").ToEngineeringString();
        Assert.AreEqual(
          "0.00002631",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "48.28E7").ToEngineeringString();
        Assert.AreEqual(
          "482.8E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "267.8E0").ToEngineeringString();
        Assert.AreEqual(
          "267.8",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "320.9E-3").ToEngineeringString();
        Assert.AreEqual(
          "0.3209",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "300.15E-3").ToEngineeringString();
        Assert.AreEqual(
          "0.30015",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "260.11E4").ToEngineeringString();
        Assert.AreEqual(
          "2.6011E+6",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "114.29E-2").ToEngineeringString();
        Assert.AreEqual(
          "1.1429",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "306.0E-6").ToEngineeringString();
        Assert.AreEqual(
          "0.0003060",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "97.7E3").ToEngineeringString();
        Assert.AreEqual(
          "97.7E+3",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "122.29E8").ToEngineeringString();
        Assert.AreEqual(
          "12.229E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "69.4E2").ToEngineeringString();
        Assert.AreEqual(
          "6.94E+3",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "383.5E0").ToEngineeringString();
        Assert.AreEqual(
          "383.5",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "315.30E3").ToEngineeringString();
        Assert.AreEqual(
          "315.30E+3",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "130.38E9").ToEngineeringString();
        Assert.AreEqual(
          "130.38E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "206.16E9").ToEngineeringString();
        Assert.AreEqual(
          "206.16E+9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "304.28E-9").ToEngineeringString();
        Assert.AreEqual(
          "304.28E-9",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "66.13E4").ToEngineeringString();
        Assert.AreEqual(
          "661.3E+3",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "185.33E-2").ToEngineeringString();
        Assert.AreEqual(
          "1.8533",
          stringTemp);
      }
      {
        stringTemp = EDecimal.FromString(
            "70.7E6").ToEngineeringString();
        Assert.AreEqual(
          "70.7E+6",
          stringTemp);
      }
    }
    [Test]
    public void TestToEFloat() {
      Assert.AreEqual(
        EFloat.Zero,
        EDecimal.Zero.ToEFloat());
      Assert.AreEqual(
        EFloat.NegativeZero,
        EDecimal.NegativeZero.ToEFloat());
      if (EFloat.Zero.ToSingle() != 0.0f) {
        Assert.Fail("Failed " + EFloat.Zero.ToDouble());
      }
      if (EFloat.Zero.ToDouble() != 0.0f) {
        Assert.Fail("Failed " + EFloat.Zero.ToDouble());
      }
      EDecimal df;
      df = EDecimal.FromInt64(20);
      {
        string stringTemp = df.ToEFloat().ToString();
        Assert.AreEqual(
          "20",
          stringTemp);
      }
      df = EDecimal.FromInt64(-20);
      {
        string stringTemp = df.ToEFloat().ToString();
        Assert.AreEqual(
          "-20",
          stringTemp);
      }
      df = EDecimal.Create((EInteger)15, (EInteger)(-1));
      {
        string stringTemp = df.ToEFloat().ToString();
        Assert.AreEqual(
          "1.5",
          stringTemp);
      }
      df = EDecimal.Create((EInteger)(-15), (EInteger)(-1));
      {
        string stringTemp = df.ToEFloat().ToString();
        Assert.AreEqual(
          "-1.5",
          stringTemp);
      }
    }
    [Test]
    public void TestToPlainString() {
      {
        string stringTemp = EDecimal.NegativeZero.ToPlainString();
        Assert.AreEqual(
          "-0",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "277.22E9").ToPlainString();
        Assert.AreEqual(
          "277220000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "391.19E4").ToPlainString();
        Assert.AreEqual(
          "3911900",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "383.27E-9").ToPlainString();
        Assert.AreEqual(
          "0.00000038327",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "47.33E9").ToPlainString();
        Assert.AreEqual(
          "47330000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "322.21E3").ToPlainString();
        Assert.AreEqual(
          "322210",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "191.3E-2").ToPlainString();
        Assert.AreEqual(
          "1.913",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "119.17E2").ToPlainString();
        Assert.AreEqual(
          "11917",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "159.6E-6").ToPlainString();
        Assert.AreEqual(
          "0.0001596",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "70.16E9").ToPlainString();
        Assert.AreEqual(
          "70160000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "166.24E9").ToPlainString();
        Assert.AreEqual(
          "166240000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "235.25E3").ToPlainString();
        Assert.AreEqual(
          "235250",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "37.22E7").ToPlainString();
        Assert.AreEqual(
          "372200000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "320.26E8").ToPlainString();
        Assert.AreEqual(
          "32026000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "127.11E-9").ToPlainString();
        Assert.AreEqual(
          "0.00000012711",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "97.29E-7").ToPlainString();
        Assert.AreEqual(
          "0.000009729",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "175.13E9").ToPlainString();
        Assert.AreEqual(
          "175130000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "38.21E-7").ToPlainString();
        Assert.AreEqual(
          "0.000003821",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "6.28E1").ToPlainString();
        Assert.AreEqual(
          "62.8",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "138.29E6").ToPlainString();
        Assert.AreEqual(
          "138290000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "160.19E1").ToPlainString();
        Assert.AreEqual(
          "1601.9",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "358.12E2").ToPlainString();
        Assert.AreEqual(
          "35812",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "249.28E10").ToPlainString();
        Assert.AreEqual(
          "2492800000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "311.23E-6").ToPlainString();
        Assert.AreEqual(
          "0.00031123",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "164.33E-3").ToPlainString();
        Assert.AreEqual(
          "0.16433",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "299.20E-1").ToPlainString();
        Assert.AreEqual(
          "29.920",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "105.39E3").ToPlainString();
        Assert.AreEqual(
          "105390",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "382.5E4").ToPlainString();
        Assert.AreEqual(
          "3825000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "90.9E1").ToPlainString();
        Assert.AreEqual(
          "909",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "329.15E8").ToPlainString();
        Assert.AreEqual(
          "32915000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "245.23E8").ToPlainString();
        Assert.AreEqual(
          "24523000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "97.19E-8").ToPlainString();
        Assert.AreEqual(
          "0.0000009719",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "55.12E7").ToPlainString();
        Assert.AreEqual(
          "551200000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "12.38E2").ToPlainString();
        Assert.AreEqual(
          "1238",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "250.20E-5").ToPlainString();
        Assert.AreEqual(
          "0.0025020",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "53.20E2").ToPlainString();
        Assert.AreEqual(
          "5320",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "141.5E8").ToPlainString();
        Assert.AreEqual(
          "14150000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "338.34E-5").ToPlainString();
        Assert.AreEqual(
          "0.0033834",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "160.39E9").ToPlainString();
        Assert.AreEqual(
          "160390000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "152.17E6").ToPlainString();
        Assert.AreEqual(
          "152170000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "13.3E9").ToPlainString();
        Assert.AreEqual(
          "13300000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "1.38E1").ToPlainString();
        Assert.AreEqual(
          "13.8",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "348.21E-9").ToPlainString();
        Assert.AreEqual(
          "0.00000034821",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "52.5E7").ToPlainString();
        Assert.AreEqual(
          "525000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "215.21E10").ToPlainString();
        Assert.AreEqual(
          "2152100000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "234.28E9").ToPlainString();
        Assert.AreEqual(
          "234280000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "310.24E9").ToPlainString();
        Assert.AreEqual(
          "310240000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "345.39E9").ToPlainString();
        Assert.AreEqual(
          "345390000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "116.38E-9").ToPlainString();
        Assert.AreEqual(
          "0.00000011638",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "276.25E10").ToPlainString();
        Assert.AreEqual(
          "2762500000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "158.32E-8").ToPlainString();
        Assert.AreEqual(
          "0.0000015832",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "272.5E2").ToPlainString();
        Assert.AreEqual(
          "27250",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "389.33E-9").ToPlainString();
        Assert.AreEqual(
          "0.00000038933",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "381.15E7").ToPlainString();
        Assert.AreEqual(
          "3811500000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "280.0E3").ToPlainString();
        Assert.AreEqual(
          "280000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "274.2E-6").ToPlainString();
        Assert.AreEqual(
          "0.0002742",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "387.14E-7").ToPlainString();
        Assert.AreEqual(
          "0.000038714",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "227.7E-7").ToPlainString();
        Assert.AreEqual(
          "0.00002277",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "201.21E2").ToPlainString();
        Assert.AreEqual(
          "20121",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "255.4E3").ToPlainString();
        Assert.AreEqual(
          "255400",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "187.27E-7").ToPlainString();
        Assert.AreEqual(
          "0.000018727",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "169.7E-4").ToPlainString();
        Assert.AreEqual(
          "0.01697",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "69.9E9").ToPlainString();
        Assert.AreEqual(
          "69900000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "3.20E-2").ToPlainString();
        Assert.AreEqual(
          "0.0320",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "236.30E2").ToPlainString();
        Assert.AreEqual(
          "23630",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "220.22E-9").ToPlainString();
        Assert.AreEqual(
          "0.00000022022",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "287.30E-1").ToPlainString();
        Assert.AreEqual(
          "28.730",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "156.3E-9").ToPlainString();
        Assert.AreEqual(
          "0.0000001563",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "136.23E-1").ToPlainString();
        Assert.AreEqual(
          "13.623",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "125.27E8").ToPlainString();
        Assert.AreEqual(
          "12527000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "180.30E-7").ToPlainString();
        Assert.AreEqual(
          "0.000018030",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "351.5E7").ToPlainString();
        Assert.AreEqual(
          "3515000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "28.28E9").ToPlainString();
        Assert.AreEqual(
          "28280000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "288.4E-3").ToPlainString();
        Assert.AreEqual(
          "0.2884",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "12.22E4").ToPlainString();
        Assert.AreEqual(
          "122200",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "257.5E-5").ToPlainString();
        Assert.AreEqual(
          "0.002575",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "389.20E3").ToPlainString();
        Assert.AreEqual(
          "389200",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "394.9E-4").ToPlainString();
        Assert.AreEqual(
          "0.03949",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "134.26E-7").ToPlainString();
        Assert.AreEqual(
          "0.000013426",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "58.29E5").ToPlainString();
        Assert.AreEqual(
          "5829000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "88.5E-5").ToPlainString();
        Assert.AreEqual(
          "0.000885",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "193.29E-4").ToPlainString();
        Assert.AreEqual(
          "0.019329",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "71.35E10").ToPlainString();
        Assert.AreEqual(
          "713500000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "252.0E1").ToPlainString();
        Assert.AreEqual(
          "2520",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "53.2E-8").ToPlainString();
        Assert.AreEqual(
          "0.000000532",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "181.20E-1").ToPlainString();
        Assert.AreEqual(
          "18.120",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "55.21E-9").ToPlainString();
        Assert.AreEqual(
          "0.00000005521",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "57.31E0").ToPlainString();
        Assert.AreEqual(
          "57.31",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "113.13E-9").ToPlainString();
        Assert.AreEqual(
          "0.00000011313",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "53.23E1").ToPlainString();
        Assert.AreEqual(
          "532.3",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "368.37E-7").ToPlainString();
        Assert.AreEqual(
          "0.000036837",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "187.4E-4").ToPlainString();
        Assert.AreEqual(
          "0.01874",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "5.26E8").ToPlainString();
        Assert.AreEqual(
          "526000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "308.32E4").ToPlainString();
        Assert.AreEqual(
          "3083200",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "76.15E-2").ToPlainString();
        Assert.AreEqual(
          "0.7615",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "117.38E7").ToPlainString();
        Assert.AreEqual(
          "1173800000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "15.37E-4").ToPlainString();
        Assert.AreEqual(
          "0.001537",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "145.3E0").ToPlainString();
        Assert.AreEqual(
          "145.3",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "226.29E8").ToPlainString();
        Assert.AreEqual(
          "22629000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "224.26E10").ToPlainString();
        Assert.AreEqual(
          "2242600000000",
          stringTemp);
      }
      {
        string stringTemp = EDecimal.FromString(
            "268.18E-9").ToPlainString();
        Assert.AreEqual(
          "0.00000026818",
          stringTemp);
      }
    }
    [Test]
    public void TestToSingle() {
      var fr = new RandomGenerator();
      float sng;
      sng = SingleOverflowToInfinity.ToSingle();
      Assert.IsTrue(Single.IsPositiveInfinity(sng));
      sng = SingleOverflowToInfinity.Negate().ToSingle();
      Assert.IsTrue(Single.IsNegativeInfinity(sng));
      sng = SingleUnderflowToZero.ToSingle();
      Assert.IsTrue(sng == 0.0);
      sng = SingleUnderflowToZero.Negate().ToSingle();
      Assert.IsTrue(sng == 0.0);
      for (var i = 0; i < 100000; ++i) {
        EDecimal edec;
        string edecstr;
        if (fr.UniformInt(100) < 10) {
          string decimals = RandomObjects.RandomBigIntString(fr);
          if (decimals[0] == '-') {
            decimals = decimals.Substring(1);
          }
          edecstr = RandomObjects.RandomBigIntString(fr) +
            "." + decimals + "e" + RandomObjects.RandomBigIntString(fr);
          edec = EDecimal.FromString(edecstr);
        } else {
          edec = RandomObjects.RandomEDecimal(fr);
          edecstr = edec.ToString();
        }
        if (edec.IsFinite) {
          sng = edec.ToSingle();
          if (Single.IsNegativeInfinity(sng)) {
            Assert.IsTrue(edec.IsNegative);
            TestCommon.CompareTestGreaterEqual(
              edec.Abs(),
              SingleOverflowToInfinity,
              edecstr);
          } else if (Single.IsPositiveInfinity(sng)) {
            Assert.IsTrue(!edec.IsNegative);
            TestCommon.CompareTestGreaterEqual(
              edec.Abs(),
              SingleOverflowToInfinity,
              edecstr);
          } else if (sng == 0.0f) {
            TestCommon.CompareTestLessEqual(
              edec.Abs(),
              SingleUnderflowToZero,
              edecstr);
            Assert.AreEqual(
              edec.IsNegative,
              EDecimal.FromSingle(sng).IsNegative,
              edecstr);
          } else {
            Assert.IsTrue(!Single.IsNaN(sng));
            edec = edec.Abs();
            TestCommon.CompareTestGreater(
              edec,
              SingleUnderflowToZero,
              edecstr);
            TestCommon.CompareTestLess(
              edec,
              SingleOverflowToInfinity,
              edecstr);
            EDecimal halfUlp = GetHalfUlp(sng);
            EDecimal difference = EDecimal.FromSingle(sng).Abs()
              .Subtract(edec).Abs();
            TestCommon.CompareTestLessEqual(difference, halfUlp, edecstr);
          }
        }
      }
    }
    private static string Repeat(string s, int count) {
      var sb = new System.Text.StringBuilder();
      for (var i = 0; i < count; ++i) {
        sb.Append(s);
      }
      return sb.ToString();
    }
    [Test]
    public void TestOnePlusOne() {
      EContext ec = EContext.ForRounding(ERounding.Up).WithPrecision(4);
      EDecimal ed = EDecimal.FromString("1");
      EDecimal ed2;
      string str;
      for (var i = 10; i < 1000; ++i) {
        str = "1." + Repeat("0", i) + "3";
        ed2 = EDecimal.FromString(str);
        Assert.AreEqual("2.001", ed.Add(ed2, ec).ToString(), str);
      }
    }

    [Test]
    public void TestToString() {
      for (var i = 0; i < ValueTestStrings.Length; i += 4) {
        Assert.AreEqual(
          ValueTestStrings[i + 1],
          EDecimal.FromString(ValueTestStrings[i]).ToString());
        Assert.AreEqual(
          ValueTestStrings[i + 2],
          EDecimal.FromString(ValueTestStrings[i]).ToEngineeringString());
        Assert.AreEqual(
          ValueTestStrings[i + 3],
          EDecimal.FromString(ValueTestStrings[i]).ToPlainString());
      }
      var fr = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        // Generate arbitrary-precision integers for exponent
        // and mantissa
        EInteger mantBig = RandomObjects.RandomEInteger(fr);
        EInteger expBig = RandomObjects.RandomEInteger(fr);
        EDecimal dec = EDecimal.Create(mantBig, expBig);
        ExtraTest.TestStringEqualRoundTrip(dec);
      }
      for (var i = 0; i < 1000; ++i) {
        EDecimal dec = RandomObjects.RandomEDecimal(fr);
        ExtraTest.TestStringEqualRoundTrip(dec);
      }
    }
    [Test]
    public void TestUnsignedMantissa() {
      // not implemented yet
    }
    [Test]
    public void TestZero() {
      Assert.AreEqual(EDecimal.Zero, EDecimal.FromInt32(0));
      if (EDecimal.Zero.ToSingle() != 0.0) {
        Assert.Fail("Failed " + EDecimal.Zero.ToSingle());
      }
      if (EDecimal.Zero.ToDouble() != 0.0) {
        Assert.Fail("Failed " + EDecimal.Zero.ToDouble());
      }
    }

    private static void AssertAddSubtract(string a, string b) {
      EDecimal decA = EDecimal.FromString(a);
      EDecimal decB = EDecimal.FromString(b);
      EDecimal decC = decA.Add(decB);
      EDecimal decD = decC.Subtract(decA);
      TestCommon.CompareTestEqual(decD, decB);
      decD = decC.Subtract(decB);
      TestCommon.CompareTestEqual(decD, decA);
    }

    private static void AssertDecimalsEquivalent(string a, string b) {
      EDecimal ca = EDecimal.FromString(a);
      EDecimal cb = EDecimal.FromString(b);
      TestCommon.CompareTestEqual(ca, cb);
    }

    private static void TestEDecimalDoubleCore(double d, string s) {
      double oldd = d;
      EDecimal bf = EDecimal.FromDouble(d);
      if (s != null) {
        Assert.AreEqual(s, bf.ToString());
      }
      d = bf.ToDouble();
      Assert.AreEqual((double)oldd, d);
    }

    private static void TestEDecimalSingleCore(float d, string s) {
      float oldd = d;
      EDecimal bf = EDecimal.FromSingle(d);
      if (s != null) {
        Assert.AreEqual(s, bf.ToString());
      }
      d = bf.ToSingle();
      Assert.AreEqual((float)oldd, d);
    }

    private void TestRoundToExponentOne(
      string input,
      string expected,
      int exponent,
      ERounding rounding) {
      EDecimal inputED = EDecimal.FromString(input);
      inputED = inputED.RoundToExponent(
          exponent,
          EContext.ForRounding(rounding));
      Assert.AreEqual(expected, inputED.ToString());
    }

    public static EDecimal Ulps(EDecimal expected, EDecimal actual, int
      precision) {
      if (expected == null) {
        throw new ArgumentNullException(nameof(expected));
      }
      if (actual == null) {
        throw new ArgumentNullException(nameof(actual));
      }
      if (precision <= 0) {
        throw new ArgumentOutOfRangeException(nameof(precision));
      }
      EInteger k = EInteger.Zero;
      while (true) {
        EDecimal pk = EDecimal.Create(EInteger.FromInt32(1), k.Negate())
          .Multiply(expected).Abs();
        if (pk.CompareTo(EDecimal.FromInt32(1)) >= 0 &&
          pk.CompareTo(EDecimal.FromInt32(10)) < 0) {
          break;
        }
        k = k.Add((pk.CompareTo(EDecimal.FromInt32(1)) < 0) ? -1 : 1);
      }
      return expected.Subtract(actual).Divide(EDecimal.Create(
            EInteger.FromInt32(1),
            k.Subtract(precision - 1)),
          EContext.ForPrecisionAndRounding(5, ERounding.Up)).Abs();
    }

    [Test]
    public void TestStringContextSpecific1() {
      string str = "60277096704082E-96";
      string expected = "6.027709E-83";
      EContext ec = EContext.Basic.WithExponentClamp(
          true).WithAdjustExponent(
          false).WithRounding(
          ERounding.Down).WithExponentRange(-95, 96).WithPrecision(7);
      TestStringContextOne(str, ec);
      string actualstr = EDecimal.FromString(str).RoundToPrecision(
          ec).ToString();
      Assert.AreEqual(expected, actualstr);
      actualstr = EDecimal.FromString(str, ec).ToString();
      Assert.AreEqual(expected, actualstr);
    }

    [Test]
    public void TestStringContextSpecific2() {
      string str = "8.888888888888E-214748365";
      string expected = "1E-103";
      EContext
      ec = EContext.Basic.WithExponentClamp(
          false).WithAdjustExponent(
          true).WithRounding(
          ERounding.OddOrZeroFiveUp).WithExponentRange(-95,
          96).WithPrecision(9);
      TestStringContextOne(str, ec);
      string actualstr = EDecimal.FromString(str).RoundToPrecision(
          ec).ToString();
      Assert.AreEqual(expected, actualstr);
      actualstr = EDecimal.FromString(str, ec).ToString();
      Assert.AreEqual(expected, actualstr);
    }

    [Test]
    public void TestStringContextSpecific7() {
      EContext ec = EContext.Unlimited.WithExponentClamp(
          true).WithAdjustExponent(
          true).WithRounding(
          ERounding.Floor).WithExponentRange(-9999999, 9999999);
      string str = TestCommon.Repeat("7", 1000) + "E-" +
        TestCommon.Repeat("7", 1000);
      TestStringContextOne(str, ec);
    }

    [Test]
    public void TestStringContextSpecific3() {
      string str = "10991.709233660650E-90";
      string expected = "1.099171E-86";
      EContext ec = EContext.Basic.WithExponentClamp(
          true).WithAdjustExponent(
          false).WithRounding(
          ERounding.Up).WithExponentRange(-95, 96).WithPrecision(7);
      TestStringContextOne(str, ec);
      string actualstr = EDecimal.FromString(str).RoundToPrecision(
          ec).ToString();
      Assert.AreEqual(expected, actualstr);
      actualstr = EDecimal.FromString(str, ec).ToString();
      Assert.AreEqual(expected, actualstr);
    }

    [Test]
    public void TestStringContextSpecific4a() {
      EContext ec = EContext.Basic.WithExponentClamp(
          true).WithAdjustExponent(
          true).WithRounding(
          ERounding.HalfUp).WithExponentRange(-95, 96).WithPrecision(7);
      TestStringContextOne("806840.80E+60", ec);
    }

    [Test]
    public void TestStringContextSpecific4b() {
      EContext ec = EContext.Basic.WithExponentClamp(
          true).WithAdjustExponent(
          false).WithRounding(
          ERounding.Ceiling).WithExponentRange(-95, 96).WithPrecision(7);
      TestStringContextOne("900.01740E-90", ec);
    }

    [Test]
    public void TestStringContextSpecific4c() {
      string num =

  "-16120570567037778210732025954408283690946444690491951476102714548515145821906708291828685686116455423481898854735868999853690814E-7564";
      TestStringContextOneEFloat(num, EContext.Binary64);
    }

    [Test]
    public void TestStringContextSpecific4d() {
      EContext ec = EContext.Unlimited.WithPrecision(53).WithExponentRange(
          -1022,
          1023).WithRounding(
          ERounding.HalfUp).WithAdjustExponent(
          true).WithExponentClamp(true).WithSimplified(false);
      string str = "1111111." + TestCommon.Repeat("1", 770) + "E-383";
      TestStringContextOneEFloat(str, ec);
    }

    [Test]
    public void TestStringContextUnderflow() {
      EContext ec = EContext.Binary64.WithRounding(
          ERounding.HalfUp);
      for (var i = 0; i < 700; ++i) {
        string str = "1111111" + (i == 0 ? String.Empty : ".") +
          TestCommon.Repeat("0", i) + "E-383";
        TestStringContextOneEFloat(str, ec);
        Assert.IsTrue(EFloat.FromString(str, ec).IsZero);
      }
    }

    [Test]
    public void TestStringContextSpecific4() {
      EContext ec = EContext.Basic.WithExponentClamp(
          true).WithAdjustExponent(
          true).WithRounding(
          ERounding.Floor).WithExponentRange(-95, 96).WithPrecision(7);
      TestStringContextOne("66666666666666666E+40", ec);
      TestStringContextOne("6666666666666666.6E+40", ec);
      TestStringContextOne("666666666666666.66E+40", ec);
      TestStringContextOne("66666666666666.666E+40", ec);
      TestStringContextOne("6.6666666666666666E+40", ec);
      TestStringContextOne("66.666666666666666E+40", ec);
      TestStringContextOne("666.66666666666666E+40", ec);
    }

    private static long unoptTime = 0;
    private static long unoptRoundTime = 0;
    private static long optTime = 0;
    /*
     private static readonly System.Diagnostics.Stopwatch swUnopt = new
     System.Diagnostics.Stopwatch();
     private static readonly System.Diagnostics.Stopwatch swUnoptRound = new
     System.Diagnostics.Stopwatch();
     private static readonly System.Diagnostics.Stopwatch swOpt2 = new
     System.Diagnostics.Stopwatch();
      */
    public static void TearDown() {
      if (unoptTime > 0) {
        Console.WriteLine("unoptTime = " + unoptTime + " ms");
        Console.WriteLine("unoptRoundTime = " + unoptRoundTime + " ms");
        Console.WriteLine("optTime = " + optTime + " ms");
        unoptTime = 0;
        unoptRoundTime = 0;
        optTime = 0;
      }
    }

    [Test]
    public void TestZerosRoundingNone() {
      EContext ec =
        EContext.Unlimited.WithPrecision(11).WithExponentRange(-14,
          15).WithRounding(
          ERounding.None).WithAdjustExponent(
          false).WithExponentClamp(
          true).WithSimplified(false).WithTraps(EContext.FlagInvalid);
      string str = "0E+5936";
      try {
        EFloat.FromString(str, ec);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestZerosRoundingNone4() {
      EContext ec =
        EContext.Unlimited.WithPrecision(11).WithExponentRange(-14,
          15).WithRounding(
          ERounding.Ceiling).WithAdjustExponent(
          true).WithExponentClamp(
          true).WithSimplified(false).WithTraps(EContext.FlagInvalid);
      string str = "049E+20";
      try {
        EFloat.FromString(str, ec);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestZerosRoundingNone3() {
      EContext ec =
        EContext.Unlimited.WithPrecision(11).WithExponentRange(-14,
          15).WithRounding(
          ERounding.None).WithAdjustExponent(
          false).WithExponentClamp(
          true).WithSimplified(false).WithTraps(EContext.FlagInvalid);
      string str = "0.0000E+59398886";
      try {
        EFloat.FromString(str, ec);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    // Test potential cases where FromString is implemented
    // to take context into account when building the EDecimal
    public static void TestStringContextOne(string str, EContext ec) {
      if (ec == null) {
        throw new ArgumentNullException(nameof(ec));
      }
      EContext noneRounding = ec.WithRounding(
          ERounding.None).WithTraps(EContext.FlagInvalid);
      EContext downRounding = ec.WithRounding(ERounding.Down);
      EDecimal ed, ed2;
      // Console.Write("TestStringContextOne ---- ec=" + (ec));
      // swUnopt.Restart();
      ed = EDecimal.FromString(str);
      EDecimal edorig = ed;
      // swUnoptRound.Restart();
      ed = ed.RoundToPrecision(ec);
      /*
       swUnoptRound.Stop();
       swUnopt.Stop();
       swOpt2.Restart();
      */
      ed2 = EDecimal.FromString(str, ec);
      /*
      swOpt2.Stop();
      unoptTime+=swUnopt.ElapsedMilliseconds;
      unoptRoundTime+=swUnoptRound.ElapsedMilliseconds;
      optTime+=swOpt2.ElapsedMilliseconds;
      if (swUnopt.ElapsedMilliseconds>100 &&
         swUnopt.ElapsedMilliseconds/4 <= swOpt2.ElapsedMilliseconds) {
       string bstr = str.Substring(0, Math.Min(str.Length, 200)) +
         (str.Length > 200 ? "..." : String.Empty);
       string edstr = ed.ToString();
       edstr = edstr.Substring(0, Math.Min(edstr.Length, 200)) +
         (edstr.Length > 200 ? "..." : String.Empty);
       Console.WriteLine(bstr +"\nresult=" + edstr + "\n" + ECString(ec)
      +"\nunopt="+
            swUnopt.ElapsedMilliseconds+" ms; opt="+swOpt2.ElapsedMilliseconds);
      }
      */

      EDecimal ef3 = EDecimal.NaN;
      try {
        ef3 = EDecimal.FromString(str, noneRounding);
      } catch (ETrapException) {
        // NOTE: Intentionally empty
      }
      EDecimal edef2 = ec.Rounding == ERounding.Down ? ed2 :
        EDecimal.FromString(str, downRounding);
      if ((ef3 != null && !ef3.IsNaN()) && edorig != null &&
        edorig.CompareTo(edef2) != 0) {
        Console.WriteLine("# ERounding.None fails to detect rounding was" +
          "\u0020necessary");
        if (str == null) {
          throw new ArgumentNullException(nameof(str));
        }
        Console.WriteLine("# str = " + str.Substring(0, Math.Min(str.Length,
              200)) + (str.Length > 200 ? "..." : String.Empty) + "\n# ec = " +
          ECString(ec));
        Console.WriteLine("# ef3 = " + ef3.ToString());
      } else if ((ef3 == null || ef3.IsNaN()) && edorig != null &&
        edorig.CompareTo(edef2) == 0) {
        Console.WriteLine("# ERounding.None fails to detect rounding was" +
          "\u0020unnecessary");
        if (str == null) {
          throw new ArgumentNullException(nameof(str));
        }
        Console.WriteLine("# str = " + str.Substring(0, Math.Min(str.Length,
              200)) + (str.Length > 200 ? "..." : String.Empty) + "\n# ec = " +
          ECString(ec));
        Console.WriteLine("# ed = " + edorig);
        Console.WriteLine("# edef2 = " + edef2);
        Console.WriteLine("# ef3 = " + ef3.ToString());
      }

      if (!ed.Equals(ed2)) {
        if (ec == null) {
          throw new ArgumentNullException(nameof(ec));
        }
        if (str == null) {
          throw new ArgumentNullException(nameof(str));
        }
        string bstr = String.Empty;
        if (ec.HasMaxPrecision) {
          EContext ecf = ec.WithBlankFlags();
          EDecimal.FromString(str).RoundToPrecision(ecf);
          bstr += "# " + ecf.Precision + " / " + ec.Precision + "\r\n";
          bstr += DecTestUtil.ContextToDecTestForm(ecf);
          bstr += "untitled toSci " + str + " -> " + ed.ToString() +
            DecTestUtil.FlagsToString(ecf.Flags) + "\n";
          str = ed2.ToString();
          bstr += "# exponent: actual " + ed2.Exponent + ", expected " +
            ed.Exponent + "\n";
          bstr += "# was: " + str.Substring(0, Math.Min(str.Length, 200)) +
            (str.Length > 200 ? "..." : String.Empty);
          } else {
          bstr += "# " + str.Substring(0, Math.Min(str.Length, 200)) +
            (str.Length > 200 ? "..." : String.Empty);
          bstr += "\n# " + ECString(ec);
        }
        throw new InvalidOperationException(bstr);
      }
    }

    public static string ECString(EContext ec) {
      var sb = new System.Text.StringBuilder().Append("EContext.Unlimited");
      if (ec == null) {
        throw new ArgumentNullException(nameof(ec));
      }
      if (ec.HasMaxPrecision) {
        sb.Append(".WithPrecision(" + ec.Precision.ToString() + ")");
      }
      if (ec.HasExponentRange) {
        sb.Append(".WithExponentRange(" + ec.EMin.ToString() + "," +
          "\u0020" + ec.EMax.ToString() + ")");
      }
      sb.Append(".WithRounding(ERounding." + ec.Rounding + ")");
      sb.Append(".WithAdjustExponent(" + (ec.AdjustExponent ? "true" :
          "false") + ")");
      sb.Append(".WithExponentClamp(" + (ec.ClampNormalExponents ? "true" :
          "false") + ")");
      sb.Append(".WithSimplified(" + (ec.IsSimplified ? "true" : "false") +
        ")");
      if (ec.HasFlags) {
        sb.Append(".WithBlankFlags()");
      }
      if (ec.Traps != 0) {
        sb.Append(".WithTraps(" + TestCommon.IntToString(ec.Traps) + ")");
      }
      return sb.ToString();
    }

    public static void TestStringContextOneEFloatSimple(string str, EContext
      ec) {
      TestStringContextOneEFloat(str, ec, true);
    }

    public static void TestStringContextOneEFloat(string str, EContext ec) {
      TestStringContextOneEFloat(str, ec, false);
    }

    public static void TestStringContextOneEFloat(
      string str,
      EContext ec,
      bool noLeadingZerosTest) {
      if (ec == null) {
        throw new ArgumentNullException(nameof(ec));
      }
      if (str == null) {
        throw new ArgumentNullException(nameof(str));
      }
      string leadingZeros = TestCommon.Repeat('0', 800);
      int[] counts = {
        0, 1, 2, 4, 6, 8, 10, 50, 100, 200, 300, 400,
        500, 600, 700, 800,
      };
      EDecimal ed = EDecimal.FromString("xyzxyz" + str, 6, str.Length);
      EFloat ef = ed.ToEFloat(ec);
      for (var i = 0; i < counts.Length; ++i) {
        // Parse a string with leading zeros (to test whether
        // the 768-digit trick delivers a correctly rounded EFloat
        // even if the string has leading zeros)
        TestStringContextOneEFloatCore(
          leadingZeros.Substring(0, counts[i]) + str,
          ec, ed, ef);
        if (noLeadingZerosTest || str.Length == 0 || str[0] == '-') {
          break;
        }
      }
    }

    // Test potential cases where FromString is implemented
    // to take context into account when building the EFloat
    public static void TestStringContextOneEFloatCore(
      string str,
      EContext ec,
      EDecimal ed,
      EFloat ef) {
      if (ec == null) {
        throw new ArgumentNullException(nameof(ec));
      }
      if (str == null) {
        throw new ArgumentNullException(nameof(str));
      }
      EFloat ef2 = null;
      EContext noneRounding = ec.WithRounding(
          ERounding.None); // .WithTraps(EContext.FlagInvalid);
      EContext downRounding = ec.WithRounding(ERounding.Down);
      ef2 = EFloat.FromString("xyzxyz" + str, 6, str.Length, ec);
      EFloat ef3 = EFloat.NaN;
      // try {
      ef3 = EFloat.FromString(str, noneRounding);
      // } catch (ETrapException) {
      // NOTE: Intentionally empty
      // }
      EDecimal edef2 = (ec.Rounding == ERounding.Down ?
          ef2 : EFloat.FromString(str, downRounding)).ToEDecimal();
      if ((ef3 != null && !ef3.IsNaN()) && ed != null &&
        ed.CompareTo(edef2) != 0) {
        Console.WriteLine("# ERounding.None fails to detect rounding was" +
          "\u0020necessary");
        if (str == null) {
          throw new ArgumentNullException(nameof(str));
        }
        Console.WriteLine("# str = " + str.Substring(0, Math.Min(str.Length,
              200)) + (str.Length > 200 ? "..." : String.Empty) + "\n# ec = " +
          ECString(ec));
        Console.WriteLine("# ef3 = " + ef3.ToString());
      } else if ((ef3 == null || ef3.IsNaN()) && ed != null &&
        ed.CompareTo(edef2) == 0) {
        Console.WriteLine("# ERounding.None fails to detect rounding was" +
          "\u0020unnecessary");
        if (str == null) {
          throw new ArgumentNullException(nameof(str));
        }
        Console.WriteLine("# str = " + str.Substring(0, Math.Min(str.Length,
              200)) + (str.Length > 200 ? "..." : String.Empty) + "\n# ec = " +
          ECString(ec));
        Console.WriteLine("# ed = " + ed);
        Console.WriteLine("# edef2 = " + edef2);
        Console.WriteLine("# ef3 = " + ef3.ToString());
      }
      if (ef == null || ef2 == null) {
        return;
      }
      if (ef.CompareTo(ef2) != 0) {
        if (ec == null) {
          throw new ArgumentNullException(nameof(ec));
        }
        if (str == null) {
          throw new ArgumentNullException(nameof(str));
        }
        string bstr = String.Empty;
        if (ec.HasMaxPrecision) {
          EContext ecf = ec.WithBlankFlags();
          EDecimal.FromString(str).RoundToPrecision(ecf);
          // bstr += DecTestUtil.ContextToDecTestForm(ecf);
          // bstr += "untitled toSci " + str + " -> " + ef.ToString() +
          // DecTestUtil.FlagsToString(ecf.Flags) + "\n";
          bstr += "{\nEContext ec = " + ECString(ec) + ";\n";
          bstr += "string str = \"" + str + "\";\n";
          bstr += "TestStringContextOneEFloat(str, ec);\n}\n";
          str = ef2.ToString();
          // bstr += "// expected: about " + Double.Parse (str) + "\n";
          bstr += "// was: " + str.Substring(0, Math.Min(str.Length, 200)) +
            (str.Length > 200 ? "..." : String.Empty);
          } else {
          bstr += "# " + str.Substring(0, Math.Min(str.Length, 200)) +
            (str.Length > 200 ? "..." : String.Empty);
          bstr += "\n# " + ECString(ec);
        }
        throw new InvalidOperationException(bstr);
      }
    }

    private static void AppendZeroFullDigits(
      StringBuilder sb,
      RandomGenerator rand,
      int count) {
      for (var i = 0; i < count; ++i) {
        if (rand.UniformInt(100) < 30) {
          sb.Append('0');
        } else {
          int c = 0x30 + rand.UniformInt(10);
          sb.Append((char)c);
        }
      }
    }

    private static void AppendNines(
      StringBuilder sb,
      int prec,
      int point) {
      if (point >= 0) {
        sb.Append(TestCommon.Repeat("9", point)).Append(".");
        sb.Append(TestCommon.Repeat("9", prec - point));
      } else {
        sb.Append(TestCommon.Repeat("9", prec));
      }
    }

    private static void AppendDigits(
      StringBuilder sb,
      RandomGenerator rand,
      int prec,
      int point) {
      string[] digits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
      if (rand.UniformInt(100) < 30) {
        if (point >= 0) {
          AppendZeroFullDigits(sb, rand, point);
          sb.Append(".");
          AppendZeroFullDigits(sb, rand, prec - point);
        } else {
          AppendZeroFullDigits(sb, rand, prec);
        }
      } else {
        int digit = rand.UniformInt(10);
        if (point >= 0) {
          sb.Append(TestCommon.Repeat(digits[digit], point)).Append(".");
          sb.Append(TestCommon.Repeat(digits[digit], prec - point));
        } else {
          sb.Append(TestCommon.Repeat(digits[digit], prec));
        }
      }
    }

    [Test]
    public void TestLeadingTrailingPoint() {
      Assert.AreEqual(EDecimal.FromString("4"), EDecimal.FromString("4."));
      Assert.AreEqual(EDecimal.FromString("0.4"), EDecimal.FromString(".4"));
      {
        object objectTemp = EDecimal.FromString("4e+5");
        object objectTemp2 = EDecimal.FromString(
            "4.e+5");
        Assert.AreEqual(objectTemp, objectTemp2);
      }
      Assert.AreEqual(
        EDecimal.FromString("99999999999"),
        EDecimal.FromString("99999999999."));
      Assert.AreEqual(EDecimal.FromString("0.99999999999"),
        EDecimal.FromString(".99999999999"));
      Assert.AreEqual(
        EDecimal.FromString("99999999999e+5"),
        EDecimal.FromString("99999999999.e+5"));
    }

    [Test]
    public void TestStringContextSpecificEFloat1() {
      var sb = new StringBuilder();
      EContext ec = EContext.Basic.WithPrecision(53).WithExponentClamp(true)
        .WithAdjustExponent(true).WithExponentRange(-1022, 1023)
        .WithRounding(ERounding.Up);
      TestStringContextOne("73007936310086.7E-383", ec);
    }

    [Test]
    public void TestStringContextSpecific5() {
      var sb = new StringBuilder();
      EContext ec = EContext.Basic.WithPrecision(7).WithExponentClamp(true)
        .WithAdjustExponent(true).WithExponentRange(-95, 96)
        .WithRounding(ERounding.HalfUp);
      AppendNines(sb, 400, 283);
      sb.Append("E-384");
      TestStringContextOne(sb.ToString(), ec);
    }

    [Test]
    public void TestStringContextSpecific6() {
      var sb = new StringBuilder();
      EContext ec = EContext.Basic.WithPrecision(7).WithExponentClamp(true)
        .WithAdjustExponent(true).WithExponentRange(-95, 96)
        .WithRounding(ERounding.HalfUp);
      AppendNines(sb, 400, 284);
      sb.Append("E-385");
      TestStringContextOne(sb.ToString(), ec);
    }

    [Test]
    public void TestStringContextSpecific6a() {
      var sb = new StringBuilder();
      EContext ec = EContext.Basic.WithPrecision(11).WithExponentClamp(true)
        .WithAdjustExponent(true).WithExponentRange(-14, 15)
        .WithRounding(ERounding.HalfDown);
      string str =

  "00726010602910507435000059115940090202200019076401000797770037005004100060.0201983258000005067E-96";
      TestStringContextOne(str, ec);
    }

    [Test]
    public void TestFromStringSubstring() {
      string tstr =

  "-3.00931381333368754713014659613049757554804012787921371662913692598770508705049030832574634419795955864174175076186656951904296875000E-49";
      try {
        EDecimal.FromString(
          "xyzxyz" + tstr,
          6,
          tstr.Length);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString("xyzxyz" + tstr, 6, tstr.Length);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString(tstr, 0, tstr.Length);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(tstr, 0, tstr.Length);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EDecimal.FromString(
          tstr + "xyzxyz",
          0,
          tstr.Length);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
      try {
        EFloat.FromString(tstr + "xyzxyz", 0, tstr.Length);
      } catch (Exception ex) {
        Assert.Fail(ex.ToString());
        throw new InvalidOperationException(String.Empty, ex);
      }
    }

    [Test]
    public void TestStringContextSpecificMore() {
      {
        string str = "precision: 7\nrounding: half_down\nmaxexponent: " +
          "96\n" + "minexponent: -95\nextended: 1\nclamp: 1\nuntitled toSci " +
          "555555555555555555E-94 -> 5.555556E-77 Inexact Rounded";
        DecTestUtil.ParseDecTests(
          str,
          false);
        EContext ec = EContext.Basic.WithPrecision(7).WithExponentClamp(true)
          .WithAdjustExponent(false).WithExponentRange(-95, 96)
          .WithRounding(ERounding.HalfEven);
        TestStringContextOne("487565.00310E-96", ec);
      }
      {
        EContext ec =
          EContext.Unlimited.WithPrecision(53).WithExponentRange(-1022,
            1023).WithRounding(ERounding.Down).WithAdjustExponent(
            true).WithExponentClamp(true).WithSimplified(false);
        string str = String.Empty + TestCommon.Repeat("6", 4605) + "." +
          TestCommon.Repeat("6",
            1538) + "E-999";
        TestStringContextOneEFloat(str, ec);
      }

      {
        EContext ec =
          EContext.Unlimited.WithPrecision(53).WithExponentRange(-1022,
            1023).WithRounding(ERounding.Ceiling).WithAdjustExponent(
            true).WithExponentClamp(true).WithSimplified(false);
        string str = String.Empty + TestCommon.Repeat("2", 6125) + "E-6143";
        TestStringContextOneEFloat(str, ec);
      }
      {
        EContext ec =
          EContext.Unlimited.WithPrecision(53).WithExponentRange(-1022,
            1023).WithRounding(ERounding.Down).WithAdjustExponent(
            true).WithExponentClamp(true).WithSimplified(false);
        string str = String.Empty + TestCommon.Repeat("2", 6165) + "E-6144";
        TestStringContextOneEFloat(str, ec);
      }
      {
        EContext ec =
          EContext.Unlimited.WithPrecision(53).WithExponentRange(-1022,
            1023).WithRounding(ERounding.HalfDown).WithAdjustExponent(
            true).WithExponentClamp(true).WithSimplified(false);
        string str =

  "0073000021960980007305056484911080609040458307738480635500600457003065700100902090896615030732652553075037501950247305214001000609697011805466300376799178890090568606761710483020290709180410260358805508079072601000651506000108202180008505073022007850910039820241008201400236050050246900250895790030707320060332426231940803760330870307891088002560602070261700009603405009775900250580042858910209705051402664037402890735007100990890000080400608930007400063761001060038422005000901008707958734420510029017800102078015560159466901008804187630045304308036045704003720440800815040565098720007028200106057003008686040653375800708409606600950560830030801760629754000760076605050983054030300803305080990620430700840009286601907275803099010881000640070927000080000008405306277002008029320337060582690200276900884274000901881004000310130000010529021820708007358724730000264010468066140278717080644405250055023049005410260170061028282960000387000838458809907775526543276066028052520064503000100807184090046410708762022080505049057381004290108034510779400054092008480560904006710454005310519093930221902122039610110010310904999053064480005347036006292155510100964159041000000080600200100953050100060302304400634050055060861170040001026314715506000820010098082607848000300070322081050000867074060748210041090041240035812005308214300200822920208400270023090003110479608027914239601710012061077000000001366560070009908108210007200750510209013151790093000730800276080250400008507803982439000000100669040403095399000960069280699214017925506071788615943730001561163245370150912016780305000103085005002106910768477605450240090047901050014022241930983590520881960050016606569010002040010370729830045000941024068389350459300480300460129770020070002010620036091110067768612651641004505298037850000830052791040670031808464000400917134087560809900804039066079020994020480506102558203708801200020055570506007057370403232340060613900619082084008555809560002551508005090489460838796507547672109005864200042100300800202808375953761040902004824600723084003003588014870008004933604890506600283627630387525107610907076802062000707616040007004667672660696000803953009260500830623049700845959005030500345031202000705360600000390110950008663000307062070324006059000068150026170125510711631670061503700990508040003960989089053312086107836920027007406700037704711600129067105252600004920298536005058003378580770002980990070504061768302300582325530610900070088099090323600407001087000440930030462709046304080073000593087600000344409005232117218901836766839909000807090823449060248032004031900400110470891000900601010431160000070805200609304320030476297778010900166400814109652150659007000004880205940084400030100534010300901904843027618403007084007410069934011066490027980605006620507000062039401035282097008512010185715616200003399000005045063608090067334002280090404390702501970603454069394005030700607001900941079758000519044404070855813207865778701005089030470063099108055888010000003500002327800542400707001714596712000400990450450094099170006070110660063300076733101806000077642101002303300105052300040581500702107300406209568326733460000050017760050800006777088068746920122152940222758028000744600119619246910290007803096261105023025068095480520103800879006903026138000075065382650022776414190069820188028458006098597000080229201880402170000023708000300233407460107004002100620601037785440008078000275604004145076507670153209445808502222360303327008009610000262892306003242430010300100300507208955280567155839100609759005082306090914146902008430007000356015185204375040562052010010106300370778308000027123420720080032072501006300902004395951040000035010055000070046080700080705006107706038049944005988070566700080000060273468025208869030759887691081340130051960076000810308727414108000040900005603783030824102080636044620119003403060406395605000000809000049693800000740740548264030900006320682153099988100417093340603621381680099828609008563245960739002010070080007093003000804025952503731474790226099261408010000790030302007718888342790470174084922301628701101613035300282601960820160194870077649252500091663420030054007077905000998009400886261390733103000189409201720888050111740989704007008606106600080833500509085085930806453406370977000439088350070539980687301901401050603080033918420570708904206031303051194000941044249882850350287700217022108640280058008034061255000708900005700182580214020076828009160506803309889379942603028064058021500016134500890569952490601000374000006654005340022209600080005977940420305900046963005063030214062337813016809210709203009208235500500922080001007072405558870306333550007038602140692020099072607760612384761090084000690025083900500374028101052519672720000485054141000704300306977025038900163623020400088010119763150804236200401850001001409500077000303050003009603400650005700902000072307202045707346000000167205713464110440039353600000399904610342901843008000005594505070080205000700000053290017653000628060060690030314284647630000227060008200230620510520606048052224017172525569283900900608092340909200258090778365000018156400220054455624700957208070003317950118450666253954414360904060640038749620706821283188030017057660780030100200874602220708065100205806002801008020070900349008951201003309048007037020034030719039090741410281099409002350082853006190004501527010804016220045556053282803803963075710866040620903150683367989410091680380309001070040036004901694203009806750070136107031500028738630130672000108030203200903203164022990715040206153000080002807000089400800510090316601000897800270023400240061092603021855008103721301372064900094700101400099060000370001204460046540253039940435053492530192158900041900801069002870000208420076700840560446600901800000007005210091070230011680030100844939703300020814109588024500654010010591923310010000258070220317280470889600010630004302304116034597040040400079961220303019772608870000007900080070807270804860260006000180013914550718684524093070000900960002370080001006099036005109603605005000000720290690207143018030912104700417603710790049567030554608500000490900420008109490930680201788168084422807403098800879740010800510180314329503000090907036709400073080057602060260501004308490265400394E-6200";
        TestStringContextOneEFloat(str, ec);
      }
    }

    [Test]
    public void TestRescaleInvalid() {
      var context = new Dictionary<string, string>();
      context["precision"] = "9";
      context["rounding"] = "half_up";
      context["maxexponent"] = "96";
      context["minexponent"] = "-96";
      {
        string objectTemp = "rr rescale 12345678.9 -2 -> NaN" +
          " Invalid_operation";
        DecTestUtil.ParseDecTest(objectTemp, context);
      }
      {
        string stringTemp = "rr quantize 12345678.9 0e-2 -> NaN" +
          " Invalid_operation";
        DecTestUtil.ParseDecTest(stringTemp, context);
      }
    }

    public static EContext RandomEFloatContext(IRandomGenExtended r) {
      return RandomEFloatContext(r, 20000);
    }

    public static EContext RandomEFloatContext(IRandomGenExtended r, int
      maxExponent) {
      if (r == null) {
        throw new ArgumentNullException(nameof(r));
      }
      int prec = 1 + r.GetInt32(53);
      int emax = 1 + r.GetInt32(maxExponent);
      int emin = -(emax - 1);
      ERounding[] roundings = {
        ERounding.Down, ERounding.Up,
        ERounding.OddOrZeroFiveUp, ERounding.HalfUp,
        ERounding.HalfDown, ERounding.HalfEven,
        ERounding.Ceiling, ERounding.Floor,
      };
      ERounding rounding = roundings[r.GetInt32(roundings.Length)];
      return EContext.Unlimited.WithPrecision(prec)
        .WithExponentRange(emin, emax).WithRounding(rounding)
        .WithSimplified(false).WithAdjustExponent(r.GetInt32(2) == 0)
        .WithExponentClamp(r.GetInt32(2) == 0);
    }

    public static EContext RandomEDecimalContext(IRandomGenExtended r) {
      if (r == null) {
        throw new ArgumentNullException(nameof(r));
      }
      int prec = 1 + r.GetInt32(100000);
      int emax = 1 + r.GetInt32(20000);
      int emin = -(emax - 1);
      ERounding[] roundings = {
        ERounding.Down, ERounding.Up,
        ERounding.OddOrZeroFiveUp, ERounding.HalfUp,
        ERounding.HalfDown, ERounding.HalfEven,
        ERounding.Ceiling, ERounding.Floor,
      };
      ERounding rounding = roundings[r.GetInt32(roundings.Length)];
      return EContext.Unlimited.WithPrecision(prec)
        .WithExponentRange(emin, emax).WithRounding(rounding)
        .WithSimplified(false).WithAdjustExponent(r.GetInt32(2) == 0)
        .WithExponentClamp(r.GetInt32(2) == 0);
    }

    [Test]
    public void TestStringContext() {
      Console.WriteLine("TestStringContextEFloat");
      TestStringContextEFloat();
      Console.WriteLine("TestStringContextEDecimal");
      TestStringContextEDecimal();
    }

    public static void TestStringContextEDecimal() {
      EContext[] econtexts = {
        EContext.Basic,
        EContext.Basic.WithExponentRange(-95, 96),
        EContext.Basic.WithAdjustExponent(false),
        EContext.Decimal32,
        EContext.Decimal32.WithAdjustExponent(false),
        EContext.Decimal32.WithExponentClamp(true),
        EContext.Decimal32.WithExponentClamp(true).WithAdjustExponent(false),
        EContext.BigDecimalJava,
        EContext.BigDecimalJava.WithAdjustExponent(true),
        EContext.BigDecimalJava.WithExponentClamp(true),
        EContext.BigDecimalJava.WithExponentClamp(true).WithAdjustExponent(
        false),
        EContext.Decimal64,
        EContext.Decimal64.WithAdjustExponent(false),
        EContext.Decimal64,
        EContext.Decimal64.WithAdjustExponent(false),
        EContext.Unlimited.WithExponentRange(-64, 64),
      };
      TestStringContextCore(econtexts, false);
    }

    public static void TestStringContextEFloat() {
      EContext[] econtexts = {
        EContext.Binary64,
        EContext.Binary64.WithExponentRange(-95, 96),
        EContext.Binary64.WithAdjustExponent(false),
        EContext.Binary32,
        EContext.Binary32.WithAdjustExponent(false),
        EContext.Binary32.WithExponentClamp(true),
        EContext.Binary32.WithExponentClamp(true).WithAdjustExponent(false),
        EContext.Binary16,
        EContext.Binary16.WithAdjustExponent(false),
        EContext.Binary16.WithExponentClamp(true),
        EContext.Binary16.WithExponentClamp(true).WithAdjustExponent(false),
        // EContext.Unlimited.WithExponentRange(-64, 64),
      };
      TestStringContextCore(econtexts, true);
    }

    public static void TestStringContextCore(EContext[] econtexts, bool
      efloat) {
      ERounding[] roundings = {
        ERounding.Down, ERounding.Up,
        ERounding.OddOrZeroFiveUp, ERounding.HalfUp,
        ERounding.HalfDown, ERounding.HalfEven,
        ERounding.Ceiling, ERounding.Floor,
      };
      int[] exponents = {
        94, 95, 96, 97,
        384, 383, 385,
        6144, 6200, 6143,
        10000, 0, 1, 2, 3, 4, 5, 10, 20, 40, 60,
        70, 80, 90,
        214748362, 214748363, 214748364, 214748365,
        Int32.MaxValue, Int32.MaxValue - 1,
      };
      int[] precisionRanges = {
        1, 7,
        1, 7,
        1, 20,
        1, 20,
        1, 20,
        1, 20,
        1, 20,
        1, 20,
        1, 20,
        90, 120,
        90, 120,
        90, 120,
        370, 400,
        370, 400,
        370, 400,
        1000, 1500,
        1000, 1500,
        6100, 6200,
        740, 800,
      };
      string[] digits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
      var rand = new RandomGenerator();
      for (var i = 0; i < 1000; ++i) {
        if (i % 10 == 0) {
          Console.WriteLine(i);
        }
        int precRange = rand.UniformInt(precisionRanges.Length / 2) * 2;
        int exponent = exponents[rand.UniformInt(exponents.Length)];
        int prec = precisionRanges[precRange] +
          rand.UniformInt(1 + (precisionRanges[precRange + 1] -
              precisionRanges[precRange]));
        precRange = rand.UniformInt(precisionRanges.Length / 2) * 2;
        int eprec = precisionRanges[precRange] +
          rand.UniformInt(1 + (precisionRanges[precRange + 1] -
              precisionRanges[precRange]));
        var point = -1;
        if (rand.UniformInt(2) == 0) {
          point = rand.UniformInt(prec);
          if (point == 0) {
            point = -1;
          }
        }
        string sbs;
        if (rand.UniformInt(2) == 0) {
          var sb = new StringBuilder();
          AppendDigits(sb, rand, prec, point);
          sb.Append(rand.UniformInt(2) == 0 ? "E+" : "E-");
          if (rand.UniformInt(100) < 10) {
            AppendDigits(sb, rand, eprec, -1);
          } else {
            sb.Append(TestCommon.LongToString(exponent));
          }
          sbs = sb.ToString();
        } else {
          sbs = RandomObjects.RandomDecimalString(rand);
        }
        if (econtexts == null) {
          throw new ArgumentNullException(nameof(econtexts));
        }
        for (var j = 0; j < econtexts.Length; ++j) {
          ERounding rounding = roundings[rand.UniformInt(roundings.Length)];
          EContext ec = econtexts[j].WithRounding(rounding);
          if (efloat) {
            TestStringContextOneEFloat(sbs, ec);
          }
          TestStringContextOne(sbs, ec);
        }
      }
      TearDown();
    }
  }
}
