using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HeartbeatWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private string currentFileName = String.Empty;
        private string currentParamValue = String.Empty;

        private void Uploading_Click(object sender, EventArgs e)
        {
            this.openFileDialog1 = new OpenFileDialog
            {
                // InitialDirectory = @"C:\Users\luxuda\Desktop",
                CheckFileExists = true,
                CheckPathExists = true,
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentFileName = openFileDialog1.FileName;
                this.richTextBox1.Text = $"source file : {openFileDialog1.FileName}";
            }
        }

        private void ReportGenerate_Click(object sender, EventArgs e)
        {
            List<string> errorLine = new List<string>();
            string resultTxt, errorTxt, completeInfo;
            List<string> resultLine =new List<string>();
            PhoneticGenerator generator = new PhoneticGenerator();
            NameMapper nameMapper = new NameMapper();
            if (this.currentParamValue == "full")
            {
                resultTxt = "result.txt";
                errorTxt = "error.txt";
                this.richTextBox1.AppendText($"start : {currentFileName} \r\n");
                List<string> deriveWords = generator.DeriveWords(currentFileName, ref errorLine);
                resultLine = nameMapper.NameMapping(this.richTextBox1, deriveWords, ref errorLine);
                completeInfo = "full";
            }
            else if (this.currentParamValue == "edReport")
            {
                resultTxt = "result_ed.txt";
                errorTxt = "error_ed.txt";
                this.richTextBox1.AppendText($"start : {currentFileName} \r\n");
                List<string> allLines = File.ReadLines(currentFileName, Encoding.UTF8).ToList();
                SampleReport sampleReport = new SampleReport();
                resultLine= sampleReport.GenerateEdReport(this.richTextBox1,allLines);
                completeInfo = "-ed Report";
            }
            else if (this.currentParamValue == "partial")
            {
                resultTxt = "result_partial.txt";
                errorTxt = "error_partial.txt";
                List<string> allLines = File.ReadLines(currentFileName, Encoding.UTF8).ToList();
                resultLine = nameMapper.NameMappingPartial(this.richTextBox1, allLines, ref errorLine);
                completeInfo = "partial";
            }
            else
            {
                resultTxt = "result_unknow.txt";
                errorTxt = "error_unknow.txt";
                completeInfo = "error : cannot find action";
            }

            Utils.DeleteFile(resultTxt);
            Utils.DeleteFile(errorTxt);

            File.WriteAllLines(errorTxt, errorLine, Encoding.UTF8);
            File.WriteAllLines(resultTxt, resultLine, Encoding.UTF8);
            this.richTextBox1.AppendText($"complete ! {completeInfo}");
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iSelectedIndex = checkedListBox1.SelectedIndex;
            this.currentParamValue = getParameter(iSelectedIndex);
            for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                if (ix != iSelectedIndex)
                    checkedListBox1.SetItemChecked(ix, false);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();
        }

        private string getParameter(int checkIndex)
        {
            if (checkIndex == 0)
            {
                return "full";
            }

            if (checkIndex == 1) //partial
            {
                return "partial";
            }

            if (checkIndex == 2) // -ed report
            {
                return "edReport";
            }

            return String.Empty;
        }
    }
}