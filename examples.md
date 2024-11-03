# More Examples

## Number Conversion

Converting a hex string to a big integer:
```c#
public static EInteger HexToEInteger(string hexString){
  // Parse the hexadecimal string as a big integer.  Will
  // throw a FormatException if the parsing fails
  var bigInteger = EInteger.FromRadixString(hexString, 16);
  // Optional: Check if the parsed integer is negative
  if(bigInteger.Sign < 0)
    throw new FormatException("negative hex string");
  return bigInteger;
}
```

Converting a big integer to a `double`:
```c#
public static double EIntegerToDouble(EInteger bigInteger){
 return EFloat.FromEInteger(bigInteger).ToDouble();
}
```

Converting a number string to a `double`:
```c#
public static double StringToDouble(string str){
 return EFloat.FromString(str).ToDouble();
}
```

Converting to and from the .NET decimal formats:
```c#
    public static EDecimal FromDotNetDecimal(int[] bits) {
      int scale = (bits[3] >> 16) & 0xff;
      var data = new byte[13];
      data[0] = (byte)(bits[0] & 0xff);
      data[1] = (byte)((bits[0] >> 8) & 0xff);
      data[2] = (byte)((bits[0] >> 16) & 0xff);
      data[3] = (byte)((bits[0] >> 24) & 0xff);
      data[4] = (byte)(bits[1] & 0xff);
      data[5] = (byte)((bits[1] >> 8) & 0xff);
      data[6] = (byte)((bits[1] >> 16) & 0xff);
      data[7] = (byte)((bits[1] >> 24) & 0xff);
      data[8] = (byte)(bits[2] & 0xff);
      data[9] = (byte)((bits[2] >> 8) & 0xff);
      data[10] = (byte)((bits[2] >> 16) & 0xff);
      data[11] = (byte)((bits[2] >> 24) & 0xff);
      data[12] = 0;
      var mantissa = EInteger.FromBytes(data, true);
      bool negative = (bits[3] >> 31) != 0;
      if (negative) {
        mantissa = -mantissa;
      }
      return EDecimal.Create(mantissa, (EInteger)(-scale));
    }

    private static int[] EncodeDotNetDecimal(
  EInteger bigmant,
  int scale,
  bool neg) {
      if (scale < 0) {
        throw new ArgumentException(
  "scale (" + scale + ") is less than 0");
      }
      if (scale > 28) {
        throw new ArgumentException(
  "scale (" + scale + ") is more than " + "28");
      }
      var data = bigmant.ToBytes(true);
      var a = 0;
      var b = 0;
      var c = 0;
      for (var i = 0; i < Math.Min(4, data.Length); ++i) {
        a |= (((int)data[i]) & 0xff) << (i * 8);
      }
      for (int i = 4; i < Math.Min(8, data.Length); ++i) {
        b |= (((int)data[i]) & 0xff) << ((i - 4) * 8);
      }
      for (int i = 8; i < Math.Min(12, data.Length); ++i) {
        c |= (((int)data[i]) & 0xff) << ((i - 8) * 8);
      }
      int d = scale << 16;
      if (neg) {
        d |= 1 << 31;
      }
      return new[] { a, b, c, d };
    }

    public int[] ToDotNetDecimal() {
      EDecimal extendedNumber = this;
      if (extendedNumber.IsInfinity() || extendedNumber.IsNaN()) {
        throw new OverflowException("This object's value is out of range");
      }
      try {
        var newDecimal = extendedNumber.RoundToPrecision(
          EContext.CliDecimal.WithTraps(EContext.FlagOverflow));
        return EncodeDotNetDecimal(
  newDecimal.Mantissa.Abs(),
  -((int)newDecimal.Exponent),
  newDecimal.Mantissa.Sign < 0);
      } catch (ETrapException ex) {
        throw new OverflowException("This object's value is out of range", ex);
      }
    }
```
