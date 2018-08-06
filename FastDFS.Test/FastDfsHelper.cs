using FastDFS.Client;
using FastDFS.Client.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastDFS.Test
{
    public static class FastDfsHelper
    {
        public static readonly FastDfsConfig Config;
        static FastDfsHelper()
        {
            Config = FastDfsManager.GetConfigSection();
            ConnectionManager.InitializeForConfigSection(Config);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="contentByte">文件数组</param>
        /// <param name="fileExt">文件拓展名</param>
        /// <returns></returns>
        public static FastDfsFile Upload(byte[] contentByte, string fileExt)
        {
            var node = FastDFSClient.GetStorageNode(Config.GroupName);
            return new FastDfsFile(Config.GroupName, FastDFSClient.UploadFile(node, contentByte, fileExt.Trim('.')));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="fileExt">文件拓展名</param>
        /// <returns></returns>
        public static FastDfsFile Upload(Stream stream, string fileExt)
        {
            using (stream)
            {
                byte[] contentByte = new byte[stream.Length];
                stream.Read(contentByte, 0, contentByte.Length);
                return Upload(contentByte, fileExt);
            }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FastDfsFile Upload(string fileName)
        {           
            using (FileStream fsRead = new FileStream(fileName, FileMode.Open))
            {
                byte[] content = new byte[] { };
                using (BinaryReader reader = new BinaryReader(fsRead))
                {
                    content = reader.ReadBytes((int)fsRead.Length);
                }
                return Upload(content, Path.GetExtension(fileName).Trim('.'));
            }
        }
        /// <summary>
        /// 将数据流读取成byte数组，并在读取完成后将流当前位置设置为0
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 移除文件
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="fileName"></param>
        public static void Remove(string groupName, string fileName)
        {
            FastDFSClient.RemoveFile(groupName, fileName);
        }

    }

    /// <summary>
    /// Dfs文件
    /// </summary>
    public class FastDfsFile
    {
        /// <summary>
        /// Dfs群组名
        /// </summary>
        public string GroupName { get; private set; }
        /// <summary>
        /// Dfs文件路径（包含文件名）
        /// </summary>
        public string FileName { get; private set; }

        public FastDfsFile(string groupName, string fileName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentNullException("groupName can not be null.");
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("fileName can not be null.");
            }
            this.GroupName = groupName;
            this.FileName = fileName;
        }
        /// <summary>
        /// 获取文件在服务器上访问的路径（不包含域名部分）
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}/{1}", this.GroupName, this.FileName);
        }
    }
}
