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
                return eConnectState.DisConnect;
            }
        }

        private List<string> GetColumnName(string tbName)
        {
            // 우선 TEST테이블에 있는 Column name을 가져옴
            string qry = string.Format("SELECT column_name from information_schema.columns WHERE table_name='{0}'", tbName);

            SqlDataReader reader;

            _cmd.CommandText = qry;
            _cmd.Connection = _Conn;
            reader = _cmd.ExecuteReader();
            List<string> colNames = new List<string>();

            while (reader.Read())
            {
                colNames.Add(reader.GetString(0));
            }
            reader.Close();
            return colNames;
        }
        private int GetColumnCount(string tbName)
        {
            if (_Conn == null)
                return 0;

            // 우선 테이블에 있는 Column name을 가져옴
            string qry = string.Format("SELECT COUNT(*) FROM information_schema.columns WHERE table_name='{0}'", tbName);

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


        public eConnectState DisConnect(eConnectState connState)
        {
            if (connState == eConnectState.Connect)
            {
                _Conn.Close();
                return eConnectState.DisConnect;
            }
            return connState;
        }

        public eExecuteResult Execute(string queryText)
        {
            if (string.IsNullOrEmpty(queryText))
                return eExecuteResult.QueryError;
            queryText.Trim().Replace(Environment.NewLine, string.Empty);

            try
            {
                SqlDataReader reader;

                _cmd.CommandText = queryText;
                _cmd.Connection = _Conn;

                reader = _cmd.ExecuteReader();
                reader.Close();
                return eExecuteResult.Sucess;
            }
            catch(Exception e)
            {
                return eExecuteResult.QueryError;
            }
        }


        public DataView GetDataViewTable(string tbName)
        {
            if (tbName == null)
                return null;
            int columnCount = GetColumnCount(tbName);
            string qry = string.Format("select * from {0}", tbName);

            DataTable dt = new DataTable();

            dt.Clear();
            dt.Columns.Clear();

            for (int i = 0; i < columnCount; i++)
            {
                dt.Columns.Add(GetColumnName(tbName)[i]);
            }
            try
            {
                SqlDataAdapter adapt = new SqlDataAdapter(qry, _Conn);
                adapt.Fill(dt);
            }
            catch
            {
                return null;
            }

            return dt.AsDataView();
        }
    }
}
