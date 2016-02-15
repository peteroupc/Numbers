## PeterO.Numbers.EContext

    public sealed class EContext

Contains parameters for controlling the precision, rounding, and exponent range of arbitrary-precision numbers. (The "E" stands for "extended", and has this prefix to group it with the other classes common to this library, particularly EDecimal, EFloat, and ERational.).

### EContext Constructor

    public EContext(
        int precision,
        PeterO.Numbers.ERounding rounding,
        int exponentMinSmall,
        int exponentMaxSmall,
        bool clampNormalExponents);

Initializes a new instance of the [PeterO.Numbers.EContext](PeterO.Numbers.EContext.md) class.  `HasFlags`  will be set to false.

<b>Parameters:</b>

 * <i>precision</i>: The maximum number of digits a number can have, or 0 for an unlimited number of digits.

 * <i>rounding</i>: The rounding mode to use when a number can't fit the given precision.

 * <i>exponentMinSmall</i>: The minimum exponent.

 * <i>exponentMaxSmall</i>: The maximum exponent.

 * <i>clampNormalExponents</i>: Whether to clamp a number's significand to the given maximum precision (if it isn't zero) while remaining within the exponent range.

### Basic

    public static readonly PeterO.Numbers.EContext Basic;

A basic arithmetic context, 9 digits precision, rounding mode half-up, unlimited exponent range. The default rounding mode is HalfUp.

### BigDecimalJava

    public static readonly PeterO.Numbers.EContext BigDecimalJava;

An arithmetic context for Java's BigDecimal format. The default rounding mode is HalfUp.

### Binary128

    public static readonly PeterO.Numbers.EContext Binary128;

An arithmetic context for the IEEE-754-2008 binary128 format, 113 bits precision. The default rounding mode is HalfEven.

### Binary16

    public static readonly PeterO.Numbers.EContext Binary16;

An arithmetic context for the IEEE-754-2008 binary16 format, 11 bits precision. The default rounding mode is HalfEven.

### Binary32

    public static readonly PeterO.Numbers.EContext Binary32;

An arithmetic context for the IEEE-754-2008 binary32 format, 24 bits precision. The default rounding mode is HalfEven.

### Binary64

    public static readonly PeterO.Numbers.EContext Binary64;

An arithmetic context for the IEEE-754-2008 binary64 format, 53 bits precision. The default rounding mode is HalfEven.

### CliDecimal

    public static readonly PeterO.Numbers.EContext CliDecimal;

An arithmetic context for the .NET Framework decimal format (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)), 96 bits precision, and a valid exponent range of -28 to 0. The default rounding mode is HalfEven. (The "Cli" stands for "Common Language Infrastructure", which defined this format as the .NET Framework decimal format in version 1, but leaves it unspecified in later versions.)

### Decimal128

    public static readonly PeterO.Numbers.EContext Decimal128;

An arithmetic context for the IEEE-754-2008 decimal128 format. The default rounding mode is HalfEven.

### Decimal32

    public static readonly PeterO.Numbers.EContext Decimal32;

An arithmetic context for the IEEE-754-2008 decimal32 format. The default rounding mode is HalfEven.

### Decimal64

    public static readonly PeterO.Numbers.EContext Decimal64;

An arithmetic context for the IEEE-754-2008 decimal64 format. The default rounding mode is HalfEven.

### FlagClamped

    public static int FlagClamped = 32;

Signals that the exponent was adjusted to fit the exponent range.

### FlagDivideByZero

    public static int FlagDivideByZero = 128;

Signals a division of a nonzero number by zero.

### FlagInexact

    public static int FlagInexact = 1;

Signals that the result was rounded to a different mathematical value, but as close as possible to the original.

### FlagInvalid

    public static int FlagInvalid = 64;

Signals an invalid operation.

### FlagLostDigits

    public static int FlagLostDigits = 256;

Signals that an operand was rounded to a different mathematical value before an operation.

### FlagOverflow

    public static int FlagOverflow = 16;

Signals that the result is non-zero and the exponent is higher than the highest exponent allowed.

### FlagRounded

    public static int FlagRounded = 2;

Signals that the result was rounded to fit the precision; either the value or the exponent may have changed from the original.

### FlagSubnormal

    public static int FlagSubnormal = 4;

Signals that the result's exponent, before rounding, is lower than the lowest exponent allowed.

### FlagUnderflow

    public static int FlagUnderflow = 8;

Signals that the result's exponent, before rounding, is lower than the lowest exponent allowed, and the result was rounded to a different mathematical value, but as close as possible to the original.

### Unlimited

    public static readonly PeterO.Numbers.EContext Unlimited;

No specific (theoretical) limit on precision. Rounding mode HalfUp.

### UnlimitedHalfEven

    public static readonly PeterO.Numbers.EContext UnlimitedHalfEven;

No specific (theoretical) limit on precision. Rounding mode HalfEven.

### AdjustExponent

    public bool AdjustExponent { get; }

Gets a value indicating whether the EMax and EMin properties refer to the number's Exponent property adjusted to the number's precision, or just the number's Exponent property. The default value is true, meaning that EMax and EMin refer to the adjusted exponent. Setting this value to false (using WithAdjustExponent) is useful for modeling floating point representations with an integer mantissa (significand) and an integer exponent, such as Java's BigDecimal.

<b>Returns:</b>

 `true`  if the EMax and EMin properties refer to the number's Exponent property adjusted to the number's precision, or false if they refer to just the number's Exponent property.

### ClampNormalExponents

    public bool ClampNormalExponents { get; }

Gets a value indicating whether a converted number's Exponent property will not be higher than EMax + 1 - Precision. If a number's exponent is higher than that value, but not high enough to cause overflow, the exponent is clamped to that value and enough zeros are added to the number's mantissa (significand) to account for the adjustment. If HasExponentRange is false, this value is always false.

<b>Returns:</b>

If true, a converted number's Exponent property will not be higher than EMax + 1 - Precision.

### EMax

    public PeterO.Numbers.EInteger EMax { get; }

Gets the highest exponent possible when a converted number is expressed in scientific notation with one digit before the radix point. For example, with a precision of 3 and an EMax of 100, the maximum value possible is 9.99E + 100. (This is not the same as the highest possible Exponent property.) If HasExponentRange is false, this value will be 0.

<b>Returns:</b>

The highest exponent possible when a converted number is expressed in scientific notation with one digit before the decimal point. For example, with a precision of 3 and an EMax of 100, the maximum value possible is 9.99E + 100. (This is not the same as the highest possible Exponent property.) If HasExponentRange is false, this value will be 0.

### EMin

    public PeterO.Numbers.EInteger EMin { get; }

Gets the lowest exponent possible when a converted number is expressed in scientific notation with one digit before the radix point. For example, with a precision of 3 and an EMin of -100, the next value that comes after 0 is 0.001E-100. (If AdjustExponent is false, this property specifies the lowest possible Exponent property instead.) If HasExponentRange is false, this value will be 0.

<b>Returns:</b>

The lowest exponent possible when a converted number is expressed in scientific notation with one digit before the decimal point.

### Flags

    public int Flags { get; set;}

Gets or sets the flags that are set from converting numbers according to this arithmetic context. If  `HasFlags`  is false, this value will be 0. This value is a combination of bit fields. To retrieve a particular flag, use the AND operation on the return value of this method. For example:  `(this.Flags &
            EContext.FlagInexact) != 0`  returns  `true`  if the Inexact flag is set.

<b>Returns:</b>

The flags that are set from converting numbers according to this arithmetic context. If.  `HasFlags`  is false, this value will be 0.

### HasExponentRange

    public bool HasExponentRange { get; }

Gets a value indicating whether this context defines a minimum and maximum exponent. If false, converted exponents can have any exponent and operations can't cause overflow or underflow.

<b>Returns:</b>

 `true`  if this context defines a minimum and maximum exponent; otherwise,  `false` .

### HasFlags

    public bool HasFlags { get; }

Gets a value indicating whether this context has a mutable Flags field.

<b>Returns:</b>

 `true`  if this context has a mutable Flags field; otherwise,  `false` .

### HasMaxPrecision

    public bool HasMaxPrecision { get; }

Gets a value indicating whether this context defines a maximum precision.

<b>Returns:</b>

 `true`  if this context defines a maximum precision; otherwise,  `false` .

### IsPrecisionInBits

    public bool IsPrecisionInBits { get; }

Gets a value indicating whether this context's Precision property is in bits, rather than digits. The default is false.

<b>Returns:</b>

 `true`  if this context's Precision property is in bits, rather than digits; otherwise,  `false` . The default is false.

### IsSimplified

    public bool IsSimplified { get; }

Gets a value indicating whether to use a "simplified" arithmetic. In the simplified arithmetic, infinity, not-a-number, and subnormal numbers are not allowed, and negative zero is treated the same as positive zero. For further details, see<a href="http://speleotrove.com/decimal/dax3274.html">http://speleotrove.com/decimal/dax3274.html</a>

<b>Returns:</b>

 `true` if a "simplified" arithmetic will be used; otherwise, `false` .

### Precision

    public PeterO.Numbers.EInteger Precision { get; }

Gets the maximum length of a converted number in digits, ignoring the radix point and exponent. For example, if precision is 3, a converted number's mantissa (significand) can range from 0 to 999 (up to three digits long). If 0, converted numbers can have any precision.

<b>Returns:</b>

The maximum length of a converted number in digits, ignoring the radix point and exponent.

### Rounding

    public PeterO.Numbers.ERounding Rounding { get; }

Gets the desired rounding mode when converting numbers that can't be represented in the given precision and exponent range.

<b>Returns:</b>

The desired rounding mode when converting numbers that can't be represented in the given precision and exponent range.

### Traps

    public int Traps { get; }

Gets the traps that are set for each flag in the context. Whenever a flag is signaled, even if  `HasFlags`  is false, and the flag's trap is enabled, the operation will throw a TrapException.For example, if Traps equals  `FlagInexact`  and FlagSubnormal, a TrapException will be thrown if an operation's return value is not the same as the exact result (FlagInexact) or if the return value's exponent is lower than the lowest allowed (FlagSubnormal).

<b>Returns:</b>

The traps that are set for each flag in the context.

### Copy

    public PeterO.Numbers.EContext Copy();

Initializes a new EContext that is a copy of another EContext.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### ExponentWithinRange

    public bool ExponentWithinRange(
        PeterO.Numbers.EInteger exponent);

Determines whether a number can have the given Exponent property under this arithmetic context.

<b>Parameters:</b>

 * <i>exponent</i>: An arbitrary-precision integer indicating the desired exponent.

<b>Return Value:</b>

 `true`  if a number can have the given Exponent property under this arithmetic context; otherwise, false . If this context allows unlimited precision, returns true for the exponent EMax and any exponent less than EMax.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>exponent</i>
 is null.

### ForPrecision

    public static PeterO.Numbers.EContext ForPrecision(
        int precision);

Creates a new arithmetic context using the given maximum number of digits, an unlimited exponent range, and the HalfUp rounding mode.

<b>Parameters:</b>

 * <i>precision</i>: Maximum number of digits (precision).

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### ForPrecisionAndRounding

    public static PeterO.Numbers.EContext ForPrecisionAndRounding(
        int precision,
        PeterO.Numbers.ERounding rounding);

Creates a new EContext object initialized with an unlimited exponent range, and the given rounding mode and maximum precision.

<b>Parameters:</b>

 * <i>precision</i>: Maximum number of digits (precision).

 * <i>rounding</i>: An ERounding object.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### ForRounding

    public static PeterO.Numbers.EContext ForRounding(
        PeterO.Numbers.ERounding rounding);

Creates a new EContext object initialized with an unlimited precision, an unlimited exponent range, and the given rounding mode.

<b>Parameters:</b>

 * <i>rounding</i>: The rounding mode for the new precision context.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### ToString

    public override string ToString();

Gets a string representation of this object. Note that the format is not intended to be parsed and may change at any time.

<b>Return Value:</b>

A string representation of this object.

### WithAdjustExponent

    public PeterO.Numbers.EContext WithAdjustExponent(
        bool adjustExponent);

Copies this EContext and sets the copy's "AdjustExponent" property to the given value.

<b>Parameters:</b>

 * <i>adjustExponent</i>: The new value of the "AdjustExponent" property for the copy.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithBigExponentRange

    public PeterO.Numbers.EContext WithBigExponentRange(
        PeterO.Numbers.EInteger exponentMin,
        PeterO.Numbers.EInteger exponentMax);

Copies this arithmetic context and sets the copy's exponent range.

<b>Parameters:</b>

 * <i>exponentMin</i>: Desired minimum exponent (EMin).

 * <i>exponentMax</i>: Desired maximum exponent (EMax).

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>exponentMin</i>
 is null.

 * System.ArgumentNullException:
The parameter <i>exponentMax</i>
 is null.

### WithBigPrecision

    public PeterO.Numbers.EContext WithBigPrecision(
        PeterO.Numbers.EInteger bigintPrecision);

Copies this EContext and gives it a particular precision value.

<b>Parameters:</b>

 * <i>bigintPrecision</i>: The parameter  <i>bigintPrecision</i>
 is not documented yet.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter <i>bigintPrecision</i>
 is null.

### WithBlankFlags

    public PeterO.Numbers.EContext WithBlankFlags();

Copies this EContext with  `HasFlags`  set to true and a Flags value of 0.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithExponentClamp

    public PeterO.Numbers.EContext WithExponentClamp(
        bool clamp);

Copies this arithmetic context and sets the copy's "ClampNormalExponents" flag to the given value.

<b>Parameters:</b>

 * <i>clamp</i>: The parameter  <i>clamp</i>
 is not documented yet.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithExponentRange

    public PeterO.Numbers.EContext WithExponentRange(
        int exponentMinSmall,
        int exponentMaxSmall);

Copies this arithmetic context and sets the copy's exponent range.

<b>Parameters:</b>

 * <i>exponentMinSmall</i>: Desired minimum exponent (EMin).

 * <i>exponentMaxSmall</i>: Desired maximum exponent (EMax).

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithNoFlags

    public PeterO.Numbers.EContext WithNoFlags();

Copies this EContext with  `HasFlags`  set to false and a Flags value of 0.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithPrecision

    public PeterO.Numbers.EContext WithPrecision(
        int precision);

Copies this EContext and gives it a particular precision value.

<b>Parameters:</b>

 * <i>precision</i>: Desired precision. 0 means unlimited precision.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithPrecisionInBits

    public PeterO.Numbers.EContext WithPrecisionInBits(
        bool isPrecisionBits);

Copies this EContext and sets the copy's "IsPrecisionInBits" property to the given value.

<b>Parameters:</b>

 * <i>isPrecisionBits</i>: The new value of the "IsPrecisionInBits" property for the copy.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithRounding

    public PeterO.Numbers.EContext WithRounding(
        PeterO.Numbers.ERounding rounding);

Copies this EContext with the specified rounding mode.

<b>Parameters:</b>

 * <i>rounding</i>: Desired value of the Rounding property.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithSimplified

    public PeterO.Numbers.EContext WithSimplified(
        bool simplified);

Copies this EContext and sets the copy's "IsSimplified" property to the given value.

<b>Parameters:</b>

 * <i>simplified</i>: The parameter  <i>simplified</i>
 is not documented yet.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithTraps

    public PeterO.Numbers.EContext WithTraps(
        int traps);

Copies this EContext with Traps set to the given value.

<b>Parameters:</b>

 * <i>traps</i>: Flags representing the traps to enable. See the property "Traps".

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

### WithUnlimitedExponents

    public PeterO.Numbers.EContext WithUnlimitedExponents();

Copies this EContext with an unlimited exponent range.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.
