using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DomainModel.Concrete.Helpers
{
    public static class Encryption
    {
        private static readonly string _secretKey = "Na+Cl-";
        private static readonly string _dictionary = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string Encrypt(string plainText, string salt)
        {
            SHA512 hasher = SHA512.Create();

            StringBuilder textToHash = new StringBuilder();

            byte [] hashResult = null;

            textToHash.Append(_secretKey);
            textToHash.Append(plainText);
            textToHash.Append(salt);

            hashResult = hasher.ComputeHash(Encoding.Default.GetBytes(textToHash.ToString()));

            return Convert.ToBase64String(hashResult);
        }

        public static bool Verify(string plainText, string hashText, string salt)
        {
            if (Encrypt(plainText, salt).Equals(hashText))
                return true;
            return false;
        }

        public static string GenerateSalt()
        {
            int length, position;
            string dictionaryCharacter;

            StringBuilder newSalt = new StringBuilder();

            Random lengthGenerator = new Random();
            Random dictionaryPosition = new Random();

            length = lengthGenerator.Next(10, 15);

            for (int i = 0; i < length; i++)
            {
                position = dictionaryPosition.Next(0, _dictionary.Length);
                dictionaryCharacter = _dictionary[position].ToString();
                newSalt.Append(dictionaryCharacter);
            }

            return newSalt.ToString();
        }


    }
}
