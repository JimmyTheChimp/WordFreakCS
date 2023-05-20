using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFrequencyAnalyzer
{
  public class Analyzer
  {
    public Dictionary<string, WordInfo> Analyze(FileStream fs)
    {
      var wordDict = new Dictionary<string, WordInfo>();

      var sr = new StreamReader(fs);

      int lineNo = 0;
      string sentence = "";
      while (!sr.EndOfStream)
      {
        char nextChar = (char)sr.Read();
        bool endOfWord = sentenceTerminatingCharacters.Contains(nextChar);

        if (!endOfWord)
        {
          sentence += nextChar;

        }
        else
        {
          processLine(wordDict, sentence, lineNo);
          sentence = "";
        }

        if (nextChar == '\n')
          lineNo++;

      }

      sr.Close();

      return wordDict;
    }

    private static char[] nonWordCharacters = { ' ', '.', ',', ';', '?', '"', '!', '(', ')', '*', ':', '', '\r', '\n', '-', '“', '”' };
    private static char[] sentenceTerminatingCharacters = {'.', '?', '!'};
    private Regex numberRegex = new Regex(@"\d+");
    private Regex apostropheRegex = new Regex(@"([^\'])+\'[^\']+");

    private void processLine(Dictionary<string, WordInfo> wordDict, string line, int lineNo)
    {
      var words = line.Split(nonWordCharacters, StringSplitOptions.RemoveEmptyEntries);
      // TODO: Use full sentence as example instead of line

      Array.ForEach(words, w => processWord(wordDict, w, line, lineNo));
    }

    private string trimSentence(string line)
    {
      string sentence = line;
      
      sentence = sentence.Replace('\n', ' ');
      sentence = sentence.Replace('\r', ' ');
      sentence = sentence.Replace("  ", " ");

      return sentence;
    }

    private void processWord(Dictionary<string, WordInfo> wordDict, string word, string line, int lineNo)
    {
      WordInfo wordInfo;

      string cleanWord = word.ToLower().Trim('\'');

      if (numberRegex.IsMatch(cleanWord) || apostropheRegex.IsMatch(cleanWord))
        return;

      if (string.IsNullOrWhiteSpace(cleanWord) || cleanWord.Length == 1)
        return;

      var sentence = trimSentence(line);

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
