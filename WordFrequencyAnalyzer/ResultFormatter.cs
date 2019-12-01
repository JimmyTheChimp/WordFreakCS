using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class ResultFormatter
  {
    public string Format(Dictionary<string, WordInfo> wordDict)
    {
      var byCount = wordDict.Values.OrderByDescending(w => w.Count);

      StringBuilder sb = new StringBuilder();

      sb.AppendLine("Word count: " + byCount.Count());

      byCount.ToList().ForEach(w =>
      {
        if (w.Count > 0)
          sb.AppendLine(formatWord(w));
      });

      return sb.ToString();
    }

    private string formatWord(WordInfo wordInfo)
    {
      var verifiedSign = wordInfo.Verified ? "*" : "";
      var firstLine = $"{wordInfo.Count,10}: {wordInfo.Word} {verifiedSign}";
      if (wordInfo.OtherForms.Details.Any())
      {
        firstLine += "(";
        wordInfo.OtherForms.Details.ToList().ForEach(o => firstLine += ((OtherForm)o).Form + ",");
        firstLine = firstLine.TrimEnd(',');
        firstLine += ")";
      }

      int maxExample = Math.Min(5, wordInfo.Examples.Details.Count);
      for (int i = 0; i < maxExample; i++)
        firstLine += "\r\n            " + ((Example)wordInfo.Examples.Details.ElementAt(i)).Sentence;

      return firstLine;
    }
  }
}
