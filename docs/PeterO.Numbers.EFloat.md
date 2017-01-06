## PeterO.Numbers.EFloat

    public sealed class EFloat :
        System.IComparable,
        System.IEquatable

Represents an arbitrary-precision binary floating-point number. (The "E" stands for "extended", meaning that instances of this class can be values other than numbers proper, such as infinity and not-a-number.) Each number consists of an integer mantissa (significand) and an integer exponent, both arbitrary-precision. The value of the number equals mantissa (significand) * 2^exponent. This class also supports values for negative zero, not-a-number (NaN) values, and infinity.Passing a signaling NaN to any arithmetic operation shown here will signal the flag FlagInvalid and return a quiet NaN, even if another operand to that operation is a quiet NaN, unless noted otherwise.

Passing a quiet NaN to any arithmetic operation shown here will return a quiet NaN, unless noted otherwise.

Unless noted otherwise,passing a null arbitrary-precision binary float argument to any method here will throw an exception.

When an arithmetic operation signals the flag FlagInvalid, FlagOverflow, or FlagDivideByZero, it will not throw an exception too, unless the operation's trap is enabled in the precision context (see EContext's Traps property).

An arbitrary-precision binary float value can be serialized in one of the following ways:

 * By calling the toString() method. However, not all strings can be converted back to an arbitrary-precision binary float without loss, especially if the string has a fractional part.

 * By calling the UnsignedMantissa, Exponent, and IsNegative properties, and calling the IsInfinity, IsQuietNaN, and IsSignalingNaN methods. The return values combined will uniquely identify a particular arbitrary-precision binary float value.

If an operation requires creating an intermediate value that might be too big to fit in memory (or might require more than 2 gigabytes of memory to store -- due to the current use of a 32-bit integer internally as a length), the operation may signal an invalid-operation flag and return not-a-number (NaN). In certain rare cases, the CompareTo method may throw OutOfMemoryException (called OutOfMemoryError in Java) in the same circumstances.

<b>Thread safety</b>

Instances of this class are immutable, so they are inherently safe for use by multiple threads. Multiple instances of this object with the same properties are interchangeable, so they should not be compared using the "==" operator (which might only check if each side of the operator is the same instance).

<b>Comparison considerations</b>

This class's natural ordering (under the CompareTo method) is not consistent with the Equals method. This means that two values that compare as equal under the CompareTo method might not be equal under the Equals method. The CompareTo method compares the mathematical values of the two instances passed to it (and considers two different NaN values as equal), while two instances with the same mathematical value, but different exponents, will be considered unequal under the Equals method.

### NaN

    public static readonly PeterO.Numbers.EFloat NaN;

A not-a-number value.

### NegativeInfinity

    public static readonly PeterO.Numbers.EFloat NegativeInfinity;

Negative infinity, less than any other number.

### NegativeZero

    public static readonly PeterO.Numbers.EFloat NegativeZero;

Represents the number negative zero.

### One

    public static readonly PeterO.Numbers.EFloat One;

Represents the number 1.

### PositiveInfinity

    public static readonly PeterO.Numbers.EFloat PositiveInfinity;

Positive infinity, greater than any other number.

### SignalingNaN

    public static readonly PeterO.Numbers.EFloat SignalingNaN;

A not-a-number value that signals an invalid operation flag when it's passed as an argument to any arithmetic operation in arbitrary-precision binary float.

### Ten

    public static readonly PeterO.Numbers.EFloat Ten;

Represents the number 10.

### Zero

    public static readonly PeterO.Numbers.EFloat Zero;

Represents the number 0.

### Exponent

    public PeterO.Numbers.EInteger Exponent { get; }

Gets this object's exponent. This object's value will be an integer if the exponent is positive or zero.

<b>Returns:</b>

This object's exponent. This object's value will be an integer if the exponent is positive or zero.

### IsFinite

    public bool IsFinite { get; }

Gets a value indicating whether this object is finite (not infinity or NaN).

<b>Returns:</b>

 `true`  if this object is finite (not infinity or not-a-number (NaN)); otherwise,  `false` .

### IsNegative

    public bool IsNegative { get; }

Gets a value indicating whether this object is negative, including negative zero.

<b>Returns:</b>

 `true`  if this object is negative, including negative zero; otherwise,  `false` .

### IsZero

    public bool IsZero { get; }

Gets a value indicating whether this object's value equals 0.

<b>Returns:</b>

 `true`  if this object's value equals 0; otherwise, .  `false` .

### Mantissa

    public PeterO.Numbers.EInteger Mantissa { get; }

Gets this object's unscaled value.

<b>Returns:</b>

This object's unscaled value. Will be negative if this object's value is negative (including a negative NaN).

### Sign

    public int Sign { get; }

Gets this value's sign: -1 if negative; 1 if positive; 0 if zero.

<b>Returns:</b>

This value's sign: -1 if negative; 1 if positive; 0 if zero.

### UnsignedMantissa

    public PeterO.Numbers.EInteger UnsignedMantissa { get; }

Gets the absolute value of this object's unscaled value.

<b>Returns:</b>

The absolute value of this object's unscaled value.

### Abs

    public PeterO.Numbers.EFloat Abs(
        PeterO.Numbers.EContext context);

Finds the absolute value of this object (if it's negative, it becomes positive).

<b>Parameters:</b>

 * <i>context</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The absolute value of this object. Signals FlagInvalid and returns quiet NaN if this value is signaling NaN.

### Abs

    public PeterO.Numbers.EFloat Abs();

Finds the absolute value of this object (if it's negative, it becomes positive).

<b>Return Value:</b>

An arbitrary-precision binary float. Returns signaling NaN if this value is signaling NaN.

### Add

    public PeterO.Numbers.EFloat Add(
        PeterO.Numbers.EFloat otherValue);

Adds this object and another binary float and returns the result.

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision binary float.

<b>Return Value:</b>

The sum of the two objects.

### Add

    public PeterO.Numbers.EFloat Add(
        PeterO.Numbers.EFloat otherValue,
        PeterO.Numbers.EContext ctx);

Finds the sum of this object and another object. The result's exponent is set to the lower of the exponents of the two operands.

<b>Parameters:</b>

 * <i>otherValue</i>: The number to add to.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The sum of thisValue and the other object.

### CompareTo

    public sealed int CompareTo(
        PeterO.Numbers.EFloat other);

Compares the mathematical values of this object and another object, accepting NaN values.This method is not consistent with the Equals method because two different numbers with the same mathematical value, but different exponents, will compare as equal.

In this method, negative zero and positive zero are considered equal.

If this object or the other object is a quiet NaN or signaling NaN, this method will not trigger an error. Instead, NaN will compare greater than any other number, including infinity. Two different NaN values will be considered equal.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision binary float.

<b>Return Value:</b>

Less than 0 if this object's value is less than the other value, or greater than 0 if this object's value is greater than the other value or if  <i>other</i>
 is null, or 0 if both values are equal.

### CompareToSignal

    public PeterO.Numbers.EFloat CompareToSignal(
        PeterO.Numbers.EFloat other,
        PeterO.Numbers.EContext ctx);

Compares the mathematical values of this object and another object, treating quiet NaN as signaling.In this method, negative zero and positive zero are considered equal.

If this object or the other object is a quiet NaN or signaling NaN, this method will return a quiet NaN and will signal a FlagInvalid flag.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision binary float.

 * <i>ctx</i>: An arithmetic context. The precision, rounding, and exponent range are ignored. If  `HasFlags`  of the context is true, will store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null.

<b>Return Value:</b>

Quiet NaN if this object or the other object is NaN, or 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater.

### CompareToTotal

    public int CompareToTotal(
        PeterO.Numbers.EFloat other);

Compares the values of this object and another object, imposing a total ordering on all possible values. In this method:

 * For objects with the same value, the one with the higher exponent has a greater "absolute value".

 * Negative zero is less than positive zero.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

 * Negative numbers are less than positive numbers.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision binary float to compare with this one.

<b>Return Value:</b>

The number 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater.

### CompareToTotal

    public int CompareToTotal(
        PeterO.Numbers.EFloat other,
        PeterO.Numbers.EContext ctx);

Compares the values of this object and another object, imposing a total ordering on all possible values. In this method:

 * For objects with the same value, the one with the higher exponent has a greater "absolute value".

 * Negative zero is less than positive zero.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

 * Negative numbers are less than positive numbers.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision binary float to compare with this one.

 * <i>ctx</i>: An arithmetic context. Flags will be set in this context only if  `HasFlags`  and  `IsSimplified`  of the context are true and only if an operand needed to be rounded before carrying out the operation. Can be null.

<b>Return Value:</b>

The number 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater. Does not signal flags if either value is signaling NaN.

### CompareToTotalMagnitude

    public int CompareToTotalMagnitude(
        PeterO.Numbers.EFloat other);

Compares the absolute values of this object and another object, imposing a total ordering on all possible values (ignoring their signs). In this method:

 * For objects with the same value, the one with the higher exponent has a greater "absolute value".

 * Negative zero and positive zero are considered equal.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision binary float to compare with this one.

<b>Return Value:</b>

The number 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater.

### CompareToWithContext

    public PeterO.Numbers.EFloat CompareToWithContext(
        PeterO.Numbers.EFloat other,
        PeterO.Numbers.EContext ctx);

Compares the mathematical values of this object and another object.In this method, negative zero and positive zero are considered equal.

If this object or the other object is a quiet NaN or signaling NaN, this method returns a quiet NaN, and will signal a FlagInvalid flag if either is a signaling NaN.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision binary float.

 * <i>ctx</i>: An arithmetic context. The precision, rounding, and exponent range are ignored. If  `HasFlags`  of the context is true, will store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null.

<b>Return Value:</b>

Quiet NaN if this object or the other object is NaN, or 0 if both objects have the same value, or -1 if this object is less than the other value, or 1 if this object is greater.

### CopySign

    public PeterO.Numbers.EFloat CopySign(
        PeterO.Numbers.EFloat other);

Returns a number with the same value as this one, but copying the sign (positive or negative) of another number.

<b>Parameters:</b>

 * <i>other</i>: A number whose sign will be copied.

<b>Return Value:</b>

An arbitrary-precision binary float.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>other</i>
 is null.

### Create

    public static PeterO.Numbers.EFloat Create(
        int mantissaSmall,
        int exponentSmall);

Creates a number with the value exponent*2^mantissa (significand).

<b>Parameters:</b>

 * <i>mantissaSmall</i>: The parameter  <i>mantissaSmall</i>
 is not documented yet.

 * <i>exponentSmall</i>: The parameter  <i>exponentSmall</i>
 is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary float.

### Create

    public static PeterO.Numbers.EFloat Create(
        PeterO.Numbers.EInteger mantissa,
        PeterO.Numbers.EInteger exponent);

Creates a number with the value exponent*2^mantissa (significand).

<b>Parameters:</b>

 * <i>mantissa</i>: The parameter  <i>mantissa</i>
 is not documented yet.

 * <i>exponent</i>: The parameter  <i>exponent</i>
 is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary float.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter "mantissa (significand)" or  <i>exponent</i>
 is null.

### CreateNaN

    public static PeterO.Numbers.EFloat CreateNaN(
        PeterO.Numbers.EInteger diag);

Creates a not-a-number arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>diag</i>: A number to use as diagnostic information associated with this object. If none is needed, should be zero.

<b>Return Value:</b>

A quiet not-a-number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>diag</i>
 is null.

 * System.ArgumentException:
The parameter <i>diag</i>
 is less than 0.

### CreateNaN

    public static PeterO.Numbers.EFloat CreateNaN(
        PeterO.Numbers.EInteger diag,
        bool signaling,
        bool negative,
        PeterO.Numbers.EContext ctx);

Creates a not-a-number arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>diag</i>: A number to use as diagnostic information associated with this object. If none is needed, should be zero.

 * <i>signaling</i>: Whether the return value will be signaling (true) or quiet (false).

 * <i>negative</i>: Whether the return value is negative.

 * <i>ctx</i>: An arithmetic context to control the precision (in bits) of the diagnostic information. The rounding and exponent range of this context will be ignored. Can be null. The only flag that can be signaled in this context is FlagInvalid, which happens if diagnostic information needs to be truncated and too much memory is required to do so.

<b>Return Value:</b>

An arbitrary-precision binary float.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>diag</i>
 is null.

 * System.ArgumentException:
The parameter <i>diag</i>
 is less than 0.

### Divide

    public PeterO.Numbers.EFloat Divide(
        PeterO.Numbers.EFloat divisor);

Divides this object by another binary float and returns the result. When possible, the result will be exact.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

<b>Return Value:</b>

The quotient of the two numbers. Returns infinity if the divisor is 0 and the dividend is nonzero. Returns not-a-number (NaN) if the divisor and the dividend are 0. Returns NaN if the result can't be exact because it would have a nonterminating binary expansion.

### Divide

    public PeterO.Numbers.EFloat Divide(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EContext ctx);

Divides this arbitrary-precision binary float by another arbitrary-precision binary float. The preferred exponent for the result is this object's exponent minus the divisor's exponent.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0; or, either  <i>ctx</i>
is null or  <i>ctx</i>
 's precision is 0, and the result would have a nonterminating binary expansion; or, the rounding mode is ERounding.None and the result is not exact.

### DivideAndRemainderNaturalScale

    public PeterO.Numbers.EFloat[] DivideAndRemainderNaturalScale(
        PeterO.Numbers.EFloat divisor);

<b>Deprecated.</b> Renamed to DivRemNaturalScale.

Calculates the quotient and remainder using the DivideToIntegerNaturalScale and the formula in RemainderNaturalScale.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

<b>Return Value:</b>

A 2 element array consisting of the quotient and remainder in that order.

### DivideAndRemainderNaturalScale

    public PeterO.Numbers.EFloat[] DivideAndRemainderNaturalScale(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to DivRemNaturalScale.

Calculates the quotient and remainder using the DivideToIntegerNaturalScale and the formula in RemainderNaturalScale.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only in the division portion of the remainder calculation; as a result, it's possible for the remainder to have a higher precision than given in this context. Flags will be set on the given context only if the context's  `HasFlags`  is true and the integer part of the division result doesn't fit the precision and exponent range without rounding. Can be null, in which the precision is unlimited and no additional rounding, other than the rounding down to an integer after division, is needed.

<b>Return Value:</b>

A 2 element array consisting of the quotient and remainder in that order.

### DivideToExponent

    public PeterO.Numbers.EFloat DivideToExponent(
        PeterO.Numbers.EFloat divisor,
        long desiredExponentSmall,
        PeterO.Numbers.EContext ctx);

Divides two arbitrary-precision binary floats, and gives a particular exponent to the result.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

 * <i>desiredExponentSmall</i>: The desired exponent. A negative number places the cutoff point to the right of the usual radix point (so a negative number means the number of binary digit places to round to). A positive number places the cutoff point to the left of the usual radix point.

 * <i>ctx</i>: An arithmetic context object to control the rounding mode to use if the result must be scaled down to have the same exponent as this value. If the precision given in the context is other than 0, calls the Quantize method with both arguments equal to the result of the operation (and can signal FlagInvalid and return NaN if the result doesn't fit the given precision). If `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the context defines an exponent range and the desired exponent is outside that range. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

### DivideToExponent

    public PeterO.Numbers.EFloat DivideToExponent(
        PeterO.Numbers.EFloat divisor,
        long desiredExponentSmall,
        PeterO.Numbers.ERounding rounding);

Divides two arbitrary-precision binary floats, and gives a particular exponent to the result.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

 * <i>desiredExponentSmall</i>: The desired exponent. A negative number places the cutoff point to the right of the usual radix point (so a negative number means the number of binary digit places to round to). A positive number places the cutoff point to the left of the usual radix point.

 * <i>rounding</i>: The rounding mode to use if the result must be scaled down to have the same exponent as this value.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

### DivideToExponent

    public PeterO.Numbers.EFloat DivideToExponent(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EInteger desiredExponent,
        PeterO.Numbers.ERounding rounding);

Divides two arbitrary-precision binary floats, and gives a particular exponent to the result.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

 * <i>desiredExponent</i>: The desired exponent. A negative number places the cutoff point to the right of the usual radix point (so a negative number means the number of binary digit places to round to). A positive number places the cutoff point to the left of the usual radix point.

 * <i>rounding</i>: The rounding mode to use if the result must be scaled down to have the same exponent as this value.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Returns not-a-number (NaN) if the divisor and the dividend are 0. Returns NaN if the rounding mode is ERounding.None and the result is not exact.

### DivideToExponent

    public PeterO.Numbers.EFloat DivideToExponent(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EInteger exponent,
        PeterO.Numbers.EContext ctx);

Divides two arbitrary-precision binary floats, and gives a particular exponent to the result.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

 * <i>exponent</i>: The desired exponent. A negative number places the cutoff point to the right of the usual radix point (so a negative number means the number of binary digit places to round to). A positive number places the cutoff point to the left of the usual radix point.

 * <i>ctx</i>: An arithmetic context object to control the rounding mode to use if the result must be scaled down to have the same exponent as this value. If the precision given in the context is other than 0, calls the Quantize method with both arguments equal to the result of the operation (and can signal FlagInvalid and return NaN if the result doesn't fit the given precision). If `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

The quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the context defines an exponent range and the desired exponent is outside that range. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

### DivideToIntegerNaturalScale

    public PeterO.Numbers.EFloat DivideToIntegerNaturalScale(
        PeterO.Numbers.EFloat divisor);

Divides two arbitrary-precision binary floats, and returns the integer part of the result, rounded down, with the preferred exponent set to this value's exponent minus the divisor's exponent.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

<b>Return Value:</b>

The integer part of the quotient of the two objects. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0.

### DivideToIntegerNaturalScale

    public PeterO.Numbers.EFloat DivideToIntegerNaturalScale(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EContext ctx);

Divides this object by another object, and returns the integer part of the result (which is initially rounded down), with the preferred exponent set to this value's exponent minus the divisor's exponent.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the integer part of the result. Flags will be set on the given context only if the context's  `HasFlags`  is true and the integer part of the result doesn't fit the precision and exponent range without rounding. Can be null, in which the precision is unlimited and no additional rounding, other than the rounding down to an integer after division, is needed.

<b>Return Value:</b>

The integer part of the quotient of the two objects. Signals FlagInvalid and returns not-a-number (NaN) if the return value would overflow the exponent range. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

### DivideToIntegerZeroScale

    public PeterO.Numbers.EFloat DivideToIntegerZeroScale(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EContext ctx);

Divides this object by another object, and returns the integer part of the result, with the exponent set to 0.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision. The rounding and exponent range settings of this context are ignored. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited.

<b>Return Value:</b>

The integer part of the quotient of the two objects. The exponent will be set to 0. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0, or if the result doesn't fit the given precision.

### DivideToSameExponent

    public PeterO.Numbers.EFloat DivideToSameExponent(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.ERounding rounding);

Divides this object by another binary float and returns a result with the same exponent as this object (the dividend).

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>rounding</i>: The rounding mode to use if the result must be scaled down to have the same exponent as this value.

<b>Return Value:</b>

The quotient of the two numbers. Signals FlagDivideByZero and returns infinity if the divisor is 0 and the dividend is nonzero. Signals FlagInvalid and returns not-a-number (NaN) if the divisor and the dividend are 0. Signals FlagInvalid and returns not-a-number (NaN) if the rounding mode is ERounding.None and the result is not exact.

### DivRemNaturalScale

    public PeterO.Numbers.EFloat[] DivRemNaturalScale(
        PeterO.Numbers.EFloat divisor);

Calculates the quotient and remainder using the DivideToIntegerNaturalScale and the formula in RemainderNaturalScale.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

<b>Return Value:</b>

A 2 element array consisting of the quotient and remainder in that order.

### DivRemNaturalScale

    public PeterO.Numbers.EFloat[] DivRemNaturalScale(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EContext ctx);

Calculates the quotient and remainder using the DivideToIntegerNaturalScale and the formula in RemainderNaturalScale.

<b>Parameters:</b>

 * <i>divisor</i>: An arbitrary-precision binary float to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only in the division portion of the remainder calculation; as a result, it's possible for the remainder to have a higher precision than given in this context. Flags will be set on the given context only if the context's  `HasFlags`  is true and the integer part of the division result doesn't fit the precision and exponent range without rounding. Can be null, in which the precision is unlimited and no additional rounding, other than the rounding down to an integer after division, is needed.

<b>Return Value:</b>

A 2 element array consisting of the quotient and remainder in that order.

### Equals

    public override bool Equals(
        object obj);

Determines whether this object's mantissa (significand), exponent, and properties are equal to those of another object and that other object is an arbitrary-precision binary float. Not-a-number values are considered equal if the rest of their properties are equal.

<b>Parameters:</b>

 * <i>obj</i>: An arbitrary object.

<b>Return Value:</b>

 `true`  if the objects are equal; otherwise,  `false` .

### Equals

    public sealed bool Equals(
        PeterO.Numbers.EFloat other);

Determines whether this object's mantissa (significand), exponent, and properties are equal to those of another object. Not-a-number values are considered equal if the rest of their properties are equal.

<b>Parameters:</b>

 * <i>other</i>: An arbitrary-precision binary float.

<b>Return Value:</b>

 `true`  if this object's mantissa (significand) and exponent are equal to those of another object; otherwise,  `false` .

### EqualsInternal

    public bool EqualsInternal(
        PeterO.Numbers.EFloat otherValue);

Determines whether this object's mantissa (significand) and exponent are equal to those of another object.

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision binary float.

<b>Return Value:</b>

 `true`  if this object's mantissa (significand) and exponent are equal to those of another object; otherwise,  `false` .

### Exp

    public PeterO.Numbers.EFloat Exp(
        PeterO.Numbers.EContext ctx);

Finds e (the base of natural logarithms) raised to the power of this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).<i>This parameter can't be null, as the exponential function's results are generally not exact.</i> (Unlike in the General Binary Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

Exponential of this object. If this object's value is 1, returns an approximation to " e" within the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the parameter <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

### FromByte

    public static PeterO.Numbers.EFloat FromByte(
        byte inputByte);

Converts a byte (from 0 to 255) to an arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>inputByte</i>: The number to convert as a byte (from 0 to 255).

<b>Return Value:</b>

This number's value as an arbitrary-precision binary float.

### FromDouble

    public static PeterO.Numbers.EFloat FromDouble(
        double dbl);

Creates a binary float from a 64-bit floating-point number. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the floating point number to a string first.

<b>Parameters:</b>

 * <i>dbl</i>: A 64-bit floating-point number.

<b>Return Value:</b>

A binary float with the same value as  <i>dbl</i>
.

### FromEInteger

    public static PeterO.Numbers.EFloat FromEInteger(
        PeterO.Numbers.EInteger bigint);

Converts an arbitrary-precision integer to the same value as a binary float.

<b>Parameters:</b>

 * <i>bigint</i>: An arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision binary float.

### FromInt16

    public static PeterO.Numbers.EFloat FromInt16(
        short inputInt16);

Converts a 16-bit signed integer to an arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>inputInt16</i>: The number to convert as a 16-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision binary float.

### FromInt32

    public static PeterO.Numbers.EFloat FromInt32(
        int inputInt32);

Converts a 32-bit signed integer to an arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>inputInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision binary float.

### FromInt64

    public static PeterO.Numbers.EFloat FromInt64(
        long inputInt64);

Converts a 64-bit signed integer to an arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>inputInt64</i>: The number to convert as a 64-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision binary float.

### FromSByte

    public static PeterO.Numbers.EFloat FromSByte(
        sbyte inputSByte);

Converts an 8-bit signed integer to an arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>inputSByte</i>: The number to convert as an 8-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision binary float.

### FromSingle

    public static PeterO.Numbers.EFloat FromSingle(
        float flt);

Creates a binary float from a 32-bit floating-point number. This method computes the exact value of the floating point number, not an approximation, as is often the case by converting the floating point number to a string first.

<b>Parameters:</b>

 * <i>flt</i>: A 32-bit floating-point number.

<b>Return Value:</b>

A binary float with the same value as  <i>flt</i>
.

### FromString

    public static PeterO.Numbers.EFloat FromString(
        string str);

Not documented yet.

<b>Parameters:</b>

 * <i>str</i>: The parameter  <i>str</i>
 is not documented yet.

<b>Return Value:</b>

An EFloat object.

### FromString

    public static PeterO.Numbers.EFloat FromString(
        string str,
        int offset,
        int length);

Creates a binary float from a text string that represents a number. For more information, see the  `FromString(String, int,
            int, EContext)`  method.

<b>Parameters:</b>

 * <i>str</i>: A text string.

 * <i>offset</i>: A zero-based index showing where the desired portion of  <i>str</i>
 begins.

 * <i>length</i>: The length, in code units, of the desired portion of  <i>str</i>
 (but not more than  <i>str</i>
 's length).

<b>Return Value:</b>

An arbitrary-precision binary float.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>str</i>
 is null.

 * System.ArgumentException:
Either  <i>offset</i>
 or  <i>length</i>
 is less than 0 or greater than  <i>str</i>
 's length, or  <i>str</i>
 ' s length minus  <i>offset</i>
 is less than  <i>length</i>
.

### FromString

    public static PeterO.Numbers.EFloat FromString(
        string str,
        int offset,
        int length,
        PeterO.Numbers.EContext ctx);

Creates a binary float from a text string that represents a number. Note that if the string contains a negative exponent, the resulting value might not be exact, in which case the resulting binary float will be an approximation of this decimal number's value.The format of the string generally consists of:

 * An optional plus sign ("+" , U+002B) or minus sign ("-", U+002D) (if '-' , the value is negative.)

 * One or more digits, with a single optional decimal point after the first digit and before the last digit.

 * Optionally, "E+"/"e+" (positive exponent) or "E-"/"e-" (negative exponent) plus one or more digits specifying the exponent.

The string can also be "-INF", "-Infinity", "Infinity", "INF", quiet NaN ("NaN") followed by any number of digits, or signaling NaN ("sNaN") followed by any number of digits, all in any combination of upper and lower case.

All characters mentioned above are the corresponding characters in the Basic Latin range. In particular, the digits must be the basic digits 0 to 9 (U+0030 to U+0039). The string is not allowed to contain white space characters, including spaces.

<b>Parameters:</b>

 * <i>str</i>: A text string.

 * <i>offset</i>: A zero-based index showing where the desired portion of  <i>str</i>
 begins.

 * <i>length</i>: The length, in code units, of the desired portion of  <i>str</i>
 (but not more than  <i>str</i>
 's length).

 * <i>ctx</i>: An EContext object specifying the precision, rounding, and exponent range (in bits) to apply to the parsed number. Can be null.

<b>Return Value:</b>

The parsed number, converted to arbitrary-precision binary float.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>str</i>
 is null.

 * System.ArgumentException:
Either  <i>offset</i>
 or  <i>length</i>
 is less than 0 or greater than  <i>str</i>
 's length, or  <i>str</i>
 ' s length minus  <i>offset</i>
 is less than  <i>length</i>
.

### FromString

    public static PeterO.Numbers.EFloat FromString(
        string str,
        PeterO.Numbers.EContext ctx);

Creates a binary float from a text string that represents a number. For more information, see the FromString(String, int, int, EContext) method.

<b>Parameters:</b>

 * <i>str</i>: A text string.

 * <i>ctx</i>: An EContext object specifying the precision, rounding, and exponent range to apply to the parsed number. Can be null.

<b>Return Value:</b>

The parsed number, converted to arbitrary-precision binary float.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>str</i>
 is null.

### FromUInt16

    public static PeterO.Numbers.EFloat FromUInt16(
        ushort inputUInt16);

Converts a 16-bit unsigned integer to an arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>inputUInt16</i>: The number to convert as a 16-bit unsigned integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision binary float.

### FromUInt32

    public static PeterO.Numbers.EFloat FromUInt32(
        uint inputUInt32);

Converts a 32-bit signed integer to an arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>inputUInt32</i>: The number to convert as a 32-bit signed integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision binary float.

### FromUInt64

    public static PeterO.Numbers.EFloat FromUInt64(
        ulong inputUInt64);

Converts a 64-bit unsigned integer to an arbitrary-precision binary float.

<b>Parameters:</b>

 * <i>inputUInt64</i>: The number to convert as a 64-bit unsigned integer.

<b>Return Value:</b>

This number's value as an arbitrary-precision binary float.

### GetHashCode

    public override int GetHashCode();

Calculates this object's hash code. No application or process IDs are used in the hash code calculation.

<b>Return Value:</b>

This object's hash code.

### IsInfinity

    public bool IsInfinity();

Gets a value indicating whether this object is positive or negative infinity.

<b>Return Value:</b>

 `true`  if this object is positive or negative infinity; otherwise,  `false` .

### IsNaN

    public bool IsNaN();

Gets a value indicating whether this object is not a number (NaN).

<b>Return Value:</b>

 `true`  if this object is not a number (NaN); otherwise,  `false` .

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

Gets a value indicating whether this object is a quiet not-a-number value.

<b>Return Value:</b>

 `true`  if this object is a quiet not-a-number value; otherwise,  `false` .

### IsSignalingNaN

    public bool IsSignalingNaN();

Gets a value indicating whether this object is a signaling not-a-number value.

<b>Return Value:</b>

 `true`  if this object is a signaling not-a-number value; otherwise,  `false` .

### Log

    public PeterO.Numbers.EFloat Log(
        PeterO.Numbers.EContext ctx);

Finds the natural logarithm of this object, that is, the power (exponent) that e (the base of natural logarithms) must be raised to in order to equal this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).<i>This parameter can't be null, as the ln function's results are generally not exact.</i> (Unlike in the General Binary Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

Ln(this object). Signals the flag FlagInvalid and returns NaN if this object is less than 0 (the result would be a complex number with a real part equal to Ln of this object's absolute value and an imaginary part equal to pi, but the return value is still NaN.). Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0). Signals no flags and returns negative infinity if this object's value is 0.

### Log10

    public PeterO.Numbers.EFloat Log10(
        PeterO.Numbers.EContext ctx);

Finds the base-10 logarithm of this object, that is, the power (exponent) that the number 10 must be raised to in order to equal this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).<i>This parameter can't be null, as the ln function's results are generally not exact.</i> (Unlike in the General Binary Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

Ln(this object)/Ln(10). Signals the flag FlagInvalid and returns not-a-number (NaN) if this object is less than 0. Signals FlagInvalid and returns not-a-number (NaN) if the parameter <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

### Max

    public static PeterO.Numbers.EFloat Max(
        PeterO.Numbers.EFloat first,
        PeterO.Numbers.EFloat second);

Gets the greater value between two binary floats.

<b>Parameters:</b>

 * <i>first</i>: An arbitrary-precision binary float.

 * <i>second</i>: Another arbitrary-precision binary float.

<b>Return Value:</b>

The larger value of the two numbers.

### Max

    public static PeterO.Numbers.EFloat Max(
        PeterO.Numbers.EFloat first,
        PeterO.Numbers.EFloat second,
        PeterO.Numbers.EContext ctx);

Gets the greater value between two binary floats.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The larger value of the two numbers.

### MaxMagnitude

    public static PeterO.Numbers.EFloat MaxMagnitude(
        PeterO.Numbers.EFloat first,
        PeterO.Numbers.EFloat second);

Gets the greater value between two values, ignoring their signs. If the absolute values are equal, has the same effect as Max.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

<b>Return Value:</b>

An arbitrary-precision binary float.

### MaxMagnitude

    public static PeterO.Numbers.EFloat MaxMagnitude(
        PeterO.Numbers.EFloat first,
        PeterO.Numbers.EFloat second,
        PeterO.Numbers.EContext ctx);

Gets the greater value between two values, ignoring their signs. If the absolute values are equal, has the same effect as Max.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

An arbitrary-precision binary float.

### Min

    public static PeterO.Numbers.EFloat Min(
        PeterO.Numbers.EFloat first,
        PeterO.Numbers.EFloat second);

Gets the lesser value between two binary floats.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

<b>Return Value:</b>

The smaller value of the two numbers.

### Min

    public static PeterO.Numbers.EFloat Min(
        PeterO.Numbers.EFloat first,
        PeterO.Numbers.EFloat second,
        PeterO.Numbers.EContext ctx);

Gets the lesser value between two binary floats.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The smaller value of the two numbers.

### MinMagnitude

    public static PeterO.Numbers.EFloat MinMagnitude(
        PeterO.Numbers.EFloat first,
        PeterO.Numbers.EFloat second);

Gets the lesser value between two values, ignoring their signs. If the absolute values are equal, has the same effect as Min.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

<b>Return Value:</b>

An arbitrary-precision binary float.

### MinMagnitude

    public static PeterO.Numbers.EFloat MinMagnitude(
        PeterO.Numbers.EFloat first,
        PeterO.Numbers.EFloat second,
        PeterO.Numbers.EContext ctx);

Gets the lesser value between two values, ignoring their signs. If the absolute values are equal, has the same effect as Min.

<b>Parameters:</b>

 * <i>first</i>: The first value to compare.

 * <i>second</i>: The second value to compare.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

An arbitrary-precision binary float.

### MovePointLeft

    public PeterO.Numbers.EFloat MovePointLeft(
        int places);

Returns a number similar to this number but with the radix point moved to the left.

<b>Parameters:</b>

 * <i>places</i>: The number of binary digit places to move the radix point to the left. If this number is negative, instead moves the radix point to the right by this number's absolute value.

<b>Return Value:</b>

A number whose exponent is decreased by  <i>places</i>
, but not to more than 0.

### MovePointLeft

    public PeterO.Numbers.EFloat MovePointLeft(
        int places,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the radix point moved to the left.

<b>Parameters:</b>

 * <i>places</i>: The number of binary digit places to move the radix point to the left. If this number is negative, instead moves the radix point to the right by this number's absolute value.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

A number whose exponent is decreased by  <i>places</i>
, but not to more than 0.

### MovePointLeft

    public PeterO.Numbers.EFloat MovePointLeft(
        PeterO.Numbers.EInteger bigPlaces);

Returns a number similar to this number but with the radix point moved to the left.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The number of binary digit places to move the radix point to the left. If this number is negative, instead moves the radix point to the right by this number's absolute value.

<b>Return Value:</b>

A number whose exponent is decreased by  <i>bigPlaces</i>
, but not to more than 0.

### MovePointLeft

    public PeterO.Numbers.EFloat MovePointLeft(
        PeterO.Numbers.EInteger bigPlaces,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the radix point moved to the left.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The number of binary digit places to move the radix point to the left. If this number is negative, instead moves the radix point to the right by this number's absolute value.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

A number whose exponent is decreased by  <i>bigPlaces</i>
, but not to more than 0.

### MovePointRight

    public PeterO.Numbers.EFloat MovePointRight(
        int places);

Returns a number similar to this number but with the radix point moved to the right.

<b>Parameters:</b>

 * <i>places</i>: The number of binary digit places to move the radix point to the right. If this number is negative, instead moves the radix point to the left by this number's absolute value.

<b>Return Value:</b>

A number whose exponent is increased by  <i>places</i>
, but not to more than 0.

### MovePointRight

    public PeterO.Numbers.EFloat MovePointRight(
        int places,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the radix point moved to the right.

<b>Parameters:</b>

 * <i>places</i>: The number of binary digit places to move the radix point to the right. If this number is negative, instead moves the radix point to the left by this number's absolute value.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

A number whose exponent is increased by  <i>places</i>
, but not to more than 0.

### MovePointRight

    public PeterO.Numbers.EFloat MovePointRight(
        PeterO.Numbers.EInteger bigPlaces);

Returns a number similar to this number but with the radix point moved to the right.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The number of binary digit places to move the radix point to the right. If this number is negative, instead moves the radix point to the left by this number's absolute value.

<b>Return Value:</b>

A number whose exponent is increased by  <i>bigPlaces</i>
, but not to more than 0.

### MovePointRight

    public PeterO.Numbers.EFloat MovePointRight(
        PeterO.Numbers.EInteger bigPlaces,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the radix point moved to the right.

<b>Parameters:</b>

 * <i>bigPlaces</i>: The number of binary digit places to move the radix point to the right. If this number is negative, instead moves the radix point to the left by this number's absolute value.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

A number whose exponent is increased by  <i>bigPlaces</i>
, but not to more than 0.

### Multiply

    public PeterO.Numbers.EFloat Multiply(
        PeterO.Numbers.EFloat op,
        PeterO.Numbers.EContext ctx);

Multiplies two binary floats. The resulting scale will be the sum of the scales of the two binary floats. The result's sign is positive if both operands have the same sign, and negative if they have different signs.

<b>Parameters:</b>

 * <i>op</i>: Another binary float.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The product of the two binary floats.

### Multiply

    public PeterO.Numbers.EFloat Multiply(
        PeterO.Numbers.EFloat otherValue);

Multiplies two binary floats. The resulting exponent will be the sum of the exponents of the two binary floats.

<b>Parameters:</b>

 * <i>otherValue</i>: Another binary float.

<b>Return Value:</b>

The product of the two binary floats.

### MultiplyAndAdd

    public PeterO.Numbers.EFloat MultiplyAndAdd(
        PeterO.Numbers.EFloat multiplicand,
        PeterO.Numbers.EFloat augend);

Multiplies by one binary float, and then adds another binary float.

<b>Parameters:</b>

 * <i>multiplicand</i>: The value to multiply.

 * <i>augend</i>: The value to add.

<b>Return Value:</b>

The result this *  <i>multiplicand</i>
 + <i>augend</i>
.

### MultiplyAndAdd

    public PeterO.Numbers.EFloat MultiplyAndAdd(
        PeterO.Numbers.EFloat op,
        PeterO.Numbers.EFloat augend,
        PeterO.Numbers.EContext ctx);

Multiplies by one value, and then adds another value.

<b>Parameters:</b>

 * <i>op</i>: The value to multiply.

 * <i>augend</i>: The value to add.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. If the precision doesn't indicate a simplified arithmetic, rounding and precision/exponent adjustment is done only once, namely, after multiplying and adding.

<b>Return Value:</b>

The result thisValue * multiplicand + augend.

### MultiplyAndSubtract

    public PeterO.Numbers.EFloat MultiplyAndSubtract(
        PeterO.Numbers.EFloat op,
        PeterO.Numbers.EFloat subtrahend,
        PeterO.Numbers.EContext ctx);

Multiplies by one value, and then subtracts another value.

<b>Parameters:</b>

 * <i>op</i>: The value to multiply.

 * <i>subtrahend</i>: The value to subtract.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed. If the precision doesn't indicate a simplified arithmetic, rounding and precision/exponent adjustment is done only once, namely, after multiplying and subtracting.

<b>Return Value:</b>

The result thisValue * multiplicand - subtrahend.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>op</i>
 or  <i>subtrahend</i>
 is null.

### Negate

    public PeterO.Numbers.EFloat Negate(
        PeterO.Numbers.EContext context);

Returns a binary float with the same value as this object but with the sign reversed.

<b>Parameters:</b>

 * <i>context</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

An arbitrary-precision binary float. If this value is positive zero, returns positive zero. Signals FlagInvalid and returns quiet NaN if this value is signaling NaN.

### Negate

    public PeterO.Numbers.EFloat Negate();

Gets an object with the same value as this one, but with the sign reversed.

<b>Return Value:</b>

An arbitrary-precision binary float. If this value is positive zero, returns negative zero. Returns signaling NaN if this value is signaling NaN.

### NextMinus

    public PeterO.Numbers.EFloat NextMinus(
        PeterO.Numbers.EContext ctx);

Finds the largest value that's smaller than the given value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision and exponent range of the result. The rounding mode from this context is ignored. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).

<b>Return Value:</b>

Returns the largest value that's less than the given value. Returns negative infinity if the result is negative infinity. Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null, the precision is 0, or <i>ctx</i>
 has an unlimited exponent range.

### NextPlus

    public PeterO.Numbers.EFloat NextPlus(
        PeterO.Numbers.EContext ctx);

Finds the smallest value that's greater than the given value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context object to control the precision and exponent range of the result. The rounding mode from this context is ignored. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).

<b>Return Value:</b>

Returns the smallest value that's greater than the given value.Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null, the precision is 0, or <i>ctx</i>
 has an unlimited exponent range.

### NextToward

    public PeterO.Numbers.EFloat NextToward(
        PeterO.Numbers.EFloat otherValue,
        PeterO.Numbers.EContext ctx);

Finds the next value that is closer to the other object's value than this object's value. Returns a copy of this value with the same sign as the other value if both values are equal.

<b>Parameters:</b>

 * <i>otherValue</i>: An arbitrary-precision binary float that the return value will approach.

 * <i>ctx</i>: An arithmetic context object to control the precision and exponent range of the result. The rounding mode from this context is ignored. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).

<b>Return Value:</b>

Returns the next value that is closer to the other object' s value than this object's value. Signals FlagInvalid and returns NaN if the parameter  <i>ctx</i>
 is null, the precision is 0, or  <i>ctx</i>
 has an unlimited exponent range.

### Operator `+`

    public static PeterO.Numbers.EFloat operator +(
        PeterO.Numbers.EFloat bthis,
        PeterO.Numbers.EFloat otherValue);

Adds this object and another binary float and returns the result.

<b>Parameters:</b>

 * <i>bthis</i>: The parameter  <i>bthis</i>
 is not documented yet.

 * <i>otherValue</i>: The parameter  <i>otherValue</i>
 is not documented yet.

<b>Return Value:</b>

The sum of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

### Operator `/`

    public static PeterO.Numbers.EFloat operator /(
        PeterO.Numbers.EFloat dividend,
        PeterO.Numbers.EFloat divisor);

Divides this object by another binary float and returns the result. When possible, the result will be exact.

<b>Parameters:</b>

 * <i>dividend</i>: The number that will be divided by the divisor.

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

The quotient of the two numbers. Returns infinity if the divisor is 0 and the dividend is nonzero. Returns not-a-number (NaN) if the divisor and the dividend are 0. Returns NaN if the result can't be exact because it would have a nonterminating binary expansion.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>dividend</i>
 is null.

### Operator `%`

    public static PeterO.Numbers.EFloat operator %(
        PeterO.Numbers.EFloat dividend,
        PeterO.Numbers.EFloat divisor);

Not documented yet.

<b>Parameters:</b>

 * <i>dividend</i>: The number that will be divided by the divisor.

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

The result of the operation.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>dividend</i>
 is null.

### Operator `*`

    public static PeterO.Numbers.EFloat operator *(
        PeterO.Numbers.EFloat operand1,
        PeterO.Numbers.EFloat operand2);

Multiplies two binary floats. The resulting exponent will be the sum of the exponents of the two binary floats.

<b>Parameters:</b>

 * <i>operand1</i>: The first operand.

 * <i>operand2</i>: The second operand.

<b>Return Value:</b>

The product of the two binary floats.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>operand1</i>
 is null.

### Operator `-`

    public static PeterO.Numbers.EFloat operator -(
        PeterO.Numbers.EFloat bthis,
        PeterO.Numbers.EFloat subtrahend);

Subtracts an arbitrary-precision binary float from this instance and returns the result.

<b>Parameters:</b>

 * <i>bthis</i>: The parameter  <i>bthis</i>
 is not documented yet.

 * <i>subtrahend</i>: The parameter  <i>subtrahend</i>
 is not documented yet.

<b>Return Value:</b>

The difference of the two objects.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bthis</i>
 is null.

### Operator `-`

    public static PeterO.Numbers.EFloat operator -(
        PeterO.Numbers.EFloat bigValue);

Gets an object with the same value as this one, but with the sign reversed.

<b>Parameters:</b>

 * <i>bigValue</i>: The parameter  <i>bigValue</i>
 is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary float. If this value is positive zero, returns negative zero. Returns signaling NaN if this value is signaling NaN.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>bigValue</i>
 is null.

### PI

    public static PeterO.Numbers.EFloat PI(
        PeterO.Numbers.EContext ctx);

Finds the constant π, the circumference of a circle divided by its diameter.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).<i>This parameter can't be null, as π can never be represented exactly.</i>.

<b>Return Value:</b>

The constant π rounded to the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the parameter <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

### Plus

    public PeterO.Numbers.EFloat Plus(
        PeterO.Numbers.EContext ctx);

Rounds this object's value to a given precision, using the given rounding mode and range of exponent, and also converts negative zero to positive zero.

<b>Parameters:</b>

 * <i>ctx</i>: A context for controlling the precision, rounding mode, and exponent range. Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

The closest value to this object's value, rounded to the specified precision. Returns the same value as this object if <i>ctx</i>
 is null or the precision and exponent range are unlimited.

### Pow

    public PeterO.Numbers.EFloat Pow(
        int exponentSmall);

Raises this object's value to the given exponent.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The exponent to raise this object's value to.

<b>Return Value:</b>

This^exponent. Returns not-a-number (NaN) if this object and exponent are both 0.

### Pow

    public PeterO.Numbers.EFloat Pow(
        int exponentSmall,
        PeterO.Numbers.EContext ctx);

Raises this object's value to the given exponent.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The exponent to raise this object's value to.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

This^exponent. Signals the flag FlagInvalid and returns NaN if this object and exponent are both 0.

### Pow

    public PeterO.Numbers.EFloat Pow(
        PeterO.Numbers.EFloat exponent,
        PeterO.Numbers.EContext ctx);

Raises this object's value to the given exponent.

<b>Parameters:</b>

 * <i>exponent</i>: An arbitrary-precision binary float expressing the exponent to raise this object's value to.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

This^exponent. Signals the flag FlagInvalid and returns NaN if this object and exponent are both 0; or if this value is less than 0 and the exponent either has a fractional part or is infinity. Signals FlagInvalid and returns not-a-number (NaN) if the parameter  <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0), and the exponent has a fractional part.

### Precision

    public PeterO.Numbers.EInteger Precision();

Finds the number of digits in this number's mantissa (significand). Returns 1 if this value is 0, and 0 if this value is infinity or not-a-number (NaN).

<b>Return Value:</b>

An arbitrary-precision integer.

### Quantize

    public PeterO.Numbers.EFloat Quantize(
        int desiredExponentInt,
        PeterO.Numbers.EContext ctx);

Returns a binary float with the same value but a new exponent.Note that this is not always the same as rounding to a given number of binary digit places, since it can fail if the difference between this value's exponent and the desired exponent is too big, depending on the maximum precision. If rounding to a number of binary digit places is desired, it's better to use the RoundToExponent and RoundToIntegral methods instead.

<b>Remark:</b>This method can be used to implement ixed-point binary arithmetic, in which each binary float has a ixed number of digits after the radix point. The following code xample returns a fixed-point number with up to 20 digits before nd exactly 5 digits after the radix point:

    // After performing arithmetic operations, adjust  // the number to 5
    digits after the radix point number = number.Quantize(-5,  // five
    digits after the radix point EContext.ForPrecision(25)  // 25-digit
    precision);

A fixed-point binary arithmetic in which no digits come after the radix point (a desired exponent of 0) is considered an "integer arithmetic".

<b>Parameters:</b>

 * <i>desiredExponentInt</i>: The desired exponent for the result. The exponent is the number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags` of the context is true, ill also store the flags resulting from the operation (the flags re in addition to the pre-existing flags). Can be null, in which ase the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float with the same value as this object but with the exponent changed. Signals FlagInvalid and returns not-a-number (NaN) if this object is infinity, if the rounded result can't fit the given precision, or if the context defines an exponent range and the given exponent is outside that range.

### Quantize

    public PeterO.Numbers.EFloat Quantize(
        PeterO.Numbers.EFloat otherValue,
        PeterO.Numbers.EContext ctx);

Returns a binary float with the same value as this object but with the same exponent as another binary float.Note that this is not always the same as rounding to a given number of binary digit places, since it can fail if the difference between this value's exponent and the desired exponent is too big, depending on the maximum precision. If rounding to a number of binary digit places is desired, it's better to use the RoundToExponent and RoundToIntegral methods instead.

<b>Remark:</b> This method can be used to implement fixed-point binary arithmetic, in which a fixed number of digits come after the radix point. A fixed-point binary arithmetic in which no digits come after the radix point (a desired exponent of 0) is considered an "integer arithmetic".

<b>Parameters:</b>

 * <i>otherValue</i>: A binary float containing the desired exponent of the result. The mantissa (significand) is ignored. The exponent is the number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the sixteenth (10b^-3, 0.0001b), and 3 means round to the sixteen-place (10b^3, 1000b). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float with the same value as this object but with the exponent changed. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding, or if the arithmetic context defines an exponent range and the given exponent is outside that range.

### Quantize

    public PeterO.Numbers.EFloat Quantize(
        PeterO.Numbers.EInteger desiredExponent,
        PeterO.Numbers.EContext ctx);

Returns a binary float with the same value but a new exponent.Note that this is not always the same as rounding to a given number of binary digit places, since it can fail if the difference between this value's exponent and the desired exponent is too big, depending on the maximum precision. If rounding to a number of binary digit places is desired, it's better to use the RoundToExponent and RoundToIntegral methods instead.

<b>Remark:</b>This method can be used to implement ixed-point binary arithmetic, in which each binary float has a ixed number of digits after the radix point. The following code xample returns a fixed-point number with up to 20 digits before nd exactly 5 digits after the radix point:

    // After performing arithmetic operations, adjust
    // the number to 5
    // digits after the radix point
    number = number.Quantize(
    EInteger.FromInt32(-5),  // five digits after the radix point
    EContext.ForPrecision(25)  // 25-digit precision);

A fixed-point binary arithmetic in which no digits come after the radix point (a desired exponent of 0) is considered an "integer arithmetic".

<b>Parameters:</b>

 * <i>desiredExponent</i>: The desired exponent for the result. The exponent is the number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags` of the context is true, ill also store the flags resulting from the operation (the flags re in addition to the pre-existing flags). Can be null, in which ase the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float with the same value as this object but with the exponent changed. Signals FlagInvalid and returns not-a-number (NaN) if this object is infinity, if the rounded result can't fit the given precision, or if the context defines an exponent range and the given exponent is outside that range.

### Reduce

    public PeterO.Numbers.EFloat Reduce(
        PeterO.Numbers.EContext ctx);

Removes trailing zeros from this object's mantissa (significand). For example, 1.00 becomes 1.If this object's value is 0, changes the exponent to 0.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and rounding isn't needed.

<b>Return Value:</b>

This value with trailing zeros removed. Note that if the result has a very high exponent and the context says to clamp high exponents, there may still be some trailing zeros in the mantissa (significand).

### Remainder

    public PeterO.Numbers.EFloat Remainder(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EContext ctx);

Finds the remainder that results when dividing two arbitrary-precision binary floats. The remainder is the value that remains when the absolute value of this object is divided by the absolute value of the other object; the remainder has the same sign (positive or negative) as this object's value.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used both in the division portion and in the remainder portion of the remainder calculation. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no additional rounding (other than the rounding from integer division) is needed.

<b>Return Value:</b>

The remainder of the two numbers. Signals FlagInvalid and returns not-a-number (NaN) if the divisor is 0, or if the result doesn't fit the given precision.

### RemainderNaturalScale

    public PeterO.Numbers.EFloat RemainderNaturalScale(
        PeterO.Numbers.EFloat divisor);

Calculates the remainder of a number by the formula `"this" - (("this" / "divisor") * "divisor")`

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

<b>Return Value:</b>

An arbitrary-precision binary float.

### RemainderNaturalScale

    public PeterO.Numbers.EFloat RemainderNaturalScale(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EContext ctx);

Calculates the remainder of a number by the formula "this" - (("this" / "divisor") * "divisor").

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision, rounding, and exponent range of the result. This context will be used only in the division portion of the remainder calculation; as a result, it's possible for the return value to have a higher precision than given in this context. Flags will be set on the given context only if the context's  `HasFlags`  is true and the integer part of the division result doesn't fit the precision and exponent range without rounding. Can be null, in which the precision is unlimited and no additional rounding, other than the rounding down to an integer after division, is needed.

<b>Return Value:</b>

An arbitrary-precision binary float.

### RemainderNear

    public PeterO.Numbers.EFloat RemainderNear(
        PeterO.Numbers.EFloat divisor,
        PeterO.Numbers.EContext ctx);

Finds the distance to the closest multiple of the given divisor, based on the result of dividing this object's value by another object's value.

 * If this and the other object divide evenly, the result is 0.

 * If the remainder's absolute value is less than half of the divisor's absolute value, the result has the same sign as this object and will be the distance to the closest multiple.

 * If the remainder's absolute value is more than half of the divisor' s absolute value, the result has the opposite sign of this object and will be the distance to the closest multiple.

 * If the remainder's absolute value is exactly half of the divisor's absolute value, the result has the opposite sign of this object if the quotient, rounded down, is odd, and has the same sign as this object if the quotient, rounded down, is even, and the result's absolute value is half of the divisor's absolute value.

 This function is also known as the "IEEE Remainder" function.

<b>Parameters:</b>

 * <i>divisor</i>: The number to divide by.

 * <i>ctx</i>: An arithmetic context object to control the precision. The rounding and exponent range settings of this context are ignored (the rounding mode is always treated as HalfEven). If `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which the precision is unlimited.

<b>Return Value:</b>

The distance of the closest multiple. Signals FlagInvalid and returns not-a-number (NaN) if the divisor is 0, or either the result of integer division (the quotient) or the remainder wouldn't fit the given precision.

### RoundToExponent

    public PeterO.Numbers.EFloat RoundToExponent(
        int exponentSmall,
        PeterO.Numbers.EContext ctx);

Returns a binary float with the same value as this object but rounded to a new exponent if necessary.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float rounded to the closest value representable in the given precision. If the result can't fit the precision, additional digits are discarded to make it fit. Signals FlagInvalid and returns not-a-number (NaN) if the precision context defines an exponent range, the new exponent must be changed to the given exponent when rounding, and the given exponent is outside of the valid range of the arithmetic context.

### RoundToExponent

    public PeterO.Numbers.EFloat RoundToExponent(
        PeterO.Numbers.EInteger exponent,
        PeterO.Numbers.EContext ctx);

Returns a binary float with the same value as this object but rounded to a new exponent if necessary.

<b>Parameters:</b>

 * <i>exponent</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float rounded to the closest value representable in the given precision. If the result can't fit the precision, additional digits are discarded to make it fit. Signals FlagInvalid and returns not-a-number (NaN) if the precision context defines an exponent range, the new exponent must be changed to the given exponent when rounding, and the given exponent is outside of the valid range of the arithmetic context.

### RoundToExponentExact

    public PeterO.Numbers.EFloat RoundToExponentExact(
        int exponentSmall,
        PeterO.Numbers.EContext ctx);

Returns a binary float with the same value as this object but rounded to an integer, and signals an inexact flag if the result would be inexact.

<b>Parameters:</b>

 * <i>exponentSmall</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float rounded to the closest value representable in the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to the given exponent when rounding, and the given exponent is outside of the valid range of the arithmetic context.

### RoundToExponentExact

    public PeterO.Numbers.EFloat RoundToExponentExact(
        PeterO.Numbers.EInteger exponent,
        PeterO.Numbers.EContext ctx);

Returns a binary float with the same value as this object but rounded to the given exponent, and signals an inexact flag if the result would be inexact.

<b>Parameters:</b>

 * <i>exponent</i>: The minimum exponent the result can have. This is the maximum number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the thousandth (10^-3, 0.0001), and 3 means round to the thousand (10^3, 1000). A value of 0 rounds the number to an integer.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float rounded to the closest value representable in the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to the given exponent when rounding, and the given exponent is outside of the valid range of the arithmetic context.

### RoundToExponentExact

    public PeterO.Numbers.EFloat RoundToExponentExact(
        PeterO.Numbers.EInteger exponent,
        PeterO.Numbers.ERounding rounding);

Returns a binary number with the same value as this object but rounded to the given exponent.

<b>Parameters:</b>

 * <i>exponent</i>: An arbitrary-precision integer.

 * <i>rounding</i>: An ERounding object.

<b>Return Value:</b>

A binary number rounded to the closest value representable in the given precision.

### RoundToIntegerExact

    public PeterO.Numbers.EFloat RoundToIntegerExact(
        PeterO.Numbers.EContext ctx);

Returns a binary float with the same value as this object but rounded to an integer, and signals an inexact flag if the result would be inexact.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float rounded to the closest integer representable in the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to 0 when rounding, and 0 is outside of the valid range of the arithmetic context.

### RoundToIntegerNoRoundedFlag

    public PeterO.Numbers.EFloat RoundToIntegerNoRoundedFlag(
        PeterO.Numbers.EContext ctx);

Returns a binary float with the same value as this object but rounded to an integer, without adding the `FlagInexact`  or  `FlagRounded`  flags.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags), except that this function will never add the  `FlagRounded`  and `FlagInexact`  flags (the only difference between this and RoundToExponentExact). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float rounded to the closest integer representable in the given precision. If the result can't fit the precision, additional digits are discarded to make it fit. Signals FlagInvalid and returns not-a-number (NaN) if the precision context defines an exponent range, the new exponent must be changed to 0 when rounding, and 0 is outside of the valid range of the arithmetic context.

### RoundToIntegralExact

    public PeterO.Numbers.EFloat RoundToIntegralExact(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to RoundToIntegerExact.

Returns a binary float with the same value as this object but rounded to an integer, and signals an inexact flag if the result would be inexact.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float rounded to the closest integer representable in the given precision. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the given precision without rounding. Signals FlagInvalid and returns not-a-number (NaN) if the arithmetic context defines an exponent range, the new exponent must be changed to 0 when rounding, and 0 is outside of the valid range of the arithmetic context.

### RoundToIntegralNoRoundedFlag

    public PeterO.Numbers.EFloat RoundToIntegralNoRoundedFlag(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to RoundToIntegerNoRoundedFlag.

Returns a binary float with the same value as this object but rounded to an integer, without adding the `FlagInexact`  or  `FlagRounded`  flags.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags), except that this function will never add the  `FlagRounded`  and `FlagInexact`  flags (the only difference between this and RoundToExponentExact). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

A binary float rounded to the closest integer representable in the given precision. If the result can't fit the precision, additional digits are discarded to make it fit. Signals FlagInvalid and returns not-a-number (NaN) if the precision context defines an exponent range, the new exponent must be changed to 0 when rounding, and 0 is outside of the valid range of the arithmetic context.

### RoundToPrecision

    public PeterO.Numbers.EFloat RoundToPrecision(
        PeterO.Numbers.EContext ctx);

Rounds this object's value to a given precision, using the given rounding mode and range of exponent.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

<b>Return Value:</b>

The closest value to this object's value, rounded to the specified precision. Returns the same value as this object if <i>ctx</i>
 is null or the precision and exponent range are unlimited.

### ScaleByPowerOfTwo

    public PeterO.Numbers.EFloat ScaleByPowerOfTwo(
        int places);

Returns a number similar to this number but with the scale adjusted.

<b>Parameters:</b>

 * <i>places</i>: A 32-bit signed integer.

<b>Return Value:</b>

An arbitrary-precision binary float.

### ScaleByPowerOfTwo

    public PeterO.Numbers.EFloat ScaleByPowerOfTwo(
        int places,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with the scale adjusted.

<b>Parameters:</b>

 * <i>places</i>: A 32-bit signed integer.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null.

<b>Return Value:</b>

An arbitrary-precision binary float.

### ScaleByPowerOfTwo

    public PeterO.Numbers.EFloat ScaleByPowerOfTwo(
        PeterO.Numbers.EInteger bigPlaces);

Returns a number similar to this number but with the scale adjusted.

<b>Parameters:</b>

 * <i>bigPlaces</i>: An arbitrary-precision integer.

<b>Return Value:</b>

A number whose exponent is increased by  <i>bigPlaces</i>
.

### ScaleByPowerOfTwo

    public PeterO.Numbers.EFloat ScaleByPowerOfTwo(
        PeterO.Numbers.EInteger bigPlaces,
        PeterO.Numbers.EContext ctx);

Returns a number similar to this number but with its scale adjusted.

<b>Parameters:</b>

 * <i>bigPlaces</i>: An arbitrary-precision integer.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null.

<b>Return Value:</b>

A number whose exponent is increased by  <i>bigPlaces</i>
.

### Sqrt

    public PeterO.Numbers.EFloat Sqrt(
        PeterO.Numbers.EContext ctx);

Finds the square root of this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).<i>This parameter can't be null, as the square root function's results are generally not exact for many inputs.</i> (Unlike in the General Binary Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

The square root. Signals the flag FlagInvalid and returns NaN if this object is less than 0 (the square root would be a complex number, but the return value is still NaN). Signals FlagInvalid and returns not-a-number (NaN) if the parameter <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

### SquareRoot

    public PeterO.Numbers.EFloat SquareRoot(
        PeterO.Numbers.EContext ctx);

<b>Deprecated.</b> Renamed to Sqrt.

Finds the square root of this object's value.

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags).<i>This parameter can't be null, as the square root function's results are generally not exact for many inputs.</i> (Unlike in the General Binary Arithmetic Specification, any rounding mode is allowed.).

<b>Return Value:</b>

The square root. Signals the flag FlagInvalid and returns NaN if this object is less than 0 (the square root would be a complex number, but the return value is still NaN). Signals FlagInvalid and returns not-a-number (NaN) if the parameter <i>ctx</i>
 is null or the precision is unlimited (the context's Precision property is 0).

### Subtract

    public PeterO.Numbers.EFloat Subtract(
        PeterO.Numbers.EFloat otherValue);

Subtracts an arbitrary-precision binary float from this instance and returns the result.

<b>Parameters:</b>

 * <i>otherValue</i>: The number to subtract from this instance's value.

<b>Return Value:</b>

The difference of the two objects.

### Subtract

    public PeterO.Numbers.EFloat Subtract(
        PeterO.Numbers.EFloat otherValue,
        PeterO.Numbers.EContext ctx);

Subtracts an arbitrary-precision binary float from this instance.

<b>Parameters:</b>

 * <i>otherValue</i>: The number to subtract from this instance's value.

 * <i>ctx</i>: An arithmetic context to control precision, rounding, and exponent range of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the precision is unlimited and no rounding is needed.

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

### ToDouble

    public double ToDouble();

Not documented yet.

<b>Return Value:</b>

A 64-bit floating-point number.

### ToEDecimal

    public PeterO.Numbers.EDecimal ToEDecimal();

Converts this value to an arbitrary-precision decimal number.

<b>Return Value:</b>

An arbitrary-precision decimal number.

### ToEInteger

    public PeterO.Numbers.EInteger ToEInteger();

Converts this value to an arbitrary-precision integer. Any fractional part of this value will be discarded when converting to an arbitrary-precision integer.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

### ToEIntegerExact

    public PeterO.Numbers.EInteger ToEIntegerExact();

<b>Deprecated.</b> Renamed to ToEIntegerIfExact.

Converts this value to an arbitrary-precision integer, checking whether the value contains a fractional part.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

 * System.ArithmeticException:
This object's value is not an exact integer.

### ToEIntegerIfExact

    public PeterO.Numbers.EInteger ToEIntegerIfExact();

Converts this value to an arbitrary-precision integer, checking whether the value contains a fractional part.

<b>Return Value:</b>

An arbitrary-precision integer.

<b>Exceptions:</b>

 * System.OverflowException:
This object's value is infinity or not-a-number (NaN).

 * System.ArithmeticException:
This object's value is not an exact integer.

### ToEngineeringString

    public string ToEngineeringString();

Converts this value to an arbitrary-precision decimal number, then returns the value of that decimal's ToEngineeringString method.

<b>Return Value:</b>

A text string.

### ToExtendedDecimal

    public PeterO.Numbers.EDecimal ToExtendedDecimal();

<b>Deprecated.</b> Renamed to ToEDecimal.

Converts this value to an arbitrary-precision decimal number.

<b>Return Value:</b>

An arbitrary-precision decimal number.

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

### ToPlainString

    public string ToPlainString();

Converts this value to a string, but without exponential notation.

<b>Return Value:</b>

A text string.

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

### ToShortestString

    public string ToShortestString(
        PeterO.Numbers.EContext ctx);

Returns a string representation of this number's value after rounding to the given precision (using the given arithmetic context). If the number after rounding is neither infinity nor not-a-number (NaN), returns the shortest decimal form (in terms of nonzero decimal digits) of this number's value that results in the rounded number after the decimal form is converted to binary floating-point format (using the given arithmetic context).

<b>Parameters:</b>

 * <i>ctx</i>: An arithmetic context to control precision (in bits), rounding, and exponent range of the rounded number. If `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null. If this parameter is null or defines no maximum precision, returns the same value as the ToString() method.

<b>Return Value:</b>

Shortest decimal form of this number's value for the given arithmetic context. The text string will be in exponential notation if the number's first nonzero decimal digit is more than five digits after the decimal point, or if the number's exponent is greater than 0 and its value is 10, 000, 000 or greater.

### ToSingle

    public float ToSingle();

Converts this value to its closest equivalent as 32-bit floating-point number. The half-even rounding mode is used.If this value is a NaN, sets the high bit of the 32-bit floating point number's significand area for a quiet NaN, and clears it for a signaling NaN. Then the other bits of the significand area are set to the lowest bits of this object's unsigned mantissa (significand), and the next-highest bit of the significand area is set if those bits are all zeros and this is a signaling NaN. Unfortunately, in the .NET implementation, the return value of this method may be a quiet NaN even if a signaling NaN would otherwise be generated.

<b>Return Value:</b>

The closest 32-bit floating-point number to this value. The return value can be positive infinity or negative infinity if this value exceeds the range of a 32-bit floating point number.

### ToString

    public override string ToString();

Converts this number's value to a text string.

<b>Return Value:</b>

A string representation of this object. The value is converted to decimal and the decimal form of this number's value is returned. The text string will be in exponential notation if the converted number's scale is positive or if the number's first nonzero decimal digit is more than five digits after the decimal point.

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

### Ulp

    public PeterO.Numbers.EFloat Ulp();

Returns the unit in the last place. The mantissa (significand) will be 1 and the exponent will be this number's exponent. Returns 1 with an exponent of 0 if this number is infinity or not-a-number (NaN).

<b>Return Value:</b>

An arbitrary-precision binary float.
