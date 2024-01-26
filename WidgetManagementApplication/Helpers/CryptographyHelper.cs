using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WidgetManagementApplication.Helpers
{
    public static class CryptographyHelper
    {
        private static readonly ILog Logger = log4net.LogManager.GetLogger(typeof(CryptographyHelper));

        public static readonly byte[] key =
        {
            4, 93, 171, 3, 85, 23, 41, 34, 216, 14, 78, 156, 78, 3, 103, 154, 9, 150,
            65, 54, 226, 95, 68, 79, 159, 36, 246, 57, 177, 107, 116, 8
        };

        public static string Encrypt(string connectionString)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.GenerateIV(); // Generate a random IV
                    aesAlg.Padding = PaddingMode.PKCS7; // Make sure to set the same padding mode during encryption


                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(connectionString);
                            }
                        }

                        byte[] iv = aesAlg.IV;
                        byte[] encryptedData = msEncrypt.ToArray();

                        // Combine IV and encrypted data for storage or transmission
                        byte[] result = new byte[iv.Length + encryptedData.Length];
                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(encryptedData, 0, result, iv.Length, encryptedData.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error while Encrypting connection string in CryptographyHelper : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error while Encrypting connection string in CryptographyHelper : "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

        public static string Decrypt(string encryptedConnectionString)
        {
            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedConnectionString);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.Padding = PaddingMode.PKCS7; // Make sure to set the same padding mode during encryption


                    // Extract IV from the encrypted data
                    byte[] iv = new byte[aesAlg.BlockSize / 8];
                    Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);

                    aesAlg.IV = iv;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null && ex.InnerException != null)
                {
                    Logger.Error("Error while Decrypting connection string in CryptographyHelper : "
                                 + Environment.NewLine + " Exception message : " + ex.Message
                                 + Environment.NewLine + " InnerException: " + ex.InnerException.ToString());
                }
                else if (ex != null)
                {
                    Logger.Error("Error while Decrypting connection string in CryptographyHelper : "
                                 + Environment.NewLine + " Exception message : " + ex.Message);
                }
                throw;//Shows Error page.
            }
        }

    }
}