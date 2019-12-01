using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class KnownWordReader
  {
    public HashSet<string> ReadKnownWords(FileStream fs)
    {
      HashSet<string> knownWords = new HashSet<string>();

      using (var sr = new StreamReader(fs))
      {
        while (!sr.EndOfStream)
        {
          var line = sr.ReadLine();

          knownWords.Add(line.Trim());
        }

        sr.Close();
      }

      return knownWords;
    }
  }
}
