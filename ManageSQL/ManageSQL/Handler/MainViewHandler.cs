﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand BtnConnect { get; set; }

        public MainViewHandler()
        {
            _DB = new DB();
            BtnConnect = new RelayCommand(DBConnectClick);

            _UserId = "cim";
            _Password = "2230";
            _DataSource = "localhost";
            _InitialCatalog = "TestDB";

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