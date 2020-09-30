using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace window_calculator.WinType
{
    /// <summary>
    /// 两开 下摇头无中柱
    /// </summary>
    class Type2:BaseWinType
    {
        private const string TYPE_NAME = "两开下摇头无中柱";
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

        public Type2()
        {
            ImageId = 2;
        }

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
