using System.Collections.Generic;

namespace DocumentPrinter.Models
{
    public class RaceInfo
    {
        public string DateNow { get; set; }
        public string Time { get; set; }
        public string Trek { get; set; }
        public string RaceNumber { get; set; }
        public string RaceOfRecordUser { get; set; }// Абсолютный рекорд трассы {Ник / Имя фамилия / Дата / Время}
        public string RaceOfRecordDate { get; set; }
        public string RaceOfRecordTime { get; set; }

        public List<RaceResult> RaceResults { get; set; }
        public List<BestPilots> RecordOfMounth { get; set; }
        public List<BestPilots> RecordOfDay { get; set; }
        public RaceInfo()
        {
            RaceResults = new List<RaceResult>();
            RecordOfMounth = new List<BestPilots>();
            RecordOfDay = new List<BestPilots>();
        }
    }
}
