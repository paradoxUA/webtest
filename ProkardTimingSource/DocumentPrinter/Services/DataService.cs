using DocumentPrinter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DocumentPrinter.Services
{
    public class PageService
    {
        public RaceInfo Info { get; set; }
        public PageSettings PageSettings { get; set; }

        public PageService()
        {
            DirectoryGuard();

            TestDataLoad();
        }

        private void TestDataLoad()
        {
            Info = GetRaceInfo();
            Info.RecordOfMounth = GetTestData(5);
            Info.RecordOfDay = GetTestData(10);
            Info.RaceResults = GetTestRaceResult(11, 35);

            PageSettings = GetPageSettings();
        }

        private PageSettings GetPageSettings()
        {
            var temp = new PageSettings();
            temp.Title = "CrazyKarting Rivera";
            temp.Address = "Одесса ТРЦ Ривьера";
            temp.Site = "www.crazykarting.com.ua";
            temp.Email = "crazykarting@gmail.com";
            temp.Phone = "3 8 048 770-66-69";


            temp.Logo = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\Images\\Logo\\CrazyKarting-150.png"));
            temp.QrCode = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\Images\\QrCodes\\code1.png"));
            temp.LeftSponsor = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\Images\\Sponsors\\l.jpg"));
            temp.CenterSponsor = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\Images\\Sponsors\\c.jpg"));
            temp.RightSponsor = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\Images\\Sponsors\\r.png"));

            return temp;
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


        // Testing
        /////////////////////////////////////////////////////////////////////////////////////////
        //
        //

        Random rand = new Random();
        private string _testPilotName = "Каролев-Сухинен Каролев-Сухинен";

        private List<BestPilots> GetTestData(int count)
        {

            List<BestPilots> bp = new List<BestPilots>();
            bp.Add(new BestPilots() { DateOfRecord = "Дата", PilotName = "Имя", RecordTime = "Время" });
            for (int i = 0; i < count; i++)
            {
                bp.Add(new BestPilots()
                {
                    PilotName = _testPilotName,
                    DateOfRecord = DateTime.Now.ToString("dd MMMM yyyy"),
                    RecordTime = $"{ rand.Next(4, 95)},00",
                });
            }
            return bp;
        }

        private List<RaceResult> GetTestRaceResult(int count, int racesCount)
        {

            List<RaceResult> temp = new List<RaceResult>();
            temp.Add(new RaceResult()
            {
                Id = "№",
                UserName = "Имя пилота",
                Kart = "Карт",
                BestCheckIn = "Лучшее",
                TimeOfLag = "Отставание",
                AmountTime = "Общее",
                RaceTimes = GetRaceTime(racesCount, true)
            });
            for (int i = 1; i <= count; i++)
            {
                temp.Add(new RaceResult()
                {
                    Id = i.ToString(),
                    UserName = _testPilotName,
                    Kart = rand.Next(3, 15).ToString(),
                    BestCheckIn = $"{ rand.Next(4, 95)},{rand.Next(100, 1000)}",
                    TimeOfLag = $"{ rand.Next(4, 95)},{rand.Next(100, 1000)}",
                    AmountTime = $"{rand.Next(1, 360)},{rand.Next(100, 1000)}",
                    RaceTimes = GetRaceTime(racesCount),
                });
            }
            return temp;
        }

        private List<RaceTime> GetRaceTime(int count, bool isFirst = false)
        {
            List<RaceTime> temp = new List<RaceTime>();

            if (isFirst)
            {
                for (int i = 1; i <= count; i++)
                    temp.Add(new RaceTime()
                    {
                        Range = i.ToString(),
                        Time = i.ToString(),
                    });
                return temp;
            }


            for (int i = 1; i <= count; i++)
            {
                temp.Add(new RaceTime()
                {
                    Range = i.ToString(),
                    Time = $"{rand.Next(13, 99)},{rand.Next(100, 1000)}",
                });
            }

            return temp;
        }

        private RaceInfo GetRaceInfo()
        {
            RaceInfo temp = new RaceInfo();
            temp.DateNow = DateTime.Now.ToString("HH MMMM yyyy");
            temp.Time = "1:45";
            temp.Trek = "CrazyKarting Riviera July";
            temp.RaceNumber = "48";
            temp.RaceOfRecordUser = $"[shalashenko] {_testPilotName}";
            temp.RaceOfRecordDate = "17 ноября 2016";
            temp.RaceOfRecordTime = "1,03";
            return temp;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
