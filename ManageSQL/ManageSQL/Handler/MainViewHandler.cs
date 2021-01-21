using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ManageSQL
{
    public class MainViewHandler : BasePropertyChange
    {
        #region Variable
        private string _UserId;
        private string _Password;
        private string _InitialCatalog;
        private string _DataSource;
        private string _QueryText;
        private string _TableName;
        private bool _CreateCheck;
        private bool _DeleteCheck;
        private bool _InsertCheck;
        private bool _UpdateCheck;

        private eConnectState _Connstate;
        private eExecuteResult _Executeret;

        private DB _DB;
        private DataView _View;
        #endregion

        #region Property
        public string UserId
        {
            get => _UserId;
            set
            {
                if (_UserId == value)
                    return;
                _UserId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        public string Password
        {
            get => _Password;
            set
            {
                if (_Password == value)
                    return;
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string InitialCatalog
        {
            get => _InitialCatalog;
            set
            {
                if (_InitialCatalog == value)
                    return;
                _InitialCatalog = value;
                OnPropertyChanged(nameof(InitialCatalog));
            }
        }
        public string DataSource
        {
            get => _DataSource;
            set
            {
                if (_DataSource == value)
                    return;
                _DataSource = value;
                OnPropertyChanged(nameof(DataSource));
            }
        }
        public eConnectState ConnState
        {
            get => _Connstate;
            set
            {
                if (_Connstate == value)
                    return;

                _Connstate = value;
                OnPropertyChanged(nameof(ConnState));
            }
        }
        public string QueryText
        {
            get => _QueryText;
            set
            {
                if (_QueryText == value)
                    return;

                _QueryText = value;
                OnPropertyChanged(nameof(QueryText));
            }
        }
        public string TableName
        {
            get => _TableName;
            set
            {
                if (_TableName == value)
                    return;
                _TableName = value;
                OnPropertyChanged(nameof(TableName));
            }
        }
        public bool CreateCheck
        {
            get => _CreateCheck;
            set
            {
                if (_CreateCheck == value)
                    return;
                _CreateCheck = value;
                OnPropertyChanged(nameof(CreateCheck));
                if (value == true&&ConnState==eConnectState.Connect)
                    QueryText = "CREATE TABLE [테이블명] (조건~~)";

            }
        }
        public bool DeleteCheck
        {
            get => _DeleteCheck;
            set
            {
                if (_DeleteCheck == value)
                    return;
                _DeleteCheck = value;
                OnPropertyChanged(nameof(DeleteCheck));
                if (value == true && ConnState == eConnectState.Connect)
                    QueryText = "DELETE FROM [테이블명]";

            }
        }
        public bool InsertCheck
        {
            get => _InsertCheck;
            set
            {
                if (_InsertCheck == value)
                    return;
                _InsertCheck = value;
                OnPropertyChanged(nameof(InsertCheck));
                if (value == true && ConnState == eConnectState.Connect)
                    QueryText = "INSERT INTO [테이블명] VALUES(~~)";

            }
        }
        public bool UpdateCheck
        {
            get => _UpdateCheck;
            set
            {
                if (_UpdateCheck == value)
                    return;
                _UpdateCheck = value;
                OnPropertyChanged(nameof(UpdateCheck));
                if (value == true && ConnState == eConnectState.Connect)
                    QueryText = "UPDATE [테이블명] SET [COL=VALUE...]";

            }
        }
        #region Command
        public ICommand BtnConnect { get; set; }
        public ICommand BtnQueryExecute { get; set; }
        public ICommand BtnGetTable { get; set; }

        #endregion
        #endregion

        #region Constructor
        public MainViewHandler()
        {
            Initialize();
        }

        private void Initialize()
        {
            _DB = new DB();
            BtnConnect = new RelayCommand(DBConnectClick);
            BtnQueryExecute = new RelayCommand(ExecuteQuery);
            BtnGetTable = new RelayCommand(GetTable);

            _UserId = "cim";
            _Password = "2230";
            _DataSource = "localhost";
            _InitialCatalog = "TestDB";
            _QueryText = "DB와 연결해주세요";
            ConnState = eConnectState.DisConnect;
        }

        #endregion

        public DataView View
        {
            get
            {
                if (_View != null)
                    return _View;
                return null ;
            }

            set
            {
                if (value == null)
                {
                    QueryText = "Table명이 잘못되었습니다";
                    return;
                }
                _View = value;
                OnPropertyChanged(nameof(View));
            }
        }
                
        private void GetTable()
        {
            if (ConnState == eConnectState.DisConnect)
            {
                QueryText = "DB와 연결이 되어있지 않습니다.";
                return;
            }
            View = _DB.GetDataViewTable(TableName);
        }

        private void ExecuteQuery()
        {
            if (ConnState == eConnectState.DisConnect)
            {
                QueryText = "DB와 연결이 되어있지 않습니다.";
                return;
            }
            _Executeret =_DB.Execute(_QueryText);

            // select, delete 구문은 결과를 바로 보여줄 수 있는대 
            // update insert 다른 쿼리문은 수정이 필요함 table명을 못가져옴ㅋㅋㅋ
            string tablename = FindTableName(_QueryText);
            View = _DB.GetDataViewTable(tablename);

            if (_Executeret == eExecuteResult.Sucess)
            {
                QueryText = "Execute Sucess!";
            }
            else
                QueryText = "쿼리문을 수정해주세요";
        }

        private string FindTableName(string queryText)
        {
            int tableNameIndex = _QueryText.IndexOf("from");
            string tb = _QueryText.Substring(tableNameIndex + 5);
            string[] sp = tb.Split();
            return sp[0];
        }

        private void DBConnectClick()
        {
            if (ConnState == eConnectState.Connect)
                ConnState = _DB.DisConnect(ConnState);
            else
            {
                ConnState = _DB.Connect(_UserId, _Password, InitialCatalog, _DataSource);
                CreateCheck = true;
            }
        }
    }
}
