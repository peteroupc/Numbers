using System;
namespace PeterO.Rand {
 public interface IRandomGen {
    int GetBytes(byte[] bytes, int offset, int length);
  }
}
