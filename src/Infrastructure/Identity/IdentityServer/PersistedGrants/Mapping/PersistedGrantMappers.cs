using AutoMapper;
using IdentityServer4.Models;
using EntitiesPersistedGrant = CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity.Models.PersistedGrant;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.PersistedGrants
{
    /// <summary>
    /// Extension methods to map to/from entity/model for persisted grants.
    /// </summary>
    public static class PersistedGrantMappers
    {
        static PersistedGrantMappers()
        {
            Mapper = new MapperConfiguration(cfg =>cfg.AddProfile<MappingProfile>()).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static PersistedGrant ToModel(this EntitiesPersistedGrant entity)
        {
            return entity == null ? null : Mapper.Map<PersistedGrant>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static EntitiesPersistedGrant ToEntity(this PersistedGrant model)
        {
            return model == null ? null : Mapper.Map<EntitiesPersistedGrant>(model);
        }

        /// <summary>
        /// Updates an entity from a model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="entity">The entity.</param>
        public static void UpdateEntity(this PersistedGrant model, EntitiesPersistedGrant entity)
        {
            Mapper.Map(model, entity);
        }
    }
}