using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace LimitTextBox
{
    public class MainViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        private string _Box;
        private string _ImpossibleText;

        public string Box
        {
            get => _Box;
            set
            {
                value= CheckString(value);
                if (_Box==value)
                    return;

                _Box = value;
                OnPropertyChanged(nameof(Box));
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
        public List<string> LimitText;
        public MainViewModel()
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

        private string CheckString(string msg)
        {
            foreach(string entity in LimitText)
            {
                if (msg.Contains(entity))
                {
                    msg = msg.Replace(entity, "");
                    ImpossibleText = entity;
                }
            }

            return msg;
        }
    }
}
