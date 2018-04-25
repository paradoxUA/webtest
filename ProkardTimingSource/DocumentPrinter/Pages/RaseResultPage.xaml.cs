using DocumentPrinter.Services;
using System.Windows.Controls;

namespace DocumentPrinter
{
    public partial class RaseResultPage : UserControl
    {
        public RaseResultPage(PageService pageService)
        {
            InitializeComponent();

            this.DataContext = pageService;
        }
    }
}
