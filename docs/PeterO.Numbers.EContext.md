## PeterO.Numbers.EContext

    public sealed class EContext

 Contains parameters for controlling the precision, rounding, and exponent range of arbitrary-precision numbers. (The "E" stands for "extended", and has this prefix to group it with the other classes common to this library, particularly EDecimal, EFloat, and ERational.). <b>Thread safety:</b> With one exception, instances of this class are immutable and are safe to use among multiple threads. The one exception involves the  `Flags`  property. If the context's  `HasFlags`  property (a read-only property) is  `true` , the  `Flags`  property is mutable, thus making the context mutable. This class doesn't synchronize access to such mutable contexts, so applications should provide their own synchronization if a context with the  `HasFlags`  property set to  `true`  will be shared among multiple threads and at least one of those threads needs to write the  `Flags`  property (which can happen, for example, by passing the context to most methods of  `EDecimal`  such as  `Add`  ).

### Member Summary
* <code>[AdjustExponent](#AdjustExponent)</code> - Gets a value indicating whether the EMax and EMin properties refer to the number's Exponent property adjusted to the number's precision, or just the number's Exponent property.
* <code>[public static readonly PeterO.Numbers.EContext Basic;](#Basic)</code> - A basic arithmetic context, 9 digits precision, rounding mode half-up, unlimited exponent range.
* <code>[public static readonly PeterO.Numbers.EContext BigDecimalJava;](#BigDecimalJava)</code> - An arithmetic context for Java's BigDecimal format.
* <code>[public static readonly PeterO.Numbers.EContext Binary128;](#Binary128)</code> - An arithmetic context for the IEEE-754-2008 binary128 format, 113 bits precision.
* <code>[public static readonly PeterO.Numbers.EContext Binary16;](#Binary16)</code> - An arithmetic context for the IEEE-754-2008 binary16 format, 11 bits precision.
* <code>[public static readonly PeterO.Numbers.EContext Binary32;](#Binary32)</code> - An arithmetic context for the IEEE-754-2008 binary32 format, 24 bits precision.
* <code>[public static readonly PeterO.Numbers.EContext Binary64;](#Binary64)</code> - An arithmetic context for the IEEE-754-2008 binary64 format, 53 bits precision.
* <code>[ClampNormalExponents](#ClampNormalExponents)</code> - Gets a value indicating whether a converted number's Exponent property will not be higher than EMax + 1 - Precision.
* <code>[public static readonly PeterO.Numbers.EContext CliDecimal;](#CliDecimal)</code> - An arithmetic context for the.
* <code>[Copy()](#Copy)</code> - Initializes a new EContext that is a copy of another EContext.
* <code>[public static readonly PeterO.Numbers.EContext Decimal128;](#Decimal128)</code> - An arithmetic context for the IEEE-754-2008 decimal128 format.
* <code>[public static readonly PeterO.Numbers.EContext Decimal32;](#Decimal32)</code> - An arithmetic context for the IEEE-754-2008 decimal32 format.
* <code>[public static readonly PeterO.Numbers.EContext Decimal64;](#Decimal64)</code> - An arithmetic context for the IEEE-754-2008 decimal64 format.
* <code>[EMax](#EMax)</code> - Gets the highest exponent possible when a converted number is expressed in scientific notation with one nonzero digit before the radix point.
* <code>[EMin](#EMin)</code> - Gets the lowest exponent possible when a converted number is expressed in scientific notation with one nonzero digit before the radix point.
* <code>[ExponentWithinRange(PeterO.Numbers.EInteger)](#ExponentWithinRange_PeterO_Numbers_EInteger)</code> - Determines whether a number can have the given Exponent property under this arithmetic context.
* <code>[public static int FlagClamped = 32;](#FlagClamped)</code> - Signals that the exponent was adjusted to fit the exponent range.
* <code>[public static int FlagDivideByZero = 128;](#FlagDivideByZero)</code> - Signals a division of a nonzero number by zero.
* <code>[public static int FlagInexact = 1;](#FlagInexact)</code> - Signals that the result was rounded to a different mathematical value, but as close as possible to the original.
* <code>[public static int FlagInvalid = 64;](#FlagInvalid)</code> - Signals an invalid operation.
* <code>[public static int FlagLostDigits = 256;](#FlagLostDigits)</code> - Signals that an operand was rounded to a different mathematical value before an operation.
* <code>[public static int FlagOverflow = 16;](#FlagOverflow)</code> - Signals that the result is non-zero and the exponent is higher than the highest exponent allowed.
* <code>[public static int FlagRounded = 2;](#FlagRounded)</code> - Signals that the result was rounded to fit the precision; either the value or the exponent may have changed from the original.
* <code>[Flags](#Flags)</code> - Gets or sets the flags that are set from converting numbers according to this arithmetic context.
* <code>[public static int FlagSubnormal = 4;](#FlagSubnormal)</code> - Signals that the result's exponent, before rounding, is lower than the lowest exponent allowed.
* <code>[public static int FlagUnderflow = 8;](#FlagUnderflow)</code> - Signals that the result's exponent, before rounding, is lower than the lowest exponent allowed, and the result was rounded to a different mathematical value, but as close as possible to the original.
* <code>[ForPrecision(int)](#ForPrecision_int)</code> - Creates a new arithmetic context using the given maximum number of digits, an unlimited exponent range, and the HalfUp rounding mode.
* <code>[ForPrecisionAndRounding(int, PeterO.Numbers.ERounding)](#ForPrecisionAndRounding_int_PeterO_Numbers_ERounding)</code> - Creates a new EContext object initialized with an unlimited exponent range, and the given rounding mode and maximum precision.
* <code>[ForRounding(PeterO.Numbers.ERounding)](#ForRounding_PeterO_Numbers_ERounding)</code> - Creates a new EContext object initialized with an unlimited precision, an unlimited exponent range, and the given rounding mode.
* <code>[HasExponentRange](#HasExponentRange)</code> - Gets a value indicating whether this context defines a minimum and maximum exponent.
* <code>[HasFlags](#HasFlags)</code> - Gets a value indicating whether this context has a mutable Flags field.
* <code>[HasFlagsOrTraps](#HasFlagsOrTraps)</code> - Gets a value indicating whether this context has a mutable Flags field, one or more trap enablers, or both.
* <code>[HasMaxPrecision](#HasMaxPrecision)</code> - Gets a value indicating whether this context defines a maximum precision.
* <code>[IsPrecisionInBits](#IsPrecisionInBits)</code> - Gets a value indicating whether this context's Precision property is in bits, rather than digits.
* <code>[IsSimplified](#IsSimplified)</code> - Gets a value indicating whether to use a "simplified" arithmetic.
* <code>[Precision](#Precision)</code> - Gets the maximum length of a converted number in digits, ignoring the radix point and exponent.
* <code>[Rounding](#Rounding)</code> - Gets the desired rounding mode when converting numbers that can't be represented in the given precision and exponent range.
* <code>[ToString()](#ToString)</code> - Gets a string representation of this object.
* <code>[Traps](#Traps)</code> - Gets the traps that are set for each flag in the context.
* <code>[public static readonly PeterO.Numbers.EContext Unlimited;](#Unlimited)</code> - No specific (theoretical) limit on precision.
* <code>[public static readonly PeterO.Numbers.EContext UnlimitedHalfEven;](#UnlimitedHalfEven)</code> - No specific (theoretical) limit on precision.
* <code>[WithAdjustExponent(bool)](#WithAdjustExponent_bool)</code> - Copies this EContext and sets the copy's "AdjustExponent" property to the given value.
* <code>[WithBigExponentRange(PeterO.Numbers.EInteger, PeterO.Numbers.EInteger)](#WithBigExponentRange_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger)</code> - Copies this arithmetic context and sets the copy's exponent range.
* <code>[WithBigPrecision(PeterO.Numbers.EInteger)](#WithBigPrecision_PeterO_Numbers_EInteger)</code> - Copies this EContext and gives it a particular precision value.
* <code>[WithBlankFlags()](#WithBlankFlags)</code> - Copies this EContext with HasFlags set to true and a Flags value of 0.
* <code>[WithExponentClamp(bool)](#WithExponentClamp_bool)</code> - Copies this arithmetic context and sets the copy's "ClampNormalExponents" flag to the given value.
* <code>[WithExponentRange(int, int)](#WithExponentRange_int_int)</code> - Copies this arithmetic context and sets the copy's exponent range.
* <code>[WithNoFlags()](#WithNoFlags)</code> - Copies this EContext with HasFlags set to false and a Flags value of 0.
* <code>[WithNoFlagsOrTraps()](#WithNoFlagsOrTraps)</code> - Copies this EContext with HasFlags set to false, a Traps value of 0, and a Flags value of 0.
* <code>[WithPrecision(int)](#WithPrecision_int)</code> - Copies this EContext and gives it a particular precision value.
* <code>[WithPrecisionInBits(bool)](#WithPrecisionInBits_bool)</code> - Copies this EContext and sets the copy's "IsPrecisionInBits" property to the given value.
* <code>[WithRounding(PeterO.Numbers.ERounding)](#WithRounding_PeterO_Numbers_ERounding)</code> - Copies this EContext with the specified rounding mode.
* <code>[WithSimplified(bool)](#WithSimplified_bool)</code> - Copies this EContext and sets the copy's "IsSimplified" property to the given value.
* <code>[WithTraps(int)](#WithTraps_int)</code> - Copies this EContext with Traps set to the given value.
* <code>[WithUnlimitedExponents()](#WithUnlimitedExponents)</code> - Copies this EContext with an unlimited exponent range.

<a id="Void_ctor_Int32_PeterO_Numbers_ERounding_Int32_Int32_Boolean"></a>
### EContext Constructor

    public EContext(
        int precision,
        PeterO.Numbers.ERounding rounding,
        int exponentMinSmall,
        int exponentMaxSmall,
        bool clampNormalExponents);

 Initializes a new instance of the [PeterO.Numbers.EContext](PeterO.Numbers.EContext.md).

       <b>Parameters:</b>

 * <i>precision</i>: A 32-bit signed integer.

 * <i>rounding</i>: An ERounding object.

 * <i>exponentMinSmall</i>: Another 32-bit signed integer.

 * <i>exponentMaxSmall</i>: A 32-bit signed integer. (3).

 * <i>clampNormalExponents</i>: A Boolean object.

<a id="Void_ctor_PeterO_Numbers_EInteger_PeterO_Numbers_ERounding_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger_Boolean"></a>
### EContext Constructor

    public EContext(
        PeterO.Numbers.EInteger bigintPrecision,
        PeterO.Numbers.ERounding rounding,
        PeterO.Numbers.EInteger exponentMin,
        PeterO.Numbers.EInteger exponentMax,
        bool clampNormalExponents);

 Initializes a new instance of the [PeterO.Numbers.EContext](PeterO.Numbers.EContext.md) class,.

       <b>Parameters:</b>

 * <i>bigintPrecision</i>: An EInteger object.

 * <i>rounding</i>: An ERounding object.

 * <i>exponentMin</i>: An EInteger object.

 * <i>exponentMax</i>: An EInteger object. (3).

 * <i>clampNormalExponents</i>: A Boolean object.

<a id="Basic"></a>
### Basic

    public static readonly PeterO.Numbers.EContext Basic;

 A basic arithmetic context, 9 digits precision, rounding mode half-up, unlimited exponent range. The default rounding mode is HalfUp.

  <a id="BigDecimalJava"></a>
### BigDecimalJava

    public static readonly PeterO.Numbers.EContext BigDecimalJava;

 An arithmetic context for Java's BigDecimal format. The default rounding mode is HalfUp.

  <a id="Binary128"></a>
### Binary128

    public static readonly PeterO.Numbers.EContext Binary128;

 An arithmetic context for the IEEE-754-2008 binary128 format, 113 bits precision. The default rounding mode is HalfEven.

  <a id="Binary16"></a>
### Binary16

    public static readonly PeterO.Numbers.EContext Binary16;

 An arithmetic context for the IEEE-754-2008 binary16 format, 11 bits precision. The default rounding mode is HalfEven.

  <a id="Binary32"></a>
### Binary32

    public static readonly PeterO.Numbers.EContext Binary32;

 An arithmetic context for the IEEE-754-2008 binary32 format, 24 bits precision. The default rounding mode is HalfEven.

  <a id="Binary64"></a>
### Binary64

    public static readonly PeterO.Numbers.EContext Binary64;

 An arithmetic context for the IEEE-754-2008 binary64 format, 53 bits precision. The default rounding mode is HalfEven.

  <a id="CliDecimal"></a>
### CliDecimal

    public static readonly PeterO.Numbers.EContext CliDecimal;

 An arithmetic context for the.NET Framework decimal format (see [&#x22;Forms of numbers&#x22;](PeterO.Numbers.EDecimal.md)"Forms of numbers" ), 96 bits precision, and a valid exponent range of -28 to 0. The default rounding mode is HalfEven. (The  `"Cli"`  stands for "Common Language Infrastructure", which defined this format as the .NET Framework decimal format in version 1, but leaves it unspecified in later versions.).

  <a id="Decimal128"></a>
### Decimal128

    public static readonly PeterO.Numbers.EContext Decimal128;

 An arithmetic context for the IEEE-754-2008 decimal128 format. The default rounding mode is HalfEven.

  <a id="Decimal32"></a>
### Decimal32

    public static readonly PeterO.Numbers.EContext Decimal32;

 An arithmetic context for the IEEE-754-2008 decimal32 format. The default rounding mode is HalfEven.

  <a id="Decimal64"></a>
### Decimal64

    public static readonly PeterO.Numbers.EContext Decimal64;

 An arithmetic context for the IEEE-754-2008 decimal64 format. The default rounding mode is HalfEven.

  <a id="FlagClamped"></a>
### FlagClamped

    public static int FlagClamped = 32;

 Signals that the exponent was adjusted to fit the exponent range.

  <a id="FlagDivideByZero"></a>
### FlagDivideByZero

    public static int FlagDivideByZero = 128;

 Signals a division of a nonzero number by zero.

  <a id="FlagInexact"></a>
### FlagInexact

    public static int FlagInexact = 1;

 Signals that the result was rounded to a different mathematical value, but as close as possible to the original.

  <a id="FlagInvalid"></a>
### FlagInvalid

    public static int FlagInvalid = 64;

 Signals an invalid operation.

  <a id="FlagLostDigits"></a>
### FlagLostDigits

    public static int FlagLostDigits = 256;

 Signals that an operand was rounded to a different mathematical value before an operation.

  <a id="FlagOverflow"></a>
### FlagOverflow

    public static int FlagOverflow = 16;

 Signals that the result is non-zero and the exponent is higher than the highest exponent allowed.

  <a id="FlagRounded"></a>
### FlagRounded

    public static int FlagRounded = 2;

 Signals that the result was rounded to fit the precision; either the value or the exponent may have changed from the original.

  <a id="FlagSubnormal"></a>
### FlagSubnormal

    public static int FlagSubnormal = 4;

 Signals that the result's exponent, before rounding, is lower than the lowest exponent allowed.

  <a id="FlagUnderflow"></a>
### FlagUnderflow

    public static int FlagUnderflow = 8;

 Signals that the result's exponent, before rounding, is lower than the lowest exponent allowed, and the result was rounded to a different mathematical value, but as close as possible to the original.

  <a id="Unlimited"></a>
### Unlimited

    public static readonly PeterO.Numbers.EContext Unlimited;

 No specific (theoretical) limit on precision. Rounding mode HalfUp.

  <a id="UnlimitedHalfEven"></a>
### UnlimitedHalfEven

    public static readonly PeterO.Numbers.EContext UnlimitedHalfEven;

 No specific (theoretical) limit on precision. Rounding mode HalfEven.

  <a id="AdjustExponent"></a>
### AdjustExponent

    public bool AdjustExponent { get; }

 Gets a value indicating whether the EMax and EMin properties refer to the number's Exponent property adjusted to the number's precision, or just the number's Exponent property. The default value is true, meaning that EMax and EMin refer to the adjusted exponent. Setting this value to false (using WithAdjustExponent) is useful for modeling floating point representations with an integer mantissa (significand) and an integer exponent, such as Java's BigDecimal.

   <b>Returns:</b>

 `true`  if the EMax and EMin properties refer to the number's Exponent property adjusted to the number's precision, or false if they refer to just the number's Exponent property.

<a id="ClampNormalExponents"></a>
### ClampNormalExponents

    public bool ClampNormalExponents { get; }

 Gets a value indicating whether a converted number's Exponent property will not be higher than EMax + 1 - Precision. If a number's exponent is higher than that value, but not high enough to cause overflow, the exponent is clamped to that value and enough zeros are added to the number's mantissa (significand) to account for the adjustment. If HasExponentRange is false, this value is always false.

   <b>Returns:</b>

If true, a converted number's Exponent property will not be higher than EMax + 1 - Precision.

<a id="EMax"></a>
### EMax

    public PeterO.Numbers.EInteger EMax { get; }

 Gets the highest exponent possible when a converted number is expressed in scientific notation with one nonzero digit before the radix point. For example, with a precision of 3 and an EMax of 100, the maximum value possible is 9.99E + 100. (This is not the same as the highest possible Exponent property.) If HasExponentRange is false, this value will be 0.

   <b>Returns:</b>

The highest exponent possible when a converted number is expressed in scientific notation with one nonzero digit before the radix point. For example, with a precision of 3 and an EMax of 100, the maximum value possible is 9.99E + 100. (This is not the same as the highest possible Exponent property.) If HasExponentRange is false, this value will be 0.

<a id="EMin"></a>
### EMin

    public PeterO.Numbers.EInteger EMin { get; }

 Gets the lowest exponent possible when a converted number is expressed in scientific notation with one nonzero digit before the radix point. For example, with a precision of 3 and an EMin of -100, the next value that comes after 0 is 0.001E-100. (If AdjustExponent is false, this property specifies the lowest possible Exponent property instead.) If HasExponentRange is false, this value will be 0.

   <b>Returns:</b>

The lowest exponent possible when a converted number is expressed in scientific notation with one nonzero digit before the radix point. For example, with a precision of 3 and an EMin of -100, the next value that comes after 0 is 0.001E-100. (If AdjustExponent is false, this property specifies the lowest possible Exponent property instead.) If HasExponentRange is false, this value will be 0.

<a id="Flags"></a>
### Flags

    public int Flags { get; set; }

 Gets or sets the flags that are set from converting numbers according to this arithmetic context. If  `HasFlags`  is false, this value will be 0. This value is a combination of bit fields. To retrieve a particular flag, use the AND operation on the return value of this method. For example:  `(this.Flags &
            EContext.FlagInexact) != 0`  returns  `true`  if the Inexact flag is set.

   <b>Returns:</b>

The flags that are set from converting numbers according to this arithmetic context. If  `HasFlags`  is false, this value will be 0. This value is a combination of bit fields. To retrieve a particular flag, use the AND operation on the return value of this method. For example:  `(this.Flags & EContext.FlagInexact) !=
            0`  returns  `true`  if the Inexact flag is set.

<a id="HasExponentRange"></a>
### HasExponentRange

    public bool HasExponentRange { get; }

 Gets a value indicating whether this context defines a minimum and maximum exponent. If false, converted exponents can have any exponent and operations can't cause overflow or underflow.

   <b>Returns:</b>

 `true`  if this context defines a minimum and maximum exponent; otherwise,  `false` .. If false, converted exponents can have any exponent and operations can't cause overflow or underflow.  `true`  if this context defines a minimum and maximum exponent; otherwise,  `false` .

<a id="HasFlags"></a>
### HasFlags

    public bool HasFlags { get; }

 Gets a value indicating whether this context has a mutable Flags field.

   <b>Returns:</b>

 `true`  if this context has a mutable Flags field; otherwise,  `false` .

<a id="HasFlagsOrTraps"></a>
### HasFlagsOrTraps

    public bool HasFlagsOrTraps { get; }

 Gets a value indicating whether this context has a mutable Flags field, one or more trap enablers, or both.

   <b>Returns:</b>

 `true`  if this context has a mutable Flags field, one or more trap enablers, or both; otherwise,  `false` .

<a id="HasMaxPrecision"></a>
### HasMaxPrecision

    public bool HasMaxPrecision { get; }

 Gets a value indicating whether this context defines a maximum precision.

   <b>Returns:</b>

 `true`  if this context defines a maximum precision; otherwise,  `false` .

<a id="IsPrecisionInBits"></a>
### IsPrecisionInBits

    public bool IsPrecisionInBits { get; }

 Gets a value indicating whether this context's Precision property is in bits, rather than digits. The default is false.

   <b>Returns:</b>

 `true`  if this context's Precision property is in bits, rather than digits; otherwise,  `false` .. The default is false.  `true`  if this context's Precision property is in bits, rather than digits; otherwise,  `false` . The default is false.

<a id="IsSimplified"></a>
### IsSimplified

    public bool IsSimplified { get; }

 Gets a value indicating whether to use a "simplified" arithmetic. In the simplified arithmetic, infinity, not-a-number, and subnormal numbers are not allowed, and negative zero is treated the same as positive zero. For further details, see <a href="http://speleotrove.com/decimal/dax3274.html"> `http://speleotrove.com/decimal/dax3274.html` </a> .

   <b>Returns:</b>

 `true`  if to use a "simplified" arithmetic; otherwise,  `false`  In the simplified arithmetic, infinity, not-a-number, and subnormal numbers are not allowed, and negative zero is treated the same as positive zero. For further details, see <a href="http://speleotrove.com/decimal/dax3274.html"> `http://speleotrove.com/decimal/dax3274.html` </a> .  `true`  if a "simplified" arithmetic will be used; otherwise,  `false` .

<a id="Precision"></a>
### Precision

    public PeterO.Numbers.EInteger Precision { get; }

 Gets the maximum length of a converted number in digits, ignoring the radix point and exponent. For example, if precision is 3, a converted number's mantissa (significand) can range from 0 to 999 (up to three digits long). If 0, converted numbers can have any precision. Not-a-number (NaN) values can carry an optional number, its payload, that serves as its "diagnostic information", In general, if an operation requires copying an NaN's payload, only up to as many digits of that payload as the precision given in this context, namely the least significant digits, are copied.

   <b>Returns:</b>

The maximum length of a converted number in digits, ignoring the radix point and exponent. For example, if precision is 3, a converted number's mantissa (significand) can range from 0 to 999 (up to three digits long). If 0, converted numbers can have any precision.

<a id="Rounding"></a>
### Rounding

    public PeterO.Numbers.ERounding Rounding { get; }

 Gets the desired rounding mode when converting numbers that can't be represented in the given precision and exponent range.

   <b>Returns:</b>

The desired rounding mode when converting numbers that can't be represented in the given precision and exponent range.

<a id="Traps"></a>
### Traps

    public int Traps { get; }

 Gets the traps that are set for each flag in the context. Whenever a flag is signaled, even if  `HasFlags`  is false, and the flag's trap is enabled, the operation will throw a TrapException. For example, if Traps equals  `FlagInexact`  and FlagSubnormal, a TrapException will be thrown if an operation's return value is not the same as the exact result (FlagInexact) or if the return value's exponent is lower than the lowest allowed (FlagSubnormal).

   <b>Returns:</b>

The traps that are set for each flag in the context. Whenever a flag is signaled, even if  `HasFlags`  is false, and the flag's trap is enabled, the operation will throw a TrapException. For example, if Traps equals  `FlagInexact`  and FlagSubnormal, a TrapException will be thrown if an operation's return value is not the same as the exact result (FlagInexact) or if the return value's exponent is lower than the lowest allowed (FlagSubnormal).

.

<a id="Copy"></a>
### Copy

    public PeterO.Numbers.EContext Copy();

 Initializes a new EContext that is a copy of another EContext.

   <b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="ExponentWithinRange_PeterO_Numbers_EInteger"></a>
### ExponentWithinRange

    public bool ExponentWithinRange(
        PeterO.Numbers.EInteger exponent);

 Determines whether a number can have the given Exponent property under this arithmetic context.

     <b>Parameters:</b>

 * <i>exponent</i>: An arbitrary-precision integer indicating the desired exponent.

<b>Return Value:</b>

 `true`  if a number can have the given Exponent property under this arithmetic context; otherwise,  `false` . If this context allows unlimited precision, returns true for the exponent EMax and any exponent less than EMax.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>exponent</i>
 is null.

<a id="ForPrecision_int"></a>
### ForPrecision

    public static PeterO.Numbers.EContext ForPrecision(
        int precision);

 Creates a new arithmetic context using the given maximum number of digits, an unlimited exponent range, and the HalfUp rounding mode.

    <b>Parameters:</b>

 * <i>precision</i>: Maximum number of digits (precision).

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="ForPrecisionAndRounding_int_PeterO_Numbers_ERounding"></a>
### ForPrecisionAndRounding

    public static PeterO.Numbers.EContext ForPrecisionAndRounding(
        int precision,
        PeterO.Numbers.ERounding rounding);

 Creates a new EContext object initialized with an unlimited exponent range, and the given rounding mode and maximum precision.

     <b>Parameters:</b>

 * <i>precision</i>: Maximum number of digits (precision).

 * <i>rounding</i>: The parameter  <i>rounding</i>
 is an ERounding object.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="ForRounding_PeterO_Numbers_ERounding"></a>
### ForRounding

    public static PeterO.Numbers.EContext ForRounding(
        PeterO.Numbers.ERounding rounding);

 Creates a new EContext object initialized with an unlimited precision, an unlimited exponent range, and the given rounding mode.

    <b>Parameters:</b>

 * <i>rounding</i>: The rounding mode for the new precision context.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="ToString"></a>
### ToString

    public override string ToString();

 Gets a string representation of this object. Note that the string's format is not intended to be parsed and may change at any time.

   <b>Return Value:</b>

A string representation of this object.

<a id="WithAdjustExponent_bool"></a>
### WithAdjustExponent

    public PeterO.Numbers.EContext WithAdjustExponent(
        bool adjustExponent);

 Copies this EContext and sets the copy's "AdjustExponent" property to the given value.

    <b>Parameters:</b>

 * <i>adjustExponent</i>: The new value of the "AdjustExponent" property for the copy.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithBigExponentRange_PeterO_Numbers_EInteger_PeterO_Numbers_EInteger"></a>
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
The parameter  <i>exponentMin</i>
 is null.

 * System.ArgumentException:
ExponentMin greater than exponentMax".

<a id="WithBigPrecision_PeterO_Numbers_EInteger"></a>
### WithBigPrecision

    public PeterO.Numbers.EContext WithBigPrecision(
        PeterO.Numbers.EInteger bigintPrecision);

 Copies this EContext and gives it a particular precision value.

     <b>Parameters:</b>

 * <i>bigintPrecision</i>: Desired precision. 0 means unlimited precision.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<b>Exceptions:</b>

 * System.ArgumentNullException:
The parameter  <i>bigintPrecision</i>
 is null.

<a id="WithBlankFlags"></a>
### WithBlankFlags

    public PeterO.Numbers.EContext WithBlankFlags();

 Copies this EContext with  `HasFlags`  set to true and a Flags value of 0.

   <b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithExponentClamp_bool"></a>
### WithExponentClamp

    public PeterO.Numbers.EContext WithExponentClamp(
        bool clamp);

 Copies this arithmetic context and sets the copy's "ClampNormalExponents" flag to the given value.

    <b>Parameters:</b>

 * <i>clamp</i>: The desired value of the "ClampNormalExponents" flag.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithExponentRange_int_int"></a>
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

<a id="WithNoFlags"></a>
### WithNoFlags

    public PeterO.Numbers.EContext WithNoFlags();

 Copies this EContext with  `HasFlags`  set to false and a Flags value of 0.

   <b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithNoFlagsOrTraps"></a>
### WithNoFlagsOrTraps

    public PeterO.Numbers.EContext WithNoFlagsOrTraps();

 Copies this EContext with  `HasFlags`  set to false, a Traps value of 0, and a Flags value of 0.

   <b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithPrecision_int"></a>
### WithPrecision

    public PeterO.Numbers.EContext WithPrecision(
        int precision);

 Copies this EContext and gives it a particular precision value.

    <b>Parameters:</b>

 * <i>precision</i>: Desired precision. 0 means unlimited precision.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithPrecisionInBits_bool"></a>
### WithPrecisionInBits

    public PeterO.Numbers.EContext WithPrecisionInBits(
        bool isPrecisionBits);

 Copies this EContext and sets the copy's "IsPrecisionInBits" property to the given value.

    <b>Parameters:</b>

 * <i>isPrecisionBits</i>: The new value of the "IsPrecisionInBits" property for the copy.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithRounding_PeterO_Numbers_ERounding"></a>
### WithRounding

    public PeterO.Numbers.EContext WithRounding(
        PeterO.Numbers.ERounding rounding);

 Copies this EContext with the specified rounding mode.

    <b>Parameters:</b>

 * <i>rounding</i>: Desired value of the Rounding property.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithSimplified_bool"></a>
### WithSimplified

    public PeterO.Numbers.EContext WithSimplified(
        bool simplified);

 Copies this EContext and sets the copy's "IsSimplified" property to the given value.

    <b>Parameters:</b>

 * <i>simplified</i>: Desired value of the IsSimplified property.

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithTraps_int"></a>
### WithTraps

    public PeterO.Numbers.EContext WithTraps(
        int traps);

 Copies this EContext with Traps set to the given value. (Also sets HasFlags on the copy to  `True` , but this may change in version 2.0 of this library.).

    <b>Parameters:</b>

 * <i>traps</i>: Flags representing the traps to enable. See the property "Traps".

<b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.

<a id="WithUnlimitedExponents"></a>
### WithUnlimitedExponents

    public PeterO.Numbers.EContext WithUnlimitedExponents();

 Copies this EContext with an unlimited exponent range.

   <b>Return Value:</b>

A context object for arbitrary-precision arithmetic settings.
