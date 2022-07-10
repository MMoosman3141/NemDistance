namespace XU_NemDistance; 

public class UnitTestEditDistance {
  [Theory]
  [InlineData("mark", "market", 2)]
  [InlineData("simple", "pimple", 1)]
  [InlineData("quick", "quickly", 2)]
  [InlineData("exonerate", "prosecute", 5)]
  [InlineData("good", "bad", 3)]
  [InlineData("street", "strait", 2)]
  [InlineData("ambidextrous", "dextrose", 6)]
  [InlineData("perfectionist", "supremacist", 9)]
  [InlineData("benedict", "marry", 8)]
  [InlineData("market", "markte", 2)]
  public void TestLevenshtain(string text1, string text2, int dist) {
    Assert.Equal(dist, EditDistance.MinEditDistance(text1, text2));
  }

  [Theory]
  [InlineData("mark", "market", 2)]
  [InlineData("simple", "pimple", 1)]
  [InlineData("quick", "quickly", 2)]
  [InlineData("exonerate", "prosecute", 5)]
  [InlineData("good", "bad", 4)]
  [InlineData("street", "strait", 2)]
  [InlineData("ambidextrous", "dextrose", 12)]
  [InlineData("perfectionist", "supremacist", 12)]
  [InlineData("benedict", "marry", 8)]
  public void TestHamming(string text1, string text2, int dist) {
    Assert.Equal(dist, EditDistance.MinEditDistance(text1, text2, Algorithms.Hamming));
  }

  [Theory]
  [InlineData("mark", "market", 2)]
  [InlineData("simple", "pimple", 1)]
  [InlineData("quick", "quickly", 2)]
  [InlineData("exonerate", "prosecute", 5)]
  [InlineData("good", "bad", 3)]
  [InlineData("street", "strait", 2)]
  [InlineData("ambidextrous", "dextrose", 6)]
  [InlineData("perfectionist", "supremacist", 9)]
  [InlineData("benedict", "marry", 8)]
  [InlineData("market", "markte", 1)]
  public void TestDemerauLevenshtain(string text1, string text2, int dist) {
    Assert.Equal(dist, EditDistance.MinEditDistance(text1, text2, Algorithms.DemerauLevenshtein));
  }


  [Theory]
  [InlineData("mark", "market", 2)]
  [InlineData("simple", "pimple", 0)]
  [InlineData("quick", "quickly", 2)]
  [InlineData("exonerate", "prosecute", 0)]
  [InlineData("good", "bad", 1)]
  [InlineData("street", "strait", 0)]
  [InlineData("ambidextrous", "dextrose", 4)]
  [InlineData("perfectionist", "supremacist", 2)]
  [InlineData("benedict", "marry", 3)]
  public void TestNone(string text1, string text2, int dist) {
    Assert.Equal(dist, EditDistance.MinEditDistance(text1, text2, Algorithms.None));
  }

}
