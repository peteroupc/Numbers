## PeterO.Numbers.ETrapException

    public sealed class ETrapException :
        System.ArithmeticException,
        System.Runtime.Serialization.ISerializable,
        System.Runtime.InteropServices._Exception

Exception thrown for arithmetic trap errors.

### ETrapException Constructor

    public ETrapException(
        int flag,
        PeterO.Numbers.EContext ctx,
        object result);

Initializes a new instance of the  class.

<b>Parameters:</b>

 * <i>flag</i>: A flag that specifies the kind of error (EContext.FlagXXX). This will only be one flag, such as FlagInexact or FlagSubnormal.

 * <i>ctx</i>: A context object for arbitrary-precision arithmetic settings.

 * <i>result</i>: An arbitrary object.

### Context

    public PeterO.Numbers.EContext Context { get; }

Gets the precision context used during the operation that triggered the trap. May be null.

<b>Returns:</b>

The precision context used during the operation that triggered the trap. May be null.

### Error

    public int Error { get; }

Gets the flag that specifies the kind of error (PrecisionContext.FlagXXX). This will only be one flag, such as FlagInexact or FlagSubnormal.

<b>Returns:</b>

The flag that specifies the kind of error (PrecisionContext.FlagXXX). This will only be one flag, such as FlagInexact or FlagSubnormal.

### Result

    public object Result { get; }

Gets the defined result of the operation that caused the trap.

<b>Returns:</b>

The defined result of the operation that caused the trap.
