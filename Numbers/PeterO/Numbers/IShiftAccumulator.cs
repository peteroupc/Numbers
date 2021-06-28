/*
Written by Peter O.
Any copyright to this work is released to the Public Domain.
In case this is not possible, this work is also
licensed under Creative Commons Zero (CC0):
https://creativecommons.org/publicdomain/zero/1.0/

 */
using System;

namespace PeterO.Numbers {
  /// <summary>Common interface for classes that shift a number of digits
  /// and record information on whether a non-zero digit was discarded
  /// this way.</summary>
  internal interface IShiftAccumulator {
    EInteger ShiftedInt {
      get;
    }

    FastInteger GetDigitLength();

    FastInteger OverestimateDigitLength();

    int OlderDiscardedDigits {
      get;
    }

    int LastDiscardedDigit {
      get;
    }

    FastInteger ShiftedIntFast {
      get;
    }

    FastInteger DiscardedDigitCount {
      get;
    }

    void TruncateOrShiftRight(FastInteger bits, bool truncate);

    int ShiftedIntMod(int mod);

    void ShiftRightInt(int bits);

    void ShiftToDigits(FastInteger bits, FastInteger preShift, bool truncate);
  }
}
