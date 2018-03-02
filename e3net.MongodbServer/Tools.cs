using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e3net.MongodbServer
{
    public static class Tools<T>
    {
        public static FieldsDocument getDisplayFiles(Dictionary<string, int> fields)
        {
            FieldsDocument fd = new FieldsDocument();
            fd.AddRange(fields);
            return fd;
        }
        public static SortDefinition<T> getSortDefinition(Dictionary<string, int> sortfields)
         {
             SortDefinition<T> sd = null;
             foreach (var item in sortfields)
             {
                 if (sd == null)
                 {
                     if (item.Value == 1 )//1为升序 0为降序
                     {
                         sd = Builders<T>.Sort.Ascending(item.Key);
                     }
                   else
                     {
                         sd = Builders<T>.Sort.Descending(item.Key);
                     }
                 }
                 else
                 {
                     if (item.Value == 1)//1为升序 0为降序
                     {
                         sd.Ascending(item.Key);
                     }
                     else
                     {
                         sd.Descending(item.Key);
                     }
                 }
             }
             return sd;
         }
        public static UpdateDefinition<T> getUpdateDefinition(Dictionary<string, object> updatedic)
        {
            UpdateDefinition<T> ud = null;
            foreach (var item in updatedic)
            {
                if (ud == null)
                {
                    ud = Builders<T>.Update.Set(item.Key, item.Value);
                }
                else
                {
                    ud.Set(item.Key, item.Value);
                }
            }
            return ud;
        }
    }
}
