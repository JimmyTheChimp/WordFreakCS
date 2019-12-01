using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class VerifiedWordsReader
  {
    public HashSet<string> ReadVerifiedWords(FileStream fs)
    {
      HashSet<string> verifiedWords = new HashSet<string>();

      using (var sr = new StreamReader(fs))
      {
        while (!sr.EndOfStream)
        {
          var line = sr.ReadLine();

          verifiedWords.Add(line.Trim());
        }

        sr.Close();
      }

      return verifiedWords;
    }
  }
}
