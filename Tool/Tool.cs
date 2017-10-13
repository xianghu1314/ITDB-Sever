using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ITDB.Tool
{
    public static class Tool
    {
        /// <summary>
        /// AES加密密钥
        /// </summary>
        public const string AesKey = "[$#FFFRGB(100)$]]";
        /// <summary>
        /// AES加密向量  16位
        /// </summary>
        public const string AesIv = "XHLX11gre5fgree8";

        public static string ToMD5(this string value)
        {
            var md5Hash=MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public static string ToJson<T>(this T ent)
        {
            var idtc = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" }; //时间处理类
            var set = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include//包含空值
            };
            set.Converters.Add(idtc);

            var reqjson = JsonConvert.SerializeObject(ent, Newtonsoft.Json.Formatting.None, set);
            return Newtonsoft.Json.JsonConvert.SerializeObject(ent, set);
        }
        #region AES CBC 256加密

        /// <summary>
        /// AES CBC 256加密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string AESEncrypt(this string s)
        {
            var aes = new RijndaelManaged();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.FeedbackSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = (new SHA256Managed()).ComputeHash(Encoding.ASCII.GetBytes(AesKey));
            aes.IV = System.Text.Encoding.ASCII.GetBytes(AesIv);
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(s);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return Convert.ToBase64String(encrypted).ToString();
        }
        #endregion

        #region AES CBC 256解密

        /// <summary>
        /// AES CBC 256解密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Key">加密密钥</param>
        /// <param name="IV">加密向量</param>
        /// <returns></returns>
        public static string AESDecrypt(this string s)
        {
            var aes = new RijndaelManaged();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.FeedbackSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = (new SHA256Managed()).ComputeHash(Encoding.ASCII.GetBytes(AesKey));
            aes.IV = System.Text.Encoding.ASCII.GetBytes(AesIv);
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var encrypted = Convert.FromBase64String(s);
            string plaintext;

            using (MemoryStream msDecrypt = new MemoryStream(encrypted))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        #endregion
        

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
