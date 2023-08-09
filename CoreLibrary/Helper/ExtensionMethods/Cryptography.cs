﻿using System.Security.Cryptography;
using System.Text;

namespace CoreLibrary.Helper.ExtensionMethods
{
    public static class Cryptography
    {
        public static string GetHash(this string value) {

            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(value);
            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array) {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
