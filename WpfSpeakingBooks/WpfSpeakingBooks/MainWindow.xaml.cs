using System.Windows;

namespace WpfSpeakingBooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            urlText.Text = @"http://techbus.safaribooksonline.com/9780132485364/ch02lev1sec2";
            bookText.Text = @"Scaling Lean & Agile Development 2";
        }

        private void Audio_Click(object sender, RoutedEventArgs e)
        {
            var bookScraper = new BookScraper(new AudioOutput());
            bookScraper.CreateAudioBook(bookText.Text, urlText.Text);
        }

        private void Pdf_Click(object sender, RoutedEventArgs e)
        {
            var bookScraper = new BookScraper(new PdfOutput());
            bookScraper.CreateAudioBook(bookText.Text, urlText.Text);
        }

        private void Html_Click(object sender, RoutedEventArgs e)
        {
            var bookScraper = new BookScraper(new HtmlOutput());
            bookScraper.CreateAudioBook(bookText.Text, urlText.Text);
        }
    }
}
