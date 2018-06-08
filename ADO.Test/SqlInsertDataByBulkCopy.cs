using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADO.Test
{
    class SqlInsertDataByBulkCopy
    {
        static System.Timers.Timer timer1 = new System.Timers.Timer();

        static List<TravelStatistics> listAll = new List<TravelStatistics>();
        static void Main()
        {
            #region timer1          
            timer1.Interval = 2000;
            timer1.Elapsed += Timer1_Elapsed;
            timer1.Enabled = true;
            #endregion
         
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StringEatMemory();
            sw.Stop();
            Console.WriteLine(string.Format("Elapsed Time is {0} Milliseconds", sw.ElapsedMilliseconds));
          
            Console.ReadKey();                         
        }

        private static void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {        
            Console.WriteLine("take memory is  "+GetProcessUsedMemory());
        }

        #region 内存消耗测试
        static void Hand1()
        {
            Stopwatch sw = new Stopwatch();
            DataTable dt = GetTableSchema();
            int num = 0;
            for (int i = 1; i <= 30; i++)
            {
                for (int count = num * 400000; count < (num + 1) * 400000; count++)
                {
                    DataRow row = dt.NewRow();
                    row["TerId"] = count;
                    row["SYear"] = 2018;
                    row["SMonth"] = 4;
                    row["SDay"] = i;
                    row["AgentId"] = 885;
                    row["UserId"] = 1197;
                    row["TravelMileages"] = 10;
                    row["TravelIntervals"] = 5;
                    row["TravelNums"] = i;
                    dt.Rows.Add(row);
                }
                sw.Start();
                BulkToDB(dt);
                sw.Stop();
                num++;
                Console.WriteLine(string.Format("Elapsed Time is {0} Milliseconds", sw.ElapsedMilliseconds));

            }
        }

        static void Hand2()
        {
            DataTable dt = GetTableSchema();

            using (DisposeClass disposeModel = new DisposeClass())
            {
                disposeModel.listTravel = new List<TravelStatistics>();
                for (int i = 1; i <= 1200000; i++)
                {
                    #region DataRow
                    DataRow row = dt.NewRow();
                    row["TerId"] = i;
                    row["SYear"] = 2018;
                    row["SMonth"] = 4;
                    row["SDay"] = 10;
                    row["AgentId"] = 885;
                    row["UserId"] = 1197;
                    row["TravelMileages"] = 10;
                    row["TravelIntervals"] = 5;
                    row["TravelNums"] = i;
                    dt.Rows.Add(row);
                    #endregion
                    disposeModel.listTravel.Add(new TravelStatistics()
                    {
                        TerId = i,
                        SYear = 2018,
                        SMonth = 4,
                        SDay = 10,
                        AgentId = 885,
                        UserId = 1197,
                        TravelMileages = 100,
                        TravelIntervals = 10,
                        TravelNums = 5
                    });
                }
                Console.WriteLine(disposeModel.listTravel.Count);
            }

            Console.WriteLine(listAll.Count);
            Console.WriteLine(dt.Rows.Count);
            //GC.Collect();
            //listAll = null;
            //dt = null;
        }


        /// <summary>
        /// 字符串内存消耗
        /// </summary>
        static void StringEatMemory()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string testStr = "";
                for (int i = 0; i < 100; i++)
                {
                    for (int k = 0; k < 10000; k++)
                    {
                        sb.Append("<root><al=\"" + 10000 + "\"></al></root>");
                    //testStr += "<root><al=\"" + 10000 + "\"></al></root>";
                    }
                    Console.WriteLine($"Capacity {sb.Capacity}");
                    Console.WriteLine(sb.ToString().Length);
                    sb.Clear();
                }
                Console.WriteLine(testStr.Length);
                Console.WriteLine(sb.ToString().Length);
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        #endregion




        public static void BulkToDB(DataTable dt)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
            bulkCopy.DestinationTableName = "TB_TravelStatistics";
            bulkCopy.BatchSize = dt.Rows.Count;
            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConn.Close();
                dt.Clear();
                dt.Dispose();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }
        }

        public static DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
                                new DataColumn("TerId",typeof(int)),
                                new DataColumn("SYear",typeof(Int16)),
                                new DataColumn("SMonth",typeof(Int16)),
                                new DataColumn("SDay",typeof(Int16)),
                                new DataColumn("AgentId",typeof(Int32)),
                                new DataColumn("UserId",typeof(Int32)),
                                new DataColumn("TravelMileages",typeof(decimal)),
                                new DataColumn("TravelIntervals",typeof(decimal)),
                                new DataColumn("TravelNums",typeof(Int32))
                            }
            );
            return dt;
        }


        public static double GetProcessUsedMemory()
        {
            double usedMemory = 0;
            Process thisProcess = Process.GetCurrentProcess();
           
            usedMemory = thisProcess.WorkingSet64 / 1024.0 / 1024.0;
            Console.WriteLine(thisProcess.ProcessName);
            return usedMemory;
        }
    }

    public class TravelStatistics
    {
        /// <summary>
        /// 统计里程次数
        /// </summary>
        public int TravelNums { get; set; }


        /// <summary>
        /// 行驶总里程
        /// </summary>
        public decimal TravelMileages { get; set; }

        /// <summary>
        /// 总时长(秒)
        /// </summary>
        public long TravelIntervals { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public long TerId { get; set; }

        public int SYear { get; set; }

        public int SMonth { get; set; }

        public int SDay { get; set; }

        public int UserId { get; set; }

        public int AgentId { get; set; }
    }

}
