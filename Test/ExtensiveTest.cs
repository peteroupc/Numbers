/*
Written by Peter O. in 2013.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using PeterO;
using PeterO.Numbers;

namespace Test {
  [TestFixture]
  public class ExtensiveTest {
    public static string[] GetTestFiles() {
      try {
        var list = new List<string>(
          Directory.GetFiles(Path.GetDirectoryName(
              System.Reflection.Assembly.GetExecutingAssembly().Location)));
        return list.ToArray();
      } catch (IOException) {
        return new string[0];
      }
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void IgnoreIfDebug() {
      Assert.Ignore();
    }

    [Test]
    public void TestParser() {
      long failures = 0;
      var errors = new List<string>();
      var dirfiles = new List<string>();
      var sw = new System.Diagnostics.Stopwatch();
      sw.Start();
      var valueSwProcessing = new System.Diagnostics.Stopwatch();
      var standardOut = Console.Out;
      var x = 0;
      dirfiles.AddRange(GetTestFiles());
      foreach (var f in dirfiles) {
        Console.WriteLine(f);
        if (errors.Count > 20) {
          break;
        }
        ++x;
        var context = new Dictionary<string, string>();
        var lowerF = DecTestUtil.ToLowerCaseAscii(f);
        var isinput = lowerF.Contains(".input");
        if (!lowerF.Contains(".input") &&
          !lowerF.Contains(".txt") &&
          !lowerF.Contains(".dectest") &&
          !lowerF.Contains(".fptest")) {
          continue;
        }
        using (var w = new StreamReader(f)) {
          while (!w.EndOfStream) {
            if (errors.Count > 10) {
              break;
            }
            var ln = w.ReadLine();
            DecTestUtil.ParseDecTest(ln, context);
          }
        }
      }
      Console.SetOut(standardOut);
      sw.Stop();
      // Total running time
      Console.WriteLine("Time: " + (sw.ElapsedMilliseconds / 1000.0) + " s");
      // Number processing time
      Console.WriteLine("ProcTime: " + (valueSwProcessing.ElapsedMilliseconds /
          1000.0) + " s");
      // Ratio of number processing time to total running time
      Console.WriteLine("Rate: " + (valueSwProcessing.ElapsedMilliseconds *
          1.0 / sw.ElapsedMilliseconds) + "%");
      if (errors.Count > 0) {
        foreach (string err in errors) {
          Console.WriteLine(err);
        }
        Assert.Fail(failures + " failure(s)");
      }
    }
  }
}
