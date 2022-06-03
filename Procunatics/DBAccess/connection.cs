using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Procunatics.DBAccess
{
    class connection
    {
        private MySqlConnection conn;
        private string server;
        private string user;
        private string pass;
        private string db;

        public connection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            db = "procunatics";
            user = "root";
            pass = "abcd1234!@#$";

            string connectionString;
            connectionString = "Data Source=" + server + ";Database=" + db + ";User Id =" + user + ";Password=" + pass + ";SSL Mode=0";
            conn = new MySqlConnection(connectionString);  

        }

        public bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        MessageBox.Show("Kenot connect to server. Admin help!!!!");
                        break;

                    case 1045:
                        MessageBox.Show("Aiyaa password got wrong meh. Try again lah");
                        break;
                }

                return false;
            }
        }
        
        public void close_conn()
        {
            this.conn.Close();
        }

        public MySqlConnection get_connection()
        {
            return this.conn;
        }
    }
}
