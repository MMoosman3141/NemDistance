using NemDistance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace XU_NemDistance {
  public class UnitTestPhrasePerformance {
    private readonly ITestOutputHelper output;

    public UnitTestPhrasePerformance(ITestOutputHelper output) {
      this.output = output;
    }

    [Fact]
    public void TestLevenshteinPerformance() {
      int numPairs = 100_000;
      List<(string phrase1, string phrase2)> phrasePairs = Utils.GenerateWordPairs(numPairs);

      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      foreach ((string phrase1, string phrase2) in phrasePairs) {
        phrase1.PhraseMinEditDistance(phrase2); //We don't care about the actuall distance, we just are timing how long it takes to run a lot.
      }

      stopwatch.Stop();
      Assert.True(stopwatch.ElapsedMilliseconds < numPairs * 500);
      output.WriteLine($"Actual elapsed time: {stopwatch.ElapsedMilliseconds:N0} milliseconds");
    }

    [Fact]
    public void TestHammingPerformance() {
      int numPairs = 100_000;
      List<(string phrase1, string phrase2)> phrasePairs = Utils.GenerateWordPairs(numPairs);

      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      foreach ((string phrase1, string phrase2) in phrasePairs) {
        phrase1.PhraseMinEditDistance(phrase2, Algorithms.Hamming); //We don't care about the actuall distance, we just are timing how long it takes to run a lot.
      }

      stopwatch.Stop();
      Assert.True(stopwatch.ElapsedMilliseconds < numPairs * 500);
      output.WriteLine($"Actual elapsed time: {stopwatch.ElapsedMilliseconds:N0} milliseconds");
    }

    [Fact]
    public void TestDemerauLevenshteinPerformance() {
      int numPairs = 100_000;
      List<(string phrase1, string phrase2)> phrasePairs = Utils.GenerateWordPairs(numPairs);

      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      foreach ((string phrase1, string phrase2) in phrasePairs) {
        phrase1.PhraseMinEditDistance(phrase2, Algorithms.DemerauLevenshtein); //We don't care about the actuall distance, we just are timing how long it takes to run a lot.
      }

      stopwatch.Stop();
      Assert.True(stopwatch.ElapsedMilliseconds < numPairs * 500);
      output.WriteLine($"Actual elapsed time: {stopwatch.ElapsedMilliseconds:N0} milliseconds");
    }

    [Fact]
    public void TestNonePerformance() {
      int numPairs = 100_000;
      List<(string phrase1, string phrase2)> phrasePairs = Utils.GenerateWordPairs(numPairs);

      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();

      foreach ((string phrase1, string phrase2) in phrasePairs) {
        phrase1.PhraseMinEditDistance(phrase2, Algorithms.None); //We don't care about the actuall distance, we just are timing how long it takes to run a lot.
      }

      stopwatch.Stop();
      Assert.True(stopwatch.ElapsedMilliseconds < numPairs * 500);
      output.WriteLine($"Actual elapsed time: {stopwatch.ElapsedMilliseconds:N0} milliseconds");
    }
  }
}
