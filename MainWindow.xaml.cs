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
        List<Paddler> paddlers = new List<Paddler>();
        int currentPaddler = 0;
        PaddlerXML paddlerXML = new PaddlerXML();

        public MainWindow()
        {
            InitializeComponent();
            paddlerXML.LoadData(paddlers);
            PopulatePanel();
            CountPaddlersOnWater();
        }

        private void P_OnWater_Chk_Checked(object sender, RoutedEventArgs e)
        {
            if (P_TandC_Chk.IsChecked == false && P_OnWater_Chk.IsChecked == true)
            {
                string messageBoxText = "Paddler should NOT enter water until\nthey have read TERMS AND CONDITIONS!\n\nAre you sure you want to check them in?";
                string caption = "WARNING";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                if (result == MessageBoxResult.No) P_OnWater_Chk.IsChecked = false;
            }
            SavePanelToList();
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


        /// <summary>
        /// Handles deleting the entry on screen in both file and memory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            SavePanelToList();
            paddlerXML.SaveData(paddlers);
            //Dialog 'are you sure?
            string messageBoxText = "Are you sure you want to delete this paddler?\n\nThe details will be permanently lost";
            string caption = "Delete Paddler";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Cancel);

            if (result == MessageBoxResult.OK)
            {
                //delete this entry as long as it's not the only one
                if (paddlers.Count > 1)
                {
                    paddlers.RemoveAt(currentPaddler);
                    paddlerXML.SaveData(paddlers);
                    if (currentPaddler > 0) currentPaddler -= 1;
                    PopulatePanel();
                    CountPaddlersOnWater();
                }
                else //otherwise set it as blank and then save it
                {
                    P_Name.Text = "";
                    P_Number.Text = "";
                    E_Name.Text = "";
                    E_Number.Text = "";
                    P_Address.Text = "";
                    P_Medical.Text = "";
                    P_TandC_Chk.IsChecked = false;
                    P_OnWater_Chk.IsChecked = false;

                    SavePanelToList();
                    CountPaddlersOnWater();
                }
            }
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
            if (currentPaddler < (paddlers.Count - 1))
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
            CountPaddlersOnWater();
        }

        //handles new button by inserting a new 'record' into our list and clearing display
        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            SavePanelToList();
            currentPaddler = paddlers.Count();
            paddlerXML.AddBlankPaddler(paddlers);
            PopulatePanel();
        }

        private void AboutBox(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Paddler Data Version 1.0.0\n\nBy Mark Foyster April 2022\n\nGNU GENERAL PUBLIC LICENSE Version 3";
            string caption = "About";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        private void HelpBox(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Paddler Data Version 1.0.0\n\nInstructions\n\nAll the help information will go here when we know exactly what this app will do.";
            string caption = "Help";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }


        /// <summary>
        /// Called on close through file menu
        /// </summary>
        /// <param name="sender">ref to sending element</param>
        /// <param name="e">event sent</param>
        private void QuitBox(object sender, RoutedEventArgs e)
        {
            //Dialog 'are you sure?
            string messageBoxText = "Are you sure you want to quit?\n\nWould you like to save the Paddler Data?";
            string caption = "Quit";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            //Save progress before exit as chosen
            if (result == MessageBoxResult.Yes)
            {
                SavePanelToList();
                paddlerXML.SaveData(paddlers);
                this.Close();
            }

            if (result == MessageBoxResult.No) this.Close();
        }
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //code to do when new chosen here
        }

        /// <summary>
        /// File - Save selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCurrent(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(paddlerXML.GetFilename());
            SavePanelToList();
            paddlerXML.SaveData(paddlers);
        }

        /// <summary>
        /// Our File - Save As handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAs(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "PaddlerData"; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "XML Document (.xml)|*.xml"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string thisFilename = dialog.FileName;
                paddlerXML.SetFilename(thisFilename);
                SavePanelToList();
                paddlerXML.SaveData(paddlers);
            }
        }

        /// <summary>
        /// Our file - open code using system dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileOpen(object sender, RoutedEventArgs e)
        {
            //Prompt user we are automatically saving first
            string AutoSavemessageBoxText = "Just so you know,\n\nThe file you were just working on will be saved automatically.";
            string AutoSavecaption = "Saving";
            MessageBoxButton AutoSavebutton = MessageBoxButton.OK;
            MessageBoxImage AutoSaveicon = MessageBoxImage.Exclamation;
            MessageBox.Show(AutoSavemessageBoxText, AutoSavecaption, AutoSavebutton, AutoSaveicon);
            //So we save like we said we would
            SavePanelToList();
            paddlerXML.SaveData(paddlers);
            //Open the File Open Dialog
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "PaddlerData"; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "XML Document (.xml)|*.xml"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string thisFilename = dialog.FileName;
                try
                {
                    //clear the list
                    paddlers.Clear();
                    //load and display
                    paddlerXML.SetFilename(thisFilename);
                    paddlerXML.LoadData(paddlers);
                    PopulatePanel();
                }
                catch (Exception eb)
                {
                    string messageBoxText = "I'm afraid there was a problem,\n\nThe file you slected was invalid, please try another file.\n\n Error message: " + eb;
                    string caption = "Whoops!";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
            }
        }


        //Counts the number of paddlers within our list with 'On Water' checked and displays the result
        private void CountPaddlersOnWater()
        {
            int count = 0, counter=0;
            List <string> paddlersOnWater = new List <String>();

            foreach (Paddler thisPaddler in paddlers)
            {
             
                if (thisPaddler.onWater == true)
                {
                    count++;
                    PaddlerOnWater paddlerOnWater = new PaddlerOnWater();
                    paddlerOnWater.paddlerName = thisPaddler.paddlerName;
                    //System.Diagnostics.Debug.WriteLine(paddlerOnWater.paddlerName);
                    paddlerOnWater.indexInPaddlersList = counter;
                    paddlersOnWater.Add(paddlerOnWater.paddlerName);
                }
                counter++; //for our index within list
            }
            NumPaddlersBox.Text = count.ToString();
            OnWaterPaddlerNames.ItemsSource = paddlersOnWater; //populate the ListBox
        }

    }
}
