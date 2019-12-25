﻿using System;
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

    [Test]
    public void Yuz()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("yüz");

      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("yüzü", new WordInfo() { Count = 1, Word = "yüzü" });
      wordDict.Add("yüzüne", new WordInfo() { Count = 1, Word = "yüzüne" });
      wordDict.Add("yüzlerine", new WordInfo() { Count = 1, Word = "yüzlerine" });
      wordDict.Add("yüzünden", new WordInfo() { Count = 1, Word = "yüzünden" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(4, wordDict["yüz"].Count);
    }

    [Test]
    public void Istiyordu()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("istemek");

      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("istiyor", new WordInfo() { Count = 1, Word = "istiyor" });
      wordDict.Add("istiyorsunuz", new WordInfo() { Count = 1, Word = "istiyorsunuz" });
      wordDict.Add("istedim", new WordInfo() { Count = 1, Word = "istedim" });
      wordDict.Add("istiyordu", new WordInfo() { Count = 1, Word = "istiyordu" });
      wordDict.Add("istiyormuşsun", new WordInfo() { Count = 1, Word = "istiyormuşsun" });
      wordDict.Add("istemiyorsun", new WordInfo() { Count = 1, Word = "istemiyorsun" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(6, wordDict["istemek"].Count);
    }

    [Test]
    public void Ediyordu()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("etmek");

      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("ediyorum", new WordInfo() { Count = 1, Word = "ediyorum" });
      wordDict.Add("ediyordular", new WordInfo() { Count = 1, Word = "ediyordular" });
      wordDict.Add("ediyormuşsunuz", new WordInfo() { Count = 1, Word = "ediyormuşsunuz" });
      wordDict.Add("ederim", new WordInfo() { Count = 1, Word = "ederim" });
      wordDict.Add("etmez", new WordInfo() { Count = 1, Word = "etmez" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(5, wordDict["etmek"].Count);
    }

    // haline, babasına, gunden

    [Test]
    public void Hal()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("hal");

      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("haline", new WordInfo() { Count = 1, Word = "haline" });
      wordDict.Add("halde", new WordInfo() { Count = 1, Word = "halde" });
      wordDict.Add("halinde", new WordInfo() { Count = 1, Word = "halinde" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(3, wordDict["hal"].Count);
    }

    [Test]
    public void Gunden()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("gün");

      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("günden", new WordInfo() { Count = 1, Word = "günden" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(1, wordDict["gün"].Count);
    }

    [Test]
    public void GelmeGelmek()
    {
      var verifier = new WordVerifier();

      var verifiedWords = new HashSet<string>();
      verifiedWords.Add("gelmek");
      verifiedWords.Add("gelme");


      var knownWords = new HashSet<string>();

      var wordDict = new Dictionary<string, WordInfo>();
      wordDict.Add("gelmedim", new WordInfo() { Count = 1, Word = "gelmedim" });
      wordDict.Add("gelmediği", new WordInfo() { Count = 1, Word = "gelmediği" });
      wordDict.Add("geldim", new WordInfo() { Count = 1, Word = "geldim" });
      wordDict.Add("geldiği", new WordInfo() { Count = 1, Word = "geldiği" });

      verifier.CombineToVerifiedWords(verifiedWords, knownWords, wordDict);

      Assert.AreEqual(4, wordDict["gelmek"].Count);
    }
  }
}
