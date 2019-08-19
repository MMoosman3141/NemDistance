# NemDistance

#### Description
This library provides methods to calculate minimum edit distance values using either character comparison or token comparison.
In addition to simple edit distance values you can also calculate percentage of similarity, error percentage, and accuracy percentages.

##### The EditDistance Class
The edit distance class is where all the work is done in the library.  It is a static class providing the following methods:
###### Character based methods
Character based methods compare strings character by character.

The methods are:
* MinEditDistance
  * This method provides a basic Edit Distance using one of 4 algorithms.
* SimilarityPercentage
  * This method provides a percentage of similarity between two strings.
* ErrorPercentage
  * This method provides an error rate calculation.  This assumes that the first string provided (expected) is correct.  The returned value is the percentage of difference from the actual string compared to the expected string.
* AccuracyPercentage
  * This method is the opposite of the ErrorPercentage calculation.  It assumes, again, that the first string provided (expected) is correct.  The return valus is the percentage of sameness from the actual string compared to the expected string.
###### Phrase based methods
Phrase based methods compare strings on a token by token basis rather than a character by character basis.
Each method has two versions:
1. A version which takes a simple string.  The string is tokenized, into an IEnumerable of strings which are then evaluated.
2. A version which takes an IEnumerable of strings.  These methods allow you to handle tokenization yourself.

The methods are:
* PhraseEditDistance
  * This methods provides a basic Edit Distance using of of 4 algorithms.
* PhraseSimilarityPercentage
  * This method provides a percentage of similarity between two phrases.
* PhraseErrorPercentage
  * This method provides an error rate calculation.  This assumes that the first phraseg provided (expected) is correct.  The returned value is the percentage of difference from the actual phrase compared to the expected phrase.
* PhraseAccuracyPercentage
  * This method is the opposite of the ErrorPercentage calculation.  It assumes, again, that the first phrase provided (expected) is correct.  The return valus is the percentage of sameness from the actual phrase compared to the expected phrase.
###### Algorithm methods
* LevenshteinDistance
  * The Levenshtein distance method determines the amount of change required to change 1 phrase/string to the other.  This is handled through determineing the minimum number of insertions, deletions, or substitutions required.
* ModHammingDistance
  * The Hamming distance algorithm normally requires strings/phrases to be of equal length, and then simply counts the number substitutions required.  This version is a modified version which allows strings to be of different length.  The difference in length is treated as required additions.
* DemerauLevenshteinDistance
  * The Demerau-Levenshtein algorithm performs a standard Levenshtein distance, but treats transpositions as a single change rather than two changes.

##### Extension methods
Extension methods are provided to work with strings.

The following extension methods are available:
* ToStrings
  * This method converts a string to an IEnumerable of strings containing a string value for each character in the source string.
* Tokenize
  * This method tokenizes a string to an IEnumerable of strings.  Word characters are kept together in tokens, as are word internal apostrophes, hyphens and underscores.  Non-word characters result in new tokens.
* MinEditDistance
  * This method performs an edit distance comparing on a character basis.
* SimilarityPercentage
  * This method retrieves the similarity percentage of the source compared with another string on a character basis.
* ErrorPercentage
  * This method retrieves the percentage of difference of a string compared to the source on a character basis.
* AccuracyPercentage
  * This method retrieves the percentage of sameness of a string compared to the source on a character basis.
* PhraseMinEditDistance
  * This method performs an edit distance comparing on a token basis.
* PhraseSimilarityPercentage
  * This method retrieves the similarity percentage of the source compared with another string on a token by token basis.
* PhraseErrorPercentage
  * This method retrieves the percentage of difference of a stirng compared to the source on a token by token basis.
* PhraseAccuracyPercentage
  * This method retieves the percentage of sameness of a string compared to the source on a token by token basis.