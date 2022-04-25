//we can use this class to sanitise the phone number as well as store it.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaddlerData
{
    internal class PhoneNumber
    {
        private string phoneNumber;

        public PhoneNumber (string inputNumber)
        {
            phoneNumber = inputNumber;
        }

        public bool Validate(string numToValidate)
        {
            bool result = true;
            foreach (char digit in numToValidate)
                if (((int) digit <48 || (int) digit > 57) && digit != ' ') result = false;
            return result;
        }

        /// <summary>
        /// Overloaded method, this one checks pre-assigned number to class
        /// </summary>
        /// <returns>True if just nums and spaces</returns>
        public bool Validate()
        {
            bool result = true;
            foreach (char digit in phoneNumber)
                if (((int)digit < 48 || (int)digit > 57) && digit != ' ') result = false;
            return result;
        }
    }
}
