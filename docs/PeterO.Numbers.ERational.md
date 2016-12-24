## PeterO.Numbers.ERational

    public sealed class ERational :
        System.IComparable,
        System.IEquatable

Represents an arbitrary-precision rational number. This class can't be inherited. (The "E" stands for "extended", meaning that instances of this class can be values other than numbers proper, such as infinity and not-a-number.)<b>Thread safety:</b> Instances of this class are immutable, so they are inherently safe for use by multiple threads. Multiple instances of this object with the same properties are interchangeable, so they should not be compared using the "==" operator (which might only check if each side of the operator is the same instance).

### ERational Constructor

    public ERational(
        PeterO.Numbers.EInteger numerator,
        PeterO.Numbers.EInteger denominator);

<b>Deprecated.</b> Use the ERational.Create method instead. This constructor will be private or unavailable in version 1.0.

Initializes a new instance of the [PeterO.Numbers.ERational](PeterO.Numbers.ERational.md) class.

<b>Parameters:</b>

 * <i>numerator</i>: An arbitrary-precision integer.

 * <i>denominator</i>: Another arbitrary-precision integer.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>numerator</i>
 or  <i>denominator</i>
 is null.

### NaN

    public static readonly PeterO.Numbers.ERational NaN;

A not-a-number value.

### NegativeInfinity

    public static readonly PeterO.Numbers.ERational NegativeInfinity;

Negative infinity, less than any other number.

### NegativeZero

    public static readonly PeterO.Numbers.ERational NegativeZero;

A rational number for negative zero.

### One

    public static readonly PeterO.Numbers.ERational One;

The rational number one.

### PositiveInfinity

    public static readonly PeterO.Numbers.ERational PositiveInfinity;

Positive infinity, greater than any other number.

### SignalingNaN

    public static readonly PeterO.Numbers.ERational SignalingNaN;

A signaling not-a-number value.

### Ten

    public static readonly PeterO.Numbers.ERational Ten;

The rational number ten.

### Zero

    public static readonly PeterO.Numbers.ERational Zero;

A rational number for zero.

### Denominator

    public PeterO.Numbers.EInteger Denominator { get; }

Gets this object's denominator.

<b>Returns:</b>

This object's denominator.

### IsFinite

    public bool IsFinite { get; }

Gets a value indicating whether this object is finite (not infinity or NaN).

<b>Returns:</b>

 `true`  if this object is finite (not infinity or not-a-number (NaN)); otherwise,  `false` .

### IsNegative

    public bool IsNegative { get; }

Gets a value indicating whether this object's value is negative (including negative zero).

<b>Returns:</b>

 `true`  if this object's value is negative; otherwise,  `false` .

### IsZero

    public bool IsZero { get; }

Gets a value indicating whether this object's value equals 0.

<b>Returns:</b>

 `true`  if this object's value equals 0; otherwise, .  `false` .

### Numerator

    public PeterO.Numbers.EInteger Numerator { get; }

Gets this object's numerator.

<b>Returns:</b>

This object's numerator. If this object is a not-a-number value, returns the diagnostic information (which will be negative if this object is negative).

### Sign

    public int Sign { get; }

Gets the sign of this rational number.

<b>Returns:</b>

Zero if this value is zero or negative zero; -1 if this value is less than 0; and 1 if this value is greater than 0.

### UnsignedNumerator

    public PeterO.Numbers.EInteger UnsignedNumerator { get; }

Gets this object's numerator with the sign removed.

<b>Returns:</b>

This object's numerator. If this object is a not-a-number value, returns the diagnostic information.

### Abs

    public PeterO.Numbers.ERational Abs();

Not documented yet.

<b>Return Value:</b>

An arbitrary-precision rational number.

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
The parameter <i>otherValue</i>
 is null.

### CompareTo

    public sealed int CompareTo(
        PeterO.Numbers.ERational other);

Compares an arbitrary-precision rational number with this instance.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision rational number.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater.

### CompareToBinary

    public int CompareToBinary(
        PeterO.Numbers.EFloat other);

Compares an arbitrary-precision binary float with this instance.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision binary float.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater.

### CompareToDecimal

    public int CompareToDecimal(
        PeterO.Numbers.EDecimal other);

Compares an arbitrary-precision decimal number with this instance.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

Zero if the values are equal; a negative number if this instance is less, or a positive number if this instance is greater.

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

The number 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater.

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

The number 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater.

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
The parameter <i>other</i>
 is null.

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

### CreateNaN

    public static PeterO.Numbers.ERational CreateNaN(
        PeterO.Numbers.EInteger diag);

Creates a not-a-number arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>diag</i>: A number to use as diagnostic information associated with this object. If none is needed, should be zero.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>diag</i>
 is null.

 * System.ArgumentException:
The parameter <i>diag</i>
 is less than 0.

### CreateNaN

    public static PeterO.Numbers.ERational CreateNaN(
        PeterO.Numbers.EInteger diag,
        bool signaling,
        bool negative);

Creates a not-a-number arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>diag</i>: A number to use as diagnostic information associated with this object. If none is needed, should be zero.

 * <i>signaling</i>: Whether the return value will be signaling (true) or quiet (false).

 * <i>negative</i>: Whether the return value is negative.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>diag</i>
 is null.

 * System.ArgumentException:
The parameter <i>diag</i>
 is less than 0.

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
The parameter <i>otherValue</i>
 is null.

### Equals

    public override bool Equals(
        object obj);

Determines whether this object's numerator, denominator, and properties are equal to those of another object and that other object is an arbitrary-precision rational number. Not-a-number values are considered equal if the rest of their properties are equal.

<b>Parameters:</b>

 * <i>obj</i>: An arbitrary object.

<b>Return Value:</b>

 `true`  if the objects are equal; otherwise,  `false` .

### Equals

    public sealed bool Equals(
        PeterO.Numbers.ERational other);

Determines whether this object's numerator, denominator, and properties are equal to those of another object. Not-a-number values are considered equal if the rest of their properties are equal.

<b>Parameters:</b>

 * <i>other</i>: The parameter  <i>other</i>
 is not documented yet.

<b>Return Value:</b>

A Boolean object.

### FromByte

    public static PeterO.Numbers.ERational FromByte(
        byte inputByte);

Converts a byte (from 0 to 255) to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputByte</i>: The number to convert as a byte (from 0 to 255).

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

### FromDecimal

    public static PeterO.Numbers.ERational FromDecimal(
        System.Decimal eint);

Not documented yet.

<b>Parameters:</b>

 * <i>eint</i>: A Decimal object.

<b>Return Value:</b>

An arbitrary-precision rational number.

### FromDouble

    public static PeterO.Numbers.ERational FromDouble(
        double flt);

Converts a 64-bit floating-point number to a rational number. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the number to a string.

<b>Parameters:</b>

 * <i>flt</i>: A 64-bit floating-point number.

<b>Return Value:</b>

A rational number with the same value as  <i>flt</i>
.

### FromEDecimal

    public static PeterO.Numbers.ERational FromEDecimal(
        PeterO.Numbers.EDecimal ef);

Converts an arbitrary-precision decimal number to a rational number.

<b>Parameters:</b>

 * <i>ef</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>ef</i>
 is null.

### FromEFloat

    public static PeterO.Numbers.ERational FromEFloat(
        PeterO.Numbers.EFloat ef);

Not documented yet.

<b>Parameters:</b>

 * <i>ef</i>: An arbitrary-precision binary float.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>ef</i>
 is null.

### FromEInteger

    public static PeterO.Numbers.ERational FromEInteger(
        PeterO.Numbers.EInteger bigint);

Converts an arbitrary-precision integer to a rational number.

<b>Parameters:</b>

 * <i>bigint</i>: An arbitrary-precision integer.

<b>Return Value:</b>

The exact value of the integer as a rational number.

### FromExtendedDecimal

    public static PeterO.Numbers.ERational FromExtendedDecimal(
        PeterO.Numbers.EDecimal ef);

<b>Deprecated.</b> Renamed to FromEDecimal.

Converts an arbitrary-precision decimal number to a rational number.

<b>Parameters:</b>

 * <i>ef</i>: An arbitrary-precision decimal number.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>ef</i>
 is null.

### FromExtendedFloat

    public static PeterO.Numbers.ERational FromExtendedFloat(
        PeterO.Numbers.EFloat ef);

<b>Deprecated.</b> Renamed to FromEFloat.

Not documented yet.

<b>Parameters:</b>

 * <i>ef</i>: An arbitrary-precision binary float.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>ef</i>
 is null.

### FromInt16

    public static PeterO.Numbers.ERational FromInt16(
        short inputInt16);

Converts a 16-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputInt16</i>: The number to convert as a 16-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

### FromInt32

    public static PeterO.Numbers.ERational FromInt32(
        int inputInt32);

Converts a 32-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

### FromInt64

    public static PeterO.Numbers.ERational FromInt64(
        long inputInt64);

Converts a 64-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputInt64</i>: The number to convert as a 64-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

### FromSByte

    public static PeterO.Numbers.ERational FromSByte(
        sbyte inputSByte);

Converts an 8-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputSByte</i>: The number to convert as an 8-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

### FromSingle

    public static PeterO.Numbers.ERational FromSingle(
        float flt);

Converts a 32-bit floating-point number to a rational number. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the number to a string.

<b>Parameters:</b>

 * <i>flt</i>: A 32-bit floating-point number.

<b>Return Value:</b>

A rational number with the same value as  <i>flt</i>
.

### FromString

    public static PeterO.Numbers.ERational FromString(
        string str);

Creates a rational number from a text string that represents a number. See  `FromString(String, int, int)`  for more information.

<b>Parameters:</b>

 * <i>str</i>: A string that represents a number.

<b>Return Value:</b>

An arbitrary-precision rational number with the same value as the given string.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>str</i>
 is null.

 * System.FormatException:
The parameter  <i>str</i>
 is not a correctly formatted number string.

### FromString

    public static PeterO.Numbers.ERational FromString(
        string str,
        int offset,
        int length);

Creates a rational number from a text string that represents a number.

The format of the string generally consists of:

 * An optional plus sign ("+" , U+002B) or minus sign ("-", U+002D) (if '-' , the value is negative.)

 * The numerator in the form of one or more digits.

 * Optionally, "/" followed by the denominator in the form of one or more digits. If a denominator is not given, it's equal to 1.

The string can also be "-INF", "-Infinity", "Infinity", "INF", quiet NaN ("NaN" /"-NaN") followed by any number of digits, or signaling NaN ("sNaN" /"-sNaN") followed by any number of digits, all in any combination of upper and lower case.

All characters mentioned above are the corresponding characters in the Basic Latin range. In particular, the digits must be the basic digits 0 to 9 (U+0030 to U+0039). The string is not allowed to contain white space characters, including spaces.

<b>Parameters:</b>

 * <i>str</i>: A text string, a portion of which represents a number.

 * <i>offset</i>: A zero-based index that identifies the start of the number.

 * <i>length</i>: The length of the number within the string.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>str</i>
 is null.

 * System.FormatException:
The parameter  <i>str</i>
 is not a correctly formatted number string.

### FromUInt16

    public static PeterO.Numbers.ERational FromUInt16(
        ushort inputUInt16);

Converts a 16-bit unsigned integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputUInt16</i>: The number to convert as a 16-bit unsigned integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

### FromUInt32

    public static PeterO.Numbers.ERational FromUInt32(
        uint inputUInt32);

Converts a 32-bit signed integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputUInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

### FromUInt64

    public static PeterO.Numbers.ERational FromUInt64(
        ulong inputUInt64);

Converts a 64-bit unsigned integer to an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>inputUInt64</i>: The number to convert as a 64-bit unsigned integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision rational number.

### GetHashCode

    public override int GetHashCode();

Returns the hash code for this instance. No application or process IDs are used in the hash code calculation.

<b>Return Value:</b>

A 32-bit hash code.

### IsInfinity

    public bool IsInfinity();

Gets a value indicating whether this object's value is infinity.

<b>Return Value:</b>

 `true`  if this object's value is infinity; otherwise,  `false` .

### IsNaN

    public bool IsNaN();

Returns whether this object is a not-a-number value.

<b>Return Value:</b>

 `true`  if this object is a not-a-number value; otherwise,  `false` .

### IsNegativeInfinity

    public bool IsNegativeInfinity();

Returns whether this object is negative infinity.

<b>Return Value:</b>

 `true`  if this object is negative infinity; otherwise,  `false` .

### IsPositiveInfinity

    public bool IsPositiveInfinity();

Returns whether this object is positive infinity.

<b>Return Value:</b>

 `true`  if this object is positive infinity; otherwise,  `false` .

### IsQuietNaN

    public bool IsQuietNaN();

Returns whether this object is a quiet not-a-number value.

<b>Return Value:</b>

 `true`  if this object is a quiet not-a-number value; otherwise,  `false` .

### IsSignalingNaN

    public bool IsSignalingNaN();

Returns whether this object is a signaling not-a-number value (which causes an error if the value is passed to any arithmetic operation in this class).

<b>Return Value:</b>

 `true`  if this object is a signaling not-a-number value (which causes an error if the value is passed to any arithmetic operation in this class); otherwise,  `false` .

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
The parameter <i>otherValue</i>
 is null.

### Negate

    public PeterO.Numbers.ERational Negate();

Returns a rational number with the sign reversed.

<b>Return Value:</b>

An arbitrary-precision rational number.

### Operator `+`

    public static PeterO.Numbers.ERational operator +(
        PeterO.Numbers.ERational bthis,
        PeterO.Numbers.ERational augend);

Adds two rational numbers.

<b>Parameters:</b>

 * <i>bthis</i>: An arbitrary-precision rational number.

 * <i>augend</i>: An arbitrary-precision rational number. (3).

<b>Return Value:</b>

The sum of the two numbers. Returns not-a-number (NaN) if either operand is NaN.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

### Operator `/`

    public static PeterO.Numbers.ERational operator /(
        PeterO.Numbers.ERational dividend,
        PeterO.Numbers.ERational divisor);

Divides this instance by the value of an arbitrary-precision rational number object.

<b>Parameters:</b>

 * <i>dividend</i>: An arbitrary-precision rational number.

 * <i>divisor</i>: An arbitrary-precision rational number. (3).

<b>Return Value:</b>

The quotient of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

### Operator `%`

    public static PeterO.Numbers.ERational operator %(
        PeterO.Numbers.ERational dividend,
        PeterO.Numbers.ERational divisor);

Finds the remainder that results when this instance is divided by the value of an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>dividend</i>: An arbitrary-precision rational number.

 * <i>divisor</i>: An arbitrary-precision rational number. (3).

<b>Return Value:</b>

The remainder of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

### Operator `*`

    public static PeterO.Numbers.ERational operator *(
        PeterO.Numbers.ERational operand1,
        PeterO.Numbers.ERational operand2);

Multiplies this instance by the value of an arbitrary-precision rational number.

<b>Parameters:</b>

 * <i>operand1</i>: An arbitrary-precision rational number.

 * <i>operand2</i>: An arbitrary-precision rational number. (3).

<b>Return Value:</b>

The product of the two numbers.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

### Operator `-`

    public static PeterO.Numbers.ERational operator -(
        PeterO.Numbers.ERational bthis,
        PeterO.Numbers.ERational subtrahend);

Subtracts an arbitrary-precision rational number from this instance.

<b>Parameters:</b>

 * <i>bthis</i>: An arbitrary-precision rational number.

 * <i>subtrahend</i>: An arbitrary-precision rational number. (3).

<b>Return Value:</b>

The difference of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "otherValue" is null.

### Operator `-`

    public static PeterO.Numbers.ERational operator -(
        PeterO.Numbers.ERational bigValue);

Not documented yet.

<b>Parameters:</b>

 * <i>bigValue</i>: An arbitrary-precision rational number to negate.

<b>Return Value:</b>

An arbitrary-precision rational number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>bigValue</i>
 is null.

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
The parameter <i>otherValue</i>
 is null.

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
The parameter <i>otherValue</i>
 is null.

### ToByteChecked

    public byte ToByteChecked();

Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) after truncating to an integer.

<b>Return Value:</b>

This number's value, truncated to a byte (from 0 to 255).

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the truncated integer is less than 0 or greater than 255.

### ToByteIfExact

    public byte ToByteIfExact();

Converts this number's value to a byte (from 0 to 255) if it can fit in a byte (from 0 to 255) without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a byte (from 0 to 255).

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than 0 or greater than 255.

### ToByteUnchecked

    public byte ToByteUnchecked();

Truncates this number's value to an integer and returns the least-significant bits of its two's-complement form as a byte (from 0 to 255).

<b>Return Value:</b>

This number, converted to a byte (from 0 to 255). Returns 0 if this value is infinity or not-a-number.

### ToDecimal

    public System.Decimal ToDecimal();

Converts this value to a  `decimal`  under the Common Language Infrastructure (usually a .NET Framework decimal). Currently, converts this value to the precision and range of a .NET Framework decimal.

<b>Return Value:</b>

A  `decimal`  under the Common Language Infrastructure (usually a .NET Framework decimal).

### ToDouble

    public double ToDouble();

Converts this value to a 64-bit floating-point number. The half-even rounding mode is used.

<b>Return Value:</b>

The closest 64-bit floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 64-bit floating point number.

### ToEDecimal

    public PeterO.Numbers.EDecimal ToEDecimal(
        PeterO.Numbers.EContext ctx);

Converts this rational number to a decimal number and rounds the result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The value of the rational number, rounded to the given precision. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating decimal expansion.

### ToEDecimal

    public PeterO.Numbers.EDecimal ToEDecimal();

Converts this rational number to a decimal number.

<b>Return Value:</b>

The exact value of the rational number, or not-a-number (NaN) if the result can't be exact because it has a nonterminating decimal expansion.

### ToEDecimalExactIfPossible

    public PeterO.Numbers.EDecimal ToEDecimalExactIfPossible(
        PeterO.Numbers.EContext ctx);

Converts this rational number to a decimal number, but if the result would have a nonterminating decimal expansion, rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only if the exact result would have a nonterminating decimal expansion. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The exact value of the rational number if possible; otherwise,the rounded version of the result if a context is given. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating decimal expansion.

### ToEFloat

    public PeterO.Numbers.EFloat ToEFloat(
        PeterO.Numbers.EContext ctx);

Converts this rational number to a binary float and rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The value of the rational number, rounded to the given precision. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating binary expansion.

### ToEFloat

    public PeterO.Numbers.EFloat ToEFloat();

Converts this rational number to a binary float.

<b>Return Value:</b>

The exact value of the rational number, or not-a-number (NaN) if the result can't be exact because it has a nonterminating binary expansion.

### ToEFloatExactIfPossible

    public PeterO.Numbers.EFloat ToEFloatExactIfPossible(
        PeterO.Numbers.EContext ctx);

Converts this rational number to a binary float, but if the result would have a nonterminating binary expansion, rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only if the exact result would have a nonterminating binary expansion. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The exact value of the rational number if possible; otherwise,the rounded version of the result if a context is given. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating binary expansion.

### ToEInteger

    public PeterO.Numbers.EInteger ToEInteger();

Converts this value to an arbitrary-precision integer. Any fractional part in this value will be discarded when converting to an arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

### ToEIntegerExact

    public PeterO.Numbers.EInteger ToEIntegerExact();

<b>Deprecated.</b> Renamed to ToEIntegerIfExact.

Converts this value to an arbitrary-precision integer, checking whether the value is an exact integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

 * System.ArithmeticException:
This object's value is not an exact integer.

### ToEIntegerIfExact

    public PeterO.Numbers.EInteger ToEIntegerIfExact();

Converts this value to an arbitrary-precision integer, checking whether the value is an exact integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

 * System.ArithmeticException:
This object's value is not an exact integer.

### ToExtendedDecimal

    public PeterO.Numbers.EDecimal ToExtendedDecimal(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to ToEDecimal.

Converts this rational number to a decimal number and rounds the result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The value of the rational number, rounded to the given precision. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating decimal expansion.

### ToExtendedDecimal

    public PeterO.Numbers.EDecimal ToExtendedDecimal();

<b>Deprecated.</b> Renamed to ToEDecimal.

Converts this rational number to a decimal number.

<b>Return Value:</b>

The exact value of the rational number, or not-a-number (NaN) if the result can't be exact because it has a nonterminating decimal expansion.

### ToExtendedDecimalExactIfPossible

    public PeterO.Numbers.EDecimal ToExtendedDecimalExactIfPossible(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to ToEDecimalExactIfPossible.

Converts this rational number to a decimal number, but if the result would have a nonterminating decimal expansion, rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only if the exact result would have a nonterminating decimal expansion. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The exact value of the rational number if possible; otherwise,the rounded version of the result if a context is given. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating decimal expansion.

### ToExtendedFloat

    public PeterO.Numbers.EFloat ToExtendedFloat(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to ToEFloat.

Converts this rational number to a binary float and rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The value of the rational number, rounded to the given precision. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating binary expansion.

### ToExtendedFloat

    public PeterO.Numbers.EFloat ToExtendedFloat();

<b>Deprecated.</b> Renamed to ToEFloat.

Converts this rational number to a binary float.

<b>Return Value:</b>

The exact value of the rational number, or not-a-number (NaN) if the result can't be exact because it has a nonterminating binary expansion.

### ToExtendedFloatExactIfPossible

    public PeterO.Numbers.EFloat ToExtendedFloatExactIfPossible(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to ToEFloatExactIfPossible.

Converts this rational number to a binary float, but if the result would have a nonterminating binary expansion, rounds that result to the given precision.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only if the exact result would have a nonterminating binary expansion. If HasFlags of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The exact value of the rational number if possible; otherwise,the rounded version of the result if a context is given. Returns not-a-number (NaN) if the context is null and the result can't be exact because it has a nonterminating binary expansion.

### ToInt16Checked

    public short ToInt16Checked();

Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer after truncating to an integer.

<b>Return Value:</b>

This number's value, truncated to a 16-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the truncated integer is less than -32768 or greater than 32767.

### ToInt16IfExact

    public short ToInt16IfExact();

Converts this number's value to a 16-bit signed integer if it can fit in a 16-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 16-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than -32768 or greater than 32767.

### ToInt16Unchecked

    public short ToInt16Unchecked();

Truncates this number's value to an integer and returns the least-significant bits of its two's-complement form as a 16-bit signed integer.

<b>Return Value:</b>

This number, converted to a 16-bit signed integer. Returns 0 if this value is infinity or not-a-number.

### ToInt32Checked

    public int ToInt32Checked();

Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer after truncating to an integer.

<b>Return Value:</b>

This number's value, truncated to a 32-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the truncated integer is less than -2147483648 or greater than 2147483647.

### ToInt32IfExact

    public int ToInt32IfExact();

Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 32-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than -2147483648 or greater than 2147483647.

### ToInt32Unchecked

    public int ToInt32Unchecked();

Truncates this number's value to an integer and returns the least-significant bits of its two's-complement form as a 32-bit signed integer.

<b>Return Value:</b>

This number, converted to a 32-bit signed integer. Returns 0 if this value is infinity or not-a-number.

### ToInt64Checked

    public long ToInt64Checked();

Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer after truncating to an integer.

<b>Return Value:</b>

This number's value, truncated to a 64-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the truncated integer is less than -9223372036854775808 or greater than 9223372036854775807.

### ToInt64IfExact

    public long ToInt64IfExact();

Converts this number's value to a 64-bit signed integer if it can fit in a 64-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 64-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than -9223372036854775808 or greater than 9223372036854775807.

### ToInt64Unchecked

    public long ToInt64Unchecked();

Truncates this number's value to an integer and returns the least-significant bits of its two's-complement form as a 64-bit signed integer.

<b>Return Value:</b>

This number, converted to a 64-bit signed integer. Returns 0 if this value is infinity or not-a-number.

### ToSByteChecked

    public sbyte ToSByteChecked();

Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer after truncating to an integer.

<b>Return Value:</b>

This number's value, truncated to an 8-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the truncated integer is less than -128 or greater than 127.

### ToSByteIfExact

    public sbyte ToSByteIfExact();

Converts this number's value to an 8-bit signed integer if it can fit in an 8-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as an 8-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than -128 or greater than 127.

### ToSByteUnchecked

    public sbyte ToSByteUnchecked();

Truncates this number's value to an integer and returns the least-significant bits of its two's-complement form as an 8-bit signed integer.

<b>Return Value:</b>

This number, converted to an 8-bit signed integer. Returns 0 if this value is infinity or not-a-number.

### ToSingle

    public float ToSingle();

Converts this value to a 32-bit floating-point number. The half-even rounding mode is used.

<b>Return Value:</b>

The closest 32-bit floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 32-bit floating point number.

### ToString

    public override string ToString();

Converts this object to a text string.

<b>Return Value:</b>

A string representation of this object. If this object's value is infinity or not-a-number, the result is the analogous return value of the  `EDecimal.ToString`  method. Otherwise, the return value has the following form: `[-]numerator/denominator` .

### ToUInt16Checked

    public ushort ToUInt16Checked();

Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer after truncating to an integer.

<b>Return Value:</b>

This number's value, truncated to a 16-bit unsigned integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the truncated integer is less than 0 or greater than 65535.

### ToUInt16IfExact

    public ushort ToUInt16IfExact();

Converts this number's value to a 16-bit unsigned integer if it can fit in a 16-bit unsigned integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 16-bit unsigned integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than 0 or greater than 65535.

### ToUInt16Unchecked

    public ushort ToUInt16Unchecked();

Truncates this number's value to an integer and returns the least-significant bits of its two's-complement form as a 16-bit unsigned integer.

<b>Return Value:</b>

This number, converted to a 16-bit unsigned integer. Returns 0 if this value is infinity or not-a-number.

### ToUInt32Checked

    public uint ToUInt32Checked();

Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer after truncating to an integer.

<b>Return Value:</b>

This number's value, truncated to a 32-bit signed integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the truncated integer is less than 0 or greater than 4294967295.

### ToUInt32IfExact

    public uint ToUInt32IfExact();

Converts this number's value to a 32-bit signed integer if it can fit in a 32-bit signed integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 32-bit signed integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than 0 or greater than 4294967295.

### ToUInt32Unchecked

    public uint ToUInt32Unchecked();

Truncates this number's value to an integer and returns the least-significant bits of its two's-complement form as a 32-bit signed integer.

<b>Return Value:</b>

This number, converted to a 32-bit signed integer. Returns 0 if this value is infinity or not-a-number.

### ToUInt64Checked

    public ulong ToUInt64Checked();

Converts this number's value to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer after truncating to an integer.

<b>Return Value:</b>

This number's value, truncated to a 64-bit unsigned integer.

<b>Exceptions:</b>

 * System.OverflowException:
This value is infinity or not-a-number, or the truncated integer is less than 0 or greater than 18446744073709551615.

### ToUInt64IfExact

    public ulong ToUInt64IfExact();

Converts this number's value to a 64-bit unsigned integer if it can fit in a 64-bit unsigned integer without rounding to a different numerical value.

<b>Return Value:</b>

This number's value as a 64-bit unsigned integer.

<b>Exceptions:</b>

 * System.ArithmeticException:
This value is infinity or not-a-number, is not an exact integer, or is less than 0 or greater than 18446744073709551615.

### ToUInt64Unchecked

    public ulong ToUInt64Unchecked();

Truncates this number's value to an integer and returns the least-significant bits of its two's-complement form as a 64-bit unsigned integer.

<b>Return Value:</b>

This number, converted to a 64-bit unsigned integer. Returns 0 if this value is infinity or not-a-number.
