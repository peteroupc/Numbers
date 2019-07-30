/*
Written by Peter O. in 2014.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;

namespace PeterO.Numbers {
    /// <summary>Exception thrown for arithmetic trap errors. (The "E"
    /// stands for "extended", and has this prefix to group it with the
    /// other classes common to this library, particularly EDecimal,
    /// EFloat, and ERational.).</summary>
  public sealed class ETrapException : ArithmeticException {
    private readonly Object result;
    private readonly EContext ctx;

    /// <summary>Gets the arithmetic context used during the operation that
    /// triggered the trap. May be null.</summary>
    /// <value>The arithmetic context used during the operation that
    /// triggered the trap. May be null.</value>
    public EContext Context {
      get {
        return this.ctx;
      }
    }

    private readonly int error;

    /// <summary>Gets the defined result of the operation that caused the
    /// trap.</summary>
    /// <value>The defined result of the operation that caused the
    /// trap.</value>
    public Object Result {
      get {
        return this.result;
      }
    }

    /// <summary>Gets the flag that specifies the kind of error
    /// (EContext.FlagXXX). This will only be one flag, such as
    /// <c>FlagInexact</c> or FlagSubnormal.</summary>
    /// <value>The flag that specifies the kind of error
    /// (EContext.FlagXXX). This will only be one flag, such as.
    /// <c>FlagInexact</c> or FlagSubnormal.</value>
    public int Error {
      get {
        return this.error;
      }
    }

    private static string FlagToMessage(int flag) {
      return (flag == EContext.FlagClamped) ? "Clamped" : ((flag ==
        EContext.FlagDivideByZero) ? "DivideByZero" : ((flag ==
        EContext.FlagInexact) ? "Inexact" : ((flag ==
        EContext.FlagInvalid) ? "Invalid" : ((flag ==
        EContext.FlagOverflow) ? "Overflow" : ((flag ==
        EContext.FlagRounded) ? "Rounded" : ((flag ==
        EContext.FlagSubnormal) ? "Subnormal" : ((flag ==
        EContext.FlagUnderflow) ? "Underflow" : "Trap")))))));
    }

    /// <summary>Initializes a new instance of the
    /// <see cref='PeterO.Numbers.ETrapException'/>.</summary>
    /// <param name='flag'>The parameter <paramref name='flag'/> is a
    /// 32-bit signed integer.</param>
    /// <param name='ctx'>The parameter <paramref name='ctx'/> is an
    /// EContext object.</param>
    /// <param name='result'>The parameter <paramref name='result'/> is an
    /// arbitrary object.</param>
    public ETrapException(int flag, EContext ctx, Object result)
      : base(FlagToMessage(flag)) {
      this.error = flag;
      this.ctx = (ctx == null) ? null : ctx.Copy();
      this.result = result;
    }
  }
}
