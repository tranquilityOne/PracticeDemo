using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;

namespace MongodbServer
{
    /// <summary>
    /// 老版 MongoDB数据访问 基础层
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoBaseDAL<T> : IMongoBaseDAL<T> where T : class,new()
    {
        public MongoDatabase db;
        public MongoBaseDAL()
        {

            db = new MDBContextFactory().GetDbContext();
        }
        public MongoBaseDAL(string ConnectionName)
        {

            db = new MDBContextFactory().GetDbContext(ConnectionName);
        }
        /// <summary>
        /// 获取某个表
        /// </summary>
        /// <returns></returns>
        private MongoCollection<T> GetDbCollection()
        {

            return db.GetCollection<T>(typeof(T).FullName);

        }

        #region 1.0 新增 实体 +int Add(T model)
        /// <summary>
        /// 新增 实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(T model)
        {
            GetDbCollection().Save(model);

            return 1;
        }


        public T LoadById(String Id)
        {
            return GetDbCollection().FindOne(Query.EQ("_id", Id));
        }

        #endregion

        #region 2.0 根据 id 删除 +int Del(T model)
        /// <summary>
        /// 根据 id 删除
        /// </summary>
        /// <param name="model">包含要删除id的对象</param>
        /// <returns></returns>
        public int Del(String Id)
        {

            GetDbCollection().Remove(Query.EQ("_id", Id));
            return 1;
        }
        #endregion

        #region 3.0 根据条件删除 +int DelBy(Expression<Func<T, bool>> delWhere)
        /// <summary>
        /// 3.0 根据条件删除
        /// </summary>
        /// <param name="delWhere"></param>
        /// <returns></returns>
        public int DelByIdList(List<String> IdList)
        {



            //3.2将要删除的数据 用删除方法添加到 EF 容器中
            IdList.ForEach(u =>
            {


                if (!String.IsNullOrEmpty(u))
                {
                    GetDbCollection().Remove((Query.EQ("_id", u)));//标识为 删除 状态
                }




            });
            //3.3一次性 生成sql语句到数据库执行删除
            return IdList.Count;
        }




        #endregion

        #region 4.0 修改 +int Modify(T model, params string[] proNames)
        /// <summary>
        /// 4.0 修改，如：
        /// T u = new T() { uId = 1, uLoginName = "asdfasdf" };
        /// this.Modify(u, "uLoginName");
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的 属性 名称</param>
        /// <returns></returns>
        public int Modify(T model)
        {
            GetDbCollection().Save(model);//标识为 删除 状态
            return 1;
        }
        #endregion

        //#region 4.0 批量修改 +int Modify(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        ///// <summary>
        ///// 4.0 批量修改
        ///// </summary>
        ///// <param name="model">要修改的实体对象</param>
        ///// <param name="whereLambda">查询条件</param>
        ///// <param name="proNames">要修改的 属性 名称</param>
        ///// <returns></returns>
        //public int ModifyBy(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        //{
        //    //4.1查询要修改的数据
        //    List<T> listModifing = GetDbCollection().AsQueryable().Where(whereLambda).ToList();

        //    //获取 实体类 类型对象
        //    Type t = typeof(T); // model.GetType();
        //    //获取 实体类 所有的 公有属性
        //    List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        //    //创建 实体属性 字典集合
        //    Dictionary<string, PropertyInfo> dictPros = new Dictionary<string, PropertyInfo>();
        //    //将 实体属性 中要修改的属性名 添加到 字典集合中 键：属性名  值：属性对象
        //    proInfos.ForEach(p =>
        //    {
        //        if (modifiedProNames.Contains(p.Name))
        //        {
        //            dictPros.Add(p.Name, p);
        //        }
        //    });

        //    //4.3循环 要修改的属性名
        //    foreach (string proName in modifiedProNames)
        //    {
        //        //判断 要修改的属性名是否在 实体类的属性集合中存在
        //        if (dictPros.ContainsKey(proName))
        //        {
        //            //如果存在，则取出要修改的 属性对象
        //            PropertyInfo proInfo = dictPros[proName];
        //            //取出 要修改的值
        //            object newValue = proInfo.GetValue(model, null); //object newValue = model.uName;

        //            //4.4批量设置 要修改 对象的 属性
        //            foreach (T usrO in listModifing)
        //            {
        //                //为 要修改的对象 的 要修改的属性 设置新的值
        //                proInfo.SetValue(usrO, newValue, null); //usrO.uName = newValue;
        //            }
        //        }
        //    }
        //    //4.4一次性 生成sql语句到数据库执行
        //    return db.SaveChanges();
        //}
        //#endregion

        #region 5.0 根据条件查询 +List<T> GetListBy(Expression<Func<T,bool>> whereLambda)
        /// <summary>
        /// 5.0 根据条件查询 +List<T> GetListBy(Expression<Func<T,bool>> whereLambda)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public List<T> GetListBy(Expression<Func<T, bool>> whereLambda)
        {
            return GetDbCollection().AsQueryable().Where(whereLambda).ToList();
        }
        #endregion

        #region 5.1 根据条件 排序 和查询 + List<T> GetListBy<TKey>
        /// <summary>
        /// 5.1 根据条件 排序 和查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">查询条件 lambda表达式</param>
        /// <param name="orderLambda">排序条件 lambda表达式</param>
        /// <returns></returns>
        public List<T> GetListBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda)
        {
            //List<int> listIds = new List<int>() { 1, 2, 3 };
            //new MODEL.OuOAEntities().Ou_UserInfo.Where(u => listIds.Contains(u.uId));
            return GetDbCollection().AsQueryable().Where(whereLambda).OrderBy(orderLambda).ToList();
        }
        #endregion

        #region 6.0 分页查询 + List<T> GetPagedList<TKey>
        /// <summary>
        /// 6.0 分页查询 + List<T> GetPagedList<TKey>
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排序 lambda表达式</param>
        /// <returns></returns>
        public List<T> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy)
        {
            // 分页 一定注意： Skip 之前一定要 OrderBy
            return GetDbCollection().AsQueryable().Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }



        public int Update(string _id, UpdateBuilder updateSet)
        {

            GetDbCollection().Update(Query.EQ("_id", _id), updateSet);


            return 1;
        }
        public int Remove(IMongoQuery query)
        {
            GetDbCollection().Remove(query);

            return 1;
        }

        #endregion
    }
}
