using System;
using System.Windows.Forms;

namespace LaboratoryWork
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var consts = new Consts();
            var calculations = new Calculations(consts);
            //подумать над переносом сюда всех форм
            Application.Run(new MainForm(consts, calculations));
        }
    }
}

