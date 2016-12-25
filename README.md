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

Written in 2016 by Peter O.

Any copyright is dedicated to the Public Domain.
[http://creativecommons.org/publicdomain/zero/1.0/](http://creativecommons.org/publicdomain/zero/1.0/)

If you like this, you should donate to Peter O.
at: [http://peteroupc.github.io/Numbers/](http://peteroupc.github.io/Numbers/)

Release notes
-------
Version 0.4:

- Add netstandard 1.0 support 

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
