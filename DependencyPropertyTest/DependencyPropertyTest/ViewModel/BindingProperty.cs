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
    public class BindingProperty : DependencyObject
    {
        MainWindow _Main;

        public BindingProperty(MainWindow mainWindow)
        {
            _Main = mainWindow;
            _Main.Check.Checked += Check_Checked;
            _Main.Check.Unchecked+= Check_Checked;
        }



        public static readonly DependencyProperty IsSetProperty =
              DependencyProperty.Register("IsSet", typeof(bool), typeof(BindingProperty),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnIsSetChange)));

        private static void OnIsSetChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindingProperty window = d as BindingProperty;
            if (window._Main.Check.IsChecked == true)
                window._Main.dependency.Text = "Dependency Property 연습~";
            else
                window._Main.dependency.Text = "";
        }

        public bool IsSet
        {
            get => (bool)GetValue(IsSetProperty);
            set { SetValue(IsSetProperty, value); }
        }


        private void Check_Checked(object sender, RoutedEventArgs e)
        {
            IsSet = (bool)_Main.Check.IsChecked;
        }
        
    }
}
