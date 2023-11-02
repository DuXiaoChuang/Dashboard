using System.Text;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
namespace HFUTIEMES
{
    /**/
    /// <summary>
    /// 对称加密算法类
    /// </summary>
    public class DecryptEncrypt
    {
        /**/
        /// <summary>
        /// 返回自身的一个类
        /// </summary>
        public static DecryptEncrypt MyDecryptEncrypt
        {
            get
            {
                return new DecryptEncrypt();
            }
        }


        private SymmetricAlgorithm mobjCryptoService;
        private string Key;

        /**/
        /// <summary>
        /// 对称加密类的构造函数
        /// </summary>
        internal DecryptEncrypt()
        {
            mobjCryptoService = new RijndaelManaged();
            Key = "rrp(%&h70x89H$jgsfgfsI0456Ftma81&fvHrr&&76*h%(12lJ$lhj!y6&(*jkPer44a";
        }
        class EnDeCryptoClass
        {
        }
        private byte[] GetLegalKey()
        {
            string _TempKey = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (_TempKey.Length > KeyLength)

                _TempKey = _TempKey.Substring(0, KeyLength);
            else if (_TempKey.Length < KeyLength)
                _TempKey = _TempKey.PadRight(KeyLength, '0');
            return ASCIIEncoding.ASCII.GetBytes(_TempKey);
        }
        private byte[] GetLegalIV()
        {
            string _TempIV = "@afetj*Ghg7!rNIfsgr95GUqd9gsrb#GG7HBh(urjj6HJ($jhWk7&!hjjri%$hjk";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (_TempIV.Length > IVLength)
                _TempIV = _TempIV.Substring(0, IVLength);
            else if (_TempIV.Length < IVLength)
                _TempIV = _TempIV.PadRight(IVLength, '0');
            return ASCIIEncoding.ASCII.GetBytes(_TempIV);
        }

        public string Encrypto(string Source)
        {
            if (Source == "")
                return Source;
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            //创建对称加密器对象 

            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            //定义将数据流链接到加密转换的流
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }
        public string Decrypto(string Source)
        {
            if (Source == "")
                return Source;
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            //创建对称解密器对象 中国网管联盟bitsCN.com 
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            //定义将数据流链接到加密转换的流
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        } 

    }
}
