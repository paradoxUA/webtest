using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Dal.Entities
{
    public class PageSettings
    {
        public Guid Id { get; set; }

        public string SettingsTitle { get; set; }
        public bool IsActive { get; set; }

        public string Title { get; set; }
        public string Address { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Logo { get; set; }
        public string LeftSponsor { get; set; }
        public string CenterSponsor { get; set; }
        public string RightSponsor { get; set; }
        public string QrCode { get; set; }

        public static PageSettings Default()
        {
            return new PageSettings()
            {
                SettingsTitle = "По умолчанию",
                Title = "CrazyKarting Rivera",
                Address = "Одесса ТРЦ Ривьера",
                Site = "www.crazykarting.com.ua",
                Email = "crazykarting@gmail.com",
                Phone = "3 8 048 770-66-69",

                Logo = GetPath($"{Directory.GetCurrentDirectory()}\\Images\\Logo\\CrazyKarting-150.png"),
                QrCode = GetPath($"{Directory.GetCurrentDirectory()}\\Images\\QrCodes\\code1.png"),
                LeftSponsor = GetPath($"{Directory.GetCurrentDirectory()}\\Images\\Sponsors\\l.jpg"),
                CenterSponsor = GetPath($"{Directory.GetCurrentDirectory()}\\Images\\Sponsors\\c.jpg"),
                RightSponsor = GetPath($"{Directory.GetCurrentDirectory()}\\Images\\Sponsors\\r.png"),
            };
        }

        public static string GetPath(string path)
        {
            return File.Exists(path) ? path : "";
        }
        public static BitmapImage GetBitmapImage(string path)
        {
            return File.Exists(path) ? new BitmapImage(new Uri(path)) : null;
        }
    }
}
