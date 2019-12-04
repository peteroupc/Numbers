## PeterO.Numbers.ETrapException

    public sealed class ETrapException :
        System.ArithmeticException,
        System.Runtime.InteropServices._Exception,
        System.Runtime.Serialization.ISerializable

Exception thrown for arithmetic trap errors. (The "E" stands for "extended", and has this prefix to group it with the other classes common to this library, particularly EDecimal, EFloat, and ERational.).This library may throw exceptions of this type in certain cases, notably when errors occur, and may supply messages to those exceptions (accessible through the  `Message`  property in .NET or the  `getMessage()`  method in Java). These messages are intended to be read by humans to help diagnose the error (or other cause of the exception); they are not intended to be parsed by computer programs, and the exact text of the messages may change at any time between versions of this library.

### Member Summary
* <code>[Context](#Context)</code> - Gets the arithmetic context used during the operation that triggered the trap.
* <code>[Error](#Error)</code> - Gets the flag that specifies the primary kind of error in one or more operations (EContext.
* <code>[Errors](#Errors)</code> - Gets the flags that were signaled as the result of one or more operations.
* <code>[HasError(int)](#HasError_int)</code> - Returns whether this trap exception specifies all the flags given.
* <code>[Result](#Result)</code> - Gets the defined result of the operation that caused the trap.

<a id="Void_ctor_Int32_PeterO_Numbers_EContext_System_Object"></a>
### ETrapException Constructor

    public ETrapException(
        int flag,
        PeterO.Numbers.EContext ctx,
        object result);

Initializes a new instance of the [PeterO.Numbers.ETrapException](PeterO.Numbers.ETrapException.md) class.

<b>Parameters:</b>

 * <i>flag</i>: The flag that specifies the kind of error from one or more operations (EContext.FlagXXX). This will only be one flag, such as  `FlagInexact`  or FlagSubnormal.

 * <i>ctx</i>: The arithmetic context used during the operation that triggered the trap. Can be null.

 * <i>result</i>: The defined result of the operation that caused the trap.

<a id="Void_ctor_Int32_Int32_PeterO_Numbers_EContext_System_Object"></a>
### ETrapException Constructor

    public ETrapException(
        int flags,
        int flag,
        PeterO.Numbers.EContext ctx,
        object result);

Initializes a new instance of the [PeterO.Numbers.ETrapException](PeterO.Numbers.ETrapException.md) class.

<b>Parameters:</b>

 * <i>flags</i>: Specifies the flags that were signaled as the result of one or more operations. This includes the flag specified in the "flag" parameter, but can include other flags. For instance, if "flag" is  `EContext.FlagInexact` , this parameter might be  `EContext.FlagInexact | EContext.FlagRounded` .

 * <i>flag</i>: Specifies the flag that specifies the primary kind of error from one or more operations (EContext.FlagXXX). This will only be one flag, such as  `FlagInexact`  or FlagSubnormal.

 * <i>ctx</i>: The arithmetic context used during the operation that triggered the trap. Can be null.

 * <i>result</i>: The defined result of the operation that caused the trap.

<b>Exceptions:</b>

 * System.ArgumentException:
The parameter  <i>flags</i>
 doesn't include all the flags in the  <i>flag</i>
 parameter.

<a id="Void_ctor_System_String"></a>
### ETrapException Constructor

    public ETrapException(
        string message);

Initializes a new instance of the [PeterO.Numbers.ETrapException](PeterO.Numbers.ETrapException.md) class.

<b>Parameters:</b>

 * <i>message</i>: The parameter  <i>message</i>
 is a text string.

<a id="Void_ctor_System_String_System_Exception"></a>
### ETrapException Constructor

    public ETrapException(
        string message,
        System.Exception innerException);

Initializes a new instance of the [PeterO.Numbers.ETrapException](PeterO.Numbers.ETrapException.md) class.

<b>Parameters:</b>

 * <i>message</i>: The parameter  <i>message</i>
 is a text string.

 * <i>innerException</i>: The parameter  <i>innerException</i>
 is an Exception object.

<a id="Void_ctor"></a>
### ETrapException Constructor

    public ETrapException();

Initializes a new instance of the [PeterO.Numbers.ETrapException](PeterO.Numbers.ETrapException.md) class.

<a id="Context"></a>
### Context

    public PeterO.Numbers.EContext Context { get; }

Gets the arithmetic context used during the operation that triggered the trap. May be null.

<b>Returns:</b>

The arithmetic context used during the operation that triggered the trap. May be null.

<a id="Error"></a>
### Error

    public int Error { get; }

Gets the flag that specifies the primary kind of error in one or more operations (EContext.FlagXXX). This will only be one flag, such as  `FlagInexact`  or FlagSubnormal.

<b>Returns:</b>

The flag that specifies the primary kind of error in one or more operations.

<a id="Errors"></a>
### Errors

    public int Errors { get; }

Gets the flags that were signaled as the result of one or more operations. This includes the flag specified in the "flag" parameter, but can include other flags. For instance, if "flag" is  `EContext.FlagInexact` , this parameter might be  `EContext.FlagInexact | EContext.FlagRounded` .

<b>Returns:</b>

The flags that specify the errors in one or more operations.

<a id="Result"></a>
### Result

    public object Result { get; }

Gets the defined result of the operation that caused the trap.

<b>Returns:</b>

The defined result of the operation that caused the trap.

<a id="HasError_int"></a>
### HasError

    public bool HasError(
        int flag);

Returns whether this trap exception specifies all the flags given. (Flags are signaled in a trap exception as the result of one or more operations involving arbitrary-precision numbers, such as multiplication of two EDecimals.).

<b>Parameters:</b>

 * <i>flag</i>: A combination of one or more flags, such as  `EContext.FlagInexact | EContext.FlagRounded` .

<b>Return Value:</b>

True if this exception pertains to all of the flags given in  <i>flag</i>
 ; otherwise, false.
