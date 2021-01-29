using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LimitTextBox
{
    public class StringCheck :INotifyPropertyChanged
    {
        private string _InputText;
        private string _RealTimeInputText;
        private string _ResultText;
        private string _ImpossibleText;

        private readonly List<string> LimitText;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public string RealTimeInputText
        {
            get => _RealTimeInputText;
            set
            {
                value = RealTimeCheckString(value); 
                if (_RealTimeInputText == value)
                    return;
                _RealTimeInputText = value;
                OnPropertyChanged(nameof(RealTimeInputText));
            }
        }

        public string InputText
        {
            get => _InputText;
            set
            {
                if (_InputText == value)
                    return;
                _InputText = value;
                OnPropertyChanged(nameof(InputText));
            }
        }
        public string ResultText
        {
            get => _ResultText;
            set
            {
                if (_ResultText == value)
                    return;
                _ResultText = value;
            }
        }
        public string ImpossibleText
        {
            get => _ImpossibleText;
            set
            {
                _ImpossibleText = value;
                OnPropertyChanged(nameof(ImpossibleText));
            }
        }
        public StringCheck()
        {
            LimitText = new List<string>
           {
                "<",
                ">",
                "\'",
                "\"",
                "&",
            };
        }
        public StringCheck(string Text) : this()
        {
            if (Text == null)
                return;

            _InputText = Text;
        }

        public string CheckString(string msg)
        {
            if (msg == null)
                return msg;

            _InputText = msg;
            string wrongText = "";
            foreach (string entity in LimitText)
            {
                if (_InputText.Contains(entity))
                {
                    _InputText = _InputText.Replace(entity, "");
                    wrongText += entity;
                    wrongText += ",";
                }
            }
            _ResultText = _InputText;
            ImpossibleText = wrongText;

            return ResultText;
        }

        private string RealTimeCheckString(string msg)
        {
            _InputText = msg;
            foreach (string entity in LimitText)
            {
                if (_InputText.Contains(entity))
                {
                    _InputText = _InputText.Replace(entity, "");
                    ImpossibleText = entity;
                }
            }
            _ResultText = _InputText;

            return _ResultText;
        }
    }
}
