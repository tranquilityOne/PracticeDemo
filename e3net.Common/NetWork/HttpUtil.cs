using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;

namespace e3net.Common.NetWork
{
    /// <summary>
    /// 网络工具类。
    /// </summary>
    public sealed class HttpUtil
    {
        private int _timeout = 40000;

        /// <summary>
        /// 请求与响应的超时时间
        /// </summary>
        public int Timeout
        {
            get { return this._timeout; }
            set { this._timeout = value; }
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
        public string PostRequest(string url, IDictionary<string, string> parameters)
        {
            var cookieContainer = new CookieContainer();
            return PostRequest(url, parameters, ref cookieContainer);
        }


        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求数据</param>
        /// <returns>HTTP响应</returns>
        public string PostRequest(string url, string jsonData)
        {
            HttpWebRequest req = GetWebRequest(url, "POST");
            req.ContentType = "application/json";
            byte[] postData = Encoding.UTF8.GetBytes(jsonData);
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            Encoding encoding = null;
            if (rsp.CharacterSet != null && rsp.CharacterSet != "")
                encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        public string PostRequest(string url, IDictionary<string, string> parameters, ref CookieContainer cookieContainer)
        {
            HttpWebRequest req = GetWebRequest(url, "POST");
            if (cookieContainer != null && cookieContainer.Count > 0)
            {
                req.CookieContainer = cookieContainer;
            }
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters));
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();

            Encoding encoding = null;
            if (rsp.CharacterSet != null && rsp.CharacterSet != "")
                encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        public string PostRequest(string url, IDictionary<string, string> parameters, ref CookieContainer cookieContainer, IWebProxy proxy)
        {
            return PostRequest(url, parameters, ref cookieContainer, proxy, "");
        }

        public string PostRequest(string url, IDictionary<string, string> parameters, ref CookieContainer cookieContainer, IWebProxy proxy, string referer)
        {
            HttpWebRequest req = GetWebRequest(url, "POST");
            if (cookieContainer != null && cookieContainer.Count > 0)
            {
                req.CookieContainer = cookieContainer;
            }
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            if (proxy != null)
                req.Proxy = proxy;
            if (!string.IsNullOrEmpty(referer))
            {
                req.Referer = referer;
            }
            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters));
            HttpWebResponse rsp = null;
            try
            {
                System.IO.Stream reqStream = req.GetRequestStream();
                reqStream.Write(postData, 0, postData.Length);
                reqStream.Close();
                rsp = (HttpWebResponse)req.GetResponse();
            }
            catch (Exception ex)
            {


            }
            if (rsp != null)
            {
                //HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                return GetResponseAsString(rsp, encoding);
            }
            else
                return string.Empty;
        }

        //------------------------------
        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
        public string GetRequest(string url, IDictionary<string, string> parameters)
        {
            var cookieContainer = new CookieContainer();
            return GetRequest(url, parameters, ref cookieContainer);
        }

        public string GetRequest(string url, IDictionary<string, string> parameters, ref CookieContainer cookieContainer)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters);
                }
                else
                {
                    url = url + "?" + BuildQuery(parameters);
                }
            }

            HttpWebRequest req = GetWebRequest(url, "GET");
            if (cookieContainer != null && cookieContainer.Count > 0)
            {
                req.CookieContainer = cookieContainer;

            }
            HttpWebResponse rsp = null;
            Encoding encoding = null;
            try
            {
                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                req.Timeout = 6000;
                req.KeepAlive = false;
                rsp = (HttpWebResponse)req.GetResponse();
                encoding = Encoding.GetEncoding(rsp.CharacterSet);
            }
            catch (Exception)
            {

            }

            return GetResponseAsString(rsp, encoding);
        }

        public string GetRequest(string url)
        {
            HttpWebRequest req = null;
            HttpWebResponse rsp = null;
            Encoding encoding = null;
            try
            {
                req = GetWebRequest(url, "GET");
                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                req.Timeout = 5000;//5秒超时
                req.KeepAlive = false;
                rsp = (HttpWebResponse)req.GetResponse();
                encoding = Encoding.GetEncoding(rsp.CharacterSet);
            }
            catch (Exception)
            {
                //return "";
            }

            string res = GetResponseAsString(rsp, encoding);
            if (req != null)
            {
                req.Abort();
            }
            return res;
        }

        //-----------------------------
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        { //直接确认，否则打不开
            return true;
        }

        public HttpWebRequest GetWebRequest(string url, string method)
        {
            HttpWebRequest req = null;
            if (url.Contains("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                req = (HttpWebRequest)WebRequest.Create(url);
            }

            req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            //req.KeepAlive = true;
            req.Timeout = this._timeout;

            return req;
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            if (rsp == null)
            {
                return string.Empty;
            }
            System.IO.Stream stream = null;
            StreamReader reader = null;
            var s = string.Empty;
            try
            {
                if (encoding == null)
                    encoding = Encoding.UTF8;
                // 以字符流的方式读取HTTP响应
                using (rsp)
                {
                    stream = rsp.GetResponseStream();
                    reader = new StreamReader(stream, encoding);
                    s = reader.ReadToEnd();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null)
                {

                    rsp.Close();
                    rsp = null;
                }
            }
            return s;
        }

        /// <summary>
        /// 组装GET请求URL。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>带参数的GET请求URL</returns>
        public string BuildGetUrl(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters);
                }
                else
                {
                    url = url + "?" + BuildQuery(parameters);
                }
            }
            return url;
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> parameters)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            if (parameters != null)
            {
                IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
                while (dem.MoveNext())
                {
                    string name = dem.Current.Key;
                    string value = dem.Current.Value;
                    // 忽略参数名或参数值为空的参数
                    //if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                    //{
                    if (hasParam)
                    {
                        postData.Append("&");
                    }

                    postData.Append(name);
                    postData.Append("=");
                    postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    hasParam = true;
                    //}
                }
            }

            return postData.ToString();
        }

        /// <summary>  
        /// 使用Post方法获取字符串结果  
        /// </summary>  
        /// <param name="url"></param>  
        /// <param name="formItems">Post表单内容</param>  
        /// <param name="cookieContainer"></param>  
        /// <param name="timeOut">默认20秒</param>  
        /// <param name="encoding">响应内容的编码类型（默认utf-8）</param>  
        /// <returns></returns>  
        public static string PostForm(string url, List<FormItemModel> formItems, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 20000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            #region 初始化请求对象
            request.Method = "POST";
            request.Timeout = timeOut;
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //request.KeepAlive = true;
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            if (!string.IsNullOrEmpty(refererUrl))
                request.Referer = refererUrl;
            if (cookieContainer != null)
                request.CookieContainer = cookieContainer;
            #endregion

            string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符  
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            //请求流  
            var postStream = new MemoryStream();
            #region 处理Form表单请求内容
            //是否用Form上传文件  
            var formUploadFile = formItems != null && formItems.Count > 0;
            if (formUploadFile)
            {
                //文件数据模板  
                string fileFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                    "\r\nContent-Type: application/octet-stream" +
                    "\r\n\r\n";
                //文本数据模板  
                string dataFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                    "\r\n\r\n{1}";
                foreach (var item in formItems)
                {
                    string formdata = null;
                    if (item.IsFile)
                    {
                        //上传文件  
                        formdata = string.Format(
                            fileFormdataTemplate,
                            item.Key, //表单键  
                            item.FileName);
                    }
                    else
                    {
                        //上传文本  
                        formdata = string.Format(
                            dataFormdataTemplate,
                            item.Key,
                            item.Value);
                    }

                    //统一处理  
                    byte[] formdataBytes = null;
                    //第一行不需要换行  
                    if (postStream.Length == 0)
                        formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                    else
                        formdataBytes = Encoding.UTF8.GetBytes(formdata);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);

                    //写入文件内容  
                    if (item.FileContent != null && item.FileContent.Length > 0)
                    {
                        using (var stream = item.FileContent)
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                //结尾  
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream.Length;

            #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;
                //直接写入流  
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                ////debug  
                //postStream.Seek(0, SeekOrigin.Begin);  
                //StreamReader sr = new StreamReader(postStream);  
                //var postStr = sr.ReadToEnd();  
                postStream.Close();//关闭文件访问  
            }
            #endregion

            //发送
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            //获取返回值
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }
        public byte[] GetImageBytes(string url, ref CookieContainer cookieContainer)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            try
            {
                request.CookieContainer = cookieContainer;
                WebResponse webres = request.GetResponse();
                Stream stream = webres.GetResponseStream();
                //byte[] bytes = new byte[stream.Length];
                //stream.Read(bytes, 0, bytes.Length);
                //stream.Seek(0, SeekOrigin.Begin);
                //stream.Close();
                //return bytes;

                Image image;
                image = Image.FromStream(stream);
                stream.Close();

                MemoryStream ms = new MemoryStream();
                image.Save(ms, image.RawFormat);
                //byte[] bytes = new byte[ms.Length];  
                //ms.Read(bytes, 0, Convert.ToInt32(ms.Length));  
                //以上两句改成下面两句  
                byte[] bytes = ms.ToArray();
                ms.Close();
                return bytes;
            }
            catch
            {
                return null;
            }
            //return image;
        }

        /// <summary>
        /// 通过Url获取文件流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public byte[] GetFileBytes(string url)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            try
            {
                WebResponse webres = request.GetResponse();
                using (Stream stream = webres.GetResponseStream())
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyToAsync(ms);
                        byte[] bytes = new byte[ms.Length];
                        ms.Read(bytes, 0, bytes.Length);
                        ms.Seek(0, SeekOrigin.Begin);
                        ms.Close();
                        return bytes;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }



    /// <summary>  
    /// 表单数据项  
    /// </summary>  
    public class FormItemModel
    {
        /// <summary>  
        /// 表单键，request["key"]  
        /// </summary>  
        public string Key { set; get; }
        /// <summary>  
        /// 表单值,上传文件时忽略，request["key"].value  
        /// </summary>  
        public string Value { set; get; }
        /// <summary>  
        /// 是否是文件  
        /// </summary>  
        public bool IsFile
        {
            get
            {
                if (FileContent == null || FileContent.Length == 0)
                    return false;

                if (FileContent != null && FileContent.Length > 0 && string.IsNullOrWhiteSpace(FileName))
                    throw new Exception("上传文件时 FileName 属性值不能为空");
                return true;
            }
        }
        /// <summary>  
        /// 上传的文件名  
        /// </summary>  
        public string FileName { set; get; }
        /// <summary>  
        /// 上传的文件内容  
        /// </summary>  
        public Stream FileContent { set; get; }
    }  
}
