using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace window_calculator.WinType
{
    /// <summary>
    /// 上下两开
    /// </summary>
    class Type7:BaseWinType
    {
        public Type7()
        {
            ImageId = 7;
        }

        private const string TYPE_NAME = "上下两开";
        private const int FB = 2;
        private const int SG = 1;
        private const int SH = 1;
        private const int XH = 1;
        private const int GQ = 4;
        private const int SXF = 4;
        private const int FG = 2;
        private const int GB = 2;

        private const int YTBL = 1;
        private const int NSBL = 2;

        private double fb;//封边
        private double sg;//上固
        private double sh;//上滑
        private double xh;//下滑
        private double gq;//光起
        private double sxf;//上下方
        private double fg;//封勾
        private double gb;//盖板

        public override void Formula()
        {

        }

        public override void UpdateData()
        {
            DataInfo dataInfo = new DataInfo();
            dataInfo.imageId = ImageId;
            dataInfo.type = TYPE_NAME;

            Dgv.AddData(dataInfo);
        }
    }
}
