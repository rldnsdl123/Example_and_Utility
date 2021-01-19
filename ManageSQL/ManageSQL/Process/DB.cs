using System;
using System.Collections.Generic;
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
        public DB()
        {
            _Builder = new SqlConnectionStringBuilder();
            _Conn = new SqlConnection();
        }
        public eConnectState Connect(string userId, string password, string initialCatalog, string dataSource)
        {
            _Builder.UserID = userId;
            _Builder.Password = password;
            _Builder.InitialCatalog = initialCatalog;
            _Builder.DataSource = dataSource;

            try
            {
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
    }
}
