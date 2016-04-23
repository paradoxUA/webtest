using System;
using System.Collections.Generic;
using Prokard_Timing.model;
namespace Prokard_Timing
{
    public class RaceClass
    {
        
        public int Status = 0; // 0 = ; 1 - рейс отменён; 2 = рейс завершён
        public int TrackID = 0; // ID трека
        public string TrackName = String.Empty; // Название трека
        public double RaceSum = 0; // Стоимость участия
        public int RaceNum = 0;   // Номер рейса (Изменчив) sngavrilenko: да уж, сразу понятно стало... 
        public int RaceID = 0;    // ID в таблице Races
        public string ID = String.Empty; // ID ячейки 
        public string Minute =String.Empty;
        public string Hour = String.Empty;
        public DateTime Date;  // Дата проведения рейса
        public string Created= String.Empty; // Дата создания рейса
        public int RowPos = 0; // Позиция в глобальной матрице
        public int ColPos = 0;
        public List<string> Karts = new List<string>(); // Список картов

        public int Light_mode = 0; // Режим "Без привязки к карту"
        

    }
   

}
