using AutoMapper;
using AutoMapper.QueryableExtensions;
using DK.Data.EF.Context;
using DK.Domain.Entity.Base;
using DK.Service.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DK.Domain.DTO.Base;
using System.Reflection;

namespace DK.Data.EF.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        #region == Ctor And Init Context ==
        internal ApplicationDBContext context;
        internal DbSet<TEntity> dbSet;
        internal IMapper _mapper;

        public GenericRepository(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
            _mapper = mapper;

        }
        #endregion

        #region == Sync Function ==
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                return query.Any(filter);

            return query.Any();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                return await query.AnyAsync(filter);

            return await query.AnyAsync();
        }

        public virtual IEnumerable<TViewModel> Get<TViewModel>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            //TODO includeProperties
            //foreach (var includeProperty in includeProperties.Split
            //    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    query = query.Include(includeProperty);
            //}

            if (orderBy != null)
                return orderBy(query).ProjectTo<TViewModel>(_mapper.ConfigurationProvider);
            else
                return query.ProjectTo<TViewModel>(_mapper.ConfigurationProvider);
        }

        public virtual IQueryable<TEntity> Query(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        public virtual IEnumerable<TEntity> Get(
            ref int listCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int page = 0,
            int pageSize = 0)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (listCount != -1)
                listCount = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            if (page != 0 && pageSize != 0)
                query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query.AsNoTracking();
        }

        public virtual IEnumerable<TViewModel> Get<TViewModel>(
            ref int listCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int page = 0,
            int pageSize = 0)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (listCount != -1) { listCount = query.Count(); }

            //foreach (var includeProperty in includeProperties.Split
            //    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    query = query.Include(includeProperty);
            //}

            if (orderBy != null)
                query = orderBy(query);

            if (page != 0 && pageSize != 0)
                query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query.AsNoTracking().ProjectTo<TViewModel>(_mapper.ConfigurationProvider);
        }

        public virtual IEnumerable<TEntity> Get<TValue>(
            ref int listCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TValue>> groupBy = null,
            int take = 0,
            int limit = 0
    )
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (listCount != -1)
                listCount = query.Count();

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (groupBy != null)
            {

                query = query.GroupBy(groupBy).SelectMany(p => p.Take(take));
            }

            if (limit != 0)
            {
                query = query.Take(limit);
            }
            return query.AsNoTracking();
        }

        public virtual IEnumerable<TViewModel> Get<TValue, TViewModel>(
            ref int listCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TValue>> groupBy = null,
            int take = 0,
            int limit = 0
    )
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (listCount != -1)
                listCount = query.Count();

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (groupBy != null)
            {

                query = query.GroupBy(groupBy).SelectMany(p => p.Take(take));
            }

            if (limit != 0)
            {
                query = query.Take(limit);
            }
            return query.AsNoTracking().ProjectTo<TViewModel>(_mapper.ConfigurationProvider);
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TViewModel GetFirst<TViewModel>(Expression<Func<TEntity, bool>> xWherePredicate)
        {
            return dbSet.Where(xWherePredicate).ProjectTo<TViewModel>(_mapper.ConfigurationProvider).FirstOrDefault();
        }

        public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> xWherePredicate)
        {
            return dbSet.FirstOrDefault(xWherePredicate);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate, IEnumerable<Expression<Func<TEntity, object>>> ExcludePropertiesFromUpdate = null)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            foreach (var xCurExcludeColumn in ExcludePropertiesFromUpdate ?? new List<Expression<Func<TEntity, object>>>())
            {
                context.Entry(entityToUpdate).Property(xCurExcludeColumn).IsModified = false;
            }
        }

        public virtual void Update<TViewModel>(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;

            var xValidEntityProp = GetSystemType(typeof(TEntity).GetProperties());
            var xValidxViewModelProp = GetSystemType(typeof(TViewModel).GetProperties());
            var xDiffrentProp = xValidEntityProp.Except(xValidxViewModelProp, new PropertyNameComparer());
            var xExculdePropList = GetNullType(xDiffrentProp, entityToUpdate).Select(i => i.Name);

            foreach (var xExcColumn in xExculdePropList)
                context.Entry(entityToUpdate).Property(xExcColumn).IsModified = false;
        }

        public virtual void UpdateWithRefrenceProperties(TEntity entityToUpdate, IEnumerable<Expression<Func<TEntity, object>>> ExcludePropertiesFromUpdate = null)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            foreach (var xCurExcludeColumn in ExcludePropertiesFromUpdate ?? new List<Expression<Func<TEntity, object>>>())
            {
                //string xPropertyName = context.Entry(entityToUpdate).Property(xCurExcludeColumn).Name;
                //PropertyInfo xPropertyInfo = entityToUpdate.GetType().GetProperty(xPropertyName);
                //Type xPropertyType = xPropertyInfo.PropertyType;

                //if (xPropertyType.IsValueType)
                //{
                //    context.Entry(entityToUpdate).Property(xCurExcludeColumn).OriginalValue = Activator.CreateInstance(xPropertyType);
                //}
                //else if (xPropertyType == typeof(string))
                //{
                //    context.Entry(entityToUpdate).Property(xCurExcludeColumn).OriginalValue = "-";
                //}
                //else
                //    context.Entry(entityToUpdate).Property(xCurExcludeColumn).OriginalValue = null;

                context.Entry(entityToUpdate).Property(xCurExcludeColumn).IsModified = false;

            }
            context.SaveChanges();
        }

        public virtual void UpdateProperty<T>(Expression<Func<TEntity, bool>> Filter,
                                   IEnumerable<object> UpdatedSet, // Updated many-to-many relationships
                                   string PropertyName) // The name of the navigation property
        {
            // Get the generic type of the set
            var type = UpdatedSet.GetType().GetGenericArguments()[0];

            var previous = context.Set<TEntity>().Include(PropertyName).FirstOrDefault(Filter);


            /* Create a container that will hold the values of
                * the generic many-to-many relationships we are updating.
                */
            var values = CreateList(type);

            /* For each object in the updated set find the existing
                 * entity in the database. This is to avoid Entity Framework
                 * from creating new objects or throwing an
                 * error because the object is already attached.
                 */
            //TODO: fix!
            //foreach (var entry in UpdatedSet
            //    .Select(obj => (T)obj
            //        .GetType()
            //        .GetProperty("xID")
            //        .GetValue(obj, null))
            //    .Select(value => context.Set() .Find(value)))
            //{
            //    values.Add(entry);
            //}

            /* Get the collection where the previous many to many relationships
                * are stored and assign the new ones.
                */
            context.Entry(previous).Collection(PropertyName).CurrentValue = values;
        }

        public IList CreateList(Type type)
        {
            var genericList = typeof(List<>).MakeGenericType(type);
            return (IList)Activator.CreateInstance(genericList);
        }
        #endregion

        #region == AsyncFunctions ==
        public virtual async Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }


        public virtual async Task<List<TViewModel>> GetAsync<TViewModel>(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //foreach (var includeProperty in includeProperties.Split
            //    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    query = query.Include(includeProperty);
            //}

            if (orderBy != null)
            {
                return await orderBy(query).ProjectTo<TViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            }
            else
            {
                return await query.ProjectTo<TViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            }
        }


        public virtual async Task<PagingResult<TEntity>> GetAsync(
            bool needDBCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int page = 0,
            int pageSize = 0)
        {
            IQueryable<TEntity> query = dbSet;
            PagingResult<TEntity> pagingGenericResult = new PagingResult<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (needDBCount) { pagingGenericResult.xCount = query.Count(); }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (page != 0 && pageSize != 0)
            {
                query = query.Skip((page - 1) * pageSize).Take(pageSize);
            }
            pagingGenericResult.xData = await query.AsNoTracking().ToListAsync();
            return pagingGenericResult;
        }


        public virtual async Task<PagingResult<TViewModel>> GetAsync<TViewModel>(
    bool needDBCount,
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    int page = 0,
    int pageSize = 0)
        {
            IQueryable<TEntity> query = dbSet;
            PagingResult<TViewModel> pagingGenericResult = new PagingResult<TViewModel>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (needDBCount) { pagingGenericResult.xCount = query.Count(); }

            //foreach (var includeProperty in includeProperties.Split
            //    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    query = query.Include(includeProperty);
            //}

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (page != 0 && pageSize != 0)
            {
                query = query.Skip((page - 1) * pageSize).Take(pageSize);
            }
            pagingGenericResult.xData = await query.AsNoTracking().ProjectTo<TViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            return pagingGenericResult;
        }



        public virtual async Task<PagingResult<TEntity>> GetAsync<TValue>(
          bool needDBCount,
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Expression<Func<TEntity, TValue>> groupBy = null,
        int take = 0,
        int limit = 0
    )
        {
            PagingResult<TEntity> pagingGenericResult = new PagingResult<TEntity>();
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (needDBCount) { pagingGenericResult.xCount = query.Count(); }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (groupBy != null)
            {

                query = query.GroupBy(groupBy).SelectMany(p => p.Take(take));
            }

            if (limit != 0)
            {
                query = query.Take(limit);
            }
            pagingGenericResult.xData = await query.AsNoTracking().ToListAsync();
            return pagingGenericResult;
        }

        public virtual async Task<PagingResult<TViewModel>> GetAsync<TValue, TViewModel>(
          bool needDBCount,
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Expression<Func<TEntity, TValue>> groupBy = null,
        int take = 0,
        int limit = 0
    )
        {
            PagingResult<TViewModel> pagingGenericResult = new PagingResult<TViewModel>();
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (needDBCount) { pagingGenericResult.xCount = query.Count(); }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (groupBy != null)
            {

                query = query.GroupBy(groupBy).SelectMany(p => p.Take(take));
            }

            if (limit != 0)
            {
                query = query.Take(limit);
            }
            pagingGenericResult.xData = await query.AsNoTracking().ProjectTo<TViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            return pagingGenericResult;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        // public virtual IEnumerable<TEntity> Get<TValue>(
        //      ref int listCount,
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    Expression<Func<TEntity, TValue>> groupBy = null,
        //    Expression<Func<TEntity, TValue>> selector
        //)
        // {
        //     IQueryable<TEntity> query = dbSet;

        //     if (filter != null)
        //     {
        //         query = query.Where(filter);
        //     }
        //     if (listCount != -1)
        //         listCount = query.Count();

        //     if (orderBy != null)
        //     {
        //         query = orderBy(query);
        //     }
        //     if (groupBy != null)
        //     {
        //         query = query.GroupBy(groupBy).Select(selector);
        //     }
        //     return query;
        // }


        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual async Task<TViewModel> GetFirstAsync<TViewModel>(Expression<Func<TEntity, bool>> xWherePredicate)
        {
            return await dbSet.Where(xWherePredicate).ProjectTo<TViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }
        #endregion

        #region == Other Function ==
        private List<PropertyInfo> GetSystemType(IEnumerable<PropertyInfo> xPropertyInfoList)
        {
            List<PropertyInfo> xSystemProperty = new List<PropertyInfo>();
            foreach (var xProperty in xPropertyInfoList)
            {
                string xTypeName = "";
                if (xProperty.PropertyType.IsGenericType)
                {
                    Type xGenericType = xProperty.PropertyType.GetGenericArguments().FirstOrDefault();
                    if (xGenericType != null)
                        xTypeName = xGenericType.FullName;
                    else
                        continue;
                }
                else
                    xTypeName = xProperty.PropertyType.FullName;

                bool xIsSystemType = xTypeName.StartsWith("System", StringComparison.OrdinalIgnoreCase);

                if (xIsSystemType)
                    xSystemProperty.Add(xProperty);
            }
            return xSystemProperty;
        }

        private List<PropertyInfo> GetNullType(IEnumerable<PropertyInfo> xPropertyList, object xEntity)
        {
            List<PropertyInfo> xNullValueProperty = new List<PropertyInfo>();
            object obj = (object)xEntity;
            foreach (var xProperty in xPropertyList)
            {
                object xPropertyValue = xProperty.GetValue(obj, null);
                var xPropertyType = xProperty.PropertyType;
                var xDefaultValue = GetDefaultValue(xPropertyType);

                if (xPropertyValue == null || xPropertyValue.Equals(xDefaultValue))
                    xNullValueProperty.Add(xProperty);
            }
            return xNullValueProperty;
        }

        private object GetDefaultValue(Type xType)
        {
            if (xType.IsValueType && Nullable.GetUnderlyingType(xType) == null)
                return Activator.CreateInstance(xType);
            else
                return null;
        }
        #endregion
    }

    internal class PropertyNameComparer : IEqualityComparer<PropertyInfo>
    {
        public bool Equals(PropertyInfo xLeft, PropertyInfo xRight)
        {
            if (string.Equals(xLeft.Name, xRight.Name, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        public int GetHashCode(PropertyInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}