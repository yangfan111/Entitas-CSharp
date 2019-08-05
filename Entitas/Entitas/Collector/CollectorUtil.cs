namespace Entitas
{
    public static class CollectorUtil
    {
        /// Creates a Collector.
        public static ICollector<TEntity> CreateCollector<TEntity>(this IContextExt<TEntity> context,
                                                                   IMatcher<TEntity> matcher)
        where TEntity : class, IEntityExt
        {
            return context.CreateCollector(new MatcherEvent<TEntity>(matcher, EGroupEvent.Added));
        }

        /// Creates a Collector.
        public static ICollector<TEntity> CreateCollector<TEntity>(this IContextExt<TEntity> context,
                                                                   params MatcherEvent<TEntity>[] triggers)
        where TEntity : class, IEntityExt
        {
            var groups      = new IGroup<TEntity>[triggers.Length];
            var groupEvents = new EGroupEvent[triggers.Length];

            for (int i = 0; i < triggers.Length; i++)
            {
                groups[i]      = context.GetGroup(triggers[i].matcher);
                groupEvents[i] = triggers[i].EGroupEvent;
            }

            return new CollectorExt<TEntity>((GroupExt<TEntity>[]) groups, groupEvents);
        }
    }
}