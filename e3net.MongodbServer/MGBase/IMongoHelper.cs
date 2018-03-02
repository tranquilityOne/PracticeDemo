using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace e3net.MongodbServer
{
    /// <summary>
    /// mongoDB数据访问层实现 基础接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMongoHelper<T>
    {


        void SetDb(string _ConnectionName);

        /// <summary>
        /// IMongoDatabase对象
        /// </summary>
        IMongoDatabase Getdb();


        /// <summary>
        /// 获取操作对象的IMongoCollection集合,强类型对象集合
        /// </summary>
        IMongoCollection<T> Getcollection();


        #region 查询数量
        /// <summary>
        ///查询数量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long GetCount(Expression<Func<T, bool>> filter);
        /// <summary>
        ///查询数量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long GetCount(FilterDefinition<T> filter);

        #endregion

        #region 查询单个


        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象 (mongodb 中的 _id) 
        /// </summary>
        /// <param name="id">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        T FindByID(string id);


        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象（异步） (mongodb 中的 _id) 
        /// </summary>
        /// <param name="id">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        Task<T> FindByIDAsync(string id);




        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="filter">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        T FindSingle(FilterDefinition<T> filter);

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        T FindSingle(Expression<Func<T, bool>> match);
        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象（异步）
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        Task<T> FindSingleAsync(FilterDefinition<T> query);



        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="orderByProperty">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        T FindSingle<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, bool isDescending = true);




        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        T FindSingle(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true);
                /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象（异步）
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        Task<T> FindSingleAsync(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true);


        #endregion

        #region 返回可查询的记录源


        /// <summary>
        /// 根据条件表达式返回可查询的记录源
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        IFindFluent<T, T> GetQueryable(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true);

        /// <summary>
        /// 根据条件表达式返回可查询的记录源
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sortDefinit">排序条件</param>
        /// <returns></returns>
        IFindFluent<T, T> GetQueryable(FilterDefinition<T> query, SortDefinition<T> sortDefinit);

        /// <summary>
        /// 根据条件表达式返回可查询的记录源
        /// </summary>
        /// <param name="match">查询条件</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        IQueryable<T> GetQueryable<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, bool isDescending = true);



        #endregion

        #region 集合 查询

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>指定对象的集合</returns>
        IList<T> Find(Expression<Func<T, bool>> match);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        IList<T> Find<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, bool isDescending = true);
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        IList<T> Find(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true);



        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortDefinit">排序</param>
        /// <returns></returns>
        IList<T> Find(FilterDefinition<T> query, SortDefinition<T> sortDefinit);






        /// <summary>
        /// 根据条件查询数据库 异步
        /// </summary>
        /// <param name="match">表达式条件</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        Task<IList<T>> FindAsync<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, bool isDescending = true);

        /// <summary>
        /// 根据条件查询数据库 异步
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        Task<IList<T>> FindAsync(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true);


        /// <summary>
        /// 根据条件查询数据库 异步
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortDefinit">排序</param>
        /// <returns></returns>
        Task<IList<T>> FindAsync(FilterDefinition<T> query, SortDefinition<T> sortDefinit);

        #endregion

        #region 条件进行分页的处理


        /// <summary>
        /// 分页 条件查找
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="match">条件表达式</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        /// <param name="RecordCount">返回记录数</param>
        /// <param name="isDescending">是否降序</param>
        /// <returns></returns>
        IList<T> FindWithPager<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, int _pageindex, int _PageSize, out int RecordCount, bool isDescending = true);

        /// <summary>
        /// 分页 条件查找
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        /// <param name="RecordCount">记录数</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">是否降序</param>
        /// <returns></returns>
        IList<T> FindWithPager(FilterDefinition<T> query, int _pageindex, int _PageSize, out int RecordCount, string sortPropertyName, bool isDescending = true);


        /// <summary>
        /// 分页 条件查找
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortDefinit">排序</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        /// <param name="RecordCount">记录数</param>
        /// <returns></returns>
        IList<T> FindWithPager(FilterDefinition<T> query, SortDefinition<T> sortDefinit, int _pageindex, int _PageSize, out int RecordCount);

        /// <summary>
        /// 异步 分页 条件查找
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="match">条件表达式</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        /// <param name="isDescending">是否降序</param>
        Task<IList<T>> FindWithPagerAsync<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, int _pageindex, int _PageSize, bool isDescending = true);

        /// <summary>
        /// 异步  根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">是否降序</param>
        Task<IList<T>> FindWithPagerAsync(FilterDefinition<T> query, int _pageindex, int _PageSize, string sortPropertyName, bool isDescending = true);


        /// <summary>
        /// 异步  根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortDefinit">排序</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        Task<IList<T>> FindWithPagerAsync(FilterDefinition<T> query, SortDefinition<T> sortDefinit, int _pageindex, int _PageSize);

        #endregion

        #region  添加

        /// <summary>
        /// 插入指定对象到数据库中 主键需要赋值
        /// </summary>
        /// <param name="t">指定的对象</param>
        void Insert(T t);
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="t">指定的对象</param>
        Task InsertAsync(T t);

        /// <summary>
        /// 添加 返回Id (没有特殊要求，不要用，高并发有可能Id重复)
        /// </summary>
        ///<param name="t">指定的对象</param>
        /// <param name="IdFild">主键Id名称</param>
        string Insert(T t, string IdFild);

        /// <summary>
        /// 异步添加 返回Id  (没有特殊要求，不要用，高并发有可能Id重复)
        /// </summary>
        /// <param name="t">实体</param>
        /// <param name="IdFild">主键列名称</param>
        /// <returns>id</returns>
        Task<string> InsertAsync(T t, string IdFild);



        /// <summary>
        ///批量 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        void InsertBatch(IEnumerable<T> list);
        /// <summary>
        ///异步批量 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        Task InsertBatchAsync(IEnumerable<T> list);
        #endregion


        #region 更新

        /// <summary>
        /// 更新1
        /// </summary>
        /// <param name="idFild">主键列名</param>
        /// <param name="id">主键值</param>
        /// <param name="t">实体</param>
        /// <returns></returns>
        bool Update(string idFild, string id, T t);

        /// <summary>
        /// 更新对象属性到数据库中 如果不存在则添加
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <param name="filter">条件</param>
        /// <returns>执行成功 成功数量</returns>
        bool Update(FilterDefinition<T> filter, T t);


        /// <summary>
        /// 更新的操作(部分字段更新)
        /// </summary>
        /// <param name="idFild">主键列名</param>
        /// <param name="id">主键值</param>
        /// <param name="update">更新对象</param>
        /// <returns></returns>
        bool Update(string idFild, string id, UpdateDefinition<T> update);

        /// <summary>
        /// 更新的操作(部分字段更新) 可更新多个
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="update">更新对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        long Update(FilterDefinition<T> filter, UpdateDefinition<T> update);


        #endregion

        #region 异步更新
        /// <summary>
        /// 更新 异步
        /// </summary>
        /// <param name="idFild">主键列名</param>
        /// <param name="id">主键值</param>
        /// <param name="t">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(string idFild, string id, T t);

        /// <summary>
        /// 更新 异步 如果不存在则添加
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <param name="filter">条件</param>
        /// <returns>执行成功 成功数量</returns>
        Task<bool> UpdateAsync(FilterDefinition<T> filter, T t);




        /// <summary>
        /// 异步  更新的操作(部分字段更新)
        /// </summary>
        /// <param name="idFild">主键列名</param>
        /// <param name="id">主键值</param>
        /// <param name="update">更新对象</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(string idFild, string id, UpdateDefinition<T> update);


        /// <summary>
        /// 异步 更新的操作(部分字段更新) 可更新多个
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="update">更新对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        Task<long> UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);


        #endregion

        #region  删除


        /// <summary>
        /// id删除 (mongodb 中的 _id)
        /// </summary>
        /// <param name="条件">对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool Delete(string id);



        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>删除成功数理</returns>
        long DeleteByQuery(FilterDefinition<T> query);

        #endregion
    }
}
