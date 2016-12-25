/*
Written by Peter O. in 2014.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
#if DEBUG
using System;
using System.Reflection;

namespace PeterO {
  internal static class DebugUtility {
    private static MethodInfo GetTypeMethod(
      Type t,
      string name,
      Type[] parameters) {
#if NET40
      return t.GetMethod(name, parameters);
#else
{
        return t.GetRuntimeMethod(name, parameters);
      }
#endif
    }

    public static void Log(string str) {
      Type type = Type.GetType("System.Console");
      var types = new[] { typeof(string) };
      GetTypeMethod(type, "WriteLine", types).Invoke(
        type,
        new object[] { str });
    }

    public static void Log(object obj) {
      Log(String.Empty + obj);
    }

    public static void Log(string format, params object[] args) {
      Log(String.Format(format, args));
    }
  }
}
#endif
