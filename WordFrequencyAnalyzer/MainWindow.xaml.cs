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
    private VerifiedWordsReader _verifiedWordsReader;
    private WordCombiner _wordCombiner;
    private WordVerifier _wordVerifier;


    public MainWindow()
    {
      InitializeComponent();

      _verifiedWordsReader = new VerifiedWordsReader();
      _wordCombiner = new WordCombiner();
      _wordVerifier = new WordVerifier();

      loadSettings();
    }

    private void btnAnalyze_Click(object sender, RoutedEventArgs e)
    {
      analyze();
    }

    private void loadSettings()
    {
      txtDictionary.Text = Properties.Settings.Default.Dictionary;
      txtFileName.Text = Properties.Settings.Default.InputFile;
      txtKnownWords.Text = Properties.Settings.Default.KnownWords;
    }

    private void saveSettings(FileInfo dictionaryFile, FileInfo inputFile, FileInfo knownWordsFile)
    {
      Properties.Settings.Default.Dictionary = dictionaryFile.FullName;
      Properties.Settings.Default.InputFile = inputFile.FullName;
      Properties.Settings.Default.KnownWords = knownWordsFile?.FullName ?? "";
      Properties.Settings.Default.Save();
    }

    private void analyze()
    {
      var filter = new KnownWordsFilter();

      var dictionaryFileInfo = getDictionaryFile();
      if (dictionaryFileInfo == null)
        return;

      // Verify the input file exists
      var inputFileInfo = new FileInfo(txtFileName.Text);
      if (!inputFileInfo.Exists)
      {
        MessageBox.Show("Input File does not exist.");
        return;
      }

      // Read the known words
      HashSet<string> knownWords = new HashSet<string>();
      FileInfo knownWordsFileInfo = null;

      if (!string.IsNullOrEmpty(txtKnownWords.Text))
      {
        // Verify the known words file exists
        knownWordsFileInfo = new FileInfo(txtKnownWords.Text);
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

      saveSettings(dictionaryFileInfo, inputFileInfo, knownWordsFileInfo);

      // Read the text
      Dictionary<string, WordInfo> results;
      using (var fs = inputFileInfo.OpenRead())
      {
        var analyzer = new Analyzer();
        results = analyzer.Analyze(fs);
      }

      // Read dictionary
      var verifiedWords = _verifiedWordsReader.ReadVerifiedWords(dictionaryFileInfo.OpenRead());

      foreach (var knownWord in knownWords)
      {
        if (!verifiedWords.Contains(knownWord))
          verifiedWords.Add(knownWord);
      }

      // Remove known words before combining
      var preFilteredResults = filter.Filter(knownWords, results);

      // Combine word entries
      //var combinedResults = _wordCombiner.Combine(knownWords, preFilteredResults);
      var combinedResults = _wordVerifier.CombineToVerifiedWords(verifiedWords, knownWords, preFilteredResults);

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

      var wordCount = filteredResults.Count;
      var verifiedCount = filteredResults.Count(r => r.Value.Verified);
      txtWordCount.Text = $"Words: {wordCount} ({verifiedCount} verified)";

      tcTabs.SelectedIndex = 1;


    }

    private FileInfo getDictionaryFile()
    {
      // Verify the dictionary file exists
      var dictionaryFile = new FileInfo(txtDictionary.Text);
      if (!dictionaryFile.Exists)
      {
        MessageBox.Show("Dictionary File does not exist.");
        return null;
      }

      return dictionaryFile;
    }

  }
}
