## PeterO.Numbers.EFloats

    public static class EFloats

A class that implements additional operations on arbitrary-precision binary floating-point numbers.

### Member Summary
* <code>[And(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#And_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Performs a logical AND operation on two binary numbers in the form of logical operands.
* <code>[BooleanToEFloat(bool, PeterO.Numbers.EContext)](#BooleanToEFloat_bool_PeterO_Numbers_EContext)</code> - Converts a Boolean value (either true or false) to an arbitrary-precision binary floating-point number.
* <code>[Canonical(PeterO.Numbers.EFloat)](#Canonical_PeterO_Numbers_EFloat)</code> - Returns a canonical version of the specified arbitrary-precision number object.
* <code>[CompareTotal(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#CompareTotal_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Compares the values of one arbitrary-precision number object and another object, imposing a total ordering on all possible values.
* <code>[CompareTotalMagnitude(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#CompareTotalMagnitude_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Compares the absolute values of two arbitrary-precision number objects, imposing a total ordering on all possible values (ignoring their signs).
* <code>[Copy(PeterO.Numbers.EFloat)](#Copy_PeterO_Numbers_EFloat)</code> - Creates a copy of the specified arbitrary-precision number object.
* <code>[CopyAbs(PeterO.Numbers.EFloat)](#CopyAbs_PeterO_Numbers_EFloat)</code> - Returns an arbitrary-precision number object with the same value as the specified number object but with a nonnegative sign (that is, the specified number object's absolute value).
* <code>[CopyNegate(PeterO.Numbers.EFloat)](#CopyNegate_PeterO_Numbers_EFloat)</code> - Returns an arbitrary-precision number object with the sign reversed from the specified number object.
* <code>[CopySign(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat)](#CopySign_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat)</code> - Returns an arbitrary-precision number object with the same value as the first given number object but with a the same sign (positive or negative) as the second given number object.
* <code>[Int32ToEFloat(int, PeterO.Numbers.EContext)](#Int32ToEFloat_int_PeterO_Numbers_EContext)</code> - Creates a binary floating-point number from a 32-bit signed integer.
* <code>[Invert(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Invert_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Performs a logical NOT operation on a binary number in the form of a logical operand.
* <code>[IsCanonical(PeterO.Numbers.EFloat)](#IsCanonical_PeterO_Numbers_EFloat)</code> - Returns whether the specified arbitrary-precision number object is in a canonical form.
* <code>[IsFinite(PeterO.Numbers.EFloat)](#IsFinite_PeterO_Numbers_EFloat)</code> - Returns whether the specified arbitrary-precision number object is neither null nor infinity nor not-a-number (NaN).
* <code>[IsInfinite(PeterO.Numbers.EFloat)](#IsInfinite_PeterO_Numbers_EFloat)</code> - Returns whether the specified arbitrary-precision number object is positive or negative infinity.
* <code>[IsNaN(PeterO.Numbers.EFloat)](#IsNaN_PeterO_Numbers_EFloat)</code> - Returns whether the specified arbitrary-precision number object is a not-a-number (NaN).
* <code>[IsNormal(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#IsNormal_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Returns whether the specified number is a normal number.
* <code>[IsQuietNaN(PeterO.Numbers.EFloat)](#IsQuietNaN_PeterO_Numbers_EFloat)</code> - Returns whether the specified arbitrary-precision number object is a quiet not-a-number (NaN).
* <code>[IsSignalingNaN(PeterO.Numbers.EFloat)](#IsSignalingNaN_PeterO_Numbers_EFloat)</code> - Returns whether the specified arbitrary-precision number object is a signaling not-a-number (NaN).
* <code>[IsSigned(PeterO.Numbers.EFloat)](#IsSigned_PeterO_Numbers_EFloat)</code> - Returns whether the specified arbitrary-precision number object is negative (including negative infinity, negative not-a-number [NaN], or negative zero).
* <code>[IsSubnormal(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#IsSubnormal_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Returns whether the specified number is a subnormal number.
* <code>[IsZero(PeterO.Numbers.EFloat)](#IsZero_PeterO_Numbers_EFloat)</code> - Returns whether the specified arbitrary-precision number object is zero (positive zero or negative zero).
* <code>[LogB(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#LogB_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Returns the base-2 exponent of an arbitrary-precision binary number (when that number is expressed in scientific notation with one nonzero digit before the radix point).
* <code>[NumberClass(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#NumberClass_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Finds the number class for an arbitrary-precision binary number object.
* <code>[NumberClassString(int)](#NumberClassString_int)</code> - Converts a number class identifier (ranging from 0 through 9) to a text string.
* <code>[Or(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Or_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Performs a logical OR operation on two binary numbers in the form of logical operands.
* <code>[Radix(PeterO.Numbers.EContext)](#Radix_PeterO_Numbers_EContext)</code> - Returns the number 2, the binary radix.
* <code>[Rescale(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Rescale_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision binary number with the same value as this object but with the specified exponent, expressed as an arbitrary-precision binary number.
* <code>[Rotate(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Rotate_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Rotates the bits of an arbitrary-precision binary number's significand.
* <code>[SameQuantum(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat)](#SameQuantum_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat)</code> - Returns whether two arbitrary-precision numbers have the same exponent, they both are not-a-number (NaN), or they both are infinity (positive, negative, or both).
* <code>[ScaleB(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#ScaleB_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Finds an arbitrary-precision binary number whose binary point is moved a given number of places.
* <code>[Shift(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Shift_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Shifts the bits of an arbitrary-precision binary floating point number's significand.
* <code>[Trim(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Trim_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Returns an arbitrary-precision number with the same value as this one but with certain trailing zeros removed from its significand.
* <code>[Xor(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Xor_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Performs a logical exclusive-OR (XOR) operation on two binary numbers in the form of logical operands.

<a id="And_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### And

    public static PeterO.Numbers.EFloat And(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Performs a logical AND operation on two binary numbers in the form of <i>logical operands</i>. A  `logical operand`  is a nonnegative base-2 number with an Exponent property of 0 (examples include the base-2 numbers  `01001`  and  `111001`  ). The logical AND operation sets each bit of the result to 1 if the corresponding bits of each logical operand are both 1, and to 0 otherwise. For example,  `01001 AND 111010=01000` .

<b>Parameters:</b>

 * <i>ed1</i>: The first logical operand to the logical AND operation.

 * <i>ed2</i>: The second logical operand to the logical AND operation.

 * <i>ec</i>: An arithmetic context to control the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more bits than the maximum precision specified in this context, the operand's most significant bits that exceed that precision are discarded. This parameter can be null.

<b>Return Value:</b>

The result of the logical AND operation as a logical operand. Signals an invalid operation and returns not-a-number (NaN) if  <i>ed1</i>
,  <i>ed2</i>
, or both are not logical operands.

<a id="BooleanToEFloat_bool_PeterO_Numbers_EContext"></a>
### BooleanToEFloat

    public static PeterO.Numbers.EFloat BooleanToEFloat(
        bool b,
        PeterO.Numbers.EContext ec);

Converts a Boolean value (either true or false) to an arbitrary-precision binary floating-point number.

<b>Parameters:</b>

 * <i>b</i>: Either true or false.

 * <i>ec</i>: A context used for rounding the result. Can be null.

<b>Return Value:</b>

Either 1 if  <i>b</i>
 is true, or 0 if  <i>b</i>
 is false.. The result will be rounded as specified by the specified context, if any.

<a id="Canonical_PeterO_Numbers_EFloat"></a>
### Canonical

    public static PeterO.Numbers.EFloat Canonical(
        PeterO.Numbers.EFloat ed);

Returns a canonical version of the specified arbitrary-precision number object. In this method, this method behaves like the Copy method.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

A copy of the parameter  <i>ed</i>
.

<a id="CompareTotal_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### CompareTotal

    public static int CompareTotal(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat other,
        PeterO.Numbers.EContext ec);

Compares the values of one arbitrary-precision number object and another object, imposing a total ordering on all possible values. In this method:

 * For objects with the same value, the one with the higher exponent has a greater "absolute value".

 * Negative zero is less than positive zero.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

 * Negative numbers are less than positive numbers.

<b>Parameters:</b>

 * <i>ed</i>: The first arbitrary-precision number to compare.

 * <i>other</i>: The second arbitrary-precision number to compare.

 * <i>ec</i>: An arithmetic context. Flags will be set in this context only if  `HasFlags`  and  `IsSimplified`  of the context are true and only if an operand needed to be rounded before carrying out the operation. Can be null.

<b>Return Value:</b>

The number 0 if both objects are null or equal, or -1 if the first object is null or less than the other object, or 1 if the first object is greater or the other object is null. Does not signal flags if either value is signaling NaN.

<a id="CompareTotalMagnitude_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### CompareTotalMagnitude

    public static int CompareTotalMagnitude(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat other,
        PeterO.Numbers.EContext ec);

Compares the absolute values of two arbitrary-precision number objects, imposing a total ordering on all possible values (ignoring their signs). In this method:

 * For objects with the same value, the one with the higher exponent has a greater "absolute value".

 * Negative zero and positive zero are considered equal.

 * Quiet NaN has a higher "absolute value" than signaling NaN. If both objects are quiet NaN or both are signaling NaN, the one with the higher diagnostic information has a greater "absolute value".

 * NaN has a higher "absolute value" than infinity.

 * Infinity has a higher "absolute value" than any finite number.

<b>Parameters:</b>

 * <i>ed</i>: The first arbitrary-precision number to compare.

 * <i>other</i>: The second arbitrary-precision number to compare.

 * <i>ec</i>: An arithmetic context. Flags will be set in this context only if  `HasFlags`  and  `IsSimplified`  of the context are true and only if an operand needed to be rounded before carrying out the operation. Can be null.

<b>Return Value:</b>

The number 0 if both objects are null or equal (ignoring their signs), or -1 if the first object is null or less than the other object (ignoring their signs), or 1 if the first object is greater (ignoring their signs) or the other object is null. Does not signal flags if either value is signaling NaN.

<a id="Copy_PeterO_Numbers_EFloat"></a>
### Copy

    public static PeterO.Numbers.EFloat Copy(
        PeterO.Numbers.EFloat ed);

Creates a copy of the specified arbitrary-precision number object.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object to copy.

<b>Return Value:</b>

A copy of the specified arbitrary-precision number object.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 is null.

<a id="CopyAbs_PeterO_Numbers_EFloat"></a>
### CopyAbs

    public static PeterO.Numbers.EFloat CopyAbs(
        PeterO.Numbers.EFloat ed);

Returns an arbitrary-precision number object with the same value as the specified number object but with a nonnegative sign (that is, the specified number object's absolute value).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

An arbitrary-precision number object with the same value as the specified number object but with a nonnegative sign.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 is null.

<a id="CopyNegate_PeterO_Numbers_EFloat"></a>
### CopyNegate

    public static PeterO.Numbers.EFloat CopyNegate(
        PeterO.Numbers.EFloat ed);

Returns an arbitrary-precision number object with the sign reversed from the specified number object.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

An arbitrary-precision number object with the sign reversed from the specified number object.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 is null.

<a id="CopySign_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat"></a>
### CopySign

    public static PeterO.Numbers.EFloat CopySign(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat other);

Returns an arbitrary-precision number object with the same value as the first given number object but with a the same sign (positive or negative) as the second given number object.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object with the value the result will have.

 * <i>other</i>: The parameter  <i>other</i>
 is an arbitrary-precision binary floating-point number.

<b>Return Value:</b>

An arbitrary-precision number object with the same value as the first given number object but with a the same sign (positive or negative) as the second given number object.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 or  <i>other</i>
 is null.

<a id="Int32ToEFloat_int_PeterO_Numbers_EContext"></a>
### Int32ToEFloat

    public static PeterO.Numbers.EFloat Int32ToEFloat(
        int i32,
        PeterO.Numbers.EContext ec);

Creates a binary floating-point number from a 32-bit signed integer.

<b>Parameters:</b>

 * <i>i32</i>: The parameter  <i>i32</i>
 is a 32-bit signed integer.

 * <i>ec</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. Can be null.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number with the closest representable value to the specified integer.

<a id="Invert_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Invert

    public static PeterO.Numbers.EFloat Invert(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EContext ec);

Performs a logical NOT operation on a binary number in the form of a <i>logical operand</i>. A  `logical operand`  is a nonnegative base-2 number with an Exponent property of 0 (examples include  `01001`  and  `111001`  ). The logical NOT operation sets each bit of the result to 1 if the corresponding bit is 0, and to 0 otherwise; it can set no more bits than the maximum precision, however. For example, if the maximum precision is 8 bits, then  `NOT 111010=11000101` .

<b>Parameters:</b>

 * <i>ed1</i>: The operand to the logical NOT operation.

 * <i>ec</i>: An arithmetic context to control the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more bits than the maximum precision specified in this context, the operand's most significant bits that exceed that precision are discarded. This parameter cannot be null and must specify a maximum precision (unlimited precision contexts are not allowed).

<b>Return Value:</b>

The result of the logical NOT operation as a logical operand. Signals an invalid operation and returns not-a-number (NaN) if  <i>ed1</i>
 is not a logical operand.

<a id="IsCanonical_PeterO_Numbers_EFloat"></a>
### IsCanonical

    public static bool IsCanonical(
        PeterO.Numbers.EFloat ed);

Returns whether the specified arbitrary-precision number object is in a canonical form. For the current version of EFloat, all EFloat objects are in a canonical form.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Always  `true` .

<a id="IsFinite_PeterO_Numbers_EFloat"></a>
### IsFinite

    public static bool IsFinite(
        PeterO.Numbers.EFloat ed);

Returns whether the specified arbitrary-precision number object is neither null nor infinity nor not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either  `true`  if the specified arbitrary-precision number object is neither null nor infinity nor not-a-number (NaN), or  `false`  otherwise.

<a id="IsInfinite_PeterO_Numbers_EFloat"></a>
### IsInfinite

    public static bool IsInfinite(
        PeterO.Numbers.EFloat ed);

Returns whether the specified arbitrary-precision number object is positive or negative infinity.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either  `true`  if the specified arbitrary-precision number object is positive or negative infinity, or  `false`  otherwise.

<a id="IsNaN_PeterO_Numbers_EFloat"></a>
### IsNaN

    public static bool IsNaN(
        PeterO.Numbers.EFloat ed);

Returns whether the specified arbitrary-precision number object is a not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either  `true`  or  `false` .

<a id="IsNormal_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### IsNormal

    public static bool IsNormal(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EContext ec);

Returns whether the specified number is a <i>normal</i> number. A <i>subnormal number</i> is a nonzero finite number whose Exponent property (or the number's exponent when that number is expressed in scientific notation with one digit before the radix point) is less than the minimum possible exponent for that number. A <i>normal number</i> is nonzero and finite, but not subnormal.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

 * <i>ec</i>: A context specifying the exponent range of arbitrary-precision numbers. Can be null. If AdjustExponent of the specified context is  `true` , a nonzero number is normal if the number's exponent (when that number is expressed in scientific notation with one nonzero digit before the radix point) is at least the specified context's EMax property (for example, if EMax is -100, 2.3456 * 10 <sup>-99</sup> is normal, but 2.3456 * 10 <sup>-102</sup> is not). If AdjustExponent of the specified context is  `false` , a nonzero number is subnormal if the number's Exponent property is at least given context's EMax property (for example, if EMax is -100, 23456 * 10 <sup>-99</sup> is normal, but 23456 * 10 <sup>-102</sup> is not).

<b>Return Value:</b>

Either  `true`  if the specified number is subnormal, or  `false`  otherwise. Returns  `true`  if the specified context is null or HasExponentRange of the specified context is  `false` .

<a id="IsQuietNaN_PeterO_Numbers_EFloat"></a>
### IsQuietNaN

    public static bool IsQuietNaN(
        PeterO.Numbers.EFloat ed);

Returns whether the specified arbitrary-precision number object is a quiet not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either  `true`  or  `false` .

<a id="IsSignalingNaN_PeterO_Numbers_EFloat"></a>
### IsSignalingNaN

    public static bool IsSignalingNaN(
        PeterO.Numbers.EFloat ed);

Returns whether the specified arbitrary-precision number object is a signaling not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either  `true`  or  `false` .

<a id="IsSigned_PeterO_Numbers_EFloat"></a>
### IsSigned

    public static bool IsSigned(
        PeterO.Numbers.EFloat ed);

Returns whether the specified arbitrary-precision number object is negative (including negative infinity, negative not-a-number [NaN], or negative zero).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either  `true`  or  `false` .

<a id="IsSubnormal_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### IsSubnormal

    public static bool IsSubnormal(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EContext ec);

Returns whether the specified number is a <i>subnormal</i> number. A <i>subnormal number</i> is a nonzero finite number whose Exponent property (or the number's exponent when that number is expressed in scientific notation with one digit before the radix point) is less than the minimum possible exponent for that number.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

 * <i>ec</i>: A context specifying the exponent range of arbitrary-precision numbers. Can be null. If AdjustExponent of the specified context is  `true` , a nonzero number is subnormal if the number's exponent (when that number is expressed in scientific notation with one nonzero digit before the radix point) is less than the specified context's EMax property (for example, if EMax is -100, 2.3456 * 10 <sup>-102</sup> is subnormal, but 2.3456 * 10 <sup>-99</sup> is not). If AdjustExponent of the specified context is  `false` , a nonzero number is subnormal if the number's Exponent property is less than the specified context's EMax property (for example, if EMax is -100, 23456 * 10 <sup>-102</sup> is subnormal, but 23456 * 10 <sup>-99</sup> is not).

<b>Return Value:</b>

Either  `true`  if the specified number is subnormal, or  `false`  otherwise. Returns  `false`  if the specified context is null or HasExponentRange of the specified context is  `false` .

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 is null.

<a id="IsZero_PeterO_Numbers_EFloat"></a>
### IsZero

    public static bool IsZero(
        PeterO.Numbers.EFloat ed);

Returns whether the specified arbitrary-precision number object is zero (positive zero or negative zero).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

 `true`  if the specified number has a value of zero (positive zero or negative zero); otherwise,  `false` .

<a id="LogB_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### LogB

    public static PeterO.Numbers.EFloat LogB(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EContext ec);

Returns the base-2 exponent of an arbitrary-precision binary number (when that number is expressed in scientific notation with one nonzero digit before the radix point). For example, returns 3 for the numbers  `1.11b * 2^3`  and  `111 * 2^1` .

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision binary number.

 * <i>ec</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. Can be null.

<b>Return Value:</b>

The base-2 exponent of the specified number (when that number is expressed in scientific notation with one nonzero digit before the radix point). Signals DivideByZero and returns negative infinity if  <i>ed</i>
 is zero. Returns positive infinity if  <i>ed</i>
 is positive infinity or negative infinity.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 is null.

<a id="NumberClass_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### NumberClass

    public static int NumberClass(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EContext ec);

Finds the number class for an arbitrary-precision binary number object.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision binary number object.

 * <i>ec</i>: A context object that specifies the precision and exponent range of arbitrary-precision numbers. This is used only to distinguish between normal and subnormal numbers. Can be null.

<b>Return Value:</b>

A 32-bit signed integer identifying the specified number object, number class as follows: 0 = positive normal; 1 = negative normal, 2 = positive subnormal, 3 = negative subnormal, 4 = positive zero, 5 = negative zero, 6 = positive infinity, 7 = negative infinity, 8 = quiet not-a-number (NaN), 9 = signaling NaN.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 is null.

<a id="NumberClassString_int"></a>
### NumberClassString

    public static string NumberClassString(
        int nc);

Converts a number class identifier (ranging from 0 through 9) to a text string. An arbitrary-precision number object can belong in one of ten number classes.

<b>Parameters:</b>

 * <i>nc</i>: An integer identifying a number class.

<b>Return Value:</b>

A text string identifying the specified number class as follows: 0 = "+Normal"; 1 = "-Normal", 2 = "+Subnormal", 3 = "-Subnormal", 4 = "+Zero", 5 = "-Zero", 6 = "+Infinity", 7 = "-Infinity", 8 = "NaN", 9 = "sNaN".

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter  <i>nc</i>
 is less than 0 or greater than 9.

<a id="Or_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Or

    public static PeterO.Numbers.EFloat Or(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Performs a logical OR operation on two binary numbers in the form of <i>logical operands</i>. A  `logical operand`  is a nonnegative base-2 number with an Exponent property of 0 (examples include the base-2 numbers  `01001`  and  `111001`  ). The logical OR operation sets each bit of the result to 1 if either or both of the corresponding bits of each logical operand are 1, and to 0 otherwise. For example,  `01001 OR 111010=111011` .

<b>Parameters:</b>

 * <i>ed1</i>: The first logical operand to the logical OR operation.

 * <i>ed2</i>: The second logical operand to the logical OR operation.

 * <i>ec</i>: An arithmetic context to control the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more bits than the maximum precision specified in this context, the operand's most significant bits that exceed that precision are discarded. This parameter can be null.

<b>Return Value:</b>

The result of the logical OR operation as a logical operand. Signals an invalid operation and returns not-a-number (NaN) if  <i>ed1</i>
,  <i>ed2</i>
, or both are not logical operands.

<a id="Radix_PeterO_Numbers_EContext"></a>
### Radix

    public static PeterO.Numbers.EFloat Radix(
        PeterO.Numbers.EContext ec);

Returns the number 2, the binary radix.

<b>Parameters:</b>

 * <i>ec</i>: Specifies an arithmetic context for rounding the number 2. Can be null.

<b>Return Value:</b>

The number 2, or the closest representable number to 2 in the arithmetic context.

<a id="Rescale_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Rescale

    public static PeterO.Numbers.EFloat Rescale(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat scale,
        PeterO.Numbers.EContext ec);

Returns an arbitrary-precision binary number with the same value as this object but with the specified exponent, expressed as an arbitrary-precision binary number. Note that this is not always the same as rounding to a given number of binary places, since it can fail if the difference between this value's exponent and the desired exponent is too big, depending on the maximum precision. If rounding to a number of binary places is desired, it's better to use the RoundToExponent and RoundToIntegral methods instead.

<b>Remark:</b> This method can be used to implement fixed-point binary arithmetic, in which a fixed number of digits come after the binary point. A fixed-point binary arithmetic in which no digits come after the binary point (a desired exponent of 0) is considered an "integer arithmetic" .

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision binary number whose exponent is to be changed.

 * <i>scale</i>: The desired exponent of the result, expressed as an arbitrary-precision binary number. The exponent is the number of fractional digits in the result, expressed as a negative number. Can also be positive, which eliminates lower-order places from the number. For example, -3 means round to the sixteenth (10b^-3, 0.0001b), and 3 means round to the sixteens-place (10b^3, 1000b). A value of 0 rounds the number to an integer.

 * <i>ec</i>: An arithmetic context to control precision and rounding of the result. If  `HasFlags`  of the context is true, will also store the flags resulting from the operation (the flags are in addition to the pre-existing flags). Can be null, in which case the default rounding mode is HalfEven.

<b>Return Value:</b>

An arbitrary-precision binary number with the same value as this object but with the exponent changed. Signals FlagInvalid and returns not-a-number (NaN) if the result can't fit the specified precision without rounding, or if the arithmetic context defines an exponent range and the specified exponent is outside that range.

<a id="Rotate_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Rotate

    public static PeterO.Numbers.EFloat Rotate(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Rotates the bits of an arbitrary-precision binary number's significand.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number containing the significand to rotate. If this significand contains more bits than the precision, the most-significant bits are chopped off the significand.

 * <i>ed2</i>: An arbitrary-precision number indicating the number of bits to rotate the first operand's significand. Must be an integer with an exponent of 0. If this parameter is positive, the significand is shifted to the left by the specified number of bits and the most-significant bits shifted out of the significand become the least-significant bits instead. If this parameter is negative, the number is shifted by the specified number of bits and the least-significant bits shifted out of the significand become the most-significant bits instead.

 * <i>ec</i>: An arithmetic context to control the precision of arbitrary-precision numbers. If this parameter is null or specifies an unlimited precision, this method has the same behavior as  `Shift` .

<b>Return Value:</b>

An arbitrary-precision binary number whose significand is rotated the specified number of bits. Signals an invalid operation and returns NaN (not-a-number) if  <i>ed2</i>
 is a signaling NaN or if  <i>ed2</i>
 is not an integer, is negative, has an exponent other than 0, or has an absolute value that exceeds the maximum precision specified in the context.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed2</i>
 or  <i>ed</i>
 is null.

<a id="SameQuantum_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat"></a>
### SameQuantum

    public static bool SameQuantum(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EFloat ed2);

Returns whether two arbitrary-precision numbers have the same exponent, they both are not-a-number (NaN), or they both are infinity (positive, negative, or both).

<b>Parameters:</b>

 * <i>ed1</i>: The first arbitrary-precision number.

 * <i>ed2</i>: The second arbitrary-precision number.

<b>Return Value:</b>

Either  `true`  if the specified arbitrary-precision numbers have the same exponent, they both are not-a-number (NaN), or they both are infinity (positive, negative, or both); otherwise,  `false` .

<a id="ScaleB_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### ScaleB

    public static PeterO.Numbers.EFloat ScaleB(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Finds an arbitrary-precision binary number whose binary point is moved a given number of places.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision binary number.

 * <i>ed2</i>: The number of binary places to move the binary point of "ed". This must be an integer with an exponent of 0.

 * <i>ec</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. Can be null.

<b>Return Value:</b>

The given arbitrary-precision binary number whose binary point is moved the specified number of places. Signals an invalid operation and returns not-a-number (NaN) if  <i>ed2</i>
 is infinity or NaN, has an Exponent property other than 0. Signals an invalid operation and returns not-a-number (NaN) if  <i>ec</i>
 defines a limited precision and exponent range and if  <i>ed2</i>
 's absolute value is greater than twice the sum of the context's EMax property and its Precision property.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 or  <i>ed2</i>
 is null.

<a id="Shift_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Shift

    public static PeterO.Numbers.EFloat Shift(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Shifts the bits of an arbitrary-precision binary floating point number's significand.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision binary floating point number containing the significand to shift.

 * <i>ed2</i>: An arbitrary-precision number indicating the number of bits to shift the first operand's significand. Must be an integer with an exponent of 0. If this parameter is positive, the significand is shifted to the left by the specified number of bits. If this parameter is negative, the significand is shifted to the right by the specified number of bits.

 * <i>ec</i>: An arithmetic context to control the precision of arbitrary-precision numbers. Can be null.

<b>Return Value:</b>

An arbitrary-precision binary number whose significand is shifted the specified number of bits. Signals an invalid operation and returns NaN (not-a-number) if  <i>ed2</i>
 is a signaling NaN or if  <i>ed2</i>
 is not an integer, is negative, has an exponent other than 0, or has an absolute value that exceeds the maximum precision specified in the context.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 or  <i>ed2</i>
 is null.

<a id="Trim_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Trim

    public static PeterO.Numbers.EFloat Trim(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EContext ec);

Returns an arbitrary-precision number with the same value as this one but with certain trailing zeros removed from its significand. If the number's exponent is 0, it is returned unchanged (but may be rounded depending on the arithmetic context); if that exponent is greater 0, its trailing zeros are removed from the significand (then rounded if necessary); if that exponent is less than 0, its trailing zeros are removed from the significand until the exponent reaches 0 (then the number is rounded if necessary).

<b>Parameters:</b>

 * <i>ed1</i>: An arbitrary-precision number.

 * <i>ec</i>: An arithmetic context to control the precision, rounding, and exponent range of the result. Can be null.

<b>Return Value:</b>

An arbitrary-precision number with the same value as this one but with certain trailing zeros removed from its significand. If  <i>ed1</i>
 is not-a-number (NaN) or infinity, it is generally returned unchanged.

<a id="Xor_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Xor

    public static PeterO.Numbers.EFloat Xor(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Performs a logical exclusive-OR (XOR) operation on two binary numbers in the form of <i>logical operands</i>. A  `logical operand`  is a nonnegative base-2 number with an Exponent property of 0 (examples include the base-2 numbers  `01001`  and  `111001`  ). The logical exclusive-OR operation sets each digit of the result to 1 if either corresponding digit of the logical operands, but not both, is 1, and to 0 otherwise. For example,  `01001 XOR 111010 = 101010` .

<b>Parameters:</b>

 * <i>ed1</i>: The first logical operand to the logical exclusive-OR operation.

 * <i>ed2</i>: The second logical operand to the logical exclusive-OR operation.

 * <i>ec</i>: An arithmetic context to control the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more bits than the maximum precision specified in this context, the operand's most significant bits that exceed that precision are discarded. This parameter can be null.

<b>Return Value:</b>

The result of the logical exclusive-OR operation as a logical operand. Signals an invalid operation and returns not-a-number (NaN) if  <i>ed1</i>
,  <i>ed2</i>
, or both are not logical operands.
