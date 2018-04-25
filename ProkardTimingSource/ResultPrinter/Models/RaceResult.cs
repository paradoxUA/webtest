using DocumentPrinter.Models;
using System.Collections.Generic;

namespace DocumentPrinter.Models
{
    public class RaceResult
    {
        public string PilotRange { get; set; }
        public string UserName { get; set; }
        public string Kart { get; set; }
        public string BestCheckIn { get; set; }
        public string TimeOfLag { get; set; }
        public string AmountTime { get; set; }
        public List<RaceTime> RaceTimes { get; set; }
    }
}
