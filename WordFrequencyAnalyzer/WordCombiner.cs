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
      newWord = rule(patternEBIL, wordDict, newWord);
      newWord = rule(patternECEK, wordDict, newWord);
      newWord = rule(patternDIK, wordDict, newWord);
      newWord = rule(patternLER, wordDict, newWord);
      newWord = rule(patternGENIS, wordDict, newWord);
      newWord = rule(patternMAK, wordDict, newWord);
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

    public class PatternAndReplace
    {
      public Regex Pattern { get; set; }
      public string Replace { get; set; }
    }



    public static Regex patternMAK = new Regex(@"(..+?)m[ae]k?$");

    //internal static Regex patternI = new Regex(@"(.+?[^dtkl])[yns]?[iıuü]$");
    public static Regex patternySYN_I = new Regex(@"(.+?[^dtkl])[ysn][iıuü]$");
    public static Regex patternI = new Regex(@"(..+?)[iıuü]$");
    //public static Regex patternE = new Regex(@"(..+?i)ne$|(..+?ı)na|(.+?[^c])y?[ea]$");
    public static Regex patternE = new Regex(@"(..+?)[ea]$");
    public static Regex patternYE = new Regex(@"(.+?[^c])y[ea]$");
    public static Regex patternNE = new Regex(@"(..+?i)ne$|(..+?ı)na$");
    
    public static Regex patternDEN = new Regex(@"(..+?)[td][ea]n$");
    public static Regex patternNDEN = new Regex(@"(..+?)nd[ea]n$");
    public static Regex patternDE = new Regex(@"(..+?)[td][ea]$");
    public static Regex patternNDE = new Regex(@"(..+?)nd[ea]$");
    public static Regex patternLER = new Regex(@"(..+?)ler(ce)?|lar(ca)?$");
    public static Regex patternDI = new Regex(@"(..+?)([dt][iıuü][mnk]?|[dt]iniz|[dt]ınız|[dt]unuz|[dt]ünüz)$");
    public static Regex patternME = new Regex(@"(..+?)me$");
    public static Regex patternMA = new Regex(@"(..+?)ma$");

    public static Regex patternKI = new Regex(@"(..+?)ki$");
    public static Regex patternIKEN = new Regex(@"(..+?)[y]?ken$");
    public static Regex patternINCE = new Regex(@"(..+?)([eiöü]nc[e])|([aıou]nc[a])$");
    public static Regex patternINCE_MAK = new Regex(@"(..+?)([aıou]nc[a])$");
    public static Regex patternINCE_MEK = new Regex(@"(..+?)([eiöü]nc[e])$");
    public static Regex patternIN = new Regex(@"(..+?)[n]?[iıüu]n$");
    
    public static Regex patternPOSESSIVE_UNSUZ = new Regex(@"(.+?[bcçdfghjklmnprsştvz])((u(m|n)(uz)?)|(ü(m|n)(üz)?)|(i(m|n)(iz)?)|(ı(m|n)(ız)?))$");
    public static Regex patternPOSESSIVE_UNLU = new Regex(@"(.+?[aeiıoöuü])(m|n|m(u|ü|i|ı)z|n(u|ü|i|ı)z)$");

    public static Regex patternBE_UNSUZ = new Regex(@"(.+?[bcçdfghjklmnprsştvz])((u|ü|i|ı)m|(u|ü|i|ı)z|s(u|ü|i|ı)n|sunuz|sünüz|sınız|sünüz)$");
    public static Regex patternBE_UNLU = new Regex(@"(.+?[aeiıoöuü])(y(u|ü|i|ı)m|y(u|ü|i|ı)z|s(u|ü|i|ı)n|sunuz|sünüz|sınız|sünüz)$");

    public static Regex patternEREK = new Regex(@"(..+?)y?arak|erek$");
    public static Regex patternEREK_MAK = new Regex(@"(..+?)(y|may)?arak$");
    public static Regex patternEREK_MEK = new Regex(@"(..+?)(y|mey)?erek$");
    public static Regex patternILE = new Regex(@"(..+?)y?l[ea]$");
    public static Regex patternEN = new Regex(@"(..+?)y?[ea]n$");
    public static Regex patternEN_MAK = new Regex(@"(..+?)y?an$");
    public static Regex patternEN_MEK = new Regex(@"(..+?)y?en$");

    public static Regex patternECEK = new Regex(@"(..+?)y?(ecek(sin(iz)?)?|acak(sın(ız)?)?|eceğim(iz)?|acağım(ız)?)$");
    public static Regex patternECEK_MAK = new Regex(@"(..+?)y?(acak(sın(ız)?)?|acağım(ız)?)$");
    public static Regex patternECEK_MEK = new Regex(@"(..+?)y?(ecek(sin(iz)?)?|eceğim(iz)?)$");

    public static Regex patternERKEN_MEK = new Regex(@"(..+?)(e|i|ü)rken$");
    public static Regex patternERKEN_MAK = new Regex(@"(..+?)(a|ı|u)rken$");

    public static Regex patternEBIL = new Regex(@"(..+?)y?[ea]bil$");
    public static Regex patternMIS = new Regex(@"(..+?)m[iıuü]ş([iıuü]m|s[iıuü]n|siniz|sınız|sunuz|sünüz|[iıuü]z)?$");
    public static Regex patternDI_MAK = new Regex(@"(..+?)([dt][ıu][mnk]?|[dt]ınız|[dt]unuz)$");
    public static Regex patternDI_MEK = new Regex(@"(..+?)([dt][iü][mnk]?|[dt]iniz|[dt]ünüz)$");

    public static Regex patternIYOR = new Regex(@"(..+?)m?[iüıu]yor$");
    public static Regex patternIYOR_MAK = new Regex(@"(..+?)m?[ıu]yor(um|sun(uz)?|uz|lar)?$");
    public static Regex patternIYOR_MEK = new Regex(@"(..+?)m?[iü]yor(um|sun(uz)?|uz|lar)?$");
    public static Regex patternIYOR_AMAK = new Regex(@"(..+?)[ıu]yor(um|sun(uz)?|uz|lar)?$");
    public static Regex patternIYOR_EMEK = new Regex(@"(..+?)[iü]yor(um|sun(uz)?|uz|lar)?$");

    public static Regex patternIYORDU_MAK = new Regex(@"(..+?)m?[ıu]yordu(m|n(uz)?|k|lar)?$");
    public static Regex patternIYORDU_MEK = new Regex(@"(..+?)m?[iü]yordu(m|n(uz)?|k|lar)?$");
    public static Regex patternIYORDU_AMAK = new Regex(@"(..+?)[ıu]yordu(m|n(uz)?|k|lar)?$");
    public static Regex patternIYORDU_EMEK = new Regex(@"(..+?)[iü]yordu(m|n(uz)?|k|lar)?$");

    public static Regex patternIYORMUS_MAK = new Regex(@"(..+?)m?[ıu]yormuş(um|sun(uz)?|uz|lar)?$");
    public static Regex patternIYORMUS_MEK = new Regex(@"(..+?)m?[iü]yormuş(um|sun(uz)?|uz|lar)?$");
    public static Regex patternIYORMUS_AMAK = new Regex(@"(..+?)[ıu]yormuş(um|sun(uz)?|uz|lar)?$");
    public static Regex patternIYORMUS_EMEK = new Regex(@"(..+?)[iü]yormuş(um|sun(uz)?|uz|lar)?$");

    public static Regex patternEDMEK = new Regex(@"(.+)?edmek$");
    public static Regex patternAMAMAK = new Regex(@"(..+?)amamak$");
    public static Regex patternEMEMEK = new Regex(@"(..+?)amamak$");

    public static Regex patternDIK = new Regex(@"(..+?)[dt](iği[mn]?|ığı[mn]?|üğü[mn]?|uğu[mn]?|iği[mn]iz|ığı[mn]ız|üğü[mn]üz|uğu[mn]uz)$");
    public static Regex patternDIK_MEK = new Regex(@"(..+?)[dt](iği[mn]?|üğü[mn]?|iği[mn]iz|üğü[mn]üz)$");
    public static Regex patternDIK_MAK = new Regex(@"(..+?)[dt](ığı[mn]?|uğu[mn]?|ığı[mn]ız|uğu[mn]uz)$");
    public static Regex patternIP = new Regex(@"(..+?)y?[iıuü]p$");
    public static Regex patternIP_MEK = new Regex(@"(..+?)y?[iü]p$");
    public static Regex patternIP_MAK = new Regex(@"(..+?)y?[ıu]p$");

    public static Regex patternGENIS = new Regex(@"(..+?)(er(i[mz]|ler|sin(iz)?)?|ar(ı[mz]|lar|sın(ız)?)?|mez((sin(iz)?)|ler)?|mem(iz)?|maz((sın(ız)?)|lar)?|mam(ız)?)$");
    //internal static Regex patternGENIS = new Regex(@"(..+?)(mez|maz)$");
    
    public static Regex patternGENIS_MAK = new Regex(@"(..+?)(ar(ı[mz]|lar(sa)?|sın(ız)?)?|(arsa)(m|n|k|nız)?|maz((sın(ız)?)|lar(sa)?|sa(m|n|k|nız)?|)?|mam(ız)?)$");
    public static Regex patternGENIS_MEK = new Regex(@"(..+?)(er(i[mz]|ler(se)?|sin(iz)?)?|(erse)(m|n|k|niz)?|mez((sin(iz)?)|ler(se)?|se(m|n|k|niz)?|)?|mem(iz)?)$");
    public static Regex patternGENIS_EMEK = new Regex(@"(..+?)(er(i[mz]|ler(se)?|sin(iz)?)?|(erse)(m|n|k|niz)?|emez((sin(iz)?)|ler(se)?|se(m|n|k|niz)?|)?|emem(iz)?)$");
    public static Regex patternGENIS_AMAK = new Regex(@"(..+?)(ar(ı[mz]|lar(sa)?|sın(ız)?)?|(arsa)(m|n|k|nız)?|amaz((sın(ız)?)|lar(sa)?|sa(m|n|k|nız)?|)?|amam(ız)?)$");

    public static Regex patternSIN_MEK = new Regex(@"(..+?)(sin|sün|siniz|sünüz|mesin|mesiniz)$");
    public static Regex patternSIN_MAK = new Regex(@"(..+?)(sın|sun|sınız|sunuz|masın|masınız)$");

    public static Regex patternReplaceG = new Regex(@"(..+?)ğ$");
    public const string replaceG = "k";
    
    public static Regex patternReplaceB = new Regex(@"(..+?)b$");
    public const string replaceB = "p";
    
    public static Regex patternReplaceC = new Regex(@"(..+?)c$");
    public const string replaceC = "ç";
    
    public static Regex patternReplaceD = new Regex(@"()..+?d$");
    public static string replaceD = "t";

    public static PatternAndReplace[] IsimPatterns = new PatternAndReplace[]
{
      new PatternAndReplace(){ Pattern = patternI, Replace = ""},
      new PatternAndReplace(){ Pattern = patternySYN_I, Replace = ""},
      new PatternAndReplace(){ Pattern = patternDEN, Replace = ""},
      new PatternAndReplace(){ Pattern = patternNDEN, Replace = ""},
      new PatternAndReplace(){ Pattern = patternDE, Replace = ""},
      new PatternAndReplace(){ Pattern = patternNDE, Replace = ""},
      new PatternAndReplace(){ Pattern = patternE, Replace = ""},
      new PatternAndReplace(){ Pattern = patternYE, Replace = ""},
      new PatternAndReplace(){ Pattern = patternNE, Replace = ""},
      new PatternAndReplace(){ Pattern = patternLER, Replace = ""},
      new PatternAndReplace(){ Pattern = patternILE, Replace = ""},
      new PatternAndReplace(){ Pattern = patternIN, Replace = ""},
      new PatternAndReplace(){ Pattern = patternKI, Replace = ""},
      new PatternAndReplace(){ Pattern = patternIKEN, Replace = ""},
      new PatternAndReplace(){ Pattern = patternMIS, Replace = ""},
      new PatternAndReplace(){ Pattern = patternDI, Replace = ""},
      new PatternAndReplace(){ Pattern = patternME, Replace = "mek"},
      new PatternAndReplace(){ Pattern = patternMA, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternDIK_MEK, Replace = "mek"},
      new PatternAndReplace(){ Pattern = patternDIK_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternPOSESSIVE_UNLU, Replace = ""},
      new PatternAndReplace(){ Pattern = patternPOSESSIVE_UNSUZ, Replace = ""},
      new PatternAndReplace(){ Pattern = patternBE_UNLU, Replace = ""},
      new PatternAndReplace(){ Pattern = patternBE_UNSUZ, Replace = ""},
      new PatternAndReplace() {Pattern = patternReplaceG, Replace = replaceG },
      new PatternAndReplace() {Pattern = patternReplaceC, Replace = replaceC },
      new PatternAndReplace() {Pattern = patternReplaceB, Replace = replaceB },
      new PatternAndReplace() {Pattern = patternReplaceD, Replace = replaceD }
};

    public static PatternAndReplace[] EylemPatterns = new PatternAndReplace[]
    {
      new PatternAndReplace(){ Pattern = patternIYOR_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternIYOR_MEK, Replace = "mek"},
      new PatternAndReplace(){ Pattern = patternIYOR_AMAK, Replace = "amak"},
      new PatternAndReplace(){ Pattern = patternIYOR_EMEK, Replace = "emek"},

      new PatternAndReplace(){ Pattern = patternIYORDU_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternIYORDU_MEK, Replace = "mek"},
      new PatternAndReplace(){ Pattern = patternIYORDU_AMAK, Replace = "amak"},
      new PatternAndReplace(){ Pattern = patternIYORDU_EMEK, Replace = "emek"},

      new PatternAndReplace(){ Pattern = patternIYORMUS_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternIYORMUS_MEK, Replace = "mek"},
      new PatternAndReplace(){ Pattern = patternIYORMUS_AMAK, Replace = "amak"},
      new PatternAndReplace(){ Pattern = patternIYORMUS_EMEK, Replace = "emek"},

      new PatternAndReplace(){ Pattern = patternGENIS_MAK, Replace = "amak"},
      new PatternAndReplace(){ Pattern = patternGENIS_AMAK, Replace = "amak"},
      new PatternAndReplace(){ Pattern = patternGENIS_MEK, Replace = "mek"},
      new PatternAndReplace(){ Pattern = patternGENIS_EMEK, Replace = "emek"},


      new PatternAndReplace(){ Pattern = patternEREK_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternEREK_MEK, Replace = "mek"},

      new PatternAndReplace(){ Pattern = patternERKEN_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternERKEN_MEK, Replace = "mek"},

      new PatternAndReplace(){ Pattern = patternINCE_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternINCE_MEK, Replace = "mek"},

      new PatternAndReplace(){ Pattern = patternEN_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternEN_MEK, Replace = "mek"},

      new PatternAndReplace(){ Pattern = patternECEK_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternECEK_MEK, Replace = "mek"},

      new PatternAndReplace(){ Pattern = patternEBIL, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternMIS, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternDI_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternDI_MEK, Replace = "mek"},

      new PatternAndReplace(){ Pattern = patternIP_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternIP_MEK, Replace = "mek"},

      new PatternAndReplace(){ Pattern = patternSIN_MAK, Replace = "mak"},
      new PatternAndReplace(){ Pattern = patternSIN_MEK, Replace = "mek"},

      new PatternAndReplace(){ Pattern = patternEDMEK, Replace = "etmek" },
      new PatternAndReplace(){ Pattern = patternAMAMAK, Replace = "mak" },
      new PatternAndReplace(){ Pattern = patternEMEMEK, Replace = "mek" }


    };

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

    internal void combineWord(Dictionary<string, WordInfo> wordDict, string from, string to)
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
