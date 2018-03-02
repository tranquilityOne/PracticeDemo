using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using e3net.Common.Entity;
namespace e3net.MongodbServer
{
    /// <summary>
    /// mongoDB数据访问层实现 补充拓展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MG_BaseDAL<T> : MongoHelper<T>, IMG_BaseDAL<T> where T :BaseEntity,new()
    {

        #region 属性
        /// <summary>
        /// 排序字段
        /// </summary>
        string SortPropertyName = "_id";
        /// <summary>
        /// 是否降
        /// </summary>
        bool IsDescending = true;

        #endregion

        #region 查询单个
        /// <summary>
        /// 指定ID的对象  (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual T FindByIDM(string id)
        {
            return collection.Find(t=>t.Id==id).FirstOrDefault();
        }


        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象（异步） (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual async Task<T> FindByIDAsyncM(string id)
        {
            return await collection.FindAsync(s => s.Id == id).Result.FirstOrDefaultAsync();
        }




        #endregion

        #region 返回可查询的记录源
        /// <summary>
        /// 返回可查询的记录源
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public virtual IFindFluent<T, T> GetQueryable(FilterDefinition<T> query)
        {
            return GetQueryable(query, this.SortPropertyName, this.IsDescending);
        }


        #endregion

        #region   集合查询

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合 默认 _id降序
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>指定对象的集合</returns>
        public virtual IList<T> FindM(FilterDefinition<T> query)
        {
            return GetQueryable(query).ToList();
        }





        /// <summary>
        /// 根据条件查询数据库 异步 默认 _id降序
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>指定对象的集合</returns>
        public virtual async Task<IList<T>> FindAsyncM(Expression<Func<T, bool>> match)
        {
            return await Task.FromResult(GetQueryable(match).ToList());
        }

        /// <summary>
        /// 根据条件查询数据库 异步  默认 _id降序
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <returns>指定对象的集合</returns>
        public virtual async Task<IList<T>> FindAsyncM(FilterDefinition<T> query)
        {
            return await GetQueryable(query).ToListAsync();
        }


        #endregion

        #region 分页
        /// <summary>
        ///分页 根据条件查询数据库
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="info">分页实体 要指定升降序</param>
        /// <returns>指定对象的集合</returns>
        public virtual IList<T> FindWithPagerM<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, PageClass info)
        {
            int pageindex = (info.sys_PageIndex < 1) ? 1 : info.sys_PageIndex;
            int pageSize = (info.sys_PageSize <= 0) ? 20 : info.sys_PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IQueryable<T> query = GetQueryable(match, orderByProperty, info.isDescending);
            info.RCount = query.Count();
            info.PCount = info.RCount / pageSize;
            return query.Skip(excludedRows).Take(pageSize).ToList();
        }

        /// <summary>
        /// 分页  根据条件查询数据库
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <param name="info">分页实体 要指定排序字段，升降序</param>
        /// <returns>指定对象的集合</returns>
        public virtual IList<T> FindWithPagerM(FilterDefinition<T> query, PageClass info)
        {
            int pageindex = (info.sys_PageIndex < 1) ? 1 : info.sys_PageIndex;
            int pageSize = (info.sys_PageSize <= 0) ? 20 : info.sys_PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            var find = GetQueryable(query, info.sys_Order, info.isDescending);
            info.RCount = (int)find.Count();
            info.PCount = info.RCount / pageSize;
            return find.Skip(excludedRows).Limit(pageSize).ToList();
        }
        /// <summary>
        /// 分页  根据条件查询数据库
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <param name="sortDefinit">排序</param>
        /// <param name="info">分页实体 不用指定排序</param>
        /// <returns>指定对象的集合</returns>
        public virtual IList<T> FindWithPagerM(FilterDefinition<T> query, SortDefinition<T> sortDefinit, PageClass info)
        {
            int pageindex = (info.sys_PageIndex < 1) ? 1 : info.sys_PageIndex;
            int pageSize = (info.sys_PageSize <= 0) ? 20 : info.sys_PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            var find = GetQueryable(query, sortDefinit);
            info.RCount = (int)find.Count();
            info.PCount = info.RCount / pageSize;
            return find.Skip(excludedRows).Limit(pageSize).ToList();
        }


        /// <summary>
        /// 异步 分页 根据条件查询数据库 
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="info">分页实体 要指定升降序</param>
        /// <returns>指定对象的集合</returns>
        public virtual async Task<IList<T>> FindWithPagerAsyncM<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, PageClass info)
        {
            int pageindex = (info.sys_PageIndex < 1) ? 1 : info.sys_PageIndex;
            int pageSize = (info.sys_PageSize <= 0) ? 20 : info.sys_PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IQueryable<T> query = GetQueryable(match, orderByProperty, info.isDescending);
            info.RCount = query.Count();
            info.PCount = info.RCount / pageSize;

            var result = query.Skip(excludedRows).Take(pageSize).ToList();
            return await Task.FromResult(result);
        }

        /// <summary>
        ///异步 分页 根据条件查询数据库
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <param name="info">分页实体 要指定排序字段，升降序</param>
        /// <returns>指定对象的集合</returns>
        public virtual async Task<IList<T>> FindWithPagerAsyncM(FilterDefinition<T> query, PageClass info)
        {
            int pageindex = (info.sys_PageIndex < 1) ? 1 : info.sys_PageIndex;
            int pageSize = (info.sys_PageSize <= 0) ? 20 : info.sys_PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            var queryable = GetQueryable(query, info.sys_Order, info.isDescending);
            info.RCount = (int)queryable.Count();
            info.PCount = info.RCount / pageSize;
            return await queryable.Skip(excludedRows).Limit(pageSize).ToListAsync();
        }



        /// <summary>
        ///异步 分页 根据条件查询数据库
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <param name="sortDefinit">排序</param>
        /// <param name="info">分页实体 不用指定排序</param>
        /// <returns>指定对象的集合</returns>
        public virtual async Task<IList<T>> FindWithPagerAsyncM(FilterDefinition<T> query, SortDefinition<T> sortDefinit, PageClass info)
        {
            int pageindex = (info.sys_PageIndex < 1) ? 1 : info.sys_PageIndex;
            int pageSize = (info.sys_PageSize <= 0) ? 20 : info.sys_PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            var queryable = GetQueryable(query, info.sys_Order, info.isDescending);
            info.RCount = (int)queryable.Count();
            info.PCount = info.RCount / pageSize;
            return await queryable.Skip(excludedRows).Limit(pageSize).ToListAsync();
        }
        #endregion

        #region 添加





        #endregion


        #region  修改
        /// <summary>
        /// 更新 (主键默认名称是Id可用) 如果没有记录则写入
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="t">指定的对象</param>

        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual bool UpdateM(string id, T t)
        {

            bool result = false;
            //使用 IsUpsert = true ，如果没有记录则写入
            var update = collection.ReplaceOne(s => s.Id == id, t, new UpdateOptions() { IsUpsert = true });
            result = update != null && update.ModifiedCount > 0;

            return result;
        }

        /// <summary>
        /// 更新 (主键默认名称是Id可用) 如果没有记录则写入
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual bool UpdateM(T t)
        {
            bool result = false;
            //使用 IsUpsert = true ，如果没有记录则写入
            var update = collection.ReplaceOne(s => s.Id == t.Id, t, new UpdateOptions() { IsUpsert = true });
            result = update != null && update.ModifiedCount > 0;
            return result;
        }

        /// <summary>
        /// 异步 更新 (主键默认名称是Id可用) 如果没有记录则写入
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="t">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual async Task<bool> UpdateAsyncM(string id, T t)
        {

            bool result = false;
            //使用 IsUpsert = true ，如果没有记录则写入
            var update = collection.ReplaceOne(s => s.Id == id, t, new UpdateOptions() { IsUpsert = true });
            result = update != null && update.ModifiedCount > 0;
            return await Task.FromResult(result);
        }


        /// <summary>
        /// 更新的操作(部分字段更新) (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="update">更新对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual bool UpdateM(string id, UpdateDefinition<T> update)
        {
            var result = collection.UpdateOne(s => s.Id == id, update, new UpdateOptions() { IsUpsert = true });
            return result != null && result.ModifiedCount > 0;
        }

        /// <summary>
        /// 异步(部分字段更新) (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="update">更新对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual async Task<bool> UpdateAsyncM(string id, UpdateDefinition<T> update)
        {
            var result = await collection.UpdateOneAsync(s => s.Id == id, update, new UpdateOptions() { IsUpsert = true });
            var sucess = result != null && result.ModifiedCount > 0;
            return await Task.FromResult(sucess);
        }
        #endregion

        #region 删除
        /// <summary> 
        /// 从数据库中删除指定对象 (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool DeleteM(string id)
        {
            var result = collection.DeleteOne(s => s.Id == id);
            return result != null && result.DeletedCount > 0;
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定指定的对象 (mongodb 中的 _id) 
        /// </summary>
        /// <param name="idList">对象的ID集合</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool DeleteBatchM(List<string> idList)
        {

            var query = Query.In("_id", new BsonArray(idList));
            var result = collection.DeleteMany(s => idList.Contains(s.Id));
            return result != null && result.DeletedCount > 0;
        }

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象 (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool DeleteByExpressionM(Expression<Func<T, bool>> match)
        {
            collection.AsQueryable().Where(match).ToList().ForEach(s => collection.DeleteOne(t => t.Id == s.Id));
            return true;
        }
        #endregion
    }
}
