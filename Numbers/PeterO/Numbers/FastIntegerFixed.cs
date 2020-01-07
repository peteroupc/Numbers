/*
Written by Peter O. in 2013.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;

namespace PeterO.Numbers {
  internal sealed class FastIntegerFixed : IComparable<FastIntegerFixed> {
    private const int CacheFirst = -24;
    private const int CacheLast = 128;

    private readonly int smallValue; // if integerMode is 0
    private readonly EInteger largeValue; // if integerMode is 2
    private readonly byte integerMode;

    public static readonly FastIntegerFixed Zero = new FastIntegerFixed(
      (byte)0,
      0,
      null);
    public static readonly FastIntegerFixed One = new FastIntegerFixed(
      (byte)0,
      1,
      null);

    private static readonly EInteger ValueInt32MinValue =
      (EInteger)Int32.MinValue;

    private static readonly EInteger ValueNegativeInt32MinValue =
      -(EInteger)ValueInt32MinValue;

    private static readonly FastIntegerFixed[] Cache =
FastIntegerFixedCache(CacheFirst,
  CacheLast);

    private static FastIntegerFixed[] FastIntegerFixedCache(
      int first,
      int last) {
#if DEBUG
if (first < -65535) {
  throw new ArgumentException("first (" + first + ") is not greater or equal" +
"\u0020to " + (-65535));
}
if (first > 65535) {
  throw new ArgumentException("first (" + first + ") is not less or equal to" +
"\u002065535");
}
if (last < -65535) {
  throw new ArgumentException("last (" + last + ") is not greater or equal" +
"\u0020to -65535");
}
if (last > 65535) {
  throw new ArgumentException("last (" + last + ") is not less or equal to" +
"65535");
}
#endif
FastIntegerFixed[] cache = new FastIntegerFixed[(last - first) + 1];
for (int i = first; i <= last; ++i) {
  if (i == 0) {
    cache[i - first] = Zero;
  } else if (i == 1) {
    cache[i - first] = One;
  } else {
 cache[i - first] = new FastIntegerFixed((byte)0, i, null);
}
}
return cache;
    }

    private FastIntegerFixed(
      byte integerMode,
      int smallValue,
      EInteger largeValue) {
      this.integerMode = integerMode;
      this.smallValue = smallValue;
      this.largeValue = largeValue;
    }

    public override bool Equals(object obj) {
      var fi = obj as FastIntegerFixed;
      if (fi == null) {
        return false;
      }
      if (this.integerMode != fi.integerMode) {
        return false;
      }
      if (this.integerMode == 0) {
        if (this.smallValue != fi.smallValue) {
          return false;
        }
      } else if (this.integerMode == 1) {
        if (!this.largeValue.Equals(fi.largeValue)) {
          return false;
        }
      }
      return true;
    }

    public override int GetHashCode() {
      int hash = unchecked(31 + this.integerMode);
      if (this.integerMode == 0) {
        hash = unchecked((hash * 31) + this.smallValue);
      } else if (this.integerMode == 1) {
        hash = unchecked((hash * 31) + this.largeValue.GetHashCode());
      }
      return hash;
    }

    internal static FastIntegerFixed FromInt32(int intVal) {
return (intVal >= CacheFirst && intVal <= CacheLast) ?
Cache[intVal - CacheFirst] :
      new FastIntegerFixed((byte)0, intVal, null);
    }

    internal static FastIntegerFixed FromLong(long longVal) {
      return (longVal >= Int32.MinValue && longVal <= Int32.MaxValue) ?
FromInt32((int)longVal) : new FastIntegerFixed(
          (byte)2,
          0,
          EInteger.FromInt64(longVal));
    }

    internal static FastIntegerFixed FromBig(EInteger bigintVal) {
      return bigintVal.CanFitInInt32() ?
FromInt32(bigintVal.ToInt32Unchecked()) : new
        FastIntegerFixed((byte)2, 0, bigintVal);
    }

    internal int AsInt32() {
      return (this.integerMode == 0) ?
        this.smallValue : this.largeValue.ToInt32Unchecked();
    }

    public static FastIntegerFixed FromFastInteger(FastInteger fi) {
      if (fi.CanFitInInt32()) {
        return FromInt32(fi.AsInt32());
      } else {
        return FastIntegerFixed.FromBig(fi.AsEInteger());
      }
    }

    public FastInteger ToFastInteger() {
      if (this.integerMode == 0) {
        return new FastInteger(this.smallValue);
      } else {
        return FastInteger.FromBig(this.largeValue);
      }
    }

    public FastIntegerFixed Increment() {
      if (this.integerMode == 0 && this.smallValue != Int32.MaxValue) {
        return FromInt32(this.smallValue + 1);
      } else {
        return Add(this, FastIntegerFixed.One);
      }
    }

    public int Mod(int value) {
      if (value < 0) {
        throw new NotSupportedException();
      }
      if (this.integerMode == 0 && this.smallValue >= 0) {
        return this.smallValue % value;
      } else {
        EInteger retval = this.ToEInteger().Remainder(EInteger.FromInt32(
  value));
        return retval.ToInt32Checked();
      }
    }

    public static FastIntegerFixed Add(FastIntegerFixed a,
      FastIntegerFixed b) {
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
          return FromInt32(a.smallValue + b.smallValue);
        }
      }
      EInteger bigA = a.ToEInteger();
      EInteger bigB = b.ToEInteger();
      return FastIntegerFixed.FromBig(bigA.Add(bigB));
    }

    public static FastIntegerFixed Subtract(
      FastIntegerFixed a,
      FastIntegerFixed b) {
      if (a.integerMode == 0 && b.integerMode == 0) {
        if (b.smallValue == 0) {
          return a;
        }
        if (
          (b.smallValue < 0 && Int32.MaxValue + b.smallValue >= a.smallValue) ||
          (b.smallValue > 0 && Int32.MinValue + b.smallValue <=
            a.smallValue)) {
          return FromInt32(a.smallValue - b.smallValue);
        }
      }
      EInteger bigA = a.ToEInteger();
      EInteger bigB = b.ToEInteger();
      return FastIntegerFixed.FromBig(bigA.Subtract(bigB));
    }

    public int CompareTo(FastIntegerFixed val) {
      switch ((this.integerMode << 2) | val.integerMode) {
        case (0 << 2) | 0: {
          int vsv = val.smallValue;
          return (this.smallValue == vsv) ? 0 : (this.smallValue < vsv ? -1 :
              1);
        }
        case (0 << 2) | 2:
          return this.ToEInteger().CompareTo(val.largeValue);
        case (2 << 2) | 0:
        case (2 << 2) | 2:
          return this.largeValue.CompareTo(val.ToEInteger());
        default: throw new InvalidOperationException();
      }
    }

    internal FastIntegerFixed Copy() {
      switch (this.integerMode) {
        case 0:
          return FromInt32(this.smallValue);
        case 2:
          return FastIntegerFixed.FromBig(this.largeValue);
        default: throw new InvalidOperationException();
      }
    }

    /// <summary>This is an internal API.</summary>
    /// <returns>A FastIntegerFixed object.</returns>
    internal FastIntegerFixed Negate() {
      switch (this.integerMode) {
        case 0:
          if (this.smallValue == Int32.MinValue) {
            return FastIntegerFixed.FromBig(ValueNegativeInt32MinValue);
          } else {
            return FromInt32(-this.smallValue);
          }
        case 2:
          return FastIntegerFixed.FromBig(-(EInteger)this.largeValue);
        default: throw new InvalidOperationException();
      }
    }

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

    /// <summary>This is an internal API.</summary>
    /// <returns>A text string.</returns>
    public override string ToString() {
      switch (this.integerMode) {
        case 0:
          return FastInteger.IntToString(this.smallValue);
        case 2:
          return this.largeValue.ToString();
        default: return String.Empty;
      }
    }

    internal int Sign {
      get {
        switch (this.integerMode) {
          case 0:
            return (this.smallValue == 0) ? 0 : ((this.smallValue < 0) ? -1 :
                1);
          case 2:
            return this.largeValue.Sign;
          default:
            return 0;
        }
      }
    }

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

    internal bool CanFitInInt64() {
      switch (this.integerMode) {
        case 0:
          return true;
        case 2:
          return this.largeValue
            .CanFitInInt64();

        default: throw new InvalidOperationException();
      }
    }

    internal long AsInt64() {
      switch (this.integerMode) {
        case 0:
          return (long)this.smallValue;
        case 2:
          return this.largeValue
            .ToInt64Unchecked();

        default: throw new InvalidOperationException();
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

    internal EInteger ToEInteger() {
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
