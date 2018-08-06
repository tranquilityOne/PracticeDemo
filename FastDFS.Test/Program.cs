using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastDFS.Client;
using System.Net;
using System.IO;
using FastDFS.Client.Common;
using System.Configuration;
using e3net.MongodbServer;

namespace FastDFS.Test
{
    class Program
    {
        static StorageNode Node;//获取对应存储服务的节点
        static readonly string GroupName = "group1";//默认文件文件分组名
        static int loop = 100;
        static readonly string WebHtpp = "http://192.168.30.38";
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("---------u  上传文件------d 清空所有文件---- t 测试并行下载");
                while (true)
                {
                    var input = Console.ReadLine().ToLower();
                    MG_BaseDAL<FastDFSData> dal = null;
                    switch (input)
                    {
                        case "u":
                            var filePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\\test.jpg";
                            FastDfsFile fsFile;
                            List<FastDFSData> listSave = new List<FastDFSData>();
                            for (int i = 0; i < loop; i++)
                            {
                                fsFile = FastDfsHelper.Upload(filePath);
                                if (fsFile != null)
                                {
                                    listSave.Add(new FastDFSData()
                                    {
                                        Id = Path.GetFileNameWithoutExtension(fsFile.FileName).TrimEnd('.'),
                                        DownLoadHost = WebHtpp,
                                        AbosoluteUrl = fsFile.ToString(),
                                        CreateTime = DateTime.Now,
                                        FileExt = Path.GetExtension(fsFile.FileName).Trim('.'),
                                        FileName = Path.GetFileName(fsFile.FileName)
                                    });
                                }
                            }
                            dal = new MG_BaseDAL<FastDFSData>();
                            dal.InsertBatch(listSave);
                            Console.WriteLine($"成功导入数据{listSave.Count}");
                            break;
                        case "d":
                            dal = new MG_BaseDAL<FastDFSData>();
                            List<FastDFSData> listAll = dal.Find(p=>p.Id!="").ToList();                          
                            foreach (var item in listAll)
                            {
                                FastDfsHelper.Remove(FastDfsHelper.Config.GroupName, item.AbosoluteUrl.Substring(8));
                            }
                            dal.DeleteByExpressionM(p=>!string.IsNullOrEmpty(p.Id));
                            Console.WriteLine("清理完成！");
                            break;
                        case "t":
                            //并行测试直接下载
                            try
                            {
                                var savePath = ConfigurationManager.AppSettings["SavePath"];
                                dal = new MG_BaseDAL<FastDFSData>();
                                List<FastDFSData> listTest = dal.Find(p => !string.IsNullOrEmpty(p.Id)).ToList();
                                int directoryIndex = 0;
                                int excuteNum = 0;
                                for (int i = 0; i < 1; i++)
                                {
                                    directoryIndex = i / 10;
                                    if (!Directory.Exists(savePath + $@"{directoryIndex}"))
                                        Directory.CreateDirectory(savePath + $@"{directoryIndex}");
                                    ParallelLoopResult result = Parallel.For(0, 1, (k, state) =>
                                    {
                                        using (WebClient web = new WebClient())
                                        {
                                            web.DownloadFile(listTest[i].DownLoadHost + listTest[i].AbosoluteUrl, savePath + $@"{directoryIndex}\{Guid.NewGuid().ToString()}.json");
                                            System.Threading.Interlocked.Increment(ref excuteNum);
                                            Console.WriteLine("excuting..."+excuteNum);
                                        }
                                    });
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                                                  
                            break;                  
                    }
                }
                            
              
                //Console.WriteLine("masterFileName is " + masterFileName + "\r\n slavefileName  is " + slavefileName + "");

            }
            catch (Exception ex)
            {
                Console.WriteLine("error :"+ex.Message);
            }          
            Console.ReadKey();
        }

        #region 废弃(旧逻辑)
        /// <summary>
        /// 废弃
        /// </summary>
        static void Init()
        {
            try
            {
                var trackerIPs = new List<IPEndPoint>();
                var strIPs = ConfigurationManager.AppSettings["TrackerIPS"];
                if (!string.IsNullOrEmpty(strIPs))
                {
                    var strArray = strIPs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        var strArray_i = strArray[i].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        trackerIPs.Add(new IPEndPoint(IPAddress.Parse(strArray_i[0]), Convert.ToInt32(strArray_i[1])));
                    }
                }
                ConnectionManager.Initialize(trackerIPs);
                Node = FastDFSClient.GetStorageNode("group1");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        static string GetFileNameByUpload(string filePath, string fileExt)
        {
            byte[] content = null;
            var returnFileName = string.Empty;
            try
            {
                if (File.Exists(filePath))
                {
                    using (FileStream fsRead = new FileStream(filePath, FileMode.Open))
                    {
                        using (BinaryReader reader = new BinaryReader(fsRead))
                        {
                            content = reader.ReadBytes((int)fsRead.Length);
                        }
                        returnFileName = FastDFSClient.UploadFile(Node, content, fileExt);
                    }
                }
            }
            catch (Exception)
            {
                returnFileName = "";
            }
            return returnFileName;
        } 
        #endregion


    }



    public class FastDFSData : BaseEntity
    {
         /// <summary>
         /// 下载链接主机
         /// </summary>
         public string DownLoadHost { get; set; }
         
         /// <summary>
         /// 文件绝对Url
         /// </summary>
         public string AbosoluteUrl { get; set; }
         
         /// <summary>
         /// 文件名
         /// </summary>
         public string FileName { get; set; }    

         /// <summary>
         /// 文件扩展名
         /// </summary>                   
         public string FileExt { get; set; }
         /// <summary>
         /// 创建时间
         /// </summary>
         public DateTime CreateTime { get; set; }

    }
}
