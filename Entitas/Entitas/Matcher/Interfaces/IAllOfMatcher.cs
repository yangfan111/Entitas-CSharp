namespace Entitas {

    public interface IAllOfMatcher<TEntity> : IMatcher<TEntity> where TEntity : class, IEntityExt {

        IMatcher<TEntity> NoneOf(params int[] indices);
        IMatcher<TEntity> NoneOf(params IMatcher<TEntity>[] matchers);
        
        IMatcher<TEntity> AnyOf(params int[] indices);
        IMatcher<TEntity> AnyOf(params IMatcher<TEntity>[] matchers);
        
        int[] AllOfIndices  { get; }
        int[] AnyOfIndices  { get; }
        int[] NoneOfIndices { get; }
        
    }
}
