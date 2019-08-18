﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NemDistance {
  /// <summary>
  /// A static class with methods to find an edit distance using various algorithms.
  /// </summary>
  public static class EditDistance {
    /// <summary>
    /// Gets the Minimum Edit distance using the algorithm specified.
    /// Comparison is performed on a character level
    /// </summary>
    /// <param name="text1">The first of 2 strings to compare to each other</param>
    /// <param name="text2">The second of 2 strings to compare to each other</param>
    /// <param name="algorithm">The type of algorithm to use to calculate the edit distance</param>
    /// <returns>The minimum edit distance of the 2 strings using the specified algorithm</returns>
    public static int MinEditDistance(string text1, string text2, Algorithms algorithm = Algorithms.Levenshtein) {
      switch(algorithm) {
        case Algorithms.None:
          return StringDistance(text1.ToStrings(), text2.ToStrings());
        case Algorithms.Hamming:
          return ModHammingDistance(text1.ToStrings(), text2.ToStrings(), 1);
        case Algorithms.DemerauLevenshtein:
          return DemerauLevenshteinDistance(text1.ToStrings(), text2.ToStrings(), 1, 1);
        default:
        case Algorithms.Levenshtein:
          return LevenshteinDistance(text1.ToStrings(), text2.ToStrings(), 1, 1);
      }
    }

    /// <summary>
    /// Gets the percentage that the 2 strings are similar to each other
    /// The comparison is performed on a character level
    /// </summary>
    /// <param name="text1">The first of 2 strings to compare to each other</param>
    /// <param name="text2">The second of 2 strings to compare to each other</param>
    /// <param name="algorithm">The type of algorithm to use to calculate the edit distance</param>
    /// <returns>The percentage that the 2 strings are similar to each other</returns>
    public static decimal SimilarityPercentage(string text1, string text2, Algorithms algorithm = Algorithms.Levenshtein) {
      decimal difference = MinEditDistance(text1, text2, algorithm);
      decimal length = Math.Max(text1.Length, text2.Length);

      return (1m - (difference / length)) * 100m;
    }

    /// <summary>
    /// Gets the percentage that the actual text is different from the expected text
    /// The comparison is performed on a character level
    /// </summary>
    /// <param name="expectedText">The expected text</param>
    /// <param name="actualText">The actual text</param>
    /// <param name="algorithm">The type of algorithm to use to calculate the edit distance</param>
    /// <returns>The percentage that the actual text is different from the expected text</returns>
    public static decimal ErrorPercentage(string expectedText, string actualText, Algorithms algorithm = Algorithms.Levenshtein) {
      decimal difference = MinEditDistance(expectedText, actualText, algorithm);
      decimal length = expectedText.Length;

      return (difference / length) * 100m;
    }

    /// <summary>
    /// Gets the percentage that the actual text is different from the expected text
    /// The comparison is performed on a character level
    /// </summary>
    /// <param name="expectedText">The expected text</param>
    /// <param name="actualText">The actual text</param>
    /// <param name="algorithm">The type of algorithm to use to calculate the edit distance</param>
    /// <returns>The percentage that the actual text is different from the expected text</returns>
    public static decimal AccuracyPercentage(string expectedText, string actualText, Algorithms algorithm = Algorithms.Levenshtein) {
      return 100m - ErrorPercentage(expectedText, actualText, algorithm);
    }

    /// <summary>
    /// Gets the Minimum Edit distance using the algorithm specified.
    /// Comparison is performed on a word level by first tokenizing the string values using the Tokenize extension method
    /// </summary>
    /// <param name="text1">The first of 2 strings to compare to each other</param>
    /// <param name="text2">The second of 2 strings to compare to each other</param>
    /// <param name="algorithm">The type of algorithm to use to calculate the edit distance</param>
    /// <returns>The minimum edit distance of the 2 strings using the specified algorithm</returns>
    public static int PhraseEditDistance(string text1, string text2, Algorithms algorithm = Algorithms.Levenshtein) {
      return PhraseEditDistance(text1.Tokenize(), text2.Tokenize(), algorithm);
    }

    /// <summary>
    /// Gets the Minimum Edit distance using the algorithm specified.
    /// Comparison is performed on a string level, treating each string in the IEnumerables seperately
    /// This method allow gettings the edit distance using your own tokenizer
    /// </summary>
    /// <param name="tokens1">The first of 2 IEnumerable&lt;string&gt; to compare to each other</param>
    /// <param name="tokens2">The second of 2 IEnumerable&lt;string&gt; to compare to each other</param>
    /// <param name="algorithm">The type of algorithm to use to calculate the edit distance</param>
    /// <returns>The minimum edit distance of the 2 values using the specified algorithm</returns>
    public static int PhraseEditDistance(IEnumerable<string> tokens1, IEnumerable<string> tokens2, Algorithms algorithm = Algorithms.Levenshtein) {
      switch (algorithm) {
        case Algorithms.None:
          return StringDistance(tokens1, tokens2);
        case Algorithms.Hamming:
          return ModHammingDistance(tokens1, tokens2, 1);
        case Algorithms.DemerauLevenshtein:
          return DemerauLevenshteinDistance(tokens1, tokens2, 1, 1);
        default:
        case Algorithms.Levenshtein:
          return LevenshteinDistance(tokens1, tokens2, 1, 1);
      }
    }

    /// <summary>
    /// Gets the percentage that the 2 phrases are similar to each other
    /// Comparison is performed on a word level by first tokenizing the string values using the Tokenize extension method
    /// </summary>
    /// <param name="text1">The first of 2 strings to compare to each other</param>
    /// <param name="text2">The second of 2 strings to compare to each other</param>
    /// <param name="algorithm">The type of algorithm to use to calculate the edit distance</param>
    /// <returns>The percentage that the 2 strings are similar to each other</returns>
    public static decimal PhraseSimilarityPercentage(string text1, string text2, Algorithms algorithm = Algorithms.Levenshtein) {
      IEnumerable<string> tokens1 = text1.Tokenize();
      IEnumerable<string> tokens2 = text2.Tokenize();

      return PhraseSimilarityPercentage(tokens1, tokens2, algorithm);
    }

    /// <summary>
    /// Gets the percentage that the 2 sets of strings are similar to each other
    /// Comparison is performed on a string level allowing you to perform your own tokenization
    /// </summary>
    /// <param name="tokens1">The first of 2 IEnumerable&lt;string&gt; values to compare to each other</param>
    /// <param name="tokens2">The second of 2 IEnumerable&lt;string&gt; values to compare to each other</param>
    /// <param name="algorithm">The type of algorithm to use to calculate the edit distance</param>
    /// <returns>The percentage that the 2 values are similar to each other</returns>
    public static decimal PhraseSimilarityPercentage(IEnumerable<string> tokens1, IEnumerable<string> tokens2, Algorithms algorithm = Algorithms.Levenshtein) {
      decimal difference = PhraseEditDistance(tokens1, tokens2, algorithm);
      decimal length = Math.Max(tokens1.Count(), tokens2.Count());

      return (1m - (difference / length)) * 100m;
    }

    /// <summary>
    /// Gets the percentage of difference that the actual text is from the expected text
    /// Comparison is performed on a word level by first tokenizing the string values
    /// </summary>
    /// <param name="expectedText">The expected text</param>
    /// <param name="actualText">The actual text</param>
    /// <param name="algorithm">The type of algorithm to use when calculating the edit distance</param>
    /// <returns>The percentage that the actual text is different from the expected text</returns>
    public static decimal PhraseErrorPercentage(string expectedText, string actualText, Algorithms algorithm = Algorithms.Levenshtein) {
      IEnumerable<string> tokens1 = expectedText.Tokenize();
      IEnumerable<string> tokens2 = actualText.Tokenize();

      return PhraseErrorPercentage(tokens1, tokens2, algorithm);
    }

    /// <summary>
    /// Gets the percentage of difference that the actual tokens are from the expected tokens
    /// Comparison is performed on a string level allowing custom tokenization
    /// </summary>
    /// <param name="expectedTokens">The expected tokens</param>
    /// <param name="actualTokens">The actual tokens</param>
    /// <param name="algorithm">The type of algorithm to use when calculating the edit distance</param>
    /// <returns>The percentage that the actual tokens are different from the expected tokens</returns>
    public static decimal PhraseErrorPercentage(IEnumerable<string> expectedTokens, IEnumerable<string> actualTokens, Algorithms algorithm = Algorithms.Levenshtein) {
      decimal difference = PhraseEditDistance(expectedTokens, actualTokens, algorithm);
      decimal length = expectedTokens.Count();

      return (difference / length) * 100m;
    }

    /// <summary>
    /// Gets the percentage of sameness that the actual text is from the expected text
    /// Comparison is performed on a word level by first tokenizing the string values
    /// </summary>
    /// <param name="expectedText">The expected text</param>
    /// <param name="actualText">The actual text</param>
    /// <param name="algorithm">The type of algorithm to use when calculating the edit distance</param>
    /// <returns>The percentage that the actual text is the same as the expected text</returns>
    public static decimal PhraseAccuracyPercentage(string expectedText, string actualText, Algorithms algorithm = Algorithms.Levenshtein) {
      return 100m - PhraseErrorPercentage(expectedText, actualText, algorithm);
    }

    /// <summary>
    /// Gets the percentage of sameness that the actual tokens are from the expected tokens
    /// Comparison is performed on a string level allowing custom tokenization
    /// </summary>
    /// <param name="expectedTokens">The expected tokens</param>
    /// <param name="actualTokens">The actual tokens</param>
    /// <param name="algorithm">The type of algorithm to use when calculating the edit distance</param>
    /// <returns>The percentage that the actual tokens are the same as the expected tokens</returns>
    public static decimal PhraseAccuracyPercentage(IEnumerable<string> expectedTokens, IEnumerable<string> actualTokens, Algorithms algorithm = Algorithms.Levenshtein) {
      return 100m - PhraseErrorPercentage(expectedTokens, actualTokens, algorithm);
    }

    /// <summary>
    /// Calculates the Levenshtein distance of 2 strings
    /// </summary>
    /// <param name="text1">The first of 2 strings to compare</param>
    /// <param name="text2">The second of 2 strings to compare</param>
    /// <param name="addDeleteWeight">The weight to apply to additions and deletions</param>
    /// <param name="substituteWeight">The weight to apply to substitutions</param>
    /// <returns>The Levenshtein distance of 2 strings</returns>
    public static int LevenshteinDistance(string text1, string text2, int addDeleteWeight, int substituteWeight) {
      return LevenshteinDistance(text1.ToStrings(), text2.ToStrings(), addDeleteWeight, substituteWeight);
    }

    /// <summary>
    /// Calculates the Levenshtein distance of 2 lists of strings
    /// </summary>
    /// <param name="text1">The first of 2 lists of strings to compare</param>
    /// <param name="text2">The second of 2 lists of strings to compare</param>
    /// <param name="addDeleteWeight">The weight to apply to additions and deletions</param>
    /// <param name="substituteWeight">The weight to apply to substitutions</param>
    /// <returns>The Levenshtein distance of 2 lists of strings</returns>
    public static int LevenshteinDistance(IEnumerable<string> text1, IEnumerable<string> text2, int addDeleteWeight, int substituteWeight) {
      if ((text1?.Count() ?? 0) == 0) {
        return text2?.Count() ?? 0;
      } else if ((text2?.Count() ?? 0) == 0) {
        return text1?.Count() ?? 0;
      }

      if (text1.Count() > text2.Count()) {
        IEnumerable<string> temp = text2;
        text2 = text1;
        text1 = temp;
      }

      int len1 = text1.Count();
      int len2 = text2.Count();

      int[,] distArray = new int[2, len1 + 1];
      for (int j = 1; j < len1; j++) {
        distArray[0, j] = j;
      }

      int currRow = 0;
      for (int i = 1; i < len2; i++) {
        currRow = i & 1;
        int prevRow = currRow ^ 1;
        distArray[currRow, 0] = i;

        for (int j = 1; j < len1; j++) {
          int cost = text2.ElementAt(j - 1) == text1.ElementAt(i - 1) ? 0 : substituteWeight;
          distArray[currRow, j] = Math.Min(Math.Min(distArray[prevRow, j] + addDeleteWeight, distArray[currRow, j - 1] + addDeleteWeight), distArray[prevRow, j - 1] + cost);
        }
      }

      return distArray[currRow, len2];
    }

    /// <summary>
    /// Calculates a modified Hamming distance of 2 strings
    /// The modification allows comparison of strings that are not of equal length, by treating all extra characters as additions
    /// </summary>
    /// <param name="text1">The first of 2 strings to compare</param>
    /// <param name="text2">The second of 2 strings to compare</param>
    /// <param name="substituteWeight">The weight to apply to substitutions</param>
    /// <returns>The modified Hamming distance of 2 strings</returns>
    public static int ModHammingDistance(string text1, string text2, int substituteWeight) {
      return ModHammingDistance(text1.ToStrings(), text2.ToStrings(), substituteWeight);
    }

    /// <summary>
    /// Calculates a modified Hamming distance of 2 lists of strings
    /// The modification allows comparison of lists that are not of equal length, by treating all extra strings as additions
    /// </summary>
    /// <param name="text1">The first of 2 lists of strings to compare</param>
    /// <param name="text2">The second of 2 lists of strings to compare</param>
    /// <param name="substituteWeight">The weight to apply to substitutions</param>
    /// <returns>The modified Hamming distance of 2 lists of strings</returns>
    public static int ModHammingDistance(IEnumerable<string> text1, IEnumerable<string> text2, int substituteWeight) {
      int length = Math.Min(text1.Count(), text2.Count());

      int diff = StringDistance(text1, text2);
      for (int i = 0; i < length; i++) {
        if (text1.ElementAt(i) != text2.ElementAt(i)) {
          diff += substituteWeight;
        }
      }

      return diff;

    }

    /// <summary>
    /// Calculates the Demerau-Levenshtein distance of 2 strings
    /// </summary>
    /// <param name="text1">The first of 2 strings to compare</param>
    /// <param name="text2">The second of 2 strings to compare</param>
    /// <param name="addDeleteWeight">The weight to apply to additions and deletions</param>
    /// <param name="substituteTransposeWeight">The weight to apply to substitutions and transpositions</param>
    /// <returns>The Demerau-Levenshtein distance of 2 strings</returns>
    public static int DemerauLevenshteinDistance(string text1, string text2, int addDeleteWeight, int substituteTransposeWeight) {
      return DemerauLevenshteinDistance(text1.ToStrings(), text2.ToStrings(), addDeleteWeight, substituteTransposeWeight);
    }

    /// <summary>
    /// Calculates the Demerau-Levenshtein distance of 2 lists of strings
    /// </summary>
    /// <param name="text1">The first of 2 lists of strings to compare</param>
    /// <param name="text2">The second of 2 lists of strings to compare</param>
    /// <param name="addDeleteWeight">The weight to apply to additions and deletions</param>
    /// <param name="substituteTransposeWeight">The weight to apply to substitutions and transpositions/param>
    /// <returns>The Demerau-Levenshtein distance of 2 lists of strings</returns>
    public static int DemerauLevenshteinDistance(IEnumerable<string> text1, IEnumerable<string> text2, int addDeleteWeight, int substituteTransposeWeight) {
      if ((text1?.Count() ?? 0) == 0) {
        return text2?.Count() ?? 0;
      } else if ((text2?.Count() ?? 0) == 0) {
        return text1?.Count() ?? 0;
      }

      int len1 = text1.Count();
      int len2 = text2.Count();

      int[,] distArray = new int[len1, len2];

      for (int i = 0; i < len1; i++) {
        distArray[i, 0] = i;
      }
      for (int j = 0; j < len2; j++) {
        distArray[0, j] = j;
      }

      for (int i = 1; i < len1; i++) {
        for (int j = 1; j < len2; i++) {
          int cost = text1.ElementAt(i - 1) == text2.ElementAt(i - 1) ? 0 : substituteTransposeWeight;
          int add = distArray[i, j - 1] + addDeleteWeight;
          int del = distArray[i - 1, j] + addDeleteWeight;
          int sub = distArray[i - 1, j - 1] + cost;

          int dist = Math.Min(Math.Min(add, del), sub);

          if (i > 1 && j > 1 && text1.ElementAt(i - 1) == text2.ElementAt(j - 2) && text1.ElementAt(i - 2) == text2.ElementAt(j - 1)) {
            dist = Math.Min(dist, distArray[i - 2, j - 2] + cost);
          }

          distArray[i, j] = dist;
        }
      }

      return distArray[len1 - 1, len2 - 1];


    }


    private static int StringDistance(IEnumerable<string> text1, IEnumerable<string> text2) {
      return Math.Abs(text1.Count() - text2.Count());
    }

    

    

  } //class
} //namespace
