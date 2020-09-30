using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace window_calculator
{
    public partial class FormSelectType : Form
    {
        public FormSelectType()
        {
            InitializeComponent();
        }

        private int typeId;

        public int TypeId { get => typeId; set => typeId = value; }


        private void FormSelectType_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Text = "选择窗户类型";

            groupBox1.Text = "";
            groupBox1.Dock = DockStyle.Fill;
            InitDefectWall(groupBox1);
        }

        private void InitDefectWall(GroupBox groupBox)
        {
            TableLayoutPanel layoutPanel = new TableLayoutPanel();
            layoutPanel.Dock = DockStyle.Fill;
            groupBox.Controls.Add(layoutPanel);

            int row = 5; int col = 4; int picId = 0;
            DynamicLayout(layoutPanel, row, col);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    picId += 1;

                    PictureBox pb = new PictureBox();
                    pb.Name = picId.ToString();
                    pb.Dock = DockStyle.Fill;                  
                    pb.BackgroundImageLayout = ImageLayout.Zoom;
                    pb.BackColor = Color.White;                  
                    pb.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + pb.Name);
                    pb.Click += new EventHandler(pictureBoxClick);

                    layoutPanel.Controls.Add(pb);
                    layoutPanel.SetRow(pb, i);
                    layoutPanel.SetColumn(pb, j);
                   
                }
            }
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void pictureBoxClick(object o, EventArgs e)
        {
            PictureBox pb = (PictureBox)o;
          
            this.DialogResult = DialogResult.OK;
            this.TypeId = Convert.ToInt32(pb.Name);
            this.Close();
        }

        /// <summary>
        /// 动态布局
        /// </summary>
        /// <param name="layoutPanel">布局面板</param>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        private void DynamicLayout(TableLayoutPanel layoutPanel, int row, int col)
        {
            layoutPanel.RowCount = row;    //设置分成几行
            for (int i = 0; i < row; i++)
            {
                layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            }
            layoutPanel.ColumnCount = col;    //设置分成几列
            for (int i = 0; i < col; i++)
            {
                layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }
        }
    }
}
