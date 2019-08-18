using System;
using System.Collections.Generic;
using System.Text;

namespace XU_NemDistance {
  public static class Utils {
    private static readonly Random _rnd = new Random();
    private static readonly List<string> _letters = new List<string>() {
      "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"
    };

    public static List<(string word1, string word2)> GenerateWordPairs(int count) {
      List<(string word1, string word2)> words = new List<(string word1, string word2)>();

      for (int i = 0; i < count; i++) {
        string word1 = GenerateRandomString();
        string word2 = GenerateRandomString();

        words.Add((word1, word2));
      }

      return words;
    }

    public static List<(string phrase1, string phrase2)> GeneratePhrasePairs(int count) {
      List<(string phrase1, string phrase2)> words = new List<(string phrase1, string phrase2)>();

      for (int i = 0; i < count; i++) {
        string phrase1 = GenerateRandomPhrase();
        string phrase2 = GenerateRandomPhrase();

        words.Add((phrase1, phrase2));
      }

      return words;
    }

    public static string GenerateRandomString() {
      int length = _rnd.Next(3, 10); //Word length

      StringBuilder strBuilder = new StringBuilder();
      for (int j = 0; j < length; j++) {
        int rndLetter = _rnd.Next(_letters.Count);
        strBuilder.Append(_letters[rndLetter]);
      }

      return strBuilder.ToString();
    }

    public static string GenerateRandomPhrase() {
      int length = _rnd.Next(3, 101); //Phrase length

      List<string> words = new List<string>();
      for(int i = 0; i < length; i++) {
        words.Add(GenerateRandomString());
      }

      return string.Join(' ', words);
    }

  }
}
