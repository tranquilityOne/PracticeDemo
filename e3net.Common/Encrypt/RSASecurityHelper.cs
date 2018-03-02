using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace e3net.Common.Encrypt
{
    /// <summary>
    /// �ǶԳƼ�����֤������
    /// </summary>
    public class RSASecurityHelper
    {

        /// <summary>
        /// ��Կ
        /// </summary>
        static string PublicKey = "122121";
        /// <summary>
        /// ��ע����Ϣ���ݲ��÷ǶԳƼ��ܵķ�ʽ����
        /// </summary>
        /// <param name="originalString">δ���ܵ��ı����������</param>
        /// <param name="encrytedString">���ܺ���ı�����ע�����к�</param>
        /// <returns>�����֤�ɹ�����True������ΪFalse</returns>
        public static bool Validate(string originalString, string encrytedString)
        {
            bool bPassed = false;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(PublicKey); //��Կ
                    RSAPKCS1SignatureDeformatter formatter = new RSAPKCS1SignatureDeformatter(rsa);
                    formatter.SetHashAlgorithm("SHA1");

                    byte[] key = Convert.FromBase64String(encrytedString); //��֤
                    SHA1Managed sha = new SHA1Managed();
                    byte[] name = sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(originalString));
                    if (formatter.VerifySignature(name, key))
                    {
                        bPassed = true;
                    }
                }
                catch
                {
                }
            }
            return bPassed;
        }


        /// <summary>
        /// HmacSha1  HmacSha2 ǩ��
        /// </summary>
        /// <param name="text">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>���ܺ��</returns>
        public static string HmacSha1Sign(string text, string key)
        {
            Encoding encode = Encoding.UTF8;
            byte[] byteData = encode.GetBytes(text);
            byte[] byteKey = encode.GetBytes(key);
            HMACSHA1 hmac = new HMACSHA1(byteKey);
            CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write);
            cs.Write(byteData, 0, byteData.Length);
            cs.Close();
            return Convert.ToBase64String(hmac.Hash);
        }

    }
}