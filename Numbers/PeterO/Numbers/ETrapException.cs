/*
Written by Peter O. in 2014.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;

namespace PeterO.Numbers {
    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="T:PeterO.Numbers.ETrapException"]/*'/>
  public sealed class ETrapException : ArithmeticException {
    private readonly Object result;
    private readonly EContext ctx;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.ETrapException.Context"]/*'/>
    public EContext Context {
      get {
        return this.ctx;
      }
    }

    private readonly int error;

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.ETrapException.Result"]/*'/>
    public Object Result {
      get {
        return this.result;
      }
    }

    /// <include file='../../docs.xml'
    /// path='docs/doc[@name="P:PeterO.Numbers.ETrapException.Error"]/*'/>
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
    /// <see cref='ETrapException'/> class.</summary>
    /// <param name='flag'>A 32-bit signed integer.</param>
    /// <param name='ctx'>An EContext object.</param>
    /// <param name='result'>An arbitrary object.</param>
    public ETrapException(int flag, EContext ctx, Object result) :
      base(FlagToMessage(flag)) {
      this.error = flag;
      this.ctx = (ctx == null) ? null : ctx.Copy();
      this.result = result;
    }
  }
}
