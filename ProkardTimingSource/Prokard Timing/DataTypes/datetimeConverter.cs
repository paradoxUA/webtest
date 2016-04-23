using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prokard_Timing
{
    public static class datetimeConverter
    {
        // раньше в проекте использовалась mysql. а там с датами всё не так, как у mssql.
        // кроме того, в методы передавались на datetime данные, а string. И вообще ничего не работало после перехода на mssql.
        // так как в mysql есть тип data, а в mssql только datetime.


        // just returns time as 0:0:0
        public static DateTime toStartDateTime(DateTime someDate)
        {
            DateTime result = new DateTime(someDate.Year, someDate.Month, someDate.Day);
            return result;
        }

        // returns date for 23.59.59
        public static DateTime toEndDateTime(DateTime someDate)
        {
            DateTime result = new DateTime(someDate.Year, someDate.Month, someDate.Day).AddDays(1).AddMilliseconds(-1);
            return result;
        }


        public static string toDateTimeString(DateTime someDate)
        {
            return String.Format("{0:s}", someDate);
        }

        public static string toDateString(DateTime someDate)
        {
            return someDate.ToLongDateString();
        }

    }
}
