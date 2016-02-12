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
    private static decimal EncodeDecimal(
EInteger bigmant,
int scale,
bool neg) {
      if (scale < 0) {
        throw new ArgumentException(
"scale (" + scale + ") is less than 0");
      }
      if (scale > 28) {
        throw new ArgumentException(
"scale (" + scale + ") is more than " + "28");
      }
      var data = bigmant.ToBytes(true);
      var a = 0;
      var b = 0;
      var c = 0;
      for (var i = 0; i < Math.Min(4, data.Length); ++i) {
        a |= (((int)data[i]) & 0xff) << (i * 8);
      }
      for (int i = 4; i < Math.Min(8, data.Length); ++i) {
        b |= (((int)data[i]) & 0xff) << ((i - 4) * 8);
      }
      for (int i = 8; i < Math.Min(12, data.Length); ++i) {
        c |= (((int)data[i]) & 0xff) << ((i - 8) * 8);
      }
      int d = scale << 16;
      if (neg) {
        d |= 1 << 31;
      }
      return new Decimal(new[] { a, b, c, d });
    }

    private static decimal ExtendedDecimalToDecimal(EDecimal
      extendedNumber) {
      if (extendedNumber.IsInfinity() || extendedNumber.IsNaN()) {
        throw new OverflowException("This object's value is out of range");
      }
      try {
        var newDecimal = extendedNumber.RoundToPrecision(
          EContext.CliDecimal.WithTraps(EContext.FlagOverflow));
        return EncodeDecimal(
newDecimal.Mantissa.Abs(),
-((int)newDecimal.Exponent),
newDecimal.Mantissa.Sign < 0);
      } catch (ETrapException ex) {
        throw new OverflowException("This object's value is out of range", ex);
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.Decimal)~PeterO.Numbers.EDecimal"]/*'/>
    public static implicit operator EDecimal(decimal dec) {
      var bits = Decimal.GetBits(dec);
      int scale = (bits[3] >> 16) & 0xff;
      var data = new byte[13];
      data[0] = (byte)(bits[0] & 0xff);
      data[1] = (byte)((bits[0] >> 8) & 0xff);
      data[2] = (byte)((bits[0] >> 16) & 0xff);
      data[3] = (byte)((bits[0] >> 24) & 0xff);
      data[4] = (byte)(bits[1] & 0xff);
      data[5] = (byte)((bits[1] >> 8) & 0xff);
      data[6] = (byte)((bits[1] >> 16) & 0xff);
      data[7] = (byte)((bits[1] >> 24) & 0xff);
      data[8] = (byte)(bits[2] & 0xff);
      data[9] = (byte)((bits[2] >> 8) & 0xff);
      data[10] = (byte)((bits[2] >> 16) & 0xff);
      data[11] = (byte)((bits[2] >> 24) & 0xff);
      data[12] = 0;
      var mantissa = EInteger.FromBytes(data, true);
      bool negative = (bits[3] >> 31) != 0;
      if (negative) {
        mantissa = -mantissa;
      }
      return Create(mantissa, (EInteger)(-scale));
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.FromDecimal(System.Decimal)"]/*'/>
    public static EDecimal FromDecimal(decimal dec) {
      return (EDecimal)dec;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Decimal"]/*'/>
    public static explicit operator decimal(EDecimal bigValue) {
      return ExtendedDecimalToDecimal(bigValue);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.Int64)~PeterO.Numbers.EDecimal"]/*'/>
    public static implicit operator EDecimal(long bigValue) {
      return FromInt64(bigValue);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.Int32)~PeterO.Numbers.EDecimal"]/*'/>
    public static implicit operator EDecimal(int smallValue) {
      return FromInt32(smallValue);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(PeterO.Numbers.EInteger)~PeterO.Numbers.EDecimal"]/*'/>
    public static implicit operator EDecimal(EInteger eint) {
      return FromEInteger(eint);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Addition(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal operator +(EDecimal bthis, EDecimal augend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Add(augend);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Subtraction(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal operator -(
   EDecimal bthis,
   EDecimal subtrahend) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Subtract(subtrahend);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Multiply(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal operator *(
    EDecimal operand1,
    EDecimal operand2) {
      if (operand1 == null) {
        throw new ArgumentNullException("operand1");
      }
      return operand1.Multiply(operand2);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Division(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal operator /(
   EDecimal dividend,
   EDecimal divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Divide(divisor);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Modulus(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal operator %(
   EDecimal dividend,
   EDecimal divisor) {
      if (dividend == null) {
        throw new ArgumentNullException("dividend");
      }
      return dividend.Remainder(divisor, null);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_UnaryNegation(PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal operator -(EDecimal bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return bigValue.Negate();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Int64"]/*'/>
    public static explicit operator long (EDecimal bigValue) {
      if (bigValue == null) {
        throw new ArgumentNullException("bigValue");
      }
      return (long)bigValue.ToEInteger();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~PeterO.Numbers.EInteger"]/*'/>
    public static explicit operator EInteger(EDecimal bigValue) {
      return bigValue.ToEInteger();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Double"]/*'/>
    public static explicit operator double (EDecimal bigValue) {
      return bigValue.ToDouble();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Single"]/*'/>
    public static explicit operator float (EDecimal bigValue) {
      return bigValue.ToSingle();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Int32"]/*'/>
    public static explicit operator int (EDecimal bigValue) {
      return (int)bigValue.ToEInteger();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Int16"]/*'/>
    public static explicit operator short (EDecimal bigValue) {
      return (short)(int)bigValue;
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Byte"]/*'/>
    public static explicit operator byte (EDecimal bigValue) {
      return (byte)(int)bigValue;
    }
  }
}
