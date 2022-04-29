using System;
using System.Collections.Generic;
using System.Linq;
//I will use this class to handle moy paddler data
using System.Text;
using System.Threading.Tasks;

namespace PaddlerData
{
    internal class Paddler
    {
        public string paddlerName;
        public string paddlerNumber;
        public string emergencyName;
        public string emergencyNumber;
        public string paddlerAddress;
        public string paddlerMedical;
        public bool termsRead;
        public bool onWater;
        public DateTime timeOnWater;

        //we'll initialise everything before we start
        public Paddler()
        {
            paddlerName = "";
            paddlerNumber = "";
            emergencyName = "";
            emergencyNumber = "";
            paddlerAddress = "";
            paddlerMedical = "";
            termsRead = false;
            onWater = false;   
        }

        public Paddler(string paddlerName, string paddlerNumber, string emergencyName, string emergencyNumber, string paddlerAddress, 
                        string paddlerMedical, bool termsRead, bool onWater, DateTime timeOnWater)
        {
            this.paddlerName = paddlerName;
            this.paddlerNumber = paddlerNumber; 
            this.emergencyName = emergencyName;
            this.emergencyNumber = emergencyNumber;
            this.paddlerAddress = paddlerAddress;
            this.paddlerMedical = paddlerMedical;
            this.termsRead = termsRead;
            this.onWater = onWater;
            this.timeOnWater = timeOnWater;
        }
       
    }
}
