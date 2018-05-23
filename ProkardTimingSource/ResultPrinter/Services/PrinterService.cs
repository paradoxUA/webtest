using ResultPrinter.Pages;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DocumentPrinter.Services
{
    public class PrinterService
    {
        public static PageService PageSettings { get; set; }
        public PrinterService()
        {
        }

        public void Print(PrintDialog printDialog)
        {
            RaseResultPage page = new RaseResultPage(PageSettings);
            page.Print(printDialog);
        }

		public void Print(PageService pageData, string printerName, int pagesCount)
		{
			var page = new RaseResultPage(pageData);
			page.Print(printerName, pagesCount);

		}
		public void ShowAdminPanel()
        {
            PageSettingsView window = new PageSettingsView();
            window.ShowDialog();
        }


    }
}
