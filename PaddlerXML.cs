using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.IO;

namespace PaddlerData
{
    internal class PaddlerXML
    {
        //XmlDocument paddlerDoc = new XmlDocument();
        string xmlFilename = "PaddlerData.xml";
        string xmlBackupFile = "PaddlerData.bak";

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
                
                paddlerReader.ReadToFollowing("paddler");//gets us to the first paddler element
                paddlerReader.MoveToFirstAttribute(); //steps into it
                
                //the capture loop, iterates once for each paddler element
                do
                {
                    paddlerReader.ReadToFollowing("paddlerName"); //It's important these are in order..
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
                    
                    //now add current xml data to the list
                    paddlers.Add(new Paddler(thisPaddlerName, thisPaddlerNumber, thisEmergencyName, thisEmergencyNumber, thisPaddlerAddress, thisPaddlerMedical,
                                   thisTermsRead, thisOnWater));

                } while (paddlerReader.ReadToFollowing("paddler"));
                paddlerReader.Close();
            }
            catch (Exception ex)
            {
                string messageBoxText = "Paddler data file not found, loading default data\n\n" + ex;
                string caption = "WARNING";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }//end of LoadData()

        public void SaveData (List<Paddler> paddlers)
        {
            //first let's backup the original file
            File.Copy(xmlFilename, xmlBackupFile, true);

            //now setup the writer and open the file
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter paddlerWriter = XmlWriter.Create(xmlFilename, settings);

            //now let's put the data in it
            paddlerWriter.WriteStartDocument();
            paddlerWriter.WriteStartElement("paddlers");
            foreach(Paddler paddler in paddlers)
            {
                paddlerWriter.WriteStartElement("paddler");
                    paddlerWriter.WriteElementString("paddlerName", paddler.paddlerName);
                    paddlerWriter.WriteElementString("paddlerNum",paddler.paddlerNumber);
                    paddlerWriter.WriteElementString("emergencyName", paddler.emergencyName);
                    paddlerWriter.WriteElementString("emergencyNum",paddler.emergencyNumber);
                    paddlerWriter.WriteElementString("paddlerAddress",paddler.paddlerAddress);
                    paddlerWriter.WriteElementString("paddlerMedical",paddler.paddlerMedical);
                    
                    if (paddler.termsRead == false) paddlerWriter.WriteElementString("readTandC", "false");
                    else paddlerWriter.WriteElementString("readTandC", "true");

                    if (paddler.onWater == false) paddlerWriter.WriteElementString("onWater", "false");
                    else paddlerWriter.WriteElementString("onWater", "true");
                paddlerWriter.WriteEndElement();
            }
            paddlerWriter.WriteEndElement();
            paddlerWriter.WriteEndDocument();
            
            //flush the buffer and close the file
            paddlerWriter.Flush();
            paddlerWriter.Close();
        }//end of SaveData
    }
}
