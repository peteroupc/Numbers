/*
Written by Peter O. in 2013.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;

namespace PeterO.Numbers {
   // TODO: Add WithNoFlagsOrTraps method which resets flags
   // and traps to 0
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="T:PeterO.Numbers.EContext"]/*'/>
  public sealed class EContext {
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.FlagClamped"]/*'/>
    public const int FlagClamped = 32;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.FlagDivideByZero"]/*'/>
    public const int FlagDivideByZero = 128;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.FlagInexact"]/*'/>
    public const int FlagInexact = 1;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.FlagInvalid"]/*'/>
    public const int FlagInvalid = 64;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.FlagLostDigits"]/*'/>
    public const int FlagLostDigits = 256;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.FlagOverflow"]/*'/>
    public const int FlagOverflow = 16;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.FlagRounded"]/*'/>
    public const int FlagRounded = 2;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.FlagSubnormal"]/*'/>
    public const int FlagSubnormal = 4;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.FlagUnderflow"]/*'/>
    public const int FlagUnderflow = 8;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.Basic"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif
    public static readonly EContext Basic =
      EContext.ForPrecisionAndRounding(9, ERounding.HalfUp);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.BigDecimalJava"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif
    public static readonly EContext BigDecimalJava =
      new EContext(0, ERounding.HalfUp, 0, 0, true)
      .WithExponentClamp(true).WithAdjustExponent(false)
      .WithBigExponentRange(
  EInteger.Zero - (EInteger)Int32.MaxValue,
  EInteger.One + (EInteger)Int32.MaxValue);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.Binary128"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif
    public static readonly EContext Binary128 =
      EContext.ForPrecisionAndRounding(113, ERounding.HalfEven)
      .WithExponentClamp(true).WithExponentRange(-16382, 16383);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.Binary16"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif
    public static readonly EContext Binary16 =
      EContext.ForPrecisionAndRounding(11, ERounding.HalfEven)
      .WithExponentClamp(true).WithExponentRange(-14, 15);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.Binary32"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif
    public static readonly EContext Binary32 =
      EContext.ForPrecisionAndRounding(24, ERounding.HalfEven)
      .WithExponentClamp(true).WithExponentRange(-126, 127);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.Binary64"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif
    public static readonly EContext Binary64 =
      EContext.ForPrecisionAndRounding(53, ERounding.HalfEven)
      .WithExponentClamp(true).WithExponentRange(-1022, 1023);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.CliDecimal"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif

    public static readonly EContext CliDecimal =
      new EContext(96, ERounding.HalfEven, 0, 28, true)
      .WithPrecisionInBits(true);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.Decimal128"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif

    public static readonly EContext Decimal128 =
      new EContext(34, ERounding.HalfEven, -6143, 6144, true);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.Decimal32"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif

    public static readonly EContext Decimal32 =
      new EContext(7, ERounding.HalfEven, -95, 96, true);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.Decimal64"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif

    public static readonly EContext Decimal64 =
      new EContext(16, ERounding.HalfEven, -383, 384, true);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.Unlimited"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif

    public static readonly EContext Unlimited =
      EContext.ForPrecision(0);

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="F:PeterO.Numbers.EContext.UnlimitedHalfEven"]/*'/>
#if CODE_ANALYSIS
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Security",
      "CA2104",
      Justification = "This PrecisionContext is immutable")]
#endif

    public static readonly EContext UnlimitedHalfEven =
      EContext.ForPrecision(0).WithRounding(ERounding.HalfEven);

    private EContext(
     bool adjustExponent,
     EInteger bigintPrecision,
     bool clampNormalExponents,
     EInteger exponentMax,
     EInteger exponentMin,
     int flags,
     bool hasExponentRange,
     bool hasFlags,
     bool precisionInBits,
     ERounding rounding,
     bool simplified,
     int traps) {
if (bigintPrecision == null) {
 throw new ArgumentNullException(nameof(bigintPrecision));
}
if (exponentMin == null) {
 throw new ArgumentNullException(nameof(exponentMin));
}
if (exponentMax == null) {
 throw new ArgumentNullException(nameof(exponentMax));
}
      if (bigintPrecision.Sign < 0) {
        throw new ArgumentException("precision (" + bigintPrecision +
          ") is less than 0");
      }
      if (exponentMin.CompareTo(exponentMax) > 0) {
        throw new ArgumentException("exponentMinSmall (" + exponentMin +
          ") is more than " + exponentMax);
      }
this.adjustExponent = adjustExponent;
this.bigintPrecision = bigintPrecision;
this.clampNormalExponents = clampNormalExponents;
this.exponentMax = exponentMax;
this.exponentMin = exponentMin;
this.flags = flags;
this.hasExponentRange = hasExponentRange;
this.hasFlags = hasFlags;
this.precisionInBits = precisionInBits;
this.rounding = rounding;
this.simplified = simplified;
this.traps = traps;
}

    private readonly bool adjustExponent;

    private readonly EInteger bigintPrecision;

    private readonly bool clampNormalExponents;
    private readonly EInteger exponentMax;

    private readonly EInteger exponentMin;

    private int flags;

    private readonly bool hasExponentRange;
    private readonly bool hasFlags;

    private readonly bool precisionInBits;

    private readonly ERounding rounding;

    private readonly bool simplified;

    private readonly int traps;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.#ctor(System.Int32,PeterO.Numbers.ERounding,System.Int32,System.Int32,System.Boolean)"]/*'/>
    public EContext(
  int precision,
  ERounding rounding,
  int exponentMinSmall,
  int exponentMaxSmall,
  bool clampNormalExponents) : this(
      true,
      EInteger.FromInt32(precision),
      clampNormalExponents,
      EInteger.FromInt32(exponentMaxSmall),
      EInteger.FromInt32(exponentMinSmall),
      0,
      true,
      false,
      false,
      rounding,
      false,
      0) {
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.#ctor(PeterO.Numbers.EInteger,PeterO.Numbers.ERounding,PeterO.Numbers.EInteger,PeterO.Numbers.EInteger,System.Boolean)"]/*'/>
    public EContext(
  EInteger bigintPrecision,
  ERounding rounding,
  EInteger exponentMin,
  EInteger exponentMax,
  bool clampNormalExponents) : this(
      true,
      bigintPrecision,
      clampNormalExponents,
      exponentMax,
      exponentMin,
      0,
      true,
      false,
      false,
      rounding,
      false,
      0) {
}

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.AdjustExponent"]/*'/>
    public bool AdjustExponent {
      get {
        return this.adjustExponent;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.ClampNormalExponents"]/*'/>
    public bool ClampNormalExponents {
      get {
        return this.hasExponentRange && this.clampNormalExponents;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.EMax"]/*'/>
    public EInteger EMax {
      get {
        return this.hasExponentRange ? this.exponentMax : EInteger.Zero;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.EMin"]/*'/>
    public EInteger EMin {
      get {
        return this.hasExponentRange ? this.exponentMin : EInteger.Zero;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.Flags"]/*'/>
    public int Flags {
      get {
        return this.flags;
      }

      set {
        if (!this.HasFlags) {
          throw new InvalidOperationException("Can't set flags");
        }
        this.flags = value;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.HasExponentRange"]/*'/>
    public bool HasExponentRange {
      get {
        return this.hasExponentRange;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.HasFlags"]/*'/>
    public bool HasFlags {
      get {
        return this.hasFlags;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.HasMaxPrecision"]/*'/>
    public bool HasMaxPrecision {
      get {
        return !this.bigintPrecision.IsZero;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.IsPrecisionInBits"]/*'/>
    public bool IsPrecisionInBits {
      get {
        return this.precisionInBits;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.IsSimplified"]/*'/>
    public bool IsSimplified {
      get {
        return this.simplified;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.Precision"]/*'/>
    public EInteger Precision {
      get {
        return this.bigintPrecision;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.Rounding"]/*'/>
    public ERounding Rounding {
      get {
        return this.rounding;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.EContext.Traps"]/*'/>
    public int Traps {
      get {
        return this.traps;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.ForPrecision(System.Int32)"]/*'/>
    public static EContext ForPrecision(int precision) {
      return new EContext(
  precision,
  ERounding.HalfUp,
  0,
  0,
  false).WithUnlimitedExponents();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.ForPrecisionAndRounding(System.Int32,PeterO.Numbers.ERounding)"]/*'/>
    public static EContext ForPrecisionAndRounding(
      int precision,
      ERounding rounding) {
      return new EContext(
  precision,
  rounding,
  0,
  0,
  false).WithUnlimitedExponents();
    }

    private static readonly EContext ForRoundingHalfEven = new EContext(
  0,
  ERounding.HalfEven,
  0,
  0,
  false).WithUnlimitedExponents();

    private static readonly EContext ForRoundingDown = new EContext(
  0,
  ERounding.Down,
  0,
  0,
  false).WithUnlimitedExponents();

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.ForRounding(PeterO.Numbers.ERounding)"]/*'/>
    public static EContext ForRounding(ERounding rounding) {
      if (rounding == ERounding.HalfEven) {
        return ForRoundingHalfEven;
      }
      if (rounding == ERounding.Down) {
        return ForRoundingDown;
      }
      return new EContext(
  0,
  rounding,
  0,
  0,
  false).WithUnlimitedExponents();
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.Copy"]/*'/>
    public EContext Copy() {
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  this.flags,
  this.hasExponentRange,
  this.hasFlags,
  this.precisionInBits,
  this.rounding,
  this.simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.ExponentWithinRange(PeterO.Numbers.EInteger)"]/*'/>
    public bool ExponentWithinRange(EInteger exponent) {
      if (exponent == null) {
        throw new ArgumentNullException(nameof(exponent));
      }
      if (!this.HasExponentRange) {
        return true;
      }
      if (this.bigintPrecision.IsZero) {
        // Only check EMax, since with an unlimited
        // precision, any exponent less than EMin will exceed EMin if
        // the mantissa is the right size
        return exponent.CompareTo(this.EMax) <= 0;
      } else {
        EInteger bigint = exponent;
        if (this.adjustExponent) {
          bigint += (EInteger)this.bigintPrecision;
          bigint -= EInteger.One;
        }
        return (bigint.CompareTo(this.EMin) >= 0) &&
          (exponent.CompareTo(this.EMax) <= 0);
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.ToString"]/*'/>
    public override string ToString() {
      return "[PrecisionContext ExponentMax=" + this.exponentMax +
        ", Traps=" + this.traps + ", ExponentMin=" + this.exponentMin +
        ", HasExponentRange=" + this.hasExponentRange + ", BigintPrecision=" +
        this.bigintPrecision + ", Rounding=" + this.rounding +
        ", ClampNormalExponents=" + this.clampNormalExponents + ", Flags=" +
        this.flags + ", HasFlags=" + this.hasFlags + "]";
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithAdjustExponent(System.Boolean)"]/*'/>
    public EContext WithAdjustExponent(bool adjustExponent) {
return new EContext(
  adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  this.flags,
  this.hasExponentRange,
  this.hasFlags,
  this.precisionInBits,
  this.rounding,
  this.simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithBigExponentRange(PeterO.Numbers.EInteger,PeterO.Numbers.EInteger)"]/*'/>
    public EContext WithBigExponentRange(
      EInteger exponentMin,
      EInteger exponentMax) {
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  exponentMax,
  exponentMin,
  this.flags,
  true,
  this.hasFlags,
  this.precisionInBits,
  this.rounding,
  this.simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithBigPrecision(PeterO.Numbers.EInteger)"]/*'/>
    public EContext WithBigPrecision(EInteger bigintPrecision) {
return new EContext(
  this.adjustExponent,
  bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  this.flags,
  this.hasExponentRange,
  this.hasFlags,
  this.precisionInBits,
  this.rounding,
  this.simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithBlankFlags"]/*'/>
    public EContext WithBlankFlags() {
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  0,
  this.hasExponentRange,
  true,
  this.precisionInBits,
  this.rounding,
  this.simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithExponentClamp(System.Boolean)"]/*'/>
    public EContext WithExponentClamp(bool clamp) {
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  clamp,
  this.exponentMax,
  this.exponentMin,
  this.flags,
  this.hasExponentRange,
  this.hasFlags,
  this.precisionInBits,
  this.rounding,
  this.simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithExponentRange(System.Int32,System.Int32)"]/*'/>
    public EContext WithExponentRange(
      int exponentMinSmall,
      int exponentMaxSmall) {
return this.WithBigExponentRange(
  EInteger.FromInt32(exponentMinSmall),
  EInteger.FromInt32(exponentMaxSmall));
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithNoFlags"]/*'/>
    public EContext WithNoFlags() {
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  0,
  this.hasExponentRange,
  false,
  this.precisionInBits,
  this.rounding,
  this.simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithPrecision(System.Int32)"]/*'/>
    public EContext WithPrecision(int precision) {
return this.WithBigPrecision(EInteger.FromInt32(precision));
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithPrecisionInBits(System.Boolean)"]/*'/>
    public EContext WithPrecisionInBits(bool isPrecisionBits) {
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  this.flags,
  this.hasExponentRange,
  this.hasFlags,
  isPrecisionBits,
  this.rounding,
  this.simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithRounding(PeterO.Numbers.ERounding)"]/*'/>
    public EContext WithRounding(ERounding rounding) {
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  this.flags,
  this.hasExponentRange,
  this.hasFlags,
  this.precisionInBits,
  rounding,
  this.simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithSimplified(System.Boolean)"]/*'/>
    public EContext WithSimplified(bool simplified) {
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  this.flags,
  this.hasExponentRange,
  this.hasFlags,
  this.precisionInBits,
  this.rounding,
  simplified,
  this.traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithTraps(System.Int32)"]/*'/>
    public EContext WithTraps(int traps) {
 // TODO: In next major version, copy HasFlags rather than
 // setting it to true
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  this.flags,
  this.hasExponentRange,
  true,
  this.precisionInBits,
  this.rounding,
  this.simplified,
  traps);
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="M:PeterO.Numbers.EContext.WithUnlimitedExponents"]/*'/>
    public EContext WithUnlimitedExponents() {
return new EContext(
  this.adjustExponent,
  this.bigintPrecision,
  this.clampNormalExponents,
  this.exponentMax,
  this.exponentMin,
  this.flags,
  false,
  this.hasFlags,
  this.precisionInBits,
  this.rounding,
  this.simplified,
  this.traps);
    }
  }
}
