using System;
using System.IO;
using PeterO.Numbers;
namespace Test {
  public static class SevenBitEncoded {
    private static void Write7BitEncoded(Stream outputStream, EInteger ei) {
      if (outputStream == null) {
        throw new ArgumentNullException(nameof(outputStream));
      }
      if (ei.Sign < 0) {
        throw new ArgumentOutOfRangeException(nameof(ei));
      }
      if (ei.IsZero) {
        outputStream.WriteByte(0);
        return;
      }
      var tmp = new byte[4];
      while (!ei.IsZero) {
        int chunk = ei.ToInt32Unchecked() & 0xfffffff;
        ei = ei.ShiftRight(7);
        if (ei.IsZero) {
          var chunksize = 0;
          tmp[chunksize++] = (byte)chunk;
          if (chunk <= 0x7f) {
            outputStream.WriteByte((byte)chunk);
          } else if (chunk > 0x3fff) {
            tmp[0] = (byte)(0x80 | (chunk & 0x7f));
            tmp[1] = (byte)((chunk >> 7) & 0x7f);
            outputStream.Write(tmp, 0, 2);
          } else if (chunk > 0x1fffff) {
            tmp[0] = (byte)(0x80 | (chunk & 0x7f));
            tmp[1] = (byte)(0x80 | ((chunk >> 7) & 0x7f));
            tmp[2] = (byte)((chunk >> 14) & 0x7f);
            outputStream.Write(tmp, 0, 3);
          } else {
            tmp[0] = (byte)(0x80 | (chunk & 0x7f));
            tmp[1] = (byte)(0x80 | ((chunk >> 7) & 0x7f));
            tmp[2] = (byte)(0x80 | ((chunk >> 14) & 0x7f));
            tmp[3] = (byte)((chunk >> 21) & 0x7f);
            outputStream.Write(tmp, 0, 4);
          }
        } else {
          tmp[0] = (byte)(0x80 | (chunk & 0x7f));
          tmp[1] = (byte)(0x80 | ((chunk >> 7) & 0x7f));
          tmp[2] = (byte)(0x80 | ((chunk >> 14) & 0x7f));
          tmp[3] = (byte)(0x80 | ((chunk >> 21) & 0x7f));
          outputStream.Write(tmp, 0, 4);
        }
      }
    }

    private static EInteger Read7BitEncoded(Stream inputStream, EInteger
      maxValue) {
      if (inputStream == null) {
        throw new ArgumentNullException(nameof(inputStream));
      }
      EInteger ei = EInteger.Zero;
      EInteger shift = EInteger.Zero;
      var endOfValue = false;
      bool haveMaxValue = maxValue != null && maxValue.Sign >= 0;
      while (!endOfValue) {
        var tmp = 0;
        var b = 0;
        var smallshift = 0;
        for (var i = 0; i < 4; ++i) {
          b = inputStream.ReadByte();
          if (b < 0) {
            throw new IOException("End of stream");
          }
          tmp += (b & 0x7f) << smallshift;
          if ((b & 0x80) == 0) {
            endOfValue = true;
            break;
          }
          smallshift += 7;
        }
        ei = ei.Add(EInteger.FromInt32(tmp).ShiftLeft(shift));
        if (haveMaxValue && ei.CompareTo(maxValue) > 0) {
          throw new IOException("Value read is too high");
        }
        shift = shift.Add(28);
      }
      return ei;
    }
  }
}
