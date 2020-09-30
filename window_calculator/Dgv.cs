using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace window_calculator
{
    public class Dgv
    {

        static DataGridView _dgv;
        static List<DataInfo> list = new List<DataInfo>();

        public static void Init(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 13, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("微软雅黑", 12, FontStyle.Regular);
            dgv.RowTemplate.Height = 50;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;// 单击选中整行
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // 单元格自动换行
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            // 内容居中显示
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "序号";
            acCode.DataPropertyName = "id";
            acCode.HeaderText = "序号";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "尺寸";
            acCode.DataPropertyName = "size";
            acCode.HeaderText = "尺寸(高x宽)";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "窗型";
            acCode.DataPropertyName = "type";
            acCode.HeaderText = "窗型";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "边框";
            acCode.DataPropertyName = "bk";
            acCode.HeaderText = "边框";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "拼板";
            acCode.DataPropertyName = "pb";
            acCode.HeaderText = "拼板";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "固定";
            acCode.DataPropertyName = "gd";
            acCode.HeaderText = "固定";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "上下滑";
            acCode.DataPropertyName = "sxh";
            acCode.HeaderText = "上下滑";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "中柱";
            acCode.DataPropertyName = "zz";
            acCode.HeaderText = "中柱";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "盖板";
            acCode.DataPropertyName = "gb";
            acCode.HeaderText = "盖板";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "扣条";
            acCode.DataPropertyName = "kt";
            acCode.HeaderText = "扣条";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "光企";
            acCode.DataPropertyName = "gq";
            acCode.HeaderText = "光企";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "勾企盖板";
            acCode.DataPropertyName = "gqgb";
            acCode.HeaderText = "勾企盖板";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "上下方";
            acCode.DataPropertyName = "sxf";
            acCode.HeaderText = "上下方";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "摇头玻璃";
            acCode.DataPropertyName = "ytbl";
            acCode.HeaderText = "摇头玻璃";
            dgv.Columns.Add(acCode);

            acCode = new DataGridViewTextBoxColumn();
            acCode.Name = "内扇玻璃";
            acCode.DataPropertyName = "nsbl";
            acCode.HeaderText = "内扇玻璃";
            dgv.Columns.Add(acCode);

            dgv.DataSource = new BindingList<DataInfo>(list);
            _dgv = dgv;
        }

        public static void AddData(DataInfo dataInfo)
        {
            list.Add(dataInfo);
            dataInfo.id = list.Count;
            _dgv.DataSource = new BindingList<DataInfo>(list);
            _dgv.FirstDisplayedScrollingRowIndex = _dgv.Rows.Count - 1;
            _dgv.CurrentCell = _dgv.Rows[_dgv.Rows.Count - 1].Cells[0];
        }

        public static void DeleteData(int index)
        {
            list.RemoveAt(index);
            for(int i = 0; i < list.Count; i++)
            {
                list[i].id = i + 1;
            }
            _dgv.DataSource = new BindingList<DataInfo>(list);
        }

        public static List<DataInfo> GetData()
        {
            return list;
        }
    }


    public class DataInfo
    {
        [DescriptionAttribute("序号")]
        public int id { get; set; }

        [DescriptionAttribute("尺寸(高x宽)")]
        public string size { get; set; }

        [DescriptionAttribute("窗型")]
        public string type { get; set; }

        [DescriptionAttribute("窗型图")]
        internal int imageId { get; set; }

        [DescriptionAttribute("边框")]
        public string bk { get; set; }

        [DescriptionAttribute("拼板")]
        public string pb { get; set; }

        [DescriptionAttribute("固定")]
        public string gd { get; set; }

        [DescriptionAttribute("上下滑")]
        public string sxh { get; set; }

        [DescriptionAttribute("中柱")]
        public string zz { get; set; }

        [DescriptionAttribute("盖板")]
        public string gb { get; set; }

        [DescriptionAttribute("扣条")]
        public string kt { get; set; }

        [DescriptionAttribute("光企")]
        public string gq { get; set; }

        [DescriptionAttribute("勾企盖板")]
        public string gqgb { get; set; }

        [DescriptionAttribute("上下方")]
        public string sxf { get; set; }

        [DescriptionAttribute("摇头玻璃")]
        public string ytbl { get; set; }

        [DescriptionAttribute("内扇玻璃")]
        public string nsbl { get; set; }

        internal double total_area { get; set; }
        internal double total_area_bl { get; set; }
        internal int total_num { get; set; }
    }
}
