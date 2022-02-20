using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LLPA_Website.Helpers
{
    public static class PasswordHelper
    {
        public static string SimpleHashPassword(string password)
        {
            return Hasher.SHA256(Hasher.MD5(password));
        }

        public static string CreateRandomPassword(int length = 12)
        {
            // Create a string of characters, numbers, three special characters that are allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789&?!";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
    }
}
