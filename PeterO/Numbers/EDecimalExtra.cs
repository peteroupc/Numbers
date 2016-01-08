/*
Written in 2013 by Peter O.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://upokecenter.dreamhosters.com/articles/donate-now-2/
 */
using System;

namespace PeterO.Numbers {
  public sealed partial class EDecimal {
    // TODO: Add .NET decimal operators
    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>Not documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    public static implicit operator EDecimal(long bigValue) {
      return FromInt64(bigValue);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='smallValue'>Not documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    public static implicit operator EDecimal(int smallValue) {
      return FromInt32(smallValue);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='eint'>Not documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    public static implicit operator EDecimal(EInteger eint) {
      return FromBigInteger(eint);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bthis'>Not documented yet.</param>
    /// <param name='augend'>Not documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='bthis'/> is null.</exception>
    public static EDecimal operator +(EDecimal bthis, EDecimal augend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Add(augend);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bthis'>Not documented yet.</param>
    /// <param name='subtrahend'>Not documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='bthis'/> is null.</exception>
    public static EDecimal operator -(
   EDecimal bthis,
   EDecimal subtrahend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Subtract(subtrahend);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='operand1'>Not documented yet.</param>
    /// <param name='operand2'>Not documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='operand1'/> is null.</exception>
    public static EDecimal operator *(
    EDecimal operand1,
    EDecimal operand2) {
      if (operand1 == null) {
        throw new ArgumentNullException("operand1");
      }
      return operand1.Multiply(operand2);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='dividend'>Not documented yet.</param>
    /// <param name='divisor'>Not documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='dividend'/> is null.</exception>
    public static EDecimal operator /(
   EDecimal dividend,
   EDecimal divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Divide(divisor);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='dividend'>Not documented yet.</param>
    /// <param name='divisor'>Not documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='dividend'/> is null.</exception>
    public static EDecimal operator %(
   EDecimal dividend,
   EDecimal divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Remainder(divisor, null);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>Not documented yet.</param>
    /// <returns>An EDecimal object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='bigValue'/> is null.</exception>
    public static EDecimal operator -(EDecimal bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return bigValue.Negate();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>Not documented yet.</param>
    /// <returns>A 64-bit signed integer.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='bigValue'/> is null.</exception>
    public static explicit operator long (EDecimal bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return (long)bigValue.ToBigInteger();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>Not documented yet.</param>
    /// <returns>An EInteger object.</returns>
    public static explicit operator EInteger(EDecimal bigValue) {
      return bigValue.ToBigInteger();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>Not documented yet.</param>
    /// <returns>A 64-bit floating-point number.</returns>
    public static explicit operator double (EDecimal bigValue) {
      return bigValue.ToDouble();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>Not documented yet.</param>
    /// <returns>A 32-bit floating-point number.</returns>
    public static explicit operator float (EDecimal bigValue) {
      return bigValue.ToSingle();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>Not documented yet.</param>
    /// <returns>A 32-bit signed integer.</returns>
    public static explicit operator int (EDecimal bigValue) {
      return (int)bigValue.ToBigInteger();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>Not documented yet.</param>
    /// <returns>A 16-bit signed integer.</returns>
    public static explicit operator short (EDecimal bigValue) {
      return (short)(int)bigValue;
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>Not documented yet.</param>
    /// <returns>A Byte object.</returns>
    public static explicit operator byte (EDecimal bigValue) {
      return (byte)(int)bigValue;
    }
  }
}
