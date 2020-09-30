using Spire.Pdf;
using Spire.Pdf.AutomaticFields;
using Spire.Pdf.Graphics;
using Spire.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace window_calculator
{
    public class PrintPDF
    {
        public PrintPDF()
        {
            
        }
        public PrintPDF(Config config)
        {
            this.config = config;
        }

        Config config = new Config();
        private  double totalArea;
        private  double totalAreaBL;
        private  int totalNumber;

        /// <summary>
        /// 打印本机pdf
        /// </summary>
        /// <param name="filepath"></param>
        public void Pirnt(string filepath)
        {
            bool re = File.Exists(filepath);
            if (re == false)
            {
                MessageUtil.ShowError("本地目录不存在此文件！");
                return;
            }
            try
            {
                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromFile(filepath);

                PrintDocument printDoc = pdf.PrintDocument;
                printDoc.PrintController = new StandardPrintController();
                printDoc.Print();
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError("打印出现异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="filepath"></param>
        public void PirntPreview(string filepath, PrintPreviewControl ppc)
        {
            bool re = File.Exists(filepath);
            if (re == false)
            {
                MessageUtil.ShowError("本地目录不存在此文件！");
                return;
            }
            try
            {
                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromFile(filepath);

                pdf.Preview(ppc); 
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError("文件预览出现异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 创建pdf文件并打印
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="datas"></param>
        /// <param name="clientName"></param>
        /// <param name="blType"></param>
        /// <param name="color"></param>
        public void PrintPdf(DataGridView dgv, List<DataInfo> datas, string clientName,string blType,string color)
        {
            if(dgv.Rows.Count==0)
            {
                return;
            }
            
            totalArea = 0;
            totalAreaBL = 0;
            totalNumber = 0;

            for (int i = 0; i < datas.Count; i++)
            {
                totalArea += datas[i].total_area;
                totalAreaBL += datas[i].total_area_bl;
                totalNumber += datas[i].total_num;
            }
            totalArea = Math.Round(totalArea, 2);
            totalAreaBL = Math.Round(totalAreaBL, 2);
            //创建PdfDocument实例
            PdfDocument doc = new PdfDocument();
            
            //设置页面为横向
            doc.PageSettings.Orientation = PdfPageOrientation.Landscape;
            
            //创建PdfMargins对象，指定期望设置的页边距 
            PdfMargins margins = new PdfMargins(5, 25, 5, 45);
            
            // 设置纸张页边距
            doc.PageSettings.Margins = margins;

            //在文档模板的顶部和底部应用页眉页脚模板 
            doc.Template.Top = CreateHeaderTemplate(doc, margins, clientName, blType, color);
            doc.Template.Bottom = CreateFooterTemplate(doc, margins);
            
            //添加一页
            PdfPageBase page = doc.Pages.Add();
                      
            //创建PDF grid
            PdfGrid grid = new PdfGrid();

            //设置单元格内容和边框之间的距离
            grid.Style.CellPadding = new PdfPaddings(1, 1, 5, 5);
          
            //设置字体
            grid.Style.Font = new PdfTrueTypeFont(new Font("宋体", 11f), true);

            // 添加行和列       
            int rowCount = dgv.Rows.Count + 1;
            int colCount = 15;
            for (int i = 0; i < rowCount; i++)
            {
                grid.Rows.Add();
            }
            grid.Columns.Add(colCount);

            //设置列宽
            grid.Columns[0].Width = 30f;
            grid.Columns[1].Width = 70f;
            grid.Columns[2].Width = 80f;
            grid.Columns[3].Width = 50f;
            grid.Columns[4].Width = 50f;
            grid.Columns[5].Width = 50f;
            grid.Columns[6].Width = 50f;
            grid.Columns[7].Width = 50f;
            grid.Columns[8].Width = 50f;
            grid.Columns[9].Width = 50f;
            grid.Columns[10].Width = 50f;
            grid.Columns[11].Width = 50f;
            grid.Columns[12].Width = 50f;
            grid.Columns[13].Width = 80f;
            grid.Columns[14].Width = 80f;

            // 设置行高 (注释原因：设置了行高后，单元格内字写不下时，不会自动换行)
            //foreach (PdfGridRow row in grid.Rows)
            //{
            //    row.Height = 20f;
            //}

            // 创建表头
            List<string> headName = new List<string>();
            headName.Add("序号");
            headName.Add("尺寸(高x宽)");
            headName.Add("窗型图");
            headName.Add("边框");
            headName.Add("拼板");
            headName.Add("固定");
            headName.Add("上下滑");
            headName.Add("中柱");
            headName.Add("盖板");
            headName.Add("扣条");
            headName.Add("光企");
            headName.Add("勾企盖板");
            headName.Add("上下方");
            headName.Add("摇头玻璃");
            headName.Add("内扇玻璃");


            for (int i = 0; i < headName.Count; i++)
            {
                grid.Rows[0].Cells[i].Value = headName[i];            
            }
            // 向单元格写数据
            for (int i = 0; i < datas.Count; i++)
            {
                grid.Rows[1 + i].Cells[0].Value = datas[i].id.ToString();
                grid.Rows[1 + i].Cells[1].Value = datas[i].size;
                grid.Rows[1 + i].Cells[2].Value = getPdfImage(datas[i].imageId);
                grid.Rows[1 + i].Cells[3].Value = datas[i].bk;
                grid.Rows[1 + i].Cells[4].Value = datas[i].pb;
                grid.Rows[1 + i].Cells[5].Value = datas[i].gd;
                grid.Rows[1 + i].Cells[6].Value = datas[i].sxh;
                grid.Rows[1 + i].Cells[7].Value = datas[i].zz;
                grid.Rows[1 + i].Cells[8].Value = datas[i].gb;
                grid.Rows[1 + i].Cells[9].Value = datas[i].kt;
                grid.Rows[1 + i].Cells[10].Value = datas[i].gq;
                grid.Rows[1 + i].Cells[11].Value = datas[i].gqgb;
                grid.Rows[1 + i].Cells[12].Value = datas[i].sxf;
                grid.Rows[1 + i].Cells[13].Value = datas[i].ytbl;
                grid.Rows[1 + i].Cells[14].Value = datas[i].nsbl;
            }
            // 设置单元格内容居中显示
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    grid.Rows[i].Cells[j].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                }
            }
            PdfBorders borders = new PdfBorders();
            borders.All = new PdfPen(Color.Black, 0.1f);
            foreach (PdfGridRow pgr in grid.Rows)
            {
                foreach (PdfGridCell pgc in pgr.Cells)
                {
                    pgc.Style.Borders = borders;
                }
            }
            //在页面的指定位置绘制grid
            grid.Draw(page,new PointF(0,12));
            
            try
            {
                //保存文件
                string path = @"C:\OutPDF";
                bool re = Directory.Exists(path);
                if (re == false)
                {
                    Directory.CreateDirectory(path);
                }
                if (clientName != "")
                {
                    path += @"\" + clientName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                }
                else
                {
                    path += @"\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                }
                doc.SaveToFile(path);
                //静默打印PDF文档
                PrintDocument printDoc = doc.PrintDocument;
                printDoc.PrintController = new StandardPrintController();
                printDoc.Print();
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 将image图像转为pdfImage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private PdfGridCellContentList getPdfImage(int imageId)
        {
            SizeF imageSize = new SizeF(64, 36);
            //加载图片
            PdfGridCellContentList contentList = new PdfGridCellContentList();
            PdfGridCellContent content = new PdfGridCellContent();
            content.Image = PdfImage.FromImage((Bitmap)Properties.Resources.ResourceManager.GetObject("_" + imageId));
            content.ImageSize = imageSize;
            contentList.List.Add(content);
            return contentList;
        }

        private List<string> GetTableHeadName(DataGridView dataGridView)
        {
            List<string> headName = new List<string>();
            int columnsCount = dataGridView.Columns.Count;           
            for (int i = 0; i < columnsCount; i++)
            {
                headName.Add(dataGridView.Columns[i].HeaderText);
            }
            return headName;
        }
        private PdfPageTemplateElement CreateHeaderTemplate(PdfDocument doc, PdfMargins margins,string clientName,string blType,string color)
        {
            //获取页面大小
            SizeF pageSize = doc.PageSettings.Size;

            //创建PdfPageTemplateElement对象headerSpace，即作页眉模板
            PdfPageTemplateElement headerSpace = new PdfPageTemplateElement(pageSize.Width, margins.Top);
            headerSpace.Foreground = false;

            //声明x,y两个float型变量
            float x = margins.Left;
            float y = 1;


            // 在headerSpace中绘文字
            PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("微软雅黑", 18f, FontStyle.Regular), true);
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Bottom);
            String headerText = config.TableTitle;
            headerSpace.Graphics.DrawString(headerText, font, PdfBrushes.Black, new PointF(420,23), format);

            headerText = "客户姓名：" + clientName + "\t颜色：" + color + "\t玻璃：" + blType;
            font = new PdfTrueTypeFont(new Font("宋体", 10f, FontStyle.Regular), true);
            format = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Bottom);
            headerSpace.Graphics.DrawString(headerText, font, PdfBrushes.Black, new PointF(5, 23), format);

            headerText = "时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            font = new PdfTrueTypeFont(new Font("宋体", 10f, FontStyle.Regular), true);
            format = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Bottom);
            headerSpace.Graphics.DrawString(headerText, font, PdfBrushes.Black, new PointF(660, 23), format);

            //在headerSpace中绘制线段
            PdfPen pen = new PdfPen(PdfBrushes.Gray, 0.5f);
            headerSpace.Graphics.DrawLine(pen, x, y + margins.Top - 2, pageSize.Width - x, y + margins.Top - 2);

            //返回headerSpace
            return headerSpace;
        }

        // 创建页脚模板
        private PdfPageTemplateElement CreateFooterTemplate(PdfDocument doc, PdfMargins margins)
        {
            //获取页面大小
            SizeF pageSize = doc.PageSettings.Size;

            //创建PdfPageTemplateElement对象footerSpace，即页脚模板
            PdfPageTemplateElement footerSpace = new PdfPageTemplateElement(pageSize.Width, margins.Bottom);
            footerSpace.Foreground = false;

            //声明x,y两个float型变量
            float x = margins.Left;
            float y = 20;

            //在footerSpace中绘制线段
            PdfPen pen = new PdfPen(PdfBrushes.Gray, 0.5f);
            footerSpace.Graphics.DrawLine(pen, x, y, pageSize.Width - x, y);

            //在footerSpace中绘制文字
            y = y + 3;
            PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("宋体", 10f, FontStyle.Regular), true);
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left);
            String footerText = string.Format("合计{0}个，总面积：{1}㎡，玻璃面积：{2}㎡\n电话：{3}  地址：{4}", 
                                            totalNumber, 
                                            totalArea, 
                                            totalAreaBL, 
                                            config.PhoneNumber, 
                                            config.Address);
            footerSpace.Graphics.DrawString(footerText, font, PdfBrushes.Black, x, y, format);

            //在footerSpace中绘制当前页码和总页码
            PdfPageNumberField number = new PdfPageNumberField();
            PdfPageCountField count = new PdfPageCountField();
            PdfCompositeField compositeField = new PdfCompositeField(font, PdfBrushes.Black, "第{0}页/共{1}页", number, count);
            compositeField.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Top);
            SizeF size = font.MeasureString(compositeField.Text);
            compositeField.Bounds = new RectangleF(pageSize.Width -70- x - size.Width, y, size.Width, size.Height);
            compositeField.Draw(footerSpace.Graphics);

            //返回footerSpace
            return footerSpace;
        }
    }
}
