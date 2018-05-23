using DocumentPrinter.Services;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DocumentPrinter
{
    public partial class RaseResultPage : UserControl
    {

        public RaseResultPage(PageService pageService)
        {
            InitializeComponent();

            this.DataContext = pageService;
			pageService.RaisePropertyChanged();
        }

        public void Print(PrintDialog dlg)
        {

            fDocument.PageHeight = dlg.PrintableAreaHeight;
            fDocument.PageWidth = dlg.PrintableAreaWidth;

            dlg.PrintDocument(((IDocumentPaginatorSource)fDocument).DocumentPaginator, "Результаты заезда");
        }

        public void Print(string printerName, int pageCount)
        {
			(DataContext as PageService).RaisePropertyChanged();

			fDocument.DataContext = DataContext;

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

            fDocument.PageHeight = dlg.PrintableAreaHeight;
            fDocument.PageWidth = dlg.PrintableAreaWidth;
						
            dlg.PrintDocument(((IDocumentPaginatorSource)fDocument).DocumentPaginator, "Результаты заезда");
        }
    }
}
