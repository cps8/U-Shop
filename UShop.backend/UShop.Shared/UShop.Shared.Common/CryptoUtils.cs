using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Shared.Common
{
    /// <summary>
    /// 加密工具类
    /// </summary>
    public static class CryptoUtils
    {
        private const int AesKeySize = 256;
        private const int AesBlockSize = 128;

        #region AES 加密解密
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesEncrypt(string plainText, string key)
        {
            using Aes aes = Aes.Create();
            aes.KeySize = AesKeySize;
            aes.BlockSize = AesBlockSize;
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(AesKeySize / 8).Substring(0, AesKeySize / 8));
            aes.IV = new byte[AesBlockSize / 8];
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] encryptedBytes = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);
            return Convert.ToBase64String(encryptedBytes);
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesDecrypt(string cipherText, string key)
        {
            using Aes aes = Aes.Create();
            aes.KeySize = AesKeySize;
            aes.BlockSize = AesBlockSize;
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(AesKeySize / 8).Substring(0, AesKeySize / 8));
            aes.IV = new byte[AesBlockSize / 8];
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        #endregion

        #region RSA 加密解密
        /// <summary>
        /// 生成RSA密钥对
        /// </summary>
        /// <returns></returns>
        public static (string publicKey, string privateKey) GenerateRsaKeys()
        {
            using RSA rsa = RSA.Create();
            rsa.KeySize = 2048;
            return (Convert.ToBase64String(rsa.ExportRSAPublicKey()), Convert.ToBase64String(rsa.ExportRSAPrivateKey()));
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string RsaEncrypt(string plainText, string publicKey)
        {
            using RSA rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            byte[] encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedBytes);
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string RsaDecrypt(string cipherText, string privateKey)
        {
            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
            byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(cipherText), RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        #endregion

        #region SHA 哈希加密
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Sha256Hash(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        #endregion
    }
}
