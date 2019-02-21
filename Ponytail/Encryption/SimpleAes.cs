using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ponytail.Encryption
{
    /// <summary>
    /// 简单AES 加密解密类
    /// </summary>
    public class SimpleAes
    {
        private Aes _aes { get; set; } = Aes.Create();

        #region 构造器

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="key">密钥字节数组，16个字节</param>
        /// <param name="iv">初始化向量字节数组，16个字节</param>
        public SimpleAes(byte[] key, byte[] iv)
        {
            _aes.Key = key;
            _aes.IV = iv;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="key">密钥字符串，16个字符</param>
        /// <param name="iv">初始化向量字符串，16个字符</param>
        public SimpleAes(string key, string iv)
        {
            _aes.Key = Encoding.Default.GetBytes(key);
            _aes.IV = Encoding.Default.GetBytes(iv);
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 字符串加密成字节数组
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>加密字节数组</returns>
        public byte[] EncryptToBytes(string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plaintext");
            }

            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV

            // Create a decrytot to perform the stream transform.
            ICryptoTransform encrytor = _aes.CreateEncryptor(_aes.Key, _aes.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encrytor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        // Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            // Return the encrypt bytes from the memory stream.
            return encrypted;
        }

        /// <summary>
        /// 字符串加密成字符串
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptToString(string plainText)
        {
           var bytes= EncryptToBytes(plainText);
            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// 加密字节数组解密成字符串
        /// </summary>
        /// <param name="cipherText">密文字节数组</param>
        /// <returns>明文</returns>
        public string DecryptFromBytes(byte[] cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }

        /// <summary>
        /// 字符串解密成字符串
        /// </summary>
        /// <param name="cipherText">密文</param>
        /// <returns></returns>
        public string DecryptFromString(string cipherText)
        {
            var bytes = StringToBytes(cipherText);
           return DecryptFromBytes(bytes);
        }

        #endregion

        #region 私有方法

        // 字符串按字面转换成字节数组
        private byte[] StringToBytes(string s)
        {
            int length = (s.Length + 1) / 3;
            byte[] arr1 = new byte[length];
            for (int i = 0; i < length; i++)
                arr1[i] = Convert.ToByte(s.Substring(3 * i, 2), 16);
            return arr1;
        }

        #endregion

    }
}
