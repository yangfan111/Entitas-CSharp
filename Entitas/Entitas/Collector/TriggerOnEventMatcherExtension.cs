namespace Entitas {

    public static class TriggerOnEventMatcherExtension {

        public static MatcherEvent<TEntity> Added<TEntity>(this IMatcher<TEntity> matcher) where TEntity : class, IEntityExt {
            return new MatcherEvent<TEntity>(matcher, EGroupEvent.Added);
        }

        public static MatcherEvent<TEntity> Removed<TEntity>(this IMatcher<TEntity> matcher) where TEntity : class, IEntityExt {
            return new MatcherEvent<TEntity>(matcher, EGroupEvent.Removed);
        }

        public static MatcherEvent<TEntity> AddedOrRemoved<TEntity>(this IMatcher<TEntity> matcher) where TEntity : class, IEntityExt {
            return new MatcherEvent<TEntity>(matcher, EGroupEvent.AddedOrRemoved);
        }
    }
}
