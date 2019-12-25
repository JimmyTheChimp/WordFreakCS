using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class WordVerifier
  {
    private WordCombiner _wordCombiner;

    public WordVerifier()
    {
      _wordCombiner = new WordCombiner();
    }

    public Dictionary<string, WordInfo> CombineToVerifiedWords(HashSet<string> verifiedWords, HashSet<string> knownWords,
      Dictionary<string, WordInfo> wordDict)
    {
      var wordList = wordDict.Keys.OrderByDescending(k => k.Length).ToList();

      foreach (var word in wordList)
      {
        var verifiedWord = verifyWord(verifiedWords, word);

        if (verifiedWord != null)
        {
          if (!wordDict.ContainsKey(verifiedWord))
            wordDict.Add(verifiedWord, new WordInfo() { Word = verifiedWord, Count = 0, Verified = true });
          else
            wordDict[verifiedWord].Verified = true;

          _wordCombiner.combineWord(wordDict, word, verifiedWord);
        }
      }

      return wordDict;

      // For Each word
      // Check if word exists in verifiedWords
      // Try each ending
      // If ending matches create new item 
      // for Each new item recurse until verified word is found

      // If verified word found
      // Combine original word to verified Word and add counts

      // If verified word not found
      // Use original word and count
    }

    private string verifyWord(HashSet<string> verifiedWords, string word)
    {
      if (verifiedWords.Contains(word))
        return word;

      var verifiedEylem = verifyWordEylem(verifiedWords, word);

      if (verifiedEylem != null)
        return verifiedEylem;

      var verifiedIsim = verifyWordIsim(verifiedWords, word);

      return verifiedIsim;
    }

    private string verifyWordIsim(HashSet<string> verifiedWords, string isim)
    {
      //if (_wordCombiner.matchesRule(WordCombiner.patternI, isim))
      //{
      //  var newIsim = _wordCombiner.baseRule(WordCombiner.patternI, isim);

      //  if (verifiedWords.Contains(newIsim))
      //    return newIsim;

      //  var baseIsim = verifyWordIsim(verifiedWords, newIsim);

      //  if (baseIsim != null)
      //    return baseIsim;
      //}
      string newIsim = null;

      foreach (var patternAndReplace in WordCombiner.IsimPatterns)
      {
        newIsim = verifyWordPattern(verifiedWords, patternAndReplace, isim, verifyWordIsim);

        if (newIsim != null)
        {
          if (newIsim.EndsWith("me") && verifiedWords.Contains(newIsim.Replace("me", "mek")))
            return newIsim.Replace("me", "mek");
          if (newIsim.EndsWith("ma") && verifiedWords.Contains(newIsim.Replace("ma", "mak")))
            return newIsim.Replace("ma", "mak");

          return newIsim;
        }
      }

      return null;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternI,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternDEN,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternDE,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternE,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternLER,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternILE,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternIN,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternKI,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternIKEN,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternMIS,"", isim);

      //if (newIsim != null)
      //  return newIsim;

      //newIsim = verifyWordPattern(verifiedWords, WordCombiner.patternDI, "", isim);

      //return newIsim;
    }

    private string verifyWordEylem(HashSet<string> verifiedWords, string eylem)
    {
      string newEylem = null;

      foreach (var patternAndReplace in WordCombiner.EylemPatterns)
      {
        newEylem = verifyWordPattern(verifiedWords, patternAndReplace, eylem, verifyWordEylem);

        if (newEylem != null)
          return newEylem;
      }

      return null;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternIYOR,"", eylem);

      //if (newEylem != null)
      //  return newEylem;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternGENIS, "", eylem);

      //if (newEylem != null)
      //  return newEylem;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternEREK, "", eylem);

      //if (newEylem != null)
      //  return newEylem;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternIKEN, "", eylem);

      //if (newEylem != null)
      //  return newEylem;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternEN, "", eylem);

      //if (newEylem != null)
      //  return newEylem;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternECEK, "", eylem);

      //if (newEylem != null)
      //  return newEylem;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternEBIL, "", eylem);

      //if (newEylem != null)
      //  return newEylem;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternMIS, "", eylem);

      //if (newEylem != null)
      //  return newEylem;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternDI, "", eylem);

      //if (newEylem != null)
      //  return newEylem;

      //newEylem = verifyWordPattern(verifiedWords, WordCombiner.patternIP, "", eylem);

      //return newEylem;
    }

    private string verifyWordPattern(HashSet<string> verifiedWords, WordCombiner.PatternAndReplace patternAndReplace, string word, Func<HashSet<string>, string, string> recurseFunc)
    {
      var pattern = patternAndReplace.Pattern;
      var replacement = patternAndReplace.Replace;

      if (_wordCombiner.matchesRule(pattern, word))
      {
        var newWord = _wordCombiner.baseReplaceRule(pattern, replacement, word);

        if (verifiedWords.Contains(newWord))
          return newWord;

        var baseWord = recurseFunc(verifiedWords, newWord);

        return baseWord;
      }

      return null;
    }

  }
}
