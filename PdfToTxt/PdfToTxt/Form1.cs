using newFPdfToTxt;
using System.Reflection.PortableExecutable;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Threading;

namespace PdfToTxt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog choosefiletodecodeDialog = new OpenFileDialog()
            {
                Title = "文件位置"
            };

            DialogResult dialogResult = choosefiletodecodeDialog.ShowDialog();
            string message = "";
            
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            else if (choosefiletodecodeDialog.CheckFileExists)
            {
                
                try
                {
                    string ext = choosefiletodecodeDialog.DefaultExt;
                    string filetodecodepath = choosefiletodecodeDialog.FileName;
                    StreamReader streamReader = new StreamReader(filetodecodepath);
                    SetLoading(true);
                    ////////不覆蓋原檔////////
                    string decodedfilepath = filetodecodepath.Replace("." + ext, "TXT." + ext);
                    string result;
                    result = Path.ChangeExtension(decodedfilepath, "txt");//新位置

                    ////////pdfDefault資料夾位置////////
                   
                    string path = System.Windows.Forms.Application.StartupPath;
                    DirectoryInfo dir = new DirectoryInfo(path);
                    DirectoryInfo dir2 = dir.Parent;
                    DirectoryInfo dir3 = dir2.Parent;
                    string @pdfDefault = dir3.Parent.FullName + @"\pdfDefault";
                    Debug.WriteLine("@pdfDefault===" + @pdfDefault);
                    ////////pdfDefault資料夾清空////////
                    DirectoryInfo di = new DirectoryInfo(@pdfDefault);
                    Debug.WriteLine("@pdfDefault==="+ @pdfDefault);
                    FileInfo[] files = di.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                    }

                    ////////拆分PDF檔////////
                    pPdfSplit ppdfSplit = new pPdfSplit();
                    ppdfSplit.iTextSharpPdfSplit(@filetodecodepath, @pdfDefault);

                    ////////PDF轉TXT////////
                    pdfToTxt pdftotxt = new pdfToTxt();
                    for (int i = 1; i <= new PdfReader(@filetodecodepath).NumberOfPages; i++)
                    {
                        pdftotxt.toTxt(@pdfDefault + @"\iTextSharp_拆分_" + i + ".pdf", i);
                    }
                    ////////合併TXT，並存到result裡面////////
                    mixTxt mixtxt = new mixTxt();

                    mixtxt.toMixTxt(result);
                    message = "PDF轉TXT完成";
                }
                catch (Exception ex)
                {
                    message = "失敗，錯誤如下：" + ex.ToString();
                }
                
                SetLoading(false);
                MessageBox.Show(message);
            }
            else
            {
                
                SetLoading(false);
                MessageBox.Show(message);
            }
        }
        private void SetLoading(bool displayLoader)
        {
            if (displayLoader) 
            { 
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
        }
        
    }
}