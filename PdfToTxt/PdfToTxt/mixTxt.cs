using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Spire.Pdf;
using Spire.Pdf.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace newFPdfToTxt
{
    internal class mixTxt
    {
        public void toMixTxt(string outFile) {
            string path = System.Windows.Forms.Application.StartupPath;
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo dir2 = dir.Parent;
            DirectoryInfo dir3 = dir2.Parent;
            string @pdfDefault = dir3.Parent.FullName + @"\pdfDefault";
            Debug.WriteLine("@pdfDefault===" + @pdfDefault);
            Process p = new Process();
            String str = null;

            p.StartInfo.FileName = "cmd.exe";

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true; //不跳出cmd視窗


            p.Start();
            string com = @"cd " + @pdfDefault;
            p.StandardInput.WriteLine(@com);
            string l_strResult = outFile.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");

            com = @"type *.txt >> " + l_strResult;
            p.StandardInput.WriteLine(@com);
            p.StandardInput.WriteLine("exit");

            str = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
        }

    }
}
