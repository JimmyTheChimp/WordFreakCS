using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace WordFrequencyAnalyzer.Tests
{
  [TestFixture]
  public class WordcombinerTests
  {
    [TestCase("eve", true)]
    [TestCase("de", false)]
    [TestCase("rivayetlere", true)]
    [TestCase("bakkala", true)]
    [TestCase("da", false)]
    [TestCase("adalara", true)]
    public void MatchRuleE1(string word, bool expectedResult)
    {
      WordCombiner combiner = new WordCombiner();

      var result = combiner.matchesRuleE(word);

      Assert.AreEqual(expectedResult, result);
    }

    [TestCase("eve", "ev")]
    [TestCase("rivayetlere", "rivayetler")]
    [TestCase("bakkala", "bakkal")]
    [TestCase("adalara", "adalar")]
    public void BaseRuleE1(string word, string expectedResult)
    {
      WordCombiner combiner = new WordCombiner();

      var result = combiner.baseRuleE(word);

      Assert.AreEqual(expectedResult, result);
    }

    [TestCase("evler", true)]
    [TestCase("rivayetler", true)]
    [TestCase("rivayet", false)]
    [TestCase("bakkallar", true)]
    [TestCase("adalar", true)]
    [TestCase("adadan", false)]
    public void MatchRuleLER1(string word, bool expectedResult)
    {
      WordCombiner combiner = new WordCombiner();

      var result = combiner.matchesRuleLER(word);

      Assert.AreEqual(expectedResult, result);
    }

    [TestCase("evler", "ev")]
    [TestCase("rivayetler", "rivayet")]
    [TestCase("bakkallar", "bakkal")]
    [TestCase("adalar", "ada")]
    public void BaseRuleLER1(string word, string expectedResult)
    {
      WordCombiner combiner = new WordCombiner();

      var result = combiner.baseRuleLER(word);

      Assert.AreEqual(expectedResult, result);
    }

    [TestCase("evden", true)]
    [TestCase("evde", false)]
    [TestCase("rivayetlerden", true)]
    [TestCase("bakkaldan", true)]
    [TestCase("bakkalda", false)]
    [TestCase("adalardan", true)]
    public void MatchRuleDEN1(string word, bool expectedResult)
    {
      WordCombiner combiner = new WordCombiner();

      var result = combiner.matchesRuleDEN(word);

      Assert.AreEqual(expectedResult, result);
    }

    [TestCase("evden", "ev")]
    [TestCase("rivayetlerden", "rivayetler")]
    [TestCase("bakkaldan", "bakkal")]
    [TestCase("adalardan", "adalar")]
    public void BaseRuleDEN1(string word, string expectedResult)
    {
      WordCombiner combiner = new WordCombiner();

      var result = combiner.baseRuleDEN(word);

      Assert.AreEqual(expectedResult, result);
    }

    [TestCase("evde", true)]
    [TestCase("evden", false)]
    [TestCase("rivayetlerde", true)]
    [TestCase("bakkalda", true)]
    [TestCase("bakkala", false)]
    [TestCase("adalarda", true)]
    public void MatchRuleDE1(string word, bool expectedResult)
    {
      WordCombiner combiner = new WordCombiner();

      var result = combiner.matchesRuleDE(word);

      Assert.AreEqual(expectedResult, result);
    }

    [TestCase("evde", "ev")]
    [TestCase("rivayetlerde", "rivayetler")]
    [TestCase("bakkalda", "bakkal")]
    [TestCase("adalarda", "adalar")]
    public void BaseRuleDE1(string word, string expectedResult)
    {
      WordCombiner combiner = new WordCombiner();

      var result = combiner.baseRuleDE(word);

      Assert.AreEqual(expectedResult, result);
    }

    [TestCase("odayı", "oda")]
    public void BaseRuleI(string word, string expectedResult)
    {
      WordCombiner combiner = new WordCombiner();
      var result = combiner.baseRule(WordCombiner.patternySYN_I, word);

      Assert.AreEqual(expectedResult, result);
    }

    [Ignore("OBE")]
    [TestCase("dükkânlarının", "dükkân")]
    [TestCase("dükkânı","dükkân")]
    [TestCase("dükkâna", "dükkân")]
    [TestCase("dükkânına", "dükkân")]
    public void Combine(string word, string expectedResult)
    {
      WordCombiner combiner = new WordCombiner();
      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add(word, new WordInfo() { Word = word, Count=0});
      var result = combiner.runRulesSingle(wordDict, word);

      Assert.AreEqual(expectedResult, result);
    }

    [TestCase("yapar","yap")]
    [TestCase("yaparım", "yap")]
    [TestCase("yaparsın", "yap")]
    [TestCase("yaparız", "yap")]
    [TestCase("yaparsınız", "yap")]
    [TestCase("yaparlar", "yap")]
    [TestCase("yapmaz", "yap")]
    [TestCase("yapmam", "yap")]
    [TestCase("yapmazsın", "yap")]
    [TestCase("yapmamız", "yap")]
    [TestCase("yapmazsınız", "yap")]
    [TestCase("yapmazlar", "yap")]
    [TestCase("gider", "gid")]
    [TestCase("giderim", "gid")]
    [TestCase("gidersin", "gid")]
    [TestCase("gidersiniz", "gid")]
    [TestCase("gideriz", "gid")]
    [TestCase("giderler", "gid")]
    [TestCase("gitmez", "git")]
    [TestCase("gitmem", "git")]
    [TestCase("gitmezsin", "git")]
    [TestCase("gitmezsiniz", "git")]
    [TestCase("gitmemiz", "git")]
    [TestCase("gitmezler", "git")]
    public void BaseRuleGENIS(string word, string expectedResult)
    {
      WordCombiner combiner = new WordCombiner();
      var result = combiner.baseRule(WordCombiner.patternGENIS, word);

      Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void CombineVerifiedWords()
    {
      WordCombiner combiner = new WordCombiner();

      HashSet<string> verifiedWords = new HashSet<string>();

      verifiedWords.Add("kol");
      verifiedWords.Add("kolun");
      verifiedWords.Add("kolundaki");

      combiner.CombineVerifiedWords(verifiedWords);


      Assert.AreEqual(1, verifiedWords.Count);
      Assert.Contains("kol", verifiedWords.ToList());
    }

    [Test]
    public void CombineVerifiedWords2()
    {
      WordCombiner combiner = new WordCombiner();

      HashSet<string> verifiedWords = new HashSet<string>();
      
      verifiedWords.Add("kolunda");
      verifiedWords.Add("kolundaki");

      combiner.CombineVerifiedWords(verifiedWords);


      Assert.AreEqual(1, verifiedWords.Count);
      Assert.Contains("kolunda", verifiedWords.ToList());
    }
  }
}
