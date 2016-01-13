/*
Written in 2013 by Peter O.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://upokecenter.dreamhosters.com/articles/donate-now-2/
 */
using System;

namespace PeterO.Numbers {
  internal sealed class FastInteger2 : IComparable<FastInteger2> {
    private int smallValue;  // if integerMode is 0
    private EInteger largeValue;  // if integerMode is 2
    private int integerMode;
    private static readonly EInteger ValueInt32MinValue =
      (EInteger)Int32.MinValue;

    private static readonly EInteger ValueInt32MaxValue =
      (EInteger)Int32.MaxValue;

    private static readonly EInteger ValueNegativeInt32MinValue =
    -(EInteger)ValueInt32MinValue;

    internal FastInteger2(int value) {
      this.smallValue = value;
    }

    public override bool Equals(object obj) {
      var fi = obj as FastInteger2;
      if (fi == null) {
 return false;
}
      return this.integerMode == fi.integerMode &&
        this.smallValue == fi.smallValue &&
        (this.largeValue == null ? fi.largeValue == null :
          this.largeValue.Equals(fi.largeValue));
    }

    public override int GetHashCode() {
      int hash = unchecked(hash * 31 + this.integerMode);
      hash = unchecked(hash * 31 + this.smallValue);
      hash = unchecked(hash * 31 +
        (this.largeValue == null ? 0 : this.largeValue.GetHashCode()));
      return hash;
    }

    internal static FastInteger2 FromBig(EInteger bigintVal) {
      if (bigintVal.CanFitInInt32()) {
        return new FastInteger2(bigintVal.AsInt32Unchecked());
      }
      var fi = new FastInteger2(0);
      fi.integerMode = 2;
      fi.largeValue = bigintVal;
      return fi;
    }

    internal int AsInt32() {
      return (this.integerMode == 0) ?
        this.smallValue : this.largeValue.AsInt32Unchecked();
    }

    public static FastInteger2 Add(FastInteger2 a, FastInteger2 b) {
      if (a.integerMode == 0 && b.integerMode == 0) {
        if (a.smallValue == 0) {
 return b;
}
        if (b.smallValue == 0) {
 return a;
}
        if ((a.smallValue < 0 && b.smallValue >= Int32.MinValue -
            a.smallValue) || (a.smallValue > 0 && b.smallValue <=
            Int32.MaxValue - a.smallValue)) {
        return new FastInteger2(a.smallValue + b.smallValue);
      }
    }
      EInteger bigA = a.AsEInteger();
      EInteger bigB = b.AsEInteger();
      return FastInteger2.FromBig(bigA.Add(bigB));
    }

    public static FastInteger2 Subtract(FastInteger2 a, FastInteger2 b) {
      if (a.integerMode == 0 && b.integerMode == 0) {
        if (b.smallValue == 0) {
 return a;
}
      if ((b.smallValue < 0 && Int32.MaxValue + b.smallValue >= a.smallValue)
          ||
         (b.smallValue > 0 && Int32.MinValue + b.smallValue <=
                  a.smallValue)) {
        return new FastInteger2(a.smallValue - b.smallValue);
      }
    }
      EInteger bigA = a.AsEInteger();
      EInteger bigB = b.AsEInteger();
      return FastInteger2.FromBig(bigA.Subtract(bigB));
    }

    public int CompareTo(FastInteger2 val) {
      switch ((this.integerMode << 2) | val.integerMode) {
        case (0 << 2) | 0:
          {
            int vsv = val.smallValue;
            return (this.smallValue == vsv) ? 0 : (this.smallValue < vsv ? -1 :
                  1);
          }
        case (0 << 2) | 2:
          return this.AsEInteger().CompareTo(val.largeValue);
        case (2 << 2) | 0:
        case (2 << 2) | 2:
          return this.largeValue.CompareTo(val.AsEInteger());
        default: throw new InvalidOperationException();
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.FastInteger2.Negate"]/*'/>
    internal FastInteger2 Negate() {
      switch (this.integerMode) {
        case 0:
          if (this.smallValue == Int32.MinValue) {
            return FastInteger2.FromBig(ValueNegativeInt32MinValue);
          } else {
            return new FastInteger2(-smallValue);
          }
        case 2:
          return FastInteger2.FromBig(-(EInteger)this.largeValue);
        default: throw new InvalidOperationException();
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.FastInteger2.IsEvenNumber"]/*'/>
    internal bool IsEvenNumber {
      get {
        switch (this.integerMode) {
          case 0:
            return (this.smallValue & 1) == 0;
          case 2:
            return this.largeValue.IsEven;
          default:
            throw new InvalidOperationException();
        }
      }
    }

    internal bool CanFitInInt32() {
      return this.integerMode == 0 || this.largeValue.CanFitInInt32();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.FastInteger2.ToString"]/*'/>
    public override string ToString() {
      switch (this.integerMode) {
        case 0:
          return FastInteger.IntToString(this.smallValue);
        case 2:
          return this.largeValue.ToString();
        default: return String.Empty;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.FastInteger2.Sign"]/*'/>
    internal int Sign {
      get {
        switch (this.integerMode) {
          case 0:
            return (this.smallValue == 0) ? 0 : ((this.smallValue < 0) ? -1 :
                1);
          case 2:
            return this.largeValue.Sign;
          default: return 0;
        }
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.FastInteger2.IsValueZero"]/*'/>
    internal bool IsValueZero {
      get {
        switch (this.integerMode) {
          case 0:
            return this.smallValue == 0;
          case 2:
            return this.largeValue.IsZero;
          default:
            return false;
        }
      }
    }

    internal int CompareToInt(int val) {
      switch (this.integerMode) {
        case 0:
          return (val == this.smallValue) ? 0 : (this.smallValue < val ? -1 :
          1);
        case 2:
          return this.largeValue.CompareTo((EInteger)val);
        default: return 0;
      }
    }

    internal EInteger AsEInteger() {
      switch (this.integerMode) {
        case 0:
          return EInteger.FromInt32(this.smallValue);
        case 2:
          return this.largeValue;
        default: throw new InvalidOperationException();
      }
    }
  }
}
