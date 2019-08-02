namespace Entitas {

    public static class CollectorContextExtension {

        /// Creates a Collector.
        public static ICollector<TEntity> CreateCollector<TEntity>(
            this IContext<TEntity> context, IMatcher<TEntity> matcher) where TEntity : class, IEntityExt {

            return context.CreateCollector(new TriggerOnEvent<TEntity>(matcher, EGroupEvent.Added));
        }

        /// Creates a Collector.
        public static ICollector<TEntity> CreateCollector<TEntity>(
            this IContext<TEntity> context, params TriggerOnEvent<TEntity>[] triggers) where TEntity : class, IEntityExt {

            var groups = new IGroup<TEntity>[triggers.Length];
            var groupEvents = new EGroupEvent[triggers.Length];

            for (int i = 0; i < triggers.Length; i++) {
                groups[i] = context.GetGroup(triggers[i].matcher);
                groupEvents[i] = triggers[i].EGroupEvent;
            }

            return new Collector<TEntity>(groups, groupEvents);
        }
    }
}
