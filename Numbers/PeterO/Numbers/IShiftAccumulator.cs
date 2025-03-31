/*
Written by Peter O.
Any copyright to this work is released to the Public Domain.
In case this is not possible, this work is also
licensed under the Unlicense: https://unlicense.org/

 */
using System;

namespace PeterO.Numbers {
  /// <summary>Common interface for classes that shift a number of digits
  /// and record information on whether a nonzero digit was discarded
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
