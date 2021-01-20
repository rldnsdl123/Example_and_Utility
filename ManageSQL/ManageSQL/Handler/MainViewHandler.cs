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

        #region Command
        public ICommand BtnConnect { get; set; }
        public ICommand BtnQueryExecute { get; set; }
        public ICommand BtnGetTable { get; set; }

        #endregion
        #endregion

        #region Constructor
        public MainViewHandler()
        {
            _DB = new DB();
            BtnConnect = new RelayCommand(DBConnectClick);
            BtnQueryExecute = new RelayCommand(ExecuteQuery);
            BtnGetTable = new RelayCommand(GetTable);

            _UserId = "cim";
            _Password = "2230";
            _DataSource = "localhost";
            _InitialCatalog = "TestDB";
            _QueryText = "Query문을 입력해주세요";
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
                if (_View == null)
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
            if (_Executeret != eExecuteResult.Sucess)
                QueryText = "쿼리문을 수정해주세요";
        }

        private void DBConnectClick()
        {
            if (ConnState == eConnectState.Connect)
                ConnState = _DB.DisConnect(ConnState);
            else
                ConnState=_DB.Connect(_UserId,_Password,InitialCatalog,_DataSource);
        }
    }
}
