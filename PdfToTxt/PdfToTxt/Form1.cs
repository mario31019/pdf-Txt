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
                Title = "����m"
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
                    ////////���л\����////////
                    string decodedfilepath = filetodecodepath.Replace("." + ext, "TXT." + ext);
                    string result;
                    result = Path.ChangeExtension(decodedfilepath, "txt");//�s��m

                    ////////pdfDefault��Ƨ���m////////
                   
                    string path = System.Windows.Forms.Application.StartupPath;
                    DirectoryInfo dir = new DirectoryInfo(path);
                    DirectoryInfo dir2 = dir.Parent;
                    DirectoryInfo dir3 = dir2.Parent;
                    string @pdfDefault = dir3.Parent.FullName + @"\pdfDefault";
                    Debug.WriteLine("@pdfDefault===" + @pdfDefault);
                    ////////pdfDefault��Ƨ��M��////////
                    DirectoryInfo di = new DirectoryInfo(@pdfDefault);
                    Debug.WriteLine("@pdfDefault==="+ @pdfDefault);
                    FileInfo[] files = di.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                    }

                    ////////���PDF��////////
                    pPdfSplit ppdfSplit = new pPdfSplit();
                    ppdfSplit.iTextSharpPdfSplit(@filetodecodepath, @pdfDefault);

                    ////////PDF��TXT////////
                    pdfToTxt pdftotxt = new pdfToTxt();
                    for (int i = 1; i <= new PdfReader(@filetodecodepath).NumberOfPages; i++)
                    {
                        pdftotxt.toTxt(@pdfDefault + @"\iTextSharp_���_" + i + ".pdf", i);
                    }
                    ////////�X��TXT�A�æs��result�̭�////////
                    mixTxt mixtxt = new mixTxt();

                    mixtxt.toMixTxt(result);
                    message = "PDF��TXT����";
                }
                catch (Exception ex)
                {
                    message = "���ѡA���~�p�U�G" + ex.ToString();
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