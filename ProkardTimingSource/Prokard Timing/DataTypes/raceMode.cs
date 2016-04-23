using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prokard_Timing.DataTypes
{
    // sgavrilenko: не используется, чтобы не ломать общую "концепцию" проекта ;-)
    /// <summary>
    /// режим заезда (длительность в минутах)
    /// </summary>
    public class raceMode
    {
        public int id;
        public string name;
        public Int16 length; // время в минутах
    }
}
