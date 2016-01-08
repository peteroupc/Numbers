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
    // TODO: Add .NET decimal operators
    public static implicit operator ERational(long bigValue) {
      return FromInt64(bigValue);
    }

    public static implicit operator ERational(int smallValue) {
      return FromInt32(smallValue);
    }

    public static implicit operator ERational(EInteger eint) {
      return FromBigInteger(eint);
    }

    public static ERational operator +(ERational bthis, ERational augend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Add(augend);
    }

    public static ERational operator -(
   ERational bthis,
   ERational subtrahend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Subtract(subtrahend);
    }

    public static ERational operator *(
    ERational operand1,
    ERational operand2) {
      if (operand1 == null) {
        throw new ArgumentNullException("operand1");
      }
      return operand1.Multiply(operand2);
    }

    public static ERational operator /(
   ERational dividend,
   ERational divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Divide(divisor);
    }

    public static ERational operator %(
   ERational dividend,
   ERational divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Remainder(divisor);
    }

    public static ERational operator -(ERational bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return bigValue.Negate();
    }

    public static explicit operator long (ERational bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return (long)bigValue.ToBigInteger();
    }

    public static explicit operator EInteger(ERational bigValue) {
      return bigValue.ToBigInteger();
    }

    public static explicit operator double (ERational bigValue) {
      return bigValue.ToDouble();
    }

    public static explicit operator float (ERational bigValue) {
      return bigValue.ToSingle();
    }

    public static explicit operator int (ERational bigValue) {
      return (int)bigValue.ToBigInteger();
    }

    public static explicit operator short (ERational bigValue) {
      return (short)(int)bigValue;
    }

    public static explicit operator byte (ERational bigValue) {
      return (byte)(int)bigValue;
    }
  }
}
