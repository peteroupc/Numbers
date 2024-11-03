/*
Written by Peter O.
Any copyright to this work is released to the Public Domain.
In case this is not possible, this work is also
licensed under the Unlicense: https://unlicense.org/

 */
using System;

namespace PeterO.Numbers {
  internal interface IRadixMathHelper<T> {
    int GetRadix();

    int GetArithmeticSupport();

    int GetSign(T value);

    int GetFlags(T value);

    EInteger GetMantissa(T value);

    EInteger GetExponent(T value);

    FastIntegerFixed GetMantissaFastInt(T value);

    FastIntegerFixed GetExponentFastInt(T value);

    T ValueOf(int val);

    T CreateNewWithFlags(EInteger mantissa, EInteger exponent, int flags);

    T CreateNewWithFlagsFastInt(
      FastIntegerFixed mantissa,
      FastIntegerFixed exponent,
      int flags);

    IShiftAccumulator CreateShiftAccumulatorWithDigits(
      EInteger value,
      int lastDigit,
      int olderDigits);

    IShiftAccumulator CreateShiftAccumulatorWithDigitsFastInt(
      FastIntegerFixed value,
      int lastDigit,
      int olderDigits);

    FastInteger DivisionShift(EInteger num, EInteger den);

    FastInteger GetDigitLength(EInteger ei);

    EInteger MultiplyByRadixPower(EInteger value, FastInteger power);

    FastIntegerFixed MultiplyByRadixPowerFastInt(
      FastIntegerFixed value,
      FastIntegerFixed power);
  }
}
