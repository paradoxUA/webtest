using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Prokard_Timing
{
    class checkDb
    {
        private static string connectionString = "";
        public checkDb()
        {
        }

        public static bool ConnectGood()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            connectionString = config.AppSettings.Settings["crazykartConnectionString"].Value;

            var connectGood = false;
            var db = new SqlConnection(connectionString);
            try
            {
                db.Open();
                connectGood = true;
            }
            catch (Exception e)
            {
            }
            return connectGood;
        }

    }
}
