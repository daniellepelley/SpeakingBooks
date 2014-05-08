using System.IO;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using WatiN.Core;

namespace WpfSpeakingBooks
{
    public class AudioOutput : IBookOutput
    {
        public string BookTitle { set; get; }

        public string Type { get { return "Audio Book"; } }

        public string DirectoryName { set; get; }

        public void CreatePage(Div div, int pageNumber, string title)
        {
            var content = div.Text
                .Replace((char)13, (char)46);

            string fileName = (string.IsNullOrEmpty(title)
                ?
                string.Format("Page{0}.wav", pageNumber)
                :
                string.Format("Page{0} - {1}.wav", pageNumber, title)
                );

            CreateWav(content, Path.Combine(DirectoryName, fileName));
        }

        public void Start()
        { }

        public void Complete()
        { }

        public void CreateWav(string text, string fileName)
        {
            using (var synth = new SpeechSynthesizer())
            {
                synth.SetOutputToWaveFile(fileName,
                    new SpeechAudioFormatInfo(44100, AudioBitsPerSample.Sixteen, AudioChannel.Mono));
                var builder = new PromptBuilder();
                builder.AppendText(text);
                synth.Speak(builder);
            }
        }
    }
}