using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class WordCombiner
  {
    private static char[] FRONT_VOWELS = { 'e', 'i', 'ü', 'Ö' };
    private static char[] BACK_VOWELS = { 'a', 'ı', 'u', 'o' };


    // TODO: Possessive endings
    // TODO: Geniş zaman
    // TODO: Gelecek zaman

    public Dictionary<string, WordInfo> Combine(HashSet<string> knownWords, Dictionary<string, WordInfo> wordDict)
    {
      // Add known words with count 0, to help with combining
      knownWords.ToList().ForEach(k =>
      {
        if (!wordDict.ContainsKey(k))
          wordDict.Add(k, new WordInfo() { Word = k, Count = 0 });
      });

      int lastCount = wordDict.Count;
      bool firstRun = true;
      do
      {
        lastCount = wordDict.Count;

        runRules(wordDict, firstRun);
        firstRun = false;
      } while (wordDict.Count < lastCount);

      return wordDict;
    }

    private void runRules(Dictionary<string, WordInfo> wordDict, bool firstRun)
    {
      var words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternINCE, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternMAK, wordDict, w); });

      if (firstRun)
      {
        words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
        words.ForEach(w => { rule(patternDEN, wordDict, w); });
        //// döngü
        //for (int x = 0; x < words.Count; x++)
        //{
        //  rule(patternDEN, wordDict, wordDict[x]));
        //}

        words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
        words.ForEach(w => { rule(patternDE, wordDict, w); });

        words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
        words.ForEach(w => { rule(patternILE, wordDict, w); });

        words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
        words.ForEach(w => { rule(patternE, wordDict, w); });

        words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
        words.ForEach(w => { rule(patternEREK, wordDict, w); });
      }

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternLER, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternMIS, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternDI, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternIYOR, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternKI, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternIKEN, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternIN, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternI, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { rule(patternEN, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { replaceRule(patternReplaceG, replaceG, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { replaceRule(patternReplaceB, replaceB, wordDict, w); });

      words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();
      words.ForEach(w => { replaceRule(patternReplaceC, replaceC, wordDict, w); });
    }

    internal static Regex patternMAK = new Regex(@"(..+?)m[ae]k?$");

    internal static Regex patternI = new Regex(@"(..+?[^dt])[yns]?[iıuü]$");
    internal static Regex patternE = new Regex(@"(..+?)[yn]?[ea]$");
    internal static Regex patternDEN = new Regex(@"(..+?)[yn]?[td][ea]n$");
    internal static Regex patternDE = new Regex(@"(..+?)[yn]?[td][ea]$");
    internal static Regex patternLER = new Regex(@"(..+?)[yn]?l[ea]r$");

    internal static Regex patternKI = new Regex(@"(..+?)ki$");
    internal static Regex patternIKEN = new Regex(@"(..+?)[y]?ken$");
    internal static Regex patternINCE = new Regex(@"(..+?)([eiöü]nc[e])|([aiou]nc[a])$");
    internal static Regex patternIN = new Regex(@"(..+?)[n]?[iıüu]n$");

    internal static Regex patternEREK = new Regex(@"(..+?)arak|erek$");
    internal static Regex patternILE = new Regex(@"(..+?)y?l[ea]$");
    internal static Regex patternEN = new Regex(@"(..+?)y?[ea]n$");

    internal static Regex patternMIS = new Regex(@"(..+?)m[iıuü]ş([iıuü]m|s[iıuü]n|siniz|sınız|sunuz|sünüz|[iıuü]z)?$");
    internal static Regex patternDI = new Regex(@"(..+?)([dt][iıuü][mnk]?|[dt]iniz|[dt]ınız|[dt]unuz|[dt]ünüz)$");
    internal static Regex patternIYOR = new Regex(@"(..+?)m?[iüıu]yor$");

    internal static Regex patternReplaceG = new Regex(@"(..+?)ğ$");
    internal const string replaceG = "k";

    internal static Regex patternReplaceB = new Regex(@"(..+?)b$");
    internal const string replaceB = "p";

    internal static Regex patternReplaceC = new Regex(@"(..+?)c$");
    internal const string replaceC = "ç";


    private void rule(Regex pattern, Dictionary<string, WordInfo> wordDict, string word)
    {
      if (matchesRule(pattern, word))
      {
        string baseWord = baseRule(pattern, word);

        if (!wordDict.ContainsKey(baseWord))
        {
          wordDict.Add(baseWord, new WordInfo() { Word = baseWord, Count = 0 });
        }

        combineWord(wordDict, word, baseWord);
      }
    }

    internal bool matchesRule(Regex pattern, string word)
    {
      return pattern.IsMatch(word);
    }

    internal string baseRule(Regex pattern, string word)
    {
      return pattern.Match(word).Groups[1].ToString();
    }

    private void replaceRule(Regex pattern, string replacement, Dictionary<string, WordInfo> wordDict, string word)
    {
      if (matchesRule(pattern, word))
      {
        string baseWord = baseReplaceRule(pattern, replacement, word);

        if (!wordDict.ContainsKey(baseWord))
        {
          wordDict.Add(baseWord, new WordInfo() { Word = baseWord, Count = 0 });
        }

        combineWord(wordDict, word, baseWord);
      }
    }

    internal string baseReplaceRule(Regex pattern, string replacement, string word)
    {
      return pattern.Match(word).Groups[1].ToString() + replacement;
    }



    private void ruleE(Dictionary<string, WordInfo> wordDict, string word)
    {
      if (matchesRuleE(word))
      {
        string baseWord = baseRuleE(word);

        if (wordDict.ContainsKey(baseWord))
        {
          combineWord(wordDict, word, baseWord);
        }
      }
    }

    internal bool matchesRuleE(string word)
    {
      if (word.Length <= 2)
        return false;

      char lastLetter = word[word.Length - 1];

      if (lastLetter == 'e' || lastLetter == 'a')
      {
        return true;
      }

      return false;
    }

    internal string baseRuleE(string word)
    {
      return word.Substring(0, word.Length - 1);
    }

    private void ruleLER(Dictionary<string, WordInfo> wordDict, string word)
    {
      if (matchesRuleLER(word))
      {
        string baseWord = baseRuleLER(word);

        if (wordDict.ContainsKey(baseWord))
        {
          combineWord(wordDict, word, baseWord);
        }
      }
    }

    internal bool matchesRuleLER(string word)
    {
      if (word.Length < 5)
        return false;

      string ending = word.Substring(word.Length - 3);

      if (ending == "ler" || ending == "lar")
      {
        return true;
      }

      return false;
    }

    internal string baseRuleLER(string word)
    {
      return word.Substring(0, word.Length - 3);
    }

    private void ruleDEN(Dictionary<string, WordInfo> wordDict, string word)
    {
      if (matchesRuleDEN(word))
      {
        string baseWord = baseRuleDEN(word);

        if (wordDict.ContainsKey(baseWord))
        {
          combineWord(wordDict, word, baseWord);
        }
      }
    }

    internal bool matchesRuleDEN(string word)
    {
      if (word.Length < 5)
        return false;

      string ending = word.Substring(word.Length - 3);

      if (ending == "den" || ending == "dan")
      {
        return true;
      }

      return false;
    }

    internal string baseRuleDEN(string word)
    {
      return word.Substring(0, word.Length - 3);
    }

    private void ruleDE(Dictionary<string, WordInfo> wordDict, string word)
    {
      if (matchesRuleDE(word))
      {
        string baseWord = baseRuleDE(word);

        if (wordDict.ContainsKey(baseWord))
        {
          combineWord(wordDict, word, baseWord);
        }
      }
    }

    internal bool matchesRuleDE(string word)
    {
      if (word.Length < 4)
        return false;

      string ending = word.Substring(word.Length - 2);

      if (ending == "de" || ending == "da")
      {
        return true;
      }

      return false;
    }

    internal string baseRuleDE(string word)
    {
      return word.Substring(0, word.Length - 2);
    }

    private void combineWord(Dictionary<string, WordInfo> wordDict, string from, string to)
    {
      var fromInfo = wordDict[from];

      wordDict[to].Count += fromInfo.Count;
      wordDict[to].OtherForms.Details.Add(new OtherForm() { Form = from, Breadcrumbs = $"{from}>{to}" });

      wordDict[from].OtherForms.Details.ToList().ForEach(o =>
      {
        ((OtherForm)o).Breadcrumbs += ">" + to;
        wordDict[to].OtherForms.Details.Add(o);
        wordDict[from].Examples.Details.ToList().ForEach(ex => wordDict[to].Examples.Details.Add(ex));
      });

      wordDict.Remove(from);
    }

    private bool isFront(char vowel)
    {
      return FRONT_VOWELS.Contains(vowel);
    }

    private bool isBack(char vowel)
    {
      return BACK_VOWELS.Contains(vowel);
    }

  }
}
