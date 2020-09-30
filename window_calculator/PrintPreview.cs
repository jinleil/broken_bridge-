using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace window_calculator
{
    public partial class PrintPreview : Form
    {
        public PrintPreview(string filepath)
        {
            InitializeComponent();
            this.filepath = filepath;
        }

        string filepath = "";

        private void PrintPreview_Load(object sender, EventArgs e)
        {
            PrintPDF printPDF = new PrintPDF();
            printPDF.PirntPreview(filepath, printPreviewControl1);
        }
    }
}
