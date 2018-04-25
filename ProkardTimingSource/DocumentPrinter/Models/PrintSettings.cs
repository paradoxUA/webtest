using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DocumentPrinter.Models
{
    public class PageSettings
    {
        public BitmapImage Logo { get; set; }
        public BitmapImage LeftSponsor { get; set; }
        public BitmapImage CenterSponsor { get; set; }
        public BitmapImage RightSponsor { get; set; }
        public BitmapImage QrCode { get; set; }

        public string Title { get; set; }
        public string Address { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
