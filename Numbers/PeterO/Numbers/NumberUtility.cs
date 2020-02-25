/*
Written by Peter O. in 2013.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;

namespace PeterO.Numbers {
  internal static class NumberUtility {
    private static readonly EInteger[] ValueBigIntPowersOfTen = {
      EInteger.One, (EInteger)10, (EInteger)100, (EInteger)1000,
      (EInteger)10000, (EInteger)100000, (EInteger)1000000,
      (EInteger)10000000, (EInteger)100000000, (EInteger)1000000000,
      (EInteger)10000000000L, (EInteger)100000000000L,
      (EInteger)1000000000000L, (EInteger)10000000000000L,
      (EInteger)100000000000000L, (EInteger)1000000000000000L,
      (EInteger)10000000000000000L,
      (EInteger)100000000000000000L, (EInteger)1000000000000000000L,
    };

    private static readonly EInteger[] ValueBigIntPowersOfFive = {
      EInteger.One, (EInteger)5, (EInteger)25, (EInteger)125,
      (EInteger)625, (EInteger)3125, (EInteger)15625,
      (EInteger)78125, (EInteger)390625,
      (EInteger)1953125, (EInteger)9765625, (EInteger)48828125,
      (EInteger)244140625, (EInteger)1220703125,
      (EInteger)6103515625L, (EInteger)30517578125L,
      (EInteger)152587890625L, (EInteger)762939453125L,
      (EInteger)3814697265625L, (EInteger)19073486328125L,
      (EInteger)95367431640625L,
      (EInteger)476837158203125L, (EInteger)2384185791015625L,
      (EInteger)11920928955078125L,
      (EInteger)59604644775390625L, (EInteger)298023223876953125L,
      (EInteger)1490116119384765625L, (EInteger)7450580596923828125L,
    };

    internal static int ShiftLeftOne(int[] arr) {
      unchecked {
        var carry = 0;
        for (var i = 0; i < arr.Length; ++i) {
          int item = arr[i];
          arr[i] = (int)(arr[i] << 1) | (int)carry;
          carry = ((item >> 31) != 0) ? 1 : 0;
        }
        return carry;
      }
    }

    private static int CountTrailingZeros(int numberValue) {
      if (numberValue == 0) {
        return 32;
      }
      var i = 0;
      unchecked {
        if ((numberValue << 16) == 0) {
          numberValue >>= 16;
          i += 16;
        }
        if ((numberValue << 24) == 0) {
          numberValue >>= 8;
          i += 8;
        }
        if ((numberValue << 28) == 0) {
          numberValue >>= 4;
          i += 4;
        }
        if ((numberValue << 30) == 0) {
          numberValue >>= 2;
          i += 2;
        }
        if ((numberValue << 31) == 0) {
          ++i;
        }
      }
      return i;
    }

    internal static int BitPrecisionInt(int numberValue) {
      if (numberValue == 0) {
        return 0;
      }
      var i = 32;
      unchecked {
        if ((numberValue >> 16) == 0) {
          numberValue <<= 16;
          i -= 8;
        }
        if ((numberValue >> 24) == 0) {
          numberValue <<= 8;
          i -= 8;
        }
        if ((numberValue >> 28) == 0) {
          numberValue <<= 4;
          i -= 4;
        }
        if ((numberValue >> 30) == 0) {
          numberValue <<= 2;
          i -= 2;
        }
        if ((numberValue >> 31) == 0) {
          --i;
        }
      }
      return i;
    }

    internal static int ShiftAwayTrailingZerosTwoElements(int[] arr) {
      int a0 = arr[0];
      int a1 = arr[1];
      int tz = CountTrailingZeros(a0);
      if (tz == 0) {
        return 0;
      }
      unchecked {
        if (tz < 32) {
          int carry = a1 << (32 - tz);
          arr[0] = (int)((a0 >> tz) & (0x7fffffff >> (tz - 1))) | (int)carry;
          arr[1] = (a1 >> tz) & (0x7fffffff >> (tz - 1));
          return tz;
        }
        tz = CountTrailingZeros(a1);
        if (tz == 32) {
          arr[0] = 0;
        } else if (tz > 0) {
          arr[0] = (a1 >> tz) & (0x7fffffff >> (tz - 1));
        } else {
          arr[0] = a1;
        }
        arr[1] = 0;
        return 32 + tz;
      }
    }

    internal static bool HasBitSet(int[] arr, int bit) {
      return (bit >> 5) < arr.Length && (arr[bit >> 5] & (1 << (bit & 31))) !=
        0;
    }

    private sealed class PowerCache {
      private const int MaxSize = 128;
      private readonly EInteger[] outputs;
      private readonly EInteger[] inputs;
      private readonly int[] inputsInts;

      public PowerCache() {
        this.outputs = new EInteger[MaxSize];
        this.inputs = new EInteger[MaxSize];
        this.inputsInts = new int[MaxSize];
      }

      private int size;

      public EInteger[] FindCachedPowerOrSmaller(EInteger bi) {
        EInteger[] ret = null;
        EInteger minValue = null;
        if (bi.CanFitInInt32()) {
          return this.FindCachedPowerIntOrSmaller(bi.ToInt32Checked());
        }
        lock (this.outputs) {
          for (var i = 0; i < this.size; ++i) {
            if (this.inputs[i].CompareTo(bi) <= 0 && (minValue == null ||
                this.inputs[i].CompareTo(minValue) >= 0)) {
              // DebugUtility.Log("Have cached power (" + inputs[i] + "," + bi + ") ");
              ret = new EInteger[2];
              ret[0] = this.inputs[i];
              ret[1] = this.outputs[i];
              minValue = this.inputs[i];
            }
          }
        }
        return ret;
      }

      public EInteger[] FindCachedPowerIntOrSmaller(int precision) {
        EInteger[] ret = null;
        var integerMinValue = -1;
        lock (this.outputs) {
          for (var i = 0; i < this.size; ++i) {
            if (this.inputsInts[i] >= 0 &&
              this.inputsInts[i] <= precision && (integerMinValue == -1 ||
                this.inputsInts[i] >= integerMinValue)) {
              // DebugUtility.Log("Have cached power (" + inputs[i] + "," + bi + ") ");
              ret = new EInteger[2];
              ret[0] = this.inputs[i];
              ret[1] = this.outputs[i];
              integerMinValue = this.inputsInts[i];
            }
          }
        }
        return ret;
      }

      public EInteger GetCachedPower(EInteger bi) {
        if (bi.CanFitInInt32()) {
          return this.GetCachedPowerInt(bi.ToInt32Checked());
        }
        lock (this.outputs) {
          for (var i = 0; i < this.size; ++i) {
            if (bi.Equals(this.inputs[i])) {
              if (i != 0) {
                EInteger tmp;
                // Move to head of cache if it isn't already
                tmp = this.inputs[i];
                this.inputs[i] = this.inputs[0];
                this.inputs[0] = tmp;
                int tmpi = this.inputsInts[i];
                this.inputsInts[i] = this.inputsInts[0];
                this.inputsInts[0] = tmpi;
                tmp = this.outputs[i];
                this.outputs[i] = this.outputs[0];
                this.outputs[0] = tmp;
                // Move formerly newest to next newest
                if (i != 1) {
                  tmp = this.inputs[i];
                  this.inputs[i] = this.inputs[1];
                  this.inputs[1] = tmp;
                  tmpi = this.inputsInts[i];
                  this.inputsInts[i] =
                    this.inputsInts[1];
                  this.inputsInts[1] = tmpi;
                  tmp = this.outputs[i];
                  this.outputs[i] = this.outputs[1];
                  this.outputs[1] = tmp;
                }
              }
              return this.outputs[0];
            }
          }
        }
        return null;
      }

      public EInteger GetCachedPowerInt(int ibi) {
        lock (this.outputs) {
          if (ibi > 0 && this.size < 64) {
            for (var i = 0; i < this.size; ++i) {
              if (this.inputsInts[i] == ibi) {
                return this.outputs[i];
              }
            }
            return null;
          }
          for (var i = 0; i < this.size; ++i) {
            if (this.inputsInts[i] >= 0 && this.inputsInts[i] == ibi) {
              if (i != 0) {
                EInteger tmp;
                // Move to head of cache if it isn't already
                tmp = this.inputs[i];
                this.inputs[i] = this.inputs[0];
                this.inputs[0] = tmp;
                int tmpi = this.inputsInts[i];
                this.inputsInts[i] = this.inputsInts[0];
                this.inputsInts[0] = tmpi;
                tmp = this.outputs[i];
                this.outputs[i] = this.outputs[0];
                this.outputs[0] = tmp;
                // Move formerly newest to next newest
                if (i != 1) {
                  tmp = this.inputs[i];
                  this.inputs[i] = this.inputs[1];
                  this.inputs[1] = tmp;
                  tmpi = this.inputsInts[i];
                  this.inputsInts[i] =
                    this.inputsInts[1];
                  this.inputsInts[1] = tmpi;
                  tmp = this.outputs[i];
                  this.outputs[i] = this.outputs[1];
                  this.outputs[1] = tmp;
                }
              }
              return this.outputs[0];
            }
          }
        }
        return null;
      }

      public void AddPower(int input, EInteger output) {
        this.AddPower(EInteger.FromInt32(input), output);
      }
      public void AddPower(EInteger input, EInteger output) {
        lock (this.outputs) {
          if (this.size < MaxSize) {
            // Shift newer entries down
            for (int i = this.size; i > 0; --i) {
              this.inputs[i] = this.inputs[i - 1];
              this.inputsInts[i] = this.inputsInts[i - 1];
              this.outputs[i] = this.outputs[i - 1];
            }
            this.inputs[0] = input;
            this.inputsInts[0] = input.CanFitInInt32() ?
              input.ToInt32Checked() : -1;
            this.outputs[0] = output;
            ++this.size;
          } else {
            // Shift newer entries down
            for (int i = MaxSize - 1; i > 0; --i) {
              this.inputs[i] = this.inputs[i - 1];
              this.inputsInts[i] = this.inputsInts[i - 1];
              this.outputs[i] = this.outputs[i - 1];
            }
            this.inputs[0] = input;
            this.inputsInts[0] = input.CanFitInInt32() ?
              input.ToInt32Checked() : -1;
            this.outputs[0] = output;
          }
        }
      }
    }

    private static readonly PowerCache ValuePowerOfFiveCache = new
    NumberUtility.PowerCache();

    private static readonly PowerCache ValuePowerOfTenCache = new
    NumberUtility.PowerCache();

    public static EInteger FindPowerOfTen(long diffLong) {
      if (diffLong < 0) {
        return EInteger.Zero;
      }
      if (diffLong == 0) {
        return EInteger.One;
      }
      return (diffLong <= Int32.MaxValue) ? FindPowerOfTen((int)diffLong) :
        FindPowerOfTenFromBig(EInteger.FromInt64(diffLong));
    }

    internal static EInteger MultiplyByPowerOfFive(EInteger v, int precision) {
      if (precision < 0 || v.IsZero) {
        return EInteger.Zero;
      }
      if (precision <= 94) {
        return v.Multiply(FindPowerOfFive(precision));
      }
      EInteger otherPower = ValuePowerOfFiveCache.GetCachedPowerInt(precision);
      if (otherPower != null) {
        return v.Multiply(otherPower);
      }
      var powprec = 64;
      v = v.Multiply(FindPowerOfFive(precision & 63));
      precision >>= 6;
      while (precision > 0) {
        if ((precision & 1) == 1) {
          otherPower = ValuePowerOfFiveCache.GetCachedPowerInt(powprec);
          if (otherPower == null) {
            // NOTE: Assumes powprec is 2 or greater and is a power of 2
            EInteger prevPower = FindPowerOfFive(powprec >> 1);
            otherPower = prevPower.Multiply(prevPower);
            ValuePowerOfFiveCache.AddPower(powprec, otherPower);
          }
          v = v.Multiply(otherPower);
        }
        powprec = unchecked(powprec << 1);
        precision >>= 1;
      }
      return v;
    }

    internal static EInteger MultiplyByPowerOfFive(EInteger v, EInteger
      epower) {
      return epower.CanFitInInt32() ? MultiplyByPowerOfFive(v,
          epower.ToInt32Checked()) : v.Multiply(FindPowerOfFiveFromBig(
            epower));
    }
    internal static EInteger FindPowerOfFiveFromBig(EInteger diff) {
      int sign = diff.Sign;
      if (sign < 0) {
        return EInteger.Zero;
      }
      if (sign == 0) {
        return EInteger.One;
      }
      if (diff.CanFitInInt32()) {
        return FindPowerOfFive(diff.ToInt32Checked());
      }
      EInteger epowprec = EInteger.One;
      EInteger ret = EInteger.One;
      while (diff.Sign > 0) {
        if (!diff.IsEven) {
          EInteger otherPower = ValuePowerOfFiveCache.GetCachedPower(epowprec);
          if (otherPower == null) {
            // NOTE: Assumes powprec is 2 or greater and is a power of 2
            EInteger prevPower = FindPowerOfFiveFromBig(epowprec.ShiftRight(
                  1));
            otherPower = prevPower.Multiply(prevPower);
            ValuePowerOfFiveCache.AddPower(epowprec, otherPower);
          }
          ret = ret.Multiply(otherPower);
        }
        epowprec = epowprec.ShiftLeft(1);
        diff = diff.ShiftRight(1);
      }
      return ret;
    }

    internal static EInteger FindPowerOfTenFromBig(EInteger
      bigintExponent) {
      int sign = bigintExponent.Sign;
      if (sign < 0) {
        return EInteger.Zero;
      }
      if (sign == 0) {
        return EInteger.One;
      }
      return bigintExponent.CanFitInInt32() ?
        FindPowerOfTen(bigintExponent.ToInt32Checked()) :
        FindPowerOfFiveFromBig(bigintExponent).ShiftLeft(bigintExponent);
    }

    private static readonly EInteger ValueFivePower40 =
      ((EInteger)95367431640625L) * (EInteger)95367431640625L;

    internal static EInteger FindPowerOfFive(int precision) {
      if (precision < 0) {
        return EInteger.Zero;
      }
      if (precision <= 27) {
        return ValueBigIntPowersOfFive[(int)precision];
      }
      EInteger bigpow;
      EInteger ret;
      if (precision == 40) {
        return ValueFivePower40;
      }
      int startPrecision = precision;
      bigpow = ValuePowerOfFiveCache.GetCachedPowerInt(precision);
      if (bigpow != null) {
        return bigpow;
      }
      var origPrecision = (EInteger)precision;
      // DebugUtility.Log("Getting power of five "+precision);
      if (precision <= 54) {
        if ((precision & 1) == 0) {
          ret = ValueBigIntPowersOfFive[(int)(precision >> 1)];
          ret *= (EInteger)ret;
          ValuePowerOfFiveCache.AddPower(origPrecision, ret);
          return ret;
        }
        ret = ValueBigIntPowersOfFive[27];
        bigpow = ValueBigIntPowersOfFive[((int)precision) - 27];
        ret *= (EInteger)bigpow;
        ValuePowerOfFiveCache.AddPower(origPrecision, ret);
        return ret;
      } else if (precision <= 94) {
        ret = ValueFivePower40;
        bigpow = FindPowerOfFive(precision - 40);
        ret *= (EInteger)bigpow;
        ValuePowerOfFiveCache.AddPower(origPrecision, ret);
        return ret;
      }
      var powprec = 64;
      // Console.WriteLine("pow="+(precision&63)+",precision="+precision);
      ret = FindPowerOfFive(precision & 63);
      precision >>= 6;
      while (precision > 0) {
        if ((precision & 1) == 1) {
          EInteger otherPower =
            ValuePowerOfFiveCache.GetCachedPowerInt(powprec);
          // Console.WriteLine("pow="+powprec+",precision="+precision);
          if (otherPower == null) {
            // NOTE: Assumes powprec is 2 or greater and is a power of 2
            EInteger prevPower = FindPowerOfFive(powprec >> 1);
            otherPower = prevPower.Multiply(prevPower);
            ValuePowerOfFiveCache.AddPower(powprec, otherPower);
          }
          ret = ret.Multiply(otherPower);
        }
        powprec = unchecked(powprec << 1);
        precision >>= 1;
      }
      return ret;
    }

    internal static EInteger FindPowerOfTen(int precision) {
      if (precision < 0) {
        return EInteger.Zero;
      }
      if (precision <= 18) {
        return ValueBigIntPowersOfTen[(int)precision];
      }
      EInteger bigpow;
      EInteger ret;
      int startPrecision = precision;
      bigpow = ValuePowerOfTenCache.GetCachedPowerInt(precision);
      if (bigpow != null) {
        return bigpow;
      }
      // Console.WriteLine("power="+precision);
      if (precision <= 27) {
        ret = ValueBigIntPowersOfFive[precision];
        ret <<= precision;
        ValuePowerOfTenCache.AddPower(precision, ret);
        return ret;
      } else if (precision <= 36) {
        if ((precision & 1) == 0) {
          ret = ValueBigIntPowersOfTen[(int)(precision >> 1)];
          ret *= (EInteger)ret;
          ValuePowerOfTenCache.AddPower(precision, ret);
          return ret;
        }
        ret = ValueBigIntPowersOfTen[18];
        bigpow = ValueBigIntPowersOfTen[((int)precision) - 18];
        ret *= (EInteger)bigpow;
        ValuePowerOfTenCache.AddPower(precision, ret);
        return ret;
      }
      return FindPowerOfFive(precision).ShiftLeft(precision);
    }

    public static int BitLength(long mantlong) {
      #if DEBUG
      if (mantlong < 0) {
        throw new ArgumentException("mantlong (" + mantlong +
          ") is not greater or equal" + "\u0020to 0");
      }
      #endif
      if (mantlong == 0) {
        return 1;
      }
      var wcextra = 64;
      if ((mantlong >> 32) == 0) {
        mantlong <<= 32;
        wcextra -= 32;
      }
      if ((mantlong >> 48) == 0) {
        mantlong <<= 16;
        wcextra -= 16;
      }
      if ((mantlong >> 56) == 0) {
        mantlong <<= 8;
        wcextra -= 8;
      }
      if ((mantlong >> 60) == 0) {
        mantlong <<= 4;
        wcextra -= 4;
      }
      if ((mantlong >> 62) == 0) {
        mantlong <<= 2;
        wcextra -= 2;
      }
      return ((mantlong >> 63) == 0) ? wcextra - 1 : wcextra;
    }

    public static THelper PreRound<THelper>(
      THelper val,
      EContext ctx,
      IRadixMath<THelper> wrapper) {
      if (ctx == null || !ctx.HasMaxPrecision) {
        return val;
      }
      IRadixMathHelper<THelper> helper = wrapper.GetHelper();
      int thisFlags = helper.GetFlags(val);
      if ((thisFlags & BigNumberFlags.FlagSpecial) != 0) {
        // Infinity or NaN
        return val;
      }
      FastInteger fastPrecision = FastInteger.FromBig(ctx.Precision);
      EInteger mant = helper.GetMantissa(val).Abs();
      // Rounding is only to be done if the digit count is
      // too big (distinguishing this case is material
      // if the value also has an exponent that's out of range)
      FastInteger[] digitBounds = NumberUtility.DigitLengthBounds(
          helper,
          mant);
      if (digitBounds[1].CompareTo(fastPrecision) <= 0) {
        // Upper bound is less than or equal to precision
        return val;
      }
      EContext ctx2 = ctx;
      if (digitBounds[0].CompareTo(fastPrecision) <= 0) {
        // Lower bound is less than or equal to precision, so
        // calculate digit length more precisely
        FastInteger digits = helper.GetDigitLength(mant);
        ctx2 = ctx.WithBlankFlags().WithTraps(0);
        if (digits.CompareTo(fastPrecision) <= 0) {
          return val;
        }
      }
      val = wrapper.RoundToPrecision(val, ctx2);
      // the only time rounding can signal an invalid
      // operation is if an operand is a signaling NaN, but
      // this was already checked beforehand
      #if DEBUG
      if ((ctx2.Flags & EContext.FlagInvalid) != 0) {
        throw new ArgumentException("doesn't" +
          "\u0020satisfy(ctx2.Flags&FlagInvalid)==0");
      }
      #endif
      if ((ctx2.Flags & EContext.FlagInexact) != 0) {
        if (ctx.HasFlags) {
          ctx.Flags |= BigNumberFlags.LostDigitsFlags;
        }
      }
      if ((ctx2.Flags & EContext.FlagRounded) != 0) {
        if (ctx.HasFlags) {
          ctx.Flags |= EContext.FlagRounded;
        }
      }
      if ((ctx2.Flags & EContext.FlagOverflow) != 0) {
        bool neg = (thisFlags & BigNumberFlags.FlagNegative) != 0;
        if (ctx.HasFlags) {
          ctx.Flags |= EContext.FlagLostDigits;
          ctx.Flags |= EContext.FlagOverflow |
            EContext.FlagInexact | EContext.FlagRounded;
        }
      }
      return val;
    }

    public static FastInteger[] DigitLengthBounds<THelper>(
      IRadixMathHelper<THelper> helper,
      EInteger ei) {
      int radix = helper.GetRadix();
      if (radix == 2) {
        FastInteger fi =
          FastInteger.FromBig(ei.GetUnsignedBitLengthAsEInteger());
        return new FastInteger[] { fi, fi };
      } else if (radix == 10) {
        EInteger bigBitLength = ei.GetUnsignedBitLengthAsEInteger();
        if (bigBitLength.CompareTo(33) < 0) {
          // Can easily be calculated without estimation
          FastInteger fi = helper.GetDigitLength(ei);
          return new FastInteger[] { fi, fi };
        } else if (bigBitLength.CompareTo(2135) <= 0) {
          int ov = 1 + ((bigBitLength.ToInt32Checked() * 631305) >> 21);
          return new FastInteger[] {
            new FastInteger(ov - 2), // lower bound
            new FastInteger(ov), // upper bound
          };
        } else {
          // Bit length is big enough that these bounds will
          // overestimate or underestimate the true base-10 digit length
          // as appropriate.
          EInteger lowerBound = bigBitLength.Multiply(100).Divide(335);
          EInteger upperBound = bigBitLength.Divide(3);
          return new FastInteger[] {
            FastInteger.FromBig(lowerBound), // lower bound
            FastInteger.FromBig(upperBound), // upper bound
          };
        }
      } else {
        FastInteger fi = helper.GetDigitLength(ei);
        return new FastInteger[] { fi, fi };
      }
    }

    public static FastInteger DigitLengthUpperBound<THelper>(
      IRadixMathHelper<THelper> helper,
      EInteger ei) {
      int radix = helper.GetRadix();
      if (radix == 2) {
        return FastInteger.FromBig(ei.GetUnsignedBitLengthAsEInteger());
      } else if (radix == 10) {
        EInteger bigBitLength = ei.GetUnsignedBitLengthAsEInteger();
        if (bigBitLength.CompareTo(2135) <= 0) {
          // May overestimate by 1
          return new FastInteger(1 + ((bigBitLength.ToInt32Checked() *
                  631305) >> 21));
        } else {
          // Bit length is big enough that dividing it by 3 will not
          // underestimate the true base-10 digit length.
          return FastInteger.FromBig(bigBitLength.Divide(3));
        }
      } else {
        return helper.GetDigitLength(ei);
      }
    }

    public static EInteger ReduceTrailingZeros(
      EInteger bigmant,
      FastInteger exponentMutable,
      int radix,
      FastInteger digits,
      FastInteger precision,
      FastInteger idealExp) {
      #if DEBUG
      if (precision != null && digits == null) {
        throw new ArgumentException("doesn't satisfy precision==null ||" +
          "\u0020digits!=null");
      }
      #endif
      /*
      #if DEBUG
      if (!(bigmant.Sign >= 0)) {
        throw new ArgumentException("doesn't satisfy bigmant.Sign >= 0");
      }
      #endif

      */ if (bigmant.IsZero) {
        exponentMutable.SetInt(0);
        return bigmant;
      }
      if (radix == 2) {
        if (!bigmant.IsEven) {
          return bigmant;
        }
        long lowbit = bigmant.GetLowBitAsInt64();
        if (lowbit != Int64.MaxValue) {
          if (precision != null && digits.CompareTo(precision) >= 0) {
            // Limit by digits minus precision
            EInteger tmp = digits.AsEInteger().Subtract(precision.AsEInteger());
            if (tmp.CompareTo(EInteger.FromInt64(lowbit)) < 0) {
              lowbit = tmp.ToInt64Checked();
            }
          }
          if (idealExp != null && exponentMutable.CompareTo(idealExp) <= 0) {
            // Limit by idealExp minus exponentMutable
            EInteger tmp =
              idealExp.AsEInteger().Subtract(exponentMutable.AsEInteger());
            if (tmp.CompareTo(EInteger.FromInt64(lowbit)) < 0) {
              lowbit = tmp.ToInt64Checked();
            }
          }
          bigmant = (lowbit <= Int32.MaxValue) ?
            bigmant.ShiftRight((int)lowbit) :
            bigmant.ShiftRight(EInteger.FromInt64(lowbit));
          if (digits != null) {
            digits.SubtractInt64(lowbit);
          }
          if (exponentMutable != null) {
            exponentMutable.AddInt64(lowbit);
          }
          return bigmant;
        }
      }
      var bigradix = (EInteger)radix;
      var bitsToShift = new FastInteger(0);
      while (!bigmant.IsZero) {
        if (precision != null && digits.CompareTo(precision) == 0) {
          break;
        }
        if (idealExp != null && exponentMutable.CompareTo(idealExp) == 0) {
          break;
        }
        EInteger bigrem;
        EInteger bigquo;
        EInteger[] divrem = bigmant.DivRem(bigradix);
        bigquo = divrem[0];
        bigrem = divrem[1];
        if (!bigrem.IsZero) {
          break;
        }
        bigmant = bigquo;
        exponentMutable.Increment();
        if (digits != null) {
          digits.Decrement();
        }
      }
      return bigmant;
    }
  }
}
