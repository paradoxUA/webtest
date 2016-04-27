using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using Prokard_Timing.model;

namespace Prokard_Timing
{
    class testClass1
    {
                    private crazykartContainer edb;
        public static string connectionString = "";
        private SqlConnection db, db1, conn;
        public void testClass()
        {
            int idTrack = 9;
            int amountOfRecords = 20;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            connectionString = config.AppSettings.Settings["crazykartConnectionString"].Value;
            db = new SqlConnection(connectionString);
            db.Open();
            db1 = new SqlConnection(connectionString);
            db1.Open();

            conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand SqlCommand = conn.CreateCommand();

            SqlCommand.CommandText = "" +
                        " select race_data.*, users.id as user_id, users.name as username " +
                        " from race_data " +
                        " join races on races.id = race_data.race_id " +
                        " join users on race_data.pilot_id = users.id " +
                        " where races.track_id = @TRACK_ID ";
            SqlCommand.Parameters.AddWithValue("@TRACK_ID", idTrack);
            da.SelectCommand = SqlCommand;
            DataSet ds = new DataSet();

            conn.Open();
            da.Fill(ds);
            conn.Close();

            List<race_times> resObjects11 = new List<race_times>();

            List<int> listUsersInts = new List<int>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                race_data rdData = new race_data();
                rdData.pilot_id = Convert.ToInt32(row[2]);
                // rdData.car_id = Convert.ToInt32(res["car_id"]);
                rdData.id = Convert.ToInt32(row[0]);
                rdData.race_id = Convert.ToInt32(row[1]);
                rdData.created = Convert.ToDateTime(row[4]);

                using (SqlCommand newmCommand
                    = new SqlCommand(" select race_times.*,race_data.*, users.name " +
                     " from race_times " +
                     " join race_data on race_times.member_id = race_data.id " +
                     " left join users on users.id = race_data.pilot_id " +
                     " where race_times.seconds in( " +
                     " select min(race_times.seconds) " +
                     " from race_times " +
                     " join race_data on race_times.member_id = race_data.id " +
                     " where race_data.pilot_id = @USER_ID) " +
                     " and race_data.pilot_id = @USER_ID " +
                     "", db1))
                {
                    newmCommand.Parameters.AddWithValue("@USER_ID", row[2]);
                    if (listUsersInts.Contains(Convert.ToInt32(row[2])))
                    {
                        continue;
                    }
                    else
                    {
                        listUsersInts.Add(Convert.ToInt32(row[2]));
                    }
                    
                    Console.WriteLine(@"@USER_ID===" + row[2]);
                    using (SqlDataReader res1 = newmCommand.ExecuteReader())
                    {
                        while (res1.Read())
                        {
                            race_times rsTimes = new race_times();
                            rsTimes.id = Convert.ToInt32(res1[0]);
                            rsTimes.lap = Convert.ToInt32(res1[1]);
                            rsTimes.seconds = Convert.ToDecimal(res1[2]);
                            rsTimes.created = Convert.ToDateTime(res1[3]);
                            rsTimes.race_data = (race_data)(rdData);
                            resObjects11.Add(rsTimes);
                        }
                    }
                    //  Console.WriteLine(res["id"]);
                }

            }

            foreach (object resObject in resObjects11)
            {
                Console.WriteLine(resObject);
            }





            List<race_times> resObjects = new List<race_times>();
            using (SqlCommand cmd
                    = new SqlCommand("" +
                        " select race_data.*, users.id as user_id, users.name as username " +
                        " from race_data " +
                        " join races on races.id = race_data.race_id " +
                        " join users on race_data.pilot_id = users.id " +
                        " where races.track_id = @TRACK_ID ", db))
            {
                cmd.Parameters.AddWithValue("@TRACK_ID", idTrack);
                Console.WriteLine(@"@TRACK_ID===" + idTrack);
                using (SqlDataReader res = cmd.ExecuteReader())
                {
                    while (res.Read())
                    {
                        race_data rdData = new race_data();
                        rdData.pilot_id = Convert.ToInt32(res[2]);
                       // rdData.car_id = Convert.ToInt32(res["car_id"]);
                        rdData.id = Convert.ToInt32(res[0]);
                        rdData.race_id = Convert.ToInt32(res[1]);
                        rdData.created = Convert.ToDateTime(res[4]);
                        using (SqlCommand newmCommand
                            = new SqlCommand(" select race_times.*,race_data.*, users.name " +
                                             " from race_times " +
                                             " join race_data on race_times.member_id = race_data.id " +
                                             " left join users on users.id = race_data.pilot_id " +
                                             " where race_times.seconds in( " +
                                             " select min(race_times.seconds) " +
                                             " from race_times " +
                                             " join race_data on race_times.member_id = race_data.id " +
                                             " where race_data.pilot_id = @USER_ID) " +
                                             " and race_data.pilot_id = @USER_ID " +
                                             "", db1))
                        {
                            newmCommand.Parameters.AddWithValue("@USER_ID", res[2]);
                            Console.WriteLine(@"@USER_ID===" + res[2]);
                            if (listUsersInts.Contains(Convert.ToInt32(res[2])))
                            {
                                //break;
                            }
                            else
                            {
                                listUsersInts.Add(Convert.ToInt32(res[2]));
                            }
                            
                            using (SqlDataReader res1 = newmCommand.ExecuteReader())
                            {
                                while (res1.Read())
                                {
                                    race_times rsTimes = new race_times();
                                    rsTimes.id = Convert.ToInt32(res1[0]);
                                    rsTimes.lap = Convert.ToInt32(res1[1]);
                                    rsTimes.seconds = Convert.ToDecimal(res1[2]);
                                    rsTimes.created = Convert.ToDateTime(res1[3]);
                                    rsTimes.race_data = (race_data)(rdData);
                                    resObjects.Add(rsTimes);
                                }
                            }
                          //  Console.WriteLine(res["id"]);
                        }
                        //Console.WriteLine(res["id"]);
                        //  string val = res[0].ToString();
                        // ret = val == String.Empty ? 0.0 : Double.Parse(val);
                    }
                }
            }

            foreach (object resObject in resObjects)
            {
                Console.WriteLine(resObject);
            }



            


        }
    }
}
