using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
//�����ַ���,ע��strEncrKey�ĳ���Ϊ8λ(���Ҫ���ӻ��߼���key����,����IV�ĳ��Ⱦ�����) 
//public string DesEncrypt(string strText, string strEncrKey) 

//�����ַ���,ע��strEncrKey�ĳ���Ϊ8λ(���Ҫ���ӻ��߼���key����,����IV�ĳ��Ⱦ�����) 
//public string DesDecrypt(string strText,string sDecrKey) 

//���������ļ�,ע��strEncrKey�ĳ���Ϊ8λ(���Ҫ���ӻ��߼���key����,����IV�ĳ��Ⱦ�����) 
//public void DesEncrypt(string m_InFilePath,string m_OutFilePath,string strEncrKey) 

//���������ļ�,ע��strEncrKey�ĳ���Ϊ8λ(���Ҫ���ӻ��߼���key����,����IV�ĳ��Ⱦ�����) 
//public void DesDecrypt(string m_InFilePath,string m_OutFilePath,string sDecrKey) 

//MD5���� 
//public string MD5Encrypt(string strText) 

namespace e3net.Common.Encrypt
{
    /// <summary>
    /// DES�ԳƼӽ��ܡ�AES RijndaelManaged�ӽ��ܡ�Base64���ܽ��ܡ�MD5���ܵȲ���������
    /// </summary>
    public sealed class EncodeHelper
    {
        /// <summary>
        /// �����ַ������ (��a="abc.." b="12345.." z="a1b2c3..
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string MixString(string a, string b)
        {

            char[] a1, b1;

            a1 = a.ToCharArray();
            b1 = b.ToCharArray();
            int aleng = a1.Length;
            int bleng = b1.Length;
            int Mleng = aleng > bleng ? aleng : bleng;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= Mleng; i++)
            {
                if (i <= aleng - 1)
                {
                    sb.Append(a1[i]);
                }
                if (i <= bleng - 1)
                {
                    sb.Append(b1[i]);
                }

            }
            return sb.ToString();
        }


        /// <summary>
        /// ǰ��һ����� ��λ ���,�����ֽ�����ת16�����ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string YHString(string str)
        {
            bool isEq = str.Length % 2 == 0;

            int tleng = str.Length / 2;
            string af;
            if (isEq)
            {
                af = str.Substring(0, tleng);
            }
            else
            {
                af = str.Substring(0, tleng) + "0";
            }
            string bf = str.Substring(tleng, tleng);
            byte[] aBytes = System.Text.Encoding.Unicode.GetBytes(af);
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(bf);

            byte[] rBytes = new byte[tleng];
            for (int i = 0; i < tleng; i++)
            {
                rBytes[i] = (byte)(aBytes[i] ^ bytes[i]);

            }
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in rBytes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }

        #region Base64���ܽ���
        /// <summary>
        /// Base64��һ�Nʹ��64����λ��Ӌ��������ʹ��2�����η������H����ӡ��ASCII ��Ԫ��
        /// �@ʹ�����Á���������]���Ă�ݔ���a����Base64�е�׃��ʹ����ԪA-Z��a-z��0-9 ��
        /// �@�ӹ���62����Ԫ���Á������_ʼ��64�����֣�����ɂ��Á����锵�ֵķ�̖�ڲ�ͬ��
        /// ϵ�y�ж���ͬ��
        /// Base64����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Encrypt(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Decrypt(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        #endregion

        #region MD5����
        /// <summary> 
        /// MD5 Encrypt 
        /// </summary> 
        /// <param name="strText">text</param> 
        /// <returns>md5 Encrypt string</returns> 
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(strText));
            return Encoding.Default.GetString(result);
        }

        public static string MD5EncryptHash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //the GetBytes method returns byte array equavalent of a string
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] temp = new char[res.Length];
            //copy to a char array which can be passed to a String constructor
            Array.Copy(res, temp, res.Length);
            //return the result as a string
            return new String(temp);
        }

        public static string MD5EncryptHashHex(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //the GetBytes method returns byte array equavalent of a string
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);

            String returnThis = string.Empty;

            for (int i = 0; i < res.Length; i++)
            {
                returnThis += Uri.HexEscape((char)res[i]);
            }
            returnThis = returnThis.Replace("%", "");
            returnThis = returnThis.ToLower();

            return returnThis;
        }

        /// <summary>
        /// MD5 ���μ����㷨.�������: (QQʹ��)
        /// 1. ��֤��תΪ��д
        /// 2. ������ʹ����������������μ��ܺ�,����֤����е���
        /// 3. Ȼ�󽫵��Ӻ�������ٴ�MD5һ��,�õ�������֤���ֵ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EncyptMD5_3_16(string s)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
            byte[] bytes1 = md5.ComputeHash(bytes);
            byte[] bytes2 = md5.ComputeHash(bytes1);
            byte[] bytes3 = md5.ComputeHash(bytes2);

            StringBuilder sb = new StringBuilder();
            foreach (var item in bytes3)
            {
                sb.Append(item.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();
        }
        #endregion


        #region SHA
        /// <summary>
        /// ���ַ�������SHA1����
        /// </summary>
        /// <param name="strIN">��Ҫ���ܵ��ַ���</param>
        /// <returns>����</returns>
        public static string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
        /// <summary>
        /// SHA256���ܣ�������ת
        /// </summary>
        /// <param name="str">string str:�����ܵ��ַ���</param>
        /// <returns>���ؼ��ܺ���ַ���</returns>
        private static string SHA256Encrypt(string str)
        {
            System.Security.Cryptography.SHA256 s256 = new System.Security.Cryptography.SHA256Managed();
            byte[] byte1;
            byte1 = s256.ComputeHash(Encoding.Default.GetBytes(str));
            s256.Clear();
            return Convert.ToBase64String(byte1);//���س���Ϊ44�ֽڵ��ַ���
        }

        /// <summary>
        /// SHA384���ܣ�������ת
        /// </summary>
        /// <param name="str">string str:�����ܵ��ַ���</param>
        /// <returns>���ؼ��ܺ���ַ���</returns>
        private static string SHA384Encrypt(string str)
        {
            System.Security.Cryptography.SHA384 s384 = new System.Security.Cryptography.SHA384Managed();
            byte[] byte1;
            byte1 = s384.ComputeHash(Encoding.Default.GetBytes(str));
            s384.Clear();
            return Convert.ToBase64String(byte1);
        }


        /// <summary>
        /// SHA512���ܣ�������ת
        /// </summary>
        /// <param name="str">string str:�����ܵ��ַ���</param>
        /// <returns>���ؼ��ܺ���ַ���</returns>
        private static string SHA512Encrypt(string str)
        {
            System.Security.Cryptography.SHA512 s512 = new System.Security.Cryptography.SHA512Managed();
            byte[] byte1;
            byte1 = s512.ComputeHash(Encoding.Default.GetBytes(str));
            s512.Clear();
            return Convert.ToBase64String(byte1);
        }

        #endregion



        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptString(string input)
        {
            return MD5Util.AddMD5Profix(Base64Util.Encrypt(MD5Util.AddMD5Profix(input)));
            //return Base64.Encrypt(MD5.AddMD5Profix(Base64.Encrypt(input)));
        }

        /// <summary>
        /// ���ܼӹ��ܵ��ַ���
        /// </summary>
        /// <param name="input"></param>
        /// <param name="throwException">����ʧ���Ƿ����쳣</param>
        /// <returns></returns>
        public static string DecryptString(string input, bool throwException)
        {
            string res = "";
            try
            {
                res = input;// Base64.Decrypt(input);
                if (MD5Util.ValidateValue(res))
                {
                    return MD5Util.RemoveMD5Profix(Base64Util.Decrypt(MD5Util.RemoveMD5Profix(res)));
                }
                else
                {
                    throw new Exception("�ַ����޷�ת���ɹ���");
                }
            }
            catch
            {
                if (throwException)
                {
                    throw;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}