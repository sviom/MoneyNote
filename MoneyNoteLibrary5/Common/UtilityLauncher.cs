﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MoneyNoteLibrary5.Common
{
    public static class UtilityLauncher
    {
        /// <summary>
        /// SHA256 암호화
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptSHA256(string plainText)
        {
            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                SHA256Managed sm = new SHA256Managed();
                byte[] encryptedBytes = sm.ComputeHash(plainBytes);
                string encryptedString = Convert.ToBase64String(encryptedBytes);
                return encryptedString;
            }
            catch
            {
                return null;
            }
        }

        public static string ConvertGuidToBase64(Guid guid)
        {
            var urlSafeBase64String = Convert.ToBase64String(guid.ToByteArray());
            return urlSafeBase64String.Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }

        public static string ConvertSafeString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Replace('+', '-').Replace('/', '_');//.TrimEnd('=');
        }

        public static string ConvertSafeToOriginalString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Replace('-', '+').Replace('_', '/');
        }

        public static Guid ConvertBase64ToGuid(string base64)
        {
            base64 = base64.Replace('-', '+').Replace('_', '/');
            int paddings = base64.Length % 4;
            if (paddings > 0)
            {
                base64 += new string('=', 4 - paddings);
            }

            var decodedByte = Convert.FromBase64String(base64);
            return new Guid(decodedByte);
        }

        /// <summary>
        /// AES256 암호화
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptAES256(string plainText, string password)
        {
            try
            {
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    byte[] textBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] salt = Encoding.UTF8.GetBytes(password.Length.ToString());
                    PasswordDeriveBytes deriveBytes = new PasswordDeriveBytes(password, salt);

                    ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(deriveBytes.GetBytes(32), deriveBytes.GetBytes(16));

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            if (cryptoStream.CanWrite)
                            {
                                cryptoStream.Write(textBytes, 0, textBytes.Length);
                            }
                        }

                        var memoryArray = memoryStream.ToArray();
                        return Convert.ToBase64String(memoryArray);
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// AES256 복호화
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string DecryptAES256(string base64String, string password)
        {
            try
            {
                string result = string.Empty;
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {

                    byte[] encryptData = Convert.FromBase64String(base64String);
                    byte[] salt = Encoding.UTF8.GetBytes(password.Length.ToString());
                    PasswordDeriveBytes deriveBytes = new PasswordDeriveBytes(password, salt);

                    ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(deriveBytes.GetBytes(32), deriveBytes.GetBytes(16));

                    using (var memoryStream = new MemoryStream(encryptData))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            if (cryptoStream.CanRead)
                            {
                                byte[] decryptedData = new byte[encryptData.Length];

                                int count = cryptoStream.Read(decryptedData, 0, decryptedData.Length);
                                result = Encoding.UTF8.GetString(decryptedData, 0, count);
                            }
                        }
                    }

                    return result;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T ConvertType<T>(this object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
