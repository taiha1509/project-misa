﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace convertData
{
    class DBSQLServerUtils
    {

        public static SqlConnection GetDBConnection(string datasource, string database, string username, string password)
        {
            //
            // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
            //
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }

        public static SqlConnection GetDBConnection()
        {
            string dataSource = "DATABASE";
            dataSource += "\\";
            dataSource += "RDDATABASE";
            string database = "IPInfo";
            string username = "sa";
            string password = "12345678@Abc";

            return DBSQLServerUtils.GetDBConnection(dataSource, database, username, password);
        }


    }
}