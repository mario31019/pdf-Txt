using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Spire.Pdf;
using Spire.Pdf.Utilities;

namespace newFPdfToTxt
{
    internal class pdfToTxt
    {
        public void toTxt(string input,int si) {
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(@input);
            StringBuilder builder = new StringBuilder();

            //抽取表格
            PdfTableExtractor extractor = new PdfTableExtractor(pdf);
            PdfTable[] tableLists = null;
            for (int pageIndex = 0; pageIndex < pdf.Pages.Count; pageIndex++)
            {
                tableLists = extractor.ExtractTable(pageIndex);
                if (tableLists != null && tableLists.Length > 0)
                {
                    foreach (PdfTable table in tableLists)
                    {
                        int row = table.GetRowCount();
                        int column = table.GetColumnCount();
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < column; j++)
                            {
                                string text = table.GetText(i, j);

                                string l_strResult = text.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                builder.Append(l_strResult + " ");
                            }
                            builder.Append("\r\n");
                        }
                    }
                }
            }
            string path = System.Windows.Forms.Application.StartupPath;
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo dir2 = dir.Parent;
            DirectoryInfo dir3 = dir2.Parent;
            string @pdfDefault = dir3.Parent.FullName + @"\pdfDefault";
            Debug.WriteLine("@pdfDefault===" + @pdfDefault);
            //保存提取的表格内容到txt文档
            File.WriteAllText(@pdfDefault + @"\" + si + ".txt", builder.ToString());
        }

        

    }
}
