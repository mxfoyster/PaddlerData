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
        List<Paddler>  paddlers = new List <Paddler>();
        int currentPaddler = 0;
        PaddlerXML paddlerXML = new PaddlerXML();
       
        public MainWindow()
        {
            InitializeComponent();
            paddlerXML.LoadData(paddlers);
            PopulatePanel();
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

        private void P_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //Name check code here
            
        }

       
        /// <summary>
        /// Uses PhoneNumber class to validate the input.. 
        /// Called by LoseFocus event using source as reference to which element called it.
        /// Doing this means we can use same method for any phone number = DRY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_TextInputted(object sender, RoutedEventArgs e)
        {
            TextBox? thisTextBox = e.Source as TextBox; //avoid possible null warning
            if (thisTextBox is not null) //avoid possible null warning
            {   
                string numToCheck = thisTextBox.Text;
                PhoneNumber thisPhoneNumber = new PhoneNumber(numToCheck);
                if (!thisPhoneNumber.Validate()) thisTextBox.Text = "Invalid Entry";
            }            
        }

        /// <summary>
        /// Clears the box when re-focused after invalid entry 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_ClearInvalid(object sender, RoutedEventArgs e)
        {
            TextBox? thisTextBox = e.Source as TextBox; //avoid possible null warning
            if (thisTextBox is not null) //agan, avoid null warning
                if (thisTextBox.Text == "Invalid Entry") thisTextBox.Text = "";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SavePanelToList();
            paddlerXML.SaveData(paddlers);
        }

        private void PopulatePanel()
        {
            Paddler thisPaddler = new Paddler();
            thisPaddler = paddlers[currentPaddler];
            P_Name.Text = thisPaddler.paddlerName;
            P_Number.Text = thisPaddler.paddlerNumber; 
            E_Name.Text = thisPaddler.emergencyName;
            E_Number.Text = thisPaddler.emergencyNumber;
            P_Address.Text = thisPaddler.paddlerAddress;
            P_Medical.Text = thisPaddler.paddlerMedical;
            P_TandC_Chk.IsChecked = thisPaddler.termsRead;
            P_OnWater_Chk.IsChecked = thisPaddler.onWater;      
        }

        //RIGHT ARROW LOGIC
        private void FwdBtn_Click(object sender, RoutedEventArgs e)
        {
            SavePanelToList();
            if (currentPaddler < (paddlers.Count-1))
            {
                currentPaddler++;
                PopulatePanel();
            }
            else
            {
                currentPaddler = 0;
                PopulatePanel();
            }
        }

        //LEFT ARROW LOGIC
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            SavePanelToList();
            if (currentPaddler > 0)
            {
                currentPaddler--;
                PopulatePanel();
            }
            else
            {
                currentPaddler = paddlers.Count - 1;
                PopulatePanel();
            }
        }

        //Copies current screen content into our list of paddlers at current index
        private void SavePanelToList()
        {
            Paddler thisPaddler = new Paddler();
            thisPaddler.paddlerName = P_Name.Text;
            thisPaddler.paddlerNumber = P_Number.Text;
            thisPaddler.emergencyName = E_Name.Text;
            thisPaddler.emergencyNumber = E_Number.Text;
            thisPaddler.paddlerAddress = P_Address.Text;
            thisPaddler.paddlerMedical = P_Medical.Text;
            if (P_TandC_Chk.IsChecked == true) thisPaddler.termsRead = true; // doing this was saves problems from type bool? and bool
            if (P_OnWater_Chk.IsChecked == true) thisPaddler.onWater = true; //as above
            paddlers[currentPaddler] = thisPaddler;
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            currentPaddler = paddlers.Count();
            Paddler thisPaddler = new Paddler();
            thisPaddler.paddlerName = "Testing";
            thisPaddler.paddlerNumber = "";
            thisPaddler.emergencyName = "";
            thisPaddler.emergencyNumber = "";
            thisPaddler.paddlerAddress = "";
            thisPaddler.paddlerMedical = "";
            thisPaddler.termsRead = false;
            thisPaddler.onWater = false;
            paddlers.Add(thisPaddler);
            PopulatePanel();
        }
    }
}
