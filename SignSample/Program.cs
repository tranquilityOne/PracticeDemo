using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3net.Common.NetWork;
using e3net.Common.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SignSample
{
    /// <summary>
    /// 签名方法
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //int _timestamp = 12345678;
            //var param = new SortedDictionary<string, string>(new AsciiComparer());
            //param.Add("token", "b38449c7e6e24f6d85a6402bbe740b84");
            //param.Add("machineSn", "123456");
            //param.Add("timeStamp", new DateTime().Ticks.ToString());
            //string _sign = GetSign(param);
            //string urlParam = string.Join("&", param.Select(i => i.Key + "=" + i.Value));
            //string url = "http://api.demo.com/dog/add?" + urlParam + "&sign=" + _sign;
            //Console.Write(url);
            //string content = "大家好啊东方舵手豆腐干放到";
            //Task.Run(() =>
            //{
            //    Console.Write(GetTranslateFromBaiduDu(content));
            //});
            //Console.WriteLine(Convert.ToDouble(69) / 10);
            //Console.ReadKey();

            Console.Write(GetMD5("d862016120100097c2017011890f10650eb244e2fb19a7d23be5f88d4"));
            Console.ReadKey();
            #region 实体验证
            //Animal dd = new Dog();
            //Animal dc = new Animal();
            //Dog dog = new Dog();
            //dd.Show();
            //dc.Show();
            //dog.Show();

            //dd.Show1();
            //dc.Show1();
            //dog.Show1();
            //Console.ReadKey(); 
            #endregion
        }

        /// <summary>
        /// 获取百度翻译内容
        /// </summary>
        static string GetTranslateFromBaiduDu(string text)
        {
            string appId = "20171218000106139";

            string sourceQ = text;
            string q = System.Web.HttpUtility.UrlEncode(sourceQ);//翻译内容
            string from = "zh";//中文
            string to = "cht";//粤语
            string salt = DateTime.Now.Ticks.ToString();//随机数
            string secretKey = "wPEUL5E0mJy8DFRy1tkE";//密钥
            string sign = GetMD5(appId + sourceQ + salt + secretKey).ToLower();

            string url = string.Format("http://api.fanyi.baidu.com/api/trans/vip/translate?q={0}&from={1}&to={2}&appid={3}&salt={4}&sign={5}", q, from, to, appId, salt, sign);
            HttpUtil httpU = new HttpUtil();
            string result = httpU.GetRequest(url);

            JObject jObject = JsonHelper.FromJson(result);
            if (jObject["trans_result"] != null)
            {
                JArray resultArray = JArray.Parse(jObject["trans_result"].ToString());
                JObject objectStr = JObject.Parse(resultArray[0].ToString());
                string str = objectStr["dst"].ToString();
            }
            return result;
        }


        /// <summary>
        /// 获取MD5加密字符()
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            var sb = new StringBuilder();
            var md5 = System.Security.Cryptography.MD5.Create();
            var output = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < output.Length; i++)
                sb.Append(output[i].ToString("X").PadLeft(2, '0'));
            return sb.ToString();
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static string GetSign(SortedDictionary<string, string> paramList, string appKey = "test")
        {
            paramList.Remove("sign");
            StringBuilder sb = new StringBuilder(appKey);
            foreach (var p in paramList)
                sb.Append(p.Key).Append(p.Value);
            sb.Append(appKey);
            return GetMD5(sb.ToString());
        }
    }

    /// <summary>
    /// 基于ASCII码排序规则的String比较器
    /// </summary>
    public class AsciiComparer : System.Collections.Generic.IComparer<string>
    {
        public int Compare(string a, string b)
        {
            if (a == b)
                return 0;
            else if (string.IsNullOrEmpty(a))
                return -1;
            else if (string.IsNullOrEmpty(b))
                return 1;
            if (a.Length <= b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] < b[i])
                        return -1;
                    else if (a[i] > b[i])
                        return 1;
                    else
                        continue;
                }
                return a.Length == b.Length ? 0 : -1;
            }
            else
            {
                for (int i = 0; i < b.Length; i++)
                {
                    if (a[i] < b[i])
                        return -1;
                    else if (a[i] > b[i])
                        return 1;
                    else
                        continue;
                }
                return 1;
            }
        }
    }


    public class Animal
    {    
          public void Show()
          {
              Console.WriteLine("this is the animal");
          }      
          
        public virtual void Show1()
        {
            Console.WriteLine("Animal-Show1");
        }
    }

    public class Dog : Animal
    {
        public new void Show()
        {
            Console.WriteLine("this is a dog");
        }

        public override void Show1()
        {
            Console.WriteLine("dog-show1");
        }
    }
}
