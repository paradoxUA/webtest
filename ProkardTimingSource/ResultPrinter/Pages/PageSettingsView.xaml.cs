using Dal.Entities;
using Dal.Interfaces;
using Dal.Providers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ResultPrinter.Pages
{
    public partial class PageSettingsView : Window
    {
        ILiteDbProvider _provider;
        PageSettings _pageSettings;


        public ObservableCollection<MyImage> SponsorCollection { get; set; }
        public ObservableCollection<MyImage> QrcodeCollection { get; set; }
        public ObservableCollection<MyImage> LogoCollection { get; set; }


        public PageSettingsView()
        {
            LogoCollection = new ObservableCollection<MyImage>();
            QrcodeCollection = new ObservableCollection<MyImage>();
            SponsorCollection = new ObservableCollection<MyImage>();

            _provider = LiteDbProvider.Create();
            _pageSettings = _provider.GetPageSettings();


            InitializeComponent();

            DirectoryGuard();
            ClubTitle = _pageSettings.Title;
            Address = _pageSettings.Address;
            Site = _pageSettings.Site;
            Email = _pageSettings.Email;
            Phone = _pageSettings.Phone;

            DataContext = this;
            
            InitImages();

            cbLogo.ItemsSource = LogoCollection;
            cbQrCode.ItemsSource = QrcodeCollection;
            cbLeftSponsor.ItemsSource = SponsorCollection;
            cbRightSponsor.ItemsSource = SponsorCollection;
            cbCenterSponsor.ItemsSource = SponsorCollection;

            InitSelection();
        }

        public BitmapImage Logo { get; set; }
        public BitmapImage LeftSponsor { get; set; }
        public BitmapImage CenterSponsor { get; set; }
        public BitmapImage RightSponsor { get; set; }
        public BitmapImage QrCode { get; set; }

        public string ClubTitle { get; set; }
        public string Address { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        private void InitImages()
        {
            var sponsors = Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Images\\Sponsors");
            var qrcodes = Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Images\\QrCodes");
            var logos = Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Images\\Logo");
            LogoCollection.Add(new MyImage());
            QrcodeCollection.Add(new MyImage());
            SponsorCollection.Add(new MyImage());

            foreach (var im in logos)
                LogoCollection.Add(new MyImage(im));

            foreach (var im in qrcodes)
                QrcodeCollection.Add(new MyImage(im));

            foreach (var im in sponsors)
                SponsorCollection.Add(new MyImage(im));
        }
        private void InitSelection()
        {
            cbLogo.SelectedItem = GetSelectedItem(cbLogo.Items, _pageSettings.Logo);//  GetSelectedItem(cbLogo.Items,_pageSettings.Logo);
            cbQrCode.SelectedItem = GetSelectedItem(cbQrCode.Items, _pageSettings.QrCode);
            cbLeftSponsor.SelectedItem = GetSelectedItem(cbLeftSponsor.Items, _pageSettings.LeftSponsor);
            cbRightSponsor.SelectedItem = GetSelectedItem(cbRightSponsor.Items, _pageSettings.RightSponsor);
            cbCenterSponsor.SelectedItem = GetSelectedItem(cbCenterSponsor.Items, _pageSettings.CenterSponsor);
        }

        private object GetSelectedItem(ItemCollection items, string path)
        {
            foreach (var item in items)
            {
                if (item != null && ((MyImage)item).Path.Equals(path))
                    return item;
            }
            return null;
        }

        private void DirectoryGuard()
        {
            List<string> path1 = new List<string>()
            {
                $"{Directory.GetCurrentDirectory()}\\Images\\Logo",
                $"{Directory.GetCurrentDirectory()}\\Images\\QrCodes",
                $"{Directory.GetCurrentDirectory()}\\Images\\Sponsors"
            };

            path1.ForEach(x =>
            {
                if (!Directory.Exists(x))
                    Directory.CreateDirectory(x);
            });
        }

        #region
        public void ClubTitleChanged()
        {
            _pageSettings.Title = ClubTitle;
            _provider.SavePageSettings(_pageSettings);
        }
        public void AddressChanged()
        {
            _pageSettings.Address = Address;
            _provider.SavePageSettings(_pageSettings);
        }
        public void SiteChanged()
        {
            _pageSettings.Site = Site;
            _provider.SavePageSettings(_pageSettings);
        }
        public void EmailChanged()
        {
            _pageSettings.Email = Email;
            _provider.SavePageSettings(_pageSettings);
        }
        public void PhoneChanged()
        {
            _pageSettings.Phone = Phone;
            _provider.SavePageSettings(_pageSettings);
        }
        #endregion
        private void cbQrCode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            var temp = sender as ComboBox;
            var img = temp.SelectedItem as MyImage;
            if (img == null) return;

            _pageSettings.QrCode = img.Path;
            _provider.SavePageSettings(_pageSettings);
        }

        private void cbLogo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = sender as ComboBox;
            var img = temp.SelectedItem as MyImage;
            if (img == null) return;

            _pageSettings.Logo = img.Path;
            _provider.SavePageSettings(_pageSettings);
        }

        private void cbLeftSponsor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = sender as ComboBox;
            var img = temp.SelectedItem as MyImage;
            if (img == null) return;

            _pageSettings.LeftSponsor = img.Path;
            _provider.SavePageSettings(_pageSettings);
        }

        private void cbRightSponsor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = sender as ComboBox;
            var img = temp.SelectedItem as MyImage;
            if (img == null) return;

            _pageSettings.RightSponsor = img.Path;
            _provider.SavePageSettings(_pageSettings);
        }

        private void cbCenterSponsor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = sender as ComboBox;
            var img = temp.SelectedItem as MyImage;
            if (img == null) return;

            _pageSettings.CenterSponsor = img.Path;
            _provider.SavePageSettings(_pageSettings);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = sender as TextBox;
            if (box == null) return;

            _pageSettings.Title = box.Text;
            _provider.SavePageSettings(_pageSettings);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

            var box = sender as TextBox;
            if (box == null) return;

            _pageSettings.Address = box.Text;
            _provider.SavePageSettings(_pageSettings);
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

            var box = sender as TextBox;
            if (box == null) return;

            _pageSettings.Site = box.Text;
            _provider.SavePageSettings(_pageSettings);
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

            var box = sender as TextBox;
            if (box == null) return;

            _pageSettings.Email = box.Text;
            _provider.SavePageSettings(_pageSettings);
        }

        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {

            var box = sender as TextBox;
            if (box == null) return;

            _pageSettings.Phone = box.Text;
            _provider.SavePageSettings(_pageSettings);
        }

        private void Window_Initialized(object sender, System.EventArgs e)
        {
        }
    }

    public class MyImage
    {
        public string Path { get; set; }
        public BitmapImage Image { get; set; }

        public MyImage()
        {
            Path = "";
            Image = null;
        }

        public MyImage(string path)
        {
            Path = path;
            Image = PageSettings.GetBitmapImage(path);
        }
    }
}
