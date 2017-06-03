using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace etud03._06._2017
{

    class database
    {
        string str = "Data Source=desktop-5vnkcbq;Initial Catalog=hotel2;Integrated Security=True;";
        SqlConnection conn;

        public database()
        {
            SqlConnection connection = new SqlConnection(str);
            this.conn = connection;

        }

        public void connect()
        {
            try
            {
                conn.Open();
                MessageBox.Show("Test");
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message);
            }
        }

        public void insert(string tableName, Dictionary<string, string> datas)
        {
            // INSERT INTO tablename (name, surname, phone) Values(@name,@surname,@phone)
            StringBuilder query = new StringBuilder();
            StringBuilder colums = new StringBuilder();
            StringBuilder values = new StringBuilder();

            foreach (var data in datas.Keys)
            {
                colums.Append(data);
                values.Append("@" + data);

                if (data != datas.Last().Key)
                {
                    colums.Append(',');
                    values.Append(',');
                }
            }

            query.Append(String.Format("INSERT INTO {0} ({1}) VALUES({2})", tableName, colums, values));
            //MessageBox.Show(query.ToString());
            using (conn)
            {
                conn.Open();

                SqlCommand command = new SqlCommand(query.ToString(), conn);

                foreach (var data in datas)
                {
                    command.Parameters.AddWithValue("@" + data.Key, data.Value);
                }

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("inserted");
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }

            }
        }

        public void delete(string tableName, Dictionary<string, string> whereData)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder wherePart = new StringBuilder();

            foreach (var data in whereData.Keys)
            {
                wherePart.Append(data+"=@"+data);
                if (data != whereData.Last().Key)
                {
                    wherePart.Append(" AND ");
                }

            }

            query.Append(String.Format("DELETE FROM {0} WHERE {1}", tableName, wherePart));
            //MessageBox.Show(query.ToString());

            using (conn)
            {
                conn.Open();

                SqlCommand command = new SqlCommand(query.ToString(), conn);

                foreach (var data in whereData)
                {
                    command.Parameters.AddWithValue("@" + data.Key, data.Value);
                }

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("deleted");
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }

            }
        }

        public void update(string tableName, Dictionary<string, string> datas, Dictionary<string, string> whereData)
        {
            //update table set(name=@name, surname=@surnam, phone=@phone) where id=@id
            StringBuilder query = new StringBuilder();
            StringBuilder columValue = new StringBuilder();
            StringBuilder wherePart = new StringBuilder();

            foreach (var data in datas.Keys)
            {
                columValue.Append(data+"=@"+data);

                if (data != datas.Last().Key)
                {
                    columValue.Append(',');                  
                }
            }

            foreach (var data in whereData.Keys)
            {
                wherePart.Append(data + "=@" + data);

                if (data != whereData.Last().Key)
                {
                    wherePart.Append(" AND ");
                }
            }
            query.Append(string.Format("update {0} set {1} where {2}", tableName, columValue, wherePart));
            using (conn)
            {
                conn.Open();

                SqlCommand command = new SqlCommand(query.ToString(), conn);

                foreach (var data in datas)
                {
                    command.Parameters.AddWithValue("@"+data.Key, data.Value);
                }
                foreach (var data in whereData)
                {
                    command.Parameters.AddWithValue("@"+data.Key,data.Value);
                }
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("updated");
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }

            }
        }
    }
}
