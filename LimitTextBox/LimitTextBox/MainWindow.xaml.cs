using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LimitTextBox
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        StringCheck TextCheck;
        public MainWindow()
        {
            InitializeComponent();

            TextCheck = new StringCheck();
            this.DataContext = TextCheck;
            TextCheck.PropertyChanged += TextCheck_PropertyChanged;

            //MainViewModel vm = new MainViewModel();
            //this.DataContext = vm;
            //vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void TextCheck_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var obj = sender as StringCheck;
            switch(e.PropertyName)
            {
                case "ImpossibleText":
                    {
                        MessageBox.Show(string.Format("{0} 사용할 수 없습니다", obj.ImpossibleText));
                    }
                    break;
            }
        }

        //private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    var obj = sender as MainViewModel;
        //    switch(e.PropertyName)
        //    {
        //        case "Box":
        //            {
        //            }
        //            break;

        //        case "ImpossibleText":
        //            {
        //                MessageBox.Show(string.Format("{0} 사용할 수 없습니다", obj.ImpossibleText));
        //            }
        //            break;
        //    }
        //}

        private void StringCheck_Click(object sender, RoutedEventArgs e)
        {
            TxtBox.Text = TextCheck.CheckString(TxtBox.Text);
        }
    }
}
