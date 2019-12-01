using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequencyAnalyzer
{
  public class WordInfo
  {
    public WordInfo()
    {
      OtherForms = new WordDetailCollection()
      {
        Name = "Other Forms",
        Details = new HashSet<IWordDetail>()
      };

      Examples = new WordDetailCollection() {
        Name = "Examples",
        Details = new List<IWordDetail>()
      };
      
      Details = new ObservableCollection<WordDetailCollection>() { OtherForms, Examples };
    }

    public string Word { get; set; }
    public int Count { get; set; }
    public WordDetailCollection OtherForms { get; }
    public WordDetailCollection Examples { get; }

    public ObservableCollection<WordDetailCollection> Details { get; }
  }
}
