using System.Collections.Generic;
using System.ComponentModel;

namespace window_calculator.WinType
{
    public class BaseWinType
    {
        private int height;
        private int width;
        private string type;
        private int number = 1;
        private int imageId;

        private double area;//窗户面积
        private double blArea;//玻璃面积

        /// <summary>
        /// 计算公式
        /// </summary>
        public virtual void Formula()
        {

        }

        /// <summary>
        /// 刷新计算结果到datagridview
        /// </summary>
        /// <param name="dgv"></param>
        public virtual void UpdateData()
        {

        }

        /// <summary>
        /// 个数
        /// </summary>
        [DisplayName("个数"),]
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value > 0)
                    number = value;
                else
                    MessageUtil.ShowTips("参数不合法！尺寸数量小于或等于零");
            }
        }

        /// <summary>
        /// 窗高
        /// </summary>
        [DisplayName("高"),]
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value > 0)
                    height = value;
                else
                    MessageUtil.ShowTips("参数不合法！高度小于或等于零");
            }
        }

        /// <summary>
        /// 窗宽
        /// </summary>
        [DisplayName("宽"),]
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value > 0)
                    width = value;
                else
                    MessageUtil.ShowTips("参数不合法！宽度小于或等于零");
            }
        }

        /// <summary>
        /// 窗户类型
        /// </summary>
        [Browsable(false),]
        internal string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value == string.Empty)
                    MessageUtil.ShowTips("参数不合法！窗户类型为空");
                else
                    type = value;
            }
        }

        /// <summary>
        /// 窗型图像Id
        /// </summary>
        [Browsable(false),]
        public int ImageId
        {
            get
            {
                return imageId;
            }
            set
            {
                imageId = value;
            }
        }

        [Browsable(false),]
        public double Area { get => area; set => area = value; }

        [Browsable(false),]
        public double BlArea { get => blArea; set => blArea = value; }
    }
}
