using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;

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
            var connectGood = false;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            try
            {
                connectionString = config.AppSettings.Settings["crazykartConnectionString"].Value;

                connectGood = false;
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
            catch (Exception e)
            {
                connectGood = false;
            }
            return connectGood;
        }

    }
}
