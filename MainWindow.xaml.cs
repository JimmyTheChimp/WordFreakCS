using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordFrequencyAnalyzer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void btnAnalyze_Click(object sender, RoutedEventArgs e)
    {
      analyze();
    }

    private void analyze()
    {
      var filter = new KnownWordsFilter();

      // Verify the input file exists
      var inputFileInfo = new FileInfo(txtFileName.Text);
      if (!inputFileInfo.Exists)
      {
        MessageBox.Show("Input File does not exist.");
        return;
      }

      // Read the known words
      HashSet<string> knownWords = new HashSet<string>();

      if (!string.IsNullOrEmpty(txtKnownWords.Text))
      {
        // Verify the known words file exists
        var knownWordsFileInfo = new FileInfo(txtKnownWords.Text);
        if (!knownWordsFileInfo.Exists)
        {
          MessageBox.Show("Known Words File does not exist.");
          return;
        }

        using (var fs = knownWordsFileInfo.OpenRead())
        {
          var knownWordsReader = new KnownWordReader();
          knownWords = knownWordsReader.ReadKnownWords(fs);
        }
      }
      
      // Read the text
      Dictionary<string, WordInfo> results;
      using (var fs = inputFileInfo.OpenRead())
      {
        var analyzer = new Analyzer();
        results = analyzer.Analyze(fs);
      }

      // Remove known words before combining
      var preFilteredResults = filter.Filter(knownWords, results);

      // Combine word entries
      var combiner = new WordCombiner();
      var combinedResults = combiner.Combine(knownWords, preFilteredResults);

      // Remove known words      
      var filteredResults = filter.Filter(knownWords, combinedResults);

      // Format results
      var formatter = new ResultFormatter();
      var formattedResults = formatter.Format(filteredResults);

      var outputFile = new FileInfo(@"E:\OneDrive\Documents\Language Stuff\word-analyzer-output.txt");

      outputFile.Delete();
      using (var outputFS = outputFile.OpenWrite())
      {
        var sw = new StreamWriter(outputFS);
        sw.WriteLine(formattedResults);
        sw.Flush();
        sw.Close();
      }

      tvResults.ItemsSource = filteredResults.Values.OrderByDescending(r => r.Count);
      txtWordCount.Text = filteredResults.Count.ToString();
      tcTabs.SelectedIndex = 1;


    }

  }
}
