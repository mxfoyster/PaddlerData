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

namespace PaddlerData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void P_OnWater_Chk_Checked(object sender, RoutedEventArgs e)
        {
            if (P_TandC_Chk.IsChecked == false)
            {
                string messageBoxText = "Paddler should NOT enter water until\nthey have read TERMS AND CONDITIONS!\n\nAre you sure you want to check them in?";
                string caption = "WARNING";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                
                if (result == MessageBoxResult.No) P_OnWater_Chk.IsChecked = false;
            }


        }
    }
}
