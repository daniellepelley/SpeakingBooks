using System.IO;
using System.Linq;
using WatiN.Core;

namespace WpfSpeakingBooks
{
    public class BookScraper
    {
        private readonly IBookOutput _bookOutput;

        public BookScraper(IBookOutput bookOutput)
        {
            _bookOutput = bookOutput;
        }

        public void CreateAudioBook(string bookName, string url)
        {
            var browser = new FireFox(url);

            _bookOutput.DirectoryName = SetUpFolder(bookName, _bookOutput.Type);
            _bookOutput.BookTitle = bookName;

            _bookOutput.Start();

            int pageNumber = 1;
            do
            {
                var div = browser.Div("HtmlView");
                var link = browser.Link(Find.ById("navbarright"));

                string title = GetTitle(div);

                _bookOutput.CreatePage(div, pageNumber, title);

                browser.GoTo(link.Url);

                pageNumber++;
            }
            while (true);

            _bookOutput.Complete();

            browser.Dispose();
        }

        private static string GetTitle(Div div)
        {
            string pageText = div.Text;

            pageText = pageText.Replace((char)10, (char)32);

            var titles = pageText.Split((char)13);

            for (int i = 0; i < 5; i++)
            {
                if (titles.Count() > i)
                {
                    var output = titles[i].Trim();
                    if (!string.IsNullOrEmpty(output))
                    {
                        output = output
                            .Replace(":", ",")
                            .Replace(".", ",")
                            .Replace("/", " & ");

                        return output;
                    }
                }
            }
            return string.Empty;
        }

        private static string SetUpFolder(string bookName, string type)
        {
            string outputFolder = @"C:\Safari Books";

            string directoryName
                = Path.Combine(outputFolder, type);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            directoryName
                = Path.Combine(directoryName, bookName);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            return directoryName;
        }



    }
}