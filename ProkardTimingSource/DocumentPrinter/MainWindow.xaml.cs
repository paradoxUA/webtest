using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Media;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Text;
using System.Printing;
using System.Collections.ObjectModel;
using DocumentPrinter.Models;
using DocumentPrinter.Services;

namespace DocumentPrinter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PrinterService ps = new PrinterService();
        public RaseResultPage resultPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            PageService PageData = new PageService();
            resultPage = new RaseResultPage(PageData);
            this.DataContext = resultPage;
        }

        private void bPrint_Click(object sender, RoutedEventArgs e)
        {
            ps.Print(resultPage.printDocument, "",1);
            this.Close();
        }
    }
}
