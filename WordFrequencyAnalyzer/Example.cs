using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class Example : IWordDetail
  {
    public int LineNo { get; set; }
    public string Sentence { get; set; }
  }
}
