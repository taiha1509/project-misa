using convertData.Mongo;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace convertData
{
    class Transfer
    {
        public static void Main()
        {
            new Data();

            SqlConnection conn = DBSQLServerUtils.GetDBConnection();


            conn.Open();

            MongoDBUtils mongoutils = new MongoDBUtils("mongodb://localhost:27017");

            int numofrecords = getTotalRows(conn);
            int p = numofrecords / 1000;
            int q = numofrecords % 1000;

            for (int i = 0; i < 1000; i += 1000)
            {
                getIP(conn, i, i + 1000);

                while (Data.data.Count > 0)
                {
                    mongoutils.insertData("fortest", "info", Data.data[0]);
                    Data.data.RemoveAt(0);
                }
            }

            getIP(conn, p * 1000, numofrecords);



            Console.WriteLine("cpmpleted");

            conn.Close();

            

        }

        public static int getTotalRows(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();

            // Liên hợp Command với Connection.
            cmd.Connection = conn;
            string query = "select count(ID) from dbo.IPInfo";

            cmd.CommandText = query;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                return Convert.ToInt32(reader.GetValue(0));
            }
        }

        public static void getIP(SqlConnection conn, int beginAt, int endAt)
        {
            

            // Tạo một đối tượng Command.
            SqlCommand cmd = new SqlCommand();

            // Liên hợp Command với Connection.
            cmd.Connection = conn;
            string query = "select * from dbo.IPInfo where ID >= '" + beginAt + "'" + "and ID < '" + endAt + "'";

            cmd.CommandText = query;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Model model = new Model();
                        int ID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ID")));
                        model.ID = ID;


                        //Console.WriteLine(ipfrom);
                        byte[] b1 = (byte[])reader.GetValue(reader.GetOrdinal("IPFrom"));
                        
                        model.IPFrom = b1;
                        //Console.WriteLine(IPFrom);

                        //Byte[] IPTo = (Byte[])reader.GetValue(reader.GetOrdinal("IPTo"));
                        //model.IPTo = IPTo;

                        byte[] b2 = (byte[])reader.GetValue(reader.GetOrdinal("IPTo"));
                        
                        model.IPTo = b2;
                        //Console.WriteLine(IPTo);

                        int IPType = Convert.ToInt16(reader.GetValue(reader.GetOrdinal("IPType")));
                        model.IPType = IPType;

                        String Country = Convert.ToString(reader.GetValue(reader.GetOrdinal("Country")));
                        model.Country = Country;

                        String StateProvince = Convert.ToString(reader.GetValue(reader.GetOrdinal("StateProvince")));
                        model.StateProvince = StateProvince;

                        String CityDistrict = Convert.ToString(reader.GetValue(reader.GetOrdinal("CityDistrict")));
                        model.CityDistrict = CityDistrict;

                        String Latitude = Convert.ToString(reader.GetValue(reader.GetOrdinal("Latitude")));
                        model.Latitude = Latitude;

                        String Longtitude = Convert.ToString(reader.GetValue(reader.GetOrdinal("Longtitude")));
                        model.Longtitude = Longtitude;

                        String TimeZoneOffset = Convert.ToString(reader.GetValue(reader.GetOrdinal("TimeZoneOffset")));
                        model.TimeZoneOffset = TimeZoneOffset;

                        String TimeZoneName = Convert.ToString(reader.GetValue(reader.GetOrdinal("TimeZoneName")));
                        model.TimeZoneName = TimeZoneName;

                        String ISPName = Convert.ToString(reader.GetValue(reader.GetOrdinal("ISPName")));
                        model.ISPName = ISPName;

                        String ConnectionType = Convert.ToString(reader.GetValue(reader.GetOrdinal("ConnectionType")));
                        model.ConnectionType = ConnectionType;

                        String OUName = Convert.ToString(reader.GetValue(reader.GetOrdinal("OUName")));
                        model.OUName = OUName;

                        Data.data.Add(model);

                        Console.WriteLine(ID);

                    }
                }
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
