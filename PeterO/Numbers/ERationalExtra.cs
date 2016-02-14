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
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Implicit(System.Int64)~PeterO.Numbers.ERational"]/*'/>
    public static implicit operator ERational(long bigValue) {
      return FromInt64(bigValue);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Implicit(System.Int32)~PeterO.Numbers.ERational"]/*'/>
    public static implicit operator ERational(int smallValue) {
      return FromInt32(smallValue);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Implicit(PeterO.Numbers.EInteger)~PeterO.Numbers.ERational"]/*'/>
    public static implicit operator ERational(EInteger eint) {
      return FromEInteger(eint);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Implicit(PeterO.Numbers.EDecimal)~PeterO.Numbers.ERational"]/*'/>
    public static implicit operator ERational(EDecimal eint) {
      return FromEDecimal(eint);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Implicit(PeterO.Numbers.EFloat)~PeterO.Numbers.ERational"]/*'/>
    public static implicit operator ERational(EFloat eint) {
      return FromEFloat(eint);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Implicit(System.Decimal)~PeterO.Numbers.ERational"]/*'/>
    public static implicit operator ERational(decimal eint) {
      return FromEDecimal((EDecimal)eint);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Implicit(System.Single)~PeterO.Numbers.ERational"]/*'/>
    public static implicit operator ERational(float eint) {
      return ERational.FromSingle(eint);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Implicit(System.Double)~PeterO.Numbers.ERational"]/*'/>
    public static implicit operator ERational(double eint) {
      return ERational.FromDouble(eint);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Implicit(System.UInt64)~PeterO.Numbers.ERational"]/*'/>
    [CLSCompliant(false)]
    public static implicit operator ERational(ulong eint) {
      return FromEInteger((EInteger)eint);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Addition(PeterO.Numbers.ERational,PeterO.Numbers.ERational)"]/*'/>
    public static ERational operator +(ERational bthis, ERational augend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Add(augend);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Subtraction(PeterO.Numbers.ERational,PeterO.Numbers.ERational)"]/*'/>
    public static ERational operator -(
   ERational bthis,
   ERational subtrahend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Subtract(subtrahend);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Multiply(PeterO.Numbers.ERational,PeterO.Numbers.ERational)"]/*'/>
    public static ERational operator *(
    ERational operand1,
    ERational operand2) {
      if (operand1 == null) {
        throw new ArgumentNullException("operand1");
      }
      return operand1.Multiply(operand2);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Division(PeterO.Numbers.ERational,PeterO.Numbers.ERational)"]/*'/>
    public static ERational operator /(
   ERational dividend,
   ERational divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Divide(divisor);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Modulus(PeterO.Numbers.ERational,PeterO.Numbers.ERational)"]/*'/>
    public static ERational operator %(
   ERational dividend,
   ERational divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Remainder(divisor);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_UnaryNegation(PeterO.Numbers.ERational)"]/*'/>
    public static ERational operator -(ERational bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return bigValue.Negate();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~System.Decimal"]/*'/>
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

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~System.Int64"]/*'/>
    public static explicit operator long(ERational bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return (long)bigValue.ToEInteger();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~PeterO.Numbers.EInteger"]/*'/>
    public static explicit operator EInteger(ERational bigValue) {
      return bigValue.ToEInteger();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~System.Double"]/*'/>
    public static explicit operator double(ERational bigValue) {
      return bigValue.ToDouble();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~System.Single"]/*'/>
    public static explicit operator float(ERational bigValue) {
      return bigValue.ToSingle();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~System.Int32"]/*'/>
    public static explicit operator int(ERational bigValue) {
      return (int)bigValue.ToEInteger();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~System.UInt64"]/*'/>
    [CLSCompliant(false)]
    public static explicit operator ulong(ERational bigValue) {
      return (ulong)bigValue.ToEInteger();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~System.UInt32"]/*'/>
    [CLSCompliant(false)]
    public static explicit operator uint(ERational bigValue) {
      return (uint)bigValue.ToEInteger();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~System.Int16"]/*'/>
    public static explicit operator short(ERational bigValue) {
      return checked((short)(int)bigValue);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.ERational.op_Explicit(PeterO.Numbers.ERational)~System.Byte"]/*'/>
    public static explicit operator byte(ERational bigValue) {
      return checked((byte)(int)bigValue);
    }
  }
}
