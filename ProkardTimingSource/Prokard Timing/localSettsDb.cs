using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using Finisar.SQLite;

namespace Prokard_Timing
{
    public class localSetts
    {

       // private string localConn = @"Data Source=|DataDirectory|\ASID.sdf";
        static SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=localSetts.sqlite;Version=3;");
        static SQLiteDataAdapter localAdapter = new SQLiteDataAdapter();
       // static DataSet DSuserslocal = new DataSet();

        internal static DataTable GetAdmins()
        {
        //    if(m_dbConnection.ConnectionString.)
            try
            {
                DataTable dt = new DataTable();
              //  List<Hashtable> res = new List<Hashtable>();
                if (m_dbConnection.State == ConnectionState.Closed) { m_dbConnection.Open(); }
                //m_dbConnection.CreateCommand();
                // m_dbConnection.Database.SelectMany()
                localAdapter.SelectCommand = new SQLiteCommand("SELECT * FROM program_users where deleted = 0", m_dbConnection);
                //  SQLiteDataReader reader = localAdapter.SelectCommand.ExecuteReader();
                //while (reader.Read())
                // {
                // res.Add(reader);
                // }
                localAdapter.Fill(dt);
                //  foreach (DataRow row in dt.AsEnumerable())
                //   {
                //       Hashtable tab = new Hashtable();
                //   }
                if (m_dbConnection.State == ConnectionState.Open) { m_dbConnection.Close(); }
                return dt;

                //  DSuserslocal
                // SQLiteCommandBuilder = new SQLiteCommandBuilder()
                //m_dbConnection.CreateCommand();
            }
            catch (Exception exp)
            {
                return new DataTable();
            }
            

        }



        internal static void addSysUser(string Login, string Password, string Stat, string Name, string Surname, string Barcode)
        {
            if (m_dbConnection.State == ConnectionState.Closed) { m_dbConnection.Open();}
            
            using (SQLiteCommand cmd = new SQLiteCommand("insert into program_users (login,password,created,stat,name,surname,barcode, modified, deleted)" +
                                                         " values ('" + Login + "','" + Password + "', DATETIME('now'), '" + Stat + "','" + Name + "','" + Surname + "','" + Barcode + "', DATETIME('now'), 0)", m_dbConnection))
            {
                cmd.ExecuteNonQuery();
            }
            if (m_dbConnection.State == ConnectionState.Open) { m_dbConnection.Close(); }
        }

        internal static void updateSysUser(string Login, string Password, string Stat, string ID, string Name, string Surname, string Barcode)
        {
            if (m_dbConnection.State == ConnectionState.Closed) { m_dbConnection.Open();}

            using (SQLiteCommand cmd = new SQLiteCommand("update program_users set name='" + Name + "', surname='" + Surname + "', login='" + Login + "', password= '" + Password + "', stat='" + Stat + "', barcode='" + Barcode + "' where id='" + ID + "'", m_dbConnection))
            {
                cmd.ExecuteNonQuery();
            }
            if (m_dbConnection.State == ConnectionState.Open) { m_dbConnection.Close(); }
        }

        internal static Hashtable GetProgramUserBarCode(string BarCode)
        {
            if (m_dbConnection.State == ConnectionState.Closed) { m_dbConnection.Open();}

            Hashtable ret = new Hashtable();
            using (SQLiteCommand cmd = new SQLiteCommand("select * from program_users where deleted=0 and barcode='" + BarCode + "'", m_dbConnection))
            {
                using (SQLiteDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                    {
                        if (m_dbConnection.State == ConnectionState.Open) { m_dbConnection.Close(); }
                        ret = ConvertResult(res);
                    }
            }
            return ret;
        }

        internal static void delUser(string ID)
        {
            if (m_dbConnection.State == ConnectionState.Closed) { m_dbConnection.Open();}

            using (SQLiteCommand cmd = new SQLiteCommand("update program_users set deleted=1, stat=9 where id='" + ID + "'", m_dbConnection))
            {
                cmd.ExecuteNonQuery();
            }

        }

        internal static Hashtable GetProgramUser(string ID, bool Connect = false, string Login = "", string Password = "")
        {

            Hashtable ret = new Hashtable();

            if (m_dbConnection.State == ConnectionState.Closed) { m_dbConnection.Open();}
            if (ProkardModel.connected)
            {
                string query;

                if (Connect)
                    query = "select * from program_users where login='" + Login + "' and password='" + Password + "' and deleted=0";
                else
                    query = "select * from program_users where deleted=0 and id='" + ID + "'";

                using (SQLiteCommand cmd = new SQLiteCommand(query, m_dbConnection))
                using (SQLiteDataReader res = cmd.ExecuteReader())
                    if (res.Read())
                        ret = ConvertResult(res);
            }

            return ret;
        }

        internal static Hashtable ConvertResult(SQLiteDataReader row)
        {
            Hashtable res = new Hashtable();
            for (int i = 0; i < row.FieldCount; i++)
            {
                res[row.GetName(i)] = row.GetValue(i).ToString();
            }
            return res;
        }

    }
}
