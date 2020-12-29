using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DependencyPropertyTest
{
    public static class IsSetAttachedProperty
    {
        public static readonly DependencyProperty IsSetProperty =
              DependencyProperty.Register("IsSet", typeof(bool), typeof(MainWindow),
                   new FrameworkPropertyMetadata(new PropertyChangedCallback(OnIsSetChange)));

        private static void OnIsSetChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow m = d as MainWindow;

            if (m.Check.IsChecked == true)
                m.Check.IsChecked = false;
            else
                m.Check.IsChecked = true;

        }

    }
}
