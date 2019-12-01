using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class KnownWordsFilter
  {

    public Dictionary<string, WordInfo> Filter(HashSet<string> knownWords, Dictionary<string, WordInfo> wordDict)
    {
      // Filter known words
      knownWords.ToList().ForEach(k => wordDict.Remove(k));

      // Filter combined/base versions of known words

      var combiner = new WordCombiner();

      var knownWordsDict = new Dictionary<string, WordInfo>();
      knownWords.ToList().ForEach(k => knownWordsDict.Add(k, new WordInfo() { Word = k, Count = 0 }));
      var combinedKnownWords = combiner.Combine(new HashSet<string>(), knownWordsDict);
           
      combinedKnownWords.Keys.ToList().ForEach(k =>
      {
        wordDict.Remove(k);
      });

      return wordDict;
    }
  }
}
