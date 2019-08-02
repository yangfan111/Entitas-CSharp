namespace Entitas {

    public interface IMatcher<TEntity> where TEntity : class, IEntityExt {

        int[] Indices { get; }
        bool Matches(TEntity entity);
    }
}
