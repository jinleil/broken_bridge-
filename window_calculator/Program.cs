using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace window_calculator {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new Form1();
            Application.Run(mainForm);
        }
        public static Form1 mainForm;
    }
}
