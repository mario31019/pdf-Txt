using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

namespace newFPdfToTxt
{
    internal class pPdfSplit
    {
        public void iTextSharpPdfSplit(string inFile,string outFile)
        {
            using (var reader = new PdfReader(inFile))
            {
                // 注意起始頁是從1開始的
                Debug.WriteLine("@pdfDefault===" + outFile);
                for (int i = 1; i <= new PdfReader(inFile).NumberOfPages; i++)
                {
                    using (var sourceDocument = new Document(reader.GetPageSizeWithRotation(i))) 
                    {
                        var pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(@outFile + @"\iTextSharp_拆分_" + i+".pdf", System.IO.FileMode.Create));
                        sourceDocument.Open();
                        var importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                        pdfCopyProvider.AddPage(importedPage);
                    }
                }
            }
        }
    }
}
