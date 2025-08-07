using MricoServices.Repository.IRepository;
using MricoServices.Shared;
using MricoServices.Shared.ApiResult;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MricoServices.Repository.Repository
{
    public class BaseRepository<T> : SimpleClient<T>, IBaseRepository<T> where T : class, new()
    {
        public BaseRepository(ISqlSugarClient db)
        {
            base.Context = db;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(T entity)
        {
            return await Context.Insertable(entity).ExecuteCommandAsync();
        }
        /// <summary>
        /// 真删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> DeleteAsync(int id)
        {
            // 使用 Deleteable().In(id) 根据ID进行删除
            return await Context.Deleteable<T>().In(id).ExecuteCommandAsync();
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> SoftDeleteAsync(int id)
        {
            if (!typeof(AuditableEntity).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException($"实体 {typeof(T).Name} 未实现 AuditableEntity，无法进行软删除。");
            }

            var entity = await Context.Queryable<T>().In(id).FirstAsync();

            if (entity == null)
            {
                return 0;
            }

            if (entity is AuditableEntity auditableEntity)
            {
                if (auditableEntity.IsDeleted)
                {
                    return 1;
                }
                auditableEntity.IsDeleted = true;
                return await UpdateAsync(entity);
            }
            return 0;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ISugarQueryable<T> GetAll()
        {
            if (typeof(AuditableEntity).IsAssignableFrom(typeof(T)))
            {
                // **这是修复后的关键改动：使用表达式树构建器来安全地访问 IsDeleted**
                // 1. 获取 T 类型的参数表达式
                ParameterExpression parameter = Expression.Parameter(typeof(T), "it");

                // 2. 将 T 类型参数转换为 AuditableEntity 类型
                Expression converted = Expression.Convert(parameter, typeof(AuditableEntity));

                // 3. 访问 AuditableEntity 上的 IsDeleted 属性
                MemberExpression property = Expression.Property(converted, nameof(AuditableEntity.IsDeleted));

                // 4. 构建 == false 的比较表达式
                ConstantExpression constant = Expression.Constant(false);
                BinaryExpression body = Expression.Equal(property, constant);

                // 5. 创建完整的 Lambda 表达式
                Expression<Func<T, bool>> whereExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

                // 6. 应用到 Queryable
                return base.Context.Queryable<T>().Where(whereExpression);
            }
            else
            {
                // 如果 T 不是 AuditableEntity，则不进行软删除过滤
                return base.Context.Queryable<T>();
            }
        }

        /// <summary>
        /// 通过Id进行查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await base.Context.Queryable<T>().In(id).FirstAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T entity)
        {
            return await Context.Updateable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据指定属性的前缀查询实体列表，不包含软删除过滤。
        /// 主要用于在添加操作前检查编号或生成编号时使用。
        /// </summary>
        /// <typeparam name="TProperty">要查询的属性的类型，通常是string。</typeparam>
        /// <param name="propertyName">实体中表示编号的属性名称，例如 "OrderCode" 或 "WorkOrderNumber"。</param>
        /// <param name="prefix">要匹配的前缀，例如 "GDBH"。</param>
        /// <returns>可查询的ISugarQueryable<T>，可以在此基础上继续添加条件。</returns>
        public ISugarQueryable<T> GetByPrefixWithoutSoftDelete<TProperty>(string propertyName, string prefix)
        {
            // 构建一个 Lambda 表达式： it => it.PropertyName.StartsWith(prefix)
            ParameterExpression parameter = Expression.Parameter(typeof(T), "it");

            // 访问属性 it.PropertyName
            MemberExpression property = Expression.Property(parameter, propertyName);

            // 确保 TProperty 是 string 类型，否则 StartsWith 方法不适用
            if (typeof(TProperty) != typeof(string))
            {
                throw new InvalidOperationException($"'{propertyName}' 属性的类型不是字符串，无法使用 StartsWith 进行前缀匹配。");
            }
            System.Reflection.MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

            // 构建调用 StartsWith 方法的表达式： it.PropertyName.StartsWith(prefix)
            MethodCallExpression callStartsWith = Expression.Call(property, startsWithMethod, Expression.Constant(prefix));

            // 构建完整的 Lambda 表达式： it => it.PropertyName.StartsWith(prefix)
            Expression<Func<T, bool>> whereExpression = Expression.Lambda<Func<T, bool>>(callStartsWith, parameter);

            // 应用前缀过滤条件，不添加 IsDeleted 过滤
            return base.Context.Queryable<T>().Where(whereExpression);
        }














        ///// <summary>
        ///// 异步插入单个实体并返回受影响行数
        ///// </summary>
        ///// <param name="entity">要插入的实体</param>
        ///// <returns>受影响行数</returns>
        //public async Task<int> AddAsync(T entity)
        //{
        //    // SimpleClient 的 InsertAsync 返回 int (受影响行数)
        //    // return await base.InsertAsync(entity);
        //    // 或者直接使用 Context
        //     return await Context.Insertable(entity).ExecuteCommandAsync();
        //}

        ///// <summary>
        ///// 异步批量插入实体并返回受影响行数
        ///// </summary>
        ///// <param name="entities">要插入的实体集合</param>
        ///// <returns>受影响行数</returns>
        //public async Task<int> AddRangeAsync(IEnumerable<T> entities)
        //{
        //    // SimpleClient 的 InsertRangeAsync 返回 int (受影响行数)
        //    //return await base.InsertRangeAsync(entities.ToList()); // 确保传入 List
        //    // 或者直接使用 Context
        //     return await Context.Insertable(entities.ToList()).ExecuteCommandAsync();
        //}

        ///// <summary>
        ///// 异步插入单个实体并返回自增ID
        ///// </summary>
        ///// <param name="entity">要插入的实体</param>
        ///// <returns>自增ID</returns>
        //public async Task<int> AddReturnIdentityAsync(T entity)
        //{
        //    // SimpleClient 的 InsertReturnIdentityAsync 返回 int
        //    //return await base.InsertReturnIdentityAsync(entity);
        //    // 或者直接使用 Context
        //     return await Context.Insertable(entity).ExecuteReturnIdentityAsync();
        //}

        //// -----------------------------------------------------------------------
        //// 查询操作
        //// -----------------------------------------------------------------------

        ///// <summary>
        ///// 异步根据ID查询单个实体
        ///// </summary>
        ///// <param name="id">实体ID</param>
        ///// <returns>匹配的实体，如果未找到则为 null</returns>
        //public async Task<T> GetByIdAsync(object id)
        //{
        //    return await base.GetByIdAsync(id); // SimpleClient 提供了此方法
        //}

        ///// <summary>
        ///// 异步查询所有实体列表
        ///// </summary>
        ///// <returns>所有实体列表</returns>
        //public async Task<List<T>> GetListAsync()
        //{
        //    return await base.GetListAsync(); // SimpleClient 提供了此方法
        //}

        ///// <summary>
        ///// 异步根据条件查询实体列表
        ///// </summary>
        ///// <param name="expression">查询条件</param>
        ///// <returns>符合条件的实体列表</returns>
        //public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression)
        //{
        //    return await base.GetListAsync(expression); // SimpleClient 提供了此方法
        //}

        ///// <summary>
        ///// 异步查询单条记录，结果集不能超过1，否则会抛出错误
        ///// </summary>
        ///// <param name="expression">查询条件</param>
        ///// <returns>符合条件的单个实体</returns>
        //public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        //{
        //    return await base.GetSingleAsync(expression); // SimpleClient 提供了此方法
        //}

        ///// <summary>
        ///// 异步查询第一条记录
        ///// </summary>
        ///// <param name="expression">查询条件</param>
        ///// <returns>符合条件的第一条实体</returns>
        //public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression)
        //{
        //    return await base.GetFirstAsync(expression); // SimpleClient 提供了此方法
        //}

        ///// <summary>
        ///// 异步分页查询
        ///// </summary>
        ///// <param name="expression">查询条件</param>
        ///// <param name="page">分页模型</param>
        ///// <returns>分页结果列表</returns>
        //public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> expression, PageModel page)
        //{
        //    return await base.GetPageListAsync(expression, page); // SimpleClient 提供了此方法
        //}

        ///// <summary>
        ///// 异步分页查询（带排序）
        ///// </summary>
        ///// <param name="expression">查询条件</param>
        ///// <param name="page">分页模型</param>
        ///// <param name="orderByExpression">排序表达式</param>
        ///// <param name="orderByType">排序类型</param>
        ///// <returns>分页结果列表</returns>
        //public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> expression, PageModel page, Expression<Func<T, object>> orderByExpression, OrderByType orderByType = OrderByType.Asc)
        //{
        //    return await base.GetPageListAsync(expression, page, orderByExpression, orderByType); // SimpleClient 提供了此方法
        //}

        ///// <summary>
        ///// 异步使用 Queryable 进行复杂查询（返回 Queryable 对象以链式调用）
        ///// </summary>
        ///// <returns>ISugarQueryable 对象</returns>
        //public ISugarQueryable<T> AsQueryable()
        //{
        //    return base.AsQueryable(); // SimpleClient 提供了此方法
        //}

        //// -----------------------------------------------------------------------
        //// 更新操作
        //// -----------------------------------------------------------------------

        ///// <summary>
        ///// 异步更新单个实体（需要有主键）
        ///// </summary>
        ///// <param name="entity">要更新的实体</param>
        ///// <returns>是否更新成功</returns>
        //public async Task<bool> UpdateAsync(T entity)
        //{
        //    // SimpleClient 的 UpdateAsync 返回 bool
        //    return await base.UpdateAsync(entity);
        //    // 或者使用 Context.Updateable().ExecuteCommandAsync() > 0
        //    // return await Context.Updateable(entity).ExecuteCommandAsync() > 0;
        //}

        ///// <summary>
        ///// 异步批量更新实体（需要有主键）
        ///// </summary>
        ///// <param name="entities">要更新的实体集合</param>
        ///// <returns>是否更新成功</returns>
        //public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities)
        //{
        //    // SimpleClient 的 UpdateRangeAsync 返回 bool
        //    return await base.UpdateRangeAsync(entities.ToList()); // 确保传入 List
        //    // 或者使用 Context.Updateable().ExecuteCommandAsync() > 0
        //    // return await Context.Updateable(entities.ToList()).ExecuteCommandAsync() > 0;
        //}

        ///// <summary>
        ///// 异步根据表达式更新指定列
        ///// </summary>
        ///// <param name="columns">更新的列和值</param>
        ///// <param name="whereExpression">更新条件</param>
        ///// <returns>受影响行数</returns>
        //public async Task<int> UpdateColumnsAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        //{
        //    // Context.Updateable().SetColumns().Where().ExecuteCommandAsync() 返回 int
        //    return await Context.Updateable<T>().SetColumns(columns).Where(whereExpression).ExecuteCommandAsync();
        //}

        //// -----------------------------------------------------------------------
        //// 删除操作
        //// -----------------------------------------------------------------------

        ///// <summary>
        ///// 异步删除单个实体（需要有主键）
        ///// </summary>
        ///// <param name="entity">要删除的实体</param>
        ///// <returns>是否删除成功</returns>
        //public async Task<bool> DeleteAsync(T entity)
        //{
        //    // SimpleClient 的 DeleteAsync 返回 bool
        //    return await base.DeleteAsync(entity);
        //    // 或者使用 Context.Deleteable().ExecuteCommandAsync() > 0
        //    // return await Context.Deleteable<T>().Where(entity).ExecuteCommandAsync() > 0;
        //}

        ///// <summary>
        ///// 异步根据ID删除实体
        ///// </summary>
        ///// <param name="id">实体ID</param>
        ///// <returns>受影响行数</returns>
        //public async Task<int> DeleteByIdAsync(object id)
        //{
        //    // SimpleClient 的 DeleteByIdAsync 返回 int
        //    //return await base.DeleteByIdAsync(id);
        //    // 或者使用 Context.Deleteable().ExecuteCommandAsync()
        //     return await Context.Deleteable<T>().In(id).ExecuteCommandAsync();
        //}

        ///// <summary>
        ///// 异步根据一组ID批量删除实体
        ///// </summary>
        ///// <param name="ids">实体ID数组</param>
        ///// <returns>受影响行数</returns>
        //public async Task<int> DeleteByIdsAsync(object[] ids)
        //{
        //    // SimpleClient 的 DeleteByIdsAsync 返回 int
        //    //return await base.DeleteByIdsAsync(ids);
        //    // 或者使用 Context.Deleteable().ExecuteCommandAsync()
        //     return await Context.Deleteable<T>().In(ids).ExecuteCommandAsync();
        //}

        ///// <summary>
        ///// 异步根据条件删除实体
        ///// </summary>
        ///// <param name="expression">删除条件</param>
        ///// <returns>受影响行数</returns>
        //public async Task<int> DeleteAsync(Expression<Func<T, bool>> expression)
        //{
        //    // SimpleClient 的 DeleteAsync(expression) 返回 int
        //    //return await base.DeleteAsync(expression);
        //    // 或者使用 Context.Deleteable().ExecuteCommandAsync()
        //    return await Context.Deleteable<T>().Where(expression).ExecuteCommandAsync();
        //}

        ///// <summary>
        ///// 异步逻辑删除实体（将IsDeleted设为true并更新审计字段）
        ///// </summary>
        ///// <param name="expression">删除条件</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> SoftDeleteAsync(Expression<Func<T, bool>> expression)
        //{
        //    // 确保 IsDeleted 字段存在于 AuditableEntity
        //    if (typeof(AuditableEntity).IsAssignableFrom(typeof(T)))
        //    {
        //        // 通过 Updateable 来更新 IsDeleted 字段为 true
        //        return await Context.Updateable<T>()
        //                            .SetColumns(it => ((AuditableEntity)(object)it).IsDeleted == true)
        //                            .Where(expression)
        //                            .ExecuteCommandAsync() > 0;
        //        // 注意：这里的 AOP 逻辑会自动填充 UpdatedAt 等，但对于 DeletedAt，如果需要，
        //        // 你可能需要确保 AOP 也能捕获到这种 SetColumns 方式的更新并填充 DeletedAt。
        //        // 更好的方式是像之前 AOP 那样，直接操作实体后调用 Update
        //    }
        //    else
        //    {
        //        throw new NotSupportedException("This entity does not support soft delete.");
        //    }
        //}
        ///// <summary>
        ///// 异步逻辑删除单个实体（将IsDeleted设为true并更新审计字段）
        ///// </summary>
        ///// <param name="entity">要删除的实体</param>
        ///// <returns>是否成功</returns>
        //public async Task<bool> SoftDeleteAsync(T entity)
        //{
        //    if (entity is AuditableEntity auditableEntity)
        //    {
        //        auditableEntity.IsDeleted = true;
        //        // 此时 AOP 拦截器中的 DataFilterType.UpdateByObject 逻辑会生效
        //        // 填充 DeletedAt, DeletedBy 等字段
        //        return await base.UpdateAsync(entity); // 调用更新方法，让 AOP 逻辑处理
        //    }
        //    else
        //    {
        //        throw new NotSupportedException("This entity does not support soft delete.");
        //    }
        //}


        //// -----------------------------------------------------------------------
        //// 事务操作（通常在服务层进行管理，但仓储层也可以暴露）
        //// -----------------------------------------------------------------------

        ///// <summary>
        ///// 获取当前仓储的 SqlSugar 客户端对象，用于执行复杂操作或事务管理
        ///// </summary>
        ///// <returns>ISqlSugarClient 实例</returns>
        //public ISqlSugarClient GetDbClient()
        //{
        //    return base.Context; // 直接返回基类的 Context 属性
        //}
    }
}
