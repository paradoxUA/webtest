using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Security.Cryptography;
using System.Configuration;
namespace CrazyKartActivation
{
    class ProgramActivation
    {

        private string GetActivateString()
        {
            string ret = String.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT maxclockspeed, datawidth, name, manufacturer FROM Win32_Processor");

            ManagementObjectCollection objCol = searcher.Get();

            foreach (ManagementObject mgtObject in objCol)
            {
               // ret += (mgtObject["maxclockspeed"].ToString() + Environment.NewLine);
                ret += (mgtObject["datawidth"].ToString() + Environment.NewLine);
                ret += (mgtObject["name"].ToString() + Environment.NewLine);
                ret += (mgtObject["manufacturer"].ToString() + Environment.NewLine);
            }

            ret += System.Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
            return Encrypt(ret);
        }

        public int KeyIsActive()
        {
            string key = GetActivateString();
            int ret = -1;
            string tkey = String.Empty;
            string path = Environment.CurrentDirectory + "\\crazykart.key";
            
            if (System.IO.File.Exists(path))
            {
                try
                {
                       System.IO.StreamReader sr = new System.IO.StreamReader(path);
                        string input;
                        do
                        {
                            input = sr.ReadLine();
                            if (input != "")
                                tkey = input;
                        } while (sr.Peek() != -1);
                        sr.Close();
                  
                }
                finally
                {
                    if (tkey == key) ret = 1; else ret = 2;
                }

            }
            else ret = 0;

            // ret = 0 - Файл не найден
            // ret = 1 - Файл найден и ключ верный
            // ret = 2 - Файл найден и ключ неверный
            return ret;
        }

        public void SaveKey()
        {
            string path = Environment.CurrentDirectory + "\\crazykart.key";
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(path);
                sw.WriteLine(GetActivateString());
                sw.Flush();
                sw.Close();
            }
            finally
            { }
        }

        private string Encrypt(string toEncrypt, bool useHashing = true)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = "yt83MJls993s1mldds1KKGWknLjhsGGCC";
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}
