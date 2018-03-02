using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e3net.MongodbServer
{
    /// <summary>
    /// MongoDB实体类的基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 基类对象的ID，MongoDB要求每个实体类必须有的主键  [BsonIgnoreAttribute]  [BsonIdAttribute]  [BsonRepresentation(BsonType.ObjectId)]
        /// </summary>
       [BsonIdAttribute] 
        public string Id { get; set; }
    }
}
