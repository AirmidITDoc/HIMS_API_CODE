using AutoMapper;
using System;
using HIMS.API.Models;
using HIMS.Core;

namespace HIMS.API.Extensions
{
    public static class MappingExtensions
    {
        private static IMapper _mapper;

        public static void InitializeMapper(this IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Utilities
        /// <summary>
        /// This method is used to convert model to enity and entity to model
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object
        /// </summary>
        /// <typeparam name="TSource">Source object type</typeparam>
        /// <typeparam name="TDestination">Destination object type</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <param name="destination">Destination object to map into</param>
        /// <returns>Mapped destination object, same instance as the passed destination object</returns>
        private static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            //use AutoMapper for mapping objects
            return _mapper.Map(source, destination);
        }

        #endregion

        #region Methods

        #region Model-Entity mapping

        /// <summary>
        /// Execute a mapping from the entity to the existing model
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="entity">Entity to map from</param>
        /// <param name="model">Model to map into</param>
        /// <returns>Mapped model</returns>
        public static TModel ToModel<TEntity, TModel>(this TEntity entity, TModel model)
            where TEntity : class where TModel : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return entity.MapTo(model);
        }

        /// <summary>
        /// Execute a mapping from the model to the existing entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="model">Model to map from</param>
        /// <param name="entity">Entity to map into</param>
        /// <returns>Mapped entity</returns>
        public static TEntity ToEntity<TEntity, TModel>(this TModel model, TEntity entity)
            where TEntity : class where TModel : class
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return model.MapTo(entity);
        }
        //public static TModel ToModel<TModel>(this object entity) where TModel : BaseModel
        //{
        //    if (entity == null)
        //        throw new ArgumentNullException(nameof(entity));
        //    return _mapper.Map<TModel>(entity);
        //}

        ///// <summary>
        ///// Execute a mapping from the model to a new entity
        ///// </summary>
        ///// <typeparam name="TEntity">Entity type</typeparam>
        ///// <param name="model">Model to map from</param>
        ///// <returns>Mapped entity</returns>
        //public static TEntity ToEntity<TEntity>(this object model) where TEntity : BaseEntity
        //{
        //    if (model == null)
        //        throw new ArgumentNullException(nameof(model));

        //    return _mapper.Map<TEntity>(model);
        //}

        #endregion

        #endregion
    }
}
