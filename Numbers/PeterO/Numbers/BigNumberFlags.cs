/*
Written by Peter O.
Any copyright to this work is released to the Public Domain.
In case this is not possible, this work is also
licensed under Creative Commons Zero (CC0):
https://creativecommons.org/publicdomain/zero/1.0/

 */
using System;

namespace PeterO.Numbers {
  internal static class BigNumberFlags {
    internal const int FlagNegative = 1;
    internal const int FlagQuietNaN = 4;
    internal const int FlagSignalingNaN = 8;
    internal const int FlagInfinity = 2;
    internal const int FlagSpecial = FlagQuietNaN | FlagSignalingNaN |
      FlagInfinity;
    internal const int FlagNaN = FlagQuietNaN | FlagSignalingNaN;
    internal const int LostDigitsFlags = EContext.FlagLostDigits |
      EContext.FlagInexact | EContext.FlagRounded;
    internal const int FiniteOnly = 0;
    internal const int FiniteAndNonFinite = 1;
  }
}
