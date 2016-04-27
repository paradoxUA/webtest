using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Prokard_Timing
{
    class Logger
    {

        public enum LogType
        {
            info,
            error,
            warning
        }
        public static void AddRecord(string message, LogType type, TimeSpan? timeSpan)
        {
            return;

            DateTime currDate = DateTime.Now;
            string fileName = currDate.Year.ToString() + currDate.Month.ToString("00") + currDate.Day.ToString("00");
            string fileSuffix = "";
            switch (type)
            {
                case LogType.error: fileSuffix = "_errors"; break;
                case LogType.info: fileSuffix = "_info"; break;
                case LogType.warning: fileSuffix = "_warning"; break;
            }

            string record = DateTime.Now.ToLongTimeString() + "\t" + message;
            if (timeSpan.HasValue)
            {
                record += "\t" + timeSpan.Value.ToString();
            }

            record += "\r\n";

            File.AppendAllText(Program.ProgramFolder + "//" + fileName + fileSuffix + ".txt", record);
        }


        /*
           DateTime startTime = DateTime.Now;
         
         TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ShowFinnalyRaceResult", Logger.LogType.info, executionTime);
         
         */
    }
}
