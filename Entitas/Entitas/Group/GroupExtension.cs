namespace Entitas {

    public static class GroupExtension {

        /// Creates a Collector for this group.
        public static ICollector<TEntity> CreateCollector<TEntity>(this IGroup<TEntity> group, EGroupEvent eGroupEvent = EGroupEvent.Added) where TEntity : class, IEntityExt {
            return new CollectorExt<TEntity>((GroupExt<TEntity>)group, eGroupEvent);
        }
    }
}
