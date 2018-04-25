using Dal.Entities;
using Dal.Interfaces;
using Dal.Providers;
using DocumentPrinter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace DocumentPrinter.Services
{
    public class PageService
    {
        private ILiteDbProvider _db;
        public RaceInfo Info { get; set; }
        public PageSettings PageSettings { get; set; }

        public BitmapImage Logo { get; set; }
        public BitmapImage LeftSponsor { get; set; }
        public BitmapImage CenterSponsor { get; set; }
        public BitmapImage RightSponsor { get; set; }
        public BitmapImage QrCode { get; set; }

        public PageService()
        {
            _db = LiteDbProvider.Create();

            DirectoryGuard();

            TestDataLoad();

            Logo = PageSettings.GetBitmapImage(PageSettings.Logo);
            LeftSponsor = PageSettings.GetBitmapImage(PageSettings.LeftSponsor);
            CenterSponsor = PageSettings.GetBitmapImage(PageSettings.CenterSponsor);
            RightSponsor = PageSettings.GetBitmapImage(PageSettings.RightSponsor);
            QrCode = PageSettings.GetBitmapImage(PageSettings.QrCode);

        }

        private void TestDataLoad()
        {
            Info = GetRaceInfo();
            Info.RecordOfMounth = GetTestData(5);
            Info.RecordOfDay = GetTestData(10);
            Info.RaceResults = GetTestRaceResult(11, 35);

            PageSettings = _db.GetPageSettings();
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
        private string _testPilotName = "Сухинен Каролев";

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
                PilotRange = "№",
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
                    PilotRange = i.ToString(),
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
