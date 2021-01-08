using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class MainViewModel :BasePropertyChanged
    {
        public RelayCommand ChkCommand { get; set; }

        private bool _EnableCheck;
        public bool EnableCheck
        {
            get => _EnableCheck;
            set
            {
                if (_EnableCheck == value)
                    return;

                _EnableCheck = value;
                OnPropertyChanged(nameof(EnableCheck));

            }
        }

        public MainViewModel()
        {
            ChkCommand = new RelayCommand(ChangeEnable);
            _EnableCheck = true;
        }

        private void ChangeEnable(object param)
        {
            if (EnableCheck == true)
                EnableCheck = false;
            else
                EnableCheck = true;
        }
    }
}
