using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace WordFrequencyAnalyzer.Tests
{
  [TestFixture]
  public class WordVerifierTest
  {

    [Test]
    public void Ev()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("ev");

      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("evde", new WordInfo() { Count = 1, Word = "evde" });
      wordDict.Add("evler", new WordInfo() { Count = 1, Word = "evler" });
      wordDict.Add("evlerde", new WordInfo() { Count = 1, Word = "evlerde" });
      wordDict.Add("evdekiler", new WordInfo() { Count = 1, Word = "evdekiler" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(4, wordDict["ev"].Count);


    }

        [Test]
    public void Yapmak()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("yapmak");

      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("yaptım", new WordInfo() { Count = 1, Word = "yaptım" });
      wordDict.Add("yapacaksın", new WordInfo() { Count = 1, Word = "yapacaksın" });
      wordDict.Add("yaparak", new WordInfo() { Count = 1, Word = "yaparak" });
      wordDict.Add("yapmışız", new WordInfo() { Count = 1, Word = "yapmışız" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(4, wordDict["yapmak"].Count);
    }

    [Test]
    public void Yarin()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("yarın");

      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("yarına", new WordInfo() { Count = 1, Word = "yarına" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(1, wordDict["yarın"].Count);
    }

    [Test]
    public void Gormek()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("görmek");

      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("gördü", new WordInfo() { Count = 1, Word = "gördü" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(1, wordDict["görmek"].Count);
    }

  }
}
