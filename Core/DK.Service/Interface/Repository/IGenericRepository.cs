using DK.Domain.Entity.Base;
using DK.Domain.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DK.Service.Interface.Repository
{
    public interface IGenericRepository<TEntity> where TEntity :class, IEntity
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        IEnumerable<TViewModel> Get<TViewModel>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        IEnumerable<TEntity> Get(
            ref int listCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int page = 0,
            int pageSize = 0);

        bool Any(Expression<Func<TEntity, bool>> filter = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null);

        IEnumerable<TViewModel> Get<TViewModel>(
            ref int listCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int page = 0,
            int pageSize = 0);


        IEnumerable<TEntity> Get<TValue>(
            ref int listCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TValue>> groupBy = null,
            int take = 0,
            int limit = 0);

        IEnumerable<TViewModel> Get<TValue, TViewModel>(
            ref int listCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TValue>> groupBy = null,
            int take = 0,
            int limit = 0);

        TEntity GetByID(object id);
        TViewModel GetFirst<TViewModel>(Expression<Func<TEntity, bool>> xWherePredicate);

        TEntity GetFirst(Expression<Func<TEntity, bool>> xWherePredicate);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate, IEnumerable<Expression<Func<TEntity, object>>> ExcludePropertiesFromUpdate = null);
        void Update<TViewModel>(TEntity entityToUpdate);
        void UpdateWithRefrenceProperties(TEntity entityToUpdate, IEnumerable<Expression<Func<TEntity, object>>> ExcludePropertiesFromUpdate = null);
        void UpdateProperty<T>(Expression<Func<TEntity, bool>> Filter, IEnumerable<object> UpdatedSet, string PropertyName);

        #region --AsyncFunctions--
        Task<List<TEntity>> GetAsync(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "");


        Task<List<TViewModel>> GetAsync<TViewModel>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);


        Task<PagingResult<TEntity>> GetAsync(
            bool needDBCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int page = 0,
            int pageSize = 0);

        Task<PagingResult<TViewModel>> GetAsync<TViewModel>(
            bool needDBCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int page = 0,
            int pageSize = 0);

        Task DeleteAsync(object id);

        Task<PagingResult<TEntity>> GetAsync<TValue>(
            bool needDBCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TValue>> groupBy = null,
            int take = 0,
            int limit = 0);

        Task<PagingResult<TViewModel>> GetAsync<TValue, TViewModel>(
            bool needDBCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TValue>> groupBy = null,
            int take = 0,
            int limit = 0);

        Task InsertAsync(TEntity entity);

        Task<TEntity> GetByIDAsync(object id);
        Task<TViewModel> GetFirstAsync<TViewModel>(Expression<Func<TEntity, bool>> xWherePredicate);
        #endregion
    }
}
