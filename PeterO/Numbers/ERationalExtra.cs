/*
Written in 2013 by Peter O.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://upokecenter.dreamhosters.com/articles/donate-now-2/
 */
using System;

namespace PeterO.Numbers {
  public sealed partial class ERational {
    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>A 64-bit signed integer.</param>
    /// <returns>An ERational object.</returns>
    public static implicit operator ERational(long bigValue) {
      return FromInt64(bigValue);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='smallValue'>A 32-bit signed integer.</param>
    /// <returns>An ERational object.</returns>
    public static implicit operator ERational(int smallValue) {
      return FromInt32(smallValue);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='eint'>An EInteger object.</param>
    /// <returns>An ERational object.</returns>
    public static implicit operator ERational(EInteger eint) {
      return FromEInteger(eint);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='eint'>An EDecimal object.</param>
    /// <returns>An ERational object.</returns>
    public static implicit operator ERational(EDecimal eint) {
      return FromEDecimal(eint);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='eint'>An EFloat object.</param>
    /// <returns>An ERational object.</returns>
    public static implicit operator ERational(EFloat eint) {
      return FromEFloat(eint);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='eint'>A Decimal object.</param>
    /// <returns>An ERational object.</returns>
    public static implicit operator ERational(decimal eint) {
      return FromEDecimal((EDecimal)eint);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='eint'>A 32-bit floating-point number.</param>
    /// <returns>An ERational object.</returns>
    public static implicit operator ERational(float eint) {
      return ERational.FromSingle(eint);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='eint'>A 64-bit floating-point number.</param>
    /// <returns>An ERational object.</returns>
    public static implicit operator ERational(double eint) {
      return ERational.FromDouble(eint);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='eint'>A 64-bit unsigned integer.</param>
    /// <returns>An ERational object.</returns>
    [CLSCompliant(false)]
    public static implicit operator ERational(ulong eint) {
      return FromEInteger((EInteger)eint);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bthis'>An ERational object.</param>
    /// <param name='augend'>An ERational object. (3).</param>
    /// <returns>An ERational object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='bthis'/> is null.</exception>
    public static ERational operator +(ERational bthis, ERational augend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Add(augend);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bthis'>An ERational object.</param>
    /// <param name='subtrahend'>An ERational object. (3).</param>
    /// <returns>An ERational object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='bthis'/> is null.</exception>
    public static ERational operator -(
   ERational bthis,
   ERational subtrahend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Subtract(subtrahend);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='operand1'>An ERational object.</param>
    /// <param name='operand2'>An ERational object. (3).</param>
    /// <returns>An ERational object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='operand1'/> is null.</exception>
    public static ERational operator *(
    ERational operand1,
    ERational operand2) {
      if (operand1 == null) {
        throw new ArgumentNullException("operand1");
      }
      return operand1.Multiply(operand2);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='dividend'>An ERational object.</param>
    /// <param name='divisor'>An ERational object. (3).</param>
    /// <returns>An ERational object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='dividend'/> is null.</exception>
    public static ERational operator /(
   ERational dividend,
   ERational divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Divide(divisor);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='dividend'>An ERational object.</param>
    /// <param name='divisor'>An ERational object. (3).</param>
    /// <returns>An ERational object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='dividend'/> is null.</exception>
    public static ERational operator %(
   ERational dividend,
   ERational divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Remainder(divisor);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>An ERational object.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='bigValue'/> is null.</exception>
    public static ERational operator -(ERational bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return bigValue.Negate();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='extendedNumber'>An ERational object.</param>
    /// <returns>A Decimal object.</returns>
    public static explicit operator decimal(ERational
  extendedNumber) {
      if (extendedNumber.IsInfinity() || extendedNumber.IsNaN()) {
        throw new OverflowException("This object's value is out of range");
      }
      try {
        EDecimal newDecimal = EDecimal.FromEInteger(extendedNumber.Numerator)
          .Divide(
EDecimal.FromEInteger(extendedNumber.Denominator),
EContext.CliDecimal.WithTraps(EContext.FlagOverflow));
        return (decimal)newDecimal;
      } catch (ETrapException ex) {
        throw new OverflowException("This object's value is out of range", ex);
      }
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>A 64-bit signed integer.</returns>
    /// <exception cref='ArgumentNullException'>The parameter <paramref
    /// name='bigValue'/> is null.</exception>
    public static explicit operator long(ERational bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return (long)bigValue.ToEInteger();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>An EInteger object.</returns>
    public static explicit operator EInteger(ERational bigValue) {
      return bigValue.ToEInteger();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>A 64-bit floating-point number.</returns>
    public static explicit operator double(ERational bigValue) {
      return bigValue.ToDouble();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>A 32-bit floating-point number.</returns>
    public static explicit operator float(ERational bigValue) {
      return bigValue.ToSingle();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>A 32-bit signed integer.</returns>
    public static explicit operator int(ERational bigValue) {
      return (int)bigValue.ToEInteger();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>A 64-bit unsigned integer.</returns>
    [CLSCompliant(false)]
    public static explicit operator ulong(ERational bigValue) {
      return (ulong)bigValue.ToEInteger();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>A 32-bit unsigned integer.</returns>
    [CLSCompliant(false)]
    public static explicit operator uint(ERational bigValue) {
      return (uint)bigValue.ToEInteger();
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>A 16-bit signed integer.</returns>
    public static explicit operator short(ERational bigValue) {
      return checked((short)(int)bigValue);
    }

    /// <summary>Not documented yet.</summary>
    /// <param name='bigValue'>An ERational object.</param>
    /// <returns>A Byte object.</returns>
    public static explicit operator byte(ERational bigValue) {
      return checked((byte)(int)bigValue);
    }
  }
}
