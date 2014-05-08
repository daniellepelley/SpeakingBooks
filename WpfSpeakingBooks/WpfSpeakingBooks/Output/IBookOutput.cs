using WatiN.Core;

namespace WpfSpeakingBooks
{
    public interface IBookOutput
    {
        string BookTitle { get; set; }
        string DirectoryName { set; get; }
        string Type { get; }

        void CreatePage(Div div, int pageNumber, string title);

        void Start();
        void Complete();
    }
}