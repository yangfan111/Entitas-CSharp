namespace Entitas {

    public struct TriggerOnEvent<TEntity> where TEntity : class, IEntityExt {

        public readonly IMatcher<TEntity> matcher;
        public readonly EGroupEvent EGroupEvent;

        public TriggerOnEvent(IMatcher<TEntity> matcher, EGroupEvent eGroupEvent) {
            this.matcher = matcher;
            this.EGroupEvent = eGroupEvent;
        }
    }
}
