## PeterO.Numbers.EDecimalExtras

    public static class EDecimalExtras

A class that implements additional operations on arbitrary-precision decimal numbers. Many of them are listed as miscellaneous operations in the General Decimal Arithmetic Specification version 1.70.

### Member Summary
* <code>[And(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#And_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Performs a logical AND operation on two decimal numbers in the form oflogical operands.
* <code>[BoolToEDecimal(bool, PeterO.Numbers.EContext)](#BoolToEDecimal_bool_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Canonical(PeterO.Numbers.EDecimal)](#Canonical_PeterO_Numbers_EDecimal)</code> - Returns a canonical version of the given arbitrary-precision number object.
* <code>[CompareTotalMagnitude(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#CompareTotalMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[CompareTotal(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#CompareTotal_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Compares the values of one arbitrary-precision number object and another object, imposing a total ordering on all possible values.
* <code>[CopyAbs(PeterO.Numbers.EDecimal)](#CopyAbs_PeterO_Numbers_EDecimal)</code> - Not documented yet.
* <code>[CopyNegate(PeterO.Numbers.EDecimal)](#CopyNegate_PeterO_Numbers_EDecimal)</code> - Not documented yet.
* <code>[CopySign(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#CopySign_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal)</code> - Not documented yet.
* <code>[Copy(PeterO.Numbers.EDecimal)](#Copy_PeterO_Numbers_EDecimal)</code> - Not documented yet.
* <code>[Int32ToEDecimal(int, PeterO.Numbers.EContext)](#Int32ToEDecimal_int_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Invert(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Invert_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[IsCanonical(PeterO.Numbers.EDecimal)](#IsCanonical_PeterO_Numbers_EDecimal)</code> - Not documented yet.
* <code>[IsFinite(PeterO.Numbers.EDecimal)](#IsFinite_PeterO_Numbers_EDecimal)</code> - Not documented yet.
* <code>[IsInfinite(PeterO.Numbers.EDecimal)](#IsInfinite_PeterO_Numbers_EDecimal)</code> - Not documented yet.
* <code>[IsNaN(PeterO.Numbers.EDecimal)](#IsNaN_PeterO_Numbers_EDecimal)</code> - Returns whether the given arbitrary-precision number object is a not-a-number (NaN).
* <code>[IsNormal(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#IsNormal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Returns whether the given number is anormalnumber.
* <code>[IsQuietNaN(PeterO.Numbers.EDecimal)](#IsQuietNaN_PeterO_Numbers_EDecimal)</code> - Returns whether the given arbitrary-precision number object is a quiet not-a-number (NaN).
* <code>[IsSignalingNaN(PeterO.Numbers.EDecimal)](#IsSignalingNaN_PeterO_Numbers_EDecimal)</code> - Returns whether the given arbitrary-precision number object is a signaling not-a-number (NaN).
* <code>[IsSigned(PeterO.Numbers.EDecimal)](#IsSigned_PeterO_Numbers_EDecimal)</code> - Returns whether the given arbitrary-precision number object is negative (including negative infinity, negative not-a-number [NaN], or negative zero).
* <code>[IsSubnormal(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#IsSubnormal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Returns whether the given number is asubnormalnumber.
* <code>[IsZero(PeterO.Numbers.EDecimal)](#IsZero_PeterO_Numbers_EDecimal)</code> - Not documented yet.
* <code>[LogB(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#LogB_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[NumberClassString(int)](#NumberClassString_int)</code> - Converts a number class identifier (ranging from 1 to 9) to a text string.
* <code>[NumberClass(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#NumberClass_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Finds the number class for an arbitrary-precision decimal number object.
* <code>[Or(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Or_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Performs a logical OR operation on two decimal numbers in the form oflogical operands.
* <code>[Radix(PeterO.Numbers.EContext)](#Radix_PeterO_Numbers_EContext)</code> - Returns the number 10, the decimal radix.
* <code>[Rescale(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Rescale_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Rotate(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Rotate_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Rotates the digits of an arbitrary-precision decimal number's mantissa.
* <code>[SameQuantum(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal)](#SameQuantum_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal)</code> - Not documented yet.
* <code>[ScaleB(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#ScaleB_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Shift(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Shift_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Trim(PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Trim_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Not documented yet.
* <code>[Xor(PeterO.Numbers.EDecimal, PeterO.Numbers.EDecimal, PeterO.Numbers.EContext)](#Xor_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext)</code> - Performs a logical exclusive-OR (XOR) operation on two decimal numbers in the form oflogical operands.

<a id="And_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### And

    public static PeterO.Numbers.EDecimal And(
        PeterO.Numbers.EDecimal ed1,
        PeterO.Numbers.EDecimal ed2,
        PeterO.Numbers.EContext ec);

Performs a logical AND operation on two decimal numbers in the form of<i>logical operands</i>. A  `logical operand`  is a non-negative base-10 number with an exponent of 0 and no other base-10 digits than 0 or 1 (examples include  `01001`  and `111001` , but not  `02001`  or  `99999`  ). The logical AND operation sets each digit of the result to 1 if the corresponding digits of each logical operand are both 1, and to 0 otherwise. For example,  `01001 AND 111010 = 01000`

<b>Parameters:</b>

 * <i>ed1</i>: The first logical operand to the logical AND operation.

 * <i>ed2</i>: The second logical operand to the logical AND operation.

 * <i>ec</i>: The parameter  <i>ec</i>
 is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<a id="BoolToEDecimal_bool_PeterO_Numbers_EContext"></a>
### BoolToEDecimal

    public static PeterO.Numbers.EDecimal BoolToEDecimal(
        bool b,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>b</i>: The parameter <i>b</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<a id="Canonical_PeterO_Numbers_EDecimal"></a>
### Canonical

    public static PeterO.Numbers.EDecimal Canonical(
        PeterO.Numbers.EDecimal ed);

Returns a canonical version of the given arbitrary-precision number object. In this method, this is the same as that object.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

The parameter "ed".

<a id="CompareTotal_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### CompareTotal

    public static int CompareTotal(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EDecimal other,
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

<a id="CompareTotalMagnitude_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### CompareTotalMagnitude

    public static int CompareTotalMagnitude(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EDecimal other,
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

<a id="Copy_PeterO_Numbers_EDecimal"></a>
### Copy

    public static PeterO.Numbers.EDecimal Copy(
        PeterO.Numbers.EDecimal ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<a id="CopyAbs_PeterO_Numbers_EDecimal"></a>
### CopyAbs

    public static PeterO.Numbers.EDecimal CopyAbs(
        PeterO.Numbers.EDecimal ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<a id="CopyNegate_PeterO_Numbers_EDecimal"></a>
### CopyNegate

    public static PeterO.Numbers.EDecimal CopyNegate(
        PeterO.Numbers.EDecimal ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<a id="CopySign_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal"></a>
### CopySign

    public static PeterO.Numbers.EDecimal CopySign(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EDecimal other);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

 * <i>other</i>: The parameter <i>other</i>
is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<a id="Int32ToEDecimal_int_PeterO_Numbers_EContext"></a>
### Int32ToEDecimal

    public static PeterO.Numbers.EDecimal Int32ToEDecimal(
        int i32,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>i32</i>: The parameter <i>i32</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<a id="Invert_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Invert

    public static PeterO.Numbers.EDecimal Invert(
        PeterO.Numbers.EDecimal ed1,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed1</i>: The parameter <i>ed1</i>
is not documented yet.

 * <i>ec</i>: A context that specifies the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more digits than the maximum precision specified in this context, the operand's most significant digits that exceed that precision are discarded. This parameter cannot be null and must specify a maximum precision (unlimited precision contexts are not allowed).

<b>Return Value:</b>

An EDecimal object.

<a id="IsCanonical_PeterO_Numbers_EDecimal"></a>
### IsCanonical

    public static bool IsCanonical(
        PeterO.Numbers.EDecimal ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

Always `true`  .

<a id="IsFinite_PeterO_Numbers_EDecimal"></a>
### IsFinite

    public static bool IsFinite(
        PeterO.Numbers.EDecimal ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsInfinite_PeterO_Numbers_EDecimal"></a>
### IsInfinite

    public static bool IsInfinite(
        PeterO.Numbers.EDecimal ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsNaN_PeterO_Numbers_EDecimal"></a>
### IsNaN

    public static bool IsNaN(
        PeterO.Numbers.EDecimal ed);

Returns whether the given arbitrary-precision number object is a not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsNormal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### IsNormal

    public static bool IsNormal(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EContext ec);

Returns whether the given number is a<i>normal</i>number. A<i>subnormal number</i>is a nonzero finite number whose Exponent property (or the number's xponent in scientific notation) is less than the minimum possible xponent for that number. A<i>normal number</i>is nonzero and finite, but not subnormal.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

 * <i>ec</i>: A context specifying the exponent range of arbitrary-precision numbers. Can be null. If AdjustExponent of the given context is `true`  , a nonzero number is normal if the number's exponent in scientific notation is at least the given context's EMax property (e.g., if EMax is -100, 2.3456 * 10<sup>-99</sup>is normal, but 2.3456 * 10<sup>-102</sup>is not). If AdjustExponent of the given context is `false`  , a nonzero number is subnormal if the number's Exponent property is at least given context's EMax property (e.g., if EMax is -100, 23456 * 10<sup>-99</sup>is normal, but 23456 * 10<sup>-102</sup>is not).

<b>Return Value:</b>

Either `true`  if the given number is subnormal, or `false`  otherwise. Returns `true`  if the given context is null or HasExponentRange of the given context is `false`  .

<a id="IsQuietNaN_PeterO_Numbers_EDecimal"></a>
### IsQuietNaN

    public static bool IsQuietNaN(
        PeterO.Numbers.EDecimal ed);

Returns whether the given arbitrary-precision number object is a quiet not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsSignalingNaN_PeterO_Numbers_EDecimal"></a>
### IsSignalingNaN

    public static bool IsSignalingNaN(
        PeterO.Numbers.EDecimal ed);

Returns whether the given arbitrary-precision number object is a signaling not-a-number (NaN).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsSigned_PeterO_Numbers_EDecimal"></a>
### IsSigned

    public static bool IsSigned(
        PeterO.Numbers.EDecimal ed);

Returns whether the given arbitrary-precision number object is negative (including negative infinity, negative not-a-number [NaN], or negative zero).

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="IsSubnormal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### IsSubnormal

    public static bool IsSubnormal(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EContext ec);

Returns whether the given number is a<i>subnormal</i>number. A<i>subnormal number</i>is a nonzero finite number whose Exponent property (or the number's xponent in scientific notation) is less than the minimum possible xponent for that number.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number object.

 * <i>ec</i>: A context specifying the exponent range of arbitrary-precision numbers. Can be null. If AdjustExponent of the given context is `true`  , a nonzero number is subnormal if the number's exponent in scientific notation is less than the given context's EMax property (e.g., if EMax is -100, 2.3456 * 10<sup>-102</sup>is subnormal, but 2.3456 * 10<sup>-99</sup>is not). If AdjustExponent of the given context is `false`  , a nonzero number is subnormal if the number's Exponent property is less than the given context's EMax property (e.g., if EMax is -100, 23456 * 10<sup>-102</sup>is subnormal, but 23456 * 10<sup>-99</sup>is not).

<b>Return Value:</b>

Either `true`  if the given number is subnormal, or `false`  otherwise. Returns `false`  if the given context is null or HasExponentRange of the given context is `false`  .

<a id="IsZero_PeterO_Numbers_EDecimal"></a>
### IsZero

    public static bool IsZero(
        PeterO.Numbers.EDecimal ed);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter <i>ed</i>
is not documented yet.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="LogB_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### LogB

    public static PeterO.Numbers.EDecimal LogB(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed</i>: The parameter  <i>ed</i>
 is not documented yet.

 * <i>ec</i>: The parameter  <i>ec</i>
 is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>ed</i>
 is null.

<a id="NumberClass_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### NumberClass

    public static int NumberClass(
        PeterO.Numbers.EDecimal ed,
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

<a id="Or_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Or

    public static PeterO.Numbers.EDecimal Or(
        PeterO.Numbers.EDecimal ed1,
        PeterO.Numbers.EDecimal ed2,
        PeterO.Numbers.EContext ec);

Performs a logical OR operation on two decimal numbers in the form of<i>logical operands</i>. A  `logical operand`  is a non-negative base-10 number with an exponent of 0 and no other base-10 digits than 0 or 1 (examples include  `01001`  and `111001` , but not  `02001`  or  `99999`  ). The logical OR operation sets each digit of the result to 1 if either or both of the corresponding digits of the logical operands are 1, and to 0 otherwise. For example,  `01001 OR 111010 = 111011`

<b>Parameters:</b>

 * <i>ed1</i>: The first logical operand to the logical OR operation.

 * <i>ed2</i>: The second logical operand to the logical OR operation.

 * <i>ec</i>: The parameter  <i>ec</i>
 is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<a id="Radix_PeterO_Numbers_EContext"></a>
### Radix

    public static PeterO.Numbers.EDecimal Radix(
        PeterO.Numbers.EContext ec);

Returns the number 10, the decimal radix.

<b>Parameters:</b>

 * <i>ec</i>: Specifies a precision context for rounding the number 10. Can be null.

<b>Return Value:</b>

The number 10, rounded as given in the precision context.

<a id="Rescale_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Rescale

    public static PeterO.Numbers.EDecimal Rescale(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EDecimal scale,
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

An EDecimal object.

<a id="Rotate_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Rotate

    public static PeterO.Numbers.EDecimal Rotate(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EDecimal ed2,
        PeterO.Numbers.EContext ec);

Rotates the digits of an arbitrary-precision decimal number's mantissa.

<b>Parameters:</b>

 * <i>ed</i>: An arbitrary-precision number containing the mantissa to rotate. If this mantissa contains more digits than the precision, the most-significant digits are chopped off the mantissa.

 * <i>ed2</i>: An arbitrary-precision number indicating the number of digits to rotate the first operand's mantissa. Must be an integer with an exponent of 0. If this parameter is positive, the mantissa is shifted by the given number of digits and the most-significant digits shifted out of the mantissa become the least-significant digits instead. If this parameter is negative, the number is shifted by the given number of digits and the least-significant digits shifted out of the mantissa become the most-significant digits instead.

 * <i>ec</i>: A context that specifies the precision of arbitrary-precision numbers. If this parameter is null or specifies an unlimited precision, this method has the same behavior as  `Shift` .

<b>Return Value:</b>

An arbitrary-precision decimal number whose mantissa is rotated the given number of bits. Signals an invalid operation and returns NaN (not-a-number) if "ed2" is a signaling NaN or if "ed2" is not an integer, is negative, has an exponent other than 0, or has an absolute value that exceeds the maximum precision specified in the context.

<a id="SameQuantum_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal"></a>
### SameQuantum

    public static bool SameQuantum(
        PeterO.Numbers.EDecimal ed1,
        PeterO.Numbers.EDecimal ed2);

Not documented yet.

<b>Parameters:</b>

 * <i>ed1</i>: The parameter <i>ed1</i>
is not documented yet.

 * <i>ed2</i>: The parameter <i>ed2</i>
is not documented yet.

<b>Return Value:</b>

Either `true`  or `false`  .

<a id="ScaleB_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### ScaleB

    public static PeterO.Numbers.EDecimal ScaleB(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EDecimal ed2,
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

An EDecimal object.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>ed</i>
or <i>ed2</i>
is null.

<a id="Shift_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Shift

    public static PeterO.Numbers.EDecimal Shift(
        PeterO.Numbers.EDecimal ed,
        PeterO.Numbers.EDecimal ed2,
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

An EDecimal object.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>ed</i>
or <i>ed2</i>
is null.

<a id="Trim_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Trim

    public static PeterO.Numbers.EDecimal Trim(
        PeterO.Numbers.EDecimal ed1,
        PeterO.Numbers.EContext ec);

Not documented yet.

<b>Parameters:</b>

 * <i>ed1</i>: The parameter <i>ed1</i>
is not documented yet.

 * <i>ec</i>: The parameter <i>ec</i>
is not documented yet.

<b>Return Value:</b>

An EDecimal object.

<a id="Xor_PeterO_Numbers_EDecimal_PeterO_Numbers_EDecimal_PeterO_Numbers_EContext"></a>
### Xor

    public static PeterO.Numbers.EDecimal Xor(
        PeterO.Numbers.EDecimal ed1,
        PeterO.Numbers.EDecimal ed2,
        PeterO.Numbers.EContext ec);

Performs a logical exclusive-OR (XOR) operation on two decimal numbers in the form of<i>logical operands</i>. A `logical operand`  is a non-negative base-10 number with an exponent of 0 and no other base-10 digits than 0 or 1 (examples include `01001`  and `111001`  , but not `02001`  or `99999`  ). The logical exclusive-OR operation sets each digit of the result to 1 if either corresponding digit of the logical operands, but not both, are 1, and to 0 otherwise. For example, `01001 OR 111010 = 101010`  .

<b>Parameters:</b>

 * <i>ed1</i>: The first logical operand to the logical exclusive-OR operation.

 * <i>ed2</i>: The second logical operand to the logical exclusive-OR operation.

 * <i>ec</i>: A context that specifies the maximum precision of arbitrary-precision numbers. If a logical operand passed to this method has more digits than the maximum precision specified in this context, the operand's most significant digits that exceed that precision are discarded. This parameter can be null.

<b>Return Value:</b>

An EDecimal object.
