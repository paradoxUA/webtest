using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DateTimeExtensions;
using MySql.Data.MySqlClient;

namespace Prokard_Timing
{
    public class ProkardModelMySql
    {
        public string Server = String.Empty;
        public string Port = String.Empty;
        public string Uid = String.Empty;
        public string Password = String.Empty;
        public string Database = String.Empty;


        private MySqlConnection db;
        private MySqlConnection db2;
        private MySqlConnection db3;

        private bool connected;
        public string LastError;

        private string now_date(bool pastdate = false)
        {
            if (pastdate) return DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            else
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // Соединяемся с базой 
        public bool Connect()
        {
            string connection = "Server=" + Server + ";Port=" + Port + ";Uid=" + Uid + ";Password=" + Password + ";Database=" + Database + ";pooling=true;CharSet=cp1251";

            bool ret = true;
            try
            {
                db = new MySqlConnection(connection);
                db2 = new MySqlConnection(connection);
                db3 = new MySqlConnection(connection);

                db.Open();
                db2.Open();
                db3.Open();
            }
            catch (Exception e)
            {
                LastError = e.Message;
                ret = false;
            }
            connected = ret;

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("set names cp1251", db)) cmd.ExecuteNonQuery();
                using (MySqlCommand cmd = new MySqlCommand("set names cp1251", db2)) cmd.ExecuteNonQuery();
                using (MySqlCommand cmd = new MySqlCommand("set names cp1251", db3)) cmd.ExecuteNonQuery();
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

        // Получает рейс за выбранную дату с данным идентификатором 
        public void GetRace(string date, RaceClass Race)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from races where date(racedate)=date('" + date + "') and raceid='" + Race.ID + "'", db))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Race.Status = Convert.ToInt32(reader["stat"]);
                        Race.RaceID = Convert.ToInt32(reader["id"]);
                        Race.Created = reader["racedate"].ToString();
                        Race.TrackID = Convert.ToInt32(reader["track_id"]);
                        Race.TrackName = GetTrackName(reader["track_id"].ToString());
                        Race.Light_mode = Convert.ToInt32(reader["light_mode"]);
                    }
                    else
                    {
                        Race.Status = 0;
                        Race.RaceID = -1;
                        Race.Created = "";
                        Race.TrackID = 0;
                        Race.TrackName = "Default";
                        Race.Light_mode = 0;
                    }
                }
            }
        }

        // Получает абсолютный рекорд трассы
        public string GetRecord(int TrackID)
        {
            string ret = String.Empty;
            List<Hashtable> br = GetBestResults(TrackID, "", false, false);


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

                ret = br[index]["nickname"] + " (" + br[index]["surname"].ToString() + " " + br[index]["name"].ToString() + ")  " + Convert.ToDateTime(br[index]["racedate"]).ToString("dd MMMM yyyy") + ":  " + br[index]["seconds"].ToString() + "  сек";
            }
            else ret = "-";
            return ret;
        }

        // Изменяет режим без картов
        public void ChangeRaceLightMode(string LM, string RaceID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update races set light_mode='" + LM + "' where id='" + RaceID + "'", db))
                    cmd.ExecuteNonQuery();
            }
        }

        // Получает карт по транспондеру
        public Hashtable GetKartFromTransponder(string Transponder)
        {
            Hashtable ret = new Hashtable();
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from karts where transponder='" + Transponder + "'", db))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = ConvertResult(res);
            }
            return ret;

        }

        // Добавляет поле в произвол
        public void AddKartNoRace(string Transponder, string RaceID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into noracekart (transponder, race_id) values ('" + Transponder + "','" + RaceID + "')", db))
                    cmd.ExecuteNonQuery();
            }

        }

        // Изменяет режим для участника
        public void ChangeMemberLightMode(string LM, string MID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update race_data set light_mode='" + LM + "' where id='" + MID + "'", db))
                    cmd.ExecuteNonQuery();
            }
        }

        // Создает рейс
        public void CreateRace(string date, string ID, string TrackID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into races (racedate,raceid,created,stat,track_id) values ('" + date + "','" + ID + "','" + now_date() + "','1','" + TrackID + "')", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Добавляет пилота в рейс
        public bool AddPilotToRace(int pilot_id, int race_id, int month = 0, int reserv = 0, int car_id = 0)
        {
            bool ret = true;
            if (race_id > 0)
            {
                if (connected)
                {
                    string query = String.Empty;
                    if (month == 1)
                    {
                        query = "insert into race_data (race_id,pilot_id,car_id,created,reserv,monthrace,race_month_date) values ('" + race_id.ToString() + "','" + pilot_id.ToString() + "','" + car_id.ToString() + "','" + now_date() + "','" + reserv.ToString() + "','" + month.ToString() + "','" + DateTime.Now.Last(DayOfWeek.Sunday).ToString("yyyy-MM-dd") + "')";
                    }
                    else
                        query = "insert into race_data (race_id,pilot_id,car_id,created,reserv,monthrace) values ('" + race_id.ToString() + "','" + pilot_id.ToString() + "','" + car_id.ToString() + "','" + now_date() + "','" + reserv.ToString() + "','" + month.ToString() + "')";

                    using (MySqlCommand cmd = new MySqlCommand(query, db))
                        cmd.ExecuteNonQuery();

                }
                else ret = false;
            }
            else ret = false;
            return ret;
        }

        // Создает нового пилота
        public void AddNewPilot(Hashtable data)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into users (name,surname,nickname,gender,birthday,created,modified,email,tel,gr,barcode) values ('" + data["name"] + "','" + data["surname"] + "','" + data["nickname"] + "','" + data["gender"] + "','" + data["birthday"] + "','" + now_date() + "','" + now_date() + "','" + data["email"] + "','" + data["tel"] + "','" + data["group"] + "','" + data["barcode"] + "')", db))
                    cmd.ExecuteNonQuery();
            }
        }

        // Изменяет данные пилота
        public void ChangePilot(Hashtable Data, string PilotID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update users set name='" + Data["name"] + "', surname='" + Data["surname"] + "',gender='" + Data["gender"] + "',birthday='" + Data["birthday"] + "',modified='" + now_date() + "',nickname='" + Data["nickname"] + "',email='" + Data["email"] + "',tel='" + Data["tel"] + "',gr='" + Data["group"] + "',barcode='" + Data["barcode"] + "' where id='" + PilotID + "'", db))
                    cmd.ExecuteNonQuery();
            }
        }

        // Получает ассоциативный массив всех пилотов
        public Hashtable GetAllPilots(string filter, PageLister page)
        {
            if (page != null) page.OnUpdate = true;
            Hashtable ret = new Hashtable();
            if (connected)
            {
                if (page != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select count(*)as c from users " + (filter != "" ? " where id>0 " + filter : ""), db))
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            int Rows = Convert.ToInt32(res["c"].ToString());

                            if (page.RowsMax != Rows || page.OnChange)
                            {
                                page.RowsMax = Rows;
                                page.PagesCount = Convert.ToInt32(Rows / page.PageSize);
                                page.FillPageNumbers();
                                page.OnChange = false;
                            }
                            page.setPageListerButtonsEnability();


                        }
                    }


                    filter += page.Filter;
                }
                using (MySqlCommand cmd = new MySqlCommand("select * from users " + (filter != "" ? " where id>0 " + filter : ""), db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

            if (page != null) page.OnUpdate = false;
            return ret;
        }

        // Получает все карты
        public Hashtable GetAllKarts(int tp = 0)
        {
            Hashtable ret = new Hashtable();
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from karts" + (tp == 1 ? " where repair=0 and wait=0" : ""), db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
            return ret;
        }

        // Получает всю информацию о данном пилоте
        public Hashtable GetPilot(int id)
        {
            Hashtable row = new Hashtable();
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from users where id='" + id + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            row = ConvertResult(res);
                        }
                    }
                }
            }
            return row;

        }

        // Получает детальную информацию про рейс
        public Hashtable GetDetalRaceInfo(int RaceID)
        {
            Hashtable ret = new Hashtable();
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select rd.id,rd.race_id,rd.light_mode,rd.pilot_id,u.name,u.nickname,rd.car_id from race_data rd,users u where u.id=rd.pilot_id and race_id = '" + RaceID + "'", db3))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
            return ret;
        }

        //что это?? 
        public List<LapResult> GetFinallyRaceInfo(int RaceID)
        {
            List<LapResult> Data = new List<LapResult>();

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select rd.id, rd.race_id, k.number,rd.light_mode, rd.pilot_id, u.name, u.nickname, rd.car_id from karts k, race_data rd,users u where k.id=rd.car_id and u.id=rd.pilot_id and race_id = '" + RaceID + "'", db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        int Lap, BestLap;
                        double TimeLap, BestTime, AverageTime;


                        while (res.Read())
                        {
                            LapResult Temp;
                            Temp.Times = new List<double>();

                            // Блок заполнения данных о пилоте
                            Temp.MemberID = Convert.ToInt32(res["id"]);
                            Temp.PilotID = Convert.ToInt32(res["pilot_id"]);
                            Temp.RaceID = Convert.ToInt32(res["race_id"]);
                            Temp.Light = res["light_mode"].ToString() == "0" ? false : true;
                            Temp.CarNum = res["number"].ToString();
                            Temp.PilotName = res["name"].ToString();
                            Temp.PilotNickName = res["nickname"].ToString();


                            Lap = BestLap = 0;
                            TimeLap = AverageTime = 0;

                            BestTime = Double.MaxValue;
                            // Блок заполнения данных о кругах
                            using (MySqlCommand cmd2 = new MySqlCommand("select lap,seconds from race_times where member_id='" + res["id"].ToString() + "' order by lap,seconds", db2))
                            using (MySqlDataReader res2 = cmd2.ExecuteReader())
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

            return Data;
        }



        // Получает список пилотов данного рейса
        public Hashtable GetRacePilots(int raceid)
        {
            Hashtable ret = new Hashtable();
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from race_data where race_id='" + raceid + "'", db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
                            ret.Add(rows, row);
                            rows++;
                        }
                    }
                }
            }
            return ret;
        }

        // Получает количество пилотов рейса
        public int GetRacePilotsCount(string raceid)
        {
            int ret = 0;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from race_data where race_id='" + raceid + "'", db3))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToInt32(res["c"]);
                    }
                }
            }

            return ret;


        }

        // Получает информацию о карте
        public Hashtable GetKart(int id)
        {

            Hashtable row = new Hashtable();

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from karts where id='" + id + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            row = ConvertResult(res);
                    }
                }
            }
            return row;

        }

        // Получает ID карта по его номеру
        public int GetKartID(string Num)
        {
            int ret = -1;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select id from karts where number='" + Num + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToInt32(res["id"]);
                    }
                }
            }

            return ret;
        }

        // Обнулить карт у данного пилота
        public void DelKartFromRace(string BaseID, string Num = null)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update race_data set car_id=0 where id='" + BaseID + "' ", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Добавляет карт пилоту
        public void AddKartToRace(string BaseID, string Name, bool Light)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update race_data set car_id='" + GetKartID(Name) + (Light ? "', light_mode=1" : "',light_mode=0") + " where id='" + BaseID + "'", db))
                    cmd.ExecuteNonQuery();

            }

        }

        // Получает все выбранные карты данного рейса
        public List<string> GetRaceKarts(int id)
        {
            List<string> ret = new List<string>();
            string s = String.Empty;
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select car_id from race_data where race_id='" + id + "' and light_mode=0", db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
            return ret;
        }

        // Есть ли такой пилот

        public bool GetNewPilot(string name, string surname, string nickname)
        {

            bool ret = false;
            string query = String.Empty;

            if (name.Length > 0) query = " and name='" + name + "' ";
            if (surname.Length > 0) query = " and surname='" + surname + "' ";
            if (nickname.Length > 0) query = " and nickname='" + nickname + "' ";

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from users where id > 0 " + query, db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable row = new Hashtable();
                        if (res.Read())
                        {
                            ret = true;
                        }
                    }
                }
            }


            return ret;
        }

        // Получает лучшее вермя пилота
        public List<string> GetPilotBestResult(string PilotID)
        {
            List<string> ret = new List<string>();
            if (connected)
            {
                string query = "select	rd.id, rd.pilot_id,	rt.seconds, rd.race_id,r.racedate, 	u.name,u.nickname, u.surname,u.email, u.tel, r.track_id,r.created,t.name as track_name	from users u, race_times rt, race_data rd, races r, tracks t where 	rt.seconds = replace((select min(replace(rt.seconds,',','.')*1.0) from 	race_times rt, 			race_data rd 	where	rd.pilot_id=u.id and rd.light_mode = 0	and not isnull(rt.seconds)  and rt.seconds > 0	and rt.member_id=rd.id),'.',',')	and rd.id = rt.member_id	and rd.pilot_id='" + PilotID + "' and u.id  = rd.pilot_id	and r.id  = rd.race_id	and t.id  = r.track_id group by u.id order by seconds limit 1";


                /*select tr.name, min(replace(t.seconds,',','.')*1.0) as seconds from tracks tr, race_times t, race_data d, races r where tr.id=r.track_id and r.id=d.race_id and d.light_mode=0 and d.pilot_id='" + PilotID + "' and replace(t.seconds,',','.')=(select min(replace(t.seconds,',','.')*1.0) as min from race_times where member_id=d.id)*/

                using (MySqlCommand cmd = new MySqlCommand(query, db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
            return ret;
        }

        // Банит или активирует пользователя
        public void PilotsActive(string PilotID, string Stat, string Date, string Message)
        {

            int InsertID = -1;
            string Mess = String.Empty;
            if (Message.Length > 0)
                Mess = Message;
            else
            {
                if (Stat == "1") Mess = "Добавлен в бан"; else Mess = "Снят с бана";

            }
            AddMessage(PilotID.ToString(), "1", "3", Date, Mess);
            InsertID = GetLastInsertID("Messages");


            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update users set message_id='" + InsertID.ToString() + "', banned='" + Stat + "', date_banned = '" + Date + "' where id='" + PilotID + "'", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Преобразовывает результат в ассоциативный массив
        private Hashtable ConvertResult(MySqlDataReader row)
        {
            Hashtable res = new Hashtable();
            for (int i = 0; i < row.FieldCount; i++)
            {
                res[row.GetName(i)] = row.GetValue(i).ToString();
            }
            return res;
        }

        // Добавляет запись временной метки рейса
        public void AddTimeStamp(int RaceMember, int Lap, string Seconds)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into race_times (member_id,lap,seconds) values ('" + RaceMember.ToString() + "','" + Lap.ToString() + "','" + Seconds + "')", db))
                    cmd.ExecuteNonQuery();

            }

        }

        // Изменяет статус резерв или нет
        public void ChangePilotReservStatus(string RaceDataId, int Status = 0)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update race_data set reserv='" + Status.ToString() + "' where id='" + RaceDataId + "'", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Удаляет пилота с рейса
        public void DelPilotFromRace(string MemberID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("Delete from race_data where id='" + MemberID + "'", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Удаляет пилота
        public void DelPilot(string UserID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("Delete from users where id='" + UserID + "'", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Удаляет группу
        public void DelGroup(string GroupID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("Delete from groups where id='" + GroupID + "'", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Добавляет карт
        public void AddKart(string Name, string Number, string Transponder)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into karts (name,number,transponder,created) values ('" + Name + "','" + Number + "','" + Transponder + "','" + now_date() + "')", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Обновляет Карт
        public void ChangeKart(string ID, string Name, string Number, string Transponder)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update karts set name='" + Name + "', number = '" + Number + "', transponder = '" + Transponder + "' where id='" + ID + "'", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Обновляет Группу
        public void ChangeGroup(string Name, string Sale, string GroupID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update groups set name='" + Name + "', sale = '" + Sale + "' where id='" + GroupID + "'", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Удаляет Карт
        public void DelKart(string ID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from karts where id='" + ID + "'", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Ремонт картов
        public void KartRepair(string KartID, string Repair, string Date, string Message)
        {

            int CarID = GetKartID(KartID);
            int InsertID = -1;
            string MMM = String.Empty;
            if (Message.Length == 0)
            {

                if (Repair == "1") MMM = "Поставлен на ремонт"; else MMM = "Снят с ремонта";
            }
            else MMM = Message;
            string M_tp = "1";
            if (Repair == "1") M_tp = "1"; else M_tp = "4";
            AddMessage(CarID.ToString(), "0", M_tp, Date, MMM);
            InsertID = GetLastInsertID("Messages");

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update karts set message_id='" + InsertID.ToString() + "', repair='" + Repair + "' where id='" + CarID.ToString() + "'", db))
                    cmd.ExecuteNonQuery();

            }
        }

        // Получает статистику по карту
        public Hashtable GetKartStatistic(string KartID, double Deflen)
        {
            Hashtable ret = new Hashtable();

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select d.id, r.track_id from race_data d, races r where d.car_id='" + KartID + "' and r.id=d.race_id", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Получает длинну трека
        public double GetTrackLength(string TrackID)
        {
            double ret = -1;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select length from tracks where id='" + TrackID + "'", db3))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToDouble(res["length"]);
                    }
                }
            }

            return ret;
        }

        // Добавляет сообщение
        public void AddMessage(string Obj_id, string Obj_t, string Message_t, string Date, string Message, string Subject = "")
        {
            /* Object Types
             *          0 - Kart
             *          1 - Pilot
             *          2 - Event
             * Message Types
             *          0 - Simple Message
             *          1 - Repair 
             *          2 - Wait
             *          3 - banned
             *          4 - UnRepair
             */


            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into messages (o_id,o_type,m_type,message,date,created,subject) values ('" + Obj_id + "','" + Obj_t + "','" + Message_t + "','" + Message + "','" + Date + "','" + now_date() + "','" + Subject + "')", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }

        }

        // Получает последний ID сообщения
        private int GetLastInsertID(string Table)
        {
            int ret = -1;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select id from " + Table + " order by id desc limit 1", db3))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = Convert.ToInt32(res["id"]);
            }

            return ret;

        }

        // Получает текст сообщения по ID
        public Hashtable GetMessageFromID(string ID)
        {

            Hashtable ret = new Hashtable();

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from messages where id='" + ID + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = ConvertResult(res);
                    }
                }
            }

            return ret;
        }

        // Получает статистику по пользователям
        public Hashtable GetUsersStatistic()
        {
            Hashtable ret = new Hashtable();

            if (connected)
            {

                // Новых пилотов
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from users", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["users_all"] = res[0]; else ret["users_all"] = 0;

                // Забанненых пилотов
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from users where banned=1", db))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["users_bann"] = res[0]; else ret["users_bann"] = 0;

                // Сумма в кассе
                ret["cash"] = GetCashFromUsers();
            }
            return ret;
        }

        // Получает статистику за день
        public Hashtable GetDayStatistic(string Day)
        {
            Hashtable ret = new Hashtable();

            if (connected)
            {
                // Заездов создано
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from races where date(created)=date('" + Day + "')", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["races"] = res[0]; else ret["races"] = 0;

                // Заездов на этот день
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from races where date(racedate)=date('" + Day + "')", db))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["races_day"] = res[0]; else ret["races_day"] = 0;

                // Заездов завершенных
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from races where date(created)=date('" + Day + "') and stat=2", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["races_end"] = res[0]; else ret["races_end"] = 0;

                // Заездов проваленных
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from races where date(created)=date('" + Day + "') and stat=1", db))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["races_fail"] = res[0]; else ret["races_fail"] = 0;

                // Новых пилотов
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from users where date(created)=date('" + Day + "')", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["users_new"] = res[0]; else ret["users_new"] = 0;

                // Забанненых пилотов
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from users where date(date_banned)=date('" + Day + "')", db))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["users_bann"] = res[0]; else ret["users_bann"] = 0;

                // Продано билетов
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from race_data where date(created)=date('" + Day + "')", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read()) ret["tickets"] = res[0]; else ret["tickets"] = 0;

                // Сумма в кассе
                ret["cash"] = GetCashFromCassa(Day, false, false, true, false);

                // Сумма у пользователей
                ret["virtual"] = GetCashFromUsers(Day);
            }
            return ret;
        }

        // Получает все сообщения по o_id
        public string GetAllMessagesFromID(string ID)
        {

            string ret = String.Empty;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select created,message from messages where o_id='" + ID + "' order by created desc", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret += res["created"].ToString() + "\r";
                            ret += res["message"].ToString() + "\n\r";
                        }
                    }
                }
            }

            return ret;
        }

        // Cохраняет настройки
        public void SaveSettings(Hashtable sett)
        {
            if (connected)
            {
                if (sett.Count > 0)
                    using (MySqlCommand cmd = new MySqlCommand("delete from settings", db))
                    {
                        cmd.ExecuteNonQuery();
                    }

                foreach (DictionaryEntry d in sett)
                {

                    using (MySqlCommand cmd = new MySqlCommand("insert into settings (name,val) values ('" + d.Key.ToString() + "','" + d.Value.ToString() + "')", db))
                    {
                        cmd.ExecuteNonQuery();
                    }

                }
            }
        }

        // Настройки по умолчанию
        private Hashtable DefaultSettings()
        {
            Hashtable sett = new Hashtable();

            // Закладка Общие настройки
            sett["time_start"] = 9;
            sett["time_end"] = 22;
            sett["time_wrap"] = 10;
            sett["wrap_pos"] = 1;
            sett["show_events"] = "True";
            sett["enter_password"] = "False";
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
            sett["default_track"] = 0;
            sett["race_time"] = 10;
            sett["warm_subtract"] = 0;
            sett["warm_time"] = false;

            //Закладка временных скидок
            sett["racesale"] = false;
            sett["sale_onelap"] = 35;
            sett["sale_half"] = 55;


            return sett;
        }

        //Удаляет данные заезда
        public string DelRaceDataTimes(string RaceID, string PilotID)
        {

            string ret = "Ошибка выполнения";
            string MemberID = "0";
            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select id from race_data where pilot_id='" + PilotID + "' and race_id='" + RaceID + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from race_times where member_id='" + MemberID + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = "Удалено записей: " + res["c"].ToString();
                    }
                }

                using (MySqlCommand cmd = new MySqlCommand("delete from race_times where member_id='" + MemberID + "'", db2))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            return ret;

        }

        // Загружает настройки
        public Hashtable LoadSettings()
        {
            Hashtable ret = new Hashtable();
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select name,val from settings", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    while (res.Read())
                        ret.Add(res["name"], res["val"]);
            }

            if (ret.Count == 0) ret = DefaultSettings();
            return ret;
        }

        // Получает максимальное количество картов
        public int GetMaxKarts()
        {
            int ret = 1;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from karts where repair=0 and wait=0", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToInt32(res["c"]);
                    }
                }
            }

            return ret;

        }

        // Получает количество участий в рейсах TP - Билеты или рейсы
        public int GetPilotRaceCount(string UserID, bool TP)
        {
            int ret = 0;

            if (connected)
            {
                string query = String.Empty;

                if (TP)
                    query = "select count(*) as c from races r, race_data d where d.pilot_id='" + UserID + "' and r.id= d.race_id and r.stat=2";
                else
                    query = "select count(*) as c from race_data d where d.pilot_id='" + UserID + "'";
                using (MySqlCommand cmd = new MySqlCommand(query, db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Convert.ToInt32(res["c"]);
                    }
                }
            }

            return ret;

        }

        // Сохраняет цены
        public void SavePrices(int Week, string[] Prices)
        {

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("delete from prices where week='" + Week.ToString() + "'; insert into prices (week,d1,d2,d3,d4,d5,d6,d7) values ('" + Week.ToString() + "','" + Prices[1] + "','" + Prices[2] + "','" + Prices[3] + "','" + Prices[4] + "','" + Prices[5] + "','" + Prices[6] + "','" + Prices[7] + "')", db))
                {
                    cmd.ExecuteNonQuery();
                }


            }

        }

        // Загружает цены
        public string[] GetPrices(int Week = 1)
        {
            string[] Prices = new string[8];

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from prices where week='" + Week.ToString() + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            for (int i = 1; i <= 7; i++)
                                Prices[i] = res["d" + i.ToString()].ToString();
                        }
                    }
                }
            }
            return Prices;
        }

        // Получает дату и время старта рейса
        public DateTime GetRaceDateTime(int RaceID)
        {

            DateTime dat = new DateTime();

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select racedate from races where id='" + RaceID + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            dat = Convert.ToDateTime(res[0]);
                        }
                    }
                }
            }
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
                AddMessage(KartID, "0", "2", now_date(), Message);
                InsertID = GetLastInsertID("Messages");


                using (MySqlCommand cmd = new MySqlCommand("update karts set message_id='" + InsertID.ToString() + "', wait='" + Stat + "' where id='" + KartID + "'", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Получает все события
        public List<Hashtable> GetAllEvents(int tp = 0)
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {
                string whr = String.Empty;

                switch (tp)
                {
                    case 0: whr = ""; break;
                    case 1: whr = " and date(created) >= date(now())"; break;
                    case 2: whr = " and date(created) < date(now())"; break;
                }

                using (MySqlCommand cmd = new MySqlCommand("select id,created,message,subject from messages where o_id=0 and o_type=2 and m_type=0 " + whr + " order by created asc", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Получает логины пользователей
        public List<Hashtable> GetUserLogins(string Date)
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select * from logins where date(created)=date('" + Date + "') order by created desc", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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


        // Получает произвольный детект
        public List<Hashtable> GetTransponderDetect(string Date)
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select * from noracekart where date(created)=date('" + Date + "') order by created desc", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Получает участников месячной гонки
        public List<Hashtable> GetMonthRaceMembers(string Date)
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select d.id,d.pilot_id,u.name, u.surname, u.email,u.tel, r.track_id ,t.name as track_name,r.created from users u, races r, tracks t, race_data d where d.light_mode=0 and t.id=r.track_id and u.id=d.pilot_id and r.id = d.race_id and d.monthrace=1 and d.race_month_date = '" + Date + "' group by d.id,d.pilot_id", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

            return ret;
        }

        // Получает журнал заездов
        public List<Hashtable> GetRaceJurnal(string Date, string Date2, int FLT = -1)
        {
            List<Hashtable> ret = new List<Hashtable>();
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
                using (MySqlCommand cmd = new MySqlCommand("select * from jurnal where (" + wr + ") and created between '" + Date + " 00:00:00' and '" + Date2 + " 23:59:59' ", db))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    while (res.Read())
                        ret.Add(ConvertResult(res));
            return ret;

        }

        public List<Hashtable> GetBestResultsFromDateRange(string d1, string d2, bool uniq, string TrackID)
        {

            List<Hashtable> ret = new List<Hashtable>();


            if (connected)
            {
                /*string query = "select   d.id,d.pilot_id,u.name,r.racedate,u.nickname, u.surname, u.email,u.tel, r.track_id,t.name as track_name,r.created, (select min(replace(seconds,',','.')*1.0) from race_times where member_id=d.id) as seconds from users u, races r, tracks t, race_data d where d.light_mode=0 and t.id=r.track_id and u.id=d.pilot_id and  (select min(replace(seconds,',','.')) from race_times where member_id=d.id) and r.id = d.race_id and r.track_id='"+TrackID+"' and r.racedate between '" + d1 + "' and '" + d2 + "' " + (uniq ? " group by d.pilot_id " : "") + " order by seconds";
                */

                string query = "select	rd.id, rd.pilot_id,	rt.seconds, rd.race_id,r.racedate, 	u.name,u.nickname, u.surname,u.email, u.tel, r.track_id,r.created,t.name as track_name	from users u, race_times rt, race_data rd, races r, tracks t where 	rt.seconds = replace((select min(replace(rt.seconds,',','.')*1.0) from 	race_times rt, 			race_data rd 	where	rd.pilot_id=u.id and rd.light_mode = 0	and not isnull(rt.seconds)  and rt.seconds > 0	and rt.member_id=rd.id),'.',',')	and rd.id = rt.member_id	and u.id  = rd.pilot_id	and r.id  = rd.race_id	and t.id  = r.track_id and r.track_id='" + TrackID.ToString() + "' and r.racedate between '" + d1 + "' and '" + d2 + "' group by u.id order by seconds";


                using (MySqlCommand cmd = new MySqlCommand(query, db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Получает лучшие результаты
        public List<Hashtable> GetBestResults(int TrackID, string Date, bool OnDay, bool uniq)
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {
                string query = String.Empty;
                string where = String.Empty;
                if (Date != "")
                {
                    if (OnDay)
                        where = "and date(r.racedate)=date('" + Date + "')  group by u.id  order by seconds limit 40";

                    else where = "and date(r.racedate)>=date('" + DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + "')  group by u.id  order by seconds limit 40";

                }
                else where = " group by u.id order by seconds limit 40";

                if (TrackID < 0)

                    query = "select	rd.id, rd.pilot_id,	rt.seconds, rd.race_id,r.racedate, 	u.name,u.nickname, u.surname,u.email, u.tel, r.track_id,r.created,t.name as track_name	from users u, race_times rt, race_data rd, races r, tracks t where 	rt.seconds = replace((select min(replace(rt.seconds,',','.')*1.0) from 	race_times rt, 			race_data rd 	where	rd.pilot_id=u.id and rd.light_mode = 0	and not isnull(rt.seconds)  and rt.seconds > 0	and rt.member_id=rd.id),'.',',')	and rd.id = rt.member_id	and u.id  = rd.pilot_id	and r.id  = rd.race_id	and t.id  = r.track_id " + where;
                else

                    query = "select	rd.id, rd.pilot_id,	rt.seconds, rd.race_id,r.racedate, 	u.name,u.nickname, u.surname,u.email, u.tel, r.track_id,r.created,t.name as track_name	from users u, race_times rt, race_data rd, races r, tracks t where 	rt.seconds = replace((select min(replace(rt.seconds,',','.')*1.0) from 	race_times rt, 			race_data rd 	where	rd.pilot_id=u.id and rd.light_mode = 0	and not isnull(rt.seconds)  and rt.seconds > 0	and rt.member_id=rd.id),'.',',')	and rd.id = rt.member_id	and u.id  = rd.pilot_id	and r.id  = rd.race_id	and t.id  = r.track_id and r.track_id='" + TrackID.ToString() + "' " + where;


                using (MySqlCommand cmd = new MySqlCommand(query, db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Получает счета пользователей
        public List<Hashtable> GetUsersBallans()
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select u.id,u.name,u.surname,u.email, u.tel from users u", db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable temp;
                        while (res.Read())
                        {
                            temp = ConvertResult(res);
                            temp["sum"] = GetUserBallans(temp["id"].ToString());


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
            string ret = String.Empty;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select min(replace(seconds,',','.')*1.0) from race_times where member_id='" + MemberID + "'", db))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = res[0].ToString();
            }

            return ret;
        }

        // Обновляет сообщение
        public void UpdateMessage(string ID, string Date, string Subject, string Message)
        {

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("update messages set subject = '" + Subject + "', message = '" + Message + "', created = '" + Date + "' where id='" + ID + "'", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }

        }

        // Выполняет запрос
        public void ExecuteQuery(string Query)
        {

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand(Query, db))
                {
                    cmd.ExecuteNonQuery();
                }
            }

        }


        // Удаляет сообщение
        public void DelMessage(string ID)
        {
            if (connected)
                using (MySqlCommand cmd = new MySqlCommand("delete from messages where id='" + ID + "'", db))
                    cmd.ExecuteNonQuery();
        }

        // Получает баланс пользователя
        public double GetUserBallans(string ID)
        {
            double ret = 0.0;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select (sum(CASE WHEN(sign=0)THEN sum else 0 end)-sum(case when (sign=1) then sum else 0 end)) as summa from user_cash where user_id='" + ID + "' and not isnull(sum)", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Добавляет запись в журнал
        public int AddToJurnal(string DocNum, string UserID, string RaceID, string Comment)
        {
            int ret = 0;
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into jurnal (tp,user_id,comment,race_id) values ('" + DocNum + "','" + UserID + "','" + Comment + "','" + RaceID + "')", db))
                {
                    cmd.ExecuteNonQuery();
                    Thread.Sleep(200);
                }
            }

            ret = GetLastInsertID("jurnal");
            return ret;
        }

        // Добавляет запись в кассу
        private void AddToCassa(string DocID, string Sum, string Sign, bool pastdate = false)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into cassa (doc_id,sum,sign,date) values ('" + DocID + "','" + Sum.Replace(",", ".") + "','" + Sign + "','" + now_date(pastdate) + "')", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }

        }

        // Добавляет запись в UserCash
        private void AddToUserCash(string DocID, string UserID, string Sum, string Sign)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into user_cash (doc_id,user_id,sum,sign) values ('" + DocID + "','" + UserID + "','" + Sum.Replace(",", ".") + "','" + Sign + "')", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Журналирование кассовых событий
        public void Jurnal_Cassa(string DocNum, string UserID, string RaceID, string Sum, string Sign, string Comments, bool pastdate = false)
        {
            int Doc_id = AddToJurnal(DocNum, UserID, RaceID, Comments);
            AddToCassa(Doc_id.ToString(), Sum, Sign, pastdate);
        }

        // Операция коррекции баланса пользователя
        public void Jurnal_UserCash(string DocNum, string UserID, string Sum, string Sign, string Comments, string RaceID)
        {
            int Doc_id = AddToJurnal(DocNum, UserID, RaceID, Comments);
            AddToUserCash(Doc_id.ToString(), UserID, Sum, Sign);
        }

        // Операция добавление денег на счет пользователя
        public void Jurnal_AddToUserCash(string DocNum, string UserID, string Sum, string Sign, string Comments)
        {
            int Doc_id = AddToJurnal(DocNum, UserID, "0", Comments);
            AddToCassa(Doc_id.ToString(), Sum, Sign);
            AddToUserCash(Doc_id.ToString(), UserID, Sum, Sign);
        }

        // Получает сумму в кассе
        public double GetCassaSumm(string D1, string D2, int TP, int sign)
        {

            double ret = 0;

            if (connected)
            {

                string query = String.Empty;
                string q1 = "(select sum(c.sum*1.0) from jurnal j, cassa c where c.doc_id=j.id and j.created between '" + D1 + " 00:00:00' and '" + D2 + " 23:59:59' and c.sign='" + sign.ToString() + "' order by j.id)";
                string q2 = "(select sum(u.sum*1.0) from jurnal j, user_cash u where u.doc_id = j.id and j.created between '" + D1 + " 00:00:00' and '" + D2 + " 23:59:59' and u.sign='" + sign.ToString() + "' order by j.id)";

                switch (TP)
                {
                    case 1: query = q1; break;
                    case 2: query = q2; break;

                }

                using (MySqlCommand cmd = new MySqlCommand(query, db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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


            return ret;
        }

        // Получает данные кассы за период
        public List<Hashtable> GetCassaReport(string Date, int tp, string Date2, PageLister page)
        {

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                string filter = String.Empty;
                if (page != null)
                {

                    string query1 = String.Empty;
                    string q11 = "(select count(*)as c from jurnal j, cassa c where c.doc_id=j.id and j.created between '" + Date + " 00:00:00' and '" + Date2 + " 23:59:59' order by j.id)";
                    string q22 = "(select count(*)as c from jurnal j, user_cash u where u.doc_id = j.id and j.created between '" + Date + " 00:00:00' and '" + Date2 + " 23:59:59' order by j.id)";

                    switch (tp)
                    {
                        case 1: query1 = q11; break;
                        case 2: query1 = q22; break;
                        case 3: query1 = q11 + " union " + q22; break;
                    }




                    using (MySqlCommand cmd = new MySqlCommand(query1, db3))
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            int Rows = Convert.ToInt32(res["c"].ToString());

                            if (page.RowsMax != Rows || page.OnChange)
                            {
                                page.RowsMax = Rows;
                                page.PagesCount = Convert.ToInt32(Rows / page.PageSize);
                                page.FillPageNumbers();
                                page.OnChange = false;
                            }
                            page.setPageListerButtonsEnability();


                        }
                    }


                    filter += page.Filter;
                }



                string query = String.Empty;
                string q1 = "(select j.id,j.created as date,j.comment,j.user_id,j.tp,j.race_id, c.sum, c.sign from jurnal j, cassa c where c.doc_id=j.id and j.created between '" + Date + " 00:00:00' and '" + Date2 + " 23:59:59'  order by j.id)";
                string q2 = "(select j.id,j.created as date,j.comment,j.user_id,j.tp,j.race_id, u.sum, u.sign from jurnal j, user_cash u where u.doc_id = j.id and j.created between '" + Date + " 00:00:00' and '" + Date2 + " 23:59:59'  order by j.id)";

                switch (tp)
                {
                    case 1: query = q1; break;
                    case 2: query = q2; break;
                    case 3: query = q1 + " union " + q2 + " order by id"; break;
                }

                using (MySqlCommand cmd = new MySqlCommand(query + filter, db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            ret.Add(ConvertResult(res));
                        }
                    }
                }
            }
            if (page != null) page.OnUpdate = false;

            return ret;
        }

        // Получает весь список трасс
        public List<Hashtable> GetAllTracks()
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select * from tracks order by created", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Получает весь список групп
        public List<Hashtable> GetAllGroups()
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select * from groups order by created", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Получает всю статистику по картам
        public List<Hashtable> GetAllKartsStatistic(double Deflen, string D1, string D2)
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select * from karts", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        Hashtable temp = new Hashtable();
                        while (res.Read())
                        {
                            temp = ConvertResult(res);
                            temp["repairs"] = GetKartRepairs(temp["id"].ToString());


                            using (MySqlCommand cmd2 = new MySqlCommand("select d.id, r.track_id from race_data d, races r where d.car_id='" + temp["id"].ToString() + "' and r.id=d.race_id and r.stat=2 and date(r.racedate)>=date('" + D1 + "') and date(r.racedate)<=date('" + D2 + "') ", db))
                            {
                                using (MySqlDataReader res2 = cmd2.ExecuteReader())
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

            return ret;
        }

        // Получает количество кругов
        public int GetMemberLapsFromRace(string MemberID)
        {
            int ret = 0;


            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select count(*) from race_times where member_id='" + MemberID + "'", db3))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                        {
                            ret = Convert.ToInt32(res[0].ToString());
                        }
                    }
                }
            }

            return ret;
        }

        // Получает всю сумму в кассе
        public string GetCashFromCassa(string Date = "", bool AllTime = false, bool NotTransferDoc = false, bool AllSumm = true, bool NoTransf = true)
        {
            string ret = String.Empty;
            string temp;

            if (connected)
            {
                string where = String.Empty;
                if (Date.Length > 2)
                {

                    if (AllSumm)
                        where = " and j.tp!=7 ";
                }
                //"+(!NoTransf?"and (j.tp != 7 and j.tp!= 15)":"") + "
                using (MySqlCommand cmd = new MySqlCommand("select (sum(CASE WHEN(sign=0)THEN sum*1.0 else 0 end)-sum(case when (sign=1) then sum*1.0 else 0 end)) as summa from cassa c, jurnal j where j.id=doc_id " + (!NoTransf ? "and (j.tp != 7 and j.tp!= 15)" : "") + "  and not isnull(c.sum)" + (Date.Length > 2 ? " and date(c.date)" + (AllTime ? "<=" : "=") + "date('" + Date + "')" : ""), db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Получает всю сумму на счетах пользователя
        public string GetCashFromUsers(string Date = "")
        {
            string ret = String.Empty;
            string temp = String.Empty;

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select (sum(CASE WHEN(sign=0)THEN sum else 0 end)-sum(case when (sign=1) then sum else 0 end)) as summa from user_cash where not isnull(sum)" + (Date.Length > 2 ? " and date(created)=date('" + Date + "')" : ""), db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Добавляет трек в базу
        public void AddTrack(string Name, string Length, string FileName)
        {

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into tracks (name,length,file) values ('" + Name + "','" + Length + "','" + FileName + "')", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Добавляет группу
        public void AddGroup(string Name, string Sale)
        {

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into groups (name,sale,created) values ('" + Name + "','" + Sale + "','" + now_date() + "')", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Удаляет трек
        public void DelTrack(string TrackID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("delete from tracks where id='" + TrackID + "'", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Обновляет данные о треке
        public void ChangeTrack(string TrackID, string Name, string Length, string FileName)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update tracks set name='" + Name + "', length = '" + Length + "', file='" + FileName + "' where id='" + TrackID + "'", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Завершает заезд
        public void StopRace(string RaceID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update races set stat = '2' where id='" + RaceID + "'", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Загружает все группы
        public List<string> GetAllGroupsName()
        {

            List<string> ret = new List<string>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select name from groups order by id", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

                using (MySqlCommand cmd = new MySqlCommand("select id from groups where name='" + Name + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from users where gr='" + GroupID + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("select name from groups where id='" + GroupID + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("select sale from groups where id='" + GroupID + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Double.Parse(res["sale"].ToString());
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

                using (MySqlCommand cmd = new MySqlCommand("select id from tracks where name='" + Name + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Int32.Parse(res["id"].ToString());
                    }
                }
            }
            return ret;
        }

        // Получает имя трека по ID
        public string GetTrackName(string TrackID)
        {
            string ret = String.Empty;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select name from tracks where id='" + TrackID + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = res["name"].ToString();
                    }
                }
            }

            if (ret == "") ret = "Default";
            return ret;
        }

        public Dictionary<int, Dictionary<string, string>> getAllDiscountCards(string field = "", string fieldName = "", string order = "id", bool direction = true)
        {
            Dictionary<int, Dictionary<string, string>> result = new Dictionary<int, Dictionary<string, string>>();

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from Discount_Card order by " + (order == null ? "id" : order) + (direction ? " asc" : " desc"), db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

        // Получает количество ремонтов карта
        public int GetKartRepairs(string KartID)
        {

            int ret = 0;

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select count(*) as c from messages where o_id='" + KartID + "' and o_type=0 and m_type = 1", db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        if (res.Read())
                            ret = Int32.Parse(res["c"].ToString());
                    }
                }
            }
            return ret;

        }

        // Добавляет пользователя программы
        public void AddProgramUser(string Login, string Password, string Stat, string Name, string Surname, string Barcode)
        {

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into program_users (login,password,created,stat,name,surname,barcode) values ('" + Login + "','" + Password + "','" + now_date() + "','" + Stat + "','" + Name + "','" + Surname + "','" + Barcode + "')", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Изменяет данные пользователя
        public void ChangeProgramUsers(string Login, string Password, string Stat, string ID, string Name, string Surname, string Barcode)
        {

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update program_users set name='" + Name + "', surname='" + Surname + "', login='" + Login + "', password= '" + Password + "', stat='" + Stat + "', barcode='" + Barcode + "' where id='" + ID + "'", db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Удаляет пользователя
        public void DelProgramUsers(string ID)
        {
            using (MySqlCommand cmd = new MySqlCommand("update program_users set deleted=1, stat=9 where id='" + ID + "'", db))
            {
                cmd.ExecuteNonQuery();
            }
        }

        // Получает всех пользователей
        public List<Hashtable> GetAllPgrogramUsers(int PU)
        {
            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select *  from program_users where deleted=0 and id!='" + PU.ToString() + "'", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

                using (MySqlCommand cmd = new MySqlCommand(query, db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = ConvertResult(res);
            }

            return ret;
        }


        // Проверка входа пользователя или получение пользователя
        public Hashtable GetProgramUserBarCode(string BarCode)
        {

            Hashtable ret = new Hashtable();

            if (connected)
            {
                string query;

                query = "select * from program_users where deleted=0 and barcode='" + BarCode + "'";

                using (MySqlCommand cmd = new MySqlCommand(query, db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = ConvertResult(res);
            }

            return ret;
        }
        //Получает имя пользователя
        public string GetProgramUserName(string ID)
        {
            string ret = String.Empty;
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select login,name,surname from program_users where id='" + ID + "'", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
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
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update race_data set race_id='" + RaceID + "' where id = '" + MemberId + "'", db2))
                    cmd.ExecuteNonQuery();
            }
        }

        // Логин пользователя
        public void Login(string UserID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into logins (user_id,stat) values ('" + UserID + "',1)", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // выход пользователя
        public void LogOut(string UserID)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into logins (user_id,stat) values ('" + UserID + "',0)", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // Залить топливо
        public void AddFuel(string KartID, string Fuel, string Comment = "")
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into fuel (car_id, fuel, sign, comment) values ('" + KartID + "','" + Fuel + "',0,'" + Comment + "')", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // Слить топливо
        public void DelFuel(string KartID, string Fuel, string Comment = "")
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into fuel (car_id, fuel, sign, comment) values ('" + KartID + "','" + Fuel + "',1,'" + Comment + "')", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // Получает общее количество топлива в карте
        public string GetKartFuel(string KartID)
        {
            string ret = String.Empty;


            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select (sum(CASE WHEN(sign=0)THEN fuel else 0 end)-sum(case when (sign=1) then fuel else 0 end)) as fuel from fuel where car_id='" + KartID + "'", db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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


        // Получает имя пилота
        public string GetPilotName(string ID)
        {
            string ret = String.Empty;


            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select * from users where id='" + ID + "'", db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
        public string GetKartFuel(string KartID, string D1, string D2, string Sign)
        {
            string ret = String.Empty;


            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select (sum(CASE WHEN(sign=0)THEN fuel else 0 end)-sum(case when (sign=1) then fuel else 0 end)) as fuel from fuel where car_id='" + KartID + "' and sign='" + Sign + "' and date(created)>=date('" + D1 + "') and date(created)<=date('" + D2 + "')", db))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
        public string GetKartFuelHistory(string KartID)
        {

            string ret = String.Empty;

            if (connected)
            {
                string DS = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");

                using (MySqlCommand cmd = new MySqlCommand("select * from fuel where car_id='" + KartID + "' and date(created) >= date('" + DS + "') order by created desc", db3))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            if (res["sign"].ToString() == "0")

                                ret += res["created"].ToString() + "\n" + "Заправлено: " + res["fuel"] + " л" + (res["comment"].ToString().Length > 0 ? "\n" + res["comment"].ToString() : "") + "\n\r";
                            else
                                ret += res["created"].ToString() + "\n" + "Использовано: " + res["fuel"] + " л" + (res["comment"].ToString().Length > 0 ? "\n" + res["comment"].ToString() : "") + "\n\r";

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
                using (MySqlCommand cmd = new MySqlCommand("insert into certificate_type (name,nominal,cost,c_type) value ('" + Name + "','" + Nominal + "','" + Cost + "','" + TP + "')", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // Добавляет новый тип сертификата
        public void ChangeSertificateType(string ID, string Name, string Nominal, string Cost, string TP)
        {
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update certificate_type set name='" + Name + "', nominal = '" + Nominal + "', cost ='" + Cost + "',c_type='" + TP + "' where id='" + ID + "'", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // Получает все типы сертификатов
        public List<Hashtable> GetAllCertificateType(string TP, string filter)
        {

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select *  from certificate_type where deleted=0 and c_type='" + TP + "' " + filter, db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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

                using (MySqlCommand cmd = new MySqlCommand("select count(*) from certificate where c_id='" + ID + "'", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
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

                using (MySqlCommand cmd = new MySqlCommand("select id from certificate order by id desc limit 1", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("update certificate_type set deleted=1 where id='" + ID + "'", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // Типы сертификатов загрузка
        public List<string> GetCertificatesType(string CT)
        {
            List<string> ret = new List<string>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select name from certificate_type where deleted=0 and c_type='" + CT + "'", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("select id from certificate_type where name='" + Name + "'", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("select count from certificate where bar_number='" + BarNumber + "'", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("select count(*) from certificate where c_id='" + CID + "' and active=" + (OnlyActive ? "1" : "0") + " ", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("update certificate set active='" + Stat + "' where bar_number='" + BarCode + "'", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // Снимает один заезд у сертификата
        public void DelRaceFromCertificate(string BarCode, int Count)
        {
            if (Count == 0) ActivateCertificate(BarCode, "0");
            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update certificate set count='" + Count.ToString() + "' where bar_number='" + BarCode + "'", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // Получает Имя сертификата
        public string GetCertificateTypeName(string ID)
        {
            string ret = String.Empty;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select name from certificate_type where id='" + ID + "'", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("select * from certificate_type where id='" + ID + "'", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = ConvertResult(res);
            }

            return ret;
        }

        // Загружает объект с базы
        public Dictionary<string, object> GetObject(string ObjectName, string id)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from " + ObjectName + " where id='" + id + "'", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        for (int i = 0; i < res.FieldCount; i++)
                        {
                            ret.Add(res.GetName(i),res.GetValue(i));
                        }
            }

            return ret;
        }

        // Добавление нового сертификата
        public void AddCertificate(string CID, string BNumber, string UID, string Dend)
        {
            Hashtable CT = GetCertificateType(CID);

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into certificate (c_id,bar_number,user_id,count,created,date_end) values('" + CID + "','" + BNumber + "','" + UID + "','" + (Convert.ToBoolean(CT["c_type"].ToString()) ? "1" : CT["nominal"].ToString()) + "','" + now_date() + "','" + Dend + "')", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // Получает все сертификаты
        public List<Hashtable> GetAllCertificates(string filter)
        {

            List<Hashtable> ret = new List<Hashtable>();

            if (connected)
            {

                using (MySqlCommand cmd = new MySqlCommand("select *  from certificate where id > 0 " + filter, db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("select * from certificate where bar_number='" + BarCode + "'", db2))
                using (MySqlDataReader res = cmd.ExecuteReader())
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
                using (MySqlCommand cmd = new MySqlCommand("insert into race_modes (name, length) values('" + name + "','" + length.ToString() + "')", db3))
                    cmd.ExecuteNonQuery();
            }
        }

        // удалить      
        public bool DelRaceMode(Int16 id)
        {
            bool result = false;


            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("Delete from race_modes where id='" + id + "'", db))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                       
                    }
                }
            }

            return result;
        }

        // редактировать
        public bool EditRaceMode(Int16 id, string name, int length)
        {
            bool result = true;

            if (connected)
            {
                using (MySqlCommand cmd = new MySqlCommand("update race_modes SET name = '" + name + "', length='" + length + "' where id = '" + id + "'", db3))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
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

                using (MySqlCommand cmd = new MySqlCommand("select *  from race_modes where id > 0 " + filter + " order by length", db2))
                {
                    using (MySqlDataReader res = cmd.ExecuteReader())
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
        



    }
}
