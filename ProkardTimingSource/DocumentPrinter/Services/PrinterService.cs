using System.Printing;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DocumentPrinter.Services
{
    public class PrinterService
    {
        public PrinterService()
        {
        }

        public void Print(FlowDocumentScrollViewer page, string printerName, int pageCount)
        {
            PrintDialog dlg = new PrintDialog();
            PrintServer myPrintServer = new PrintServer();
            PrintQueue queue = null;

            PrintQueueCollection myPrintQueues = myPrintServer.GetPrintQueues();

            foreach (var que in myPrintQueues)
                if (que.FullName.Equals(printerName))
                    queue = que;

            if (queue == null)
                queue = new LocalPrintServer().DefaultPrintQueue;

            dlg.PrintQueue = queue;
            dlg.PrintTicket.CopyCount = pageCount;
            dlg.PrintTicket.PageOrientation = PageOrientation.Portrait;

            page.Document.PageHeight = dlg.PrintableAreaHeight;
            page.Document.PageWidth = dlg.PrintableAreaWidth;

            dlg.PrintDocument(((IDocumentPaginatorSource)page.Document).DocumentPaginator, "Результаты заезда");
        }
    }
}
