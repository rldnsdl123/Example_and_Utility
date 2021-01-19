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
        private string _UserId;
        private string _Password;
        private string _InitialCatalog;
        private string _DataSource;
        private DB _DB;
        private eConnectState _Connstate;
        private string _QueryText;

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

        public ICommand BtnConnect { get; set; }
        public ICommand BtnQueryExecute { get; set; }
        public ICommand BtnGetTable { get; set; }

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

        }


        public DataTable datatable { get; set; }
                
        private void GetTable()
        {
            //_DB.GetColumnName();
            int columnCount = _DB.GetColumnCount();

            datatable = _DB.GetTable();

        }

        private void ExecuteQuery()
        {
            _DB.Execute(_QueryText);
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
