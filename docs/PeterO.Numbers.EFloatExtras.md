## PeterO.Numbers.EFloatExtras

    public static class EFloatExtras

A class that implements additional operations on arbitrary-precision binary floating-point numbers.

### Member Summary
* <code>[And(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#And_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[BoolToEFloat(bool, PeterO.Numbers.EContext)](#BoolToEFloat_bool_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Canonical(PeterO.Numbers.EFloat)](#Canonical_PeterO_Numbers_EFloat)</code> - Returns a canonical version of the given arbitrary-precision number object.
* <code>[CompareTotalMagnitude(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#CompareTotalMagnitude_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[CompareTotal(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#CompareTotal_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Compares the values of one arbitrary-precision number object and another object, imposing a total ordering on all possible values.
* <code>[CopyAbs(PeterO.Numbers.EFloat)](#CopyAbs_PeterO_Numbers_EFloat)</code> - Not documented yet.
* <code>[CopyNegate(PeterO.Numbers.EFloat)](#CopyNegate_PeterO_Numbers_EFloat)</code> - Not documented yet.
* <code>[CopySign(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat)](#CopySign_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat)</code> - Not documented yet.
* <code>[Copy(PeterO.Numbers.EFloat)](#Copy_PeterO_Numbers_EFloat)</code> - Not documented yet.
* <code>[Int32ToEFloat(int, PeterO.Numbers.EContext)](#Int32ToEFloat_int_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Invert(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Invert_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[IsCanonical(PeterO.Numbers.EFloat)](#IsCanonical_PeterO_Numbers_EFloat)</code> - Not documented yet.
* <code>[IsFinite(PeterO.Numbers.EFloat)](#IsFinite_PeterO_Numbers_EFloat)</code> - Not documented yet.
* <code>[IsInfinite(PeterO.Numbers.EFloat)](#IsInfinite_PeterO_Numbers_EFloat)</code> - Not documented yet.
* <code>[IsNaN(PeterO.Numbers.EFloat)](#IsNaN_PeterO_Numbers_EFloat)</code> - Returns whether the given arbitrary-precision number object is a not-a-number (NaN).
* <code>[IsNormal(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#IsNormal_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Returns whether the given number is anormalnumber.
* <code>[IsQuietNaN(PeterO.Numbers.EFloat)](#IsQuietNaN_PeterO_Numbers_EFloat)</code> - Returns whether the given arbitrary-precision number object is a quiet not-a-number (NaN).
* <code>[IsSignalingNaN(PeterO.Numbers.EFloat)](#IsSignalingNaN_PeterO_Numbers_EFloat)</code> - Returns whether the given arbitrary-precision number object is a signaling not-a-number (NaN).
* <code>[IsSigned(PeterO.Numbers.EFloat)](#IsSigned_PeterO_Numbers_EFloat)</code> - Returns whether the given arbitrary-precision number object is negative (including negative infinity, negative not-a-number [NaN], or negative zero).
* <code>[IsSubnormal(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#IsSubnormal_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Returns whether the given number is asubnormalnumber.
* <code>[IsZero(PeterO.Numbers.EFloat)](#IsZero_PeterO_Numbers_EFloat)</code> - Not documented yet.
* <code>[LogB(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#LogB_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[NumberClassString(int)](#NumberClassString_int)</code> - Converts a number class identifier (ranging from 1 to 9) to a text string.
* <code>[NumberClass(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#NumberClass_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Finds the number class for an arbitrary-precision decimal number object.
* <code>[Or(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Or_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Radix(PeterO.Numbers.EContext)](#Radix_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Rescale(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Rescale_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Rotate(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Rotate_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Rotates the digits of an arbitrary-precision binary number's mantissa.
* <code>[SameQuantum(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat)](#SameQuantum_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat)</code> - Not documented yet.
* <code>[ScaleB(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#ScaleB_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Shift(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Shift_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Trim(PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Trim_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Xor(PeterO.Numbers.EFloat, PeterO.Numbers.EFloat, PeterO.Numbers.EContext)](#Xor_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext)</code> - Not documented yet.

<a id="And_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### And

    public static PeterO.Numbers.EFloat And(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed1</i>: The parameter <i>ed1</i>
is not documented yet.

 * <i>ed2</i>: The parameter <i>ed2</i>
is not documented yet.

 * <i>ec</i>: A context that specifies the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more bits than the maximum precision specified in this context, the operand's most significant bits that exceed that precision are discarded. This parameter can be null.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="BoolToEFloat_bool_PeterO_Numbers_EContext"></a>
### BoolToEFloat

    public static PeterO.Numbers.EFloat BoolToEFloat(
        bool b,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>b</i>: The parameter <i>b</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="Canonical_PeterO_Numbers_EFloat"></a>
### Canonical

    public static PeterO.Numbers.EFloat Canonical(
        PeterO.Numbers.EFloat ed);

Returns a canonical version of the given arbitrary-precision number object. In this method, this is the same as that object.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

The parameter "ed".

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

 * <i>other</i>: An arbitrary-precision number to compare with the other one.

 * <i>ctx</i>: An arithmetic context. Flags will be set in this context only if `HasFlags`  and `IsSimplified`  of the context are true and only if an operand needed to be rounded before carrying out the operation. Can be null.

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

The number 0 if both objects have the same value, or -1 if the first object is less than the other value, or 1 if the first object is greater. Does not signal flags if either value is signaling NaN.

<a id="CompareTotalMagnitude_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### CompareTotalMagnitude

    public static int CompareTotalMagnitude(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat other,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

 * <i>other</i>: The parameter <i>other</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

A 32-bit signed integer.

<a id="Copy_PeterO_Numbers_EFloat"></a>
### Copy

    public static PeterO.Numbers.EFloat Copy(
        PeterO.Numbers.EFloat ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="CopyAbs_PeterO_Numbers_EFloat"></a>
### CopyAbs

    public static PeterO.Numbers.EFloat CopyAbs(
        PeterO.Numbers.EFloat ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="CopyNegate_PeterO_Numbers_EFloat"></a>
### CopyNegate

    public static PeterO.Numbers.EFloat CopyNegate(
        PeterO.Numbers.EFloat ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="CopySign_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat"></a>
### CopySign

    public static PeterO.Numbers.EFloat CopySign(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat other);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

 * <i>other</i>: The parameter <i>other</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="Int32ToEFloat_int_PeterO_Numbers_EContext"></a>
### Int32ToEFloat

    public static PeterO.Numbers.EFloat Int32ToEFloat(
        int i32,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>i32</i>: The parameter <i>i32</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="Invert_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Invert

    public static PeterO.Numbers.EFloat Invert(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed1</i>: The parameter <i>ed1</i>
is not documented yet.

 * <i>ec</i>: A context that specifies the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more bits than the maximum precision specified in this context, the operand's most significant bits that exceed that precision are discarded. This parameter cannot be null and must specify a maximum precision (unlimited precision contexts are not allowed).

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="IsCanonical_PeterO_Numbers_EFloat"></a>
### IsCanonical

    public static bool IsCanonical(
        PeterO.Numbers.EFloat ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

Always `true`  .

<a id="IsFinite_PeterO_Numbers_EFloat"></a>
### IsFinite

    public static bool IsFinite(
        PeterO.Numbers.EFloat ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsInfinite_PeterO_Numbers_EFloat"></a>
### IsInfinite

    public static bool IsInfinite(
        PeterO.Numbers.EFloat ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsNaN_PeterO_Numbers_EFloat"></a>
### IsNaN

    public static bool IsNaN(
        PeterO.Numbers.EFloat ed);

Returns whether the given arbitrary-precision number object is a not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsNormal_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### IsNormal

    public static bool IsNormal(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EContext ec);

Returns whether the given number is a<i>normal</i>number. A<i>subnormal number</i>is a nonzero finite number whose Exponent property (or the number's xponent in scientific notation) is less than the minimum possible xponent for that number. A<i>normal number</i>is nonzero and finite, but not subnormal.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

 * <i>ec</i>: A context specifying the exponent range of arbitrary-precision numbers. Can be null. If AdjustExponent of the given context is `true`  , a nonzero number is normal if the number's exponent in scientific notation is at least the given context's EMax property (e.g., if EMax is -100, 2.3456 * 10<sup>-99</sup>is normal, but 2.3456 * 10<sup>-102</sup>is not). If AdjustExponent of the given context is `false`  , a nonzero number is subnormal if the number's Exponent property is at least given context's EMax property (e.g., if EMax is -100, 23456 * 10<sup>-99</sup>is normal, but 23456 * 10<sup>-102</sup>is not).

<b>Return Value:</b>

Either `true`  if the given number is subnormal, or `false`  otherwise. Returns `true`  if the given context is null or HasExponentRange of the given context is `false`  .

<a id="IsQuietNaN_PeterO_Numbers_EFloat"></a>
### IsQuietNaN

    public static bool IsQuietNaN(
        PeterO.Numbers.EFloat ed);

Returns whether the given arbitrary-precision number object is a quiet not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsSignalingNaN_PeterO_Numbers_EFloat"></a>
### IsSignalingNaN

    public static bool IsSignalingNaN(
        PeterO.Numbers.EFloat ed);

Returns whether the given arbitrary-precision number object is a signaling not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsSigned_PeterO_Numbers_EFloat"></a>
### IsSigned

    public static bool IsSigned(
        PeterO.Numbers.EFloat ed);

Returns whether the given arbitrary-precision number object is negative (including negative infinity, negative not-a-number [NaN], or negative zero).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsSubnormal_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### IsSubnormal

    public static bool IsSubnormal(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EContext ec);

Returns whether the given number is a<i>subnormal</i>number. A<i>subnormal number</i>is a nonzero finite number whose Exponent property (or the number's xponent in scientific notation) is less than the minimum possible xponent for that number.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

 * <i>ec</i>: A context specifying the exponent range of arbitrary-precision numbers. Can be null. If AdjustExponent of the given context is `true`  , a nonzero number is subnormal if the number's exponent in scientific notation is less than the given context's EMax property (e.g., if EMax is -100, 2.3456 * 10<sup>-102</sup>is subnormal, but 2.3456 * 10<sup>-99</sup>is not). If AdjustExponent of the given context is `false`  , a nonzero number is subnormal if the number's Exponent property is less than the given context's EMax property (e.g., if EMax is -100, 23456 * 10<sup>-102</sup>is subnormal, but 23456 * 10<sup>-99</sup>is not).

<b>Return Value:</b>

Either `true`  if the given number is subnormal, or `false`  otherwise. Returns `false`  if the given context is null or HasExponentRange of the given context is `false`  .

<a id="IsZero_PeterO_Numbers_EFloat"></a>
### IsZero

    public static bool IsZero(
        PeterO.Numbers.EFloat ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="LogB_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### LogB

    public static PeterO.Numbers.EFloat LogB(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter  <i>ed</i>
 is not documented yet.

 * <i>ec</i>: The parameter  <i>ec</i>
 is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 is null.

<a id="NumberClass_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### NumberClass

    public static int NumberClass(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EContext ec);

Finds the number class for an arbitrary-precision decimal number object.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision decimal number object.

 * <i>ec</i>: A context object that specifies the precision and exponent range of arbitrary-precision numbers. This is used only to distinguish between normal and subnormal numbers. Can be null.

<b>Return Value:</b>

A 32-bit signed integer identifying the given number class as follows: 0 = positive normal; 1 = negative normal, 2 = positive subnormal, 3 = negative subnormal, 4 = positive zero, 5 = negative zero, 6 = positive infinity, 7 = negative infinity, 8 = quiet not-a-number (NaN), 9 = signaling NaN.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>ed</i>
is null.

<a id="NumberClassString_int"></a>
### NumberClassString

    public static string NumberClassString(
        int nc);

Converts a number class identifier (ranging from 1 to 9) to a text string. An arbitrary-precision number object can belong in one of ten number classes.

<b>Parameters:</b>

 * <i>nc</i>: An integer identifying a number class.

<b>Return Value:</b>

A text string identifying the given number class as follows: 0 = "+Normal"; 1 = "-Normal", 2 = "+Subnormal", 3 = "-Subnormal", 4 = "+Zero", 5 = "-Zero", 6 = "+Infinity", 7 = "-Infinity", 8 = "NaN", 9 = "sNaN".

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter <i>nc</i>
 is less than 0 or greater than 9 .

<a id="Or_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Or

    public static PeterO.Numbers.EFloat Or(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed1</i>: The parameter <i>ed1</i>
is not documented yet.

 * <i>ed2</i>: The parameter <i>ed2</i>
is not documented yet.

 * <i>ec</i>: A context that specifies the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more bits than the maximum precision specified in this context, the operand's most significant bits that exceed that precision are discarded. This parameter can be null.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="Radix_PeterO_Numbers_EContext"></a>
### Radix

    public static PeterO.Numbers.EFloat Radix(
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="Rescale_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Rescale

    public static PeterO.Numbers.EFloat Rescale(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat scale,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

 * <i>scale</i>: The parameter <i>scale</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="Rotate_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Rotate

    public static PeterO.Numbers.EFloat Rotate(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Rotates the digits of an arbitrary-precision binary number's mantissa.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number containing the mantissa to rotate. If this mantissa contains more bits than the precision, the most-significant bits are chopped off the mantissa.

 * <i>ed2</i>: An arbitrary-precision number indicating the number of bits to rotate the first operand's mantissa. Must be an integer with an exponent of 0. If this parameter is positive, the mantissa is shifted by the given number of bits and the most-significant bits shifted out of the mantissa become the least-significant bits instead. If this parameter is negative, the number is shifted by the given number of bits and the least-significant bits shifted out of the mantissa become the most-significant bits instead.

 * <i>ec</i>: A context that specifies the precision of arbitrary-precision numbers. If this parameter is null or specifies an unlimited precision, this method has the same behavior as  `Shift` .

<b>Return Value:</b>

An arbitrary-precision binary number whose mantissa is rotated the given number of bits. Signals an invalid operation and returns NaN (not-a-number) if "ed2" is a signaling NaN or if "ed2" is not an integer, is negative, has an exponent other than 0, or has an absolute value that exceeds the maximum precision specified in the context.

<a id="SameQuantum_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat"></a>
### SameQuantum

    public static bool SameQuantum(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EFloat ed2);

Not documented yet.

<b>Parameters:</b>

 * <i>ed1</i>: The parameter <i>ed1</i>
is not documented yet.

 * <i>ed2</i>: The parameter <i>ed2</i>
is not documented yet.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="ScaleB_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### ScaleB

    public static PeterO.Numbers.EFloat ScaleB(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

 * <i>ed2</i>: The parameter <i>ed2</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>ed</i>
or <i>ed2</i>
is null.

<a id="Shift_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Shift

    public static PeterO.Numbers.EFloat Shift(
        PeterO.Numbers.EFloat ed,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

 * <i>ed2</i>: The parameter <i>ed2</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>ed</i>
or <i>ed2</i>
is null.

<a id="Trim_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Trim

    public static PeterO.Numbers.EFloat Trim(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed1</i>: The parameter <i>ed1</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.

<a id="Xor_PeterO_Numbers_EFloat_PeterO_Numbers_EFloat_PeterO_Numbers_EContext"></a>
### Xor

    public static PeterO.Numbers.EFloat Xor(
        PeterO.Numbers.EFloat ed1,
        PeterO.Numbers.EFloat ed2,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed1</i>: The parameter <i>ed1</i>
is not documented yet.

 * <i>ed2</i>: The parameter <i>ed2</i>
is not documented yet.

 * <i>ec</i>: A context that specifies the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more bits than the maximum precision specified in this context, the operand's most significant bits that exceed that precision are discarded. This parameter can be null.

<b>Return Value:</b>

An arbitrary-precision binary floating-point number.
