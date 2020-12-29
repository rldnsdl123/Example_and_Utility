using DependencyPropertyTest.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DependencyPropertyTest
{
    public class BindingProperty:DependencyObject
    {
        MainWindow _Main;

        public static readonly DependencyProperty IsSetProperty =
           DependencyProperty.Register("IsSet", typeof(bool), typeof(MainWindow),
                new FrameworkPropertyMetadata(OnIsSetChange));

        private static void OnIsSetChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        //public ICommand IsSetCmd { get; set; }
        
        public bool IsSet
        {
            get => (bool)GetValue(IsSetProperty);
            set { SetValue(IsSetProperty, value); }
        }

        public BindingProperty(MainWindow mainWindow)
        {
            _Main = mainWindow;
            //IsSetCmd = new RelayCommand(() => _Main.Check.IsChecked = true);
        }
    }
}
