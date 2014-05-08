using System.IO;
using WatiN.Core;

namespace WpfSpeakingBooks
{
    public class HtmlOutput : IBookOutput
    {
        public string BookTitle { set; get; }

        public string Type { get { return "Html Book"; } }

        public string DirectoryName { set; get; }

        public void CreatePage(Div div, int pageNumber, string title)
        {
            string fileName = (string.IsNullOrEmpty(title)
                ?
                string.Format("Page{0}.html", pageNumber)
                :
                string.Format("Page{0} - {1}.html", pageNumber, title)
                );

            TextWriter tw = new StreamWriter(Path.Combine(DirectoryName, fileName));

            // write a line of text to the file
            tw.WriteLine(div.OuterHtml);

            // close the stream
            tw.Close();
        }

        public void Start()
        { }

        public void Complete()
        { }
    }
}