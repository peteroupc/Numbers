/*
Written by Peter O.
Any copyright to this work is released to the Public Domain.
In case this is not possible, this work is also
licensed under Creative Commons Zero (CC0):
http://creativecommons.org/publicdomain/zero/1.0/

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
    [Timeout(300000)]
    public void TestParser() {
      var errors = new List<string>();
      var dirfiles = new List<string>();
      var slowlines = new List<string>();
      var sw = new System.Diagnostics.Stopwatch();
      sw.Start();
      var valueSwProcessing = new System.Diagnostics.Stopwatch();
      var standardOut = Console.Out;
      var x = 0;
      dirfiles.AddRange(GetTestFiles());
      foreach (var f in dirfiles) {
        ++x;
        var context = new Dictionary<string, string>();
        var lowerF = DecTestUtil.ToLowerCaseAscii(f);
        var isinput = DecTestUtil.Contains(lowerF, ".input");
        if (!DecTestUtil.Contains(lowerF, ".input") &&
          !DecTestUtil.Contains(lowerF, ".txt") &&
          !DecTestUtil.Contains(lowerF, ".dectest") &&
          !DecTestUtil.Contains(lowerF, ".fptest")) {
          continue;
        }
        Console.WriteLine((sw.ElapsedMilliseconds / 1000.0) + " " + f);
        var i = 0;
        using (var w = new StreamReader(f)) {
          while (!w.EndOfStream) {
            var ln = w.ReadLine();
            ++i;
            double em = sw.ElapsedMilliseconds / 1000.0;
            valueSwProcessing.Start();
            DecTestUtil.ParseDecTest(ln, context);
            valueSwProcessing.Stop();
            double em2 = sw.ElapsedMilliseconds / 1000.0;
            if (em2 - em > 1) {
              foreach (var k in context.Keys) {
                slowlines.Add(k + ": " + context[k]);
              }
              slowlines.Add(ln);
              Console.WriteLine(
                ln.Substring(0, Math.Min(ln.Length, 200)));
              Console.WriteLine("Processing time: " + (em2 - em) + " s");
            }
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
      // System.IO.File.WriteAllLines("slow.dectest",slowlines.ToArray());
    }
  }
}
