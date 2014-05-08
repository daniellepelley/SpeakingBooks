using System;
using System.IO;
using System.Windows;
using WatiN.Core;

namespace WpfSpeakingBooks
{
    public class PdfOutput : IBookOutput
    {
        private iTextSharp.text.Document doc;

        public string BookTitle { set; get; }

        public string Type { get { return "Pdf Books"; } }

        public string DirectoryName { set; get; }

        public void Start()
        {
            doc = new iTextSharp.text.Document();


            string fileName = string.Format("{0}.pdf", BookTitle);

            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(Path.Combine(DirectoryName, fileName), FileMode.Create));

            doc.Open();
        }

        public void CreatePage(Div div, int pageNumber, string title)
        {
            var styleSheet = new iTextSharp.text.html.simpleparser.StyleSheet();
            styleSheet.LoadStyle("docEmphasis", "font-weight", "bold");
            
            var section = doc;

            try
            {
                foreach(var element in div.Elements)
                {
                    if (element.ClassName == "docText")
                    {
                        //TextReader reader = new StringReader(element.OuterHtml);

                        //var pdfElements = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(reader, styleSheet,);

                        //foreach(var pdfElement in pdfElements)
                        //{
                        //    doc.Add(pdfElement);
                        //}


                        

                        section.Add(new iTextSharp.text.Paragraph(element.Text));
                        section.Add(new iTextSharp.text.Paragraph(" "));
                    }
                    else if (element.ClassName == "docFootnote")
                    {
                        var font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11F, iTextSharp.text.Font.ITALIC);

                        section.Add(new iTextSharp.text.Paragraph("docFootnote" + element.Text, font));
                        section.Add(new iTextSharp.text.Paragraph(" "));
                    }
                    else if (element.ClassName == "docList")
                    {
                        var font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11F, iTextSharp.text.Font.ITALIC);

                        section.Add(new iTextSharp.text.ListItem(element.Text, font));
                        section.Add(new iTextSharp.text.Paragraph(" "));
                    }
                    else if (element.ClassName == "docEmphasis")
                    {
                        //iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11F, iTextSharp.text.Font.ITALIC);

                        //doc.Add(new iTextSharp.text.ListItem("docEmphasis: " + element.Text, font));
                        //doc.Add(new iTextSharp.text.Paragraph(" "));
                    }
                    else if (element.ClassName == "docEmphStrong")
                    {
                        //iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11F, iTextSharp.text.Font.BOLDITALIC);

                        //doc.Add(new iTextSharp.text.ListItem("docEmphStrong: " + element.Text, font));
                        //doc.Add(new iTextSharp.text.Paragraph(" "));
                    }

                    else if (element.ClassName == "docNoteTitle")
                    {
                        var font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12F, iTextSharp.text.Font.BOLD);

                        section.Add(new iTextSharp.text.ListItem(element.Text, font));
                        section.Add(new iTextSharp.text.Paragraph(" "));
                    }
                    else if (element.ClassName == "docSection1Title")
                    {
                        var font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16F, iTextSharp.text.Font.BOLD);

                        section.Add(new iTextSharp.text.ListItem(element.Text, font));
                        section.Add(new iTextSharp.text.Paragraph(" "));
                    }
                    else if (element is WatiN.Core.Image)
                    {
                        try
                        {
                            var httpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(
                                "http://techbus.safaribooksonline.com/getfile?item=ZTY0Ly9yMzlnOGMwL2ltcHN0OGQxNDVhMzJnYXI3Zy9pLnBoMTBnYzIwc3BmaWo-");
                            var httpWebReponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                            var stream = httpWebReponse.GetResponseStream();

                            var img = System.Drawing.Image.FromStream(stream);

                            var image = (WatiN.Core.Image)element;
                            var pdfImage = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                            section.Add(pdfImage);
                        }
                        catch { }
                    }
                    else if (string.IsNullOrEmpty(element.Text) ||
                             string.IsNullOrEmpty(element.ClassName) ||
                             element.ClassName == "docLink" ||
                             element.ClassName == "htmlContent")
                    {

                    }
                    else
                    {
                        var r = element.OuterHtml;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //doc.Add(section);
            doc.NewPage();

            if (false)
            {
                doc.Close();
            }
            doc.Close();
        }

        public void Complete()
        {
            doc.Close();
        }
    }
}