using System;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Prokard_Timing
{
    static class Program
    {
        public static string programFolder;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            programFolder = Directory.GetCurrentDirectory();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                      

            ProgramActivation pa = new ProgramActivation();
            
            if (args.Length > 0)
            {
                if(args[0] == "/s")
                {
                    pa.SaveKey();
                }
            }
             
           // pa.SaveKey();
            int activate =pa.KeyIsActive();

            switch (activate)
            {
                case 0: MessageBox.Show("Файл с ключом не найден!","Активация",MessageBoxButtons.OK,MessageBoxIcon.Error); Application.Exit(); break;
                case 1:
                    {
                        
                        Thread.CurrentThread.Priority = ThreadPriority.Highest;
                        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

                        Application.Run(new MainForm());

                    } break;
                case 2: MessageBox.Show("Неверный ключ программы","Активация",MessageBoxButtons.OK,MessageBoxIcon.Error); Application.Exit(); break;
                default: Application.Exit(); break;
            }






        }
    }
}
