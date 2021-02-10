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

namespace MakeGUIusingCSfile
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MakeGrid();
        }

        private void MakeGrid()
        {
            Grid grd = new Grid();
            Content = grd;
            grd.ShowGridLines = true;
            RowDefinition rd = new RowDefinition();

            ColumnDefinition cd = new ColumnDefinition();

            for (int i = 0; i <8; i++)
            {
                rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                grd.RowDefinitions.Add(rd);

                cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                grd.ColumnDefinitions.Add(cd);
            }

            Button btn1 = new Button();
            btn1.Content = "XX";
            grd.Children.Add(btn1);
            Grid.SetRow(btn1, 1);
            Grid.SetColumn(btn1, 1);

            Button btn2 = new Button();
            btn2.Content = "XXXXXX";
            grd.Children.Add(btn2);
            Grid.SetRow(btn2, 3);
            Grid.SetRowSpan(btn2, 3);
            Grid.SetColumn(btn2, 5);

        }
    }
}
