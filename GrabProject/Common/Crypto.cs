using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Crypto
    {
        private static byte XOR = 0x27;
        public static string encrypt(string str)
        {
            string ret = "";
            char[] values = str.ToCharArray();

            foreach (char letter in values)
            {
                // Get the integral value of the character. 
                int value = Convert.ToInt32(letter);
                value ^= XOR;
                // Convert the decimal value to a hexadecimal value in string form. 
                string hexOutput = String.Format("{0:X}", value);
                ret += hexOutput;
            }

            return ret;
        }

        public static string decrypt(string str)
        {
            string ret = "";
            string hex = "";
            foreach (char tmp in str)
            {
                hex += tmp;
                
                if (hex.Length == 2) {
                    // Convert the number expressed in base-16 to an integer. 
                    int value = Convert.ToInt32(hex, 16);
                    value ^= XOR;
                    // Get the character corresponding to the integral value. 
                    string stringValue = Char.ConvertFromUtf32(value);
                    char charValue = (char)value;
                    ret += charValue;
                    hex = "";
                }
            }
            return ret;
        }
    }
}
