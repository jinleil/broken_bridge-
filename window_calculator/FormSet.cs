using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace window_calculator
{
    public partial class FormSet : Form
    {
        public FormSet(Config config)
        {
            InitializeComponent();
            this.config = config;
        }

        Config config;

        private void FormSet_Load(object sender, EventArgs e)
        {                
            propertyGrid1.SelectedObject = this.config;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string msg = "";
            ReadAndWriteXML.WriteConfig(config,out msg);
            if (msg != "")
            {
                MessageUtil.ShowError("保存失败！" + msg);
            }
        }
    }

    public class Config
    {
        private string tableTitle;
        private string phoneNumber;
        private string address;
        private string colors;
        private string blType;

        /// <summary>
        /// 表名
        /// </summary>
        [DisplayName("表名")]
        public string TableTitle
        {
            get
            {
                return tableTitle;
            }
            set
            {
                if(value != "")
                {
                    tableTitle = value;
                }
            }
        }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (value != "")
                {
                    phoneNumber = value;
                }
            }
        }

        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (value != "")
                {
                    address = value;
                }
            }
        }

        /// <summary>
        /// 型材颜色
        /// </summary>
        [DisplayName("型材颜色"), Description("多种颜色用中文逗号“，”分隔")]
        public string Colors
        {
            get
            {
                return colors;
            }
            set
            {
                if (value != "")
                {
                    colors = value;
                }
            }
        }

        /// <summary>
        /// 玻璃类型
        /// </summary>
        [DisplayName("玻璃类型"),Description("多种类型用中文逗号“，”分隔")]
        public string BLType
        {
            get
            {
                return blType;
            }
            set
            {
                if (value != "")
                {
                    blType = value;
                }
            }
        }
    }

    public class ReadAndWriteXML
    {
        static string filePath = Environment.CurrentDirectory + "\\config.xml";

        private static XmlElement Init(ref XmlDocument doc)
        {
            XmlElement root;
            if (File.Exists(filePath))
            {
                // 参数存在，加载XML
                doc.Load(filePath);
                // 获得根节点
                root = doc.DocumentElement;
            }
            else
            {
                // 参数不存在，创建xml文档第一行
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                // 创建根节点
                root = doc.CreateElement("root");
                doc.AppendChild(root);
            }

            return root;
        }

        /// <summary>
        /// 系统配置文件读操作
        /// </summary>
        /// <param name="systemConfig"></param>
        /// <param name="msg"></param>
        public static void ReadConfig(Config config, out string msg)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = Init(ref doc);
            msg = string.Empty;
            try
            {
                PropertyInfo[] PropertyList = config.GetType().GetProperties();
                foreach (PropertyInfo item in PropertyList)
                {
                    XmlNode xn = root.SelectSingleNode(item.Name);
                    if (xn != null)
                    {
                        if (xn.InnerText.Trim() == "")
                        {
                            msg = "节点" + item.Name + "没有值！";
                            return;
                        }
                        else
                        {
                            item.SetValue(config, xn.InnerText.Trim(), null);
                        }
                    }
                    else
                    {
                        msg = "节点" + item.Name + "不存在！";
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        /// <summary>
        /// 系统配置文件写操作
        /// </summary>
        /// <param name="systemConfig"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool WriteConfig(Config config, out string msg)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = Init(ref doc);
            msg = string.Empty;
            try
            {
                PropertyInfo[] PropertyList = config.GetType().GetProperties();
                foreach (PropertyInfo item in PropertyList)
                {
                    XmlNode xn = root.SelectSingleNode(item.Name);
                    if (xn != null)
                        xn.InnerText = item.GetValue(config, null).ToString();
                    else
                    {
                        xn = doc.CreateElement(item.Name);
                        xn.InnerText = item.GetValue(config, null).ToString();
                        root.AppendChild(xn);
                    }
                }
                doc.Save(filePath);
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

        }
    }

}
