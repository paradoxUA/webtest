using ResultPrinter.Pages;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DocumentPrinter.Services
{
    public class PrinterService
    {
        public static PageService PageSettings { get; set; } = new PageService();
        public PrinterService()
        {
        }

        public void Print(PrintDialog printDialog)
        {
            RaseResultPage page = new RaseResultPage(PageSettings);
            page.Print(printDialog);
        }
        public void ShowAdminPanel()
        {
            PageSettingsView window = new PageSettingsView();
            window.ShowDialog();
        }


    }
}
