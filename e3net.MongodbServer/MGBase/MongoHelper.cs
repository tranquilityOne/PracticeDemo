using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace e3net.MongodbServer
{
    /// <summary>
    ///  mongoDB数据访问层实现 基础
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class MongoHelper<T> : IMongoHelper<T>
    {

        #region 查询数量
        /// <summary>
        ///查询数量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long GetCount(Expression<Func<T, bool>> filter)
        {
            return collection.Count(filter);
        }
        /// <summary>
        ///查询数量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long GetCount(FilterDefinition<T> filter)
        {
            return collection.Count(filter);
        }

        #endregion

        #region 属性
        /// <summary>
        /// 连接节点名
        /// </summary>
        public string ConnectionName { set; get; }


        private string m_tableName;
        //数据库名前缀
        public readonly string BL_TableName_Prefix = "";
        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName
        {
            set
            {
                m_tableName =  value;
            }
            get
            {
                if (string.IsNullOrEmpty(m_tableName))
                {
                    m_tableName = BL_TableName_Prefix + typeof(T).Name;
                    return  m_tableName;
                }
                else
                {
                    return m_tableName;
                }

            }
        }

        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="_ConnectionName">连接节点名</param>
        public void SetDb(string _ConnectionName)
        {
            ConnectionName = _ConnectionName;
        }
        /// <summary>
        /// 设置 集合
        /// </summary>
        /// <param name="_ConnectionName">连接节点名</param>
        public void SetTableName(string tableName)
        {
            m_tableName = BL_TableName_Prefix+tableName;
        }


        /// <summary>
        /// 默认数据库
        /// </summary>
        public MongoHelper()
        {
            ConnectionName = MDbFactory.GetKeyFrom(this);//数据库连接 节点存在类的特性里
        }
        /// <summary>
        /// IMongoDatabase对象
        /// </summary>
        public IMongoDatabase db
        {
            get
            {
                if (!string.IsNullOrEmpty(ConnectionName))
                {
                    return new MDBContextFactory().GetIDbContext(ConnectionName);
                }
                else
                {
                    return new MDBContextFactory().GetIDbContext();
                }
            }
        }

        /// <summary>
        /// 获取操作对象的IMongoCollection集合,强类型对象集合
        /// </summary>
        public IMongoCollection<T> collection
        {
            get
            {
                return db.GetCollection<T>(this.TableName);

            }
        }
        /// <summary>
        ///获取 IMongoDatabase对象
        /// </summary>
        public IMongoDatabase Getdb()
        {
            return db;
        }

        /// <summary>
        /// 获取操作对象的IMongoCollection集合,强类型对象集合
        /// </summary>
        public IMongoCollection<T> Getcollection()
        {
            return collection;
        }

        #endregion

        #region 查找出指定字段 ，有时间再实现

        Task<List<T>> _Find(string[] fields, Dictionary<string, int> sortfields, System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            IFindFluent<T, T> iff = null;
            if (expression == null)
            {
                iff = collection.Find<T>(new BsonDocument());
            }
            else
            {
                iff = collection.Find<T>(expression);
            }
            if (fields.Length > 0)
            {
                Dictionary<string, int> dicfields = new Dictionary<string, int>();
                foreach (string item in fields)
                {
                    dicfields.Add(item, 1);
                }
                iff = iff.Project<T>(Tools<T>.getDisplayFiles(dicfields));
            }
            if (sortfields != null)
            {
                iff = iff.Sort(Tools<T>.getSortDefinition(sortfields));
            }
            return iff.ToListAsync();
        }
        Task<T> _FindOne(string id, string[] fields)
        {
            Task<T> result = null;
            IFindFluent<T, T> iff = null;
            try
            {
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);

                //FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                iff = collection.Find<T>(filter);
                if (fields != null && fields.Length > 0)
                {
                    Dictionary<string, int> dicfields = new Dictionary<string, int>();
                    foreach (string item in fields)
                    {
                        dicfields.Add(item, 1);
                    }
                    iff = iff.Project<T>(Tools<T>.getDisplayFiles(dicfields));
                }
                result = iff.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
            }
            return result;

        }

        #endregion

        #region 查询单个


        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象 (mongodb 中的 _id) 
        /// </summary>
        /// <param name="id">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual T FindByID(string id)
        {
            //FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            return collection.Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象（异步） (mongodb 中的 _id) 
        /// </summary>
        /// <param name="id">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual async Task<T> FindByIDAsync(string id)
        {
            //  FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            return await collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }




        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="filter">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        public virtual T FindSingle(FilterDefinition<T> filter)
        {
            return collection.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        public virtual T FindSingle(Expression<Func<T, bool>> match)
        {
            return collection.Find(match).FirstOrDefault();
        }


        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="orderByProperty">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual T FindSingle<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, bool isDescending = true)
        {
            return GetQueryable(match, orderByProperty, isDescending).FirstOrDefault();
        }




        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual T FindSingle(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true)
        {
            return GetQueryable(query, sortPropertyName, isDescending).FirstOrDefault();
        }



        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象（异步）
        /// </summary>
        /// <param name="query">条件表达式</param>
        /// <returns>存在则返回指定的第一个对象,否则返回默认值</returns>
        public virtual async Task<T> FindSingleAsync(FilterDefinition<T> query)
        {
            return await collection.Find(query).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象（异步）
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual async Task<T> FindSingleAsync(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true)
        {
            return await GetQueryable(query, sortPropertyName, isDescending).FirstOrDefaultAsync();
        }


        #endregion

        #region 返回可查询的记录源


        /// <summary>
        /// 根据条件表达式返回可查询的记录源
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual IFindFluent<T, T> GetQueryable(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true)
        {

            IFindFluent<T, T> queryable = collection.Find(query);
            var sort = isDescending ? Builders<T>.Sort.Descending(sortPropertyName) : Builders<T>.Sort.Ascending(sortPropertyName);
            return queryable.Sort(sort);
        }

        /// <summary>
        /// 根据条件表达式返回可查询的记录源
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sortDefinit">排序条件</param>
        /// <returns></returns>
        public virtual IFindFluent<T, T> GetQueryable(FilterDefinition<T> query, SortDefinition<T> sortDefinit)
        {

            IFindFluent<T, T> queryable = collection.Find(query);
            return queryable.Sort(sortDefinit);
        }


        /// <summary>
        /// 根据条件表达式返回可查询的记录源
        /// </summary>
        /// <param name="match">查询条件</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetQueryable<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, bool isDescending = true)
        {
            IQueryable<T> query = collection.AsQueryable();

            if (match != null)
            {
                query = query.Where(match);
            }
            query = isDescending ? query.OrderByDescending(orderByProperty) : query.OrderBy(orderByProperty);

            return query;
        }



        #endregion

        #region 集合 查询

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>指定对象的集合</returns>
        public virtual IList<T> Find(Expression<Func<T, bool>> match)
        {
            return collection.Find(match).ToList();
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual IList<T> Find<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, bool isDescending = true)
        {
            return GetQueryable<TKey>(match, orderByProperty, isDescending).ToList();
        }
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual IList<T> Find(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true)
        {
            return GetQueryable(query, sortPropertyName, isDescending).ToList();
        }




        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortDefinit">排序</param>
        /// <returns></returns>
        public virtual IList<T> Find(FilterDefinition<T> query, SortDefinition<T> sortDefinit)
        {
            return GetQueryable(query, sortDefinit).ToList();
        }


        /// <summary>
        /// 根据条件查询数据库 异步
        /// </summary>
        /// <param name="match">表达式条件</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        public virtual async Task<IList<T>> FindAsync<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, bool isDescending = true)
        {
            return await Task.FromResult(GetQueryable(match, orderByProperty, isDescending).ToList());
        }

        /// <summary>
        /// 根据条件查询数据库 异步
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">如果为true则为降序，否则为升序</param>
        /// <returns></returns>
        public virtual async Task<IList<T>> FindAsync(FilterDefinition<T> query, string sortPropertyName, bool isDescending = true)
        {
            return await GetQueryable(query, sortPropertyName, isDescending).ToListAsync();
        }

        /// <summary>
        /// 根据条件查询数据库 异步
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortDefinit">排序</param>
        /// <returns></returns>
        public virtual async Task<IList<T>> FindAsync(FilterDefinition<T> query, SortDefinition<T> sortDefinit)
        {
            return await GetQueryable(query, sortDefinit).ToListAsync();
        }

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
        public virtual IList<T> FindWithPager<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, int _pageindex, int _PageSize, out int RecordCount, bool isDescending = true)
        {
            int pageindex = (_pageindex < 1) ? 1 : _pageindex;
            int pageSize = (_PageSize <= 0) ? 20 : _PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IQueryable<T> query = GetQueryable(match, orderByProperty, isDescending);
            RecordCount = query.Count();
            return query.Skip(excludedRows).Take(pageSize).ToList();
        }

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
        public virtual IList<T> FindWithPager(FilterDefinition<T> query, int _pageindex, int _PageSize, out int RecordCount, string sortPropertyName, bool isDescending = true)
        {
            int pageindex = (_pageindex < 1) ? 1 : _pageindex;
            int pageSize = (_PageSize <= 0) ? 20 : _PageSize;
            int excludedRows = (pageindex - 1) * pageSize;

            var find = GetQueryable(query, sortPropertyName, isDescending);
            RecordCount = (int)find.Count();

            return find.Skip(excludedRows).Limit(pageSize).ToList();
        }


        /// <summary>
        /// 分页 条件查找
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortDefinit">排序</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        /// <param name="RecordCount">记录数</param>
        /// <returns></returns>
        public virtual IList<T> FindWithPager(FilterDefinition<T> query, SortDefinition<T> sortDefinit, int _pageindex, int _PageSize, out int RecordCount)
        {
            int pageindex = (_pageindex < 1) ? 1 : _pageindex;
            int pageSize = (_PageSize <= 0) ? 20 : _PageSize;
            int excludedRows = (pageindex - 1) * pageSize;

            var find = GetQueryable(query, sortDefinit);
            RecordCount = (int)find.Count();

            return find.Skip(excludedRows).Limit(pageSize).ToList();
        }


        /// <summary>
        /// 异步 分页 条件查找
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="match">条件表达式</param>
        /// <param name="orderByProperty">排序表达式</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        /// <param name="isDescending">是否降序</param>
        public virtual async Task<IList<T>> FindWithPagerAsync<TKey>(Expression<Func<T, bool>> match, Expression<Func<T, TKey>> orderByProperty, int _pageindex, int _PageSize, bool isDescending = true)
        {
            int pageindex = (_pageindex < 1) ? 1 : _pageindex;
            int pageSize = (_PageSize <= 0) ? 20 : _PageSize;

            int excludedRows = (pageindex - 1) * pageSize;

            IQueryable<T> query = GetQueryable(match, orderByProperty, isDescending);
            //RecordCount = query.Count();

            var result = query.Skip(excludedRows).Take(pageSize).ToList();
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 异步  根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        /// <param name="sortPropertyName">排序字段</param>
        /// <param name="isDescending">是否降序</param>
        public virtual async Task<IList<T>> FindWithPagerAsync(FilterDefinition<T> query, int _pageindex, int _PageSize, string sortPropertyName, bool isDescending = true)
        {
            int pageindex = (_pageindex < 1) ? 1 : _pageindex;
            int pageSize = (_PageSize <= 0) ? 20 : _PageSize;
            int excludedRows = (pageindex - 1) * pageSize;

            var queryable = GetQueryable(query, sortPropertyName, isDescending);
            //info.RecordCount = (int)queryable.Count();

            return await queryable.Skip(excludedRows).Limit(pageSize).ToListAsync();
        }
        /// <summary>
        /// 异步  根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="sortDefinit">排序</param>
        /// <param name="_pageindex">页码</param>
        /// <param name="_PageSize">每页数量</param>
        public virtual async Task<IList<T>> FindWithPagerAsync(FilterDefinition<T> query, SortDefinition<T> sortDefinit, int _pageindex, int _PageSize)
        {
            int pageindex = (_pageindex < 1) ? 1 : _pageindex;
            int pageSize = (_PageSize <= 0) ? 20 : _PageSize;
            int excludedRows = (pageindex - 1) * pageSize;

            var queryable = GetQueryable(query, sortDefinit);
            //info.RecordCount = (int)queryable.Count();

            return await queryable.Skip(excludedRows).Limit(pageSize).ToListAsync();
        }

        #endregion

        #region  添加

        /// <summary>
        /// 插入指定对象到数据库中 主键需要赋值
        /// </summary>
        /// <param name="t">指定的对象</param>
        public virtual void Insert(T t)
        {
            collection.InsertOne(t);
        }
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="t">指定的对象</param>
        public virtual async Task InsertAsync(T t)
        {
            await collection.InsertOneAsync(t);
        }

        /// <summary>
        /// 添加 返回Id  (没有特殊要求，不要用，高并发有可能Id重复)
        /// </summary>
        ///<param name="t">指定的对象</param>
        /// <param name="IdFild">主键Id名称</param>
        public virtual string Insert(T t, string IdFild)
        {
            string flag = null;
            try
            {
                flag = ObjectId.GenerateNewId().ToString();
                t.GetType().GetProperty(IdFild).SetValue(t, flag);
                collection.InsertOne(t);
            }
            catch (Exception)
            {

            }
            return flag;
        }

        /// <summary>
        /// 异步添加 返回Id  (没有特殊要求，不要用，高并发有可能Id重复)
        /// </summary>
        /// <param name="t">实体</param>
        /// <param name="IdFild">主键列名称</param>
        /// <returns>id</returns>
        public async Task<string> InsertAsync(T t, string IdFild)
        {
            string flag = null;
            try
            {
                flag = ObjectId.GenerateNewId().ToString();
                t.GetType().GetProperty(IdFild).SetValue(t, flag);
                await collection.InsertOneAsync(t);
            }
            catch (Exception)
            {

            }
            return flag;
        }

        /// <summary>
        ///批量 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        public virtual void InsertBatch(IEnumerable<T> list)
        {
            collection.InsertMany(list);
        }

        /// <summary>
        ///异步批量 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        public virtual async Task InsertBatchAsync(IEnumerable<T> list)
        {
            await collection.InsertManyAsync(list);
        }
        #endregion

        #region 更新

        /// <summary>
        /// 更新1
        /// </summary>
        /// <param name="idFild">主键列名</param>
        /// <param name="id">主键值</param>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public virtual bool Update(string idFild, string id, T t)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(idFild, id);
            return Update(filter, t);
        }

        /// <summary>
        /// 更新对象属性到数据库中 如果不存在则添加
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <param name="filter">条件</param>
        /// <returns>执行成功 成功数量</returns>
        public virtual bool Update(FilterDefinition<T> filter, T t)
        {
            bool result = false;
            //使用 IsUpsert = true ，如果没有记录则写入
            var update = collection.ReplaceOne(filter, t, new UpdateOptions() { IsUpsert = true });
            result = update != null && update.ModifiedCount > 0;
            return result;
        }


        /// <summary>
        /// 更新的操作(部分字段更新)
        /// </summary>
        /// <param name="idFild">主键列名</param>
        /// <param name="id">主键值</param>
        /// <param name="update">更新对象</param>
        /// <returns></returns>
        public virtual bool Update(string idFild, string id, UpdateDefinition<T> update)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(idFild, id);

            return Update(filter, update) > 0;
        }

        /// <summary>
        /// 更新的操作(部分字段更新) 可更新多个
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="update">更新对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual long Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var result = collection.UpdateMany(filter, update, new UpdateOptions() { IsUpsert = false });
            var sucess = result == null ? 0 : result.ModifiedCount;
            return sucess;
        }

        #endregion

        #region 异步更新
        /// <summary>
        /// 更新 异步
        /// </summary>
        /// <param name="idFild">主键列名</param>
        /// <param name="id">主键值</param>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(string idFild, string id, T t)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(idFild, id);
            bool result = false;
            //使用 IsUpsert = true ，如果没有记录则写入
            var update = collection.ReplaceOne(filter, t, new UpdateOptions() { IsUpsert = true });
            result = update != null && update.ModifiedCount > 0;
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 更新 异步 如果不存在则添加
        /// </summary>
        /// <param name="t">指定的对象</param>
        /// <param name="filter">条件</param>
        /// <returns>执行成功 成功数量</returns>
        public virtual async Task<bool> UpdateAsync(FilterDefinition<T> filter, T t)
        {
            bool result = false;
            //使用 IsUpsert = true ，如果没有记录则写入
            var update = collection.ReplaceOne(filter, t, new UpdateOptions() { IsUpsert = true });
            result = update != null && update.ModifiedCount > 0;
            return await Task.FromResult(result);
        }




        /// <summary>
        /// 异步  更新的操作(部分字段更新)
        /// </summary>
        /// <param name="idFild">主键列名</param>
        /// <param name="id">主键值</param>
        /// <param name="update">更新对象</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(string idFild, string id, UpdateDefinition<T> update)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(idFild, id);
            var result = await collection.UpdateOneAsync(filter, update, new UpdateOptions() { IsUpsert = true });
            var sucess = result != null && result.ModifiedCount > 0;
            return await Task.FromResult(sucess);
        }


        /// <summary>
        /// 异步 更新的操作(部分字段更新) 可更新多个
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <param name="update">更新对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c></returns>
        public virtual async Task<long> UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var result = await collection.UpdateManyAsync(filter, update, new UpdateOptions() { IsUpsert = true });
            var sucess = result == null ? 0 : result.ModifiedCount;
            return await Task.FromResult(sucess);
        }


        #endregion

        #region  删除


        /// <summary>
        /// id删除 (mongodb 中的 _id)
        /// </summary>
        /// <param name="条件">对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete(string id)
        {
            //FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            long i = DeleteByQuery(filter);
            return i > 0;
        }



        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象 多个
        /// </summary>
        /// <param name="match">条件表达式</param>
        /// <returns>删除成功数理</returns>
        public virtual long DeleteByQuery(FilterDefinition<T> query)
        {
            var result = collection.DeleteMany(query);
            if (result != null)
            {
                return result.DeletedCount;
            }
            else
            {
                return 0;
            }
        }

        #endregion


    }
}
