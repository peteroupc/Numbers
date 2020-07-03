Release notes
-------

Version 1.7.0

- Added overloads to string-to-number methods that take char[] and byte[] arrays.
- Added methods that convert EDecimal, EFloat, and ERational to and from raw bits that follow IEEE 754 binary floating-point formats (To/FromDoubleBits, To/FromSingleBits).
- Added Log1P and ExpM1 methods to EDecimal and EFloat
- Added 'long' overloads to several arithmetic methods
- Added implication and equivalence (Imp/Eqv) methods and an nth-root method to EInteger

Version 1.6.0

- Numerous performance improvements in the EInteger, EDecimal, EFloat, and ERational classes.  Among them is the use of caches for small EInteger, EDecimal, and EFloat values, and faster multiplication algorithms for large EIntegers.
- Correctness fixes to the Log() methods in EDecimal and EFloat
- New LogN() method in EDecimal and EFloat
- New methods in EInteger, including FromBytes, GetDigitCountAsEInteger, and DivRem(long)
- New ToSizedEInteger/ToSizedEIntegerIfExact/IsInteger methods in EDecimal, EFloat, and ERational
- New Create overloads in EFloat and ERational
- New Min and Max methods in EInteger and ERational
- Bug fixes

Version 1.5.1

- Fix bugs in EDecimal.FromString and ERational.FromString involving substrings containing negative numbers.

Version 1.5.0

- Major performance improvements in certain number parsing and generating methods, including the FromString methods of EInteger, EDecimal, EFloat, and ERational, and the ToEFloat method of EDecimal, especially where they take an arithmetic context (EContext) that specifies a limited precision and exponent range.
- There were also performance improvements in digit count calculation and in rounding many-digit-long numbers.
- Add int overloads to EDecimal.Pow and EFloat.Pow.
- Add int overloads to several ERational methods.
- Add CompareTo overloads and CompareToValue (which implements current CompareTo) in EDecimal, EFloat, and ERational.  In a future version, CompareTo's behavior might change to CompareToTotal in each of these classes.  Also certain CompareTo* methods now have consistent behavior when they receive a null argument.
- ETrapException now has an Errors property that holds all errors that occur at the same time as the primary error.
- Fixed edge cases when ToShortestString might return an incorrect result.
- Fixed bug when some ETrapExceptions aren't thrown as they should.
- Other bug fixes.

Version 1.4.3

- Fix accuracy issue with Log, especially where 1 < x < 1.07
- Remove StyleCop.Analyzers, which is used only in development, as dependency in .NET version

Version 1.4.2

- Bug fix in the EInteger.CanFitInInt64 method

Version 1.4.1

- Added EDecimals and EFloats classes to .NET 2.0 and .NET 4.0 versions; those classes were inadvertently omitted there

Version 1.4.0

- Added EDecimals and EFloats class that implements more methods for arbitrary-precision decimal and binary numbers
- Increment and decrement operators added to EInteger, EDecimal, EFloat, and ERational classes
- Allowed EDecimal values in (-1, 0) to EDecimal's *Checked methods, to conform to documentation.
- Added WithNoFlagsOrTraps method and HasFlagsOrTraps property to EContext
- Add Mod(int), Pow(int), and FromBoolean methods to EInteger
- Add And, Not, Xor, and Or methods to EInteger.cs
- Add Copy method to EDecimal, EFloat, and ERational
- Add CompareToTotalMagnitude overload to EDecimal, EFloat, and ERational
- Deprecated Odd and ZeroFiveUp rounding modes
- Bug fixes and performance improvements

Version 1.3.0:

- Improve performance of EDecimal.CompareToBinary in certain cases
- Fix ERational.ToSingle method
- Add EInteger overloads to EInteger.GetSignedBit, EInteger.GetUnsignedBit, EInteger.ShiftLeft, and EInteger.ShiftRight
- Add GetDigitCountAsEInteger and GetSignedBitLengthAsEInteger methods to EInteger class
- Check for overflow in GetLowBit, GetDigitCount, GetUnsignedBitLength, and
  GetSignedBitLength methods in EInteger class; deprecate those methods
- Add FromBoolean methods to EDecimal, EFloat, and ERational

Version 1.2.2:

- Fixed referencing issues with minor version 1.2.1

Version 1.2.1:

- Fixed bugs with new EInteger.Add and EInteger.Subtract overloads

Version 1.2:

- Add arithmetic methods to EInteger, EDecimal, and EFloat that
 take 'int' operands.
- Fix issues with EDecimal/EFloat Remainder method in corner cases
- Add RemainderNoRoundAfterDivide in EDecimal and EFloat

Version 1.1.2

- Add .NET Framework 4.0 targeted assembly to avoid compiler warnings that can appear when this package is added to a project that targets .NET Framework 4.0 or later.

Version 1.1.1

- Numbers .NET 2.0 assembly had wrong version number.

Version 1.1.0

- Added build targeting .NET Framework 2.0.

Version 1.0.2

- Really strong-name sign the assembly, which (probably) was inadvertently delay-signed in version 1.0.

Version 1.0

- Filled out documentation so that there are no more undocumented parts

Version 0.5

- Moved from .NET Portable to .NET Standard 1.0. Contributed by GitHub user NZSmartie
- Broke backwards compatibility with .NET Framework 4.0
- Bug fixes

Version 0.4:

- Assembly signed with a strong name
- Some improvements to documentation

Version 0.3:

- Deprecated ERational constructor
- Added many type conversion operators and methods
 to EDecimal, EFloat, ERational, and EInteger
- Added FromString, CompareToTotal, and
  CompareToTotalMagnitude methods to ERational
- An overload of RoundToExponentExact in EDecimal is
 no longer obsolete and uses the rounding mode specified
- Used a new division implementation in EInteger
- Used the new division implementation to optimize conversion
  of huge EIntegers to decimal strings
- Bug fixes

Version 0.2.2:

- Previous assembly was released with wrong version number

Version 0.2.1:

- Fixed corner cases in EFloat's ToSingle and ToDouble methods

Version 0.2:

- Performance improvements
- Added several overloads for DivideToExponent method
- GCD code in EInteger rewritten
- Added CopySign, CompareToTotal, and CompareToTotalMagnitude
 methods to EDecimal and EFloat.
- Renamed several methods in EDecimal and EFloat
- RoundToIntegral\* methods renamed to RoundToInteger\* methods
- Renamed some EInteger integer conversion methods; added
 CanFitInInt64, GetUnsignedBitLengthAsEInteger,
 and GetLowBitAsEInteger methods
- Several operators added to EDecimal in C# version
- Rewrote code that converts from decimal to binary floating-point;
 add ToEFloat method taking an EContext in EDecimal
- Added ToShortestString method in EFloat
- Add UnlimitedHalfEven EContext object
- Bug fixes

Version 0.1:

- Initial release
