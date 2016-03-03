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

    private decimal ToDecimal() {
      EDecimal extendedNumber = this;
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
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.FromDecimal(System.Decimal)"]/*'/>
    public static EDecimal FromDecimal(decimal dec) {
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
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.Decimal)~PeterO.Numbers.EDecimal"]/*'/>
    public static implicit operator EDecimal(decimal dec) {
      return FromDecimal(dec);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Decimal"]/*'/>
    public static explicit operator decimal(EDecimal bigValue) {
      return bigValue.ToDecimal();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(PeterO.Numbers.EInteger)~PeterO.Numbers.EDecimal"]/*'/>
    public static implicit operator EDecimal(EInteger eint) {
      return FromEInteger(eint);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Addition(PeterO.Numbers.EDecimal,PeterO.Numbers.EDecimal)"]/*'/>
    public static EDecimal operator +(EDecimal bthis, EDecimal otherValue) {
      if (bthis == null) {
        throw new ArgumentNullException("bthis");
      }
      return bthis.Add(otherValue);
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
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~PeterO.Numbers.EInteger"]/*'/>
    public static explicit operator EInteger(EDecimal bigValue) {
      return bigValue.ToEInteger();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Double"]/*'/>
    public static explicit operator double(EDecimal bigValue) {
      return bigValue.ToDouble();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Single"]/*'/>
    public static explicit operator float(EDecimal bigValue) {
      return bigValue.ToSingle();
    }

    // Begin integer conversions

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Byte"]/*'/>
public static explicit operator byte(EDecimal input) {
 return input.ToByteChecked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.Byte)~PeterO.Numbers.EDecimal"]/*'/>
public static implicit operator EDecimal(byte inputByte) {
 return EDecimal.FromByte(inputByte);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToSByteChecked"]/*'/>
[CLSCompliant(false)]
public sbyte ToSByteChecked() {
 if (!this.IsFinite) {
 throw new OverflowException("Value is infinity or NaN");
}
if (this.IsIntegerPartZero()) {
 return (sbyte)0;
}
if (this.exponent.CompareToInt(3) >= 0) {
throw new OverflowException("Value out of range: ");
}
 return this.ToEInteger().ToSByteChecked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToSByteUnchecked"]/*'/>
[CLSCompliant(false)]
public sbyte ToSByteUnchecked() {
 return this.IsFinite ? this.ToEInteger().ToSByteUnchecked() : (sbyte)0;
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToSByteIfExact"]/*'/>
[CLSCompliant(false)]
public sbyte ToSByteIfExact() {
 if (!this.IsFinite) {
 throw new OverflowException("Value is infinity or NaN");
}
 if (this.IsZero) {
 return (sbyte)0;
}
if (this.exponent.CompareToInt(3) >= 0) {
throw new OverflowException("Value out of range");
}
 return this.ToEIntegerIfExact().ToSByteChecked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.FromSByte(System.SByte)"]/*'/>
[CLSCompliant(false)]
public static EDecimal FromSByte(sbyte inputSByte) {
 var val = (int)inputSByte;
 return FromInt32(val);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.SByte"]/*'/>
[CLSCompliant(false)]
public static explicit operator sbyte(EDecimal input) {
 return input.ToSByteChecked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.SByte)~PeterO.Numbers.EDecimal"]/*'/>
[CLSCompliant(false)]
public static implicit operator EDecimal(sbyte inputSByte) {
 return EDecimal.FromSByte(inputSByte);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Int16"]/*'/>
public static explicit operator short(EDecimal input) {
 return input.ToInt16Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.Int16)~PeterO.Numbers.EDecimal"]/*'/>
public static implicit operator EDecimal(short inputInt16) {
 return EDecimal.FromInt16(inputInt16);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToUInt16Checked"]/*'/>
[CLSCompliant(false)]
public ushort ToUInt16Checked() {
 if (!this.IsFinite) {
 throw new OverflowException("Value is infinity or NaN");
}
if (this.IsIntegerPartZero()) {
 return (ushort)0;
}
if (this.IsNegative) {
 throw new OverflowException("Value out of range");
}
if (this.exponent.CompareToInt(5) >= 0) {
throw new OverflowException("Value out of range: ");
}
 return this.ToEInteger().ToUInt16Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToUInt16Unchecked"]/*'/>
[CLSCompliant(false)]
public ushort ToUInt16Unchecked() {
 return this.IsFinite ? this.ToEInteger().ToUInt16Unchecked() : (ushort)0;
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToUInt16IfExact"]/*'/>
[CLSCompliant(false)]
public ushort ToUInt16IfExact() {
 if (!this.IsFinite) {
 throw new OverflowException("Value is infinity or NaN");
}
 if (this.IsZero) {
 return (ushort)0;
}
 if (this.IsNegative) {
throw new OverflowException("Value out of range");
}
if (this.exponent.CompareToInt(5) >= 0) {
throw new OverflowException("Value out of range");
}
 return this.ToEIntegerIfExact().ToUInt16Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.FromUInt16(System.UInt16)"]/*'/>
[CLSCompliant(false)]
public static EDecimal FromUInt16(ushort inputUInt16) {
 int val = ((int)inputUInt16) & 0xffff;
 return FromInt32(val);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.UInt16"]/*'/>
[CLSCompliant(false)]
public static explicit operator ushort(EDecimal input) {
 return input.ToUInt16Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.UInt16)~PeterO.Numbers.EDecimal"]/*'/>
[CLSCompliant(false)]
public static implicit operator EDecimal(ushort inputUInt16) {
 return EDecimal.FromUInt16(inputUInt16);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Int32"]/*'/>
public static explicit operator int(EDecimal input) {
 return input.ToInt32Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.Int32)~PeterO.Numbers.EDecimal"]/*'/>
public static implicit operator EDecimal(int inputInt32) {
 return EDecimal.FromInt32(inputInt32);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToUInt32Checked"]/*'/>
[CLSCompliant(false)]
public uint ToUInt32Checked() {
 if (!this.IsFinite) {
 throw new OverflowException("Value is infinity or NaN");
}
if (this.IsIntegerPartZero()) {
 return (uint)0;
}
if (this.IsNegative) {
 throw new OverflowException("Value out of range");
}
if (this.exponent.CompareToInt(10) >= 0) {
throw new OverflowException("Value out of range: ");
}
 return this.ToEInteger().ToUInt32Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToUInt32Unchecked"]/*'/>
[CLSCompliant(false)]
public uint ToUInt32Unchecked() {
 return this.IsFinite ? this.ToEInteger().ToUInt32Unchecked() : (uint)0;
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToUInt32IfExact"]/*'/>
[CLSCompliant(false)]
public uint ToUInt32IfExact() {
 if (!this.IsFinite) {
 throw new OverflowException("Value is infinity or NaN");
}
 if (this.IsZero) {
 return (uint)0;
}
 if (this.IsNegative) {
throw new OverflowException("Value out of range");
}
if (this.exponent.CompareToInt(10) >= 0) {
throw new OverflowException("Value out of range");
}
 return this.ToEIntegerIfExact().ToUInt32Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.FromUInt32(System.UInt32)"]/*'/>
[CLSCompliant(false)]
public static EDecimal FromUInt32(uint inputUInt32) {
 long val = ((long)inputUInt32) & 0xffffffffL;
 return FromInt64(val);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.UInt32"]/*'/>
[CLSCompliant(false)]
public static explicit operator uint(EDecimal input) {
 return input.ToUInt32Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.UInt32)~PeterO.Numbers.EDecimal"]/*'/>
[CLSCompliant(false)]
public static implicit operator EDecimal(uint inputUInt32) {
 return EDecimal.FromUInt32(inputUInt32);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.Int64"]/*'/>
public static explicit operator long(EDecimal input) {
 return input.ToInt64Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.Int64)~PeterO.Numbers.EDecimal"]/*'/>
public static implicit operator EDecimal(long inputInt64) {
 return EDecimal.FromInt64(inputInt64);
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToUInt64Checked"]/*'/>
[CLSCompliant(false)]
public ulong ToUInt64Checked() {
 if (!this.IsFinite) {
 throw new OverflowException("Value is infinity or NaN");
}
if (this.IsIntegerPartZero()) {
 return (ulong)0;
}
if (this.IsNegative) {
 throw new OverflowException("Value out of range");
}
if (this.exponent.CompareToInt(20) >= 0) {
throw new OverflowException("Value out of range: ");
}
 return this.ToEInteger().ToUInt64Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToUInt64Unchecked"]/*'/>
[CLSCompliant(false)]
public ulong ToUInt64Unchecked() {
 return this.IsFinite ? this.ToEInteger().ToUInt64Unchecked() : (ulong)0;
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.ToUInt64IfExact"]/*'/>
[CLSCompliant(false)]
public ulong ToUInt64IfExact() {
 if (!this.IsFinite) {
 throw new OverflowException("Value is infinity or NaN");
}
 if (this.IsZero) {
 return (ulong)0;
}
 if (this.IsNegative) {
throw new OverflowException("Value out of range");
}
if (this.exponent.CompareToInt(20) >= 0) {
throw new OverflowException("Value out of range");
}
 return this.ToEIntegerIfExact().ToUInt64Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.FromUInt64(System.UInt64)"]/*'/>
[CLSCompliant(false)]
public static EDecimal FromUInt64(ulong inputUInt64) {
 return FromEInteger(EInteger.FromUInt64(inputUInt64));
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Explicit(PeterO.Numbers.EDecimal)~System.UInt64"]/*'/>
[CLSCompliant(false)]
public static explicit operator ulong(EDecimal input) {
 return input.ToUInt64Checked();
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EDecimal.op_Implicit(System.UInt64)~PeterO.Numbers.EDecimal"]/*'/>
[CLSCompliant(false)]
public static implicit operator EDecimal(ulong inputUInt64) {
 return EDecimal.FromUInt64(inputUInt64);
}

// End integer conversions
  }
}
