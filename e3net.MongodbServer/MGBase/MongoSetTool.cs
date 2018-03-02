using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace e3net.MongodbServer.MGBase
{
    public class MongoSetTool
    {

        /// <summary>
        /// 值分隔符 and
        /// </summary>
        public const string spAndStr = "&█&";
        /// <summary>
        /// 值分隔符 or
        /// </summary>
        public const string spOrStr = "|█|";
        /// <summary>
        /// sql 拼接 转成mongodb 条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlSet"></param>
        /// <returns></returns>
        public static FilterDefinition<T> GetSql<T>(string sqlSet) where T : new()
        {

            //name_op:value█
            if (!string.IsNullOrEmpty(sqlSet))
            {
                string[] data = sqlSet.Split('█');
                List<FilterDefinition<T>> list = new List<FilterDefinition<T>>();

                for (int i = 0; i < data.Length; i++)
                {
                    int index = data[i].IndexOf(":");
                    var nameData = data[i].Substring(0, index);

                    string[] name = nameData.Split('_');
                    string value = data[i].Substring(index + 1);
                    list.Add(GetOP<T>(name[0], name[1], value));

                }
                return Builders<T>.Filter.And(list);

            }
            else
            {

                return Builders<T>.Filter.Empty;
            }
        }

        /// <summary>
        /// sql 拼接 转成mongodb 条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlSet"></param>
        /// <returns></returns>
        public static List<FilterDefinition<T>> GetSql2<T>(string sqlSet) where T : new()
        {
            List<FilterDefinition<T>> list = new List<FilterDefinition<T>>();
            //name_op:value█
            if (!string.IsNullOrEmpty(sqlSet))
            {
                string[] data = sqlSet.Split('█');
                for (int i = 0; i < data.Length; i++)
                {
                    int index = data[i].IndexOf(":");
                    var nameData = data[i].Substring(0, index);

                    string[] name = nameData.Split('_');
                    string value = data[i].Substring(index + 1);
                    list.Add(GetOP<T>(name[0], name[1], value));

                }
            }
            return list;
        }

        /// <summary>
        /// 单个条件生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="op"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static FilterDefinition<T> GetOP<T>(string name, string op, string values) where T : new()
        {


            #region  多字段查询 or  如： OwnerName|OwnerCode|BuildingCode|HouseCode
            string[] names = name.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length > 1)
            {
                List<FilterDefinition<T>> list = new List<FilterDefinition<T>>();
                for (int i = 0; i < names.Length; i++)
                {
                    list.Add(GetOP<T>(names[i], op, values));//使用表达式
                }
                return Builders<T>.Filter.Or(list);
            }

            #endregion

            #region  多字段  and  如： OwnerName&OwnerCode&BuildingCode&HouseCode
            string[] names2 = name.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            if (names2.Length > 1)
            {
                List<FilterDefinition<T>> list = new List<FilterDefinition<T>>();

                for (int i = 0; i < names.Length; i++)
                {
                    list.Add(GetOP<T>(names2[i], op, values));//使用表达式
                }
                return Builders<T>.Filter.And(list);
            }
            #endregion

            #region  单字段 多值  or  如： 1|█|2|█|3|█|4
            //string[] valuesSet = values.Split(new string[] { spOrStr }, StringSplitOptions.RemoveEmptyEntries);
            //if (valuesSet.Length > 1)
            //{
            //    List<FilterDefinition<T>> list = new List<FilterDefinition<T>>();

            //    for (int i = 0; i < names.Length; i++)
            //    {
            //        list.Add(GetOP<T>(name, op, valuesSet[i]));//使用表达式
            //    }
            //    return Builders<T>.Filter.Or(list);
            //}
            #endregion

            #region  单字段 多值  and  如： 1&█&2&█&3&█&4
            //string[] valuesSet2 = values.Split(new string[] { spAndStr }, StringSplitOptions.RemoveEmptyEntries);
            //if (valuesSet2.Length > 1)
            //{
            //    List<FilterDefinition<T>> list = new List<FilterDefinition<T>>();

            //    for (int i = 0; i < names.Length; i++)
            //    {
            //        list.Add(GetOP<T>(name, op, valuesSet2[i]));//使用表达式
            //    }
            //    return Builders<T>.Filter.And(list);
            //}
            #endregion

            switch (op)
            {
                case "like"://all
                    Regex regexlike = new Regex(values);//包含
                    return Builders<T>.Filter.Regex(name, new BsonRegularExpression(regexlike));//使用表达式
                case "like1":// 前固定
                    Regex regexlike1 = new Regex("^" + values + "");//以开头
                    return Builders<T>.Filter.Regex(name, new BsonRegularExpression(regexlike1));//使用表达式
                case "like2"://后固定
                    Regex regexlike2 = new Regex("" + values + "$");//以开头
                    return Builders<T>.Filter.Regex(name, new BsonRegularExpression(regexlike2));//使用表达式

                case "eq":
                    return Builders<T>.Filter.Eq(name, values);
                case "lt":
                    return Builders<T>.Filter.Lt(name, values);
                case "le":
                    return Builders<T>.Filter.Lte(name, values);
                case "gt":
                    return Builders<T>.Filter.Gt(name, values);
                case "ge":
                    return Builders<T>.Filter.Gte(name, values);
                case "ne":
                    return Builders<T>.Filter.Ne(name, values);
                default:
                    return "";

            }
        }



        /// <summary>
        /// 单个条件生成 isAnd  true 是与 false 是或
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">查询的列</param>
        /// <param name="op">操作</param>
        /// <param name="values"></param>
        /// <param name="isAnd">true 是与 false 是或</param>
        /// <returns></returns>
        public static FilterDefinition<T> GetOP<T>(string name, string op, string[] values, bool isAnd = false) where T : new()
        {

            List<FilterDefinition<T>> list = new List<FilterDefinition<T>>();
            for (int i = 0; i < values.Length; i++)
            {
                list.Add(GetOP<T>(name, op, values[i]));//使用表达式
            }
            if (isAnd)
            {
                return Builders<T>.Filter.And(list);
            }
            else
            {
                return Builders<T>.Filter.Or(list);
            }
        }
    }
}
