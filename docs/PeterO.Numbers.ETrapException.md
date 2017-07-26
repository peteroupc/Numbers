## PeterO.Numbers.ETrapException

    public sealed class ETrapException :
        System.ArithmeticException,
        System.Runtime.InteropServices._Exception,
        System.Runtime.Serialization.ISerializable

Exception thrown for arithmetic trap errors. (The "E" stands for "extended", and has this prefix to group it with the other classes common to this library, particularly EDecimal, EFloat, and ERational.).

### ETrapException Constructor

    public ETrapException(
        int flag,
        PeterO.Numbers.EContext ctx,
        object result);

Initializes a new instance of the [PeterO.Numbers.ETrapException](PeterO.Numbers.ETrapException.md) class.

<b>Parameters:</b>

 * <i>flag</i>: A flag that specifies the kind of error ( `EContext.FlagXXYY`  ). This will only be one flag, such as `FlagInexact`  or FlagSubnormal.

 * <i>ctx</i>: A context object for arbitrary-precision arithmetic settings.

 * <i>result</i>: The parameter  <i>result</i>
 is an arbitrary object.

### Context

    public PeterO.Numbers.EContext Context { get; }

Gets the arithmetic context used during the operation that triggered the trap. May be null.

<b>Returns:</b>

The arithmetic context used during the operation that triggered the trap. May be null.

### Error

    public int Error { get; }

Gets the flag that specifies the kind of error (EContext.FlagXXX). This will only be one flag, such as `FlagInexact`  or FlagSubnormal.

<b>Returns:</b>

The flag that specifies the kind of error (EContext.FlagXXX). This will only be one flag, such as. `FlagInexact`  or FlagSubnormal.

### Result

    public object Result { get; }

Gets the defined result of the operation that caused the trap.

<b>Returns:</b>

The defined result of the operation that caused the trap.
