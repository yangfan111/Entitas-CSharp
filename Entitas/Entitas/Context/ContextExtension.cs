namespace Entitas
{
    public static class ContextExtension
    {
        /// Returns all entities matching the specified matcher.
        public static TEntity[] GetEntities<TEntity>(this IContextExt<TEntity> context, IMatcher<TEntity> matcher)
        where TEntity : class, IEntityExt
        {
            return context.GetGroup(matcher).GetEntities();
        }

        /// Creates a new entity and adds copies of all
        /// specified components to it.
        /// If replaceExisting is true it will replace exisintg components.
        public static TEntity CloneEntity<TEntity>(this IContextExt<TEntity> context, IEntityExt entity,
                                                   bool replaceExisting = false, params int[] indices)
        where TEntity : class, IEntityExt
        {
            var target = context.CreateEntity();
            entity.CopyTo(target, replaceExisting, indices);
            return target;
        }
    }
}