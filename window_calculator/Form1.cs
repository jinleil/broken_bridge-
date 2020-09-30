using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using window_calculator.WinType;

namespace window_calculator
{
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }


        /// <summary>
        /// 最后选择的窗型
        /// </summary>
        private WinType.BaseWinType WinType;
        private PrintPDF printPDF;
        private Config config;


        private void Form1_Load(object sender, EventArgs e) {

            Dgv.Init(dataGridView);

            // 读取软件配置，下料单上的抬头、店名、电话、地址等信息
            config = new Config();
            string msg = string.Empty;
            ReadAndWriteXML.ReadConfig(config, out msg);
            if (msg == "")
            {
                printPDF = new PrintPDF(config);
                string[] colors = config.Colors.Split('，');
                cmb_colors.DataSource = colors;
                string[] bltype = config.BLType.Split('，');
                cmb_blType.DataSource = bltype;
            }
            else
            {
                MessageUtil.ShowError(msg);
            }
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e) {
            if (dataGridView.Rows.Count == 0)
            {
                MessageUtil.ShowTips("没有可以打印的数据！");
                return;
            }

            string name = textBoxClientName.Text.Trim();
            string color = cmb_colors.Text.Trim();
            string bltype = cmb_blType.Text.Trim();
            printPDF.PrintPdf(dataGridView, Dgv.GetData(), name, bltype, color);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_delete_Click(object sender, EventArgs e) {
            if (dataGridView.CurrentCell == null)
                return;
            int index = dataGridView.CurrentCell.RowIndex;
            DialogResult dialogResult = MessageUtil.ShowYesNoAndTips("您确定删除吗？");
            if(dialogResult == DialogResult.Yes)
            {
                Dgv.DeleteData(index);
            }          
        }
        
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_set_Click(object sender, EventArgs e)
        {
            FormSet formSet = new FormSet(config);
            formSet.ShowDialog();
        }

        /// <summary>
        /// 查看历史
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_historyfile_Click(object sender, EventArgs e)
        {
            new History().ShowDialog();
        }


        // 添加新尺寸
        private void btn_addNewSize_Click(object sender, EventArgs e)
        {
            if (WinType == null)
            {
                MessageUtil.ShowTips("未选择窗型！");
                return;
            }
 
            WinType.Formula();
            WinType.UpdateData();        
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            WinType = (WinType.BaseWinType)propertyGrid1.SelectedObject;
        }

        private void btn_selectType_Click(object sender, EventArgs e)
        {
            //FormAllPicture formAllPicture = new FormAllPicture();
            FormSelectType formAllPicture = new FormSelectType();
            if (formAllPicture.ShowDialog() == DialogResult.OK)
            {
                int typeId = formAllPicture.TypeId;
                WinType = TypeFactory.getInstance().getType(typeId);
                propertyGrid1.SelectedObject = WinType;


                //或者直接这样写，name为资源中已有的名称
                pictureBoxMain.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + typeId);               
                pictureBoxMain.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }
    }
}
