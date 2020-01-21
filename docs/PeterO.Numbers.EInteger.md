## PeterO.Numbers.EInteger

    public sealed class EInteger :
        System.IComparable,
        System.IEquatable

Represents an arbitrary-precision integer. (The "E" stands for "extended", and has this prefix to group it with the other classes common to this library, particularly EDecimal, EFloat, and ERational.) Instances of this class are immutable, so they are inherently safe for use by multiple threads. Multiple instances of this object with the same value are interchangeable, but they should be compared using the "Equals" method rather than the "==" operator.

<b>Security note</b>

It is not recommended to implement security-sensitive algorithms using the methods in this class, for several reasons:

 *  `EInteger`  objects are immutable, so they can't be modified, and the memory they occupy is not guaranteed to be cleared in a timely fashion due to garbage collection. This is relevant for applications that use many-bit-long numbers as secret parameters.

 * The methods in this class (especially those that involve arithmetic) are not guaranteed to be "constant-time" (non-data-dependent) for all relevant inputs. Certain attacks that involve encrypted communications have exploited the timing and other aspects of such communications to derive keying material or cleartext indirectly.

Applications should instead use dedicated security libraries to handle big numbers in security-sensitive algorithms.

### Member Summary
* <code>[Abs()](#Abs)</code> - Returns the absolute value of this object's value.
* <code>[Add(int)](#Add_int)</code> - Adds this object and another object.
* <code>[Add(PeterO.Numbers.EInteger)](#Add_PeterO_Numbers_EInteger)</code> - Adds this object and another object.
* <code>[And(PeterO.Numbers.EInteger)](#And_PeterO_Numbers_EInteger)</code> - Does an AND operation between two arbitrary-precision integer values.
* <code>[And(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#And_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> - Does an AND operation between two arbitrary-precision integer values.
* <code>[AsInt32Checked()](#AsInt32Checked)</code> - <b>Deprecated:</b> Renamed to ToInt32Checked.
* <code>[AsInt32Unchecked()](#AsInt32Unchecked)</code> - <b>Deprecated:</b> Renamed to ToInt32Unchecked.
* <code>[AsInt64Checked()](#AsInt64Checked)</code> - <b>Deprecated:</b> Renamed to ToInt64Checked.
* <code>[AsInt64Unchecked()](#AsInt64Unchecked)</code> - <b>Deprecated:</b> Renamed to ToInt64Unchecked.
* <code>[CanFitInInt32()](#CanFitInInt32)</code> - Returns whether this object's value can fit in a 32-bit signed integer.
* <code>[CanFitInInt64()](#CanFitInInt64)</code> - Returns whether this object's value can fit in a 64-bit signed integer.
* <code>[CompareTo(int)](#CompareTo_int)</code> - Compares an arbitrary-precision integer with this instance.
* <code>[CompareTo(PeterO.Numbers.EInteger)](#CompareTo_PeterO_Numbers_EInteger)</code> - Compares an arbitrary-precision integer with this instance.
* <code>[Decrement()](#Decrement)</code> - Returns one subtracted from this arbitrary-precision integer.
* <code>[Divide(int)](#Divide_int)</code> - Divides this instance by the value of an arbitrary-precision integer.
* <code>[Divide(PeterO.Numbers.EInteger)](#Divide_PeterO_Numbers_EInteger)</code> - Divides this instance by the value of an arbitrary-precision integer.
* <code>[DivRem(PeterO.Numbers.EInteger)](#DivRem_PeterO_Numbers_EInteger)</code> - Divides this object by another arbitrary-precision integer and returns the quotient and remainder.
* <code>[DivRem(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger, PeterO.Numbers.EInteger&amp;)](#DivRem_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> - <b>Deprecated:</b> Use the DivRem instance method instead.
* <code>[Equals(object)](#Equals_object)</code> - Determines whether this object and another object are equal and have the same type.
* <code>[Equals(PeterO.Numbers.EInteger)](#Equals_PeterO_Numbers_EInteger)</code> - Determines whether this object and another object are equal.
* <code>[explicit operator byte(PeterO.Numbers.EInteger)](#explicit_operator_byte_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255).
* <code>[explicit operator int(PeterO.Numbers.EInteger)](#explicit_operator_int_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to a 32-bit signed integer if it can fit in a 32-bit signed integer.
* <code>[explicit operator long(PeterO.Numbers.EInteger)](#explicit_operator_long_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to a 64-bit signed integer if it can fit in a 64-bit signed integer.
* <code>[explicit operator PeterO.Numbers.EInteger(bool)](#explicit_operator_PeterO_Numbers_EInteger_bool)</code> - Converts a byte (from 0 to 255) to an arbitrary-precision integer.
* <code>[explicit operator sbyte(PeterO.Numbers.EInteger)](#explicit_operator_sbyte_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to an 8-bit signed integer if it can fit in an 8-bit signed integer.
* <code>[explicit operator short(PeterO.Numbers.EInteger)](#explicit_operator_short_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to a 16-bit signed integer if it can fit in a 16-bit signed integer.
* <code>[explicit operator uint(PeterO.Numbers.EInteger)](#explicit_operator_uint_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to a 32-bit signed integer if it can fit in a 32-bit signed integer.
* <code>[explicit operator ulong(PeterO.Numbers.EInteger)](#explicit_operator_ulong_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer.
* <code>[explicit operator ushort(PeterO.Numbers.EInteger)](#explicit_operator_ushort_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer.
* <code>[FromBoolean(bool)](#FromBoolean_bool)</code> - Converts a boolean value (true or false) to an arbitrary-precision integer.
* <code>[FromByte(byte)](#FromByte_byte)</code> - Converts a byte (from 0 to 255) to an arbitrary-precision integer.
* <code>[FromBytes(byte[], bool)](#FromBytes_byte_bool)</code> - Initializes an arbitrary-precision integer from an array of bytes.
* <code>[FromInt16(short)](#FromInt16_short)</code> - Converts a 16-bit signed integer to an arbitrary-precision integer.
* <code>[FromInt32(int)](#FromInt32_int)</code> - Converts a 32-bit signed integer to an arbitrary-precision integer.
* <code>[FromInt64(long)](#FromInt64_long)</code> - Converts a 64-bit signed integer to an arbitrary-precision integer.
* <code>[FromRadixString(string, int)](#FromRadixString_string_int)</code> - Converts a string to an arbitrary-precision integer in a given radix.
* <code>[FromRadixSubstring(string, int, int, int)](#FromRadixSubstring_string_int_int_int)</code> - Converts a portion of a string to an arbitrary-precision integer in a given radix.
* <code>[FromSByte(sbyte)](#FromSByte_sbyte)</code> - Converts an 8-bit signed integer to an arbitrary-precision integer.
* <code>[FromString(string)](#FromString_string)</code> - Converts a string to an arbitrary-precision integer.
* <code>[FromSubstring(string, int, int)](#FromSubstring_string_int_int)</code> - Converts a portion of a string to an arbitrary-precision integer.
* <code>[FromUInt16(ushort)](#FromUInt16_ushort)</code> - Converts a 16-bit unsigned integer to an arbitrary-precision integer.
* <code>[FromUInt32(uint)](#FromUInt32_uint)</code> - Converts a 32-bit signed integer to an arbitrary-precision integer.
* <code>[FromUInt64(ulong)](#FromUInt64_ulong)</code> - Converts a 64-bit unsigned integer to an arbitrary-precision integer.
* <code>[Gcd(PeterO.Numbers.EInteger)](#Gcd_PeterO_Numbers_EInteger)</code> - Returns the greatest common divisor of this integer and the given integer.
* <code>[GetBits(int, int)](#GetBits_int_int)</code> - Retrieves bits from this integer's two's-complement form.
* <code>[GetDigitCount()](#GetDigitCount)</code> - <b>Deprecated:</b> This method may overflow. Use GetDigitCountAsEInteger instead.
* <code>[GetDigitCountAsEInteger()](#GetDigitCountAsEInteger)</code> - Returns the number of decimal digits used by this integer, in the form of an arbitrary-precision integer.
* <code>[GetHashCode()](#GetHashCode)</code> - Returns the hash code for this instance.
* <code>[GetLowBit()](#GetLowBit)</code> - <b>Deprecated:</b> This method may overflow. Use GetLowBitAsEInteger instead.
* <code>[GetLowBitAsEInteger()](#GetLowBitAsEInteger)</code> - Gets the lowest set bit in this number's absolute value, in the form of an arbitrary-precision integer.
* <code>[GetSignedBit(int)](#GetSignedBit_int)</code> - Returns whether a bit is set in the two's-complement form (see T:PeterO.
* <code>[GetSignedBit(PeterO.Numbers.EInteger)](#GetSignedBit_PeterO_Numbers_EInteger)</code> - Returns whether a bit is set in the two's-complement form (see T:PeterO.
* <code>[GetSignedBitLength()](#GetSignedBitLength)</code> - <b>Deprecated:</b> This method may overflow. Use GetSignedBitLengthAsEInteger instead.
* <code>[GetSignedBitLengthAsEInteger()](#GetSignedBitLengthAsEInteger)</code> - Finds the minimum number of bits needed to represent this object's value, except for its sign, in the form of an arbitrary-precision integer.
* <code>[GetUnsignedBit(int)](#GetUnsignedBit_int)</code> - Returns whether a bit is set in this number's absolute value.
* <code>[GetUnsignedBit(PeterO.Numbers.EInteger)](#GetUnsignedBit_PeterO_Numbers_EInteger)</code> - Returns whether a bit is set in this number's absolute value.
* <code>[GetUnsignedBitLength()](#GetUnsignedBitLength)</code> - <b>Deprecated:</b> This method may overflow. Use GetUnsignedBitLengthAsEInteger instead.
* <code>[GetUnsignedBitLengthAsEInteger()](#GetUnsignedBitLengthAsEInteger)</code> - Finds the minimum number of bits needed to represent this number's absolute value.
* <code>[implicit operator PeterO.Numbers.EInteger(byte)](#implicit_operator_PeterO_Numbers_EInteger_byte)</code> - Converts a byte (from 0 to 255) to an arbitrary-precision integer.
* <code>[implicit operator PeterO.Numbers.EInteger(int)](#implicit_operator_PeterO_Numbers_EInteger_int)</code> - Converts a 32-bit signed integer to an arbitrary-precision integer.
* <code>[implicit operator PeterO.Numbers.EInteger(long)](#implicit_operator_PeterO_Numbers_EInteger_long)</code> - Converts a 64-bit signed integer to an arbitrary-precision integer.
* <code>[implicit operator PeterO.Numbers.EInteger(sbyte)](#implicit_operator_PeterO_Numbers_EInteger_sbyte)</code> - Converts an 8-bit signed integer to an arbitrary-precision integer.
* <code>[implicit operator PeterO.Numbers.EInteger(short)](#implicit_operator_PeterO_Numbers_EInteger_short)</code> - Converts a 16-bit signed integer to an arbitrary-precision integer.
* <code>[implicit operator PeterO.Numbers.EInteger(uint)](#implicit_operator_PeterO_Numbers_EInteger_uint)</code> - Converts a 32-bit signed integer to an arbitrary-precision integer.
* <code>[implicit operator PeterO.Numbers.EInteger(ulong)](#implicit_operator_PeterO_Numbers_EInteger_ulong)</code> - Converts a 64-bit unsigned integer to an arbitrary-precision integer.
* <code>[implicit operator PeterO.Numbers.EInteger(ushort)](#implicit_operator_PeterO_Numbers_EInteger_ushort)</code> - Converts a 16-bit unsigned integer to an arbitrary-precision integer.
* <code>[Increment()](#Increment)</code> - Returns one added to this arbitrary-precision integer.
* <code>[IsEven](#IsEven)</code> - Gets a value indicating whether this value is even.
* <code>[IsPowerOfTwo](#IsPowerOfTwo)</code> - Gets a value indicating whether this object's value is a power of two, and greater than 0.
* <code>[IsZero](#IsZero)</code> - Gets a value indicating whether this value is 0.
* <code>[Max(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#Max_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> -
* <code>[MaxMagnitude(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#MaxMagnitude_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> -
* <code>[Min(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#Min_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> -
* <code>[MinMagnitude(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#MinMagnitude_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> -
* <code>[Mod(int)](#Mod_int)</code> - Finds the modulus remainder that results when this instance is divided by the value of another integer.
* <code>[Mod(PeterO.Numbers.EInteger)](#Mod_PeterO_Numbers_EInteger)</code> - Finds the modulus remainder that results when this instance is divided by the value of an arbitrary-precision integer.
* <code>[ModPow(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#ModPow_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> - Calculates the remainder when this arbitrary-precision integer raised to a certain power is divided by another arbitrary-precision integer.
* <code>[ModPow(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#ModPow_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> - Calculates the remainder when an arbitrary-precision integer raised to a certain power is divided by another arbitrary-precision integer.
* <code>[Multiply(int)](#Multiply_int)</code> - Multiplies this instance by the value of an arbitrary-precision integer object.
* <code>[Multiply(PeterO.Numbers.EInteger)](#Multiply_PeterO_Numbers_EInteger)</code> - Multiplies this instance by the value of an arbitrary-precision integer object.
* <code>[Negate()](#Negate)</code> - Gets the value of this object with the sign reversed.
* <code>[Not()](#Not)</code> - Returns an arbitrary-precision integer with every bit flipped from this one.
* <code>[Not(PeterO.Numbers.EInteger)](#Not_PeterO_Numbers_EInteger)</code> - Returns an arbitrary-precision integer with every bit flipped.
* <code>[One](#One)</code> - Gets the number 1 as an arbitrary-precision integer.
* <code>[PeterO.Numbers.EInteger operator +(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_Addition)</code> - Adds two arbitrary-precision integer objects and returns the result.
* <code>[PeterO.Numbers.EInteger operator &amp;(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_BitwiseAnd)</code> - Does an AND operation between two arbitrary-precision integer values.
* <code>[PeterO.Numbers.EInteger operator |(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_BitwiseOr)</code> - Does an OR operation between two arbitrary-precision integer instances.
* <code>[PeterO.Numbers.EInteger operator --(PeterO.Numbers.EInteger)](#op_Decrement)</code> - Subtracts one from an arbitrary-precision integer.
* <code>[PeterO.Numbers.EInteger operator /(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_Division)</code> - Divides an arbitrary-precision integer by the value of an arbitrary-precision integer object.
* <code>[PeterO.Numbers.EInteger operator ^(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_ExclusiveOr)</code> - Finds the exclusive "or" of two arbitrary-precision integer objects.
* <code>[bool operator &gt;(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_GreaterThan)</code> - Determines whether an arbitrary-precision integer is greater than another arbitrary-precision integer.
* <code>[bool operator &gt;=(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_GreaterThanOrEqual)</code> - Determines whether an arbitrary-precision integer value is greater than another arbitrary-precision integer.
* <code>[PeterO.Numbers.EInteger operator ++(PeterO.Numbers.EInteger)](#op_Increment)</code> - Adds one to an arbitrary-precision integer.
* <code>[PeterO.Numbers.EInteger operator &lt;&lt;(PeterO.Numbers.EInteger, int)](#op_LeftShift)</code> - Returns an arbitrary-precision integer with the bits shifted to the left by a number of bits.
* <code>[bool operator &lt;(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_LessThan)</code> - Determines whether an arbitrary-precision integer is less than another arbitrary-precision integer.
* <code>[bool operator &lt;=(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_LessThanOrEqual)</code> - Determines whether an arbitrary-precision integer is up to another arbitrary-precision integer.
* <code>[PeterO.Numbers.EInteger operator %(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_Modulus)</code> - Finds the remainder that results when an arbitrary-precision integer is divided by the value of another arbitrary-precision integer.
* <code>[PeterO.Numbers.EInteger operator &#x2a;(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_Multiply)</code> - Multiplies an arbitrary-precision integer by the value of an arbitrary-precision integer.
* <code>[PeterO.Numbers.EInteger operator ~(PeterO.Numbers.EInteger)](#op_OnesComplement)</code> - Returns an arbitrary-precision integer with every bit flipped.
* <code>[PeterO.Numbers.EInteger operator &gt;&gt;(PeterO.Numbers.EInteger, int)](#op_RightShift)</code> - Shifts the bits of an arbitrary-precision integer to the right.
* <code>[PeterO.Numbers.EInteger operator -(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#op_Subtraction)</code> - Subtracts two arbitrary-precision integer values.
* <code>[PeterO.Numbers.EInteger operator -(PeterO.Numbers.EInteger)](#op_UnaryNegation)</code> - Negates an arbitrary-precision integer.
* <code>[Or(PeterO.Numbers.EInteger)](#Or_PeterO_Numbers_EInteger)</code> - Does an OR operation between two arbitrary-precision integer instances.
* <code>[Or(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#Or_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> - Does an OR operation between two arbitrary-precision integer instances.
* <code>[Pow(int)](#Pow_int)</code> - Raises an arbitrary-precision integer to a power.
* <code>[Pow(PeterO.Numbers.EInteger)](#Pow_PeterO_Numbers_EInteger)</code> - Raises an arbitrary-precision integer to a power.
* <code>[PowBigIntVar(PeterO.Numbers.EInteger)](#PowBigIntVar_PeterO_Numbers_EInteger)</code> - Raises an arbitrary-precision integer to a power, which is given as another arbitrary-precision integer.
* <code>[Remainder(int)](#Remainder_int)</code> - Finds the remainder that results when this instance is divided by the value of an arbitrary-precision integer.
* <code>[Remainder(PeterO.Numbers.EInteger)](#Remainder_PeterO_Numbers_EInteger)</code> - Finds the remainder that results when this instance is divided by the value of an arbitrary-precision integer.
* <code>[ShiftLeft(int)](#ShiftLeft_int)</code> - Returns an arbitrary-precision integer with the bits shifted to the left by a number of bits.
* <code>[ShiftLeft(PeterO.Numbers.EInteger)](#ShiftLeft_PeterO_Numbers_EInteger)</code> - Returns an arbitrary-precision integer with the bits shifted to the left by a number of bits given as an arbitrary-precision integer.
* <code>[ShiftRight(int)](#ShiftRight_int)</code> - Returns an arbitrary-precision integer with the bits shifted to the right.
* <code>[ShiftRight(PeterO.Numbers.EInteger)](#ShiftRight_PeterO_Numbers_EInteger)</code> - Returns an arbitrary-precision integer with the bits shifted to the right.
* <code>[Sign](#Sign)</code> - Gets the sign of this object's value.
* <code>[Sqrt()](#Sqrt)</code> - Finds the square root of this instance's value, rounded down.
* <code>[SqrtRem()](#SqrtRem)</code> - Calculates the square root and the remainder.
* <code>[Subtract(int)](#Subtract_int)</code> - Subtracts an arbitrary-precision integer from this arbitrary-precision integer.
* <code>[Subtract(PeterO.Numbers.EInteger)](#Subtract_PeterO_Numbers_EInteger)</code> - Subtracts an arbitrary-precision integer from this arbitrary-precision integer.
* <code>[Ten](#Ten)</code> - Gets the number 10 as an arbitrary-precision integer.
* <code>[ToByteChecked()](#ToByteChecked)</code> - Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255).
* <code>[ToBytes(bool)](#ToBytes_bool)</code> - Returns a byte array of this integer's value.
* <code>[ToByteUnchecked()](#ToByteUnchecked)</code> - Converts this number to a byte (from 0 to 255), returning the least-significant bits of this number's two's-complement form.
* <code>[ToInt16Checked()](#ToInt16Checked)</code> - Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer.
* <code>[ToInt16Unchecked()](#ToInt16Unchecked)</code> - Converts this number to a 16-bit signed integer, returning the least-significant bits of this number's two's-complement form.
* <code>[ToInt32Checked()](#ToInt32Checked)</code> - Converts this object's value to a 32-bit signed integer, throwing an exception if it can't fit.
* <code>[ToInt32Unchecked()](#ToInt32Unchecked)</code> - Converts this object's value to a 32-bit signed integer.
* <code>[ToInt64Checked()](#ToInt64Checked)</code> - Converts this object's value to a 64-bit signed integer, throwing an exception if it can't fit.
* <code>[ToInt64Unchecked()](#ToInt64Unchecked)</code> - Converts this object's value to a 64-bit signed integer.
* <code>[ToRadixString(int)](#ToRadixString_int)</code> - Generates a string representing the value of this object, in the given radix.
* <code>[ToSByteChecked()](#ToSByteChecked)</code> - Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer.
* <code>[ToSByteUnchecked()](#ToSByteUnchecked)</code> - Converts this number to an 8-bit signed integer, returning the least-significant bits of this number's two's-complement form.
* <code>[ToString()](#ToString)</code> - Converts this object to a text string in base 10.
* <code>[ToUInt16Checked()](#ToUInt16Checked)</code> - Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer.
* <code>[ToUInt16Unchecked()](#ToUInt16Unchecked)</code> - Converts this number to a 16-bit unsigned integer, returning the least-significant bits of this number's two's-complement form.
* <code>[ToUInt32Checked()](#ToUInt32Checked)</code> - Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer.
* <code>[ToUInt32Unchecked()](#ToUInt32Unchecked)</code> - Converts this number to a 32-bit signed integer, returning the least-significant bits of this number's two's-complement form.
* <code>[ToUInt64Checked()](#ToUInt64Checked)</code> - Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer.
* <code>[ToUInt64Unchecked()](#ToUInt64Unchecked)</code> - Converts this number to a 64-bit signed integer, returning the least-significant bits of this number's two's-complement form.
* <code>[Xor(PeterO.Numbers.EInteger)](#Xor_PeterO_Numbers_EInteger)</code> - Finds the exclusive "or" of two arbitrary-precision integer objects.
* <code>[Xor(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#Xor_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> - Finds the exclusive "or" of two arbitrary-precision integer objects.
* <code>[Zero](#Zero)</code> - Gets the number zero as an arbitrary-precision integer.

<a id="IsEven"></a>
### IsEven

    public bool IsEven { get; }

Gets a value indicating whether this value is even.

<b>Returns:</b>

 `true`  if this value is even; otherwise,  `false` .

<a id="IsPowerOfTwo"></a>
### IsPowerOfTwo

    public bool IsPowerOfTwo { get; }

Gets a value indicating whether this object's value is a power of two, and greater than 0.

<b>Returns:</b>

 `true`  if this object's value is a power of two, and greater than 0; otherwise,  `false` .

<a id="IsZero"></a>
### IsZero

    public bool IsZero { get; }

Gets a value indicating whether this value is 0.

<b>Returns:</b>

 `true`  if this value is 0; otherwise,  `false` .

<a id="One"></a>
### One

    public static PeterO.Numbers.EInteger One { get; }

Gets the number 1 as an arbitrary-precision integer.

<b>Returns:</b>

The number 1 as an arbitrary-precision integer.

<a id="Sign"></a>
### Sign

    public int Sign { get; }

Gets the sign of this object's value.

<b>Returns:</b>

The sign of this object's value.

<a id="Ten"></a>
### Ten

    public static PeterO.Numbers.EInteger Ten { get; }

Gets the number 10 as an arbitrary-precision integer.

<b>Returns:</b>

The number 10 as an arbitrary-precision integer.

<a id="Zero"></a>
### Zero

    public static PeterO.Numbers.EInteger Zero { get; }

Gets the number zero as an arbitrary-precision integer.

<b>Returns:</b>

The number zero as an arbitrary-precision integer.

<a id="Abs"></a>
### Abs

    public PeterO.Numbers.EInteger Abs();

Returns the absolute value of this object's value.

<b>Return Value:</b>

This object's value with the sign removed.

<a id="Add_int"></a>
### Add

    public PeterO.Numbers.EInteger Add(
        int intValue);

Adds this object and another object.

<b>Parameters:</b>

 * <i>intValue</i>: The parameter  <i>intValue</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<a id="Add_PeterO_Numbers_EInteger"></a>
### Add

    public PeterO.Numbers.EInteger Add(
        PeterO.Numbers.EInteger bigintAugend);

Adds this object and another object.

<b>Parameters:</b>

 * <i>bigintAugend</i>: Another arbitrary-precision integer.

<b>Return Value:</b>

The sum of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigintAugend</i>
 is null.

<a id="And_PeterO_Numbers_EInteger"></a>
### And

    public PeterO.Numbers.EInteger And(
        PeterO.Numbers.EInteger other);

Does an AND operation between two arbitrary-precision integer values.

Each arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) for the purposes of this operator.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer in which each bit is set if the corresponding bits of this integer and the other integer are both set.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>other</i>
 is null.

<a id="And_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
### And

    public static PeterO.Numbers.EInteger And(
        PeterO.Numbers.EInteger a,
        PeterO.Numbers.EInteger b);

Does an AND operation between two arbitrary-precision integer values.

Each arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) for the purposes of this operator.

<b>Parameters:</b>

 * <i>a</i>: The first arbitrary-precision integer.

 * <i>b</i>: The second arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer in which each bit is set if the corresponding bits of the two integers are both set.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>a</i>
 or  <i>b</i>
 is null.

<a id="AsInt32Checked"></a>
### AsInt32Checked

    public int AsInt32Checked();

<b>Deprecated.</b> Renamed to ToInt32Checked.

Converts this object's value to a 32-bit signed integer, throwing an exception if it can't fit.

<b>Return Value:</b>

A 32-bit signed integer.

<b>Exceptions:</b>

 *  T:System.OverflowException:
This object's value is too big to fit a 32-bit signed integer.

<a id="AsInt32Unchecked"></a>
### AsInt32Unchecked

    public int AsInt32Unchecked();

<b>Deprecated.</b> Renamed to ToInt32Unchecked.

Converts this object's value to a 32-bit signed integer. If the value can't fit in a 32-bit integer, returns the lower 32 bits of this object's two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) (in which case the return value might have a different sign than this object's value).

<b>Return Value:</b>

A 32-bit signed integer.

<a id="AsInt64Checked"></a>
### AsInt64Checked

    public long AsInt64Checked();

<b>Deprecated.</b> Renamed to ToInt64Checked.

Converts this object's value to a 64-bit signed integer, throwing an exception if it can't fit.

<b>Return Value:</b>

A 64-bit signed integer.

<b>Exceptions:</b>

 *  T:System.OverflowException:
This object's value is too big to fit a 64-bit signed integer.

<a id="AsInt64Unchecked"></a>
### AsInt64Unchecked

    public long AsInt64Unchecked();

<b>Deprecated.</b> Renamed to ToInt64Unchecked.

Converts this object's value to a 64-bit signed integer. If the value can't fit in a 64-bit integer, returns the lower 64 bits of this object's two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) (in which case the return value might have a different sign than this object's value).

<b>Return Value:</b>

A 64-bit signed integer.

<a id="CanFitInInt32"></a>
### CanFitInInt32

    public bool CanFitInInt32();

Returns whether this object's value can fit in a 32-bit signed integer.

<b>Return Value:</b>

 `true`  if this object's value is from -2147483648 through 2147483647; otherwise,  `false` .

<a id="CanFitInInt64"></a>
### CanFitInInt64

    public bool CanFitInInt64();

Returns whether this object's value can fit in a 64-bit signed integer.

<b>Return Value:</b>

 `true`  if this object's value is from -9223372036854775808 through 9223372036854775807; otherwise,  `false` .

<a id="CompareTo_int"></a>
### CompareTo

    public int CompareTo(
        int intValue);

Compares an arbitrary-precision integer with this instance.

<b>Parameters:</b>

 * <i>intValue</i>: The parameter  <i>intValue</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater.

<a id="CompareTo_PeterO_Numbers_EInteger"></a>
### CompareTo

    public sealed int CompareTo(
        PeterO.Numbers.EInteger other);

Compares an arbitrary-precision integer with this instance.

<b>Parameters:</b>

 * <i>other</i>: The integer to compare to this value.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="Decrement"></a>
### Decrement

    public PeterO.Numbers.EInteger Decrement();

Returns one subtracted from this arbitrary-precision integer.

<b>Return Value:</b>

The given arbitrary-precision integer minus one.

<a id="Divide_int"></a>
### Divide

    public PeterO.Numbers.EInteger Divide(
        int intValue);

Divides this instance by the value of an arbitrary-precision integer. The result is rounded down (the fractional part is discarded). Except if the result is 0, it will be negative if this object is positive and the other is negative, or vice versa, and will be positive if both are positive or both are negative.

<b>Parameters:</b>

 * <i>intValue</i>: The divisor.

<b>Return Value:</b>

The quotient of the two objects.

<b>Exceptions:</b>

 * System.DivideByZeroException:
Attempted to divide by zero.

<a id="Divide_PeterO_Numbers_EInteger"></a>
### Divide

    public PeterO.Numbers.EInteger Divide(
        PeterO.Numbers.EInteger bigintDivisor);

Divides this instance by the value of an arbitrary-precision integer. The result is rounded down (the fractional part is discarded). Except if the result is 0, it will be negative if this object is positive and the other is negative, or vice versa, and will be positive if both are positive or both are negative.

<b>Parameters:</b>

 * <i>bigintDivisor</i>: The divisor.

<b>Return Value:</b>

The quotient of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigintDivisor</i>
 is null.

 * System.DivideByZeroException:
Attempted to divide by zero.

<a id="DivRem_PeterO_Numbers_EInteger"></a>
### DivRem

    public PeterO.Numbers.EInteger[] DivRem(
        PeterO.Numbers.EInteger divisor);

Divides this object by another arbitrary-precision integer and returns the quotient and remainder.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

An array with two arbitrary-precision integers: the first is the quotient, and the second is the remainder.

<b>Exceptions:</b>

 * System.DivideByZeroException:
The parameter divisor is 0.

 * System.ArgumentNullException:
The parameter  <i>divisor</i>
 is null.

<a id="DivRem_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
### DivRem

    public static PeterO.Numbers.EInteger DivRem(
        PeterO.Numbers.EInteger dividend,
        PeterO.Numbers.EInteger divisor,
        PeterO.Numbers.EInteger& remainder);

<b>Deprecated.</b> Use the DivRem instance method instead.

Divides one arbitrary-precision integer by another, and returns the quotient and sets an output parameter to the remainder.

<b>Parameters:</b>

 * <i>dividend</i>: The arbitrary-precision integer to be divided.

 * <i>divisor</i>: The arbitrary-precision integer to divide by.

 * <i>remainder</i>: An arbitrary-precision integer.

<b>Return Value:</b>

An array of two arbitrary-precision integers: the first is the quotient, and the second is the remainder.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>dividend</i>
 or  <i>divisor</i>
 is null.

<a id="Equals_object"></a>
### Equals

    public override bool Equals(
        object obj);

Determines whether this object and another object are equal and have the same type.

<b>Parameters:</b>

 * <i>obj</i>: The parameter  <i>obj</i>
 is an arbitrary object.

<b>Return Value:</b>

 `true`  if this object and another object are equal; otherwise,  `false` .

<a id="Equals_PeterO_Numbers_EInteger"></a>
### Equals

    public sealed bool Equals(
        PeterO.Numbers.EInteger other);

Determines whether this object and another object are equal.

<b>Parameters:</b>

 * <i>other</i>: Another arbitrary-precision integer.

<b>Return Value:</b>

 `true`  if this object and another object are equal; otherwise,  `false` .

<a id="FromBoolean_bool"></a>
### FromBoolean

    public static PeterO.Numbers.EInteger FromBoolean(
        bool boolValue);

Converts a boolean value (true or false) to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>boolValue</i>: Either true or false.

<b>Return Value:</b>

The number 1 if  <i>boolValue</i>
 is true; otherwise, 0.

<a id="FromByte_byte"></a>
### FromByte

    public static PeterO.Numbers.EInteger FromByte(
        byte inputByte);

Converts a byte (from 0 to 255) to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputByte</i>: The number to convert as a byte (from 0 to 255).

<b>Return Value:</b>

This number's value as an arbitrary-precision integer.

<a id="FromBytes_byte_bool"></a>
### FromBytes

    public static PeterO.Numbers.EInteger FromBytes(
        byte[] bytes,
        bool littleEndian);

Initializes an arbitrary-precision integer from an array of bytes.

<b>Parameters:</b>

 * <i>bytes</i>: A byte array consisting of the two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) of the arbitrary-precision integer to create. The byte array is encoded using the following rules:

 * Positive numbers have the first byte's highest bit cleared, and negative numbers have the bit set.

 * The last byte contains the lowest 8-bits, the next-to-last contains the next lowest 8 bits, and so on. For example, the number 300 can be encoded as  `0x01, 0x2C`  and 200 as  `0x00,
            0xC8` . (Note that the second example contains a set high bit in  `0xC8` , so an additional 0 is added at the start to ensure it's interpreted as positive.)

 * To encode negative numbers, take the absolute value of the number, subtract by 1, encode the number into bytes, and toggle each bit of each byte. Any further bits that appear beyond the most significant bit of the number will be all ones. For example, the number -450 can be encoded as  `0xfe, 0x70`  and -52869 as  `0xff, 0x31, 0x7B` . (Note that the second example contains a cleared high bit in  `0x31, 0x7B` , so an additional 0xff is added at the start to ensure it's interpreted as negative.)

For little-endian, the byte order is reversed from the byte order just discussed.

.

 * <i>littleEndian</i>: If true, the byte order is little-endian, or least-significant-byte first. If false, the byte order is big-endian, or most-significant-byte first.

<b>Return Value:</b>

An arbitrary-precision integer. Returns 0 if the byte array's length is 0.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bytes</i>
 is null.

<a id="FromInt16_short"></a>
### FromInt16

    public static PeterO.Numbers.EInteger FromInt16(
        short inputInt16);

Converts a 16-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputInt16</i>: The number to convert as a 16-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision integer.

<a id="FromInt32_int"></a>
### FromInt32

    public static PeterO.Numbers.EInteger FromInt32(
        int intValue);

Converts a 32-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>intValue</i>: The parameter  <i>intValue</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

An arbitrary-precision integer with the same value as the 64-bit number.

<a id="FromInt64_long"></a>
### FromInt64

    public static PeterO.Numbers.EInteger FromInt64(
        long longerValue);

Converts a 64-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>longerValue</i>: The parameter  <i>longerValue</i>
 is a 64-bit signed integer.

<b>Return Value:</b>

An arbitrary-precision integer with the same value as the 64-bit number.

<a id="FromRadixString_string_int"></a>
### FromRadixString

    public static PeterO.Numbers.EInteger FromRadixString(
        string str,
        int radix);

Converts a string to an arbitrary-precision integer in a given radix.

<b>Parameters:</b>

 * <i>str</i>: A string described by the FromRadixSubstring method.

 * <i>radix</i>: A base from 2 to 36. Depending on the radix, the string can use the basic digits 0 to 9 (U+0030 to U+0039) and then the basic upper-case letters A to Z (U+0041 to U+005A). For example, 0-9 in radix 10, and 0-9, then A-F in radix 16. Where a basic upper-case letter A to Z is allowed in the string, the corresponding basic lower-case letter (U+0061 to U+007a) is allowed instead.

<b>Return Value:</b>

An arbitrary-precision integer with the same value as the given string.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>str</i>
 is null.

<a id="FromRadixSubstring_string_int_int_int"></a>
### FromRadixSubstring

    public static PeterO.Numbers.EInteger FromRadixSubstring(
        string str,
        int radix,
        int index,
        int endIndex);

Converts a portion of a string to an arbitrary-precision integer in a given radix.

<b>Parameters:</b>

 * <i>str</i>: A text string. The desired portion of the string must contain only characters allowed by the given radix, except that it may start with a minus sign ("-", U+002D) to indicate a negative number. The desired portion is not allowed to contain white space characters, including spaces. The desired portion may start with any number of zeros.

 * <i>radix</i>: A base from 2 to 36. Depending on the radix, the string can use the basic digits 0 to 9 (U+0030 to U+0039) and then the basic upper-case letters A to Z (U+0041 to U+005A). For example, 0-9 in radix 10, and 0-9, then A-F in radix 16. Where a basic upper-case letter A to Z is allowed in the string, the corresponding basic lower-case letter (U+0061 to U+007a) is allowed instead.

 * <i>index</i>: The index of the string that starts the string portion.

 * <i>endIndex</i>: The index of the string that ends the string portion. The length will be index + endIndex - 1.

<b>Return Value:</b>

An arbitrary-precision integer with the same value as given in the string portion.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>str</i>
 is null.

 * System.FormatException:
The string portion is empty or in an invalid format.

<a id="FromSByte_sbyte"></a>
### FromSByte

    public static PeterO.Numbers.EInteger FromSByte(
        sbyte inputSByte);

<b>This API is not CLS-compliant.</b>

Converts an 8-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputSByte</i>: The number to convert as an 8-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision integer.

<a id="FromString_string"></a>
### FromString

    public static PeterO.Numbers.EInteger FromString(
        string str);

Converts a string to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>str</i>: A text string describing an integer in base-10 (decimal) form. The string must contain only basic digits 0 to 9 (U+0030 to U+0039), except that it may start with a minus sign ("-", U+002D) to indicate a negative number. The string is not allowed to contain white space characters, including spaces. The string may start with any number of zeros.

<b>Return Value:</b>

An arbitrary-precision integer with the same value as given in the string.

<b>Exceptions:</b>

 * System.FormatException:
The parameter  <i>str</i>
 is in an invalid format.

 * System.ArgumentNullException:
The parameter  <i>str</i>
 is null.

<a id="FromSubstring_string_int_int"></a>
### FromSubstring

    public static PeterO.Numbers.EInteger FromSubstring(
        string str,
        int index,
        int endIndex);

Converts a portion of a string to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>str</i>: A text string, the desired portion of which describes an integer in base-10 (decimal) form. The desired portion of the string must contain only basic digits 0 to 9 (U+0030 to U+0039), except that it may start with a minus sign ("-", U+002D) to indicate a negative number. The desired portion is not allowed to contain white space characters, including spaces. The desired portion may start with any number of zeros.

 * <i>index</i>: The index of the string that starts the string portion.

 * <i>endIndex</i>: The index of the string that ends the string portion. The length will be index + endIndex - 1.

<b>Return Value:</b>

An arbitrary-precision integer with the same value as given in the string portion.

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter  <i>index</i>
 is less than 0,  <i>endIndex</i>
 is less than 0, or either is greater than the string's length, or  <i>endIndex</i>
 is less than  <i>index</i>
.

 * System.ArgumentNullException:
The parameter  <i>str</i>
 is null.

<a id="FromUInt16_ushort"></a>
### FromUInt16

    public static PeterO.Numbers.EInteger FromUInt16(
        ushort inputUInt16);

<b>This API is not CLS-compliant.</b>

Converts a 16-bit unsigned integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputUInt16</i>: The number to convert as a 16-bit unsigned integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision integer.

<a id="FromUInt32_uint"></a>
### FromUInt32

    public static PeterO.Numbers.EInteger FromUInt32(
        uint inputUInt32);

<b>This API is not CLS-compliant.</b>

Converts a 32-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputUInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision integer.

<a id="FromUInt64_ulong"></a>
### FromUInt64

    public static PeterO.Numbers.EInteger FromUInt64(
        ulong ulongValue);

<b>This API is not CLS-compliant.</b>

Converts a 64-bit unsigned integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>ulongValue</i>: The number to convert as a 64-bit unsigned integer.

<b>Return Value:</b>

The value of  <i>ulongValue</i>
 as an arbitrary-precision integer.

<a id="Gcd_PeterO_Numbers_EInteger"></a>
### Gcd

    public PeterO.Numbers.EInteger Gcd(
        PeterO.Numbers.EInteger bigintSecond);

Returns the greatest common divisor of this integer and the given integer. The greatest common divisor (GCD) is also known as the greatest common factor (GCF).

<b>Parameters:</b>

 * <i>bigintSecond</i>: Another arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigintSecond</i>
 is null.

<a id="GetBits_int_int"></a>
### GetBits

    public long GetBits(
        int index,
        int numberBits);

Retrieves bits from this integer's two's-complement form.

<b>Parameters:</b>

 * <i>index</i>: Zero-based index of the first bit to retrieve, where 0 is the least-significant bit of the number.

 * <i>numberBits</i>: The number of bits to retrieve, starting with the first. Must be from 0 through 64.

<b>Return Value:</b>

A 64-bit signed integer containing the bits from this integer's two's-complement form. The least significant bit is the first bit, and any unused bits are set to 0.

<a id="GetDigitCount"></a>
### GetDigitCount

    public int GetDigitCount();

<b>Deprecated.</b> This method may overflow. Use GetDigitCountAsEInteger instead.

Returns the number of decimal digits used by this integer.

<b>Return Value:</b>

The number of digits in the decimal form of this integer. Returns 1 if this number is 0.

<b>Exceptions:</b>

 * System.OverflowException:
The return value would exceed the range of a 32-bit signed integer.

<a id="GetDigitCountAsEInteger"></a>
### GetDigitCountAsEInteger

    public PeterO.Numbers.EInteger GetDigitCountAsEInteger();

Returns the number of decimal digits used by this integer, in the form of an arbitrary-precision integer.

<b>Return Value:</b>

The number of digits in the decimal form of this integer. Returns 1 if this number is 0.

<a id="GetHashCode"></a>
### GetHashCode

    public override int GetHashCode();

Returns the hash code for this instance. No application or process IDs are used in the hash code calculation.

<b>Return Value:</b>

A 32-bit signed integer.

<a id="GetLowBit"></a>
### GetLowBit

    public int GetLowBit();

<b>Deprecated.</b> This method may overflow. Use GetLowBitAsEInteger instead.

Gets the lowest set bit in this number's absolute value. (This will also be the lowest set bit in the number's two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ).).

<b>Return Value:</b>

The lowest bit set in the number, starting at 0. Returns -1 if this value is 0.

<a id="GetLowBitAsEInteger"></a>
### GetLowBitAsEInteger

    public PeterO.Numbers.EInteger GetLowBitAsEInteger();

Gets the lowest set bit in this number's absolute value, in the form of an arbitrary-precision integer. (This will also be the lowest set bit in the number's two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ).).

<b>Return Value:</b>

The lowest bit set in the number, starting at 0. Returns -1 if this value is 0 or odd.

<a id="GetSignedBit_int"></a>
### GetSignedBit

    public bool GetSignedBit(
        int index);

Returns whether a bit is set in the two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) of this object's value.

<b>Parameters:</b>

 * <i>index</i>: The index, starting at 0, of the bit to test, where 0 is the least significant bit, 1 is the next least significant bit, and so on.

<b>Return Value:</b>

 `true`  if the given bit is set in the two' s-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) of this object's value; otherwise,  `false` .

<a id="GetSignedBit_PeterO_Numbers_EInteger"></a>
### GetSignedBit

    public bool GetSignedBit(
        PeterO.Numbers.EInteger bigIndex);

Returns whether a bit is set in the two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) of this object's value.

<b>Parameters:</b>

 * <i>bigIndex</i>: The index, starting at zero, of the bit to test, where 0 is the least significant bit, 1 is the next least significant bit, and so on.

<b>Return Value:</b>

 `true`  if the given bit is set in the two' s-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) of this object's value; otherwise,  `false` .

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigIndex</i>
 is null.

<a id="GetSignedBitLength"></a>
### GetSignedBitLength

    public int GetSignedBitLength();

<b>Deprecated.</b> This method may overflow. Use GetSignedBitLengthAsEInteger instead.

Finds the minimum number of bits needed to represent this object's value, except for its sign. If the value is negative, finds the number of bits in the value equal to this object's absolute value minus 1. For example, all integers in the interval [-(2^63), (2^63) - 1], which is the same as the range of integers in Java's and.NET's  `long`  type, have a signed bit length of 63 or less, and all other integers have a signed bit length of greater than 63.

<b>Return Value:</b>

The number of bits in this object's value. Returns 0 if this object's value is 0 or negative 1.

<b>Exceptions:</b>

 * System.OverflowException:
The return value would exceed the range of a 32-bit signed integer.

<a id="GetSignedBitLengthAsEInteger"></a>
### GetSignedBitLengthAsEInteger

    public PeterO.Numbers.EInteger GetSignedBitLengthAsEInteger();

Finds the minimum number of bits needed to represent this object's value, except for its sign, in the form of an arbitrary-precision integer. If the value is negative, finds the number of bits in the value equal to this object's absolute value minus 1. For example, all integers in the interval [-(2^63), (2^63) - 1], which is the same as the range of integers in Java's and.NET's  `long`  type, have a signed bit length of 63 or less, and all other integers have a signed bit length of greater than 63.

<b>Return Value:</b>

The number of bits in this object's value. Returns 0 if this object's value is 0 or negative 1.

<a id="GetUnsignedBit_int"></a>
### GetUnsignedBit

    public bool GetUnsignedBit(
        int index);

Returns whether a bit is set in this number's absolute value.

<b>Parameters:</b>

 * <i>index</i>: The index, starting at 0, of the bit to test, where 0 is the least significant bit, 1 is the next least significant bit, and so on.

<b>Return Value:</b>

 `true`  if the given bit is set in this number's absolute value.

<a id="GetUnsignedBit_PeterO_Numbers_EInteger"></a>
### GetUnsignedBit

    public bool GetUnsignedBit(
        PeterO.Numbers.EInteger bigIndex);

Returns whether a bit is set in this number's absolute value.

<b>Parameters:</b>

 * <i>bigIndex</i>: The index, starting at zero, of the bit to test, where 0 is the least significant bit, 1 is the next least significant bit, and so on.

<b>Return Value:</b>

 `true`  if the given bit is set in this number's absolute value.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigIndex</i>
 is null.

<a id="GetUnsignedBitLength"></a>
### GetUnsignedBitLength

    public int GetUnsignedBitLength();

<b>Deprecated.</b> This method may overflow. Use GetUnsignedBitLengthAsEInteger instead.

Finds the minimum number of bits needed to represent this number's absolute value. For example, all integers in the interval [-((2^63) - 1), (2^63) - 1] have an unsigned bit length of 63 or less, and all other integers have an unsigned bit length of greater than 63. This interval is not the same as the range of integers in Java's and.NET's  `long`  type.

<b>Return Value:</b>

The number of bits in this object's value. Returns 0 if this object's value is 0, and returns 1 if the value is negative 1.

<b>Exceptions:</b>

 * System.OverflowException:
The return value would exceed the range of a 32-bit signed integer.

<a id="GetUnsignedBitLengthAsEInteger"></a>
### GetUnsignedBitLengthAsEInteger

    public PeterO.Numbers.EInteger GetUnsignedBitLengthAsEInteger();

Finds the minimum number of bits needed to represent this number's absolute value. For example, all integers in the interval [-((2^63) - 1), (2^63) - 1] have an unsigned bit length of 63 or less, and all other integers have an unsigned bit length of greater than 63. This interval is not the same as the range of integers in Java's and.NET's  `long`  type.

<b>Return Value:</b>

The number of bits in this object's value. Returns 0 if this object's value is 0, and returns 1 if the value is negative 1.

<a id="Increment"></a>
### Increment

    public PeterO.Numbers.EInteger Increment();

Returns one added to this arbitrary-precision integer.

<b>Return Value:</b>

The given arbitrary-precision integer plus one.

<a id="Mod_int"></a>
### Mod

    public PeterO.Numbers.EInteger Mod(
        int smallDivisor);

Finds the modulus remainder that results when this instance is divided by the value of another integer. The modulus remainder is the same as the normal remainder if the normal remainder is positive, and equals divisor plus normal remainder if the normal remainder is negative.

<b>Parameters:</b>

 * <i>smallDivisor</i>: The divisor of the modulus.

<b>Return Value:</b>

The modulus remainder.

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter  <i>smallDivisor</i>
 is less than 0.

<a id="Mod_PeterO_Numbers_EInteger"></a>
### Mod

    public PeterO.Numbers.EInteger Mod(
        PeterO.Numbers.EInteger divisor);

Finds the modulus remainder that results when this instance is divided by the value of an arbitrary-precision integer. The modulus remainder is the same as the normal remainder if the normal remainder is positive, and equals divisor plus normal remainder if the normal remainder is negative.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter  <i>divisor</i>
 is less than 0.

 * System.ArgumentNullException:
The parameter  <i>divisor</i>
 is null.

<a id="ModPow_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
### ModPow

    public PeterO.Numbers.EInteger ModPow(
        PeterO.Numbers.EInteger pow,
        PeterO.Numbers.EInteger mod);

Calculates the remainder when this arbitrary-precision integer raised to a certain power is divided by another arbitrary-precision integer.

<b>Parameters:</b>

 * <i>pow</i>: The power to raise this integer by.

 * <i>mod</i>: The integer to divide the raised number by.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>pow</i>
 or  <i>mod</i>
 is null.

<a id="ModPow_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
### ModPow

    public static PeterO.Numbers.EInteger ModPow(
        PeterO.Numbers.EInteger bigintValue,
        PeterO.Numbers.EInteger pow,
        PeterO.Numbers.EInteger mod);

Calculates the remainder when an arbitrary-precision integer raised to a certain power is divided by another arbitrary-precision integer.

<b>Parameters:</b>

 * <i>bigintValue</i>: The starting operand.

 * <i>pow</i>: The power to raise this integer by.

 * <i>mod</i>: The integer to divide the raised number by.

<b>Return Value:</b>

The value (  <i>bigintValue</i>
 ^  <i>pow</i>
 )%  <i>mod</i>
.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigintValue</i>
 is null.

<a id="Multiply_int"></a>
### Multiply

    public PeterO.Numbers.EInteger Multiply(
        int intValue);

Multiplies this instance by the value of an arbitrary-precision integer object.

    EInteger result = EInteger.FromString("5").Multiply(200);

 .

<b>Parameters:</b>

 * <i>intValue</i>: The parameter  <i>intValue</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

The product of the two numbers.

<a id="Multiply_PeterO_Numbers_EInteger"></a>
### Multiply

    public PeterO.Numbers.EInteger Multiply(
        PeterO.Numbers.EInteger bigintMult);

Multiplies this instance by the value of an arbitrary-precision integer object.

<b>Parameters:</b>

 * <i>bigintMult</i>: Another arbitrary-precision integer.

<b>Return Value:</b>

The product of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigintMult</i>
 is null.

<a id="Negate"></a>
### Negate

    public PeterO.Numbers.EInteger Negate();

Gets the value of this object with the sign reversed.

<b>Return Value:</b>

This object's value with the sign reversed.

<a id="Not"></a>
### Not

    public PeterO.Numbers.EInteger Not();

Returns an arbitrary-precision integer with every bit flipped from this one.

<b>Return Value:</b>

An arbitrary-precision integer.

<a id="Not_PeterO_Numbers_EInteger"></a>
### Not

    public static PeterO.Numbers.EInteger Not(
        PeterO.Numbers.EInteger valueA);

Returns an arbitrary-precision integer with every bit flipped.

<b>Parameters:</b>

 * <i>valueA</i>: The operand as an arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>valueA</i>
 is null.

<a id="op_Addition"></a>
### Operator `+`

    public static PeterO.Numbers.EInteger operator +(
        PeterO.Numbers.EInteger bthis,
        PeterO.Numbers.EInteger augend);

Adds two arbitrary-precision integer objects and returns the result.

<b>Parameters:</b>

 * <i>bthis</i>: The first operand.

 * <i>augend</i>: The second operand.

<b>Return Value:</b>

The sum of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_BitwiseAnd"></a>
### Operator `&`

    public static PeterO.Numbers.EInteger operator &(
        PeterO.Numbers.EInteger thisValue,
        PeterO.Numbers.EInteger otherValue);

Does an AND operation between two arbitrary-precision integer values. For each bit of the result, that bit is 1 if the corresponding bits of the two operands are both 1, or is 0 otherwise.

Each arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) for the purposes of this operator.

<b>Parameters:</b>

 * <i>thisValue</i>: The first operand.

 * <i>otherValue</i>: The second operand.

<b>Return Value:</b>

The result of the operation.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "a" or "b" is null.

<a id="op_BitwiseOr"></a>
### Operator `|`

    public static PeterO.Numbers.EInteger operator |(
        PeterO.Numbers.EInteger thisValue,
        PeterO.Numbers.EInteger otherValue);

Does an OR operation between two arbitrary-precision integer instances. For each bit of the result, that bit is 1 if either or both of the corresponding bits of the two operands are 1, or is 0 otherwise.

Each arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) for the purposes of this operator.

<b>Parameters:</b>

 * <i>thisValue</i>: An arbitrary-precision integer.

 * <i>otherValue</i>: Another arbitrary-precision integer.

<b>Return Value:</b>

The result of the operation.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "first" or "second" is null.

<a id="op_Decrement"></a>
### Operator `--`

    public static PeterO.Numbers.EInteger operator --(
        PeterO.Numbers.EInteger bthis);

Subtracts one from an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>bthis</i>: An arbitrary-precision integer.

<b>Return Value:</b>

The given arbitrary-precision integer minus one.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_Division"></a>
### Operator `/`

    public static PeterO.Numbers.EInteger operator /(
        PeterO.Numbers.EInteger dividend,
        PeterO.Numbers.EInteger divisor);

Divides an arbitrary-precision integer by the value of an arbitrary-precision integer object.

<b>Parameters:</b>

 * <i>dividend</i>: The number that will be divided by the divisor.

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

The quotient of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>dividend</i>
 is null.

<a id="op_ExclusiveOr"></a>
### Operator `^`

    public static PeterO.Numbers.EInteger operator ^(
        PeterO.Numbers.EInteger a,
        PeterO.Numbers.EInteger b);

Finds the exclusive "or" of two arbitrary-precision integer objects. For each bit of the result, that bit is 1 if either of the corresponding bits of the two operands, but not both, is 1, or is 0 otherwise. Each arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) for the purposes of this operator.

<b>Parameters:</b>

 * <i>a</i>: The first arbitrary-precision integer.

 * <i>b</i>: The second arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer in which each bit is set if it's set in one input integer but not the other.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>a</i>
 or  <i>b</i>
 is null.

<a id="explicit_operator_byte_PeterO_Numbers_EInteger"></a>
### Explicit Operator

    public static explicit operator byte(
        PeterO.Numbers.EInteger input);

Converts an arbitrary-precision integer to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255).

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision integer.

<b>Return Value:</b>

The value of  <i>input</i>
 as a byte (from 0 to 255).

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is less than 0 or greater than 255.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_int_PeterO_Numbers_EInteger"></a>
### Explicit Operator

    public static explicit operator int(
        PeterO.Numbers.EInteger input);

Converts an arbitrary-precision integer to a 32-bit signed integer if it can fit in a 32-bit signed integer.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision integer.

<b>Return Value:</b>

The value of  <i>input</i>
 as a 32-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is less than -2147483648 or greater than 2147483647.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_long_PeterO_Numbers_EInteger"></a>
### Explicit Operator

    public static explicit operator long(
        PeterO.Numbers.EInteger input);

Converts an arbitrary-precision integer to a 64-bit signed integer if it can fit in a 64-bit signed integer.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision integer.

<b>Return Value:</b>

The value of  <i>input</i>
 as a 64-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is less than -9223372036854775808 or greater than 9223372036854775807.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_PeterO_Numbers_EInteger_bool"></a>
### Explicit Operator

    public static explicit operator PeterO.Numbers.EInteger(
        bool boolValue);

Converts a byte (from 0 to 255) to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>boolValue</i>: Either  `true`  or  `false` .

<b>Return Value:</b>

The value of  <i>boolValue</i>
 as an arbitrary-precision integer.

<a id="explicit_operator_sbyte_PeterO_Numbers_EInteger"></a>
### Explicit Operator

    public static explicit operator sbyte(
        PeterO.Numbers.EInteger input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision integer to an 8-bit signed integer if it can fit in an 8-bit signed integer.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision integer.

<b>Return Value:</b>

The value of  <i>input</i>
 as an 8-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is less than -128 or greater than 127.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_short_PeterO_Numbers_EInteger"></a>
### Explicit Operator

    public static explicit operator short(
        PeterO.Numbers.EInteger input);

Converts an arbitrary-precision integer to a 16-bit signed integer if it can fit in a 16-bit signed integer.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision integer.

<b>Return Value:</b>

The value of  <i>input</i>
 as a 16-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is less than -32768 or greater than 32767.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_uint_PeterO_Numbers_EInteger"></a>
### Explicit Operator

    public static explicit operator uint(
        PeterO.Numbers.EInteger input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision integer to a 32-bit signed integer if it can fit in a 32-bit signed integer.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision integer.

<b>Return Value:</b>

The value of  <i>input</i>
 as a 32-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is less than 0 or greater than 4294967295.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_ulong_PeterO_Numbers_EInteger"></a>
### Explicit Operator

    public static explicit operator ulong(
        PeterO.Numbers.EInteger input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision integer to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision integer.

<b>Return Value:</b>

The value of  <i>input</i>
 as a 64-bit unsigned integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is less than 0 or greater than 18446744073709551615.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_ushort_PeterO_Numbers_EInteger"></a>
### Explicit Operator

    public static explicit operator ushort(
        PeterO.Numbers.EInteger input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision integer to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision integer.

<b>Return Value:</b>

The value of  <i>input</i>
 as a 16-bit unsigned integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is less than 0 or greater than 65535.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="op_GreaterThan"></a>
### Operator `>`

    public static bool operator >(
        PeterO.Numbers.EInteger thisValue,
        PeterO.Numbers.EInteger otherValue);

Determines whether an arbitrary-precision integer is greater than another arbitrary-precision integer.

<b>Parameters:</b>

 * <i>thisValue</i>: The first arbitrary-precision integer.

 * <i>otherValue</i>: The second arbitrary-precision integer.

<b>Return Value:</b>

 `true`  if  <i>thisValue</i>
 is greater than  <i>otherValue</i>
 ; otherwise,  `false` .

<a id="op_GreaterThanOrEqual"></a>
### Operator `>=`

    public static bool operator >=(
        PeterO.Numbers.EInteger thisValue,
        PeterO.Numbers.EInteger otherValue);

Determines whether an arbitrary-precision integer value is greater than another arbitrary-precision integer.

<b>Parameters:</b>

 * <i>thisValue</i>: The first arbitrary-precision integer.

 * <i>otherValue</i>: The second arbitrary-precision integer.

<b>Return Value:</b>

 `true`  if  <i>thisValue</i>
 is at least  <i>otherValue</i>
 ; otherwise,  `false` .

<a id="implicit_operator_PeterO_Numbers_EInteger_byte"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EInteger(
        byte inputByte);

Converts a byte (from 0 to 255) to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputByte</i>: The number to convert as a byte (from 0 to 255).

<b>Return Value:</b>

The value of  <i>inputByte</i>
 as an arbitrary-precision integer.

<a id="implicit_operator_PeterO_Numbers_EInteger_int"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EInteger(
        int inputInt32);

Converts a 32-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputInt32</i>
 as an arbitrary-precision integer.

<a id="implicit_operator_PeterO_Numbers_EInteger_long"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EInteger(
        long inputInt64);

Converts a 64-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputInt64</i>: The number to convert as a 64-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputInt64</i>
 as an arbitrary-precision integer.

<a id="implicit_operator_PeterO_Numbers_EInteger_sbyte"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EInteger(
        sbyte inputSByte);

<b>This API is not CLS-compliant.</b>

Converts an 8-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputSByte</i>: The number to convert as an 8-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputSByte</i>
 as an arbitrary-precision integer.

<a id="implicit_operator_PeterO_Numbers_EInteger_short"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EInteger(
        short inputInt16);

Converts a 16-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputInt16</i>: The number to convert as a 16-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputInt16</i>
 as an arbitrary-precision integer.

<a id="implicit_operator_PeterO_Numbers_EInteger_uint"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EInteger(
        uint inputUInt32);

<b>This API is not CLS-compliant.</b>

Converts a 32-bit signed integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputUInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputUInt32</i>
 as an arbitrary-precision integer.

<a id="implicit_operator_PeterO_Numbers_EInteger_ulong"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EInteger(
        ulong inputUInt64);

<b>This API is not CLS-compliant.</b>

Converts a 64-bit unsigned integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputUInt64</i>: The number to convert as a 64-bit unsigned integer.

<b>Return Value:</b>

The value of  <i>inputUInt64</i>
 as an arbitrary-precision integer.

<a id="implicit_operator_PeterO_Numbers_EInteger_ushort"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EInteger(
        ushort inputUInt16);

<b>This API is not CLS-compliant.</b>

Converts a 16-bit unsigned integer to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>inputUInt16</i>: The number to convert as a 16-bit unsigned integer.

<b>Return Value:</b>

The value of  <i>inputUInt16</i>
 as an arbitrary-precision integer.

<a id="op_Increment"></a>
### Operator `++`

    public static PeterO.Numbers.EInteger operator ++(
        PeterO.Numbers.EInteger bthis);

Adds one to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>bthis</i>: An arbitrary-precision integer.

<b>Return Value:</b>

The given arbitrary-precision integer plus one.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_LeftShift"></a>
### Operator `<<`

    public static PeterO.Numbers.EInteger operator <<(
        PeterO.Numbers.EInteger bthis,
        int bitCount);

Returns an arbitrary-precision integer with the bits shifted to the left by a number of bits. A value of 1 doubles this value, a value of 2 multiplies it by 4, a value of 3 by 8, a value of 4 by 16, and so on.

<b>Parameters:</b>

 * <i>bthis</i>: The arbitrary-precision integer to shift left.

 * <i>bitCount</i>: The number of bits to shift. Can be negative, in which case this is the same as shiftRight with the absolute value of this parameter.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_LessThan"></a>
### Operator `<`

    public static bool operator <(
        PeterO.Numbers.EInteger thisValue,
        PeterO.Numbers.EInteger otherValue);

Determines whether an arbitrary-precision integer is less than another arbitrary-precision integer.

<b>Parameters:</b>

 * <i>thisValue</i>: The first arbitrary-precision integer.

 * <i>otherValue</i>: The second arbitrary-precision integer.

<b>Return Value:</b>

 `true`  if  <i>thisValue</i>
 is less than  <i>otherValue</i>
 ; otherwise,  `false` .

<a id="op_LessThanOrEqual"></a>
### Operator `<=`

    public static bool operator <=(
        PeterO.Numbers.EInteger thisValue,
        PeterO.Numbers.EInteger otherValue);

Determines whether an arbitrary-precision integer is up to another arbitrary-precision integer.

<b>Parameters:</b>

 * <i>thisValue</i>: The first arbitrary-precision integer.

 * <i>otherValue</i>: The second arbitrary-precision integer.

<b>Return Value:</b>

 `true`  if  <i>thisValue</i>
 is up to  <i>otherValue</i>
 ; otherwise,  `false` .

<a id="op_Modulus"></a>
### Operator `%`

    public static PeterO.Numbers.EInteger operator %(
        PeterO.Numbers.EInteger dividend,
        PeterO.Numbers.EInteger divisor);

Finds the remainder that results when an arbitrary-precision integer is divided by the value of another arbitrary-precision integer.

<b>Parameters:</b>

 * <i>dividend</i>: The first operand.

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

The remainder of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>dividend</i>
 is null.

<a id="op_Multiply"></a>
### Operator `*`

    public static PeterO.Numbers.EInteger operator *(
        PeterO.Numbers.EInteger operand1,
        PeterO.Numbers.EInteger operand2);

Multiplies an arbitrary-precision integer by the value of an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>operand1</i>: The first operand.

 * <i>operand2</i>: The second operand.

<b>Return Value:</b>

The product of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>operand1</i>
 is null.

<a id="op_OnesComplement"></a>
### Operator `~`

    public static PeterO.Numbers.EInteger operator ~(
        PeterO.Numbers.EInteger thisValue);

Returns an arbitrary-precision integer with every bit flipped.

<b>Parameters:</b>

 * <i>thisValue</i>: The operand as an arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>thisValue</i>
 is null.

<a id="op_RightShift"></a>
### Operator `>>`

    public static PeterO.Numbers.EInteger operator >>(
        PeterO.Numbers.EInteger bthis,
        int smallValue);

Shifts the bits of an arbitrary-precision integer to the right.

For this operation, the arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ). Thus, for negative values, the arbitrary-precision integer is sign-extended.

<b>Parameters:</b>

 * <i>bthis</i>: Another arbitrary-precision integer.

 * <i>smallValue</i>: The parameter  <i>smallValue</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_Subtraction"></a>
### Operator `-`

    public static PeterO.Numbers.EInteger operator -(
        PeterO.Numbers.EInteger bthis,
        PeterO.Numbers.EInteger subtrahend);

Subtracts two arbitrary-precision integer values.

<b>Parameters:</b>

 * <i>bthis</i>: An arbitrary-precision integer.

 * <i>subtrahend</i>: Another arbitrary-precision integer.

<b>Return Value:</b>

The difference of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_UnaryNegation"></a>
### Operator `-`

    public static PeterO.Numbers.EInteger operator -(
        PeterO.Numbers.EInteger bigValue);

Negates an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>bigValue</i>: An arbitrary-precision integer to negate.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="Or_PeterO_Numbers_EInteger"></a>
### Or

    public PeterO.Numbers.EInteger Or(
        PeterO.Numbers.EInteger second);

Does an OR operation between two arbitrary-precision integer instances.

Each arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) for the purposes of this operator.

<b>Parameters:</b>

 * <i>second</i>: The second operand.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>second</i>
 is null.

<a id="Or_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
### Or

    public static PeterO.Numbers.EInteger Or(
        PeterO.Numbers.EInteger first,
        PeterO.Numbers.EInteger second);

Does an OR operation between two arbitrary-precision integer instances.

Each arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) for the purposes of this operator.

<b>Parameters:</b>

 * <i>first</i>: The first operand.

 * <i>second</i>: The second operand.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="Pow_int"></a>
### Pow

    public PeterO.Numbers.EInteger Pow(
        int powerSmall);

Raises an arbitrary-precision integer to a power.

<b>Parameters:</b>

 * <i>powerSmall</i>: The exponent to raise this integer to.

<b>Return Value:</b>

The result. Returns 1 if  <i>powerSmall</i>
 is 0.

<a id="Pow_PeterO_Numbers_EInteger"></a>
### Pow

    public PeterO.Numbers.EInteger Pow(
        PeterO.Numbers.EInteger bigPower);

Raises an arbitrary-precision integer to a power.

<b>Parameters:</b>

 * <i>bigPower</i>: The exponent to raise this integer to.

<b>Return Value:</b>

The result. Returns 1 if  <i>bigPower</i>
 is 0.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigPower</i>
 is null.

 * System.ArgumentException:
BigPower is negative.

<a id="PowBigIntVar_PeterO_Numbers_EInteger"></a>
### PowBigIntVar

    public PeterO.Numbers.EInteger PowBigIntVar(
        PeterO.Numbers.EInteger power);

Raises an arbitrary-precision integer to a power, which is given as another arbitrary-precision integer.

<b>Parameters:</b>

 * <i>power</i>: The exponent to raise to.

<b>Return Value:</b>

The result. Returns 1 if  <i>power</i>
 is 0.

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter  <i>power</i>
 is less than 0.

 * System.ArgumentNullException:
The parameter  <i>power</i>
 is null.

<a id="Remainder_int"></a>
### Remainder

    public PeterO.Numbers.EInteger Remainder(
        int intValue);

Finds the remainder that results when this instance is divided by the value of an arbitrary-precision integer. The remainder is the value that remains when the absolute value of this object is divided by the absolute value of the other object; the remainder has the same sign (positive or negative) as this object.

<b>Parameters:</b>

 * <i>intValue</i>: The parameter  <i>intValue</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

The remainder of the two numbers.

<b>Exceptions:</b>

 * System.DivideByZeroException:
Attempted to divide by zero.

 * System.ArgumentNullException:
The parameter  <i>intValue</i>
 is null.

<a id="Remainder_PeterO_Numbers_EInteger"></a>
### Remainder

    public PeterO.Numbers.EInteger Remainder(
        PeterO.Numbers.EInteger divisor);

Finds the remainder that results when this instance is divided by the value of an arbitrary-precision integer. The remainder is the value that remains when the absolute value of this object is divided by the absolute value of the other object; the remainder has the same sign (positive or negative) as this object.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

The remainder of the two numbers.

<b>Exceptions:</b>

 * System.DivideByZeroException:
Attempted to divide by zero.

 * System.ArgumentNullException:
The parameter  <i>divisor</i>
 is null.

<a id="ShiftLeft_int"></a>
### ShiftLeft

    public PeterO.Numbers.EInteger ShiftLeft(
        int numberBits);

Returns an arbitrary-precision integer with the bits shifted to the left by a number of bits. A value of 1 doubles this value, a value of 2 multiplies it by 4, a value of 3 by 8, a value of 4 by 16, and so on.

<b>Parameters:</b>

 * <i>numberBits</i>: The number of bits to shift. Can be negative, in which case this is the same as shiftRight with the absolute value of this parameter.

<b>Return Value:</b>

An arbitrary-precision integer.

<a id="ShiftLeft_PeterO_Numbers_EInteger"></a>
### ShiftLeft

    public PeterO.Numbers.EInteger ShiftLeft(
        PeterO.Numbers.EInteger eshift);

Returns an arbitrary-precision integer with the bits shifted to the left by a number of bits given as an arbitrary-precision integer. A value of 1 doubles this value, a value of 2 multiplies it by 4, a value of 3 by 8, a value of 4 by 16, and so on.

<b>Parameters:</b>

 * <i>eshift</i>: The number of bits to shift. Can be negative, in which case this is the same as shiftRight with the absolute value of this parameter.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>eshift</i>
 is null.

<a id="ShiftRight_int"></a>
### ShiftRight

    public PeterO.Numbers.EInteger ShiftRight(
        int numberBits);

Returns an arbitrary-precision integer with the bits shifted to the right. For this operation, the arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ). Thus, for negative values, the arbitrary-precision integer is sign-extended.

<b>Parameters:</b>

 * <i>numberBits</i>: The number of bits to shift. Can be negative, in which case this is the same as shiftLeft with the absolute value of this parameter.

<b>Return Value:</b>

An arbitrary-precision integer.

<a id="ShiftRight_PeterO_Numbers_EInteger"></a>
### ShiftRight

    public PeterO.Numbers.EInteger ShiftRight(
        PeterO.Numbers.EInteger eshift);

Returns an arbitrary-precision integer with the bits shifted to the right. For this operation, the arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ). Thus, for negative values, the arbitrary-precision integer is sign-extended.

<b>Parameters:</b>

 * <i>eshift</i>: The number of bits to shift. Can be negative, in which case this is the same as shiftLeft with the absolute value of this parameter.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>eshift</i>
 is null.

<a id="Sqrt"></a>
### Sqrt

    public PeterO.Numbers.EInteger Sqrt();

Finds the square root of this instance's value, rounded down.

<b>Return Value:</b>

The square root of this object's value. Returns 0 if this value is 0 or less.

<a id="SqrtRem"></a>
### SqrtRem

    public PeterO.Numbers.EInteger[] SqrtRem();

Calculates the square root and the remainder.

<b>Return Value:</b>

An array of two arbitrary-precision integers: the first integer is the square root, and the second is the difference between this value and the square of the first integer. Returns two zeros if this value is 0 or less, or one and zero if this value equals 1.

<a id="Subtract_int"></a>
### Subtract

    public PeterO.Numbers.EInteger Subtract(
        int intValue);

Subtracts an arbitrary-precision integer from this arbitrary-precision integer.

<b>Parameters:</b>

 * <i>intValue</i>: The parameter  <i>intValue</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

The difference of the two objects.

<a id="Subtract_PeterO_Numbers_EInteger"></a>
### Subtract

    public PeterO.Numbers.EInteger Subtract(
        PeterO.Numbers.EInteger subtrahend);

Subtracts an arbitrary-precision integer from this arbitrary-precision integer.

<b>Parameters:</b>

 * <i>subtrahend</i>: Another arbitrary-precision integer.

<b>Return Value:</b>

The difference of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>subtrahend</i>
 is null.

<a id="ToByteChecked"></a>
### ToByteChecked

    public byte ToByteChecked();

Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255).

<b>Return Value:</b>

This number's value as a byte (from 0 to 255).

<b>Exceptions:</b>

 * System.OverflowException:
This value is less than 0 or greater than 255.

<a id="ToBytes_bool"></a>
### ToBytes

    public byte[] ToBytes(
        bool littleEndian);

Returns a byte array of this integer's value. The byte array will take the number's two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ), using the fewest bytes necessary to store its value unambiguously. If this value is negative, the bits that appear beyond the most significant bit of the number will be all ones. The resulting byte array can be passed to the  `FromBytes()`  method (with the same byte order) to reconstruct this integer's value.

<b>Parameters:</b>

 * <i>littleEndian</i>: Either  `true`  or  `false` .

<b>Return Value:</b>

A byte array. If this value is 0, returns a byte array with the single element 0.

<a id="ToByteUnchecked"></a>
### ToByteUnchecked

    public byte ToByteUnchecked();

Converts this number to a byte (from 0 to 255), returning the least-significant bits of this number's two's-complement form.

<b>Return Value:</b>

This number, converted to a byte (from 0 to 255).

<a id="ToInt16Checked"></a>
### ToInt16Checked

    public short ToInt16Checked();

Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer.

<b>Return Value:</b>

This number's value as a 16-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is less than -32768 or greater than 32767.

<a id="ToInt16Unchecked"></a>
### ToInt16Unchecked

    public short ToInt16Unchecked();

Converts this number to a 16-bit signed integer, returning the least-significant bits of this number's two's-complement form.

<b>Return Value:</b>

This number, converted to a 16-bit signed integer.

<a id="ToInt32Checked"></a>
### ToInt32Checked

    public int ToInt32Checked();

Converts this object's value to a 32-bit signed integer, throwing an exception if it can't fit.

<b>Return Value:</b>

A 32-bit signed integer.

<b>Exceptions:</b>

 *  T:System.OverflowException:
This object's value is too big to fit a 32-bit signed integer.

<a id="ToInt32Unchecked"></a>
### ToInt32Unchecked

    public int ToInt32Unchecked();

Converts this object's value to a 32-bit signed integer. If the value can't fit in a 32-bit integer, returns the lower 32 bits of this object's two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) (in which case the return value might have a different sign than this object's value).

<b>Return Value:</b>

A 32-bit signed integer.

<a id="ToInt64Checked"></a>
### ToInt64Checked

    public long ToInt64Checked();

Converts this object's value to a 64-bit signed integer, throwing an exception if it can't fit.

<b>Return Value:</b>

A 64-bit signed integer.

<b>Exceptions:</b>

 *  T:System.OverflowException:
This object's value is too big to fit a 64-bit signed integer.

<a id="ToInt64Unchecked"></a>
### ToInt64Unchecked

    public long ToInt64Unchecked();

Converts this object's value to a 64-bit signed integer. If the value can't fit in a 64-bit integer, returns the lower 64 bits of this object's two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) (in which case the return value might have a different sign than this object's value).

<b>Return Value:</b>

A 64-bit signed integer.

<a id="ToRadixString_int"></a>
### ToRadixString

    public string ToRadixString(
        int radix);

Generates a string representing the value of this object, in the given radix.

<b>Parameters:</b>

 * <i>radix</i>: A radix from 2 through 36. For example, to generate a hexadecimal (base-16) string, specify 16. To generate a decimal (base-10) string, specify 10.

<b>Return Value:</b>

A string representing the value of this object. If this value is 0, returns "0". If negative, the string will begin with a minus sign ("-", U+002D). Depending on the radix, the string will use the basic digits 0 to 9 (U+0030 to U+0039) and then the basic upper-case letters A to Z (U+0041 to U+005A). For example, 0-9 in radix 10, and 0-9, then A-F in radix 16.

<a id="ToSByteChecked"></a>
### ToSByteChecked

    public sbyte ToSByteChecked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer.

<b>Return Value:</b>

This number's value as an 8-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is less than -128 or greater than 127.

<a id="ToSByteUnchecked"></a>
### ToSByteUnchecked

    public sbyte ToSByteUnchecked();

<b>This API is not CLS-compliant.</b>

Converts this number to an 8-bit signed integer, returning the least-significant bits of this number's two's-complement form.

<b>Return Value:</b>

This number, converted to an 8-bit signed integer.

<a id="ToString"></a>
### ToString

    public override string ToString();

Converts this object to a text string in base 10.

<b>Return Value:</b>

A string representation of this object. If negative, the string will begin with a minus sign ("-", U+002D). The string will use the basic digits 0 to 9 (U+0030 to U+0039).

<a id="ToUInt16Checked"></a>
### ToUInt16Checked

    public ushort ToUInt16Checked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer.

<b>Return Value:</b>

This number's value as a 16-bit unsigned integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is less than 0 or greater than 65535.

<a id="ToUInt16Unchecked"></a>
### ToUInt16Unchecked

    public ushort ToUInt16Unchecked();

<b>This API is not CLS-compliant.</b>

Converts this number to a 16-bit unsigned integer, returning the least-significant bits of this number's two's-complement form.

<b>Return Value:</b>

This number, converted to a 16-bit unsigned integer.

<a id="ToUInt32Checked"></a>
### ToUInt32Checked

    public uint ToUInt32Checked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer.

<b>Return Value:</b>

This number's value as a 32-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is less than 0 or greater than 4294967295.

<a id="ToUInt32Unchecked"></a>
### ToUInt32Unchecked

    public uint ToUInt32Unchecked();

<b>This API is not CLS-compliant.</b>

Converts this number to a 32-bit signed integer, returning the least-significant bits of this number's two's-complement form.

<b>Return Value:</b>

This number, converted to a 32-bit signed integer.

<a id="ToUInt64Checked"></a>
### ToUInt64Checked

    public ulong ToUInt64Checked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer.

<b>Return Value:</b>

This number's value as a 64-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is outside the range of a 64-bit signed integer.

<a id="ToUInt64Unchecked"></a>
### ToUInt64Unchecked

    public ulong ToUInt64Unchecked();

<b>This API is not CLS-compliant.</b>

Converts this number to a 64-bit signed integer, returning the least-significant bits of this number's two's-complement form.

<b>Return Value:</b>

This number, converted to a 64-bit signed integer.

<a id="Xor_PeterO_Numbers_EInteger"></a>
### Xor

    public PeterO.Numbers.EInteger Xor(
        PeterO.Numbers.EInteger other);

Finds the exclusive "or" of two arbitrary-precision integer objects. Each arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) for the purposes of this operator.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer in which each bit is set if the corresponding bit is set in one input integer but not in the other.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>other</i>
 is null.

<a id="Xor_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
### Xor

    public static PeterO.Numbers.EInteger Xor(
        PeterO.Numbers.EInteger a,
        PeterO.Numbers.EInteger b);

Finds the exclusive "or" of two arbitrary-precision integer objects. Each arbitrary-precision integer is treated as a two's-complement form (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) for the purposes of this operator.

<b>Parameters:</b>

 * <i>a</i>: The first arbitrary-precision integer.

 * <i>b</i>: The second arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer in which each bit is set if the corresponding bit is set in one input integer but not in the other.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>a</i>
 or  <i>b</i>
 is null.
