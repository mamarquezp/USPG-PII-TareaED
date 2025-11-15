using USPG_PII_TareaED.Model;
using USPG_PII_TareaED.Playlist;
using System.Windows.Forms;

namespace USPG_PII_TareaED
{
    internal static class Program
    {
        /// <summary>
        ///  Punto de entrada principal de la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new PlayerForm());
        }
    }
}