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

        runRulesAll(wordDict, firstRun);
        firstRun = false;
      } while (wordDict.Count < lastCount);

      return wordDict;
    }


    private void runRulesAll(Dictionary<string, WordInfo> wordDict, bool firstRun)
    {
      var words = wordDict.Keys.OrderByDescending(k => k.Length).ToList();

      words.ForEach(w => runRulesSingle(wordDict, w));
    }

    internal string runRulesSingle(Dictionary<string, WordInfo> wordDict, string word)
    {
      if (!wordDict.ContainsKey(word))
        return word;

      var firstRulesWord = runFirstRules(wordDict, word);

      int lastLength;
      var repeatRulesWord = firstRulesWord;
      do
      {
        lastLength = repeatRulesWord.Length;

        repeatRulesWord = runRepeatableRules(wordDict, repeatRulesWord);

      } while (repeatRulesWord.Length < lastLength);

      var lastRulesWord = runLastRules(wordDict, repeatRulesWord);

      return lastRulesWord;
    }

    private string runFirstRules(Dictionary<string, WordInfo> wordDict, string word)
    {
      string newWord = word;

      newWord = rule(patternINCE, wordDict, newWord);
      if (newWord != word)
        return newWord;

      newWord = rule(patternDEN, wordDict, newWord);
      if (newWord != word)
        return newWord;

      newWord = rule(patternDE, wordDict, newWord);
      if (newWord != word)
        return newWord;

      newWord = rule(patternILE, wordDict, newWord);
      if (newWord != word)
        return newWord;

      newWord = rule(patternE, wordDict, newWord);
      if (newWord != word)
        return newWord;

      newWord = rule(patternIP, wordDict, newWord);
      if (newWord != word)
        return newWord;

      newWord = rule(patternEREK, wordDict, newWord);

      return newWord;
    }

    private string runRepeatableRules(Dictionary<string, WordInfo> wordDict, string word)
    {
      string newWord = word;

      newWord = rule(patternINCE, wordDict, newWord);
      newWord = rule(patternECEK, wordDict, newWord);
      newWord = rule(patternDIK, wordDict, newWord);
      newWord = rule(patternMAK, wordDict, newWord);
      newWord = rule(patternLER, wordDict, newWord);
      newWord = rule(patternMIS, wordDict, newWord);
      newWord = rule(patternDI, wordDict, newWord);
      newWord = rule(patternIYOR, wordDict, newWord);
      newWord = rule(patternKI, wordDict, newWord);
      newWord = rule(patternIKEN, wordDict, newWord);
      newWord = rule(patternIN, wordDict, newWord);
      newWord = rule(patternI, wordDict, newWord);
      newWord = rule(patternEN, wordDict, newWord);

      return newWord;
    }

    private string runLastRules(Dictionary<string, WordInfo> wordDict, string word)
    {
      var newWord = word;

      newWord = replaceRule(patternReplaceG, replaceG, wordDict, newWord);
      if (newWord != word)
        return newWord;

      newWord = replaceRule(patternReplaceB, replaceB, wordDict, newWord);
      if (newWord != word)
        return newWord;

      newWord = replaceRule(patternReplaceC, replaceC, wordDict, newWord);
      if (newWord != word)
        return newWord;

      newWord = replaceRule(patternReplaceD, replaceD, wordDict, newWord);

      return newWord;

    }

    internal static Regex patternMAK = new Regex(@"(..+?)m[ae]k?$");

    //internal static Regex patternI = new Regex(@"(.+?[^dtkl])[yns]?[iıuü]$");
    internal static Regex patternI = new Regex(@"(.+?[^dtkl])[ys]?[iıuü]$");
    internal static Regex patternE = new Regex(@"(..+?i)ne$|(..+?ı)na|(.+?[^c])y?[ea]$");
    internal static Regex patternDEN = new Regex(@"(..+?)[yn]?[td][ea]n$");
    internal static Regex patternDE = new Regex(@"(..+?)[yn]?[td][ea]$");
    internal static Regex patternLER = new Regex(@"(..+?)l[ea]r$");

    internal static Regex patternKI = new Regex(@"(..+?)ki$");
    internal static Regex patternIKEN = new Regex(@"(..+?)[y]?ken$");
    internal static Regex patternINCE = new Regex(@"(..+?)([eiöü]nc[e])|([aıou]nc[a])$");
    internal static Regex patternIN = new Regex(@"(..+?)[n]?[iıüu]n$");

    internal static Regex patternEREK = new Regex(@"(..+?)y?arak|erek$");
    internal static Regex patternILE = new Regex(@"(..+?)y?l[ea]$");
    internal static Regex patternEN = new Regex(@"(..+?)y?[ea]n$");

    internal static Regex patternECEK = new Regex(@"(..+?)y?(ecek(sin(iz)?)?|acak(sın(ız)?)?|eceğim(iz)?|acağım(ız)?)$");
    internal static Regex patternMIS = new Regex(@"(..+?)m[iıuü]ş([iıuü]m|s[iıuü]n|siniz|sınız|sunuz|sünüz|[iıuü]z)?$");
    internal static Regex patternDI = new Regex(@"(..+?)([dt][iıuü][mnk]?|[dt]iniz|[dt]ınız|[dt]unuz|[dt]ünüz)$");
    internal static Regex patternIYOR = new Regex(@"(..+?)m?[iüıu]yor$");
    internal static Regex patternDIK = new Regex(@"(..+?)[dt](iği[mn]?|ığı[mn]?|üğü[mn]?|uğu[mn]?|iği[mn]iz|ığı[mn]ız|üğü[mn]üz|uğu[mn]uz)$");
    internal static Regex patternIP = new Regex(@"(..+?)[iıuü]p$");

    internal static Regex patternReplaceG = new Regex(@"(..+?)ğ$");
    internal const string replaceG = "k";

    internal static Regex patternReplaceB = new Regex(@"(..+?)b$");
    internal const string replaceB = "p";

    internal static Regex patternReplaceC = new Regex(@"(..+?)c$");
    internal const string replaceC = "ç";

    internal static Regex patternReplaceD = new Regex(@"()..+?d$");
    internal static string replaceD = "t";

    private string rule(Regex pattern, Dictionary<string, WordInfo> wordDict, string word)
    {
      if (matchesRule(pattern, word))
      {
        string baseWord = baseRule(pattern, word);

        if (!wordDict.ContainsKey(baseWord))
        {
          wordDict.Add(baseWord, new WordInfo() { Word = baseWord, Count = 0 });
        }

        combineWord(wordDict, word, baseWord);
        return baseWord;
      }

      return word;
    }

    internal bool matchesRule(Regex pattern, string word)
    {
      return pattern.IsMatch(word);
    }

    internal string baseRule(Regex pattern, string word)
    {
      return pattern.Match(word).Groups[1].ToString();
    }

    private string replaceRule(Regex pattern, string replacement, Dictionary<string, WordInfo> wordDict, string word)
    {
      if (matchesRule(pattern, word))
      {
        string baseWord = baseReplaceRule(pattern, replacement, word);

        if (!wordDict.ContainsKey(baseWord))
        {
          wordDict.Add(baseWord, new WordInfo() { Word = baseWord, Count = 0 });
        }

        combineWord(wordDict, word, baseWord);

        return baseWord;
      }

      return word;
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
      var toInfo = wordDict[to];

      toInfo.Count += fromInfo.Count;


      if (!toInfo.OtherForms.Details.Any(d => (d as OtherForm).Form == from))
      {
        toInfo.OtherForms.Details.Add(new OtherForm() { Form = from, Breadcrumbs = $"{from}>{to}" });
      }

      fromInfo.OtherForms.Details.ToList().ForEach(o =>
      {
        var otherForm = (OtherForm)o;
        otherForm.Breadcrumbs += ">" + to;

        if (toInfo.OtherForms.Details.Any(d => (d as OtherForm).Form == otherForm.Form))
        {
          var toForm = (OtherForm)toInfo.OtherForms.Details.First(d => (d as OtherForm).Form == otherForm.Form);
          toForm.Breadcrumbs = otherForm.Breadcrumbs + ">" + toForm.Breadcrumbs;
        }
        else
        {
          toInfo.OtherForms.Details.Add(o);
        }
      });

      fromInfo.Examples.Details.ToList().ForEach(ex =>
      {
        if (!toInfo.Examples.Details.Any(e => (e as Example).LineNo == (ex as Example).LineNo))
          toInfo.Examples.Details.Add(ex);
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
