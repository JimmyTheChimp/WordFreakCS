using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class Analyzer
  {
    public Analyzer()
    {
    }

    public Dictionary<string, WordInfo> Analyze(FileStream fs)
    {
      var wordDict = new Dictionary<string, WordInfo>();

      var sr = new StreamReader(fs);

      int max = int.MaxValue; //100;
      int lineNo = 0;
      while (!sr.EndOfStream && lineNo < max)
      {
        var line = sr.ReadLine();        

        processLine(wordDict, line, lineNo);

        lineNo++;
      }

      sr.Close();

      return wordDict;
    }

    private static char[] nonWordCharacters = { ' ', '.', ',', ';', '?', '"', '!', '(', ')', '*', ':', '', '\r', '\n' };
    private Regex numberRegex = new Regex(@"\d+");
    private Regex apostropheRegex = new Regex(@"([^'])+'[^']+");
    
    private void processLine(Dictionary<string, WordInfo> wordDict, string line, int lineNo)
    {
      var words = line.Split(nonWordCharacters, StringSplitOptions.RemoveEmptyEntries);
      // TODO: Use full sentence as example instead of line
      
      Array.ForEach(words, w => processWord(wordDict, w, line, lineNo));
    }

    private string trimSentence(string line, string word)    {
      string sentence = line;

      var indexOfWord = sentence.IndexOf(word);

      if (indexOfWord > 0)
      {
        var lastIndexOfPrecedingPeriod = sentence.LastIndexOf('.', indexOfWord);
        if (lastIndexOfPrecedingPeriod > -1)
          sentence = sentence.Substring(lastIndexOfPrecedingPeriod + 1);
      }

      var indexOfSubsequentPeriod = sentence.IndexOf(".", word.Length);
      if (indexOfSubsequentPeriod > -1)
        sentence = sentence.Substring(0, indexOfSubsequentPeriod);

      return sentence;
    }

    private void processWord(Dictionary<string, WordInfo> wordDict, string word, string line, int lineNo)
    {
      WordInfo wordInfo;

      string cleanWord = word.ToLower().Trim('\'');
      if (numberRegex.IsMatch(cleanWord) || apostropheRegex.IsMatch(cleanWord))
        return;

      if (string.IsNullOrWhiteSpace(cleanWord))
        return;

      var sentence = trimSentence(line, word);

      if (wordDict.TryGetValue(cleanWord, out wordInfo))
      {
        wordInfo.Count++;
        wordInfo.Examples.Details.Add(new Example() { Sentence = sentence, LineNo = lineNo });
      }
      else
      {
        wordInfo = new WordInfo()
        {
          Word = cleanWord,
          Count = 1
        };

        
        wordInfo.Examples.Details.Add(new Example() { Sentence = sentence, LineNo = lineNo });

        wordDict.Add(cleanWord, wordInfo);
      }
    }
  }
}
