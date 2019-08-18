using System;
using System.Collections.Generic;
using System.Text;

namespace NemDistance {
  /// <summary>
  /// These are the allowable algorithms to be used when calculating the edit distance.
  /// </summary>
  public enum Algorithms {
    /// <summary>
    /// No edit distance algorithm is used.  Distance is based on string length.
    /// </summary>
    None = 0,
    /// <summary>
    /// In the Levenshtein distance, uses simple addition, substitution, and deletion between strings.
    /// </summary>
    Levenshtein = 1,
    /// <summary>
    /// The Hamming distance compares only string of equal length.
    /// </summary>
    Hamming = 2,
    /// <summary>
    /// The Demerau-Levenshtein distance includes transpositions in it's opterations
    /// </summary>
    DemerauLevenshtein = 3
  }
}
