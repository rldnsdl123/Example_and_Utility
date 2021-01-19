using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageSQL
{
    public class DB
    {
        private SqlConnectionStringBuilder _Builder;
        SqlConnection _Conn;
        SqlCommand _cmd;

        public DB()
        {
            _Builder = new SqlConnectionStringBuilder();
            _Conn = new SqlConnection();
            _cmd = new SqlCommand();
        }
        public eConnectState Connect(string userId, string password, string initialCatalog, string dataSource)
        {
            try
            {
                _Builder.UserID = userId;
                _Builder.Password = password;
                _Builder.InitialCatalog = initialCatalog;
                _Builder.DataSource = dataSource;

                _Conn.ConnectionString = _Builder.ToString();
                _Conn.Open();
                return eConnectState.Connect;
            }
            catch(Exception e)
            {
                return eConnectState.NotConnect;
            }
        }

        public eConnectState DisConnect(eConnectState connState)
        {
            if (connState == eConnectState.Connect)
            {
                _Conn.Close();
                return eConnectState.NotConnect;
            }

            return connState;
        }

        public void Execute(string queryText)
        {
            if (string.IsNullOrEmpty(queryText))
                return;

            SqlDataReader reader;

            _cmd.CommandText = queryText;
            _cmd.Connection = _Conn;
            reader = _cmd.ExecuteReader();
            reader.Close();

        }

        public List<string> GetColumnName()
        {
            // 우선 TEST테이블에 있는 Column name을 가져옴
            string qry = $"SELECT column_name from information_schema.columns WHERE table_name='TEST'";

            SqlDataReader reader;

            _cmd.CommandText = qry;
            _cmd.Connection = _Conn;
            reader = _cmd.ExecuteReader();
            List<string> colNames = new List<string>();

            while(reader.Read())
            {
                colNames.Add(reader.GetString(0));
            }
            reader.Close();
            return colNames;
        }
        public int GetColumnCount()
        {
            // 우선 TEST테이블에 있는 Column name을 가져옴
            string qry = $"SELECT COUNT(*) FROM information_schema.columns WHERE table_name='TEST'";

            SqlDataReader reader;

            _cmd.CommandText = qry;
            _cmd.Connection = _Conn;
            reader = _cmd.ExecuteReader();

            int colCount = 0;

            while (reader.Read())
            {
                colCount = reader.GetInt32(0);
            }
            reader.Close();
            return colCount;
        }
        public DataTable GetTable()
        {
            string qry = $"select * from Test";
            int columnCount = GetColumnCount();

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn();

            dt.Clear();
            dt.Columns.Clear();

            for(int i=0;i<columnCount;i++)
            {
                dt.Columns.Add(GetColumnName()[i]);
            }
            _cmd.CommandText = qry;
            SqlDataAdapter adapt = new SqlDataAdapter(qry, _Conn);
            adapt.Fill(dt);

            return dt;
        }
    }
}
