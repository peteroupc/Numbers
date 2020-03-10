Numbers
====
[![NuGet Status](http://img.shields.io/nuget/v/PeterO.Numbers.svg?style=flat)](https://www.nuget.org/packages/PeterO.Numbers)

**Download source code: [ZIP file](https://github.com/peteroupc/Numbers/archive/master.zip)**

If you like this software, consider donating to me at this link: [http://peteroupc.github.io/](http://peteroupc.github.io/)

----

A C# library that supports arbitrary-precision binary and decimal floating-point numbers and rational numbers with arbitrary-precision components.

Source code is available in the [project page](https://github.com/peteroupc/Numbers).

How to Install
---------
The library is available in the
NuGet Package Gallery under the name
[PeterO.Numbers](https://www.nuget.org/packages/PeterO.Numbers). To install
this library as a NuGet package, enter `Install-Package PeterO.Numbers` in the
NuGet Package Manager Console.

Documentation
------------

**See the [C# (.NET) API documentation](https://peteroupc.github.io/Numbers/docs/).**

Examples
----------

*For more examples, see [examples.md](https://github.com/peteroupc/Numbers/tree/master/examples.md).*

About
-----------

Written by Peter O.

Any copyright is dedicated to the Public Domain.
[http://creativecommons.org/publicdomain/zero/1.0/](http://creativecommons.org/publicdomain/zero/1.0/)

If you like this, you should donate to Peter O.
at: [http://peteroupc.github.io/Numbers/](http://peteroupc.github.io/Numbers/)

Release notes
-------

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

For full release history, see [History.md](History.md).
