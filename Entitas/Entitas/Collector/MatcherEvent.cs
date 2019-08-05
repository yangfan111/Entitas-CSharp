namespace Entitas {

    public struct MatcherEvent<TEntity> where TEntity : class, IEntityExt {

        public readonly IMatcher<TEntity> matcher;
        public readonly EGroupEvent EGroupEvent;

        public MatcherEvent(IMatcher<TEntity> matcher, EGroupEvent eGroupEvent) {
            this.matcher = matcher;
            this.EGroupEvent = eGroupEvent;
        }
    }
}
