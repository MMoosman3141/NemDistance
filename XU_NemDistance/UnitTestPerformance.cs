using System.Collections.Generic;
using System.Diagnostics;
using Xunit.Abstractions;

namespace XU_NemDistance;

public class UnitTestPerformance {
  private readonly ITestOutputHelper output;

  public UnitTestPerformance(ITestOutputHelper output) {
    this.output = output;
  }

  [Fact]
  public void TestLevenshteinPerformance() {
    int numPairs = 100_000;
    List<(string word1, string word2)> wordPairs = Utils.GenerateWordPairs(numPairs);

    Stopwatch stopwatch = new();
    stopwatch.Start();

    foreach ((string word1, string word2) in wordPairs) {
      word1.MinEditDistance(word2); //We don't care about the actuall distance, we just are timing how long it takes to run a lot.
    }

    stopwatch.Stop();
    Assert.True(stopwatch.ElapsedMilliseconds < numPairs * 500);
    output.WriteLine($"Actual elapsed time: {stopwatch.ElapsedMilliseconds}");
  }

  [Fact]
  public void TestHammingPerformance() {
    int numPairs = 100_000;
    List<(string word1, string word2)> wordPairs = Utils.GenerateWordPairs(numPairs);

    Stopwatch stopwatch = new();
    stopwatch.Start();

    foreach ((string word1, string word2) in wordPairs) {
      word1.MinEditDistance(word2, Algorithms.Hamming); //We don't care about the actuall distance, we just are timing how long it takes to run a lot.
    }

    stopwatch.Stop();
    Assert.True(stopwatch.ElapsedMilliseconds < numPairs * 500);
    output.WriteLine($"Actual elapsed time: {stopwatch.ElapsedMilliseconds}");
  }

  [Fact]
  public void TestDemerauLevenshteinPerformance() {
    int numPairs = 100_000;
    List<(string word1, string word2)> wordPairs = Utils.GenerateWordPairs(numPairs);

    Stopwatch stopwatch = new();
    stopwatch.Start();

    foreach ((string word1, string word2) in wordPairs) {
      word1.MinEditDistance(word2, Algorithms.DemerauLevenshtein); //We don't care about the actuall distance, we just are timing how long it takes to run a lot.
    }

    stopwatch.Stop();
    Assert.True(stopwatch.ElapsedMilliseconds < numPairs * 500);
    output.WriteLine($"Actual elapsed time: {stopwatch.ElapsedMilliseconds}");
  }

  [Fact]
  public void TestNonePerformance() {
    int numPairs = 100_000;
    List<(string word1, string word2)> wordPairs = Utils.GenerateWordPairs(numPairs);

    Stopwatch stopwatch = new();
    stopwatch.Start();

    foreach ((string word1, string word2) in wordPairs) {
      word1.MinEditDistance(word2, Algorithms.None); //We don't care about the actuall distance, we just are timing how long it takes to run a lot.
    }

    stopwatch.Stop();
    Assert.True(stopwatch.ElapsedMilliseconds < numPairs * 500);
    output.WriteLine($"Actual elapsed time: {stopwatch.ElapsedMilliseconds}");
  }
}
