using System;
using System.IO;
using System.Windows.Forms;

namespace window_calculator
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }

        FileInfo[] fileInfos;
        string path = @"C:\OutPDF";

        private void History_Load(object sender, EventArgs e)
        {
            bool re = Directory.Exists(path);
            if (re == false)
            {
                Directory.CreateDirectory(path);
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            fileInfos = directoryInfo.GetFiles();
            listBox1.DataSource = fileInfos;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0)
            {
                string filepath = fileInfos[index].FullName;
                PrintPDF printPDF = new PrintPDF();
                printPDF.Pirnt(filepath);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0)
            {
                string filepath = fileInfos[index].FullName;
               
                DialogResult dialogResult = MessageUtil.ShowYesNoAndTips("您确定要删除此文件吗？");
                if(dialogResult == DialogResult.Yes)
                {
                    FileInfo fileInfo = new FileInfo(filepath);
                    fileInfo.Delete();

                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    fileInfos = directoryInfo.GetFiles();
                    listBox1.DataSource = fileInfos;
                }                            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0)
            {
                string filepath = fileInfos[index].FullName;
                PrintPreview printPreview = new PrintPreview(filepath);
                printPreview.ShowDialog();
            }
        }
    }
}
