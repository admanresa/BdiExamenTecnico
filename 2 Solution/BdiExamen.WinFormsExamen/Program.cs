using System;
using System.Windows.Forms;

namespace BdiExamen.WinFormsExamen
{
    // Punto de entrada principal para la aplicación
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmExamen());
        }
    }
}
