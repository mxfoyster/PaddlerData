using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace PaddlerData
{
    internal class PaddlerXML
    {
        //XmlDocument paddlerDoc = new XmlDocument();
        string xmlFilename = "PaddlerData.xml";

        /// <summary>
        /// Load the XML file 
        /// If not found, alert user and load default data
        /// </summary>
        /// <param name="paddlers">Array of type paddlers to store data into</param>
        public void LoadData(List<Paddler> paddlers)
        {
            try
            {
                using var paddlerReader = XmlReader.Create(xmlFilename);
                
                paddlerReader.ReadToFollowing("paddler");
                paddlerReader.MoveToFirstAttribute();
                do
                {
                    paddlerReader.ReadToFollowing("paddlerName");
                    //paddlers[0].paddlerName = "testing array";//paddlerReader.ReadElementContentAsString();
                    string thisPaddlerName = paddlerReader.ReadElementContentAsString();

                    paddlerReader.ReadToFollowing("paddlerNum");
                    string thisPaddlerNumber = paddlerReader.ReadElementContentAsString();

                    paddlerReader.ReadToFollowing("emergencyName");
                    string thisEmergencyName = paddlerReader.ReadElementContentAsString();

                    paddlerReader.ReadToFollowing("emergencyNum");
                    string thisEmergencyNumber = paddlerReader.ReadElementContentAsString();

                    paddlerReader.ReadToFollowing("paddlerAddress");
                    string thisPaddlerAddress = paddlerReader.ReadElementContentAsString();

                    paddlerReader.ReadToFollowing("paddlerMedical");
                    string thisPaddlerMedical = paddlerReader.ReadElementContentAsString();

                    paddlerReader.ReadToFollowing("readTandC");
                    bool thisTermsRead = paddlerReader.ReadElementContentAsBoolean();

                    paddlerReader.ReadToFollowing("onWater");
                    bool thisOnWater = paddlerReader.ReadElementContentAsBoolean();
                    paddlers.Add(new Paddler(thisPaddlerName, thisPaddlerNumber, thisEmergencyName, thisEmergencyNumber, thisPaddlerAddress, thisPaddlerMedical,
                                   thisTermsRead, thisOnWater));

                } while (paddlerReader.ReadToFollowing("paddler"));
            }
            catch (Exception ex)
            {
                string messageBoxText = "Paddler data file not found, loading default data\n\n" + ex;
                string caption = "WARNING";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }
}
