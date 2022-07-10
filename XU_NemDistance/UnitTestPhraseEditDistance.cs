namespace XU_NemDistance; 

public class UnitTestPhraseEditDistance {
  [Theory]
  [InlineData("The little house on the prairie.", "The big tiger in the zoo.", 4)]
  [InlineData("This is a test of the emergency broadcast system.  It is only a test.", "This test is of the emergency broadcast system.  It is only a simple test.", 4)]
  [InlineData("The quick brown fox jumps over the lazey dog.", "Now is the time for all good mean to come to the aid of their country", 14)]
  [InlineData("We the people of the United States.", "We the people the of United States.", 2)]
  public void TestLevenshtain(string text1, string text2, int dist) {
    Assert.Equal(dist, EditDistance.PhraseEditDistance(text1, text2));
  }

  [Theory]
  [InlineData("The little house on the prairie.", "The big tiger in the zoo.", 4)]
  [InlineData("This is a test of the emergency broadcast system.  It is only a test.", "This test is of the emergency broadcast system.  It is only a simple test.", 13)]
  [InlineData("The quick brown fox jumps over the lazey dog.", "Now is the time for all good mean to come to the aid of their country", 15)]
  [InlineData("We the people of the United States.", "We the people the of United States.", 2)]
  public void TestHamming(string text1, string text2, int dist) {
    Assert.Equal(dist, EditDistance.PhraseEditDistance(text1, text2, Algorithms.Hamming));
  }

  [Theory]
  [InlineData("The little house on the prairie.", "The big tiger in the zoo.", 4)]
  [InlineData("This is a test of the emergency broadcast system.  It is only a test.", "This test is of the emergency broadcast system.  It is only a simple test.", 4)]
  [InlineData("The quick brown fox jumps over the lazey dog.", "Now is the time for all good mean to come to the aid of their country", 14)]
  [InlineData("We the people of the United States.", "We the people the of United States.", 1)]
  public void TestDemerauLevenshtain(string text1, string text2, int dist) {
    Assert.Equal(dist, EditDistance.PhraseEditDistance(text1, text2, Algorithms.DemerauLevenshtein));
  }


  [Theory]
  [InlineData("The little house on the prairie.", "The big tiger in the zoo.", 0)]
  [InlineData("This is a test of the emergency broadcast system.  It is only a test.", "This test is of the emergency broadcast system.  It is only a simple test.", 0)]
  [InlineData("The quick brown fox jumps over the lazey dog.", "Now is the time for all good mean to come to the aid of their country", 6)]
  [InlineData("We the people of the United States.", "We the people the of United States.", 0)]
  public void TestNone(string text1, string text2, int dist) {
    Assert.Equal(dist, EditDistance.PhraseEditDistance(text1, text2, Algorithms.None));
  }
}
