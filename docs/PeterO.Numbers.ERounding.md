## PeterO.Numbers.ERounding

    public sealed struct ERounding :
        System.Enum,
        System.IFormattable,
        System.IComparable,
        System.IConvertible

Specifies the mode to use when "shortening" numbers that otherwise can't fit a given number of digits, so that the shortened number has about the same value. This "shortening" is known as rounding. (The "E" stands for "extended", and has this prefix to group it with the other classes common to this library, particularly EDecimal, EFloat, and ERational.).

### Member Summary
* <code>[Ceiling](#Ceiling)</code> - If there is a fractional part, the number is rounded to the highest representable number that's closest to it.
* <code>[Down](#Down)</code> - The fractional part is discarded (the number is truncated).
* <code>[Floor](#Floor)</code> - If there is a fractional part, the number is rounded to the lowest representable number that's closest to it.
* <code>[HalfDown](#HalfDown)</code> - Rounded to the nearest number; if the fractional part is exactly half, it is discarded.
* <code>[HalfEven](#HalfEven)</code> - Rounded to the nearest number; if the fractional part is exactly half, the number is rounded to the closest representable number that is even.
* <code>[HalfUp](#HalfUp)</code> - Rounded to the nearest number; if the fractional part is exactly half, the number is rounded to the closest representable number away from zero.
* <code>[None](#None)</code> - Indicates that rounding will not be used.
* <code>[Odd](#Odd)</code> - If there is a fractional part and the whole number part is even, the number is rounded to the closest representable odd number away from zero.
* <code>[OddOrZeroFiveUp](#OddOrZeroFiveUp)</code> - For binary floating point numbers, this is the same as Odd.
* <code>[Up](#Up)</code> - If there is a fractional part, the number is rounded to the closest representable number away from zero.
* <code>[ZeroFiveUp](#ZeroFiveUp)</code> - If there is a fractional part and if the last digit before rounding is 0 or half the radix, the number is rounded to the closest representable number away from zero; otherwise the fractional part is discarded.

<a id="Ceiling"></a>
### Ceiling

    public static PeterO.Numbers.ERounding Ceiling = 6;

If there is a fractional part, the number is rounded to the highest representable number that's closest to it.

<a id="Down"></a>
### Down

    public static PeterO.Numbers.ERounding Down = 2;

The fractional part is discarded (the number is truncated).

<a id="Floor"></a>
### Floor

    public static PeterO.Numbers.ERounding Floor = 7;

If there is a fractional part, the number is rounded to the lowest representable number that's closest to it.

<a id="HalfDown"></a>
### HalfDown

    public static PeterO.Numbers.ERounding HalfDown = 4;

Rounded to the nearest number; if the fractional part is exactly half, it is discarded.

<a id="HalfEven"></a>
### HalfEven

    public static PeterO.Numbers.ERounding HalfEven = 5;

Rounded to the nearest number; if the fractional part is exactly half, the number is rounded to the closest representable number that is even. This is sometimes also known as "banker's rounding".

<a id="HalfUp"></a>
### HalfUp

    public static PeterO.Numbers.ERounding HalfUp = 3;

Rounded to the nearest number; if the fractional part is exactly half, the number is rounded to the closest representable number away from zero. This is the most familiar rounding mode for many people.

<a id="None"></a>
### None

    public static PeterO.Numbers.ERounding None = 0;

Indicates that rounding will not be used. If rounding is required, the rounding operation will report an error.

<a id="Odd"></a>
### Odd

    public static PeterO.Numbers.ERounding Odd = 8;

If there is a fractional part and the whole number part is even, the number is rounded to the closest representable odd number away from zero.

<a id="OddOrZeroFiveUp"></a>
### OddOrZeroFiveUp

    public static PeterO.Numbers.ERounding OddOrZeroFiveUp = 10;

For binary floating point numbers, this is the same as Odd. For other bases (including decimal numbers), this is the same as ZeroFiveUp. This rounding mode is useful for rounding intermediate results at a slightly higher precision (at least 2 bits more for binary) than the final precision.

<a id="Up"></a>
### Up

    public static PeterO.Numbers.ERounding Up = 1;

If there is a fractional part, the number is rounded to the closest representable number away from zero.

<a id="ZeroFiveUp"></a>
### ZeroFiveUp

    public static PeterO.Numbers.ERounding ZeroFiveUp = 9;

If there is a fractional part and if the last digit before rounding is 0 or half the radix, the number is rounded to the closest representable number away from zero; otherwise the fractional part is discarded. In overflow, the fractional part is always discarded.
