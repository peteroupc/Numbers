/*
Written by Peter O.
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
        var path = Path.GetDirectoryName(
              System.Reflection.Assembly.GetExecutingAssembly().Location);
        var list = new List<string>(
          Directory.GetFiles(path));
        list.Sort();
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
      var errors = new List<string>();
      var dirfiles = new List<string>();
      var sw = new System.Diagnostics.Stopwatch();
      sw.Start();
      var valueSwProcessing = new System.Diagnostics.Stopwatch();
      var standardOut = Console.Out;
      var x = 0;
      dirfiles.AddRange(GetTestFiles());
      foreach (var f in dirfiles) {
        Console.WriteLine((sw.ElapsedMilliseconds / 1000.0) + " " + f);
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
        var lines = 0;
        using (var w = new StreamReader(f)) {
          while (!w.EndOfStream) {
            ++lines;
            if (lines >= 150) {
              break;
            }
            var ln = w.ReadLine();
            valueSwProcessing.Start();
            DecTestUtil.ParseDecTest(ln, context);
            valueSwProcessing.Stop();
          }
        }
      }
      sw.Stop();
      // Total running time
      Console.WriteLine("Time: " + (sw.ElapsedMilliseconds / 1000.0) + " s");
      // Number processing time
      Console.WriteLine("ProcTime: " + (valueSwProcessing.ElapsedMilliseconds /
          1000.0) + " s");
      // Ratio of number processing time to total running time
      Console.WriteLine("Rate: " + (valueSwProcessing.ElapsedMilliseconds *
          100.0 / sw.ElapsedMilliseconds) + "%");
    }
  }
}
