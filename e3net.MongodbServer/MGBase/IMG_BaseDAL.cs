﻿using e3net.Common.Entity;
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
    /// mongoDB数据访问层实现 补充拓展接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMG_BaseDAL<T> : IMongoHelper<T>
    {


        #region 查询单个
        /// <summary>
        /// 指定ID的对象  (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        T FindByIDM(string id);


        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象（异步） (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
         Task<T> FindByIDAsyncM(string id);




        #endregion

        #region 返回可查询的记录源
        /// <summary>
        /// 返回可查询的记录源
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        IFindFluent<T, T> GetQueryable(FilterDefinition<T> query);

        #endregion

        #region   集合查询

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合 默认 _id降序
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>指定对象的集合</returns>
        IList<T> FindM(FilterDefinition<T> query);





        /// <summary>
        /// 根据条件查询数据库 异步 默认 _id降序
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>指定对象的集合</returns>
         Task<IList<T>> FindAsyncM(Expression<Func<T, bool>> match);

        /// <summary>
        /// 根据条件查询数据库 异步  默认 _id降序
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <returns>指定对象的集合</returns>
         Task<IList<T>> FindAsyncM(FilterDefinition<T> query);


        #endregion

        #region 分页
        /// <summary>
        ///分页 根据条件查询数据库
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="info">分页实体 要指定排序字段，升降序</param>
        /// <returns>指定对象的集合</returns>
        IList<T> FindWithPagerM<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, PageClass info);

        /// <summary>
        /// 分页  根据条件查询数据库
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <param name="info">分页实体 要指定排序字段，升降序</param>
        /// <returns>指定对象的集合</returns>
        IList<T> FindWithPagerM(FilterDefinition<T> query, PageClass info);

                /// <summary>
        /// 分页  根据条件查询数据库
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <param name="sortDefinit">排序</param>
        /// <param name="info">分页实体 不用指定排序</param>
        /// <returns>指定对象的集合</returns>
        IList<T> FindWithPagerM(FilterDefinition<T> query, SortDefinition<T> sortDefinit, PageClass info);

        /// <summary>
        /// 异步 分页 根据条件查询数据库 
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="info">分页实体 要指定排序字段，升降序</param>
        /// <returns>指定对象的集合</returns>
         Task<IList<T>> FindWithPagerAsyncM<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, PageClass info);

        /// <summary>
        ///异步 分页 根据条件查询数据库
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <param name="info">分页实体 要指定排序字段，升降序</param>
        /// <returns>指定对象的集合</returns>
         Task<IList<T>> FindWithPagerAsyncM(FilterDefinition<T> query, PageClass info);
             /// <summary>
        ///异步 分页 根据条件查询数据库
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <param name="sortDefinit">排序</param>
        /// <param name="info">分页实体 不用指定排序</param>
        /// <returns>指定对象的集合</returns>
         Task<IList<T>> FindWithPagerAsyncM(FilterDefinition<T> query, SortDefinition<T> sortDefinit, PageClass info);
        #endregion

        #region 添加





        #endregion


        #region  修改
         /// <summary>
         /// 更新 (主键默认名称是Id可用) 如果没有记录则写入
         /// </summary>
         /// <param name="t">指定的对象</param>
         /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
         bool UpdateM(T t);

        /// <summary>
        /// 更新 (主键默认名称是Id可用) 如果没有记录则写入
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="t">指定的对象</param>

        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        bool UpdateM(string id, T t);
        /// <summary>
        /// 异步 更新 (主键默认名称是Id可用) 如果没有记录则写入
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="t">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
         Task<bool> UpdateAsyncM(string id, T t);


        /// <summary>
        /// 更新的操作(部分字段更新) (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="update">更新对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        bool UpdateM(string id, UpdateDefinition<T> update);

        /// <summary>
        /// 异步(部分字段更新) (主键默认名称是Id可用)  
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="update">更新对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
         Task<bool> UpdateAsyncM(string id, UpdateDefinition<T> update);
        #endregion

        #region 删除
        /// <summary> 
        /// 从数据库中删除指定对象 (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteM(string id);

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定指定的对象 (mongodb 中的 _id) 
        /// </summary>
        /// <param name="idList">对象的ID集合</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteBatchM(List<string> idList);

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象 (主键默认名称是Id可用) 
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteByExpressionM(Expression<Func<T, bool>> match);
        #endregion

  
    }
}
