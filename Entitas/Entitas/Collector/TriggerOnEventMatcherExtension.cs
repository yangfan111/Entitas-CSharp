namespace Entitas {

    public static class TriggerOnEventMatcherExtension {

        public static TriggerOnEvent<TEntity> Added<TEntity>(this IMatcher<TEntity> matcher) where TEntity : class, IEntityExt {
            return new TriggerOnEvent<TEntity>(matcher, EGroupEvent.Added);
        }

        public static TriggerOnEvent<TEntity> Removed<TEntity>(this IMatcher<TEntity> matcher) where TEntity : class, IEntityExt {
            return new TriggerOnEvent<TEntity>(matcher, EGroupEvent.Removed);
        }

        public static TriggerOnEvent<TEntity> AddedOrRemoved<TEntity>(this IMatcher<TEntity> matcher) where TEntity : class, IEntityExt {
            return new TriggerOnEvent<TEntity>(matcher, EGroupEvent.AddedOrRemoved);
        }
    }
}
