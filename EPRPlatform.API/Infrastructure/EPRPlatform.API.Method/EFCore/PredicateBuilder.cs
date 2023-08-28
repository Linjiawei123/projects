using System.Linq.Expressions;
using EPRPlatform.API.Models;

namespace EPRPlatform.API.Method.EFCore
{
    /// <summary>
    /// PredicateBuilder
    /// </summary>
    public static class PredicateBuilder
    {
        /// <summary>
        /// 条件true
        /// </summary>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        /// <summary>
        /// 条件false
        /// </summary>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        /// <summary>
        /// 或条件
        /// </summary>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }
        /// <summary>
        /// 和条件
        /// </summary>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }
        /// <summary>
        /// 返回软删除后的数据
        /// </summary>
        public static IQueryable<TEntity> NotDeleted<TEntity>(this IQueryable<TEntity> queryable) where TEntity : class, IDeletable
        {
            return queryable.Where(e => !((IDeletable)e).IsDeleted);
        }

        #region 自定义方法
        ///// <summary>
        ///// 软删除
        ///// </summary>
        //public static void SoftDelete<TEntity>(this TEntity entity) where TEntity : class, IDeletable
        //{
        //    entity.IsDeleted = true;
        //    Entry(entity).Property(q => q.IsDeleted).IsModified = true;
        //}
        #endregion
    }
}
