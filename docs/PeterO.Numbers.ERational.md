## PeterO.Numbers.ERational

    public sealed class ERational :
        System.IComparable,
        System.IEquatable

Represents an arbitrary-precision rational number. This class can't be inherited. (The "E" stands for "extended", meaning that instances of this class can be values other than numbers proper, such as infinity and not-a-number.) <b>Thread safety:</b> Instances of this class are immutable, so they are inherently safe for use by multiple threads. Multiple instances of this object with the same properties are interchangeable, so they should not be compared using the "==" operator (which might only check if each side of the operator is the same instance).

### Member Summary
* <code>[Abs()](#Abs)</code> - Returns the absolute value of this rational number, that is, a number with the same value as this one but as a nonnegative number.
* <code>[Add(int)](#Add_int)</code> - Returns the sum of a rational number and a 32-bit signed integer.
* <code>[Add(PeterO.Numbers.ERational)](#Add_PeterO_Numbers_ERational)</code> - Adds two rational numbers.
* <code>[CompareTo(int)](#CompareTo_int)</code> - Compares the mathematical value of an arbitrary-precision rational number with that of this instance.
* <code>[CompareTo(PeterO.Numbers.ERational)](#CompareTo_PeterO_Numbers_ERational)</code> - Compares the mathematical value of an arbitrary-precision rational number with that of this instance.
* <code>[CompareToBinary(PeterO.Numbers.EFloat)](#CompareToBinary_PeterO_Numbers_EFloat)</code> - Compares an arbitrary-precision binary floating-point number with this instance.
* <code>[CompareToDecimal(PeterO.Numbers.EDecimal)](#CompareToDecimal_PeterO_Numbers_EDecimal)</code> - Compares an arbitrary-precision decimal number with this instance.
* <code>[CompareToTotal(PeterO.Numbers.ERational)](#CompareToTotal_PeterO_Numbers_ERational)</code> - Compares the values of this object and another object, imposing a total ordering on all possible values.
* <code>[CompareToTotalMagnitude(PeterO.Numbers.ERational)](#CompareToTotalMagnitude_PeterO_Numbers_ERational)</code> - Compares the absolute values of this object and another object, imposing a total ordering on all possible values (ignoring their signs).
* <code>[CompareToValue(int)](#CompareToValue_int)</code> - Compares the mathematical value of an arbitrary-precision rational number with that of this instance.
* <code>[CompareToValue(PeterO.Numbers.ERational)](#CompareToValue_PeterO_Numbers_ERational)</code> - Compares the mathematical value of an arbitrary-precision rational number with that of this instance.
* <code>[Copy()](#Copy)</code> - Creates a copy of this arbitrary-precision rational number.
* <code>[CopySign(PeterO.Numbers.ERational)](#CopySign_PeterO_Numbers_ERational)</code> - Returns a number with the same value as this one, but copying the sign (positive or negative) of another number.
* <code>[Create(int, int)](#Create_int_int)</code> - Creates a rational number with the given numerator and denominator.
* <code>[Create(long, long)](#Create_long_long)</code> - Creates a rational number with the given numerator and denominator.
* <code>[Create(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#Create_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> - Creates a rational number with the given numerator and denominator.
* <code>[CreateNaN(PeterO.Numbers.EInteger)](#CreateNaN_PeterO_Numbers_EInteger)</code> - Creates a not-a-number arbitrary-precision rational number.
* <code>[CreateNaN(PeterO.Numbers.EInteger, bool, bool)](#CreateNaN_PeterO_Numbers_EInteger_bool_bool)</code> - Creates a not-a-number arbitrary-precision rational number.
* <code>[Decrement()](#Decrement)</code> - Subtracts one from an arbitrary-precision rational number.
* <code>[Denominator](#Denominator)</code> - Gets this object's denominator.
* <code>[Divide(int)](#Divide_int)</code> - Divides this instance by the value of an arbitrary-precision rational number object.
* <code>[Divide(PeterO.Numbers.ERational)](#Divide_PeterO_Numbers_ERational)</code> - Divides this instance by the value of an arbitrary-precision rational number object.
* <code>[Equals(object)](#Equals_object)</code> - Determines whether this object's numerator, denominator, and properties are equal to those of another object and that other object is an arbitrary-precision rational number.
* <code>[Equals(PeterO.Numbers.ERational)](#Equals_PeterO_Numbers_ERational)</code> - Determines whether this object's numerator, denominator, and properties are equal to those of another object.
* <code>[explicit operator byte(PeterO.Numbers.ERational)](#explicit_operator_byte_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) after converting it to an integer by discarding its fractional part.
* <code>[explicit operator decimal(PeterO.Numbers.ERational)](#explicit_operator_decimal_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a decimal under the Common Language Infrastructure (see T:PeterO.
* <code>[explicit operator double(PeterO.Numbers.ERational)](#explicit_operator_double_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a 64-bit floating-point number.
* <code>[explicit operator float(PeterO.Numbers.ERational)](#explicit_operator_float_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a 32-bit binary floating-point number.
* <code>[explicit operator int(PeterO.Numbers.ERational)](#explicit_operator_int_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator long(PeterO.Numbers.ERational)](#explicit_operator_long_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a 64-bit signed integer if it can fit in a 64-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator PeterO.Numbers.EInteger(PeterO.Numbers.ERational)](#explicit_operator_PeterO_Numbers_EInteger_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to an arbitrary-precision integer.
* <code>[explicit operator PeterO.Numbers.ERational(bool)](#explicit_operator_PeterO_Numbers_ERational_bool)</code> - Converts a boolean value (true or false) to an arbitrary-precision rational number.
* <code>[explicit operator sbyte(PeterO.Numbers.ERational)](#explicit_operator_sbyte_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to an 8-bit signed integer if it can fit in an 8-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator short(PeterO.Numbers.ERational)](#explicit_operator_short_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a 16-bit signed integer if it can fit in a 16-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator uint(PeterO.Numbers.ERational)](#explicit_operator_uint_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator ulong(PeterO.Numbers.ERational)](#explicit_operator_ulong_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator ushort(PeterO.Numbers.ERational)](#explicit_operator_ushort_PeterO_Numbers_ERational)</code> - Converts an arbitrary-precision rational number to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer after converting it to an integer by discarding its fractional part.
* <code>[FromBoolean(bool)](#FromBoolean_bool)</code> - Converts a boolean value (true or false) to an arbitrary-precision rational number.
* <code>[FromByte(byte)](#FromByte_byte)</code> - Converts a byte (from 0 to 255) to an arbitrary-precision rational number.
* <code>[FromDecimal(decimal)](#FromDecimal_decimal)</code> - Converts a decimal under the Common Language Infrastructure (usually a.
* <code>[FromDouble(double)](#FromDouble_double)</code> - Converts a 64-bit floating-point number to a rational number.
* <code>[FromEDecimal(PeterO.Numbers.EDecimal)](#FromEDecimal_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal number to a rational number.
* <code>[FromEFloat(PeterO.Numbers.EFloat)](#FromEFloat_PeterO_Numbers_EFloat)</code> - Converts an arbitrary-precision binary floating-point number to a rational number.
* <code>[FromEInteger(PeterO.Numbers.EInteger)](#FromEInteger_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to a rational number.
* <code>[FromExtendedDecimal(PeterO.Numbers.EDecimal)](#FromExtendedDecimal_PeterO_Numbers_EDecimal)</code> - <b>Deprecated:</b> Renamed to FromEDecimal.
* <code>[FromExtendedFloat(PeterO.Numbers.EFloat)](#FromExtendedFloat_PeterO_Numbers_EFloat)</code> - <b>Deprecated:</b> Renamed to FromEFloat.
* <code>[FromInt16(short)](#FromInt16_short)</code> - Converts a 16-bit signed integer to an arbitrary-precision rational number.
* <code>[FromInt32(int)](#FromInt32_int)</code> - Converts a 32-bit signed integer to an arbitrary-precision rational number.
* <code>[FromInt64(long)](#FromInt64_long)</code> - Converts a 64-bit signed integer to an arbitrary-precision rational number.
* <code>[FromSByte(sbyte)](#FromSByte_sbyte)</code> - Converts an 8-bit signed integer to an arbitrary-precision rational number.
* <code>[FromSingle(float)](#FromSingle_float)</code> - Converts a 32-bit binary floating-point number to a rational number.
* <code>[FromString(string)](#FromString_string)</code> - Creates a rational number from a text string that represents a number.
* <code>[FromString(string, int, int)](#FromString_string_int_int)</code> - Creates a rational number from a text string that represents a number.
* <code>[FromUInt16(ushort)](#FromUInt16_ushort)</code> - Converts a 16-bit unsigned integer to an arbitrary-precision rational number.
* <code>[FromUInt32(uint)](#FromUInt32_uint)</code> - Converts a 32-bit signed integer to an arbitrary-precision rational number.
* <code>[FromUInt64(ulong)](#FromUInt64_ulong)</code> - Converts a 64-bit unsigned integer to an arbitrary-precision rational number.
* <code>[GetHashCode()](#GetHashCode)</code> - Returns the hash code for this instance.
* <code>[implicit operator PeterO.Numbers.ERational(byte)](#implicit_operator_PeterO_Numbers_ERational_byte)</code> - Converts a byte (from 0 to 255) to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(decimal)](#implicit_operator_PeterO_Numbers_ERational_decimal)</code> - Converts a decimal under the Common Language Infrastructure (usually a.
* <code>[implicit operator PeterO.Numbers.ERational(double)](#implicit_operator_PeterO_Numbers_ERational_double)</code> - Converts a 64-bit floating-point number to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(float)](#implicit_operator_PeterO_Numbers_ERational_float)</code> - Converts a 32-bit binary floating-point number to a rational number.
* <code>[implicit operator PeterO.Numbers.ERational(int)](#implicit_operator_PeterO_Numbers_ERational_int)</code> - Converts a 32-bit signed integer to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(long)](#implicit_operator_PeterO_Numbers_ERational_long)</code> - Converts a 64-bit signed integer to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(PeterO.Numbers.EDecimal)](#implicit_operator_PeterO_Numbers_ERational_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal floating-point number to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(PeterO.Numbers.EFloat)](#implicit_operator_PeterO_Numbers_ERational_PeterO_Numbers_EFloat)</code> - Converts an arbitrary-precision binary floating-point number to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(PeterO.Numbers.EInteger)](#implicit_operator_PeterO_Numbers_ERational_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(sbyte)](#implicit_operator_PeterO_Numbers_ERational_sbyte)</code> - Converts an 8-bit signed integer to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(short)](#implicit_operator_PeterO_Numbers_ERational_short)</code> - Converts a 16-bit signed integer to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(uint)](#implicit_operator_PeterO_Numbers_ERational_uint)</code> - Converts a 32-bit signed integer to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(ulong)](#implicit_operator_PeterO_Numbers_ERational_ulong)</code> - Converts a 64-bit unsigned integer to an arbitrary-precision rational number.
* <code>[implicit operator PeterO.Numbers.ERational(ushort)](#implicit_operator_PeterO_Numbers_ERational_ushort)</code> - Converts a 16-bit unsigned integer to an arbitrary-precision rational number.
* <code>[Increment()](#Increment)</code> - Adds one to an arbitrary-precision rational number.
* <code>[IsFinite](#IsFinite)</code> - Gets a value indicating whether this object is finite (not infinity or NaN).
* <code>[IsInfinity()](#IsInfinity)</code> - Gets a value indicating whether this object's value is infinity.
* <code>[IsNaN()](#IsNaN)</code> - Returns whether this object is a not-a-number value.
* <code>[IsNegative](#IsNegative)</code> - Gets a value indicating whether this object's value is negative (including negative zero).
* <code>[IsNegativeInfinity()](#IsNegativeInfinity)</code> - Returns whether this object is negative infinity.
* <code>[IsPositiveInfinity()](#IsPositiveInfinity)</code> - Returns whether this object is positive infinity.
* <code>[IsQuietNaN()](#IsQuietNaN)</code> - Returns whether this object is a quiet not-a-number value.
* <code>[IsSignalingNaN()](#IsSignalingNaN)</code> - Returns whether this object is a signaling not-a-number value (which causes an error if the value is passed to any arithmetic operation in this class).
* <code>[IsZero](#IsZero)</code> - Gets a value indicating whether this object's value equals 0.
* <code>[Max(PeterO.Numbers.ERational, PeterO.Numbers.ERational)](#Max_PeterO_Numbers_ERational_PeterO_Numbers_ERational)</code> - Gets the greater value between two rational numbers.
* <code>[MaxMagnitude(PeterO.Numbers.ERational, PeterO.Numbers.ERational)](#MaxMagnitude_PeterO_Numbers_ERational_PeterO_Numbers_ERational)</code> - Gets the greater value between two values, ignoring their signs.
* <code>[Min(PeterO.Numbers.ERational, PeterO.Numbers.ERational)](#Min_PeterO_Numbers_ERational_PeterO_Numbers_ERational)</code> - Gets the lesser value between two rational numbers.
* <code>[MinMagnitude(PeterO.Numbers.ERational, PeterO.Numbers.ERational)](#MinMagnitude_PeterO_Numbers_ERational_PeterO_Numbers_ERational)</code> - Gets the lesser value between two values, ignoring their signs.
* <code>[Multiply(int)](#Multiply_int)</code> - Returns the value of this instance multiplied by a 32-bit signed integer.
* <code>[Multiply(PeterO.Numbers.ERational)](#Multiply_PeterO_Numbers_ERational)</code> - Multiplies this instance by the value of an arbitrary-precision rational number.
* <code>[public static readonly PeterO.Numbers.ERational NaN;](#NaN)</code> - A not-a-number value.
* <code>[Negate()](#Negate)</code> - Returns a rational number with the same value as this one but with the sign reversed.
* <code>[public static readonly PeterO.Numbers.ERational NegativeInfinity;](#NegativeInfinity)</code> - Negative infinity, less than any other number.
* <code>[public static readonly PeterO.Numbers.ERational NegativeZero;](#NegativeZero)</code> - A rational number for negative zero.
* <code>[Numerator](#Numerator)</code> - Gets this object's numerator.
* <code>[public static readonly PeterO.Numbers.ERational One;](#One)</code> - The rational number one.
* <code>[PeterO.Numbers.ERational operator +(PeterO.Numbers.ERational, PeterO.Numbers.ERational)](#op_Addition)</code> - Adds two rational numbers.
* <code>[PeterO.Numbers.ERational operator --(PeterO.Numbers.ERational)](#op_Decrement)</code> - Subtracts one from an arbitrary-precision rational number.
* <code>[PeterO.Numbers.ERational operator /(PeterO.Numbers.ERational, PeterO.Numbers.ERational)](#op_Division)</code> - Divides an arbitrary-precision rational number by the value of another arbitrary-precision rational number object.
* <code>[PeterO.Numbers.ERational operator ++(PeterO.Numbers.ERational)](#op_Increment)</code> - Adds one to an arbitrary-precision rational number.
* <code>[PeterO.Numbers.ERational operator %(PeterO.Numbers.ERational, PeterO.Numbers.ERational)](#op_Modulus)</code> - Finds the remainder that results when this instance is divided by the value of an arbitrary-precision rational number.
* <code>[PeterO.Numbers.ERational operator &#x2a;(PeterO.Numbers.ERational, PeterO.Numbers.ERational)](#op_Multiply)</code> - Multiplies this instance by the value of an arbitrary-precision rational number.
* <code>[PeterO.Numbers.ERational operator -(PeterO.Numbers.ERational, PeterO.Numbers.ERational)](#op_Subtraction)</code> - Subtracts an arbitrary-precision rational number from this instance.
* <code>[PeterO.Numbers.ERational operator -(PeterO.Numbers.ERational)](#op_UnaryNegation)</code> - Returns an arbitrary-precision rational number with the same value as the given one but with its sign reversed.
* <code>[public static readonly PeterO.Numbers.ERational PositiveInfinity;](#PositiveInfinity)</code> - Positive infinity, greater than any other number.
* <code>[Remainder(int)](#Remainder_int)</code> - Finds the remainder that results when this instance is divided by the value of an arbitrary-precision rational number.
* <code>[Remainder(PeterO.Numbers.ERational)](#Remainder_PeterO_Numbers_ERational)</code> - Finds the remainder that results when this instance is divided by the value of an arbitrary-precision rational number.
* <code>[Sign](#Sign)</code> - Gets the sign of this rational number.
* <code>[public static readonly PeterO.Numbers.ERational SignalingNaN;](#SignalingNaN)</code> - A signaling not-a-number value.
* <code>[Subtract(int)](#Subtract_int)</code> - Returns the result of subtracting a 32-bit signed integer from this instance.
* <code>[Subtract(PeterO.Numbers.ERational)](#Subtract_PeterO_Numbers_ERational)</code> - Subtracts an arbitrary-precision rational number from this instance.
* <code>[public static readonly PeterO.Numbers.ERational Ten;](#Ten)</code> - The rational number ten.
* <code>[ToByteChecked()](#ToByteChecked)</code> - Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) after converting it to an integer by discarding its fractional part.
* <code>[ToByteIfExact()](#ToByteIfExact)</code> - Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) without rounding to a different numerical value.
* <code>[ToByteUnchecked()](#ToByteUnchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a byte (from 0 to 255).
* <code>[ToDecimal()](#ToDecimal)</code> - Converts this value to a decimal under the Common Language Infrastructure (usually a.
* <code>[ToDouble()](#ToDouble)</code> - Converts this value to a 64-bit floating-point number.
* <code>[ToEDecimal()](#ToEDecimal)</code> - Converts this rational number to an arbitrary-precision decimal number.
* <code>[ToEDecimal(PeterO.Numbers.EContext)](#ToEDecimal_PeterO_Numbers_EContext)</code> - Converts this rational number to an arbitrary-precision decimal number and rounds the result to the given precision.
* <code>[ToEDecimalExactIfPossible(PeterO.Numbers.EContext)](#ToEDecimalExactIfPossible_PeterO_Numbers_EContext)</code> - Converts this rational number to an arbitrary-precision decimal number, but if the result would have a nonterminating decimal expansion, rounds that result to the given precision.
* <code>[ToEFloat()](#ToEFloat)</code> - Converts this rational number to a binary floating-point number.
* <code>[ToEFloat(PeterO.Numbers.EContext)](#ToEFloat_PeterO_Numbers_EContext)</code> - Converts this rational number to a binary floating-point number and rounds that result to the given precision.
* <code>[ToEFloatExactIfPossible(PeterO.Numbers.EContext)](#ToEFloatExactIfPossible_PeterO_Numbers_EContext)</code> - Converts this rational number to a binary floating-point number, but if the result would have a nonterminating binary expansion, rounds that result to the given precision.
* <code>[ToEInteger()](#ToEInteger)</code> - Converts this value to an arbitrary-precision integer by dividing the numerator by the denominator and discarding the fractional part of the result.
* <code>[ToEIntegerExact()](#ToEIntegerExact)</code> - <b>Deprecated:</b> Renamed to ToEIntegerIfExact.
* <code>[ToEIntegerIfExact()](#ToEIntegerIfExact)</code> - Converts this value to an arbitrary-precision integer, checking whether the value is an exact integer.
* <code>[ToExtendedDecimal()](#ToExtendedDecimal)</code> - <b>Deprecated:</b> Renamed to ToEDecimal.
* <code>[ToExtendedDecimal(PeterO.Numbers.EContext)](#ToExtendedDecimal_PeterO_Numbers_EContext)</code> - <b>Deprecated:</b> Renamed to ToEDecimal.
* <code>[ToExtendedDecimalExactIfPossible(PeterO.Numbers.EContext)](#ToExtendedDecimalExactIfPossible_PeterO_Numbers_EContext)</code> - <b>Deprecated:</b> Renamed to ToEDecimalExactIfPossible.
* <code>[ToExtendedFloat()](#ToExtendedFloat)</code> - <b>Deprecated:</b> Renamed to ToEFloat.
* <code>[ToExtendedFloat(PeterO.Numbers.EContext)](#ToExtendedFloat_PeterO_Numbers_EContext)</code> - <b>Deprecated:</b> Renamed to ToEFloat.
* <code>[ToExtendedFloatExactIfPossible(PeterO.Numbers.EContext)](#ToExtendedFloatExactIfPossible_PeterO_Numbers_EContext)</code> - <b>Deprecated:</b> Renamed to ToEFloatExactIfPossible.
* <code>[ToInt16Checked()](#ToInt16Checked)</code> - Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToInt16IfExact()](#ToInt16IfExact)</code> - Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer without rounding to a different numerical value.
* <code>[ToInt16Unchecked()](#ToInt16Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 16-bit signed integer.
* <code>[ToInt32Checked()](#ToInt32Checked)</code> - Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToInt32IfExact()](#ToInt32IfExact)</code> - Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer without rounding to a different numerical value.
* <code>[ToInt32Unchecked()](#ToInt32Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 32-bit signed integer.
* <code>[ToInt64Checked()](#ToInt64Checked)</code> - Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToInt64IfExact()](#ToInt64IfExact)</code> - Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer without rounding to a different numerical value.
* <code>[ToInt64Unchecked()](#ToInt64Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 64-bit signed integer.
* <code>[ToSByteChecked()](#ToSByteChecked)</code> - Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToSByteIfExact()](#ToSByteIfExact)</code> - Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer without rounding to a different numerical value.
* <code>[ToSByteUnchecked()](#ToSByteUnchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as an 8-bit signed integer.
* <code>[ToSingle()](#ToSingle)</code> - Converts this value to a 32-bit binary floating-point number.
* <code>[ToString()](#ToString)</code> - Converts this object to a text string.
* <code>[ToUInt16Checked()](#ToUInt16Checked)</code> - Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer after converting it to an integer by discarding its fractional part.
* <code>[ToUInt16IfExact()](#ToUInt16IfExact)</code> - Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer without rounding to a different numerical value.
* <code>[ToUInt16Unchecked()](#ToUInt16Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 16-bit unsigned integer.
* <code>[ToUInt32Checked()](#ToUInt32Checked)</code> - Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToUInt32IfExact()](#ToUInt32IfExact)</code> - Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer without rounding to a different numerical value.
* <code>[ToUInt32Unchecked()](#ToUInt32Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 32-bit signed integer.
* <code>[ToUInt64Checked()](#ToUInt64Checked)</code> - Converts this number's value to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer after converting it to an integer by discarding its fractional part.
* <code>[ToUInt64IfExact()](#ToUInt64IfExact)</code> - Converts this number's value to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer without rounding to a different numerical value.
* <code>[ToUInt64Unchecked()](#ToUInt64Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 64-bit unsigned integer.
* <code>[UnsignedNumerator](#UnsignedNumerator)</code> - Gets this object's numerator with the sign removed.
* <code>[public static readonly PeterO.Numbers.ERational Zero;](#Zero)</code> - A rational number for zero.

<a id="Void_ctor_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
### ERational Constructor

    public ERational(
        PeterO.Numbers.EInteger numerator,
        PeterO.Numbers.EInteger denominator);

Initializes a new instance of the [PeterO.Numbers.ERational](PeterO.Numbers.ERational.md) class.

<b>Parameters:</b>

 * <i>numerator</i>: An arbitrary-precision integer serving as the numerator.

 * <i>denominator</i>: An arbitrary-precision integer serving as the denominator.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>numerator</i>
 or  <i>denominator</i>
 is null.

 * System.ArgumentException:
Denominator is zero.

<a id="NaN"></a>
### NaN

    public static readonly PeterO.Numbers.ERational NaN;

A not-a-number value.

<a id="NegativeInfinity"></a>
### NegativeInfinity

    public static readonly PeterO.Numbers.ERational NegativeInfinity;

Negative infinity, less than any other number.

<a id="NegativeZero"></a>
### NegativeZero

    public static readonly PeterO.Numbers.ERational NegativeZero;

A rational number for negative zero.

<a id="One"></a>
### One

    public static readonly PeterO.Numbers.ERational One;

The rational number one.

<a id="PositiveInfinity"></a>
### PositiveInfinity

    public static readonly PeterO.Numbers.ERational PositiveInfinity;

Positive infinity, greater than any other number.

<a id="SignalingNaN"></a>
### SignalingNaN

    public static readonly PeterO.Numbers.ERational SignalingNaN;

A signaling not-a-number value.

<a id="Ten"></a>
### Ten

    public static readonly PeterO.Numbers.ERational Ten;

The rational number ten.

<a id="Zero"></a>
### Zero

    public static readonly PeterO.Numbers.ERational Zero;

A rational number for zero.

<a id="Denominator"></a>
### Denominator

    public PeterO.Numbers.EInteger Denominator { get; }

Gets this object's denominator.

<b>Returns:</b>

This object's denominator.

<a id="IsFinite"></a>
### IsFinite

    public bool IsFinite { get; }

Gets a value indicating whether this object is finite (not infinity or NaN).

<b>Returns:</b>

 `true`  if this object is finite (not infinity or NaN); otherwise,  `false` .

<a id="IsNegative"></a>
### IsNegative

    public bool IsNegative { get; }

Gets a value indicating whether this object's value is negative (including negative zero).

<b>Returns:</b>

 `true`  if this object's value is negative (including negative zero); otherwise,  `false` .  `true`  if this object's value is negative; otherwise,  `false` .

<a id="IsZero"></a>
### IsZero

    public bool IsZero { get; }

Gets a value indicating whether this object's value equals 0.

<b>Returns:</b>

 `true`  if this object's value equals 0; otherwise,  `false` .  `true`  if this object's value equals 0; otherwise,  `false` .

<a id="Numerator"></a>
### Numerator

    public PeterO.Numbers.EInteger Numerator { get; }

Gets this object's numerator.

<b>Returns:</b>

This object's numerator. If this object is a not-a-number value, returns the diagnostic information (which will be negative if this object is negative).

<a id="Sign"></a>
### Sign

    public int Sign { get; }

Gets the sign of this rational number.

<b>Returns:</b>

The sign of this rational number.

<a id="UnsignedNumerator"></a>
### UnsignedNumerator

    public PeterO.Numbers.EInteger UnsignedNumerator { get; }

Gets this object's numerator with the sign removed.

<b>Returns:</b>

This object's numerator. If this object is a not-a-number value, returns the diagnostic information.

<a id="Abs"></a>
### Abs

    public PeterO.Numbers.ERational Abs();

Returns the absolute value of this rational number, that is, a number with the same value as this one but as a nonnegative number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="Add_int"></a>
### Add

    public PeterO.Numbers.ERational Add(
        int v);

Returns the sum of a rational number and a 32-bit signed integer.

<b>Parameters:</b>

 * <i>v</i>: A 32-bit signed integer.

<b>Return Value:</b>

The sum of the two numbers. Returns not-a-number (NaN) if this object is NaN.

<a id="Add_PeterO_Numbers_ERational"></a>
### Add

    public PeterO.Numbers.ERational Add(
        PeterO.Numbers.ERational otherValue);

Adds two rational numbers.

<b>Parameters:</b>

 * <i>otherValue</i>: Another arbitrary-precision rational number.

<b>Return Value:</b>

The sum of the two numbers. Returns not-a-number (NaN) if either operand is NaN.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>otherValue</i>
 is null.

<a id="CompareTo_int"></a>
### CompareTo

    public int CompareTo(
        int intOther);

Compares the mathematical value of an arbitrary-precision rational number with that of this instance. This method currently uses the rules given in the CompareToValue method, so that it it is not consistent with the Equals method, but it may change in a future version to use the rules for the CompareToTotal method instead.

<b>Parameters:</b>

 * <i>intOther</i>: The parameter  <i>intOther</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater.

<a id="CompareTo_PeterO_Numbers_ERational"></a>
### CompareTo

    public sealed int CompareTo(
        PeterO.Numbers.ERational other);

Compares the mathematical value of an arbitrary-precision rational number with that of this instance. This method currently uses the rules given in the CompareToValue method, so that it it is not consistent with the Equals method, but it may change in a future version to use the rules for the CompareToTotal method instead.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToBinary_PeterO_Numbers_EFloat"></a>
### CompareToBinary

    public int CompareToBinary(
        PeterO.Numbers.EFloat other);

Compares an arbitrary-precision binary floating-point number with this instance. In this method, NaN values are greater than any other ERational or EFloat value, and two NaN values (even if their payloads differ) are treated as equal by this method.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision binary floating-point number.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToDecimal_PeterO_Numbers_EDecimal"></a>
### CompareToDecimal

    public int CompareToDecimal(
        PeterO.Numbers.EDecimal other);

Compares an arbitrary-precision decimal number with this instance.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToTotal_PeterO_Numbers_ERational"></a>
### CompareToTotal

    public int CompareToTotal(
        PeterO.Numbers.ERational other);

Compares the values of this object and another object, imposing a total ordering on all possible values. In this method:

 * For objects with the same value, the one with the higher denominator has a greater "absolute value".

 * Negative zero is less than positive zero.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

 * Negative numbers are less than positive numbers.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision rational number to compare with this one.

<b>Return Value:</b>

The number 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToTotalMagnitude_PeterO_Numbers_ERational"></a>
### CompareToTotalMagnitude

    public int CompareToTotalMagnitude(
        PeterO.Numbers.ERational other);

Compares the absolute values of this object and another object, imposing a total ordering on all possible values (ignoring their signs). In this method:

 * For objects with the same value, the one with the higher denominator has a greater "absolute value".

 * Negative zero and positive zero are considered equal.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision rational number to compare with this one.

<b>Return Value:</b>

The number 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToValue_int"></a>
### CompareToValue

    public int CompareToValue(
        int intOther);

Compares the mathematical value of an arbitrary-precision rational number with that of this instance. In this method, NaN values are greater than any other ERational value, and two NaN values (even if their payloads differ) are treated as equal by this method. This method is not consistent with the Equals method.

<b>Parameters:</b>

 * <i>intOther</i>: The parameter  <i>intOther</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater.

<a id="CompareToValue_PeterO_Numbers_ERational"></a>
### CompareToValue

    public int CompareToValue(
        PeterO.Numbers.ERational other);

Compares the mathematical value of an arbitrary-precision rational number with that of this instance. In this method, NaN values are greater than any other ERational value, and two NaN values (even if their payloads differ) are treated as equal by this method. This method is not consistent with the Equals method.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="Copy"></a>
### Copy

    public PeterO.Numbers.ERational Copy();

Creates a copy of this arbitrary-precision rational number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="CopySign_PeterO_Numbers_ERational"></a>
### CopySign

    public PeterO.Numbers.ERational CopySign(
        PeterO.Numbers.ERational other);

Returns a number with the same value as this one, but copying the sign (positive or negative) of another number.

<b>Parameters:</b>

 * <i>other</i>: A number whose sign will be copied.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>other</i>
 is null.

<a id="Create_int_int"></a>
### Create

    public static PeterO.Numbers.ERational Create(
        int numeratorSmall,
        int denominatorSmall);

Creates a rational number with the given numerator and denominator.

<b>Parameters:</b>

 * <i>numeratorSmall</i>: The numerator.

 * <i>denominatorSmall</i>: The denominator.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentException:
The denominator is zero.

<a id="Create_long_long"></a>
### Create

    public static PeterO.Numbers.ERational Create(
        long numeratorLong,
        long denominatorLong);

Creates a rational number with the given numerator and denominator.

<b>Parameters:</b>

 * <i>numeratorLong</i>: The numerator.

 * <i>denominatorLong</i>: The denominator.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentException:
The denominator is zero.

<a id="Create_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
### Create

    public static PeterO.Numbers.ERational Create(
        PeterO.Numbers.EInteger numerator,
        PeterO.Numbers.EInteger denominator);

Creates a rational number with the given numerator and denominator.

<b>Parameters:</b>

 * <i>numerator</i>: The numerator.

 * <i>denominator</i>: The denominator.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentException:
The denominator is zero.

<a id="CreateNaN_PeterO_Numbers_EInteger"></a>
### CreateNaN

    public static PeterO.Numbers.ERational CreateNaN(
        PeterO.Numbers.EInteger diag);

Creates a not-a-number arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>diag</i>: An integer, 0 or greater, to use as diagnostic information associated with this object. If none is needed, should be zero. To get the diagnostic information from another arbitrary-precision rational number, use that object's  `UnsignedNumerator`  property.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter  <i>diag</i>
 is less than 0.

<a id="CreateNaN_PeterO_Numbers_EInteger_bool_bool"></a>
### CreateNaN

    public static PeterO.Numbers.ERational CreateNaN(
        PeterO.Numbers.EInteger diag,
        bool signaling,
        bool negative);

Creates a not-a-number arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>diag</i>: An integer, 0 or greater, to use as diagnostic information associated with this object. If none is needed, should be zero. To get the diagnostic information from another arbitrary-precision rational number, use that object's  `UnsignedNumerator`  property.

 * <i>signaling</i>: Whether the return value will be signaling (true) or quiet (false).

 * <i>negative</i>: Whether the return value is negative.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter  <i>diag</i>
 is less than 0.

 * System.ArgumentNullException:
The parameter  <i>diag</i>
 is null.

<a id="Decrement"></a>
### Decrement

    public PeterO.Numbers.ERational Decrement();

Subtracts one from an arbitrary-precision rational number.

<b>Return Value:</b>

The given arbitrary-precision rational number minus one.

<a id="Divide_int"></a>
### Divide

    public PeterO.Numbers.ERational Divide(
        int v);

Divides this instance by the value of an arbitrary-precision rational number object.

<b>Parameters:</b>

 * <i>v</i>: The parameter  <i>v</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

The quotient of the two objects.

<b>Exceptions:</b>

 * System.ArithmeticException:
The parameter  <i>v</i>
 is zero.

<a id="Divide_PeterO_Numbers_ERational"></a>
### Divide

    public PeterO.Numbers.ERational Divide(
        PeterO.Numbers.ERational otherValue);

Divides this instance by the value of an arbitrary-precision rational number object.

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

The quotient of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>otherValue</i>
 is null.

<a id="Equals_object"></a>
### Equals

    public override bool Equals(
        object obj);

Determines whether this object's numerator, denominator, and properties are equal to those of another object and that other object is an arbitrary-precision rational number. Not-a-number values are considered equal if the rest of their properties are equal.

<b>Parameters:</b>

 * <i>obj</i>: The parameter  <i>obj</i>
 is an arbitrary object.

<b>Return Value:</b>

 `true`  if the objects are equal; otherwise,  `false` . In this method, two objects are not equal if they don't have the same type or if one is null and the other isn't.

<a id="Equals_PeterO_Numbers_ERational"></a>
### Equals

    public sealed bool Equals(
        PeterO.Numbers.ERational other);

Determines whether this object's numerator, denominator, and properties are equal to those of another object. Not-a-number values are considered equal if the rest of their properties are equal.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision rational number to compare to.

<b>Return Value:</b>

Either  `true`  or  `false` .

<a id="FromBoolean_bool"></a>
### FromBoolean

    public static PeterO.Numbers.ERational FromBoolean(
        bool boolValue);

Converts a boolean value (true or false) to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>boolValue</i>: Either true or false.

<b>Return Value:</b>

The number 1 if  <i>boolValue</i>
 is true; otherwise, 0.

<a id="FromByte_byte"></a>
### FromByte

    public static PeterO.Numbers.ERational FromByte(
        byte inputByte);

Converts a byte (from 0 to 255) to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputByte</i>: The number to convert as a byte (from 0 to 255).

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

<a id="FromDecimal_decimal"></a>
### FromDecimal

    public static PeterO.Numbers.ERational FromDecimal(
        decimal eint);

Converts a  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal) to a rational number.

<b>Parameters:</b>

 * <i>eint</i>: The number to convert as a  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal).

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="FromDouble_double"></a>
### FromDouble

    public static PeterO.Numbers.ERational FromDouble(
        double flt);

Converts a 64-bit floating-point number to a rational number. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the number to a string.

<b>Parameters:</b>

 * <i>flt</i>: The parameter  <i>flt</i>
 is a 64-bit floating-point number.

<b>Return Value:</b>

A rational number with the same value as  <i>flt</i>
.

<a id="FromEDecimal_PeterO_Numbers_EDecimal"></a>
### FromEDecimal

    public static PeterO.Numbers.ERational FromEDecimal(
        PeterO.Numbers.EDecimal ef);

Converts an arbitrary-precision decimal number to a rational number.

<b>Parameters:</b>

 * <i>ef</i>: The number to convert as an arbitrary-precision decimal number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ef</i>
 is null.

<a id="FromEFloat_PeterO_Numbers_EFloat"></a>
### FromEFloat

    public static PeterO.Numbers.ERational FromEFloat(
        PeterO.Numbers.EFloat ef);

Converts an arbitrary-precision binary floating-point number to a rational number.

<b>Parameters:</b>

 * <i>ef</i>: The number to convert as an arbitrary-precision binary floating-point number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ef</i>
 is null.

<a id="FromEInteger_PeterO_Numbers_EInteger"></a>
### FromEInteger

    public static PeterO.Numbers.ERational FromEInteger(
        PeterO.Numbers.EInteger bigint);

Converts an arbitrary-precision integer to a rational number.

<b>Parameters:</b>

 * <i>bigint</i>: The number to convert as an arbitrary-precision integer.

<b>Return Value:</b>

The exact value of the integer as a rational number.

<a id="FromExtendedDecimal_PeterO_Numbers_EDecimal"></a>
### FromExtendedDecimal

    public static PeterO.Numbers.ERational FromExtendedDecimal(
        PeterO.Numbers.EDecimal ef);

<b>Deprecated.</b> Renamed to FromEDecimal.

Converts an arbitrary-precision decimal number to a rational number.

<b>Parameters:</b>

 * <i>ef</i>: The number to convert as an arbitrary-precision decimal number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="FromExtendedFloat_PeterO_Numbers_EFloat"></a>
### FromExtendedFloat

    public static PeterO.Numbers.ERational FromExtendedFloat(
        PeterO.Numbers.EFloat ef);

<b>Deprecated.</b> Renamed to FromEFloat.

Converts an arbitrary-precision binary floating-point number to a rational number.

<b>Parameters:</b>

 * <i>ef</i>: The number to convert as an arbitrary-precision binary floating-point number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="FromInt16_short"></a>
### FromInt16

    public static PeterO.Numbers.ERational FromInt16(
        short inputInt16);

Converts a 16-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputInt16</i>: The number to convert as a 16-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

<a id="FromInt32_int"></a>
### FromInt32

    public static PeterO.Numbers.ERational FromInt32(
        int inputInt32);

Converts a 32-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

<a id="FromInt64_long"></a>
### FromInt64

    public static PeterO.Numbers.ERational FromInt64(
        long inputInt64);

Converts a 64-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputInt64</i>: The number to convert as a 64-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

<a id="FromSByte_sbyte"></a>
### FromSByte

    public static PeterO.Numbers.ERational FromSByte(
        sbyte inputSByte);

<b>This API is not CLS-compliant.</b>

Converts an 8-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputSByte</i>: The number to convert as an 8-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

<a id="FromSingle_float"></a>
### FromSingle

    public static PeterO.Numbers.ERational FromSingle(
        float flt);

Converts a 32-bit binary floating-point number to a rational number. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the number to a string.

<b>Parameters:</b>

 * <i>flt</i>: The parameter  <i>flt</i>
 is a 32-bit binary floating-point number.

<b>Return Value:</b>

A rational number with the same value as  <i>flt</i>
.

<a id="FromString_string"></a>
### FromString

    public static PeterO.Numbers.ERational FromString(
        string str);

Creates a rational number from a text string that represents a number. See  `FromString(String, int, int)`  for more information.

<b>Parameters:</b>

 * <i>str</i>: A string that represents a number.

<b>Return Value:</b>

An arbitrary-precision rational number with the same value as the given string.

<b>Exceptions:</b>

 * System.FormatException:
The parameter  <i>str</i>
 is not a correctly formatted number string.

<a id="FromString_string_int_int"></a>
### FromString

    public static PeterO.Numbers.ERational FromString(
        string str,
        int offset,
        int length);

Creates a rational number from a text string that represents a number.

The format of the string generally consists of:

 * An optional plus sign ("+" , U+002B) or minus sign ("-", U+002D) (if '-' , the value is negative.)

 * The numerator in the form of one or more digits (these digits may begin with any number of zeros).

 * Optionally, "/" followed by the denominator in the form of one or more digits (these digits may begin with any number of zeros). If a denominator is not given, it's equal to 1.

The string can also be "-INF", "-Infinity", "Infinity", "INF", quiet NaN ("NaN" /"-NaN") followed by any number of digits, or signaling NaN ("sNaN" /"-sNaN") followed by any number of digits, all in any combination of upper and lower case.

All characters mentioned above are the corresponding characters in the Basic Latin range. In particular, the digits must be the basic digits 0 to 9 (U+0030 to U+0039). The string is not allowed to contain white space characters, including spaces.

<b>Parameters:</b>

 * <i>str</i>: A text string, a portion of which represents a number.

 * <i>offset</i>: An index starting at 0 showing where the desired portion of  <i>str</i>
 begins.

 * <i>length</i>: The length, in code units, of the desired portion of  <i>str</i>
 (but not more than  <i>str</i>
 's length).

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.FormatException:
The parameter  <i>str</i>
 is not a correctly formatted number string.

 * System.ArgumentNullException:
The parameter  <i>str</i>
 is null.

 * System.ArgumentException:
Either  <i>offset</i>
 or  <i>length</i>
 is less than 0 or greater than  <i>str</i>
 's length, or  <i>str</i>
 's length minus  <i>offset</i>
 is less than  <i>length</i>
.

<a id="FromUInt16_ushort"></a>
### FromUInt16

    public static PeterO.Numbers.ERational FromUInt16(
        ushort inputUInt16);

<b>This API is not CLS-compliant.</b>

Converts a 16-bit unsigned integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputUInt16</i>: The number to convert as a 16-bit unsigned integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

<a id="FromUInt32_uint"></a>
### FromUInt32

    public static PeterO.Numbers.ERational FromUInt32(
        uint inputUInt32);

<b>This API is not CLS-compliant.</b>

Converts a 32-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputUInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

<a id="FromUInt64_ulong"></a>
### FromUInt64

    public static PeterO.Numbers.ERational FromUInt64(
        ulong inputUInt64);

<b>This API is not CLS-compliant.</b>

Converts a 64-bit unsigned integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputUInt64</i>: The number to convert as a 64-bit unsigned integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

<a id="GetHashCode"></a>
### GetHashCode

    public override int GetHashCode();

Returns the hash code for this instance. No application or process IDs are used in the hash code calculation.

<b>Return Value:</b>

A 32-bit signed integer.

<a id="Increment"></a>
### Increment

    public PeterO.Numbers.ERational Increment();

Adds one to an arbitrary-precision rational number.

<b>Return Value:</b>

The given arbitrary-precision rational number plus one.

<a id="IsInfinity"></a>
### IsInfinity

    public bool IsInfinity();

Gets a value indicating whether this object's value is infinity.

<b>Return Value:</b>

 `true`  if this object's value is infinity; otherwise,  `false` .

<a id="IsNaN"></a>
### IsNaN

    public bool IsNaN();

Returns whether this object is a not-a-number value.

<b>Return Value:</b>

 `true`  if this object is a not-a-number value; otherwise,  `false` .

<a id="IsNegativeInfinity"></a>
### IsNegativeInfinity

    public bool IsNegativeInfinity();

Returns whether this object is negative infinity.

<b>Return Value:</b>

 `true`  if this object is negative infinity; otherwise,  `false` .

<a id="IsPositiveInfinity"></a>
### IsPositiveInfinity

    public bool IsPositiveInfinity();

Returns whether this object is positive infinity.

<b>Return Value:</b>

 `true`  if this object is positive infinity; otherwise,  `false` .

<a id="IsQuietNaN"></a>
### IsQuietNaN

    public bool IsQuietNaN();

Returns whether this object is a quiet not-a-number value.

<b>Return Value:</b>

 `true`  if this object is a quiet not-a-number value; otherwise,  `false` .

<a id="IsSignalingNaN"></a>
### IsSignalingNaN

    public bool IsSignalingNaN();

Returns whether this object is a signaling not-a-number value (which causes an error if the value is passed to any arithmetic operation in this class).

<b>Return Value:</b>

 `true`  if this object is a signaling not-a-number value (which causes an error if the value is passed to any arithmetic operation in this class); otherwise,  `false` .

<a id="Max_PeterO_Numbers_ERational_PeterO_Numbers_ERational"></a>
### Max

    public static PeterO.Numbers.ERational Max(
        PeterO.Numbers.ERational first,
        PeterO.Numbers.ERational second);

Gets the greater value between two rational numbers.

<b>Parameters:</b>

 * <i>first</i>: An arbitrary-precision rational number.

 * <i>second</i>: Another arbitrary-precision rational number.

<b>Return Value:</b>

The larger value of the two numbers. If one is positive zero and the other is negative zero, returns the positive zero. If the two numbers are positive and have the same value, returns the one with the larger denominator. If the two numbers are negative and have the same value, returns the one with the smaller denominator.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="MaxMagnitude_PeterO_Numbers_ERational_PeterO_Numbers_ERational"></a>
### MaxMagnitude

    public static PeterO.Numbers.ERational MaxMagnitude(
        PeterO.Numbers.ERational first,
        PeterO.Numbers.ERational second);

Gets the greater value between two values, ignoring their signs. If the absolute values are equal, has the same effect as Max.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

<b>Return Value:</b>

The larger value of the two numbers, ignoring their signs.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="Min_PeterO_Numbers_ERational_PeterO_Numbers_ERational"></a>
### Min

    public static PeterO.Numbers.ERational Min(
        PeterO.Numbers.ERational first,
        PeterO.Numbers.ERational second);

Gets the lesser value between two rational numbers.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

<b>Return Value:</b>

The smaller value of the two numbers. If one is positive zero and the other is negative zero, returns the negative zero. If the two numbers are positive and have the same value, returns the one with the smaller denominator. If the two numbers are negative and have the same value, returns the one with the larger denominator.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="MinMagnitude_PeterO_Numbers_ERational_PeterO_Numbers_ERational"></a>
### MinMagnitude

    public static PeterO.Numbers.ERational MinMagnitude(
        PeterO.Numbers.ERational first,
        PeterO.Numbers.ERational second);

Gets the lesser value between two values, ignoring their signs. If the absolute values are equal, has the same effect as Min.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

<b>Return Value:</b>

The smaller value of the two numbers, ignoring their signs.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="Multiply_int"></a>
### Multiply

    public PeterO.Numbers.ERational Multiply(
        int v);

Returns the value of this instance multiplied by a 32-bit signed integer.

<b>Parameters:</b>

 * <i>v</i>: The parameter  <i>v</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

The product of the two numbers.

<a id="Multiply_PeterO_Numbers_ERational"></a>
### Multiply

    public PeterO.Numbers.ERational Multiply(
        PeterO.Numbers.ERational otherValue);

Multiplies this instance by the value of an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

The product of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>otherValue</i>
 is null.

<a id="Negate"></a>
### Negate

    public PeterO.Numbers.ERational Negate();

Returns a rational number with the same value as this one but with the sign reversed.

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="op_Addition"></a>
### Operator `+`

    public static PeterO.Numbers.ERational operator +(
        PeterO.Numbers.ERational bthis,
        PeterO.Numbers.ERational augend);

Adds two rational numbers.

<b>Parameters:</b>

 * <i>bthis</i>: The first operand.

 * <i>augend</i>: The second operand.

<b>Return Value:</b>

The sum of the two numbers. Returns not-a-number (NaN) if either operand is NaN.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

<a id="op_Decrement"></a>
### Operator `--`

    public static PeterO.Numbers.ERational operator --(
        PeterO.Numbers.ERational bthis);

Subtracts one from an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>bthis</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

The number given in  <i>bthis</i>
 minus one.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_Division"></a>
### Operator `/`

    public static PeterO.Numbers.ERational operator /(
        PeterO.Numbers.ERational dividend,
        PeterO.Numbers.ERational divisor);

Divides an arbitrary-precision rational number by the value of another arbitrary-precision rational number object.

<b>Parameters:</b>

 * <i>dividend</i>: An arbitrary-precision rational number serving as the dividend.

 * <i>divisor</i>: An arbitrary-precision rational number serving as the divisor.

<b>Return Value:</b>

The quotient of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

<a id="explicit_operator_byte_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator byte(
        PeterO.Numbers.ERational input);

Converts an arbitrary-precision rational number to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The value of  <i>input</i>
, truncated to a byte (from 0 to 255).

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than 0 or greater than 255.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_decimal_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator decimal(
        PeterO.Numbers.ERational extendedNumber);

Converts an arbitrary-precision rational number to a  `decimal`  under the Common Language Infrastructure (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ).

<b>Parameters:</b>

 * <i>extendedNumber</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

A  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal).

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>extendedNumber</i>
 is null.

<a id="explicit_operator_double_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator double(
        PeterO.Numbers.ERational bigValue);

Converts an arbitrary-precision rational number to a 64-bit floating-point number. The half-even rounding mode is used.

<b>Parameters:</b>

 * <i>bigValue</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The closest 64-bit floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 64-bit floating point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="explicit_operator_float_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator float(
        PeterO.Numbers.ERational bigValue);

Converts an arbitrary-precision rational number to a 32-bit binary floating-point number. The half-even rounding mode is used.

<b>Parameters:</b>

 * <i>bigValue</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The closest 32-bit binary floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 32-bit floating point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="explicit_operator_int_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator int(
        PeterO.Numbers.ERational input);

Converts an arbitrary-precision rational number to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The value of  <i>input</i>
, truncated to a 32-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than -2147483648 or greater than 2147483647.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_long_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator long(
        PeterO.Numbers.ERational input);

Converts an arbitrary-precision rational number to a 64-bit signed integer if it can fit in a 64-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The value of  <i>input</i>
, truncated to a 64-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than -9223372036854775808 or greater than 9223372036854775807.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_PeterO_Numbers_EInteger_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator PeterO.Numbers.EInteger(
        PeterO.Numbers.ERational bigValue);

Converts an arbitrary-precision rational number to an arbitrary-precision integer. Any fractional part in the value will be discarded when converting to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>bigValue</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="explicit_operator_PeterO_Numbers_ERational_bool"></a>
### Explicit Operator

    public static explicit operator PeterO.Numbers.ERational(
        bool boolValue);

Converts a boolean value (true or false) to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>boolValue</i>: Either true or false.

<b>Return Value:</b>

1 if  <i>boolValue</i>
 is true; otherwise, 0.

<a id="explicit_operator_sbyte_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator sbyte(
        PeterO.Numbers.ERational input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision rational number to an 8-bit signed integer if it can fit in an 8-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The value of  <i>input</i>
, truncated to an 8-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than -128 or greater than 127.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_short_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator short(
        PeterO.Numbers.ERational input);

Converts an arbitrary-precision rational number to a 16-bit signed integer if it can fit in a 16-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The value of  <i>input</i>
, truncated to a 16-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than -32768 or greater than 32767.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_uint_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator uint(
        PeterO.Numbers.ERational input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision rational number to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The value of  <i>input</i>
, truncated to a 32-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than 0 or greater than 4294967295.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_ulong_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator ulong(
        PeterO.Numbers.ERational input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision rational number to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The value of  <i>input</i>
, truncated to a 64-bit unsigned integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than 0 or greater than 18446744073709551615.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="explicit_operator_ushort_PeterO_Numbers_ERational"></a>
### Explicit Operator

    public static explicit operator ushort(
        PeterO.Numbers.ERational input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision rational number to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision rational number.

<b>Return Value:</b>

The value of  <i>input</i>
, truncated to a 16-bit unsigned integer.

<b>Exceptions:</b>

 * System.OverflowException:
The parameter  <i>input</i>
 is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than 0 or greater than 65535.

 * System.ArgumentNullException:
The parameter  <i>input</i>
 is null.

<a id="implicit_operator_PeterO_Numbers_ERational_byte"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        byte inputByte);

Converts a byte (from 0 to 255) to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputByte</i>: The number to convert as a byte (from 0 to 255).

<b>Return Value:</b>

The value of  <i>inputByte</i>
 as an arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_decimal"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        decimal eint);

Converts a  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal). to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>eint</i>: A  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal).

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_double"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        double eint);

Converts a 64-bit floating-point number to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>eint</i>: The parameter  <i>eint</i>
 is a 64-bit floating-point number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_float"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        float eint);

Converts a 32-bit binary floating-point number to a rational number.

<b>Parameters:</b>

 * <i>eint</i>: The parameter  <i>eint</i>
 is a 32-bit binary floating-point number.

<b>Return Value:</b>

The value of  <i>eint</i>
 as an arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_int"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        int inputInt32);

Converts a 32-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputInt32</i>
 as an arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_long"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        long inputInt64);

Converts a 64-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputInt64</i>: The number to convert as a 64-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputInt64</i>
 as an arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_PeterO_Numbers_EDecimal"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        PeterO.Numbers.EDecimal eint);

Converts an arbitrary-precision decimal floating-point number to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>eint</i>: The parameter  <i>eint</i>
 is an arbitrary-precision decimal floating-point number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_PeterO_Numbers_EFloat"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        PeterO.Numbers.EFloat eint);

Converts an arbitrary-precision binary floating-point number to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>eint</i>: An arbitrary-precision binary floating-point number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_PeterO_Numbers_EInteger"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        PeterO.Numbers.EInteger eint);

Converts an arbitrary-precision integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>eint</i>: An arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_sbyte"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        sbyte inputSByte);

<b>This API is not CLS-compliant.</b>

Converts an 8-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputSByte</i>: The number to convert as an 8-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputSByte</i>
 as an arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_short"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        short inputInt16);

Converts a 16-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputInt16</i>: The number to convert as a 16-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputInt16</i>
 as an arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_uint"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        uint inputUInt32);

<b>This API is not CLS-compliant.</b>

Converts a 32-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputUInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputUInt32</i>
 as an arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_ulong"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        ulong inputUInt64);

<b>This API is not CLS-compliant.</b>

Converts a 64-bit unsigned integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputUInt64</i>: The number to convert as a 64-bit unsigned integer.

<b>Return Value:</b>

The value of  <i>inputUInt64</i>
 as an arbitrary-precision rational number.

<a id="implicit_operator_PeterO_Numbers_ERational_ushort"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.ERational(
        ushort inputUInt16);

<b>This API is not CLS-compliant.</b>

Converts a 16-bit unsigned integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputUInt16</i>: The number to convert as a 16-bit unsigned integer.

<b>Return Value:</b>

The value of  <i>inputUInt16</i>
 as an arbitrary-precision rational number.

<a id="op_Increment"></a>
### Operator `++`

    public static PeterO.Numbers.ERational operator ++(
        PeterO.Numbers.ERational bthis);

Adds one to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>bthis</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

The number given in  <i>bthis</i>
 plus one.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_Modulus"></a>
### Operator `%`

    public static PeterO.Numbers.ERational operator %(
        PeterO.Numbers.ERational dividend,
        PeterO.Numbers.ERational divisor);

Finds the remainder that results when this instance is divided by the value of an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>dividend</i>: The dividend.

 * <i>divisor</i>: The divisor.

<b>Return Value:</b>

The remainder of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

<a id="op_Multiply"></a>
### Operator `*`

    public static PeterO.Numbers.ERational operator *(
        PeterO.Numbers.ERational operand1,
        PeterO.Numbers.ERational operand2);

Multiplies this instance by the value of an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>operand1</i>: The first operand.

 * <i>operand2</i>: The second operand.

<b>Return Value:</b>

The product of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

<a id="op_Subtraction"></a>
### Operator `-`

    public static PeterO.Numbers.ERational operator -(
        PeterO.Numbers.ERational bthis,
        PeterO.Numbers.ERational subtrahend);

Subtracts an arbitrary-precision rational number from this instance.

<b>Parameters:</b>

 * <i>bthis</i>: The first operand.

 * <i>subtrahend</i>: The second operand.

<b>Return Value:</b>

The difference of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

<a id="op_UnaryNegation"></a>
### Operator `-`

    public static PeterO.Numbers.ERational operator -(
        PeterO.Numbers.ERational bigValue);

Returns an arbitrary-precision rational number with the same value as the given one but with its sign reversed.

<b>Parameters:</b>

 * <i>bigValue</i>: An arbitrary-precision rational number to negate.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="Remainder_int"></a>
### Remainder

    public PeterO.Numbers.ERational Remainder(
        int v);

Finds the remainder that results when this instance is divided by the value of an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>v</i>: The divisor.

<b>Return Value:</b>

The remainder of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter  <i>v</i>
 is zero.

<a id="Remainder_PeterO_Numbers_ERational"></a>
### Remainder

    public PeterO.Numbers.ERational Remainder(
        PeterO.Numbers.ERational otherValue);

Finds the remainder that results when this instance is divided by the value of an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

The remainder of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>otherValue</i>
 is null.

<a id="Subtract_int"></a>
### Subtract

    public PeterO.Numbers.ERational Subtract(
        int v);

Returns the result of subtracting a 32-bit signed integer from this instance.

<b>Parameters:</b>

 * <i>v</i>: The parameter  <i>v</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

The difference of the two objects.

<a id="Subtract_PeterO_Numbers_ERational"></a>
### Subtract

    public PeterO.Numbers.ERational Subtract(
        PeterO.Numbers.ERational otherValue);

Subtracts an arbitrary-precision rational number from this instance.

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

The difference of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>otherValue</i>
 is null.

<a id="ToByteChecked"></a>
### ToByteChecked

    public byte ToByteChecked();

Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) after converting it to an integer by discarding its fractional part.

<b>Return Value:</b>

This number's value, truncated to a byte (from 0 to 255).

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than 0 or greater than 255.

<a id="ToByteIfExact"></a>
### ToByteIfExact

    public byte ToByteIfExact();

Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a byte (from 0 to 255).

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than 0 or greater than 255.

<a id="ToByteUnchecked"></a>
### ToByteUnchecked

    public byte ToByteUnchecked();

Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a byte (from 0 to 255).

<b>Return Value:</b>

This number, converted to a byte (from 0 to 255). Returns 0 if this value is infinity or not-a-number.

<a id="ToDecimal"></a>
### ToDecimal

    public decimal ToDecimal();

Converts this value to a  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal). Currently, converts this value to the precision and range of a.NET Framework decimal.

<b>Return Value:</b>

A  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal).

<a id="ToDouble"></a>
### ToDouble

    public double ToDouble();

Converts this value to a 64-bit floating-point number. The half-even rounding mode is used.

<b>Return Value:</b>

The closest 64-bit floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 64-bit floating point number.

<a id="ToEDecimal_PeterO_Numbers_EContext"></a>
### ToEDecimal

    public PeterO.Numbers.EDecimal ToEDecimal(
        PeterO.Numbers.EContext ctx);

Converts this rational number to an arbitrary-precision decimal number and rounds the result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The value of the rational number, rounded to the given precision. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating decimal expansion.

<a id="ToEDecimal"></a>
### ToEDecimal

    public PeterO.Numbers.EDecimal ToEDecimal();

Converts this rational number to an arbitrary-precision decimal number.

<b>Return Value:</b>

The exact value of the rational number, or not-a-number (NaN) if the result can't be exact because it has a nonterminating decimal expansion.

<a id="ToEDecimalExactIfPossible_PeterO_Numbers_EContext"></a>
### ToEDecimalExactIfPossible

    public PeterO.Numbers.EDecimal ToEDecimalExactIfPossible(
        PeterO.Numbers.EContext ctx);

Converts this rational number to an arbitrary-precision decimal number, but if the result would have a nonterminating decimal expansion, rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only if the exact result would have a nonterminating decimal expansion. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The exact value of the rational number if possible; otherwise, the rounded version of the result if a context is given. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating decimal expansion.

<a id="ToEFloat_PeterO_Numbers_EContext"></a>
### ToEFloat

    public PeterO.Numbers.EFloat ToEFloat(
        PeterO.Numbers.EContext ctx);

Converts this rational number to a binary floating-point number and rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The value of the rational number, rounded to the given precision. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating binary expansion.

<a id="ToEFloat"></a>
### ToEFloat

    public PeterO.Numbers.EFloat ToEFloat();

Converts this rational number to a binary floating-point number.

<b>Return Value:</b>

The exact value of the rational number, or not-a-number (NaN) if the result can't be exact because it has a nonterminating binary expansion.

<a id="ToEFloatExactIfPossible_PeterO_Numbers_EContext"></a>
### ToEFloatExactIfPossible

    public PeterO.Numbers.EFloat ToEFloatExactIfPossible(
        PeterO.Numbers.EContext ctx);

Converts this rational number to a binary floating-point number, but if the result would have a nonterminating binary expansion, rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only if the exact result would have a nonterminating binary expansion. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The exact value of the rational number if possible; otherwise, the rounded version of the result if a context is given. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating binary expansion.

<a id="ToEInteger"></a>
### ToEInteger

    public PeterO.Numbers.EInteger ToEInteger();

Converts this value to an arbitrary-precision integer by dividing the numerator by the denominator and discarding the fractional part of the result.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

<a id="ToEIntegerExact"></a>
### ToEIntegerExact

    public PeterO.Numbers.EInteger ToEIntegerExact();

<b>Deprecated.</b> Renamed to ToEIntegerIfExact.

Converts this value to an arbitrary-precision integer, checking whether the value is an exact integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

<a id="ToEIntegerIfExact"></a>
### ToEIntegerIfExact

    public PeterO.Numbers.EInteger ToEIntegerIfExact();

Converts this value to an arbitrary-precision integer, checking whether the value is an exact integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

<a id="ToExtendedDecimal_PeterO_Numbers_EContext"></a>
### ToExtendedDecimal

    public PeterO.Numbers.EDecimal ToExtendedDecimal(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to ToEDecimal.

Converts this rational number to an arbitrary-precision decimal number and rounds the result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The value of the rational number, rounded to the given precision. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating decimal expansion.

<a id="ToExtendedDecimal"></a>
### ToExtendedDecimal

    public PeterO.Numbers.EDecimal ToExtendedDecimal();

<b>Deprecated.</b> Renamed to ToEDecimal.

Converts this rational number to an arbitrary-precision decimal number.

<b>Return Value:</b>

The exact value of the rational number, or not-a-number (NaN) if the result can't be exact because it has a nonterminating decimal expansion.

<a id="ToExtendedDecimalExactIfPossible_PeterO_Numbers_EContext"></a>
### ToExtendedDecimalExactIfPossible

    public PeterO.Numbers.EDecimal ToExtendedDecimalExactIfPossible(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to ToEDecimalExactIfPossible.

Converts this rational number to an arbitrary-precision decimal number, but if the result would have a nonterminating decimal expansion, rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only if the exact result would have a nonterminating decimal expansion. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The exact value of the rational number if possible; otherwise, the rounded version of the result if a context is given. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating decimal expansion.

<a id="ToExtendedFloat_PeterO_Numbers_EContext"></a>
### ToExtendedFloat

    public PeterO.Numbers.EFloat ToExtendedFloat(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to ToEFloat.

Converts this rational number to a binary floating-point number and rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The value of the rational number, rounded to the given precision. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating binary expansion.

<a id="ToExtendedFloat"></a>
### ToExtendedFloat

    public PeterO.Numbers.EFloat ToExtendedFloat();

<b>Deprecated.</b> Renamed to ToEFloat.

Converts this rational number to a binary floating-point number.

<b>Return Value:</b>

The exact value of the rational number, or not-a-number (NaN) if the result can't be exact because it has a nonterminating binary expansion.

<a id="ToExtendedFloatExactIfPossible_PeterO_Numbers_EContext"></a>
### ToExtendedFloatExactIfPossible

    public PeterO.Numbers.EFloat ToExtendedFloatExactIfPossible(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to ToEFloatExactIfPossible.

Converts this rational number to a binary floating-point number, but if the result would have a nonterminating binary expansion, rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only if the exact result would have a nonterminating binary expansion. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The exact value of the rational number if possible; otherwise, the rounded version of the result if a context is given. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating binary expansion.

<a id="ToInt16Checked"></a>
### ToInt16Checked

    public short ToInt16Checked();

Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Return Value:</b>

This number's value, truncated to a 16-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than -32768 or greater than 32767.

<a id="ToInt16IfExact"></a>
### ToInt16IfExact

    public short ToInt16IfExact();

Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 16-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than -32768 or greater than 32767.

<a id="ToInt16Unchecked"></a>
### ToInt16Unchecked

    public short ToInt16Unchecked();

Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 16-bit signed integer.

<b>Return Value:</b>

This number, converted to a 16-bit signed integer. Returns 0 if this value is infinity or not-a-number.

<a id="ToInt32Checked"></a>
### ToInt32Checked

    public int ToInt32Checked();

Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Return Value:</b>

This number's value, truncated to a 32-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than -2147483648 or greater than 2147483647.

<a id="ToInt32IfExact"></a>
### ToInt32IfExact

    public int ToInt32IfExact();

Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 32-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than -2147483648 or greater than 2147483647.

<a id="ToInt32Unchecked"></a>
### ToInt32Unchecked

    public int ToInt32Unchecked();

Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 32-bit signed integer.

<b>Return Value:</b>

This number, converted to a 32-bit signed integer. Returns 0 if this value is infinity or not-a-number.

<a id="ToInt64Checked"></a>
### ToInt64Checked

    public long ToInt64Checked();

Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Return Value:</b>

This number's value, truncated to a 64-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than -9223372036854775808 or greater than 9223372036854775807.

<a id="ToInt64IfExact"></a>
### ToInt64IfExact

    public long ToInt64IfExact();

Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 64-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than -9223372036854775808 or greater than 9223372036854775807.

<a id="ToInt64Unchecked"></a>
### ToInt64Unchecked

    public long ToInt64Unchecked();

Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 64-bit signed integer.

<b>Return Value:</b>

This number, converted to a 64-bit signed integer. Returns 0 if this value is infinity or not-a-number.

<a id="ToSByteChecked"></a>
### ToSByteChecked

    public sbyte ToSByteChecked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Return Value:</b>

This number's value, truncated to an 8-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than -128 or greater than 127.

<a id="ToSByteIfExact"></a>
### ToSByteIfExact

    public sbyte ToSByteIfExact();

<b>This API is not CLS-compliant.</b>

Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as an 8-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than -128 or greater than 127.

<a id="ToSByteUnchecked"></a>
### ToSByteUnchecked

    public sbyte ToSByteUnchecked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as an 8-bit signed integer.

<b>Return Value:</b>

This number, converted to an 8-bit signed integer. Returns 0 if this value is infinity or not-a-number.

<a id="ToSingle"></a>
### ToSingle

    public float ToSingle();

Converts this value to a 32-bit binary floating-point number. The half-even rounding mode is used.

<b>Return Value:</b>

The closest 32-bit binary floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 32-bit floating point number.

<a id="ToString"></a>
### ToString

    public override string ToString();

Converts this object to a text string.

<b>Return Value:</b>

A string representation of this object. If this object's value is infinity or not-a-number, the result is the analogous return value of the  `EDecimal.ToString`  method. Otherwise, the return value has the following form:  `[-]numerator/denominator` .

<a id="ToUInt16Checked"></a>
### ToUInt16Checked

    public ushort ToUInt16Checked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer after converting it to an integer by discarding its fractional part.

<b>Return Value:</b>

This number's value, truncated to a 16-bit unsigned integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than 0 or greater than 65535.

<a id="ToUInt16IfExact"></a>
### ToUInt16IfExact

    public ushort ToUInt16IfExact();

<b>This API is not CLS-compliant.</b>

Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 16-bit unsigned integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than 0 or greater than 65535.

<a id="ToUInt16Unchecked"></a>
### ToUInt16Unchecked

    public ushort ToUInt16Unchecked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 16-bit unsigned integer.

<b>Return Value:</b>

This number, converted to a 16-bit unsigned integer. Returns 0 if this value is infinity or not-a-number.

<a id="ToUInt32Checked"></a>
### ToUInt32Checked

    public uint ToUInt32Checked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Return Value:</b>

This number's value, truncated to a 32-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than 0 or greater than 4294967295.

<a id="ToUInt32IfExact"></a>
### ToUInt32IfExact

    public uint ToUInt32IfExact();

<b>This API is not CLS-compliant.</b>

Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 32-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than 0 or greater than 4294967295.

<a id="ToUInt32Unchecked"></a>
### ToUInt32Unchecked

    public uint ToUInt32Unchecked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 32-bit signed integer.

<b>Return Value:</b>

This number, converted to a 32-bit signed integer. Returns 0 if this value is infinity or not-a-number.

<a id="ToUInt64Checked"></a>
### ToUInt64Checked

    public ulong ToUInt64Checked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer after converting it to an integer by discarding its fractional part.

<b>Return Value:</b>

This number's value, truncated to a 64-bit unsigned integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the number, once converted to an integer by discarding its fractional part, is less than 0 or greater than 18446744073709551615.

<a id="ToUInt64IfExact"></a>
### ToUInt64IfExact

    public ulong ToUInt64IfExact();

<b>This API is not CLS-compliant.</b>

Converts this number's value to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 64-bit unsigned integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than 0 or greater than 18446744073709551615.

<a id="ToUInt64Unchecked"></a>
### ToUInt64Unchecked

    public ulong ToUInt64Unchecked();

<b>This API is not CLS-compliant.</b>

Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 64-bit unsigned integer.

<b>Return Value:</b>

This number, converted to a 64-bit unsigned integer. Returns 0 if this value is infinity or not-a-number.
