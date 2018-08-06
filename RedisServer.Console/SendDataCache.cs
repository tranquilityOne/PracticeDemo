using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisServer;
using System.Configuration;

namespace RedisServer.Console
{     
    /// 指令数据缓存处理
    /// </summary>
    public class SendDataCache
    {
        static string RedisServerFlag = ConfigurationManager.AppSettings["RedisServerFalg"] +"_";

        /// <summary>
        /// 消息加入缓存
        /// </summary>
        /// <param name="sendEntity">缓存消息实体</param>
        public static void SetSendData(TerResultEntiy sendEntity)
        {
            try
            {
                var listTer = HashOperator.Get<List<TerResultEntiy>>(RedisServerFlag + sendEntity.IMEI);
                if (listTer != null)
                {
                    var singleModel = listTer.SingleOrDefault(p => p.OrderLogID == sendEntity.OrderLogID);
                    //如果存在,先删除当前指令
                    if (singleModel != null)
                    {
                        listTer.Remove(singleModel);
                    }
                }
                else
                {
                    listTer = new List<TerResultEntiy>();                    
                }
                listTer.Add(sendEntity);
                HashOperator.Set<List<TerResultEntiy>>(RedisServerFlag + sendEntity.IMEI, listTer);                
            }
            catch (Exception)
            {
                throw;
            }          
        }

        /// <summary>
        /// 读取缓存数据
        /// </summary>
        /// <param name="IMEI">IMEI号</param>
        /// <returns>null or list</returns>
        public static List<TerResultEntiy> GetSendData(string IMEI)
        {
            try
            {
                return HashOperator.Get<List<TerResultEntiy>>(RedisServerFlag + IMEI); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 移除缓存数据
        /// </summary>
        /// <param name="sendEntity">缓存消息实体</param>
        /// <returns></returns>
        public static void RemoveSendData(TerResultEntiy sendEntity)
        {
            try
            {
                var listTer = HashOperator.Get<List<TerResultEntiy>>(RedisServerFlag + sendEntity.IMEI);
                if (listTer != null)
                {
                    //仅剩下一条数据,移除整个key
                    if (listTer.Count == 1)
                    {
                         HashOperator.Remove(RedisServerFlag + sendEntity.IMEI);
                    }
                    else
                    {
                        //移除指定指令数据
                        var singleModel = listTer.SingleOrDefault(p => p.OrderLogID == sendEntity.OrderLogID);
                        if (singleModel != null)
                        {
                            listTer.Remove(singleModel);
                            HashOperator.Set<List<TerResultEntiy>>(RedisServerFlag + sendEntity.IMEI, listTer);
                        } 
                    }
                }                       
            }
            catch (Exception)
            {
                throw; 
            }
        }
    }
}
