## PeterO.Numbers.EDecimal

    public sealed class EDecimal :
        System.IComparable,
        System.IEquatable

 Represents an arbitrary-precision decimal floating-point number. (The "E" stands for "extended", meaning that instances of this class can be values other than numbers proper, such as infinity and not-a-number.) <b>About decimal arithmetic</b>

Decimal (base-10) arithmetic, such as that provided by this class, is appropriate for calculations involving such real-world data as prices and other sums of money, tax rates, and measurements. These calculations often involve multiplying or dividing one decimal with another decimal, or performing other operations on decimal numbers. Many of these calculations also rely on rounding behavior in which the result after rounding is an arbitrary-precision decimal number (for example, multiplying a price by a premium rate, then rounding, should result in a decimal amount of money).

On the other hand, most implementations of  `float`  and  `double`  , including in C# and Java, store numbers in a binary (base-2) floating-point format and use binary floating-point arithmetic. Many decimal numbers can't be represented exactly in binary floating-point format (regardless of its length). Applying binary arithmetic to numbers intended to be decimals can sometimes lead to unintuitive results, as is shown in the description for the FromDouble() method of this class.

<b>About EDecimal instances</b>

Each instance of this class consists of an integer significand and an integer exponent, both arbitrary-precision. The value of the number equals significand * 10^exponent.

The significand is the value of the digits that make up a number, ignoring the decimal point and exponent. For example, in the number 2356.78, the significand is 235678. The exponent is where the "floating" decimal point of the number is located. A positive exponent means "move it to the right", and a negative exponent means "move it to the left." In the example 2, 356.78, the exponent is -2, since it has 2 decimal places and the decimal point is "moved to the left by 2." Therefore, in the arbitrary-precision decimal representation, this number would be stored as 235678 * 10^-2.

The significand and exponent format preserves trailing zeros in the number's value. This may give rise to multiple ways to store the same value. For example, 1.00 and 1 would be stored differently, even though they have the same value. In the first case, 100 * 10^-2 (100 with decimal point moved left by 2), and in the second case, 1 * 10^0 (1 with decimal point moved 0).

This class also supports values for negative zero, not-a-number (NaN) values, and infinity. <b>Negative zero</b> is generally used when a negative number is rounded to 0; it has the same mathematical value as positive zero. <b>Infinity</b> is generally used when a non-zero number is divided by zero, or when a very high or very low number can't be represented in a given exponent range. <b>Not-a-number</b> is generally used to signal errors.

This class implements the General Decimal Arithmetic Specification version 1.70 except part of chapter 6(  `http://speleotrove.com/decimal/decarith.html`  ).

<b>Errors and Exceptions</b>

Passing a signaling NaN to any arithmetic operation shown here will signal the flag FlagInvalid and return a quiet NaN, even if another operand to that operation is a quiet NaN, unless the operation's documentation expressly states that another result happens when a signaling NaN is passed to that operation.

Passing a quiet NaN to any arithmetic operation shown here will return a quiet NaN, unless the operation's documentation expressly states that another result happens when a quiet NaN is passed to that operation. Invalid operations will also return a quiet NaN, as stated in the individual methods.

Unless noted otherwise, passing a null arbitrary-precision decimal argument to any method here will throw an exception.

When an arithmetic operation signals the flag FlagInvalid, FlagOverflow, or FlagDivideByZero, it will not throw an exception too, unless the flag's trap is enabled in the arithmetic context (see EContext's Traps property).

If an operation requires creating an intermediate value that might be too big to fit in memory (or might require more than 2 gigabytes of memory to store -- due to the current use of a 32-bit integer internally as a length), the operation may signal an invalid-operation flag and return not-a-number (NaN). In certain rare cases, the CompareTo method may throw OutOfMemoryException (called OutOfMemoryError in Java) in the same circumstances.

<b>Serialization</b>

An arbitrary-precision decimal value can be serialized (converted to a stable format) in one of the following ways:

 * By calling the toString() method, which will always return distinct strings for distinct arbitrary-precision decimal values.

 * By calling the UnsignedMantissa, Exponent, and IsNegative properties, and calling the IsInfinity, IsQuietNaN, and IsSignalingNaN methods. The return values combined will uniquely identify a particular arbitrary-precision decimal value.

<b>Thread safety</b>

Instances of this class are immutable, so they are inherently safe for use by multiple threads. Multiple instances of this object with the same properties are interchangeable, so they should not be compared using the "==" operator (which might only check if each side of the operator is the same instance).

<b>Comparison considerations</b>

This class's natural ordering (under the CompareTo method) is not consistent with the Equals method. This means that two values that compare as equal under the CompareTo method might not be equal under the Equals method. The CompareTo method compares the mathematical values of the two instances passed to it (and considers two different NaN values as equal), while two instances with the same mathematical value, but different exponents, will be considered unequal under the Equals method.

<b>Security note</b>

It is not recommended to implement security-sensitive algorithms using the methods in this class, for several reasons:

 *  `EDecimal`  objects are immutable, so they can't be modified, and the memory they occupy is not guaranteed to be cleared in a timely fashion due to garbage collection. This is relevant for applications that use many-digit-long numbers as secret parameters.

 * The methods in this class (especially those that involve arithmetic) are not guaranteed to be "constant-time" (non-data-dependent) for all relevant inputs. Certain attacks that involve encrypted communications have exploited the timing and other aspects of such communications to derive keying material or cleartext indirectly.

Applications should instead use dedicated security libraries to handle big numbers in security-sensitive algorithms.

<b>Reproducibility note</b>

Some applications, such as simulations, care about results that are reproducible, bit for bit, across computers and across runs of the application. Bruce Dawson, in "Floating-Point Determinism" (  `https://randomascii.wordpress.com/`  `2013/07/16/floating-point-determinism/`  ), identified many reproducibility issues with floating-point numbers, and here is how they relate to the EDecimal and EFloat classes of this library:

 * Runtime floating-point settings: All the settings that change how EDecimal and EFloat behave are given as parameters to the appropriate methods, especially via EContext objects, which specify the precision, rounding, and exponent range of numbers, among other things. The EDecimal and EFloat classes avoid the use of "native" floating-point data types (except for methods that convert to or from  `float`  ,  `double`  , or  `System.Decimal`  ). Such "native" types are often subject to runtime settings that change how floating-point math behaves with them, and these settings are often not accessible to .NET or Java code.

 * Non-associativity and intermediate precisions: In general, EDecimal and EFloat use "unlimited" precision in their calculations unless specified otherwise by an EContext object. However, by limiting the precision of EDecimal, EFloat, and other floating-point numbers in this way, operations such as addition and multiplication on three or more numbers can be <i>non-associative</i> , meaning the result can change depending on the order in which those numbers are added or multiplied. This property means that if an algorithm does not ensure such numbers are added or multiplied in the same order every time, its results may not be reproducible across computers or across runs of the application. This non-associativity problem can happen, for example, if an application splits a calculation across several threads and combines their results in the end. The problems with an unspecified order of operations (in the same line of code) and intermediate precisions (problems present in C and C++, for example) don't exist with method calls to EDecimal and EFloat methods, especially since they require limited-precision support to be declared explicitly via EContext.

 * fmadd instruction: EDecimal and EFloat include a MultiplyAndAdd method with the same semantics as in the General Decimal Arithmetic Specification, which requires delivering correctly rounded results for this method.

 * Square root estimate: Not applicable since EDecimal and EFloat don't include any estimates to square root.

 * Transcendental functions: This includes logarithms, exponentials, and the Pi method. For these functions, results are not guaranteed to always be correctly rounded. When using transcendentals, an application that cares about reproducibility should choose one version of this library and stick to it; this at least has the advantage that the implementation will be the same across computers, unlike with "native" floating-point types where the choice of implementation is often not within the application's control.

 * Conversions: Conversions between EDecimal or EFloat and text strings have the same implementation across computers for the same version of this library (see also the advice for transcendentals above). But as for the ToDouble, ToSingle, FromDouble, and FromSingle methods, note that some implementations of Java and.NET may or may not support preserving the value of subnormal numbers (numbers other than zero with the lowest possible exponent) or the payloads held in a not-a-number (NaN) value of float or double; thus these methods should not be considered reproducible across computers.

 * Compiler differences: Not applicable where these classes don't use "native" floating-point types.

 * Uninitialized data; per-processor code: Not applicable.

<b>Forms of numbers</b>

There are several other types of numbers that are mentioned in this class and elsewhere in this documentation. For reference, they are specified here.

<b>Unsigned integer</b> : An integer that's always 0 or greater, with the following maximum values:

 * 8-bit unsigned integer, or <i>byte</i> : 255.

 * 16-bit unsigned integer: 65535.

 * 32-bit unsigned integer: (2 <sup>32</sup> -1).

 * 64-bit unsigned integer: (2 <sup>64</sup> -1).

<b>Signed integer</b> : An integer in <i>two's-complement form</i> , with the following ranges:

 * 8-bit signed integer: -128 to 127.

 * 16-bit signed integer: -32768 to 32767.

 * 32-bit signed integer: -2 <sup>31</sup> to (2 <sup>31</sup> - 1).

 * 64-bit signed integer: -2 <sup>63</sup> to (2 <sup>63</sup> - 1).

<b>Two's complement form</b> : In <i>two's-complement form</i> , nonnegative numbers have the highest (most significant) bit set to zero, and negative numbers have that bit (and all bits beyond) set to one, and a negative number is stored in such form by decreasing its absolute value by 1 and swapping the bits of the resulting number.

<b>64-bit floating-point number</b> : A 64-bit binary floating-point number, in the form <i>significand</i> * 2 <sup><i>exponent</i></sup> . The significand is 53 bits long (Precision) and the exponent ranges from -1074 (EMin) to 971 (EMax). The number is stored in the following format (commonly called the IEEE 754 format):

    |C|BBB...BBB|AAAAAA...AAAAAA|

 * A. Low 52 bits (Precision minus 1 bits): Lowest bits of the significand.

 * B. Next 11 bits: Exponent area:

 * If all bits are ones, the final stored value is infinity (positive or negative depending on the C bit) if all bits in area A are zeros, or not-a-number (NaN) otherwise.

 * If all bits are zeros, the final stored value is a subnormal number, the exponent is EMin, and the highest bit of the significand is zero.

 * If any other number, the exponent is this value reduced by 1, then raised by EMin, and the highest bit of the significand is one.

 * C. Highest bit: If one, this is a negative number.

The elements described above are in the same order as the order of each bit of each element, that is, either most significant first or least significant first.

<b>32-bit binary floating-point number</b> : A 32-bit binary number which is stored similarly to a <i>64-bit floating-point number</i> , except that:

 * Precision is 24 bits.

 * EMin is -149.

 * EMax is 104.

 * A. The low 23 bits (Precision minus 1 bits) are the lowest bits of the significand.

 * B. The next 8 bits are the exponent area.

 * C. If the highest bit is one, this is a negative number.

<b>.NET Framework decimal</b> : A 128-bit decimal floating-point number, in the form <i>significand</i> * 10 <sup>- <i>scale</i></sup> , where the scale ranges from 0 to 28. The number is stored in the following format:

 * Low 96 bits are the significand, as a 96-bit unsigned integer (all 96-bit values are allowed, up to (2 <sup>96</sup> -1)).

 * Next 16 bits are unused.

 * Next 8 bits are the scale, stored as an 8-bit unsigned integer.

 * Next 7 bits are unused.

 * If the highest bit is one, it's a negative number.

The elements described above are in the same order as the order of each bit of each element, that is, either most significant first or least significant first.

### Member Summary
* <code>[Abs()](#Abs)</code> - Finds the absolute value of this object (if it's negative, it becomes positive).
* <code>[Abs(PeterO.Numbers.EContext)](#Abs_PeterO_Numbers_EContext)</code> - Finds the absolute value of this object (if it's negative, it becomes positive).
* <code>[Add(int)](#Add_int)</code> - Adds this arbitrary-precision decimal floating-point number and a 32-bit signed integer and returns the result.
* <code>[Add(long)](#Add_long)</code> - Adds this arbitrary-precision decimal floating-point number and a 64-bit signed integer and returns the result.
* <code>[Add(PeterO.Numbers.EDecimal)](#Add_PeterO_Numbers_EDecimal)</code> - Adds this arbitrary-precision decimal floating-point number and another arbitrary-precision decimal floating-point number and returns the result.
* <code>[Add(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Add_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Adds this arbitrary-precision decimal floating-point number and another arbitrary-precision decimal floating-point number and returns the result.
* <code>[CompareTo(int)](#CompareTo_int)</code> - Compares the mathematical values of this object and another object, accepting NaN values.
* <code>[CompareTo(long)](#CompareTo_long)</code> - Compares the mathematical values of this object and another object, accepting NaN values.
* <code>[CompareTo(PeterO.Numbers.EDecimal)](#CompareTo_PeterO_Numbers_EDecimal)</code> - Compares the mathematical values of this object and another object, accepting NaN values.
* <code>[CompareToBinary(PeterO.Numbers.EFloat)](#CompareToBinary_PeterO_Numbers_EFloat)</code> - Compares an arbitrary-precision binary floating-point number with this instance.
* <code>[CompareToSignal(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#CompareToSignal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Compares the mathematical values of this object and another object, treating quiet NaN as signaling.
* <code>[CompareToTotal(PeterO.Numbers.EDecimal)](#CompareToTotal_PeterO_Numbers_EDecimal)</code> - Compares the values of this object and another object, imposing a total ordering on all possible values.
* <code>[CompareToTotal(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#CompareToTotal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Compares the values of this object and another object, imposing a total ordering on all possible values.
* <code>[CompareToTotalMagnitude(PeterO.Numbers.EDecimal)](#CompareToTotalMagnitude_PeterO_Numbers_EDecimal)</code> - Compares the absolute values of this object and another object, imposing a total ordering on all possible values (ignoring their signs).
* <code>[CompareToTotalMagnitude(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#CompareToTotalMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Compares the values of this object and another object, imposing a total ordering on all possible values (ignoring their signs).
* <code>[CompareToValue(int)](#CompareToValue_int)</code> - Compares the mathematical values of this object and another object, accepting NaN values.
* <code>[CompareToValue(long)](#CompareToValue_long)</code> - Compares the mathematical values of this object and another object, accepting NaN values.
* <code>[CompareToValue(PeterO.Numbers.EDecimal)](#CompareToValue_PeterO_Numbers_EDecimal)</code> - Compares the mathematical values of this object and another object, accepting NaN values.
* <code>[CompareToWithContext(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#CompareToWithContext_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Compares the mathematical values of this object and another object.
* <code>[Copy()](#Copy)</code> - Creates a copy of this arbitrary-precision binary number.
* <code>[CopySign(PeterO.Numbers.EDecimal)](#CopySign_PeterO_Numbers_EDecimal)</code> - Returns a number with the same value as this one, but copying the sign (positive or negative) of another number.
* <code>[Create(int, int)](#Create_int_int)</code> - Returns a number with the value exponent*10^significand.
* <code>[Create(long, int)](#Create_long_int)</code> - Creates a number with the value exponent*10^significand.
* <code>[Create(long, long)](#Create_long_long)</code> - Creates a number with the value exponent*10^significand.
* <code>[Create(PeterO.Numbers.EInteger, int)](#Create_PeterO_Numbers_EInteger_int)</code> - Creates a number with the value exponent*10^significand.
* <code>[Create(PeterO.Numbers.EInteger, long)](#Create_PeterO_Numbers_EInteger_long)</code> - Creates a number with the value exponent*10^significand.
* <code>[Create(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#Create_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> - Creates a number with the value exponent*10^significand.
* <code>[CreateNaN(PeterO.Numbers.EInteger)](#CreateNaN_PeterO_Numbers_EInteger)</code> - Creates a not-a-number arbitrary-precision decimal number.
* <code>[CreateNaN(PeterO.Numbers.EInteger, bool, bool, PeterO.Numbers.EContext)](#CreateNaN_PeterO_Numbers_EInteger_bool_bool_PeterO_Numbers_EContext)</code> - Creates a not-a-number arbitrary-precision decimal number.
* <code>[Decrement()](#Decrement)</code> - Returns one subtracted from this arbitrary-precision decimal number.
* <code>[Divide(int)](#Divide_int)</code> - Divides this arbitrary-precision decimal floating-point number by a 32-bit signed integer and returns the result; returns NaN instead if the result would have a nonterminating decimal expansion (including 1/3, 1/12, 1/7, 2/3, and so on); if this is not desired, use DivideToExponent, or use the Divide overload that takes an EContext.
* <code>[Divide(long)](#Divide_long)</code> - Divides this arbitrary-precision decimal floating-point number by a 64-bit signed integer and returns the result; returns NaN instead if the result would have a nonterminating decimal expansion (including 1/3, 1/12, 1/7, 2/3, and so on); if this is not desired, use DivideToExponent, or use the Divide overload that takes an EContext.
* <code>[Divide(PeterO.Numbers.EDecimal)](#Divide_PeterO_Numbers_EDecimal)</code> - Divides this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result; returns NaN instead if the result would have a nonterminating decimal expansion (including 1/3, 1/12, 1/7, 2/3, and so on); if this is not desired, use DivideToExponent, or use the Divide overload that takes an EContext.
* <code>[Divide(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Divide_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Divides this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result.
* <code>[DivideAndRemainderNaturalScale(PeterO.Numbers.EDecimal)](#DivideAndRemainderNaturalScale_PeterO_Numbers_EDecimal)</code> - <b>Deprecated:</b> Renamed to DivRemNaturalScale.
* <code>[DivideAndRemainderNaturalScale(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#DivideAndRemainderNaturalScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - <b>Deprecated:</b> Renamed to DivRemNaturalScale.
* <code>[DivideToExponent(PeterO.Numbers.EDecimal, int)](#DivideToExponent_PeterO_Numbers_EDecimal_int)</code> - Divides two arbitrary-precision decimal numbers, and gives a particular exponent (expressed as a 32-bit signed integer) to the result, using the half-even rounding mode.
* <code>[DivideToExponent(PeterO.Numbers.EDecimal, int, PeterO.Numbers.EContext)](#DivideToExponent_PeterO_Numbers_EDecimal_int_PeterO_Numbers_EContext)</code> - Divides two arbitrary-precision decimal numbers, and gives a particular exponent (expressed as a 32-bit signed integer) to the result, using the half-even rounding mode.
* <code>[DivideToExponent(PeterO.Numbers.EDecimal, int, PeterO.Numbers.ERounding)](#DivideToExponent_PeterO_Numbers_EDecimal_int_PeterO_Numbers_ERounding)</code> - Divides two arbitrary-precision decimal numbers, and gives a particular exponent (expressed as a 32-bit signed integer) to the result, using the half-even rounding mode.
* <code>[DivideToExponent(PeterO.Numbers.EDecimal, long)](#DivideToExponent_PeterO_Numbers_EDecimal_long)</code> - Divides two arbitrary-precision decimal numbers, and gives a particular exponent (expressed as a 64-bit signed integer) to the result, using the half-even rounding mode.
* <code>[DivideToExponent(PeterO.Numbers.EDecimal, long, PeterO.Numbers.EContext)](#DivideToExponent_PeterO_Numbers_EDecimal_long_PeterO_Numbers_EContext)</code> - Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result.
* <code>[DivideToExponent(PeterO.Numbers.EDecimal, long, PeterO.Numbers.ERounding)](#DivideToExponent_PeterO_Numbers_EDecimal_long_PeterO_Numbers_ERounding)</code> - Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result.
* <code>[DivideToExponent(PeterO.Numbers.EDecimal, PeterO.Numbers.EInteger)](#DivideToExponent_PeterO_Numbers_EDecimal_PeterO_Numbers_EInteger)</code> - Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result, using the half-even rounding mode.
* <code>[DivideToExponent(PeterO.Numbers.EDecimal, PeterO.Numbers.EInteger, PeterO.Numbers.EContext)](#DivideToExponent_PeterO_Numbers_EDecimal_PeterO_Numbers_EInteger_PeterO_Numbers_EContext)</code> - Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result.
* <code>[DivideToExponent(PeterO.Numbers.EDecimal, PeterO.Numbers.EInteger, PeterO.Numbers.ERounding)](#DivideToExponent_PeterO_Numbers_EDecimal_PeterO_Numbers_EInteger_PeterO_Numbers_ERounding)</code> - Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result.
* <code>[DivideToIntegerNaturalScale(PeterO.Numbers.EDecimal)](#DivideToIntegerNaturalScale_PeterO_Numbers_EDecimal)</code> - Divides two arbitrary-precision decimal numbers, and returns the integer part of the result, rounded down, with the preferred exponent set to this value's exponent minus the divisor's exponent.
* <code>[DivideToIntegerNaturalScale(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#DivideToIntegerNaturalScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Divides this object by another object, and returns the integer part of the result (which is initially rounded down), with the preferred exponent set to this value's exponent minus the divisor's exponent.
* <code>[DivideToIntegerZeroScale(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#DivideToIntegerZeroScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Divides this object by another object, and returns the integer part of the result, with the exponent set to 0.
* <code>[DivideToSameExponent(PeterO.Numbers.EDecimal, PeterO.Numbers.ERounding)](#DivideToSameExponent_PeterO_Numbers_EDecimal_PeterO_Numbers_ERounding)</code> - Divides this object by another decimal number and returns a result with the same exponent as this object (the dividend).
* <code>[DivRemNaturalScale(PeterO.Numbers.EDecimal)](#DivRemNaturalScale_PeterO_Numbers_EDecimal)</code> - Divides this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns a two-item array containing the result of the division and the remainder, in that order.
* <code>[DivRemNaturalScale(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#DivRemNaturalScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Divides this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns a two-item array containing the result of the division and the remainder, in that order.
* <code>[Equals(object)](#Equals_object)</code> - Determines whether this object's significand, exponent, and properties are equal to those of another object and that other object is an arbitrary-precision decimal number.
* <code>[Equals(PeterO.Numbers.EDecimal)](#Equals_PeterO_Numbers_EDecimal)</code> - Determines whether this object's significand, exponent, and properties are equal to those of another object.
* <code>[Exp(PeterO.Numbers.EContext)](#Exp_PeterO_Numbers_EContext)</code> - Finds e (the base of natural logarithms) raised to the power of this object's value.
* <code>[explicit operator byte(PeterO.Numbers.EDecimal)](#explicit_operator_byte_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal number to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) after converting it to an integer by discarding its fractional part.
* <code>[explicit operator decimal(PeterO.Numbers.EDecimal)](#explicit_operator_decimal_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal's value to a decimal under the Common Language Infrastructure (see T:PeterO.
* <code>[explicit operator double(PeterO.Numbers.EDecimal)](#explicit_operator_double_PeterO_Numbers_EDecimal)</code> - Converts this value to its closest equivalent as a 64-bit floating-point number.
* <code>[explicit operator float(PeterO.Numbers.EDecimal)](#explicit_operator_float_PeterO_Numbers_EDecimal)</code> - Converts this value to its closest equivalent as a 32-bit floating-point number.
* <code>[explicit operator int(PeterO.Numbers.EDecimal)](#explicit_operator_int_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal number to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator long(PeterO.Numbers.EDecimal)](#explicit_operator_long_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal number to a 64-bit signed integer if it can fit in a 64-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator PeterO.Numbers.EDecimal(bool)](#explicit_operator_PeterO_Numbers_EDecimal_bool)</code> - Converts a boolean value (true or false) to an arbitrary precision decimal.
* <code>[explicit operator PeterO.Numbers.EInteger(PeterO.Numbers.EDecimal)](#explicit_operator_PeterO_Numbers_EInteger_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal floating-point number to an arbitrary-precision integer.
* <code>[explicit operator sbyte(PeterO.Numbers.EDecimal)](#explicit_operator_sbyte_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal number to an 8-bit signed integer if it can fit in an 8-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator short(PeterO.Numbers.EDecimal)](#explicit_operator_short_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal number to a 16-bit signed integer if it can fit in a 16-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator uint(PeterO.Numbers.EDecimal)](#explicit_operator_uint_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal number to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator ulong(PeterO.Numbers.EDecimal)](#explicit_operator_ulong_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal number to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer after converting it to an integer by discarding its fractional part.
* <code>[explicit operator ushort(PeterO.Numbers.EDecimal)](#explicit_operator_ushort_PeterO_Numbers_EDecimal)</code> - Converts an arbitrary-precision decimal number to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer after converting it to an integer by discarding its fractional part.
* <code>[ExpM1(PeterO.Numbers.EContext)](#ExpM1_PeterO_Numbers_EContext)</code> - Finds e (the base of natural logarithms) raised to the power of this object's value, and subtracts the result by 1 and returns the final result, in a way that avoids loss of precision if the true result is very close to 0.
* <code>[Exponent](#Exponent)</code> - Gets this object's exponent.
* <code>[FromBoolean(bool)](#FromBoolean_bool)</code> - Converts a boolean value (true or false) to an arbitrary-precision decimal number.
* <code>[FromByte(byte)](#FromByte_byte)</code> - Converts a byte (from 0 to 255) to an arbitrary-precision decimal number.
* <code>[FromDecimal(decimal)](#FromDecimal_decimal)</code> - Converts a decimal under the Common Language Infrastructure (see T:PeterO.
* <code>[FromDouble(double)](#FromDouble_double)</code> - Creates an arbitrary-precision decimal number from a 64-bit binary floating-point number.
* <code>[FromDoubleBits(long)](#FromDoubleBits_long)</code> - Creates an arbitrary-precision decimal number from a 64-bit binary floating-point number, encoded in the IEEE 754 binary64 format.
* <code>[FromEFloat(PeterO.Numbers.EFloat)](#FromEFloat_PeterO_Numbers_EFloat)</code> - Creates an arbitrary-precision decimal number from an arbitrary-precision binary floating-point number.
* <code>[FromEInteger(PeterO.Numbers.EInteger)](#FromEInteger_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to an arbitrary precision decimal.
* <code>[FromExtendedFloat(PeterO.Numbers.EFloat)](#FromExtendedFloat_PeterO_Numbers_EFloat)</code> - <b>Deprecated:</b> Renamed to FromEFloat.
* <code>[FromInt16(short)](#FromInt16_short)</code> - Converts a 16-bit signed integer to an arbitrary-precision decimal number.
* <code>[FromInt32(int)](#FromInt32_int)</code> - Creates an arbitrary-precision decimal number from a 32-bit signed integer.
* <code>[FromInt64(long)](#FromInt64_long)</code> - Creates an arbitrary-precision decimal number from a 64-bit signed integer.
* <code>[FromSByte(sbyte)](#FromSByte_sbyte)</code> - Converts an 8-bit signed integer to an arbitrary-precision decimal number.
* <code>[FromSingle(float)](#FromSingle_float)</code> - Creates an arbitrary-precision decimal number from a 32-bit binary floating-point number.
* <code>[FromSingleBits(int)](#FromSingleBits_int)</code> - Creates an arbitrary-precision decimal number from a 32-bit binary floating-point number encoded in the IEEE 754 binary32 format.
* <code>[FromString(byte[])](#FromString_byte)</code> - Creates an arbitrary-precision decimal number from a sequence of bytes (interpreted as text) that represents a number.
* <code>[FromString(byte[], int, int)](#FromString_byte_int_int)</code> - Creates an arbitrary-precision decimal number from a sequence of bytes (interpreted as text) that represents a number.
* <code>[FromString(byte[], int, int, PeterO.Numbers.EContext)](#FromString_byte_int_int_PeterO_Numbers_EContext)</code> - Creates an arbitrary-precision decimal number from a sequence of bytes (interpreted as text) that represents a number.
* <code>[FromString(byte[], PeterO.Numbers.EContext)](#FromString_byte_PeterO_Numbers_EContext)</code> - Creates an arbitrary-precision decimal number from a sequence of bytes (interpreted as text) that represents a number.
* <code>[FromString(char[])](#FromString_char)</code> - Creates an arbitrary-precision decimal number from a sequence of char s that represents a number.
* <code>[FromString(char[], int, int)](#FromString_char_int_int)</code> - Creates an arbitrary-precision decimal number from a sequence of char s that represents a number.
* <code>[FromString(char[], int, int, PeterO.Numbers.EContext)](#FromString_char_int_int_PeterO_Numbers_EContext)</code> - Creates an arbitrary-precision decimal number from a sequence of char s that represents a number.
* <code>[FromString(char[], PeterO.Numbers.EContext)](#FromString_char_PeterO_Numbers_EContext)</code> - Creates an arbitrary-precision decimal number from a sequence of char s that represents a number.
* <code>[FromString(string)](#FromString_string)</code> - Creates an arbitrary-precision decimal number from a text string that represents a number.
* <code>[FromString(string, int, int)](#FromString_string_int_int)</code> - Creates an arbitrary-precision decimal number from a text string that represents a number.
* <code>[FromString(string, int, int, PeterO.Numbers.EContext)](#FromString_string_int_int_PeterO_Numbers_EContext)</code> - Creates an arbitrary-precision decimal number from a text string that represents a number.
* <code>[FromString(string, PeterO.Numbers.EContext)](#FromString_string_PeterO_Numbers_EContext)</code> - Creates an arbitrary-precision decimal number from a text string that represents a number.
* <code>[FromUInt16(ushort)](#FromUInt16_ushort)</code> - Converts a 16-bit unsigned integer to an arbitrary-precision decimal number.
* <code>[FromUInt32(uint)](#FromUInt32_uint)</code> - Converts a 32-bit signed integer to an arbitrary-precision decimal number.
* <code>[FromUInt64(ulong)](#FromUInt64_ulong)</code> - Converts a 64-bit unsigned integer to an arbitrary-precision decimal number.
* <code>[GetHashCode()](#GetHashCode)</code> - Calculates this object's hash code.
* <code>[implicit operator PeterO.Numbers.EDecimal(byte)](#implicit_operator_PeterO_Numbers_EDecimal_byte)</code> - Converts a byte (from 0 to 255) to an arbitrary-precision decimal number.
* <code>[implicit operator PeterO.Numbers.EDecimal(decimal)](#implicit_operator_PeterO_Numbers_EDecimal_decimal)</code> - Converts an arbitrary-precision decimal number to a decimal under the Common Language Infrastructure (see T:PeterO.
* <code>[implicit operator PeterO.Numbers.EDecimal(int)](#implicit_operator_PeterO_Numbers_EDecimal_int)</code> - Converts a 32-bit signed integer to an arbitrary-precision decimal number.
* <code>[implicit operator PeterO.Numbers.EDecimal(long)](#implicit_operator_PeterO_Numbers_EDecimal_long)</code> - Converts a 64-bit signed integer to an arbitrary-precision decimal number.
* <code>[implicit operator PeterO.Numbers.EDecimal(PeterO.Numbers.EInteger)](#implicit_operator_PeterO_Numbers_EDecimal_PeterO_Numbers_EInteger)</code> - Converts an arbitrary-precision integer to an arbitrary precision decimal.
* <code>[implicit operator PeterO.Numbers.EDecimal(sbyte)](#implicit_operator_PeterO_Numbers_EDecimal_sbyte)</code> - Converts an 8-bit signed integer to an arbitrary-precision decimal number.
* <code>[implicit operator PeterO.Numbers.EDecimal(short)](#implicit_operator_PeterO_Numbers_EDecimal_short)</code> - Converts a 16-bit signed integer to an arbitrary-precision decimal number.
* <code>[implicit operator PeterO.Numbers.EDecimal(uint)](#implicit_operator_PeterO_Numbers_EDecimal_uint)</code> - Converts a 32-bit signed integer to an arbitrary-precision decimal number.
* <code>[implicit operator PeterO.Numbers.EDecimal(ulong)](#implicit_operator_PeterO_Numbers_EDecimal_ulong)</code> - Converts a 64-bit unsigned integer to an arbitrary-precision decimal number.
* <code>[implicit operator PeterO.Numbers.EDecimal(ushort)](#implicit_operator_PeterO_Numbers_EDecimal_ushort)</code> - Converts a 16-bit unsigned integer to an arbitrary-precision decimal number.
* <code>[Increment()](#Increment)</code> - Returns one added to this arbitrary-precision decimal number.
* <code>[IsFinite](#IsFinite)</code> - Gets a value indicating whether this object is finite (not infinity or NaN).
* <code>[IsInfinity()](#IsInfinity)</code> - Gets a value indicating whether this object is positive or negative infinity.
* <code>[IsInteger()](#IsInteger)</code> - Returns whether this object's value is an integer.
* <code>[IsNaN()](#IsNaN)</code> - Gets a value indicating whether this object is not a number (NaN).
* <code>[IsNegative](#IsNegative)</code> - Gets a value indicating whether this object is negative, including negative zero.
* <code>[IsNegativeInfinity()](#IsNegativeInfinity)</code> - Returns whether this object is negative infinity.
* <code>[IsPositiveInfinity()](#IsPositiveInfinity)</code> - Returns whether this object is positive infinity.
* <code>[IsQuietNaN()](#IsQuietNaN)</code> - Gets a value indicating whether this object is a quiet not-a-number value.
* <code>[IsSignalingNaN()](#IsSignalingNaN)</code> - Gets a value indicating whether this object is a signaling not-a-number value.
* <code>[IsZero](#IsZero)</code> - Gets a value indicating whether this object's value equals 0.
* <code>[Log(PeterO.Numbers.EContext)](#Log_PeterO_Numbers_EContext)</code> - Finds the natural logarithm of this object, that is, the power (exponent) that e (the base of natural logarithms) must be raised to in order to equal this object's value.
* <code>[Log10(PeterO.Numbers.EContext)](#Log10_PeterO_Numbers_EContext)</code> - Finds the base-10 logarithm of this object, that is, the power (exponent) that the number 10 must be raised to in order to equal this object's value.
* <code>[Log1P(PeterO.Numbers.EContext)](#Log1P_PeterO_Numbers_EContext)</code> - Adds 1 to this object's value and finds the natural logarithm of the result, in a way that avoids loss of precision when this object's value is between 0 and 1.
* <code>[LogN(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#LogN_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Finds the base-N logarithm of this object, that is, the power (exponent) that the number N must be raised to in order to equal this object's value.
* <code>[Mantissa](#Mantissa)</code> - Gets this object's unscaled value, or significand, and makes it negative if this object is negative.
* <code>[Max(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#Max_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal)</code> - Gets the greater value between two decimal numbers.
* <code>[Max(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Max_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Gets the greater value between two decimal numbers.
* <code>[MaxMagnitude(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#MaxMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal)</code> - Gets the greater value between two values, ignoring their signs.
* <code>[MaxMagnitude(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#MaxMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Gets the greater value between two values, ignoring their signs.
* <code>[Min(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#Min_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal)</code> - Gets the lesser value between two decimal numbers.
* <code>[Min(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Min_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Gets the lesser value between two decimal numbers.
* <code>[MinMagnitude(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#MinMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal)</code> - Gets the lesser value between two values, ignoring their signs.
* <code>[MinMagnitude(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#MinMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Gets the lesser value between two values, ignoring their signs.
* <code>[MovePointLeft(int)](#MovePointLeft_int)</code> - Returns a number similar to this number but with the decimal point moved to the left.
* <code>[MovePointLeft(int, PeterO.Numbers.EContext)](#MovePointLeft_int_PeterO_Numbers_EContext)</code> - Returns a number similar to this number but with the decimal point moved to the left.
* <code>[MovePointLeft(PeterO.Numbers.EInteger)](#MovePointLeft_PeterO_Numbers_EInteger)</code> - Returns a number similar to this number but with the decimal point moved to the left.
* <code>[MovePointLeft(PeterO.Numbers.EInteger, PeterO.Numbers.EContext)](#MovePointLeft_PeterO_Numbers_EInteger_PeterO_Numbers_EContext)</code> - Returns a number similar to this number but with the decimal point moved to the left.
* <code>[MovePointRight(int)](#MovePointRight_int)</code> - Returns a number similar to this number but with the decimal point moved to the right.
* <code>[MovePointRight(int, PeterO.Numbers.EContext)](#MovePointRight_int_PeterO_Numbers_EContext)</code> - Returns a number similar to this number but with the decimal point moved to the right.
* <code>[MovePointRight(PeterO.Numbers.EInteger)](#MovePointRight_PeterO_Numbers_EInteger)</code> - Returns a number similar to this number but with the decimal point moved to the right.
* <code>[MovePointRight(PeterO.Numbers.EInteger, PeterO.Numbers.EContext)](#MovePointRight_PeterO_Numbers_EInteger_PeterO_Numbers_EContext)</code> - Returns a number similar to this number but with the decimal point moved to the right.
* <code>[Multiply(int)](#Multiply_int)</code> - Multiplies this arbitrary-precision decimal floating-point number by a 32-bit signed integer and returns the result.
* <code>[Multiply(long)](#Multiply_long)</code> - Multiplies this arbitrary-precision decimal floating-point number by a 64-bit signed integer and returns the result.
* <code>[Multiply(PeterO.Numbers.EDecimal)](#Multiply_PeterO_Numbers_EDecimal)</code> - Multiplies this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result.
* <code>[Multiply(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Multiply_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Multiplies this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result.
* <code>[MultiplyAndAdd(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#MultiplyAndAdd_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal)</code> - Multiplies by one decimal number, and then adds another decimal number.
* <code>[MultiplyAndAdd(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#MultiplyAndAdd_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Multiplies by one value, and then adds another value.
* <code>[MultiplyAndSubtract(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#MultiplyAndSubtract_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Multiplies by one value, and then subtracts another value.
* <code>[public static readonly PeterO.Numbers.EDecimal NaN;](#NaN)</code> - A not-a-number value.
* <code>[Negate()](#Negate)</code> - Gets an object with the same value as this one, but with the sign reversed.
* <code>[Negate(PeterO.Numbers.EContext)](#Negate_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value as this object but with the sign reversed.
* <code>[public static readonly PeterO.Numbers.EDecimal NegativeInfinity;](#NegativeInfinity)</code> - Negative infinity, less than any other number.
* <code>[public static readonly PeterO.Numbers.EDecimal NegativeZero;](#NegativeZero)</code> - Represents the number negative zero.
* <code>[NextMinus(PeterO.Numbers.EContext)](#NextMinus_PeterO_Numbers_EContext)</code> - Finds the largest value that's smaller than the given value.
* <code>[NextPlus(PeterO.Numbers.EContext)](#NextPlus_PeterO_Numbers_EContext)</code> - Finds the smallest value that's greater than the given value.
* <code>[NextToward(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#NextToward_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Finds the next value that is closer to the other object's value than this object's value.
* <code>[public static readonly PeterO.Numbers.EDecimal One;](#One)</code> - Represents the number 1.
* <code>[PeterO.Numbers.EDecimal operator +(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#op_Addition)</code> - Adds an arbitrary-precision decimal floating-point number and another arbitrary-precision decimal floating-point number and returns the result.
* <code>[PeterO.Numbers.EDecimal operator --(PeterO.Numbers.EDecimal)](#op_Decrement)</code> - Subtracts one from an arbitrary-precision decimal number.
* <code>[PeterO.Numbers.EDecimal operator /(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#op_Division)</code> - Divides this object by another decimal number and returns the result.
* <code>[PeterO.Numbers.EDecimal operator ++(PeterO.Numbers.EDecimal)](#op_Increment)</code> - Adds one to an arbitrary-precision decimal number.
* <code>[PeterO.Numbers.EDecimal operator %(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#op_Modulus)</code> - Returns the remainder that would result when an arbitrary-precision decimal floating-point number is divided by another arbitrary-precision decimal floating-point number.
* <code>[PeterO.Numbers.EDecimal operator &#x2a;(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#op_Multiply)</code> - Multiplies an arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result.
* <code>[PeterO.Numbers.EDecimal operator -(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#op_Subtraction)</code> - Subtracts one arbitrary-precision decimal number from another and returns the result.
* <code>[PeterO.Numbers.EDecimal operator -(PeterO.Numbers.EDecimal)](#op_UnaryNegation)</code> - Gets an arbitrary-precision decimal number with the same value as the given one, but with the sign reversed.
* <code>[PI(PeterO.Numbers.EContext)](#PI_PeterO_Numbers_EContext)</code> - Finds the constant π, the circumference of a circle divided by its diameter.
* <code>[Plus(PeterO.Numbers.EContext)](#Plus_PeterO_Numbers_EContext)</code> - Rounds this object's value to a given precision, using the given rounding mode and range of exponent, and also converts negative zero to positive zero.
* <code>[public static readonly PeterO.Numbers.EDecimal PositiveInfinity;](#PositiveInfinity)</code> - Positive infinity, greater than any other number.
* <code>[Pow(int)](#Pow_int)</code> - Raises this object's value to the given exponent.
* <code>[Pow(int, PeterO.Numbers.EContext)](#Pow_int_PeterO_Numbers_EContext)</code> - Raises this object's value to the given exponent.
* <code>[Pow(PeterO.Numbers.EDecimal)](#Pow_PeterO_Numbers_EDecimal)</code> - Raises this object's value to the given exponent, using unlimited precision.
* <code>[Pow(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Pow_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Raises this object's value to the given exponent.
* <code>[Precision()](#Precision)</code> - Finds the number of digits in this number's significand.
* <code>[PreRound(PeterO.Numbers.EContext)](#PreRound_PeterO_Numbers_EContext)</code> - Returns a number in which the value of this object is rounded to fit the maximum precision allowed if it has more significant digits than the maximum precision.
* <code>[Quantize(int, PeterO.Numbers.EContext)](#Quantize_int_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value but a new exponent.
* <code>[Quantize(int, PeterO.Numbers.ERounding)](#Quantize_int_PeterO_Numbers_ERounding)</code> - Returns an arbitrary-precision decimal number with the same value as this one but a new exponent.
* <code>[Quantize(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Quantize_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value as this object but with the same exponent as another decimal number.
* <code>[Quantize(PeterO.Numbers.EInteger, PeterO.Numbers.EContext)](#Quantize_PeterO_Numbers_EInteger_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value but a new exponent.
* <code>[Reduce(PeterO.Numbers.EContext)](#Reduce_PeterO_Numbers_EContext)</code> - Returns an object with the same numerical value as this one but with trailing zeros removed from its significand.
* <code>[Remainder(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Remainder_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Returns the remainder that would result when this arbitrary-precision decimal floating-point number is divided by another arbitrary-precision decimal floating-point number.
* <code>[RemainderNaturalScale(PeterO.Numbers.EDecimal)](#RemainderNaturalScale_PeterO_Numbers_EDecimal)</code> - Calculates the remainder of a number by the formula "this" - (("this" / "divisor") * "divisor").
* <code>[RemainderNaturalScale(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#RemainderNaturalScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Calculates the remainder of a number by the formula "this" - (("this" / "divisor") * "divisor").
* <code>[RemainderNear(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#RemainderNear_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Finds the distance to the closest multiple of the given divisor, based on the result of dividing this object's value by another object's value.
* <code>[RemainderNoRoundAfterDivide(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#RemainderNoRoundAfterDivide_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Finds the remainder that results when dividing two arbitrary-precision decimal numbers, except the intermediate division is not adjusted to fit the precision of the given arithmetic context.
* <code>[RoundToExponent(int)](#RoundToExponent_int)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary, using the HalfEven rounding mode.
* <code>[RoundToExponent(int, PeterO.Numbers.EContext)](#RoundToExponent_int_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary.
* <code>[RoundToExponent(int, PeterO.Numbers.ERounding)](#RoundToExponent_int_PeterO_Numbers_ERounding)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary.
* <code>[RoundToExponent(PeterO.Numbers.EInteger)](#RoundToExponent_PeterO_Numbers_EInteger)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary, using the HalfEven rounding mode.
* <code>[RoundToExponent(PeterO.Numbers.EInteger, PeterO.Numbers.EContext)](#RoundToExponent_PeterO_Numbers_EInteger_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary.
* <code>[RoundToExponent(PeterO.Numbers.EInteger, PeterO.Numbers.ERounding)](#RoundToExponent_PeterO_Numbers_EInteger_PeterO_Numbers_ERounding)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary, using the given rounding mode.
* <code>[RoundToExponentExact(int, PeterO.Numbers.EContext)](#RoundToExponentExact_int_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to the given exponent represented as a 32-bit signed integer, and signals an inexact flag if the result would be inexact.
* <code>[RoundToExponentExact(int, PeterO.Numbers.ERounding)](#RoundToExponentExact_int_PeterO_Numbers_ERounding)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to the given exponent represented as a 32-bit signed integer, and signals an inexact flag if the result would be inexact.
* <code>[RoundToExponentExact(PeterO.Numbers.EInteger, PeterO.Numbers.EContext)](#RoundToExponentExact_PeterO_Numbers_EInteger_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to the given exponent represented as an arbitrary-precision integer, and signals an inexact flag if the result would be inexact.
* <code>[RoundToIntegerExact(PeterO.Numbers.EContext)](#RoundToIntegerExact_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to an integer, and signals an inexact flag if the result would be inexact.
* <code>[RoundToIntegerNoRoundedFlag(PeterO.Numbers.EContext)](#RoundToIntegerNoRoundedFlag_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision decimal number with the same value as this object but rounded to an integer, without adding the FlagInexact or FlagRounded flags.
* <code>[RoundToIntegralExact(PeterO.Numbers.EContext)](#RoundToIntegralExact_PeterO_Numbers_EContext)</code> - <b>Deprecated:</b> Renamed to RoundToIntegerExact.
* <code>[RoundToIntegralNoRoundedFlag(PeterO.Numbers.EContext)](#RoundToIntegralNoRoundedFlag_PeterO_Numbers_EContext)</code> - <b>Deprecated:</b> Renamed to RoundToIntegerNoRoundedFlag.
* <code>[RoundToPrecision(PeterO.Numbers.EContext)](#RoundToPrecision_PeterO_Numbers_EContext)</code> - Rounds this object's value to a given precision, using the given rounding mode and range of exponent.
* <code>[ScaleByPowerOfTen(int)](#ScaleByPowerOfTen_int)</code> - Returns a number similar to this number but with the scale adjusted.
* <code>[ScaleByPowerOfTen(int, PeterO.Numbers.EContext)](#ScaleByPowerOfTen_int_PeterO_Numbers_EContext)</code> - Returns a number similar to this number but with the scale adjusted.
* <code>[ScaleByPowerOfTen(PeterO.Numbers.EInteger)](#ScaleByPowerOfTen_PeterO_Numbers_EInteger)</code> - Returns a number similar to this number but with the scale adjusted.
* <code>[ScaleByPowerOfTen(PeterO.Numbers.EInteger, PeterO.Numbers.EContext)](#ScaleByPowerOfTen_PeterO_Numbers_EInteger_PeterO_Numbers_EContext)</code> - Returns a number similar to this number but with its scale adjusted.
* <code>[Sign](#Sign)</code> - Gets this value's sign: -1 if negative; 1 if positive; 0 if zero.
* <code>[public static readonly PeterO.Numbers.EDecimal SignalingNaN;](#SignalingNaN)</code> - A not-a-number value that signals an invalid operation flag when it's passed as an argument to any arithmetic operation in arbitrary-precision decimal.
* <code>[Sqrt(PeterO.Numbers.EContext)](#Sqrt_PeterO_Numbers_EContext)</code> - Finds the square root of this object's value.
* <code>[SquareRoot(PeterO.Numbers.EContext)](#SquareRoot_PeterO_Numbers_EContext)</code> - <b>Deprecated:</b> Renamed to Sqrt.
* <code>[Subtract(int)](#Subtract_int)</code> - Subtracts a 32-bit signed integer from this arbitrary-precision decimal floating-point number and returns the result.
* <code>[Subtract(long)](#Subtract_long)</code> - Subtracts a 64-bit signed integer from this arbitrary-precision decimal floating-point number and returns the result.
* <code>[Subtract(PeterO.Numbers.EDecimal)](#Subtract_PeterO_Numbers_EDecimal)</code> - Subtracts an arbitrary-precision decimal floating-point number from this arbitrary-precision decimal floating-point number and returns the result.
* <code>[Subtract(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Subtract_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Subtracts an arbitrary-precision decimal floating-point number from this arbitrary-precision decimal floating-point number and returns the result.
* <code>[public static readonly PeterO.Numbers.EDecimal Ten;](#Ten)</code> - Represents the number 10.
* <code>[ToByteChecked()](#ToByteChecked)</code> - Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) after converting it to an integer by discarding its fractional part.
* <code>[ToByteIfExact()](#ToByteIfExact)</code> - Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) without rounding to a different numerical value.
* <code>[ToByteUnchecked()](#ToByteUnchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a byte (from 0 to 255).
* <code>[ToDecimal()](#ToDecimal)</code> - Converts this value to a decimal under the Common Language Infrastructure (see T:PeterO.
* <code>[ToDouble()](#ToDouble)</code> - Converts this value to its closest equivalent as a 64-bit floating-point number, using the half-even rounding mode.
* <code>[ToDoubleBits()](#ToDoubleBits)</code> - Converts this value to its closest equivalent as a 64-bit floating-point number encoded in the IEEE 754 binary64 format, using the half-even rounding mode.
* <code>[ToEFloat()](#ToEFloat)</code> - Creates a binary floating-point number from this object's value.
* <code>[ToEFloat(PeterO.Numbers.EContext)](#ToEFloat_PeterO_Numbers_EContext)</code> - Creates a binary floating-point number from this object's value.
* <code>[ToEInteger()](#ToEInteger)</code> - Converts this value to an arbitrary-precision integer, discarding the fractional part in this value.
* <code>[ToEIntegerExact()](#ToEIntegerExact)</code> - <b>Deprecated:</b> Renamed to ToEIntegerIfExact.
* <code>[ToEIntegerIfExact()](#ToEIntegerIfExact)</code> - Converts this value to an arbitrary-precision integer, checking whether the fractional part of the value would be lost.
* <code>[ToEngineeringString()](#ToEngineeringString)</code> - Same as ToString(), except that when an exponent is used it will be a multiple of 3.
* <code>[ToExtendedFloat()](#ToExtendedFloat)</code> - <b>Deprecated:</b> Renamed to ToEFloat.
* <code>[ToInt16Checked()](#ToInt16Checked)</code> - Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToInt16IfExact()](#ToInt16IfExact)</code> - Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer without rounding to a different numerical value.
* <code>[ToInt16Unchecked()](#ToInt16Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 16-bit signed integer.
* <code>[ToInt32Checked()](#ToInt32Checked)</code> - Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToInt32IfExact()](#ToInt32IfExact)</code> - Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer without rounding to a different numerical value.
* <code>[ToInt32Unchecked()](#ToInt32Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 32-bit signed integer.
* <code>[ToInt64Checked()](#ToInt64Checked)</code> - Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToInt64IfExact()](#ToInt64IfExact)</code> - Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer without rounding to a different numerical value.
* <code>[ToInt64Unchecked()](#ToInt64Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 64-bit signed integer.
* <code>[ToPlainString()](#ToPlainString)</code> - Converts this value to a string as though with the ToString method, but without using exponential notation.
* <code>[ToSByteChecked()](#ToSByteChecked)</code> - Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToSByteIfExact()](#ToSByteIfExact)</code> - Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer without rounding to a different numerical value.
* <code>[ToSByteUnchecked()](#ToSByteUnchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as an 8-bit signed integer.
* <code>[ToSingle()](#ToSingle)</code> - Converts this value to its closest equivalent as a 32-bit floating-point number, using the half-even rounding mode.
* <code>[ToSingleBits()](#ToSingleBits)</code> - Converts this value to its closest equivalent as a 32-bit floating-point number encoded in the IEEE 754 binary32 format, using the half-even rounding mode.
* <code>[ToSizedEInteger(int)](#ToSizedEInteger_int)</code> - Converts this value to an arbitrary-precision integer by discarding its fractional part and checking whether the resulting integer overflows the given signed bit count.
* <code>[ToSizedEIntegerIfExact(int)](#ToSizedEIntegerIfExact_int)</code> - Converts this value to an arbitrary-precision integer, only if this number's value is an exact integer and that integer does not overflow the given signed bit count.
* <code>[ToString()](#ToString)</code> - Converts this value to a string.
* <code>[ToUInt16Checked()](#ToUInt16Checked)</code> - Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer after converting it to an integer by discarding its fractional part.
* <code>[ToUInt16IfExact()](#ToUInt16IfExact)</code> - Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer without rounding to a different numerical value.
* <code>[ToUInt16Unchecked()](#ToUInt16Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 16-bit unsigned integer.
* <code>[ToUInt32Checked()](#ToUInt32Checked)</code> - Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.
* <code>[ToUInt32IfExact()](#ToUInt32IfExact)</code> - Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer without rounding to a different numerical value.
* <code>[ToUInt32Unchecked()](#ToUInt32Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 32-bit signed integer.
* <code>[ToUInt64Checked()](#ToUInt64Checked)</code> - Converts this number's value to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer after converting it to an integer by discarding its fractional part.
* <code>[ToUInt64IfExact()](#ToUInt64IfExact)</code> - Converts this number's value to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer without rounding to a different numerical value.
* <code>[ToUInt64Unchecked()](#ToUInt64Unchecked)</code> - Converts this number's value to an integer by discarding its fractional part, and returns the least-significant bits of its two's-complement form as a 64-bit unsigned integer.
* <code>[Ulp()](#Ulp)</code> - Returns the unit in the last place.
* <code>[UnsignedMantissa](#UnsignedMantissa)</code> - Gets the absolute value of this object's unscaled value, or significand.
* <code>[public static readonly PeterO.Numbers.EDecimal Zero;](#Zero)</code> - Represents the number 0.

<a id="NaN"></a>
### NaN

    public static readonly PeterO.Numbers.EDecimal NaN;

A not-a-number value.

<a id="NegativeInfinity"></a>
### NegativeInfinity

    public static readonly PeterO.Numbers.EDecimal NegativeInfinity;

Negative infinity, less than any other number.

<a id="NegativeZero"></a>
### NegativeZero

    public static readonly PeterO.Numbers.EDecimal NegativeZero;

Represents the number negative zero.

<a id="One"></a>
### One

    public static readonly PeterO.Numbers.EDecimal One;

Represents the number 1.

<a id="PositiveInfinity"></a>
### PositiveInfinity

    public static readonly PeterO.Numbers.EDecimal PositiveInfinity;

Positive infinity, greater than any other number.

<a id="SignalingNaN"></a>
### SignalingNaN

    public static readonly PeterO.Numbers.EDecimal SignalingNaN;

A not-a-number value that signals an invalid operation flag when it's passed as an argument to any arithmetic operation in arbitrary-precision decimal.

<a id="Ten"></a>
### Ten

    public static readonly PeterO.Numbers.EDecimal Ten;

Represents the number 10.

<a id="Zero"></a>
### Zero

    public static readonly PeterO.Numbers.EDecimal Zero;

Represents the number 0.

<a id="Exponent"></a>
### Exponent

    public PeterO.Numbers.EInteger Exponent { get; }

Gets this object's exponent. This object's value will be an integer if the exponent is positive or zero.

<b>Returns:</b>

This object's exponent. This object's value will be an integer if the exponent is positive or zero.

<a id="IsFinite"></a>
### IsFinite

    public bool IsFinite { get; }

Gets a value indicating whether this object is finite (not infinity or NaN).

<b>Returns:</b>

 `true`  if this object is finite (not infinity or NaN); otherwise,  `false` .

<a id="IsNegative"></a>
### IsNegative

    public bool IsNegative { get; }

Gets a value indicating whether this object is negative, including negative zero.

<b>Returns:</b>

 `true`  if this object is negative, including negative zero; otherwise,  `false` .

<a id="IsZero"></a>
### IsZero

    public bool IsZero { get; }

Gets a value indicating whether this object's value equals 0.

<b>Returns:</b>

 `true`  if this object's value equals 0; otherwise,  `false` .  `true`  if this object's value equals 0; otherwise,  `false` .

<a id="Mantissa"></a>
### Mantissa

    public PeterO.Numbers.EInteger Mantissa { get; }

Gets this object's unscaled value, or significand, and makes it negative if this object is negative. If this value is not-a-number (NaN), that value's absolute value is the NaN's "payload" (diagnostic information).

<b>Returns:</b>

This object's unscaled value. Will be negative if this object's value is negative (including a negative NaN).

<a id="Sign"></a>
### Sign

    public int Sign { get; }

Gets this value's sign: -1 if negative; 1 if positive; 0 if zero.

<b>Returns:</b>

This value's sign: -1 if negative; 1 if positive; 0 if zero.

<a id="UnsignedMantissa"></a>
### UnsignedMantissa

    public PeterO.Numbers.EInteger UnsignedMantissa { get; }

Gets the absolute value of this object's unscaled value, or significand. If this value is not-a-number (NaN), that value is the NaN's "payload" (diagnostic information).

<b>Returns:</b>

The absolute value of this object's unscaled value.

<a id="Abs_PeterO_Numbers_EContext"></a>
### Abs

    public PeterO.Numbers.EDecimal Abs(
        PeterO.Numbers.EContext context);

Finds the absolute value of this object (if it's negative, it becomes positive).

<b>Parameters:</b>

 * <i>context</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The absolute value of this object. Signals FlagInvalid and returns quiet NaN if this value is signaling NaN.

<a id="Abs"></a>
### Abs

    public PeterO.Numbers.EDecimal Abs();

Finds the absolute value of this object (if it's negative, it becomes positive).

<b>Return Value:</b>

An arbitrary-precision decimal number. Returns signaling NaN if this value is signaling NaN. (In this sense, this method is similar to the "copy-abs" operation in the General Decimal Arithmetic Specification, except this method does not necessarily return a copy of this object.).

<a id="Add_int"></a>
### Add

    public PeterO.Numbers.EDecimal Add(
        int intValue);

Adds this arbitrary-precision decimal floating-point number and a 32-bit signed integer and returns the result. The exponent for the result is the lower of this arbitrary-precision decimal floating-point number's exponent and the other 32-bit signed integer's exponent.

<b>Parameters:</b>

 * <i>intValue</i>: A 32-bit signed integer to add to this object.

<b>Return Value:</b>

The sum of the two numbers, that is, this arbitrary-precision decimal floating-point number plus a 32-bit signed integer. If this arbitrary-precision decimal floating-point number is not-a-number (NaN), returns NaN.

<a id="Add_long"></a>
### Add

    public PeterO.Numbers.EDecimal Add(
        long longValue);

Adds this arbitrary-precision decimal floating-point number and a 64-bit signed integer and returns the result. The exponent for the result is the lower of this arbitrary-precision decimal floating-point number's exponent and the other 64-bit signed integer's exponent.

<b>Parameters:</b>

 * <i>longValue</i>: The parameter  <i>longValue</i>
 is a 64-bit signed integer.

<b>Return Value:</b>

The sum of the two numbers, that is, this arbitrary-precision decimal floating-point number plus a 64-bit signed integer. If this arbitrary-precision decimal floating-point number is not-a-number (NaN), returns NaN.

<a id="Add_PeterO_Numbers_EDecimal"></a>
### Add

    public PeterO.Numbers.EDecimal Add(
        PeterO.Numbers.EDecimal otherValue);

Adds this arbitrary-precision decimal floating-point number and another arbitrary-precision decimal floating-point number and returns the result. The exponent for the result is the lower of this arbitrary-precision decimal floating-point number's exponent and the other arbitrary-precision decimal floating-point number's exponent.

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

The sum of the two numbers, that is, this arbitrary-precision decimal floating-point number plus another arbitrary-precision decimal floating-point number. If this arbitrary-precision decimal floating-point number is not-a-number (NaN), returns NaN.

<a id="Add_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Add

    public PeterO.Numbers.EDecimal Add(
        PeterO.Numbers.EDecimal otherValue,
        PeterO.Numbers.EContext ctx);

Adds this arbitrary-precision decimal floating-point number and another arbitrary-precision decimal floating-point number and returns the result.

<b>Parameters:</b>

 * <i>otherValue</i>: The number to add to.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The sum of the two numbers, that is, this arbitrary-precision decimal floating-point number plus another arbitrary-precision decimal floating-point number. If this arbitrary-precision decimal floating-point number is not-a-number (NaN), returns NaN.

<a id="CompareTo_int"></a>
### CompareTo

    public int CompareTo(
        int intOther);

Compares the mathematical values of this object and another object, accepting NaN values. This method currently uses the rules given in the CompareToValue method, so that it it is not consistent with the Equals method, but it may change in a future version to use the rules for the CompareToTotal method instead.

<b>Parameters:</b>

 * <i>intOther</i>: The parameter  <i>intOther</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

Less than 0 if this object's value is less than the other value, or greater than 0 if this object's value is greater than the other value, or 0 if both values are equal.

<a id="CompareTo_long"></a>
### CompareTo

    public int CompareTo(
        long intOther);

Compares the mathematical values of this object and another object, accepting NaN values. This method currently uses the rules given in the CompareToValue method, so that it it is not consistent with the Equals method, but it may change in a future version to use the rules for the CompareToTotal method instead.

<b>Parameters:</b>

 * <i>intOther</i>: The parameter  <i>intOther</i>
 is a 64-bit signed integer.

<b>Return Value:</b>

Less than 0 if this object's value is less than the other value, or greater than 0 if this object's value is greater than the other value, or 0 if both values are equal.

<a id="CompareTo_PeterO_Numbers_EDecimal"></a>
### CompareTo

    public sealed int CompareTo(
        PeterO.Numbers.EDecimal other);

Compares the mathematical values of this object and another object, accepting NaN values. This method currently uses the rules given in the CompareToValue method, so that it it is not consistent with the Equals method, but it may change in a future version to use the rules for the CompareToTotal method instead.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

Less than 0 if this object's value is less than the other value, or greater than 0 if this object's value is greater than the other value or if  <i>other</i>
 is null, or 0 if both values are equal. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToBinary_PeterO_Numbers_EFloat"></a>
### CompareToBinary

    public int CompareToBinary(
        PeterO.Numbers.EFloat other);

Compares an arbitrary-precision binary floating-point number with this instance.

<b>Parameters:</b>

 * <i>other</i>: The other object to compare. Can be null.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less; or a positive number if this instance is greater. Returns 0 if both values are NaN (even signaling NaN) and 1 if this value is NaN (even signaling NaN) and the other isn't, or if the other value is null. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToSignal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### CompareToSignal

    public PeterO.Numbers.EDecimal CompareToSignal(
        PeterO.Numbers.EDecimal other,
        PeterO.Numbers.EContext ctx);

Compares the mathematical values of this object and another object, treating quiet NaN as signaling. In this method, negative zero and positive zero are considered equal.

If this object or the other object is a quiet NaN or signaling NaN, this method will return a quiet NaN and will signal a FlagInvalid flag.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number.

 * <i>ctx</i>: An arithmetic context. The precision, rounding, and exponent range are ignored. If  `HasFlags`  of the context is true, will store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null.

<b>Return Value:</b>

Quiet NaN if this object or the other object is NaN, or 0 if both objects have the same value, or -1 if this object is less than the other value, or a 1 if this object is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToTotal_PeterO_Numbers_EDecimal"></a>
### CompareToTotal

    public int CompareToTotal(
        PeterO.Numbers.EDecimal other);

Compares the values of this object and another object, imposing a total ordering on all possible values. In this method:

 * For objects with the same value, the one with the higher exponent has a greater "absolute value".

 * Negative zero is less than positive zero.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

 * Negative numbers are less than positive numbers.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number to compare with this one.

<b>Return Value:</b>

The number 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToTotal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### CompareToTotal

    public int CompareToTotal(
        PeterO.Numbers.EDecimal other,
        PeterO.Numbers.EContext ctx);

Compares the values of this object and another object, imposing a total ordering on all possible values. In this method:

 * For objects with the same value, the one with the higher exponent has a greater "absolute value".

 * Negative zero is less than positive zero.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

 * Negative numbers are less than positive numbers.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number to compare with this one.

 * <i>ctx</i>: An arithmetic context. Flags will be set in this context only if  `HasFlags`  and  `IsSimplified`  of the context are true and only if an operand needed to be rounded before carrying out the operation. Can be null.

<b>Return Value:</b>

The number 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater. Does not signal flags if either value is signaling NaN. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToTotalMagnitude_PeterO_Numbers_EDecimal"></a>
### CompareToTotalMagnitude

    public int CompareToTotalMagnitude(
        PeterO.Numbers.EDecimal other);

Compares the absolute values of this object and another object, imposing a total ordering on all possible values (ignoring their signs). In this method:

 * For objects with the same value, the one with the higher exponent has a greater "absolute value".

 * Negative zero and positive zero are considered equal.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number to compare with this one.

<b>Return Value:</b>

The number 0 if both objects have the same value (ignoring their signs), or -1 if this object is less than the other value (ignoring their signs), or 1 if this object is greater (ignoring their signs). This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToTotalMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### CompareToTotalMagnitude

    public int CompareToTotalMagnitude(
        PeterO.Numbers.EDecimal other,
        PeterO.Numbers.EContext ctx);

Compares the values of this object and another object, imposing a total ordering on all possible values (ignoring their signs). In this method:

 * For objects with the same value, the one with the higher exponent has a greater "absolute value".

 * Negative zero is less than positive zero.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

 * Negative numbers are less than positive numbers.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number to compare with this one.

 * <i>ctx</i>: An arithmetic context. Flags will be set in this context only if  `HasFlags`  and  `IsSimplified`  of the context are true and only if an operand needed to be rounded before carrying out the operation. Can be null.

<b>Return Value:</b>

The number 0 if both objects have the same value (ignoring their signs), or -1 if this object is less than the other value (ignoring their signs), or 1 if this object is greater (ignoring their signs). Does not signal flags if either value is signaling NaN. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToValue_int"></a>
### CompareToValue

    public int CompareToValue(
        int intOther);

Compares the mathematical values of this object and another object, accepting NaN values. This method is not consistent with the Equals method because two different numbers with the same mathematical value, but different exponents, will compare as equal.

In this method, negative zero and positive zero are considered equal.

If this object is a quiet NaN or signaling NaN, this method will not trigger an error. Instead, NaN will compare greater than any other number, including infinity.

<b>Parameters:</b>

 * <i>intOther</i>: The parameter  <i>intOther</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

Less than 0 if this object's value is less than the other value, or greater than 0 if this object's value is greater than the other value, or 0 if both values are equal.

<a id="CompareToValue_long"></a>
### CompareToValue

    public int CompareToValue(
        long intOther);

Compares the mathematical values of this object and another object, accepting NaN values. This method is not consistent with the Equals method because two different numbers with the same mathematical value, but different exponents, will compare as equal.

In this method, negative zero and positive zero are considered equal.

If this object is a quiet NaN or signaling NaN, this method will not trigger an error. Instead, NaN will compare greater than any other number, including infinity.

<b>Parameters:</b>

 * <i>intOther</i>: The parameter  <i>intOther</i>
 is a 64-bit signed integer.

<b>Return Value:</b>

Less than 0 if this object's value is less than the other value, or greater than 0 if this object's value is greater than the other value, or 0 if both values are equal.

<a id="CompareToValue_PeterO_Numbers_EDecimal"></a>
### CompareToValue

    public int CompareToValue(
        PeterO.Numbers.EDecimal other);

Compares the mathematical values of this object and another object, accepting NaN values. This method is not consistent with the Equals method because two different numbers with the same mathematical value, but different exponents, will compare as equal.

In this method, negative zero and positive zero are considered equal.

If this object or the other object is a quiet NaN or signaling NaN, this method will not trigger an error. Instead, NaN will compare greater than any other number, including infinity. Two different NaN values will be considered equal.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

Less than 0 if this object's value is less than the other value, or greater than 0 if this object's value is greater than the other value or if  <i>other</i>
 is null, or 0 if both values are equal. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="CompareToWithContext_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### CompareToWithContext

    public PeterO.Numbers.EDecimal CompareToWithContext(
        PeterO.Numbers.EDecimal other,
        PeterO.Numbers.EContext ctx);

Compares the mathematical values of this object and another object. In this method, negative zero and positive zero are considered equal.

If this object or the other object is a quiet NaN or signaling NaN, this method returns a quiet NaN, and will signal a FlagInvalid flag if either is a signaling NaN.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number.

 * <i>ctx</i>: An arithmetic context. The precision, rounding, and exponent range are ignored. If  `HasFlags`  of the context is true, will store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null.

<b>Return Value:</b>

Quiet NaN if this object or the other object is NaN, or 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater. This implementation returns a positive number if  <i>other</i>
 is null, to conform to the.NET definition of CompareTo. This is the case even in the Java version of this library, for consistency's sake, even though implementations of  `Comparable.compareTo()`  in Java ought to throw an exception if they receive a null argument rather than treating null as less or greater than any object.

.

<a id="Copy"></a>
### Copy

    public PeterO.Numbers.EDecimal Copy();

Creates a copy of this arbitrary-precision binary number.

<b>Return Value:</b>

An arbitrary-precision decimal floating-point number.

<a id="CopySign_PeterO_Numbers_EDecimal"></a>
### CopySign

    public PeterO.Numbers.EDecimal CopySign(
        PeterO.Numbers.EDecimal other);

Returns a number with the same value as this one, but copying the sign (positive or negative) of another number. (This method is similar to the "copy-sign" operation in the General Decimal Arithmetic Specification, except this method does not necessarily return a copy of this object.).

<b>Parameters:</b>

 * <i>other</i>: A number whose sign will be copied.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>other</i>
 is null.

<a id="Create_int_int"></a>
### Create

    public static PeterO.Numbers.EDecimal Create(
        int mantissaSmall,
        int exponentSmall);

Returns a number with the value  `exponent*10^significand` .

<b>Parameters:</b>

 * <i>mantissaSmall</i>: Desired value for the significand.

 * <i>exponentSmall</i>: Desired value for the exponent.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<a id="Create_long_int"></a>
### Create

    public static PeterO.Numbers.EDecimal Create(
        long mantissaLong,
        int exponentSmall);

Creates a number with the value  `exponent*10^significand` .

<b>Parameters:</b>

 * <i>mantissaLong</i>: Desired value for the significand.

 * <i>exponentSmall</i>: Desired value for the exponent.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<a id="Create_long_long"></a>
### Create

    public static PeterO.Numbers.EDecimal Create(
        long mantissaLong,
        long exponentLong);

Creates a number with the value  `exponent*10^significand` .

<b>Parameters:</b>

 * <i>mantissaLong</i>: Desired value for the significand.

 * <i>exponentLong</i>: Desired value for the exponent.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<a id="Create_PeterO_Numbers_EInteger_int"></a>
### Create

    public static PeterO.Numbers.EDecimal Create(
        PeterO.Numbers.EInteger mantissa,
        int exponentSmall);

Creates a number with the value  `exponent*10^significand` .

<b>Parameters:</b>

 * <i>mantissa</i>: Desired value for the significand.

 * <i>exponentSmall</i>: Desired value for the exponent.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>mantissa</i>
 is null.

<a id="Create_PeterO_Numbers_EInteger_long"></a>
### Create

    public static PeterO.Numbers.EDecimal Create(
        PeterO.Numbers.EInteger mantissa,
        long exponentLong);

Creates a number with the value  `exponent*10^significand` .

<b>Parameters:</b>

 * <i>mantissa</i>: Desired value for the significand.

 * <i>exponentLong</i>: Desired value for the exponent.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>mantissa</i>
 is null.

<a id="Create_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
### Create

    public static PeterO.Numbers.EDecimal Create(
        PeterO.Numbers.EInteger mantissa,
        PeterO.Numbers.EInteger exponent);

Creates a number with the value  `exponent*10^significand` .

<b>Parameters:</b>

 * <i>mantissa</i>: Desired value for the significand.

 * <i>exponent</i>: Desired value for the exponent.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>mantissa</i>
 or  <i>exponent</i>
 is null.

<a id="CreateNaN_PeterO_Numbers_EInteger"></a>
### CreateNaN

    public static PeterO.Numbers.EDecimal CreateNaN(
        PeterO.Numbers.EInteger diag);

Creates a not-a-number arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>diag</i>: An integer, 0 or greater, to use as diagnostic information associated with this object. If none is needed, should be zero. To get the diagnostic information from another arbitrary-precision decimal floating-point number, use that object's  `UnsignedMantissa`  property.

<b>Return Value:</b>

A quiet not-a-number.

<a id="CreateNaN_PeterO_Numbers_EInteger_bool_bool_PeterO_Numbers_EContext"></a>
### CreateNaN

    public static PeterO.Numbers.EDecimal CreateNaN(
        PeterO.Numbers.EInteger diag,
        bool signaling,
        bool negative,
        PeterO.Numbers.EContext ctx);

Creates a not-a-number arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>diag</i>: An integer, 0 or greater, to use as diagnostic information associated with this object. If none is needed, should be zero. To get the diagnostic information from another arbitrary-precision decimal floating-point number, use that object's  `UnsignedMantissa`  property.

 * <i>signaling</i>: Whether the return value will be signaling (true) or quiet (false).

 * <i>negative</i>: Whether the return value is negative.

 * <i>ctx</i>: An arithmetic context to control the precision (in decimal digits) of the diagnostic information. The rounding and exponent range of this context will be ignored. Can be null. The only flag that can be signaled in this context is FlagInvalid, which happens if diagnostic information needs to be truncated and too much memory is required to do so.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>diag</i>
 is null or is less than 0.

<a id="Decrement"></a>
### Decrement

    public PeterO.Numbers.EDecimal Decrement();

Returns one subtracted from this arbitrary-precision decimal number.

<b>Return Value:</b>

The given arbitrary-precision decimal number minus one.

<a id="Divide_int"></a>
### Divide

    public PeterO.Numbers.EDecimal Divide(
        int intValue);

Divides this arbitrary-precision decimal floating-point number by a 32-bit signed integer and returns the result; returns NaN instead if the result would have a nonterminating decimal expansion (including 1/3, 1/12, 1/7, 2/3, and so on); if this is not desired, use DivideToExponent, or use the Divide overload that takes an EContext.

<b>Parameters:</b>

 * <i>intValue</i>: A 32-bit signed integer, the divisor, to divide this object by.

<b>Return Value:</b>

The result of dividing this arbitrary-precision decimal floating-point number by a 32-bit signed integer. Returns infinity if the divisor (this arbitrary-precision decimal floating-point number) is 0 and the dividend (the other 32-bit signed integer) is nonzero. Returns not-a-number (NaN) if the divisor and the dividend are 0. Returns NaN if the result can't be exact because it would have a nonterminating binary expansion (examples include 1 divided by any multiple of 3, such as 1/3 or 1/12). If this is not desired, use DivideToExponent instead, or use the Divide overload that takes an  `EContext`  (such as  `EContext.Decimal128`  ) instead.

<a id="Divide_long"></a>
### Divide

    public PeterO.Numbers.EDecimal Divide(
        long longValue);

Divides this arbitrary-precision decimal floating-point number by a 64-bit signed integer and returns the result; returns NaN instead if the result would have a nonterminating decimal expansion (including 1/3, 1/12, 1/7, 2/3, and so on); if this is not desired, use DivideToExponent, or use the Divide overload that takes an EContext.

<b>Parameters:</b>

 * <i>longValue</i>: The parameter  <i>longValue</i>
 is a 64-bit signed integer.

<b>Return Value:</b>

The result of dividing this arbitrary-precision decimal floating-point number by a 64-bit signed integer. Returns infinity if the divisor (this arbitrary-precision decimal floating-point number) is 0 and the dividend (the other 64-bit signed integer) is nonzero. Returns not-a-number (NaN) if the divisor and the dividend are 0. Returns NaN if the result can't be exact because it would have a nonterminating binary expansion (examples include 1 divided by any multiple of 3, such as 1/3 or 1/12). If this is not desired, use DivideToExponent instead, or use the Divide overload that takes an  `EContext`  (such as  `EContext.Decimal128`  ) instead.

<a id="Divide_PeterO_Numbers_EDecimal"></a>
### Divide

    public PeterO.Numbers.EDecimal Divide(
        PeterO.Numbers.EDecimal divisor);

Divides this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result; returns NaN instead if the result would have a nonterminating decimal expansion (including 1/3, 1/12, 1/7, 2/3, and so on); if this is not desired, use DivideToExponent, or use the Divide overload that takes an EContext.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

The result of dividing this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number. Returns infinity if the divisor (this arbitrary-precision decimal floating-point number) is 0 and the dividend (the other arbitrary-precision decimal floating-point number) is nonzero. Returns not-a-number (NaN) if the divisor and the dividend are 0. Returns NaN if the result can't be exact because it would have a nonterminating binary expansion (examples include 1 divided by any multiple of 3, such as 1/3 or 1/12). If this is not desired, use DivideToExponent instead, or use the Divide overload that takes an  `EContext`  (such as  `EContext.Decimal128`  ) instead.

<a id="Divide_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Divide

    public PeterO.Numbers.EDecimal Divide(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EContext ctx);

Divides this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The result of dividing this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number. Signals FlagDivideByZero and returns infinity if the divisor (this arbitrary-precision decimal floating-point number) is 0 and the dividend (the other arbitrary-precision decimal floating-point number) is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0; or, either  <i>ctx</i>
 is null or  <i>ctx</i>
 's precision is 0, and the result would have a nonterminating decimal expansion (examples include 1 divided by any multiple of 3, such as 1/3 or 1/12); or, the rounding mode is ERounding.None and the result is not exact.

<a id="DivideAndRemainderNaturalScale_PeterO_Numbers_EDecimal"></a>
### DivideAndRemainderNaturalScale

    public PeterO.Numbers.EDecimal[] DivideAndRemainderNaturalScale(
        PeterO.Numbers.EDecimal divisor);

<b>Deprecated.</b> Renamed to DivRemNaturalScale.

Calculates the quotient and remainder using the DivideToIntegerNaturalScale and the formula in RemainderNaturalScale.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

A 2 element array consisting of the quotient and remainder in that order.

<a id="DivideAndRemainderNaturalScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### DivideAndRemainderNaturalScale

    public PeterO.Numbers.EDecimal[] DivideAndRemainderNaturalScale(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to DivRemNaturalScale.

Calculates the quotient and remainder using the DivideToIntegerNaturalScale and the formula in RemainderNaturalScale.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only in the division portion of the remainder calculation; as a result, it's possible for the remainder to have a higher precision than given in this context. Flags will be set on the given context only if the context's  `HasFlags`  is true and the integer part of the division result doesn't fit the precision and exponent range without rounding. Can be null, in which the precision is unlimited and no additional rounding, other than the rounding down to an integer after division, is needed.

<b>Return Value:</b>

A 2 element array consisting of the quotient and remainder in that order.

<a id="DivideToExponent_PeterO_Numbers_EDecimal_int"></a>
### DivideToExponent

    public PeterO.Numbers.EDecimal DivideToExponent(
        PeterO.Numbers.EDecimal divisor,
        int desiredExponentInt);

Divides two arbitrary-precision decimal numbers, and gives a particular exponent (expressed as a 32-bit signed integer) to the result, using the half-even rounding mode.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>desiredExponentInt</i>: The desired exponent. A negative number places the cutoff point to the right of the usual decimal point (so a negative number means the number of decimal places to round to). A positive number places the cutoff point to the left of the usual decimal point.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0.

<a id="DivideToExponent_PeterO_Numbers_EDecimal_int_PeterO_Numbers_EContext"></a>
### DivideToExponent

    public PeterO.Numbers.EDecimal DivideToExponent(
        PeterO.Numbers.EDecimal divisor,
        int desiredExponentInt,
        PeterO.Numbers.EContext ctx);

Divides two arbitrary-precision decimal numbers, and gives a particular exponent (expressed as a 32-bit signed integer) to the result, using the half-even rounding mode.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>desiredExponentInt</i>: The desired exponent. A negative number places the cutoff point to the right of the usual decimal point (so a negative number means the number of decimal places to round to). A positive number places the cutoff point to the left of the usual decimal point.

 * <i>ctx</i>: An arithmetic context object to control the rounding mode to use if the result must be scaled down to have the same exponent as this value. If the precision given in the context is other than 0, calls the Quantize method with both arguments equal to the result of the operation (and can signal FlagInvalid and return NaN if the result doesn't fit the given precision). If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the context defines an exponent range and the desired exponent is outside that range. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

<a id="DivideToExponent_PeterO_Numbers_EDecimal_int_PeterO_Numbers_ERounding"></a>
### DivideToExponent

    public PeterO.Numbers.EDecimal DivideToExponent(
        PeterO.Numbers.EDecimal divisor,
        int desiredExponentInt,
        PeterO.Numbers.ERounding rounding);

Divides two arbitrary-precision decimal numbers, and gives a particular exponent (expressed as a 32-bit signed integer) to the result, using the half-even rounding mode.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>desiredExponentInt</i>: The desired exponent. A negative number places the cutoff point to the right of the usual decimal point (so a negative number means the number of decimal places to round to). A positive number places the cutoff point to the left of the usual decimal point.

 * <i>rounding</i>: The rounding mode to use if the result must be scaled down to have the same exponent as this value.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

<a id="DivideToExponent_PeterO_Numbers_EDecimal_long"></a>
### DivideToExponent

    public PeterO.Numbers.EDecimal DivideToExponent(
        PeterO.Numbers.EDecimal divisor,
        long desiredExponentSmall);

Divides two arbitrary-precision decimal numbers, and gives a particular exponent (expressed as a 64-bit signed integer) to the result, using the half-even rounding mode.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>desiredExponentSmall</i>: The desired exponent. A negative number places the cutoff point to the right of the usual decimal point (so a negative number means the number of decimal places to round to). A positive number places the cutoff point to the left of the usual decimal point.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0.

<a id="DivideToExponent_PeterO_Numbers_EDecimal_long_PeterO_Numbers_EContext"></a>
### DivideToExponent

    public PeterO.Numbers.EDecimal DivideToExponent(
        PeterO.Numbers.EDecimal divisor,
        long desiredExponentSmall,
        PeterO.Numbers.EContext ctx);

Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>desiredExponentSmall</i>: The desired exponent. A negative number places the cutoff point to the right of the usual decimal point (so a negative number means the number of decimal places to round to). A positive number places the cutoff point to the left of the usual decimal point.

 * <i>ctx</i>: An arithmetic context object to control the rounding mode to use if the result must be scaled down to have the same exponent as this value. If the precision given in the context is other than 0, calls the Quantize method with both arguments equal to the result of the operation (and can signal FlagInvalid and return NaN if the result doesn't fit the given precision). If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the context defines an exponent range and the desired exponent is outside that range. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

<a id="DivideToExponent_PeterO_Numbers_EDecimal_long_PeterO_Numbers_ERounding"></a>
### DivideToExponent

    public PeterO.Numbers.EDecimal DivideToExponent(
        PeterO.Numbers.EDecimal divisor,
        long desiredExponentSmall,
        PeterO.Numbers.ERounding rounding);

Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>desiredExponentSmall</i>: The desired exponent. A negative number places the cutoff point to the right of the usual decimal point (so a negative number means the number of decimal places to round to). A positive number places the cutoff point to the left of the usual decimal point.

 * <i>rounding</i>: The rounding mode to use if the result must be scaled down to have the same exponent as this value.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

<a id="DivideToExponent_PeterO_Numbers_EDecimal_PeterO_Numbers_EInteger_PeterO_Numbers_ERounding"></a>
### DivideToExponent

    public PeterO.Numbers.EDecimal DivideToExponent(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EInteger desiredExponent,
        PeterO.Numbers.ERounding rounding);

Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>desiredExponent</i>: The desired exponent. A negative number places the cutoff point to the right of the usual decimal point (so a negative number means the number of decimal places to round to). A positive number places the cutoff point to the left of the usual decimal point.

 * <i>rounding</i>: The rounding mode to use if the result must be scaled down to have the same exponent as this value.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Returns not-a-number (NaN) if the divisor and the dividend are 0. Returns NaN if the rounding mode is ERounding.None and the result is not exact.

<a id="DivideToExponent_PeterO_Numbers_EDecimal_PeterO_Numbers_EInteger"></a>
### DivideToExponent

    public PeterO.Numbers.EDecimal DivideToExponent(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EInteger exponent);

Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result, using the half-even rounding mode.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>exponent</i>: The desired exponent. A negative number places the cutoff point to the right of the usual decimal point (so a negative number means the number of decimal places to round to). A positive number places the cutoff point to the left of the usual decimal point.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0.

<a id="DivideToExponent_PeterO_Numbers_EDecimal_PeterO_Numbers_EInteger_PeterO_Numbers_EContext"></a>
### DivideToExponent

    public PeterO.Numbers.EDecimal DivideToExponent(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EInteger exponent,
        PeterO.Numbers.EContext ctx);

Divides two arbitrary-precision decimal numbers, and gives a particular exponent to the result.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>exponent</i>: The desired exponent. A negative number places the cutoff point to the right of the usual decimal point (so a negative number means the number of decimal places to round to). A positive number places the cutoff point to the left of the usual decimal point.

 * <i>ctx</i>: An arithmetic context object to control the rounding mode to use if the result must be scaled down to have the same exponent as this value. If the precision given in the context is other than 0, calls the Quantize method with both arguments equal to the result of the operation (and can signal FlagInvalid and return NaN if the result doesn't fit the given precision). If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the context defines an exponent range and the desired exponent is outside that range. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

<a id="DivideToIntegerNaturalScale_PeterO_Numbers_EDecimal"></a>
### DivideToIntegerNaturalScale

    public PeterO.Numbers.EDecimal DivideToIntegerNaturalScale(
        PeterO.Numbers.EDecimal divisor);

Divides two arbitrary-precision decimal numbers, and returns the integer part of the result, rounded down, with the preferred exponent set to this value's exponent minus the divisor's exponent.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

The integer part of the quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0.

<a id="DivideToIntegerNaturalScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### DivideToIntegerNaturalScale

    public PeterO.Numbers.EDecimal DivideToIntegerNaturalScale(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EContext ctx);

Divides this object by another object, and returns the integer part of the result (which is initially rounded down), with the preferred exponent set to this value's exponent minus the divisor's exponent.

<b>Parameters:</b>

 * <i>divisor</i>: The parameter  <i>divisor</i>
 is an arbitrary-precision decimal floating-point number.

 * <i>ctx</i>: The parameter  <i>ctx</i>
 is an EContext object.

<b>Return Value:</b>

The integer part of the quotient of the two objects. Signals FlagInvalid and returns not-a-number (NaN) if the return value would overflow the exponent range. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

<a id="DivideToIntegerZeroScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### DivideToIntegerZeroScale

    public PeterO.Numbers.EDecimal DivideToIntegerZeroScale(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EContext ctx);

Divides this object by another object, and returns the integer part of the result, with the exponent set to 0.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision. The rounding and exponent range settings of this context are ignored. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited.

<b>Return Value:</b>

The integer part of the quotient of the two objects. The exponent will be set to 0. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0, or if the result doesn't fit the given precision.

<a id="DivideToSameExponent_PeterO_Numbers_EDecimal_PeterO_Numbers_ERounding"></a>
### DivideToSameExponent

    public PeterO.Numbers.EDecimal DivideToSameExponent(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.ERounding rounding);

Divides this object by another decimal number and returns a result with the same exponent as this object (the dividend).

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>rounding</i>: The rounding mode to use if the result must be scaled down to have the same exponent as this value.

<b>Return Value:</b>

The quotient of the two numbers. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

<a id="DivRemNaturalScale_PeterO_Numbers_EDecimal"></a>
### DivRemNaturalScale

    public PeterO.Numbers.EDecimal[] DivRemNaturalScale(
        PeterO.Numbers.EDecimal divisor);

Divides this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns a two-item array containing the result of the division and the remainder, in that order. The result of division is calculated as though by  `DivideToIntegerNaturalScale` , and the remainder is calculated as though by  `RemainderNaturalScale` .

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

An array of two items: the first is the result of the division as an arbitrary-precision decimal floating-point number, and the second is the remainder as an arbitrary-precision decimal floating-point number. The result of division is the result of the method on the two operands, and the remainder is the result of the Remainder method on the two operands.

<a id="DivRemNaturalScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### DivRemNaturalScale

    public PeterO.Numbers.EDecimal[] DivRemNaturalScale(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EContext ctx);

Divides this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns a two-item array containing the result of the division and the remainder, in that order. The result of division is calculated as though by  `DivideToIntegerNaturalScale` , and the remainder is calculated as though by  `RemainderNaturalScale` .

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only in the division portion of the remainder calculation; as a result, it's possible for the remainder to have a higher precision than given in this context. Flags will be set on the given context only if the context's  `HasFlags`  is true and the integer part of the division result doesn't fit the precision and exponent range without rounding. Can be null, in which the precision is unlimited and no additional rounding, other than the rounding down to an integer after division, is needed.

<b>Return Value:</b>

An array of two items: the first is the result of the division as an arbitrary-precision decimal floating-point number, and the second is the remainder as an arbitrary-precision decimal floating-point number. The result of division is the result of the method on the two operands, and the remainder is the result of the Remainder method on the two operands.

<a id="Equals_object"></a>
### Equals

    public override bool Equals(
        object obj);

Determines whether this object's significand, exponent, and properties are equal to those of another object and that other object is an arbitrary-precision decimal number. Not-a-number values are considered equal if the rest of their properties are equal.

<b>Parameters:</b>

 * <i>obj</i>: The parameter  <i>obj</i>
 is an arbitrary object.

<b>Return Value:</b>

 `true`  if the objects are equal; otherwise,  `false` . In this method, two objects are not equal if they don't have the same type or if one is null and the other isn't.

<a id="Equals_PeterO_Numbers_EDecimal"></a>
### Equals

    public sealed bool Equals(
        PeterO.Numbers.EDecimal other);

Determines whether this object's significand, exponent, and properties are equal to those of another object. Not-a-number values are considered equal if the rest of their properties are equal.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

 `true`  if this object's significand and exponent are equal to those of another object; otherwise,  `false` .

<a id="Exp_PeterO_Numbers_EContext"></a>
### Exp

    public PeterO.Numbers.EDecimal Exp(
        PeterO.Numbers.EContext ctx);

Finds e (the base of natural logarithms) raised to the power of this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). <i>This parameter can't be null, as the exponential function's results are generally not exact.</i> (Unlike in the General Decimal Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

Exponential of this object. If this object's value is 1, returns an approximation to " e" within the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

<a id="ExpM1_PeterO_Numbers_EContext"></a>
### ExpM1

    public PeterO.Numbers.EDecimal ExpM1(
        PeterO.Numbers.EContext ctx);

Finds e (the base of natural logarithms) raised to the power of this object's value, and subtracts the result by 1 and returns the final result, in a way that avoids loss of precision if the true result is very close to 0.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). <i>This parameter can't be null, as the exponential function's results are generally not exact.</i> (Unlike in the General Binary Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

Exponential of this object, minus 1. Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

<a id="FromBoolean_bool"></a>
### FromBoolean

    public static PeterO.Numbers.EDecimal FromBoolean(
        bool boolValue);

Converts a boolean value (true or false) to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>boolValue</i>: Either true or false.

<b>Return Value:</b>

The number 1 if  <i>boolValue</i>
 is true; otherwise, 0.

<a id="FromByte_byte"></a>
### FromByte

    public static PeterO.Numbers.EDecimal FromByte(
        byte inputByte);

Converts a byte (from 0 to 255) to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputByte</i>: The number to convert as a byte (from 0 to 255).

<b>Return Value:</b>

This number's value as an arbitrary-precision decimal number.

<a id="FromDecimal_decimal"></a>
### FromDecimal

    public static PeterO.Numbers.EDecimal FromDecimal(
        decimal dec);

Converts a  `decimal`  under the Common Language Infrastructure (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ) to an arbitrary-precision decimal.

<b>Parameters:</b>

 * <i>dec</i>: A  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal).

<b>Return Value:</b>

An arbitrary-precision decimal floating-point number.

<a id="FromDouble_double"></a>
### FromDouble

    public static PeterO.Numbers.EDecimal FromDouble(
        double dbl);

Creates an arbitrary-precision decimal number from a 64-bit binary floating-point number. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the floating point number to a string first. Remember, though, that the exact value of a 64-bit binary floating-point number is not always the value that results when passing a literal decimal number (for example, calling  `EDecimal.FromDouble(0.1)`  ), since not all decimal numbers can be converted to exact binary numbers (in the example given, the resulting arbitrary-precision decimal will be the value of the closest "double" to 0.1, not 0.1 exactly). To create an arbitrary-precision decimal number from a decimal value, use FromString instead in most cases (for example:  `EDecimal.FromString("0.1")`  ).

<b>Parameters:</b>

 * <i>dbl</i>: The parameter  <i>dbl</i>
 is a 64-bit floating-point number.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as  <i>dbl</i>
.

<a id="FromDoubleBits_long"></a>
### FromDoubleBits

    public static PeterO.Numbers.EDecimal FromDoubleBits(
        long dblBits);

Creates an arbitrary-precision decimal number from a 64-bit binary floating-point number, encoded in the IEEE 754 binary64 format. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the floating point number to a string first. Remember, though, that the exact value of a 64-bit binary floating-point number is not always the value that results when passing a literal decimal number, since not all decimal numbers can be converted to exact binary numbers (in the example given, the resulting arbitrary-precision decimal will be the value of the closest "double" to 0.1, not 0.1 exactly). To create an arbitrary-precision decimal number from a decimal value, use FromString instead in most cases.

<b>Parameters:</b>

 * <i>dblBits</i>: The parameter  <i>dblBits</i>
 is a 64-bit signed integer.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as  <i>dblBits</i>
.

<a id="FromEFloat_PeterO_Numbers_EFloat"></a>
### FromEFloat

    public static PeterO.Numbers.EDecimal FromEFloat(
        PeterO.Numbers.EFloat bigfloat);

Creates an arbitrary-precision decimal number from an arbitrary-precision binary floating-point number.

<b>Parameters:</b>

 * <i>bigfloat</i>: An arbitrary-precision binary floating-point number.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigfloat</i>
 is null.

<a id="FromEInteger_PeterO_Numbers_EInteger"></a>
### FromEInteger

    public static PeterO.Numbers.EDecimal FromEInteger(
        PeterO.Numbers.EInteger bigint);

Converts an arbitrary-precision integer to an arbitrary precision decimal.

<b>Parameters:</b>

 * <i>bigint</i>: An arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision decimal number with the exponent set to 0.

<a id="FromExtendedFloat_PeterO_Numbers_EFloat"></a>
### FromExtendedFloat

    public static PeterO.Numbers.EDecimal FromExtendedFloat(
        PeterO.Numbers.EFloat ef);

<b>Deprecated.</b> Renamed to FromEFloat.

Converts an arbitrary-precision binary floating-point number to an arbitrary precision decimal.

<b>Parameters:</b>

 * <i>ef</i>: An arbitrary-precision binary floating-point number.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<a id="FromInt16_short"></a>
### FromInt16

    public static PeterO.Numbers.EDecimal FromInt16(
        short inputInt16);

Converts a 16-bit signed integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputInt16</i>: The number to convert as a 16-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision decimal number.

<a id="FromInt32_int"></a>
### FromInt32

    public static PeterO.Numbers.EDecimal FromInt32(
        int valueSmaller);

Creates an arbitrary-precision decimal number from a 32-bit signed integer.

<b>Parameters:</b>

 * <i>valueSmaller</i>: The parameter  <i>valueSmaller</i>
 is a 32-bit signed integer.

<b>Return Value:</b>

An arbitrary-precision decimal number with the exponent set to 0.

<a id="FromInt64_long"></a>
### FromInt64

    public static PeterO.Numbers.EDecimal FromInt64(
        long valueSmall);

Creates an arbitrary-precision decimal number from a 64-bit signed integer.

<b>Parameters:</b>

 * <i>valueSmall</i>: The parameter  <i>valueSmall</i>
 is a 64-bit signed integer.

<b>Return Value:</b>

An arbitrary-precision decimal number with the exponent set to 0.

<a id="FromSByte_sbyte"></a>
### FromSByte

    public static PeterO.Numbers.EDecimal FromSByte(
        sbyte inputSByte);

<b>This API is not CLS-compliant.</b>

Converts an 8-bit signed integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputSByte</i>: The number to convert as an 8-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision decimal number.

<a id="FromSingle_float"></a>
### FromSingle

    public static PeterO.Numbers.EDecimal FromSingle(
        float flt);

Creates an arbitrary-precision decimal number from a 32-bit binary floating-point number. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the floating point number to a string first. Remember, though, that the exact value of a 32-bit binary floating-point number is not always the value that results when passing a literal decimal number (for example, calling  `EDecimal.FromSingle(0.1f)`  ), since not all decimal numbers can be converted to exact binary numbers (in the example given, the resulting arbitrary-precision decimal will be the the value of the closest "float" to 0.1, not 0.1 exactly). To create an arbitrary-precision decimal number from a decimal value, use FromString instead in most cases (for example:  `EDecimal.FromString("0.1")`  ).

<b>Parameters:</b>

 * <i>flt</i>: The parameter  <i>flt</i>
 is a 32-bit binary floating-point number.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as  <i>flt</i>
.

<a id="FromSingleBits_int"></a>
### FromSingleBits

    public static PeterO.Numbers.EDecimal FromSingleBits(
        int value);

Creates an arbitrary-precision decimal number from a 32-bit binary floating-point number encoded in the IEEE 754 binary32 format. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the floating point number to a string first. Remember, though, that the exact value of a 32-bit binary floating-point number is not always the value that results when passing a literal decimal number, since not all decimal numbers can be converted to exact binary numbers (in the example given, the resulting arbitrary-precision decimal will be the the value of the closest "float" to 0.1, not 0.1 exactly). To create an arbitrary-precision decimal number from a decimal value, use FromString instead in most cases.

<b>Parameters:</b>

 * <i>value</i>: A 32-bit binary floating-point number encoded in the IEEE 754 binary32 format.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as  <i>value</i>
.

<a id="FromString_byte"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        byte[] bytes);

Creates an arbitrary-precision decimal number from a sequence of bytes (interpreted as text) that represents a number. See  `FromString(String, int, int, EContext)`  for more information. Note that calling the overload that takes an EContext is often much faster than creating the EDecimal then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Parameters:</b>

 * <i>bytes</i>: A sequence that represents a number.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given sequence of bytes (interpreted as text).

<b>Exceptions:</b>

 * System.FormatException:
The parameter  <i>bytes</i>
 is not a correctly formatted number sequence.

<a id="FromString_byte_int_int"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        byte[] bytes,
        int offset,
        int length);

Creates an arbitrary-precision decimal number from a sequence of bytes (interpreted as text) that represents a number. See  `FromString(String, int, int, EContext)`  for more information. Note that calling the overload that takes an EContext is often much faster than creating the EDecimal then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Parameters:</b>

 * <i>bytes</i>: A sequence that represents a number.

 * <i>offset</i>: An index starting at 0 showing where the desired portion of  <i>bytes</i>
 begins.

 * <i>length</i>: The length, in code units, of the desired portion of  <i>bytes</i>
 (but not more than  <i>bytes</i>
 's length).

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given sequence of bytes (interpreted as text).

<b>Exceptions:</b>

 * System.FormatException:
The parameter  <i>bytes</i>
 is not a correctly formatted number sequence.

 * System.ArgumentNullException:
The parameter  <i>bytes</i>
 is null.

 * System.ArgumentException:
Either  <i>offset</i>
 or  <i>length</i>
 is less than 0 or greater than  <i>bytes</i>
 's length, or  <i>bytes</i>
 's length minus  <i>offset</i>
 is less than  <i>length</i>
.

<a id="FromString_byte_int_int_PeterO_Numbers_EContext"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        byte[] bytes,
        int offset,
        int length,
        PeterO.Numbers.EContext ctx);

Creates an arbitrary-precision decimal number from a sequence of bytes (interpreted as text) that represents a number. Each byte in the sequence has to be a code point in the Basic Latin range (0x00 to 0x7f or U+0000 to U+007F) of the Unicode Standard.

The format of the sequence generally consists of:

 * An optional plus sign ("+" , U+002B) or minus sign ("-", U+002D) (if the minus sign, the value is negative.)

 * One or more digits, with a single optional decimal point (".", U+002E) before or after those digits or between two of them. These digits may begin with any number of zeros.

 * Optionally, "E"/"e" followed by an optional (positive exponent) or "-" (negative exponent) and followed by one or more digits specifying the exponent (these digits may begin with any number of zeros).

The sequence can also be "-INF", "-Infinity", "Infinity", "INF", quiet NaN ("NaN" /"-NaN") followed by any number of digits (these digits may begin with any number of zeros), or signaling NaN ("sNaN" /"-sNaN") followed by any number of digits (these digits may begin with any number of zeros), all where the letters can be any combination of basic upper-case and/or basic lower-case letters.

All characters mentioned above are the corresponding characters in the Basic Latin range. In particular, the digits must be the basic digits 0 to 9 (U+0030 to U+0039). The sequence is not allowed to contain white space characters, including spaces.

<b>Parameters:</b>

 * <i>bytes</i>: A sequence of bytes (interpreted as text), a portion of which represents a number.

 * <i>offset</i>: An index starting at 0 showing where the desired portion of  <i>bytes</i>
 begins.

 * <i>length</i>: The length, in code units, of the desired portion of  <i>bytes</i>
 (but not more than  <i>bytes</i>
 's length).

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. Note that providing a context is often much faster than creating the EDecimal without a context then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given sequence of bytes (interpreted as text).

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bytes</i>
 is null.

 * System.ArgumentException:
Either  <i>offset</i>
 or  <i>length</i>
 is less than 0 or greater than  <i>bytes</i>
 's length, or  <i>bytes</i>
 's length minus  <i>offset</i>
 is less than  <i>length</i>
.

<a id="FromString_byte_PeterO_Numbers_EContext"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        byte[] bytes,
        PeterO.Numbers.EContext ctx);

Creates an arbitrary-precision decimal number from a sequence of bytes (interpreted as text) that represents a number. See  `FromString(String, int, int, EContext)`  for more information.

<b>Parameters:</b>

 * <i>bytes</i>: A sequence of bytes (interpreted as text) that represents a number.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. Note that providing a context is often much faster than creating the EDecimal without a context then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given sequence of bytes (interpreted as text).

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bytes</i>
 is null.

<a id="FromString_char"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        char[] chars);

Creates an arbitrary-precision decimal number from a sequence of  `char`  s that represents a number. See  `FromString(String, int, int, EContext)`  for more information. Note that calling the overload that takes an EContext is often much faster than creating the EDecimal then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Parameters:</b>

 * <i>chars</i>: A sequence that represents a number.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given sequence of  `char`  s.

<b>Exceptions:</b>

 * System.FormatException:
The parameter  <i>chars</i>
 is not a correctly formatted number sequence.

<a id="FromString_char_int_int"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        char[] chars,
        int offset,
        int length);

Creates an arbitrary-precision decimal number from a sequence of  `char`  s that represents a number. See  `FromString(String, int, int, EContext)`  for more information. Note that calling the overload that takes an EContext is often much faster than creating the EDecimal then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Parameters:</b>

 * <i>chars</i>: A sequence that represents a number.

 * <i>offset</i>: An index starting at 0 showing where the desired portion of  <i>chars</i>
 begins.

 * <i>length</i>: The length, in code units, of the desired portion of  <i>chars</i>
 (but not more than  <i>chars</i>
 's length).

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given sequence of  `char`  s.

<b>Exceptions:</b>

 * System.FormatException:
The parameter  <i>chars</i>
 is not a correctly formatted number sequence.

 * System.ArgumentNullException:
The parameter  <i>chars</i>
 is null.

 * System.ArgumentException:
Either  <i>offset</i>
 or  <i>length</i>
 is less than 0 or greater than  <i>chars</i>
 's length, or  <i>chars</i>
 's length minus  <i>offset</i>
 is less than  <i>length</i>
.

<a id="FromString_char_int_int_PeterO_Numbers_EContext"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        char[] chars,
        int offset,
        int length,
        PeterO.Numbers.EContext ctx);

Creates an arbitrary-precision decimal number from a sequence of  `char`  s that represents a number.

The format of the sequence generally consists of:

 * An optional plus sign ("+" , U+002B) or minus sign ("-", U+002D) (if the minus sign, the value is negative.)

 * One or more digits, with a single optional decimal point (".", U+002E) before or after those digits or between two of them. These digits may begin with any number of zeros.

 * Optionally, "E"/"e" followed by an optional (positive exponent) or "-" (negative exponent) and followed by one or more digits specifying the exponent (these digits may begin with any number of zeros).

The sequence can also be "-INF", "-Infinity", "Infinity", "INF", quiet NaN ("NaN" /"-NaN") followed by any number of digits (these digits may begin with any number of zeros), or signaling NaN ("sNaN" /"-sNaN") followed by any number of digits (these digits may begin with any number of zeros), all where the letters can be any combination of basic upper-case and/or basic lower-case letters.

All characters mentioned above are the corresponding characters in the Basic Latin range. In particular, the digits must be the basic digits 0 to 9 (U+0030 to U+0039). The sequence is not allowed to contain white space characters, including spaces.

<b>Parameters:</b>

 * <i>chars</i>: A sequence of  `char`  s, a portion of which represents a number.

 * <i>offset</i>: An index starting at 0 showing where the desired portion of  <i>chars</i>
 begins.

 * <i>length</i>: The length, in code units, of the desired portion of  <i>chars</i>
 (but not more than  <i>chars</i>
 's length).

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. Note that providing a context is often much faster than creating the EDecimal without a context then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given sequence of  `char`  s.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>chars</i>
 is null.

 * System.ArgumentException:
Either  <i>offset</i>
 or  <i>length</i>
 is less than 0 or greater than  <i>chars</i>
 's length, or  <i>chars</i>
 's length minus  <i>offset</i>
 is less than  <i>length</i>
.

<a id="FromString_char_PeterO_Numbers_EContext"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        char[] chars,
        PeterO.Numbers.EContext ctx);

Creates an arbitrary-precision decimal number from a sequence of  `char`  s that represents a number. See  `FromString(String, int, int, EContext)`  for more information.

<b>Parameters:</b>

 * <i>chars</i>: A sequence of  `char`  s that represents a number.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. Note that providing a context is often much faster than creating the EDecimal without a context then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given sequence of  `char`  s.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>chars</i>
 is null.

<a id="FromString_string"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        string str);

Creates an arbitrary-precision decimal number from a text string that represents a number. See  `FromString(String, int,
            int, EContext)`  for more information. Note that calling the overload that takes an EContext is often much faster than creating the EDecimal then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Parameters:</b>

 * <i>str</i>: A string that represents a number.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given string.

<b>Exceptions:</b>

 * System.FormatException:
The parameter  <i>str</i>
 is not a correctly formatted number string.

<a id="FromString_string_int_int"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        string str,
        int offset,
        int length);

Creates an arbitrary-precision decimal number from a text string that represents a number. See  `FromString(String, int,
            int, EContext)`  for more information. Note that calling the overload that takes an EContext is often much faster than creating the EDecimal then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Parameters:</b>

 * <i>str</i>: A string that represents a number.

 * <i>offset</i>: An index starting at 0 showing where the desired portion of  <i>str</i>
 begins.

 * <i>length</i>: The length, in code units, of the desired portion of  <i>str</i>
 (but not more than  <i>str</i>
 's length).

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given string.

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

<a id="FromString_string_int_int_PeterO_Numbers_EContext"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        string str,
        int offset,
        int length,
        PeterO.Numbers.EContext ctx);

Creates an arbitrary-precision decimal number from a text string that represents a number.

The format of the string generally consists of:

 * An optional plus sign ("+" , U+002B) or minus sign ("-", U+002D) (if the minus sign, the value is negative.)

 * One or more digits, with a single optional decimal point (".", U+002E) before or after those digits or between two of them. These digits may begin with any number of zeros.

 * Optionally, "E"/"e" followed by an optional (positive exponent) or "-" (negative exponent) and followed by one or more digits specifying the exponent (these digits may begin with any number of zeros).

The string can also be "-INF", "-Infinity", "Infinity", "INF", quiet NaN ("NaN" /"-NaN") followed by any number of digits (these digits may begin with any number of zeros), or signaling NaN ("sNaN" /"-sNaN") followed by any number of digits (these digits may begin with any number of zeros), all where the letters can be any combination of basic upper-case and/or basic lower-case letters.

All characters mentioned above are the corresponding characters in the Basic Latin range. In particular, the digits must be the basic digits 0 to 9 (U+0030 to U+0039). The string is not allowed to contain white space characters, including spaces.

<b>Parameters:</b>

 * <i>str</i>: A text string, a portion of which represents a number.

 * <i>offset</i>: An index starting at 0 showing where the desired portion of  <i>str</i>
 begins.

 * <i>length</i>: The length, in code units, of the desired portion of  <i>str</i>
 (but not more than  <i>str</i>
 's length).

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. Note that providing a context is often much faster than creating the EDecimal without a context then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given string.

<b>Exceptions:</b>

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

<a id="FromString_string_PeterO_Numbers_EContext"></a>
### FromString

    public static PeterO.Numbers.EDecimal FromString(
        string str,
        PeterO.Numbers.EContext ctx);

Creates an arbitrary-precision decimal number from a text string that represents a number. See  `FromString(String, int,
            int, EContext)`  for more information.

<b>Parameters:</b>

 * <i>str</i>: A string that represents a number.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. Note that providing a context is often much faster than creating the EDecimal without a context then calling  `RoundToPrecision`  on that EDecimal, especially if the context specifies a precision limit and exponent range.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as the given string.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>str</i>
 is null.

<a id="FromUInt16_ushort"></a>
### FromUInt16

    public static PeterO.Numbers.EDecimal FromUInt16(
        ushort inputUInt16);

<b>This API is not CLS-compliant.</b>

Converts a 16-bit unsigned integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputUInt16</i>: The number to convert as a 16-bit unsigned integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision decimal number.

<a id="FromUInt32_uint"></a>
### FromUInt32

    public static PeterO.Numbers.EDecimal FromUInt32(
        uint inputUInt32);

<b>This API is not CLS-compliant.</b>

Converts a 32-bit signed integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputUInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision decimal number.

<a id="FromUInt64_ulong"></a>
### FromUInt64

    public static PeterO.Numbers.EDecimal FromUInt64(
        ulong inputUInt64);

<b>This API is not CLS-compliant.</b>

Converts a 64-bit unsigned integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputUInt64</i>: The number to convert as a 64-bit unsigned integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision decimal number.

<a id="GetHashCode"></a>
### GetHashCode

    public override int GetHashCode();

Calculates this object's hash code. No application or process IDs are used in the hash code calculation.

<b>Return Value:</b>

A 32-bit signed integer.

<a id="Increment"></a>
### Increment

    public PeterO.Numbers.EDecimal Increment();

Returns one added to this arbitrary-precision decimal number.

<b>Return Value:</b>

The given arbitrary-precision decimal number plus one.

<a id="IsInfinity"></a>
### IsInfinity

    public bool IsInfinity();

Gets a value indicating whether this object is positive or negative infinity.

<b>Return Value:</b>

 `true`  if this object is positive or negative infinity; otherwise,  `false` .

<a id="IsInteger"></a>
### IsInteger

    public bool IsInteger();

Returns whether this object's value is an integer.

<b>Return Value:</b>

 `true`  if this object's value is an integer; otherwise,  `false` .

<a id="IsNaN"></a>
### IsNaN

    public bool IsNaN();

Gets a value indicating whether this object is not a number (NaN).

<b>Return Value:</b>

 `true`  if this object is not a number (NaN); otherwise,  `false` .

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

Gets a value indicating whether this object is a quiet not-a-number value.

<b>Return Value:</b>

 `true`  if this object is a quiet not-a-number value; otherwise,  `false` .

<a id="IsSignalingNaN"></a>
### IsSignalingNaN

    public bool IsSignalingNaN();

Gets a value indicating whether this object is a signaling not-a-number value.

<b>Return Value:</b>

 `true`  if this object is a signaling not-a-number value; otherwise,  `false` .

<a id="Log_PeterO_Numbers_EContext"></a>
### Log

    public PeterO.Numbers.EDecimal Log(
        PeterO.Numbers.EContext ctx);

Finds the natural logarithm of this object, that is, the power (exponent) that e (the base of natural logarithms) must be raised to in order to equal this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). <i>This parameter can't be null, as the ln function's results are generally not exact.</i> (Unlike in the General Decimal Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

Ln(this object). Signals the flag FlagInvalid and returns NaN if this object is less than 0 (the result would be a complex number with a real part equal to Ln of this object's absolute value and an imaginary part equal to pi, but the return value is still NaN.). Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0). Signals no flags and returns negative infinity if this object's value is 0.

<a id="Log10_PeterO_Numbers_EContext"></a>
### Log10

    public PeterO.Numbers.EDecimal Log10(
        PeterO.Numbers.EContext ctx);

Finds the base-10 logarithm of this object, that is, the power (exponent) that the number 10 must be raised to in order to equal this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). <i>This parameter can't be null, as the ln function's results are generally not exact.</i> (Unlike in the General Decimal Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

Ln(this object)/Ln(10). Signals the flag FlagInvalid and returns not-a-number (NaN) if this object is less than 0. Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

<a id="Log1P_PeterO_Numbers_EContext"></a>
### Log1P

    public PeterO.Numbers.EDecimal Log1P(
        PeterO.Numbers.EContext ctx);

Adds 1 to this object's value and finds the natural logarithm of the result, in a way that avoids loss of precision when this object's value is between 0 and 1.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). <i>This parameter can't be null, as the ln function's results are generally not exact.</i> (Unlike in the General Binary Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

Ln(1+(this object)). Signals the flag FlagInvalid and returns NaN if this object is less than -1 (the result would be a complex number with a real part equal to Ln of 1 plus this object's absolute value and an imaginary part equal to pi, but the return value is still NaN.). Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0). Signals no flags and returns negative infinity if this object's value is 0.

<a id="LogN_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### LogN

    public PeterO.Numbers.EDecimal LogN(
        PeterO.Numbers.EDecimal baseValue,
        PeterO.Numbers.EContext ctx);

Finds the base-N logarithm of this object, that is, the power (exponent) that the number N must be raised to in order to equal this object's value.

<b>Parameters:</b>

 * <i>baseValue</i>: The parameter  <i>baseValue</i>
 is a Numbers.EDecimal object.

 * <i>ctx</i>: The parameter  <i>ctx</i>
 is a Numbers.EContext object.

<b>Return Value:</b>

Ln(this object)/Ln(baseValue). Signals the flag FlagInvalid and returns not-a-number (NaN) if this object is less than 0. Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>baseValue</i>
 is null.

<a id="Max_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal"></a>
### Max

    public static PeterO.Numbers.EDecimal Max(
        PeterO.Numbers.EDecimal first,
        PeterO.Numbers.EDecimal second);

Gets the greater value between two decimal numbers.

<b>Parameters:</b>

 * <i>first</i>: An arbitrary-precision decimal number.

 * <i>second</i>: Another arbitrary-precision decimal number.

<b>Return Value:</b>

The larger value of the two numbers. If one is positive zero and the other is negative zero, returns the positive zero. If the two numbers are positive and have the same value, returns the one with the larger exponent. If the two numbers are negative and have the same value, returns the one with the smaller exponent.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="Max_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Max

    public static PeterO.Numbers.EDecimal Max(
        PeterO.Numbers.EDecimal first,
        PeterO.Numbers.EDecimal second,
        PeterO.Numbers.EContext ctx);

Gets the greater value between two decimal numbers.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The larger value of the two numbers. If one is positive zero and the other is negative zero, returns the positive zero. If the two numbers are positive and have the same value, returns the one with the larger exponent. If the two numbers are negative and have the same value, returns the one with the smaller exponent.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="MaxMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal"></a>
### MaxMagnitude

    public static PeterO.Numbers.EDecimal MaxMagnitude(
        PeterO.Numbers.EDecimal first,
        PeterO.Numbers.EDecimal second);

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

<a id="MaxMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### MaxMagnitude

    public static PeterO.Numbers.EDecimal MaxMagnitude(
        PeterO.Numbers.EDecimal first,
        PeterO.Numbers.EDecimal second,
        PeterO.Numbers.EContext ctx);

Gets the greater value between two values, ignoring their signs. If the absolute values are equal, has the same effect as Max.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The larger value of the two numbers, ignoring their signs.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="Min_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal"></a>
### Min

    public static PeterO.Numbers.EDecimal Min(
        PeterO.Numbers.EDecimal first,
        PeterO.Numbers.EDecimal second);

Gets the lesser value between two decimal numbers.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

<b>Return Value:</b>

The smaller value of the two numbers. If one is positive zero and the other is negative zero, returns the negative zero. If the two numbers are positive and have the same value, returns the one with the smaller exponent. If the two numbers are negative and have the same value, returns the one with the larger exponent.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="Min_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Min

    public static PeterO.Numbers.EDecimal Min(
        PeterO.Numbers.EDecimal first,
        PeterO.Numbers.EDecimal second,
        PeterO.Numbers.EContext ctx);

Gets the lesser value between two decimal numbers.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The smaller value of the two numbers. If one is positive zero and the other is negative zero, returns the negative zero. If the two numbers are positive and have the same value, returns the one with the smaller exponent. If the two numbers are negative and have the same value, returns the one with the larger exponent.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="MinMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal"></a>
### MinMagnitude

    public static PeterO.Numbers.EDecimal MinMagnitude(
        PeterO.Numbers.EDecimal first,
        PeterO.Numbers.EDecimal second);

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

<a id="MinMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### MinMagnitude

    public static PeterO.Numbers.EDecimal MinMagnitude(
        PeterO.Numbers.EDecimal first,
        PeterO.Numbers.EDecimal second,
        PeterO.Numbers.EContext ctx);

Gets the lesser value between two values, ignoring their signs. If the absolute values are equal, has the same effect as Min.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The smaller value of the two numbers, ignoring their signs.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>first</i>
 or  <i>second</i>
 is null.

<a id="MovePointLeft_int"></a>
### MovePointLeft

    public PeterO.Numbers.EDecimal MovePointLeft(
        int places);

Returns a number similar to this number but with the decimal point moved to the left.

<b>Parameters:</b>

 * <i>places</i>: The number of decimal places to move the decimal point to the left. If this number is negative, instead moves the decimal point to the right by this number's absolute value.

<b>Return Value:</b>

A number whose exponent is decreased by  <i>places</i>
, but not to more than 0.

<a id="MovePointLeft_int_PeterO_Numbers_EContext"></a>
### MovePointLeft

    public PeterO.Numbers.EDecimal MovePointLeft(
        int places,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the decimal point moved to the left.

<b>Parameters:</b>

 * <i>places</i>: The number of decimal places to move the decimal point to the left. If this number is negative, instead moves the decimal point to the right by this number's absolute value.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

A number whose exponent is decreased by  <i>places</i>
, but not to more than 0.

<a id="MovePointLeft_PeterO_Numbers_EInteger"></a>
### MovePointLeft

    public PeterO.Numbers.EDecimal MovePointLeft(
        PeterO.Numbers.EInteger bigPlaces);

Returns a number similar to this number but with the decimal point moved to the left.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The number of decimal places to move the decimal point to the left. If this number is negative, instead moves the decimal point to the right by this number's absolute value.

<b>Return Value:</b>

A number whose exponent is decreased by  <i>bigPlaces</i>
, but not to more than 0.

<a id="MovePointLeft_PeterO_Numbers_EInteger_PeterO_Numbers_EContext"></a>
### MovePointLeft

    public PeterO.Numbers.EDecimal MovePointLeft(
        PeterO.Numbers.EInteger bigPlaces,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the decimal point moved to the left.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The number of decimal places to move the decimal point to the left. If this number is negative, instead moves the decimal point to the right by this number's absolute value.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

A number whose exponent is decreased by  <i>bigPlaces</i>
, but not to more than 0.

<a id="MovePointRight_int"></a>
### MovePointRight

    public PeterO.Numbers.EDecimal MovePointRight(
        int places);

Returns a number similar to this number but with the decimal point moved to the right.

<b>Parameters:</b>

 * <i>places</i>: The number of decimal places to move the decimal point to the right. If this number is negative, instead moves the decimal point to the left by this number's absolute value.

<b>Return Value:</b>

A number whose exponent is increased by  <i>places</i>
, but not to more than 0.

<a id="MovePointRight_int_PeterO_Numbers_EContext"></a>
### MovePointRight

    public PeterO.Numbers.EDecimal MovePointRight(
        int places,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the decimal point moved to the right.

<b>Parameters:</b>

 * <i>places</i>: The number of decimal places to move the decimal point to the right. If this number is negative, instead moves the decimal point to the left by this number's absolute value.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

A number whose exponent is increased by  <i>places</i>
, but not to more than 0.

<a id="MovePointRight_PeterO_Numbers_EInteger"></a>
### MovePointRight

    public PeterO.Numbers.EDecimal MovePointRight(
        PeterO.Numbers.EInteger bigPlaces);

Returns a number similar to this number but with the decimal point moved to the right.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The number of decimal places to move the decimal point to the right. If this number is negative, instead moves the decimal point to the left by this number's absolute value.

<b>Return Value:</b>

A number whose exponent is increased by  <i>bigPlaces</i>
, but not to more than 0.

<a id="MovePointRight_PeterO_Numbers_EInteger_PeterO_Numbers_EContext"></a>
### MovePointRight

    public PeterO.Numbers.EDecimal MovePointRight(
        PeterO.Numbers.EInteger bigPlaces,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the decimal point moved to the right.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The number of decimal places to move the decimal point to the right. If this number is negative, instead moves the decimal point to the left by this number's absolute value.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

A number whose exponent is increased by  <i>bigPlaces</i>
, but not to more than 0.

<a id="Multiply_int"></a>
### Multiply

    public PeterO.Numbers.EDecimal Multiply(
        int intValue);

Multiplies this arbitrary-precision decimal floating-point number by a 32-bit signed integer and returns the result. The exponent for the result is this arbitrary-precision decimal floating-point number's exponent plus the other 32-bit signed integer's exponent.

<b>Parameters:</b>

 * <i>intValue</i>: A 32-bit signed integer to multiply this object by.

<b>Return Value:</b>

The product of the two numbers, that is, this arbitrary-precision decimal floating-point number times a 32-bit signed integer.

<a id="Multiply_long"></a>
### Multiply

    public PeterO.Numbers.EDecimal Multiply(
        long longValue);

Multiplies this arbitrary-precision decimal floating-point number by a 64-bit signed integer and returns the result. The exponent for the result is this arbitrary-precision decimal floating-point number's exponent plus the other 64-bit signed integer's exponent.

<b>Parameters:</b>

 * <i>longValue</i>: The parameter  <i>longValue</i>
 is a 64-bit signed integer.

<b>Return Value:</b>

The product of the two numbers, that is, this arbitrary-precision decimal floating-point number times a 64-bit signed integer.

<a id="Multiply_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Multiply

    public PeterO.Numbers.EDecimal Multiply(
        PeterO.Numbers.EDecimal op,
        PeterO.Numbers.EContext ctx);

Multiplies this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result.

<b>Parameters:</b>

 * <i>op</i>: Another decimal number.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The product of the two numbers, that is, this arbitrary-precision decimal floating-point number times another arbitrary-precision decimal floating-point number.

<a id="Multiply_PeterO_Numbers_EDecimal"></a>
### Multiply

    public PeterO.Numbers.EDecimal Multiply(
        PeterO.Numbers.EDecimal otherValue);

Multiplies this arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result. The exponent for the result is this arbitrary-precision decimal floating-point number's exponent plus the other arbitrary-precision decimal floating-point number's exponent.

<b>Parameters:</b>

 * <i>otherValue</i>: Another decimal number.

<b>Return Value:</b>

The product of the two numbers, that is, this arbitrary-precision decimal floating-point number times another arbitrary-precision decimal floating-point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>otherValue</i>
 is null.

<a id="MultiplyAndAdd_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal"></a>
### MultiplyAndAdd

    public PeterO.Numbers.EDecimal MultiplyAndAdd(
        PeterO.Numbers.EDecimal multiplicand,
        PeterO.Numbers.EDecimal augend);

Multiplies by one decimal number, and then adds another decimal number.

<b>Parameters:</b>

 * <i>multiplicand</i>: The value to multiply.

 * <i>augend</i>: The value to add.

<b>Return Value:</b>

An arbitrary-precision decimal floating-point number.

<a id="MultiplyAndAdd_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### MultiplyAndAdd

    public PeterO.Numbers.EDecimal MultiplyAndAdd(
        PeterO.Numbers.EDecimal op,
        PeterO.Numbers.EDecimal augend,
        PeterO.Numbers.EContext ctx);

Multiplies by one value, and then adds another value.

<b>Parameters:</b>

 * <i>op</i>: The value to multiply.

 * <i>augend</i>: The value to add.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. If the precision doesn't indicate a simplified arithmetic, rounding and precision/exponent adjustment is done only once, namely, after multiplying and adding.

<b>Return Value:</b>

The result thisValue * multiplicand + augend.

<a id="MultiplyAndSubtract_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### MultiplyAndSubtract

    public PeterO.Numbers.EDecimal MultiplyAndSubtract(
        PeterO.Numbers.EDecimal op,
        PeterO.Numbers.EDecimal subtrahend,
        PeterO.Numbers.EContext ctx);

Multiplies by one value, and then subtracts another value.

<b>Parameters:</b>

 * <i>op</i>: The value to multiply.

 * <i>subtrahend</i>: The value to subtract.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. If the precision doesn't indicate a simplified arithmetic, rounding and precision/exponent adjustment is done only once, namely, after multiplying and subtracting.

<b>Return Value:</b>

The result thisValue * multiplicand - subtrahend.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>op</i>
 or  <i>subtrahend</i>
 is null.

<a id="Negate_PeterO_Numbers_EContext"></a>
### Negate

    public PeterO.Numbers.EDecimal Negate(
        PeterO.Numbers.EContext context);

Returns an arbitrary-precision decimal number with the same value as this object but with the sign reversed.

<b>Parameters:</b>

 * <i>context</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

An arbitrary-precision decimal number. If this value is positive zero, returns positive zero. Signals FlagInvalid and returns quiet NaN if this value is signaling NaN.

<a id="Negate"></a>
### Negate

    public PeterO.Numbers.EDecimal Negate();

Gets an object with the same value as this one, but with the sign reversed.

<b>Return Value:</b>

An arbitrary-precision decimal number. If this value is positive zero, returns negative zero. Returns signaling NaN if this value is signaling NaN. (In this sense, this method is similar to the "copy-negate" operation in the General Decimal Arithmetic Specification, except this method does not necessarily return a copy of this object.).

<a id="NextMinus_PeterO_Numbers_EContext"></a>
### NextMinus

    public PeterO.Numbers.EDecimal NextMinus(
        PeterO.Numbers.EContext ctx);

Finds the largest value that's smaller than the given value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision and exponent range of the result. The rounding mode from this context is ignored. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).

<b>Return Value:</b>

Returns the largest value that's less than the given value. Returns negative infinity if the result is negative infinity. Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null, the precision is 0, or  <i>ctx</i>
 has an unlimited exponent range.

<a id="NextPlus_PeterO_Numbers_EContext"></a>
### NextPlus

    public PeterO.Numbers.EDecimal NextPlus(
        PeterO.Numbers.EContext ctx);

Finds the smallest value that's greater than the given value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision and exponent range of the result. The rounding mode from this context is ignored. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).

<b>Return Value:</b>

Returns the smallest value that's greater than the given value.Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null, the precision is 0, or  <i>ctx</i>
 has an unlimited exponent range.

<a id="NextToward_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### NextToward

    public PeterO.Numbers.EDecimal NextToward(
        PeterO.Numbers.EDecimal otherValue,
        PeterO.Numbers.EContext ctx);

Finds the next value that is closer to the other object's value than this object's value. Returns a copy of this value with the same sign as the other value if both values are equal.

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision decimal number that the return value will approach.

 * <i>ctx</i>: An arithmetic context object to control the precision and exponent range of the result. The rounding mode from this context is ignored. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).

<b>Return Value:</b>

Returns the next value that is closer to the other object' s value than this object's value. Signals FlagInvalid and returns NaN if the parameter  <i>ctx</i>
 is null, the precision is 0, or  <i>ctx</i>
 has an unlimited exponent range.

<a id="op_Addition"></a>
### Operator `+`

    public static PeterO.Numbers.EDecimal operator +(
        PeterO.Numbers.EDecimal bthis,
        PeterO.Numbers.EDecimal otherValue);

Adds an arbitrary-precision decimal floating-point number and another arbitrary-precision decimal floating-point number and returns the result.

<b>Parameters:</b>

 * <i>bthis</i>: The first arbitrary-precision decimal floating-point number.

 * <i>otherValue</i>: The second decimal binary floating-point number.

<b>Return Value:</b>

The sum of the two numbers, that is, an arbitrary-precision decimal floating-point number plus another arbitrary-precision decimal floating-point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 or  <i>otherValue</i>
 is null.

<a id="op_Decrement"></a>
### Operator `--`

    public static PeterO.Numbers.EDecimal operator --(
        PeterO.Numbers.EDecimal bthis);

Subtracts one from an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>bthis</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

The given arbitrary-precision decimal number minus one.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_Division"></a>
### Operator `/`

    public static PeterO.Numbers.EDecimal operator /(
        PeterO.Numbers.EDecimal dividend,
        PeterO.Numbers.EDecimal divisor);

Divides this object by another decimal number and returns the result. When possible, the result will be exact.

<b>Parameters:</b>

 * <i>dividend</i>: The number that will be divided by the divisor.

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

The quotient of the two numbers. Returns infinity if the divisor is 0 and the dividend is nonzero. Returns not-a-number (NaN) if the divisor and the dividend are 0. Returns NaN if the result can't be exact because it would have a nonterminating decimal expansion; examples include 1 divided by any multiple of 3, such as 1/3 or 1/12. If this is not desired, use DivideToExponent instead, or use the Divide overload that takes an EContext instead.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>dividend</i>
 is null.

<a id="explicit_operator_byte_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator byte(
        PeterO.Numbers.EDecimal input);

Converts an arbitrary-precision decimal number to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision decimal number.

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

<a id="explicit_operator_decimal_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator decimal(
        PeterO.Numbers.EDecimal bigValue);

Converts an arbitrary-precision decimal's value to a  `decimal`  under the Common Language Infrastructure (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ), using the half-even rounding mode.

<b>Parameters:</b>

 * <i>bigValue</i>: The parameter  <i>bigValue</i>
 is an arbitrary-precision decimal floating-point number.

<b>Return Value:</b>

A  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal).

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="explicit_operator_double_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator double(
        PeterO.Numbers.EDecimal bigValue);

Converts this value to its closest equivalent as a 64-bit floating-point number. The half-even rounding mode is used. If this value is a NaN, sets the high bit of the 64-bit floating point number's significand area for a quiet NaN, and clears it for a signaling NaN. Then the other bits of the significand area are set to the lowest bits of this object's unsigned significand, and the next-highest bit of the significand area is set if those bits are all zeros and this is a signaling NaN. Unfortunately, in the.NET implementation, the return value of this method may be a quiet NaN even if a signaling NaN would otherwise be generated.

<b>Parameters:</b>

 * <i>bigValue</i>: The value to convert to a 64-bit floating-point number.

<b>Return Value:</b>

The closest 64-bit floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 64-bit floating point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="explicit_operator_float_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator float(
        PeterO.Numbers.EDecimal bigValue);

Converts this value to its closest equivalent as a 32-bit floating-point number. The half-even rounding mode is used. If this value is a NaN, sets the high bit of the 32-bit floating point number's significand area for a quiet NaN, and clears it for a signaling NaN. Then the other bits of the significand area are set to the lowest bits of this object's unsigned significand, and the next-highest bit of the significand area is set if those bits are all zeros and this is a signaling NaN. Unfortunately, in the.NET implementation, the return value of this method may be a quiet NaN even if a signaling NaN would otherwise be generated.

<b>Parameters:</b>

 * <i>bigValue</i>: The number to convert as an arbitrary-precision decimal number.

<b>Return Value:</b>

The closest 32-bit binary floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 32-bit floating point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="explicit_operator_int_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator int(
        PeterO.Numbers.EDecimal input);

Converts an arbitrary-precision decimal number to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision decimal number.

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

<a id="explicit_operator_long_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator long(
        PeterO.Numbers.EDecimal input);

Converts an arbitrary-precision decimal number to a 64-bit signed integer if it can fit in a 64-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision decimal number.

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

<a id="explicit_operator_PeterO_Numbers_EDecimal_bool"></a>
### Explicit Operator

    public static explicit operator PeterO.Numbers.EDecimal(
        bool boolValue);

Converts a boolean value (true or false) to an arbitrary precision decimal.

<b>Parameters:</b>

 * <i>boolValue</i>: Either true or false.

<b>Return Value:</b>

The number 1 if  <i>boolValue</i>
 is true; otherwise, 0.

<a id="explicit_operator_PeterO_Numbers_EInteger_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator PeterO.Numbers.EInteger(
        PeterO.Numbers.EDecimal bigValue);

Converts an arbitrary-precision decimal floating-point number to an arbitrary-precision integer. Any fractional part in this value will be discarded when converting to an arbitrary-precision integer.

<b>Parameters:</b>

 * <i>bigValue</i>: The number to convert as an arbitrary-precision decimal.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="explicit_operator_sbyte_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator sbyte(
        PeterO.Numbers.EDecimal input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision decimal number to an 8-bit signed integer if it can fit in an 8-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision decimal number.

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

<a id="explicit_operator_short_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator short(
        PeterO.Numbers.EDecimal input);

Converts an arbitrary-precision decimal number to a 16-bit signed integer if it can fit in a 16-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision decimal number.

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

<a id="explicit_operator_uint_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator uint(
        PeterO.Numbers.EDecimal input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision decimal number to a 32-bit signed integer if it can fit in a 32-bit signed integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision decimal number.

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

<a id="explicit_operator_ulong_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator ulong(
        PeterO.Numbers.EDecimal input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision decimal number to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision decimal number.

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

<a id="explicit_operator_ushort_PeterO_Numbers_EDecimal"></a>
### Explicit Operator

    public static explicit operator ushort(
        PeterO.Numbers.EDecimal input);

<b>This API is not CLS-compliant.</b>

Converts an arbitrary-precision decimal number to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer after converting it to an integer by discarding its fractional part.

<b>Parameters:</b>

 * <i>input</i>: The number to convert as an arbitrary-precision decimal number.

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

<a id="implicit_operator_PeterO_Numbers_EDecimal_byte"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        byte inputByte);

Converts a byte (from 0 to 255) to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputByte</i>: The number to convert as a byte (from 0 to 255).

<b>Return Value:</b>

The value of  <i>inputByte</i>
 as an arbitrary-precision decimal number.

<a id="implicit_operator_PeterO_Numbers_EDecimal_decimal"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        decimal dec);

Converts an arbitrary-precision decimal number to a  `decimal`  under the Common Language Infrastructure (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ), using the half-even rounding mode.

<b>Parameters:</b>

 * <i>dec</i>: The number to convert as an arbitrary-precision decimal floating-point number.

<b>Return Value:</b>

A  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal).

<a id="implicit_operator_PeterO_Numbers_EDecimal_int"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        int inputInt32);

Converts a 32-bit signed integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputInt32</i>
 as an arbitrary-precision decimal number.

<a id="implicit_operator_PeterO_Numbers_EDecimal_long"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        long inputInt64);

Converts a 64-bit signed integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputInt64</i>: The number to convert as a 64-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputInt64</i>
 as an arbitrary-precision decimal number.

<a id="implicit_operator_PeterO_Numbers_EDecimal_PeterO_Numbers_EInteger"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        PeterO.Numbers.EInteger eint);

Converts an arbitrary-precision integer to an arbitrary precision decimal.

<b>Parameters:</b>

 * <i>eint</i>: An arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision decimal number with the exponent set to 0.

<a id="implicit_operator_PeterO_Numbers_EDecimal_sbyte"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        sbyte inputSByte);

<b>This API is not CLS-compliant.</b>

Converts an 8-bit signed integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputSByte</i>: The number to convert as an 8-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputSByte</i>
 as an arbitrary-precision decimal number.

<a id="implicit_operator_PeterO_Numbers_EDecimal_short"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        short inputInt16);

Converts a 16-bit signed integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputInt16</i>: The number to convert as a 16-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputInt16</i>
 as an arbitrary-precision decimal number.

<a id="implicit_operator_PeterO_Numbers_EDecimal_uint"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        uint inputUInt32);

<b>This API is not CLS-compliant.</b>

Converts a 32-bit signed integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputUInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

The value of  <i>inputUInt32</i>
 as an arbitrary-precision decimal number.

<a id="implicit_operator_PeterO_Numbers_EDecimal_ulong"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        ulong inputUInt64);

<b>This API is not CLS-compliant.</b>

Converts a 64-bit unsigned integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputUInt64</i>: The number to convert as a 64-bit unsigned integer.

<b>Return Value:</b>

The value of  <i>inputUInt64</i>
 as an arbitrary-precision decimal number.

<a id="implicit_operator_PeterO_Numbers_EDecimal_ushort"></a>
### Implicit Operator

    public static implicit operator PeterO.Numbers.EDecimal(
        ushort inputUInt16);

<b>This API is not CLS-compliant.</b>

Converts a 16-bit unsigned integer to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>inputUInt16</i>: The number to convert as a 16-bit unsigned integer.

<b>Return Value:</b>

The value of  <i>inputUInt16</i>
 as an arbitrary-precision decimal number.

<a id="op_Increment"></a>
### Operator `++`

    public static PeterO.Numbers.EDecimal operator ++(
        PeterO.Numbers.EDecimal bthis);

Adds one to an arbitrary-precision decimal number.

<b>Parameters:</b>

 * <i>bthis</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

The given arbitrary-precision decimal number plus one.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

<a id="op_Modulus"></a>
### Operator `%`

    public static PeterO.Numbers.EDecimal operator %(
        PeterO.Numbers.EDecimal dividend,
        PeterO.Numbers.EDecimal divisor);

Returns the remainder that would result when an arbitrary-precision decimal floating-point number is divided by another arbitrary-precision decimal floating-point number. The remainder is the number that remains when the absolute value of an arbitrary-precision decimal floating-point number is divided (as though by DivideToIntegerZeroScale) by the absolute value of the other arbitrary-precision decimal floating-point number; the remainder has the same sign (positive or negative) as this arbitrary-precision decimal floating-point number.

<b>Parameters:</b>

 * <i>dividend</i>: The number that will be divided by the divisor.

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

The remainder that would result when an arbitrary-precision decimal floating-point number is divided by another arbitrary-precision decimal floating-point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>dividend</i>
 is null.

<a id="op_Multiply"></a>
### Operator `*`

    public static PeterO.Numbers.EDecimal operator *(
        PeterO.Numbers.EDecimal operand1,
        PeterO.Numbers.EDecimal operand2);

Multiplies an arbitrary-precision decimal floating-point number by another arbitrary-precision decimal floating-point number and returns the result.

<b>Parameters:</b>

 * <i>operand1</i>: The first operand.

 * <i>operand2</i>: The second operand.

<b>Return Value:</b>

The product of the two numbers, that is, an arbitrary-precision decimal floating-point number times another arbitrary-precision decimal floating-point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>operand1</i>
 or  <i>operand2</i>
 is null.

<a id="op_Subtraction"></a>
### Operator `-`

    public static PeterO.Numbers.EDecimal operator -(
        PeterO.Numbers.EDecimal bthis,
        PeterO.Numbers.EDecimal subtrahend);

Subtracts one arbitrary-precision decimal number from another and returns the result.

<b>Parameters:</b>

 * <i>bthis</i>: The first operand.

 * <i>subtrahend</i>: The second operand.

<b>Return Value:</b>

The difference of the two decimal numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 or  <i>subtrahend</i>
 is null.

<a id="op_UnaryNegation"></a>
### Operator `-`

    public static PeterO.Numbers.EDecimal operator -(
        PeterO.Numbers.EDecimal bigValue);

Gets an arbitrary-precision decimal number with the same value as the given one, but with the sign reversed.

<b>Parameters:</b>

 * <i>bigValue</i>: An arbitrary-precision decimal number to negate.

<b>Return Value:</b>

An arbitrary-precision decimal number. If this value is positive zero, returns negative zero. Returns signaling NaN if this value is signaling NaN.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigValue</i>
 is null.

<a id="PI_PeterO_Numbers_EContext"></a>
### PI

    public static PeterO.Numbers.EDecimal PI(
        PeterO.Numbers.EContext ctx);

Finds the constant π, the circumference of a circle divided by its diameter.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). <i>This parameter can't be null, as π can never be represented exactly.</i>.

<b>Return Value:</b>

The constant π rounded to the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

<a id="Plus_PeterO_Numbers_EContext"></a>
### Plus

    public PeterO.Numbers.EDecimal Plus(
        PeterO.Numbers.EContext ctx);

Rounds this object's value to a given precision, using the given rounding mode and range of exponent, and also converts negative zero to positive zero. The idiom  `EDecimal.SignalingNaN.Plus(ctx)`  is useful for triggering an invalid operation and returning not-a-number (NaN) for custom arithmetic operations.

<b>Parameters:</b>

 * <i>ctx</i>: A context for controlling the precision, rounding mode, and exponent range. Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The closest value to this object's value, rounded to the specified precision. If  <i>ctx</i>
 is null or the precision and exponent range are unlimited, returns the same value as this object (or a quiet NaN if this object is a signaling NaN).

<a id="Pow_int"></a>
### Pow

    public PeterO.Numbers.EDecimal Pow(
        int exponentSmall);

Raises this object's value to the given exponent.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The exponent to raise this object's value to.

<b>Return Value:</b>

This^exponent. Returns not-a-number (NaN) if this object and exponent are both 0.

<a id="Pow_int_PeterO_Numbers_EContext"></a>
### Pow

    public PeterO.Numbers.EDecimal Pow(
        int exponentSmall,
        PeterO.Numbers.EContext ctx);

Raises this object's value to the given exponent.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The exponent to raise this object's value to.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

This^exponent. Signals the flag FlagInvalid and returns NaN if this object and exponent are both 0.

<a id="Pow_PeterO_Numbers_EDecimal"></a>
### Pow

    public PeterO.Numbers.EDecimal Pow(
        PeterO.Numbers.EDecimal exponent);

Raises this object's value to the given exponent, using unlimited precision.

<b>Parameters:</b>

 * <i>exponent</i>: An arbitrary-precision decimal number expressing the exponent to raise this object's value to.

<b>Return Value:</b>

This^exponent. Returns not-a-number (NaN) if the exponent has a fractional part.

<a id="Pow_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Pow

    public PeterO.Numbers.EDecimal Pow(
        PeterO.Numbers.EDecimal exponent,
        PeterO.Numbers.EContext ctx);

Raises this object's value to the given exponent.

<b>Parameters:</b>

 * <i>exponent</i>: An arbitrary-precision decimal number expressing the exponent to raise this object's value to.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

This^exponent. Signals the flag FlagInvalid and returns NaN if this object and exponent are both 0; or if this value is less than 0 and the exponent either has a fractional part or is infinity. Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0), and the exponent has a fractional part.

<a id="Precision"></a>
### Precision

    public PeterO.Numbers.EInteger Precision();

Finds the number of digits in this number's significand. Returns 1 if this value is 0, and 0 if this value is infinity or not-a-number (NaN).

<b>Return Value:</b>

An arbitrary-precision integer.

<a id="PreRound_PeterO_Numbers_EContext"></a>
### PreRound

    public PeterO.Numbers.EDecimal PreRound(
        PeterO.Numbers.EContext ctx);

Returns a number in which the value of this object is rounded to fit the maximum precision allowed if it has more significant digits than the maximum precision. The maximum precision allowed is given in an arithmetic context. This method is designed for preparing operands to a custom arithmetic operation in accordance with the "simplified" arithmetic given in Appendix A of the General Decimal Arithmetic Specification.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited. Signals the flag LostDigits if the input number has greater precision than allowed and was rounded to a different numerical value in order to fit the precision.

<b>Return Value:</b>

This object rounded to the given precision. Returns this object and signals no flags if  <i>ctx</i>
 is null or specifies an unlimited precision, if this object is infinity or not-a-number (including signaling NaN), or if the number's value has no more significant digits than the maximum precision given in  <i>ctx</i>
.

<a id="Quantize_int_PeterO_Numbers_EContext"></a>
### Quantize

    public PeterO.Numbers.EDecimal Quantize(
        int desiredExponentInt,
        PeterO.Numbers.EContext ctx);

 Returns an arbitrary-precision decimal number with the same value but a new exponent. Note that this is not always the same as rounding to a given number of decimal places, since it can fail if the difference between this value's exponent and the desired exponent is too big, depending on the maximum precision. If rounding to a number of decimal places is desired, it's better to use the RoundToExponent and RoundToIntegral methods instead.

<b>Remark:</b> This method can be used to implement fixed-point decimal arithmetic, in which each decimal number has a fixed number of digits after the decimal point. The following code example returns a fixed-point number with up to 20 digits before and exactly 5 digits after the decimal point:

    /* After performing arithmetic operations, adjust the number to 5
                digits
                after the decimal point */ number = number.Quantize(-5, /* five digits
                after the decimal point */EContext.ForPrecision(25) /* 25-digit
                precision*/);

A fixed-point decimal arithmetic in which no digits come after the decimal point (a desired exponent of 0) is considered an "integer arithmetic".

<b>Parameters:</b>

 * <i>desiredExponentInt</i>: The desired exponent for the result. The exponent is the number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as this object but with the exponent changed. Signals FlagInvalid and returns not-a-number (NaN) if this object is infinity, if the rounded result can't fit the given precision, or if the context defines an exponent range and the given exponent is outside that range.

<a id="Quantize_int_PeterO_Numbers_ERounding"></a>
### Quantize

    public PeterO.Numbers.EDecimal Quantize(
        int desiredExponentInt,
        PeterO.Numbers.ERounding rounding);

Returns an arbitrary-precision decimal number with the same value as this one but a new exponent. <b>Remark:</b> This method can be used to implement fixed-point decimal arithmetic, in which a fixed number of digits come after the decimal point. A fixed-point decimal arithmetic in which no digits come after the decimal point (a desired exponent of 0) is considered an "integer arithmetic" .

<b>Parameters:</b>

 * <i>desiredExponentInt</i>: The desired exponent for the result. The exponent is the number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>rounding</i>: A rounding mode to use in case the result needs to be rounded to fit the given exponent.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as this object but with the exponent changed. Returns not-a-number (NaN) if this object is infinity, or if the rounding mode is ERounding.None and the result is not exact.

<a id="Quantize_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Quantize

    public PeterO.Numbers.EDecimal Quantize(
        PeterO.Numbers.EDecimal otherValue,
        PeterO.Numbers.EContext ctx);

Returns an arbitrary-precision decimal number with the same value as this object but with the same exponent as another decimal number. Note that this is not always the same as rounding to a given number of decimal places, since it can fail if the difference between this value's exponent and the desired exponent is too big, depending on the maximum precision. If rounding to a number of decimal places is desired, it's better to use the RoundToExponent and RoundToIntegral methods instead.

<b>Remark:</b> This method can be used to implement fixed-point decimal arithmetic, in which a fixed number of digits come after the decimal point. A fixed-point decimal arithmetic in which no digits come after the decimal point (a desired exponent of 0) is considered an "integer arithmetic" .

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision decimal number containing the desired exponent of the result. The significand is ignored. The exponent is the number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousands-place (10^3, 1000). A value of 0 rounds the number to an integer. The following examples for this parameter express a desired exponent of 3:  `10e3` ,  `8888e3` ,  `4.56e5` .

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as this object but with the exponent changed. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding, or if the arithmetic context defines an exponent range and the given exponent is outside that range.

<a id="Quantize_PeterO_Numbers_EInteger_PeterO_Numbers_EContext"></a>
### Quantize

    public PeterO.Numbers.EDecimal Quantize(
        PeterO.Numbers.EInteger desiredExponent,
        PeterO.Numbers.EContext ctx);

 Returns an arbitrary-precision decimal number with the same value but a new exponent. Note that this is not always the same as rounding to a given number of decimal places, since it can fail if the difference between this value's exponent and the desired exponent is too big, depending on the maximum precision. If rounding to a number of decimal places is desired, it's better to use the RoundToExponent and RoundToIntegral methods instead.

<b>Remark:</b> This method can be used to implement fixed-point decimal arithmetic, in which each decimal number has a fixed number of digits after the decimal point. The following code example returns a fixed-point number with up to 20 digits before and exactly 5 digits after the decimal point:

     /* After performing arithmetic operations, adjust
                /* the number to 5*/*/
                /**/
                digits after the decimal point number = number.Quantize(
                EInteger.FromInt32(-5), /* five digits after the decimal
                point*/
                EContext.ForPrecision(25) /* 25-digit
                precision);*/

A fixed-point decimal arithmetic in which no digits come after the decimal point (a desired exponent of 0) is considered an "integer arithmetic".

<b>Parameters:</b>

 * <i>desiredExponent</i>: The desired exponent for the result. The exponent is the number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number with the same value as this object but with the exponent changed. Signals FlagInvalid and returns not-a-number (NaN) if this object is infinity, if the rounded result can't fit the given precision, or if the context defines an exponent range and the given exponent is outside that range.

<a id="Reduce_PeterO_Numbers_EContext"></a>
### Reduce

    public PeterO.Numbers.EDecimal Reduce(
        PeterO.Numbers.EContext ctx);

Returns an object with the same numerical value as this one but with trailing zeros removed from its significand. For example, 1.00 becomes 1. If this object's value is 0, changes the exponent to 0.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

This value with trailing zeros removed. Note that if the result has a very high exponent and the context says to clamp high exponents, there may still be some trailing zeros in the significand.

<a id="Remainder_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Remainder

    public PeterO.Numbers.EDecimal Remainder(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EContext ctx);

Returns the remainder that would result when this arbitrary-precision decimal floating-point number is divided by another arbitrary-precision decimal floating-point number. The remainder is the number that remains when the absolute value of this arbitrary-precision decimal floating-point number is divided (as though by DivideToIntegerZeroScale) by the absolute value of the other arbitrary-precision decimal floating-point number; the remainder has the same sign (positive or negative) as this arbitrary-precision decimal floating-point number.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result, and of the intermediate integer division. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which the precision is unlimited.

<b>Return Value:</b>

The remainder that would result when this arbitrary-precision decimal floating-point number is divided by another arbitrary-precision decimal floating-point number. Signals FlagDivideByZero and returns infinity if the divisor (this arbitrary-precision decimal floating-point number) is 0 and the dividend (the other arbitrary-precision decimal floating-point number) is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0, or if the result of the division doesn't fit the given precision.

<a id="RemainderNaturalScale_PeterO_Numbers_EDecimal"></a>
### RemainderNaturalScale

    public PeterO.Numbers.EDecimal RemainderNaturalScale(
        PeterO.Numbers.EDecimal divisor);

Calculates the remainder of a number by the formula  `"this" - (("this" / "divisor") * "divisor")` .

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<a id="RemainderNaturalScale_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### RemainderNaturalScale

    public PeterO.Numbers.EDecimal RemainderNaturalScale(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EContext ctx);

Calculates the remainder of a number by the formula "this" - (("this" / "divisor") * "divisor").

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only in the division portion of the remainder calculation; as a result, it's possible for the return value to have a higher precision than given in this context. Flags will be set on the given context only if the context's  `HasFlags`  is true and the integer part of the division result doesn't fit the precision and exponent range without rounding. Can be null, in which the precision is unlimited and no additional rounding, other than the rounding down to an integer after division, is needed.

<b>Return Value:</b>

An arbitrary-precision decimal number.

<a id="RemainderNear_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### RemainderNear

    public PeterO.Numbers.EDecimal RemainderNear(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EContext ctx);

Finds the distance to the closest multiple of the given divisor, based on the result of dividing this object's value by another object's value.

 * If this and the other object divide evenly, the result is 0.

 * If the remainder's absolute value is less than half of the divisor's absolute value, the result has the same sign as this object and will be the distance to the closest multiple.

 * If the remainder's absolute value is more than half of the divisor's absolute value, the result has the opposite sign of this object and will be the distance to the closest multiple.

 * If the remainder's absolute value is exactly half of the divisor's absolute value, the result has the opposite sign of this object if the quotient, rounded down, is odd, and has the same sign as this object if the quotient, rounded down, is even, and the result's absolute value is half of the divisor's absolute value.

 This function is also known as the "IEEE Remainder" function.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision. The rounding and exponent range settings of this context are ignored (the rounding mode is always treated as HalfEven). If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which the precision is unlimited.

<b>Return Value:</b>

The distance of the closest multiple. Signals FlagInvalid and returns not-a-number (NaN) if the divisor is 0, or either the result of integer division (the quotient) or the remainder wouldn't fit the given precision.

<a id="RemainderNoRoundAfterDivide_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### RemainderNoRoundAfterDivide

    public PeterO.Numbers.EDecimal RemainderNoRoundAfterDivide(
        PeterO.Numbers.EDecimal divisor,
        PeterO.Numbers.EContext ctx);

Finds the remainder that results when dividing two arbitrary-precision decimal numbers, except the intermediate division is not adjusted to fit the precision of the given arithmetic context. The value of this object is divided by the absolute value of the other object; the remainder has the same sign (positive or negative) as this object's value.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result, but not also of the intermediate integer division. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which the precision is unlimited.

<b>Return Value:</b>

The remainder of the two numbers. Signals FlagInvalid and returns not-a-number (NaN) if the divisor is 0, or if the result doesn't fit the given precision.

<a id="RoundToExponent_int"></a>
### RoundToExponent

    public PeterO.Numbers.EDecimal RoundToExponent(
        int exponentSmall);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary, using the HalfEven rounding mode. The resulting number's Exponent property will not necessarily be the given exponent; use the Quantize method instead to give the result a particular exponent.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest value representable for the given exponent.

<a id="RoundToExponent_int_PeterO_Numbers_EContext"></a>
### RoundToExponent

    public PeterO.Numbers.EDecimal RoundToExponent(
        int exponentSmall,
        PeterO.Numbers.EContext ctx);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary. The resulting number's Exponent property will not necessarily be the given exponent; use the Quantize method instead to give the result a particular exponent.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest value representable in the given precision. If the result can't fit the precision, additional digits are discarded to make it fit. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to the given exponent when rounding, and the given exponent is outside of the valid range of the arithmetic context.

<a id="RoundToExponent_int_PeterO_Numbers_ERounding"></a>
### RoundToExponent

    public PeterO.Numbers.EDecimal RoundToExponent(
        int exponentSmall,
        PeterO.Numbers.ERounding rounding);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary. The resulting number's Exponent property will not necessarily be the given exponent; use the Quantize method instead to give the result a particular exponent.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>rounding</i>: The desired mode to use to round the given number to the given exponent.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the given negative number of decimal places.

<a id="RoundToExponent_PeterO_Numbers_EInteger"></a>
### RoundToExponent

    public PeterO.Numbers.EDecimal RoundToExponent(
        PeterO.Numbers.EInteger exponent);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary, using the HalfEven rounding mode. The resulting number's Exponent property will not necessarily be the given exponent; use the Quantize method instead to give the result a particular exponent.

<b>Parameters:</b>

 * <i>exponent</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest value representable for the given exponent.

<a id="RoundToExponent_PeterO_Numbers_EInteger_PeterO_Numbers_EContext"></a>
### RoundToExponent

    public PeterO.Numbers.EDecimal RoundToExponent(
        PeterO.Numbers.EInteger exponent,
        PeterO.Numbers.EContext ctx);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary. The resulting number's Exponent property will not necessarily be the given exponent; use the Quantize method instead to give the result a particular exponent.

<b>Parameters:</b>

 * <i>exponent</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest value representable in the given precision. If the result can't fit the precision, additional digits are discarded to make it fit. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to the given exponent when rounding, and the given exponent is outside of the valid range of the arithmetic context.

<a id="RoundToExponent_PeterO_Numbers_EInteger_PeterO_Numbers_ERounding"></a>
### RoundToExponent

    public PeterO.Numbers.EDecimal RoundToExponent(
        PeterO.Numbers.EInteger exponent,
        PeterO.Numbers.ERounding rounding);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to a new exponent if necessary, using the given rounding mode. The resulting number's Exponent property will not necessarily be the given exponent; use the Quantize method instead to give the result a particular exponent.

<b>Parameters:</b>

 * <i>exponent</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>rounding</i>: Desired mode for rounding this number's value.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest value representable for the given exponent.

<a id="RoundToExponentExact_int_PeterO_Numbers_EContext"></a>
### RoundToExponentExact

    public PeterO.Numbers.EDecimal RoundToExponentExact(
        int exponentSmall,
        PeterO.Numbers.EContext ctx);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to the given exponent represented as a 32-bit signed integer, and signals an inexact flag if the result would be inexact. The resulting number's Exponent property will not necessarily be the given exponent; use the Quantize method instead to give the result a particular exponent.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest value representable in the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to the given exponent when rounding, and the given exponent is outside of the valid range of the arithmetic context.

<a id="RoundToExponentExact_int_PeterO_Numbers_ERounding"></a>
### RoundToExponentExact

    public PeterO.Numbers.EDecimal RoundToExponentExact(
        int exponentSmall,
        PeterO.Numbers.ERounding rounding);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to the given exponent represented as a 32-bit signed integer, and signals an inexact flag if the result would be inexact. The resulting number's Exponent property will not necessarily be the given exponent; use the Quantize method instead to give the result a particular exponent.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>rounding</i>: Desired mode for rounding this object's value.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest value representable using the given exponent.

<a id="RoundToExponentExact_PeterO_Numbers_EInteger_PeterO_Numbers_EContext"></a>
### RoundToExponentExact

    public PeterO.Numbers.EDecimal RoundToExponentExact(
        PeterO.Numbers.EInteger exponent,
        PeterO.Numbers.EContext ctx);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to the given exponent represented as an arbitrary-precision integer, and signals an inexact flag if the result would be inexact. The resulting number's Exponent property will not necessarily be the given exponent; use the Quantize method instead to give the result a particular exponent.

<b>Parameters:</b>

 * <i>exponent</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest value representable in the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to the given exponent when rounding, and the given exponent is outside of the valid range of the arithmetic context.

<a id="RoundToIntegerExact_PeterO_Numbers_EContext"></a>
### RoundToIntegerExact

    public PeterO.Numbers.EDecimal RoundToIntegerExact(
        PeterO.Numbers.EContext ctx);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to an integer, and signals an inexact flag if the result would be inexact. The resulting number's Exponent property will not necessarily be 0; use the Quantize method instead to give the result an exponent of 0.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest integer representable in the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to 0 when rounding, and 0 is outside of the valid range of the arithmetic context.

<a id="RoundToIntegerNoRoundedFlag_PeterO_Numbers_EContext"></a>
### RoundToIntegerNoRoundedFlag

    public PeterO.Numbers.EDecimal RoundToIntegerNoRoundedFlag(
        PeterO.Numbers.EContext ctx);

Returns an arbitrary-precision decimal number with the same value as this object but rounded to an integer, without adding the  `FlagInexact`  or  `FlagRounded`  flags. The resulting number's Exponent property will not necessarily be 0; use the Quantize method instead to give the result an exponent of 0.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags), except that this function will never add the  `FlagRounded`  and  `FlagInexact`  flags (the only difference between this and RoundToExponentExact). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest integer representable in the given precision. If the result can't fit the precision, additional digits are discarded to make it fit. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to 0 when rounding, and 0 is outside of the valid range of the arithmetic context.

<a id="RoundToIntegralExact_PeterO_Numbers_EContext"></a>
### RoundToIntegralExact

    public PeterO.Numbers.EDecimal RoundToIntegralExact(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to RoundToIntegerExact.

Returns an arbitrary-precision decimal number with the same value as this object but rounded to an integer, and signals an inexact flag if the result would be inexact.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest integer representable in the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to 0 when rounding, and 0 is outside of the valid range of the arithmetic context.

<a id="RoundToIntegralNoRoundedFlag_PeterO_Numbers_EContext"></a>
### RoundToIntegralNoRoundedFlag

    public PeterO.Numbers.EDecimal RoundToIntegralNoRoundedFlag(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to RoundToIntegerNoRoundedFlag.

Returns an arbitrary-precision decimal number with the same value as this object but rounded to an integer, without adding the  `FlagInexact`  or  `FlagRounded`  flags.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags), except that this function will never add the  `FlagRounded`  and  `FlagInexact`  flags (the only difference between this and RoundToExponentExact). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision decimal number rounded to the closest integer representable in the given precision. If the result can't fit the precision, additional digits are discarded to make it fit. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to 0 when rounding, and 0 is outside of the valid range of the arithmetic context.

<a id="RoundToPrecision_PeterO_Numbers_EContext"></a>
### RoundToPrecision

    public PeterO.Numbers.EDecimal RoundToPrecision(
        PeterO.Numbers.EContext ctx);

Rounds this object's value to a given precision, using the given rounding mode and range of exponent.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The closest value to this object's value, rounded to the specified precision. Returns the same value as this object if  <i>ctx</i>
 is null or the precision and exponent range are unlimited.

<a id="ScaleByPowerOfTen_int"></a>
### ScaleByPowerOfTen

    public PeterO.Numbers.EDecimal ScaleByPowerOfTen(
        int places);

Returns a number similar to this number but with the scale adjusted.

<b>Parameters:</b>

 * <i>places</i>: The power of 10 to scale by.

<b>Return Value:</b>

A number whose exponent is increased by  <i>places</i>
. For example, if  <i>places</i>
 is 5, "78E-2" becomes "78E+3" and has a bigger value.

<a id="ScaleByPowerOfTen_int_PeterO_Numbers_EContext"></a>
### ScaleByPowerOfTen

    public PeterO.Numbers.EDecimal ScaleByPowerOfTen(
        int places,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the scale adjusted.

<b>Parameters:</b>

 * <i>places</i>: The power of 10 to scale by.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

A number whose exponent is generally increased by  <i>places</i>
. For example, in general, if  <i>places</i>
 is 5, "78E-2" becomes "78E+3" and has a bigger value.

<a id="ScaleByPowerOfTen_PeterO_Numbers_EInteger"></a>
### ScaleByPowerOfTen

    public PeterO.Numbers.EDecimal ScaleByPowerOfTen(
        PeterO.Numbers.EInteger bigPlaces);

Returns a number similar to this number but with the scale adjusted.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The power of 10 to scale by.

<b>Return Value:</b>

A number whose exponent is increased by  <i>bigPlaces</i>
. For example, if  <i>bigPlaces</i>
 is 5, "78E-2" becomes "78E+3" and has a bigger value.

<a id="ScaleByPowerOfTen_PeterO_Numbers_EInteger_PeterO_Numbers_EContext"></a>
### ScaleByPowerOfTen

    public PeterO.Numbers.EDecimal ScaleByPowerOfTen(
        PeterO.Numbers.EInteger bigPlaces,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with its scale adjusted.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The power of 10 to scale by.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

A number whose exponent is generally increased by  <i>bigPlaces</i>
. For example, in general, if  <i>bigPlaces</i>
 is 5, "78E-2" becomes "78E+3" and has a bigger value.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigPlaces</i>
 is null.

<a id="Sqrt_PeterO_Numbers_EContext"></a>
### Sqrt

    public PeterO.Numbers.EDecimal Sqrt(
        PeterO.Numbers.EContext ctx);

Finds the square root of this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). <i>This parameter can't be null, as the square root function's results are generally not exact for many inputs.</i> (Unlike in the General Decimal Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

The square root. Signals the flag FlagInvalid and returns NaN if this object is less than 0 (the square root would be a complex number, but the return value is still NaN). Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

<a id="SquareRoot_PeterO_Numbers_EContext"></a>
### SquareRoot

    public PeterO.Numbers.EDecimal SquareRoot(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to Sqrt.

Finds the square root of this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). <i>This parameter can't be null, as the square root function's results are generally not exact for many inputs.</i> (Unlike in the General Decimal Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

The square root. Signals the flag FlagInvalid and returns NaN if this object is less than 0 (the square root would be a complex number, but the return value is still NaN). Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

<a id="Subtract_int"></a>
### Subtract

    public PeterO.Numbers.EDecimal Subtract(
        int intValue);

Subtracts a 32-bit signed integer from this arbitrary-precision decimal floating-point number and returns the result. The exponent for the result is the lower of this arbitrary-precision decimal floating-point number's exponent and the other 32-bit signed integer's exponent.

<b>Parameters:</b>

 * <i>intValue</i>: A 32-bit signed integer to subtract from this object.

<b>Return Value:</b>

The difference between the two numbers, that is, this arbitrary-precision decimal floating-point number minus a 32-bit signed integer. If this arbitrary-precision decimal floating-point number is not-a-number (NaN), returns NaN.

<a id="Subtract_long"></a>
### Subtract

    public PeterO.Numbers.EDecimal Subtract(
        long longValue);

Subtracts a 64-bit signed integer from this arbitrary-precision decimal floating-point number and returns the result. The exponent for the result is the lower of this arbitrary-precision decimal floating-point number's exponent and the other 64-bit signed integer's exponent.

<b>Parameters:</b>

 * <i>longValue</i>: The parameter  <i>longValue</i>
 is a 64-bit signed integer.

<b>Return Value:</b>

The difference between the two numbers, that is, this arbitrary-precision decimal floating-point number minus a 64-bit signed integer. If this arbitrary-precision decimal floating-point number is not-a-number (NaN), returns NaN.

<a id="Subtract_PeterO_Numbers_EDecimal"></a>
### Subtract

    public PeterO.Numbers.EDecimal Subtract(
        PeterO.Numbers.EDecimal otherValue);

Subtracts an arbitrary-precision decimal floating-point number from this arbitrary-precision decimal floating-point number and returns the result. The exponent for the result is the lower of this arbitrary-precision decimal floating-point number's exponent and the other arbitrary-precision decimal floating-point number's exponent.

<b>Parameters:</b>

 * <i>otherValue</i>: The number to subtract from this instance's value.

<b>Return Value:</b>

The difference between the two numbers, that is, this arbitrary-precision decimal floating-point number minus another arbitrary-precision decimal floating-point number. If this arbitrary-precision decimal floating-point number is not-a-number (NaN), returns NaN.

<a id="Subtract_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Subtract

    public PeterO.Numbers.EDecimal Subtract(
        PeterO.Numbers.EDecimal otherValue,
        PeterO.Numbers.EContext ctx);

Subtracts an arbitrary-precision decimal floating-point number from this arbitrary-precision decimal floating-point number and returns the result.

<b>Parameters:</b>

 * <i>otherValue</i>: The number to subtract from this instance's value.

 * <i>ctx</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The difference between the two numbers, that is, this arbitrary-precision decimal floating-point number minus another arbitrary-precision decimal floating-point number. If this arbitrary-precision decimal floating-point number is not-a-number (NaN), returns NaN.

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

Converts this value to a  `decimal`  under the Common Language Infrastructure (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ), using the half-even rounding mode.

<b>Return Value:</b>

A  `decimal`  under the Common Language Infrastructure (usually a.NET Framework decimal).

<a id="ToDouble"></a>
### ToDouble

    public double ToDouble();

Converts this value to its closest equivalent as a 64-bit floating-point number, using the half-even rounding mode. If this value is a NaN, sets the high bit of the 64-bit floating point number's significand area for a quiet NaN, and clears it for a signaling NaN. Then the other bits of the significand area are set to the lowest bits of this object's unsigned significand, and the next-highest bit of the significand area is set if those bits are all zeros and this is a signaling NaN. Unfortunately, in the.NET implementation, the return value of this method may be a quiet NaN even if a signaling NaN would otherwise be generated.

<b>Return Value:</b>

The closest 64-bit floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 64-bit floating point number.

<a id="ToDoubleBits"></a>
### ToDoubleBits

    public long ToDoubleBits();

Converts this value to its closest equivalent as a 64-bit floating-point number encoded in the IEEE 754 binary64 format, using the half-even rounding mode. If this value is a NaN, sets the high bit of the binary64 value's significand area for a quiet NaN, and clears it for a signaling NaN. Then the other bits of the significand area are set to the lowest bits of this object's unsigned significand, and the next-highest bit of the significand area is set if those bits are all zeros and this is a signaling NaN.

<b>Return Value:</b>

The closest 64-bit floating-point number to this value, encoded in the IEEE 754 binary64 format. The return value can be positive infinity or negative infinity, encoded in the IEEE 754 binary64 format, if this value exceeds the range of a 64-bit floating point number.

<a id="ToEFloat_PeterO_Numbers_EContext"></a>
### ToEFloat

    public PeterO.Numbers.EFloat ToEFloat(
        PeterO.Numbers.EContext ec);

Creates a binary floating-point number from this object's value. Note that if the binary floating-point number contains a negative exponent, the resulting value might not be exact, in which case the resulting binary floating-point number will be an approximation of this decimal number's value.

<b>Parameters:</b>

 * <i>ec</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. The precision is in bits, and an example of this parameter is  `EContext.Binary64` . Can be null.

<b>Return Value:</b>

An arbitrary-precision float floating-point number.

<a id="ToEFloat"></a>
### ToEFloat

    public PeterO.Numbers.EFloat ToEFloat();

Creates a binary floating-point number from this object's value. Note that if the binary floating-point number contains a negative exponent, the resulting value might not be exact, in which case the resulting binary floating-point number will be an approximation of this decimal number's value, using the half-even rounding mode.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="ToEInteger"></a>
### ToEInteger

    public PeterO.Numbers.EInteger ToEInteger();

Converts this value to an arbitrary-precision integer, discarding the fractional part in this value. Note that depending on the value, especially the exponent, generating the arbitrary-precision integer may require a huge amount of memory. Use the ToSizedEInteger method to convert a number to an EInteger only if the integer fits in a bounded bit range; that method will throw an exception on overflow.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

 * System.NotSupportedException:
There is not enough memory to store the value as an EInteger.

<a id="ToEIntegerExact"></a>
### ToEIntegerExact

    public PeterO.Numbers.EInteger ToEIntegerExact();

<b>Deprecated.</b> Renamed to ToEIntegerIfExact.

Converts this value to an arbitrary-precision integer, checking whether the fractional part of the value would be lost. Note that depending on the value, especially the exponent, generating the arbitrary-precision integer may require a huge amount of memory. Use the ToSizedEIntegerIfExact method to convert a number to an EInteger only if the integer fits in a bounded bit range; that method will throw an exception on overflow.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

 * System.ArithmeticException:
This object's value is not an exact integer.

<a id="ToEIntegerIfExact"></a>
### ToEIntegerIfExact

    public PeterO.Numbers.EInteger ToEIntegerIfExact();

Converts this value to an arbitrary-precision integer, checking whether the fractional part of the value would be lost. Note that depending on the value, especially the exponent, generating the arbitrary-precision integer may require a huge amount of memory. Use the ToSizedEIntegerIfExact method to convert a number to an EInteger only if the integer fits in a bounded bit range; that method will throw an exception on overflow.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

 * System.ArithmeticException:
This object's value is not an exact integer.

<a id="ToEngineeringString"></a>
### ToEngineeringString

    public string ToEngineeringString();

Same as ToString(), except that when an exponent is used it will be a multiple of 3.

<b>Return Value:</b>

A text string.

<a id="ToExtendedFloat"></a>
### ToExtendedFloat

    public PeterO.Numbers.EFloat ToExtendedFloat();

<b>Deprecated.</b> Renamed to ToEFloat.

Creates a binary floating-point number from this object's value. Note that if the binary floating-point number contains a negative exponent, the resulting value might not be exact, in which case the resulting binary floating-point number will be an approximation of this decimal number's value, using the half-even rounding mode.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

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

<a id="ToPlainString"></a>
### ToPlainString

    public string ToPlainString();

Converts this value to a string as though with the ToString method, but without using exponential notation.

<b>Return Value:</b>

A text string.

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

Converts this value to its closest equivalent as a 32-bit floating-point number, using the half-even rounding mode. If this value is a NaN, sets the high bit of the 32-bit floating point number's significand area for a quiet NaN, and clears it for a signaling NaN. Then the other bits of the significand area are set to the lowest bits of this object's unsigned significand, and the next-highest bit of the significand area is set if those bits are all zeros and this is a signaling NaN. Unfortunately, in the.NET implementation, the return value of this method may be a quiet NaN even if a signaling NaN would otherwise be generated.

<b>Return Value:</b>

The closest 32-bit binary floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 32-bit floating point number.

<a id="ToSingleBits"></a>
### ToSingleBits

    public int ToSingleBits();

Converts this value to its closest equivalent as a 32-bit floating-point number encoded in the IEEE 754 binary32 format, using the half-even rounding mode. If this value is a NaN, sets the high bit of the 32-bit floating point number's significand area for a quiet NaN, and clears it for a signaling NaN. Then the other bits of the significand area are set to the lowest bits of this object's unsigned significand, and the next-highest bit of the significand area is set if those bits are all zeros and this is a signaling NaN.

<b>Return Value:</b>

The closest 32-bit binary floating-point number to this value, encoded in the IEEE 754 binary32 format. The return value can be positive infinity or negative infinity if this value exceeds the range of a 32-bit floating point number.

<a id="ToSizedEInteger_int"></a>
### ToSizedEInteger

    public PeterO.Numbers.EInteger ToSizedEInteger(
        int maxBitLength);

Converts this value to an arbitrary-precision integer by discarding its fractional part and checking whether the resulting integer overflows the given signed bit count.

<b>Parameters:</b>

 * <i>maxBitLength</i>: The maximum number of signed bits the integer can have. The integer's value may not be less than -(2^maxBitLength) or greater than (2^maxBitLength) - 1.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN), or this number's value, once converted to an integer by discarding its fractional part, is less than -(2^maxBitLength) or greater than (2^maxBitLength) - 1.

<a id="ToSizedEIntegerIfExact_int"></a>
### ToSizedEIntegerIfExact

    public PeterO.Numbers.EInteger ToSizedEIntegerIfExact(
        int maxBitLength);

Converts this value to an arbitrary-precision integer, only if this number's value is an exact integer and that integer does not overflow the given signed bit count.

<b>Parameters:</b>

 * <i>maxBitLength</i>: The maximum number of signed bits the integer can have. The integer's value may not be less than -(2^maxBitLength) or greater than (2^maxBitLength) - 1.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN), or this number's value, once converted to an integer by discarding its fractional part, is less than -(2^maxBitLength) or greater than (2^maxBitLength) - 1.

 * System.ArithmeticException:
This object's value is not an exact integer.

<a id="ToString"></a>
### ToString

    public override string ToString();

Converts this value to a string. Returns a value compatible with this class's FromString method.

<b>Return Value:</b>

A string representation of this object. The text string will be in exponential notation if the exponent is greater than 0 or if the number's first nonzero digit is more than five digits after the decimal point.

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

<a id="Ulp"></a>
### Ulp

    public PeterO.Numbers.EDecimal Ulp();

Returns the unit in the last place. The significand will be 1 and the exponent will be this number's exponent. Returns 1 with an exponent of 0 if this number is infinity or not-a-number (NaN).

<b>Return Value:</b>

An arbitrary-precision decimal number.
