using System;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Test {
  internal sealed class AppResources {
    private readonly ResourceManager mgr;

    public AppResources(string name) {
#if NET20 || NET40
      this.mgr = new ResourceManager(
          name,
          Assembly.GetExecutingAssembly());
#else
      this.mgr = new ResourceManager(typeof(AppResources));
#endif
    }

    private static string ParseJSONString(string str) {
      int c;
if (str.Length==0 || str[0]!='"') {
        return null;
      }
      int index = 1;
      var sb = new StringBuilder();
      while (index < str.Length) {
        c = index >= str.Length ? -1 : str[index++];
        // TODO: Handle supplementals
        if (c == -1 || c < 0x20) {
          return null;
        }
        switch (c) {
          case '\\':
            c = index >= str.Length ? -1 : str[index++];
            // TODO: Handle supplementals
            switch (c) {
              case '\\':
                sb.Append('\\');
                break;
              case '/':
                // Now allowed to be escaped under RFC 8259
                sb.Append('/');
                break;
              case '\"':
                sb.Append('\"');
                break;
              case 'b':
                sb.Append('\b');
                break;
              case 'f':
                sb.Append('\f');
                break;
              case 'n':
                sb.Append('\n');
                break;
              case 'r':
                sb.Append('\r');
                break;
              case 't':
                sb.Append('\t');
                break;
              case 'u': { // Unicode escape
                  c = 0;
                  // Consists of 4 hex digits
                  for (var i = 0; i < 4; ++i) {
                    int ch = index >= str.Length ? -1 : str[index++];
                    if (ch >= '0' && ch <= '9') {
                      c <<= 4;
                      c |= ch - '0';
                    } else if (ch >= 'A' && ch <= 'F') {
                      c <<= 4;
                      c |= ch + 10 - 'A';
                    } else if (ch >= 'a' && ch <= 'f') {
                      c <<= 4;
                      c |= ch + 10 - 'a';
                    } else {
                    return null;
                    }
                  }
                  if ((c & 0xf800) != 0xd800) {
                    // Non-surrogate
                    sb.Append((char)c);
                  } else if ((c & 0xfc00) == 0xd800) {
                    int ch = (index >= str.Length ? -1 : str[index++]);
                    if (ch != '\\' ||
                       (index>= str.Length ? -1 : str[index++]) != 'u') {
                      return null;
                    }
                    var c2 = 0;
                    for (var i = 0; i < 4; ++i) {
                      ch = index >= str.Length ? -1 : str[index++];
                      if (ch >= '0' && ch <= '9') {
                        c2 <<= 4;
                        c2 |= ch - '0';
                      } else if (ch >= 'A' && ch <= 'F') {
                        c2 <<= 4;
                        c2 |= ch + 10 - 'A';
                      } else if (ch >= 'a' && ch <= 'f') {
                        c2 <<= 4;
                        c2 |= ch + 10 - 'a';
                      } else {
                        return null;
                      }
                    }
                    if ((c2 & 0xfc00) != 0xdc00) {
                      return null;
                    } else {
                      sb.Append((char)c);
                      sb.Append((char)c2);
                    }
                  } else {
                    return null;
                  }
                  break;
                }
              default: {
                    return null;
                }
            }
            break;
          case 0x22: // double quote
            return sb.ToString();
          default: {
              // NOTE: Assumes the character reader
              // throws an error on finding illegal surrogate
              // pairs in the string or invalid encoding
              // in the stream
              if ((c >> 16) == 0) {
                sb.Append((char)c);
              } else {
                sb.Append((char)((((c - 0x10000) >> 10) & 0x3ff) +
                    0xd800));
                sb.Append((char)(((c - 0x10000) & 0x3ff) + 0xdc00));
              }
              break;
            }
        }
      }
      return null;
    }

    public string GetJSONString(string name) {
      return ParseJSONString(this.GetString(name));
    }

    public string GetString(string name) {
      return this.mgr.GetString(name);
    }
  }
}
