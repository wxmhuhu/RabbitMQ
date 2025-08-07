using MricoServices.Shared.ApiResult;
using SqlSugar;
using System.Linq.Expressions;

namespace MricoServices.Repository.IRepository
{

        // 定义一个泛型仓储接口，T 是实体类型
        // where T : class, new() 约束确保 T 是引用类型且有一个无参构造函数
        // 这与 SimpleClient<T> 的约束保持一致
        public interface IBaseRepository<T> where T : class, new()
        {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AddAsync(T entity);

        /// <summary>
        /// 通过Id进行查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        ISugarQueryable<T> GetAll();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T entity);
        /// <summary>
        /// 真删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(int id);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> SoftDeleteAsync(int id);
        /// <summary>
        /// 编号生成
        /// </summary>
        ISugarQueryable<T> GetByPrefixWithoutSoftDelete<TProperty>(string propertyName, string prefix);








        //    // -----------------------------------------------------------------------
        //    // 插入操作
        //    // -----------------------------------------------------------------------

        //    /// <summary>
        //    /// 异步插入单个实体并返回受影响行数
        //    /// </summary>
        //    /// <param name="entity">要插入的实体</param>
        //    /// <returns>受影响行数</returns>
        //    Task<int> AddAsync(T entity);

        //    /// <summary>
        //    /// 异步批量插入实体并返回受影响行数
        //    /// </summary>
        //    /// <param name="entities">要插入的实体集合</param>
        //    /// <returns>受影响行数</returns>
        //    Task<int> AddRangeAsync(IEnumerable<T> entities);

        //    /// <summary>
        //    /// 异步插入单个实体并返回自增ID
        //    /// </summary>
        //    /// <param name="entity">要插入的实体</param>
        //    /// <returns>自增ID</returns>
        //    Task<int> AddReturnIdentityAsync(T entity);


        //    // -----------------------------------------------------------------------
        //    // 查询操作
        //    // -----------------------------------------------------------------------

        //    /// <summary>
        //    /// 异步根据ID查询单个实体
        //    /// </summary>
        //    /// <param name="id">实体ID</param>
        //    /// <returns>匹配的实体，如果未找到则为 null</returns>
        //    Task<T> GetByIdAsync(object id);

        //    /// <summary>
        //    /// 异步查询所有实体列表
        //    /// </summary>
        //    /// <returns>所有实体列表</returns>
        //    Task<List<T>> GetListAsync();

        //    /// <summary>
        //    /// 异步根据条件查询实体列表
        //    /// </summary>
        //    /// <param name="expression">查询条件</param>
        //    /// <returns>符合条件的实体列表</returns>
        //    Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression);

        //    /// <summary>
        //    /// 异步查询单条记录，结果集不能超过1，否则会抛出错误
        //    /// </summary>
        //    /// <param name="expression">查询条件</param>
        //    /// <returns>符合条件的单个实体</returns>
        //    Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);

        //    /// <summary>
        //    /// 异步查询第一条记录
        //    /// </summary>
        //    /// <param name="expression">查询条件</param>
        //    /// <returns>符合条件的第一条实体</returns>
        //    Task<T> GetFirstAsync(Expression<Func<T, bool>> expression);

        //    /// <summary>
        //    /// 异步分页查询
        //    /// </summary>
        //    /// <param name="expression">查询条件</param>
        //    /// <param name="page">分页模型</param>
        //    /// <returns>分页结果列表</returns>
        //    Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> expression, PageModel page);

        //    /// <summary>
        //    /// 异步分页查询（带排序）
        //    /// </summary>
        //    /// <param name="expression">查询条件</param>
        //    /// <param name="page">分页模型</param>
        //    /// <param name="orderByExpression">排序表达式</param>
        //    /// <param name="orderByType">排序类型</param>
        //    /// <returns>分页结果列表</returns>
        //    Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> expression, PageModel page, Expression<Func<T, object>> orderByExpression, OrderByType orderByType = OrderByType.Asc);

        //    /// <summary>
        //    /// 异步使用 Queryable 进行复杂查询（返回 Queryable 对象以链式调用）
        //    /// </summary>
        //    /// <returns>ISugarQueryable 对象</returns>
        //    ISugarQueryable<T> AsQueryable();

        //    // -----------------------------------------------------------------------
        //    // 更新操作
        //    // -----------------------------------------------------------------------

        //    /// <summary>
        //    /// 异步更新单个实体（需要有主键）
        //    /// </summary>
        //    /// <param name="entity">要更新的实体</param>
        //    /// <returns>是否更新成功</returns>
        //    Task<bool> UpdateAsync(T entity);

        //    /// <summary>
        //    /// 异步批量更新实体（需要有主键）
        //    /// </summary>
        //    /// <param name="entities">要更新的实体集合</param>
        //    /// <returns>是否更新成功</returns>
        //    Task<bool> UpdateRangeAsync(IEnumerable<T> entities);

        //    /// <summary>
        //    /// 异步根据表达式更新指定列
        //    /// </summary>
        //    /// <param name="columns">更新的列和值</param>
        //    /// <param name="whereExpression">更新条件</param>
        //    /// <returns>受影响行数</returns>
        //    Task<int> UpdateColumnsAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);


        //    // -----------------------------------------------------------------------
        //    // 删除操作
        //    // -----------------------------------------------------------------------

        //    /// <summary>
        //    /// 异步删除单个实体（需要有主键）
        //    /// </summary>
        //    /// <param name="entity">要删除的实体</param>
        //    /// <returns>是否删除成功</returns>
        //    Task<bool> DeleteAsync(T entity);

        //    /// <summary>
        //    /// 异步根据ID删除实体
        //    /// </summary>
        //    /// <param name="id">实体ID</param>
        //    /// <returns>受影响行数</returns>
        //    Task<int> DeleteByIdAsync(object id);

        //    /// <summary>
        //    /// 异步根据一组ID批量删除实体
        //    /// </summary>
        //    /// <param name="ids">实体ID数组</param>
        //    /// <returns>受影响行数</returns>
        //    Task<int> DeleteByIdsAsync(object[] ids);

        //    /// <summary>
        //    /// 异步根据条件删除实体
        //    /// </summary>
        //    /// <param name="expression">删除条件</param>
        //    /// <returns>受影响行数</returns>
        //    Task<int> DeleteAsync(Expression<Func<T, bool>> expression);

        //    /// <summary>
        //    /// 异步逻辑删除实体（将IsDeleted设为true并更新审计字段）
        //    /// </summary>
        //    /// <param name="expression">删除条件</param>
        //    /// <returns>是否成功</returns>
        //    Task<bool> SoftDeleteAsync(Expression<Func<T, bool>> expression);

        //    /// <summary>
        //    /// 异步逻辑删除单个实体（将IsDeleted设为true并更新审计字段）
        //    /// </summary>
        //    /// <param name="entity">要删除的实体</param>
        //    /// <returns>是否成功</returns>
        //    Task<bool> SoftDeleteAsync(T entity);


        //// -----------------------------------------------------------------------
        //// 事务操作（通常在服务层进行管理，但仓储层也可以暴露）
        //// -----------------------------------------------------------------------

        ///// <summary>
        ///// 获取当前仓储的 SqlSugar 客户端对象，用于执行复杂操作或事务管理
        ///// </summary>
        ///// <returns>ISqlSugarClient 实例</returns>
        //ISqlSugarClient GetDbClient();
    }
    }
