using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaboratoryWork
{
    class CommonHelper
    {
        public static void MyHelp(Form ThisForm, string NameHtmlPage)
        {
            try
            {
                Help.ShowHelp(ThisForm, "Help.chm", HelpNavigator.Topic, NameHtmlPage);
            }
            catch
            {
                MessageBox.Show("Не найден файл справки");
            }
        }
    }
}
