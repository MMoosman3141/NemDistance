using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NemDistance {
  public static class Extensions {
    /// <summary>
    /// Converts a string to an IEnumerable of strings containing the individual characters
    /// </summary>
    /// <param name="source">The string to split</param>
    /// <returns>An IEnumerable of the strings for each character</returns>
    public static IEnumerable<string> ToStrings(this string source) {
      return source.ToCharArray().Select(c => c.ToString());
    }

    /// <summary>
    /// Tokenizes a phrase into separate words, and punctuation
    /// For word internal punctuation of apostrophes, hyphens, and underscores the characters will be maintained
    /// as part of the word if word characters appear on both sides of the punctuation
    /// </summary>
    /// <param name="text">The text to tokenize</param>
    /// <returns>An IEnumerable of string representing the tokenized text</returns>
    public static IEnumerable<string> Tokenize(this string text) {
      StringBuilder tokenBuilder = new StringBuilder();
      Regex notWordCharRgx = new Regex(@"\W", RegexOptions.Compiled);

      for (int i = 0; i < text.Length; i++) {
        string prev = i > 0 ? text[i].ToString() : null;
        string curr = text[i].ToString();
        string next = i < text.Length - 1 ? text[i + 1].ToString() : null;

        tokenBuilder.Append(curr);

        if (curr == "'" || curr == "-" || curr == "_") {
          if (notWordCharRgx.IsMatch(prev) || notWordCharRgx.IsMatch(next)) {
            string token = tokenBuilder.ToString();
            tokenBuilder = new StringBuilder();
            yield return token;
          }
        } else if (notWordCharRgx.IsMatch(curr)) {
          string token = tokenBuilder.ToString();
          tokenBuilder = new StringBuilder();
          yield return token;
        }
      }
    }

    /// <summary>
    /// Gets the Minimum edit distance of comparing the source to another string using the specified algorithm
    /// </summary>
    /// <param name="source">The source text to use in the comparison</param>
    /// <param name="text">The other text to use in the comparison</param>
    /// <param name="algorithm">The edit distance algorithm to use.  Defaults to Levenshtein</param>
    /// <returns>The minimum edit distance using the specified algorithm</returns>
    public static int MinEditDistance(this string source, string text, Algorithms algorithm = Algorithms.Levenshtein) {
      return EditDistance.MinEditDistance(source, text, algorithm);
    }

    /// <summary>
    /// Gets the percentage that the text is similar to this text
    /// </summary>
    /// <param name="source">This text</param>
    /// <param name="text">The text to use as a comparison</param>
    /// <param name="algorithm">The edit distance algorithm to use.  Defaults to Levenshtein.</param>
    /// <returns>The percentage of similarity</returns>
    public static decimal SimilarityPercentage(this string source, string text, Algorithms algorithm = Algorithms.Levenshtein) {
      return EditDistance.SimilarityPercentage(source, text, algorithm);
    }

    /// <summary>
    /// Gets the percentage that the text is different from this text
    /// </summary>
    /// <param name="expected">This text</param>
    /// <param name="actual">The text being compared for differences</param>
    /// <param name="algorithm">The edit distance algorithm to use.  Defaults to Levenshtein.</param>
    /// <returns>The percentage difference</returns>
    public static decimal ErrorPercentage(this string expected, string actual, Algorithms algorithm = Algorithms.Levenshtein) {
      return EditDistance.ErrorPercentage(expected, actual, algorithm);
    }

    /// <summary>
    /// Gets the percentage that the text is the same as this text
    /// </summary>
    /// <param name="expected">This text</param>
    /// <param name="actual">The text being compared as the same</param>
    /// <param name="algorithm">The edit distance algorithm to use.  Defaults to Levenshtein.</param>
    /// <returns>The percentage of sameness</returns>
    public static decimal AccuracyPercentage(this string expected, string actual, Algorithms algorithm = Algorithms.Levenshtein) {
      return EditDistance.AccuracyPercentage(expected, actual, algorithm);
    }

    /// <summary>
    /// Gets the minimum edit distance of the text compared to this text.
    /// Tokenizes the strings and compares using word level comparisons
    /// </summary>
    /// <param name="source">This text</param>
    /// <param name="text">The text to compare to</param>
    /// <param name="algorithm">The edit distance algorithm to use.  Defaults to Levenshtein.</param>
    /// <returns>The minimum edit distance between the two phrases</returns>
    public static decimal PhraseMinEditDistance(this string source, string text, Algorithms algorithm = Algorithms.Levenshtein) {
      return EditDistance.PhraseEditDistance(source, text, algorithm);
    }

    /// <summary>
    /// Gets the amount of similarity between the text and this text.  This is the inverse of the PercentageError
    /// Tokenizes the strings and compares using word level comparisons
    /// </summary>
    /// <param name="source">This text</param>
    /// <param name="text">The text to compare to</param>
    /// <param name="algorithm">The edit distance algorithm to use.  Defaults to Levenshtein.</param>
    /// <returns>The percentage of similarity</returns>
    public static decimal PhraseSimilarityPercentage(this string source, string text, Algorithms algorithm = Algorithms.Levenshtein) {
      return EditDistance.PhraseSimilarityPercentage(source, text, algorithm);
    }

    /// <summary>
    /// Gets the amount that the text is different from this text
    /// Tokenizes the strings and compares using word level comparisons
    /// </summary>
    /// <param name="expected">This text</param>
    /// <param name="actual">The text to compare to</param>
    /// <param name="algorithm">The edit distance algorithm to use.  Defaults to Levenshtein.</param>
    /// <returns>The percentage that the text is different from expected</returns>
    public static decimal PhraseErrorPercentage(this string expected, string actual, Algorithms algorithm = Algorithms.Levenshtein) {
      return EditDistance.PhraseErrorPercentage(expected, actual, algorithm);
    }

    /// <summary>
    /// Gets the amounts that the text is the same from this text.  This is the inverse of the PercentageError.
    /// Tokenizes the strings and compares using word level comparisons
    /// </summary>
    /// <param name="expected">This text</param>
    /// <param name="actual">The text to compare to</param>
    /// <param name="algorithm">The edit distance algorithm to use.  Defaults to Levenshtein.</param>
    /// <returns>The percentage that the text is the same as expected.</returns>
    public static decimal PhraseAccuracyPercentage(this string expected, string actual, Algorithms algorithm = Algorithms.Levenshtein) {
      return EditDistance.PhraseAccuracyPercentage(expected, actual, algorithm);
    }


  }
}
