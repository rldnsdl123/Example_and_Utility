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

        public MainViewModel()
        {
        }

        private string CheckString(string msg)
        {
            List<string> LimitText = new List<string>
            {
                "<",
                ">",
                "\'",
                "\"",
                "&",
            };

            foreach(string entity in LimitText)
            {
                if (msg.Contains(entity))
                {
                    msg = msg.Replace(entity, "");
                    System.Diagnostics.Debug.WriteLine(entity + "삭제");
                }
            }

            return msg;
        }
    }
}
