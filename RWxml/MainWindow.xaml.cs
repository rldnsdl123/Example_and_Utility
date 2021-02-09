using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RWxml
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex regex = new Regex("[<>\'\"&]");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (regex.IsMatch(e.Text))
            {
                e.Handled = true;
                MessageBox.Show(string.Format("{0} 사용할 수 없습니다", e.Text), "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {

            }

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML File *.xml|*.xml";
            dialog.ShowDialog();
            RecoveryText text = new RecoveryText();
            text.Solution = txtSolution.Text;

            bool result= ReadWriteXml.SaveXml(text, dialog.FileName);
            if(result==true)
            {
                MessageBox.Show("저장완료");
            }
            else
            {
                MessageBox.Show("저장실패");
            }
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            txtSolution.Text = "";

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML File *.xml|*.xml";
            RecoveryText recovery = new RecoveryText();

            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
                recovery = ReadWriteXml.LoadXml(dialog.FileName);

            if (recovery == null)
            {
                MessageBox.Show("XML파일 포멧 오류");
                return;
            }
            txtSolution.Text = recovery.Solution;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSolution.Text = "";
        }
    }
}
