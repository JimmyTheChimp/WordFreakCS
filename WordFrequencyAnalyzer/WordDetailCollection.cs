using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class WordDetailCollection
  {
    public string Name { get; set; }
    public ICollection<IWordDetail> Details { get; set; }
  }
}
