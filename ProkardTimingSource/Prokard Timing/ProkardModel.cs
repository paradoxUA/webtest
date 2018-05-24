using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using DateTimeExtensions;
//using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using System.Linq;
using System.Data.Entity;
using Rentix.model;
using System.IO;
using System.Data.SQLite;
using DocumentPrinter.Models;
using ECM7.Migrator;
using ECM7.Migrator.Providers.SqlServer;
using Rentix.Migrations;
using Rentix.Extensions;

namespace Rentix
{
    public class ProkardModel
    {
        private crazykartContainer edb;
        public static string connectionString = "";

        public ProkardModel()
        {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //connectionString = config.AppSettings.Settings["crazykartConnectionString"].Value;
            connectionString = ConfigurationManager.ConnectionStrings["crazykartConnectionString"].ConnectionString;
            try
            {
                edb = new crazykartContainer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }






        /* Object Types
             *          -1 события от неизвестно кого ???
             *          0 - Kart
             *          1 - Pilot
             *          2 - Event
         *          */






        private SqlConnection db, db2, db3;


        public static bool connected;
        public string LastError;

        internal static string getDate(bool pastdate = false)
        {
            if (pastdate == false)
            {
                return datetimeConverter.toDateTimeString(DateTime.Now.AddDays(-1)); //.ToString("yyyy-MM-dd HH:mm:ss"
            }
            else
            {
                return datetimeConverter.toDateTimeString(DateTime.Now); //  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

		

		// Соединяемся с базой 
		public bool Connect()
        {

            //  string connection = "Server=" + Server + ";Port=" + Port + ";Uid=" + Uid + ";Password=" + Password + ";Database=" + Database + ";pooling=true;CharSet=cp1251";

            bool ret = true;
            try
            {
                db = new SqlConnection(connectionString);
                db2 = new SqlConnection(connectionString);
                db3 = new SqlConnection(connectionString);

                db.Open();
                db2.Open();
                db3.Open();
            }
            catch (Exception e)
            {
                LastError = e.Message;
                ret = false;
                MessageBox.Show(e.Message);
            }
            connected = ret;

            if (connected)
            {
				var provider = new SqlServerTransformationProvider(db, 9999);
				var migrator = new Migrator(provider, typeof(MigrationForAssembly).Assembly);
				migrator.Migrate();

				/*
                using (SqlCommand cmd = new SqlCommand("set names cp1251", db)) cmd.ExecuteNonQueryHandled();
                using (SqlCommand cmd = new SqlCommand("set names cp1251", db2)) cmd.ExecuteNonQueryHandled();
                using (SqlCommand cmd = new SqlCommand("set names cp1251", db3)) cmd.ExecuteNonQueryHandled();
            
                 */
			}


            return ret;
        }

        // Разрывает соединение и освобождает ресурсы
        public bool Close()
        {

            bool ret = true;
            try
            {
                if (connected)
                {
                    db.Close();
                    db.Dispose();
                    db2.Close();
                    db2.Dispose();
                    db3.Close();
                    db3.Dispose();

                    connected = false;
                }
            }
            catch (Exception e)
            {
                LastError = e.Message;
                ret = false;
            }

            return ret;
        }

		



		// узнать, разрешён ли запуск заезда
		/* разрешён, если есть пилоты в заезде и гонка не окончена (на паузе или не запускалась)
         * 
         */
		public bool isRaceCanBeStarted(int idRace)
        {
            bool result = true;

            races someRace = edb.races.Find(idRace);
            if (someRace == null)
            {
                return result;
            }

            // 1 - рейс отменён; 2 = рейс завершён
            if (someRace.stat == 2)
            {
                return false;
            }

            // рейс не отменялся и не завершался, поэтому нужно проверить, есть ли в нём пилоты. Если есть, то не запрещаем продолжение рейса
            if (someRace.race_data.Count > 0)
            {
                return true;
            }

            return false;

        }

        public double getPaidAmount(int idRaceData)
        {
            race_data rd = edb.race_data.Find(idRaceData);
            if (rd != null)
            {
                return Convert.ToDouble(rd.paid_amount);
            }

            return 0;
        }

        // получить лучшие результаты по треку 
       
        /*
        // вернуть список режимов гонки
        public IEnumerable<race_modes> GetRaceModes()
        {
            return edb.race_modes;
        }
         */

        // Получает рейс за выбранную дату с данным идентификатором 
        public void GetRace(DateTime date, RaceClass Race)
        {
            DateTime startTime = DateTime.Now;
            // MessageBox.Show("GetRace 1");

            int hours = Convert.ToInt16(Race.ID.Substring(0,
                Race.ID.IndexOf(",")));


            // муть...
            string minutesCellIndex = Race.ID.Substring(Race.ID.IndexOf(",") + 1);
            int minutes = Convert.ToInt16(minutesCellIndex);
            minutes = AdminControl.GetStMinutesFromIndex(minutes) == 60 ? 59 : AdminControl.GetStMinutesFromIndex(minutes);

            date = date.AddHours(hours).AddMinutes(minutes);


            //  string raceDateTime = date + " " + hours.ToString("00") + ":" + minutes.ToString("00") + ":00";


            if (connected)
            {
                //  MessageBox.Show("GetRace 2");

                // TODO обрезать время

                string command = "select * from races where racedate='" + datetimeConverter.toDateTimeString(date) + "' and raceid='" + Race.ID + "'";
                // MessageBox.Show(command);

                SqlCommand cmd = new SqlCommand(command, db);

                // MessageBox.Show("GetRace before execute reader");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //  MessageBox.Show("GetRace readed has executed");

                    if (reader.HasRows)
                    {
                        //  MessageBox.Show("GetRace has rows");

                        reader.Read();

                        //   MessageBox.Show("GetRace read");

                        Race.Status = Convert.ToInt32(reader["stat"]);
                        Race.RaceID = Convert.ToInt32(reader["id"]);
                        Race.Created = reader["racedate"].ToString();
                        Race.TrackID = Convert.ToInt32(reader["track_id"]);
                        Race.TrackName = GetTrackName((int)reader["track_id"]);
                        Race.Light_mode = Convert.ToInt32(reader["light_mode"]);
                    }
                    else
                    {
                        //   MessageBox.Show("GetRace can not read");
                        Race.Status = 0;
                        Race.RaceID = -1;
                        Race.Created = "";
                        Race.TrackID = 0;
                        Race.TrackName = "Default";
                        Race.Light_mode = 0;
                    }
                }

                cmd.Dispose();
            }
            else
            {
                //  MessageBox.Show("GetRace 3 not connected");
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRace(sql command)", Logger.LogType.info, executionTime);
        }

        // Получает рейс по Id
        public RaceClass GetRace(DateTime date, int id)
        {
            RaceClass Race = new RaceClass();

            if (connected)
            {
                //  MessageBox.Show("GetRace 2");

                // TODO обрезать время

                //string command = "select * from races where id='" + id + "'";
                string command = "select * from races where racedate='" + datetimeConverter.toDateTimeString(date) + "' and raceid='" + Race.ID + "'";
                // MessageBox.Show(command);

                SqlCommand cmd = new SqlCommand(command, db);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        //   MessageBox.Show("GetRace read");

                        Race.Status = Convert.ToInt32(reader["stat"]);
                        Race.RaceID = Convert.ToInt32(reader["id"]);
                        Race.Created = reader["racedate"].ToString();
                        Race.TrackID = Convert.ToInt32(reader["track_id"]);
                        Race.TrackName = GetTrackName(Race.TrackID);
                        Race.Light_mode = Convert.ToInt32(reader["light_mode"]);
                    }
                    else
                    {
                        //   MessageBox.Show("GetRace can not read");
                        Race.Status = 0;
                        Race.RaceID = -1;
                        Race.Created = "";
                        Race.TrackID = 0;
                        Race.TrackName = "Default";
                        Race.Light_mode = 0;
                    }
                }

                cmd.Dispose();
            }
            else
            {
                //  MessageBox.Show("GetRace 3 not connected");
            }
            return Race;
        }
        // Получает абсолютный рекорд трассы
        public string GetRecord(int TrackID)
        {
            DateTime startTime = DateTime.Now;

            string ret = String.Empty;
            List<Hashtable> br = GetBestResults(TrackID,
                 DateTime.Now.AddYears(-100),
                DateTime.Now.AddYears(100), 1);


            if (br.Count > 0)
            {
                double min = Double.MaxValue;
                int index = 0;
                for (int i = 0; i < br.Count; i++)
                {
                    if (min > Convert.ToDouble(br[i]["seconds"]))
                    {
                        min = Convert.ToDouble(br[i]["seconds"]);
                        index = i;
                    }

                }

                if (br[index]["nickname"].ToString().Length > 0)
                {
                    ret = "[" + br[index]["nickname"] + "] ";
                }

                ret = ret + br[index]["name"].ToString() + " " + br[index]["surname"].ToString() + "  " + Convert.ToDateTime(br[index]["racedate"]).ToString("dd MMMM yyyy") + ":  " + br[index]["seconds"].ToString() + "  сек";
            }
            else
            {
                ret = "-";
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRecord", Logger.LogType.info, executionTime);

            return ret;
        }


        // Получает абсолютный рекорд трассы
        public AbsoluteRecordOfRace GetAbsoluteRecordOfRace(int TrackID)
        {
            DateTime startTime = DateTime.Now;
            AbsoluteRecordOfRace aror = new AbsoluteRecordOfRace();

            string ret = String.Empty;
            List<Hashtable> br = GetBestResults(TrackID,
                DateTime.Now.AddYears(-100),
                DateTime.Now.AddYears(100), 1);


            if (br.Count > 0)
            {
                double min = Double.MaxValue;
                int index = 0;
                for (int i = 0; i < br.Count; i++)
                {
                    if (min > Convert.ToDouble(br[i]["seconds"]))
                    {
                        min = Convert.ToDouble(br[i]["seconds"]);
                        index = i;
                    }

                }

                if (br[index]["nickname"].ToString().Length > 0)
                {
                    ret = "[" + br[index]["nickname"] + "] ";
                }

                aror.Date = Convert.ToDateTime(br[index]["racedate"]);
                aror.RecordTime = br[index]["seconds"].ToString();
                aror.Pilot = ret + br[index]["name"].ToString() + " " + br[index]["surname"].ToString();
            }
            else
            {
                ret = "";
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRecord", Logger.LogType.info, executionTime);

            return aror;
        }
        // Изменяет режим без картов
        public void ChangeRaceLightMode(string LM, string RaceID)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update races set light_mode='" + LM + "' where id='" + RaceID + "'", db))
                    cmd.ExecuteNonQueryHandled();
            }
        }

        // Получает карт по транспондеру
        public Hashtable GetKartFromTransponder(string Transponder)
        {
            DateTime startTime = DateTime.Now;

            Hashtable ret = new Hashtable();
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from karts where transponder='" + Transponder + "'", db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            ret = ConvertResult(res);
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetKartFromTransponder(sql)", Logger.LogType.info, executionTime);

            return ret;

        }

        // Добавляет поле в список в режиме Свободный заезд
        public void AddKartNoRace(string Transponder, string RaceID)
        {

            DateTime startTime = DateTime.Now;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into noracekart (transponder, race_id, created) values ('" + Transponder + "','" + RaceID + "', GETDATE())", db))
                    cmd.ExecuteNonQueryHandled();
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("AddKartNoRace(sql)", Logger.LogType.info, executionTime);

        }

        // Изменяет режим для участника
        public void ChangeMemberLightMode(string LM, string MID)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update race_data set light_mode='" + LM + "' where id='" + MID + "'", db))
                    cmd.ExecuteNonQueryHandled();
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ChangeMemberLightMode(sql)", Logger.LogType.info, executionTime);

        }


        // в date должно быть время  начала заезда. 2012-03-14 02:15:00.000
        private DateTime getRaceDateTime(DateTime date, int hour, int stage)
        {
            int minutes = GetMinutesFromIndex(stage);

            if (minutes == 60)
            {
                minutes = 59;
            }

            DateTime result = new DateTime(date.Year, date.Month,
               date.Day, hour, minutes, 0);

            return result;
        }

        // вернуть режим заезда
        public race_modes GetRaceModeById(int idRaceMode)
        {
            DateTime startTime = DateTime.Now;

            race_modes someRaceMode = edb.race_modes.Find(idRaceMode);

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRaceModeById", Logger.LogType.info, executionTime);

            return someRaceMode;
        }


        // Получает минуты по этапу
        public static int GetMinutesFromIndex(int i)
        {
            int Minutes = 0;
            for (int j = 1; j < i; j++)
            {
                Minutes += 15;
            }
            if (Minutes >= 60)
            {
                Minutes = 59;
            }
            return Minutes;
        }


        // Создает рейс
        public int CreateRace(DateTime date, string ID, string TrackID, int stage)
        {
            DateTime startTime = DateTime.Now;

            date = getRaceDateTime(date, date.Hour, stage);

            int idRace = -1;

            if (connected)
            {
                // если такой рейс есть, вернуть его id                
                SqlCommand cmd = new SqlCommand(
                    "select TOP 1 id from races where racedate ='" + String.Format("{0:s}", date) +
                    "' AND raceid = '" + ID + "'", db);


                if (cmd.ExecuteScalar() != null)
                {
                    idRace = Convert.ToInt32(cmd.ExecuteScalar());
                    return idRace;
                }



                // иначе - добавить и вернуть id
                using (SqlCommand cmd2 = new SqlCommand("insert into races (racedate,raceid,created,stat,track_id, light_mode, modified) values ('"
                     + datetimeConverter.toDateTimeString(date) + "','" + ID + "', GETDATE(),'1','" +
                    TrackID + "', 0, GETDATE()); select scope_identity()", db))
                {
                    idRace = Convert.ToInt32(cmd2.ExecuteScalar());
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("CreateRace(sql)", Logger.LogType.info, executionTime);

            return idRace;
        }

        // Добавляет пилота в рейс
        public bool AddPilotToRace(int pilot_id, int race_id, bool
            lightMode, double paidAmount, int idDiscountCard, int idRaceMode, int month = 0, int reserv = 0,
            int car_id = 0)
        {
            DateTime startTime = DateTime.Now;

            string idCar = "null";

            if (car_id > 0)
            {
                idCar = Convert.ToString(car_id);
            }

            string cardIdAsString = "null";

            if (idDiscountCard > -1)
            {
                cardIdAsString = idDiscountCard.ToString();
            }

            bool ret = true;
            if (race_id > 0)
            {
                if (connected)
                {
                    string query = String.Empty;
                    if (month == 1)
                    {
                        query = "insert into race_data (race_id,pilot_id,car_id,created,reserv,monthrace,race_month_date, light_mode, paid_amount, id_discount_card, id_race_mode) values ('" +
                            race_id.ToString() + "','" + pilot_id.ToString() + "'," +
                            idCar + ",GETDATE(),'" +
                            reserv.ToString() + "','" + month.ToString() + "','" +
                            datetimeConverter.toDateTimeString(DateTime.Now.Last(DayOfWeek.Sunday)) +
                            "', " + Convert.ToString(Convert.ToInt16(lightMode)) + ", " + paidAmount.ToString() + ", " + cardIdAsString + ", " + idRaceMode.ToString() + ")";
                    }
                    else
                    {
                        query = "insert into race_data (race_id,pilot_id,car_id,created,reserv,monthrace, light_mode, modified, paid_amount, id_discount_card, id_race_mode) values ('" +
                            race_id.ToString() + "','" + pilot_id.ToString() + "'," +
                            idCar + ",GETDATE(),'" +
                            reserv.ToString() + "','" + month.ToString() + "', " + Convert.ToString(Convert.ToInt16(lightMode)) + ", GETDATE()," +
                            paidAmount.ToString() + ", " + cardIdAsString + ", " + idRaceMode + ")";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, db))
                    {
                        cmd.ExecuteNonQueryHandled();
                    }

                }
                else
                {
                    ret = false;
                }
            }
            else
            {
                ret = false;
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("AddPilotToRace(sql)", Logger.LogType.info, executionTime);

            return ret;
        }

        // Создает нового пилота
        public users AddNewPilot(Hashtable data)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into users (name,surname,nickname,gender,birthday,created,modified,email,tel,gr,barcode, banned, deleted) values ('" +
                    data["name"] + "','" + data["surname"] + "','" + data["nickname"] + "','" +
                    data["gender"] + "','" + datetimeConverter.toDateTimeString(Convert.ToDateTime(data["birthday"])) + "',GETDATE(), GETDATE(),'" +
                    data["email"] + "','" + data["tel"] + "','" + data["group"] + "','" +
                    data["barcode"] + "', " + Convert.ToInt16(data["banned"]) + ", 0)", db))
                    cmd.ExecuteNonQueryHandled();

                string name = data["name"].ToString();
                string surname = data["surname"].ToString();
                string nick = data["nickname"].ToString();

                users result = edb.users.Where(m => m.name == name).Where(m =>
                    m.surname ==
                    surname).Where(m =>
                        m.nickname == nick).Take(1).SingleOrDefault();

                TimeSpan executionTime = DateTime.Now - startTime;
                Logger.AddRecord("AddNewPilot(sql)", Logger.LogType.info, executionTime);

                return result;
            }

            return null;
        }

		internal void DeleteCassa(DateTime from, DateTime to)
		{
			if (!connected)
			{
				return;
			}
			var query = $"DELETE FROM cassa WHERE cassa.date BETWEEN @start and @end";
			using (var cmd = new SqlCommand(query, db))
			{
				cmd.Parameters.Add(new SqlParameter("@start", SqlDbType.DateTime) { Value = from.AsDayStart()});
				cmd.Parameters.Add(new SqlParameter("@end", SqlDbType.DateTime) { Value = to.AsDayEnd() });
				cmd.ExecuteNonQueryHandled();
			}
		}

		// Изменяет данные пилота
		public void ChangePilot(Hashtable Data, int PilotID)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update users set name='" + Data["name"] + "', surname='" + Data["surname"] + "',gender='" + Data["gender"] + "',birthday='" + datetimeConverter.toDateTimeString(Convert.ToDateTime(Data["birthday"])) + "',modified='" + getDate() + "',nickname='" + Data["nickname"] + "',email='" + Data["email"] + "',tel='" + Data["tel"] + "',gr='" + Data["group"] + "',barcode='" + Data["barcode"] + "', banned = " + Convert.ToInt16(Data["banned"]) + " where id='" + PilotID + "'", db))
                    cmd.ExecuteNonQueryHandled();
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ChangePilot(sql)", Logger.LogType.info, executionTime);

        }

        // Получает ассоциативный массив всех пилотов и заполняются параметры пэйджлистера
        public Hashtable GetAllPilots(PageLister page, int idGroup,
            string filter, bool withPhonesOnly)
        {
            DateTime startTime = DateTime.Now;

            if (page != null)
            {
                page.OnUpdate = true;
            }

            Hashtable ret = new Hashtable();
            if (connected)
            {
                if (page != null)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("GetPilotsByGroup", db);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = page.CurrentPageNumber;
                        cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = page.PageSize;
                        cmd.Parameters.Add("@filter", SqlDbType.NVarChar).Value = filter;
                        cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = idGroup;

                        using (SqlDataReader res = cmd.ExecuteReader())
                        {
                            Hashtable row = new Hashtable();
                            int rows = 0;
                            while (res.Read())
                            {
                                row = ConvertResult(res);

                                if (withPhonesOnly == false || (withPhonesOnly && row["tel"] != null))
                                {
                                    ret.Add(rows, row);
                                    rows++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    page.RowsMax = GetPilotsCount(idGroup, filter, withPhonesOnly);
                    page.PagesCount = Convert.ToInt32(Math.Ceiling((double)page.RowsMax / (double)page.PageSize));
                    page.FillPageNumbers();
                }


            }

            /*
            Hashtable ret = new Hashtable();
            if (connected)
            {
                if (page != null)
                {
                    using (SqlCommand cmd = new SqlCommand("select count(*)as c from users " + (filter != "" ? " where id>0 " + filter : ""), db))
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            int Rows = Convert.ToInt32(res["c"].ToString());

                            if (page.RowsMax != Rows || page.OnChange)
                            {
                                page.RowsMax = Rows;
                                page.Pages = Convert.ToInt32(Rows / page.OnPage);
                                page.ShowPages();
                                page.OnChange = false;
                            }
                            page.F();
                        }
                    }


                    filter += page.Filter;
                }
                // TODO using (SqlCommand cmd = new SqlCommand("select * from users " + (filter != "" ? " where id>0 " + filter : ""), db))
                
                using (SqlCommand cmd = new SqlCommand("select * from users ", db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();
                        int rows = 0;
                        while (res.Read())
                        {

                            row = ConvertResult(res);
                            ret.Add(rows, row);
                            rows++;
                        }
                    }
                }
            }
            */

            if (page != null)
            {
                page.OnUpdate = false;
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllPilots(sp)", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает массив  пилотов 
        public Hashtable GetAllPilots(string filter)
        {
            DateTime startTime = DateTime.Now;

            Hashtable ret = new Hashtable();
            if (connected)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetPilotsByGroup", db);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = 200;
                    cmd.Parameters.Add("@filter", SqlDbType.NVarChar).Value = filter;
                    cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = -1;

                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();
                        int rows = 0;
                        while (res.Read())
                        {

                            row = ConvertResult(res);
                            ret.Add(rows, row);
                            rows++;
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllPilots(filter + sp)", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает массив  пилотов 
        public Hashtable GetAllPilots(string filter, int idGroup, bool withPhonesOnly)
        {
            DateTime startTime = DateTime.Now;

            Hashtable ret = new Hashtable();
            if (connected)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetPilotsByGroup", db);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = 2000000000;
                    cmd.Parameters.Add("@filter", SqlDbType.NVarChar).Value = filter;
                    cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = idGroup;

                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();
                        int rows = 0;
                        while (res.Read())
                        {

                            row = ConvertResult(res);

                            if (withPhonesOnly == false || (withPhonesOnly && row["tel"].ToString().Length > 0))
                            {
                                ret.Add(rows, row);
                                rows++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllPilots(filter2, sp)", Logger.LogType.info, executionTime);

            return ret;
        }


        // Количество пилотов
        public int GetPilotsCount(int idGroup, string filter, bool withPhonesOnly)
        {
            DateTime startTime = DateTime.Now;

            int result = -1;

            if (connected)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetPilotsCount", db);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@filter", SqlDbType.NVarChar).Value = filter;
                    cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = idGroup;
                    cmd.Parameters.Add("@with_phones_only", SqlDbType.Bit).Value = withPhonesOnly;

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {

                }
            }

            /*
            Hashtable ret = new Hashtable();
            if (connected)
            {
                if (page != null)
                {
                    using (SqlCommand cmd = new SqlCommand("select count(*)as c from users " + (filter != "" ? " where id>0 " + filter : ""), db))
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            int Rows = Convert.ToInt32(res["c"].ToString());

                            if (page.RowsMax != Rows || page.OnChange)
                            {
                                page.RowsMax = Rows;
                                page.Pages = Convert.ToInt32(Rows / page.OnPage);
                                page.ShowPages();
                                page.OnChange = false;
                            }
                            page.F();
                        }
                    }


                    filter += page.Filter;
                }
                // TODO using (SqlCommand cmd = new SqlCommand("select * from users " + (filter != "" ? " where id>0 " + filter : ""), db))
                
                using (SqlCommand cmd = new SqlCommand("select * from users ", db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();
                        int rows = 0;
                        while (res.Read())
                        {

                            row = ConvertResult(res);
                            ret.Add(rows, row);
                            rows++;
                        }
                    }
                }
            }
            */

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetPilotsCount", Logger.LogType.info, executionTime);

            return result;
        }



        // Получает все карты
        public Hashtable GetAllKarts(int tp = 0)
        {

            DateTime startTime = DateTime.Now;

            Hashtable ret = new Hashtable();
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from karts" + (tp == 1 ? " where repair=0 and wait=0" : ""), db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();
                        int rows = 0;
                        while (res.Read())
                        {
                            row = ConvertResult(res);
                            ret.Add(rows, row);
                            rows++;
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllKarts(sql)", Logger.LogType.info, executionTime);

            return ret;
        }




        // Получает всю информацию о данном пилоте
        public Hashtable GetPilot(int id)
        {
            DateTime startTime = DateTime.Now;

            Hashtable row = new Hashtable();
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from users where id='" + id + "'", db2)) // (SELECT pilot_id FROM race_data WHERE id=
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            row = ConvertResult(res);
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetPilot(sql)", Logger.LogType.info, executionTime);

            return row;

        }

        // Получает детальную информацию про рейс
        public Hashtable GetDetalRaceInfo(int RaceID)
        {
            DateTime startTime = DateTime.Now;

            Hashtable ret = new Hashtable();
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select rd.id,rd.race_id,rd.light_mode,rd.pilot_id,u.name,u.surname,u.nickname,rd.car_id from race_data rd,users u where u.id=rd.pilot_id and race_id = '" + RaceID + "'", db3))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable temp, Kart;
                        string car_id;

                        int rows = 0;
                        while (res.Read())
                        {
                            temp = ConvertResult(res);
                            car_id = res["car_id"].ToString();

                            if (car_id.Length > 0 && car_id != "0" && res["light_mode"].ToString() != "1")
                            {
                                Kart = GetKart(Convert.ToInt32(car_id));
                                temp["number"] = Kart["number"];
                                temp["transponder"] = Kart["transponder"];
                            }
                            else
                            {
                                ChangeMemberLightMode("1", temp["id"].ToString());
                                temp["light"] = 1;
                            }

                            ret.Add(rows, temp);
                            rows++;
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetDetalRaceInfo(sql)", Logger.LogType.info, executionTime);

            return ret;
        }
        // получить полную информацию о заезде
        public List<LapResult> GetFinallyRaceInfo(int RaceID)
        {
            DateTime startTime = DateTime.Now;

            List<LapResult> Data = new List<LapResult>();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select rd.id, rd.race_id, k.number,rd.light_mode, rd.pilot_id, u.name, u.surname, u.nickname, rd.car_id from karts k, race_data rd,users u where k.id=rd.car_id and u.id=rd.pilot_id and race_id = '" + RaceID + "'", db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        int Lap, BestLap;
                        double TimeLap, BestTime, AverageTime;


                        while (res.Read())
                        {
                            LapResult Temp;
                            Temp.Times = new List<double>();

                            // Блок заполнения данных о пилоте
                            Temp.MemberID = Convert.ToInt32(res["id"]);

                            //  Temp.PilotID = Convert.ToInt32(res["id_pilot"]); 
                            Temp.PilotID = Convert.ToInt32(res["pilot_id"]);
                            Temp.RaceID = Convert.ToInt32(res["race_id"]);
                            if (res["light_mode"].ToString().ToLower() == "true")
                            {
                                Temp.Light = true;
                            }
                            else
                            {
                                Temp.Light = false;
                            }
                            // Temp.Light = res["light_mode"].ToString().ToLower == "0" ? false : true;
                            Temp.CarNum = res["number"].ToString();
                            Temp.PilotName = res["name"].ToString() + " " + res["surname"].ToString();
                            Temp.PilotNickName = res["nickname"].ToString();


                            Lap = BestLap = 0;
                            TimeLap = AverageTime = 0;

                            BestTime = Double.MaxValue;
                            // Блок заполнения данных о кругах
                            using (SqlCommand cmd2 = new SqlCommand("select lap,seconds from race_times where member_id='" + res["id"].ToString() + "' order by lap,seconds", db2))
                            using (SqlDataReader res2 = cmd2.ExecuteReader())
                                while (res2.Read())
                                {
                                    try
                                    {
                                        Lap = Convert.ToInt32(res2["lap"]);
                                        TimeLap = Convert.ToDouble(res2["seconds"]);

                                        if (BestTime > TimeLap)
                                        {
                                            BestLap = Lap;
                                            BestTime = TimeLap;
                                        }

                                        AverageTime += TimeLap;

                                        Temp.Times.Add(TimeLap);
                                    }
                                    catch (Exception ex)
                                    {   // Заглушка неотработанных ошибок
                                        string s = ex.Message;
                                    }
                                }
                            Temp.MaxLap = Lap;
                            Temp.BestTimeLap = BestLap;
                            Temp.BestTime = BestTime == Double.MaxValue ? 0 : BestTime;
                            Temp.AverageTime = AverageTime / (Temp.MaxLap == 0 ? 1 : Temp.MaxLap);
                            Data.Add(Temp);
                        }

                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetFinallyRaceInfo(sql)", Logger.LogType.info, executionTime);

            return Data;
        }


        // Получает список пилотов данного рейса
        public IEnumerable<race_data> GetRacePilots(int raceid)
        {
            DateTime startTime = DateTime.Now;
            IEnumerable<race_data> result = edb.race_data.Where(m => m.race_id == raceid);

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRacePilots(" + raceid + ")", Logger.LogType.info, executionTime);

            return result;
        }

        // Получает список пилотов данного рейса
        public Hashtable GetRacePilotsOld(int raceid)
        {
            DateTime startTime = DateTime.Now;

            //   IEnumerable<race_modes> raceModes = edb.race_modes;

            Hashtable ret = new Hashtable();
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from race_data where race_id='" + raceid + "'", db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();
                        int rows = 0;
                        while (res.Read())
                        {

                            row = GetPilot(Convert.ToInt32(res["pilot_id"]));
                            row["BaseID"] = res["id"];
                            row["car_id"] = res["car_id"];
                            row["reserv"] = res["reserv"];
                            row["light_mode"] = res["light_mode"];
                            row["id_race_mode"] = edb.race_modes.Find(Convert.ToInt16(res["id_race_mode"])).id;
                            row["race_mode"] = edb.race_modes.Find(Convert.ToInt16(res["id_race_mode"])).name;

                            //row["name"] = res["name"];
                            // row["surname"] = res["surname"];
                            ret.Add(rows, row);
                            rows++;
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRacePilots(" + raceid + ")", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает количество пилотов рейса
        public int GetRacePilotsCount(string raceid)
        {
            DateTime startTime = DateTime.Now;

            int ret = 0;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from race_data where race_id='" + raceid + "'", db3))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToInt32(res["c"]);
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRacePilotsCount(sql)", Logger.LogType.info, executionTime);

            return ret;


        }

        // Получает информацию о карте
        public Hashtable GetKart(int id)
        {
            DateTime startTime = DateTime.Now;

            Hashtable row = new Hashtable();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from karts where id='" + id + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            row = ConvertResult(res);
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetKart(sql)", Logger.LogType.info, executionTime);

            return row;

        }

        // Получает ID карта по его номеру
        public int GetKartID(string Num)
        {
            DateTime startTime = DateTime.Now;

            int ret = -1;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select id from karts where number='" + Num + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToInt32(res["id"]);
                    }
                }
            }
            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetKartID(sql)", Logger.LogType.info, executionTime);

            return ret;
        }

        // Обнулить карт у данного пилота
        public void DelKartFromRace(string BaseID, string Num = null)
        {
            DateTime startTime = DateTime.Now;
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update race_data set car_id=null where id='" + BaseID + "' ", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }


            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("DelKartFromRace(sql)", Logger.LogType.info, executionTime);
        }

        // Добавляет карт пилоту
        public void AddKartToRace(string BaseID, string Name, bool Light)
        {
            DateTime startTime = DateTime.Now;

            int id = Convert.ToInt32(BaseID);

            race_data someRaceData = edb.race_data.Where(markRaceModeAsDeleted => markRaceModeAsDeleted.id == id).Take(1).SingleOrDefault();
            someRaceData.light_mode = Light;
            someRaceData.car_id = edb.karts.Where(m => m.number == Name).Take(1).SingleOrDefault().id;
            edb.SaveChanges();

            /*

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update race_data set car_id='" + GetKartID(Name) + (Light ? "', light_mode=1" : "',light_mode=0") + " where id='" + BaseID + "'", db))
                    cmd.ExecuteNonQueryHandled();

            }
             */

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("AddKartToRace(sql)", Logger.LogType.info, executionTime);

        }

        // Получает все выбранные карты данного рейса
        public List<string> GetRaceKarts(int id)
        {
            DateTime startTime = DateTime.Now;

            List<string> ret = new List<string>();

            if (id < 0)
            {
                return ret;
            }
            //   MessageBox.Show("GetRaceKarts " + id); 

            string s = String.Empty;
            if (connected)
            {
                // MessageBox.Show("GetRaceKarts connected " + id);
                using (SqlCommand cmd = new SqlCommand("select car_id from race_data where race_id='" + id + "' and light_mode=0", db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();
                        while (res.Read())
                        {
                            s = res["car_id"].ToString();
                            row = GetKart(Convert.ToInt32(s.Length == 0 ? "0" : res["car_id"].ToString()));
                            if (row.Count > 0)
                                ret.Add(row["number"].ToString());
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRaceKarts(sql)", Logger.LogType.info, executionTime);

            return ret;
        }


        public bool isNickUnique(string nick)
        {
            if (edb.users.Where(m => m.nickname.ToLower() == nick.Trim().ToLower()).Count() > 0)
            {
                return false;
            }

            return true;
        }

        public bool isNameAndLastNameUnique(string name, string lastName)
        {
            if (edb.users.Where(m => m.name.ToLower() == name.Trim().ToLower()).Where(m => m.surname.ToLower() == lastName.ToLower().Trim()).Count() > 0)
            {
                return false;
            }

            return true;
        }



        // Есть ли такой пилот // уже не используется

        public bool GetNewPilot(string name, string surname, string nickname)
        {

            DateTime startTime = DateTime.Now;

            bool ret = false;
            string query = String.Empty;

            if (name.Length > 0) query = " and name='" + name + "' ";
            if (surname.Length > 0) query = " and surname='" + surname + "' ";
            if (nickname.Length > 0) query = " and nickname='" + nickname + "' ";

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from users where id > 0 " + query, db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();
                        if (res.Read())
                        {
                            ret = true;
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetNewPilot(sql)", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает лучшее вермя пилота
        public List<string> GetPilotBestResult(int PilotID)
        {
            DateTime startTime = DateTime.Now;

            List<string> ret = new List<string>();
            if (connected)
            {
                string query = "select TOP 1 rd.id, rd.pilot_id, rt.seconds, rd.race_id,r.racedate, u.name,u.nickname, u.surname,u.email, u.tel, r.track_id,r.created,t.name as track_name from users u, race_times rt, race_data rd, races r, tracks t where rt.seconds = (select min(rt.seconds) from race_times rt, race_data rd where rd.pilot_id=u.id and rd.light_mode = 0 and rt.seconds > 0 and rt.member_id=rd.id and rd.id = rt.member_id	and rd.pilot_id=" + PilotID + " and u.id  = rd.pilot_id	and r.id  = rd.race_id and t.id  = r.track_id) order by seconds ";

                // TODO string query = "select	rd.id, rd.pilot_id,	rt.seconds, rd.race_id,r.racedate, 	u.name,u.nickname, u.surname,u.email, u.tel, r.track_id,r.created,t.name as track_name	from users u, race_times rt, race_data rd, races r, tracks t where 	rt.seconds = replace((select min(replace(rt.seconds,',','.')*1.0) from 	race_times rt, 			race_data rd 	where	rd.pilot_id=u.id and rd.light_mode = 0	and not isnull(rt.seconds)  and rt.seconds > 0	and rt.member_id=rd.id),'.',',')	and rd.id = rt.member_id	and rd.pilot_id='" + PilotID + "' and u.id  = rd.pilot_id	and r.id  = rd.race_id	and t.id  = r.track_id group by u.id order by seconds limit 1";

                /*select tr.name, min(replace(t.seconds,',','.')*1.0) as seconds from tracks tr, race_times t, race_data d, races r where tr.id=r.track_id and r.id=d.race_id and d.light_mode=0 and d.pilot_id='" + PilotID + "' and replace(t.seconds,',','.')=(select min(replace(t.seconds,',','.')*1.0) as min from race_times where member_id=d.id)*/

                using (SqlCommand cmd = new SqlCommand(query, db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            ret.Add(res["seconds"].ToString() + " сек");
                            ret.Add(res["track_name"].ToString());
                        }
                        else
                        {
                            ret.Add("Нет данных");
                            ret.Add("Нет данных");
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetPilotBestResult(sql)", Logger.LogType.info, executionTime);

            return ret;
        }

        // Банит или активирует пользователя
        public void ActivatePilot(int PilotID, string Stat, DateTime Date, string Message)
        {

            int InsertID = -1;
            string Mess = String.Empty;
            if (Message.Length > 0)
                Mess = Message;
            else
            {
                if (Stat == "1") Mess = "Добавлен в бан"; else Mess = "Снят с бана";

            }
            AddMessage(PilotID, 1, 3, Date, Mess);
            InsertID = GetLastInsertID("Messages");


            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update users set message_id='" + InsertID.ToString() + "', banned='" + Stat + "', date_banned = '" + datetimeConverter.toDateTimeString(Date) + "' where id='" + PilotID + "'", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }

            }
        }

        // Преобразовывает результат в ассоциативный массив
        public Hashtable ConvertResult(SqlDataReader row)
        {
            Hashtable res = new Hashtable();
            for (int i = 0; i < row.FieldCount; i++)
            {
                res[row.GetName(i)] = row.GetValue(i).ToString();
            }
            return res;
        }

        // сохраним время круга
        public void AddTimeStamp(int RaceMember, int Lap, decimal Seconds)
        {
            DateTime startTime = DateTime.Now;

            race_times someLapTime = new race_times
            {
                member_id = RaceMember,
                lap = Lap,
                seconds = Seconds,
                created = DateTime.Now
            };
            edb.race_times.Add(someLapTime);
            edb.SaveChanges();

            /*


            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into race_times (member_id,lap,seconds, created) values ('" + RaceMember.ToString() + "','" + Lap.ToString() + "'," + Seconds.ToString().Replace(',', '.') + ", GETDATE())", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }

            }
             */

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("AddTimeStamp(sql)", Logger.LogType.info, executionTime);

        }

        // Изменяет статус резерв или нет
        public void ChangePilotReservStatus(string RaceDataId, int Status = 0)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update race_data set reserv='" + Status.ToString() + "' where id='" + RaceDataId + "'", db))
                    cmd.ExecuteNonQueryHandled();

            }
        }

        // Удаляет пилота с рейса
        public bool DelPilotFromRace(string MemberID)
        {
            bool result = false;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("Delete from race_data where id='" + MemberID + "'", db))
                {
                    try
                    {
                        cmd.ExecuteNonQueryHandled();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Нельзя удалить пилота из заезда, так как имеется информация о времени кругов");

                    }
                }

            }

            return result;
        }

        // Удаляет пилота
        public void DelPilot(string UserID)
        {
            model.users pilot = edb.users.Find(Convert.ToInt32(UserID));
            if (pilot != null)
            {
                pilot.deleted = true;
                pilot.date_deleted = DateTime.Now;
                edb.SaveChanges();
            }


            /*

            if (connected)
            {                
                using (SqlCommand cmd = new SqlCommand("Delete from users where id='" + UserID + "'", db))
                    cmd.ExecuteNonQueryHandled();
            }
            */
        }

        // Удаляет группу
        public void DelGroup(string GroupID)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("Delete from groups where id='" + GroupID + "'", db))
                    cmd.ExecuteNonQueryHandled();

            }
        }

        // Добавляет карт
        public void AddKart(string Name, string Number, string Transponder)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into karts (name,number,transponder,created, repair, wait) values ('" + Name + "','" + Number + "','" + Transponder + "','" + getDate() + "', 0, 0)", db))
                    cmd.ExecuteNonQueryHandled();

            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("AddKart(sql)", Logger.LogType.info, executionTime);
        }

        // Обновляет Карт
        public void ChangeKart(string ID, string Name, string Number, string Transponder)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update karts set name='" + Name + "', number = '" + Number + "', transponder = '" + Transponder + "' where id='" + ID + "'", db))
                    cmd.ExecuteNonQueryHandled();

            }
        }

        // Обновляет Группу
        public void ChangeGroup(string Name, string Sale, string Price, string GroupID)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update groups set name='" + Name + "', sale = '" + Sale + "', price = '" + Price + "' where id='" + GroupID + "'", db))
                    cmd.ExecuteNonQueryHandled();

            }
        }

        // Удаляет Карт
        public void DelKart(string ID)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("delete from karts where id='" + ID + "'", db))
                {
                    try
                    {
                        cmd.ExecuteNonQueryHandled();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Запрещено удаление карта, так как он участвовал в заездах");
                    }
                }

            }
        }

        // Ремонт картов
        public void KartRepair(string KartNumber, string Repair, DateTime Date, string Message)
        {

            int CarID = GetKartID(KartNumber);
            int InsertID = -1;
            string MMM = String.Empty;
            if (Message.Length == 0)
            {

                if (Repair == "1") MMM = "Поставлен на ремонт"; else MMM = "Снят с ремонта";
            }
            else MMM = Message;
            int M_tp = 1;
            if (Repair == "1") M_tp = 1; else M_tp = 4;
            AddMessage(CarID, 0, M_tp, Date, MMM);
            InsertID = GetLastInsertID("Messages");

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update karts set message_id='" + InsertID.ToString() + "', repair='" + Repair + "' where id='" + CarID.ToString() + "'", db))
                    cmd.ExecuteNonQueryHandled();

            }
        }

        // Получает статистику по карту
        public Hashtable GetKartStatistic(string KartID, double Deflen, DateTime fromDateTime, DateTime toDateTime)
        {
            Hashtable ret = new Hashtable();

            if (connected)
            {
				var query = $"select d.id, r.track_id from race_data d, races r where d.car_id='{KartID}' and r.id=d.race_id and r.racedate	BETWEEN '{fromDateTime.AsDayStart().ToSqlString()}' and '{toDateTime.AsDayEnd().ToSqlString()}'";
				using (SqlCommand cmd = new SqlCommand(query, db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        List<Hashtable> temp = new List<Hashtable>();
                        while (res.Read())
                            temp.Add(ConvertResult(res));

                        ret["races"] = temp.Count;

                        double length = 0;
                        double tlength;
                        double ttlength;
                        for (int i = 0; i < temp.Count; i++)
                        {
                            tlength = GetTrackLength(temp[i]["track_id"].ToString());
                            ttlength = tlength < 0 ? Deflen : tlength;

                            length += ttlength * GetMemberLapsFromRace(temp[i]["id"].ToString());
                        }

                        ret["length"] = length;
                    }
                }
            }


            return ret;
        }

        // Получает длину трека
        public double GetTrackLength(string TrackID)
        {
            double ret = -1;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select length from tracks where id='" + TrackID + "'", db3))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToDouble(res["length"]);
                    }
                }
            }

            return ret;
        }

        // Добавляет сообщение
        public void AddMessage(int idObject, int objectType, int MessageType, DateTime Date,
            string Message, string Subject = "")
        {

            DateTime startTime = DateTime.Now;

            /* Object Types
             *          -1 события от неизвестно кого ???
             *          0 - Kart
             *          1 - Pilot
             *          2 - Event
             *          
             * 
             * Message Types
             *          0 - Simple Message
             *          1 - Repair 
             *          2 - Wait
             *          3 - banned
             *          4 - UnRepair
             */


            string commandText = "";

            switch (objectType)
            {
                case 0:
                    commandText = "insert into messages (id_kart, m_type,message,date,created,subject, modified) values (" + idObject.ToString() + "," + MessageType.ToString() + ",'" + Message + "','" + datetimeConverter.toDateTimeString(Date)
                + "','" + getDate() + "','" + Subject + "', GETDATE())"; break;
                case 1:
                    commandText = "insert into messages (id_pilot, m_type,message,date,created,subject, modified) values (" + idObject.ToString() + "," + MessageType.ToString() + ",'" + Message + "','" + datetimeConverter.toDateTimeString(Date)
                + "','" + getDate() + "','" + Subject + "', GETDATE())"; break;
                case 2:
                    commandText = "insert into messages (id_program_user, m_type,message,date,created,subject, modified) values (" + idObject.ToString() + "," + MessageType.ToString() + ",'" + Message + "','" + datetimeConverter.toDateTimeString(Date)
                + "','" + getDate() + "','" + Subject + "', GETDATE())"; break;
                case -1:
                    commandText = "insert into messages (m_type,message,date,created,subject, modified) values (" + MessageType.ToString() + ",'" + Message + "','" + datetimeConverter.toDateTimeString(Date)
           + "','" + getDate() + "','" + Subject + "', GETDATE())"; break;

            }


            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand(commandText, db))
                {
                    try
                    {
                        cmd.ExecuteNonQueryHandled();
                    }
                    catch (Exception ex)
                    {
                        MainForm.log("Error 4770: Не удалось добавить запись в messages. Ошибка: " + ex.Message);
                        throw ex;
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("AddMessage(sql)", Logger.LogType.info, executionTime);

        }

		internal void UpdatePartner(int? id, string name, float commision)
		{
			if (!connected)
			{
				return;
			}
			var commissionText = commision.ToString().Replace(",", ".");
			var query = id.HasValue ?
				$"UPDATE partners SET name = '{name}', commission = {commissionText} WHERE id = {id.Value}" :
				$"INSERT INTO partners(name, commission) VALUES ('{name}', {commissionText})";
			using (var command = new SqlCommand(query, db))
			{
				command.ExecuteNonQueryHandled();
			}
		}

		internal void RemovePartner(int id)
		{
			if (!connected)
			{
				return;
			}
			var query = $"UPDATE partners SET deleted = 1 WHERE id = {id}";
			using (var command = new SqlCommand(query, db))
			{
				command.ExecuteNonQueryHandled();
			}
		}

		// Получает последний ID сообщения
		private int GetLastInsertID(string Table)
        {
            DateTime startTime = DateTime.Now;

            int ret = -1;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select TOP 1 id from " + Table + " order by id desc ", db3))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                        if (res.Read())
                        {
                            ret = Convert.ToInt32(res["id"]);
                        }
                }
            }

            if (ret == -1)
            {
                MainForm.log("Error 2893: не удалось получить id последней записи из таблицы " + Table);

            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetLastInsertID(sql)", Logger.LogType.info, executionTime);

            return ret;

        }

        // Получает текст сообщения по ID
        public Hashtable GetMessageFromID(string ID)
        {
            DateTime startTime = DateTime.Now;

            Hashtable ret = new Hashtable();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from messages where id='" + ID + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = ConvertResult(res);
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetMessageFromID(sql)", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает статистику по пользователям
        public Hashtable GetUsersStatistic()
        {
            DateTime startTime = DateTime.Now;

            Hashtable ret = new Hashtable();

            if (connected)
            {

                // Новых пилотов
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from users", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["users_all"] = res[0]; else ret["users_all"] = 0;

                // Забанненых пилотов всего
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from users where banned=1", db))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["users_bann"] = res[0]; else ret["users_bann"] = 0;


                // Удалённых пилотов всего
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from users where deleted = 1", db))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                    {
                        ret["users_deleted"] = res[0];
                    }
                    else
                    {
                        ret["users_bann"] = 0;
                    }


                // Сумма в кассе на сегодня
                ret["cash"] = GetCashFromUsers(DateTime.Now);
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetUsersStatistic(sql)", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает статистику за день
        public Hashtable GetDayStatistic(DateTime Day)
        {
            DateTime startTime = DateTime.Now;

            DateTime startDate = datetimeConverter.toStartDateTime(Day);
            DateTime endDate = datetimeConverter.toEndDateTime(Day);

            Hashtable ret = new Hashtable();

            if (connected)
            {
                // Заездов создано
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from races where created <= '" +
                    datetimeConverter.toDateTimeString(endDate) + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["races"] = res[0]; else ret["races"] = 0;

                // Заездов на этот день
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from races where racedate >='"
                    + datetimeConverter.toDateTimeString(startDate) + "' AND racedate <= '" +
                    datetimeConverter.toDateTimeString(endDate) + "'", db))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["races_day"] = res[0]; else ret["races_day"] = 0;

                // Заездов завершенных
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from races where created >='"
                    + datetimeConverter.toDateTimeString(startDate) + "' AND created <= '" +
                    datetimeConverter.toDateTimeString(endDate) + "' and stat=2", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                {
                    if (res.Read())
                    {
                        ret["races_end"] = res[0];
                    }
                    else
                    {
                        ret["races_end"] = 0;
                    }
                }

                // Заездов проваленных
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from races where created >='" +
                     datetimeConverter.toDateTimeString(startDate) + "' AND created <= '" +
                    datetimeConverter.toDateTimeString(endDate) + "' and stat=1", db))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["races_fail"] = res[0]; else ret["races_fail"] = 0;

                // Новых пилотов
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from users where created >='" +
                    datetimeConverter.toDateTimeString(startDate) + "' AND created <= '" +
                    datetimeConverter.toDateTimeString(endDate) + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["users_new"] = res[0]; else ret["users_new"] = 0;

                // Забанненых пилотов
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from users where date_banned >='"
                   + datetimeConverter.toDateTimeString(startDate) + "' AND date_banned <= '" +
                    datetimeConverter.toDateTimeString(endDate) + "'", db))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["users_bann"] = res[0]; else ret["users_bann"] = 0;


                // Удалённых пилотов
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from users where deleted = 1 AND date_deleted >='"
                   + datetimeConverter.toDateTimeString(startDate) + "' AND date_deleted <= '" +
                    datetimeConverter.toDateTimeString(endDate) + "'", db))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                    {
                        ret["users_deleted"] = res[0];
                    }
                    else
                    {
                        ret["users_deleted"] = 0;
                    }



                // Продано билетов
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from race_data where created >='"
                    + datetimeConverter.toDateTimeString(startDate) + "' AND created <= '" +
                    datetimeConverter.toDateTimeString(endDate) + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["tickets"] = res[0]; else ret["tickets"] = 0;

                /*
                // Сумма в кассе
                ret["cash"] = GetCashFromCassa(Day, false, false, true, false);
                 */

                // Сумма в кассе
                ret["cash"] = GetCashFromCassa(Day, true, false, true, false);

                // Сумма у пользователей
                ret["virtual"] = GetCashFromUsers(Day);
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetDayStatistic(sql)", Logger.LogType.info, executionTime);

            return ret;
        }

        #region сообщения по пилотам и картам
        // sgavrilenko: это всё я сделал, так как раньше было сделано хитро - карты имели id с 0, а пилоты с 240 (не помню точно). а в таблице сообщений в столбце id owner был или id пилота, или id карта или id админа. связей не было

        // Получает все сообщения по пилотам 
        public string GetAllPilotsMessages(int ID)
        {
            DateTime startTime = DateTime.Now;
            string ret = String.Empty;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select created, message from messages where id_pilot='" + ID + "' order by created desc", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret += res["created"].ToString() + "\r";
                            ret += res["message"].ToString() + "\n\r";
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllPilotsMessages(sql)", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает все сообщения по картам 
        public string GetAllKartsMessages(int ID, DateTime? from = null, DateTime? to = null)
        {
            DateTime startTime = DateTime.Now;
            string ret = String.Empty;
			var dateTimeRestriction = from.HasValue && to.HasValue;

			if (connected)
            {
				var query = $"select created, message from messages where id_kart='{ID}' {dateTimeRestriction.ToStringIf("and created BETWEEN @from and @to")} order by created desc";
				using (SqlCommand cmd = new SqlCommand(query, db2))
                {
					if (dateTimeRestriction)
					{
						cmd.Parameters.Add(new SqlParameter("@from", SqlDbType.DateTime) { Value = from.Value.AsDayStart() });
						cmd.Parameters.Add(new SqlParameter("@to", SqlDbType.DateTime) { Value = to.Value.AsDayEnd() });
					}
					using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret += res["created"].ToString() + "\r";
                            ret += res["message"].ToString() + "\n\r";
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllKartsMessages", Logger.LogType.info, executionTime);

            return ret;
        }

        #endregion

        // Cохраняет настройки
        public void SaveSettings(Hashtable sett)
        {
            m_dbConnection =
                new SQLiteConnection("Data Source=setts.sqlite;Version=3;");
            m_dbConnection.Open();

            if (m_dbConnection.State == ConnectionState.Open)
            {

                if (sett.Count > 0)
                    using (SQLiteCommand cmd =
                        new SQLiteCommand("delete from settings", m_dbConnection))
                    {
                        cmd.ExecuteNonQueryHandled();
                    }

                foreach (DictionaryEntry d in sett)
                {

                    using (SQLiteCommand cmd =
                        new SQLiteCommand(
                            "insert into settings (name,val) values ('" + d.Key.ToString() + "','" +
                            d.Value.ToString() + "')", m_dbConnection))
                    {
                        cmd.ExecuteNonQueryHandled();
                    }

                }
            }

            //if (connected)
            //{
            //    if (sett.Count > 0)
            //        using (SqlCommand cmd = new SqlCommand("delete from settings", db))
            //        {
            //            cmd.ExecuteNonQueryHandled();
            //        }

            //    foreach (DictionaryEntry d in sett)
            //    {

            //        using (SqlCommand cmd = new SqlCommand("insert into settings (name,val) values ('" + d.Key.ToString() + "','" + d.Value.ToString() + "')", db))
            //        {
            //            cmd.ExecuteNonQueryHandled();
            //        }

            //    }
            //}
        }


        public Hashtable DefaultAnonserSettings(Hashtable sett)
        {
            DateTime startTime = DateTime.Now;

            if (sett["anonser_nextRacePilotsTimer"] == null)
            {
                sett["anonser_nextRacePilotsTimer"] = 15; // Показывает пилотов следующего рейса
            }

            if (sett["anonser_raceLoadTimer"] == null)
            {
                sett["anonser_raceLoadTimer"] = 45; // Показывает загрузку рейса
            }
            if (sett["anonser_bestResultTimer"] == null)
            {
                sett["anonser_bestResultTimer"] = 30;  // Показывает лучший результат
            }
            if (sett["anonser_currentResultTimer"] == null)
            {
                sett["anonser_currentResultTimer"] = 5; // время по кругам
            }
            if (sett["anonser_imageRotateTimer"] == null)
            {
                sett["anonser_imageRotateTimer"] = 23;  // смена картинки, когда нет заезда         
            }
            if (sett["sponsors_images_rotate_timer"] == null)
            {
                sett["sponsors_images_rotate_timer"] = 24; // смена логотипов спонсоров
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("DefaultAnonserSettings", Logger.LogType.info, executionTime);

            return sett;
        }

        // Настройки по умолчанию
        private Hashtable DefaultSettings()
        {
            Hashtable sett = new Hashtable();

            // Закладка Общие настройки
            sett["time_start"] = 9;
            sett["time_end"] = 22;
            sett["time_wrap"] = "True"; // не показывать с начала
            sett["wrap_pos"] = 1; // насколько сдвигать
            sett["show_events"] = "True";
            sett["enter_password"] = "True";
            sett["sertificat_day"] = 30;
            sett["card_user"] = "False";
            sett["beep_system"] = false;
            sett["beep_windows"] = false;
            sett["uniquebestres"] = false;

            // Закладка оборудование
            sett["rs232_port"] = "COM1";
            sett["decoder"] = "AMB20";

            sett["printer_check"] = "";
            sett["printer_result"] = "";
            sett["print_check"] = "False";
            // Закладка Гонка
            sett["stop_on_pause"] = "True";
            sett["start_after_detection"] = "True";
            sett["print_result"] = "True";
            sett["show_zero_lap"] = "True";
            sett["noise_time"] = 10;
            sett["track_length"] = 1000;
            sett["fuel_on_lap"] = 0.5;
            sett["default_track"] = 1;
            sett["race_time"] = 10;
            sett["warm_subtract"] = "False";
            sett["warm_time"] = 5;
            sett["default_race_mode_id"] = 1;



            //Закладка временных скидок
            sett["racesale"] = false;
            sett["sale_onelap"] = 35;
            sett["sale_half"] = 55;

            // Анонсер
            sett["images_for_anonser"] = "";

            sett = DefaultAnonserSettings(sett);

            return sett;
        }

        //Удаляет данные заезда
        public string DelRaceDataTimes(string RaceID, int PilotID)
        {
            DateTime startTime = DateTime.Now;

            string ret = "Ошибка выполнения";
            string MemberID = "0";
            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select id from race_data where pilot_id='" + PilotID + "' and race_id='" + RaceID + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            MemberID = res["id"].ToString();
                    }
                }

                if (MemberID == "0")
                {
                    ret = "Запись не найдена";
                    return ret;
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from race_times where member_id='" + MemberID + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = "Удалено записей: " + res["c"].ToString();
                    }
                }

                using (SqlCommand cmd = new SqlCommand("delete from race_times where member_id='" + MemberID + "'", db2))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("DelRaceDataTimes", Logger.LogType.info, executionTime);

            return ret;

        }

        SQLiteConnection m_dbConnection;

        // Загружает настройки
        public Hashtable LoadSettings()
        {
            Hashtable ret = new Hashtable();
            m_dbConnection =
                new SQLiteConnection("Data Source=setts.sqlite;Version=3;");
            try
            {
                m_dbConnection.Open();
            }
            catch (Exception exp)
            {
                //   m_dbConnection.
                SQLiteConnection.CreateFile("setts.sqlite");
                SQLiteCommand cmd =
                    new SQLiteCommand("create table settings (name varchar(40), val varchar(40))",
                        m_dbConnection);
                ret = DefaultSettings();
                SaveSettings(ret);
            }

            if (m_dbConnection.State == ConnectionState.Open)
            {
                try
                {

                    // MessageBox.Show("load Settings");
                    using (SQLiteCommand cmd =
                        new SQLiteCommand("select name,val from settings", m_dbConnection))
                    {
                        using (SQLiteDataReader res = cmd.ExecuteReader())
                        {
                            while (res.Read())
                            {
                                // один раз как-то получилось, что в БД было по несколько значений с одним ключом. 

                                if (ret[res["name"]] == null)
                                {
                                    ret.Add(res["name"], res["val"]);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    SQLiteCommand cmd =
                        new SQLiteCommand("create table settings (name varchar(40), val varchar(40))",
                            m_dbConnection);
                    cmd.ExecuteNonQueryHandled();
                    ret = DefaultSettings();
                    SaveSettings(ret);
                }
                //using (SqlCommand cmd = new SqlCommand("select name,val from settings", db2))
                //{
                //    using (SqlDataReader res = cmd.ExecuteReader())
                //    {
                //        while (res.Read())
                //        {
                //            // один раз как-то получилось, что в БД было по несколько значений с одним ключом. 

                //            if (ret[res["name"]] == null)
                //            {
                //                ret.Add(res["name"], res["val"]);
                //            }
                //        }
                //    }
                //}
            }

            if (ret.Count == 0)
            {
                //  MessageBox.Show("DefaultSettings");

                ret = DefaultSettings();
                SaveSettings(ret);
            }

            ret = DefaultAnonserSettings(ret);

            //SaveSQLiteSettings(ret);
            // SaveSettings(ret);

            return ret;
        }


        // Получает максимальное количество картов
        public int GetMaxKarts()
        {
            int ret = 1;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) as c from karts where repair=0 and wait=0", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToInt32(res["c"]);
                    }
                }
            }

            return ret;

        }

        // Получает количество участий в рейсах TP - Билеты или рейсы
        public int GetPilotRaceCount(int UserID, bool TP)
        {
            DateTime startTime = DateTime.Now;

            int ret = 0;

            if (connected)
            {
                string query = String.Empty;

                if (TP)
                    query = "select count(*) as c from races r, race_data d where d.pilot_id='" + UserID + "' and r.id= d.race_id and r.stat=2";
                else
                    query = "select count(*) as c from race_data d where d.pilot_id='" + UserID + "'";
                using (SqlCommand cmd = new SqlCommand(query, db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToInt32(res["c"]);
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetPilotRaceCount", Logger.LogType.info, executionTime);

            return ret;

        }

        // Сохраняет цены
        public void SavePrices(int Week, string[] Prices, int raceModeId, int idGroup)
        {
            DateTime startTime = DateTime.Now;
            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("delete from prices where week='" + Week.ToString()
                    + "' AND idRaceMode = " + raceModeId +
                    " AND idGroup = " + idGroup +
                    "; insert into prices (week,d1,d2,d3,d4,d5,d6,d7, idRaceMode, idGroup) values ('" + Week.ToString() + "','" + Prices[1] + "','" +
                    Prices[2] + "','" + Prices[3] + "','" + Prices[4] + "','" + Prices[5] + "','" +
                    Prices[6] + "','" + Prices[7] + "', " + raceModeId + ", " + idGroup + ")", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }


            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("SavePrices", Logger.LogType.info, executionTime);

        }

        // Загружает все цены
        public List<AdminControl.PricesForRaceModes> GetPrices(int Week = 1)
        {
            DateTime startTime = DateTime.Now;

            List<AdminControl.PricesForRaceModes> result = new List<AdminControl.PricesForRaceModes>();


            //string[] Prices = new string[9];

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from prices where week='" + Week.ToString() + "'", db2))
                {
                    SqlDataReader res = cmd.ExecuteReader();

                    while (res.Read())
                    {
                        AdminControl.PricesForRaceModes pricesForSomeRaceMode = new AdminControl.PricesForRaceModes();

                        for (int i = 1; i <= 7; i++)
                        {
                            pricesForSomeRaceMode.Prices[i] = res["d" + i.ToString()].ToString();
                        }
                        pricesForSomeRaceMode.idRaceMode = Convert.ToInt32(res["idRaceMode"]);
                        pricesForSomeRaceMode.idGroup = res["idGroup"] == DBNull.Value ? -1 : Convert.ToInt32(res["idGroup"]);
                        pricesForSomeRaceMode.idGroup = res["idGroup"] != DBNull.Value ? Convert.ToInt32(res["idGroup"]) : 0;

                        result.Add(pricesForSomeRaceMode);
                    }

                    res.Close();

                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetPrices", Logger.LogType.info, executionTime);

            return result;
        }





        // Получает дату и время старта рейса
        public DateTime GetRaceDateTime(int RaceID)
        {
            DateTime startTime = DateTime.Now;

            DateTime dat = new DateTime();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select racedate from races where id='" + RaceID + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            dat = Convert.ToDateTime(res[0]);
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRaceDateTime", Logger.LogType.info, executionTime);


            return dat;
        }

        // Ставит или убирает машину со стоянки
        public void SetKartWait(string KartID, string Stat)
        {
            if (connected)
            {
                int InsertID = -1;
                string Message = String.Empty;

                if (Stat == "1") Message = "Поставлен на стоянку"; else Message = "Убран со стоянки";
                AddMessage(Convert.ToInt32(KartID), 0, 2, DateTime.Now, Message);
                InsertID = GetLastInsertID("Messages");


                using (SqlCommand cmd = new SqlCommand("update karts set message_id='" + InsertID.ToString() + "', wait='" + Stat + "' where id='" + KartID + "'", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
        }

        /* Object Types
             *          -1 события от неизвестно кого ???
             *          0 - Kart
             *          1 - Pilot
             *          2 - Event
         *          */


        // Получает все события


        /* Object Types
             *          -1 события от неизвестно кого ???
             *          0 - Kart
             *          1 - Pilot
             *          2 - Event
         *          */
        // eventType - 0 - все, 1 = будущие, 2 = прошедшие, 3 - только на какую-то дату
        public List<Hashtable> GetAllEvents(int objectType, DateTime someDate, int eventType = 0)
        {
            DateTime startTime = DateTime.Now;

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {
                string commandText = "";

                string objectTypeName = "";
                {
                    switch (objectType)
                    {
                        case 0: objectTypeName = " id_kart > -1 and "; break;
                        case 1: objectTypeName = " id_pilot > -1 and "; break;
                        case 2: objectTypeName = " id_program_user > -1 and "; break;
                        case -1: objectTypeName = " "; break;
                    }
                }


                switch (eventType)
                {
                    case 0: commandText = "select id,[date],message,subject from messages where " + objectTypeName + " m_type=0 order by created asc"; break; // за всё время
                    case 1: commandText = "select id,[date],message,subject from messages where " + objectTypeName + " m_type=0 and [date] >= GETDATE() order by created asc"; break; // будущие
                    case 2:
                        commandText = "select id,[date],message,subject from messages where " + objectTypeName + " m_type=0 and [date] <= '" +
                    datetimeConverter.toDateTimeString(DateTime.Now) + "' order by created asc"; break; // прошедшие
                    case 3:
                        commandText = "select id,[date],message,subject from messages where " + objectTypeName + " m_type=0 and [date] >= '" +
               datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(someDate)) + "' order by [date] asc"; break;

                        /*
                        case 3: commandText = "select id,[date],message,subject from messages where " + objectTypeName + " m_type=0 and [date] >= '" +
                        datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(someDate)) + "' and [date] <= '" +
                        datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(someDate)) + "' order by created asc"; break;
                        */

                }

                using (SqlCommand cmd = new SqlCommand(commandText, db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllEvents", Logger.LogType.info, executionTime);

            return ret;
        }

		public IEnumerable<string[]> GetPartners(bool notDeleted = false)
		{
			if (!connected)
			{
				yield break;
			}
			var query = $"SELECT * FROM partners {(notDeleted ? "WHERE deleted = 0" : "")}";
			using (var command = new SqlCommand(query, db))
			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					var arr = new string[4];
					arr[0] = reader["id"].ToString();
					arr[1] = reader["name"].ToString();
					arr[2] = Convert.ToDecimal(reader["commission"]).ToString("F2");
					arr[3] = Convert.ToBoolean(Convert.ToInt32(reader["deleted"])) ? "да" : "нет"; 
					yield return arr;
				}
			}
		}

		// Получает логины пользователей
		public List<Hashtable> GetUserLogins(DateTime Date)
        {
            DateTime startTime = DateTime.Now;

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select * from logins where created >='" +
                    datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(Date)) +
                    "' AND created <= '" + datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(Date)) +
                    "' order by created desc", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetUsersLogins", Logger.LogType.info, executionTime);

            return ret;
        }


        // Получает набор данных по проездам, которые не зарегистрированы в заездах
        public List<Hashtable> GetTransponderDetect(DateTime startDate, DateTime endDate)
        {
            DateTime startTime = DateTime.Now;

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select * from noracekart where created  >='" +
                    datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(startDate)) + "' AND created  <='" +
                    datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(endDate)) +
                    "' order by created desc", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetTransponderDetect", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает участников месячной гонки
        public List<Hashtable> GetMonthRaceMembers(DateTime Date)
        {
            DateTime startTime = DateTime.Now;
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select d.id,d.pilot_id,u.[name], u.surname, u.email,u.tel, r.track_id ,t.name as track_name,r.created from users u, races r, tracks t, race_data d where d.light_mode=0 and t.id=r.track_id and u.id=d.pilot_id and r.id = d.race_id and d.monthrace=1 and d.race_month_date = '" +
                    datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(Date))
                    + "' group by d.id,d.pilot_id, u.name, u.surname, u.email, u.tel, r.track_id, t.name, r.created", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable temp;
                        while (res.Read())
                        {
                            temp = ConvertResult(res);
                            temp["seconds"] = GetMinTimeFromRace(temp["id"].ToString());

                            if (temp["seconds"].ToString().Length > 0)
                                ret.Add(temp);

                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetMonthRaceMembers", Logger.LogType.info, executionTime);

            return ret;
        }

        // удалить круг
        public bool delLap(int idLap, string pilot, string operatorNick, string lapTime)
        {
            bool result = false;

            if (connected)
            {
                race_times someLap = edb.race_times.Find(idLap);
                if (someLap != null)
                {

                    edb.Entry(someLap).State = EntityState.Deleted;
                    edb.SaveChanges();

                    AddToJurnal("14", -1, -1, "Оператор программы " +
                        operatorNick + " удалил круг " + idLap + " (время: " + lapTime + ") пилота " + pilot);

                    result = true;
                }

            }

            return result;
        }


        // удалить все круги пилота по определённой трассе
        public bool delPilotStatisticForSomeTrack(int idPilot, string operatorNick, int trackId)
        {
            bool result = false;

            IEnumerable<race_times> pilotLaps = edb.race_times.Where(m => m.race_data.pilot_id == idPilot).Where(m => m.race_data.race.track_id == trackId);

            BusyForm busyForm = new BusyForm("Удаление заездов пилота '" + edb.users.Find(idPilot).name + " " + edb.users.Find(idPilot).surname + "' по трассе '" + edb.tracks.Find(trackId).name + "'", pilotLaps.Count());
            busyForm.Show();

            int i = 1;

            bool isCancelled = false;

            edb.Configuration.AutoDetectChangesEnabled = false;

            foreach (race_times item in pilotLaps)
            {
                edb.Entry(item).State = EntityState.Deleted;
                isCancelled = busyForm.SetProgressValue(i);
                if (isCancelled)
                {
                    break;
                }
                i++;
            }

            edb.Configuration.AutoDetectChangesEnabled = true;
            edb.Database.Connection.Close();
            edb.Database.Connection.Open();

            if (isCancelled)
            {
                busyForm.CancelForm();
            }
            else
            {

                try
                {

                    edb.SaveChanges();
                    busyForm.CloseForm();
                }
                catch (Exception ex)
                {
                    busyForm.CloseForm();
                    MessageBox.Show("Не удалось удалить данные по пилоту, так как имеются зависимые данные");
                    return result;
                }

                AddToJurnal("16", -1, -1, "Оператор программы " +
                           operatorNick + " удалил данные по пилоту " + edb.users.Find(idPilot).name + " по трассе " + edb.tracks.Find(trackId).name);


                result = true;
            }

            return result;
        }

        // удалить все круги пилота
        public bool delPilotStatistic(int idPilot, string operatorNick)
        {
            bool result = false;

            IEnumerable<race_times> pilotLaps = edb.race_times.Where(m => m.race_data.pilot_id == idPilot);

            BusyForm busyForm = new BusyForm("Удаление всех заездов пилота " + edb.users.Find(idPilot).name + " " + edb.users.Find(idPilot).surname, pilotLaps.Count());
            busyForm.Show();

            int i = 1;

            bool isCancelled = false;

            edb.Configuration.AutoDetectChangesEnabled = false;

            foreach (race_times item in pilotLaps)
            {
                edb.Entry(item).State = EntityState.Deleted;
                isCancelled = busyForm.SetProgressValue(i);
                if (isCancelled)
                {
                    break;
                }
                i++;
            }

            edb.Configuration.AutoDetectChangesEnabled = true;

            edb.Database.Connection.Close();
            edb.Database.Connection.Open();

            if (isCancelled)
            {
                busyForm.CancelForm();
            }
            else
            {

                try
                {
                    edb.SaveChanges();
                    busyForm.CloseForm();
                }
                catch (Exception ex)
                {
                    busyForm.CloseForm();
                    MessageBox.Show("Не удалось удалить данные по пилоту, так как имеются зависимые данные");
                    return result;
                }

                AddToJurnal("15", -1, -1, "Оператор программы " +
                           operatorNick + " удалил все данные по пилоту " + edb.users.Find(idPilot).name);


                result = true;
            }

            return result;
        }



        // по номеру строки возвращает тип события 
        public int getIdEvent(int eventIndex)
        {

            int idEvent = -1;

            switch (eventIndex)
            {
                case 1: idEvent = 10; break;
                case 2: idEvent = 11; break;
                case 3: idEvent = 12; break;
                case 4: idEvent = 13; break;
                case 5: idEvent = 14; break; // круг удалён
                case 6: idEvent = 15; break; // удалены все круги пилота
                case 7: idEvent = 16; break; // удалены все круги пилота по определённому треку
            }

            return idEvent;
        }

        // Получает журнал заездов
        public List<Hashtable> GetRaceJurnal(PageLister page, DateTime startDate, DateTime endDate, int eventIndex,
            int pageIndex, int pageSize)
        {

            DateTime startTime = DateTime.Now;

            if (page != null)
            {
                page.OnUpdate = true;
            }


            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("GetRacesByEventJournal", db);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                    cmd.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;
                    cmd.Parameters.Add("@idEvent", SqlDbType.Int).Value = getIdEvent(eventIndex);
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;


                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();

                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
                catch (Exception ex)
                {

                }


                page.RowsMax = GetRacesCount(startDate, endDate, eventIndex);

                page.PagesCount = Convert.ToInt32(Math.Ceiling((double)page.RowsMax / (double)page.PageSize));
                page.FillPageNumbers();

            }


            if (page != null)
            {
                page.OnUpdate = false;
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRaceJurnal", Logger.LogType.info, executionTime);

            return ret;


            /*

            string wr = String.Empty;
            switch (FLT)
            {
                case 1: wr = "tp=10"; break;
                case 2: wr = "tp=11"; break;
                case 3: wr = "tp=12"; break;
                case 4: wr = "tp=13"; break;
                case 0:
                default: wr = "tp=10 or tp=11 or tp=12 or tp=13"; break;
            }

            if (connected)
                using (SqlCommand cmd = new SqlCommand("select * from jurnal where (" + wr + ") and created between '" + Date + " 00:00:00' and '" + Date2 + " 23:59:59' ", db))
                using (SqlDataReader res = cmd.ExecuteReader())
                    while (res.Read())
                        ret.Add(ConvertResult(res));
             */


        }


        // Получает количество событий по заездам указанного типа
        public int GetRacesCount(DateTime startDate, DateTime endDate, int eventIndex)
        {
            DateTime startTime = DateTime.Now;

            int result = -1;

            if (connected)
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("GetRacesCount", db);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                    cmd.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;
                    cmd.Parameters.Add("@idEvent", SqlDbType.Int).Value = getIdEvent(eventIndex);

                    result = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception ex)
                {

                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetRacesCount", Logger.LogType.info, executionTime);

            return result;
        }


        // только для анонсера ??
        public List<Hashtable> GetAnonserBestResultsFromDateRange(DateTime d1, DateTime d2,
            bool uniq,
            int TrackID)
        {

            DateTime startTime = DateTime.Now;
            List<Hashtable> result = GetBestResults(TrackID, d1, d2, 20);

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAnonserBestResultsFromDateRange", Logger.LogType.info, executionTime);

            return result;

            /*

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {
               
                try
                    {
                        SqlCommand cmd = new SqlCommand("getAnonserBestTrackResults", db);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_track", SqlDbType.Int).Value = TrackID;
                        cmd.Parameters.Add("@start_date", SqlDbType.DateTime).Value = d1;
                        cmd.Parameters.Add("@end_date", SqlDbType.DateTime).Value = d2;
                        cmd.Parameters.Add("@is_unique", SqlDbType.Bit).Value = uniq;

                        using (SqlDataReader res = cmd.ExecuteReader())
                        {
                            Hashtable row = new Hashtable();
                          //  int rows = 0;
                            while (res.Read())
                            {
                                ret.Add(ConvertResult(res));

                               
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
 
                */


            /*string query = "select   d.id,d.pilot_id,u.name,r.racedate,u.nickname, u.surname, u.email,u.tel, r.track_id,t.name as track_name,r.created, (select min(replace(seconds,',','.')*1.0) from race_times where member_id=d.id) as seconds from users u, races r, tracks t, race_data d where d.light_mode=0 and t.id=r.track_id and u.id=d.pilot_id and  (select min(replace(seconds,',','.')) from race_times where member_id=d.id) and r.id = d.race_id and r.track_id='"+TrackID+"' and r.racedate between '" + d1 + "' and '" + d2 + "' " + (uniq ? " group by d.pilot_id " : "") + " order by seconds";
            */

            /*
            string query = "select	rd.id, rd.pilot_id,	rt.seconds, rd.race_id,r.racedate, 	u.name,u.nickname, u.surname,u.email, u.tel, r.track_id,r.created,t.name as track_name	from users u, race_times rt, race_data rd, races r, tracks t where 	rt.seconds = replace((select min(replace(rt.seconds,',','.')*1.0) from 	race_times rt, 			race_data rd 	where	rd.pilot_id=u.id and rd.light_mode = 0	and not isnull(rt.seconds)  and rt.seconds > 0	and rt.member_id=rd.id),'.',',')	and rd.id = rt.member_id	and u.id  = rd.pilot_id	and r.id  = rd.race_id	and t.id  = r.track_id and r.track_id='" + TrackID.ToString() + "' and r.racedate between '" + d1 + "' and '" + d2 + "' group by u.id order by seconds";


            using (SqlCommand cmd = new SqlCommand(query, db2))
            {
                using (SqlDataReader res = cmd.ExecuteReader())
                {
                    while (res.Read())
                    {
                        ret.Add(ConvertResult(res));
                    }
                }
            }
             */

            /*
        }


        return ret;
            */
        }

        // Получает лучшие результаты
        public List<Hashtable> GetBestResults(int TrackID, DateTime startDate, DateTime endDate, int amountOfRecords)
        {
            DateTime startTime = DateTime.Now;

            List<Hashtable> result = new List<Hashtable>();
            TimeSpan executionTime;
			if (!connected)
			{
				return result;
			}

			var query =
				"SELECT u.name, u.nickname, rd.created, u.tel, u.surname, seconds, u.email, rd.pilot_id, rd.modified as racedate, lap as id, race_id " +
				"FROM " +
					"(SELECT race_data_times.member_id, pilot_top.seconds, race_data_times.lap " +
					"FROM " +
						$"(SELECT TOP {amountOfRecords} rd.pilot_id, MIN(seconds) as seconds " +
						"FROM((SELECT member_id, MIN(seconds) as seconds FROM crazykart.dbo.race_times GROUP BY member_id) rt LEFT JOIN crazykart.dbo.race_data rd ON rt.member_id = rd.id) LEFT JOIN crazykart.dbo.races r ON r.id = rd.race_id " +
						$"WHERE r.racedate BETWEEN '{startDate.AsDayStart().ToSqlString()}' and '{endDate.AsDayEnd().ToSqlString()}' {(TrackID != -1).ToStringIf("and r.track_id = " + TrackID)} " +
						"GROUP BY rd.pilot_id " +
						"ORDER BY seconds) pilot_top, " +
						"(SELECT member_id, seconds, pilot_id, lap FROM crazykart.dbo.race_times rt INNER JOIN crazykart.dbo.race_data rd ON rt.member_id = rd.id) race_data_times " +
					"WHERE pilot_top.pilot_id = race_data_times.pilot_id and pilot_top.seconds = race_data_times.seconds) top_res LEFT JOIN crazykart.dbo.race_data rd ON top_res.member_id = rd.id LEFT JOIN crazykart.dbo.users u ON u.id = rd.pilot_id " +
				"ORDER BY seconds";

			using (var command = new SqlCommand(query, db))
			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					var set = new Hashtable();
					set["name"] = reader["name"];
					set["nickname"] = reader["nickname"];
					set["created"] = reader["created"];
					set["tel"] = reader["tel"];
					set["surname"] = reader["surname"];
					set["seconds"] = reader["seconds"];
					set["email"] = reader["email"];
					set["pilot_id"] = reader["pilot_id"];
					set["racedate"] = reader["racedate"];
					set["id"] = reader["id"];
					set["track_id"] = TrackID;
					set["race_id"] = reader["race_id"];
					result.Add(set);
				}
			}
			return result;
        }

        // Возвращает лучший результат дня
        public List<BestPilots> GetBestPilots(int TrackID, bool uniq, DateTime startDate, DateTime endDate, int amountOfRecords)
        {
            List<BestPilots> bestPilots = new List<BestPilots>();

            if (connected)
            {
				List<race_times> raceTimes = new List<race_times>();
                    //getTop40LapsTimes(TrackID, startDate, endDate, uniq, amountOfRecords);

                for (int i = 0; i < raceTimes.Count; i++)
                {
                    Hashtable row = new Hashtable();
                    try
                    {
                        race_times someRaceTime = raceTimes.ElementAt(i);

                        var temp = new BestPilots()
                        {
                            PilotName = someRaceTime.race_data.user.name + " " + someRaceTime.race_data.user.surname,
                            DateOfRecord = someRaceTime.race_data.race.racedate.Value.ToString("dd MMMM yyyy"),
                            RecordTime = someRaceTime.seconds.ToString(),
                        };

                        bestPilots.Add(temp);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return bestPilots;
        }




        // Получает счета пользователей
        public List<Hashtable> GetUsersBallans()
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select u.id,u.name,u.surname,u.email, u.tel from users u", db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable temp;
                        while (res.Read())
                        {
                            temp = ConvertResult(res);
                            temp["sum"] = GetUserBallans(Convert.ToInt32(temp["id"].ToString()));


                            if (Convert.ToDouble(temp["sum"]) != 0)
                                ret.Add(temp);

                        }
                    }
                }
            }

            return ret;
        }

        // Получает минимальное время круга у пилота
        public string GetMinTimeFromRace(string MemberID)
        {
            DateTime startTime = DateTime.Now;

            string ret = String.Empty;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select min(replace(seconds,',','.')*1.0) from race_times where member_id='" + MemberID + "'", db))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = res[0].ToString();
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetMinTimeFromRace", Logger.LogType.info, executionTime);

            return ret;
        }

        // Обновляет сообщение
        public void UpdateMessage(string ID, DateTime Date, string Subject, string Message)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("update messages set subject = '" + Subject +
                    "', message = '" + Message + "', [date] = '" +
                   datetimeConverter.toDateTimeString(Date) + "', modified=GETDATE() where id='" + ID + "'", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("UpdateMessage", Logger.LogType.info, executionTime);
        }

        // Выполняет запрос
        public void ExecuteQuery(string Query)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand(Query, db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ExecuteQuery(" + Query + ")", Logger.LogType.info, executionTime);

        }


        // Удаляет сообщение
        public void DelMessage(string ID)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
                using (SqlCommand cmd = new SqlCommand("delete from messages where id='" + ID + "'", db))
                    cmd.ExecuteNonQueryHandled();

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("DelMessage", Logger.LogType.info, executionTime);
        }

        // Получает баланс пользователя
        public double GetUserBallans(int ID)
        {
            double ret = 0.0;

            if (connected)
            {
                // TODO using (SqlCommand cmd = new SqlCommand("select (sum(CASE WHEN(sign=0)THEN sum else 0 end)-sum(case when (sign=1) then sum else 0 end)) as summa from user_cash where user_id='" + ID + "' and not isnull(sum)", db2))

                using (SqlCommand cmd = new SqlCommand("select (sum(CASE WHEN(sign=0)THEN sum else 0 end)-sum(case when (sign=1) then sum else 0 end)) as summa from user_cash where user_id=" + ID, db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            string val = res[0].ToString();
                            ret = val == String.Empty ? 0.0 : Double.Parse(val);
                        }
                    }
                }
            }

            return ret;
        }

        // Добавляет запись в журнал и возвращает её id
        public int AddToJurnal(string DocNum, int UserID, int RaceID, string Comment)
        {
            DateTime startTime = DateTime.Now;

            string idUser = "null";

            string idRace = "null";

            if (UserID > 0)
            {
                idUser = Convert.ToString(UserID);
            }

            if (RaceID > 0)
            {
                idRace = Convert.ToString(RaceID);
            }

            int ret = 0;
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into jurnal (tp,user_id,comment,race_id, created) values ('" + DocNum + "'," + idUser + ",'" + Comment + "'," +
                    idRace + ", GETDATE())", db))
                {
                    try
                    {
                        cmd.ExecuteNonQueryHandled();
                    }
                    catch (Exception ex)
                    {
                        MainForm.log("Error 4768: Не удалось добавить запись в jurnal. Ошибка: " + ex.Message);
                        throw ex;
                    }

                    // Thread.Sleep(200);
                }
            }

            ret = GetLastInsertID("jurnal");

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("AddToJurnal", Logger.LogType.info, executionTime);

            return ret;
        }

        // Добавляет запись в кассу
        private void AddToCassa(string DocID, string Sum, string Sign, int partnerId, string partnerCode, bool pastdate = false)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand($"insert into cassa (doc_id,sum,sign,date, partner_id, ref_code) values ('{DocID}','{Sum.Replace(",", ".")}','{Sign}', GETDATE(), {(partnerId == -1 ? "null" : partnerId.ToString())}, '{(partnerId == -1 ? "null" : partnerCode.ToString())}')", db))
                {
                    try
                    {
                        cmd.ExecuteNonQueryHandled();
                    }
                    catch (Exception ex)
                    {
                        MainForm.log("Error 4769: Не удалось добавить запись в cassa. Ошибка: " + ex.Message);
                        throw ex;
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("AddToCassa", Logger.LogType.info, executionTime);

        }

        // Добавляет запись в UserCash
        private void AddToUserCash(string DocID, int UserID, string Sum, string Sign, int partnerId, string partnerCode)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into user_cash (doc_id,user_id,sum,sign, created, partner_id, ref_code) values ('" + DocID + "'," + UserID + ",'" + Sum.Replace(",", ".") + "','" + Sign + $"', GETDATE(), {(partnerId == -1 ? "null" : partnerId.ToString())}, '{(partnerId == -1 ? "null" : partnerCode.ToString())}')", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("AddToUserCash", Logger.LogType.info, executionTime);

        }

        // Журналирование кассовых событий
        public void Jurnal_Cassa(string DocNum, int UserID, int RaceID, string Sum, string Sign, string Comments, int partnerId = -1, string partnerCode = "", bool pastdate = false)
        {
            int Doc_id = AddToJurnal(DocNum, UserID, RaceID, Comments);
            AddToCassa(Doc_id.ToString(), Sum, Sign, partnerId, partnerCode, pastdate);
        }

        // Операция коррекции баланса пользователя
        public void Jurnal_UserCash(string DocNum, int idRaceData, string Sum, string Sign, string Comments, int RaceID, int partnerId = -1, string partnerCode = "")
        {

            int Doc_id = AddToJurnal(DocNum,
                getUserByIdRaceData(idRaceData).id, RaceID, Comments);
            AddToUserCash(Doc_id.ToString(), getUserByIdRaceData(idRaceData).id, Sum, Sign, partnerId, partnerCode);
        }

        // Операция коррекции баланса пользователя
        public void Jurnal_UserCashByUserId(string DocNum, int idUser, string Sum, string Sign, string Comments, int RaceID, int partnerId, string partnerCode)
        {

            int Doc_id = AddToJurnal(DocNum,
                idUser, RaceID, Comments);
            AddToUserCash(Doc_id.ToString(),
                idUser, Sum, Sign, partnerId, partnerCode);
        }

        // returns user by his record in race_data table
        public users getUserByIdRaceData(int idRaceData)
        {
            race_data rd = edb.race_data.Find(idRaceData);

            if (rd == null)
            {
                return null;
            }

            return edb.users.Find(rd.pilot_id);
        }


        // Операция добавление денег на счет пользователя
        public void Jurnal_AddToUserCash(string DocNum, int UserID, string Sum, string Sign, string Comments, int partnerId = -1, string partnerCode = "")
        {
            int Doc_id = AddToJurnal(DocNum, UserID, -1, Comments);
            AddToCassa(Doc_id.ToString(), Sum, Sign, partnerId, partnerCode);
            AddToUserCash(Doc_id.ToString(), UserID, Sum, Sign, partnerId, partnerCode);
        }

        // Получает сумму в кассе
        public double GetCassaSumm(DateTime D1, DateTime D2, int TP, int sign, int race_id = 0)
        {
            DateTime startTime = DateTime.Now;

            double ret = 0;

            if (connected)
            {

                string query = String.Empty;
                string q1 = "select sum(c.sum*1.0) from jurnal j, cassa c where c.doc_id=j.id" +
                    (race_id != 0 ? " and (j.race_id = " + race_id + ") " : " and j.created between '" +
                    datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(D1)) +
                    "' and '" +
                    datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(D2))
                     + "'") +
                     " and c.sign='" + sign.ToString() + "' ";
                string q2 = "select sum(u.sum*1.0) from jurnal j, user_cash u where u.doc_id = j.id" +
                    (race_id != 0 ? " and (j.race_id = " + race_id + ") " : " and j.created between '" +
                    datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(D1)) +
                    "' and '" +
                    datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(D2))
                     + "'") +
                     " and u.sign='" + sign.ToString() + "' ";

                switch (TP)
                {
                    case 1: query = q1; break;
                    case 2: query = q2; break;

                }

                using (SqlCommand cmd = new SqlCommand(query, db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            string s = res[0].ToString();
                            if (s.Length > 0)
                                ret = Double.Parse(s);
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetCassaSumm", Logger.LogType.info, executionTime);

            return ret;
        }

		// Получает список операций по кассе за период
		// 1 reportType = реальные, 2 = виртуальные
		public List<Hashtable> GetCassaReport(DateTime Date, int reportType, DateTime Date2, PageLister page, int race_id, int raceTypeId, int userGroupId, int partnerId, int cashTerminalType)
		{
			DateTime startTime = DateTime.Now;

			List<Hashtable> ret = new List<Hashtable>();


			if (connected)
			{

				#region for pagelister
				string filter = String.Empty;
				if (page != null)
				{
					string query1 = String.Empty;
					string q11 = "select count(*)as c from jurnal j, cassa c where c.doc_id=j.id and j.created between '"
						+ datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(Date)) +
						"' and '" +
						datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(Date2))
						+ $"' and j.tp in ({GetJurnalTp(cashTerminalType)}) and c.sign in (0,1)";
					string q22 = "select count(*)as c from jurnal j, user_cash u where u.doc_id = j.id and j.created between '" +
						datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(Date)) +
						"' and '" +
						 datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(Date2)) +
						$"' and j.tp in ({GetJurnalTp(cashTerminalType)}) and c.sign in (0,1)";

					switch (reportType)
					{
						case 1: query1 = q11; break;
						case 2: query1 = q22; break;
						case 3: query1 = "(" + q11 + ")" + " union " + "(" + q22 + ")"; break;
					}




					using (SqlCommand cmd2 = new SqlCommand(query1, db3))
					{
						using (SqlDataReader res = cmd2.ExecuteReader())
						{
							if (res.Read())
							{
								int Rows = Convert.ToInt32(res["c"].ToString());

								if (page.RowsMax != Rows || page.OnChange)
								{
									page.RowsMax = Rows;
									page.PagesCount = Convert.ToInt32(Math.Ceiling((double)page.RowsMax / (double)page.PageSize));
									page.FillPageNumbers();
									page.OnChange = false;
								}
								//  page.setPageListerButtonsEnability();


							}
						}
					}


					// filter += page.Filter;
				}

				#endregion

				var cmd = new SqlCommand(
						"SELECT * FROM " +
							"(SELECT ROW_NUMBER() OVER(ORDER BY j.id) AS RowNum, j.id, j.created as 'date', j.comment, j.user_id, j.tp, j.race_id, c.sum, c.sign, rm.name as race_mode_name, p.name as partner_name, g.name as group_name, j.tp as terminal, c.ref_code  " +
							$"FROM jurnal j LEFT JOIN(SELECT race_id, pilot_id, car_id, id_race_mode FROM race_data GROUP BY race_id, pilot_id, car_id, id_race_mode) rd ON j.race_id = rd.race_id and j.user_id = rd.pilot_id LEFT JOIN {(reportType == 1 ? "cassa" : "user_cash")} c ON j.id = c.doc_id LEFT JOIN partners p ON c.partner_id = p.id LEFT JOIN users u ON j.user_id = u.id LEFT JOIN race_modes rm ON rm.id = rd.id_race_mode LEFT JOIN groups g ON u.gr = g.id " +
							$"WHERE j.tp in ({GetJurnalTp(cashTerminalType)}) and c.sign in (0,1) and j.created BETWEEN @startDate and @endDate {(raceTypeId > -1).ToStringIf("and rd.id_race_mode = " + raceTypeId)} {(userGroupId > -1).ToStringIf("and u.gr = " + userGroupId)} {(partnerId > -1).ToStringIf("and c.partner_id = " + partnerId)}) as some_table " +
						"WHERE RowNum BETWEEN(@PageIndex -1) * @PageSize + 1 and @PageIndex * @PageSize " +
						"ORDER BY id", db2);
				cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = page.CurrentPageNumber;
				cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = page.PageSize;
				cmd.Parameters.Add("@startDate", SqlDbType.DateTime).Value = Date;
				cmd.Parameters.Add("@endDate", SqlDbType.DateTime).Value = Date2;

				//if (reportType == 2)
				//               {
				//                   StoredProcName = "GetVirtualCassaReport";
				//               }
				//               cmd = new SqlCommand(StoredProcName, db2);
				//               cmd.CommandType = CommandType.StoredProcedure;
				//               cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = page.CurrentPageNumber;
				//               cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = page.PageSize;
				//               cmd.Parameters.Add("@startDate", SqlDbType.DateTime).Value = Date;
				//               cmd.Parameters.Add("@endDate", SqlDbType.DateTime).Value = Date2;



				using (SqlDataReader res = cmd.ExecuteReader())
				{
					Hashtable row = new Hashtable();
					int rows = 0;
					while (res.Read())
					{
						row = ConvertResult(res);
						ret.Add(row);
						rows++;
					}
				}

				cmd.Dispose();

			}

			if (page != null)
            {
                page.OnUpdate = false;
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetCassaReport", Logger.LogType.info, executionTime);

            return ret;
        }

		private string GetJurnalTp(int moneyType)
		{
			var tps = new List<int>() { 4, 3, 15 };
			if(moneyType == -1)
			{
				tps.Add(1);
				tps.Add(33);
			}
			else
			{
				tps.Add(moneyType);
			}
			return string.Join(", ", tps);
		}

		public double GetCassaReportSum(DateTime Date, int reportType, DateTime Date2, int race_id, int raceTypeId, int userGroupId, int partnerId, int cashTerminalType)
		{
			Date = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0);
			Date2 = new DateTime(Date2.Year, Date2.Month, Date2.Day, 23, 59, 59);
			var result = 0d;
			if (!connected)
			{
				return result;
			}
			var query =
				"SELECT c.sum, CASE c.sign WHEN 0 THEN 1 ELSE -1 END as my_sign " +
				$"FROM jurnal j LEFT JOIN(SELECT race_id, pilot_id, car_id, id_race_mode FROM race_data GROUP BY race_id, pilot_id, car_id, id_race_mode) rd ON j.race_id = rd.race_id and j.user_id = rd.pilot_id LEFT JOIN {(reportType == 1 ? "cassa" : "user_cash")} c ON j.id = c.doc_id LEFT JOIN partners p ON c.partner_id = p.id LEFT JOIN users u ON j.user_id = u.id " +
							$"WHERE j.tp in ({GetJurnalTp(cashTerminalType)}) and c.sign in (0,1) and j.created BETWEEN @startDate and @endDate {(raceTypeId > -1).ToStringIf("and rd.id_race_mode = " + raceTypeId)} {(userGroupId > -1).ToStringIf("and u.gr = " + userGroupId)} {(partnerId > -1).ToStringIf("and c.partner_id = " + partnerId)}";
			var cmd = new SqlCommand(query, db2);
			cmd.Parameters.Add("@startDate", SqlDbType.DateTime).Value = Date;
			cmd.Parameters.Add("@endDate", SqlDbType.DateTime).Value = Date2;
			using (SqlDataReader res = cmd.ExecuteReader())
			{
				while (res.Read())
				{
					result += (double)res["sum"] * (int)res["my_sign"];
				}
			}

			cmd.Dispose();
			return result;
		}

		// Получает весь список трасс
		public List<Hashtable> GetAllTracks()
        {
            DateTime startTime = DateTime.Now;

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select * from tracks where is_deleted <> 1 order by created", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllTracks", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает весь список групп
        public List<Hashtable> GetAllGroups()
        {
            DateTime startTime = DateTime.Now;

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select * from groups order by created", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllGroups", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает всю статистику по картам
        public List<Hashtable> GetAllKartsStatistic(double Deflen, DateTime D1, DateTime D2)
        {
            DateTime startTime = DateTime.Now;

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select * from karts", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable temp = new Hashtable();
                        while (res.Read())
                        {
                            temp = ConvertResult(res);
                            temp["repairs"] = GetKartRepairs(temp["id"].ToString(), D1, D2);


                            using (SqlCommand cmd2 = new SqlCommand("select d.id, r.track_id from race_data d, races r where d.car_id='" + temp["id"].ToString() + "' and r.id=d.race_id and r.stat=2 and r.racedate>='" + datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(D1)) + "' and r.racedate<='" + datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(D2)) + "' ", db))
                            {
                                using (SqlDataReader res2 = cmd2.ExecuteReader())
                                {
                                    List<Hashtable> temp2 = new List<Hashtable>();
                                    while (res2.Read())
                                        temp2.Add(ConvertResult(res2));

                                    temp["races_all"] = temp2.Count;

                                    double length = 0;
                                    double tlength;
                                    double ttlength;
                                    for (int i = 0; i < temp2.Count; i++)
                                    {
                                        tlength = GetTrackLength(temp2[i]["track_id"].ToString());
                                        ttlength = tlength < 0 ? Deflen : tlength;

                                        length += ttlength * GetMemberLapsFromRace(temp2[i]["id"].ToString());
                                    }

                                    temp["length_all"] = length;
                                }
                            }

                            temp["fuel_add"] = GetKartFuel(temp["id"].ToString(), D1, D2, "0");
                            temp["fuel_rem"] = GetKartFuel(temp["id"].ToString(), D1, D2, "1");
                            ret.Add(temp);
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetAllKartsStatistic", Logger.LogType.info, executionTime);

            return ret;
        }

        // Получает количество кругов, которые проехал участник заезда
        public int GetMemberLapsFromRace(string MemberID)
        {
            DateTime startTime = DateTime.Now;

            int ret = 0;


            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select count(*) from race_times where member_id='" + MemberID + "'", db3))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            ret = Convert.ToInt32(res[0].ToString());
                        }
                    }
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetMemberLapsFromRace", Logger.LogType.info, executionTime);

            return ret;
        }


        // Получает всю сумму в кассе
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="forOneDay">за один день или за всё время</param>
        /// <param name="NotTransferDoc"></param>
        /// <param name="AllSumm"></param>
        /// <param name="NoTransf"></param>
        /// <returns></returns>
        public string GetCashFromCassa(DateTime Date, bool forOneDay = false, bool NotTransferDoc = false, bool AllSumm = true, bool NoTransf = true, int race_id = 0)
        {
            string ret = String.Empty;
            string temp;

            if (connected)
            {
                string where = String.Empty;
                if (forOneDay)
                {
                    if (AllSumm)
                    {
                        where = " and j.tp!=7 ";
                    }
                }
                //"+(!NoTransf?"and (j.tp != 7 and j.tp!= 15)":"") + "

                //using (SqlCommand cmd = new SqlCommand("select (sum(CASE WHEN(sign=0)THEN sum*1.0 else 0 end)-sum(case when (sign=1) then sum*1.0 else 0 end)) as summa from cassa c, jurnal j where j.id=doc_id " + (!NoTransf ? "and (j.tp != 7 and j.tp!= 15)" : "") + "  and not isnull(c.sum)" + (Date.Length > 2 ? " and c.date" + (AllTime ? "<=" : "=") + "'" + Date + "'" : ""), db))



                string commandText = "";

                if (forOneDay)
                {
                    // за один день
                    commandText = "select (sum(CASE WHEN(sign=0)THEN sum*1.0 else 0 end)-sum(case when (sign=1) then sum*1.0 else 0 end)) as summa from cassa c, jurnal j where j.id=doc_id " +
                       (!NoTransf ? "and (j.tp != 7 and j.tp!= 15)" : "") +
                       (race_id != 0 ? "and (j.race_id = " + race_id + ")" : "") +
                       " and c.date >= '" +
                       datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(Date)) +
                       "' and c.date <= '" +
                       datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(Date)) + "'";
                    /*

                          commandText = "select (sum(CASE WHEN(sign=0)THEN sum*1.0 else 0 end)-sum(case when (sign=1) then sum*1.0 else 0 end)) as summa from cassa c, jurnal j where j.id=doc_id " + 
                              (!NoTransf ? "and (j.tp != 7 and j.tp!= 15)" : "") +
                              " and c.date <= '" + 
                              datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(Date)) + "'";
                    */

                    // string temp = "select (sum(CASE WHEN(sign=0)THEN sum*1.0 else 0 end)-sum(case when (sign=1) then sum*1.0 else 0 end)) as summa from cassa c, jurnal j where j.id=doc_id " + (!NoTransf ? "and (j.tp != 7 and j.tp!= 15)" : "") + " and not isnull(c.sum)" + (Date.Length > 2 ? " and c.date" + (AllTime ? "<=" : "=") + "'" + Date + "'" : "");

                    /*
                    commandText = "select (sum(CASE WHEN(sign=0)THEN sum*1.0 else 0 end)-sum(case when (sign=1) then sum*1.0 else 0 end)) as summa from cassa c, jurnal j where j.id=doc_id " + (!NoTransf ? "and (j.tp != 7 and j.tp!= 15)" : "") + " and c.sum != null" +
                        (AllTime ? " and c.date " + (AllTime ? "<=" : ">=") + "'" + datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(Date)) 
                        + "' AND and c.date <='" + datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(Date)) + "'" : "");
                    */
                }
                else // за всё время
                {
                    if (race_id != 0)
                    {
                        commandText =
                            "select (sum(CASE WHEN(sign=0)THEN sum*1.0 else 0 end)-sum(case when (sign=1) then sum*1.0 else 0 end)) as summa from cassa c, jurnal j where j.id=doc_id " +
                            (!NoTransf ? "and (j.tp != 7 and j.tp!= 15)" : "") +
                            (race_id != 0 ? "and (j.race_id = " + race_id + ")" : "");


                    }
                    else
                    {
                        commandText = "select (sum(CASE WHEN(sign=0)THEN sum*1.0 else 0 end)-sum(case when (sign=1) then sum*1.0 else 0 end)) as summa from cassa c, jurnal j where j.id=doc_id " +
                            (!NoTransf ? "and (j.tp != 7 and j.tp!= 15)" : "") +
                            " and c.date <= '" + datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(Date)) + "'";

                    }

                    //commandText = "select (sum(CASE WHEN(sign=0)THEN sum*1.0 else 0 end)-sum(case when (sign=1) then sum*1.0 else 0 end)) as summa from cassa c, jurnal j where j.id=doc_id " + (!NoTransf ? "and (j.tp != 7 and j.tp!= 15)" : "") + " and c.date >= '" + datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(Date)) + "' and c.date <='" + datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(Date)) + "'";
                }

                using (SqlCommand cmd = new SqlCommand(commandText, db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            temp = res[0].ToString();
                            if (temp.Length > 0)
                                ret = Math.Round(Convert.ToDouble(temp), 3).ToString();
                            else ret = "0";
                        }
                    }
                }
            }

            return ret;
        }

		

        // Получает сумму на счетах пилотов
        public string GetCashFromUsers(DateTime date)
        {



            string ret = String.Empty;
            string temp = String.Empty;

            if (connected)
            {

                string command = "select (sum(CASE WHEN(sign=0)THEN sum else 0 end)-sum(case when (sign=1) then sum else 0 end)) as summa from user_cash where created >= '"
                    + datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(date)) + "' AND created <= '" + datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(date)) + "'";

                using (SqlCommand cmd = new SqlCommand(command, db))

                // using (SqlCommand cmd = new SqlCommand("select (sum(CASE WHEN(sign=0)THEN sum else 0 end)-sum(case when (sign=1) then sum else 0 end)) as summa from user_cash where not isnull(sum, '')" + (Date.Length > 2 ? " and created='" + Date + "'" : ""), db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            temp = res[0].ToString();
                            if (temp.Length > 0)
                            {
                                ret = Math.Round(Convert.ToDouble(temp), 3).ToString();
                            }
                            else ret = "0";
                        }
                    }
                }
            }

            return ret;
        }

        // Добавляет трек в базу
        public void AddTrack(string Name, string Length, string FileName)
        {

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into tracks ([name],length,[file], created) values ('" + Name + "'," + Length + ",'" + FileName + "', GETDATE())", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
        }

        // Добавляет группу
        public void AddGroup(string Name, string Sale, string Price)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into groups (name, sale, price, created, modified) values ('" + Name + "','" + Sale + "','" + Price + "','" + getDate() + "', GETDATE())", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
        }

        // Удаляет трек
        public void DelTrack(string TrackID)
        {
            int trackId = Convert.ToInt32(TrackID);



            if (edb.races.Where(m => m.track_id == trackId).Count() > 0)
            {
                edb.tracks.Find(trackId).is_deleted = true;
            }
            else
            {
                edb.Entry(edb.tracks.Find(trackId)).State = EntityState.Deleted;
            }

            edb.SaveChanges();

            /*
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("delete from tracks where id='" + TrackID + "'", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
             */
        }

        // Обновляет данные о треке
        public void ChangeTrack(string TrackID, string Name, string Length, string FileName)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update tracks set [name]='" + Name + "', length = " + Length + ", [file]='" + FileName + "' where id=" + TrackID + "", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
        }

        // Завершает заезд
        public void StopRace(string RaceID)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update races set stat = '2' where id='" + RaceID + "'", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("StopRace", Logger.LogType.info, executionTime);

        }

        // Загружает все группы
        public List<string> GetAllGroupsName()
        {

            List<string> ret = new List<string>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select name from groups order by id", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(res["name"].ToString());
                        }
                    }
                }
            }

            return ret;
        }

        // Получает ID группы
        public int GetGroupID(string Name)
        {

            int ret = -1;

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select id from groups where name='" + Name + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Int32.Parse(res["id"].ToString());
                    }
                }
            }
            return ret;
        }

        // Получает Количество пользователей группы
        public int GetCountPilotsOfGroup(string GroupID)
        {

            int ret = -1;

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select count(*) as c from users where deleted = 0 AND gr='" + GroupID + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Int32.Parse(res["c"].ToString());
                    }
                }
            }
            return ret;
        }

        // Получает имя группы по ID
        public string GetGroupName(string GroupID)
        {
            string ret = String.Empty;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select name from groups where id='" + GroupID + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = res["name"].ToString();
                    }
                }
            }
            return ret;
        }

        // Получает % скидки группы
        public double GetGroupSale(string GroupID)
        {
            double ret = 0;
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select sale from groups where id='" + GroupID + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            if (res["sale"].ToString().Trim().Length > 0)
                            {
                                ret = Double.Parse(res["sale"].ToString());
                            }
                            else
                            {
                                ret = Double.Parse("0");
                            }
                    }
                }
            }
            return ret;
        }

        // Получает ID трека
        public int GetTrackID(string Name)
        {

            int ret = -1;

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select id from tracks where name='" + Name + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Int32.Parse(res["id"].ToString());
                    }
                }
            }
            return ret;
        }

		// Получает имя трека по ID
		public string GetTrackName(int TrackID)
		{
			if (TrackID == -1)
			{
				return "Не задан";
			}
            string ret = String.Empty;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select name from tracks where id='" + TrackID + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = res["name"].ToString();
                    }
                }
            }

            if (ret == "") ret = "Default";
            return ret;
        }

        #region скидочные карты и типы

        public IEnumerable<DiscountCardGroup> getCardsTypies()
        {
            return edb.DiscountCardGroups;
        }

        public bool editDiscountCard(DiscountCard someCard)
        {
            if (someCard.idOwner == -1)
            {
                someCard.idOwner = null;
            }

            edb.Entry(someCard).State = EntityState.Modified;
            edb.SaveChanges();

            updateBarcode(someCard);

            return true;
        }

        // так сделано, что в таблице пользователей не id карты, а её номер (barcode называется ;-). а в свойствах карты указывается id владельца. поэтому придётся обновить barcode
        private void updateBarcode(DiscountCard someCard)
        {
            // химия химическая
            #region если эта карта кому-то выдана, то сохраним barcode
            if (someCard.idOwner != null)
            {
                // но сначала заберём её у других
                IEnumerable<users> oldOwners = edb.users.Where(m =>
                    m.barcode == someCard.Number).Where(m => m.id != someCard.idOwner);

                foreach (users item in oldOwners)
                {
                    item.barcode = "";
                }

                edb.SaveChanges();

                users newOwner = edb.users.Find(someCard.idOwner);

                newOwner.barcode = someCard.Number;
                edb.SaveChanges();
            }
            #endregion

            #region если была ранее выдана, а теперь забрали, то очистим barcode
            if (someCard.idOwner == null)
            {
                IEnumerable<users> oldOwners = edb.users.Where(m =>
                    m.barcode == someCard.Number);

                foreach (users item in oldOwners)
                {
                    item.barcode = "";
                }

                edb.SaveChanges();
            }
            #endregion

        }



        public bool assignDiscountCard(DiscountCard someCard)
        {
            if (someCard.idOwner == -1)
            {
                someCard.idOwner = null;
            }

            if (edb.DiscountCards.Where(m => m.Number == someCard.Number).Count() > 0)
            {
                return false;
            }


            edb.DiscountCards.Add(someCard);

            edb.SaveChanges();

            updateBarcode(someCard);

            return true;
        }




        public IEnumerable<DiscountCard> getAllDiscountCards(string filter = "", string fieldName = "", string order = "id", bool direction = true)
        {
            IEnumerable<DiscountCard> result = null;

            if (connected)
            {
                if (filter.Length < 1)
                {
                    result = edb.DiscountCards.OrderBy(m => m.Number);

                }
                else
                {
                    result = (from dc in edb.DiscountCards
                              where dc.Number.Contains(filter) ||
                                  dc.owner.name.Contains(filter) ||
                                  dc.owner.nickname.Contains(filter) ||
                                  dc.owner.email.Contains(filter) ||
                                  dc.owner.tel.Contains(filter) ||
                                  dc.owner.surname.Contains(filter)
                              select dc).OrderBy(m => m.Number);
                }

            }

            return result;
        }

        public void addDiscountCardType(short percentOfDiscount)
        {
            DiscountCardGroup someGroup = new DiscountCardGroup
            {
                PercentOfDiscount = percentOfDiscount,
                IsDeleted = false
            };

            edb.DiscountCardGroups.Add(someGroup);
            edb.SaveChanges();

        }

        public void deleteDiscountCardType(int id)
        {
            DiscountCardGroup someGroup = edb.DiscountCardGroups.Find(id);
            someGroup.IsDeleted = !someGroup.IsDeleted;
            edb.SaveChanges();
        }

        public void blockCard(int id)
        {
            DiscountCard someCard = edb.DiscountCards.Find(id);

            if (someCard != null)
            {
                someCard.IsBlocked = true;
                edb.SaveChanges();
            }
        }

        public DiscountCard getCardByNumber(string number)
        {
            if (number.Length != 8)
            {
                return null;
            }

            return edb.DiscountCards.Where(m => m.Number == number).Take(1).SingleOrDefault();
        }

        public DiscountCard getCardByUserId(int userId)
        {
            return edb.DiscountCards.Where(m => m.idOwner == userId).Take(1).SingleOrDefault();
        }

        // если какой-то другой пилот в этом заезде уже использует эту карту, то повторно не разрешать её использовать
        public bool isCardUsedInRaceData(int idCard, int idRace, int idUser)
        {
            if (edb.race_data.Where(m =>
                m.id_discount_card == idCard).Where(m =>
                m.pilot_id != idUser).Where(m =>
                    m.race_id == idRace).Count() > 0)
            {
                return true;
            }

            return false;
        }

        #endregion

        public IEnumerable<users> getUsers()
        {
            return edb.users.Where(m => m.deleted != true).Where(m => m.banned != true);
        }

        /*
        public Dictionary<int, Dictionary<string, string>> getAllDiscountCards(string field = "", string fieldName = "", string order = "id", bool direction = true)
        {
            Dictionary<int, Dictionary<string, string>> result = new Dictionary<int, Dictionary<string, string>>();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from Discount_Card order by " + (order == null ? "id" : order) + (direction ? " asc" : " desc"), db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        int count = 0;
                        while (res.Read())
                        {
                            Dictionary<string, string> temp = new Dictionary<string, string>();

                            for (int i = 0; i < res.FieldCount; i++)
                            {
                                temp.Add(res.GetName(i), res.GetValue(i).ToString());
                            }

                            result.Add(count, temp);
                            count++;
                            temp = null;
                        }
                    }
                }
            }

            return result;
        }
        */
        // Получает количество ремонтов карта
        public int GetKartRepairs(string KartID, DateTime from, DateTime to)
        {

            int ret = 0;

            if (connected)
            {
				var query = $"select count(*) as c from messages where id_kart={KartID} and m_type = 1 and date BETWEEN '{from.ToSqlString()}' and '{to.ToSqlString()}'";
				using (SqlCommand cmd = new SqlCommand(query, db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Int32.Parse(res["c"].ToString());
                    }
                }
            }
            return ret;

        }

		internal void DropKartKmStats(int? kartId = null, DateTime? from = null, DateTime? to = null)
		{
			if (!connected)
			{
				return;
			}
			var query = $"UPDATE race_data SET car_id = null {(kartId.HasValue || (from.HasValue && to.HasValue)).ToStringIf("WHERE")} {kartId.HasValue.ToStringIf($"car_id = {kartId}")} {(kartId.HasValue && from.HasValue && to.HasValue).ToStringIf("AND")} {(from.HasValue && to.HasValue).ToStringIf($"created BETWEEN '{from.Value.AsDayStart().ToSqlString()}' and '{to.Value.AsDayEnd().ToSqlString()}'")}";
			using (var cmd = new SqlCommand(query, db))
			{
				cmd.ExecuteNonQueryHandled();
			}
		}

		// Добавляет пользователя программы
		public void AddProgramUser(string Login, string Password, string Stat, string Name, string Surname, string Barcode)
        {

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into program_users (login,password,created,stat,name,surname,barcode, modified, deleted) values ('" + Login + "','" + Password + "','" + getDate() + "','" + Stat + "','" + Name + "','" + Surname + "','" + Barcode + "', GETDATE(), 0)", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
        }

        // Изменяет данные оператора
        public void ChangeProgramUsers(string Login, string Password, string Stat, string ID, string Name, string Surname, string Barcode)
        {

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update program_users set name='" + Name + "', surname='" + Surname + "', login='" + Login + "', password= '" + Password + "', stat='" + Stat + "', barcode='" + Barcode + "' where id='" + ID + "'", db))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
        }

        // Удаляет оператора
        public void DelProgramUsers(string ID)
        {
            using (SqlCommand cmd = new SqlCommand("update program_users set deleted=1, stat=9 where id='" + ID + "'", db))
            {
                cmd.ExecuteNonQueryHandled();
            }
        }

        // Получает всех операторов
        public List<Hashtable> GetAllPgrogramUsers(int PU)
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select *  from program_users where deleted=0 and id!='" + PU.ToString() + "'", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }

            return ret;
        }

        // Проверка входа пользователя или получение пользователя
        public Hashtable GetProgramUser(string ID, bool Connect = false, string Login = "", string Password = "")
        {
			Hashtable ret = new Hashtable();

            if (connected)
            {
                string query;

                if (Connect)
                    query = "select * from program_users where login='" + Login + "' and password='" + Password + "' and deleted=0";
                else
                    query = "select * from program_users where deleted=0 and id='" + ID + "'";

                using (SqlCommand cmd = new SqlCommand(query, db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = ConvertResult(res);
            }

            return ret;
        }


        // Проверка входа пользователя или получение пользователя
        public Hashtable GetProgramUserBarCode(string BarCode)
        {
            Hashtable ret = new Hashtable();
            //   Hashtable ret = localSetts.GetProgramUserBarCode(BarCode);

            if (connected)
            {
                string query;

                query = "select * from program_users where deleted=0 and barcode='" + BarCode + "'";

                using (SqlCommand cmd = new SqlCommand(query, db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        if (res != null)
                        {
                            ret = ConvertResult(res);
                        }

            }

            return ret;
        }

        //Получает имя пользователя
        public string GetProgramUserName(string ID)
        {
            string ret = String.Empty;
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select login,name,surname from program_users where id='" + ID + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                    {
                        if (res["name"].ToString().Length == 0 || res["surname"].ToString().Length == 0)
                            ret = res["login"].ToString();
                        else
                            ret = res["surname"].ToString() + " " + res["name"].ToString();
                    }
            }
            return ret;
        }

        // Изменяет рейс у пилота
        public void ChangePilotRace(string MemberId, string RaceID)
        {
            DateTime startTime = DateTime.Now;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update race_data set race_id='" + RaceID + "' where id = '" + MemberId + "'", db2))
                    cmd.ExecuteNonQueryHandled();
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("ChangePilotRace", Logger.LogType.info, executionTime);
        }

        // Логин пользователя
        public void Login(string UserID)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into logins (user_id,stat, created) values ('"
                    + UserID + "',1, GETDATE())", db3))
                    cmd.ExecuteNonQueryHandled();
            }
        }

        // выход пользователя
        public void LogOut(string UserID)
        {
            if (UserID == "0")
            {
                return;
            }

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into logins (user_id,stat, created) values ('" + UserID + "',0, GETDATE())", db3))
                    cmd.ExecuteNonQueryHandled();
            }
        }

        // Залить топливо
        public void AddFuel(string KartID, string Fuel, string Comment = "")
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into fuel (car_id, fuel_value, sign, comment, created) values ('" + KartID + "','" + Fuel + "',0,'" + Comment + "', GETDATE())", db3))
                    cmd.ExecuteNonQueryHandled();
            }
        }

        // Слить топливо
        public void DelFuel(string KartID, string Fuel, string Comment = "")
        {

            if (KartID == "-1")
            {
                return;
            }

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("insert into fuel (car_id, fuel_value, sign, comment, created) values ('" + KartID + "','" + Fuel + "',1,'" + Comment + "', GETDATE())", db3))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
        }

        // Получает общее количество топлива в карте
        public string GetKartFuel(string KartID, DateTime? from = null, DateTime? to = null)
        {
            string ret = String.Empty;


            if (connected)
            {
				var query = $"select (sum(CASE WHEN(sign=0)THEN fuel_value else 0 end)-sum(case when (sign=1) then fuel_value else 0 end)) as fuel_value from fuel where car_id='{KartID}' {(from.HasValue && to.HasValue ? $"and created BETWEEN '{from.Value.AsDayStart().ToSqlString()}' and '{to.Value.AsDayEnd().ToSqlString()}'" : "")}";

				using (SqlCommand cmd = new SqlCommand(query, db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {

                            string temp = res[0].ToString();
                            if (temp.Length > 0)
                                ret = Math.Round(Convert.ToDouble(res[0]), 3).ToString();

                            else ret = "0";
                        }
                    }
                }
            }

            return ret;
        }

		internal void DropFuel(int? cartId = null, DateTime? from = null, DateTime? to = null)
		{
			if (!connected)
			{
				return;
			}

			var query = $"DELETE FROM fuel {(cartId.HasValue || (from.HasValue && to.HasValue)).ToStringIf("WHERE")} {cartId.HasValue.ToStringIf($"car_id = {cartId}")} {(cartId.HasValue && from.HasValue && to.HasValue).ToStringIf("AND")} {(from.HasValue && to.HasValue).ToStringIf($"created BETWEEN '{from.Value.AsDayStart().ToSqlString()}' and '{to.Value.AsDayEnd().ToSqlString()}'")}";
			using (var cmd = new SqlCommand(query, db))
			{
				cmd.ExecuteNonQueryHandled();
			}
		}


		// Получает имя пилота
		public string GetPilotName(string ID)
        {
            string ret = String.Empty;


            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select * from users where id='" + ID + "'", db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            ret = res["surname"] + " " + res["name"];
                        }
                    }
                }
            }

            return ret;
        }


        // Получает топливо за период
        public string GetKartFuel(string KartID, DateTime D1, DateTime D2, string Sign)
        {
            string ret = String.Empty;


            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select (sum(CASE WHEN(sign=0)THEN fuel_value else 0 end)-sum(case when (sign=1) then fuel_value else 0 end)) as fuel from fuel where car_id='"
                    + KartID + "' and sign='" + Sign + "' and created>='" +
                    datetimeConverter.toDateTimeString(datetimeConverter.toStartDateTime(D1)) +
                    "' and created<='" + datetimeConverter.toDateTimeString(datetimeConverter.toEndDateTime(D2)) +
                    "'", db))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {

                            string temp = res[0].ToString();
                            if (temp.Length > 0)
                                ret = Math.Round(Convert.ToDouble(res[0]), 3).ToString();

                            else ret = "0";
                        }
                    }
                }
            }

            return ret;
        }

        // Получает историю по заправкам за последний месяц
        public string GetKartFuelHistory(string KartID, DateTime? from = null, DateTime? to = null)
        {

            string ret = String.Empty;

            if (connected)
            {
				var query = $"select * from fuel where car_id='{KartID}' {(from.HasValue && to.HasValue ? $"and created BETWEEN '{from.Value.AsDayStart().ToSqlString()}' and '{to.Value.AsDayEnd().ToSqlString()}'" : "")} order by created desc";

				using (SqlCommand cmd = new SqlCommand(query, db3))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            if (res["sign"].ToString() == "0")

                                ret += res["created"].ToString() + "\n" + "Заправлено: " + res["fuel_value"] + " л" + (res["comment"].ToString().Length > 0 ? "\n" + res["comment"].ToString() : "") + "\n\r";
                            else
                                ret += res["created"].ToString() + "\n" + "Использовано: " + res["fuel_value"] + " л" + (res["comment"].ToString().Length > 0 ? "\n" + res["comment"].ToString() : "") + "\n\r";

                        }
                    }
                }
            }

            return ret;
        }

        // Добавляет новый тип сертификата
        public void AddCertificateType(string Name, string Nominal, string Cost, string TP)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into certificate_type ([name],nominal,cost,c_type, created, deleted) values ('" +
                    Name + "','" + Nominal + "','" + Cost + "','" + TP
                    + "', GETDATE(), 0)", db3))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
        }

        // редактирование сертификата
        public void ChangeSertificateType(string ID, string Name, string Nominal, string Cost, string type)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update certificate_type set [name]='" + Name + "', nominal = '" + Nominal + "', cost ='" + Cost + "',c_type='" + type + "' where id='" + ID + "'", db3))
                {
                    cmd.ExecuteNonQueryHandled();
                }
            }
        }

        // Получает все типы сертификатов
        public List<Hashtable> GetAllCertificateType(string TP, string filter)
        {

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select *  from certificate_type where deleted=0 and c_type='" + TP + "' " + filter, db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }

            return ret;
        }

        // Количество выданных сертификатов 
        public int GetCertificateTypeCount(string ID)
        {
            int ret = 1;

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select count(*) from certificates where c_id='" + ID + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = Convert.ToInt32(res[0]);
            }

            return ret;
        }

        // Количество сертификатов
        public int GetNextCertificateNum()
        {
            int ret = 1;

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select TOP 1 id from certificates order by id desc ", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = Convert.ToInt32(res[0]) + 1;
            }

            return ret;
        }

        // Удаляет тип сертификата
        public void DelCertificateType(string ID)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update certificate_type set deleted=1 where id='" + ID + "'", db3))
                    cmd.ExecuteNonQueryHandled();
            }
        }

        // Типы сертификатов загрузка
        public List<string> GetCertificatesType(string CT)
        {
            List<string> ret = new List<string>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select [name] from certificate_type where deleted=0 and c_type='" + CT + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    while (res.Read())
                        ret.Add(res["name"].ToString());
            }

            return ret;
        }

        // Получает ID сертификата
        public int GetCertificateTypeID(string Name)
        {
            int ret = 0;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select id from certificate_type where name='" + Name + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = Convert.ToInt32(res[0]);
            }

            return ret;
        }

        // Получает остаток гонок в сертификате
        public int GetCertificateRaceCount(string BarNumber)
        {
            int ret = 0;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) from certificate where bar_number='" + BarNumber + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = Convert.ToInt32(res[0]);
            }

            return ret;
        }

        // Получает количество сертификатов определенного типа
        public int GetCertificateCountFromType(string CID, bool OnlyActive = true)
        {
            int ret = 0;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) from certificates where c_id='" + CID + "' and active=" + (OnlyActive ? "1" : "0") + " ", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = Convert.ToInt32(res[0]);
            }

            return ret;
        }

        // Активирует сертификат 
        public void ActivateCertificate(string BarCode, string Stat)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update certificates set active='" + Stat + "' where bar_number='" + BarCode + "'", db3))
                    cmd.ExecuteNonQueryHandled();
            }
        }

        // Снимает один заезд у сертификата
        public void DelRaceFromCertificate(string BarCode, int Count)
        {
            if (Count == 0) ActivateCertificate(BarCode, "0");
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update certificates set count='" + Count.ToString() + "' where bar_number='" + BarCode + "'", db3))
                    cmd.ExecuteNonQueryHandled();
            }
        }

        // Получает Имя сертификата
        public string GetCertificateTypeName(string ID)
        {
            string ret = String.Empty;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select [name] from certificate_type where id='" + ID + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = res[0].ToString();
            }

            return ret;
        }

        // Получает все о типе сертификата
        public Hashtable GetCertificateType(string ID)
        {
            Hashtable ret = new Hashtable();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from certificate_type where id='" + ID + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = ConvertResult(res);
            }

            return ret;
        }

        // Загружает объект с базы
        public Dictionary<string, object> GetObject(string ObjectName, string id)
        {
            DateTime startTime = DateTime.Now;
            Dictionary<string, object> ret = new Dictionary<string, object>();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from " + ObjectName + " where id='" + id + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        for (int i = 0; i < res.FieldCount; i++)
                        {
                            ret.Add(res.GetName(i), res.GetValue(i));
                        }
            }

            TimeSpan executionTime = DateTime.Now - startTime;
            Logger.AddRecord("GetObject", Logger.LogType.info, executionTime);

            return ret;
        }

        // Добавление нового сертификата
        public void AddCertificate(string CID, string BNumber, int UID, DateTime Dend)
        {
            Hashtable CT = GetCertificateType(CID);

            string idUser = "null";

            if (UID > 0)
            {
                idUser = Convert.ToString(UID);
            }

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into certificates (c_id,bar_number,user_id,count,created,date_end, active, modified) values('" + CID + "','" + BNumber + "', " + idUser
                    + ",'" + (Convert.ToBoolean(CT["c_type"].ToString())
                    ? "1" : CT["nominal"].ToString()) + "','" + getDate() +
                    "','" + datetimeConverter.toDateTimeString(Dend) + "', 1, GETDATE())", db3))
                    cmd.ExecuteNonQueryHandled();
            }
        }

        // Получает все сертификаты
        public List<Hashtable> GetAllCertificates(string filter)
        {

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select *  from certificates where id > 0 " + filter, db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }

            return ret;
        }

        // Получает сертификат
        public Hashtable GetCertificate(string BarCode)
        {
            Hashtable ret = new Hashtable();

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("select * from certificates where bar_number='" + BarCode + "'", db2))
                using (SqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = ConvertResult(res);
            }

            return ret;
        }

        #region режимы заезда

        // добавить        
        public void AddRaceMode(string name, Int32 length)
        {
            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("insert into race_modes ([name], length, is_deleted) values('" + name + "','" + length.ToString() + "', 0)", db3))
                    cmd.ExecuteNonQueryHandled();
            }
        }

        // удалить      
        public bool DelRaceMode(Int16 id)
        {
            bool result = false;


            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("Delete from race_modes where id='" + id + "'", db))
                {
                    try
                    {
                        cmd.ExecuteNonQueryHandled();
                        result = true;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return result;
        }

        // пометить, как удалённый
        public void markRaceModeAsDeleted(Int16 id)
        {
            race_modes raceMode = edb.race_modes.Find(id);
            if (raceMode != null)
            {
                raceMode.is_deleted = !raceMode.is_deleted;
                edb.SaveChanges();
            }
        }

        // редактировать
        public bool EditRaceMode(Int16 id, string name, int length)
        {
            bool result = true;

            if (connected)
            {
                using (SqlCommand cmd = new SqlCommand("update race_modes SET name = '" + name + "', length='" + length + "' where id = '" + id + "'", db3))
                {
                    try
                    {
                        cmd.ExecuteNonQueryHandled();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        // список      
        public List<Hashtable> GetAllRaceModes(string filter)
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (SqlCommand cmd = new SqlCommand("select *  from race_modes where id > 0 " + filter + " order by length", db2))
                {
                    using (SqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }

            return ret;
        }

        #endregion

        #region бензин

        public IEnumerable<Petroleum> getPetroleum(DateTime startDate, DateTime endDate)
        {
            return edb.Petroleums.Where(m => m.Date >= startDate).Where(m => m.Date <= endDate);
        }

        public void petroleumSpent(Petroleum somePetroleum)
        {
            edb.Petroleums.Add(somePetroleum);
            edb.SaveChanges();
        }

        #endregion





        internal bool IsDoublePilot(int pilot_id, int RaceID)
        {
            bool returnValue = false;
            if (connected)
            {

                SqlCommand cmd =
                    new SqlCommand(
                        "select id from race_data where race_id='" + RaceID + "' and pilot_id = '" + pilot_id + "' ",
                        db2);
                var res = cmd.ExecuteScalar();


                if (res != null)
                {
                    returnValue = true;
                }

            }
            return returnValue;
        }
    }
    public class AbsoluteRecordOfRace
    {
        public string Pilot { get; set; }
        public DateTime Date { get; set; }
        public string RecordTime { get; set; }
    }

}
